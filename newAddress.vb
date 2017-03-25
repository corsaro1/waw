Imports System.IO
Imports System.Net
Imports NBitcoin
Imports Newtonsoft.Json.Linq
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Public Class newAddress
    Dim testoxxxx As String
    Dim pubkeyx As String

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim mnemo As New Mnemonic(Wordlist.English, WordCount.Twelve)
        ' Dim hdRoot As ExtKey = mnemo.DeriveExtKey("my password")
        'Console.WriteLine(mnemo)
        ' MsgBox(mnemo.ToString)
        TextBox11.Text = mnemo.ToString
        '  Console.WriteLine(hdRoot)

        'hdRoot = mnemo.DeriveExtKey("my password")

        '  MsgBox(hdRoot.ToString)
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        '  Dim url As String = "https://api.arknode.net/api/accounts/generatePublicKey"
        '  Dim url As String = "https://api.arknode.net/api/accounts/open"
        '  Dim url As String = url1 + "api/accounts/open"


        Dim url As String = "https://api.arknode.net/api/transactions"

        If Lisk.RadioButton1.Checked = True Then
            url = "https://api.arknode.net/api/transactions"


        End If
        If Lisk.RadioButton2.Checked = True Then
            url = "https://94.23.250.111/api/transactions"

        End If
        If Lisk.RadioButton3.Checked = True Then
            url = "http://127.0.0.1:4001api/accounts/open"

        End If
        If Lisk.RadioButton4.Checked = True Then
            url = "https://liskwallet.punkrock.me/api/accounts/open"

        End If
        If Lisk.RadioButton5.Checked = True Then
            url = "https://lisk-login.vipertkd.com/api/accounts/open"

        End If
        If Lisk.RadioButton6.Checked = True Then
            url = "https://lisk.delegates.site/api/accounts/open"

        End If
        If Lisk.RadioButton7.Checked = True Then
            url = "https://wallet.lisknode.io/api/accounts/open"

        End If
        If Lisk.RadioButton8.Checked = True Then
            url = "https://wallet.mylisk.com/api/accounts/open"

        End If

        If Lisk.RadioButton8.Checked = True Then
            url = "https://login.liskwallet.net/api/accounts/open"

        End If



        '/api/accounts/open
        ' MsgBox(url)

        Dim request As HttpWebRequest

        Dim responsex As HttpWebResponse = Nothing

        '    Dim readerx As StreamReader

        '  request = DirectCast(WebRequest.Create("https://api.arknode.net/api/accounts?address=" & senderId), HttpWebRequest)
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
        ' prompt = "Hello there. What's your seed?"
        ' seed = InputBox(prompt, title, defaultResponse)
        ' If seed Is "" Then GoTo fooerror
        seed = mnemo.ToString
        ' seedx = seed
        '  Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seed & Chr(34) & "}"
        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seed & Chr(34) & "," & Chr(34) & "amount" & Chr(34) & ":" & "1" & "," & Chr(34) & "recipientId" & Chr(34) & ":" & Chr(34) & "AbEtS8VA4LA1wgJWf4wA3JKyZrfvimp4Ht" & Chr(34) & "}"

        '  MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

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
        ' MsgBox(result)
        Dim jResults2 As Object = JObject.Parse(result)
        '  Dim testo2 As String = If(jResults2("account")("publicKey") Is Nothing, "", jResults2("account")("publicKey").ToString())
        Dim testo2 As String = If(jResults2("error") Is Nothing, "", jResults2("error").ToString())



        '     Dim jResults3 As Object = JObject.Parse(result)
        '   Dim testo3 As String = If(jResults3("account")("address") Is Nothing, "", jResults3("account")("address").ToString())

        TextBox13.Text = testo2.Replace("Account does not have enough ARK: ", "").Replace(" balance: 0", "")
        testoxxxx = testo2.Replace("Account does not have enough ARK: ", "").Replace(" balance: 0", "")
        MsgBox("New address is " & testoxxxx) ' mia pubkey


        pubkey()


        ' MsgBox("your pubKey is " & testo2) ' mia pubkey
        TextBox12.Text = pubkeyx



        If Not response Is Nothing Then response.Close()
        '   pubkeyx = testo2

        Label4.Visible = True
        TextBox11.Visible = False
        Label1.Visible = True
        Label1.Text = "***************************************"

        Label3.Visible = True
        TextBox12.Visible = True

        Label2.Visible = True
        TextBox13.Visible = True

        Label23.Visible = True
        'Label3.Visible = False

        Button25.Visible = True

        '   MsgBox(result)

        '   End Try
        '   Dim Save As New SaveFileDialog()

        '   Dim myStreamWriter As System.IO.StreamWriter

        '   Save.Filter = "Text [*.txt*]|*.txt|All Files [*.*]|*.*"

        '    Save.CheckPathExists = True
        '   Save.Title = "Save File"
        '  Save.FileName = testo3
        '' Save.ShowDialog(Me)
        '   Try
        'myStreamWriter = System.IO.File.AppendText(Save.FileName)
        ' myStreamWriter.Write("Secret:")
        'myStreamWriter.Write(vbCrLf)
        'myStreamWriter.Write(TextBox11.Text)
        'myStreamWriter.Write(vbCrLf)
        'myStreamWriter.Write("publicKey:")
        'myStreamWriter.Write(vbCrLf)
        'myStreamWriter.Write(TextBox12.Text)
        'myStreamWriter.Write(vbCrLf)
        '  myStreamWriter.Write("address:")
        '    myStreamWriter.Write(vbCrLf)
        '   myStreamWriter.Write(TextBox13.Text)
        ' myStreamWriter.Flush()
        ' myStreamWriter.Dispose()

        '' If Windows.Forms.DialogResult.Yes Is True Then


        '  Catch ex As Exception
        '  End Try

        ' If Save.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
        ' If Save.ShowDialog = Windows.Forms.DialogResult.Cancel Then
        'MsgBox("You have not saved the secret of this newly generated address" + vbCrLf + "Remember to Save carefully the Secret before using this new generated address ")
        ' Button25.Text = "IMPORTANT! - Save Secret"
        '   Else
        '  MsgBox("Secret has been saved on your PC. Kindly make different copies of this file to not lost your LISK in case of hdd failure")
        '   End If



        Button25.Enabled = True
        Button25.PerformClick()




