using System;

public class Dagatal
{
    public static void Main()
    {
        Logik logik = new Logik();
    }
}
public class Vidmot
{
}

public class Logik
{
    private string currDay, currString, filename;

    public Logik()
    {
        currDay = DateTime.Now.ToString("yyyyMMdd");
        filename = "calendardb.dat";
        currString = "";
        loadDay(currDay);
    }

    void closeDay()
    {
        // update database
        fileCheck();
        days[currDay] = currString;
    }

    void loadDay(string dagur)
    {
        // Retrieve from database
        closeDay();
        currDay = dagur;
        Value days = Value.MakeTable();
        days = Value.LoadFile(filename);
        try
          {
          currString = (string) days[currDay];
          } catch(NullReferenceException e) 
          {
          currString = "";
          }
    }

    /*void switchDay(string dagur)
      {
    // Close current day and load a new one.
    closeDay();
    currString = loadDay(dagur);
    currDay = dagur;
    }*/

    public string fetchCurrDay()
    {
        // Return the selected day of the logic part.
        return currDay;
    }

    public string fetchCurrString()
    {
        // Return the text of the selected day of in the logic part.
        return currString;
    }

    void fileCheck()
    {
        // Tests if calendar entry file exists and creates and initializes one
        // if needed.
        Value days;
        try
        {
            days = Value.LoadFile(filename);
        } catch(System.IO.FileNotFoundException e) {
            System.IO.File.Create(filename);
        } catch(System.IO.EndOfStreamException f) {
            days = Value.MakeTable();
            days[currDay] = "";
            Value.SaveFile(days, filename);
        }
    }
}
