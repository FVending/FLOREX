void Received_data()// обработка полученных данных
{

  Serial.println(Cell);
  if (Cell <= 6)
  {
    DoorUP = true;
    Sector = Cell;
    Level = 1;
    Running();
  }
  if (Cell >= 7)
  {
    DoorDown = true;
    Sector = Cell - 6;
    Level = 2;
    Running();
  }

}
