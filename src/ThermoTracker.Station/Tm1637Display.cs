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

        public bool UpperDotOn
        {
            get => _upperDotOn;
            set
            {
                if(value)
                    _tm1637.Display(4, Character.Digit8);
                else
                    _tm1637.Display(4, Character.Nothing);

                _upperDotOn = value;
            }
        }

        public bool LowerDotOn
        {
            get => _lowerDotOn;
            set
            {
                if (value)
                    _tm1637.Display(5, Character.Digit8);
                else
                    _tm1637.Display(5, Character.Nothing);

                _lowerDotOn = value;
            }
        }

        public const int MaxCharactersDisplayed = 4;
        private Tm1637 _tm1637;
        private bool _upperDotOn = false;
        private bool _lowerDotOn = false;

        public Tm1637Display(int clkPin, int dioPin, PinNumberingScheme numberingScheme)
        {
            _tm1637 = new Tm1637(clkPin, dioPin, numberingScheme, new GpioController(numberingScheme, new RaspberryPi3Driver()));
        }

        public void DisplayDigit(int digit, int position)
        {
            if(digit < 0 || digit >= 10)
                throw new ArgumentException($"Must be digit", nameof(digit));
            if(position < 0 || position >= 4)
                throw new ArgumentException($"Position '{position}' is out of bounds.", nameof(position));
            _tm1637.Display((byte)position, GetCharacterForDigit(digit));
        }

        public void DisplayNumber(int number, bool displayLeadingZeros = true)
        {
            if(number < 0 || number >= (int)Math.Pow(10, MaxCharactersDisplayed))
                throw new ArgumentException($"Number must have {MaxCharactersDisplayed} digits and cannot be negative.");

            var hadLeadingNonZero = false;
            for (int power = MaxCharactersDisplayed - 1; power >= 0; power--)
            {
                var digit = number / (int)Math.Pow(10, power);
                if (digit != 0)
                    hadLeadingNonZero = true;

                if(digit != 0 || hadLeadingNonZero || displayLeadingZeros)
                    DisplayDigit(digit, MaxCharactersDisplayed - power - 1);

                number %= (int)Math.Pow(10, power);
            }
        }

        public void Clear()
        {
            _tm1637.ClearDisplay();
            _upperDotOn = false;
            _lowerDotOn = false;
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
