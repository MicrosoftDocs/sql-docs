---
title: Connect to Server (Login page) - Database Engine
description: This article shows how to use the Connect to Server (Login page) Database Engine.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 04/18/2024
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.connection.login.sqlserver.f1"
  - "sql13.swb.connecttosqlserver.login.f1"
  - "sql13.swb.connectoserverunknownservertype.f1"
---

# Connect to Server (Login page) - Database Engine

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use this tab to configure the connection properties when you connect to [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. In most cases, you can connect by entering the computer name of the database server in the **Server name** box and then selecting **Connect**. If you're connecting to a named instance, use the computer name followed by a backslash and then the instance name. For example, `mycomputer\myinstance`. See [Connect to the Database Engine](../../sql-server/connect-to-database-engine.md) for more examples.

[!INCLUDE [entra-id-hard-coded](../../includes/entra-id-hard-coded.md)]

Many factors can affect your ability to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For help, see the following resources:

- [Lesson 1: Connecting to the Database Engine](../../relational-databases/lesson-1-connecting-to-the-database-engine.md)

- [Troubleshoot Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)

- [Troubleshoot connectivity issues in SQL Server](/troubleshoot/sql/database-engine/connect/resolve-connectivity-errors-overview)

> [!NOTE]  
> To connect with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be configured in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication mode. For more information about determining the authentication mode and changing the authentication mode, see [Change server authentication mode](../../database-engine/configure-windows/change-server-authentication-mode.md).

## Connect to a server

You can connect to any supported server from the login page by providing the server name and authentication details, as noted in the following sections.

### Server type

When you register a server from Object Explorer, select the type of server to connect to: [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)], or [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)]. The dialog box only shows the options for the selected server type. When you register a server from **Registered Servers**, the **Server type** box is read-only and matches the server type displayed in the **Registered Servers** component. To register a different type of server, select [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE [ssEW](../../includes/ssew-md.md)], or [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] from the **Registered Servers** toolbar before you start to register a new server.

### Server name

Select the server instance you want to connect to. The most recent server instance you connected to, appears by default.

To connect to an instance of [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)], connect using the named pipes protocol specifying the pipe name, such as `np:\\.\pipe\3C3DF6B1-2262-47\tsql\query`. For more information, see the [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] documentation.

> [!NOTE]  
> Connections are persisted in the Most Recently Used (MRU) history. Select the **Server name** dropdown list to remove entries from the MRU, hover over the server name to remove, and then select the **Delete** key on your keyboard.

When you connect to Azure SQL Database, you can specify a database in the **Connect to Server** dialog box on the **Connection Properties** tab, which you can access by selecting **Options>>**.

By default, you connect to the `master` database. If you specify a user database when you connect to Azure SQL Database, you only see that database and its objects in Object Explorer. If you connect to `master`, you can see all databases. For more information, see the [Microsoft Azure SQL Database Overview](/azure/sql-database/sql-database-technical-overview/).

### Authentication

The current version of SQL Server Management Studio (SSMS) offers eight authentication modes when you connect to a [!INCLUDE [ssDE](../../includes/ssde-md.md)]. If your **Authentication** dialog box doesn't match the following list, download the [most recent version of SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).

- **Windows Authentication**: [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows Authentication mode allows a user to connect through a Windows user account.

- **SQL Server Authentication**: When you connect with a specified login name and password, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication by checking to see if a matching [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login account exists and if the specified password matches the one previously recorded. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login doesn't exist, authentication fails, and you receive an error message.

- **Microsoft Entra MFA**: This interactive workflow supports Microsoft Entra multifactor authentication (MFA). MFA helps safeguard access to data and applications while meeting user demand for a simple sign-in process. It delivers strong authentication with various easy verification options like phone calls, text messages, smart cards with pins, and mobile app notifications. When the user account is configured for MFA, the interactive authentication workflow requires more user interaction through pop-up dialog boxes and smart card use. If the user account doesn't require MFA, you can still use the other Microsoft Entra authentication options. For more information, see [Using Microsoft Entra multifactor authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview).

- **Microsoft Entra Password**: This method for connecting to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] uses identities and their passwords in Microsoft Entra ID. It's useful when your Windows login credentials aren't in an Azure federated domain, or the initial or client domain is using Microsoft Entra authentication. For more information, see [Use Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview).

- **Microsoft Entra Integrated**: This mechanism for connecting to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] uses Windows identities federated with Microsoft Entra ID. Use this method for connecting to [!INCLUDE [ssSDS](../../includes/sssds-md.md)] if you're logged in to Windows and using your credentials from a federated domain, or a managed domain configured for seamless single sign-on for pass-through and password hash authentication. For more information, see [Use Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview) and [Microsoft Entra seamless single sign-on](/entra/identity/hybrid/connect/how-to-connect-sso).

