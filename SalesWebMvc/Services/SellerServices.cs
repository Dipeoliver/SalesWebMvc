﻿using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerServices
    {
        private readonly SalesWebMvcContext _context;

        // adicionar dependencia
        public SellerServices(SalesWebMvcContext context)
        {
            _context = context;
        }

        // operacao retornar lista com todos vendedores
        // operação sincrona
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }


    }
}