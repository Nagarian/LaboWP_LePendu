﻿#pragma checksum "E:\One Drive\Documents\Project\C#\Le Pendu\Le Pendu 8.0\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CA256F06D2A066DD1690997836FB1CA6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34014
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Le_Pendu_8._0 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.TextBlock wordToFind;
        
        internal System.Windows.Controls.Image PenduImg;
        
        internal System.Windows.Controls.TextBox MyTextBox;
        
        internal System.Windows.Controls.TextBlock Compteur;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Le%20Pendu%208.0;component/MainPage.xaml", System.UriKind.Relative));
            this.wordToFind = ((System.Windows.Controls.TextBlock)(this.FindName("wordToFind")));
            this.PenduImg = ((System.Windows.Controls.Image)(this.FindName("PenduImg")));
            this.MyTextBox = ((System.Windows.Controls.TextBox)(this.FindName("MyTextBox")));
            this.Compteur = ((System.Windows.Controls.TextBlock)(this.FindName("Compteur")));
        }
    }
}
