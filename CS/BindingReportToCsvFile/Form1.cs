using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Excel;

namespace BindingReportToCsvFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an empty report.
            XtraReport report = new XtraReport();

            // Create a new Excel data source.
            ExcelDataSource excelDataSource = new ExcelDataSource();
            excelDataSource.FileName = "..//..//Northwind.csv";

            // Specify import settings.
            CsvSourceOptions csvSourceOptions = new CsvSourceOptions();
            csvSourceOptions.DetectEncoding = true;
            csvSourceOptions.DetectNewlineType = true;
            csvSourceOptions.DetectValueSeparator = true;
            excelDataSource.SourceOptions = csvSourceOptions;

            // Define the data source schema.
            FieldInfo fieldCategoryID = new FieldInfo { Name = "CategoryID", Type = typeof(double), Selected = false };
            FieldInfo fieldCategoryName = new FieldInfo { Name = "CategoryName", Type = typeof(string) };
            FieldInfo fieldDescription = new FieldInfo { Name = "Description", Type = typeof(string) };
            // Add the created fields to the data source schema in the order that matches the column order in the source file.
            excelDataSource.Schema.AddRange(new FieldInfo[] { fieldCategoryID, fieldCategoryName, fieldDescription });

            // Assign the data source to the report.
            report.DataSource = excelDataSource;

            // Add a detail band to the report.
            DetailBand detailBand = new DetailBand();
            detailBand.Height = 50;
            report.Bands.Add(detailBand);

            // Add a label to the detail band.
            XRLabel label = new XRLabel();
            label.DataBindings.Add("Text", null, "CategoryName");
            detailBand.Controls.Add(label);

            // Show the report's print preview.
            report.ShowPreview();

        }
    }
}
