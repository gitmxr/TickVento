using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Enums;

namespace TickVento.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public UserRole Role { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<Booking> Bookings { get; private set; }
        public ICollection<Payment> Payments { get; private set; }


        // Parameterless constructor for EF Core
        private User() { }

        public User(string email, string fullName, UserRole role)
        {
            Id = Guid.NewGuid();
            Email = email;
            FullName = fullName;
            Role = role;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            Bookings = new List<Booking>();
            Payments = new List<Payment>();
        }
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
        public void SetRole(UserRole role) => Role = role;

    }
}
