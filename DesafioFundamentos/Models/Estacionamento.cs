using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    //
    // Summary:
    //     Classe que representa um estacionamento.
    //
    // TODO: Precisa ser testada com testes unitários.
    // TODO: Aprenda um framework de teste (xUnit, NUnit, MSTest).
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();
        Regex regex = new Regex(@"^\w{3}[._-]?\d{1}(\d|\w){1}\d{2}$", RegexOptions.IgnoreCase);

        //
        // Parameters:
        //   precoInicial:
        //     o valor inicial pago para estacionar
        //
        //   precoPorHora:
        //     O preço da hora estacionada.
        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        //
        // Summary:
        //     Adiciona um novo veículo no estacionamento.
        //
        public void AdicionarVeiculo()
        {
            string placa;

            Console.WriteLine("Digite a placa do veículo para estacionar:");

            do
            {
                placa = Console.ReadLine();
                placa = RemoverPontuacao(placa).ToUpper();

                if (placa == "")
                {
                    break;
                }

                if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
                {
                    Console.WriteLine($"Veículo {placa}, já está estacionado!\n");
                    continue;
                }
                else if (!regex.Match(placa.Trim()).Success)
                {
                    Console.WriteLine(
                        $"A placa: {placa}, não é valida. \ntente: AAA-1b34 ou aaa1234, com ou sem separador (-)\n"
                    );
                    continue;
                }

                veiculos.Add(placa);

                Console.WriteLine(
                    $"veículo '{placa.Substring(0, 3)}-{placa.Substring(3)}' adicionado\n"
                );
            } while (placa != "");
        }

        //
        // Summary:
        //     Remover um veículo do estacionamento.
        public void RemoverVeiculo()
        {
            if (veiculos.Count == 0)
            {
                Console.WriteLine("Não há veículos estacionados. Adicione algum veículo.");
                return;
            }

            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa;

            do
            {
                if (veiculos.Count() == 0)
                {
                    Console.WriteLine("Não há mais veículos estacionados.");
                    return;
                }
                placa = Console.ReadLine();
                placa = RemoverPontuacao(placa).ToUpper();

                if (placa == "")
                {
                    return;
                }

                if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
                {
                    Console.WriteLine(
                        "Digite a quantidade de horas que o veículo permaneceu estacionado:"
                    );

                    try
                    {
                        int horas = Convert.ToInt32(Console.ReadLine());

                        decimal valorTotal = precoInicial + precoPorHora * horas;

                        veiculos.Remove(placa);

                        Console.WriteLine(
                            $"O veículo {placa.Substring(0, 3)}-{placa.Substring(3)} foi removido e o preço total foi de: R$ {valorTotal}\n"
                        );
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"{e.Message}\n");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente\n"
                    );
                    ListarVeiculos();

                    Console.WriteLine("\nDigite a placa do veículo para remover:");
                }
            } while (placa != "");
        }

        //
        // Summary:
        //     Lista todos os veículos estacionados.
        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:\n");

                for (var i = 0; i < veiculos.Count(); i++)
                {
                    Console.WriteLine(
                        $"Veículo.{i + 1} - {veiculos[i].Substring(0, 3)}-{veiculos[i].Substring(3)}"
                    );
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        //
        // Summary:
        //     Remove as pontuações. Somente permite letras e números.
        //
        // Parameters:
        //   texto:
        //     A String para remover a pontuação.
        //
        // Returns:
        //     A String sem pontuações.
        public string RemoverPontuacao(string texto)
        {
            Regex pontuacaoRegex = new Regex(@"[^a-zA-Z0-9]");

            string textoSemPontuacao = "";

            for (int i = 0; i < texto.Length; i++)
            {
                if (!pontuacaoRegex.Match(texto[i].ToString()).Success)
                {
                    textoSemPontuacao += texto[i];
                }
            }

            return textoSemPontuacao;
        }
    }
}
