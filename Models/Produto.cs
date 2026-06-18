namespace POOStore.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }

        // ENCAPSULAMENTO: "private set" impede que qualquer parte do codigo
        // mude o estoque direto (ex: produto.Estoque = 999). A unica forma de
        // alterar e pelos metodos abaixo, que controlam a regra de negocio.
        public int Estoque { get; private set; }
        public Categoria? Categoria { get; set; }

        public void AdicionarEstoque(int quantidade)
        {
            Estoque += quantidade;
        }

        public bool RemoverEstoque(int quantidade)
        {
            // regra de negocio: nao deixa estoque ficar negativo
            if (quantidade > Estoque)
                return false;

            Estoque -= quantidade;
            return true;
        }
    }
}
