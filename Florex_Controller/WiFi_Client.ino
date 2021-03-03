void Responce_Clitnt()
{ 
  WiFiClient client = server.available();
  if (client) {
    Serial.println("New client");
    memset(lineBuf, 0, sizeof(lineBuf));
    charCount = 0;
    String currentLine = "";   
    boolean currentLineIsBlank = true;
    while (client.connected())
    {
      if (client.available())
      {
        char c = client.read();
        Serial.write(c);       
        lineBuf[charCount] = c;
        if (charCount < sizeof(lineBuf) - 1)
        {
          charCount++;
        }
        
        if (c == '\n' && currentLineIsBlank)
        {        
          client.println("HTTP/1.1 200 OK");
          client.println("Content-Type: text/html");         
          client.println("Connection: close");         
          client.println();         
          String webPage = "<!DOCTYPE HTML>";
          webPage += "  <p>";
          webPage += "    DOOR_OPEN = ";
//          webPage += digitalRead(Inn_31);
          webPage += "  <br>";
          webPage += "    DOOR_CLOSE = ";
//          webPage += digitalRead(Inn_37);
          webPage += "  <br>";
          webPage += "    ROTATION_STOP = ";
//          webPage += digitalRead(Inn_23);
          webPage += "  <br>";
          client.println(webPage);
          break;
        }
        if (c == '\n')
        {      
          currentLineIsBlank = true;
          if (strstr(lineBuf, "GET /=") > 0)
          {
            String responce = lineBuf;// Получаем что пришло по запросу
            responce = responce.substring(6);// Берем значение после знака =
            Cell = responce.toInt();// Присваиваем номер 
            Received_data();// обработка полученных данных
           
          }                  
          currentLineIsBlank = true;
          memset(lineBuf, 0, sizeof(lineBuf));
          charCount = 0;
        }
        else if (c != '\r')
        {         
          currentLineIsBlank = false;
        }
      }
    }   
    delay(1);    
    client.stop();
    Serial.println("client disconnected");
  }
}
