---
title: "Integration Services (SSIS) Connections | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, connections"
  - "SSIS packages, connections"
  - "sources [Integration Services], connections"
  - "packages [Integration Services], connections"
  - "destinations [Integration Services], connections"
  - "tasks [Integration Services], connections"
  - "connections [Integration Services], about connections"
  - "connections [Integration Services]"
  - "SQL Server Integration Services packages, connections"
ms.assetid: 72f5afa3-d636-410b-9e81-2ffa27772a8c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services (SSIS) Connections
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages use connections to perform different tasks and to implement [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] features:  
  
-   Connecting to source and destination data stores such as text, XML, Excel workbooks, and relational databases to extract and load data.  
  
-   Connecting to relational databases that contain reference data to perform exact or fuzzy lookups.  
  
-   Connecting to relational databases to run SQL statements such as SELECT, DELETE, and INSERT commands and also stored procedures.  
  
-   Connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to perform maintenance and transfer tasks such as backing up databases and transferring logins.  
  
-   Writing log entries in text and XML files and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables and package configurations to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables.  
  
-   Connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to create temporary work tables that some transformations require to do their work.  
  
-   Connecting to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] projects and databases to access data mining models, process cubes and dimensions, and run DDL code.  
  
-   Specifying existing or creating new files and folders to use with Foreach Loop enumerators and tasks.  
  
-   Connecting to message queues and to Windows Management Instrumentation (WMI), [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO), Web, and mail servers.  
  
 To make these connections, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses connection managers, as described in the next section.  
  
## Connection Managers  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses the connection manager as a logical representation of a connection. At design time, you set the properties of a connection manager to describe the physical connection that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates when the package runs. For example, a connection manager includes the `ConnectionString` property that you set at design time; at run time, a physical connection is created using the value in the connection string property.  
  
 A package can use multiple instances of a connection manager type, and you can set the properties on each instance. At run time, each instance of a connection manager type creates a connection that has different attributes.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides different types of connection managers that enable packages to connect to a variety of data sources and servers:  
  
-   There are built-in connection managers that Setup installs when you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
-   There are connection managers that are available for download from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] website.  
  
-   You can create your own custom connection manager if the existing connection managers do not meet your needs.  
  
### Built-in Connection Managers  
 The following table lists the connection manager types that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides.  
  
|Type|Description|Topic|  
|----------|-----------------|-----------|  
|ADO|Connects to ActiveX Data Objects (ADO) objects.|[ADO Connection Manager](ado-connection-manager.md)|  
|ADO.NET|Connects to a data source by using a .NET provider.|[ADO.NET Connection Manager](ado-net-connection-manager.md)|  
|CACHE|Reads data from the data flow or from a cache file (.caw), and can save data to the cache file.|[Cache Connection Manager](cache-connection-manager.md)|  
|DQS|Connects to a Data Quality Services server and a Data Quality Services database on the server.|[DQS Cleansing Connection Manager](dqs-cleansing-connection-manager.md)|  
|EXCEL|Connects to an Excel workbook file.|[Excel Connection Manager](excel-connection-manager.md)|  
|FILE|Connects to a file or a folder.|[File Connection Manager](file-connection-manager.md)|  
|FLATFILE|Connect to data in a single flat file.|[Flat File Connection Manager](flat-file-connection-manager.md)|  
|FTP|Connect to an FTP server.|[FTP Connection Manager](ftp-connection-manager.md)|  
|HTTP|Connects to a webserver.|[HTTP Connection Manager](http-connection-manager.md)|  
|MSMQ|Connects to a message queue.|[MSMQ Connection Manager](msmq-connection-manager.md)|  
|MSOLAP100|Connects to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|[Analysis Services Connection Manager](analysis-services-connection-manager.md)|  
|MULTIFILE|Connects to multiple files and folders.|[Multiple Files Connection Manager](multiple-files-connection-manager.md)|  
|MULTIFLATFILE|Connects to multiple data files and folders.|[Multiple Flat Files Connection Manager](multiple-flat-files-connection-manager.md)|  
|OLEDB|Connects to a data source by using an OLE DB provider.|[OLE DB Connection Manager](ole-db-connection-manager.md)|  
|ODBC|Connects to a data source by using ODBC.|[ODBC Connection Manager](odbc-connection-manager.md)|  
|SMOServer|Connects to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) server.|[SMO Connection Manager](smo-connection-manager.md)|  
|SMTP|Connects to an SMTP mail server.|[SMTP Connection Manager](smtp-connection-manager.md)|  
|SQLMOBILE|Connects to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.|[SQL Server Compact Edition Connection Manager](sql-server-compact-edition-connection-manager.md)|  
|WMI|Connects to a server and specifies the scope of Windows Management Instrumentation (WMI) management on the server.|[WMI Connection Manager](wmi-connection-manager.md)|  
  
