---
title: Connect to Server (Database Engine)
description: "Connect to Server (Database Engine)"
author: markingmyname
ms.author: maghan
ms.date: 04/07/2020
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.connectoserverunknownservertype.f1"
  - "sql13.swb.connection.login.sqlce.f1"
  - "sql13.swb.connecttoce.f1"
  - "SQL13.SWB.CONNECTION.LOGIN.SQLSERVER.F1"
  - "sql13.swb.manageSS2k.f1"
  - "connect-to-server-database-engine"
---

# Connect to Server (Database Engine)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use this dialog to view or specify options when connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. In most cases, you can connect by entering the computer name of the database server in the **Server name** box and then clicking **Connect**. If you are connecting to a named instance, use the computer name followed by a backslash, and then the instance name. For example, `mycomputer\myinstance`. If you are connecting to [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], use the computer name followed by **\sqlexpress**.
  
Many factors can affect your ability to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For help, see the following resources:

- [Tutorial Lesson 1: Connecting to the Database Engine](../../relational-databases/lesson-1-connecting-to-the-database-engine.md)  

- [Troubleshoot Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)  

- [Solving Connectivity errors to SQL Server](https://support.microsoft.com/help/4009936/solving-connectivity-errors-to-sql-server)   
  
## Options

**Server type**  
When registering a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. The rest of the dialog shows only the options that apply to the selected server type. When registering a server from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssEW](../../includes/ssew-md.md)], or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] from the Registered Servers toolbar before starting to register a new server.  
  
**Server name**  
Select the server instance to connect to. The server instance last connected to is displayed by default.  
  
> [!NOTE]  
> To connect to an active user instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], connect using named pipes protocol specifying the pipe name, such as `np:\\.\pipe\3C3DF6B1-2262-47\tsql\query`. For more information, see the [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] documentation.  

> [!NOTE]  
> Connections are typically persisted in the "Most Recently Used" (MRU) history. To remove entries from the MRU, simply click on the **Server name** combobox, select the name of the server to remove, then press the **DEL** key.  

**Authentication**  
The current version of SSMS offers eight authentication modes when connecting to a [!INCLUDE[ssDE](../../includes/ssde-md.md)]. If your Authentication dialog box does not match the following list, download the most recent version of SSMS from [Download SQL Server Management Studio (SSMS)](../download-sql-server-management-studio-ssms.md).  

> **Windows Authentication**  
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication mode allows a user to connect through a Windows user account.  
> 
> **SQL Server Authentication**  
> When a user connects with a specified login name and password from a non-trusted connection, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication itself by checking to see if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have a login account set, authentication fails, and the user receives an error message. When possible, use Windows Authentication or Azure Active Directory - Password authentication.  
> 
> **Azure Active Directory - Universal with MFA**  
> Active Directory - Universal with MFA is an interactive workflow that supports Azure Multi-Factor Authentication (MFA). Azure MFA helps safeguard access to data and applications while meeting user demand for a simple sign-in process. It delivers strong authentication with a range of easy verification options: phone call, text message, smart cards with pin, mobile app notifications, and more. When the user account is configured for MFA, the interactive authentication workflow requires additional user interaction through pop-up dialog boxes, smart card use, etc. If the user account does not require MFA, the user can still use the other Azure Active Directory Authentication options. For more information, see [SSMS support for Azure AD MFA with SQL Database and Azure Synapse Analytics](/azure/azure-sql/database/authentication-mfa-ssms-overview).
> 
> **Azure Active Directory - Password**  
> Azure Active Directory Authentication is a mechanism of connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] by using identities in Azure Active Directory (Azure AD).  Use this method for connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] if you are logged in to Windows using credentials from a domain that is not federated with Azure, or when using Azure AD authentication using Azure AD based on the initial or the client domain. For more information, see [Connecting to SQL Database By Using Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview).  
> 
> **Active Directory - Integrated**  
> Azure Active Directory Authentication is a mechanism of connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] by using identities in Azure Active Directory (Azure AD). Use this method for connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] if you are logged in to Windows using your Azure Active Directory credentials from a federated domain. For more information, see [Connecting to SQL Database By Using Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview).
> 
> **Azure Active Directory - Service Principal**  
> A service principal is an Azure AD identity that can be created for use with automated tools, jobs and applications. With the Service Principal authentication option, you can connect to your SQL instance using the client ID and secret of a service principal identity. 
> 
> **Azure Active Directory - Managed Identity**  
> Managed identities are special forms of service principals. There are two types of managed identities: system-assigned and user-assigned. System-assigned managed identities are enabled directly on an Azure resource, whereas user-assigned managed identities are a standalone resource that can be assigned to one or more Azure resources.  
> In order to use a managed identity to connect to a SQL resource through GUI clients such as SSMS and ADS, the machine running the client application must have an Azure AD client running with the identity's certificate stored in it. This is most commonly achieved through an Azure VM, as the identity can be easily assigned to the machine through the VM's portal blade.
> 
> **Azure Active Directory - Default**  
> The Default authentication option with Azure AD performs authentication based on password-less and non-interactive mechanisms including: Managed Identities, Visual Studio, Visual Studio Code, Azure CLI, and more.
  
**User name**  
The user name to connect with. This setting is read-only when you select **Windows Authentication** or **Azure Active Directory - Integrated** authentication, and pre-filled with the Windows user name you are currently logged in with.

If connecting with **Azure Active Directory - Universal with MFA**, **Azure Active Directory - Password**, **Azure Active Directory - Service Principal**, or **Azure Active Directory - Default**, this is the name of the Azure AD identity you are connecting with.

**User assigned identity**
This option is displayed when connecting with **Azure Active Directory - Managed Identity**. How to fill this option properly depends on the type of the identity:
- With a user-assigned managed identity, the name of the identity should be entered.
- With a system-assigned managed identity, the field must be left blank. Entering the name of the system-assigned managed identity will cause the authentication to fail.

**Login**  
Enter the login to connect with. This option is only available if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
> [!NOTE]  
> Connections are typically persisted in the "Most Recently Used" (MRU) history. To remove entries from the MRU, simply click on the **Server name** combobox, select the name of the server to remove, then press the **DEL** key. This was introduced with SSMS 18.5.

**Password**  
Enter the password for the login. This option is only editable if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, Azure Active Directory - Service Principal, or Azure Active Directory - Password authentication.  

**Remember password**
Select this checkbox to have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] store the password you have entered. This option is displayed for the same authentication methods mentioned for the Password option.

**Connect**  
Click to connect to the server.

**Options**  
Click to display the **Connection Properties**, and **Additional Connection Parameters** tabs.
