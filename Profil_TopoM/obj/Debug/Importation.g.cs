﻿#pragma checksum "..\..\Importation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "742B4FE9166C8D9F7626FF1D292794FCB00228251CA9C13A0CEB957AAE3CF2D9"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Profil_TopoM;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Profil_TopoM {
    
    
    /// <summary>
    /// Importation
    /// </summary>
    public partial class Importation : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grids;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas cnv;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider zoom;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Supprimer;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Arret;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextBtn;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button prevBtn;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\Importation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button next;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Profil_TopoM;component/importation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Importation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Grids = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.img = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.cnv = ((System.Windows.Controls.Canvas)(target));
            
            #line 43 "..\..\Importation.xaml"
            this.cnv.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.cnv_MouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 43 "..\..\Importation.xaml"
            this.cnv.MouseMove += new System.Windows.Input.MouseEventHandler(this.cnv_MouseMove);
            
            #line default
            #line hidden
            
            #line 43 "..\..\Importation.xaml"
            this.cnv.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.cnv_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.zoom = ((System.Windows.Controls.Slider)(target));
            return;
            case 6:
            this.Supprimer = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\Importation.xaml"
            this.Supprimer.Click += new System.Windows.RoutedEventHandler(this.Supprimer_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Arret = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\Importation.xaml"
            this.Arret.Click += new System.Windows.RoutedEventHandler(this.Arret_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.nextBtn = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\Importation.xaml"
            this.nextBtn.Click += new System.Windows.RoutedEventHandler(this.next_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.prevBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.next = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\Importation.xaml"
            this.next.Click += new System.Windows.RoutedEventHandler(this.next_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

