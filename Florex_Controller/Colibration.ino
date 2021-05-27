void Colibration()
{
  int caunt = 0;
  timing_sensor = millis();
  while (true)
  {
    digitalWrite(Rotor_STEP, HIGH);
    delayMicroseconds(Speed_ROW_Rotor);
    digitalWrite(Rotor_STEP, LOW);
    delayMicroseconds(Speed_ROW_Rotor);

    if (digitalRead(Sensor_ROW) == 0)
    {
      if (millis() - timing_sensor > 100)
      {
        caunt++;
        if (distance < millis() - timing_sensor)
        {
          distance = millis() - timing_sensor;
          Serial.println(distance);
        }
        if(caunt >= 15)
        {
//          Running();
          break;
        }
      }
      timing_sensor = millis();
    }
  }
}
