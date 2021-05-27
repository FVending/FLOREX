#include <WiFi.h>// подключяем библиотеку для работы с Wi-Fi server
#include <Wire.h>

int Cell;// переменная для номера ячейки
int Speed_ROW_Rotor = 1000;// переменная скорости поворота барабана
int Level = 0;// для указания окна 1=низ, 2=верх
const int Rotor_STEP = 32;// Шаговый мотор поворота карусели
const int Rotor_DIR = 33;// Старт, стоп шагового мотора
unsigned long timing_window = millis();
unsigned long timing_sensor  = millis();
int Sector = 0;
const int Motor_Door_Pin_2 = 12;
const int Motor_Door_Pin_1 = 13;
const int Sensor_Door_Up = 4;
const int Sensor_Door_Centr = 2;
const int Sensor_Door_Down = 15;
const int Sensor_ROW = 23;
unsigned long distance = 0;
bool Finish_Sector = true;
bool DoorUP = false;
bool DoorDown = false;
bool DoorCentr = false;

// вводим имя и пароль точки доступа
const char* ssid = "FLOREX";
const char* password = "florex00";

WiFiServer server(80);// инициализируем сервер на 80 порте
WiFiClient client;// Инициализируем объект client

String header;

// создаем буфер и счетчик для буфера
char lineBuf[80];
int charCount = 0;

void setup()
{
  WiFi.softAP(ssid, password); // чтобы была точка доступа
  delay(5000);// запас времени на открытие монитора порта — 5 секунд

  pinMode(Rotor_STEP, OUTPUT);
  pinMode(Rotor_DIR, OUTPUT);
  pinMode(Motor_Door_Pin_1, OUTPUT);
  pinMode(Motor_Door_Pin_2, OUTPUT);
  digitalWrite(Rotor_DIR, HIGH);
  digitalWrite(Motor_Door_Pin_1, LOW);
  digitalWrite(Motor_Door_Pin_2, LOW);

  pinMode(Sensor_ROW, INPUT_PULLUP);//_PULLUP
  pinMode(Sensor_Door_Centr, INPUT_PULLUP);
  pinMode(Sensor_Door_Down, INPUT_PULLUP);
  pinMode(Sensor_Door_Up, INPUT_PULLUP);

  Serial.begin(115200);// инициализируем монитор порта

  server.begin();
  Colibration();
}

void loop()
{
  Responce_Clitnt();

  digitalWrite(Rotor_STEP, HIGH);
  delayMicroseconds(Speed_ROW_Rotor * 3);
  digitalWrite(Rotor_STEP, LOW);
  delayMicroseconds(Speed_ROW_Rotor * 3);
}
