using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public  interface ICacheService
    {
        Task<string> GetFromCache(string Key);

        Task SetInCache(string Key,object value);

        Task ClearAllAsync(string pattern);
    }
}
