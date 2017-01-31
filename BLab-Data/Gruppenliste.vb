Imports System.IO
Imports Microsoft.Office.Interop
Public Class Gruppenliste

    Private Sub Gruppenliste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Chemzähler As Integer = 200
        Do Until Chemzähler = 299
            If File.Exists(Chemzähler & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Chemzähler & ".txt")
                With ListView1.Items.Add(Chemzähler)
                    .SubItems.Add(Chem(1))
                    .SubItems.Add(Chem(2))
                    .SubItems.Add(Chem(3))
                    .SubItems.Add(Chem(0))
                End With

            End If
            Chemzähler = Chemzähler + 1
        Loop
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

    End Sub



    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            Dim Chem() As String = File.ReadAllLines(ListView1.SelectedItems(0).Text & ".txt")
            TextBox1.Text = Chem(0)
            TextBox2.Text = Chem(1)
            TextBox3.Text = Chem(2)
            TextBox4.Text = Chem(3)
        Catch ex As Exception
        End Try
        Button1.Enabled = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim EChem() As String = File.ReadAllLines(ListView1.SelectedItems(0).Text & ".txt")
        EChem(0) = TextBox1.Text
        EChem(1) = TextBox2.Text
        EChem(2) = TextBox3.Text
        EChem(3) = TextBox4.Text
        File.WriteAllLines(ListView1.SelectedItems(0).Text & ".txt", EChem)
        ListView1.Items.Clear()
        Dim Chemzähler As Integer = 200
        Do Until Chemzähler = 299
            If File.Exists(Chemzähler & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Chemzähler & ".txt")
                With ListView1.Items.Add(Chemzähler)
                    .SubItems.Add(Chem(1))
                    .SubItems.Add(Chem(2))
                    .SubItems.Add(Chem(3))
                    .SubItems.Add(Chem(0))
                End With

            End If
            Chemzähler = Chemzähler + 1
        Loop
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Button1.Enabled = False
    End Sub

    Private Sub ListView1_LostFocus(sender As Object, e As EventArgs) Handles ListView1.LostFocus
        If TextBox1.Focused = False And TextBox2.Focused = False And TextBox3.Focused = False And TextBox4.Focused = False Then
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim vitem As ListViewItem = ListView1.FindItemWithText(TextBox5.Text, True, 0)
        ListView1.Select()
        Try
            vitem.Selected = True
        Catch
        End Try
        TextBox5.Text = ""
    End Sub



    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim vitem As ListViewItem = ListView1.FindItemWithText(TextBox5.Text, True, 0)
            ListView1.Select()
            Try
                vitem.Selected = True
            Catch
            End Try
            TextBox5.Text = ""
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Gruppendatenbank.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        MsgBox("Um die Änderungen zu speichern wird das Programm nun neu gestartet!", MsgBoxStyle.OkOnly, "Blab-Data Gruppendatenbank")
        Application.Restart()
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        'Release an automation object
        Try
            Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        SaveFileDialog1.Title = "Save Excel File"
        SaveFileDialog1.Filter = "Excel files (*.xls)|*.xls|Excel Files (*.xlsx)|*.xslx"
        SaveFileDialog1.ShowDialog()
        'exit if no file selected
        If SaveFileDialog1.FileName = "" Then
            Exit Sub
        End If
        'create objects to interface to Excel
        Dim xls As New Excel.Application
        Dim book As Excel.Workbook
        Dim sheet As Excel.Worksheet
        'create a workbook and get reference to first worksheet
        xls.Workbooks.Add()
        book = xls.ActiveWorkbook
        sheet = book.ActiveSheet
        'step through rows and columns and copy data to worksheet
        Dim row As Integer = 1
        Dim col As Integer = 1
        For Each item As ListViewItem In ListView1.Items
            For i As Integer = 0 To item.SubItems.Count - 1
                sheet.Cells(row, col) = item.SubItems(i).Text
                col = col + 1
            Next
            row += 1
            col = 1
        Next
        book.SaveAs(SaveFileDialog1.FileName)
        xls.Workbooks.Close()
        xls.Quit()
        releaseObject(sheet)
        releaseObject(book)
        releaseObject(xls)
    End Sub
End Class