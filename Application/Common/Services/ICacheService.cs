using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Services
{
    public  interface ICacheService
    {
        Task<string> GetFromCache(string Key);

        Task SetInCache(string Key,object value,TimeSpan TTL);

        Task ClearAllAsync(string pattern);
    }
}
