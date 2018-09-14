using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////
        //datamembers
        Controller c = new Controller();
        Password[] localdata;
        int index = 0;


        const int x = 7;

        ///////////////////////////////////////////////
        //form
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!c.valid())
            {
                MessageBox.Show("file is corapdted");
                this.Close();
            }

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            c.save();
        }

        ///////////////////////////////////////////////
        //tab
        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = 0;
            //qsearch tab
            name_qsearch_txt.Text = "";
            username_qsearch_txt.Text = "";
            count_qsearch_lbl.Visible = false;
            left_qsearch_btn.Visible = false;
            right_qsearch_btn.Visible = false;
            details_qsearch_lbl.Text = "";
            //search tab
            showname_search_lbl.Text = "";
            showusername_search_lbl.Text = "";
            password_search_btn.Text = "";
            email_search_btn.Text = "";
            link_search_btn.Text = "";
            answer_search_btn.Text = "";
            details_search_btn.Text = "";
            searchusername_search_txt.Text = "";
            searchname_search_txt.Text = "";
            list_search_lst.Items.Clear();
            //add tab
            name_add_txt.Text = "";
            username_add_txt.Text = "";
            password_add_txt.Text = "";
            email_add_txt.Text = "";
            link_add_txt.Text = "";
            answer_add_txt.Text = "";
            details_add_txt.Text = "";
        }

        ///////////////////////////////////////////////
        //add tab
        private void add_add_btn_Click(object sender, EventArgs e)
        {
            if ((name_add_txt.Text == "") || (username_add_txt.Text == "") || (password_add_txt.Text == ""))
            {
                MessageBox.Show("name, username and password are required");
                return;
            }
            string s=c.add(name_add_txt.Text, username_add_txt.Text, password_add_txt.Text, email_add_txt.Text, link_add_txt.Text, answer_add_txt.Text, details_add_txt.Text);
            MessageBox.Show(s);

        }

        ///////////////////////////////////////////////
        //qsearch tab        
        private void name_qsearch_txt_TextChanged(object sender, EventArgs e)
        {
            localdata = c.filter(name_qsearch_txt.Text, username_qsearch_txt.Text);
            index = 0;


            if (localdata.Length == 0)
            {
                details_qsearch_lbl.Text = "none";
                count_qsearch_lbl.Visible = false;
                left_qsearch_btn.Visible = false;
                right_qsearch_btn.Visible = false;
            }
            else
            {
                details_qsearch_lbl.Text = localdata[0].standartShow();
                explain_qsearch_lbl.Text = localdata[0].username + "-" + localdata[0].password;
                explain_qsearch_lbl.Visible = false;

                count_qsearch_lbl.Text = "1/" + localdata.Length;
                count_qsearch_lbl.Visible = true;
                left_qsearch_btn.Visible = true;
                right_qsearch_btn.Visible = true;
                left_qsearch_btn.Enabled = true;
                right_qsearch_btn.Enabled = true;
                if (localdata.Length <= 1)
                {
                    left_qsearch_btn.Enabled = false;
                    right_qsearch_btn.Enabled = false;
                }
            }
        }
        private void username_qsearch_txt_TextChanged(object sender, EventArgs e)
        {
            name_qsearch_txt_TextChanged(sender, e);
        }
        private void left_qsearch_btn_Click(object sender, EventArgs e)
        { 
            index--;
            if (index==-1)
                index = localdata.Length - 1;
            count_qsearch_lbl.Text = (index + 1) + "/" + localdata.Length;
            details_qsearch_lbl.Text = localdata[index].standartShow();
        }
        private void right_qsearch_btn_Click(object sender, EventArgs e)
        {
            index++;
            if (index == localdata.Length)
                index = 0;
            count_qsearch_lbl.Text = (index + 1) + "/" + localdata.Length;
            details_qsearch_lbl.Text = localdata[index].standartShow();
        }
        private void details_qsearch_lbl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(explain_qsearch_lbl.Text.Split('-')[1]);
        }
        private void details_qsearch_lbl_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(explain_qsearch_lbl.Text.Split('-')[0]);
        }

        ///////////////////////////////////////////////
        //search tab
        private void searchname_search_txt_TextChanged(object sender, EventArgs e)
        {
            localdata = c.filter(searchname_search_txt.Text, searchusername_search_txt.Text);
            index = 0;

            list_search_lst.Items.Clear();
            for (int i = 0; i < localdata.Length; i++)
                list_search_lst.Items.Add(localdata[i].name + "-" + localdata[i].username);
        }
        private void searchusername_search_txt_TextChanged(object sender, EventArgs e)
        {
            searchname_search_txt_TextChanged(sender, e);
        }
        private void list_search_lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = list_search_lst.SelectedIndex;
            if (index != -1)
            {
                showname_search_lbl.Text = localdata[index].name;
                showusername_search_lbl.Text = localdata[index].username;
                password_search_btn.Text = localdata[index].password;
                email_search_btn.Text = localdata[index].email;
                link_search_btn.Text = localdata[index].link;
                answer_search_btn.Text = localdata[index].answer;
                details_search_btn.Text = localdata[index].details;
            }
            else
            {
                showname_search_lbl.Text = "";
                showusername_search_lbl.Text = "";
                password_search_btn.Text = "";
                email_search_btn.Text = "";
                link_search_btn.Text = "";
                answer_search_btn.Text = "";
                details_search_btn.Text = "";
            }
        }
        private void update_search_btn_Click(object sender, EventArgs e)
        {
            index = list_search_lst.SelectedIndex;
            if (index == -1 || index == localdata.Length)
            {
                MessageBox.Show("error, please select index");
                return;
            }

            localdata[index].password = password_search_btn.Text;
            localdata[index].email = email_search_btn.Text;
            localdata[index].link = link_search_btn.Text;
            localdata[index].answer = answer_search_btn.Text;
            localdata[index].details = details_search_btn.Text;

            c.update(localdata[index].name, localdata[index].username, localdata[index].password, localdata[index].email, localdata[index].link, localdata[index].answer, localdata[index].details);

            MessageBox.Show("information has successfuly updated");

        }
        private void delete_search_btn_Click(object sender, EventArgs e)
        {
            index = list_search_lst.SelectedIndex;
            if (index == -1 || index == localdata.Length)
            {
                MessageBox.Show("error, please select index");
                return;
            }

            c.delete(localdata[index].name, localdata[index].username);
            searchname_search_txt_TextChanged(sender, e);

            showname_search_lbl.Text = "";
            showusername_search_lbl.Text = "";
            password_search_btn.Text = "";
            email_search_btn.Text = "";
            link_search_btn.Text = "";
            answer_search_btn.Text = "";
            details_search_btn.Text = "";
            list_search_lst.Items.Clear();
            searchname_search_txt_TextChanged(sender, e);
        }
    }
}
