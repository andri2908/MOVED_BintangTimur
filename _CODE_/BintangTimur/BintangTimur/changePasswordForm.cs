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

namespace AlphaSoft
{
    public partial class changePasswordForm : Form
    {
        private globalUtilities gutil = new globalUtilities();
        private Data_Access DS = new Data_Access();
        
        private int selectedUserID = 0;

        public changePasswordForm(int userID)
        {
            InitializeComponent();

            selectedUserID = userID;
            gutil.saveSystemDebugLog(0, "CREATE CHANGE PASSWORD FORM UID[" + selectedUserID + "]");
        }

        private bool validateOldPassword()
        {
            string oldPassword = oldPasswordTextBox.Text;
            oldPassword = MySqlHelper.EscapeString(oldPassword);
            int result;

            result = Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_USER WHERE ID = " + selectedUserID + " AND USER_PASSWORD = '" + oldPassword + "'"));
            if (result != 0)
                return true;

            gutil.saveSystemDebugLog(0, "VALIDATE OLD PASSWORD FAILED ["+oldPassword+"]");

            return false;
        }

        private bool dataValidated()
        {
            if (oldPasswordTextBox.Text.Trim().Equals(""))
            {
                errorLabel.Text = "OLD PASSWORD TIDAK BOLEH KOSONG";
                return false;
            }

            if (!validateOldPassword())
            {
                errorLabel.Text = "OLD PASSWORD SALAH";
                return false;
            }
            
            if (newPasswordTextBox.Text.Trim().Equals(""))
            {
                errorLabel.Text = "NEW PASSWORD TIDAK BOLEH KOSONG";
                return false;
            }

            if (!gutil.matchRegEx(newPasswordTextBox.Text, globalUtilities.REGEX_ALPHANUMERIC_ONLY))
            {
                errorLabel.Text = "PASSWORD HARUS ALPHANUMERIC";
                return false;
            }

            if (!newPasswordTextBox.Text.Equals(newPassword2TextBox.Text))
            {
                errorLabel.Text = "NEW PASSWORD DAN RE-TYPE PASSWORD HARUS SAMA";
                return false;
            }

            return true;
        }

        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";

            string newPassword = newPasswordTextBox.Text;
            //newPassword = MySqlHelper.EscapeString(newPassword);

            MySqlException internalEX = null;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                sqlCommand = "UPDATE MASTER_USER SET USER_PASSWORD = '"+newPassword+"' WHERE ID = " + selectedUserID;
                gutil.saveSystemDebugLog(0, "UPDATE NEW PASSWORD [" + newPassword + "]");

                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
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

        private void loginButton_Click(object sender, EventArgs e) //savebutton
        {
            if (MessageBox.Show("Yakin merubah password?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (saveData())
                {
                    gutil.saveUserChangeLog(0, globalConstants.CHANGE_LOG_UPDATE, "USER CHANGE PASSWORD");
                    gutil.showSuccess(gutil.UPD);
                    gutil.ResetAllControls(this);
                }
            } else
            {
                oldPasswordTextBox.Select();
            }
        }

        private void changePasswordForm_Load(object sender, EventArgs e)
        {
            Button[] arrButton = new Button[2];
            arrButton[0] = saveButton;
            arrButton[1] = button1;
            gutil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);
            gutil.reArrangeTabOrder(this);
            errorLabel.Text = "";
            oldPasswordTextBox.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gutil.ResetAllControls(this);
            errorLabel.Text = "";
        }

        private void changePasswordForm_Activated(object sender, EventArgs e)
        {
            oldPasswordTextBox.Select();
        }

        private void oldPasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorLabel.Text = "";
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (oldPasswordTextBox.Text.Trim().Equals(""))
                {
                    errorLabel.Text = "OLD PASSWORD TIDAK BOLEH KOSONG";                    
                } else
                {
                    newPasswordTextBox.Select();
                }
            }
        }

        private void newPasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorLabel.Text = "";
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (newPasswordTextBox.Text.Trim().Equals(""))
                {
                    errorLabel.Text = "NEW PASSWORD TIDAK BOLEH KOSONG";
                }
                else
                {
                    newPassword2TextBox.Select();
                }
            }
        }

        private void newPassword2TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                saveButton.PerformClick();
            }
        }
    }
}
