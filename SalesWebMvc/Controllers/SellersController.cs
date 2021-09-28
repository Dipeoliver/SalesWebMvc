using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // injecao de dependencia
        private readonly SellerServices _sellerService;
        public SellersController(SellerServices sellerServices)
        {
            _sellerService = sellerServices;
        }


        public IActionResult Index()
        {
            // operação para chamar a lista FindAll
            var list = _sellerService.FindAll(); // vai me retornar uma lista de sellers
            return View(list); // passa valores para a view
        }


    }
}
