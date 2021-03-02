using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MDB : MonoBehaviour
{
    SerialPort Port;
    string COM_Port;
    string Money;
    int Number_Price = 0;
    string Request_Adr = "http://192.168.0.";
    string Rec = ""; // переменная для записи по два байта всюстроку. так как UTF8 состоит из например (208 144)
    bool Initialization = true;
    public static byte Response;
    public static bool Vend = false;
    public GameObject Errors_Panel;
    public GameObject Start_Panel;
    public GameObject Pay_Done_Gui;
    public GameObject Pay_DENIED_Gui;
    public GameObject Pay_Page;

    public static byte[] Reset = new byte[] { 0x10 };//  Reset
    public static byte[] Setap = new byte[] { 0x11, 0x00, 0x03, 0x00, 0x00, 0x00 };// Setap
    public static byte[] Setap2 = new byte[] { 0x11, 0x01, 0xff, 0xff, 0x00, 0x00 };// Setap2
    public static byte[] RequestID = (new byte[] { 0x17, 0x00, 0x4D, 0x45, 0x49, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                                                   0x30, 0x4E, 0x47, 0x43, 0x20, 0x4D, 0x44, 0x42, 0x20, 0x30, 0x30, 0x30, 0x31, 0x01, 0x27 }); // Expansion2
    public static byte[] Expansion = (new byte[] { 0x17, 0x04, 0x00, 0x00, 0x00, 0x20 }); // Expansion
    public static byte[] Enable = new byte[] { 0x14, 0x01 };  //Enable
    public static byte[] VendRequest2 = new byte[] { 0x13, 0x00, 0x03, 0xE8, 0x00, 0x00 };// VendRequest
    public static byte[] VendRequest = new byte[] { 0x13, 0x00, 0x00, 0x50, 0x00, 0x00 };// VendRequest
    public static byte[] VendCancel = (new byte[] { 0x13, 0x01 }); // vend_cancel
    public static byte[] VendSuccess = (new byte[] { 0x13, 0x02, 0x00, 0x00 }); // vend_success                                                                                    
    public static byte[] VendFailure = (new byte[] { 0x13, 0x03 }); // vend_failure
    public static byte[] SessionComplete = (new byte[] { 0x13, 0x04 }); // session_complete
    public static byte[] Disable = (new byte[] { 0x14, 0x00 }); // DISABLE
    public static byte[] Poll = new byte[] { 0x12 };
    public static byte[] ACK = new byte[] { 0x00 };
    public static byte[] NAK = new byte[] { 0xff };


    void Awake()
    {
        Process.Start(Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\USB_Skan.exe");
        

    }

    void Start()
    {
       
        SCAN_PORT();
     
    }


    void Update()
    {
        if (Port.IsOpen)
        {
            Terminal_Card();
        }

    }

    public void Click_Price(string Money_Price, string Name) // тут получаем значение из вне
    {
        Number_Price = Convert.ToInt32(Name.Split('.').First()) * 10;
        //Debug.Log(Number_Price);
        //gameObject.GetComponent<HttpReqoestExample>().Send(Request_Adr + Number_Price +"/on1");
        
        //gameObject.GetComponent<Controller>().Magic(Request_Adr + Number_Price);
        //char xx = Convert.ToChar(Name);
        Money = Money_Price;
        byte[] Byte_Name = Encoding.UTF8.GetBytes(Name); // декодируем название в UTF8
        int Count_Two = 0; // переменная для групировки по два байта       
                           //string Rec = ""; // переменная для записи по два байта всюстроку. так как UTF8 состоит из например (208 144)
        //gameObject.GetComponent<HttpReqoestExample>().Send("http://192.168.0.10/start");

        for (int i = 0; Byte_Name.Length > i; i++) // цикл который разделяет слешем строку. для упрощения работы с ней в дальнейшем
        {
            Count_Two = Count_Two + 1;
            if (Count_Two != 0)
            {
                Rec = Rec + Convert.ToString(Byte_Name[i]);
            }
            if (Count_Two == 2)
            {
                Rec = Rec + "/";
                Count_Two = 0;
            }

        }
        //Debug.Log(Rec);
        //gameObject.GetComponent<BDSqlLite>().WriteBD(Money, Rec);

        Pay_Page.SetActive(true);
        Vend = true;

        int money = Convert.ToInt32(Money_Price); // берем полученное значение и преобразуем в целое число

        if (money < 256) // если число укладывается до 255 то все норм. это один байт
        {
            VendRequest[3] = Convert.ToByte(Money_Price);  // вставляем цену в байт
            ProcessingByte(VendRequest, 250);
        }
        else // если это число больше 255, то это не один байт, а уже два. (нужно их разделить по одному)
        {

            int Count = 1; // это для замены адреса в массиве
            int AnBack = 2; // это для того чтобы перевернуть зеркально полученные два байта
            Byte[] bytes = BitConverter.GetBytes(Convert.ToUInt16(Money_Price)); // тут получаем два байта

            for (int i = 0; i < bytes.Length; i++) // запускаем цикл для работы с масивом и полученым байтом
            {
                Count = Count + 1;
                AnBack = AnBack - 1;
                VendRequest2[Count] = bytes[AnBack]; // меняем значения в массиве VendRequest2.               
            }
            ProcessingByte(VendRequest2, 250);
        }
    }

    void SCAN_PORT()
    {
        Thread.Sleep(1000);
        string[] ReadConfig = File.ReadAllLines(Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\Config.smp"); // Создаем массив в котором будут строки из файла в корневой дерриктории проекта
        foreach (string s in ReadConfig) // перебираем массив
        {

            if (s.Split('=').First() == "bill_acceptor") // если в массиве нашли перед символом равно (name)
            {
                COM_Port = s.Split('=').Last();
            }
        }

        Port = new SerialPort(COM_Port, 9600, Parity.None, 8, StopBits.One); // назначаем порт
        try
        {
            Port.Open();
            Port.ReadTimeout = 1;
            Port.WriteTimeout = 1;
        }
        catch
        {
            Errors_Panel.GetComponent<Errors>().ERROR("ВНИМАНИЕ !!! НЕ ПОДКЛЮЧЕНА ПЛАТЕЖНАЯ СИСТЕМА !!!");
        }

    }

    void Terminal_Card()
    {

        if (Initialization)
        {
            ProcessingByte(ACK, 250);
            ProcessingByte(Reset, 250);
            ProcessingByte(Setap, 250);
            ProcessingByte(Setap2, 250);
            ProcessingByte(ACK, 250);
            ProcessingByte(RequestID, 250);
            ProcessingByte(ACK, 250);
            ProcessingByte(Expansion, 250);
            ProcessingByte(Enable, 250);
            Initialization = false;
        }

        try
        {
            if (Vend)
            {
                ProcessingByte(Poll, 25);
            }

            int xx = Port.ReadByte();
            Debug.Log(xx);

            switch (xx)
            {
                case 5:
                    if (Vend)
                    {
                        ProcessingByte(ACK, 250);
                        ProcessingByte(VendSuccess, 250);
                        ProcessingByte(SessionComplete, 250);
                        Pay_Page.SetActive(false);
                        Pay_Done_Gui.SetActive(true);
                        Pay_Page.SetActive(false);
                        Debug.Log("Добро");
                        gameObject.GetComponent<Controller>().Magic(Request_Adr + Number_Price);
                        Initialization = true;
                        gameObject.GetComponent<BDSqlLite>().CashAndCell = true;
                        gameObject.GetComponent<BDSqlLite>().WriteBD(Money, Rec);
                        Vend = false;
                    }

                    break;

                case 6:
                    if (Vend)
                    {
                        ProcessingByte(ACK, 250);
                        ProcessingByte(VendSuccess, 250);
                        ProcessingByte(SessionComplete, 250);
                        Debug.Log("Нищеброд");
                        Pay_Page.SetActive(false);
                        Pay_DENIED_Gui.SetActive(true);
                        Pay_Page.SetActive(false);
                        Initialization = true;
                        Vend = false;
                    }

                    break;
                case 7:
                    if (!Vend)
                    {
                        ProcessingByte(ACK, 250);
                        ProcessingByte(Disable, 250);
                        ProcessingByte(Enable, 250);
                        Debug.Log("Disable");
                    }

                    break;
            }
        }
        catch { }

    }

    void ProcessingByte(byte[] commands, int Sleep)
    {
        var checkSum = CalCheckSum(commands, commands.Length);
        byte[] Summ;

        Thread.Sleep(Sleep);
        Port.Parity = Parity.Mark;
        Port.Write(commands, 0, commands.Length);
        Port.Parity = Parity.Space;
        Summ = new byte[] { checkSum };
        Port.Write(Summ, 0, Summ.Length);
        //Debug.Log(Summ.Length);
        //foreach (byte s in Summ)
        //{
        //    Debug.Log(s);
        //}
    }

    private byte CalCheckSum(byte[] _PacketData, int PacketLength)
    {
        Byte _CheckSumByte = 0x00;
        for (int i = 0; i < PacketLength; i++)
        {
            _CheckSumByte += _PacketData[i];
        }

        //Debug.Log(_CheckSumByte & 0xFF);
        return (byte)(_CheckSumByte & 0xFF);
    }
}
