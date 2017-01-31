Imports System.IO
Public Class Gruppendatenbank




    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If File.Exists(TextBox1.Text & ".txt") Then

            Button3.Enabled = False
            Dim Group() As String = File.ReadAllLines(TextBox1.Text & ".txt")
            TextBox5.Text = Group(0)
            TextBox2.Text = Group(1)
            TextBox3.Text = Group(2)
            TextBox4.Text = Group(3)
        Else
            TextBox5.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

            Button3.Enabled = True
        End If
        If File.Exists(TextBox1.Text & ".jpg") Then
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim response = MsgBox("Durch das Ändern gehen die Verknüpfung aller von dieser Gruppe bereits ausgeliehenen Gegenstände verloren!", MsgBoxStyle.YesNo, "Änderungen speichern?")
        If response = MsgBoxResult.Yes Then
            Dim Group(4) As String
            Group(0) = TextBox5.Text
            Group(1) = TextBox2.Text
            Group(2) = TextBox3.Text
            Group(3) = TextBox4.Text
            Group(4) = "endof"
            File.WriteAllLines(TextBox1.Text & ".txt", Group)
            MsgBox("Anderungen wurden gespeichert!", MsgBoxStyle.OkOnly, "Aktion erfolgreich!")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim response = MsgBox("Durch das Löschen gehen die Verknüpfung aller von dieser Gruppe bereits ausgeliehenen Gegenstände verloren!", MsgBoxStyle.YesNo, "Gruppe löschen?")
        If response = MsgBoxResult.Yes Then
            File.Delete(TextBox1.Text & ".txt")
            If File.Exists(TextBox1.Text & ".jpg") Then
                File.Delete(TextBox1.Text & ".jpg")
            End If
            MsgBox("Gruppe wurde gelöscht!", MsgBoxStyle.OkOnly, "Aktion erfolgreich!")
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Group(4) As String
        Group(0) = TextBox5.Text
        Group(1) = TextBox2.Text
        If TextBox3.Text = "" Then
            TextBox3.Text = "none"
        End If
        Group(2) = TextBox3.Text
        If TextBox4.Text = "" Then
            TextBox4.Text = "none"
        End If
        Group(3) = TextBox4.Text
        Group(4) = "endof"
        File.WriteAllLines(TextBox1.Text & ".txt", Group)
        MsgBox("Gruppe wurde erstellt!", MsgBoxStyle.OkOnly, "Aktion erfolgreich!")

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Gruppenliste.Show()
    End Sub
End Class