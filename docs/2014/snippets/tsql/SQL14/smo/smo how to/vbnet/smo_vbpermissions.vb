Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBPermissions

    Sub Main()
        Dim vGrantee As String
        vGrantee = "BUILTIN\Administrators"
        ' <snippetSMO_VBPermissions1>
        'Connect to the local, default instance of SQL Server.
        Dim svr As Server
        svr = New Server()
        'Define a ServerPermissionSet that contains permission to Create Endpoint and Alter Any Endpoint.
        Dim sps As ServerPermissionSet
        sps = New ServerPermissionSet(ServerPermission.CreateEndpoint)
        sps.Add(ServerPermission.AlterAnyEndpoint)
        'This sample assumes that the grantee already has permission to Create Endpoints. 
        'Enumerate and display the server permissions in the set for the grantee specified in the vGrantee string variable.
        Dim spis As ServerPermissionInfo()
        spis = svr.EnumServerPermissions(vGrantee, sps)
        Dim spi As ServerPermissionInfo
        Console.WriteLine("=================Before revoke===========================")
        For Each spi In spis
            Console.WriteLine(spi.Grantee & " has " & spi.PermissionType.ToString & " permission.")
        Next
        Console.WriteLine(" ")
        'Remove a permission from the set.
        sps.Remove(ServerPermission.CreateEndpoint)
        'Revoke the create endpoint permission from the grantee.
        svr.Revoke(sps, vGrantee)
        'Enumerate and display the server permissions in the set for the grantee specified in the vGrantee string variable.
        spis = svr.EnumServerPermissions(vGrantee, sps)
        Console.WriteLine("=================After revoke============================")
        For Each spi In spis
            Console.WriteLine(spi.Grantee & " has " & spi.PermissionType.ToString & " permission.")
        Next
        Console.WriteLine(" ")
        'Grant the Create Endpoint permission to the grantee.
        svr.Grant(sps, vGrantee)
        'Enumerate and display the server permissions in the set for the grantee specified in the vGrantee string variable.
        spis = svr.EnumServerPermissions(vGrantee, sps)
        Console.WriteLine("=================After grant=============================")
        For Each spi In spis
            Console.WriteLine(spi.Grantee & " has " & spi.PermissionType.ToString & " permission.")
        Next
        Console.WriteLine("")
        ' </snippetSMO_VBPermissions1>
    End Sub

End Module
