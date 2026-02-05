using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickVento.Domain.Entities
{
    public class Venue
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        private Venue() { }
        public Venue(string name, int capacity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Venue Name cannot be empty.");
            if (capacity <= 0)
                throw new ArgumentException("Capacity can't be Zero!");
    
            Id = Guid.NewGuid();
            Name = name;
            Capacity = capacity;
        }
        public void UpdateVenueDetails(string name, int capacity  )
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Venue Name cannot be empty.");
            if (capacity <= 0)
                throw new ArgumentException("Capacity can't be Zero!");
            Name = name;
            Capacity = capacity;
        }
    }
}
