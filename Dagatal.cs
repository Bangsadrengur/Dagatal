using Gtk;
using System;
using Transport;

public class Dagatal
{
    public static void Main()
    {
        Vidmot vidmot = new Vidmot();
    }
}
public class Vidmot
{
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

        tvBuffer.Text = logik.fetchCurrString();

        sw.Add(tv);
        vb.Add(cal);
        vb.Add(sw);
        win.Add(vb);

        win.ShowAll();

        win.Hidden += delegate
        {
            Application.Quit();
        };

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
    private string currDay, currString, filename;
    Value days;

    public Logik()
    {
        currDay = DateTime.Now.ToString("yyyyMMdd");
        filename = "calendardb.dat";
        currString = "";
        days = Value.LoadFile(filename);
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
}
