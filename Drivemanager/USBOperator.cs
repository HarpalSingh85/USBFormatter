using System.Collections.Generic;
using System.Linq;
using System.Management;


namespace DriveManager
{
    class USBOperator 
    {
        public ICollection<USBDisk> GetAvailableDisks()
        {
            ICollection<USBDisk> diskCollection = new List<USBDisk>();

            foreach (ManagementObject drive in new ManagementObjectSearcher("select * from Win32_DiskDrive where InterfaceType='USB'").Get())
            {
                USBDisk _disk = new USBDisk();
                
                foreach (ManagementObject partition in new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"] + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {                    
                    _disk.Partition = partition["Name"].ToString();

                    foreach (ManagementObject disk in new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] + "'} WHERE AssocClass =  Win32_LogicalDiskToPartition").Get())
                    {
                        _disk.Drive = disk["Name"].ToString().Split(':').ElementAt(0).ToCharArray()[0];                       
                    }
                                        
                }

                _disk.Serial = new ManagementObject("Win32_PhysicalMedia.Tag='" + drive["DeviceID"] + "'")["SerialNumber"].ToString();

                diskCollection.Add(_disk);
            }

            return diskCollection;
        }
    }

    class USBDisk{

        public string Partition { get;  set; }
        public char Drive { get;  set; }

        public string Serial { get;  set; }

    }


}
