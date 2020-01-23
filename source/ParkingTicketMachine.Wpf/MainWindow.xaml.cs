using ParkingTicketMachine.Core;
using System;
using System.Text;
using System.Windows;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FastClock.Instance.Factor = 3600;
            FastClock.Instance.IsRunning = true;
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
            SlotMachineWindow slot1 = new SlotMachineWindow("LIMESSTRAßE", OnTicketReady) { Owner = this };
            slot1.Show();
            SlotMachineWindow slot2 = new SlotMachineWindow("LINZERSTRAßE", OnTicketReady) { Owner = this };
            slot2.Show();
        }

        private void OnTicketReady(object sender, Ticket ticket)
        {
            AddLineToTextBox($"{ticket.SlotMachine.ToString()} | {ticket.ToString()}");
        }

        void AddLineToTextBox(string line)
        {
            StringBuilder text = new StringBuilder(TextBlockLog.Text);
            text.Append($"{FastClock.Instance.Time.ToShortTimeString()} | ");
            text.Append(line + "\n");
            TextBlockLog.Text = text.ToString();
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            Title = $"Ticketverwaltung: {FastClock.Instance.Time.ToShortTimeString()}";
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            SlotMachineWindow newSlot = new SlotMachineWindow(TextBoxAddress.Text, OnTicketReady) { Owner = this };
            newSlot.Show();
        }
    }
}
