---
title: "Configure a report server database connection (Report Server Configuration Manager)"
description: "Each report server instance requires a connection to the report server database that stores reports, shared data sources, resources, and metadata managed by the server."
author: maggiesMSFT
ms.author: maggies
ms.date: 08/01/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
#customer-intent: As an SQL Server administrator, I want to understand how to configure and manage a SQL Server report server database connection to ensure seamless report server operations.
---

# Configure a report server database connection (Report Server Configuration Manager)

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

In this article, you learn the purpose of the SQL Server report server database connection and essential information about how it works. This information includes how to configure it and what considerations to keep in mind during configuration. 

Each report server instance requires a connection to the report server database that stores reports, shared data sources, resources, and metadata managed by the server. You can create the initial connection during a report server installation if you install the default configuration. In most cases, you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to configure the connection after Setup is complete. Modify the connection at any time to change the account type or reset credentials. For more information on how to create the database and configure the connection, see [Create a Native mode report server database (Report Server Configuration Manager)](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).

## When to configure a report server database connection

You must configure a report server database connection in the following circumstances:

- **First-time configuration**: Configure the connection when you first use the report server.
- **Database changes**: Configure a report server to use a different report server database.
- **Account changes**: Configure the connection if the user account name or password changes.
   > [!NOTE]
   > Update the connection when the account information is stored in the `RSReportServer.config` file. If you use the service account, which uses Windows integrated security, the password isn't stored. For more information about changing accounts, see [Configure the report server service account (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).
