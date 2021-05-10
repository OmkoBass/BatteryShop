using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.StorageRepo
{
    public interface IStorageRepo
    {
        public Task<List<Storage>> GetAllStorages();
        public Task<Storage> GetStorage(int Id);
        public Task<bool> AddStorage(Storage Storage);
        public bool UpdateStorage(Storage Storage);
        public bool DeleteStorage(Storage Storage);
    }
}
