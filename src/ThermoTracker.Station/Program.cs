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
                display.Brightness = 5;
                display.Clear();
                display.DisplayNumber(15);
                display.UpperDotOn = true;
                display.LowerDotOn = true;
                await Task.Delay(TimeSpan.FromSeconds(10));
                await FlashScreen(display, 5);

                display.TurnedOn = false;

                Console.WriteLine("This is it folks!");
            }
        }

        private static async Task FlashScreen(Tm1637Display display, int numberOfFlashes, TimeSpan? delay = null)
        {
            if (delay == null)
                delay = TimeSpan.FromMilliseconds(500);

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
