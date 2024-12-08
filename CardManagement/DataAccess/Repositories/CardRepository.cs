using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities.Card;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CardRepository: BaseRepository<Card>, ICardRepository
    {
        public CardRepository(AppDbContext context) : base(context) { }
    }
}
