using POOStore.Models;

namespace POOStore
{
    class Program
    {
        static Repositorio repo = new Repositorio();

        static void Main(string[] args)
        {
            repo.Carregar();
            Console.WriteLine("=== POO Store ===");

            while (true)
            {
                Console.WriteLine("\n1 - Entrar");
                Console.WriteLine("2 - Criar conta");
                Console.WriteLine("0 - Sair");
                Console.Write("Opcao: ");
                string op = Console.ReadLine() ?? "";

                if (op == "0") break;
                else if (op == "1") Entrar();
                else if (op == "2") CriarConta();
            }
        }

        static void Entrar()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            var usuario = repo.BuscarUsuario(email, senha);
            if (usuario == null)
            {
                Console.WriteLine("Email ou senha incorretos.");
                return;
            }

            Console.WriteLine($"\nBem-vindo, {usuario.Nome}!");
            usuario.ExibirTipo();
            usuario.AcessarSistema();

            if (usuario is Administrador admin)
                MenuAdmin(admin);
            else if (usuario is Cliente cliente)
                MenuCliente(cliente);
        }

        static void CriarConta()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            if (repo.EmailExiste(email))
            {
                Console.WriteLine("Email ja cadastrado.");
                return;
            }

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            Console.WriteLine("Tipo de conta:");
            Console.WriteLine("1 - Cliente Comum");
            Console.WriteLine("2 - Cliente Premium");
            Console.Write("Opcao: ");
            string tipo = Console.ReadLine() ?? "";

            int id = repo.ProximoIdCliente();

            if (tipo == "1")
                repo.ClientesComuns.Add(new ClienteComum { Id = id, Nome = nome, Email = email, Senha = senha });
            else if (tipo == "2")
                repo.ClientesPremium.Add(new ClientePremium { Id = id, Nome = nome, Email = email, Senha = senha });
            else
            {
                Console.WriteLine("Opcao invalida.");
                return;
            }

