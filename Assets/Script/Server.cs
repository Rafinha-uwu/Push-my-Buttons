using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UI;



public class ServerScript : MonoBehaviour
{
    TcpListener server = null;
    TcpClient client = null;
    NetworkStream stream = null;
    Thread thread;
    //Thread and things like this i think u understand, TcpListener and so on is Library at least u can search them in the lear.microsoft.com


    bool con = true; // con of connected not good practic but it works like this

    public string ServerIP = "";
    public byte[] buffer = new byte[1024];
    public string data = null;

    private void Start()
    {
        thread = new Thread(new ThreadStart(SetupServer));
        ServerIP = GetLocalIPAddress();
        thread.Start();
    }

    private void Update()
    {
        // Sending "Hello" when u press space in game, you know this


        //con = server.Pending(); server.Pending() -> returns bool, if there are conenctions pending or not 
        // Sending a message when is "connected"

    }

    public void SetupServer()
    {
        try
        {
            // So seting up your server, the IPAddress.Parse("") - the argument is the IP of your computer, in the network you currently in
            IPAddress localAddr = IPAddress.Parse(ServerIP);
            //TCPListener(localAddr, 3214) the number 3214 is the port that is connected to, We want multiclient so probably a vector/array of Ports
            server = new TcpListener(localAddr, 4040);
            server.Start();



            while (true)
            {
                Debug.Log("Waiting for connection...");
                //print("Waiting for connection...");
                client = server.AcceptTcpClient();
                Debug.Log("Connected!");
                // Dont know, really, where i find Debug.log
                print("Client Conected!");
                //con = true;
                data = null;
                stream = client.GetStream();

                int i;
                // Get's the data from the client and responds to the client with Server Responde : message
                while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    data = Encoding.UTF8.GetString(buffer, 0, i);
                    Debug.Log("Received: " + data);


                }
                client.Close();
            }
        }
        catch (SocketException e)
        {
            print("SocketException: " + e);
            Debug.Log("SocketException: " + e);
        }
        finally
        {
            server.Stop();
        }
    }

    private void OnApplicationQuit()
    {
        stream.Close();
        client.Close();
        server.Stop();
        thread.Abort();
    }

    //public string GetLocalIPAddress()
    //{
    //    var host = Dns.GetHostEntry(Dns.GetHostName());
    //    foreach (var ip in host.AddressList)
    //    {
    //        if (ip.AddressFamily == AddressFamily.InterNetwork)
    //        {
    //            return ip.ToString();
    //        }
    //    }
    //    throw new System.Exception("No network adapters with an IPv4 address in the system!");
    //}
    private string GetLocalIPAddress()
    {
        string localIP = string.Empty;
        try
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIP = ip.Address.ToString();
                            return localIP;
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error fetching IP address: " + e.Message);
        }

        return "Not connected to a Wi-Fi network";
    }
}