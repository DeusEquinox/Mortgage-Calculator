using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
internal class MortgageCalculator
{
    static void Main()
    {
        Console.Write("Press 1 to Start App or 0 to exit: ");
        string userChoice = Console.ReadLine();
        if (userChoice == "1" )
        {
            MortgageCalculatorApp();
        }
        else if (userChoice == "0" )
        {
            Environment.ExitCode = 0;
        }
        else
        {
            Console.WriteLine("Try again");
            Main();
        }
    }
    static void MortgageCalculatorApp()
    {
        Console.WriteLine("\nWelcome to the Mortgage Calculator.");

        //Initialize Variables
        int loanAmount;
        float annualInterestRate;
        int loanTermYears;

        int Loan()
        {
            //Getting user input
            Console.Write("Enter a loan amount: ");
            string input = Console.ReadLine();

            //Error checking
            int Amount;
            while (!int.TryParse(input, out Amount) || Amount <= 0)
            {
                Console.WriteLine("Please enter a valid, positive integer.");
                input = Console.ReadLine();
            }

            return loanAmount = Amount;
        }

        float Interest()
        {
            //Reading user input
            Console.Write("Enter your interest rate: ");
            string input = Console.ReadLine();

            //Error checking conversion
            float interestRate;
            while (!float.TryParse(input, out interestRate) || interestRate <= 0)
            {
                Console.WriteLine("Please enter a valid, positive integer.");
                input = Console.ReadLine();
            }

            return annualInterestRate = interestRate;
        }

        int Term()
        {
            //Reading user input
            Console.Write("Enter how many years the loan needs to be paid off in: ");
            string input = Console.ReadLine();

            //Error checking conversion
            int LoanTerm;
            while (!int.TryParse(input, out LoanTerm) || LoanTerm <= 0 || LoanTerm > 100)
            {
                Console.WriteLine("Please enter a valid, positive integer.");
                input = Console.ReadLine();
            }

            return loanTermYears = LoanTerm;
        }

        Loan();
        Interest();
        Term();
        MainMenu(loanAmount, annualInterestRate, loanTermYears);
    }
    static void MainMenu(int loanAmount, float annualInterestRate, int loanTermYears)
    {
        Console.WriteLine("\n" +
        "What would you like to do?\n" +
        "(1) Calculate Monthly Repayment\n" +
        "(2) Calculate Total Interst Paid\n" +
        "(3) Calculate Total Amount Paid\n" +
        "(4) Generate Amortization Schedule\n" +
        "(5) Back");
        Console.Write("Choice: ");

        //Error checking user input
        string choice = Console.ReadLine();
        while ((choice != "1") && (choice != "2") && (choice != "3") && (choice != "4") && (choice != "5"))
        {
            Console.Write("Please choose an option between 1 and 4: ");
            choice = Console.ReadLine();
        }

        //Calling related method based on user choice
        if (choice == "1")
        {
            CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
        }
        else if (choice == "2")
        {
            CalculateTotalInterestPaid(loanAmount, annualInterestRate, loanTermYears);
        }
        else if (choice == "3")
        {
            CalculateTotalAmountPaid(loanAmount, annualInterestRate, loanTermYears);
        }
        else if (choice == "4")
        {
            GenerateAmortizationSchedule(loanAmount, annualInterestRate, loanTermYears);
        }
        else if (choice == "5")
        {
            Main();
        }
    }
    static void CalculateMonthlyRepayment(int loanAmount, float annualInterestRate, int loanTermYears)
    {
        //Assigning variables
        float monthlyInterestRate = annualInterestRate / (100*12);
        int numberOfPayments = loanTermYears * 12;

        //Calculating monthly payment
        double monthlyRepayment = loanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments)); 
        
        //Displaying in user friendly manner
        Console.WriteLine("\nThe monthly loan repayments will be: R" + Math.Round(monthlyRepayment, 2) + " p/m.");

        MainMenu(loanAmount, annualInterestRate, loanTermYears);
    }
    static void CalculateTotalInterestPaid(int loanAmount, float annualInterestRate, int loanTermYears)
    {
        //Assigning variables
        float monthlyInterestRate = annualInterestRate / (100 * 12);
        int numberOfPayments = loanTermYears * 12;
        double monthlyRepayment = loanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments));
        // Calculating interest paid
        float interestPaid = (float) monthlyRepayment * numberOfPayments - loanAmount;
        
        //Displaying in user friendly manner
        Console.WriteLine("\nThe total interest paid will be: R" + Math.Round(interestPaid, 2));

        MainMenu(loanAmount, annualInterestRate, loanTermYears);
    }
    static void CalculateTotalAmountPaid(int loanAmount, float annualInterestRate, int loanTermYears)
    {
        //Assigning variables
        float monthlyInterestRate = annualInterestRate / (100 * 12);
        int numberOfPayments = loanTermYears * 12;
        double monthlyRepayment = loanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments));
        //Calculating totay amount paid
        float totalPaid = (float) monthlyRepayment * numberOfPayments;

        //Displaying in user friendly manner
        Console.WriteLine("\nThe total amount paid will be: R" + Math.Round(totalPaid, 2));

        MainMenu(loanAmount, annualInterestRate, loanTermYears);
    }
    static void GenerateAmortizationSchedule(int loanAmount, float annualInterestRate, int loanTermYears)
    {
        int numberOfPayments = loanTermYears * 12;
        float monthlyInterestRate = annualInterestRate / (100 * 12), loanBalance = loanAmount;
        

        Console.WriteLine("Amortization Schedule\n");

        //Using for loop to display monthly breakdown
        for (int i = 1; i <= numberOfPayments; i++)
        {
            float monthlyInterest = loanBalance * monthlyInterestRate;
            double monthlyRepayment = loanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments));
            //Setting monthly payment to remainder if it exceeds the remaining balance
            if (loanBalance < monthlyRepayment)
            {
                monthlyRepayment = loanBalance + monthlyInterest;
            }
            loanBalance -= (float) monthlyRepayment - monthlyInterest;
            //Displaying monthly breakdown
            Console.WriteLine("Month " + i 
                + ": The interst is R" + Math.Round(monthlyInterest, 2) 
                + ". The repayment is R" + Math.Round(monthlyRepayment,2) 
                + " and the remaining balance is R" + Math.Round(loanBalance, 2));
        }
        MainMenu(loanAmount, annualInterestRate, loanTermYears);
    }
}
