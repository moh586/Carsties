using System.ComponentModel.DataAnnotations;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace AuctionService;

[AutoMap(typeof(Item), ReverseMap = true)]
public class CreateItemDTO
{
    [SourceMember(nameof(Item.Make))]
    [Required]
    public string Make { get; set; }

    [SourceMember(nameof(Item.Model))]
    [Required]
    public string Model { get; set; }

    [SourceMember(nameof(Item.Year))]
    [Required]
    public int Year { get; set; }

    [SourceMember(nameof(Item.Color))]
    [Required]
    public string Color { get; set; }

    [SourceMember(nameof(Item.Mileage))]
    [Required]
    public int Mileage { get; set; }

    [SourceMember(nameof(Item.ImageUrl))]
    [Required]
    public string ImageUrl { get; set; }

    public Guid AuctionId { get; set; }

}
