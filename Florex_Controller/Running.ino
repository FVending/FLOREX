void Running()
{
  timing_sensor = millis();
  int Interval = 0;
  bool Home = false;
  bool Home2 = false;
  bool Row = true;
  int sector_sensor = 0;

  Serial.println("start");

  while (Home == false)
  {
    digitalWrite(Rotor_STEP, HIGH);
    delayMicroseconds(Speed_ROW_Rotor);
    digitalWrite(Rotor_STEP, LOW);
    delayMicroseconds(Speed_ROW_Rotor);

    if (digitalRead(Sensor_ROW) == 0 and millis() - timing_sensor > distance)
    {
      Home2 = true;     
    }
    if (Home2 == true and digitalRead(Sensor_ROW) == 0)// поиск HOME
    {
      if (millis() - timing_sensor > 30 and millis() - timing_sensor < distance / 2)
      {
        Serial.println("Home true");
        Home2 = false;
        break;
      }
      timing_sensor = millis();
    }
  }

  while (Row)
  {
    digitalWrite(Rotor_STEP, HIGH);
    delayMicroseconds(Speed_ROW_Rotor);
    digitalWrite(Rotor_STEP, LOW);
    delayMicroseconds(Speed_ROW_Rotor);
    if (digitalRead(Sensor_ROW) == 0 and millis() - timing_sensor > distance)
    {
      sector_sensor++;
      Serial.println(sector_sensor);
      if (Sector == sector_sensor)
      {
        Row = false;
        sector_sensor = 0;
        Sector = 0;
        break;
      }
      timing_sensor = millis();
    }
  }
  Open_Door();//открываем окно
  Serial.println("Конец");
}
