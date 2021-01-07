using System.Collections.Generic;
using MoviesBooking.Models;

namespace MoviesBooking.ViewModel
{
    public class TicketViewModel
    {
        public Ticket ticket { get; set; }
        public List<Ticket> tickets { get; set; }
    }
}

