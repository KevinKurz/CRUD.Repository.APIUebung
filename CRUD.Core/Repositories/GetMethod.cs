using CRUD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Core.Repositories
{
    public abstract class GetMethod<TOutput> : IGetMethod
    {
        public abstract IEnumerable<TOutput> GetAll();
    }

    public abstract class GetMethod<TOutput, TTableId> : IGetMethod
    {
        public abstract IEnumerable<TOutput> GetAll(TTableId tableId);
    }

    public abstract class GetMethod<TOutput, TTableId, TQueryParams> : IGetMethod
    {
        public abstract IEnumerable<TOutput> GetAll(TTableId tableId, TQueryParams queryParams);
    }
}
