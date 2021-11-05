using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Domains.Contracts.Core {
    public interface IRepository<T, K> {
        List<T> getAll();
        T getOne(K id);
        void add(T item);
        void modify(K id, T item);
        void delete(K id);
    }
}
