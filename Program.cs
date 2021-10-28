using System;
using System.Windows.Automation;
using System.Threading;

namespace MasterDataTestSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ClickLogInButton())
            {
                Console.WriteLine("[window not hooked | program failed]");
            }
            else
            {
                Console.WriteLine("[window hooked] Waiting to hook user/pass fields...");
                LogIn();
                Console.WriteLine("[logon completed]");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static bool ClickLogInButton()
        {
            // try to get the first window and push the Log In button
            // searching for MainWindow
            AutomationElement firstWindow =
                AutomationElement.RootElement.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, "MainWindow"));

            // searching for Log In button
            AutomationElement loginButton =
                firstWindow?.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, "Log In"));

            try
            {
                if (loginButton != null)
                {
                    // push the button
                    InvokePattern ivkp = loginButton?.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                    ivkp.Invoke();
                }
            }
            catch (Exception ex) // sorry i'm too lazy to write all exception types here
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        private static bool LogIn()
        {
            // searching for credentials window
            AutomationElement credentialsWindow =
                AutomationElement.RootElement.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, "Connect to myserver"));

            // searching for username field
            AutomationElement usernameField =
                credentialsWindow?.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "1003"));

            // searching for password field
            AutomationElement passwordField =
                credentialsWindow?.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "1005"));

            // searching for OK button
            AutomationElement okButton =
                credentialsWindow?.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, "OK"));

            // try to get CredentialsWindow and type user/pass combination
            try
            {
                if (usernameField != null && passwordField != null)
                {
                    // typing username into username field (thx, Captain Obvious)
                    ValuePattern vp0 = usernameField.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                    vp0?.SetValue("admin");

                    // typing password into password field
                    ValuePattern vp1 = passwordField.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                    vp1?.SetValue("12345"); // security engineer probably would kill me

                    // push the button
                    InvokePattern ivkp = okButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                    ivkp.Invoke();
                }
            }
            catch (Exception ex) // yeah i'm too lazy
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}