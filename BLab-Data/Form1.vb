Imports System.IO

Public Class Form1
    Dim Gruppe As Integer = 0
    Dim Aktion As String = "NONE"
    Dim chem As String = "0"
    Dim endof As Boolean = False
    Dim endofline As Integer = 0
    Dim Buttonask As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Focus()

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            Gruppe = TextBox1.Text
            TextBox1.Text = ""
            Dim Gruppeninfo() As String = IO.File.ReadAllLines(Gruppe & ".txt")
            Label3.Text = Gruppeninfo(0)
            Label5.Text = Gruppeninfo(1)
            Label6.Text = Gruppeninfo(2)
            Label7.Text = Gruppeninfo(3)
            Panel4.Visible = True
            PictureBox2.Image = New Bitmap("2. Schritt.PNG")
            Dim chemzahl As Integer = 3
            Do Until endof = True
                chemzahl = chemzahl + 1
                If Not Gruppeninfo(chemzahl) = "endof" Then
                    Dim Cheminfo() As String = IO.File.ReadAllLines(Gruppeninfo(chemzahl) & ".txt")
                    ListBox1.Items.Add(Cheminfo(0))
                ElseIf Gruppeninfo(chemzahl) = "endof" Then
                    endof = True
                End If
            Loop
            endofline = chemzahl + 1
            Label8.Text = "Bereits ausgeliehen (" & chemzahl - 4 & ") :"
            If File.Exists(TextBox1.Text & ".jpg") Then
                'PictureBox10.Image = Image.FromFile(TextBox1.Text & ".jpg")
            Else
                'PictureBox10.Image = Nothing
            End If
            Panel2.Visible = True
            TextBox2.Focus()
        End If
    End Sub


    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown

        If e.KeyData = Keys.Enter Then
            If TextBox2.Text = 102 Then
                Aktion = "rein"
            ElseIf TextBox2.Text = 103 Then
                Aktion = "raus"
            ElseIf TextBox2.Text = 101 Then
                Close()
            ElseIf TextBox2.Text = 104 Then
                Application.Restart()
            End If
            Aktionsauswahl()
        End If

    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Aktion = "rein"
        Aktionsauswahl()
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Aktion = "raus"
        Aktionsauswahl()
    End Sub

    Private Sub Aktionsauswahl()
        If Not Aktion = "NONE" Then
            TextBox2.Text = ""
            If Aktion = "rein" Then
                PictureBox8.Image = New Bitmap("rücknahme g.PNG")
                Label9.Text = "Artikel zum Ausleihen:"
            ElseIf Aktion = "raus" Then
                PictureBox7.Image = New Bitmap("ausleihen g.PNG")
                Label9.Text = "Artikel zur Rückgabe:"
                Button3.Enabled = True
            End If
        End If
        Panel3.Visible = True
        TextBox3.Focus()
        PictureBox3.Image = New Bitmap("3. Schritt.PNG")

    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyData = Keys.Enter Then
            If TextBox3.Text = 104 Then
                Application.Restart()
            ElseIf TextBox3.Text = 101 Then
                Close()
            ElseIf TextBox3.Text < 1000 And TextBox3.Text > 800 Then
                If Aktion = "rein" Then
                    Dim Chem() As String = IO.File.ReadAllLines(TextBox3.Text & ".txt")
                    ListBox2.Items.Add(Chem(0))
                    If Chem(2) = 0 Then

                    ElseIf Chem(2) = 1 Then
                        MsgBox("Dieser Artikel besitzt eine erhöhte Gefahreneinstufung!", MsgBoxStyle.OkOnly, "Gefahr!")
                    End If
                    Dim Chemgruppe() As String = File.ReadAllLines(Gruppe & ".txt")
                    Chemgruppe(endofline - 1) = TextBox3.Text
                    File.WriteAllLines(Gruppe & ".txt", Chemgruppe)
                    My.Computer.FileSystem.WriteAllText(Gruppe & ".txt", "endof", True)
                    endofline = endofline + 1
                ElseIf Aktion = "raus" Then
                    Dim Chem() As String = IO.File.ReadAllLines(TextBox3.Text & ".txt")
                    ListBox2.Items.Add(Chem(0))
                    Dim Chemgruppe() As String = File.ReadAllLines(Gruppe & ".txt")
                    Dim LErg As String = "weiter"
                    Dim chemline As Integer = 3
                    Do While LErg = "weiter"
                        chemline = chemline + 1
                        If Chemgruppe(chemline) = TextBox3.Text Then
                            Zeileloeschen(Gruppe & ".txt", chemline)
                            LErg = "gefunden"
                        ElseIf Chemgruppe(chemline) = "endof" Then
                            LErg = "fertig"
                            MsgBox("Nicht gefunden!")
                        End If
                    Loop


                End If
                TextBox3.Text = ""
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Chemgruppe() As String = File.ReadAllLines(Gruppe & ".txt")
        Dim NewChemgruppe(4) As String
        NewChemgruppe(0) = Chemgruppe(0)
        NewChemgruppe(1) = Chemgruppe(1)
        NewChemgruppe(2) = Chemgruppe(2)
        NewChemgruppe(3) = Chemgruppe(3)
        NewChemgruppe(4) = "endof"
        File.WriteAllLines(Gruppe & ".txt", NewChemgruppe)
        ListBox2.Items.AddRange(ListBox1.Items)
        TextBox3.Focus()
    End Sub

    Public Sub Zeileloeschen(ByVal path As String, ByVal line As Integer)
        Dim lines As String() = File.ReadAllLines(path)
        Dim sw As New StreamWriter(path, False)
        For i As Integer = 0 To lines.Length - 1
            If Not i = line Then
                sw.WriteLine(lines(i))
            End If
        Next
        sw.Close()
        sw.Dispose()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Restart()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Artikelliste.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Gruppenliste.Show()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class
