---
title: "Connect to Server (Login Page) Database Engine | Microsoft Docs"
ms.custom: ""
ms.date: "08/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.connecttosqlserver.login.f1"
ms.assetid: e08cfbc3-bed5-4401-a13b-1c66d902fe32
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Connect to Server (Login Page) Database Engine
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Use this tab to view or specify options when connecting to [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion_md.md)]. In most cases, you can connect by entering the computer name of the database server in the **Server name** box and then clicking **Connect**. If you are connecting to a named instance, use the computer name followed by a backslash, and then the instance name. For example, `mycomputer\myinstance`. If you are connecting to [!INCLUDE[ssExpress](../../includes/ssexpress_md.md)], use the computer name followed by **\sqlexpress**.  
  
Many factors can affect your ability to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For help, see the following resources:  
- [Tutorial Lesson 1: Connecting to the Database Engine](../../relational-databases/lesson-1-connecting-to-the-database-engine.md)  
- [Troubleshoot Connecting to the SQL Server Database Engine](../../database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine.md)  
- [Solving Connectivity errors to SQL Server](https://support.microsoft.com/help/4009936/solving-connectivity-errors-to-sql-server)    
  
> [!NOTE]  
> To connect with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be configured in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication mode. For more information about how to determine the authentication mode and to change the authentication mode, see [How to: Change Server Authentication Mode](../../database-engine/configure-windows/change-server-authentication-mode.md).  
  
## Options  
**Server type**  
When registering a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../../includes/ssde_md.md)], Analysis Services, Reporting Services, or Integration Services. The rest of the dialog box shows only the options that apply to the selected server type. When registering a server from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../../includes/ssde_md.md)], Analysis Services, Reporting Services, or Integration Services from the Registered Servers toolbar before starting to register a new server.  
  
When connecting to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine through [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you must use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication and specify a database in the **Connect to Server** dialog box, on the **Connection Properties** tab. Ensure that you select the **Encrypt connection** checkbox.  
  
By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connects to **master**. If you specify a user database, you only see that database and its objects in Object Explorer. If you connect to **master**, you can see all databases. For more information, see the [Windows Azure SQL Database Overview](https://go.microsoft.com/fwlink/?LinkId=163948).  
  
**Server name**  
Select the server instance to connect to. The server instance last connected to is displayed by default.  
  
**Authentication**  
The current version of SSMS, offers five authentication modes when connecting to an instance of the [!INCLUDE[ssDE](../../includes/ssde_md.md)]. If your Authentication dialog box does not match the following list, download the most recent version of SSMS, from [Download SQL Server Management Studio (SSMS)](../download-sql-server-management-studio-ssms.md).     
  
When connecting to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine through [!INCLUDE[ssSDS](../../includes/sssds-md.md)], you must use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication and specify a database in the **Connect to Server** dialog box, on the **Connection Properties** tab. Ensure that you select the **Encrypt connection** checkbox.  
  
By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connects to **master**. If you specify a user database when connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)], you only see that database and its objects in Object Explorer. If you connect to **master**, you can see all databases. For more information, see the [Windows Azure SQL Database Overview](https://go.microsoft.com/fwlink/?LinkId=163948).  
  
> **Windows Authentication**  
> [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows Authentication mode allows a user to connect through a Windows user account.  
> 
> **SQL Server Authentication**  
> When a user connects with a specified login name and password from a non-trusted connection, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication itself by checking to see if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have a login account set, authentication fails, and the user receives an error message. When possible, use Windows Authentication.  
> 
> **Active Directory - Universal with MFA support**  
> Active Directory - Universal with MFA is an interactive work flow that supports Azure Multi-Factor Authentication (MFA). Azure MFA helps safeguard access to data and applications while meeting user demand for a simple sign-in process. It delivers strong authentication with a range of easy verification options-phone call, text message, smart cards with pin, or mobile app notification-allowing users to choose the method they prefer. When the user account is configured for MFA the interactive authentication work flow requires additional user interaction through pop-up dialog boxes, smart card use, etc. When the user account is configured for MFA, the user must select Azure Universal Authentication to connect. If the user account does not require MFA, the user can still use the other two Azure Active Directory Authentication options. For more information, see [SSMS support for Azure AD MFA with SQL Database and SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-database-ssms-mfa-authentication/). If necessary, you can change the domain that authenticates the login, by clicking **Options**, selecting the **Connection Properties** tab, and then completing the **AD domain name or tenant ID** box.  
> 
> **Active Directory - Password**  
> Azure Active Directory Authentication is a mechanism of connecting to [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] by using identities in Azure Active Directory (Azure AD).  Use this method for connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] if you are logged in to Windows using credentials from a domain that is not federated with Azure, or when using Azure AD authentication using Azure AD based on the initial or the client domain. For more information, see [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/).  
> 
> **Active Directory - Integrated**  
> Azure Active Directory Authentication is a mechanism of connecting to [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] by using identities in Azure Active Directory (Azure AD). Use this method for connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] if you are logged in to Windows using your Azure Active Directory credentials from a federated domain. For more information, see [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/).  
  
**User name**  
The Windows user name to connect with. This option is only available if you have selected to connect using **Active Directory Password Authentication**. It is read-only when you select **Windows Authentication** or **Active Directory - Integrated** authentication.  
  
**Login**  
Enter the login to connect with. This option is only available if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication or Active Directory Password Authentication.  
  
**Password**  
Enter the password for the login. This option is only editable if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication or Active Directory - Password authentication.  
  
**Remember password**  
Click to have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] store the password you have entered. This option is only displayed if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
**Connect**  
Click to connect to the server.  
  
**Options**  
Click to display the **Connection Properties**, and **Additional Connection Parameters** tabs.  
   
  
