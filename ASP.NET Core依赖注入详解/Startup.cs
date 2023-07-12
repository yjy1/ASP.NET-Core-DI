using ASP.NET_Core依赖注入详解.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore
;
namespace ASP.NET_Core依赖注入详解
{
    public class Startup
    {
        //参考资料：
        //作者：.NET开发菜鸟  
        //博客地址：https://www.jb51.net/article/242009.htm
        //来源：脚本之家

        // ASP.NET Core依赖注入详解
        // 更新时间：2022年03月24日 08:49:24   作者：.NET开发菜鸟
        //本文详细讲解了ASP.NET Core的依赖注入，文中通过示例代码介绍的非常详细。对大家的学习或工作具有一定的参考借鉴价值，需要的朋友可以参考下

        //ASP.NET Core的底层设计支持和使用依赖注入。ASP.NET Core应用程序可以利用内置的框架服务将它们注入到启动类的方法中，并且应用程序服务能够配置注入。由ASP.NET Core提供的默认服务容器提供了最小功能集，并不是要取代其它容器。


        //一、什么是依赖注入
        //依赖注入（Dependency injection，DI）是一种实现 对象 及其合作者 或 依赖项 之间 松散耦合 的技术。将类用来执行其操作的这些对象以某种方式提供给该类，而不是 直接实例化合作者 或 使用静态引用 。通常，类会通过它们的 构造函数 声明 其依赖关系，允许它们遵循 显示依赖原则。这种方法被称为“构造函数注入”。

        //当类的设计使用DI思想时，它们的耦合更加松散，因为它们没有对它们的合作者直接硬编码的依赖。这遵循“依赖倒置原则（Dependency Inversion Principle）”，其中指出，“高层模块 不应该依赖于 低层模块；两者都应该 依赖于抽象”。类要求在它们构造时向 其提供 抽象（通常是interfaces），而不是引用特定的 实现。提取接口的依赖关系和提供这些接口的实现作为参数也是“策略设计模式”的一个示例。


        //当系统被设计使用DI，很多类通过它们的构造函数（或属性）请求其依赖关系，当一个类被用来创建这些类及其相关的依赖关系是很有帮助的。这些类被称为“容器（containers）”，或者更具体地被称为“控制反转（Inversion of Control，IOC）容器”或者“依赖注入（Dependency injection，DI）容器”。容器本质上是一个工厂，负责提供向它请求的类型实例。如果一个给定类型声明它具有依赖关系，并且容器已经被配置为提供依赖类型，那么它将把创建依赖关系作为创建请求实例的一部分。通过这种方式，可以向类型提供复杂的依赖关系而不需要任何硬编码的类型构造。除了创建对象的依赖关系外，容器通常还会管理应用程序中对象的生命周期。

        //ASP.NET Core包含了一个默认支持构造函数注入的简单内置容器（由IServiceProvider接口表示），并且ASP.NET Core使某些服务可以通过DI获取。ASP.NET Core的容器指的是它管理的类型为services。services是指由ASP.NET Core的IOC容器管理的类型。我们可以在应用程序Startup类的ConfigureServices方法中配置内置容器的服务。


        //二、使用框架提供的服务
        //Startup类中的 ConfigureServices 方法负责 定义应用程序将使用的服务，包括平台功能，比如EntityFramework Core和ASP.NET Core MVC。最初，IServiceCollection只向ConfigureServices提供了几个服务定义。如下面的例子：

        //除了使用默认提供的几个服务定义，我们还可以自己添加。下面是一个如何使用一些扩展方法（如AddDbContext，AddIdentity）向容器中添加额外服务的例子：


        // ASP.NET提供的功能和中间件，例如MVC，遵循约定使用一个单一的AddService扩展方法来注册所有该功能所需的服务。

        //当然，除了使用各种框架功能配置应用程序外，还可以使用ConfigureServices来配置自己的应用程序服务。 


        //三、注册服务
        //可以按照下面的方式注册自己的应用程序服务。第一个泛型类型表示将要从容器中请求的类型（这里的类型通常是一个接口）。第二个泛型类型表示将由容器实例化并且用于完成这些请求的具体类型：

        // 添加自己的服务
        // IRepository是一个接口，表示要请求的类型
        // UserRepository表示IRepository接口的具体实现类型

        //每个services.Add<service> 调用添加服务。例如，services.AddControllersWithViews() 表示添加MVC需要的服务。

