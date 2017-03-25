Imports System.IO
Imports System.Net
Imports NBitcoin
Imports Newtonsoft.Json.Linq
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System
Imports System.Linq

Public Class secondSecret
    Private Function ValidateRemoteCertificate(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        'Throw New NotImplementedException()
        Return True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        '  Dim mnemo As New Mnemonic(Wordlist.English, WordCount.Twelve)
        ' Dim hdRoot As ExtKey = mnemo.DeriveExtKey("my password")
        'Console.WriteLine(mnemo)
        ' MsgBox(mnemo.ToString)
        '  TextBox1.Text = mnemo.ToString
        '  Console.WriteLine(hdRoot)

        'hdRoot = mnemo.DeriveExtKey("my password")

        '  MsgBox(hdRoot.ToString)
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Button2.Visible = True

        '   Button2.PerformClick()

        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        '  Dim url As String = "https://wallet.shiftnrg.org/api/accounts/generatePublicKey"
        '  Dim url As String = "https://wallet.shiftnrg.org/api/accounts/open"
        '  Dim url As String = url1 + "api/accounts/open"

        Dim url As String = "https://api.arknode.net/api/signatures"

        If RadioButton1.Checked = True Then




            If Lisk.RadioButton1.Checked = True Then
                url = "https://api.arknode.net/api/signatures"


            End If
            If Lisk.RadioButton2.Checked = True Then
                url = "https://94.23.250.111/api/signatures"

            End If
            If Lisk.RadioButton3.Checked = True Then
                url = "http://127.0.0.1:4001api/signatures"

            End If
            If Lisk.RadioButton4.Checked = True Then
                url = "https://liskwallet.punkrock.me/api/signatures"

            End If
            If Lisk.RadioButton5.Checked = True Then
                url = "https://lisk-login.vipertkd.com/api/signatures"

            End If
            If Lisk.RadioButton6.Checked = True Then
                url = "https://lisk.delegates.site/api/signatures"

            End If
            If Lisk.RadioButton7.Checked = True Then
                url = "https://wallet.lisknode.io/api/signatures"

            End If
            If Lisk.RadioButton8.Checked = True Then
                url = "https://wallet.mylisk.com/api/signatures"

            End If

            If Lisk.RadioButton8.Checked = True Then
                url = "https://login.liskwallet.net/api/signatures"

            End If

        Else
            url = "https://testnet.lisk.io/api/signatures"
            ' url = "http://127.0.0.1:9405/api/signatures"
        End If


        '/api/accounts/open
        ' MsgBox(url)

        Dim request As HttpWebRequest

        Dim responsex As HttpWebResponse = Nothing

        '    Dim readerx As StreamReader

        '  request = DirectCast(WebRequest.Create("https://wallet.shiftnrg.org/api/accounts?address=" & senderId), HttpWebRequest)
        '   responsex = DirectCast(request.GetResponse(), HttpWebResponse)
        '  readerx = New StreamReader(responsex.GetResponseStream())
        '  Try
        'Dim rawresp As String
        '  rawresp = readerx.ReadToEnd()
        '  Dim jResults As Object = JObject.Parse(rawresp)
        '  Dim testo As String = If(jResults("account") Is Nothing, "", jResults("account").ToString())
        '  Dim jResults2 As Object = JObject.Parse(testo)
        '  Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())
        ' MsgBox("your pubKey is " & testo2) ' mia pubkey
        '  Catch

        Dim seed As Object
        Dim seed2 As Object
        prompt = "Hello there. What's your first secret?"
        seed = InputBox(prompt, title, defaultResponse)
        If seed Is "" Then GoTo fooerror


        prompt = "Paste here the second secret you choose"
        seed2 = InputBox(prompt, title, defaultResponse)
        If seed2 = TextBox1.Text Then

        Else
            MsgBox("your pasted seed is not the same of the generated one. Exiting")
            Me.Hide()
            GoTo fooerror

            ' Me.Hide()
            ' Me.Close()
            'Me.Dispose()

        End If

        If seed2 Is "" Then GoTo fooerror


        'seed2 = TextBox1.Text
        ' seedx = seed
        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seed & Chr(34) & "," & Chr(34) & "secondSecret" & Chr(34) & ":" & Chr(34) & seed2 & Chr(34) & "}"

        MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        request.Method = "PUT"
        request.ContentType = "application/json"
        request.ContentLength = arr.Length
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(arr, 0, arr.Length)
        dataStream.Close()

        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)


        '' https://developer.yahoo.com/dotnet/howto-xml_vb.html

        Dim reader As StreamReader
        Dim result As String

        response = DirectCast(request.GetResponse(), HttpWebResponse)

        reader = New StreamReader(response.GetResponseStream())

        result = reader.ReadToEnd()
        MsgBox(result)
        Dim jResults2 As Object = JObject.Parse(result)
        Dim testo2 As String = If(jResults2("success") Is Nothing, "", jResults2("success").ToString())
        MsgBox(testo2)
        If testo2 = "True" Then
            MsgBox("secondSecret aggiunta con successo")

        Else
            MsgBox("operazione fallita")
        End If
        'MsgBox(testo2) ' mia pubkey
        'TextBox2.Text = testo2

        '  Dim jResults3 As Object = JObject.Parse(result)
        ' Dim testo3 As String = If(jResults3("account")("address") Is Nothing, "", jResults3("account")("address").ToString())

        ' TextBox3.Text = testo3
        ' MsgBox("New address is " & testo3) ' mia pubkey


        If Not response Is Nothing Then response.Close()
        ' Button2.PerformClick()
        '   pubkeyx = testo2
