using AuctionService.Data;
using AuctionService.DTOs.Auctions;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers.Auctions;

[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    private readonly AuctionDbContext _auctionDbContext;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuctionsController(AuctionDbContext auctionDbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _auctionDbContext = auctionDbContext;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDTO>>> GetAll(string date)
    {
        var query = _auctionDbContext.Auctions.OrderBy(x => x.Item.Make).AsQueryable();
        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }
        return await query.ProjectTo<AuctionDTO>(_mapper.ConfigurationProvider).ToListAsync();
        //var auctions = await _auctionDbContext.Auctions.Include(x => x.Item).AsNoTracking().ToListAsync();
        //var result = _mapper.Map<List<AuctionDTO>>(auctions);
        //return Ok(result);
    }

    [HttpGet("{id:Guid}", Name = "GetAuction")]
    public async Task<ActionResult<AuctionDTO>> GetAuction(Guid id)
    {
        var auction = await _auctionDbContext.Auctions.Include(x => x.Item).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (auction == null)
        {
            return NotFound();
        }
        var result = _mapper.Map<AuctionDTO>(auction);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDTO>> CreateAuction([FromBody] CreateAuctionDTO createAuctionDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var auction = _mapper.Map<Auction>(createAuctionDTO);
        auction.Seller = User.Identity.Name;
        await _auctionDbContext.Auctions.AddAsync(auction);
        var newAuction = _mapper.Map<AuctionDTO>(auction);
        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));
        var result = await _auctionDbContext.SaveChangesAsync() > 0;

        return result ? CreatedAtAction(nameof(GetAuction), new { Id = auction.Id }, newAuction) : BadRequest();
    }

    [Authorize]
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<AuctionDTO>> UpdateAuction(Guid id, [FromBody] UpdateAuctionDTO updateAuction)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var auction = await _auctionDbContext.Auctions.Include(x => x.Item).FirstOrDefaultAsync(a => a.Id == id);
        if (auction == null)
        {
            return NotFound("Auction Not Found");
        }
        if (auction.Seller != User.Identity.Name) return Forbid();
        auction.Item.Make = updateAuction.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuction.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuction.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuction.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuction.Year ?? auction.Item.Year;

        //_mapper.Map(updateAuction, auction);

        _auctionDbContext.Auctions.Attach(auction);
        _auctionDbContext.Entry(auction).State = EntityState.Modified;

        await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));
        var result = await _auctionDbContext.SaveChangesAsync() > 0;
        return result ? NoContent() : BadRequest();

    }

    [Authorize]
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _auctionDbContext.Auctions.FindAsync(id);
        if (auction == null)
        {
            return NotFound();
        }
        if (auction.Seller != User.Identity.Name) return Forbid();
        _auctionDbContext.Auctions.Remove(auction);

        //await _publishEndpoint.Publish<AuctionDeleted>(new AuctionDeleted{Id=auction.Id.ToString()});
        await _publishEndpoint.Publish(_mapper.Map<AuctionDeleted>(auction));

        var result = await _auctionDbContext.SaveChangesAsync() > 0;
        return result ? NoContent() : BadRequest();
    }
}
