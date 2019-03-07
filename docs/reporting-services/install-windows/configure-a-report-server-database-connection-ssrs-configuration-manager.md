---
title: "Configure a Report Server Database Connection  (SSRS Configuration Manager) | Microsoft Docs"
author: markingmyname
ms.author: maghan
manager: kfile
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.topic: conceptual
ms.custom: seodec18
ms.date: 12/12/2018
---

# Configure a Report Server Database Connection (SSRS Configuration Manager)

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

Each report server instance requires a connection to the report server database that stores reports, report models, shared data sources, resources, and metadata managed by the server. The initial connection can be created during a report server installation if you are installing the default configuration. In most cases, you will use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to configure the connection after Setup is complete. You can modify the connection at any time to change the account type or reset credentials. For step-by-step instructions on how to create the database and configure the connection, see [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).

 You must configure a report server database connection in the following circumstances:  
  
- Configuring a report server for first use.  
  
- Configuring a report server to use a different report server database.  
  
- Changing the user account or password that is used for the database connection. You only need to update the database connection when the account information is stored in the RSReportServer.config file. If you are using the service account for the connection (which uses Windows integrated security as the credential type), the password is not stored, eliminating the need to update the connection information. For more information about changing accounts, see [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
- Configuring a report server scale-out deployment. Configuring a scale-out deployment requires that you create multiple connections to a report server database. For more information about how to perform this multi-step operation, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).  
  
## How Reporting Services Connects to the Database Engine

 Report server access to a report server database depends on credentials and connection information, and on encryption keys that are valid for the report server instance that uses that database. Having valid encryption keys is necessary for storing and retrieving sensitive data. Encryption keys are created automatically when you configure the database for the first time. After the keys are created, you must update them if you change the Report Server service identity. For more information about working with encryption keys, see [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).  
  
 The report server database is an internal component, accessed only by the report server. The credentials and connection information you specify for the report server database are used exclusively by the report server. Users who request reports do not require databases permissions or a database login for the report server database.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses **System.Data.SqlClient** to connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that hosts the report server database. If you are using a local instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], the report server will establish the connection using shared memory. If you are using a remote database server for the report server database, you might have to enable remote connections depending on the edition you are using. If you are using the Enterprise edition, remote connections are enabled for TCP/IP by default.  
  
 To verify that the instance accepts remote connections, click **Start**, click **All Programs**, click [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], click **Configuration Tools**, click **SQL Server Configuration Manager**, and then verify that the TCP/IP protocol is enabled for each service.  
  
 When you enable remote connections, the client and server protocols will also be enabled. To verify the protocols are enabled, click **Start**, click **All Programs**, click [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], click **Configuration Tools**, click **SQL Server Configuration Manager**, click **SQL Server Network Configuration**, and then click **Protocols for MSSQLSERVER**. For more information, see [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Defining a Report Server Database Connection

 To configure the connection, you must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager tool or the **rsconfig** command line utility. A report server requires the following connection information:  
  
- Name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance hosting the report server database..  
  
- Name of the report server database. When creating a connection for the first time, you can create a new report server database or select an existing database. For more information, see [Create a Report Server Database;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md).  
  
- Credential type. You can use the service accounts, a Windows domain account, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login.  
  
- User name and password (required only if you are using Windows domain account or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login).

 The credentials that you provide must be granted access to the report server database. If you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, this step is performed automatically. For more information about the permissions required to access the database, see the "Database Permissions" section in this topic.  
  
### Storing Database Connection Information

 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores and encrypts the connection information in the following RSreportserver.config settings. You must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or rsconfig utility to create encrypted values for these settings.  
  
 Not all of the values are set for every type of connection. If you configure the connection using the default values (that is, using the service accounts to make the connection), \<**LogonUser**>, \<**LogonDomain**>, and \<**LogonCred**> will be empty, as follows:  
  
```
<Dsn></Dsn>  
<ConnectionType></ConnectionType>  
<LogonUser></LogonUser>  
<LogonDomain></LogonDomain>  
<LogonCred></LogonCred>  
```
  
 If you configure the connection to use a specific Windows account or database login, you must remember to update the values that are stored if you subsequently change the account or login.  
  
### Choosing a Credential Type

 There are three types of credentials that can be used in a connection to a report server database:  
  
- Windows integrated security using the Report Server service account. Because the report server is implemented as a single service, only the account under which the service runs requires database access.  
  
- A Windows user account. If the report server and the report server database are installed on the same computer, you can use a local account. Otherwise, you must use a domain account.  
  
- A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.
  
> [!NOTE]
> A custom authentication extension cannot be used to connect to a report server database. Custom authentication extensions are used only to authenticate a principal to a report server. They do not affect connections to the report server database or to external data sources that provide content to reports.
  
 If the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is configured for Windows Authentication and is in the same domain or a trusted domain with the report server computer, you can configure the connection to use the service account or a domain user account that you manage as a connection property through the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. If the database server is in a different domain or if you are using workgroup security, you must configure the connection to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login. In this case, be sure to encrypt the connection.  
  
#### Using Service Accounts and Integrated Security

 You can use Windows integrated security to connect through the Report Server service account. The account is granted login rights to the report server database. This is the default credential type chosen by Setup if you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the default configuration.  
  
 The service account is a trusted account that provides a low-maintenance approach to managing a report server database connection. Because the service account uses Windows integrated security to make the connection, the credentials do not have to be stored. However, if you subsequently change the service account password or identity (for example, switching from a built-in account to a domain account), be sure to use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to make the change. The tool automatically updates the database permissions to use the revised account information. For more information, see [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
 If you configure the database connection to use the service account, the account must have network permissions if the report server database is on a remote computer. Do not use the service account if the report server database is on a different domain, behind a firewall, or if you are using workgroup security instead of domain security. Use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user account instead.  
  
#### Using a Domain User Account

 You can specify a Windows user account for the report server connection to the report server database. If you use a local or domain account, you must update the report server database connection every time you change the password or the account. Always use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to update the connection.  
  
#### Using a SQL Server Login

 You can specify a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to connect to the report server database. If you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication and the report server database is on a remote computer, use IPSec to help secure the transmission of data between the servers. If you use a database login, you must update the report server database connection every time you change the password or the account.  
  
### Database Permissions

 Accounts used to connect to the report server database are granted the following roles:  
  
- **public** and **RSExecRole** roles for the **ReportServer** database.  
  
- **RSExecRole** role for the **master**, **msdb**, and **ReportServerTempDB** databases.  
  
 When you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to create or modify the connection, these permissions are granted automatically. If you use the rsconfig utility, and you are specifying a different account for the connection, you must update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for that new account. You can create script files in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool that will update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for the report server.  
  
### Verifying the Database Name

 Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to determine which report server database is used by a particular report server instance. To find the name, connect to the report server instance and open the Database Setup page.  
  
## Using a Different Report Server Database or Moving a Report Server Database

 You can configure a report server instance to use a different report server database by changing the connection information. A common case for switching databases is when you deploy a production report server. Switching from a test report server database to a production report server database is typically how production servers are rolled out. You can also move a report server database to another computer. For more information, see [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Configuring Multiple Reports Servers to Use the Same Report Server Database

 You can configure multiple report servers to use the same report server database. This deployment configuration is called a scale-out deployment. This configuration is a prerequisite if you want to run multiple report servers in a server cluster. However, you can also use this configuration if you want to segment service applications or if you want to test the installation and settings of a new report server instance to compare it with an existing report server installation. For more information, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).  

## Next steps

[Create a Report Server Database](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)   
[Manage a Reporting Services Native Mode Report Server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)   
[Configure the Report Server Service Account](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
