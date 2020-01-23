using System;

namespace ParkingTicketMachine.Core
{
    public class Ticket
    {
        public int TicketPrice { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime EndTime { get; set; }

        public SlotMachine SlotMachine { get; private set; }

        public Ticket(SlotMachine slotMachine)
        {
            SlotMachine = slotMachine;
            Starttime = DateTime.Now;
            EndTime = DateTime.Now;
            TicketPrice = 0;
        }

        public override string ToString()
        {
            return $"Gültig von {Starttime} bis {EndTime}! Einwurf = {TicketPrice}";
        }
    }
}