fooerror:




    End Sub



    Private Function ValidateRemoteCertificate(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        'Throw New NotImplementedException()
        Return True
    End Function

    Private Sub pubkey()
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        Dim url1 As String = "https://api.arknode.net/"

        '  Dim url As String = "https://api.arknode.net/api/accounts/generatePublicKey"
        'Dim url As String = url1 + "api/accounts/generatePublicKey"
        '/api/accounts/getPublicKey
        Dim url As String = url1 + "api/accounts/getPublicKey"
        Dim senderId As String = Lisk.senderId

        Dim request As HttpWebRequest

        Dim responsex As HttpWebResponse = Nothing

        Dim readerx As StreamReader

        request = DirectCast(WebRequest.Create(url & "?address=" & testoxxxx), HttpWebRequest)
        responsex = DirectCast(request.GetResponse(), HttpWebResponse)
        readerx = New StreamReader(responsex.GetResponseStream())
        'Try
        Dim rawresp As String
        rawresp = readerx.ReadToEnd()
        Dim jResults As Object = JObject.Parse(rawresp)
        Dim testo As String = If(jResults("publicKey") Is Nothing, "", jResults("publicKey").ToString())
        ' Dim jResults2 As Object = JObject.Parse(testo)
        'Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())
        MsgBox("your pubKey is " & testo) ' mia pubkey
        pubkeyx = testo

        'Catch

        '  End Try



        '  Dim seed As Object
        ' prompt = "Hello there. What's your secret?"
        ' seed = InputBox(prompt, title, defaultResponse)
        ' If seed Is "" Then GoTo fooerror
        ' seedx = seed
        '  Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seed & Chr(34) & "}"

        ''  MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SEED" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        '  Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        '  request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        '  request.Method = "GET"
        '  request.ContentType = "application/json"
        '  request.ContentLength = arr.Length
        '  ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        '  Dim dataStream As Stream = request.GetRequestStream()
        '  dataStream.Write(arr, 0, arr.Length)
        '  dataStream.Close()

        '  Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)


        '' https://developer.yahoo.com/dotnet/howto-xml_vb.html

        '  Dim reader As StreamReader
        '  Dim result As String

        '  response = DirectCast(request.GetResponse(), HttpWebResponse)

        '   reader = New StreamReader(response.GetResponseStream())

        ' result = reader.ReadToEnd()

        '  Dim jResults2 As Object = JObject.Parse(result)
        '   Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())
        ' MsgBox("your pubKey is " & testo2) ' mia pubkey
        '  If Not response Is Nothing Then response.Close()
        ' pubkeyx = testo2

        ''   MsgBox(result)





fooerror:
    End Sub


    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click
        TextBox11.Visible = False
        Label1.Visible = True
        Label23.Visible = True
        Label24.Visible = False
    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click
        TextBox11.Visible = True
        Label1.Visible = False
        TextBox11.ReadOnly = True
        Label24.Visible = True
        Label23.Visible = False
    End Sub

    Private Sub newAddress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rect As Rectangle = Screen.PrimaryScreen.WorkingArea
        'Divide the screen in half, and find the center of the form to center it
        Me.Top = (rect.Height / 2) - (Me.Height / 2)
        Me.Left = (rect.Width / 2) - (Me.Width / 2)

        Me.Button24.PerformClick()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Dim Save As New SaveFileDialog()

        Dim myStreamWriter As System.IO.StreamWriter

        Save.Filter = "Text [*.txt*]|*.txt|All Files [*.*]|*.*"

        Save.CheckPathExists = True
        Save.Title = "Save File"
        Save.FileName = TextBox13.Text
        '  Save.ShowDialog(Me)

        If Save.ShowDialog = Windows.Forms.DialogResult.Cancel = True Then
            MsgBox("You have not saved the secret of this newly generated address" + vbCrLf + "Remember to Save carefully the Secret before using this new generated address ")
            Button25.Text = "IMPORTANT! - Save Secret"
        Else
            'Save.ShowDialog(Me)
            Try
                myStreamWriter = System.IO.File.AppendText(Save.FileName)
                myStreamWriter.Write("Secret:")
                myStreamWriter.Write(vbCrLf)
                myStreamWriter.Write(TextBox11.Text)
                myStreamWriter.Write(vbCrLf)
                myStreamWriter.Write("publicKey:")
                myStreamWriter.Write(vbCrLf)
                myStreamWriter.Write(TextBox12.Text)
                myStreamWriter.Write(vbCrLf)
                myStreamWriter.Write("address:")
                myStreamWriter.Write(vbCrLf)
                myStreamWriter.Write(TextBox13.Text)
                myStreamWriter.Flush()
                myStreamWriter.Dispose()

            Catch ex As Exception
            End Try
        End If

    End Sub
End Class