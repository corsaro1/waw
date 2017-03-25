Imports System.Net
Imports System.IO
Imports System.Security.Cryptography.X509Certificates



Public Class cosignForm

    Dim senderId As Object = Nothing


    Public Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ' Dim url As String = "https://api.arknode.net/api/multisignatures/sign"

        Dim url As String

        If RadioButton1.Checked = True Then
            '    url = "https://api.arknode.net/api/multisignatures/sign"



            If Lisk.RadioButton1.Checked = True Then
                url = "https://api.arknode.net/api/multisignatures/sign"

            End If
            If Lisk.RadioButton2.Checked = True Then
                url = "https://arkworld.info/api/multisignatures/sign"

            End If
            If Lisk.RadioButton3.Checked = True Then
                url = "http://127.0.0.1:4001/api/multisignatures/sign"

            End If
            If Lisk.RadioButton4.Checked = True Then
                url = "https://liskwallet.punkrock.me/api/multisignatures/sign"

            End If
            If Lisk.RadioButton5.Checked = True Then
                url = "https://lisk-login.vipertkd.com/api/multisignatures/sign"

            End If
            If Lisk.RadioButton6.Checked = True Then
                url = "https://lisk.delegates.site/api/multisignatures/sign"

            End If
            If Lisk.RadioButton7.Checked = True Then
                url = "https://wallet.lisknode.io/api/multisignatures/sign"

            End If
            If Lisk.RadioButton8.Checked = True Then
                url = "https://wallet.mylisk.com/api/multisignatures/sign"

            End If




        Else
            url = "https://testnet.lisk.io/api/multisignatures/sign"
        End If



        'http://stackoverflow.com/questions/812711/how-do-you-do-an-http-put

        Dim title As String = String.Empty
        Dim defaultResponse As String = String.Empty
        Dim prompt As String = String.Empty

        Dim rawresp As String


        Dim request As HttpWebRequest

        Dim response As HttpWebResponse = Nothing

        Dim reader As StreamReader

        request = DirectCast(WebRequest.Create("https://api.arknode.net/api/accounts?address=" & senderId), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())




        rawresp = reader.ReadToEnd()


        ' Dim jResults As Object = JObject.Parse(rawresp)
        '   Dim testo As String = If(jResults("account") Is Nothing, "", jResults("account").ToString())
        '  Dim jResults2 As Object = JObject.Parse(testo)
        ' Dim testo2 As String = If(jResults2("publicKey") Is Nothing, "", jResults2("publicKey").ToString())
        '   MsgBox("your pubKey is " & testo2) ' mia pubkey

        ' If jResults2("publicKey") Is Nothing Then MsgBox("xxx")

        '   pubkey()

        Dim seed As Object
        prompt = "What's your secret?"
        seed = InputBox(prompt, title, defaultResponse)
        If seed Is "" Then GoTo fooerror2

        '    request = DirectCast(WebRequest.Create("https://api.arknode.net/api/accounts/delegates/?address=" & senderId), HttpWebRequest)
        '  response = DirectCast(request.GetResponse(), HttpWebResponse)
        '  reader = New StreamReader(response.GetResponseStream())

        '   rawresp = reader.ReadToEnd()


        '   Dim jResultsb As Object = JObject.Parse(rawresp)
        ' Dim testob As String = If(jResultsb("delegates") Is Nothing, "", jResultsb("delegates").ToString())
        '
        '

        'Dim transactionId As Object
        ' prompt = "What's the tx id you want to co-sign?"
        ' transactionId = InputBox(prompt, title, defaultResponse)
        ' If transactionId Is "" Then GoTo fooerror2



        Dim transactionId As Object
        ' prompt = "What's the tx id you want to co-sign?"
        transactionId = TextBox1.Text
        If transactionId Is "" Then GoTo fooerror2




        '   Dim LineOfText As String



        Dim votelist As String = "all delegates here"

        Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seed & Chr(34) & "," & Chr(34) & "transactionId" & Chr(34) & ":" & Chr(34) & transactionId & Chr(34) & "}"

        '  Dim xml As String = "{" & Chr(34) & "secret" & Chr(34) & ":" & Chr(34) & seed & Chr(34) & "," & Chr(34) & "publicKey" & Chr(34) & ":" & Chr(34) & testo2 & Chr(34) & "," & Chr(34) & "delegates" & Chr(34) & ":[" & Chr(34) & "+" & pubkey1 & Chr(34) & "," & Chr(34) & "+" & pubkey2 & Chr(34) & "," & Chr(34) & "+" & pubkey3 & Chr(34) & "," & Chr(34) & "+" & pubkey4 & Chr(34) & "," & Chr(34) & "+" & pubkey5 & Chr(34) & "," & Chr(34) & "+" & pubkey6 & Chr(34) & "," & Chr(34) & "+" & pubkey7 & Chr(34) & "," & Chr(34) & "+" & pubkey8 & Chr(34) & "]" & "}"

        MsgBox("DO NOT SHARE THIS SCREEN. IT CONTAINS YOUR SECRET" & vbCrLf & vbCrLf & xml & " will be sent to " & url)

        Dim arr As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
        request = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        request.ContentLength = arr.Length
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(arr, 0, arr.Length)
        dataStream.Close()
        Try
            ' Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Dim returnString As String = response.StatusCode.ToString()
            Dim returnString2 As String = response.ToString()
            MsgBox(returnString)
            '   MsgBox(returnString2)


            '' prova
            '' https://developer.yahoo.com/dotnet/howto-xml_vb.html
            '  Dim requestxx As HttpWebRequest
            '  Dim responsexx As HttpWebResponse = Nothing
            '  Dim reader As StreamReader
            Dim result As String

            Try
                ' Create the web request  
                '   request = DirectCast(WebRequest.Create(address), HttpWebRequest)

                ' Get response  
                response = DirectCast(request.GetResponse(), HttpWebResponse)

                ' Get the response stream into a reader  
                reader = New StreamReader(response.GetResponseStream())

                ' Read the whole contents and return as a string  
                result = reader.ReadToEnd()
            Finally
                If Not response Is Nothing Then response.Close()
            End Try

            MsgBox(result)
            TextBox2.Text = result
            TextBox2.Enabled = True


            '' prova

        Catch
            MsgBox("tx fallita")
        End Try
        seed = ""
        '  seed2 = ""
Fooerror2:
    End Sub

    Private Sub cosignForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox2.Enabled = False
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class