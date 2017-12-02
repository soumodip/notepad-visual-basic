Imports System.IO
Imports System.Threading
Imports System.Drawing.Printing

Public Class Form1


    Private varcheckthread As Integer = 0
    Private varopentosavefile As Integer = 0
    Private filename As String = Nothing
    Private mythread As System.Threading.Thread
    Private checkfont As Integer = 0 'means only from now colors/font will change
    Private typefont As New Font("Arial", 11, FontStyle.Regular)


    Private Sub aboutsoumopad()
        MessageBox.Show("SoumoPad is a new and innovative " + Environment.NewLine + "Note-taking application made " + Environment.NewLine +
                          "free for all user around the world by Conste.", "About")
    End Sub

    Private Sub aboutdeveloper()
        MessageBox.Show("Soumodip Paul is a developer who " + Environment.NewLine + "works for Conste", "About")
    End Sub

    Private Sub AboutSoumopadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutSoumopadToolStripMenuItem.Click
        aboutsoumopad()
    End Sub

    Private Sub AboutDeveloperToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutDeveloperToolStripMenuItem.Click
        aboutdeveloper()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If varopentosavefile = 0 Then
                If Me.TextBox1.Text.Length > 0 Then
                    Dim sfd As New SaveFileDialog
                    sfd.Filter = "Text File (.txt) | *.txt"
                    sfd.FileName = "Untitled.txt"
                    If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Me.Text = "SoumoPad -" & Path.GetFileName(sfd.FileName)
                        System.IO.File.WriteAllText(sfd.FileName, TextBox1.Text.ToString)
                    End If
                Else
                    MessageBox.Show("Please Write Something !!!", "Save")
                End If
            ElseIf varopentosavefile = 1 Then
                System.IO.File.WriteAllText(filename, TextBox1.Text.ToString)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Me.ofd.Filter = "Text File (.txt) | *.txt"
        If Me.ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.Text = "SoumoPad -" & ofd.SafeFileName
            varopentosavefile = 1
            filename = Me.ofd.FileName
            Dim sr As New StreamReader(ofd.FileName)
            TextBox1.Text = sr.ReadToEnd
            sr.Dispose()
        End If
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If varcheckthread = 1 Then
            mythread.Abort()
        End If
    End Sub


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("Do you really want to exit?", "Close", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub


    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        mythread = New System.Threading.Thread(AddressOf showform)
        mythread.SetApartmentState(ApartmentState.STA)
        mythread.Start()
    End Sub

    Private Sub showform()
        Dim formnew As New Form1
        formnew.ShowDialog()
        formnew.checkthread(1)
    End Sub

    Private Sub checkthread(ByVal var As Integer)
        varcheckthread = 1
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "SoumoPad - Untitled.txt"
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        Me.TextBox1.Clear()
    End Sub

    Private Sub FontsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontsToolStripMenuItem.Click
        Dim fd As New FontDialog
        Dim str As String = ""
        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            If checkfont = 0 Then
                typefont = fd.Font
                TextBox1.ForeColor = fd.Color
                TextBox1.Font = fd.Font
            End If
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub


    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        PrintDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString(TextBox1.Text, typefont, Brushes.Black, 100, 100)
    End Sub

End Class
