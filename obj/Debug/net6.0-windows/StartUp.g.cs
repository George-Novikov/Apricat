﻿#pragma checksum "..\..\..\StartUp.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A06F60C9E569D451B1EFEBD431FB60A2571C063F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Apricat;
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


namespace Apricat {
    
    
    /// <summary>
    /// StartUp
    /// </summary>
    public partial class StartUp : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox loginGroupBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel userChoiceList;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button loginButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox registerGroupBox;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton beginnerButton;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton elementaryButton;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton intermediateButton;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton upperIntermediateButton;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton advancedButton;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button registerButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Apricat;component/startup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StartUp.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\StartUp.xaml"
            ((Apricat.StartUp)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.StartUpClosing_LoadMainWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.loginGroupBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 3:
            this.userChoiceList = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.loginButton = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.registerGroupBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 6:
            this.beginnerButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 80 "..\..\..\StartUp.xaml"
            this.beginnerButton.Click += new System.Windows.RoutedEventHandler(this.ToggleButtons_UncheckOnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.elementaryButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 95 "..\..\..\StartUp.xaml"
            this.elementaryButton.Click += new System.Windows.RoutedEventHandler(this.ToggleButtons_UncheckOnClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.intermediateButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 110 "..\..\..\StartUp.xaml"
            this.intermediateButton.Click += new System.Windows.RoutedEventHandler(this.ToggleButtons_UncheckOnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.upperIntermediateButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 125 "..\..\..\StartUp.xaml"
            this.upperIntermediateButton.Click += new System.Windows.RoutedEventHandler(this.ToggleButtons_UncheckOnClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.advancedButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 141 "..\..\..\StartUp.xaml"
            this.advancedButton.Click += new System.Windows.RoutedEventHandler(this.ToggleButtons_UncheckOnClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.registerButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
