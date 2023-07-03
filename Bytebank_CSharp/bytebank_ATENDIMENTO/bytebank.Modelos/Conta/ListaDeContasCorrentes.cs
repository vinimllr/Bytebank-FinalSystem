using bytebank.Modelos.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace bytebank_ATENDIMENTO.bytebank.Modelos.Conta
{
    internal class ListaDeContasCorrentes
    {
        private int proximaPosicao = 0;
        private ContaCorrente[] _items = null;

        public ListaDeContasCorrentes(int tamanhoInicial=5)
        {
            _items = new ContaCorrente[tamanhoInicial];
        }

        public void Adicionar(ContaCorrente item)
        {
            Console.WriteLine($"Adicionando no indice {proximaPosicao}");
            VerificaCapacidade(proximaPosicao + 1);
            _items[proximaPosicao] = item;
            Console.WriteLine("Agencia: " + _items[proximaPosicao].Numero_agencia + " Conta: " + _items[proximaPosicao].Conta);
            proximaPosicao++;
        }

        public void Remover(ContaCorrente item)
        {
            int indiceItem = 0;

            for (int i = 0; i < _items.Length; i++)
            {
                ContaCorrente contaAtual = _items[i];
                if (contaAtual == item)
                {
                    indiceItem = i;
                    break;
                }
            }
            //Conta[0] Conta[1] Conta[2] Conta[3] Conta[4]
            for (int i = indiceItem; i < _items.Length-1; i++)
            {
                _items[i] = _items[i + 1];
            }
            proximaPosicao--;
            _items[proximaPosicao] = null;
        }
        public void ExibeLista()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    var conta = _items[i];
                    Console.WriteLine($" Indice[{i}] = " +
                        $"Conta:{conta.Conta} - " +
                        $"N° da Agência: {conta.Numero_agencia}");
                }
            }
        }
        private void VerificaCapacidade(int tamanhoNecessario)
        {
            if (tamanhoNecessario <= _items.Length)
            {
                return;
            }
            ContaCorrente[] listaAuxiliar = new ContaCorrente[tamanhoNecessario];

            for (int i = 0; i < _items.Length; i++) 
            {
                listaAuxiliar[i] = _items[i];
            }
            Console.WriteLine("Aumentando Capacidade");
            _items = listaAuxiliar;
        }

        public ContaCorrente MaiorSaldo()
        {
            ContaCorrente maiorSaldo = _items[0];

            for (int i = 1; i< _items.Length;i++)
            {
                ContaCorrente contaAtual = _items[i];
                if (contaAtual.Saldo > maiorSaldo.Saldo)
                {
                    maiorSaldo = contaAtual;
                }
            }
            Console.WriteLine("A conta com maior saldo é : " + maiorSaldo.Saldo + "Conta: " + maiorSaldo.Numero_agencia);
            return maiorSaldo;
        }
        public ContaCorrente RecuperarContaIndice(int indice)
        {
            if(indice < 0 || indice >= proximaPosicao)
            {
                throw new ArgumentOutOfRangeException(nameof(indice) + "Valor menor que zero ou maior que o tamanho da lista");
            }
            return _items[indice];
        }
        public int Tamanho { 
            get { return proximaPosicao; }
                }
        public ContaCorrente this[int indice]
        {
            get { return RecuperarContaIndice(indice); }
        }
    }
}
