using System.Linq;

namespace LeilaoOnline
{
    public class MaiorValor : IModalidade
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(l => l.Valor)
                .LastOrDefault();
                
        }
        

        
    }
}