using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace DenonControl
{
    public class TrayIcon : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

        public TrayIcon()
        {
            _notifyIcon = InitializeAppContextComponent();
        }

        private static NotifyIcon InitializeAppContextComponent()
        {
            var notifyIcon = new NotifyIcon
            {
                Icon = new Icon("Resources/tray.ico"),
                Text = "DenonControl",
                Visible = true
            };

            var exitButton = new ToolStripMenuItem { Text = "Exit Program" };
            exitButton.Click += (sender, args) =>
            {
                notifyIcon.Icon.Dispose();
                notifyIcon.Dispose();
                Application.Exit();
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add(new ToolStripLabel("DenonControl"));
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(exitButton);

            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.MouseClick += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    typeof(NotifyIcon)
                        .GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic)?
                        .Invoke(notifyIcon, null);
                }
            };

            return notifyIcon;
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }
    }
}