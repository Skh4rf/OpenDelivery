﻿#pragma checksum "S:\Schule\3chel\FSST\_project\OpenDelivery\src\OpenDelivery\RoutePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6DA86954D3998A6FA660940992E83CCE53D8729D9080DBA387353E28D479CBD9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenDelivery
{
    partial class RoutePage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // RoutePage.xaml line 12
                {
                    this.RouteSelect = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 3: // RoutePage.xaml line 23
                {
                    this.ButtonReloadDatabase = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ButtonReloadDatabase).Click += this.Button_Click;
                }
                break;
            case 4: // RoutePage.xaml line 20
                {
                    this.GridRouteListing = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5: // RoutePage.xaml line 15
                {
                    this.ComboBoxRouteSelect = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.ComboBoxRouteSelect).SelectionChanged += this.ComboBoxRouteSelect_SelectionChanged;
                }
                break;
            case 6: // RoutePage.xaml line 16
                {
                    this.ButtonCreateRoute = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 7: // RoutePage.xaml line 17
                {
                    this.LoadRoute = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.LoadRoute).Click += this.LoadRoute_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

