using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeilaoOnline.Test
{
    public  class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            var valorNegativo = -100;

         var excecaoObtida =   Assert.Throws<System.ArgumentException>(

                  () => new Lance(null, valorNegativo)
                ); ;

            var mensagemEsperada = "Valor do lance deve ser igual ou maior que zero.";
            Assert.Equal(mensagemEsperada, excecaoObtida.Message);
        }
    }
}