            repo.Salvar();
            Console.WriteLine("Conta criada com sucesso!");
        }

        // ===== MENU ADMIN =====

        static void MenuAdmin(Administrador admin)
        {
            while (true)
            {
                Console.WriteLine("\n=== Painel Admin ===");
                Console.WriteLine("1 - Listar produtos");
                Console.WriteLine("2 - Cadastrar produto");
                Console.WriteLine("3 - Editar estoque");
                Console.WriteLine("4 - Remover produto");
                Console.WriteLine("5 - Ver pedidos");
                Console.WriteLine("6 - Atualizar status de pedido");
                Console.WriteLine("0 - Sair");
                Console.Write("Opcao: ");
                string op = Console.ReadLine() ?? "";

                if (op == "0") break;
                else if (op == "1") { admin.GerenciarProdutos(); ListarProdutos(); }
                else if (op == "2") { admin.GerenciarProdutos(); CadastrarProduto(); }
                else if (op == "3") { admin.GerenciarProdutos(); EditarEstoque(); }
                else if (op == "4") { admin.GerenciarProdutos(); RemoverProduto(); }
                else if (op == "5") { admin.AcompanharPedidos(); ListarPedidos(); }
                else if (op == "6") { admin.AcompanharPedidos(); AtualizarStatusPedido(); }
            }
        }

        static void ListarProdutos()
        {
            Console.WriteLine("\n--- Produtos ---");
            if (repo.Produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }
            foreach (var p in repo.Produtos)
            {
                Console.WriteLine($"[{p.Id}] {p.Nome} | R$ {p.Preco:F2} | Estoque: {p.Estoque} | Categoria: {p.Categoria?.Nome}");
                if (p.Estoque <= 5 && p.Estoque > 0)
                    Console.WriteLine("  AVISO: estoque baixo");
                if (p.Estoque == 0)
                    Console.WriteLine("  AVISO: sem estoque");
            }
        }

        // Usa o metodo VisualizarProdutos da classe Cliente (presente no UML)
        static void VerProdutosCliente(Cliente cliente)
        {
            Console.WriteLine("\n--- Produtos Disponiveis ---");
            var disponiveis = cliente.VisualizarProdutos(repo.Produtos);

            if (disponiveis.Count == 0)
            {
                Console.WriteLine("Nenhum produto disponivel.");
                return;
            }

            foreach (var p in disponiveis)
                Console.WriteLine($"[{p.Id}] {p.Nome} | R$ {p.Preco:F2} | Estoque: {p.Estoque} | Categoria: {p.Categoria?.Nome}");
        }

        static void CadastrarProduto()
        {
            Console.WriteLine("\n--- Cadastrar Produto ---");

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Descricao: ");
            string desc = Console.ReadLine() ?? "";
            Console.Write("Preco: ");
            decimal.TryParse(Console.ReadLine(), out decimal preco);
            Console.Write("Quantidade em estoque: ");
            int.TryParse(Console.ReadLine(), out int estoque);

            Console.WriteLine("Categorias:");
            foreach (var c in repo.Categorias)
                Console.WriteLine($"[{c.Id}] {c.Nome}");
            Console.Write("ID da categoria: ");
            int.TryParse(Console.ReadLine(), out int catId);
            Categoria? categoria = repo.Categorias.Find(c => c.Id == catId);

            var produto = new Produto
            {
                Id = repo.ProximoIdProduto(),
                Nome = nome,
                Descricao = desc,
                Preco = preco,
                Categoria = categoria
            };
            produto.AdicionarEstoque(estoque);

            repo.Produtos.Add(produto);
            repo.Salvar();
            Console.WriteLine("Produto cadastrado.");
        }

        static void EditarEstoque()
        {
            ListarProdutos();
            Console.Write("ID do produto: ");
            int.TryParse(Console.ReadLine(), out int id);
            var produto = repo.Produtos.Find(p => p.Id == id);

            if (produto == null)
            {
                Console.WriteLine("Produto nao encontrado.");
                return;
            }

            Console.WriteLine($"Estoque atual: {produto.Estoque}");
            Console.WriteLine("1 - Adicionar  2 - Remover");
            Console.Write("Opcao: ");
            string op = Console.ReadLine() ?? "";
            Console.Write("Quantidade: ");
            int.TryParse(Console.ReadLine(), out int qtd);

            if (op == "1")
            {
                produto.AdicionarEstoque(qtd);
                Console.WriteLine($"Estoque atualizado: {produto.Estoque}");
            }
            else if (op == "2")
            {
                bool ok = produto.RemoverEstoque(qtd);
                if (ok) Console.WriteLine($"Estoque atualizado: {produto.Estoque}");
                else Console.WriteLine("Estoque insuficiente.");
            }

            repo.Salvar();
        }

        static void RemoverProduto()
        {
            ListarProdutos();
            Console.Write("ID do produto a remover: ");
            int.TryParse(Console.ReadLine(), out int id);
            var produto = repo.Produtos.Find(p => p.Id == id);

            if (produto == null)
            {
                Console.WriteLine("Produto nao encontrado.");
                return;
            }

            repo.Produtos.Remove(produto);
            repo.Salvar();
            Console.WriteLine("Produto removido.");
        }

        static void ListarPedidos()
        {
            Console.WriteLine("\n--- Pedidos ---");
            if (repo.Pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido.");
                return;
            }
            foreach (var ped in repo.Pedidos)
            {
                Console.WriteLine($"[{ped.Id}] {ped.Data:dd/MM/yyyy} | Status: {ped.Status} | Total: R$ {ped.ValorTotal:F2} | Pagamento: {ped.TipoPagamento}");
                foreach (var item in ped.Itens)
                    Console.WriteLine($"  - {item.Produto?.Nome} x{item.Quantidade} = R$ {item.Subtotal():F2}");
                if (ped.Endereco != null)
                    Console.WriteLine($"  Entrega: {ped.Endereco}");
            }
        }

        static void AtualizarStatusPedido()
        {
            ListarPedidos();
            Console.Write("ID do pedido: ");
            int.TryParse(Console.ReadLine(), out int id);
            var pedido = repo.Pedidos.Find(p => p.Id == id);

            if (pedido == null)
            {
                Console.WriteLine("Pedido nao encontrado.");
                return;
            }

            Console.WriteLine("1 - Pendente");
            Console.WriteLine("2 - Em Processamento");
            Console.WriteLine("3 - Enviado");
            Console.WriteLine("4 - Entregue");
            Console.Write("Novo status: ");
            string op = Console.ReadLine() ?? "";

            StatusPedido novoStatus = op switch
            {
                "1" => StatusPedido.Pendente,
                "2" => StatusPedido.EmProcessamento,
                "3" => StatusPedido.Enviado,
                "4" => StatusPedido.Entregue,
                _ => pedido.Status
            };

            pedido.AtualizarStatus(novoStatus);
            repo.Salvar();
            Console.WriteLine($"Status atualizado para: {pedido.Status}");
        }

        // ===== MENU CLIENTE =====

        static void MenuCliente(Cliente cliente)
        {
            while (true)
            {
                Console.WriteLine("\n=== Loja ===");
                Console.WriteLine("1 - Ver produtos");
                Console.WriteLine("2 - Adicionar ao carrinho");
                Console.WriteLine("3 - Ver carrinho");
                Console.WriteLine("4 - Finalizar compra");
                Console.WriteLine("5 - Meus pedidos");
                Console.WriteLine("0 - Sair");
                Console.Write("Opcao: ");
                string op = Console.ReadLine() ?? "";

                if (op == "0") break;
                else if (op == "1") VerProdutosCliente(cliente);
                else if (op == "2") AdicionarAoCarrinho(cliente);
                else if (op == "3") VerCarrinho(cliente);
                else if (op == "4") FinalizarCompra(cliente);
                else if (op == "5") VerMeusPedidos(cliente);
            }
        }

        static void AdicionarAoCarrinho(Cliente cliente)
        {
            ListarProdutos();
            Console.Write("ID do produto: ");
            int.TryParse(Console.ReadLine(), out int id);
            var produto = repo.Produtos.Find(p => p.Id == id);

            if (produto == null)
            {
                Console.WriteLine("Produto nao encontrado.");
                return;
            }
            if (produto.Estoque == 0)
            {
                Console.WriteLine("Produto sem estoque.");
                return;
            }

            Console.Write($"Quantidade (disponivel: {produto.Estoque}): ");
            int.TryParse(Console.ReadLine(), out int qtd);

            if (qtd <= 0 || qtd > produto.Estoque)
            {
                Console.WriteLine("Quantidade invalida.");
                return;
            }

            cliente.Carrinho.AdicionarItem(produto, qtd);
            Console.WriteLine("Item adicionado ao carrinho.");
        }

        static void VerCarrinho(Cliente cliente)
        {
            Console.WriteLine("\n--- Carrinho ---");
            if (cliente.Carrinho.Itens.Count == 0)
            {
                Console.WriteLine("Carrinho vazio.");
                return;
            }

            foreach (var item in cliente.Carrinho.Itens)
                Console.WriteLine($"- {item.Produto.Nome} x{item.Quantidade} = R$ {item.Subtotal():F2}");

            decimal total = cliente.Carrinho.CalcularTotal();
            decimal totalComDesconto = cliente.AplicarDesconto(total);
            Console.WriteLine($"Total: R$ {totalComDesconto:F2}");

            Console.WriteLine("\n1 - Remover item  2 - Limpar carrinho  0 - Voltar");
            Console.Write("Opcao: ");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                Console.Write("ID do produto a remover: ");
                int.TryParse(Console.ReadLine(), out int id);
                cliente.Carrinho.RemoverItem(id);
                Console.WriteLine("Item removido.");
            }
            else if (op == "2")
            {
                cliente.Carrinho.Limpar();
                Console.WriteLine("Carrinho limpo.");
            }
        }

        static void FinalizarCompra(Cliente cliente)
        {
            if (cliente.Carrinho.Itens.Count == 0)
            {
                Console.WriteLine("Carrinho vazio.");
                return;
            }

            // Verificar estoque
            foreach (var item in cliente.Carrinho.Itens)
            {
                if (item.Quantidade > item.Produto.Estoque)
                {
                    Console.WriteLine($"Estoque insuficiente para {item.Produto.Nome}.");
                    return;
                }
            }

            // Endereco
            Console.WriteLine("\n--- Endereco de Entrega ---");
            Console.Write("Rua: "); string rua = Console.ReadLine() ?? "";
            Console.Write("Numero: "); string numero = Console.ReadLine() ?? "";
            Console.Write("Bairro: "); string bairro = Console.ReadLine() ?? "";
            Console.Write("Cidade: "); string cidade = Console.ReadLine() ?? "";
            Console.Write("Estado: "); string estado = Console.ReadLine() ?? "";
            Console.Write("CEP: "); string cep = Console.ReadLine() ?? "";

            var endereco = new Endereco { Rua = rua, Numero = numero, Bairro = bairro, Cidade = cidade, Estado = estado, CEP = cep };

            // Pagamento
            Console.WriteLine("\n--- Pagamento ---");
            Console.WriteLine("1 - Pix");
            Console.WriteLine("2 - Cartao de Credito");
            Console.WriteLine("3 - Boleto");
            Console.Write("Opcao: ");
            string opPag = Console.ReadLine() ?? "";

            decimal total = cliente.Carrinho.CalcularTotal();
            decimal totalFinal = cliente.AplicarDesconto(total);

            int idPedido = repo.ProximoIdPedido();

            Pagamento pagamento;
            if (opPag == "1")
                pagamento = new PagamentoPix { Id = idPedido, Valor = totalFinal };
            else if (opPag == "2")
            {
                Console.Write("Parcelas: ");
                int.TryParse(Console.ReadLine(), out int parcelas);
                if (parcelas < 1) parcelas = 1;
                pagamento = new PagamentoCartao { Id = idPedido, Valor = totalFinal, Parcelas = parcelas };
            }
            else
                pagamento = new PagamentoBoleto { Id = idPedido, Valor = totalFinal };

            pagamento.ProcessarPagamento();

            // Descontar estoque
            foreach (var item in cliente.Carrinho.Itens)
                item.Produto.RemoverEstoque(item.Quantidade);

            // Criar pedido
            var pedido = new Pedido
            {
                Id = idPedido,
                ClienteId = cliente.Id,
                Itens = new List<ItemCarrinho>(cliente.Carrinho.Itens),
                ValorTotal = totalFinal,
                Data = DateTime.Now,
                Status = StatusPedido.EmProcessamento,
                TipoPagamento = pagamento.Forma,
                Endereco = endereco
            };

            repo.Pedidos.Add(pedido);
            cliente.Carrinho.Limpar();
            repo.Salvar();

            Console.WriteLine($"\nPedido #{pedido.Id} realizado com sucesso!");
        }

        static void VerMeusPedidos(Cliente cliente)
        {
            Console.WriteLine("\n--- Meus Pedidos ---");
            var pedidos = repo.Pedidos.FindAll(p => p.ClienteId == cliente.Id);

            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido encontrado.");
                return;
            }

            foreach (var ped in pedidos)
            {
                Console.WriteLine($"[{ped.Id}] {ped.Data:dd/MM/yyyy} | Status: {ped.Status} | Total: R$ {ped.ValorTotal:F2}");
                foreach (var item in ped.Itens)
                    Console.WriteLine($"  - {item.Produto?.Nome} x{item.Quantidade}");
            }
        }
    }
}
