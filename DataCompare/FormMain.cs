using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataCompare
{
    public partial class FormMain : Form
    {
        List<string> lines;
        List<string> lines0;
        List<string> lines1;
        List<string> lines2;
        List<string> lines3;
        List<string> lines4;

        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Title = "选择文本文件";
                openFileDialog1.FileName = "*.txt";
                openFileDialog1.Filter = "文本文件(*.txt)|*.txt";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog1.FileName;
                    lines1 = new List<string>();
                    string line1 = string.Empty;
                    using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                    {
                        try { line1 = reader.ReadLine(); }
                        catch (IOException ex) { MessageBox.Show(ex.ToString()); }
                        while (line1 != "" && line1 != null)
                        {
                            lines1.Add(line1);
                            try { line1 = reader.ReadLine(); }
                            catch (IOException ex) { MessageBox.Show(ex.ToString()); }
                        }
                    }
                    lines1 = lines1.Distinct().ToList();
                    MessageBox.Show(Path.GetFileName(openFileDialog1.FileName) + " 去除重复后，共计 " + lines1.Count.ToString() + " 行");
                }
            }
            catch (IOException ex) { MessageBox.Show(ex.Message, "选择文件", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog2.Title = "选择文本文件";
                openFileDialog2.FileName = "*.txt";
                openFileDialog2.Filter = "文本文件(*.txt)|*.txt";
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = openFileDialog2.FileName;
                    lines2 = new List<string>();
                    string line2 = string.Empty;
                    using (StreamReader reader = new StreamReader(openFileDialog2.FileName))
                    {
                        try { line2 = reader.ReadLine(); }
                        catch (IOException ex) { MessageBox.Show(ex.ToString()); }

                        while (line2 != "" && line2 != null)
                        {
                            lines2.Add(line2);
                            try { line2 = reader.ReadLine(); }
                            catch (IOException ex) { MessageBox.Show(ex.ToString()); }
                        }
                    }
                    lines2 = lines2.Distinct().ToList();
                    MessageBox.Show(Path.GetFileName(openFileDialog2.FileName) + " 去除重复后，共计 " + lines2.Count.ToString() + " 行");
                }
            }
            catch (IOException ex) { MessageBox.Show(ex.Message, "选择文件", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Path.GetDirectoryName(openFileDialog1.FileName)))
                {
                    MessageBox.Show("基准文本文件不能为空！");
                    return;
                }

                if (string.IsNullOrEmpty(Path.GetDirectoryName(openFileDialog2.FileName)))
                {
                    MessageBox.Show("比较文本文件不能为空！");
                    return;
                }

                lines = new List<string>();
                lines0 = new List<string>();
                lines3 = new List<string>();
                lines4 = new List<string>();

                string strFilepath1 = Path.GetDirectoryName(openFileDialog1.FileName);
                string strFilename1 = Path.GetFileName(openFileDialog1.FileName);
                string strFilepath2 = Path.GetDirectoryName(openFileDialog2.FileName);
                string strFilename2 = Path.GetFileName(openFileDialog2.FileName);
                String filename = strFilepath2 + Path.DirectorySeparatorChar + strFilename2.Substring(0, strFilename2.LastIndexOf('.')) + "-" + strFilename1.Substring(0, strFilename1.LastIndexOf('.')) + "_共有数据.txt";
                String filename0 = strFilepath1 + Path.DirectorySeparatorChar + strFilename1.Substring(0, strFilename1.LastIndexOf('.')) + "-" + strFilename2.Substring(0, strFilename2.LastIndexOf('.')) + "_共有数据.txt";
                String filename1 = strFilepath1 + Path.DirectorySeparatorChar + strFilename1.Substring(0, strFilename1.LastIndexOf('.')) + "-" + strFilename2.Substring(0, strFilename2.LastIndexOf('.')) + "_去除共有数据.txt";
                String filename2 = strFilepath2 + Path.DirectorySeparatorChar + strFilename2.Substring(0, strFilename2.LastIndexOf('.')) + "-" + strFilename1.Substring(0, strFilename1.LastIndexOf('.')) + "_去除共有数据.txt";
                String filename3 = strFilepath1 + Path.DirectorySeparatorChar + strFilename1.Substring(0, strFilename1.LastIndexOf('.')) + "-" + strFilename2.Substring(0, strFilename2.LastIndexOf('.')) + "_辖区出生日期相同数据.txt";
                String filename4 = strFilepath2 + Path.DirectorySeparatorChar + strFilename2.Substring(0, strFilename2.LastIndexOf('.')) + "-" + strFilename1.Substring(0, strFilename1.LastIndexOf('.')) + "_辖区出生日期相同数据.txt";

                if (File.Exists(filename))
                {
                    DialogResult dlResult = MessageBox.Show(filename + "\n\n已经存在。覆盖该文件吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    if (dlResult != DialogResult.Yes) { return; }
                }

                DateTime dtStart = DateTime.Now;
                string strMessage = "";
                StreamWriter swSame = new StreamWriter(filename);
                StreamWriter swSame0 = new StreamWriter(filename0);

                for (int i = 0; i < lines2.Count; i++)
                {
                    for (int n = 0; n < lines1.Count; n++)
                    {

                        if (checkBox3.Checked == true)
                        {
                            if (lines2[i].Length == 15 && lines1[n].Length == 15)
                            {
                                if (lines2[i].ToUpper() == lines1[n].ToUpper())
                                {
                                    lines.Add(lines2[i]);
                                    lines0.Add(lines1[n]);
                                }
                            }

                            if (lines2[i].Length == 18 && lines1[n].Length == 18)
                            {
                                if (lines2[i].ToUpper() == lines1[n].ToUpper())
                                {
                                    lines.Add(lines2[i]);
                                    lines0.Add(lines1[n]);
                                }
                            }

                            if (lines2[i].Length == 15 && lines1[n].Length == 18)
                            {
                                try
                                {
                                    if (Convert.ToInt64(lines2[i].Substring(0, 14)) > 0)
                                    {
                                        if (lines2[i].Substring(0, 12) == lines1[n].Substring(0, 6) + lines1[n].Substring(8, 6))
                                        {
                                            lines4.Add(lines2[i]);
                                            lines3.Add(lines1[n]);
                                        }
                                    }
                                }
                                catch { }
                            }

                            if (lines2[i].Length == 18 && lines1[n].Length == 15)
                            {
                                try
                                {
                                    if (Convert.ToInt64(lines2[i].Substring(0, 17)) > 0)
                                    {
                                        if (lines1[n].Substring(0, 12) == lines2[i].Substring(0, 6) + lines2[i].Substring(8, 6))
                                        {
                                            lines4.Add(lines2[i]);
                                            lines3.Add(lines1[n]);
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            if (lines2[i].ToUpper() == lines1[n].ToUpper())
                            {
                                lines.Add(lines2[i]);
                                lines0.Add(lines1[n]);
                            }
                        }
                    }
                }

                for (int i = 0; i < lines.Count; i++)
                {
                    swSame.WriteLine(lines[i].ToString());
                }
                swSame.Close();
                strMessage += filename + "\n\n";

                for (int i = 0; i < lines0.Count; i++)
                {
                    swSame0.WriteLine(lines0[i].ToString());
                }
                swSame0.Close();
                strMessage += filename0 + "\n\n";

                if (checkBox3.Checked == true)
                {
                    try
                    {
                        lines3 = lines3.Distinct().ToList();
                        lines4 = lines4.Distinct().ToList();
                    }
                    catch { }

                    StreamWriter swBirthday2 = new StreamWriter(filename4);
                    for (int i = 0; i < lines4.Count; i++)
                    {
                        swBirthday2.WriteLine(lines4[i].ToString());
                    }
                    swBirthday2.Close();
                    strMessage += filename4 + "\n\n";

                    StreamWriter swBirthday1 = new StreamWriter(filename3);
                    for (int i = 0; i < lines3.Count; i++)
                    {
                        swBirthday1.WriteLine(lines3[i].ToString());
                    }
                    swBirthday1.Close();
                    strMessage += filename3 + "\n\n";
                }

                if (checkBox2.Checked == true)
                {
                    StreamWriter swDiffrence2 = new StreamWriter(filename2);

                    for (int i = 0; i < lines2.Count; i++)
                    {
                        for (int n = 0; n < lines.Count; n++)
                        {
                            try
                            {
                                if (lines2[i].ToUpper() == lines[n].ToUpper())
                                {
                                    lines2.Remove(lines2[i]);
                                }
                            }
                            catch { }
                        }
                    }

                    for (int i = 0; i < lines2.Count; i++)
                    {
                        swDiffrence2.WriteLine(lines2[i].ToString());
                    }

                    swDiffrence2.Close();
                    strMessage += filename2 + "\n\n";
                }

                if (checkBox1.Checked == true)
                {
                    StreamWriter swDiffrence1 = new StreamWriter(filename1);
                    for (int i = 0; i < lines1.Count; i++)
                    {
                        for (int n = 0; n < lines0.Count; n++)
                        {
                            try
                            {
                                if (lines1[i].ToUpper() == lines0[n].ToUpper())
                                {
                                   lines1.Remove(lines1[i]);
                                }
                            }
                            catch { }
                        }
                    }

                    for (int i = 0; i < lines1.Count; i++)
                    {
                        swDiffrence1.WriteLine(lines1[i].ToString());
                    }

                    swDiffrence1.Close();
                    strMessage += filename1 + "\n\n";
                }
                DateTime dtEnd = DateTime.Now;
                TimeSpan tsTotal = dtEnd - dtStart;
                MessageBox.Show(strMessage + "保存完成！\n\n用时：" + tsTotal.ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
