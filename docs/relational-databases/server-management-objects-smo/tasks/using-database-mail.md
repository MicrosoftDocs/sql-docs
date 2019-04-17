---
title: "Using Database Mail | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "e-mail [SMO]"
  - "Database Mail [SMO]"
  - "mail [SMO]"
ms.assetid: 7605390f-b485-48cc-8d97-e364a066067b
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Database Mail
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  In SMO, the Database Mail subsystem is represented by the <xref:Microsoft.SqlServer.Management.Smo.Mail.SqlMail> object that is referenced by the <xref:Microsoft.SqlServer.Management.Smo.Server.Mail%2A> property. By using the SMO <xref:Microsoft.SqlServer.Management.Smo.Mail.SqlMail> object, you can configure the Database Mail subsystem and manage profiles and mail accounts. The SMO <xref:Microsoft.SqlServer.Management.Smo.Mail.SqlMail> object belongs to the **Server** object, meaning that scope of the mail accounts is at the server level.  
  
## Examples  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
 For programs that use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Database Mail, you must include the **Imports** statement to qualify the Mail namespace. Insert the statement after the other **Imports** statements, before any declarations in the application, such as:  
  
 `Imports Microsoft.SqlServer.Management.Smo`  
  
 `Imports Microsoft.SqlServer.Management.Common`  
  
 `Imports Microsoft.SqlServer.Management.Smo.Mail`  
  
## Creating a Database Mail Account by Using Visual Basic  
 This code example shows how to create an e-mail account in SMO. Database Mail is represented by the <xref:Microsoft.SqlServer.Management.Smo.Mail.SqlMail> object and referenced by the <xref:Microsoft.SqlServer.Management.Smo.Server.Mail%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server> object. SMO can be used to programmatically configure Database Mail, but it cannot be used to send or handle received e-mail.  
  
```VBNET
'Connect to the local, default instance of SQL Server.
Dim srv As Server
srv = New Server()
'Define the Database Mail service with a SqlMail object variable and reference it using the Server Mail property.
Dim sm As SqlMail
sm = srv.Mail
'Define and create a mail account by supplying the Database Mail service, name, description, display name, and email address arguments in the constructor.
Dim a As MailAccount
a = New MailAccount(sm, "AdventureWorks Administrator", "AdventureWorks Automated Mailer", "Mail account for administrative e-mail.", "dba@Adventure-Works.com")
a.Create()
```
  
## Creating a Database Mail Account by Using Visual C#  
 This code example shows how to create an e-mail account in SMO. Database Mail is represented by the <xref:Microsoft.SqlServer.Management.Smo.Mail.SqlMail> object and referenced by the <xref:Microsoft.SqlServer.Management.Smo.Server.Mail%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server> object. SMO can be used to programmatically configure Database Mail, but it cannot be used to send or handle received e-mail.  
  
```csharp  
{  
         //Connect to the local, default instance of SQL Server.  
         Server srv = default(Server);   
           srv = new Server();   
           //Define the Database Mail service with a SqlMail object variable   
           //and reference it using the Server Mail property.   
           SqlMail sm;   
           sm = srv.Mail;   
           //Define and create a mail account by supplying the Database Mail  
           //service, name, description, display name, and email address  
           //arguments in the constructor.   
           MailAccount a = default(MailAccount);   
           a = new MailAccount(sm, "AdventureWorks2012 Administrator", "AdventureWorks2012 Automated Mailer", "Mail account for administrative e-mail.", "dba@Adventure-Works.com");   
           a.Create();    
}  
```  
  
## Creating a Database Mail Account by Using PowerShell  
 This code example shows how to create an e-mail account in SMO. Database Mail is represented by the <xref:Microsoft.SqlServer.Management.Smo.Mail.SqlMail> object and referenced by the <xref:Microsoft.SqlServer.Management.Smo.Server.Mail%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server> object. SMO can be used to programmatically configure Database Mail, but it cannot be used to send or handle received e-mail.  
  
  
  
```powershell  
#Connect to the local, default instance of SQL Server.  
  
#Get a server object which corresponds to the default instance  
$srv = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Server  
  
#Define the Database Mail; reference it using the Server Mail property.  
$sm = $srv.Mail  
  
#Define and create a mail account by supplying the Database Mail service,  
#name, description, display name, and email address arguments in the constructor.  
$a = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Mail.MailAccount -argumentlist $sm, `  
"Adventure Works Administrator", "Adventure Works Automated Mailer",`  
 "Mail account for administrative e-mail.", "dba@Adventure-Works.com"  
$a.Create()  
```  
  
  
