namespace POOStore.Models
{
    // HERANÇA: Cliente herda de Usuario (Id, Nome, Email, Senha, Login).
    // Continua abstrata porque na pratica so existe ClienteComum ou ClientePremium.
    public abstract class Cliente : Usuario
    {
        public Endereco? Endereco { get; set; }
        public Carrinho Carrinho { get; set; } = new Carrinho();

        // Metodo abstrato: cada tipo de cliente decide se da desconto ou nao
        public abstract decimal AplicarDesconto(decimal total);

        // Cliente acessa o sistema podendo comprar (UML)
        public override void AcessarSistema()
        {
            Console.WriteLine($"{Nome} entrou no sistema como cliente.");
        }

        // Cliente visualiza os produtos disponiveis (UML)
        public List<Produto> VisualizarProdutos(List<Produto> produtos)
        {
            return produtos.FindAll(p => p.Estoque > 0);
        }
    }

    // HERANÇA: ClienteComum herda de Cliente (que ja herda de Usuario)
    public class ClienteComum : Cliente
    {
        public override void ExibirTipo()
        {
            Console.WriteLine("Tipo: Cliente Comum");
        }

        // POLIMORFISMO: aqui o desconto e zero
        public override decimal AplicarDesconto(decimal total)
        {
            return total;
        }
    }

    // HERANÇA: ClientePremium tambem herda de Cliente
    public class ClientePremium : Cliente
    {
        public override void ExibirTipo()
        {
            Console.WriteLine("Tipo: Cliente Premium (10% de desconto)");
        }

        // POLIMORFISMO: mesmo metodo AplicarDesconto, mas aqui aplica 10%
        // -> essa e a diferenca pratica entre ClienteComum e ClientePremium
        public override decimal AplicarDesconto(decimal total)
        {
            decimal desconto = total * 0.10m;
            Console.WriteLine($"Desconto de 10% aplicado: -R$ {desconto:F2}");
            return total - desconto;
        }
    }

    // HERANÇA: Administrador herda direto de Usuario (nao e Cliente)
    public class Administrador : Usuario
    {
        public override void ExibirTipo()
        {
            Console.WriteLine("Tipo: Administrador");
        }

        // POLIMORFISMO: mesmo metodo AcessarSistema de Usuario, mas aqui o
        // comportamento e diferente do Cliente (compara com Cliente.AcessarSistema)
        public override void AcessarSistema()
        {
            Console.WriteLine($"{Nome} entrou no sistema como administrador.");
        }

        // Administrador gerencia o catalogo de produtos (UML)
        public void GerenciarProdutos()
        {
            Console.WriteLine("Administrador acessando o gerenciamento de produtos.");
        }

        // Administrador acompanha os pedidos da loja (UML)
        public void AcompanharPedidos()
        {
            Console.WriteLine("Administrador acompanhando os pedidos realizados.");
        }
    }
}
