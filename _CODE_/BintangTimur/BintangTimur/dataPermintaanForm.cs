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

namespace AlphaSoft
{
    public partial class dataPermintaanForm : Form
    {
        private int originModuleID = 0;
        private int selectedBranchFromID = 0;
        private int selectedBranchToID = 0;
        private int selectedROID = 0;

        private Data_Access DS = new Data_Access();

        private globalUtilities gUtil = new globalUtilities();
        private CultureInfo culture = new CultureInfo("id-ID");

        public dataPermintaanForm()
        {
            InitializeComponent();
        }

        public dataPermintaanForm(int moduleID)
        {
            InitializeComponent();
            originModuleID = moduleID;

            //if (moduleID == globalConstants.CEK_DATA_MUTASI)
            //    newButton.Visible = false;
            //else if (moduleID == globalConstants.PERMINTAAN_BARANG)
            //    importButton.Visible = false;
        }
        
        private void displaySpecificForm(int roID = 0)
        {
            switch (originModuleID)
            {
                case globalConstants.PERMINTAAN_BARANG:
                    if (roID == 0)
                    {
                        permintaanProdukForm requestOrderForm = new permintaanProdukForm(globalConstants.NEW_REQUEST_ORDER);
                        requestOrderForm.ShowDialog(this);
                    }
                    else
                    {
                        permintaanProdukForm editRequestOrderForm = new permintaanProdukForm(globalConstants.EDIT_REQUEST_ORDER, roID);
                        editRequestOrderForm.ShowDialog(this);
                    }
                    break;

                case globalConstants.CEK_DATA_MUTASI:
                    dataMutasiBarangDetailForm displayedForm = new dataMutasiBarangDetailForm(globalConstants.CEK_DATA_MUTASI, roID);
                    displayedForm.ShowDialog(this);
                    break;
            }
        }

        private void fillInBranchCombo(ComboBox visibleCombo, ComboBox hiddenCombo)
        {
            MySqlDataReader rdr;
            string sqlCommand = "";

            sqlCommand = "SELECT BRANCH_ID, BRANCH_NAME FROM MASTER_BRANCH WHERE BRANCH_ACTIVE = 1 ORDER BY BRANCH_NAME ASC";

            visibleCombo.Items.Clear();
            hiddenCombo.Items.Clear();

            using (rdr = DS.getData(sqlCommand))
            {
                while (rdr.Read())
                {
                    visibleCombo.Items.Add(rdr.GetString("BRANCH_NAME"));
                    hiddenCombo.Items.Add(rdr.GetString("BRANCH_ID"));
                }
            }

            rdr.Close();
        }
        
        private void loadROdata(bool showAll = false)
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string sqlCommand;
            string dateFrom, dateTo;
            string noROInvoiceParam = "";

            DS.mySqlConnect();

            sqlCommand = "SELECT ID, RO_INVOICE AS 'NO PERMINTAAN', DATE_FORMAT(RO_DATETIME, '%d-%M-%Y')  AS 'TANGGAL PERMINTAAN', DATE_FORMAT(RO_EXPIRED, '%d-%M-%Y') AS 'TANGGAL EXPIRED', M1.BRANCH_NAME AS 'ASAL PERMINTAAN', RO_TOTAL AS 'TOTAL' " +
                                "FROM REQUEST_ORDER_HEADER RH, MASTER_BRANCH M1 " + 
                                "WHERE 1 = 1 AND RO_BRANCH_ID_TO = M1.BRANCH_ID AND RO_ACTIVE = 1";

            if (!showAll)
            {
                dateFrom = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(DateTime.Now));
                if (!showExpiredCheckBox.Checked)
                {
                    sqlCommand = sqlCommand + " AND DATE_FORMAT(RO_EXPIRED, '%Y%m%d') > '" + dateFrom + "'";
                }
 
                if (noROInvoiceTextBox.Text.Length > 0)
                {
                    noROInvoiceParam = MySqlHelper.EscapeString(noROInvoiceTextBox.Text);
                    sqlCommand = sqlCommand + " AND RO_INVOICE LIKE '%" + noROInvoiceParam + "%'";
                }

