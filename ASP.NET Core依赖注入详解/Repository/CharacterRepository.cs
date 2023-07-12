using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core依赖注入详解.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        // 定义私有字段
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 通过构造函数注入，并且给私有字段赋值
        /// </summary>
        /// <param name="dbContext"></param>
        public CharacterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Character character)
        {
            // 添加
            _dbContext.Character.Add(character);
            // 保存
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Character> ListAll()
        {
            return _dbContext.Character.AsEnumerable();
        }
    }
}
