using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiddingService.Models;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace BiddingService.Consumers
{
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine("Bidding Service ==========> Consuming auction created" + context.Message.Id);

            var auction = new Auction
            {
                ID = context.Message.Id.ToString(),
                Seller = context.Message.Seller,
                AuctionEnd = context.Message.AuctionEnd,
                ReservePrice = context.Message.ReservePrice
            };

            await auction.SaveAsync();
        }
    }
}