using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace AuctionService.DTOs.Items
{
    [AutoMap(typeof(Item), ReverseMap = true)]
    public class UpdateItemDTO
    {
        [SourceMember(nameof(Item.Make))]
        public string Make { get; set; }

        [SourceMember(nameof(Item.Model))]
        public string Model { get; set; }

        [SourceMember(nameof(Item.Year))]
        public int Year { get; set; }

        [SourceMember(nameof(Item.Color))]
        public string Color { get; set; }

        [SourceMember(nameof(Item.Mileage))]
        public int Mileage { get; set; }

        [SourceMember(nameof(Item.ImageUrl))]
        public string ImageUrl { get; set; }

    }
}