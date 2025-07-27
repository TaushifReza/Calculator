using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private double? firstOperand = null;
        private string operation = null;
        private bool isNewInput = true;
        private double memory = 0;
        private bool hasMemoryValue = false;
        private bool waitingForOperand = false;

        public MainWindow()
        {
            InitializeComponent();
            UpdateDisplay();
            this.Focus(); // Ensure window can receive keyboard events
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string value = (string)((Button)sender).Content;
            HandleNumberInput(value);
        }

        private void HandleNumberInput(string value)
        {
            try
            {
                // Handle special case for "00" when display is "0"
                if (value == "00" && currentInput == "0")
                    return;

                if (isNewInput || currentInput == "0")
                {
                    currentInput = value == "00" ? "0" : value;
                    isNewInput = false;
                }
                else
                {
                    // Check if adding this number would exceed 20 characters
                    if (currentInput.Length + value.Length <= 20)
                    {
                        currentInput += value;
                    }
                }

                waitingForOperand = false;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                ShowError("Input Error: " + ex.Message);
            }
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isNewInput)
                {
                    currentInput = "0.";
                    isNewInput = false;
                }
                else if (!currentInput.Contains(".") && currentInput.Length < 19)
                {
                    currentInput += ".";
                }

                waitingForOperand = false;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                ShowError("Input Error: " + ex.Message);
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            string newOperation = (string)((Button)sender).Content;
            HandleOperatorInput(newOperation);
        }

        private void HandleOperatorInput(string newOperation)
        {
            try
            {
                // Convert × back to x for internal processing
                if (newOperation == "×") newOperation = "x";

                if (firstOperand != null && !isNewInput && !waitingForOperand)
                {
                    Calculate();
                }

                if (double.TryParse(currentInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
                {
                    firstOperand = result;
                    operation = newOperation;
                    isNewInput = true;
                    waitingForOperand = true;
                }
            }
            catch (Exception ex)
            {
                ShowError("Operation Error: " + ex.Message);
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (firstOperand != null && !waitingForOperand)
                {
                    Calculate();
                    operation = null;
                    firstOperand = null;
                }
            }
            catch (Exception ex)
            {
                ShowError("Calculation Error: " + ex.Message);
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            firstOperand = null;
            operation = null;
            isNewInput = true;
            waitingForOperand = false;
            UpdateDisplay();
        }

        private void ClearLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isNewInput && currentInput.Length > 1)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    if (currentInput == "" || currentInput == "-")
                        currentInput = "0";
                }
                else
                {
                    currentInput = "0";
                }
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                ShowError("Clear Error: " + ex.Message);
            }
        }

        private void MemoryAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double valueToAddToMemory;

                // If operation is in progress, calculate silently first (as per specification)
                if (firstOperand != null && !isNewInput && !waitingForOperand)
                {
                    // Calculate the result but don't update the display
                    if (double.TryParse(currentInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double secondOperand))
                    {
                        double result = firstOperand.Value;
                        switch (operation)
                        {
                            case "+": result += secondOperand; break;
                            case "-": result -= secondOperand; break;
                            case "x": result *= secondOperand; break;
                            case "÷":
                                if (secondOperand == 0)
                                {
                                    ShowError("Cannot divide by zero");
                                    return;
                                }
                                result /= secondOperand;
                                break;
                            default: return;
                        }

                        valueToAddToMemory = result;

                        // Clear the operation state but DON'T update display
                        firstOperand = null;
                        operation = null;
                        waitingForOperand = false;
                    }
                    else
                    {
                        ShowError("Invalid number format");
                        return;
                    }
                }
                else
                {
                    // No operation in progress, just use current display value
                    if (!double.TryParse(currentInput, NumberStyles.Float, CultureInfo.InvariantCulture, out valueToAddToMemory))
                    {
                        ShowError("Invalid number format");
                        return;
                    }
                }

                memory += valueToAddToMemory;
                hasMemoryValue = true;
                UpdateMemoryIndicator();

                // The display should NOT change - this is the key requirement
                // But prepare for next input
                isNewInput = true;
            }
            catch (Exception ex)
            {
                ShowError("Memory Error: " + ex.Message);
            }
        }

        private void MemorySubtract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double valueToSubtractFromMemory;

                // If operation is in progress, calculate silently first (as per specification)
                if (firstOperand != null && !isNewInput && !waitingForOperand)
                {
                    // Calculate the result but don't update the display
                    if (double.TryParse(currentInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double secondOperand))
                    {
                        double result = firstOperand.Value;
                        switch (operation)
                        {
                            case "+": result += secondOperand; break;
                            case "-": result -= secondOperand; break;
                            case "x": result *= secondOperand; break;
                            case "÷":
                                if (secondOperand == 0)
                                {
                                    ShowError("Cannot divide by zero");
                                    return;
                                }
                                result /= secondOperand;
                                break;
                            default: return;
                        }

                        valueToSubtractFromMemory = result;

                        // Clear the operation state but DON'T update display
                        firstOperand = null;
                        operation = null;
                        waitingForOperand = false;
                    }
                    else
                    {
                        ShowError("Invalid number format");
                        return;
                    }
                }
                else
                {
                    // No operation in progress, just use current display value
                    if (!double.TryParse(currentInput, NumberStyles.Float, CultureInfo.InvariantCulture, out valueToSubtractFromMemory))
                    {
                        ShowError("Invalid number format");
                        return;
                    }
                }

                memory -= valueToSubtractFromMemory;
                hasMemoryValue = true;
                UpdateMemoryIndicator();

                // The display should NOT change - this is the key requirement
                // But prepare for next input
                isNewInput = true;
            }
            catch (Exception ex)
            {
                ShowError("Memory Error: " + ex.Message);
            }
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // MRC always shows what's in memory, even if it's 0
                currentInput = FormatResult(memory);
                isNewInput = true;
                waitingForOperand = false;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                ShowError("Memory Error: " + ex.Message);
            }
        }

        private void MemoryClear_Click(object sender, RoutedEventArgs e)
        {
            memory = 0;
            hasMemoryValue = false;
            UpdateMemoryIndicator();
        }

        private void Calculate()
        {
            try
            {
                if (!double.TryParse(currentInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double secondOperand))
                {
                    ShowError("Invalid number format");
                    return;
                }

                double result = firstOperand.Value;

                switch (operation)
                {
                    case "+":
                        result += secondOperand;
                        break;
                    case "-":
                        result -= secondOperand;
                        break;
                    case "x":
                        result *= secondOperand;
                        break;
                    case "÷":
                        if (secondOperand == 0)
                        {
                            ShowError("Cannot divide by zero");
                            return;
                        }
                        result /= secondOperand;
                        break;
                    default:
                        return;
                }

                // Check for overflow
                if (double.IsInfinity(result) || double.IsNaN(result))
                {
                    ShowError("Result too large");
                    return;
                }

                currentInput = FormatResult(result);
                firstOperand = null; // Clear after calculation for memory operations
                operation = null;    // Clear operation after calculation
                isNewInput = true;
                waitingForOperand = false;
                UpdateDisplay();
            }
            catch (OverflowException)
            {
                ShowError("Number too large");
            }
            catch (Exception ex)
            {
                ShowError("Calculation Error: " + ex.Message);
            }
        }

        private string FormatResult(double value)
        {
            // Handle negative zero
            if (value == 0) return "0";

            string result;

            // For very large or very small numbers, use scientific notation
            if (Math.Abs(value) >= 1e15 || (Math.Abs(value) < 1e-6 && value != 0))
            {
                result = value.ToString("E6", CultureInfo.InvariantCulture);
            }
            else
            {
                // Use G format to automatically choose between fixed and exponential
                result = value.ToString("G15", CultureInfo.InvariantCulture);
            }

            // Ensure result doesn't exceed 20 characters
            if (result.Length > 20)
            {
                result = value.ToString("E6", CultureInfo.InvariantCulture);
                if (result.Length > 20)
                {
                    result = result.Substring(0, 20);
                }
            }

            return result;
        }

        private void UpdateDisplay()
        {
            try
            {
                string displayText = currentInput;

                // Ensure display doesn't exceed 20 characters
                if (displayText.Length > 20)
                {
                    displayText = displayText.Substring(0, 20);
                }

                Display.Text = displayText;
            }
            catch (Exception ex)
            {
                Display.Text = "Error";
                ShowError("Display Error: " + ex.Message);
            }
        }

        private void UpdateMemoryIndicator()
        {
            // Show memory indicator when memory has a non-zero value
            MemoryIndicator.Visibility = (hasMemoryValue && memory != 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowError(string message)
        {
            currentInput = "0";
            firstOperand = null;
            operation = null;
            isNewInput = true;
            waitingForOperand = false;
            UpdateDisplay();

            MessageBox.Show(message, "Calculator Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // Keyboard support
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.D0:
                    case Key.NumPad0:
                        HandleNumberInput("0");
                        break;
                    case Key.D1:
                    case Key.NumPad1:
                        HandleNumberInput("1");
                        break;
                    case Key.D2:
                    case Key.NumPad2:
                        HandleNumberInput("2");
                        break;
                    case Key.D3:
                    case Key.NumPad3:
                        HandleNumberInput("3");
                        break;
                    case Key.D4:
                    case Key.NumPad4:
                        HandleNumberInput("4");
                        break;
                    case Key.D5:
                    case Key.NumPad5:
                        HandleNumberInput("5");
                        break;
                    case Key.D6:
                    case Key.NumPad6:
                        HandleNumberInput("6");
                        break;
                    case Key.D7:
                    case Key.NumPad7:
                        HandleNumberInput("7");
                        break;
                    case Key.D8:
                    case Key.NumPad8:
                        HandleNumberInput("8");
                        break;
                    case Key.D9:
                    case Key.NumPad9:
                        HandleNumberInput("9");
                        break;
                    case Key.OemPeriod:
                    case Key.Decimal:
                        Dot_Click(sender, e);
                        break;
                    case Key.OemPlus:
                    case Key.Add:
                        HandleOperatorInput("+");
                        break;
                    case Key.OemMinus:
                    case Key.Subtract:
                        HandleOperatorInput("-");
                        break;
                    case Key.Multiply:
                        HandleOperatorInput("x");
                        break;
                    case Key.Divide:
                        HandleOperatorInput("÷");
                        break;
                    case Key.Enter:
                        Equals_Click(sender, e);
                        break;
                    case Key.Escape:
                        ClearAll_Click(sender, e);
                        break;
                    case Key.Back:
                        ClearLast_Click(sender, e);
                        break;
                    case Key.Delete:
                        ClearAll_Click(sender, e);
                        break;
                }

                e.Handled = true;
            }
            catch (Exception ex)
            {
                ShowError("Keyboard Error: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
    }
}