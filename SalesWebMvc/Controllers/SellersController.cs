using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;

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

        // ação para adicionar item
        public IActionResult Create()
        {
            return View();
        }

        // Ação "creat" receber post de um form
        [HttpPost] // dizer que e post
        [ValidateAntiForgeryToken] // prevenção contra ataques
        public IActionResult Create(Seller seller)
        {
            // passar para o service o seller
            _sellerService.Insert(seller);
            // redirecionar para a index
            return RedirectToAction(nameof(Index));
        }
    }
}
