using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // injecao de dependencia
        private readonly SellerServices _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerServices sellerServices, DepartmentService departmentService)
        {
            _sellerService = sellerServices;
            _departmentService = departmentService;
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
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };

            // a tela quando for acionada pela primeira vez ja ira receber os departamentos
            return View(viewModel);
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


        // abrir a tela para confirmação de deletar (GET).
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // Ação para deletar o item (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


        // exibir os detalhes de um item
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

    }
}
