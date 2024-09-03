using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace TCPService
{
    public class TCPServer
    {
        private TcpListener _tcpListener;
        public TCPServer()
        {
            StartServer();
        }

        private void StartServer()
        {
            // Define local address
            var port = 13000;
            var hostAddress = IPAddress.Parse("127.0.0.1");

            // TcpListener takes a local address and port as arguments.
            _tcpListener = new TcpListener(hostAddress, port);
            _tcpListener.Start();

            // Define the size of the byte array that represents the acceptable size of incoming stream.
            byte[] buffer = new byte[256];

            while (true)
            {
                Task.Delay(500).Wait();

                Console.Clear();
                Console.WriteLine("--> Server waiting for connection...");

                // Instantiate a TcpClient to handle incoming clients
                using TcpClient client = _tcpListener.AcceptTcpClient();

                Console.Clear();
                Console.WriteLine($"--> Server connected to client at {DateTimeOffset.Now}!");

                // Get stream/message
                NetworkStream tcpStream = client.GetStream();
                int readTotal = 0;
                string messageBuilder;

                while ((readTotal = tcpStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    // Encodes the stream into text
                    messageBuilder = Encoding.UTF8.GetString(buffer, 0, readTotal);

                    // Check if the message ends with \r\n, indicating the end of the message
                    if (!messageBuilder.ToString().EndsWith("\r\n"))
                    {
                        string incomingMessage = messageBuilder.ToString().Trim();
                        Console.WriteLine($"--> Server received message: {incomingMessage}");
                        Console.WriteLine($"--> At: {DateTimeOffset.Now}");
                        Console.WriteLine($". . .");

                        // Clear the message builder
                        messageBuilder = "";

                        // Send a response
                        string responseMessage = $"Server received message: {incomingMessage}\r\n";
                        byte[] response = Encoding.UTF8.GetBytes(responseMessage);

                        tcpStream.Write(response, 0, response.Length);
                    }
                }
            }
        }
    }
}
