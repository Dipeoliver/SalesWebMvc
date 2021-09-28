using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerServices
    {
        // adicionar dependencia
        private readonly SalesWebMvcContext _context;
        public SellerServices(SalesWebMvcContext context)
        {
            _context = context;
        }


        // operacao retornar lista (SELECT) com todos vendedores
        // operação sincrona
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }



        // metodo para inserir (INSERT) no banco de dados
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        // Busca no banco de dados atraves de um Id;
        public Seller FindById(int id)
        {   // antes
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);   => antes

            // depois    incluir Id na visualização (join de tabelas)
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);

        }

        // metodo para deletar (DELETE) no banco de dados
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
