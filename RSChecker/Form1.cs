using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using RestSharp;
using System.Threading;

namespace RSChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
        }

        bool running = false;

        private async void button1_Click(object sender, EventArgs e)
        {
            if (running)
            {
                running = false;
                label5.Text = String.Empty;
                groupBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = false;
                button1.Text = "Start";
                return;
            }
            groupBox1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button1.Text = "Stop";
            button2.Text = "Pause";
            running = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var client = new RestClient();
            client.Timeout = 5000;
            client.BaseUrl = new Uri("https://secure.runescape.com");

            if (radioButton1.Checked)
            {
                List<string> nameslist = new List<string>();
                nameslist = textBox1.Lines.ToList();

                if (label5.Text != String.Empty)
                {
                    nameslist.RemoveRange(0, nameslist.FindIndex(name => label5.Text.Split(':')[0] == name) + 1);
                }
                else
                {
                    textBox4.Text = "";
                }

                foreach (string name in nameslist)
                {

                    var request = new RestRequest();
                    request.Resource = "/m=account-creation/g=oldscape/check_displayname.ajax";
                    request.AddHeader("Referer", "http://secure.runescape.com/m=account-creation/g=oldscape/create_account");
                    request.Method = Method.POST;

                    request.AddParameter("displayname", name);
                    request.AddParameter("noNameSuggestions", "true");
                    
                    IRestResponse response = await client.ExecuteTaskAsync(request);

                    if (!running)
                        return;

                    if (response.Content.Contains("\"displayNameIsValid\":\"true\""))
                    {
                        textBox4.Text += name + "\r\n";
                        label5.Text = name + ": Available";
                    }
                    else if (response.Content.Contains("\"displayNameIsValid\":\"false\""))
                    {
                        label5.Text = name + ": Not Available";
                    }
                    else
                    {
                        request = new RestRequest();
                        request.Resource = "/m=account-creation/g=oldscape/check_displayname.ajax";
                        request.AddHeader("Referer", "http://secure.runescape.com/m=account-creation/g=oldscape/create_account");
                        request.Method = Method.POST;

                        request.AddParameter("displayname", name);
                        request.AddParameter("noNameSuggestions", "true");

                        response = await client.ExecuteTaskAsync(request);

                        if (!running)
                            return;

                        if (response.Content.Contains("\"displayNameIsValid\":\"true\""))
                        {
                            textBox4.Text += name + "\r\n";
                            label5.Text = name + ": Available";
                        }
                        else if (response.Content.Contains("\"displayNameIsValid\":\"false\""))
                        {
                            label5.Text = name + ": Not Available";
                        }
                        else
                        {
                            label5.Text = name + ": ERROR";
                        }
                    }

                    Thread.Sleep(Convert.ToInt32(numericUpDown1.Value));
                }
            }
            if (radioButton2.Checked)
            {
                textBox2.Text = textBox2.Text.ToUpper();
                textBox3.Text = textBox3.Text.ToUpper();
                string name = textBox2.Text;
                string end = textBox3.Text;
                {
                    char[] digits = end.ToCharArray();
                    for (int i = end.Length - 1; i >= 0; --i)
                    {
                        if (digits[i] == 'Z')
                        {
                            digits[i] = 'A';
                            if (i == 0)
                            {
                                end = new string(digits) + 'A';
                                break;
                            }
                        }
                        else
                        {
                            digits[i]++;
                            end = new string(digits);
                            break;
                        }
                    }
                }

                if (label5.Text != String.Empty)
                {
                    name = label5.Text.Split(':')[0];
                }
                else
                {
                    textBox4.Text = "";
                }

                do
                {
                    
                    var request = new RestRequest();
                    request.Resource = "/m=account-creation/g=oldscape/check_displayname.ajax";
                    request.AddHeader("Referer", "http://secure.runescape.com/m=account-creation/g=oldscape/create_account");
                    request.Method = Method.POST;

                    request.AddParameter("displayname", name);
                    request.AddParameter("noNameSuggestions", "true");

                    IRestResponse response = await client.ExecuteTaskAsync(request);

                    if (!running)
                        return;

                    if (response.Content.Contains("\"displayNameIsValid\":\"true\""))
                    {
                        textBox4.Text += name + "\r\n";
                        label5.Text = name + ": Available";
                    }
                    else if (response.Content.Contains("\"displayNameIsValid\":\"false\""))
                    {
                        label5.Text = name + ": Not Available";
                    }
                    else
                    {
                        request = new RestRequest();
                        request.Resource = "/m=account-creation/g=oldscape/check_displayname.ajax";
                        request.AddHeader("Referer", "http://secure.runescape.com/m=account-creation/g=oldscape/create_account");
                        request.Method = Method.POST;

                        request.AddParameter("displayname", name);
                        request.AddParameter("noNameSuggestions", "true");

                        response = await client.ExecuteTaskAsync(request);

                        if (!running)
                            return;

                        if (response.Content.Contains("\"displayNameIsValid\":\"true\""))
                        {
                            textBox4.Text += name + "\r\n";
                            label5.Text = name + ": Available";
                        }
                        else if (response.Content.Contains("\"displayNameIsValid\":\"false\""))
                        {
                            label5.Text = name + ": Not Available";
                        }
                        else
                        {
                            label5.Text = name + ": ERROR";
                        }
                    }


                    char[] digits = name.ToCharArray();
                    for (int i = name.Length - 1; i >= 0; --i)
                    {
                        if (digits[i] == 'Z')
                        {
                            digits[i] = 'A';
                            if (i == 0)
                            {
                                name = new string(digits) + 'A';
                                break;
                            }
                        }
                        else
                        {
                            digits[i]++;
                            name = new string(digits);
                            break;
                        }
                    }
                    Console.WriteLine(name);

                    
                    Thread.Sleep(Convert.ToInt32(numericUpDown1.Value));

                } while (name != end);
                

            }

           

            running = false;
            groupBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            button1.Text = "Start";
            Thread.Sleep(Convert.ToInt32(numericUpDown1.Value));
            label5.Text = String.Empty;
        }

        private void checkName(string name)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Text = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (running)
            {
                running = false;
                button1.Enabled = true;
                button2.Enabled = true;
                button1.Text = "Resume";
                button2.Text = "Stop";
            }
            else
            {
                running = false;
                label5.Text = String.Empty;
                groupBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = false;
                button1.Text = "Start";
            }
        }
    }
}
