# POO-Store

# Entrega Parcial - Modelagem do Sistema em POO

## 1. Apresentação do sistema
[cite_start]O POO Store é um sistema de loja virtual desenvolvido com base nos quatro pilares da Programação Orientada a Objetos (POO): encapsulamento, herança, polimorfismo e abstração[cite: 183]. [cite_start]O sistema simula o funcionamento de um, permitindo o cadastro de produtos, gestão de clientes, realização de pedidos e controle de estoque, tudo estruturado por meio de classes e objetos bem definidos[cite: 184].

**Para quem serve:**
[cite_start]O sistema é voltado para pequenos e médios lojistas que desejam disponibilizar seus produtos online[cite: 186]. [cite_start]A arquitetura orientada a objetos garante que o código seja organizado, reutilizável e de fácil manutenção[cite: 187].

**Principais funcionalidades:**
* [cite_start]Cadastro de Produtos: registro de itens com nome, descrição, preço e quantidade em estoque, utilizando classes especializadas por categoria (ex.: Eletrônico, Vestuário, Alimento)[cite: 191].
* [cite_start]Gestão de Clientes: cadastro de clientes com dados pessoais e histórico de compras, com distinção entre Cliente Comum e Cliente Premium[cite: 192].
* [cite_start]Carrinho de Compras: adição e remoção de produtos, com cálculo automático do total e aplicação de descontos conforme o tipo de cliente[cite: 195].
* [cite_start]Realização de Pedidos: geração de pedidos com status atualizável (Pendente, Em processamento, Enviado, Entregue), encapsulando as regras de negócio[cite: 196].
* [cite_start]Controle de Estoque: atualização automática da quantidade disponível ao confirmar um pedido, com alertas de estoque baixo[cite: 197].
* [cite_start]Formas de Pagamento: suporte a múltiplos métodos (Cartão de Crédito, Boleto, Pix), cada um implementado como uma classe concreta derivada de uma interface abstrata Pagamento[cite: 198, 199].

## 2. Modelagem do sistema
**Classes Principais e Papéis:**
* [cite_start]**Usuario:** Representa uma pessoa que acessa o sistema, armazenando dados comuns como nome, email e senha[cite: 205].
* [cite_start]**Cliente:** Representa o usuário que realiza compras na loja, podendo visualizar produtos, adicionar itens ao carrinho e finalizar pedidos[cite: 207].
* [cite_start]**Administrador:** Representa o usuário responsável por gerenciar a loja, cadastrando produtos, atualizando estoque e acompanhando pedidos[cite: 208, 210].
* [cite_start]**Produto:** Representa os itens vendidos na loja, contendo informações como nome, descrição, preço, estoque e categoria[cite: 212].
* [cite_start]**Categoria:** Organiza os produtos por tipo, facilitando a separação e busca dos itens na loja[cite: 214, 215].
* [cite_start]**Carrinho:** Armazena temporariamente os produtos escolhidos pelo cliente antes da finalização da compra[cite: 217].
* [cite_start]**ItemCarrinho:** Representa cada produto dentro do carrinho, junto com a quantidade escolhida pelo cliente[cite: 219, 221].
* [cite_start]**Pedido:** Representa a compra finalizada pelo cliente, contendo os itens comprados, valor total, data e status do pedido[cite: 223].
* [cite_start]**Pagamento:** Representa a simulação do pagamento do pedido, armazenando forma de pagamento, valor e status[cite: 225].
* [cite_start]**Endereco:** Guarda os dados de entrega do cliente, como rua, número, bairro, cidade, estado e CEP[cite: 227].

**Relacionamento entre as Classes:**
[cite_start]O sistema terá uma classe Usuario, que servirá como base para Cliente e Administrador, pois ambos possuem dados em comum, como nome, email e senha[cite: 229]. [cite_start]O Cliente poderá possuir um Carrinho, onde serão adicionados os produtos escolhidos[cite: 230]. [cite_start]O Carrinho será formado por vários ItemCarrinho, e cada ItemCarrinho estará relacionado a um Produto[cite: 231]. [cite_start]Cada Produto poderá pertencer a uma Categoria, permitindo organizar melhor os itens da loja[cite: 232]. [cite_start]Quando o cliente finalizar a compra, o carrinho dará origem a um Pedido[cite: 233]. [cite_start]Esse pedido terá uma lista de itens, um valor total, um status e estará relacionado a um Pagamento simulado[cite: 234]. [cite_start]Além disso, o pedido utilizará um Endereco para representar o local de entrega[cite: 235]. [cite_start]O Administrador será responsável por gerenciar os produtos, categorias, estoque e acompanhar os pedidos realizados pelos clientes[cite: 236].

