using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Menu
{
    class Slider : IMenuObject
    {
        public string Content { get; set; }

        public int Value { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public event EventHandler OnValueChanged;

        protected virtual void ValueChanged(EventArgs e)
        {
            EventHandler handler = OnValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public Slider(string content, int value, int min, int max)
        {
            Content = content;
            Value = value;
            Min = min;
            Max = max;
        }
    }
}
