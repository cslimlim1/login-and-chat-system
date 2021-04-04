Public Class Form1
    Public Shared i As Integer = 0
    Public Shared already_read_abc As Integer = 0
    Public Shared already_read_def As Integer = 0
    Dim Counts As Integer
    Dim read_abc As Integer = 0
    Dim read_def As Integer = 0
    Public Shared first_time_read_abc As Integer = 0
    Public Shared first_time_read_def As Integer = 0
    Dim dupText As String
    Public Shared array_abc As New List(Of String)
    Public Shared array_def As New List(Of String)
    Dim array_abc_output As String()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox3.Text = "login abc" And array_abc.Count = 0 Then
            i = 1
            TextBox4.Text = "abc logged in"
            TextBox5.Text = "abc"
            read_abc = 1 'control the state of login 
            read_def = 0
        ElseIf TextBox3.Text = "login def" And array_def.Count = 0 Then
            i = 1
            TextBox4.Text = "def logged in"
            TextBox5.Text = "def"
            read_def = 1 'control the state of login
            read_abc = 0
            'read "sent" from input
        ElseIf i = 1 And TextBox3.Text.Split(" ".ToCharArray)(0) = "sent" Then
            If TextBox3.Text.Split(" ".ToCharArray)(1) <> "abc" And TextBox3.Text.Split(" ".ToCharArray)(1) <> "def" Then
                TextBox4.Text = "error: User does not exist"
            Else
                If TextBox3.Text.Split(" ".ToCharArray)(1) = "abc" Then
                    Dim input_text As String = TextBox3.Text.Replace("sent abc", "").Trim()
                    TextBox4.Text = input_text
                    array_abc.Add("from def:" & input_text)
                    TextBox4.Text = "message sent"
                ElseIf TextBox3.Text.Split(" ".ToCharArray)(1) = "def" Then
                    Dim input_text As String = TextBox3.Text.Replace("sent def", "").Trim()
                    TextBox4.Text = input_text
                    array_def.Add("from abc:" & input_text)
                    TextBox4.Text = "message sent"
                End If
            End If
            'login abc and gt message 
        ElseIf TextBox3.Text = "login abc" And array_abc.Count <> 0 Then
            TextBox5.Text = "abc"
            Dim total_message As Integer = array_abc.Count
            TextBox4.Text = "abc log in," & total_message & " new messages"
            read_abc = 1 'control the state of login 
            read_def = 0
            'login def and gt message
        ElseIf TextBox3.Text = "login def" And array_def.Count <> 0 Then
            TextBox5.Text = "def"
            Dim total_message As Integer = array_def.Count
            TextBox4.Text = "def log in," & total_message & " new messages"
            read_def = 1 'control the state of login
            read_abc = 0

            'login abc, message array not empty, and read input 
        ElseIf read_abc = 1 And read_def = 0 And array_abc.Count <> 0 And TextBox3.Text = "read" Then 'abc already login in and message array not empty 
            If first_time_read_abc = 0 Then
                TextBox4.Text = array_abc(0)
                first_time_read_abc = 1
                already_read_abc = 1 'already read messsage 
            ElseIf first_time_read_abc = 1 Then 'second time read, will remove first item in array first
                array_abc.RemoveAt(0)
                If array_abc.Count <> 0 Then
                    TextBox4.Text = array_abc(0)
                End If
            End If
            'login def, message array not empty, and read input
        ElseIf read_def = 1 And read_abc = 0 And array_def.Count <> 0 And TextBox3.Text = "read" Then 'def already login in and message array not empty 
            If first_time_read_def = 0 Then
                TextBox4.Text = array_def(0)
                first_time_read_def = 1
                already_read_def = 1 'already read messsage 
            ElseIf first_time_read_def = 1 Then 'second time read, will remove first item in array first
                array_def.RemoveAt(0)
                If array_def.Count <> 0 Then
                    TextBox4.Text = array_def(0)
                End If
            End If
            'reply for abc login
        ElseIf already_read_abc = 1 And read_abc = 1 And read_def = 0 And TextBox3.Text.Split(" ".ToCharArray)(0) = "reply" Then
            Dim abc_input_text As String = TextBox3.Text.Replace("reply", "").Trim()
            TextBox4.Text = abc_input_text
            array_def.Add("from abc:" & abc_input_text)
            TextBox4.Text = "message sent to def"
            'reply for def login
        ElseIf already_read_def = 1 And read_def = 1 And read_abc = 0 And TextBox3.Text.Split(" ".ToCharArray)(0) = "reply" Then
            Dim def_input_text As String = TextBox3.Text.Replace("reply", "").Trim()
            TextBox4.Text = def_input_text
            array_abc.Add("from def:" & def_input_text)
            TextBox4.Text = "message sent to abc"
            'forward for abc login
        ElseIf already_read_abc = 1 And read_abc = 1 And read_def = 0 And TextBox3.Text.Split(" ".ToCharArray)(0) = "forward" Then
            Dim forward_abc_string As String = "from def,abc:" & array_abc(0).Replace("from def:", "").Trim()
            array_def.Add(forward_abc_string)
            TextBox4.Text = "message forwarded to def"
            'forward for def login
        ElseIf already_read_def = 1 And read_def = 1 And read_abc = 0 And TextBox3.Text.Split(" ".ToCharArray)(0) = "forward" Then
            Dim forward_def_string As String = "from abc,def:" & array_def(0).Replace("from abc:", "").Trim()
            array_abc.Add(forward_def_string)
            TextBox4.Text = "message forwarded to abc"
            'broadcast to all user
        ElseIf TextBox3.Text.Split(" ".ToCharArray)(0) = "broadcast" Then
            Dim broadcast_input_text As String = TextBox3.Text.Replace("broadcast", "").Trim()
            TextBox4.Text = broadcast_input_text
            If read_def = 1 And read_abc = 0 Then
                array_abc.Add("from def:" & broadcast_input_text)
                array_def.Add("from def:" & broadcast_input_text)
            ElseIf read_abc = 1 And read_def = 0 Then
                array_def.Add("from abc:" & broadcast_input_text)
                array_abc.Add("from abc:" & broadcast_input_text)
            End If
        ElseIf i = 0 Then
            TextBox4.Text = "error: Please log in First"
        Else
            TextBox4.Text = "Error"
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub
End Class