fooerror:
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Save As New SaveFileDialog()

        Dim myStreamWriter As System.IO.StreamWriter

        Save.Filter = "Text [*.txt*]|*.txt|All Files [*.*]|*.*"

        Save.CheckPathExists = True
        Save.Title = "Save File"
        Save.FileName = "secondSecret"
        '  Save.ShowDialog(Me)

        If Save.ShowDialog = Windows.Forms.DialogResult.Cancel = True Then
            MsgBox("You have not saved the secret of this newly generated address" + vbCrLf + "Remember to Save carefully the Secret before using this new generated address ")
            Button2.Text = "IMPORTANT! - Save Secret"
        Else
            'Save.ShowDialog(Me)
            Try
                myStreamWriter = System.IO.File.AppendText(Save.FileName)
                myStreamWriter.Write("secondSecret:")
                myStreamWriter.Write(vbCrLf)
                myStreamWriter.Write(TextBox1.Text)
                '  myStreamWriter.Write(vbCrLf)
                ' myStreamWriter.Write("publicKey:")
                ' myStreamWriter.Write(vbCrLf)
                ' myStreamWriter.Write(TextBox12.Text)
                ' myStreamWriter.Write(vbCrLf)
                ' myStreamWriter.Write("address:")
                '  myStreamWriter.Write(vbCrLf)
                '  myStreamWriter.Write(TextBox13.Text)
                myStreamWriter.Flush()
                myStreamWriter.Dispose()

                Button1.Enabled = True
                Button2.Enabled = False
                Label3.Font = New Font(Label2.Font, FontStyle.Regular)
                Label4.Font = New Font(Label3.Font, FontStyle.Bold)

            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim mnemo As New Mnemonic(Wordlist.English, WordCount.Twelve)
        ' Dim hdRoot As ExtKey = mnemo.DeriveExtKey("my password")
        'Console.WriteLine(mnemo)
        ' MsgBox(mnemo.ToString)
        TextBox1.Text = mnemo.ToString
        Button1.Visible = True
        Button3.Visible = False
        Button2.Visible = True

        Button4.Visible = True
        Button2.Enabled = False
        Button1.Enabled = False
        Button4.Text = "Copy to Clipboard"
        Label1.Visible = True
        Label1.Text = "REMEMBER TO SAVE THIS SEED IN A SAFE PLACE. IF YOU LOST IT YOUR FUNDS WILL BE INACCESSIBLE"
        TextBox1.Visible = True
        TextBox1.ReadOnly = True
        Label2.Text = "1"
        Label3.Text = "2"
        Label4.Text = "3"
        Label2.Visible = True
        Label2.Font = New Font(Label2.Font, FontStyle.Bold)
        Label3.Visible = True
        Label4.Visible = True
        Button4.Enabled = True


    End Sub

    Private Sub secondSecret_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button3.Visible = True
        TextBox1.Visible = False
        Button1.Visible = False
        Button2.Visible = False
        Button4.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        On Error GoTo foerror
        Clipboard.Clear()
        Clipboard.SetText(TextBox1.Text)
        Button2.Enabled = True
        Button4.Enabled = False
        Label2.Font = New Font(Label2.Font, FontStyle.Regular)
        Label3.Font = New Font(Label3.Font, FontStyle.Bold)
foerror:

    End Sub
End Class