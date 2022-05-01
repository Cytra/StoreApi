﻿using Application.Models;
using MediatR;

namespace Application.Commands;

public static class AddProduct
{
    public class Commands : IRequest<Response>
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public List<Photo> Photos { get; set; }
    }

    public class Response
    {

    }

    public sealed class Handler : IRequestHandler<Commands, Response>
    {
        public async Task<Response> Handle(Commands request, CancellationToken cancellationToken)
        {
            return new Response();
        }
    }
}