### Connection Managers Available for Download  
 The following table lists additional types of connection manager that you can download from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] website.  
  
> [!IMPORTANT]  
>  The connection managers listed in the following table work only with [!INCLUDE[ssEnterpriseEd11](../../includes/ssenterpriseed11-md.md)] and [!INCLUDE[ssDeveloperEd11](../../includes/ssdevelopered11-md.md)].  
  
|Type|Description|Topic|  
|----------|-----------------|-----------|  
|ORACLE|Connects to an Oracle \<version info> server.|The Oracle connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Oracle by Attunity. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Oracle by Attunity also includes a source and a destination. For more information, see the download page, [Microsoft Connectors for Oracle and Teradata by Attunity](https://go.microsoft.com/fwlink/?LinkId=251526).|  
|SAPBI|Connects to an SAP NetWeaver BI version 7 system.|The SAP BI connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for SAP BI. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for SAP BI also includes a source and a destination. For more information, see the download page, [Microsoft SQL Server 2008 Feature Pack](https://go.microsoft.com/fwlink/?LinkId=262016).|  
|TERADATA|Connects to a Teradata \<version info> server.|The Teradata connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Teradata by Attunity. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Teradata by Attunity also includes a source and a destination. For more information, see the download page, [Microsoft Connectors for Oracle and Teradata by Attunity](https://go.microsoft.com/fwlink/?LinkId=251526).|  
  
### Custom Connection Managers  
 You can also write custom connection managers. For more information, see [Developing a Custom Connection Manager](../extending-packages-custom-objects/connection-manager/developing-a-custom-connection-manager.md).  
  
## Related Tasks  
 For details about how to add or delete a connection manager in a package, see [Add, Delete, or Share a Connection Manager in a Package](../add-delete-or-share-a-connection-manager-in-a-package.md).  
  
 For details about how to set the properties of a connection manager in a package, see [Set the Properties of a Connection Manager](../set-the-properties-of-a-connection-manager.md).  
  
## Related Content  
  
-   Video, [Leverage Microsoft Attunity Connector for Oracle to enhance Package Performance](https://technet.microsoft.com/sqlserver/gg598963.aspx), on technet.microsoft.com  
  
-   Wiki articles, [SSIS Connectivity](https://social.technet.microsoft.com/wiki/contents/articles/sql-server-integration-services-ssis.aspx#Connectivity), on social.technet.microsoft.com  
  
-   Blog entry, [Connecting to MySQL from SSIS](https://go.microsoft.com/fwlink/?LinkId=217669), on blogs.msdn.com.  
  
-   Technical article, [Extracting and Loading SharePoint Data in SQL Server Integration Services](https://go.microsoft.com/fwlink/?LinkId=247826), on msdn.microsoft.com.  
  
-   Technical article, [You get "DTS_E_CANNOTACQUIRECONNECTIONFROMCONNECTIONMANAGER" error message when using Oracle connection manager in SSIS](https://go.microsoft.com/fwlink/?LinkId=233696), on support.microsoft.com.  
  
  
