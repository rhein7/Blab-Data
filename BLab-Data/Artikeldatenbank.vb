Imports System.IO
Public Class Artikeldatenbank
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If File.Exists(TextBox1.Text & ".txt") Then
            Dim Chem() As String = File.ReadAllLines(TextBox1.Text & ".txt")
            TextBox3.Text = Chem(0)
            TextBox2.Text = Chem(1)
            If Chem(2) = 0 Then
                RadioButton3.Checked = True
            ElseIf Chem(2) = 1 Then
                RadioButton4.Checked = True
            End If
            If Chem(3) = 0 Then
                RadioButton1.Checked = True
            ElseIf Chem(3) = 1 Then
                RadioButton2.Checked = True
            End If
            Button5.Enabled = True
            Button6.Enabled = True
        Else
            TextBox3.Text = ""
            TextBox2.Text = ""
            Button5.Enabled = False
            Button6.Enabled = False
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            TextBox2.Text = TextBox2.Text + 1
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox2.Text = TextBox2.Text - 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim Chem() As String = File.ReadAllLines(TextBox1.Text & ".txt")
        Chem(0) = TextBox3.Text
        Chem(1) = TextBox2.Text
        If RadioButton3.Checked = True Then
            Chem(2) = 0
        ElseIf RadioButton4.Checked = True Then
            Chem(2) = 1
        End If
        If RadioButton1.Checked = True Then
            Chem(3) = 0
        ElseIf RadioButton2.Checked = True Then
            Chem(3) = 1
        End If
        File.WriteAllLines(TextBox1.Text & ".txt", Chem)
        MsgBox("Das Objekt wurde gespeichert!", MsgBoxStyle.OkOnly, "Vorgang erfolgreich")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not TextBox1.Text = "" And Not TextBox2.Text = "" And Not TextBox3.Text = "" Then
            If Not File.Exists(TextBox1.Text & ".txt") Then
                Dim NewChem(3) As String
                NewChem(0) = TextBox3.Text
                NewChem(1) = TextBox2.Text
                If RadioButton3.Checked = True Then
                    NewChem(2) = 0
                ElseIf RadioButton4.Checked = True Then
                    NewChem(2) = 1
                End If
                If RadioButton1.Checked = True Then
                    NewChem(3) = 0
                ElseIf RadioButton2.Checked = True Then
                    NewChem(3) = 1
                End If
                File.WriteAllLines(TextBox1.Text & ".txt", NewChem)
                MsgBox("Das Objekt wurde hinzugefügt!", MsgBoxStyle.OkOnly, "Vorgang erfolgreich")
            Else
                MsgBox("Das Objekt existiert bereits!")
            End If
            TextBox1.Text = ""
        Else
            MsgBox("Es müssen alle Felder ausgefüllt werden!")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MsgBox("Um die Änderungen zu speichern wird das Programm nun neu gestartet!", MsgBoxStyle.OkOnly, "Blab-Data Artikeldatenbank")
        Application.Restart()
    End Sub
End Class