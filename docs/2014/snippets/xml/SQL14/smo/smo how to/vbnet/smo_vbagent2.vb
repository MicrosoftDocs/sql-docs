Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo.Agent
Module SMO_VBAgent2

    Sub Main()

        Dim vIdentity As String
        Dim vLogin As String
        Dim srv1 As Server
        srv1 = New Server
        Dim l As Login
        vLogin = srv1.Name + "\Guest"
        vIdentity = srv1.Name + "\Guest"
        l = New Login(srv1, vLogin)
        l.LoginType = LoginType.WindowsUser
        l.Create()
        ' <snippetSMO_VBAgent2>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Declare a JobServer object variable and reference the SQL Agent.
        Dim js As JobServer
        js = srv.JobServer
        'Define a Credential object variable by supplying the parent server and name arguments in the constructor.
        Dim c As Credential
        c = New Credential(srv, "Proxy_accnt")
        'Set the identity to a valid login represented by the vIdentity string variable. 
        'The sub system will run under this login.
        c.Identity = vIdentity
        'Create the credential on the instance of SQL Server.
        c.Create()
        'Define a ProxyAccount object variable by supplying the SQL Agent, the name, the credential, the description arguments in the constructor.
        Dim pa As ProxyAccount
        pa = New ProxyAccount(js, "Test_proxy", "Proxy_accnt", True, "Proxy account for users to run job steps in command shell.")
        'Create the proxy account on the SQL Agent.
        pa.Create()
        'Add the login, represented by the vLogin string variable, to the proxy account. 
        pa.AddLogin(vLogin)
        'Add the CmdExec subsytem to the proxy account. 
        pa.AddSubSystem(AgentSubSystem.CmdExec)
        'Now users logged on as vLogin can run CmdExec job steps with the specified credentials.
        ' </snippetSMO_VBAgent2>
        pa.Drop()
        c.Drop()
        l.Drop()



    End Sub

End Module
