using System.Text.Json;
using POOStore.Models;

namespace POOStore
{
    public class Repositorio
    {
        private readonly string pasta = "dados";
        private readonly JsonSerializerOptions opcoes = new JsonSerializerOptions { WriteIndented = true };

        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<ClienteComum> ClientesComuns { get; set; } = new List<ClienteComum>();
        public List<ClientePremium> ClientesPremium { get; set; } = new List<ClientePremium>();
        public List<Administrador> Administradores { get; set; } = new List<Administrador>();
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public void Carregar()
        {
            Directory.CreateDirectory(pasta);

            Produtos = Ler<List<Produto>>("produtos.json") ?? new List<Produto>();
            Categorias = Ler<List<Categoria>>("categorias.json") ?? new List<Categoria>();
            ClientesComuns = Ler<List<ClienteComum>>("clientes_comuns.json") ?? new List<ClienteComum>();
            ClientesPremium = Ler<List<ClientePremium>>("clientes_premium.json") ?? new List<ClientePremium>();
            Administradores = Ler<List<Administrador>>("administradores.json") ?? new List<Administrador>();
            Pedidos = Ler<List<Pedido>>("pedidos.json") ?? new List<Pedido>();

            // Reconectar objetos Produto dentro dos pedidos
            foreach (var pedido in Pedidos)
                foreach (var item in pedido.Itens)
                    item.Produto = Produtos.Find(p => p.Id == item.Produto?.Id) ?? item.Produto;

            DadosIniciais();
        }

        public void Salvar()
        {
            Gravar("produtos.json", Produtos);
            Gravar("categorias.json", Categorias);
            Gravar("clientes_comuns.json", ClientesComuns);
            Gravar("clientes_premium.json", ClientesPremium);
            Gravar("administradores.json", Administradores);
            Gravar("pedidos.json", Pedidos);
        }

        private T? Ler<T>(string arquivo)
        {
            string caminho = Path.Combine(pasta, arquivo);
            if (!File.Exists(caminho)) return default;
            return JsonSerializer.Deserialize<T>(File.ReadAllText(caminho), opcoes);
        }

        private void Gravar<T>(string arquivo, T dados)
        {
            File.WriteAllText(Path.Combine(pasta, arquivo), JsonSerializer.Serialize(dados, opcoes));
        }

        // IDs
        public int ProximoIdProduto() => Produtos.Count > 0 ? Produtos.Max(p => p.Id) + 1 : 1;
        public int ProximoIdCategoria() => Categorias.Count > 0 ? Categorias.Max(c => c.Id) + 1 : 1;
        public int ProximoIdPedido() => Pedidos.Count > 0 ? Pedidos.Max(p => p.Id) + 1 : 1;
        public int ProximoIdCliente()
        {
            int maxComum = ClientesComuns.Count > 0 ? ClientesComuns.Max(c => c.Id) : 0;
            int maxPremium = ClientesPremium.Count > 0 ? ClientesPremium.Max(c => c.Id) : 0;
            int maxAdmin = Administradores.Count > 0 ? Administradores.Max(a => a.Id) : 0;
            return Math.Max(maxComum, Math.Max(maxPremium, maxAdmin)) + 1;
        }

        public Usuario? BuscarUsuario(string email, string senha)
        {
            Usuario? u = ClientesComuns.Find(c => c.Login(email, senha));
            if (u != null) return u;
            u = ClientesPremium.Find(c => c.Login(email, senha));
            if (u != null) return u;
            return Administradores.Find(a => a.Login(email, senha));
        }

        public bool EmailExiste(string email)
        {
            return ClientesComuns.Exists(c => c.Email == email)
                || ClientesPremium.Exists(c => c.Email == email)
                || Administradores.Exists(a => a.Email == email);
        }

        private void DadosIniciais()
        {
            if (Administradores.Count == 0)
            {
                Administradores.Add(new Administrador { Id = 1, Nome = "Admin", Email = "admin@loja.com", Senha = "123" });
            }

            if (Categorias.Count == 0)
            {
                Categorias.Add(new Categoria { Id = 1, Nome = "Eletronicos", Descricao = "Produtos eletronicos em geral" });
                Categorias.Add(new Categoria { Id = 2, Nome = "Vestuario", Descricao = "Roupas e acessorios" });
                Categorias.Add(new Categoria { Id = 3, Nome = "Alimentos", Descricao = "Itens alimenticios" });
            }

            if (Produtos.Count == 0)
            {
                var p1 = new Produto { Id = 1, Nome = "Fone Bluetooth", Descricao = "Sem fio", Preco = 150.00m, Categoria = Categorias[0] };
                p1.AdicionarEstoque(10);
                Produtos.Add(p1);

                var p2 = new Produto { Id = 2, Nome = "Camiseta", Descricao = "Algodao", Preco = 40.00m, Categoria = Categorias[1] };
                p2.AdicionarEstoque(20);
                Produtos.Add(p2);

                var p3 = new Produto { Id = 3, Nome = "Cafe 500g", Descricao = "Arabica", Preco = 30.00m, Categoria = Categorias[2] };
                p3.AdicionarEstoque(15);
                Produtos.Add(p3);
            }

            Salvar();
        }
    }
}
