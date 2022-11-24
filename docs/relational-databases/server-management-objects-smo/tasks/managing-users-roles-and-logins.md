---
description: "Managing Users, Roles, and Logins"
title: "Managing Users, Roles, and Logins"
ms.custom: seo-dt-2019
ms.date: "08/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "logins [SMO]"
  - "roles [SMO]"
  - "users [SMO]"
ms.assetid: 74e411fa-74ed-49ec-ab58-68c250f2280e
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Managing Users, Roles, and Logins
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  In SMO, logins are represented by the <xref:Microsoft.SqlServer.Management.Smo.Login> object. When the logon exists in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], it can be added to a server role. The server role is represented by the <xref:Microsoft.SqlServer.Management.Smo.ServerRole> object. The database role is represented by the <xref:Microsoft.SqlServer.Management.Smo.DatabaseRole> object and the application role is represented by the <xref:Microsoft.SqlServer.Management.Smo.ApplicationRole> object.  
  
 Privileges associated with the server level are listed as properties of the <xref:Microsoft.SqlServer.Management.Smo.ServerPermission> object. The server level privileges can be granted to, denied to, or revoked from individual logon accounts.  
  
 Every <xref:Microsoft.SqlServer.Management.Smo.Database> object has a <xref:Microsoft.SqlServer.Management.Smo.UserCollection> object that specifies all users in the database. Each user is associated with a logon. One logon can be associated with users in more than one database. The <xref:Microsoft.SqlServer.Management.Smo.Login> object's <xref:Microsoft.SqlServer.Management.Smo.Login.EnumDatabaseMappings%2A> method can be used to list all users in every database that is associated with the logon. Alternatively, the <xref:Microsoft.SqlServer.Management.Smo.User> object's <xref:Microsoft.SqlServer.Management.Smo.Login> property specifies the logon that is associated with the user.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases also have roles that specify a set of database level privileges that let a user perform specific tasks. Unlike server roles, database roles are not fixed. They can be created, modified, and removed. Privileges and users can be assigned to a database role for bulk administration.  
  
## Example  
 For the following code examples, you will have to select the programming environment, programming template and the programming language to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Enumerating Logins and Associated Users in Visual C#  
 Every user in a database is associated with a logon. The logon can be associated with users in more than one database. The code example shows how to call the <xref:Microsoft.SqlServer.Management.Smo.Login.EnumDatabaseMappings%2A> method of the <xref:Microsoft.SqlServer.Management.Smo.Login> object to list all the database users who are associated with the logon. The example creates a logon and user in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] database to make sure there is mapping information to enumerate.  
  
```csharp  
{   
Server srv = new Server();   
//Iterate through each database and display.   
  
foreach ( Database db in srv.Databases) {   
   Console.WriteLine("========");   
   Console.WriteLine("Login Mappings for the database: " + db.Name);   
   Console.WriteLine(" ");   
   //Run the EnumLoginMappings method and return details of database user-login mappings to a DataTable object variable.   
   DataTable d;  
   d = db.EnumLoginMappings();   
   //Display the mapping information.   
   foreach (DataRow r in d.Rows) {   
      foreach (DataColumn c in r.Table.Columns) {   
         Console.WriteLine(c.ColumnName + " = " + r[c]);   
      }   
      Console.WriteLine(" ");   
   }   
}   
}  
```  
  
## Enumerating Logins and Associated Users in PowerShell  
 Every user in a database is associated with a logon. The logon can be associated with users in more than one database. The code example shows how to call the <xref:Microsoft.SqlServer.Management.Smo.Login.EnumDatabaseMappings%2A> method of the <xref:Microsoft.SqlServer.Management.Smo.Login> object to list all the database users who are associated with the logon. The example creates a logon and user in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] database to make sure there is mapping information to enumerate.  
  
```powershell  
# Set the path context to the local, default instance of SQL Server.
CD \sql\localhost\Default\Databases

#Iterate through all databases 
foreach ($db in Get-ChildItem)
{
  "====="
  "Login Mappings for the database: "+ $db.Name

  #get the datatable containing the mapping from the smo database oject
  $dt = $db.EnumLoginMappings()

  #display the results
  foreach($row in $dt.Rows)
  {
     foreach($col in $row.Table.Columns)
     {
       $col.ColumnName + "=" + $row[$col]
     }
   }
 }
```  
  
## Managing Roles and Users  
 This sample demonstrates how to how to manage roles and users. To run this sample you will need to reference the following assemblies:  
  
-   Microsoft.SqlServer.Smo.dll  
  
-   Microsoft.SqlServer.Management.Sdk.Sfc.dll  
  
-   Microsoft.SqlServer.ConnectionInfo.dll  
  
-   Microsoft.SqlServer.SqlEnum.dll  
  
```csharp  
using Microsoft.SqlServer.Management.Smo;  
using System;  
  
public class A {  
   public static void Main() {  
      Server svr = new Server();  
      Database db = new Database(svr, "TESTDB");  
      db.Create();  
  
      // Creating Logins  
      Login login = new Login(svr, "Login1");  
      login.LoginType = LoginType.SqlLogin;  
      login.Create("password@1");  
  
      Login login2 = new Login(svr, "Login2");  
      login2.LoginType = LoginType.SqlLogin;  
      login2.Create("password@1");  
  
      // Creating Users in the database for the logins created  
      User user1 = new User(db, "User1");  
      user1.Login = "Login1";  
      user1.Create();  
  
      User user2 = new User(db, "User2");  
      user2.Login = "Login2";  
      user2.Create();  
  
      // Creating database permission Sets  
      DatabasePermissionSet dbPermSet = new DatabasePermissionSet(DatabasePermission.AlterAnySchema);  
      dbPermSet.Add(DatabasePermission.AlterAnyUser);  
  
      DatabasePermissionSet dbPermSet2 = new DatabasePermissionSet(DatabasePermission.CreateType);  
      dbPermSet2.Add(DatabasePermission.CreateSchema);  
      dbPermSet2.Add(DatabasePermission.CreateTable);  
  
      // Creating Database roles  
      DatabaseRole role1 = new DatabaseRole(db, "Role1");  
      role1.Create();  
  
      DatabaseRole role2 = new DatabaseRole(db, "Role2");  
      role2.Create();  
  
      // Granting Database Permission Sets to Roles  
      db.Grant(dbPermSet, role1.Name);  
      db.Grant(dbPermSet2, role2.Name);  
  
      // Adding members (Users / Roles) to Role  
      role1.AddMember("User1");  
  
      role2.AddMember("User2");  
  
      // Role1 becomes a member of Role2  
      role2.AddMember("Role1");  
  
      // Enumerating through explicit permissions granted to Role1  
      // enumerates all database permissions for the Grantee  
      DatabasePermissionInfo[] dbPermsRole1 = db.EnumDatabasePermissions("Role1");     
      foreach (DatabasePermissionInfo dbp in dbPermsRole1) {  
         Console.WriteLine(dbp.Grantee + " has " + dbp.PermissionType.ToString() + " permission.");  
      }  
      Console.WriteLine(" ");  
   }  
}  
```
  
  