- **Microsoft Entra Service Principal**: A service principal is a Microsoft Entra identity that you can create for use with automated tools, jobs, and applications. With Service Principal authentication, you can connect to your SQL instance by using the client ID and secret of a service principal identity. In SSMS, enter the client ID in the **User name** field and the secret in the **Password** field. For more information, see [Microsoft Entra server principals](/azure/azure-sql/database/authentication-azure-ad-logins) and [Microsoft Entra service principal with Azure SQL](/azure/azure-sql/database/authentication-aad-service-principal).

- **Microsoft Entra Managed Identity**: Managed identities are special forms of service principals. There are two types of managed identities: system-assigned and user-assigned. System-assigned managed identities are enabled directly on an Azure resource. User-assigned managed identities are a standalone resource you can assign to one or more Azure resources.

  To use a managed identity to connect to a SQL resource through GUI clients such as SSMS and ADS, the machine running the client application must have a Microsoft Entra client running with the identity's certificate stored in it. This requirement is most commonly achieved through an Azure VM because you can assign the identity to the machine through the VM's portal pane. For more information, see [Managed identities in Microsoft Entra for Azure SQL](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity).

- **Microsoft Entra Default**: This option performs authentication based on passwordless and noninteractive mechanisms, including managed identities, Visual Studio, Visual Studio Code, and the Azure CLI.

### User name

The user name to connect. This setting is read-only when you select **Windows Authentication** or **Microsoft Entra Integrated** authentication. The setting is prefilled with the current login with your Windows user name.

If you connect with **Microsoft Entra Universal with MFA**, **Microsoft Entra Password**, **Microsoft Entra Service Principal**, or **Microsoft Entra Default**, enter the name of the Microsoft Entra identity you're connecting with.

### User assigned identity

This option appears when you connect with **Microsoft Entra Managed Identity**. Completing this option properly depends on the type of the identity.

### Login

Enter the login you connect with. This option is only available if you connect using **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication**.

### Password

Enter the password for the login. This option is only editable if you choose to connect using **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication**, **Microsoft Entra Service Principal**, or **Microsoft Entra Password** authentication.

### Remember password

Select this option for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to store the password entered. This option is displayed for the same authentication methods mentioned for the **Password** option.

### Encryption

Select the level of encryption for the connection. The options for [!INCLUDE [ssms20-md](../../includes/ssms20-md.md)] are *Strict (SQL Server 2022 and Azure SQL)*, *Mandatory*, and *Optional*. When enabled, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses TLS encryption for all the data sent between the client and server. For more information, see [SQL Server and client encryption summary](../../database-engine/configure-windows/sql-server-and-client-encryption-summary.md).

[!INCLUDE [ssms-encryption](../includes/ssms-encryption.md)]

*Mandatory* encryption can be used for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] when the instance has **Force Encryption** enabled. It can also be used when no encryption is configured for the instance, if **Trust server certificate** is enabled.  While this method is less secure than installing a trusted certificate, it does support an encrypted connection.

The **Encryption** property appears on the Login page for SSMS 20.x and later versions.

### Trust server certificate

When enabled, with *Optional* or *Mandatory* encryption selected, or if the server is configured to force encryption, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] doesn't validate the server certificate on the client machine when encryption is enabled for the network communication between client and server.

The **Trust server certificate** property appears on the Login page for SSMS 20.x and later versions.

### Host name in the certificate

The value provided in this option is used to specify a different, but expected, CN or SAN in the server certificate for the server to which SSMS is connecting. This option can be left blank, so that certificate validation ensures that the Common Name (CN) or Subject Alternate Name (SAN) in the certificate matches the server name to which you're connecting. This parameter can be populated when the server name doesn't match the CN or SAN, for example, when using DNS aliases. For more information, see [Encryption and certificate validation in Microsoft.Data.SqlClient](../../connect/ado-net/encryption-and-certificate-validation.md). |

The Encryption property appears on the Login page for SSMS 20.x and later versions.

### Connect

Select to connect to the server.

### Options

Select to collapse the connection dialog or expand the dialog to display the **Connection Properties**, **Always Encrypted**, and **Additional Connection Parameters** tabs.

## Related content

- [Connect to server (Connection Properties page) - Database Engine](connect-to-server-connection-properties-page-database-engine.md)
- [Connect to Server (Always Encrypted page) - Database Engine](connect-to-server-always-encrypted-page-database-engine.md)
- [Connect to Server (Additional Connection Parameters page) - Database Engine](connect-to-server-additional-connection-parameters-page-database-engine.md)
