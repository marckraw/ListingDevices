using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using Newtonsoft.Json;

namespace ListingDevices
{
    public partial class Form1 : Form
    {
        List<Device> devices = new List<Device>();

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DsDevice[] videoDevices = DirectShowLib.DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            foreach(DsDevice dev in videoDevices)
            {
                string id = PrepareVideoId(dev);

                devices.Add(new Device { category = "video", name = dev.Name, path = dev.DevicePath, id = id });
                Console.WriteLine("####");
                Console.WriteLine(dev.Name);
                Console.WriteLine(dev.DevicePath);
                Console.WriteLine("####");
                Console.WriteLine("   ");
                dataGridView1.Rows.Add(dev.Name, dev.DevicePath, id);
            }
        }

        private void listAudioDevices_Click(object sender, EventArgs e)
        {
            DsDevice[] audioDevices = DirectShowLib.DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice);
            foreach (DsDevice dev in audioDevices)
            {
                string id = PrepareVideoId(dev);

                devices.Add(new Device { category = "audioInput", name = dev.Name, path = dev.DevicePath, id = id });
                Console.WriteLine("####");
                Console.WriteLine(dev.Name);
                Console.WriteLine(dev.DevicePath);
                Console.WriteLine("####");
                Console.WriteLine("   ");
                dataGridView2.Rows.Add(dev.Name, dev.DevicePath, id);
            }

        }

        private void listOutputAudioDevices_Click(object sender, EventArgs e)
        {
            DsDevice[] audioOutputDevices = DirectShowLib.DsDevice.GetDevicesOfCat(FilterCategory.AudioRendererCategory);
            foreach (DsDevice dev in audioOutputDevices)
            {
                string id = PrepareVideoId(dev);

                devices.Add(new Device { category = "perhapsAudioOuput", name = dev.Name, path = dev.DevicePath, id = id});
                Console.WriteLine("####");
                Console.WriteLine(dev.Name);
                Console.WriteLine(dev.DevicePath);
                Console.WriteLine("####");
                Console.WriteLine("   ");
                dataGridView3.Rows.Add(dev.Name, dev.DevicePath, id);

            }
        }

        private static string PrepareVideoId(DsDevice device)
        {
            var index = device.DevicePath.IndexOf("\\", StringComparison.Ordinal);

            return device.DevicePath.IndexOf(":sw:", StringComparison.OrdinalIgnoreCase) > 0
                ? $"{device.Name}:"
                : $"{device.Name}:{device.DevicePath.Substring(index)}";
        }

        private void generateData_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(devices, Formatting.Indented);            
            File.WriteAllText("data.json", json);
        }
    }
}
