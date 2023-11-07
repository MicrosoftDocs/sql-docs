---
title: Connect to Server (Login Page) Database Engine
description: "Connect to Server (Login Page) Database Engine"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 11/06/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.connecttosqlserver.login.f1"
  - "sql13.swb.connectoserverunknownservertype.f1"
---

# Connect to Server (Login Page) Database Engine

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use this tab to view or specify options when connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. In most cases, you can connect by entering the computer name of the database server in the **Server name** box and then selecting **Connect**. If you're connecting to a named instance, use the computer name followed by a backslash, and then the instance name. For example, `mycomputer\myinstance`. See [Connect to the Database Engine](../../sql-server/connect-to-database-engine.md) for more examples.

Many factors can affect your ability to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For help, see the following resources:

- [Tutorial Lesson 1: Connecting to the Database Engine](../../relational-databases/lesson-1-connecting-to-the-database-engine.md)

- [Troubleshoot Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)  

- [Solving Connectivity errors in SQL Server](https://support.microsoft.com/help/4009936/solving-connectivity-errors-to-sql-server)

> [!NOTE]
> To connect with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be configured in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication mode. For more information about how to determine the authentication mode and to change the authentication mode, see [How to: Change Server Authentication Mode](../../database-engine/configure-windows/change-server-authentication-mode.md).  

[!INCLUDE [entra-id-hard-coded](../../includes/entra-id-hard-coded.md)]

## Options

**Server type**  
When you register a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. The dialog box shows only the options that apply to the selected server type. When you register a server from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssEW](../../includes/ssew-md.md)], or [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] from the Registered Servers toolbar before starting to register a new server.

**Server name**  
Select the server instance to connect to, the server instance last connected to is displayed by default.  

> [!NOTE]  
> To connect to an instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], connect using named pipes protocol specifying the pipe name, such as `np:\\.\pipe\3C3DF6B1-2262-47\tsql\query`. For more information, see the [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] documentation.  

> [!NOTE]  
> Connections are persisted in the "Most Recently Used" (MRU) history. To remove entries from the MRU, select the **Server name** drop-down, hover over the name of the server to remove, then press the **DEL** key.  

When connecting to Azure SQL Database you can specify a database in the **Connect to Server** dialog box on the **Connection Properties** tab, which you can access by selecting **Options >>**.

By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connects to **master**. If you specify a user database when connecting to Azure SQL Database, you only see that database and its objects in Object Explorer. If you connect to **master**, you can see all databases. For more information, see the [Microsoft Azure SQL Database Overview](/azure/sql-database/sql-database-technical-overview/).

**Authentication**  
The current version of SQL Server Management Studio (SSMS) offers eight authentication modes when connecting to a [!INCLUDE[ssDE](../../includes/ssde-md.md)]. If your Authentication dialog box doesn't match the following list, download the most recent version of SSMS from [Download SQL Server Management Studio (SSMS)](../download-sql-server-management-studio-ssms.md).

> **Windows Authentication**  
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication mode allows a user to connect through a Windows user account.  
>
> **SQL Server Authentication**  
> When a user connects with a specified login name and password, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication by checking to see if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have a login account set, authentication fails, and the user receives an error message.
>
> **Azure Active Directory - Universal with MFA**  
> Azure Active Directory - Universal with MFA is an interactive work flow that supports Azure Multi-Factor Authentication (MFA). Azure MFA helps safeguard access to data and applications while meeting user demand for a simple sign-in process. It delivers strong authentication with a range of easy verification options: phone call, text message, smart cards with pin, mobile app notifications, and more. When the user account is configured for MFA the interactive authentication workflow requires additional user interaction through pop-up dialog boxes, smart card use, etc. If the user account does not require MFA, the user can still use the other Azure Active Directory Authentication options. For more information, see [Using Azure Active Directory Multi-Factor Authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview).
>
> **Azure Active Directory - Password**  
> Azure Active Directory - Password is a mechanism of connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] by using identities in Azure Active Directory (Azure AD).  Use this method for connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] if you're logged in to Windows using credentials from a domain that is not federated with Azure, or when using Azure AD authentication based on the initial or the client domain. For more information, see [Use Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview).  
>
> **Azure Active Directory - Integrated**  
> Azure Active Directory Authentication is a mechanism of connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] by using identities in Azure Active Directory (Azure AD). Use this method for connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] if you're logged in to Windows using your Azure Active Directory credentials from a federated domain, or a managed domain that is configured for seamless single sign-on for pass-through and password hash authentication. For more information, see [Connecting to SQL Database By Using Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview) and [Azure Active Directory Seamless single sign-on](/azure/active-directory/hybrid/connect/how-to-connect-sso).
>
> **Azure Active Directory - Service Principal**  
> A service principal is an Azure AD identity that can be created for use with automated tools, jobs and applications. With the Service Principal authentication option, you can connect to your SQL instance using the client ID and secret of a service principal identity. In SSMS, enter the client ID in the username field, and the secret in the password field. For more information, see [Azure Active Directory server principals](/azure/azure-sql/database/authentication-azure-ad-logins) and [Azure Active Directory server principal with Azure SQL](/azure/azure-sql/database/authentication-aad-service-principal).
>
> **Azure Active Directory - Managed Identity**  
> Managed identities are special forms of service principals. There are two types of managed identities: system-assigned and user-assigned. System-assigned managed identities are enabled directly on an Azure resource, whereas user-assigned managed identities are a standalone resource that can be assigned to one or more Azure resources.  
> In order to use a managed identity to connect to a SQL resource through GUI clients such as SSMS and ADS, the machine running the client application must have an Azure AD client running with the identity's certificate stored in it. This is most commonly achieved through an Azure VM, as the identity can be easily assigned to the machine through the VM's portal blade.  For more information, see [Managed identities in Azure AD for Azure SQL](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity).
>
> **Azure Active Directory - Default**  
> The Default authentication option with Azure Active Directory performs authentication based on password-less and non-interactive mechanisms including: Managed Identities, Visual Studio, Visual Studio Code, Azure CLI, and more.

**User name**  
The user name to connect with. This setting is read-only when you select **Windows Authentication** or **Azure Active Directory - Integrated** authentication, and prefilled with the Windows user name you're currently logged in with.

If connecting with **Azure Active Directory - Universal with MFA**, **Azure Active Directory - Password**, **Azure Active Directory - Service Principal**, or **Azure Active Directory - Default**, enter the name of the Azure AD identity you're connecting with.

**User assigned identity**  
This option is displayed when connecting with **Azure Active Directory - Managed Identity**. Completing this option properly depends on the type of the identity:

- With a user-assigned managed identity, the name of the identity should be entered.
- With a system-assigned managed identity, the field must be left blank. Entering the name of the system-assigned managed identity causes the authentication to fail.

**Login**  
Enter the login to connect with. This option is only available if you connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.

**Password**  
Enter the password for the login. This option is only editable if you connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, Azure Active Directory - Service Principal, or Azure Active Directory - Password authentication.  

**Remember password**  
Select for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store the password entered. This option is displayed for the same authentication methods mentioned for the Password option.

**Connect**  
Select to connect to the server.  

**Options**  
Select to display the **Connection Properties**, **Always Encrypted** and **Additional Connection Parameters** tabs.

## See Also  

[Connect to Server (Connection Properties Page) Database Engine](connect-to-server-connection-properties-page-database-engine.md)
[Connect to Server (Always Encrypted Page) Database Engine](connect-to-server-always-encrypted-page-database-engine.md)
[Connect to Server (Additional Connection Parameters Page) Database Engine](connect-to-server-additional-connection-parameters-page-database-engine.md)
