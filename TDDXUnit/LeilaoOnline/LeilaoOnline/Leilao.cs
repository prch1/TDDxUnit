using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeilaoOnline
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private IList<Lance> _lances;
        private Interessada _ultimoCliente;
        private IModalidade _avaliador;
      
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; set; }
        public double ValorDestino { get; }

        public Leilao(string peca, IModalidade avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador;       
        }


        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if(NovoLanceEhAceito(cliente,valor))
            {      
              _lances.Add(new Lance(cliente, valor));
              _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
          if(Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new System.InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado.Para isso, utilize o método IniciaPregao().");
            }
            Ganhador = _avaliador.Avalia(this);                   
            Estado = EstadoLeilao.LeilaoFinalizado;
        }

    }
}
