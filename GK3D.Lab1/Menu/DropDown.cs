using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Menu
{
    class DropDown : IMenuObject
    {
        public List<string> Items { get; set; }

        public string Content { get; set; }

        public event EventHandler OnValueChanged;

        public int SelectedIndex { get; set; }

        protected virtual void ValueChanged(EventArgs e)
        {
            EventHandler handler = OnValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public DropDown(string content)
        {
            Content = content;
            Items = new List<string>();
            SelectedIndex = 0;
        }

        public void AddItem(string item)
        {
            Items.Add(item);
        }
    }
}
