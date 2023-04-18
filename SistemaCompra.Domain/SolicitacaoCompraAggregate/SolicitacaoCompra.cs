using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }        
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }        
        public decimal TotalGeral { get; private set; }
        public int CondicaoPagamento { get; set; }
        public SituacaoCompra Situacao { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor, decimal totalGeral, int condicaoPagto)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            TotalGeral = totalGeral;
            Situacao = SituacaoCompra.Solicitado;
            CondicaoPagamento = RetornaCondicaoPagamento(totalGeral, condicaoPagto);
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public int RetornaCondicaoPagamento(decimal valor, int condPagto)
        {

            if (valor > 50000)
            {
                return 30;
            }
            else {
                return condPagto;
            }            
            

        }

        public string RetornaQtItensCompra(int qtItens)
        {
            if (qtItens == 0)
            {
                throw new BusinessRuleException("O total de itens de compra deve ser maior que 0");
            }
            else
            {
                return qtItens.ToString();
            }

        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            
        }


    }
}
