using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Primality
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            minimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            closeButton.Click += (s, e) => Close();
        }

        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            outErrors.Text = "";
            outMsg.Text = "";

            int primeCandidate, securityParameter;
            bool successP = int.TryParse(inCandidate.Text, out primeCandidate);
            bool successS = int.TryParse(inSecurity.Text, out securityParameter);

            if (successP == false)
            {
                outErrors.Text = "Please enter valid integers for the prime candidate.";
                return;
            }
            else if (successS == false)
            {
                outErrors.Text = "Please enter valid integers for the security parameter.";
                return;
            }

            outMsg.Text = primalityTest(primeCandidate, securityParameter);
        }

        String primalityTest (int varP, int varS)
        {
            int varU;
            double varR;
            double varZ;
            int varA;

            int tempVar = varP - 1;
            int tempCount = 0;
            while (tempVar % 2 == 0)
            {
                tempVar /= 2;
                tempCount++;
            }
            varR = tempVar;
            varU = tempCount;

            Random rand = new Random();

            for (int i = 0; i <= varS; i++)
            {
                varA = rand.Next(varP - 1) + 1;
                varZ = powerMod(varA, varR, varP);

                if (varZ != 1 && varZ != varP - 1)
                {
                    for (int j = 0; j <= (varP - 1); j++)
                    {
                        varZ = powerMod(varZ, 2, varP);
                        if (varZ == 1)
                        {
                            return "P is composite.";
                        }
                        if (varZ != varP - 1)
                        {
                            return "P is composite.";
                        }
                    }
                }
            }
            return "P is likely prime!";
        }

        double powerMod(double bas, double exp, double mod)
        {
            double result = 1;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                {
                    result = (bas * result) % mod;
                    exp -= 1;
                }
                bas = (bas * bas) % mod;
                exp /= 2;
            }
            return result;
        }

        private void inCandidate_TextChanged(object sender, TextChangedEventArgs e)
        {
            outErrors.Text = "";
            outMsg.Text = "";
        }

        private void inSecurity_TextChanged(object sender, TextChangedEventArgs e)
        {
            outErrors.Text = "";
            outMsg.Text = "";
        }
    }
}
