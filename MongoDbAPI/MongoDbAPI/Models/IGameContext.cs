using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbAPI.Models
{
    public interface IGameContext
    {
        IMongoCollection<Game> Games { get; }
    }
}
