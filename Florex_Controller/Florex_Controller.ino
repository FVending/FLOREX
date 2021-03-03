#include <WiFi.h>// подключяем библиотеку для работы с Wi-Fi server
#include <Wire.h>

int Cell;// переменная для номера ячейки
int Speed_ROW_Rotor = 200;// переменная скорости поворота барабана
int Level = 0;// для указания окна 1=низ, 2=верх
const int Rotor_STEP = 32;
const int Rotor_DIR = 33;
unsigned long timing_window = millis();
unsigned long timing_sensor  = millis();
int Sector = 0;
//const int Elevator_STEP = 25;
//const int Elevator_DIR = 26;
//const int Push_STEP = 27;
//const int Push_DIR = 14;
//const int Pump = 12;
const int Sensor_ROW = 23;
const int Button = 19;

// вводим имя и пароль точки доступа
const char* ssid = "SERMAN2";
const char* password = "serman33";

WiFiServer server(80);// инициализируем сервер на 80 порте

// создаем буфер и счетчик для буфера
char lineBuf[80];
int charCount = 0;

void setup()
{
  WiFi.softAP(ssid, password); // чтобы была точка доступа
  delay(5000);// запас времени на открытие монитора порта — 5 секунд

  pinMode(Rotor_STEP, OUTPUT);
  pinMode(Rotor_DIR, OUTPUT);


  pinMode(Sensor_ROW, INPUT_PULLUP);
  pinMode(Button, INPUT_PULLUP);  

  Serial.begin(115200);// инициализируем монитор порта

  Serial.println("");
  Serial.println("Wi-Fi connected");
  Serial.println("IP-address: ");

  Serial.println(WiFi.localIP());// пишем IP-адрес в монитор порта
  server.begin();

}

void loop()
{


}
