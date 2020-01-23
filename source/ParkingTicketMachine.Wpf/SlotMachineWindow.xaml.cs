using System;
using System.Windows;
using ParkingTicketMachine.Core;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for SlotMachineWindow.xaml
    /// </summary>
    public partial class SlotMachineWindow
    {
        private SlotMachine _slotMachine;

        public SlotMachineWindow(string name, EventHandler<Ticket> ticketReady)
        {
            InitializeComponent();
            Title = name;
            _slotMachine = new SlotMachine(ticketReady, name);
        }

        private void ButtonInsertCoin_Click(object sender, RoutedEventArgs e)
        {
            FastClock.Instance.IsRunning = false;

            if (ListBoxCoins.SelectedIndex >= 0)
            {
                _slotMachine.InitTicketStartAndEndTime();
                TextBoxTimeUntil.Text = _slotMachine.CalculateNewTicketEndTime(_slotMachine.coins[ListBoxCoins.SelectedIndex]);
            }
            else
            {
                TextBoxTimeUntil.Text = "Keine Münze eingeworfen!";
            }
        }

        private void ButtonPrintTicket_Click(object sender, RoutedEventArgs e)
        {
            if (_slotMachine._ticket.Starttime == _slotMachine._ticket.EndTime)
            {
                MessageBox.Show("Erst Münze einwerfen!");
            }
            else
            {
                _slotMachine.OnTicketReady.Invoke(this, _slotMachine._ticket);
                MessageBox.Show($"Sie dürfen bis {_slotMachine._ticket.EndTime.ToShortTimeString()} parken!");
                _slotMachine.ResetTicket();
                TextBoxTimeUntil.Text = "";
                FastClock.Instance.IsRunning = true;
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            _slotMachine.ResetTicket();
            TextBoxTimeUntil.Text = "";
        }
    }
}
