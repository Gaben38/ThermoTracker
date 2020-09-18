using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Text;
using System.Threading.Tasks;
using Iot.Device.Tm1637;

namespace ThermoTracker.Station
{
    public class Tm1637Display : IDisposable
    {
        public byte Brightness
        {
            get => _tm1637.Brightness;
            set => _tm1637.Brightness = value;
        }

        public bool TurnedOn
        {
            get => _tm1637.ScreenOn;
            set => _tm1637.ScreenOn = value;
        }

        private Tm1637 _tm1637;

        public Tm1637Display(int clkPin, int dioPin, PinNumberingScheme numberingScheme)
        {
            _tm1637 = new Tm1637(clkPin, dioPin, numberingScheme, new GpioController(numberingScheme, new RaspberryPi3Driver()));
        }

        public void DisplayDigit(int digit)
        {
            if(digit < 0 || digit >= 10)
                throw new ArgumentException($"Must be digit", nameof(digit));
            _tm1637.Display(0, GetCharacterForDigit(digit));
        }

        public void Clear()
        {
            _tm1637.ClearDisplay();
        }

        public void Dispose()
        {
            _tm1637?.Dispose();
        }

        private Character GetCharacterForDigit(int digit)
        {
            if (digit < 0 || digit >= 10)
                throw new ArgumentException($"Must be digit", nameof(digit));

            return digit switch
            {
                0 => Character.Digit0,
                1 => Character.Digit1,
                2 => Character.Digit2,
                3 => Character.Digit3,
                4 => Character.Digit4,
                5 => Character.Digit5,
                6 => Character.Digit6,
                7 => Character.Digit7,
                8 => Character.Digit8,
                9 => Character.Digit9,
                _ => throw new NotImplementedException($"Unexpected digit '{digit}'.")
            };
        }
    }
}
