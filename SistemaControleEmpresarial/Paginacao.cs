using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SistemaControleEmpresarial
{
    public class Paginacao<T> : List<T>
    {
        public int Pagina { get; private set; }
        public int TotalPaginas { get; private set; }

        public Paginacao(List<T> items, int count, int pagina, int tamanhoPagina)
        {
            Pagina = pagina;
            TotalPaginas = (int)Math.Ceiling(count / (double)tamanhoPagina);

            this.AddRange(items);
        }

        public bool PaginaAnterior
        {
            get
            {
                return (Pagina > 1);
            }
        }

        public bool ProximaPagina
        {
            get
            {
                return (Pagina < TotalPaginas);
            }
        }

        public static async Task<Paginacao<T>> CreateAsync(IQueryable<T> source, int pagina, int tamanhoPagina)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
            return new Paginacao<T>(items, count, pagina, tamanhoPagina);
        }
    }
}