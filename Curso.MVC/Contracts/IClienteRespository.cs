using Curso.Domains.Contracts.Core;
using Curso.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Domains.Contracts {
    public interface IClienteRespository : IRepository<Customer, int> {
    }
}
