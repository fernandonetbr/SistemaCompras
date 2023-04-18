using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }        
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        [NotMapped]
        public Money TotalGeral { get; private set; }
        public SituacaoCompra Situacao { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor, string data)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = SituacaoCompra.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public int RetornaCondicaoPagamento(double valor)
        {

            if (valor > 50000)
            {
                return new CondicaoPagamento(30).Valor;
            }
            else {
                return 0;
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
            /*foreach (var item in itens)
            {
                
            }*/
        }


    }
}
