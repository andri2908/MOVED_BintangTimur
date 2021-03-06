﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Drawing.Printing;
using Hotkeys;


namespace AlphaSoft
{
    public partial class dataReturPenjualanForm : Form
    {
        private int originModuleID;
        private int selectedCustomerID;
        private string selectedProductID;
        private string selectedSalesInvoice = " ";
        private string selectedSalesRev = " ";
        private double globalTotalValue = 0;
        private bool isLoading = false;
        private bool returnCash = false;
        private string currencyFormat = "C2";

        //private List<string> returnQty = new List<string>();
        //private List<string> productPriceList = new List<string>();
        //private List<string> SOreturnQty = new List<string>();
        //private List<string> subtotalList = new List<string>();

        private string previousInput = "";
        private double extraAmount = 0;
        private string returID = "0";
        private int locationID = 0;
        private Hotkeys.GlobalHotkey ghk_F1;
        private Hotkeys.GlobalHotkey ghk_F2;
        private Hotkeys.GlobalHotkey ghk_F8;
        private Hotkeys.GlobalHotkey ghk_F9;
        private Hotkeys.GlobalHotkey ghk_F11;
        private Hotkeys.GlobalHotkey ghk_DEL;

        private Hotkeys.GlobalHotkey ghk_CTRL_DEL;
        private Hotkeys.GlobalHotkey ghk_CTRL_ENTER;

        private Data_Access DS = new Data_Access();
        private globalUtilities gutil = new globalUtilities();
        private CultureInfo culture = new CultureInfo("id-ID");

        public dataReturPenjualanForm()
        {
            InitializeComponent();
        }

        public dataReturPenjualanForm(int moduleID, string purchaseInvoice = "", int customerID = 0)
        {
            InitializeComponent();

            originModuleID = moduleID;

            if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
            {
                invoiceDateLabel.Visible = false;
                invoiceDateTextBox.Visible = false;
                invoiceTotalLabel.Visible = false;
                invoiceTotalLabelValue.Visible = false;
                invoiceSignLabel.Visible = false;
                selectedCustomerID = customerID;
                selectedSalesInvoice = customerID.ToString();
                label2.Visible = false;

                invoiceInfoLabel.Text = "PELANGGAN";
            }
            else
            {
                selectedSalesInvoice = purchaseInvoice;
            }
        }

        public dataReturPenjualanForm(int moduleID, string purchaseInvoice = "", string revNo = "")
        {
            InitializeComponent();

            originModuleID = moduleID;

            if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
            {
                invoiceDateLabel.Visible = false;
                invoiceDateTextBox.Visible = false;
                invoiceTotalLabel.Visible = false;
                invoiceTotalLabelValue.Visible = false;
                invoiceSignLabel.Visible = false;
                label2.Visible = false;

                invoiceInfoLabel.Text = "PELANGGAN";
            }
            else
            {
                selectedSalesRev = revNo;
                selectedSalesInvoice = purchaseInvoice;
            }
        }

        private void captureAll(Keys key)
        {
            string searchParam = "";
            switch (key)
            {
                case Keys.F1:
                    penerimaanBarangHelpForm displayHelp = new penerimaanBarangHelpForm();
                    displayHelp.ShowDialog(this);
                    break;

                case Keys.F2:
                    if (saveButton.Enabled == true)
                    { 
                        barcodeForm displayBarcodeForm = new barcodeForm(this, globalConstants.RETUR_PENJUALAN);

                        displayBarcodeForm.Top = this.Top + 5;
                        displayBarcodeForm.Left = this.Left + 5;//(Screen.PrimaryScreen.Bounds.Width / 2) - (displayBarcodeForm.Width / 2);

                        displayBarcodeForm.ShowDialog(this);
                    }
                    break;

                case Keys.F8:
                    if (detailReturDataGridView.ReadOnly == false)
                    { 
                        detailReturDataGridView.Focus();
                        addNewRow();
                    }
                    break;

                case Keys.F9:
                    if ( saveButton.Enabled == true )
                        saveButton.PerformClick();
                    break;

                case Keys.F11:
                    if (detailReturDataGridView.ReadOnly == false)
                    {
                        if (originModuleID == globalConstants.RETUR_PENJUALAN)
                            searchParam = selectedSalesInvoice;
                        else if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
                            searchParam = selectedCustomerID.ToString();

                        dataProdukForm displayProdukForm = new dataProdukForm(originModuleID, this, searchParam);
                        displayProdukForm.ShowDialog(this);
                    }
                    break;
            }
        }

        private void captureCtrlModifier(Keys key)
        {
            switch (key)
            {
                case Keys.Delete: // CTRL + DELETE
                    if (detailReturDataGridView.ReadOnly == false)
                    {
                        if (DialogResult.Yes == MessageBox.Show("DELETE CURRENT ROW?", "WARNING", MessageBoxButtons.YesNo))
                        {
                            deleteCurrentRow();
                            calculateTotal();
                        }
                    }
                    break;

                case Keys.Enter: // CTRL + ENTER
                    if (saveButton.Enabled == true)
                        saveButton.PerformClick();
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                int modifier = (int)m.LParam & 0xFFFF;

                if (modifier == Constants.NOMOD)
                    captureAll(key);
                //else if (modifier == Constants.ALT)
                //    captureAltModifier(key);
                else if (modifier == Constants.CTRL)
                    captureCtrlModifier(key);
            }

            base.WndProc(ref m);
        }

        private void registerGlobalHotkey()
        {
            ghk_F1 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F1, this);
            ghk_F1.Register();

            //ghk_F2 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F2, this);
            //ghk_F2.Register();

            ghk_F8 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F8, this);
            ghk_F8.Register();

