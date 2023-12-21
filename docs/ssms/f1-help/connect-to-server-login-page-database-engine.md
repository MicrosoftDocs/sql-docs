---
title: Connect to Server (Login page) - Database Engine
description: This article shows how to use the Connect to Server (Login page) Database Engine.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 11/22/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.connecttosqlserver.login.f1"
  - "sql13.swb.connectoserverunknownservertype.f1"
---

# Connect to Server (Login page) - Database Engine

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use this tab to configure the connection properties when you connect to [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. In most cases, you can connect by entering the computer name of the database server in the **Server name** box and then selecting **Connect**. If you're connecting to a named instance, use the computer name followed by a backslash, and then the instance name. For example, `mycomputer\myinstance`. For more examples, see [Connect to the Database Engine](../../sql-server/connect-to-database-engine.md).

[!INCLUDE [entra-id-hard-coded](../../includes/entra-id-hard-coded.md)]

Many factors can affect your ability to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For help, see the following resources:

- [Lesson 1: Connecting to the Database Engine](../../relational-databases/lesson-1-connecting-to-the-database-engine.md)

- [Troubleshoot Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)

- [Solving Connectivity errors in SQL Server](https://support.microsoft.com/help/4009936/solving-connectivity-errors-to-sql-server)

> [!NOTE]  
> To connect with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be configured in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication mode. For more information about how to determine the authentication mode and to change the authentication mode, see [Change server authentication mode](../../database-engine/configure-windows/change-server-authentication-mode.md).

## Connect to a server

You can connect to any supported server from the login page by providing the server name and authentication details, as noted in the following sections.

#### Server type

When you register a server from Object Explorer, select the type of server to connect to: [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)], or [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)]. The dialog box shows only the options that apply to the selected server type. When you register a server from **Registered Servers**, the **Server type** box is read-only and matches the type of server displayed in the **Registered Servers** component. To register a different type of server, select [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE [ssEW](../../includes/ssew-md.md)], or [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] from the **Registered Servers** toolbar before you start to register a new server.

#### Server name

Select the server instance to connect to. The most recent server instance you connected to appears by default.

To connect to an instance of [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)], connect using the named pipes protocol specifying the pipe name, such as `np:\\.\pipe\3C3DF6B1-2262-47\tsql\query`. For more information, see the [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] documentation.

> [!NOTE]  
> Connections are persisted in the Most Recently Used (MRU) history. To remove entries from the MRU, select the **Server name** dropdown list, hover over the name of the server to remove, and then select the **Delete** key on your keyboard.

When you connect to Azure SQL Database, you can specify a database in the **Connect to Server** dialog box on the **Connection Properties** tab, which you can access by selecting **Options>>**.

By default, you are connected to the `master` database. If you specify a user database when you connect to Azure SQL Database, you only see that database and its objects in Object Explorer. If you connect to `master`, you can see all databases. For more information, see the [Microsoft Azure SQL Database Overview](/azure/sql-database/sql-database-technical-overview/).

#### Authentication

The current version of SQL Server Management Studio (SSMS) offers eight authentication modes when you connect to a [!INCLUDE [ssDE](../../includes/ssde-md.md)]. If your **Authentication** dialog box doesn't match the following list, download the most recent version of SSMS from [Download SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).

