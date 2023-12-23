using System;
using System.Windows;
using System.Windows.Controls;

namespace WordPuzzleSolver.Wpf.Controls;

public partial class IntegerUpDown
{
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(IntegerUpDown), new FrameworkPropertyMetadata(defaultValue: 1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
        nameof(Step), typeof(int), typeof(IntegerUpDown), 
        new PropertyMetadata(defaultValue: 1));

    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
        nameof(Minimum), typeof(int), typeof(IntegerUpDown), 
        new PropertyMetadata(default(int), OnMinimumMaximumPropertyChanged));

    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
        nameof(Maximum), typeof(int), typeof(IntegerUpDown), 
        new PropertyMetadata(default(int), OnMinimumMaximumPropertyChanged));

    public IntegerUpDown()
    {
        InitializeComponent();
    }

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public int Step
    {
        get => (int)GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }

    public int Minimum
    {
        get => (int)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public int Maximum
    {
        get => (int)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    private static void OnMinimumMaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (IntegerUpDown)d;
        control.EnforceRange();
    }

    private void EnforceRange()
    {
        Value = Math.Max(Minimum, Math.Min(Maximum, Value));
    }

    private void Up_Click(object sender, RoutedEventArgs e)
    {
        if (Value < Maximum)
        {
            Value = Math.Min(Value + Step, Maximum);
        }
    }

    private void Down_Click(object sender, RoutedEventArgs e)
    {
        if (Value > Minimum)
        {
            Value = Math.Max(Value - Step, Minimum);
        }
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && int.TryParse(textBox.Text, out var enteredValue))
        {
            Value = Math.Max(Minimum, Math.Min(Maximum, enteredValue));
        }
    }
}