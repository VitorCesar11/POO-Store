namespace POOStore.Models
{
    public enum StatusPedido
    {
        Pendente,
        EmProcessamento,
        Enviado,
        Entregue
    }

    // Pedido "nasce" de um Carrinho quando o cliente finaliza a compra.
    // Guarda os ItemCarrinho (composicao) e um Endereco e um TipoPagamento (associacao)
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public StatusPedido Status { get; set; }
        public string? TipoPagamento { get; set; }
        public Endereco? Endereco { get; set; }

        // encapsulamento simples: status só muda atraves desse metodo
        public void AtualizarStatus(StatusPedido novoStatus)
        {
            Status = novoStatus;
        }

        // Metodo presente no UML: recalcula o total a partir dos itens do pedido
        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Itens)
                total += item.Subtotal();
            return total;
        }
    }
}
