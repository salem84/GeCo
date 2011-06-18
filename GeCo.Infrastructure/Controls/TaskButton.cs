using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace GeCo.Infrastructure.Controls
{
    /// <summary>
    /// A button styled to resemble a Outlook 2010 task button.
    /// </summary>
    public class TaskButton : RadioButton
    {
        public static readonly DependencyProperty ImageProperty;
        public static readonly DependencyProperty TextProperty;
        /// <summary>
        /// The image displayed by the button.
        /// </summary>
        /// <remarks>The image is specified in XAML as an absolute or relative path.</remarks>
        [Description("The image displayed by the button"), Category("Common Properties")]
        public ImageSource Image
        {
            get
            {
                return (ImageSource)base.GetValue(TaskButton.ImageProperty);
            }
            set
            {
                base.SetValue(TaskButton.ImageProperty, value);
            }
        }
        /// <summary>
        /// The text displayed by the button.
        /// </summary>
        [Category("Common Properties"), Description("The text displayed by the button.")]
        public string Text
        {
            get
            {
                return (string)base.GetValue(TaskButton.TextProperty);
            }
            set
            {
                base.SetValue(TaskButton.TextProperty, value);
            }
        }
        static TaskButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(TaskButton), new FrameworkPropertyMetadata(typeof(TaskButton)));
            TaskButton.ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(TaskButton), new UIPropertyMetadata(null));
            TaskButton.TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TaskButton), new UIPropertyMetadata(null));
        }
    }
}