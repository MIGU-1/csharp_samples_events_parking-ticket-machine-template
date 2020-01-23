using System;

namespace ParkingTicketMachine.Core
{
    public class SlotMachine
    {
        public int[] coins = { 10, 20, 50, 100, 200 };
        public EventHandler<Ticket> OnTicketReady;
        public Ticket _ticket;
        private int _sumCoins;

        public string Location { get; private set; }
        public SlotMachine(EventHandler<Ticket> ticketReady, string title)
        {
            _ticket = new Ticket(this);
            Location = title;
            OnTicketReady += ticketReady;
        }

        public string CalculateNewTicketEndTime(int coin)
        {
            string retText = _ticket.EndTime.ToShortTimeString();
            DateTime maxTime = _ticket.Starttime.AddMinutes(90);

            if (_ticket.EndTime < maxTime)
            {
                _sumCoins += coin;
                while (_sumCoins >= 50 && _ticket.EndTime < maxTime)
                {
                    _ticket.EndTime = _ticket.EndTime.AddMinutes(30);
                    retText = _ticket.EndTime.ToShortTimeString();
                    _sumCoins -= 50;
                }
            }

            _ticket.TicketPrice += coin;

            return retText;
        }

        public void InitTicketStartAndEndTime()
        {
            if (_ticket.Starttime < FastClock.Instance.Time)
            {
                _ticket.Starttime = FastClock.Instance.Time;
                _ticket.EndTime = FastClock.Instance.Time;
            }
        }

        public void ResetTicket()
        {
            _ticket.Starttime = DateTime.MinValue;
            _ticket.EndTime = DateTime.MinValue;
            _ticket.TicketPrice = 0;
        }

        public override string ToString()
        {
            return Location;
        }
    }
}
