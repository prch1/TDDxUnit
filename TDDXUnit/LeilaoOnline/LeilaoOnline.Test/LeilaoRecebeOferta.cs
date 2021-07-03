using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace LeilaoOnline.Test
{
   

    public class LeilaoRecebeOferta
    {

        
        [Fact]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado()
       {
         var modalidade = new MaiorValor();
         var leilao = new Leilao("Van Gogh",modalidade);
         var fulano = new Interessada("Fulano", leilao);
         var maria = new Interessada("Maria", leilao);

        leilao.IniciaPregao();

        leilao.RecebeLance(fulano, 800);
        leilao.RecebeLance(maria, 900);
        leilao.TerminaPregao();

        //Act - método sob teste
        leilao.RecebeLance(fulano, 1000);

        //Assert
        var valorEsperado = 2;
        var valorObtido = leilao.Lances.Count();

        Assert.Equal(valorEsperado, valorObtido);
    }



        [Theory]
        [InlineData(4,new double[] {100,1200,1400,1300})]
        [InlineData(2, new double[] { 800, 900})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado2(int qtEsperada, double[] ofertas)
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            
            for(int i = 0; i < ofertas.Length; i ++)
            {

                var valor = ofertas[i];
                if((i%2) ==0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
                
            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtObtida = leilao.Lances.Count();

            Assert.Equal(qtEsperada, qtObtida);
        }
        

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            var modalidade = new MaiorValor();
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);


        }

    }
}
