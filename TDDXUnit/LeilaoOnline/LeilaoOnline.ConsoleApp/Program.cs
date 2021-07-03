using System;

namespace LeilaoOnline.ConsoleApp
{
    class Program
    {
       
       private static void Verifica(double esperado, double obtido)
        {
            var cor = Console.ForegroundColor;

            if(esperado == obtido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TESTE OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    $"TESTE FALHOU! Esperado: {esperado}, obtido: {obtido}.");
            }
            Console.ForegroundColor = cor;
        }
        
       
        private static void LeilaoComVariosLances()
        {
            var modalidade = new MaiorValor();

            //Arranje - cenario
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);

            //if (valorEsperado == valorObtido)
            //{
            //    Console.WriteLine("Teste OK");
            //}
            //else
            //{
            //    Console.WriteLine("Teste Falha");
            //}

        }

        private static void LeilaoComUmLance()
        {
            //Arranje - cenario
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);


            //if (valorEsperado == valorObtido)
            //{
            //    Console.WriteLine("Teste OK");
            //}
            //else
            //{
            //    Console.WriteLine("Teste Falha");
            //}

        }

        static void Main()
        {
            LeilaoComVariosLances();
            LeilaoComUmLance();
            
            Console.ReadKey();
        }
        
    }
}
