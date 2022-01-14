using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Конвертер
{   
    public partial class Form1 : Form
    {

        XDocument xdoc = XDocument.Load("http://www.cbr.ru/scripts/XML_daily.asp");
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, decimal> codeToRate = xdoc.Descendants("Valute")
            .Where(v => v.Element("Nominal").Value != "1")
            .Select(v => new {
                CharCode = v.Element("CharCode").Value,
                Value = decimal.Parse(v.Element("Value").Value, CultureInfo.GetCultureInfo("ru-RU")),
                Nominal = int.Parse(v.Element("Nominal").Value)
            })
            .ToDictionary(r => r.CharCode, r => r.Value / r.Nominal);
            decimal nok = Convert.ToDecimal(textBox1.Text), rub;
            rub = Decimal.Round(nok * codeToRate["HUF"], 3);
            decimal huf = Decimal.Round(rub / codeToRate["NOK"], 3);
            textBox2.Text = huf.ToString();
        }
    }
}