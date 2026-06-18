namespace POOStore.Models
{
    // ABSTRAÇÃO: Pagamento e abstrata porque "pagamento" sozinho nao existe,
    // sempre e Pix, Cartao ou Boleto. So define o que e comum aos tres.
    public abstract class Pagamento
    {
        public int Id { get; set; }
        public string? Forma { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; } = "Pendente";

        // metodo abstrato: cada forma de pagamento implementa do seu jeito
        public abstract void ProcessarPagamento();
    }

    // HERANÇA: PagamentoPix herda de Pagamento
    public class PagamentoPix : Pagamento
    {
        public PagamentoPix()
        {
            Forma = "Pix";
        }

        // POLIMORFISMO: mesmo metodo ProcessarPagamento, comportamento proprio do Pix
        public override void ProcessarPagamento()
        {
            Console.WriteLine("Pagamento via Pix.");
            Console.WriteLine("Chave Pix: poostore@loja.com");
            Console.WriteLine($"Valor: R$ {Valor:F2}");
            Console.WriteLine("Pix confirmado.");
            Status = "Confirmado";
        }
    }

    // HERANÇA: PagamentoCartao herda de Pagamento
    public class PagamentoCartao : Pagamento
    {
        public int Parcelas { get; set; }

        public PagamentoCartao()
        {
            Forma = "Cartao de Credito";
        }

        // POLIMORFISMO: mesmo metodo, mas aqui calcula parcelas
        public override void ProcessarPagamento()
        {
            Console.WriteLine("Pagamento via Cartao de Credito.");
            Console.WriteLine($"Valor: R$ {Valor:F2} em {Parcelas}x de R$ {Valor / Parcelas:F2}");
            Console.WriteLine("Cartao aprovado.");
            Status = "Confirmado";
        }
    }

    // HERANÇA: PagamentoBoleto herda de Pagamento
    public class PagamentoBoleto : Pagamento
    {
        public PagamentoBoleto()
        {
            Forma = "Boleto";
        }

        // POLIMORFISMO: mesmo metodo, aqui gera data de vencimento
        public override void ProcessarPagamento()
        {
            Console.WriteLine("Pagamento via Boleto.");
            Console.WriteLine($"Valor: R$ {Valor:F2}");
            Console.WriteLine($"Vencimento: {DateTime.Now.AddDays(3):dd/MM/yyyy}");
            Console.WriteLine("Boleto gerado.");
            Status = "Confirmado";
        }
    }
}