        //在示例中，有一个名称为CharactersController的控制器。它的Index方法显示已经存储在应用程序的当前字符列表，并且，如果它不存在的话，则初始化具有少量字符的集合。值得注意的是：虽然应用程序使用Entity Framework Core和AppDbContext类作为持久化工具，这在控制器中都不是显而易见的。相反，具体的数据访问机制被抽象在遵循仓储模式的ICharacterRepository接口后面。ICharacterRepository实例是通过构造函数注入的，并且分配给一个私有字段，然后用来访问所需的字符：




        //ICharacterRepository接口中只定义了控制器需要使用的Character实例的两个方法：

        // 这个接口在运行时需要使用一个具体的CharacterRepository类型来实现。

        //在CharacterRepository类中使用DI的方式是一个可以在你的应用程序服务遵循的通用模型，不只是在“仓储”或者数据访问类中：

        // 需要注意的是，CharacterRepository需要一个AppDbContext在它的构造函数中。依赖注入用于像这样的链式方法并不少见，每个请求依次请求它的依赖关系。容器负责解析所有的依赖关系，并返回完全解析后的服务。

        //创建请求对象和它需要的所有对象，以及那些需要的所有对象，有时称为一个对象图。同样的，必须解析依赖关系的集合通常称为依赖树或者依赖图。


        //在这种情况下，ICharacterRepository和AppDbContext都必须在Startup类的ConfigureServices方法的服务容器中注册。AppDbContext配置调用AddDbContex<T> 扩展方法。下面的代码展示了ICharacterRepository和AppDbContext类型的注册：


        //    Entity Framework Core的数据上下文应当使用Scope的生命周期添加到服务容器中。如果使用上面的AddDbContext<T> 方法则会自动处理。仓储将使用与Entity Framework Core相同的生命周期。


        //四、生命周期
        //ASP.NET Core服务可以配置为以下三种生命周期：

        //Transient：瞬时生命周期。瞬时生命周期服务在它们每次请求时被创建。这一生命周期适合轻量级的、无状态的服务。
        //Scoped：作用域生命周期。作用域生命周期服务在每次请求时被创建一次。
        //Singleton：单例生命周期。单例生命周期服务在它们第一次被请求时创建，并且每个后续请求将使用相同的实例。如果你的应用程序需要单例行为，则建议让服务容器管理服务的生命周期，而不是在自己的类中实现单例模式和管理对象的生命周期。
        //服务可以用多种方式在容器中注册。我们已经看到了如何通过指定具体类型来注册一个给定类型的服务实现。除此之外，可以指定一个工厂，它将被用来创建需要的实例。第三种方式是直接指定要使用的类型的实例。在这种情况下，容器将永远不会尝试创建一个实例。

        //为了说明这些生命周期和注册选项之间的差异，考虑一个简单的接口将一个或多个任务表示为有一个唯一标识符OperationId的操作。根据我们配置这个服务的生命周期的方法，容器将为请求的类提供相同或不同的服务实例。为了弄清楚哪一个生命周期被请求，我们需要创建每一个生命周期选项的类型。我们先定义一个接口，里面定义基接口和三种注入模式的接口：


        //我们使用OperationRepository类来实现这些接口：


        //然后在Startup类的ConfigureServices中，每一个类型根据它们命名的生命周期被添加到容器中：

        public void ConfigureServices(IServiceCollection services)
        {
            // 添加EntityFrameworkCore服务
            // 这里是注册AppDbContext使用AddDbContext<T>的形式
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // 添加自己的服务
            // IRepository是一个接口，表示要请求的类型
            // UserRepository 表示IRepository接口的 具体实现类型
            services.AddTransient<IRepository, UserRepository>();

            // 注册 ICharacterRepository 类型 ,CharacterRepository实现类
            services.AddTransient<ICharacterRepository, CharacterRepository>();

            // 添加瞬时生命周期
            services.AddTransient<IOperationTransientRepository, OperationTransientRepository>();
            // 添加作用域生命周期
            services.AddScoped<IOperationScopeRepository, OperationScopeRepository>();
            // 添加单例生命周期
            services.AddSingleton<IOperationSingletonRepository, OperationSingletonRepository>();
            // 添加MVC服务
            services.AddMvc();
        }



