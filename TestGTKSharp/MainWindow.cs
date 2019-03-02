using System;
using Gtk;
using MessageCreator;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButton1Clicked(object sender, EventArgs e)
    {
        var dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, UserGreeting.GreetUser(entry1.Text));

        dialog.Run();
        dialog.Destroy();
    }

    private string SayHelloToUser(string name) => $"Hello, {name}!";
}
