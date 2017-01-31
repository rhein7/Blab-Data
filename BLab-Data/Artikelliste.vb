Imports System.IO
Public Class Artikelliste

    Public Chemzähler As Integer = 900
    Private Sub Artikelliste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Do Until Chemzähler = 1000
            If File.Exists(Chemzähler & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Chemzähler & ".txt")
                If Chem(3) = 0 Then 'Chemikalie
                    ListBox2.Items.Add(Chem(0))
                ElseIf Chem(3) = 1 Then 'Objekt
                    ListBox1.Items.Add(Chem(0))
                End If
            End If
            Chemzähler = Chemzähler + 1
        Loop
        ListBox1.Sorted = True
        ListBox2.Sorted = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text <> String.Empty Then
            Dim index1 As Integer = ListBox1.FindString(TextBox2.Text)
            If index1 <> -1 Then
                ListBox1.SetSelected(index1, True)
            Else
                Dim index2 As Integer = ListBox2.FindString(TextBox2.Text)
                If index2 <> -1 Then
                    ListBox2.SetSelected(index2, True)
                Else
                    MsgBox("Die Suche ergab keine Treffer!", MsgBoxStyle.OkOnly, "BLab-Data Datenbank")
                End If
            End If
        Else
            If File.Exists(TextBox1.Text & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(TextBox1.Text & ".txt")
                Try
                    ListBox1.SelectedItem = Chem(0)
                    ListBox2.SelectedItem = Chem(0)
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If Not ListBox1.SelectedIndex = -1 Then
            ListBox2.SelectedIndex = -1
        End If
        Dim Auswahl As Integer = 900
        Do Until Auswahl = 1000
            If File.Exists(Auswahl & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Auswahl & ".txt")
                If Chem(0) = ListBox1.SelectedItem Then
                    TextBox3.Text = Chem(0)
                    TextBox4.Text = Chem(1)
                    Label9.Text = Auswahl
                    If Chem(2) = "0" Then 'Keine Gefahr
                        RadioButton1.Checked = True
                    ElseIf Chem(2) = "1" Then 'Gefahr
                        RadioButton2.Checked = True
                    End If
                    Panel3.Enabled = True
                    Button3.Enabled = True
                    Button2.Enabled = False
                    Exit Do
                End If
            End If
            Auswahl = Auswahl + 1
        Loop
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If Not ListBox2.SelectedIndex = -1 Then
            ListBox1.SelectedIndex = -1
        End If
        Dim Auswahl As Integer = 900
        Do Until Auswahl = 1000
            If File.Exists(Auswahl & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Auswahl & ".txt")
                If Chem(0) = ListBox2.SelectedItem Then
                    TextBox3.Text = Chem(0)
                    TextBox4.Text = Chem(1)
                    Label9.Text = Auswahl
                    If Chem(2) = "0" Then 'Keine Gefahr
                        RadioButton1.Checked = True
                    ElseIf Chem(2) = "1" Then 'Gefahr
                        RadioButton2.Checked = True
                    End If
                    Panel3.Enabled = True
                    Button2.Enabled = True
                    Button3.Enabled = False
                    Exit Do
                End If
            End If
            Auswahl = Auswahl + 1
        Loop
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If Not TextBox2.Text = "" And TextBox1.Text = "" Then
            Button1.Enabled = True
            Button1.ForeColor = ColorTranslator.FromWin32(RGB(247, 152, 75))
        Else
            Button1.Enabled = False
            Button1.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Not TextBox1.Text = "" And TextBox2.Text = "" Then
            Button1.Enabled = True
            Button1.ForeColor = ColorTranslator.FromWin32(RGB(247, 152, 75))
        Else
            Button1.Enabled = False
            Button1.ForeColor = Color.Black
        End If
    End Sub
    Public Function ItemExists(oControl As Object, ByVal sItem As String) As Boolean
        On Error Resume Next
        oControl.Text = sItem
        If Err.Number <> 0 Or oControl.ListIndex < 0 Then
            ItemExists = False
        Else
            ItemExists = True
        End If
        On Error GoTo 0
    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Artikeldatenbank.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        MsgBox("Um die Änderungen zu speichern wird das Programm nun neu gestartet!", MsgBoxStyle.OkOnly, "Blab-Data Artikeldatenbank")
        Application.Restart()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        My.Computer.FileSystem.DeleteFile(Label9.Text & ".txt")
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Chemzähler = 900
        Do Until Chemzähler = 1000
            If File.Exists(Chemzähler & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Chemzähler & ".txt")
                If Chem(3) = 0 Then 'Chemikalie
                    ListBox2.Items.Add(Chem(0))
                ElseIf Chem(3) = 1 Then 'Objekt
                    ListBox1.Items.Add(Chem(0))
                End If
            End If
            Chemzähler = Chemzähler + 1
        Loop
        ListBox1.Sorted = True
        ListBox2.Sorted = True
        MsgBox("Das Objekt wurde entfernt!", MsgBoxStyle.OkOnly, "Vorgang erfolgreich")
        ListBox1.SelectedIndex = 0

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim EChem() As String = File.ReadAllLines(Label9.Text & ".txt")
        EChem(0) = TextBox3.Text
        EChem(1) = TextBox4.Text

        If RadioButton1.Checked = True Then
            EChem(2) = 0
        ElseIf RadioButton2.Checked = True Then
            EChem(2) = 1
        End If
        File.WriteAllLines(Label9.Text & ".txt", EChem)
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Chemzähler = 900
        Do Until Chemzähler = 1000
            If File.Exists(Chemzähler & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Chemzähler & ".txt")
                If Chem(3) = 0 Then 'Chemikalie
                    ListBox2.Items.Add(Chem(0))
                ElseIf Chem(3) = 1 Then 'Objekt
                    ListBox1.Items.Add(Chem(0))
                End If
            End If
            Chemzähler = Chemzähler + 1
        Loop
        ListBox1.Sorted = True
        ListBox2.Sorted = True
        MsgBox("Das Objekt wurde gespeichert!", MsgBoxStyle.OkOnly, "BLab-Data Datenbank")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim EChem() As String = File.ReadAllLines(Label9.Text & ".txt")
        EChem(3) = "1"
        File.WriteAllLines(Label9.Text & ".txt", EChem)
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Chemzähler = 900
        Do Until Chemzähler = 1000
            If File.Exists(Chemzähler & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Chemzähler & ".txt")
                If Chem(3) = 0 Then 'Chemikalie
                    ListBox2.Items.Add(Chem(0))
                ElseIf Chem(3) = 1 Then 'Objekt
                    ListBox1.Items.Add(Chem(0))
                End If
            End If
            Chemzähler = Chemzähler + 1
        Loop
        ListBox1.Sorted = True
        ListBox2.Sorted = True
        ListBox1.SelectedItem = TextBox3.Text
        MsgBox("Das Objekt wurde verschoben!", MsgBoxStyle.OkOnly, "BLab-Data Datenbank")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim EChem() As String = File.ReadAllLines(Label9.Text & ".txt")
        EChem(3) = "0"
        File.WriteAllLines(Label9.Text & ".txt", EChem)
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Chemzähler = 900
        Do Until Chemzähler = 1000
            If File.Exists(Chemzähler & ".txt") Then
                Dim Chem() As String = File.ReadAllLines(Chemzähler & ".txt")
                If Chem(3) = 0 Then 'Chemikalie
                    ListBox2.Items.Add(Chem(0))
                ElseIf Chem(3) = 1 Then 'Objekt
                    ListBox1.Items.Add(Chem(0))
                End If
            End If
            Chemzähler = Chemzähler + 1
        Loop
        ListBox1.Sorted = True
        ListBox2.Sorted = True
        ListBox2.SelectedItem = TextBox3.Text
        MsgBox("Das Objekt wurde verschoben!", MsgBoxStyle.OkOnly, "BLab-Data Datenbank")
    End Sub
End Class