using System.ComponentModel.DataAnnotations;
using AuctionService.DTOs.Items;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace AuctionService.DTOs.Auctions;



public class CreateAuctionDTO
{

    public int ReservePrice { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public string Color { get; set; }

    public int Mileage { get; set; }

    public int Year { get; set; }

    public string ImageUrl { get; set; }

    public DateTime AuctionEnd { get; set; }

}