        /* public void ConfigureServices(IServiceCollection services)
         {
             // 添加EntityFrameworkCore服务
             // 这里是注册AppDbContext使用AddDbContext<T>的形式
             services.AddDbContext<AppDbContext>(options =>
             {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
             });

             // 添加自己的服务
             // IRepository是一个接口，表示要请求的类型
             // UserRepository表示IRepository接口的具体实现类型
             services.AddTransient<IRepository, UserRepository>();

             // 注册ICharacterRepository类型
             services.AddTransient<ICharacterRepository, CharacterRepository>();
             // 添加MVC服务
             services.AddControllersWithViews();
         }*/

        //然后添加一个控制器：


        //对应的Index视图代码：
 
        //然后我们打开两个浏览器，刷新多次，只会发现“TransientGuid” 和“ScopedGuid”的值在不断变化，而“SingletonGuid”的值是不会变化的，这就体现了单例模式的作用，如下图所示：



        //但是这样还不够，要知道我们的Scoped的解读是“生命周期横贯整次请求”，但是现在演示起来和Transient好像没有什么区别（因为两个页面每次浏览器请求仍然是独立的，并不包含于一次中），所以我们采用以下代码来演示下（同一请求源）：

          
        //<div class="row">
        //    <div>
        //        <h2>GuidItem Shows</h2>
        //        <h3>TransientGuid: @OperationTransientRepository.GetOperationId()</h3>
        //        <h3>ScopedGuid: @OperationScopeRepository.GetOperationId()</h3>
        //        <h3>SingletonGuid: @OperationSingletonRepository.GetOperationId()</h3>
        //    </div>
        //</div>
        //然后修改Index视图：

        //<div class="row">
        //    <div>
        //        @Html.Partial("GuidPartial")
        //        <h2>**************************</h2>
        //        <h2>GuidItem Shows</h2>
        //        <h3>TransientGuid: @ViewBag.TransientGuid</h3>
        //        <h3>ScopedGuid: @ViewBag.ScopedGuid</h3>
        //        <h3>SingletonGuid: @ViewBag.SingletonGuid</h3>
        //    </div>
        //</div>
        //在运行程序执行：
         

        //可以看到：每次请求的时候Scope生命周期在同一请求中是不变的，而Transient生命周期还是会不断变化的。

        //瞬时（Transient）：对象总是不同的，向每一个控制器和每一个服务提供了一个新的实例（同一个页面内的Transient也是不同的）。
        //作用域（Scoped）：对象在一次请求中是相同的，但在不同请求中是不同的（在同一个页面内多个Scoped是相同的，在不同页面中是不同的）。
        //单例（Singleton）：对象对每个对象和每个请求是相同的（无论是否在ConfigureServices中提供实例）。

        //五、请求服务
        //来自HttpContext的一次ASP.NET请求中，可用的服务是通过RequestServices集合公开的。

        //请求服务将你配置的服务和请求描述为应用程序的一部分。在你的对象指定依赖关系后，这些满足要求的对象可通过查找RequestServices中对应的类型得到，而不是ApplicationServices。


        //通过，不应该直接使用这些属性，而是通过类的构造函数请求需要的类的类型，并且让框架来注入依赖关系。这将会生成更易于测试的和更松散耦合的类。


        //六、设计你的依赖服务
        //应该设计你的依赖注入服务来获取它们的合作者。这意味着在你的服务中，避免使用有状态的静态方法调用和直接实例化依赖的类型。

        //如果你的类有太多的依赖关系被注入时该怎么办？这通常表明你的类试图做太多，并且可能违反了单一职责原则。看看是否可以通过转移一些职责到一个新的类来重构。

        //注意，你的Controller类应该重点关注用户界面（UI），因此业务规则和数据访问实现细节应该保存在这些适合单独关注的类中。

        //关于数据访问，如果你已经在Startup类中配置了EF，那么你能够方便地注入Entity Framework的DBContext类型到你的控制器中。然而，最好不要在你的UI项目中直接依赖DBContext。相反，应该依赖于一个抽象（比如一个仓储接口），并且限定使用EF（或其他任何数据访问技术）来实现这个接口。这将减少应用程序和特定的数据访问策略之间的耦合，并且使你的应用程序代码更容易测试。

        //GitHub示例代码：https://github.com/jxl1024/DependencyInjection


        //到此这篇关于ASP.NET Core依赖注入的文章就介绍到这了。希望对大家的学习有所帮助，也希望大家多多支持脚本之家。
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
