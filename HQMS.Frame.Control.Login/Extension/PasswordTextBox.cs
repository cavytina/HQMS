using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace HQMS.Frame.Control.Login.Extension
{
    public class PasswordTextBox : TextBox
    {
        public string PasswordText
        {
            get { return (string)GetValue(PasswordTextProperty); }
            set { SetValue(PasswordTextProperty, value); }
        }

        public static DependencyProperty PasswordTextProperty =
            DependencyProperty.Register("PasswordText", typeof(string), typeof(PasswordTextBox), new PropertyMetadata(string.Empty));

        public PasswordTextBox()
        {
            Binding binding = new Binding();
            binding.Converter = new PasswordTextConverter();
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Path = new PropertyPath("PasswordText");
            binding.Source = this;
            BindingOperations.SetBinding(this, TextProperty, binding);
        }
    }
}
