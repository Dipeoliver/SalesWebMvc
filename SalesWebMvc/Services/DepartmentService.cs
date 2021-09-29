using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;
        // adicionar dependencia
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public List<Department> FindAll()
        {
            // retornar os departamentos ordenados
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

        internal Task<List<Department>> FindAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
