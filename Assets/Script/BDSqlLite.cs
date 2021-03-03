using Mono.Data.Sqlite;
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using UnityEngine;


public class BDSqlLite : MonoBehaviour
{
    public SqliteConnection Connect_BD;
    string Path;
    public SqliteCommand Command_BD;
    public SqliteDataReader Read_BD;
    public bool CashAndCell = true;


    void Start()
    {     
        Connact();
    }


    void Update()
    {

    }

    public void WriteBD(string money, string cell)
    {

        if (Connect_BD.State == ConnectionState.Open) // Если база открыта
        {
            if (CashAndCell)
            {                
                //Debug.Log(money);
                Command_BD = new SqliteCommand("INSERT INTO 'log' ( 'DATETIME', 'CELL', 'MONEY') VALUES (DATETIME('now', '+3 hours'), '" + cell + "', '" + money + "');", Connect_BD);
                Command_BD.ExecuteNonQuery();
                CashAndCell = false;
            }
         
        }
    }

    public void Connact()
    {
        Path = Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\SqlLite.s3db";

        //Path = Application.dataPath + "/SqlLite.s3db"; // Указываем путь к базе Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\Price.smp");
        Connect_BD = new SqliteConnection("URI=file:" + Path); // Соеденяемся с базой
        Connect_BD.Open(); // Открываем базу 

        if (Connect_BD.State == ConnectionState.Open) // Если база открыта
        {
            Command_BD = new SqliteCommand("CREATE TABLE if not exists log (id INTEGER PRIMARY KEY AUTOINCREMENT, DATETIME datetime, MONEY VARCHAR(20), CELL TEXT);", Connect_BD);
            Command_BD.ExecuteNonQuery();
            //Debug.Log("Conntct - BD");
            //Command_BD = new SqliteCommand("SELECT * FROM s", Connect_BD);// Обращаемся к таблице базы     
            //Command_BD = new SqliteCommand("CREATE TABLE example (id INTEGER PRIMARY KEY, value TEXT);", Connect_BD);
            //Command_BD = new SqliteCommand("INSERT INTO 'example' ( 'value') VALUES ( 'uuyut');", Connect_BD);
        }


        //Read_BD = Command_BD.ExecuteReader(); // Читаем все что есть в базе (Массив)           

        //while (Read_BD.Read())
        //{
        //    Debug.Log(Read_BD[1].ToString());// Читаем вторую ячейку в базе
        //}


    }

 
}
