﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace ColorBlind
{
    enum TransType { protanope, deuteranope, tritanope }
  

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenu contextMenu;

        private System.Windows.Forms.MenuItem protanopeMenuItem;    //红色盲
        private System.Windows.Forms.MenuItem deuteranopeMenuItem;  //绿色盲
        private System.Windows.Forms.MenuItem tritanopeMenuItem;   //蓝色盲
        //private Transformation transformation = new Transformation();
        private TransformationManager manager = new TransformationManager();
        TransType selectedType;


        public MainWindow()
        {
            
            InitializeComponent();
            InitializeContextMenu();
            InitialTray();
            manager.init();
        }

        private void InitializeContextMenu()
        {
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            
            protanopeMenuItem = new System.Windows.Forms.MenuItem("红色盲", new System.EventHandler(Type_Click));
            deuteranopeMenuItem = new System.Windows.Forms.MenuItem("绿色盲", new System.EventHandler(Type_Click));
            tritanopeMenuItem = new System.Windows.Forms.MenuItem("蓝色盲", new System.EventHandler(Type_Click));
            protanopeMenuItem.RadioCheck = true;
            protanopeMenuItem.Checked = true;
            deuteranopeMenuItem.RadioCheck = true;
            tritanopeMenuItem.RadioCheck = true;
            contextMenu.MenuItems.Add(protanopeMenuItem);
            contextMenu.MenuItems.Add(deuteranopeMenuItem);
            contextMenu.MenuItems.Add(tritanopeMenuItem);

            contextMenu.MenuItems.Add(
                new System.Windows.Forms.MenuItem("隐藏", new System.EventHandler(Hide_Click)));


            contextMenu.MenuItems.Add(
                new System.Windows.Forms.MenuItem("退出", new System.EventHandler(Exit_Click)));
   
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }




        protected void Type_Click(Object sender, System.EventArgs e)
        {


            if (sender == protanopeMenuItem)
            {
                protanopeMenuItem.Checked = true;
                deuteranopeMenuItem.Checked = false;
                tritanopeMenuItem.Checked = false;
                selectedType = TransType.protanope;
                manager.setColorEffect(selectedType);
               
            }

            else if(sender == deuteranopeMenuItem)
            {
                protanopeMenuItem.Checked = false;
                deuteranopeMenuItem.Checked = true;
                tritanopeMenuItem.Checked = false;
                selectedType = TransType.deuteranope;
                manager.setColorEffect(selectedType);
            }

            else if(sender == tritanopeMenuItem)
            {

                protanopeMenuItem.Checked = false;
                deuteranopeMenuItem.Checked = false;
                tritanopeMenuItem.Checked = true;
                selectedType = TransType.tritanope;
                manager.setColorEffect(selectedType);
               
            }
            




        }


        protected void Exit_Click(Object sender, System.EventArgs e)
        {

            Close();
        }
        private void InitialTray()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = ColorBlind.Properties.Resources.MainIcon;
            notifyIcon.Visible = true;
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Click);
            notifyIcon.DoubleClick += new EventHandler(this.notifyIcon_Double_Click);

            
            // NotifyIcon
        }

        private void notifyIcon_Double_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void notifyIcon_Click(object Sender, System.Windows.Forms.MouseEventArgs e)
        {
            
            
            if(e.Button == MouseButtons.Left)
            {
                manager.Toggle();
            }
        }


    }
}
