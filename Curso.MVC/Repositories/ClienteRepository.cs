using Curso.Domains.Contracts;
using Curso.Domains.Entities;
using Curso.Infraestructure.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Infraestructure.Repositories {
    public class ClienteRepository : IClienteRespository {
        private readonly TiendaDbContext _context;

        public ClienteRepository(TiendaDbContext context) {
            _context = context;
        }
         public void add(Customer item) {
            throw new NotImplementedException();
        }

        public void delete(int id) {
            throw new NotImplementedException();
        }

        public List<Customer> getAll() {
            throw new NotImplementedException();
        }

        public Customer getOne(int id) {
            throw new NotImplementedException();
        }

        public void modify(int id, Customer item) {
            throw new NotImplementedException();
        }
    }
    public class ClienteRepositoryMock : IClienteRespository {
        private readonly TiendaDbContext _context;

        public void add(Customer item) {
            throw new NotImplementedException();
        }

        public void delete(int id) {
            throw new NotImplementedException();
        }

        public List<Customer> getAll() {
            throw new NotImplementedException();
        }

        public Customer getOne(int id) {
            throw new NotImplementedException();
        }

        public void modify(int id, Customer item) {
            throw new NotImplementedException();
        }
    }
}
