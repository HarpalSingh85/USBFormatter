using System;
using System.Collections.Generic;

namespace DriveManager
{
    class Program
    {
        static void Main(string[] args)
        {
            bool _opresult = false;

            ICollection<USBDisk> usbdisks = 
                new USBOperator().GetAvailableDisks();


            if(usbdisks.Count > 0)
            {
                foreach (var disk in usbdisks)
                {

                    Console.WriteLine($"Formatting {disk.Drive} on {disk.Partition} of {disk.Serial}");
                    Console.WriteLine($"Are you sure you want to format (USB) : {disk.Serial} [Y/N]? ");
                    var key = Console.ReadKey();
                    Console.WriteLine();
                    switch(key.Key)
                    {
                        case ConsoleKey.Y:
                            _opresult = DriveOperator.FormatDrive(disk.Drive, "", "FAT32", true, false);
                            if (_opresult)
                                Console.WriteLine($"Formatted {disk.Drive} on {disk.Partition} of {disk.Serial}");
                            else
                                Console.WriteLine($"Formatting failed : {disk.Drive} on {disk.Partition} of {disk.Serial}");
                            break;
                        
                        default:
                            Console.WriteLine("Exiting Interface..");
                            break;
                    }

                }

            }
            else
            {
                Console.WriteLine($"No USB drives identified to be mounted.");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
            
        }
    }
}
