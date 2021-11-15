using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum GeneralOptions { Exit, Add, Update, View, ListView };
        static void Main(string[] args)
        {
            GeneralOptions option = new GeneralOptions();
            IBL.IBL blObject = new BL.BL();
            do
            {
                Console.WriteLine("Choose Option: \n" +
                "1 - Add \n" +
                "2 - Update \n" +
                "3 - View \n" +
                "4 - View lists \n" +
                "0 - Exit");

                int x = 0;
                int.TryParse(Console.ReadLine(), out x);
                option = (GeneralOptions)x;

                switch (option)
                {
                    case GeneralOptions.Add:
                        {
                            AddMenu(blObject);
                            break;
                        }

                    case GeneralOptions.Update:
                        {
                            UpdateMenu(blObject);
                            break;
                        }

                    case GeneralOptions.View:
                        {
                            ViewMenu(blObject);
                            break;
                        }

                    case GeneralOptions.ListView:
                        {
                            ListViewMenu(blObject);
                            break;
                        }

                    case GeneralOptions.Exit:
                        break;

                    default:
                        // code block
                        break;
                }
            } while (option != GeneralOptions.Exit);

        }
    }
}
