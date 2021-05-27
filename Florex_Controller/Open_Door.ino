void Open_Door()
{
  bool timer = false;
  
  while (DoorUP)
  {
    digitalWrite(Motor_Door_Pin_1, LOW);
    digitalWrite(Motor_Door_Pin_2, HIGH);

    if (digitalRead(Sensor_Door_Up) == 0)
    {
      Serial.println("открыто верх");
      digitalWrite(Motor_Door_Pin_1, LOW);
      digitalWrite(Motor_Door_Pin_2, LOW);
      Timer(20000);// запускаем отсчет времени
      while (true)// закрываем обратно
      {
        digitalWrite(Motor_Door_Pin_1, HIGH);
        digitalWrite(Motor_Door_Pin_2, LOW);

        if (digitalRead(Sensor_Door_Centr) == 0)
        {
          Serial.println("закрыто верх");
          digitalWrite(Motor_Door_Pin_1, LOW);
          digitalWrite(Motor_Door_Pin_2, LOW);
          DoorUP = false;
          break;
        }
      }
    }
  }
  while (DoorDown)
  {
    digitalWrite(Motor_Door_Pin_1, HIGH);
    digitalWrite(Motor_Door_Pin_2, LOW);

    if (digitalRead(Sensor_Door_Down) == 0)
    {
      Serial.println("открыто низ");
      digitalWrite(Motor_Door_Pin_1, LOW);
      digitalWrite(Motor_Door_Pin_2, LOW);
      Timer(20000);// запускаем отсчет времени
      while (true)// закрываем обратно
      {
        digitalWrite(Motor_Door_Pin_1, LOW);
        digitalWrite(Motor_Door_Pin_2, HIGH);

        if (digitalRead(Sensor_Door_Centr) == 0)
        {
          Serial.println("закрыто низ");
          digitalWrite(Motor_Door_Pin_1, LOW);
          digitalWrite(Motor_Door_Pin_2, LOW);
          DoorDown = false;
          break;
        }
      }
    }
  }
}
void Timer(int Interval)
{
  timing_window = millis(); // засекаем время
  while (true)
  {
    if (millis() - timing_window > Interval)
    {
//      timing_window = millis();
//      Serial.print("время - ");
//      Serial.println(timing_window);
      break;
    }
  }
}
