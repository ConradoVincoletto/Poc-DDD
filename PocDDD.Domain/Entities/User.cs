using PocDDD.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace PocDDD.Domain.Entities
{

    public class User
    {
        private User() { }


        public User(bool isActive, string firstName,  string lastName, string email, string password)
        {
            ValidateDomain(isActive, firstName, lastName, email, password);
            CreatePasswordHash(password);
        }


        public void Update(int id, bool isActive, string firstName, string lastName, string email, string password)
        {
            UserId = id;
            ValidateDomain(isActive, firstName, lastName, email, password);
            CreatePasswordHash(password);
        }       

        public void ValidateDomain(bool isActive, string firstName, string lastName, string email, string password)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(firstName), "Nome inválido.");

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(lastName), "Sobrenome inválido.");

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email), "E-mail inválido.");

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(password), "Senha inválida.");

            DomainExceptionValidation.When(!EmailValidation.ValidateEmail(email), "Formato de e-mail inválido.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsActive = isActive;
           
        }

        private void CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool CheckHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmacsha = new HMACSHA512(passwordSalt))
            {
                var hashCalculado = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < hashCalculado.Length; i++)
                {
                    if (hashCalculado[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int UserId { get; private set; }
        public bool IsActive { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        [NotMapped]
        public string Password { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; set; }
        public virtual List<Order> Orders { get; private set; }
        public string AutomaticMigrationTest { get; private set; }
        public string AutomaticMigrationTest2 { get; private set; }
    }
}