            ghk_F9 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F9, this);
            ghk_F9.Register();

            ghk_F11 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F11, this);
            ghk_F11.Register();


            ghk_CTRL_DEL = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Delete, this);
            ghk_CTRL_DEL.Register();

            ghk_CTRL_ENTER = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Enter, this);
            ghk_CTRL_ENTER.Register();

        }

        private void unregisterGlobalHotkey()
        {
            ghk_F1.Unregister();
            //ghk_F2.Unregister();
            ghk_F8.Unregister();
            ghk_F9.Unregister();
            ghk_F11.Unregister();

            ghk_CTRL_DEL.Unregister();
            ghk_CTRL_ENTER.Unregister();
        }

        public void addNewRow()
        {
            int newRowIndex = 0;
            bool allowToAdd = true;

            for (int i = 0; i < detailReturDataGridView.Rows.Count && allowToAdd; i++)
            {
                if (null != detailReturDataGridView.Rows[i].Cells["productName"].Value)
                {
                    if (!gutil.isProductIDExist(detailReturDataGridView.Rows[i].Cells["productName"].Value.ToString()))
                    {
                        allowToAdd = false;
                        newRowIndex = i;
                    }
                }
                else
                {
                    allowToAdd = false;
                    newRowIndex = i;
                }
            }

            if (allowToAdd)
            {
                detailReturDataGridView.Rows.Add();
                //returnQty.Add("0");
                newRowIndex = detailReturDataGridView.Rows.Count - 1;
            }
            else
            {
                DataGridViewRow selectedRow = detailReturDataGridView.Rows[newRowIndex];
                clearUpSomeRowContents(selectedRow, newRowIndex);
            }

            detailReturDataGridView.Select();
            detailReturDataGridView.CurrentCell = detailReturDataGridView.Rows[newRowIndex].Cells["productName"];
            detailReturDataGridView.BeginEdit(true);
        }

        public void addNewRowFromBarcode(string productID, string productName, int rowIndex = -1)
        {
            int i = 0;
            bool found = false;
            int rowSelectedIndex = 0;
            bool foundEmptyRow = false;
            int emptyRowIndex = 0;
            double currQty;
            double subTotal;
            double hpp;

            if (detailReturDataGridView.ReadOnly == true)
                return;

            detailReturDataGridView.Focus();

            if (rowIndex >= 0)
            {
                rowSelectedIndex = rowIndex;
            }
            else
            {
                // CHECK FOR EXISTING SELECTED ITEM
                for (i = 0; i < detailReturDataGridView.Rows.Count && !found && !foundEmptyRow; i++)
                {
                    if (null != detailReturDataGridView.Rows[i].Cells["productName"].Value)
                    {
                        if (detailReturDataGridView.Rows[i].Cells["productName"].Value.ToString() == productName)
                        {
                            found = true;
                            rowSelectedIndex = i;
                        }
                    }
                    else
                    {
                        foundEmptyRow = true;
                        emptyRowIndex = i;
                    }
                }

                if (!found)
                {
                    if (foundEmptyRow)
                    {
                        //returnQty[emptyRowIndex] = "0";
                        rowSelectedIndex = emptyRowIndex;
                    }
                    else
                    {
                        detailReturDataGridView.Rows.Add();
                        //returnQty.Add("0");
                        rowSelectedIndex = detailReturDataGridView.Rows.Count - 1;
                    }
                }
            }

            DataGridViewRow selectedRow = detailReturDataGridView.Rows[rowSelectedIndex];
            updateSomeRowContents(selectedRow, rowSelectedIndex, productID, productName);

            if (!found)
            {
                selectedRow.Cells["qty"].Value = 0;
                //returnQty[rowSelectedIndex] = "0";
                currQty = 0;
            }
            else
            {
                //currQty = Convert.ToDouble(returnQty[rowSelectedIndex]) + 1;
                currQty = Convert.ToDouble(selectedRow.Cells["qty"].Value) + 1;

                selectedRow.Cells["qty"].Value = currQty;
                //returnQty[rowSelectedIndex] = currQty.ToString();
            }

            hpp = Convert.ToDouble(selectedRow.Cells["productPrice"].Value);

            subTotal = Math.Round((hpp * currQty), 2);
            selectedRow.Cells["subTotal"].Value = subTotal;

            calculateTotal();

            detailReturDataGridView.Select();
            detailReturDataGridView.CurrentCell = selectedRow.Cells["qty"];
            detailReturDataGridView.BeginEdit(true);
        }

        private void addDataGridColumn()
        {
            DataGridViewTextBoxColumn productIDColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn stockQtyColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn purchaseQtyColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn retailPriceColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn subtotalColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productDisc1Column = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productDisc2Column = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productDiscRPColumn = new DataGridViewTextBoxColumn();

            productIDColumn.HeaderText = "KODE PRODUK";
            productIDColumn.Name = "productID";
            productIDColumn.Width = 200;
            productIDColumn.Visible = false;
            productIDColumn.DefaultCellStyle.BackColor = Color.LightBlue;
            detailReturDataGridView.Columns.Add(productIDColumn);

            productNameColumn.HeaderText = "NAMA PRODUK";
            productNameColumn.Name = "productName";
            productNameColumn.Width = 300;
            productNameColumn.DefaultCellStyle.BackColor = Color.LightBlue;
            detailReturDataGridView.Columns.Add(productNameColumn);

            retailPriceColumn.HeaderText = "SALES PRICE";
            retailPriceColumn.Name = "productPrice";
            retailPriceColumn.Width = 100;
            retailPriceColumn.ReadOnly = true;
            detailReturDataGridView.Columns.Add(retailPriceColumn);

            if (originModuleID == globalConstants.RETUR_PENJUALAN)
            {
                purchaseQtyColumn.HeaderText = "SO QTY";
                purchaseQtyColumn.Name = "SOqty";
                purchaseQtyColumn.Width = 100;
                purchaseQtyColumn.ReadOnly = true;
                detailReturDataGridView.Columns.Add(purchaseQtyColumn);

                productDisc1Column.HeaderText = "DISC 1 (%)";
                productDisc1Column.Name = "disc1";
                productDisc1Column.Width = 100;
                productDisc1Column.ReadOnly = true;
                detailReturDataGridView.Columns.Add(productDisc1Column);

                productDisc2Column.HeaderText = "DISC 2 (%)";
                productDisc2Column.Name = "disc2";
                productDisc2Column.Width = 100;
                productDisc2Column.ReadOnly = true;
                detailReturDataGridView.Columns.Add(productDisc2Column);

                productDiscRPColumn.HeaderText = "DISC RP";
                productDiscRPColumn.Name = "discRP";
                productDiscRPColumn.Width = 100;
                productDiscRPColumn.ReadOnly = true;
                detailReturDataGridView.Columns.Add(productDiscRPColumn);
            }

            stockQtyColumn.HeaderText = "QTY";
            stockQtyColumn.Name = "qty";
            stockQtyColumn.Width = 100;
            stockQtyColumn.DefaultCellStyle.BackColor = Color.LightBlue;
            detailReturDataGridView.Columns.Add(stockQtyColumn);

            subtotalColumn.HeaderText = "SUBTOTAL";
            subtotalColumn.Name = "subtotal";
            subtotalColumn.Width = 100;
            subtotalColumn.ReadOnly = true;
            detailReturDataGridView.Columns.Add(subtotalColumn);

        }

        private bool noReturExist()
        {
            bool result = false;
            string noReturParam = MySqlHelper.EscapeString(noReturTextBox.Text);

            if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM RETURN_SALES_HEADER WHERE RS_INVOICE = '" + noReturParam + "'")) > 0)
                result = true;

            return result;
        }

        private void calculateTotal()
        {
            double total = 0;
            double discTotal = 0;

            for (int i = 0; i < detailReturDataGridView.Rows.Count; i++)
            {
                if (null != detailReturDataGridView.Rows[i].Cells["subtotal"].Value)
                    //total = total + Convert.ToDouble(subtotalList[i]);
                    total = total + Convert.ToDouble(detailReturDataGridView.Rows[i].Cells["subtotal"].Value);
            }

            if (originModuleID == globalConstants.RETUR_PENJUALAN)
            {
                discTotal = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SALES_DISCOUNT_FINAL, 0) FROM SALES_HEADER WHERE SALES_INVOICE = '" + selectedSalesInvoice + "'"));
            }

            globalTotalValue = total - discTotal;
            totalLabel.Text = total.ToString(currencyFormat, culture);//"Rp. " + total.ToString();
        }

        private double getProductPriceValue(string productID)
        {
            double result = 0;
            string sqlCommand = "";
            //DS.mySqlConnect();

            if (originModuleID == globalConstants.RETUR_PENJUALAN)
                sqlCommand = "SELECT PRODUCT_SALES_PRICE FROM SALES_DETAIL WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND PRODUCT_ID = '" + productID + "' AND REV_NO = " + selectedSalesRev;
            else if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
                sqlCommand = "SELECT PRODUCT_RETAIL_PRICE FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "' AND PRODUCT_ACTIVE = 1";

            result = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            return result;
        }

        private double getSOQty(string productID)
        {
            double result = 0;
            double soQTY = 0;
            double returQTY = 0;
            string sqlCommand = "";
            DS.mySqlConnect();

            sqlCommand = "SELECT IFNULL(SUM(PRODUCT_QTY), 0) FROM SALES_DETAIL WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND PRODUCT_ID = '" + productID + "' AND REV_NO = " + selectedSalesRev;
            soQTY = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            sqlCommand = "SELECT IFNULL(SUM(PRODUCT_RETURN_QTY), 0) FROM RETURN_SALES_DETAIL RSD, RETURN_SALES_HEADER RSH WHERE RSD.RS_INVOICE = RSH.RS_INVOICE AND RSH.SALES_INVOICE = '" + selectedSalesInvoice + "' AND RSD.PRODUCT_ID = '" + productID + "'";
            returQTY = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            result = soQTY - returQTY;

            return result;
        }

        private void detailReturDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((detailReturDataGridView.CurrentCell.OwningColumn.Name == "productName") && e.Control is TextBox)
            {
                TextBox productIDTextBox = e.Control as TextBox;

                productIDTextBox.PreviewKeyDown -= TextBox_previewKeyDown;
                productIDTextBox.PreviewKeyDown += TextBox_previewKeyDown;
                productIDTextBox.CharacterCasing = CharacterCasing.Upper;
                productIDTextBox.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

        private void clearUpSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex)
        {
            isLoading = true;
            selectedRow.Cells["productName"].Value = "";

            selectedRow.Cells["productPrice"].Value = "0";
            //productPriceList[rowSelectedIndex] = "0";

            selectedRow.Cells["subTotal"].Value = "0";
            //subtotalList[rowSelectedIndex] = "0";

            selectedRow.Cells["qty"].Value = "0";
            //returnQty[rowSelectedIndex] = "0";

            if (originModuleID == globalConstants.RETUR_PENJUALAN)
            { 
                selectedRow.Cells["SOQty"].Value = "0";
                selectedRow.Cells["disc1"].Value = "0";
                selectedRow.Cells["disc2"].Value = "0";
                selectedRow.Cells["discRP"].Value = "0";
            }

            calculateTotal();
            isLoading = false;
        }

        private void updateSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex, string productID, string productName)
        {
            int numRow = 0;
            string selectedProductID = "";
            string selectedProductName = "";

            double hpp = 0;
            string currentProductID = "";
            string currentProductName = "";
            bool changed = false;
            double soQTY = 0;
            double disc1 = 0;
            double disc2 = 0;
            double discRP = 0;

            numRow = Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));

            if (numRow > 0)
            {
                selectedProductName = productName;

                if (null != selectedRow.Cells["productID"].Value)
                    currentProductID = selectedRow.Cells["productID"].Value.ToString();

                if (null != selectedRow.Cells["productName"].Value)
                    currentProductName = selectedRow.Cells["productName"].Value.ToString();

                selectedProductID = productID;// DS.getDataSingleValue("SELECT IFNULL(PRODUCT_ID,'') FROM MASTER_PRODUCT WHERE PRODUCT_NAME = '" + currentValue + "'").ToString();

                selectedRow.Cells["productId"].Value = selectedProductID;
                selectedRow.Cells["productName"].Value = selectedProductName;

                if (selectedProductID != currentProductID)
                    changed = true;

                if (selectedProductName != currentProductName)
                    changed = true;

                if (!changed)
                    return;

                hpp = getProductPriceValue(selectedProductID);
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "updateSomeRowsContent, PRODUCT_BASE_PRICE [" + hpp + "]");

                selectedRow.Cells["productPrice"].Value = hpp;
                selectedRow.Cells["qty"].Value = 0;
                selectedRow.Cells["subTotal"].Value = 0;

                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "updateSomeRowsContent, attempt to calculate total");

                calculateTotal();

                if (originModuleID == globalConstants.RETUR_PENJUALAN)
                {
                    soQTY = getSOQty(selectedProductID);//Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(PRODUCT_QTY), 0) FROM SALES_DETAIL WHERE SALES_INVOICE = '"+selectedSalesInvoice+"' AND PRODUCT_ID = '"+ selectedProductID+"'"));
                    selectedRow.Cells["SOQty"].Value = soQTY;

                    disc1 = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(PRODUCT_DISC1, 0) FROM SALES_DETAIL WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND PRODUCT_ID = '" + selectedProductID + "' AND REV_NO = " + selectedSalesRev));
                    selectedRow.Cells["disc1"].Value = disc1;

                    disc2 = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(PRODUCT_DISC2, 0) FROM SALES_DETAIL WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND PRODUCT_ID = '" + selectedProductID + "' AND REV_NO = " + selectedSalesRev));
                    selectedRow.Cells["disc2"].Value = disc2;

                    discRP = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(PRODUCT_DISC_RP, 0) FROM SALES_DETAIL WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND PRODUCT_ID = '" + selectedProductID + "' AND REV_NO = " + selectedSalesRev));
                    selectedRow.Cells["discRP"].Value = discRP;
                }
            }
            else
            {
                clearUpSomeRowContents(selectedRow, rowSelectedIndex);
            }
        }

        private void TextBox_previewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string currentValue = "";
            int rowSelectedIndex = 0;
            DataGridViewTextBoxEditingControl dataGridViewComboBoxEditingControl = sender as DataGridViewTextBoxEditingControl;

            if (detailReturDataGridView.CurrentCell.OwningColumn.Name != "productName")
                return;

            if (e.KeyCode == Keys.Enter)
            {
                currentValue = dataGridViewComboBoxEditingControl.Text;
                rowSelectedIndex = detailReturDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = detailReturDataGridView.Rows[rowSelectedIndex];

                if (currentValue.Length > 0)
                {
                    //updateSomeRowContents(selectedRow, rowSelectedIndex, currentValue);
                    //detailReturDataGridView.CurrentCell = selectedRow.Cells["qty"];
                    //POSSearchProductForm browseProduk = new POSSearchProductForm(globalConstants.RETUR_PENJUALAN, this, currentValue, rowSelectedIndex);
                    dataProdukForm browseProduk = new dataProdukForm(originModuleID, this, "", currentValue, rowSelectedIndex, selectedSalesInvoice);
                    browseProduk.ShowDialog(this);
                }
                else
                {
                    //clearUpSomeRowContents(selectedRow, rowSelectedIndex);
                }
            }
        }

        private void noReturTextBox_TextChanged(object sender, EventArgs e)
        {
            if (noReturExist())
            {
                errorLabel.Text = "NO RETUR SUDAH ADA";
                noReturTextBox.Focus();
            }
            else
                errorLabel.Text = "";
        }

        private void detailReturDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (isLoading)
                return;

            detailReturDataGridView.Rows[e.RowIndex].Cells["qty"].Value = "0";
        }
        
        private string getInvoiceTotalValue()
        {
            string result = "";
            double resultValue = 0;
            double invoiceValue = 0;
            double paymentValue = 0;

            // GLOBAL SALES TOTAL VALUE WITHOUT ANY PAYMENT / RETURN
            invoiceValue = Convert.ToDouble(DS.getDataSingleValue("SELECT SALES_TOTAL FROM SALES_HEADER WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND REV_NO = " + selectedSalesRev));

            // TOTAL PAYMENT / RETURN
            paymentValue = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(PAYMENT_NOMINAL), 0) FROM PAYMENT_CREDIT PC, CREDIT C WHERE PC.CREDIT_ID = C.CREDIT_ID AND C.SALES_INVOICE = '" + selectedSalesInvoice + "'"));

            resultValue = invoiceValue - paymentValue;

            result = resultValue.ToString(currencyFormat, culture);

            return result;
        }

        private string getPelangganName()
        {
            string result = "";
            
            if (selectedCustomerID > 0)
                result = DS.getDataSingleValue("SELECT IFNULL(CUSTOMER_FULL_NAME, '') FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = "+selectedCustomerID).ToString();

            return result;
        }

        private void loadDataHeader()
        {
            MySqlDataReader rdr;
            string sqlCommand;
            DateTime salesDate = DateTime.Now;

            sqlCommand = "SELECT SALES_INVOICE, SALES_DATE, CUSTOMER_ID FROM SALES_HEADER WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND REV_NO = " + selectedSalesRev;
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    rdr.Read();

                    invoiceInfoTextBox.Text = selectedSalesInvoice;//rdr.GetString("SALES_INVOICE");
                    salesDate = rdr.GetDateTime("SALES_DATE");
                    invoiceDateTextBox.Text = String.Format(culture, "{0:dd MMM yyyy}", salesDate);
                    selectedCustomerID = rdr.GetInt32("CUSTOMER_ID");
                }
            }
            rdr.Close();
        }

        private void deleteCurrentRow()
        {
            if (detailReturDataGridView.SelectedCells.Count > 0)
            {
                int rowSelectedIndex = detailReturDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = detailReturDataGridView.Rows[rowSelectedIndex];
                detailReturDataGridView.CurrentCell = selectedRow.Cells["productName"];

                //if (null != selectedRow && rowSelectedIndex != detailReturDataGridView.Rows.Count - 1)
                {
                    //for (int i = rowSelectedIndex; i < detailReturDataGridView.Rows.Count - 1; i++)
                    //{
                    //    returnQty[i] = returnQty[i + 1];
                    //    productPriceList[i] = productPriceList[i + 1];
                    //    subtotalList[i] = subtotalList[i + 1];
                    //}

                    isLoading = true;
                    detailReturDataGridView.Rows.Remove(selectedRow);
                    isLoading = false;
                }
            }
        }

        private void calculateSalesCommissionIfAny(string soInvoice)
        {
            //int salesPersonID = 0;
            //string selectedSQInvoice = "";
            //double commissionValue = 0;
            //string sqlCommand = "";
            //MySqlException internalEX = null;
            string ReturDateTime = "";
            DateTime selectedReturDate;

            selectedReturDate = rsDateTimePicker.Value;
            ReturDateTime = String.Format(culture, "{0:dd-MM-yyyy}", selectedReturDate);

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                //// INSERT TO SALES COMMISSION DETAIL
                //// CHECK WHETHER THE SALES INVOICE COMES FROM A SALES QUOTATION
                //selectedSQInvoice = DS.getDataSingleValue("SELECT IFNULL(SQ_INVOICE, '') FROM SALES_HEADER WHERE SALES_INVOICE = '" + soInvoice + "' AND SALES_VOID = 0 AND SALES_ACTIVE = 1").ToString();

                //if (selectedSQInvoice.Length > 0)
                //{
                //    salesPersonID = Convert.ToInt32(DS.getDataSingleValue("SELECT SALESPERSON_ID FROM SALES_QUOTATION_HEADER WHERE SQ_INVOICE = '" + selectedSQInvoice + "'"));
                //    commissionValue = gutil.getSalesCommission(soInvoice, selectedSQInvoice);

                //    sqlCommand = "INSERT INTO SALES_COMMISSION_DETAIL (SALESPERSON_ID, COMMISSION_DATE, COMMISSION_AMOUNT, SALES_INVOICE) VALUES " +
                //                            "(" + salesPersonID + ", STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y %H:%i'), " + commissionValue + ", " + soInvoice + ")";

                //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES COMMISSION DETAIL [" + soInvoice + "/" + salesPersonID + "/ " + commissionValue + "]");
                //    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                //        throw internalEX;
                //}
            }
            catch(Exception ex)
            {
                gutil.saveSystemDebugLog(originModuleID, "EXCEPTION THROWN [" + ex.Message + "]");
                try
                {
                    DS.rollBack();
                }
                catch (MySqlException e)
                {
                    if (DS.getMyTransConnection() != null)
                    {
                        gutil.showDBOPError(e, "ROLLBACK");
                    }
                }

                gutil.showDBOPError(ex, "INSERT");
            }
            finally
            {
                DS.mySqlClose();
            }
        }

        private bool dataValidated()
        {
            bool dataExist = true;
            bool validInput = true;
            double SOQty = 0;

            int i = 0;
            if (noReturTextBox.Text.Length <= 0)
            {
                errorLabel.Text = "NO RETUR TIDAK BOLEH KOSONG";
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "dataValidated [NO RETUR TIDAK BOLEH KOSONG]");
                return false;   
            }

            if (globalTotalValue <= 0)
            {
                errorLabel.Text = "NILAI RETUR 0";
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "dataValidated [NILAI RETUR 0]");
                return false;   
            }

            if (detailReturDataGridView.Rows.Count <= 0)
            {
                errorLabel.Text = "TIDAK ADA BARANG YANG DIRETUR";
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "dataValidated [TIDAK ADA BARANG YANG DIRETUR]");
                return false;
            }

            for (i = 0; i < detailReturDataGridView.Rows.Count && dataExist; i++)
            {
                // CHECK PRODUCT NAME
                if (null != detailReturDataGridView.Rows[i].Cells["productName"].Value)
                    dataExist = gutil.isProductNameExist(detailReturDataGridView.Rows[i].Cells["productName"].Value.ToString());
                else
                    dataExist = false;

                if (!dataExist)
                {
                    errorLabel.Text = "NAMA PRODUK PADA BARIS [" + (i+1) + "] INVALID";
                    continue;
                }

                // CHECK QTY
                if (null != detailReturDataGridView.Rows[i].Cells["qty"].Value)
                    validInput = gutil.matchRegEx(detailReturDataGridView.Rows[i].Cells["qty"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                else
                    validInput = false;

                if (!validInput)
                {
                    errorLabel.Text = "INPUT QTY PADA BARIS [" + (i + 1) + "] TIDAK VALID";
                    continue;
                }

                if (originModuleID == globalConstants.RETUR_PENJUALAN)
                {
                    if (null != detailReturDataGridView.Rows[i].Cells["SOqty"].Value)
                        SOQty = Convert.ToDouble(detailReturDataGridView.Rows[i].Cells["SOqty"].Value);

                    if (SOQty >= Convert.ToDouble(detailReturDataGridView.Rows[i].Cells["qty"].Value))
                        validInput = true;
                    else
                    { 
                        validInput = false;
                        errorLabel.Text = "INPUT QTY PADA BARIS [" + (i + 1) + "] LEBIH BESAR DARI INVOICE";
                        continue;
                    }
                }
            }

            if (!dataExist || !validInput)
            {
                return false;
            }

            return true;
        }

        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";

            int customerID = 0;
            string ReturDateTime = "";
            DateTime selectedReturDate;
            double returTotal = 0;
            double hppValue;
            double qtyValue;
            double soQty = 0;
            string descriptionValue;
            MySqlException internalEX = null;
            double totalCredit = 0;

            double returNominal = 0;
            double actualReturAmount = 0;
            double outstandingCreditAmount = 0;
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            int rowCounter;
            int currentCreditID;
            string currentSalesInvoice;
            bool fullyPaid = false;

            
            int selectedCreditID;

            // RE-GENERATE NO RETUR IN CASE THE ID CHANGED
            noReturTextBox.Text = gutil.getAutoGenerateID("RETURN_SALES_HEADER", "RS", "-", "RS_INVOICE");

            returID = MySqlHelper.EscapeString(noReturTextBox.Text);
            customerID = selectedCustomerID;

            selectedReturDate = rsDateTimePicker.Value;
            ReturDateTime = String.Format(culture, "{0:dd-MM-yyyy}", selectedReturDate);

            returTotal = globalTotalValue;
            returNominal = returTotal;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                // SAVE HEADER TABLE
                if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
                    sqlCommand = "INSERT INTO RETURN_SALES_HEADER (RS_INVOICE, CUSTOMER_ID, RS_DATETIME, RS_TOTAL) VALUES " +
                                        "('" + returID + "', " + selectedCustomerID +", STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), " + gutil.validateDecimalNumericInput(returTotal) + ")";
                else
                    sqlCommand = "INSERT INTO RETURN_SALES_HEADER (RS_INVOICE, SALES_INVOICE, CUSTOMER_ID, RS_DATETIME, RS_TOTAL) VALUES " +
                                    "('" + returID + "', '" + selectedSalesInvoice + "', " + selectedCustomerID + ", STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), " + gutil.validateDecimalNumericInput(returTotal) + ")";

                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "INSERT INTO RETURN SALES HEADER [" + returID + "]");

                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                // SAVE DETAIL TABLE
                for (int i = 0; i < detailReturDataGridView.Rows.Count; i++)
                {
                    if (null == detailReturDataGridView.Rows[i].Cells["productID"].Value || !gutil.isProductIDExist(detailReturDataGridView.Rows[i].Cells["productID"].Value.ToString()))
                        continue;

                    //hppValue = Convert.ToDouble(productPriceList[i]);
                    //qtyValue = Convert.ToDouble(returnQty[i]);

                    hppValue = Convert.ToDouble(detailReturDataGridView.Rows[i].Cells["productPrice"].Value);
                    qtyValue = Convert.ToDouble(detailReturDataGridView.Rows[i].Cells["qty"].Value);

                    if (originModuleID == globalConstants.RETUR_PENJUALAN)
                        soQty = Convert.ToDouble(detailReturDataGridView.Rows[i].Cells["SOqty"].Value);

                    try
                    {
                        descriptionValue = detailReturDataGridView.Rows[i].Cells["description"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        descriptionValue = " ";
                    }

                    sqlCommand = "INSERT INTO RETURN_SALES_DETAIL (RS_INVOICE, PRODUCT_ID, PRODUCT_SALES_PRICE, PRODUCT_SALES_QTY, PRODUCT_RETURN_QTY, RS_DESCRIPTION, RS_SUBTOTAL) VALUES " +
                                        "('" + returID + "', '" + detailReturDataGridView.Rows[i].Cells["productID"].Value.ToString() + "', " + hppValue + ", " + soQty + ", " + qtyValue + ", '" + descriptionValue + "', " + gutil.validateDecimalNumericInput(Convert.ToDouble(detailReturDataGridView.Rows[i].Cells["subTotal"].Value)) + ")";

                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "INSERT INTO RETURN SALES DETAIL [" + detailReturDataGridView.Rows[i].Cells["productID"].Value.ToString() + ", " + hppValue + ", " + soQty + ", " + qtyValue + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // UPDATE PRODUCT LOCATION
                    sqlCommand = "UPDATE PRODUCT_LOCATION SET PRODUCT_LOCATION_QTY = PRODUCT_LOCATION_QTY + " + qtyValue + " WHERE PRODUCT_ID = '" + detailReturDataGridView.Rows[i].Cells["productID"].Value.ToString() + "' AND LOCATION_ID = " + locationID;

                    gutil.saveSystemDebugLog(originModuleID, "UPDATE PRODUCT_LOCATION QTY [" + detailReturDataGridView.Rows[i].Cells["productID"].Value.ToString() + "/" + locationID + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // UPDATE MASTER PRODUCT
                    sqlCommand = "UPDATE MASTER_PRODUCT SET PRODUCT_STOCK_QTY = PRODUCT_STOCK_QTY + " + qtyValue + " WHERE PRODUCT_ID = '" + detailReturDataGridView.Rows[i].Cells["productID"].Value.ToString() + "'";

                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "UPDATE MASTER PRODUCT QTY ["+ detailReturDataGridView.Rows[i].Cells["productID"].Value.ToString() + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                extraAmount = 0;
                // IF THERE'S ANY CREDIT LEFT FOR THAT PARTICULAR INVOICE
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "CHECK FOR ANY OUTSTANDING AMOUNT FOR THE INVOICE");
                if (originModuleID == globalConstants.RETUR_PENJUALAN)
                {
                    totalCredit = getTotalCredit();
                    selectedCreditID = getCreditID();

                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "selectedCreditID [" + selectedCreditID + "]");
                    if (selectedCreditID > 0)
                    {
                        if (totalCredit >= globalTotalValue)
                        {
                            // RETUR VALUE LESS THAN OR EQUAL TOTAL CREDIT
                            // add retur as cash payment with description retur no
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "RETUR VALUE LESS THAN OR EQUAL TOTAL CREDIT");
                            sqlCommand = "INSERT INTO PAYMENT_CREDIT (CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_DUE_DATE, PAYMENT_CONFIRMED_DATE) VALUES " +
                                                "(" + selectedCreditID + ", STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), 1, " + gutil.validateDecimalNumericInput(globalTotalValue) + ", 'RETUR [" + returID + "]', 1, STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'))";

                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "INSERT INTO PAYMENT CREDIT [" + selectedCreditID + ", " + gutil.validateDecimalNumericInput(globalTotalValue) + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }
                        else
                        {
                            // RETUR VALUE BIGGER THAN TOTAL CREDIT
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "RETUR VALUE BIGGER THAN TOTAL CREDIT");
                            // return the extra amount as cash
                            extraAmount = globalTotalValue - totalCredit;
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "extraAmount [" + extraAmount + "]");
                            sqlCommand = "INSERT INTO PAYMENT_CREDIT (CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_DUE_DATE, PAYMENT_CONFIRMED_DATE) VALUES " +
                                                "(" + selectedCreditID + ", STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), 1, " + gutil.validateDecimalNumericInput(totalCredit) + ", 'RETUR [" + noReturTextBox.Text + "]', 1, STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'))";

                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "INSERT INTO PAYMENT CREDIT [" + selectedCreditID + ", " + gutil.validateDecimalNumericInput(totalCredit) + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }

                        if (totalCredit <= globalTotalValue)
                        {
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "RETUR VALUE BIGGER THAN TOTAL CREDIT VALUE, MEANS FULLY PAID");
                            
                            // UPDATE SALES HEADER TABLE
                            sqlCommand = "UPDATE SALES_HEADER SET SALES_PAID = 1 WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND REV_NO = " + selectedSalesRev;
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "UPDATE SALES HEADER [" + selectedSalesInvoice + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;

                            // UPDATE SALES HEADER TAX TABLE
                            sqlCommand = "UPDATE SALES_HEADER_TAX SET SALES_PAID = 1 WHERE ORIGIN_SALES_INVOICE = '" + selectedSalesInvoice + "'";
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "UPDATE SALES HEADER TAX [" + selectedSalesInvoice + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;

                            // UPDATE CREDIT TABLE
                            sqlCommand = "UPDATE CREDIT SET CREDIT_PAID = 1 WHERE CREDIT_ID = " + selectedCreditID;
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "UPDATE CREDIT [" + selectedCreditID + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }
                    }
                    else
                        extraAmount = globalTotalValue;
                }
                else if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "GET LIST OF OUTSTANDING CREDIT AND PAY FROM THE OLDEST CREDIT");
                    if (returnCash)
                        extraAmount = globalTotalValue;
                    else
                    {
                        // GET A LIST OF OUTSTANDING SALES CREDIT
                        sqlCommand = "SELECT C.SALES_INVOICE, C.CREDIT_ID, (CREDIT_NOMINAL - IFNULL(PC.PAYMENT, 0)) AS 'SISA PIUTANG' " +
                                            "FROM SALES_HEADER S, CREDIT C LEFT OUTER JOIN (SELECT CREDIT_ID, SUM(PAYMENT_NOMINAL) AS PAYMENT FROM PAYMENT_CREDIT GROUP BY CREDIT_ID) PC ON PC.CREDIT_ID = C.CREDIT_ID  " +
                                            "WHERE S.CUSTOMER_ID = " + selectedCustomerID + " AND C.CREDIT_PAID = 0 AND S.REV_NO = " + selectedSalesRev + " " +
                                            "AND C.SALES_INVOICE = S.SALES_INVOICE ORDER BY C.CREDIT_ID ASC";

                        using (rdr = DS.getData(sqlCommand))
                        {
                            if (rdr.HasRows)
                                dt.Load(rdr);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "AMOUNT OF RETUR ["+ returNominal + "] NUM OF OUTSTANDING CREDIT [" + dt.Rows.Count + "]");
                            rowCounter = 0;
                            while (returNominal > 0 && rowCounter < dt.Rows.Count)
                            {
                                fullyPaid = false;

                                currentSalesInvoice = dt.Rows[rowCounter]["SALES_INVOICE"].ToString();
                                currentCreditID = Convert.ToInt32(dt.Rows[rowCounter]["CREDIT_ID"].ToString());
                                outstandingCreditAmount = Convert.ToDouble(dt.Rows[rowCounter]["SISA PIUTANG"].ToString());

                                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "currentCreditID [" + currentCreditID + "] outstandingCreditAmount [" + outstandingCreditAmount + "]");

                                if (outstandingCreditAmount <= returNominal && outstandingCreditAmount > 0)
                                {
                                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "currentCreditID [" + currentCreditID + "] FULLY PAID");
                                    actualReturAmount = outstandingCreditAmount;
                                    fullyPaid = true;
                                }
                                else
                                {
                                    actualReturAmount = returNominal;
                                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "currentCreditID [" + currentCreditID + "] NOT FULLY PAID [" + actualReturAmount + "]");
                                }

                                sqlCommand = "INSERT INTO PAYMENT_CREDIT (CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_DUE_DATE, PAYMENT_CONFIRMED_DATE) VALUES " +
                                                    "(" + currentCreditID + ", STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), 1, " + gutil.validateDecimalNumericInput(actualReturAmount) + ", 'RETUR [" + returID + "]', 1, STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'), STR_TO_DATE('" + ReturDateTime + "', '%d-%m-%Y'))";

                                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "INSERT TO PAYMENT CREDIT [" + currentCreditID + ", " + gutil.validateDecimalNumericInput(actualReturAmount) + "]");
                                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                    throw internalEX;

                                if (fullyPaid)
                                {
                                    // UPDATE CREDIT TABLE
                                    sqlCommand = "UPDATE CREDIT SET CREDIT_PAID = 1 WHERE CREDIT_ID = " + currentCreditID;
                                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "UPDATE CREDIT [" + currentCreditID + "] TO FULLY PAID");
                                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                        throw internalEX;

                                    // UPDATE SALES HEADER TABLE
                                    sqlCommand = "UPDATE SALES_HEADER SET SALES_PAID = 1 WHERE SALES_INVOICE = " + currentSalesInvoice + " AND REV_NO = " + selectedSalesRev;
                                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "UPDATE SALES_HEADER [" + currentSalesInvoice + "] TO FULLY PAID");
                                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                        throw internalEX;

                                    // UPDATE SALES HEADER TAX TABLE
                                    sqlCommand = "UPDATE SALES_HEADER_TAX SET SALES_PAID = 1 WHERE ORIGIN_SALES_INVOICE = " + currentSalesInvoice;
                                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "UPDATE SALES_HEADER_TAX [" + currentSalesInvoice + "] TO FULLY PAID");
                                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                        throw internalEX;

                                }

                                returNominal = returNominal - actualReturAmount;
                                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "returNominal [" + returNominal + "]");
                                rowCounter += 1;
                                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "rowCounter [" + rowCounter + "]");
                            }
                            extraAmount = returNominal;
                        }
                        else
                            extraAmount = globalTotalValue;
                    }
                }

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "EXCEPTION THROWN [" + e.Message + "]");
                try
                {
                    DS.rollBack();
                }
                catch (MySqlException ex)
                {
                    if (DS.getMyTransConnection() != null)
                    {
                        gutil.showDBOPError(ex, "ROLLBACK");
                    }
                }

                gutil.showDBOPError(e, "INSERT");
                result = false;
            }
            finally
            {
                DS.mySqlClose();
            }

            return result;
        }

        private bool saveData()
        {
            bool result = false;
            if (dataValidated())
            {
                smallPleaseWait pleaseWait = new smallPleaseWait();
                pleaseWait.Show();

                //  ALlow main UI thread to properly display please wait form.
                Application.DoEvents();
                result = saveDataTransaction();

                pleaseWait.Close();

                return result;
            }

            return result;
        }

        private double getTotalCredit()
        {
            double totalPayment = 0;
            double totalSales = 0;
            double totalCredit = 0;
            string sqlCommand = "";

            // get Total sales for that particular Invoice
            sqlCommand = "SELECT IFNULL(SALES_TOTAL, 0) FROM SALES_HEADER WHERE SALES_INVOICE = '" + selectedSalesInvoice + "' AND REV_NO = " + selectedSalesRev;
            totalSales = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            // get Total Payment for that invoice 
            sqlCommand = "SELECT IFNULL(SUM(PAYMENT_NOMINAL), 0) FROM PAYMENT_CREDIT, CREDIT WHERE CREDIT.SALES_INVOICE = '" + selectedSalesInvoice + "' AND PAYMENT_CREDIT.CREDIT_ID = CREDIT.CREDIT_ID";
            totalPayment = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            totalCredit = totalSales - totalPayment;

            return totalCredit;
        }

        private int getCreditID()
        {
            int result = 0;

            result = Convert.ToInt32(DS.getDataSingleValue("SELECT IFNULL(CREDIT_ID, 0) FROM CREDIT WHERE SALES_INVOICE = '" + selectedSalesInvoice + "'"));

            return result;
        }

        private void loadInfoToko(int opt, out string namatoko, out string almt, out string telepon, out string email)
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            namatoko = ""; almt = ""; telepon = ""; email = "";
            //DS.mySqlConnect();
            //1 load default 2 setting user
            using (rdr = DS.getData("SELECT IFNULL(BRANCH_ID,0) AS 'BRANCH_ID', IFNULL(HQ_IP4,'') AS 'IP', IFNULL(STORE_NAME,'') AS 'NAME', IFNULL(STORE_ADDRESS,'') AS 'ADDRESS', IFNULL(STORE_PHONE,'') AS 'PHONE', IFNULL(STORE_EMAIL,'') AS 'EMAIL' FROM SYS_CONFIG WHERE ID =  " + opt))
            {
                if (rdr.HasRows)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "loadInfoToko");
                    while (rdr.Read())
                    {
                        if (!String.IsNullOrEmpty(rdr.GetString("NAME")))
                        {
                            namatoko = rdr.GetString("NAME");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("ADDRESS")))
                        {
                            almt = rdr.GetString("ADDRESS");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("PHONE")))
                        {
                            telepon = rdr.GetString("PHONE");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("EMAIL")))
                        {
                            email = rdr.GetString("EMAIL");
                        }
                    }
                }
            }
        }

        private void loadNamaUser(int user_id, out string nama)
        {
            nama = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            //DS.mySqlConnect();
            //1 load default 2 setting user
            using (rdr = DS.getData("SELECT USER_NAME AS 'NAME' FROM MASTER_USER WHERE ID=" + user_id))
            {
                if (rdr.HasRows)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "CASHIER FORM : loadNamaUser");

                    rdr.Read();
                    nama = rdr.GetString("NAME");
                }
            }
        }

        private int calculatePageLength()
        {
            string nm, almt, tlpn, email;
            //event printing

            int startY = 0;
            int Offset = 5;
            int offset_plus = 3;
            Font font = new Font("Courier New", 9);
            int rowheight = (int)Math.Ceiling(font.GetHeight());
            int add_offset = rowheight;
            int totalLengthPage = startY + Offset;
            string sqlCommand = "";

            String ucapan = "";

            //event printing

            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "printDocument1_PrintPage, print POS size receipt");
                   
            string customer = "";
            string tgl = "";
            string group = "";
            double total = 0;
            string paymentDesc = "";
            double totalPayment = 0;
            string soInvoice = "";

            //HEADER

            loadInfoToko(2, out nm, out almt, out tlpn, out email);

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            if (!email.Equals(""))
            {
                Offset = Offset + 10;
            }

            Offset = Offset + 13;
            //end of header


            Offset = Offset + 12;

            //2. CUSTOMER NAME
            Offset = Offset + 12;

            Offset = Offset + 13;

            Offset = Offset + 12;

            Offset = Offset + 15 + offset_plus;

            Offset = Offset + 15 + offset_plus;

            Offset = Offset + 12;

            Offset = Offset + 12;

            Offset = Offset + 12;

            Offset = Offset + 13;

            MySqlDataReader rdr;
            string product_desc = "";
            //sqlCommand = "SELECT DATE_FORMAT(PC.PAYMENT_DATE, '%d-%M-%Y') AS 'PAYMENT_DATE', PC.PAYMENT_NOMINAL, PC.PAYMENT_DESCRIPTION FROM PAYMENT_CREDIT PC, CREDIT C WHERE PC.PAYMENT_CONFIRMED = 1 AND PC.CREDIT_ID = C.CREDIT_ID AND C.SALES_INVOICE = '" + selectedSOInvoice + "'";
            sqlCommand = "SELECT RD.PRODUCT_ID, MP.PRODUCT_NAME, RD.PRODUCT_SALES_PRICE, RD.PRODUCT_RETURN_QTY, RD.RS_DESCRIPTION, RD.RS_SUBTOTAL " +
                                    "FROM RETURN_SALES_DETAIL RD, MASTER_PRODUCT MP " +
                                    "WHERE RD.RS_INVOICE = '" + returID + "' AND RD.PRODUCT_ID = MP.PRODUCT_ID";
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        product_desc = rdr.GetString("RS_DESCRIPTION");
                        Offset = Offset + 15;
                        if (product_desc.Length > 0)
                        {
                            Offset = Offset + 15;
                        }
                    }
                }
            }

            Offset = Offset + 13;

            Offset = Offset + 15;

            Offset = Offset + 25 + offset_plus;
            //eNd of content

            //FOOTER

            Offset = Offset + 13;

            Offset = Offset + 15;

            Offset = Offset + 15;

            Offset = Offset + 15;
            //end of footer

            totalLengthPage = totalLengthPage + Offset + 15;

            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "calculatePageLength, totalLengthPage [" + totalLengthPage + "]");
            return totalLengthPage;
        }

        private void printReceipt()
        {
            string sqlCommandx;
            int papermode = comboBox1.SelectedIndex + 1;//gutil.getPaper();
            gutil.setPaper(papermode);
            //sqlCommandx = "SELECT SH.SALES_INVOICE, SH.SALES_TOTAL, SH.SALES_DISCOUNT_FINAL, SH.SALES_TOP, SH.SALES_TOP_DATE, PC.PAYMENT_DATE, PC.PAYMENT_CONFIRMED, PC.PAYMENT_CONFIRMED_DATE, PC.PAYMENT_NOMINAL, IF(PC.PAYMENT_CONFIRMED = 1, PC.PAYMENT_NOMINAL, 0) AS ACTUAL_PAYMENT " +
            //                                  "FROM SALES_HEADER SH, CREDIT C, PAYMENT_CREDIT PC " +
            //                                  "WHERE C.SALES_INVOICE = SH.SALES_INVOICE AND PC.CREDIT_ID = C.CREDIT_ID AND SH.SALES_INVOICE = '" + invoiceNoTextBox.Text + "'";

            sqlCommandx = "SELECT RH.SALES_INVOICE, MP.PRODUCT_NAME, RD.PRODUCT_SALES_PRICE, RD.PRODUCT_RETURN_QTY, RD.RS_DESCRIPTION, RD.RS_SUBTOTAL " +
                                     "FROM MASTER_PRODUCT MP, RETURN_SALES_DETAIL RD, RETURN_SALES_HEADER RH, " +
                                     "(SELECT MAX(RS_INVOICE) AS INVOICE " +
                                     "FROM RETURN_SALES_HEADER " +
                                     "GROUP BY SALES_INVOICE) TAB1 " +
                                     "WHERE RD.PRODUCT_ID = MP.PRODUCT_ID AND RH.RS_INVOICE = TAB1.INVOICE AND RD.RS_INVOICE = RH.RS_INVOICE AND RH.SALES_INVOICE = '" + invoiceInfoTextBox.Text + "'";

            DS.writeXML(sqlCommandx, globalConstants.returPenjualanXML);
            dataReturPenjualanPrintOutForm displayForm = new dataReturPenjualanPrintOutForm();
            displayForm.ShowDialog(this);


            //int paperLength;

            //paperLength = calculatePageLength();
            //PaperSize psize = new PaperSize("Custom", 320, paperLength);//820);
            //printDocument1.DefaultPageSettings.PaperSize = psize;
            //DialogResult result;
            //printPreviewDialog1.Width = 512;
            //printPreviewDialog1.Height = 768;
            //result = printPreviewDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    printDocument1.Print();
            //}
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int salesPaidStatus;

            if (DialogResult.No == MessageBox.Show("SAVE DATA", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                return;

            if (originModuleID == globalConstants.RETUR_PENJUALAN)
            {
                salesPaidStatus = Convert.ToInt32(DS.getDataSingleValue("SELECT IFNULL(SALES_PAID, 0) FROM SALES_HEADER WHERE SALES_INVOICE = '"+ selectedSalesInvoice + "' AND REV_NO = " + selectedSalesRev));
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "SALES PAID STATUS [" + salesPaidStatus + "]");
                if (salesPaidStatus == 1)
                {
                    MessageBox.Show("INVOICE SUDAH TERBAYAR, RETUR BERUPA UANG CASH", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    returnCash = true;
                }
            }

            if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
            {
                if (selectedCustomerID != 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("RETUR BERUPA UANG CASH?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        returnCash = true;
                }
                else
                    returnCash = true;

                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN_STOK, "RETURN CASH [" + returnCash.ToString() + "]");
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "ATTEMPT TO SAVE DATA");
            if (saveData())
            {
                if (extraAmount > 0)
                {
                    MessageBox.Show("JUMLAH YANG DIKEMBALIKAN SEBESAR " + extraAmount.ToString("C", culture));
                    gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "extraAmount [" + extraAmount + "]");
                }

                //salesPaidStatus = Convert.ToInt32(DS.getDataSingleValue("SELECT IFNULL(SALES_PAID, 0) FROM SALES_HEADER WHERE SALES_INVOICE = '" + selectedSalesInvoice + "'"));
                //if (salesPaidStatus == 1)
                //    calculateSalesCommissionIfAny(selectedSalesInvoice);

                gutil.saveUserChangeLog(globalConstants.MENU_RETUR_PENJUALAN, globalConstants.CHANGE_LOG_INSERT, "RETUR PENJUALAN [" + noReturTextBox.Text + "]");
                errorLabel.Text = "";

                if (originModuleID != globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT &&  DialogResult.Yes == MessageBox.Show("PRINT RECEIPT ?", "WARNING", MessageBoxButtons.YesNo))
                {
                    printReceipt();
                }

                gutil.showSuccess(gutil.INS);
                saveButton.Enabled = false;
                detailReturDataGridView.ReadOnly = true;
                noReturTextBox.Enabled = false;
                rsDateTimePicker.Enabled = false;
            }
            else
            {
                MessageBox.Show("FAIL TO SAVE");
                gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "FAILED TO SAVE");
            }
        }
        
        private void dataReturPenjualanForm_Load(object sender, EventArgs e)
        {
            locationID = gutil.loadlocationID(2);
            if (locationID <= 0)
            {
                MessageBox.Show("LOCATION ID BELUM DI SET");
                this.Close();
            }

            errorLabel.Text = "";
            rsDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

            isLoading = true;
            if (originModuleID == globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT)
                invoiceInfoTextBox.Text = getPelangganName();
            else
            {
                invoiceTotalLabelValue.Text = getInvoiceTotalValue();
                loadDataHeader();
            }

            addDataGridColumn();

            isLoading = false;

            detailReturDataGridView.EditingControlShowing += detailReturDataGridView_EditingControlShowing;
            gutil.reArrangeTabOrder(this);

            // AUTO GENERATE NO RETUR
            noReturTextBox.Text = gutil.getAutoGenerateID("RETURN_SALES_HEADER", "RS", "-", "RS_INVOICE");
        }

        private void detailReturDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            String ucapan = "";
            string nm, almt, tlpn, email;

            //event printing

            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "printDocument1_PrintPage, print POS size receipt");

            Graphics graphics = e.Graphics;
            int startX = 5;
            int colxwidth = 93; //31x3
            int totrowwidth = 310; //310/10=31

            int startY = 0;
            int Offset = 5;
            int offset_plus = 3;

            Font font = new Font("Courier New", 9);
            float fontHeight = font.GetHeight();
            int rowheight = (int)Math.Ceiling(font.GetHeight());
            int add_offset = rowheight;
            int totalLengthPage = startY + Offset;

            string sqlCommand = "";
            string customer = "";
            string tgl = "";
            string group = "";
            double total = 0;
            string soInvoice = "";

            //HEADER

            //set allignemnt
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            //set whole printing area
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(startX, startY + Offset, totrowwidth, rowheight);
            //set right print area
            System.Drawing.RectangleF rectright = new System.Drawing.RectangleF(totrowwidth - colxwidth - startX, startY + Offset, colxwidth, rowheight);
            //set middle print area
            System.Drawing.RectangleF rectcenter = new System.Drawing.RectangleF((startX + (totrowwidth / 2) - colxwidth - startX), startY + Offset, (totrowwidth / 2) - startX, rowheight);

            loadInfoToko(2, out nm, out almt, out tlpn, out email);

            graphics.DrawString(nm, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(almt,
                     new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(tlpn,
                     new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            if (!email.Equals(""))
            {
                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                graphics.DrawString(email,
                         new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);
            }

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            String underLine = "-------------------------------------";  //37 character
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);
            //end of header

            //start of content
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            DS.mySqlConnect();
            //load customer id
            sqlCommand = "SELECT RS.RS_INVOICE, IFNULL(RS.SALES_INVOICE, '') AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(RS.RS_DATETIME, '%d-%M-%Y') AS 'DATE',RS.RS_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM RETURN_SALES_HEADER RS,MASTER_CUSTOMER C WHERE RS.CUSTOMER_ID = C.CUSTOMER_ID AND RS.RS_INVOICE = '" + returID + "'" +
                " UNION " +
                "SELECT RS.RS_INVOICE, IFNULL(RS.SALES_INVOICE, ' ') AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(RS.RS_DATETIME, '%d-%M-%Y') AS 'DATE', RS.RS_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM RETURN_SALES_HEADER RS WHERE RS.CUSTOMER_ID = 0 AND RS.RS_INVOICE = '" + returID+ "'" +
                "ORDER BY DATE ASC";
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    customer = rdr.GetString("CUSTOMER");
                    tgl = rdr.GetString("DATE");
                    total = rdr.GetDouble("TOTAL");
                    group = rdr.GetString("GROUP");
                    soInvoice = rdr.GetString("INVOICE");
                }
            }
            DS.mySqlClose();

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            Offset = Offset + add_offset;
            rect.Width = 280;
            //SET TO LEFT MARGIN
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "RETUR PENJUALAN   ";
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            //2. CUSTOMER NAME
            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "PELANGGAN : " + customer + " [" + group + "]";
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.Width = totrowwidth;
            ucapan = "BUKTI RETUR     ";
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset + offset_plus;
            rect.Y = startY + Offset;
            rect.X = startX + 15;
            rect.Width = 280;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "NO. RETUR " + returID;
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset + offset_plus;
            rect.Y = startY + Offset;
            rect.X = startX + add_offset;
            rect.Width = 280;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "NO. INVOICE " + soInvoice;
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TOTAL    : " + total.ToString("C2", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TANGGAL  : " + tgl;
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            string nama = "";
            loadNamaUser(gutil.getUserID(), out nama);
            ucapan = "OPERATOR : " + nama;
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            // JUMLAH TOTAL INVOICE
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;

            // DISPLAY DETAIL FOR RETUR
            string product_id = "";
            string product_name = "";
            double product_qty = 0;
            double product_price = 0;
            string product_desc = "";
            double total_qty= 0;

            //sqlCommand = "SELECT DATE_FORMAT(PC.PAYMENT_DATE, '%d-%M-%Y') AS 'PAYMENT_DATE', PC.PAYMENT_NOMINAL, PC.PAYMENT_DESCRIPTION FROM PAYMENT_CREDIT PC, CREDIT C WHERE PC.PAYMENT_CONFIRMED = 1 AND PC.CREDIT_ID = C.CREDIT_ID AND C.SALES_INVOICE = '" + selectedSOInvoice + "'";
            sqlCommand = "SELECT RD.PRODUCT_ID, MP.PRODUCT_NAME, RD.PRODUCT_SALES_PRICE, RD.PRODUCT_RETURN_QTY, RD.RS_DESCRIPTION, RD.RS_SUBTOTAL " +
                                    "FROM RETURN_SALES_DETAIL RD, MASTER_PRODUCT MP " +
                                    "WHERE RD.RS_INVOICE = '" + returID + "' AND RD.PRODUCT_ID = MP.PRODUCT_ID";
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        product_id = rdr.GetString("PRODUCT_ID");
                        product_name = rdr.GetString("PRODUCT_NAME");
                        product_qty = rdr.GetDouble("PRODUCT_RETURN_QTY");
                        product_price = rdr.GetDouble("PRODUCT_SALES_PRICE");
                        product_desc = rdr.GetString("RS_DESCRIPTION");
                        Offset = Offset + add_offset;
                        rect.Y = startY + Offset;
                        rect.X = startX + 15;
                        rect.Width = 280;
                        sf.LineAlignment = StringAlignment.Near;
                        sf.Alignment = StringAlignment.Near;
                        ucapan = product_qty + " X [" + product_id + "] " + product_name;
                        if (ucapan.Length > 30)
                        {
                            ucapan = ucapan.Substring(0, 30); //maximum 30 character
                        }
                        //
                        graphics.DrawString(ucapan, new Font("Courier New", 7),
                                 new SolidBrush(Color.Black), rect, sf);

                        rectright.Y = Offset - startY;
                        sf.LineAlignment = StringAlignment.Far;
                        sf.Alignment = StringAlignment.Far;
                        ucapan = "@ " + product_price.ToString("C2", culture);//" Rp." + product_price;
                        graphics.DrawString(ucapan, new Font("Courier New", 7),
                                 new SolidBrush(Color.Black), rectright, sf);

                        if (product_desc.Length>0)
                        {
                            Offset = Offset + add_offset;
                            rect.Y = startY + Offset;
                            rect.X = startX + add_offset;
                            rect.Width = 280;
                            sf.LineAlignment = StringAlignment.Near;
                            sf.Alignment = StringAlignment.Near;
                            ucapan = product_desc;
                            if (ucapan.Length > 30)
                            {
                                ucapan = ucapan.Substring(0, 30); //maximum 30 character
                            }
                            //
                            graphics.DrawString(ucapan, new Font("Courier New", 7),
                                     new SolidBrush(Color.Black), rect, sf);
                        }
                    }
                }
            }


            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX + add_offset;
            rect.Width = 260;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "               JUMLAH  :";
            rectcenter.Y = rect.Y;
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rectcenter, sf);
            sf.LineAlignment = StringAlignment.Far;
            sf.Alignment = StringAlignment.Far;
            ucapan = total.ToString("C2", culture);
            rectright.Y = Offset - startY + 1;
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rectright, sf);

            total_qty = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(PRODUCT_RETURN_QTY), 0) FROM RETURN_SALES_DETAIL RD, MASTER_PRODUCT MP WHERE RD.PRODUCT_ID = MP.PRODUCT_ID AND RD.RS_INVOICE = '" + returID+ "'"));

            Offset = Offset + add_offset + offset_plus;
            rect.Y = startY + Offset;
            rect.X = startX + add_offset;
            rect.Width = 280;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "TOTAL BARANG : " + total_qty;
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);
            //eNd of content

            //FOOTER

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TERIMA KASIH ATAS KUNJUNGAN ANDA";
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "MAAF BARANG YANG SUDAH DIBELI";
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TIDAK DAPAT DITUKAR/ DIKEMBALIKKAN";
            graphics.DrawString(ucapan, new Font("Courier New", 7),
                     new SolidBrush(Color.Black), rect, sf);
            //end of footer
        }

        private void dataReturPenjualanForm_Activated(object sender, EventArgs e)
        {
            registerGlobalHotkey();
            //noReturTextBox.Select();
        }

        private void dataReturPenjualanForm_Deactivate(object sender, EventArgs e)
        {
            unregisterGlobalHotkey();
        }

        private void detailReturDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //var cell = detailReturDataGridView[e.ColumnIndex, e.RowIndex];
            //DataGridViewRow selectedRow = detailReturDataGridView.Rows[e.RowIndex];

            //if (cell.OwningColumn.Name == "productName")
            //{
            //    if (null != cell.Value)
            //    {
            //        if (cell.Value.ToString().Length > 0)
            //        {
            //            updateSomeRowContents(selectedRow, e.RowIndex, cell.Value.ToString());
            //        }
            //        else
            //        {
            //            clearUpSomeRowContents(selectedRow, e.RowIndex);
            //        }
            //    }
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (originModuleID != globalConstants.RETUR_PENJUALAN_STOCK_ADJUSTMENT && DialogResult.Yes == MessageBox.Show("PRINT RECEIPT ?", "WARNING", MessageBoxButtons.YesNo))
            {
                printReceipt();
            }
        }

        private void detailReturDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            detailReturDataGridView.SuspendLayout();
        }

        private void detailReturDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            detailReturDataGridView.ResumeLayout();
        }

        private void detailReturDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowSelectedIndex = 0;
            double subTotal = 0;
            double productPrice = 0;
            string previousInput = "";
            string tempString = "";
            string cellValue = "";
            string columnName = "";
            double soQTY = 0;
            bool validQty = false;
            double tempVal = 0;
            var cell = detailReturDataGridView[e.ColumnIndex, e.RowIndex];
            DataGridViewRow selectedRow = detailReturDataGridView.Rows[e.RowIndex];
           
            rowSelectedIndex = e.RowIndex;
            columnName = cell.OwningColumn.Name;

            gutil.saveSystemDebugLog(globalConstants.MENU_RETUR_PENJUALAN, "RETUR PENJUALAN : detailReturDataGridView_CellValueChanged [" + columnName + "]");

            if (null != selectedRow.Cells[columnName].Value)
                cellValue = selectedRow.Cells[columnName].Value.ToString();
            else
                cellValue = "";

            if (columnName == "productName")
            {
                if (cellValue.Length > 0)
                {
                    //updateSomeRowContents(selectedRow, rowSelectedIndex, cellValue);
                    //int pos = cashierDataGridView.CurrentCell.RowIndex;

                    //if (pos > 0)
                    //    cashierDataGridView.CurrentCell = cashierDataGridView.Rows[pos - 1].Cells["qty"];

                    //forceUpOneLevel = true;
                }
            }
            else if (columnName == "qty")
            { 
                if (cellValue.Length <= 0)
                {
                    // IF TEXTBOX IS EMPTY, DEFAULT THE VALUE TO 0 AND EXIT THE CHECKING
                    isLoading = true;
                    // reset subTotal Value and recalculate total
                    selectedRow.Cells["subtotal"].Value = 0;
                    //subtotalList[rowSelectedIndex] = "0";

                    //if (returnQty.Count > rowSelectedIndex)
                    //    returnQty[rowSelectedIndex] = "0";

                    selectedRow.Cells[columnName].Value = "0";

                    calculateTotal();

                    return;
                }

                //if (returnQty.Count > rowSelectedIndex)
                //    previousInput = returnQty[rowSelectedIndex];
                //else
                //    previousInput = "0";

                //if (previousInput == "0")
                //{
                //    tempString = cellValue;
                //    if (tempString.IndexOf('0') == 0 && tempString.Length > 1 && tempString.IndexOf("0.") < 0)
                //        selectedRow.Cells[columnName].Value = tempString.Remove(tempString.IndexOf('0'), 1);
                //}

                if (originModuleID == globalConstants.RETUR_PENJUALAN)
                {
                    if (null != selectedRow.Cells["SOqty"].Value)
                        soQTY = Convert.ToDouble(selectedRow.Cells["SOqty"].Value);

                    if (Double.TryParse(cellValue, out tempVal))
                    {
                        if (soQTY >= Convert.ToDouble(cellValue))
                            validQty = true;
                        else
                            validQty = false;
                    }
                    else
                    {
                        validQty = false;
                    }
                }
                else
                    validQty = true;

                if (gutil.matchRegEx(cellValue, globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL)
                    && (cellValue.Length > 0) && validQty
                    )
                {
                    //if (returnQty.Count > rowSelectedIndex)
                    //    returnQty[rowSelectedIndex] = cellValue;
                    //else
                    //    returnQty.Add(cellValue);
                    errorLabel.Text = "";
                }
                else
                {
                    //selectedRow.Cells[columnName].Value = previousInput;
                    errorLabel.Text = "INPUT QTY PADA BARIS [" + (rowSelectedIndex + 1) + "] TIDAK VALID";
                    validQty = false;
                }

                if (validQty)
                {
                    //productPrice = Convert.ToDouble(productPriceList[rowSelectedIndex]);
                    productPrice = Convert.ToDouble(selectedRow.Cells["productPrice"].Value);

                    // subTotal = Math.Round((productPrice * Convert.ToDouble(returnQty[rowSelectedIndex])), 2);
                    subTotal = Math.Round((productPrice * Convert.ToDouble(selectedRow.Cells["qty"].Value)), 2);

                    if (originModuleID == globalConstants.RETUR_PENJUALAN)
                    {
                        if (null != selectedRow.Cells["disc1"].Value)
                        {
                            subTotal = subTotal - Math.Round((subTotal * Convert.ToDouble(selectedRow.Cells["disc1"].Value) / 100), 2);
                        }

                        if (null != selectedRow.Cells["disc2"].Value)
                        {
                            subTotal = subTotal - Math.Round((subTotal * Convert.ToDouble(selectedRow.Cells["disc2"].Value) / 100), 2);
                        }

                        if (null != selectedRow.Cells["discRP"].Value)
                        {
                            subTotal = subTotal - Convert.ToDouble(selectedRow.Cells["discRP"].Value);
                        }
                    }

                    selectedRow.Cells["subtotal"].Value = subTotal;
                    //subtotalList[rowSelectedIndex] = subTotal.ToString();

                    calculateTotal();
                }
            }
        }

        private void detailReturDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (detailReturDataGridView.IsCurrentCellDirty)
            {
                detailReturDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void ChangePrinterButton_Click(object sender, EventArgs e)
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "RETUR PENJUALAN FORM : ChangePrinterButton_Click, DISPLAY PRINTER SELECTION FORM");
            SetPrinterForm displayedForm = new SetPrinterForm();
            displayedForm.ShowDialog(this);
        }
    }
}
