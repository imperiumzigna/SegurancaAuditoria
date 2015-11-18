using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Runtime.Serialization;
using System.Linq;

namespace Chat.Models
{
    [Table("Mensagens")]
    public class Mensagem
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MensagemId { get; set; }
        public string Autor { get; set; }
        public string Conteudo { get; set; }
        public long Hora { get; set; }
        public virtual Conversa Conversa { get; set; }
        public virtual List<Usuario> Usuarios { get; set; }

        public Mensagem()
        {
            Usuarios = new List<Usuario>();
        }

        public Mensagem(string a, string c, long h)
        {
            Autor = a;
            Conteudo = c;
            Hora = h;
            Usuarios = new List<Usuario>();
        }

        public string GetAutor(string myname)
        {
            if (myname == Autor)
            {
                return "Você";
            }

            return Autor;
        }
    }

    [Table("Conversas")]
    public class Conversa
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ConversaId { get; set; }
        public string NomeConversa { get; set; }
        public virtual List<Mensagem> Mensagens { get; set; }
        public virtual List<Usuario> Participantes { get; set; }
        [NotMapped]
        public bool IsGrupo
        {
            get
            {
                return Participantes.Count > 2;
            }
        }

        public Conversa()
        {
            Participantes = new List<Usuario>();
            Mensagens = new List<Mensagem>();
        }

        public string GetNomeConversa(string myname)
        {
            if (IsGrupo)
            {
                return NomeConversa;
            }
            else
            {
                return Participantes.FirstOrDefault(x => x.UsuarioNome != myname).UsuarioNome;
            }
        }

        public void BroadCast(Mensagem m)
        {
            foreach (var item in Participantes)
            {
                item.Mensagens.Add(m);
            }
        }
    }

    [DataContract]
    public class MensagemJson
    {
        [DataMember]
        public int MensagemId { get; set; }
        [DataMember]
        public string Autor { get; set; }
        [DataMember]
        public string NomeConversa { get; set; }
        [DataMember]
        public string Conteudo { get; set; }
        [DataMember]
        public string Hora { get; set; }

        public MensagemJson()
        {
        }

        public MensagemJson(int id, string autor, string nomeconversa, string c, string h)
        {
            MensagemId = id;
            Autor = autor;
            Conteudo = c;
            Hora = h;
            NomeConversa = nomeconversa;
        }
    }

    //[DataContract]
    //public class ConversaJson
    //{
    //    [DataMember]
    //    public int ConversaId { get; set; }
    //    [DataMember]
    //    public string NomeConversa { get; set; }
    //    [DataMember]
    //    public IEnumerable<MensagemJson> Mensagens { get; set; }

    //    public ConversaJson()
    //    {
    //        Mensagens = new List<MensagemJson>();
    //    }

    //    public ConversaJson(string nomeconversa, IEnumerable<MensagemJson> m)
    //    {
    //        Mensagens = m;
    //        NomeConversa = nomeconversa;
    //    }
    //}
}
