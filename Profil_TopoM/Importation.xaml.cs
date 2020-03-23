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

namespace Profil_TopoM
{
    /// <summary>
    /// Logique d'interaction pour Importation.xaml
    /// </summary>
    public partial class Importation : UserControl
    {
        public Importation(BitmapImage userImage)
        {
            InitializeComponent();
            img.Source = userImage;
        }
    }
}
