using PinSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PinterestTest
{
	public partial class Form1 : Form
	{
		private string AccessToken = "AUtAnCVo-ezgcyg62ad6Kcy_PwybFRD8u-8wdFJEr3zvoYAozgAAAAA";
		private string appID = "4949311670966044439";
		private string appSecret = "4524368bb1e46e67acf32ef6a44b96e3df7e405fdb0dd3169943077811e8d84d";
		private PinterestService service;
		private PinterestUser user;
		private string imageURL = "https://cdn-images-1.medium.com/max/2000/1*WpOK6v_X2lr8CsmAdJR4og.jpeg";
		
		public Form1()
		{
			InitializeComponent();
			service = new PinterestService(appID, appSecret, AccessToken);
		}

		private void btnGetUser_Click(object sender, EventArgs e)
		{
            AccessToken = textBox1.Text;
            appID = textBox2.Text;
            appSecret = textBox3.Text;

            try
			{				
				user = service.getUser();

				if (user != null)
				{
					btnNewBoard.Enabled = true;
					btnGetBoards.Enabled = true;
					btnGetPins.Enabled = true;
					lblUserID.Text = "Id: " + user.Id;
					lblFirstName.Text = "First Name: " + user.FirstName;
					lblLastName.Text = "Last Name: " + user.LastName;
					lblURL.Text = "URL: " + user.URL;
				}
				else
				{
					btnNewBoard.Enabled = false;
					btnNewPin.Enabled = false;
					btnGetPins.Enabled = false;
					btnGetBoards.Enabled = false;
					lblUserID.Text = "Id: ";
					lblFirstName.Text = "First Name: ";
					lblLastName.Text = "Last Name: ";
					lblURL.Text = "URL: ";
				}
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.btnGetUser_Click(): " + E.Message);
			}
		}

		private void btnGetPins_Click(object sender, EventArgs e)
		{
			getPins();
		}

		private void getPins()
		{
			try
			{
				List<Pin> pins = service.getPins();
				lblPinCount.Text = "Pin Count: " + pins.Count;
				lbPins.DisplayMember = "Note";
				lbPins.ValueMember = "Id";
				lbPins.DataSource = pins;
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.getPins(): " + E.Message);
			}
		}

		private void lbPins_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedValue = lbPins.SelectedValue.ToString();
				Pin pin = service.getPin(selectedValue);
				if (pin != null)
				{
					btnDeletePin.Enabled = true;
					lblPinID.Text = "Id: " + pin.Id;
					lblPinLink.Text = "Link: " + pin.Link;
					lblPinNote.Text = "Note: " + pin.Note;
					lblPinURL.Text = "URL: " + pin.URL;
				}
				else
				{
					btnDeletePin.Enabled = false;
					lblPinID.Text = "Id: ";
					lblPinLink.Text = "Link: ";
					lblPinNote.Text = "Note: ";
					lblPinURL.Text = "URL: ";
				}
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.lbPins_SelectedIndexChanged(): " + E.Message);
			}
		}

		private void btnGetBoards_Click(object sender, EventArgs e)
		{
			getBoards();
		}

		private void getBoards()
		{
			try
			{
				List<Board> boards = service.getBoards();
				lblBoardCount.Text = "Board Count: " + boards.Count;
				lbBoards.DisplayMember = "Name";
				lbBoards.ValueMember = "Id";
				lbBoards.DataSource = boards;
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.getBoards(): " + E.Message);
			}
		}

		private void lbBoards_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedValue = lbBoards.SelectedValue.ToString();
				Board board = service.getBoard(selectedValue);
				if (board != null)
				{
					btnDeleteBoard.Enabled = true;
					btnNewPin.Enabled = true;
					lblBoardID.Text = "Id: " + board.Id;
					lblBoardName.Text = "Name: " + board.Name;
					lblBoardURL.Text = "URL: " + board.URL;
				}
				else
				{
					btnDeleteBoard.Enabled = false;
					btnNewPin.Enabled = false;
					lblBoardID.Text = "Id: ";
					lblBoardName.Text = "Name: ";
					lblBoardURL.Text = "URL: ";
				}
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.lbPins_SelectedIndexChanged(): " + E.Message);
			}
		}

		private void btnDeletePin_Click(object sender, EventArgs e)
		{
			try
			{
				string selectedValue = lbPins.SelectedValue.ToString();
				bool ret = service.deletePin(selectedValue);				
				if (ret)
				{
					btnDeletePin.Enabled = false;
					getPins();
				}
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.lbPins_SelectedIndexChanged(): " + E.Message);
			}
		}

		private void btnNewPin_Click(object sender, EventArgs e)
		{
			try
			{
				string boardID = lbBoards.SelectedValue.ToString();
				Pin pin = service.createPin(boardID, txtPinNote.Text, imageURL, imageURL);
				if (pin != null)
					this.getPins();
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.btnNewPin_Click(): " + E.Message);
			}
		}

		private void btnNewBoard_Click(object sender, EventArgs e)
		{
			try
			{
				Board board = service.createBoard(txtBoardName.Text, txtBoardDescription.Text);
				if (board != null)
					this.getBoards();
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.btnNewBoard_Click(): " + E.Message);
			}
		}

		private void btnDeleteBoard_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Are you sure you want to delete this board?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					string boardID = lbBoards.SelectedValue.ToString();
					if (service.deleteBoard(boardID))
						getBoards();
				}
			}
			catch (Exception E)
			{
				Debug.WriteLine("Exception in Form1.btnDeleteBoard_Click(): " + E.Message);
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void ReadInTimeSheet()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = ofd.FileName;
                //var fileLines = File.ReadAllLines(@"C:\filepath\MyTimeSheet.txt");
                var fileLines = File.ReadAllLines(fileToOpen);
                for (int i = 0; i + 4 < fileLines.Length; i += 5)
                {
                    listView1.Items.Add(
                        new ListViewItem(new[]
                        {
                            fileLines[i],
                            fileLines[i + 1],
                            fileLines[i + 2],
                            fileLines[i + 3],
                            fileLines[i + 4]
                        }));
                }

                // Resize the columns
                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    listView1.Columns[i].Width = -2;
                } 
            } 
        }
    }
}
