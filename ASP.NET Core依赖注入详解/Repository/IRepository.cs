using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core依赖注入详解.Repository
{
    public interface IRepository
    {
    }
    /// <summary>
    /// 基接口
    /// </summary>
    public interface IOperationRepository
    {
        Guid GetOperationId();
    }

    /// <summary>
    /// 瞬时接口
    /// </summary>
    public interface IOperationTransientRepository : IOperationRepository
    {

    }

    /// <summary>
    /// 作用域接口
    /// </summary>
    public interface IOperationScopeRepository : IOperationRepository
    {

    }

    /// <summary>
    /// 单例接口
    /// </summary>
    public interface IOperationSingletonRepository : IOperationRepository
    {

    }
}
