using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Runtime.Serialization;

namespace Chat.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("Chat")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conversa>().HasMany<Usuario>(x => x.Participantes).WithMany(y => y.Conversas).Map(
                z => z.MapLeftKey("ConversaId")
                    .MapRightKey("UsuarioId")
                    .ToTable("ControleConversas")
                );

            modelBuilder.Entity<Usuario>().HasMany<Mensagem>(x => x.Mensagens).WithMany(y => y.Usuarios).Map(
                z => z.MapLeftKey("UsuarioId")
                    .MapRightKey("MensagemId")
                    .ToTable("ControleMensagens")
                );
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Conversa> Conversas { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

    }

    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public virtual List<Mensagem> Mensagens { get; set; }
        public virtual List<Conversa> Conversas { get; set; }
        // Diz se o usuário está logado
        public DateTime Logado { get; set; }
        public Usuario()
        {
            Mensagens = new List<Mensagem>();
            Conversas = new List<Conversa>();
            Logado = DateTime.MinValue;
        }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Nome do usuário")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Senha atual é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nova senha é obrigatória")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no máximo {1} e no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        [Display(Name = "Nome do usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }

        public DateTime Logado { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        [StringLength(50, ErrorMessage = "O {0} deve ter no máximo {1} e no mínimo {2} caracteres.", MinimumLength = 4)]
        [Display(Name = "Nome do usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no máximo {1} e no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
