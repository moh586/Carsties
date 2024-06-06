using AuctionService.DTOs.Items;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace AuctionService.DTOs.Auctions;


public class UpdateAuctionDTO
{
    public string Make { get; set; }

    public string Model { get; set; }

    public string Color { get; set; }

    public int? Mileage { get; set; }

    public int? Year { get; set; }
}
