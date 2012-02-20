using Gtk;
using System;
using Transport;

// Master class for program. Is just for the main function.
public class Dagatal
{
    public static void Main()
    {
        Vidmot vidmot = new Vidmot();
    }
}

// GUI class.
public class Vidmot
{

    // Gui variables.
    Window win;
    VBox vb;
    ScrolledWindow sw;
    TextView tv;
    Calendar cal;
    TextBuffer tvBuffer;
    string tvStrengur;
    
    public Vidmot()
    {
        Application.Init();
        Logik logik = new Logik();

        win = new Window("Diary");
        vb = new VBox();
        sw = new ScrolledWindow();
        tv = new TextView();
        cal = new Calendar();
        tvBuffer = tv.Buffer;

        // TextView buffer set.
        tvBuffer.Text = logik.fetchCurrString();

        sw.Add(tv);
        vb.Add(cal);
        vb.Add(sw);
        win.Add(vb);

        win.ShowAll();

        // On hide we save to file and close application.
        win.Hidden += delegate
        {
            logik.saveDiary();
            Application.Quit();
        };

        // When a new day is selected we send the logic class the new date as
        // a string, we send the text that belonged to the previous date,
        // we update the textview field.
        cal.DaySelected += delegate
        {
            string newDate = cal.GetDate().ToString("yyyyMMdd");
            System.Console.WriteLine("Strengurinn var: " + tvBuffer.Text);
            logik.loadDay(newDate, tvBuffer.Text);
            tvBuffer.Text = logik.fetchCurrString();
            System.Console.WriteLine("Nú er dagurinn: " + logik.fetchCurrDay());
            System.Console.WriteLine("Nú eru skilaboðin: " + logik.fetchCurrString());
        };

        Application.Run();
    }


}

public class Logik
{
    // currDay is the current active date.
    // currString is the current active content.
    // filename is the name of the database file which must
    // exist beforehand.
    // days is a the database variable of type Transport.Value.
    private string currDay, currString, filename;
    Value days;

    public Logik()
    {
        // Constructor for class. Sets date to today.
        currDay = DateTime.Now.ToString("yyyyMMdd");
        filename = "calendardb.dat";
        days = Value.LoadFile(filename);
        try
        {
            currString = (string) days[currDay];
        } catch(NullReferenceException e) 
        {
            currString = "";
        }
    }

    public void loadDay(string dagur, string exString)
    {
        // Retrieve from database
        days[currDay] = exString;
        currDay = dagur;
        try
        {
            currString = (string) days[currDay];
        } catch(NullReferenceException e) 
        {
            currString = "";
        }
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

    public void saveDiary()
    {
        // Saves all changes to file.
        Value.SaveFile(days, filename);
    }
}
