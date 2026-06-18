# POO Store

Sistema de loja virtual desenvolvido como trabalho prático da disciplina de Programação Orientada a Objetos (POO), aplicando os quatro pilares: abstração, encapsulamento, herança e polimorfismo.

## Integrantes

| Nome | Responsabilidade |
|---|---|
| Samuel | Apresentação do sistema |
| Rafael | Classes principais e responsabilidades |
| Gustavo Ribeiro | Relacionamento entre classes e diagrama UML |
| Gustavo | Quatro pilares da POO |
| Christian | Decisões iniciais e formato de armazenamento |
| Vitor | Repositório GitHub e README |

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

Verifique se está instalado:

```bash
dotnet --version
```

## Como compilar e executar

```bash
# Clone o repositório
git clone https://github.com/VitorCesar11/POO-Store.git

# Entre na pasta do projeto
cd POO-Store

# Execute
dotnet run
```

O projeto compila automaticamente ao executar. Não é necessário rodar `dotnet build` separadamente.

## Login padrão (admin)

| Campo | Valor |
|---|---|
| Email | admin@loja.com |
| Senha | 123 |

## Estrutura do projeto

```
POOStore/
├── Models/
│   ├── Usuario.cs        # Classe abstrata base (Abstração + Polimorfismo)
│   ├── Cliente.cs        # Cliente, ClienteComum, ClientePremium, Administrador (Herança)
│   ├── Produto.cs        # Estoque encapsulado com private set (Encapsulamento)
│   ├── Categoria.cs
│   ├── Carrinho.cs       # ItemCarrinho e Carrinho
│   ├── Pedido.cs         # Enum StatusPedido
│   ├── Pagamento.cs      # Classe abstrata + Pix, Cartão, Boleto (Polimorfismo)
│   └── Endereco.cs
├── Repositorio.cs        # Leitura e gravação em JSON
├── Program.cs            # Menu principal e fluxo do sistema
└── POOStore.csproj
```

## Armazenamento

Os dados são salvos automaticamente na pasta `dados/` em arquivos JSON:

```
dados/
├── produtos.json
├── categorias.json
├── clientes_comuns.json
├── clientes_premium.json
├── administradores.json
└── pedidos.json
```

A pasta é criada automaticamente na primeira execução.

## Funcionalidades

**Cliente:**
- Visualizar produtos disponíveis por categoria
- Adicionar e remover itens do carrinho
- Finalizar compra com endereço de entrega
- Escolher forma de pagamento (Pix, Cartão de Crédito ou Boleto)
- Desconto automático de 10% para clientes Premium
- Consultar histórico de pedidos

**Administrador:**
- Cadastrar, editar e remover produtos
- Ajustar estoque (adicionar ou remover unidades)
- Visualizar alertas de estoque baixo
- Acompanhar pedidos e atualizar status (Pendente → Em Processamento → Enviado → Entregue)
- Gerenciar categorias
