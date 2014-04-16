using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindWordTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\123.txt");
            DataTable table = new DataTable();

            DataColumn column = null;

            // 把列假如到table中
            column = new DataColumn("id");
            table.Columns.Add(column);
            column = new DataColumn("source");
            table.Columns.Add(column);
            column = new DataColumn("datatype");
            table.Columns.Add(column);
            column = new DataColumn("userid");
            table.Columns.Add(column);
            column = new DataColumn("concent");
            table.Columns.Add(column);
            column = new DataColumn("create_time");
            table.Columns.Add(column);

            DataRow row;

            int counter = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] values = line.Split('\t');

                row = table.NewRow();
                row["id"] = values[0];
                row["source"] = values[1];
                row["datatype"] = values[2];
                row["userid"] = values[3];
                row["concent"] = values[4];
                row["create_time"] = values[5];
                table.Rows.Add(row);
                
                counter++;
            }
            Write(table,@"C:\456.txt");
        }
        public void Write(DataTable dt, string filePath)
        {
            int i = 0;
            StreamWriter sw = null;

            try
            {                
                sw = new StreamWriter(filePath, false);
               
                for (i = 0; i < dt.Columns.Count - 1; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName + " | ");
                }
                sw.Write(dt.Columns[i].ColumnName);
                sw.WriteLine();

              
                foreach (DataRow row in dt.Rows)
                {
                    object[] array = row.ItemArray;

                    for (i = 0; i < array.Length - 1; i++)
                    {
                        sw.Write(array[i].ToString() + " | ");
                    }
                    sw.Write(array[i].ToString());
                    sw.WriteLine();
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Operation : \n" + ex.ToString(),  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Search(string keyword)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var Fi = from line in File.ReadLines(@"C:\comments.txt", Encoding.Default)
                     where line.ToLower().Contains(keyword)
                     select new
                     {
                         Line = line
                     };

            StringBuilder sb = new StringBuilder();
            int count = 0;

            foreach (var f in Fi)
            {
                sb.AppendFormat("{0}\r\n", f.Line);
                count++;
            }
            sw.Stop();

            if (sb.Length == 0) sb.Append("Not Found");

            textBox2.Text = sb.ToString();
            label1.Text = "花費時間: " + sw.Elapsed + "  筆數: " + count.ToString();

            //MessageBox.Show(sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search(textBox1.Text);
        }


    }

}
