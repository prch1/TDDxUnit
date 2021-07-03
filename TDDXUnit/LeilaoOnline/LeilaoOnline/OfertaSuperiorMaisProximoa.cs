using System.Linq;

namespace LeilaoOnline
{
    public class OfertaSuperiorMaisProximoa : IModalidade
    {
        public double ValorDestino { get; }

        public OfertaSuperiorMaisProximoa(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .Where(l => l.Valor > ValorDestino)
                .OrderBy(l => l.Valor)
                .FirstOrDefault();
        }

    }
}