                dateFrom = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(RODtPicker_1.Value));
                dateTo= String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(RODtPicker_2.Value));
                sqlCommand = sqlCommand + " AND DATE_FORMAT(RO_DATETIME, '%Y%m%d')  >= '" + dateFrom + "' AND DATE_FORMAT(RO_DATETIME, '%Y%m%d')  <= '" + dateTo + "'";

                if (branchToCombo.Text.Length > 0)
                {
                    sqlCommand = sqlCommand + " AND RO_BRANCH_ID_TO = " + selectedBranchToID;
                }
            }

            using (rdr = DS.getData(sqlCommand))
            {
                dataRequestOrderGridView.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dataRequestOrderGridView.DataSource = dt;

                    dataRequestOrderGridView.Columns["ID"].Visible = false;

                    dataRequestOrderGridView.Columns["NO PERMINTAAN"].Width = 200;
                    dataRequestOrderGridView.Columns["TANGGAL PERMINTAAN"].Width = 200;
                    dataRequestOrderGridView.Columns["TANGGAL EXPIRED"].Width = 200;
                    dataRequestOrderGridView.Columns["ASAL PERMINTAAN"].Width = 200;
                    dataRequestOrderGridView.Columns["TOTAL"].Width = 200;
                }

                rdr.Close();
            }
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            if (showAllCheckBox.Checked)
            {
                loadROdata(true);
            }
            else
                loadROdata();
        }

        private void dataPermintaanForm_Load(object sender, EventArgs e)
        {
            int userAccessOption = 0;
            Button[] arrButton = new Button[3];

            RODtPicker_1.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            RODtPicker_2.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

            fillInBranchCombo(branchFromCombo, branchFromHiddenCombo);
            fillInBranchCombo(branchToCombo, branchToHiddenCombo);

            userAccessOption = DS.getUserAccessRight(globalConstants.MENU_REQUEST_ORDER, gUtil.getUserGroupID());

            if (userAccessOption == 2 || userAccessOption == 6)
            {
                newButton.Visible = true;
                importButton.Visible = true;
            }
            else
            {
                newButton.Visible = false;
                importButton.Visible = false;
            }

            if (originModuleID == globalConstants.CEK_DATA_MUTASI)
            {
                newButton.Visible = false;
            }

            //arrButton[0] = displayButton;
            //arrButton[1] = newButton;
            //arrButton[2] = importButton;
            //gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

            gUtil.reArrangeTabOrder(this);

            noROInvoiceTextBox.Select();
        }

        private void dataRequestOrderGridView_DoubleClick(object sender, EventArgs e)
        {
            if (dataRequestOrderGridView.Rows.Count <= 0)
                return;

            int rowSelectedIndex = dataRequestOrderGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataRequestOrderGridView.Rows[rowSelectedIndex];
            selectedROID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            displaySpecificForm(selectedROID);
        }

        private void dataRequestOrderGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            displaySpecificForm();
        }

        private void branchFromCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedBranchFromID = Convert.ToInt32(branchFromHiddenCombo.Items[branchFromCombo.SelectedIndex]);
        }

        private void branchToCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedBranchToID = Convert.ToInt32(branchToHiddenCombo.Items[branchToCombo.SelectedIndex]);
        }

        private bool noROExist(string roInvoiceValue)
        {
            bool result = false;

            if (0 < Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM REQUEST_ORDER_HEADER WHERE RO_INVOICE = '" + roInvoiceValue + "'")))
                result = true;

            return result;
        }

        private bool loadImportedDataRO(string filePath)
        {
            bool result = false;
            string sqlCommand = "";
            string roInvoice = "";
            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                System.IO.StreamReader file = new System.IO.StreamReader(filePath);

                roInvoice = file.ReadLine();

                if (!noROExist(roInvoice))
                {
                    while ((sqlCommand = file.ReadLine()) != null)
                    {
                        DS.executeNonQueryCommand(sqlCommand);
                    }

                    file.Close();

                    DS.commit();
                }
                else
                    MessageBox.Show("NOMOR REQUEST ORDER SUDAH ADA");
            }
            catch (Exception e)
            {
                try
                {
                    //myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    if (DS.getMyTransConnection() != null)
                    {
                        MessageBox.Show("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                }

                MessageBox.Show("An exception of type " + e.GetType() +
                                  " was encountered while inserting the data.");
                MessageBox.Show("Neither record was written to database.");
            }
            finally
            {
                DS.mySqlClose();
                result = true;
            }

            return result;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            string importFileName = "";

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Export File (.exp)|*.exp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    importFileName = openFileDialog1.FileName;
                    if (loadImportedDataRO(importFileName))
                    {
                        if (showAllCheckBox.Checked)
                        {
                            loadROdata(true);
                        }
                        else
                            loadROdata();                        
                    }
                    else
                        MessageBox.Show("ERROR CAN'T LOAD FILE");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void dataPermintaanForm_Activated(object sender, EventArgs e)
        {
            //if need something
            if (dataRequestOrderGridView.Rows.Count > 0)
                displayButton.PerformClick();
            noROInvoiceTextBox.Select();
        }

        private void dataRequestOrderGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataRequestOrderGridView.Rows.Count <= 0)
                    return;

                int rowSelectedIndex = (dataRequestOrderGridView.SelectedCells[0].RowIndex);
                DataGridViewRow selectedRow = dataRequestOrderGridView.Rows[rowSelectedIndex];
                selectedROID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                displaySpecificForm(selectedROID);
            }


        }
    }
}
