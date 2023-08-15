using PocDDD.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocDDD.Domain.Entities
{   

    public class User
    {
        private User() { }

        public User(int id, string firstName, string lastName, string email, string password)
        {
            Id = id;
            ValidateDomain(firstName, lastName, email, password);

        }       

        public void Update(string firstName, string lastName, string email, string password)
        {
            ValidateDomain(firstName, lastName, email, password);
        }

        public void ValidateDomain(string firstName, string lastName, string email, string password)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(firstName), "Nome inválido.");

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(lastName), "Sobrenome inválido.");

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email), "E-mail inválido.");

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(password), "Senha inválida.");

            DomainExceptionValidation.When(EmailValidation.ValidateEmail(email), "Formato de e-mail inválido.");
           
        }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }


}
