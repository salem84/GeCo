﻿#pragma checksum "..\..\..\..\View\ShellWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2EBC41FE7788A176F81DD6757B71303D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Ribbon;
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


namespace GeCo.Shell.Views {
    
    
    /// <summary>
    /// ShellWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class ShellWindow : Microsoft.Windows.Controls.Ribbon.RibbonWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GeCo.Shell.Views.ShellWindow RibbonWindow;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition RibbonRow;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition ClientRow;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.Ribbon.Ribbon ApplicationRibbon;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.Ribbon.RibbonTab HomeTab;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.Ribbon.RibbonButton btnInizializza;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ClientArea;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition NavigationColumn;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition WorkspaceColumn;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid NavigationPane;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition Navigator;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition TaskButtons;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl TaskButtonRegion;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\View\ShellWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl WorkspaceRegion;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/GeCo.Shell;component/view/shellwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\ShellWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RibbonWindow = ((GeCo.Shell.Views.ShellWindow)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.RibbonRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 4:
            this.ClientRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 5:
            this.ApplicationRibbon = ((Microsoft.Windows.Controls.Ribbon.Ribbon)(target));
            return;
            case 6:
            
            #line 37 "..\..\..\..\View\ShellWindow.xaml"
            ((Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnAbout_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 40 "..\..\..\..\View\ShellWindow.xaml"
            ((Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.HomeTab = ((Microsoft.Windows.Controls.Ribbon.RibbonTab)(target));
            return;
            case 9:
            this.btnInizializza = ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target));
            
            #line 54 "..\..\..\..\View\ShellWindow.xaml"
            this.btnInizializza.Click += new System.Windows.RoutedEventHandler(this.btnInizializza_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 59 "..\..\..\..\View\ShellWindow.xaml"
            ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.btnAbout_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 63 "..\..\..\..\View\ShellWindow.xaml"
            ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ClientArea = ((System.Windows.Controls.Grid)(target));
            return;
            case 13:
            this.NavigationColumn = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 14:
            this.WorkspaceColumn = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 15:
            this.NavigationPane = ((System.Windows.Controls.Grid)(target));
            return;
            case 16:
            this.Navigator = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 17:
            this.TaskButtons = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 18:
            this.TaskButtonRegion = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 19:
            this.WorkspaceRegion = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

