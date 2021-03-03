void Received_data()// обработка полученных данных
{


	if (Cell <= 7)
	{
		Sector = Cell;
		Level = 1;
		Running();
	}
	if (Cell <= 13)
	{
		Sector = Cell - 10;
		Level = 2;
		Running();
	}

}
