﻿using BOLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewLayer
{
    public class View
    {
        public void loginScreen()
        {
            Console.WriteLine("-----Welcome to ATM-----\n\n" +
                            "Login as:\n" +
                            "1----Administrator\n" +
                            "2----Customer\n\n" +
                            "Enter 1 or 2:");
            try
            {
            getUser:
                {
                    string user = Console.ReadLine();
                    // Checking if input is correct
                    if (user == "1" || user == "2")
                    {

                        switch (user)
                        {
                            // Case 1 for Administrator Login
                            case "1":
                                Console.WriteLine("-----Administrator Login-----\n" +
                                    "Please Enter your username & 5-digit Pin");
                                //Decalring an Admin object
                                Admin admin = new Admin();
                                bool isSignedin = false;
                                Logic logic = new Logic();
                                while (!isSignedin)
                                {
                                    // Reading and storing username
                                    Console.Write("Username: ");
                                    admin.Username = Console.ReadLine();
                                    //Applying encryption
                                    admin.Username = logic.EncryptionDecryption(admin.Username);

                                    // Reading and storing Pin
                                    Console.Write("5-digit Pin: ");
                                    admin.Pin = Console.ReadLine();
                                    //Applying encryption
                                    admin.Pin = logic.EncryptionDecryption(admin.Pin);
                                    // Verifying login details                                
                                    if (logic.VerifyLogin(admin))
                                    {
                                        Console.WriteLine("\n---Loggedin as Administrator---\n");
                                        isSignedin = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong Username/Pin. Try again.");
                                    }
                                }
                                // As successfully signedin, displaying admin screen
                                AdminScreen();
                                break;

                            // Case 2 for Customer Login
                            case "2":
                                Console.WriteLine("-----Customer Login-----\n" +
                                    "Please Enter your username & 5-digit Pin");
                                //Decalring an Customer object
                                Customer customer = new Customer();
                                Logic logic2 = new Logic();

                            getUsername:
                                {
                                    // Reading and storing username
                                    Console.Write("Username: ");
                                    customer.Username = Console.ReadLine();
                                    //Doing encryption
                                    customer.Username = logic2.EncryptionDecryption(customer.Username);
                                    if (logic2.isValidUsername(customer.Username))
                                    {
                                        if (logic2.isUserActive(customer.Username) == 1)
                                        {
                                            int wrong = 0;
                                        getPin:
                                            {
                                                // Reading and storing Pin
                                                Console.Write("5-digit Pin: ");
                                                customer.Pin = Console.ReadLine();
                                                // Do encryption
                                                customer.Pin = logic2.EncryptionDecryption(customer.Pin);
                                                // Verifying login details                                
                                                if (logic2.VerifyLogin(customer))
                                                {
                                                    Console.WriteLine("\n---Loggedin as Customer---\n");
                                                    CustomerScreen(customer.Username);
                                                }
                                                else
                                                {
                                                    wrong++;
                                                    if (wrong < 3)
                                                    {
                                                        Console.WriteLine("Wrong Pin. Try again.");
                                                        goto getPin;
                                                    }
                                                    else if (wrong == 3)
                                                    {
                                                        logic2.DisableAccount(customer.Username);
                                                        Console.WriteLine("Wrong Pin input 3 times. Account is disabled!");
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else if (logic2.isUserActive(customer.Username) == 2)
                                        {
                                            Console.WriteLine("Your account is disabled. Please contact administrator.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Username does not exist! Try again");
                                            goto getUsername;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input. Enter Again.");
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input. Please try again");
                        goto getUser;
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Please try again");
            }
        }

        // Admin Screen and option
        public void AdminScreen()
        {
        adminScreen:
            {
                Console.Clear();
                Console.WriteLine("-----Admin Menu-----\n");
                Console.WriteLine("1----Create New Account\n" +
                    "2----Delete Existing Account\n" +
                    "3----Update Account Information\n" +
                    "4----Search for Account\n" +
                    "5----View Reports\n" +
                    "6----Exit");

                try
                {
                getAdminOption:
                    {
                        string option = Console.ReadLine();
                        // Checking if input is correct
                        if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5" || option == "6")
                        {
                            Logic logic = new Logic();
                            switch (option)
                            {
                                // To create a new account
                                case "1":
                                    logic.CreateAccount();
                                    break;
                                case "2":
                                    logic.DeleteAccount();
                                    break;
                                case "3":
                                    logic.UpdateAccount();
                                    break;
                                case "4":
                                    logic.SearchAccount();
                                    break;
                                case "5":
                                    logic.ViewReports();
                                    break;
                                // Exits the applicaion
                                case "6":
                                    System.Environment.Exit(0);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input. Please try again");
                            goto getAdminOption;
                        }
                    }
                    // Asks user if to continue or not
                    Console.Write("\nDo you wish to continue(Y/N): ");
                    string wish = Console.ReadLine();
                    if (wish == "y" || wish == "Y")
                    {
                        goto adminScreen;
                    }
                    else
                    {
                        System.Environment.Exit(0);
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Please try again");
                }
            }
        }

        public void CustomerScreen(string username)
        {
        customerScreen:
            {
                Console.WriteLine("=====Customer Menu=====");
                Console.WriteLine("1----Withdraw Cash\n" +
                    "2----Cash Transfer\n" +
                    "3----Deposit Cash\n" +
                    "4----Display Balance\n" +
                    "5----Exit");

                try
                {
                getCustomerOption:
                    {
                        string option = Console.ReadLine();
                        // Checking if input is correct
                        if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5")
                        {
                            Logic logic = new Logic();
                            switch (option)
                            {
                                case "1":
                                    logic.CashWithdraw(username);
                                    break;
                                case "2":
                                    logic.CashTransfer(username);
                                    break;
                                case "3":
                                    logic.CashDeposit(username);
                                    break;
                                case "4":
                                    logic.DisplayBalance(username);
                                    break;
                                case "5":
                                    System.Environment.Exit(0);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input. Please try again");
                            goto getCustomerOption;
                        }
                    }
                    // Asks user if to continue or not
                    Console.Write("\nDo you wish to continue(Y/N): ");
                    string wish = Console.ReadLine();
                    if (wish == "y" || wish == "Y")
                    {
                        goto customerScreen;
                    }
                    else
                    {
                        System.Environment.Exit(0);
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Please try again");
                }
            }
        }
    }
}
