using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.Bytebank.Atendimento;
using bytebank_ATENDIMENTO.Bytebank.Exceptions;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Xml.Schema;

Console.WriteLine("Boas Vindas ao ByteBank, Atendimento.");

#region

//int acumulador = 0;
//PercorreArray();
//void PercorreArray()
//{
//    string[] ArrayDePalavras = new string[5];

//	for (int i = 0; i < ArrayDePalavras.Length; i++)
//	{
//		Console.WriteLine($"Digite a {i + 1}ª palavra");
//		ArrayDePalavras[i] = Console.ReadLine();
//	}
//	Console.WriteLine("Digite a palavra a ser pesquisada");
//	string busca = Console.ReadLine();

//	foreach(string palavra in ArrayDePalavras)
//	{
//		if (palavra.Equals(busca))
//		{
//			Console.WriteLine($"Palavra encontrada = {busca}");
//		}
//		else
//		{
//            Console.WriteLine("Palavra não encontrda");
//        }
//	}
//}
//Array listadenumeros = Array.CreateInstance(typeof(double), 6);
//listadenumeros.SetValue(5.5, 0);
//listadenumeros.SetValue(1.8, 1);
//listadenumeros.SetValue(2.2, 2);
//listadenumeros.SetValue(7.8, 3);
//listadenumeros.SetValue(0.5, 4);
//listadenumeros.SetValue(8.8, 5);

//CalculaMediana(listaDeNumeros);
//void CalculaMediana(Array array)
//{
//	if ((array == null) || (array.Length == 0))
//	{
//        Console.WriteLine("Array inválida");
//    }

//    double[] numerosOrdenados = (double[])array.Clone();
//    Array.Sort(numerosOrdenados);
//    int tamanho = numerosOrdenados.Length;
//    int meio = tamanho / 2;

//    double mediana = (tamanho % 2 != 0) ? numerosOrdenados[meio] :
//        (numerosOrdenados[meio] + numerosOrdenados[meio])/2;

//    for (int i = 0; i < numerosOrdenados.Length; i++)
//    {
//        Console.WriteLine($"{numerosOrdenados[i]}");
//    }

//    Console.WriteLine(mediana);
//}


//Array lista = Array.CreateInstance(typeof(double),5);
//lista.SetValue(1.2,0);
//lista.SetValue(1.8, 1);
//lista.SetValue(5.2, 2);
//lista.SetValue(4.7, 3);
//lista.SetValue(8.5, 4);

//void CalculaMedia(Array lista)
//{
//    double acumulador = 0;
//    double[] listacopia = (double[])lista.Clone();
//    for (int i = 0; i < listacopia.Length; i++)
//    {
//        acumulador += listacopia[i];
//    }
//    double media = acumulador / listacopia.Length;
//    Console.WriteLine(media);
//}

//CalculaMedia(lista);
//CalculaMedia(listadenumeros);
#endregion
#region Codigos de teste;
//void TestaContaCorrente()
//{
//    ListaDeContasCorrentes listaDeContas = new ListaDeContasCorrentes();
//    listaDeContas.Adicionar(new ContaCorrente(874, "54644544-A"));
//    listaDeContas.Adicionar(new ContaCorrente(874, "54888512-B"));
//    listaDeContas.Adicionar(new ContaCorrente(874, "45112212-C"));
//    listaDeContas.Adicionar(new ContaCorrente(874, "45112212-C"));
//    listaDeContas.Adicionar(new ContaCorrente(874, "45112212-C"));
//}


//ListaDeContasCorrentes listaDeContas = new ListaDeContasCorrentes();
//listaDeContas.Adicionar(new ContaCorrente(984, "4444111-X"));
//listaDeContas.Adicionar(new ContaCorrente(245, "4444111-X"));
//listaDeContas.Adicionar(new ContaCorrente(783, "4444111-X"));
//listaDeContas.Adicionar(new ContaCorrente(454, "4444111-X"));
//listaDeContas.Adicionar(new ContaCorrente(555, "4444111-X"));


//for (int i = 0; i < listaDeContas.Tamanho; i++)
//{
//    ContaCorrente conta = listaDeContas[i];
//    Console.WriteLine($"Indice [{i}] = {conta.Conta} // {conta.Numero_agencia}");
//}
#endregion

new BytebankAtendimento().AtendimentoCliente();
