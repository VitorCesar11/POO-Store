namespace POOStore.Models
{
    // ABSTRAÇÃO: Usuario é uma classe abstrata. Não existe "usuario" sozinho no
    // sistema, sempre é Cliente ou Administrador. A classe so guarda o que é
    // comum aos dois.
    public abstract class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        public bool Login(string email, string senha)
        {
            return Email == email && Senha == senha;
        }

        // Metodo abstrato: cada subclasse decide como exibir seu tipo
        public abstract void ExibirTipo();

        // POLIMORFISMO: metodo abstrato, cada tipo de usuario (Cliente ou
        // Administrador) implementa esse mesmo metodo de um jeito diferente
        public abstract void AcessarSistema();
    }
}
