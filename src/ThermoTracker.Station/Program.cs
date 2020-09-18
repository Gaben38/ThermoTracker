using System;
using System.Device.Gpio;
using System.Threading.Tasks;

namespace ThermoTracker.Station
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (var display = new Tm1637Display(3, 2, PinNumberingScheme.Logical))
            {
                display.TurnedOn = true;
                display.Brightness = 7;
                display.DisplayDigit(0);
                await Task.Delay(TimeSpan.FromSeconds(10));
                await FlashScreen(display, 5);

                display.TurnedOn = false;

                Console.WriteLine("This is it folks!");
            }
        }

        private static async Task FlashScreen(Tm1637Display display, int numberOfFlashes, TimeSpan? delay = null)
        {
            if (delay == null)
                delay = TimeSpan.FromMilliseconds(100);

            var initiallyTurnedOn = display.TurnedOn;
            for (int i = 0; i < numberOfFlashes; i++)
            {
                display.TurnedOn = !display.TurnedOn;
                await Task.Delay(delay.Value);
            }

            display.TurnedOn = initiallyTurnedOn;

        }
    }
}
