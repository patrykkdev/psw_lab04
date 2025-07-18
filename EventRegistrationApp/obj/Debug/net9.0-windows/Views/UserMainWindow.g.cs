﻿#pragma checksum "..\..\..\..\Views\UserMainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "82D9DA5FE2802D4356A99FF3A8C77B4B0BB223E6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace EventRegistrationApp.Views {
    
    
    /// <summary>
    /// UserMainWindow
    /// </summary>
    public partial class UserMainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock WelcomeText;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LogoutButton;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox EventComboBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AgendaTextBlock;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock EventDateTextBlock;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ParticipationTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FoodPreferenceComboBox;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RegistrationMessage;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RegisterForEventButton;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem MyRegistrationsTab;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\..\Views\UserMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid MyRegistrationsDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EventRegistrationApp;component/views/usermainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\UserMainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.WelcomeText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.LogoutButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\Views\UserMainWindow.xaml"
            this.LogoutButton.Click += new System.Windows.RoutedEventHandler(this.LogoutButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.EventComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 62 "..\..\..\..\Views\UserMainWindow.xaml"
            this.EventComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.EventComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AgendaTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.EventDateTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.ParticipationTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.FoodPreferenceComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.RegistrationMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.RegisterForEventButton = ((System.Windows.Controls.Button)(target));
            
            #line 140 "..\..\..\..\Views\UserMainWindow.xaml"
            this.RegisterForEventButton.Click += new System.Windows.RoutedEventHandler(this.RegisterForEventButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.MyRegistrationsTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 11:
            this.MyRegistrationsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

