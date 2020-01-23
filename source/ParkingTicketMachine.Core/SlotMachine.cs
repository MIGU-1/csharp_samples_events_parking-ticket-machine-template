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
            _ticket.TicketPrice += coin;
            _sumCoins += coin;
            int factor = _sumCoins / 50;
            _sumCoins = _sumCoins % 50;
            if (factor > 3)
                factor = 3;
            int addMinutes = factor * 30;
            DateTime endTimeForecast = _ticket.EndTime.AddMinutes(addMinutes);

            if (_ticket.EndTime.Hour >= 18 || _ticket.EndTime.Hour <= 6)
            {
                _ticket.EndTime = _ticket.EndTime.AddDays(1);
                _ticket.EndTime = new DateTime(_ticket.EndTime.Year, _ticket.EndTime.Month, _ticket.EndTime.Day, 6, 0, 0);
                _ticket.EndTime = _ticket.EndTime.AddMinutes(addMinutes);
            }
            else if (endTimeForecast.Hour >= 18 || endTimeForecast.Hour <= 6)
            {
                DateTime tmp = _ticket.EndTime;
                int min = 0;

                while (tmp.Hour < 18)
                {
                    tmp = tmp.AddMinutes(1);
                    min++;
                }

                _ticket.EndTime = _ticket.EndTime.AddDays(1);
                _ticket.EndTime = new DateTime(_ticket.EndTime.Year, _ticket.EndTime.Month, _ticket.EndTime.Day, 6, 0, 0);
                _ticket.EndTime = _ticket.EndTime.AddMinutes(addMinutes - min);
            }
            else
            {
                _ticket.EndTime = _ticket.EndTime.AddMinutes(addMinutes);
            }

            return _ticket.EndTime.ToShortTimeString();
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
