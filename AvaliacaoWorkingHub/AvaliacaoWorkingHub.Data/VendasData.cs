using AvaliacaoWorkingHub.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaliacaoWorkingHub.Models;
using System.Data.Entity;

namespace AvaliacaoWorkingHub.Data
{
    public class VendasData : IVendasData
    {
        private VendasDbContext _ctx;
        private DbSet<Venda> _set;

        public VendasData(VendasDbContext ctx)
        {
            _ctx = ctx;
            _set = _ctx.Set<Venda>();
        }

        public void Commit()
        {
            _ctx.SaveChanges();
        }

        public void Insert(Venda venda)
        {
            _set.Add(venda);
        }
    }
}
