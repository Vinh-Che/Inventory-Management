﻿#pragma checksum "C:\Users\Vinh\Desktop\DOTNET DELIVERY\InventoryManagement\InventoryManagement\Views\Order.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3736C62E28A0BE746D8D11A4289E1CB4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagement
{
    partial class Order : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\Order.xaml line 16
                {
                    this.dataGrid1 = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.dataGrid1).ItemClick += this.GridView_ItemClick;
                }
                break;
            case 3: // Views\Order.xaml line 58
                {
                    this.ErrorMessage = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Views\Order.xaml line 59
                {
                    this.CategoryText = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CategoryText).SelectionChanged += this.CategoryText_SelectionChanged;
                }
                break;
            case 5: // Views\Order.xaml line 61
                {
                    this.dataGrid2 = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.dataGrid2).ItemClick += this.dataGrid2_ItemClick;
                }
                break;
            case 6: // Views\Order.xaml line 102
                {
                    this.CategoryId = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7: // Views\Order.xaml line 103
                {
                    this.ErrorProducts = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // Views\Order.xaml line 104
                {
                    this.AddCustomer = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddCustomer).Click += this.AddCustomer_Click;
                }
                break;
            case 9: // Views\Order.xaml line 105
                {
                    this.CustomerId = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // Views\Order.xaml line 106
                {
                    this.CustomerName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // Views\Order.xaml line 107
                {
                    this.CustomerMail = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // Views\Order.xaml line 108
                {
                    this.ProductQuantity = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.ProductQuantity).BeforeTextChanging += this.ProductQuantity_BeforeTextChanging;
                }
                break;
            case 13: // Views\Order.xaml line 109
                {
                    this.ProductId = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14: // Views\Order.xaml line 110
                {
                    this.Productname = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 15: // Views\Order.xaml line 111
                {
                    this.ProductPrice = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 16: // Views\Order.xaml line 119
                {
                    this.dataGrid3 = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.dataGrid3).ItemClick += this.dataGrid3_ItemClick;
                }
                break;
            case 17: // Views\Order.xaml line 169
                {
                    this.AddOrderList = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddOrderList).Click += this.AddOrder_Click;
                }
                break;
            case 18: // Views\Order.xaml line 170
                {
                    this.BtnOrders = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnOrders).Click += this.BtnOrders_Click;
                }
                break;
            case 19: // Views\Order.xaml line 171
                {
                    this.BtnDlOrde = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnDlOrde).Click += this.BtnDlOrde_Click;
                }
                break;
            case 20: // Views\Order.xaml line 173
                {
                    this.AddProduct = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddProduct).Click += this.AddProduct_Click;
                }
                break;
            case 21: // Views\Order.xaml line 174
                {
                    this.BtnClr = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnClr).Click += this.BtnClr_Click;
                }
                break;
            case 22: // Views\Order.xaml line 175
                {
                    this.OrderId = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 23: // Views\Order.xaml line 176
                {
                    this.OrderText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 24: // Views\Order.xaml line 177
                {
                    this.QuantityRemain = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 25: // Views\Order.xaml line 178
                {
                    this.orderQuantity = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 26: // Views\Order.xaml line 179
                {
                    this.pId = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
