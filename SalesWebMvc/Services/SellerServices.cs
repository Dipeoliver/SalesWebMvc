using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }



        // metodo para inserir (INSERT) no banco de dados
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Busca no banco de dados atraves de um Id;
        public async Task<Seller> FindByIdAsync(int id)
        {  // antes
           //return _context.Seller.FirstOrDefault(obj => obj.Id == id);   => antes

            // depois    incluir Id na visualização (join de tabelas)
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // metodo para deletar (DELETE) no banco de dados
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }


        // metodo de atualizar (UPDATE) no banco
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            // exceção de nivel de banco (DbUpdateConcurrencyException)
            catch (DbUpdateConcurrencyException e)
            {
                // exceção de nivel de camada (DbConcurrencyException)
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
