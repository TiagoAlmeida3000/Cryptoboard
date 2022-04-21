using CryptoBoard.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoBoard.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public DateTime CreationDate { get; private set; }

        public ICollection<WatchList> WatchLists { get; set; }

        public User(string userName, string email, string password)
        {
            ValidateDomain(userName, email, password);
        }
        public User(int id, string userName, string email, string password)
        {
            DomainExceptionValidation.When(id < 0, "Id invalido");

            Id = id;

            ValidateDomain(userName, email, password);
        }

        public void Update(string userName, string email, string password)
        {
            ValidateDomain(userName, email, password);
        }

        private void ValidateDomain(string userName, string email, string password)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(userName),
                "Nome Invalido. Nome é requerido");

            DomainExceptionValidation.When(userName.Length < 5,
                "Nome Invalido. Nome tem que ter no minimo 3 caracteres");

            DomainExceptionValidation.When(userName.Length > 50,
                "Nome Invalido. Nome tem que ter no maximo 50 caracteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(email),
                "Email invalido. Email é requerido");

            DomainExceptionValidation.When(!Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)),
                "Email invalido. Digite Novamente");

            DomainExceptionValidation.When(string.IsNullOrEmpty(password),
                "Senha invalida. Senha é requerida");

            DomainExceptionValidation.When(!Regex.IsMatch(password, @"^.*(?=.{8,})(?=.*[\d])(?=.*[\W]).*$"),
                "Senha invalida. Senha tem que conter pelo menos 8 caracteres, pelos menos 1 digito e um caractere especial ");

            UserName = userName;

            Email = email;

            Password = password;

            CreationDate = DateTime.Now;
        }

    }



    


}
