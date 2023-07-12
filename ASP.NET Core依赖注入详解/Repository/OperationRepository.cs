using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core依赖注入详解.Repository
{
    public class OperationRepository : IOperationRepository
    {
        private readonly Guid _guid;

        public OperationRepository()
        {
            _guid = Guid.NewGuid();
        }

        public Guid GetOperationId()
        {
            return _guid;
        }
    }

    public class OperationTransientRepository : OperationRepository, IOperationTransientRepository
    {

    }

    public class OperationScopeRepository : OperationRepository, IOperationScopeRepository
    {

    }

    public class OperationSingletonRepository : OperationRepository, IOperationSingletonRepository
    {

    }
}
