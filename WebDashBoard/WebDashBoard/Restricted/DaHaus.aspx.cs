using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;


namespace WebDashBoard
{
    public partial class DaHaus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Meubel_keuze kijkt in de gekregen gekregen String naar het eerste wordt om daarna te kijken wat de staat is van de meubel en het dan te zetten naar de andere.
        //de form van de gekregen string moet in de form zijn van "[meubel] [getal]"
        //in het geval dat het eerste woord "heater" is, kijkt het naar de tweede getal voor de temperatuur, anders is het welk meubel het manupileert.
        private void Meubel_Keuze(String Object)
        {
            String State = "";

            if (Object.Substring(0, 4) == "lamp")
            {
                State = CheckState(Object);

                if (State.Substring(10, 2) == "On")
                {
                    String newState = Object + " off";

                    DaHauS(newState);
                }
                else
                {
                    String newState = Object + " on";

                    DaHauS(newState);
                }
            }
            else if (Object.Substring(0, 6) == "window")
            {
                State = CheckState(Object);

                if (State.Substring(12, 4) == "Open")
                {
                    String newState = Object + " close";

                    DaHauS(newState);
                }
                else
                {
                    String newState = Object + " open";

                    DaHauS(newState);
                }
            }
            else
            {
                DaHauS(Object);
            }
        }

        //DaHaus maakt verbinding met de DaHaus programma, daarna stuurt het een Command naar DaHaus waarna het de verbinding sluit. 
        private void DaHauS(String Command)
        {

            try
            {
                Int32 port = 11000;
                String server = "127.0.0.1";
                TcpClient client = new TcpClient();
                client.Connect(server, port);

                NetworkStream Netstream = client.GetStream();
                StreamWriter StreamW = new StreamWriter(Netstream);
                StreamReader StreamR = new StreamReader(Netstream);

                StreamW.WriteLine(Command);
                StreamW.Flush();

                String responseData1 = StreamR.ReadLine();
                lbl_State.Text = ("return: " + responseData1);

                StreamW.WriteLine("exit");
                StreamW.Flush();

                String responseData2 = StreamR.ReadLine();

                StreamW.Close();
                StreamR.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                String returndata = ("ArgumentNullException: " + e);
            }
            catch (SocketException e)
            {
                String returndata = ("SocketException: " + e);
            }
            catch (IOException e)
            {
                String returndata = ("IOException: " + e);
            }
        }

        //CheckState werkt hetzelfde als DaHaus Methode, maar het stuurt een string terug. Hierin moet de staat zijn van de meubel je bekijkt.
        private String CheckState(String Command)
        {
            String ReturnData = "";

            try
            {
                Int32 port = 11000;
                String server = "127.0.0.1";
                TcpClient client = new TcpClient();
                client.Connect(server, port);

                NetworkStream Netstream = client.GetStream();
                StreamWriter StreamW = new StreamWriter(Netstream);
                StreamReader StreamR = new StreamReader(Netstream);

                StreamW.WriteLine(Command);
                StreamW.Flush();

                String responseData1 = StreamR.ReadLine();
                ReturnData = responseData1;

                StreamW.WriteLine("exit");
                StreamW.Flush();

                String responseData2 = StreamR.ReadLine();

                StreamW.Close();
                StreamR.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                String returndata = ("ArgumentNullException: " + e);
            }
            catch (SocketException e)
            {
                String returndata = ("SocketException: " + e);
            }
            catch (IOException e)
            {
                String returndata = ("IOException: " + e);
            }

            return ReturnData;
        }








        /*
        private void DaHausOld(String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 11000;
                String server = "127.0.0.1";
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                // Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();
                
                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Lbl_sent.Text = ("Sent: " + message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                String returndata = ("Received: " + responseData);
                Lbl_status.Text = returndata;

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                String returndata = ("ArgumentNullException: "+ e);
                Lbl_status.Text = returndata;
            }
            catch (SocketException e)
            {
                String returndata = ("SocketException: " + e);
                Lbl_status.Text = returndata;
            }
        }

    */

        protected void Btn_close_Click(object sender, EventArgs e)
        {

        }

        protected void Btn_Lamp1_Click(object sender, EventArgs e)
        {
            String Meubel = "lamp 0";
            Meubel_Keuze(Meubel);
        }

        protected void Btn_Lamp2_Click(object sender, EventArgs e)
        {
            String Meubel = "lamp 1";
            Meubel_Keuze(Meubel);
        }

        protected void Btn_Lamp3_Click(object sender, EventArgs e)
        {
            String Meubel = "lamp 2";
            Meubel_Keuze(Meubel);
        }

        protected void Btn_Lamp4_Click(object sender, EventArgs e)
        {
            String Meubel = "lamp 3";
            Meubel_Keuze(Meubel);
        }

        protected void Btn_Lamp5_Click(object sender, EventArgs e)
        {
            String Meubel = "lamp 4";
            Meubel_Keuze(Meubel);
        }

        protected void Btn_Window1_Click(object sender, EventArgs e)
        {
            String Meubel = "window 0";
            Meubel_Keuze(Meubel);
        }

        protected void Btn_Window2_Click(object sender, EventArgs e)
        {
            String Meubel = "window 1";
            Meubel_Keuze(Meubel);
        }

        protected void Btn_Heater_Click(object sender, EventArgs e)
        {
            String Meubel = "heater " + txt_heater.Text;
            Meubel_Keuze(Meubel);
        }
    }
}