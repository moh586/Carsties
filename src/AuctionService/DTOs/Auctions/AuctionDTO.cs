﻿using AuctionService.DTOs.Items;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace AuctionService.DTOs.Auctions;



public class AuctionDTO
{

    public Guid Id { get; set; }


    public int ReservePrice { get; set; }


    public string Seller { get; set; }


    public string Winner { get; set; }


    public int? SoldAmount { get; set; }


    public int? CurrentHighBid { get; set; }


    public DateTime CreatedAt { get; set; }


    public DateTime UpdatedAt { get; set; }


    public DateTime AuctionEnd { get; set; }


    public Status Status { get; set; }

    //public UpdateItemDTO Item { get; set; }


    public string Make { get; set; }

    public string Model { get; set; }


    public string Color { get; set; }


    public int? Milage { get; set; }


    public int Year { get; set; }

    public string ImageUrl { get; set; }



}