- **Windows Authentication**: [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows Authentication mode allows a user to connect through a Windows user account.

- **SQL Server Authentication**: When a user connects with a specified login name and password, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication by checking to see if a matching [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login account exists and if the specified password matches the one previously recorded. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login doesn't exist, authentication fails, and the user receives an error message.

- **Microsoft Entra Universal with MFA**: This interactive workflow supports Microsoft Entra multifactor authentication (MFA). MFA helps safeguard access to data and applications while meeting user demand for a simple sign-in process. It delivers strong authentication with a range of easy verification options, such as phone call, text message, smart cards with pin, and mobile app notifications. When the user account is configured for MFA, the interactive authentication workflow requires more user interaction through pop-up dialog boxes and smart card use. If the user account doesn't require MFA, the user can still use the other Microsoft Entra authentication options. For more information, see [Use Azure AD multifactor authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview).

- **Microsoft Entra Password**: This mechanism for connecting to [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] uses identities in Microsoft Entra. Use this method for connecting to [!INCLUDE [ssSDS](../../includes/sssds-md.md)] if you're logged in to Windows by using credentials from a domain that isn't federated with Azure, or when you use Microsoft Entra authentication based on the initial or the client domain. For more information, see [Use Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview).

- **Microsoft Entra Integrated**: This mechanism for connecting to [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] uses identities in Microsoft Entra. Use this method for connecting to [!INCLUDE [ssSDS](../../includes/sssds-md.md)] if you're logged in to Windows by using your Microsoft Entra credentials from a federated domain, or a managed domain that's configured for seamless single sign-on for pass-through and password hash authentication. For more information, see [Connect to SQL Database by using Azure AD authentication](/azure/azure-sql/database/authentication-aad-overview) and [Azure AD seamless single sign-on](/azure/active-directory/hybrid/connect/how-to-connect-sso).

- **Microsoft Entra Service Principal**: A service principal is a Microsoft Entra identity that you can create for use with automated tools, jobs, and applications. With the Service Principal authentication option, you can connect to your SQL instance by using the client ID and secret of a service principal identity. In SSMS, enter the client ID in the **User name** field, and enter the secret in the **Password** field. For more information, see [Azure AD server principals](/azure/azure-sql/database/authentication-azure-ad-logins) and [Azure AD server principal with Azure SQL](/azure/azure-sql/database/authentication-aad-service-principal).

- **Microsoft Entra Managed Identity**: Managed identities are special forms of service principals. There are two types of managed identities: system-assigned and user-assigned. System-assigned managed identities are enabled directly on an Azure resource. User-assigned managed identities are a standalone resource that you can assign to one or more Azure resources.

  To use a managed identity to connect to a SQL resource through GUI clients such as SSMS and ADS, the machine running the client application must have a Microsoft Entra client running with the identity's certificate stored in it. This requirement is most commonly achieved through an Azure VM because you can assign the identity to the machine through the VM's portal pane. For more information, see [Managed identities in Azure AD for Azure SQL](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity).

- **Microsoft Entra Default**: This option performs authentication based on password-less and noninteractive mechanisms, including managed identities, Visual Studio, Visual Studio Code, and the Azure CLI.

#### User name

The user name to connect with. This setting is read-only when you select **Windows Authentication** or **Microsoft Entra Integrated** authentication. The setting is prefilled with the Windows user name you're currently logged in with.

If you're connecting with **Microsoft Entra Universal with MFA**, **Microsoft Entra Password**, **Microsoft Entra Service Principal**, or **Microsoft Entra Default**, enter the name of the Microsoft Entra identity you're connecting with.

#### User assigned identity

This option appears when you connect with **Microsoft Entra Managed Identity**. Completing this option properly depends on the type of the identity.

#### Login

Enter the login you connect with. This option is only available if you connect using **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication**.

#### Password

Enter the password for the login. This option is only editable if you connect using **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication**, **Microsoft Entra Service Principal**, or **Microsoft Entra Password** authentication.

#### Remember password

Select this option for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to store the password entered. This option is displayed for the same authentication methods mentioned for the **Password** option.

#### Connect

Select to connect to the server.

#### Options

Select to display the **Connection Properties**, **Always Encrypted**, and **Additional Connection Parameters** tabs.

## Related content

- [Connect to Server (Connection Properties page) - Database Engine](connect-to-server-connection-properties-page-database-engine.md)
- [Connect to Server (Always Encrypted page) - Database Engine](connect-to-server-always-encrypted-page-database-engine.md)
- [Connect to Server (Additional Connection Parameters page) - Database Engine](connect-to-server-additional-connection-parameters-page-database-engine.md)
