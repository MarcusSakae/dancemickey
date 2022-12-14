namespace mickey;
using System.Runtime.InteropServices;

public partial class Form1 : Form
{


    static NotifyIcon notifyIcon = new NotifyIcon();
    static bool Vis = true;
    

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    public static void SetConsoleWindowVisibility(bool visible)
    {
        IntPtr hWnd = FindWindow(null, Console.Title);
        if (hWnd != IntPtr.Zero)
        {
            if (visible) ShowWindow(hWnd, 1); //1 = SW_SHOWNORMAL           
            else ShowWindow(hWnd, 0); //0 = SW_HIDE               
        }
    }
    public Form1()
    {
        InitializeComponent();
        notifyIcon.DoubleClick += (s, e) =>
        {
            Vis = !Vis;
            SetConsoleWindowVisibility(Vis);
        };
        notifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        notifyIcon.Visible = true;
        notifyIcon.Text = Application.ProductName;

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Exit", null, (s, e) => { Application.Exit(); });
        notifyIcon.ContextMenuStrip = contextMenu;

        Console.WriteLine("Running!");

        // Standard message loop to catch click-events on notify icon
        // Code after this method will be running only after Application.Exit()
        Application.Run();

        notifyIcon.Visible = false;
    }
}
