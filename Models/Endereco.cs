namespace POOStore.Models
{
    public class Endereco
    {
        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }

        public override string ToString()
        {
            return $"{Rua}, {Numero} - {Bairro}, {Cidade}/{Estado} - CEP: {CEP}";
        }
    }
}
