Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI
Imports DevExpress.DataAccess.Excel
Imports DevExpress.XtraReports.Configuration

Namespace BindingReportToCsvFile

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Create an empty report.
            Dim report As XtraReport = New XtraReport()
            ' Create a new Excel data source.
            Dim excelDataSource As ExcelDataSource = New ExcelDataSource()
            excelDataSource.FileName = "..//..//Northwind.csv"
            ' Specify import settings.
            Dim csvSourceOptions As CsvSourceOptions = New CsvSourceOptions()
            csvSourceOptions.DetectEncoding = True
            csvSourceOptions.DetectNewlineType = True
            csvSourceOptions.DetectValueSeparator = True
            excelDataSource.SourceOptions = csvSourceOptions
            ' Define the data source schema.
            Dim fieldCategoryID As FieldInfo = New FieldInfo With {.Name = "CategoryID", .Type = GetType(Double), .Selected = False}
            Dim fieldCategoryName As FieldInfo = New FieldInfo With {.Name = "CategoryName", .Type = GetType(String)}
            Dim fieldDescription As FieldInfo = New FieldInfo With {.Name = "Description", .Type = GetType(String)}
            ' Add the created fields to the data source schema in the order that matches the column order in the source file.
            excelDataSource.Schema.AddRange(New FieldInfo() {fieldCategoryID, fieldCategoryName, fieldDescription})
            ' Assign the data source to the report.
            report.DataSource = excelDataSource
            ' Add a detail band to the report.
            Dim detailBand As DetailBand = New DetailBand()
            detailBand.Height = 50
            report.Bands.Add(detailBand)
            ' Create a new label.
            Dim label As XRLabel = New XRLabel()
            ' Specify the label's binding depending on the data binding mode.
            If Settings.Default.UserDesignerOptions.DataBindingMode = DataBindingMode.Bindings Then
                label.DataBindings.Add("Text", Nothing, "CategoryName")
            Else
                label.ExpressionBindings.Add(New ExpressionBinding("BeforePrint", "Text", "[CategoryName]"))
            End If

            ' Add the label to the detail band.
            detailBand.Controls.Add(label)
            ' Show the report's print preview.
            report.ShowPreview()
        End Sub
    End Class
End Namespace
