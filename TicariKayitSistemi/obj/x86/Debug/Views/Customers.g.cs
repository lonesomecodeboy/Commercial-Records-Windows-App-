﻿

#pragma checksum "C:\Users\İbrahim\Documents\Projeler\ticarikayitsistemi\TicariKayitSistemi\Views\Customers.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "52EDC24A459D4DA48F18935E5EBED278"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicariKayitSistemi
{
    partial class Costumers : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 48 "..\..\..\Views\Customers.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.costumersList_clicked;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 197 "..\..\..\Views\Customers.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.addCustomerButton_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 200 "..\..\..\Views\Customers.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.deleteCustButton_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 203 "..\..\..\Views\Customers.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.editCustButton_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 31 "..\..\..\Views\Customers.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.backButton_Clicked;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


