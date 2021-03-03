void Running()
{
	timing_sensor = millis();
	bool Home = false;

	while (true)
	{
		digitalWrite(Rotor_STEP, HIGH);
		delayMicroseconds(Speed_ROW_Rotor);
		digitalWrite(Rotor_STEP, LOW);
		delayMicroseconds(Speed_ROW_Rotor);

		if (Sensor_ROW == 1)
		{			
			if (millis() - timing_sensor > 2000)//Раз в 2с проверяем значение датчика
			{
				if (Sensor_ROW == 0 and Home == true)
				{
					Sector = Sector + 1;
				}
			}
			else
			{
				if (Sensor_ROW == 0 and Home == false)
				{
					Home = true;
				}
			}
		}
		else
		{
			timing_sensor = millis();
		}
	}
	Serial.println("Home true");

	for (int i = 0; i < Cell; i++)
	{

	}

	Serial.println("Level true");
}
