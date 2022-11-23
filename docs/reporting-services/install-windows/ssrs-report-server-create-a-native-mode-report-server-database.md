---
title: "Create a Native Mode Report Server Database (Configuration Manager) | Microsoft Docs"
description: "Native mode Reporting Services uses a SQL Server database for internal storage. The database is required and it is used to store published reports, models, shared data sources, session data, resources, and server metadata."
ms.date: 12/04/2019
ms.service: reporting-services
ms.custom: seo-lt-2019​, seo-mmd-2019

ms.topic: conceptual
helpviewer_keywords: 
  - "report servers [Reporting Services], databases"
  - "databases [Reporting Services], creating"
ms.assetid: 81b9f4ad-800b-4688-8b47-a5a83dc8ff10
author: maggiesMSFT
ms.author: maggies
---

# Create a Native Mode Report Server Database (Report Server Configuration Manager)

[!INCLUDE[ssrs-appliesto-sql2016-preview](../../includes/ssrs-appliesto-sql2016-preview.md)]

Native mode [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database for internal storage. The database is required and it is used to store published reports, models, shared data sources, session data, resources, and server metadata.  

To create a report server database or to change the connection string or credentials, use the options in the Database page in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager.  
  
## When to Create or Configure the Report Server Databases  
 You must create and configure the report server database if you installed the report server in files-only mode.  
  
 If you installed [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the default configuration for native mode, the report server database was created and configured automatically when the report server instance was installed. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to view or modify the settings that Setup configured for you.  
  
##  <a name="rsdbrequirements"></a> Before You Start  
 Creating or configuring a report server database is a multi-step process. Before you create the report server database, consider how you want to specify the following items:  
  
 **Select a database server**  
 Review the supported versions of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and review the supported editions in the topic, [Create a Report Server Database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md).  
  
 **Enable TCP/IP connections**  
 Enable TCP/IP connections for the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Some editions of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] do not enable TCP/IP by default. Instructions are provided in this topic.  
  
 **Open port for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**  
 For a remote server, if you are using firewall software, you must open the port that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] listens on.  
  
 **Decide on report server credentials**  
 Decide how the report server will connect to the report server databases. Credential types include domain user account, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user account, or the Report Server service account.  
  
 These credentials are encrypted and stored in the RSReportServer.config file. The report server uses these credentials for ongoing connections to the report server database. If you want to use a Windows user account or a database user account, be sure to specify one that already exists. Although the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager will create a login and set the necessary permissions, it will not create an account for you. For more information, see [Configure a Report Server Database Connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
 **Decide on a report server language**  
 Choose a language to specify for the report server. Predefined role names, descriptions, and the My Reports folders do not appear in different languages when users connect to the server using different language versions of a browser.  
  
 **Check credentials to create and provision the database**  
 Verify that you have account credentials that have permission to create databases on the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance. These credentials are used for a one-time connection to create the report server database and **RSExecRole**. If a login does not already exist, a database user login will be created for the account used by the report server to connect to the database. You can connect under the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account you are logged in as, or you can enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login.  
  
### To enable access to a remote report server database  
  
1.  If you are using a remote [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance, log on to the database server to verify or enable TCP/IP connections.  
  
2.  Point to **Start**, point to **All Programs**, point to **Microsoft SQL Server**, point to **Configuration Tools**, and click **SQL Server Configuration Manager**.  
  
3.  Open **SQL Server Network Configuration**.  
  
4.  Select the Database instance.  
  
5.  Right-click **TCP/IP** and select **Enabled**.  
  
6.  Restart the service.  
  
7.  Open your firewall software and open the port that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens on. For the default instance, this is typically port 1433 for TCP/IP connections. For more information, see [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).  
  
### To create a local report server database  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and connect to the report server instance for which you are creating the database. For more information, see [Report Server Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).  
  
2.  On the Database page, select **Change Database**.  
  
3.  Select **Create a new report server database**, and then select **Next**.  
  
4.  Connect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that you will use to create and host the report server database:  
  
    1.  Type the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance that you want to use. The wizard will display a local [!INCLUDE[ssDE](../../includes/ssde-md.md)] that runs as the default instance if it is available. Otherwise, you must type the server and instance to use. Named instances are specified in this format: \<servername>\\<instancename\>.  
  
    2.  Enter the credentials used for a one-time connection to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for the purpose of creating the report server databases. For more information about how these credentials are used, see [Before You Start](#rsdbrequirements) in this topic.  
  
    3.  Select **Test Connection** to validate the connection to the server.  
  
    4.  Select **Next**.  
  
5.  Specify properties used to create the database. For more information about how these properties are used, see [Before You Start](#rsdbrequirements) in this topic:  
  
    1.  Type the name of the report server database. A temporary database is created along with the primary database. Consider using a descriptive name to help you remember how the database is used. Note that the name you specify will be used for the lifetime of the database. You cannot rename a report server database after it is created.  
  
    2.  Select the language in which you want role definitions and My Reports to appear.  
  
    3.  The Report Server Mode is always set to **Native**.  
  
    4.  Select **Next**.  
  
6.  Specify the credentials used by the report server to connect to the report server database.  
  
    1.  Specify the authentication type:  
  
         Select **Database Credentials** to connect using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login that is already defined. Using database credentials is recommended if the report server is on a computer that is in a different domain, a non-trusted domain, or behind a firewall.  
  
         Select **Windows Credentials** if you have a least-privileged domain user account that has permission to log on to the computer and the database server.  
  
         Select **Service Credentials** if you want the report server to connect using its service account. With this option, the server connects using integrated security; credentials are not encrypted or stored.  
  
    2.  Select **Next**.  
  
7.  Review the information on the Summary page to verify the settings are correct, and then select **Next**.  
  
8.  Verify the connection by selecting a URL on the Report Server URL page. The URLs must be defined in order for this test to work. If the report server database connection is valid, you will see the report server folder hierarchy. For more information, see [Verify a Reporting Services Installation](../../reporting-services/install-windows/verify-a-reporting-services-installation.md).  

## Change database credentials

The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager provides the Change Credentials Wizard to guide you through the steps of reconfiguring the account that the report server uses to connect to the report server database. When you change credentials, the Configuration Manager will update all permissions and database login information on the database server for the report server database that is actively used by the report server. 

1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and connect to the report server instance for which you are creating the database. For more information, see [Report Server Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).  
  
2.  On the Database page, select **Change Credentials**. 

3.  Connect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that you will use to create and host the report server database:  
  
    1.  Enter the credentials used for a one-time connection to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for the purpose of creating the report server databases. For more information about how these credentials are used, see [Before You Start](#rsdbrequirements) in this topic.  
  
    2.  Select **Test Connection** to validate the connection to the server.  
  
    3.  Select **Next**.  

4.  Specify the credentials used by the report server to connect to the report server database.  
  
    1.  Specify the authentication type:  
  
         Select **Database Credentials** to connect using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login that is already defined. Using database credentials is recommended if the report server is on a computer that is in a different domain, a non-trusted domain, or behind a firewall.  
  
         Select **Windows Credentials** if you have a least-privileged domain user account that has permission to log on to the computer and the database server.  
  
         Select **Service Credentials** if you want the report server to connect using its service account. With this option, the server connects using integrated security; credentials are not encrypted or stored.  
  
    2.  Select **Next**. 

5. Review the settings and select **Next**.

6. After the changes are made select **Finish**.

## Next steps

[Configure a Report Server Database Connection](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
[Manage a Reporting Services Native Mode Report Server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)   
[Report Server Configuration Manager](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
