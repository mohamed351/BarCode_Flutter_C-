using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace BarCodeServer
{
    public delegate void ReciveMessageDelegate(Socket socket, ReciverDTO reciver);
    public delegate void ReciveBarCodeDelegate(Socket socket, string barcode);

    public static class SocketApplication
    {
       public static Thread thread = new Thread(OpenServer);

        static List<Socket> sockets = new List<Socket>();

        public static event ReciveBarCodeDelegate OnReciveMessage;


        public static event ReciveMessageDelegate OnSuccessfull;
        public static int PortNumber { get; set; } = 5000;


    
        public static void OpenServer()
        {
            TcpListener tcpListener = TcpListener.Create(PortNumber);
            tcpListener.Start();
            while (true)
            {
               var newSoket =   tcpListener.AcceptSocket();
                sockets.Add(newSoket);
                OnSuccessfull(newSoket, new ReciverDTO() { RequestType = RequestType.NewConnection });
                Thread reciverThread = new Thread(() =>
                {
                    while (true)
                    {
                        if (newSoket.Available != 0)
                        {
                            byte[] array = new byte[newSoket.ReceiveBufferSize];
                            newSoket.Receive(array);
                            var response = StringHelper.ConvertByteArrayToString(array);
                            OnReciveMessage(newSoket, response);
                        }
                    }
               
                     
                   


                });
                reciverThread.Start();


            }
        }
    }
    public static class StringHelper
    {
        public static byte[] ConvertStringToByteArray(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
        public static string ConvertByteArrayToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static ReciverDTO DeseilzieRequest(string currentString)
        {
         return  JsonConvert.DeserializeObject<ReciverDTO>(currentString);
        }
        public static string SerilizeRequest(ReciverDTO reciverDTO)
        {
            return JsonConvert.SerializeObject(reciverDTO);

        }




    }


    
   

    
}
