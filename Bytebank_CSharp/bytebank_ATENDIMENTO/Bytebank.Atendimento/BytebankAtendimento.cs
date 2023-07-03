using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.Bytebank.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace bytebank_ATENDIMENTO.Bytebank.Atendimento
{
    public partial class BytebankAtendimento
    {
        protected List<ContaCorrente> _listaDeContas = new List<ContaCorrente>
{
    new ContaCorrente(95, 1888) { Saldo = 100, Titular = new Cliente() {Cpf = "111", Nome = "Maria" } },
    new ContaCorrente(95, 1999) { Saldo = 200, Titular = new Cliente() {Cpf = "222", Nome = "Pedro" } },
    new ContaCorrente(94, 1920) { Saldo = 60, Titular = new Cliente() {Cpf = "333", Nome = "Joao" } }
};
        public void AtendimentoCliente()
        {
            try
            {
                char opcao = '0';

                while (opcao != '9')
                {
                    Console.WriteLine("===============================");
                    Console.WriteLine("===       Atendimento       ===");
                    Console.WriteLine("===1 - Cadastrar Conta      ===");
                    Console.WriteLine("===2 - Listar Contas        ===");
                    Console.WriteLine("===3 - Remover Conta        ===");
                    Console.WriteLine("===4 - Ordenar Contas       ===");
                    Console.WriteLine("===5 - Pesquisar Conta      ===");
                    Console.WriteLine("===6 - Exportar Contas      ===");
                    Console.WriteLine("===7 - Importar Contas(JSON)===");
                    Console.WriteLine("===8 - Importar Contas(CSV) ===");
                    Console.WriteLine("===9 - Sair do Sistema      ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n\n");
                    Console.Write("Digite a opção desejada: ");
                    try
                    {
                        opcao = Console.ReadLine()[0];
                    }
                    catch (Exception excecao)
                    {
                        throw new BytebankException(excecao.Message);
                    }

                    switch (opcao)
                    {
                        case '1':
                            try
                            {
                                CadastrarConta();
                            }
                            catch (FormatException ex) { Console.WriteLine(ex.Message + " Não foi digitado o formato correto"); }
                            break;
                        case '2':
                            ListarContas();
                            break;
                        case '3':
                            RemoverConta();
                            break;
                        case '4':
                            OrdenarConta();
                            break;
                        case '5':
                            PesquisarConta();
                            break;
                        case '6':
                            ExportarContas();
                            break;
                        case '7':
                            try
                            {
                               ImportarContasJSON();
                            } catch(FileNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message + "\n" + " Arquivo não encontrado. Digite um arquivo válido presente na pasta net6.0\n" + "Voltando para o menu...");
                            }
                            break;
                        case '8':
                            try
                            {
                                ImportarContasCSV();
                            }
                            catch (FileNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message + "\n" + " Arquivo não encontrado. Digite um arquivo válido presente na pasta net6.0\n" + "Voltando para o menu...");
                            }

                            break;
                        case '9':
                            Console.WriteLine("Saindo do sistema.");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Opcao não implementada.");
                            break;
                    }
                }

            }
            catch (BytebankException excecao)
            {

                Console.WriteLine(excecao.Message);
            }

        }

        private void ImportarContasJSON()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===  Importar Contas(Json)  ===");
            Console.WriteLine("===============================");
            Console.WriteLine("Na opção Importar Contas(Json) vai ser adicionado de um arquivo externo no formato Json");
            Console.WriteLine("Informe o nome do arquivo (Ele precisa estar dentro da pasta |Net6.0|.) \n" +
            "Para fins de exemplo da funcionalidade tem um arquivo pronto na pasta, digite (contasparaimportar.json) sem parênteses");
            string nomeArquivo = Console.ReadLine();


            var fileName = nomeArquivo;
            string jsonString = File.ReadAllText(fileName);
            List<ContaCorrente> jsonList = JsonConvert.DeserializeObject<List<ContaCorrente>>(jsonString);
            
            foreach(var conta in jsonList)
            {
                
                var agencia = conta.Numero_agencia;
                var numeroConta = conta.Conta;
                ContaCorrente novaConta = new ContaCorrente(agencia, numeroConta);
                novaConta.Depositar(conta.Saldo);
                novaConta.Titular = new Cliente();
                novaConta.Titular.Nome = conta.Titular.Nome;
                _listaDeContas.Add(novaConta);

            }
            Console.WriteLine("Contas adicionadas de um arquivo externo para a lista com sucesso. Execute Listar Contas para conferir o resultado");
        }

        private void ExportarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     EXPORTAR CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não existe dados para exportação...");
                Console.ReadKey();
            }
            else
            {
                string json = JsonConvert.SerializeObject(_listaDeContas, Formatting.Indented);
                try
                {
                    using(var fs = new FileStream("contasExportadas.json", FileMode.Create))
                    using(var sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(json);
                    }
                    Console.WriteLine("Arquivo salvo");
                }
                catch (Exception excecao)
                {
                    throw new BytebankException(excecao.Message);
                    Console.ReadKey();
                }
            }
        }
        //OPÇÃO ALTERNATIVA PARA IMPORTAR DE ARQUIVO CSC
        private void ImportarContasCSV()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===  Importar Contas(CSV)  ===");
            Console.WriteLine("===============================");
            Console.WriteLine("Na opção Adicionar Lista vai ser adicionado de um arquivo externo no formato CSV ou TXT");
            Console.WriteLine("Informe o nome do arquivo (Ele precisa estar dentro da pasta |Net6.0|.) \n" +
                "Para fins de exemplo da funcionalidade tem um arquivo pronto na pasta, digite (contas.txt) sem parênteses");
            var nomeDoArquivo = Console.ReadLine();
            AdicionarArquivoNaLista(nomeDoArquivo, _listaDeContas);
            Console.WriteLine("Contas adicionadas de um arquivo externo para a lista com sucesso. Execute Listar Contas para conferir o resultado");
            Console.ReadKey();


            static void AdicionarArquivoNaLista(string caminhoArquivo, List<ContaCorrente> lista)
            {
                var caminhoDoArquivo = caminhoArquivo;
                using (var fluxoDoArquivo = new FileStream(caminhoDoArquivo, FileMode.Open))
                {
                    var leitor = new StreamReader(fluxoDoArquivo);

                    while (!leitor.EndOfStream)
                    {
                        var linha = leitor.ReadLine();
                        ContaCorrente conta = TransformarStringEmConta(linha);
                        lista.Add(conta);
                    }
                }
            }
            static ContaCorrente TransformarStringEmConta(string linha)
            {
                var campos = linha.Split(',');
                var agencia = int.Parse(campos[0]);
                var numeroConta = int.Parse(campos[1]);
                var saldo = double.Parse(campos[2].Replace('.', ','));
                var nome = campos[3];

                ContaCorrente conta = new ContaCorrente(agencia, numeroConta);
                conta.Depositar(saldo);
                conta.Titular = new Cliente();
                conta.Titular.Nome = nome;

                return conta;
            }
        }

        private void PesquisarConta()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===    PESQUISAR CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.Write("Deseja pesquisar por (1) NUMERO DA CONTA ou (2)CPF TITULAR ou (3)NUMERO DA AGENCIA ? ");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    Console.WriteLine("Informe o NUMERO DA CONTA");
                    string _numeroDaConta = Console.ReadLine();
                    ContaCorrente consultaConta = ConsultaPorNumeroDaConta(_numeroDaConta);
                    Console.Write(consultaConta.ToString());
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Informe o CPF DO TITULAR");
                    string _cpfDoTitular = Console.ReadLine();
                    ContaCorrente consultaCpf = ConsultaPorCpfDoTitular(_cpfDoTitular);
                    Console.WriteLine(consultaCpf.ToString());
                    Console.ReadKey();
                    break;
                case 3:
                    Console.WriteLine("Informe o NUMERO DA AGENCIA");
                    int _numeroAgencia = int.Parse(Console.ReadLine());
                    List<ContaCorrente> consultaAgencia = ConsultaPorNumeroAgencia(_numeroAgencia);
                    ExibeLista(consultaAgencia);
                    Console.ReadKey();
                    break;

            }
        }

        private void ExibeLista(List<ContaCorrente> consultaAgencia)
        {
            foreach (var item in consultaAgencia)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private List<ContaCorrente> ConsultaPorNumeroAgencia(int? numeroAgencia)
        {
            var consulta = (from conta in _listaDeContas
                            where conta.Numero_agencia == numeroAgencia
                            select conta).ToList();
            return consulta;
        }

        private ContaCorrente ConsultaPorCpfDoTitular(string? cpfDoTitular)
        {
            //ContaCorrente conta = null;
            //for (int i = 0; i<_listaDeContas.Count; i++)
            //{
            //    if (_listaDeContas[i].Titular.Cpf.Equals(cpfDoTitular))
            //    {
            //        conta = _listaDeContas[i];
            //    }
            //}
            //return conta;
            return _listaDeContas.Where(conta => conta.Titular.Cpf == cpfDoTitular).FirstOrDefault();
        }

        private ContaCorrente ConsultaPorNumeroDaConta(string? numeroDaConta)
        {
            //ContaCorrente conta = null;
            //for (int i = 0; i < _listaDeContas.Count; i++)
            //{
            //    if (_listaDeContas[i].Conta.Equals(numeroDaConta))
            //    {
            //        conta = _listaDeContas[i];
            //    }
            //}
            //return conta;
            var consulta = (from conta in _listaDeContas
                            where conta.Conta.Equals(numeroDaConta)
                            select conta).FirstOrDefault();
            return consulta;
        }

        private void OrdenarConta()
        {
            _listaDeContas.Sort();
            Console.WriteLine("...Contas ordenadas com sucesso");
        }
        private void RemoverConta()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===      REMOVER CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.Write("Informe o número da conta: ");
            string numeroConta = Console.ReadLine();
            ContaCorrente conta = null;
            foreach (ContaCorrente item in _listaDeContas)
            {
                if (item.Conta.Equals(numeroConta))
                {
                    conta = item;
                }
            }
            if (conta != null)
            {
                _listaDeContas.Remove(conta);
                Console.WriteLine("... Conta removida da lista! ...");
            }
            else
            {
                Console.WriteLine(" ... Conta para remoção não encontrada ...");
            }

            Console.ReadKey();
        }


        private void ListarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     LISTA DE CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não há contas cadastradas! ...");
                Console.ReadKey();
                return;
            }
            foreach (ContaCorrente item in _listaDeContas)
            {
                Console.WriteLine("===  Dados da Conta  ===");
                Console.WriteLine("Número da Conta : " + item.Conta);
                Console.WriteLine("Saldo da Conta : " + item.Saldo);
                Console.WriteLine("Titular da Conta: " + item.Titular.Nome);
                Console.WriteLine("CPF do Titular  : " + item.Titular.Cpf);
                Console.WriteLine("Profissão do Titular: " + item.Titular.Profissao);
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            }

        }
        private void CadastrarConta()
        {
            Console.WriteLine("Informe o numero da agencia");
            int numeroAgencia = int.Parse(Console.ReadLine());
            if (numeroAgencia.GetType() != typeof(int))
            {
                throw new FormatException(" Formato inválido, use apenas numeros");
            }
            Console.WriteLine("Informe o numero da conta");
            int numeroconta = int.Parse(Console.ReadLine());
            ContaCorrente conta = new ContaCorrente(numeroAgencia, numeroconta);
            Console.WriteLine($"Numero da Conta [NOVA] : {conta.Conta}");

            Console.WriteLine("Qual o saldo inicial da conta?");
            conta.Saldo = double.Parse(Console.ReadLine());
            Console.WriteLine("Informe o titular da conta");
            conta.Titular.Nome = Console.ReadLine();
            Console.WriteLine("Qual o CPF do titular?");
            conta.Titular.Cpf = Console.ReadLine();
            Console.WriteLine("Qual a profissão do titular?");
            conta.Titular.Profissao = Console.ReadLine();

            _listaDeContas.Add(conta);
            Console.WriteLine("...Conta cadastrada com sucesso!");
            Console.ReadKey();
        }

    }
}
