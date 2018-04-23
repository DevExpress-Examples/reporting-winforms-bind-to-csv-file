Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI
Imports DevExpress.DataAccess.Excel

Namespace BindingReportToCsvFile
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
            ' Create an empty report.
            Dim report As New XtraReport()

            ' Create a new Excel data source.
            Dim excelDataSource As New ExcelDataSource()
            excelDataSource.FileName = "..//..//Northwind.csv"

            ' Specify import settings.
            Dim csvSourceOptions As New CsvSourceOptions()
            csvSourceOptions.DetectEncoding = True
            csvSourceOptions.DetectNewlineType = True
            csvSourceOptions.DetectValueSeparator = True
            excelDataSource.SourceOptions = csvSourceOptions

            ' Define the data source schema.
            Dim fieldCategoryID As FieldInfo = New FieldInfo With {.Name = "CategoryID", .Type = GetType(Double), .Selected = False}
            Dim fieldCategoryName As FieldInfo = New FieldInfo With {.Name = "CategoryName", .Type = GetType(String)}
            Dim fieldDescription As FieldInfo = New FieldInfo With {.Name = "Description", .Type = GetType(String)}
            ' Add the created fields to the data source schema in the order that matches the column order in the source file.
            excelDataSource.Schema.AddRange(New FieldInfo() { fieldCategoryID, fieldCategoryName, fieldDescription })

            ' Assign the data source to the report.
            report.DataSource = excelDataSource

            ' Add a detail band to the report.
            Dim detailBand As New DetailBand()
            detailBand.Height = 50
            report.Bands.Add(detailBand)

            ' Add a label to the detail band.
            Dim label As New XRLabel()
            label.DataBindings.Add("Text", Nothing, "CategoryName")
            detailBand.Controls.Add(label)

            ' Show the report's print preview.
            report.ShowPreview()

        End Sub
    End Class
End Namespace
