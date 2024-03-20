using System;
using System.Drawing;
using System.Windows.Forms;
using System.Management;


namespace Equipment_manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void GetHardWareInfo(string key, ListView list)
        {
            list.Items.Clear();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELEST * FROM" + key);
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    ListViewGroup listViewGroup;
                    try
                    {
                        listViewGroup = list.Groups.Add(obj["Name"].ToString(), obj["Name"].ToString());
                    }
                    catch (Exception ex)
                    {

                        listViewGroup = list.Groups.Add(obj.ToString(), obj.ToString());
                    }
                    if (obj.Properties.Count == 0)
                    {
                        MessageBox.Show("No information available", "mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    foreach (PropertyData data in obj.Properties)
                    {
                        ListViewItem item = new ListViewItem(listViewGroup);
                        if (list.Items.Count % 2 != 0)
                        {
                            item.BackColor = Color.White;

                        }
                        else 
                        {
                            item.BackColor = Color.WhiteSmoke;
                        }
                        item.Text = data.Name;
                        if (data.Value != null && !string.IsNullOrEmpty(data.Value.ToString())) 
                        {
                            switch (data.Value.GetType().ToString()) 
                            {
                                case "System.String[]":
                                    string[] stringData = data.Value as string[];
                                    string resStr1 = string.Empty;
                                    foreach (string s in stringData)
                                    {
                                        resStr1 += $"{s}";
                                    }
                                    item.SubItems.Add(resStr1);
                                    break;

                                case "System.UInt16[]":
                                    ushort[] ushortData = data.Value as ushort[];
                                    string resStr2 = string.Empty;

                                    foreach (ushort  u in ushortData)
                                    {
                                        resStr2 += $"{Convert.ToString(u)}";
                                    }

                                    item.SubItems.Add(resStr2);
                                    break;
                                default:

                                    item.SubItems.Add(data.Value.ToString());
                                    break;

                            }
                            list.Items.Add(item);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.Message, "mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = string.Empty;
            switch (toolStripComboBox1.SelectedItem.ToString()) 
            {
                case "processor":
                    key = "Win32_Processor";
                    break;
                case "video card":
                    key = "Win32_VideoController";
                    break;
                case "chipset":
                    key = "Win32_IDEController";
                    break;
                case "battery":
                    key = "Win32_Battery";
                    break;
                case "bios":
                    key = "Win32_BIOS";
                    break;
                case "random access memory":
                    key = "Win32_PhysicalMemory";
                    break;
                case "cache":
                    key = "Win32_CacheMemory";
                    break;
                case "usb":
                    key = "Win32_USBController";
                    break;
                case "disk":
                    key = "Win32_DiskDrive";
                    break;
                case "logical disks":
                    key = "Win32_LogicalDisk";
                    break;
                case "keyboard":
                    key = "Win32_Keyboard";
                    break;
                case "network":
                    key = "Win32_NetworkAdapter";
                    break;
                case "users":
                    key = "Win32_Account";
                    break;
                default:
                    key = "Win32_Processor";
                    break;
            }
            GetHardWareInfo(key,listView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
        }
    }
}