## 3. Aplicação dos quatro pilares da POO
[cite_start]Os pilares da POO ajudam a deixar o sistema mais organizado, seguro e fácil de manter[cite: 83]. [cite_start]No sistema da loja virtual, eles são usados para melhorar a estrutura e facilitar futuras melhorias[cite: 84]:
* [cite_start]**Abstração:** A abstração consiste em representar apenas as informações mais importantes de um objeto[cite: 50]. [cite_start]No sistema da loja virtual, cada classe representa algo do mundo real: Produto (nome, preço e estoque), Cliente (nome, email e senha) e Pedido (itens e valor total)[cite: 51, 52, 53, 54, 55]. [cite_start]Isso facilita a organização do sistema[cite: 56].
* [cite_start]**Encapsulamento:** O encapsulamento protege os dados da classe, permitindo acesso apenas por métodos específicos[cite: 58]. [cite_start]Na classe Produto, o estoque não deve ser alterado diretamente[cite: 60]. [cite_start]O sistema pode usar métodos para adicionar ou remover quantidade do estoque[cite: 61]. [cite_start]Isso aumenta a segurança e organização do código[cite: 62].
* [cite_start]**Herança:** A herança permite que uma classe herde características de outra[cite: 64]. [cite_start]No sistema, Cliente e Administrador herdam informações da classe Usuario, como: nome, email e senha[cite: 65, 66, 67, 68]. [cite_start]Assim, evita repetição de código[cite: 69].
* [cite_start]**Polimorfismo:** O polimorfismo permite que métodos com o mesmo nome tenham comportamentos diferentes[cite: 71]. [cite_start]O método acessarSistema() pode funcionar de forma diferente: Cliente (realiza compras) e Administrador (gerencia produtos e pedidos)[cite: 73, 74, 75, 76, 77]. [cite_start]Outro exemplo é o processamento de pagamentos: Pix, Cartão e Boleto[cite: 78, 79, 80, 81]. [cite_start]Cada um funciona de uma maneira diferente[cite: 82].

## 4. Decisões iniciais
* **Formato de armazenamento:** `[Christian deve inserir o formato definido: .txt, .xml ou .json]`
* **Divisão de tarefas entre os integrantes:**
    * **Samuel Marques:** Apresentação do sistema, explicando o que é a loja virtual, para quem serve e quais funcionalidades ela vai ter.
    * **Rafael Bernardoni:** Listagem das classes principais (Cliente, Produto, Carrinho, Pedido, Pagamento, Administrador, etc.) e explicação do papel de cada uma.
    * **Gustavo Ribeiro:** Mapeamento do relacionamento entre as classes e elaboração da explicação/diagrama UML de conexão entre elas.
    * **Gustavo (Vargas):** Explicação da aplicação dos 4 pilares da POO (abstração, encapsulamento, herança e polimorfismo) no sistema.
    * **Christian Fernandes:** Definição das decisões iniciais, incluindo o formato de armazenamento escolhido, e estruturação da divisão de tarefas do grupo.
    * **Vitor Cesar Arruda Xavier:** Criação e configuração do repositório no GitHub, elaboração do `README.md` (com nome do projeto, integrantes e descrição) e unificação de todas as partes no documento PDF final.

## 5. Repositório GitHub
**Link do repositório:** `[Insira aqui o link do repositório configurado no GitHub]`

**Conteúdo do arquivo README.md (já formatado para o GitHub):**
# POO Store

**Descrição:**
[cite_start]O POO Store é um sistema de loja virtual desenvolvido com base nos quatro pilares da Programação Orientada a Objetos (POO): encapsulamento, herança, polimorfismo e abstração[cite: 183]. [cite_start]O sistema permite o cadastro de produtos, gestão de clientes, realização de pedidos e controle de estoque[cite: 184]. [cite_start]Ele é voltado para pequenos e médios lojistas que desejam disponibilizar seus produtos online com um sistema organizado e de fácil manutenção[cite: 186, 187].

**Integrantes:**
1. Vitor Cesar Arruda Xavier
2. Christian Fernandes
3. Gustavo Ribeiro
4. Rafael Bernardoni
5. Samuel Marques
6. Gustavo Vargas