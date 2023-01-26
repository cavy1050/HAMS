using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace HAMS.Frame.Kernel.Controls
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
