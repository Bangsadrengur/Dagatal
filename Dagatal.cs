using System;
using Transport;

public class Dagatal
{
    public static void Main()
    {
        Logik logik = new Logik();
        logik.fileCheck();
        System.Console.WriteLine(logik.fetchCurrDay());
    }
}
public class Vidmot
{
}

public class Logik
{
    private string currDay, currString;

    public Logik()
    {
        currDay = DateTime.Now.ToString("yyyyMMdd");
        currString = loadDay(currDay);
    }

    void closeDay(string dagur, string strengur)
    {
        // update database
        System.Console.WriteLine("Lokum degi: " + dagur +
                " með því að uppfæra hann með texta: " +
                strengur);
    }

    string loadDay(string dagur)
    {
        // Retrieve from database
        return "Herp derp derp";
    }

    void switchDay(string dagur)
    {
        // Close current day and load a new one.
        closeDay(currDay, currString);
        currString = loadDay(dagur);
        currDay = dagur;
    }

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

    public void fileCheck()
    {
        // Test if file available for day and create one if not available.
        Value days = Value.MakeTable();
        string filename = currDay + ".dat";
        try
        {
            days = Value.LoadFile(filename);
        } catch(System.IO.FileNotFoundException e) {
            System.IO.File.Create(filename);
        }
    }

}
