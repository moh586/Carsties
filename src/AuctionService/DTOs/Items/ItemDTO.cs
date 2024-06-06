using AuctionService.DTOs.Auctions;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace AuctionService.DTOs.Items;



[AutoMap(typeof(Item), ReverseMap = true)]
public class ItemDTO : CreateItemDTO
{
    [SourceMember(nameof(Item.Id))]
    public Guid Id { get; set; }

    public AuctionDTO Auction { get; set; }
}
