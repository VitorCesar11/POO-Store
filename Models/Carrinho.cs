namespace POOStore.Models
{
    public class ItemCarrinho
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public ItemCarrinho(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public decimal Subtotal()
        {
            return Produto.Preco * Quantidade;
        }
    }

    public class Carrinho
    {
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();

        public void AdicionarItem(Produto produto, int quantidade)
        {
            var itemExistente = Itens.Find(i => i.Produto.Id == produto.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                Itens.Add(new ItemCarrinho(produto, quantidade));
            }
        }

        public void RemoverItem(int produtoId)
        {
            Itens.RemoveAll(i => i.Produto.Id == produtoId);
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Itens)
                total += item.Subtotal();
            return total;
        }

        public void Limpar()
        {
            Itens.Clear();
        }
    }
}
