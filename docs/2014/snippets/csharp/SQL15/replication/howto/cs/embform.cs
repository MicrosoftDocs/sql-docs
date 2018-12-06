using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.MessageBox;
using Microsoft.Win32;

namespace EMB
{
	public partial class EMBForm : Form
	{
		private Boolean alwaysShow = true;

		public EMBForm()
		{
			InitializeComponent();
            
            // Clear any existing reg keys.
            RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software");
            foreach (string keyname in key.GetSubKeyNames())
            {
                if (keyname == "TestApp")
                {
                    Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(@"Software\TestApp");
                }
            }
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//<snippetemb_showOKbutton>
			try
			{
				// Do something that may generate an exception.
				throw new ApplicationException("An error has occured");
			}
			catch (ApplicationException ex)
			{
				// Define a new top-level error message.
				string str = "The action failed.";

				// Add the new top-level message to the handled exception.
				ApplicationException exTop = new ApplicationException(str, ex);
				exTop.Source = this.Text;

				// Show an exception message box with an OK button (the default).
				ExceptionMessageBox box = new ExceptionMessageBox(exTop);
				box.Show(this);
			}
			//</snippetemb_showOKbutton>
		}
		private void button2_Click(object sender, EventArgs e)
		{
			//<snippetemb_showYesNobutton>
			// Define the message and caption to display.
			string str = @"Are you sure you want to delete file 'c:\somefile.txt'?";
			string caption = "Confirm File Deletion";

			// Show the exception message box with Yes and No buttons.
			ExceptionMessageBox box = new ExceptionMessageBox(str,
				caption, ExceptionMessageBoxButtons.YesNo,
				ExceptionMessageBoxSymbol.Question,
				ExceptionMessageBoxDefaultButton.Button2);

			if (DialogResult.Yes == box.Show(this))
			{
				// Delete the file.
			}
			//</snippetemb_showYesNobutton>
		}
		private void button3_Click(object sender, EventArgs e)
		{
			//<snippetemb_showcustombutton>
			try
			{
				// Do something that may cause an exception.
				throw new ApplicationException("An error has occured");
			}
			catch (ApplicationException ex)
			{
				string str = "Action failed. What do you want to do?";
				ApplicationException exTop = new ApplicationException(str, ex);
				exTop.Source = this.Text;

				// Show the exception message box with three custom buttons.
				ExceptionMessageBox box = new ExceptionMessageBox(exTop);

				// Set the names of the three custom buttons.
				box.SetButtonText("Skip", "Retry", "Stop Processing");

				// Set the Retry button as the default.
				box.DefaultButton = ExceptionMessageBoxDefaultButton.Button2;
				box.Symbol = ExceptionMessageBoxSymbol.Question;
				box.Buttons = ExceptionMessageBoxButtons.Custom;

				box.Show(this);

				// Do something, depending on the button that the user clicks.
				switch (box.CustomDialogResult)
				{
					case ExceptionMessageBoxDialogResult.Button1:
						// Skip action
						break;
					case ExceptionMessageBoxDialogResult.Button2:
						// Retry action
						break;
					case ExceptionMessageBoxDialogResult.Button3:
						// Stop processing action
						break;
				}
			}
			//</snippetemb_showcustombutton>
		}
		private void button4_Click(object sender, EventArgs e)
		{
			//<snippetemb_usecheckbox>
			try
			{
				// Do something that may cause an exception.
				throw new ApplicationException("An error has occured.");
			}
			catch (ApplicationException ex)
			{
				string str = "The action failed.";
				ApplicationException exTop = new ApplicationException(str, ex);
				exTop.Source = this.Text;

				// Show a message box if the global variable is true.
				if (alwaysShow)
				{
					ExceptionMessageBox box = new ExceptionMessageBox(exTop);
					box.ShowCheckBox = true;
					box.IsCheckBoxChecked = true;
					box.CheckBoxText = "Always show this message";
					box.Show(this);

					// Set the global variable.
					alwaysShow = box.IsCheckBoxChecked;
				}
			}
			//</snippetemb_usecheckbox>
		}
		private void button5_Click(object sender, EventArgs e)
		{
			//<snippetemb_useregkey>
			try
			{
				// Do something that could generate an exception.
				throw new ApplicationException("An error has occured.");
			}
			catch (ApplicationException ex)
			{
				string str = "The action failed. Do you want to continue?";
				ApplicationException exTop = new ApplicationException(str, ex);
				exTop.Source = this.Text;

				// Show a message box with Yes and No buttons
				ExceptionMessageBox box = new ExceptionMessageBox(exTop,
					ExceptionMessageBoxButtons.YesNo,
					ExceptionMessageBoxSymbol.Question,
					ExceptionMessageBoxDefaultButton.Button2);

				// Enable the check box.
				box.ShowCheckBox = true;

				// Define the registry key to use.
				box.CheckBoxRegistryKey =
					Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
					@"Software\TestApp");
				box.CheckBoxRegistryValue = "DontShowActionFailedMessage";
				box.CheckBoxRegistryMeansDoNotShowDialog = true;
				box.DefaultDialogResult = DialogResult.Yes;

				// The message box won't be displayed if the
				// "DontShowActionFailedMessage" value of the registry key 
				// contains a non-zero value.
				if (box.Show(this) == DialogResult.No)
				{
					// Do something if the user clicks the No button.
					this.Close();
				}
			}
			//</snippetemb_useregkey>
		}
		private void button6_Click(object sender, EventArgs e)
		{
			//<snippetemb_moreinfo>
			try
			{
				// Do something that you don't expect to generate an exception.
				throw new ApplicationException("Failed to connect to the server.");
			}
			catch (ApplicationException ex)
			{
				string str = "An unexpected error occurred. Please call Helpdesk.";
				ApplicationException exTop = new ApplicationException(str, ex);
				exTop.Source = this.Text;

				// Information in the Data property of an exception that has a name
				// beginning with "HelpLink.Advanced" is shown when the user
				// clicks the Advanced Information button of the exception message
				// box dialog box.
				exTop.Data.Add("AdvancedInformation.FileName", "application.dll");
				exTop.Data.Add("AdvancedInformation.FilePosition", "line 355");
				exTop.Data.Add("AdvancedInformation.UserContext", "single user mode");

				// Show the exception message box with additional information that 
				// is helpful when a user calls technical support.
				ExceptionMessageBox box = new ExceptionMessageBox(exTop);

				box.Show(this);
			}
			//</snippetemb_moreinfo>
		}
	}
}