- **Scale-out deployment**: If you configure a scale-out deployment, you must create multiple connections to a report server database. For more information about how to perform this multi-step operation, see [Configure a Native mode report server scale-out deployment (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).

## How Reporting Services connects to the database engine

Report server access to a report server database depends on:

- **Credentials and connection information**: Specified for the report server database and used exclusively by the report server.
- **Encryption keys**: Necessary for storing and retrieving sensitive data. Created automatically when you configure the database for the first time. After the keys are created, you must update them if you change the Report Server service identity. For more information about working with encryption keys, see [Configure and manage encryption keys (Report Server Configuration Manager)](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).

The report server database is an internal component, accessed only by the report server. The credentials and connection information you specify for the report server database are used exclusively by the report server. Users who request reports don't require databases permissions or a database sign-in for the report server database.

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses **System.Data.SqlClient** to connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that hosts the report server database. If you're using a local instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], the report server establishes the connection using shared memory. If you use a remote database server for the report server database, you might have to enable remote connections depending on the edition that you use. If you use the Enterprise edition, remote connections are enabled for TCP/IP by default.

You can verify that the instance accepts remote connections by opening SQL Server Configuration Manager and ensuring that the TCP/IP protocol is enabled for each service. Enabling remote connections also activates the necessary client and server protocols. To confirm that these protocols are enabled, open SQL Server Configuration Manager, navigate to **SQL Server Network Configuration** and choose **Protocols for MSSQLSERVER**. For more information, see [Enable or disable a server network protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Define a report server database connection

To configure the connection, use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager tool or the **rsconfig** command line utility. A report server requires the following connection information:

- **Name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance**: The name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance hosting the report server database.
- **Name of the report server database**: When creating a connection for the first time, you can create a new report server database or select an existing database. For more information, see [Create a report server database, Report Server Configuration Manager](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md).
- **Credential type**: You can use the service accounts, a Windows domain account, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database sign-in credentials.
- **User name and password**: Required only if you're using Windows domain account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign-in credentials.

The credentials that you provide must be granted access to the report server database. If you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, this step occurs automatically. For more information about the permissions required to access the database, see the [How Reporting Services connects to the database engine](#how-reporting-services-connects-to-the-database-engine) section in this article.

## Store database connection information

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores and encrypts the connection information in the following `RSreportserver.config` settings. Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or **rsconfig** utility to create encrypted values for these settings.

Not all of the values are set for every type of connection. If you configure the connection using the default values by using the service accounts to make the connection, \<**LogonUser**>, \<**LogonDomain**>, and \<**LogonCred**> are empty, as follows:

```
<Dsn></Dsn>
<ConnectionType></ConnectionType>
<LogonUser></LogonUser>
<LogonDomain></LogonDomain>
<LogonCred></LogonCred>
```

If you configure the connection to use a specific Windows account or database sign-in credentials, you must remember to update the stored values if you then change the account or sign-in credentials.

## Choose a credential type

There are three types of credentials that you can use in a connection to a report server database:

- **Windows integrated security with the Report Server service account**: Because the report server is implemented as a single service, only the account under which the service runs requires database access.
- **Windows user account**: If the report server and the report server database are installed on the same computer, you can use a local account. Otherwise, use a domain account.
- **SQL Server sign-in credentials**: Use SQL Server sign-in credentials to authenticate and connect to the report server database. This option is useful when the database server is in a different domain or when using workgroup security instead of domain security.

> [!NOTE]
> You can't use a custom authentication extension to connect to a report server database. Custom authentication extensions are used only to authenticate a principal to a report server. They don't affect connections to the report server database or to external data sources that provide content to reports.

::: moniker range=">=sql-server-ver15"

> [!NOTE]
> When you use Azure SQL Managed Instance to host report server databases, SQL Server authentication is the only supported credential type. In addition, Managed Instance can't host report server instance.

::: moniker-end

### Use Windows integrated security with the Report Server service account

You can use Windows integrated security to connect through the Report Server service account. The account is granted sign-in rights to the report server database. This credential type is the default chosen by Setup if you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the default configuration.

The service account is a trusted account that provides a low-maintenance approach to managing a report server database connection. Because the service account uses Windows integrated security to make the connection, the credentials don't have to be stored. However, if you then change the service account password or identity, be sure to use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to make the change. The tool automatically updates the database permissions to use the revised account information. For more information, see [Configure the report server service account (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).

If you configure the database connection to use the service account, the account must have network permissions if the report server database is on a remote computer. Don't use the service account if the report server database is on a different domain, behind a firewall, or if you're using workgroup security instead of domain security. Use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user account instead.

If you configure the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for Windows Authentication and the instance is in the same domain or a trusted domain with the report server computer, you can configure the connection to use the service account or a domain user account that you manage as a connection property through the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. If the database server is in a different domain, or if you use workgroup security, configure the connection to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database sign-in credentials. In this case, be sure to encrypt the connection.

### Use a Windows user account

You can specify a Windows user account for the report server connection to the report server database. If you use a local or domain account, you must update the report server database connection every time you change the password or the account. Always use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to update the connection.

### Use SQL Server sign-in credentials

You can specify one set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign-in credentials to connect to the report server database. If you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication and the report server database is on a remote computer, use IPSec to help secure the transmission of data between the servers. If you use database sign-in credentials, you must update the report server database connection every time you change the password or the account.

## Database permissions

Accounts used to connect to the report server database are granted the following roles:

- **public** and **RSExecRole** roles for the **ReportServer** database.
- **RSExecRole** role for the **master**, **msdb**, and **ReportServerTempDB** databases.

When you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to create or modify the connection, these permissions are granted automatically. If you use the **rsconfig** utility, and you specify a different account for the connection, update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign-in for that new account. You can create script files in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool that update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign-in credentials for the report server.

## Verify the database name

Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to determine which report server database is used by a particular report server instance. To find the name, connect to the report server instance and open the Database Setup page.

## Use a different report server database or move a report server database

You can configure a report server instance to use a different report server database by changing the connection information. A common case for switching databases is when you deploy a production report server. Switching from a test report server database to a production report server database is typically how production servers are rolled out. You can also move a report server database to another computer. For more information, see [Upgrade and migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Configure multiple report servers to use the same report server database

You can configure multiple report servers to use the same report server database. This deployment configuration is called a scale-out deployment. This configuration is a prerequisite if you want to run multiple report servers in a server cluster. However, you can also use this configuration if you want to segment service applications. You can also use it to test the installation and settings of a new report server instance to compare it with an existing report server installation. For more information, see [Configure a Native mode report server scale-out deployment](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).

## Related content

- [Create a report server database](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)
- [Manage a Reporting Services Native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)
- [Configure the report server service account](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)

More questions? Try asking the [Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
