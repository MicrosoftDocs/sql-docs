Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VB3

    Sub Main()
        Dim vlogin As String
        Dim vpassword As String
        Dim srv1 As Server
        srv1 = New Server()
        srv1.ConnectionContext.ExecuteNonQuery("CREATE LOGIN [Jane] WITH PASSWORD=N'23tY8$%7Ui(*', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON")


        Try

            vpassword = "!Password1"
            vlogin = "sa"
            ' <snippetSMO_VB3>

            'Declare a ServerConnection object variable to specify SQL authentication, login and password.
            Dim conn As New ServerConnection
            conn.LoginSecure = False
            conn.Login = vlogin
            conn.Password = vpassword
            'Connect to the local, default instance of SQL Server.
            Dim srv As Server
            srv = New Server(conn)
            'The actual connection is made when a property is retrieved.
            Console.WriteLine(srv.Information.Version)
            'The connection is automatically disconnected when the Server variable goes out of scope.
            ' </snippetSMO_VB3>



        Catch ex As Exception
            Console.WriteLine(ex.Message)
            If ex.InnerException IsNot Nothing Then
                Console.WriteLine(ex.InnerException.Message)
                If ex.InnerException.InnerException IsNot Nothing Then
                    Console.WriteLine(ex.InnerException.InnerException.Message)
                End If

            End If

            Console.ReadLine()


        End Try

       
    End Sub

End Module
