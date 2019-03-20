---
title: "Integration Services (SSIS) Connections | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.asvs.connectionmanager.f1"
  - "sql13.dts.designer.adddtsconnection.f1"
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
author: janinezhang
ms.author: janinez
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
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses the connection manager as a logical representation of a connection. At design time, you set the properties of a connection manager to describe the physical connection that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates when the package runs. For example, a connection manager includes the **ConnectionString** property that you set at design time; at run time, a physical connection is created using the value in the connection string property.  
  
 A package can use multiple instances of a connection manager type, and you can set the properties on each instance. At run time, each instance of a connection manager type creates a connection that has different attributes.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides different types of connection managers that enable packages to connect to a variety of data sources and servers:  
  
-   There are built-in connection managers that Setup installs when you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
-   There are connection managers that are available for download from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] website.  
  
-   You can create your own custom connection manager if the existing connection managers do not meet your needs.  

### Package level and project level connection managers
A connection manager can be created at the package level or at the project level. The connection manager created at the project level is available all the packages in the project. Whereas, connection manager created at the package level is available to that specific package.  
  
 You use connection managers that are created at the project level in place of data sources, to share connections to sources. To add a connection manager at the project level, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project must use the project deployment model. When a project is configured to use this model, the **Connection Managers** folder appears in **Solution Explorer**, and the **Data Sources** folder is removed from **Solution Explorer**.  
  
> [!NOTE]  
>  If you want to use data sources in your package, you need to convert the project to the package deployment model.  
>   
>  For more information about the two models, and about converting a project to the project deployment model, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md).

### Built-in Connection Managers  
 The following table lists the connection manager types that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides.  
  
|Type|Description|Topic|  
|----------|-----------------|-----------|  
|ADO|Connects to ActiveX Data Objects (ADO) objects.|[ADO Connection Manager](../../integration-services/connection-manager/ado-connection-manager.md)|  
|ADO.NET|Connects to a data source by using a .NET provider.|[ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md)|  
|CACHE|Reads data from the data flow or from a cache file (.caw), and can save data to the cache file.|[Cache Connection Manager](../../integration-services/connection-manager/cache-connection-manager.md)|  
|DQS|Connects to a Data Quality Services server and a Data Quality Services database on the server.|[DQS Cleansing Connection Manager](../../integration-services/connection-manager/dqs-cleansing-connection-manager.md)|  
|EXCEL|Connects to an Excel workbook file.|[Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md)|  
|FILE|Connects to a file or a folder.|[File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)|  
|FLATFILE|Connect to data in a single flat file.|[Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md)|  
|FTP|Connect to an FTP server.|[FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md)|  
|HTTP|Connects to a webserver.|[HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md)|  
|MSMQ|Connects to a message queue.|[MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md)|  
|MSOLAP100|Connects to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|[Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md)|  
|MULTIFILE|Connects to multiple files and folders.|[Multiple Files Connection Manager](../../integration-services/connection-manager/multiple-files-connection-manager.md)|  
|MULTIFLATFILE|Connects to multiple data files and folders.|[Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md)|  
|OLEDB|Connects to a data source by using an OLE DB provider.|[OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md)|  
|ODBC|Connects to a data source by using ODBC.|[ODBC Connection Manager](../../integration-services/connection-manager/odbc-connection-manager.md)|  
|SMOServer|Connects to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) server.|[SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md)|  
|SMTP|Connects to an SMTP mail server.|[SMTP Connection Manager](../../integration-services/connection-manager/smtp-connection-manager.md)|  
|SQLMOBILE|Connects to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact database.|[SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md)|  
|WMI|Connects to a server and specifies the scope of Windows Management Instrumentation (WMI) management on the server.|[WMI Connection Manager](../../integration-services/connection-manager/wmi-connection-manager.md)|  
  
### Connection Managers available for download  
 The following table lists additional types of connection manager that you can download from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] website.  
  
> [!IMPORTANT]  
>  The connection managers listed in the following table work only with [!INCLUDE[ssEnterpriseEd11](../../includes/ssenterpriseed11-md.md)] and [!INCLUDE[ssDeveloperEd11](../../includes/ssdevelopered11-md.md)].  
  
|Type|Description|Topic|  
|----------|-----------------|-----------|  
|ORACLE|Connects to an Oracle \<version info\> server.|The Oracle connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Oracle by Attunity. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Oracle by Attunity also includes a source and a destination. For more information, see the download page, [Microsoft Connectors for Oracle and Teradata by Attunity](https://go.microsoft.com/fwlink/?LinkId=251526).|  
|SAPBI|Connects to an SAP NetWeaver BI version 7 system.|The SAP BI connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for SAP BI. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for SAP BI also includes a source and a destination. For more information, see the download page, [Microsoft SQL Server 2008 Feature Pack](https://go.microsoft.com/fwlink/?LinkId=262016).|  
|TERADATA|Connects to a Teradata \<version info\> server.|The Teradata connection manager is the connection manager component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Teradata by Attunity. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector for Teradata by Attunity also includes a source and a destination. For more information, see the download page, [Microsoft Connectors for Oracle and Teradata by Attunity](https://go.microsoft.com/fwlink/?LinkId=251526).|  
  
### Custom Connection Managers  
 You can also write custom connection managers. For more information, see [Developing a Custom Connection Manager](../../integration-services/extending-packages-custom-objects/connection-manager/developing-a-custom-connection-manager.md).  
  
## Create connection managers
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a variety of connection managers to suit the needs of tasks that connect to different types of servers and data sources. Connection managers are used by the data flow components that extract and load data in different types of data stores, and by the log providers that write logs to a server, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, or file. For example, a package with a Send Mail task uses an SMTP connection manager type to connect to a Simple Mail Transfer Protocol (SMTP) server. A package with an Execute SQL task can use an OLE DB connection manager to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. For more information, see [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md).  
  
 To automatically create and configure connection managers when you create a new package, you can use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard. The wizard also helps you create and configure the sources and destinations that use the connection managers. For more information, see [Create Packages in SQL Server Data Tools](../../integration-services/create-packages-in-sql-server-data-tools.md).  
  
 To manually create a new connection manager and add it to an existing package, you use the **Connection Managers** area that appears on the **Control Flow**, **Data Flow**, and **Event Handlers** tabs of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. From the **Connection Manager** area, you choose the type of connection manager to create, and then set the properties of the connection manager by using a dialog box that [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides. For more information, see the section, "Using the Connection Managers Area," later in this topic.  
  
 After the connection manager is added to a package, you can use it in tasks, Foreach Loop containers, sources, transformations, and destinations. For more information, see [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md), [Foreach Loop Container](../../integration-services/control-flow/foreach-loop-container.md), and [Data Flow](../../integration-services/data-flow/data-flow.md).  
  
### Using the Connection Managers Area  
 You can create connection managers while the **Control Flow**, **Data Flow**, or **Event Handlers** tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer is active.  
  
 The following diagram shows the **Connection Managers** area on the **Control Flow** tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 ![Screenshot of control flow designer with package](../../integration-services/connection-manager/media/samplecontrolflow.gif "Screenshot of control flow designer with package")    
  
### 32-Bit and 64-Bit Providers for Connection Managers  
 Many of the providers that connection managers use are available in 32-bit and 64-bit versions. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] design environment is a 32-bit environment and you see only 32-bit providers while you are designing a package. Therefore, you can only configure a connection manager to use a specific 64-bit provider if the 32-bit version of the same provider is also installed.  
  
 At run time, the correct version is used, and it does not matter that you specified the 32-bit version of the provider at design time. The 64-bit version of the provider can be run even if the package is run in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
  Both versions of the provider have the same ID. To specify whether the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] runtime uses an available 64-bit version of the provider, you set the Run64BitRuntime property of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project. If the Run64BitRuntime property is set to **true**, the runtime finds and uses the 64-bit provider; if Run64BitRuntime is **false**, the runtime finds and uses the 32-bit provider. For more information about properties you can set on [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects, see [Integration Services &(SSIS) and Studio Environments](https://msdn.microsoft.com/library/ms140028.aspx).   

## Add a connection manager
###  <a name="wizard"></a> Add a connection manager when you create a package  
  
-   Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard  
  
     In addition to creating and configuring a connection manager, the wizard also helps you create and configure the sources and destinations that use the connection manager. For more information, see [Create Packages in SQL Server Data Tools](../../integration-services/create-packages-in-sql-server-data-tools.md).  
  
###  <a name="package"></a> Add a connection manager to an existing package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Control Flow** tab, the **Data Flow** tab, or the **Event Handler** tab to make the **Connection Managers** area available.  
  
4.  Right-click anywhere in the **Connection Managers** area, and then do one of the following:  
  
    -   Click the connection manager type to add to the package.  
  
         -or-  
  
    -   If the type that you want to add is not listed, click **New Connection** to open the **Add SSIS Connection Manager** dialog box, select a connection manager type, and then click **OK**.  
  
     The custom dialog box for the selected connection manager type opens. For more information about connection manager types and the options that are available, see the following options table.  
  
    |Connection manager|Options|  
    |------------------------|-------------|  
    |[ADO Connection Manager](../../integration-services/connection-manager/ado-connection-manager.md)|[Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md)|  
    |[ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md)|[Configure ADO.NET Connection Manager](../../integration-services/connection-manager/configure-ado-net-connection-manager.md)|  
    |[Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md)|[Add Analysis Services Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)|  
    |[Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md)|[Excel Connection Manager Editor](../../integration-services/connection-manager/excel-connection-manager-editor.md)|  
    |[File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)|[File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)|  
    |[Multiple Files Connection Manager](../../integration-services/connection-manager/multiple-files-connection-manager.md)|[Add File Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-file-connection-manager-dialog-box-ui-reference.md)|  
    |[Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md)|[Flat File Connection Manager Editor &#40;General Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-general-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-columns-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-advanced-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Preview Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-preview-page.md)|  
    |[Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md)|[Multiple Flat Files Connection Manager Editor &#40;General Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-general-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-columns-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Advanced Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-advanced-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-preview-page.md)|  
    |[FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md)|[FTP Connection Manager Editor](../../integration-services/connection-manager/ftp-connection-manager-editor.md)|  
    |[HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md)|[HTTP Connection Manager Editor &#40;Server Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-server-page.md)<br /><br /> [HTTP Connection Manager Editor &#40;Proxy Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-proxy-page.md)|  
    |[MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md)|[MSMQ Connection Manager Editor](../../integration-services/connection-manager/msmq-connection-manager-editor.md)|  
    |[ODBC Connection Manager](../../integration-services/connection-manager/odbc-connection-manager.md)|[ODBC Connection Manager UI Reference](../../integration-services/connection-manager/odbc-connection-manager-ui-reference.md)|  
    |[OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md)|[Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md)|  
    |[SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md)|[SMO Connection Manager Editor](../../integration-services/connection-manager/smo-connection-manager-editor.md)|  
    |[SMTP Connection Manager](../../integration-services/connection-manager/smtp-connection-manager.md)|[SMTP Connection Manager Editor](../../integration-services/connection-manager/smtp-connection-manager-editor.md)|  
    |[SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md)|[SQL Server Compact Edition Connection Manager Editor &#40;Connection Page&#41;](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager-editor-connection-page.md)<br /><br /> [SQL Server Compact Edition Connection Manager Editor &#40;All Page&#41;](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager-editor-all-page.md)|  
    |[WMI Connection Manager](../../integration-services/connection-manager/wmi-connection-manager.md)|[WMI Connection Manager Editor](../../integration-services/connection-manager/wmi-connection-manager-editor.md)|  
  
     The **Connection Managers** area lists the added connection manager.  
  
5.  Optionally, right-click the connection manager, click **Rename**, and then modify the default name of the connection manager.  
  
6.  To save the updated package, click **Save Selected Item** on the **File** menu.  
  
###  <a name="project"></a> Add a connection manager at the project level  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project.  
  
2.  In **Solution Explorer**, right-click **Connection Managers**, and click **New Connection Manager**.  
  
3.  In the **Add SSIS Connection Manager** dialog box, select the type of connection manager, and then click **Add**.  
  
     The custom dialog box for the selected connection manager type opens. For more information about connection manager types and the options that are available, see the following options table.  
  
    |Connection manager|Options|  
    |------------------------|-------------|  
    |[ADO Connection Manager](../../integration-services/connection-manager/ado-connection-manager.md)|[Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md)|  
    |[ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md)|[Configure ADO.NET Connection Manager](../../integration-services/connection-manager/configure-ado-net-connection-manager.md)|  
    |[Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md)|[Add Analysis Services Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)|  
    |[Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md)|[Excel Connection Manager Editor](../../integration-services/connection-manager/excel-connection-manager-editor.md)|  
    |[File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)|[File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)|  
    |[Multiple Files Connection Manager](../../integration-services/connection-manager/multiple-files-connection-manager.md)|[Add File Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-file-connection-manager-dialog-box-ui-reference.md)|  
    |[Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md)|[Flat File Connection Manager Editor &#40;General Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-general-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-columns-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-advanced-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Preview Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-preview-page.md)|  
    |[Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md)|[Multiple Flat Files Connection Manager Editor &#40;General Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-general-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-columns-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Advanced Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-advanced-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-preview-page.md)|  
    |[FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md)|[FTP Connection Manager Editor](../../integration-services/connection-manager/ftp-connection-manager-editor.md)|  
    |[HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md)|[HTTP Connection Manager Editor &#40;Server Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-server-page.md)<br /><br /> [HTTP Connection Manager Editor &#40;Proxy Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-proxy-page.md)|  
    |[MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md)|[MSMQ Connection Manager Editor](../../integration-services/connection-manager/msmq-connection-manager-editor.md)|  
    |[ODBC Connection Manager](../../integration-services/connection-manager/odbc-connection-manager.md)|[ODBC Connection Manager UI Reference](../../integration-services/connection-manager/odbc-connection-manager-ui-reference.md)|  
    |[OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md)|[Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md)|  
    |[SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md)|[SMO Connection Manager Editor](../../integration-services/connection-manager/smo-connection-manager-editor.md)|  
    |[SMTP Connection Manager](../../integration-services/connection-manager/smtp-connection-manager.md)|[SMTP Connection Manager Editor](../../integration-services/connection-manager/smtp-connection-manager-editor.md)|  
    |[SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md)|[SQL Server Compact Edition Connection Manager Editor &#40;Connection Page&#41;](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager-editor-connection-page.md)<br /><br /> [SQL Server Compact Edition Connection Manager Editor &#40;All Page&#41;](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager-editor-all-page.md)|  
    |[WMI Connection Manager](../../integration-services/connection-manager/wmi-connection-manager.md)|[WMI Connection Manager Editor](../../integration-services/connection-manager/wmi-connection-manager-editor.md)|  
  
     The connection manager you added will show up under the **Connections Managers** node in the **Solution Explorer**. It will also appear in the **Connection Managers** tab in the **SSIS Designer** window for all the packages in the project. The name of the connection manager in this tab will have a **(project)** prefix in order to differentiate this project level connection manager from the package level connection managers.  
  
4.  Optionally, right-click the connection manager in the **Solution Explorer** window under **Connection Managers** node (or) in the **Connection Managers** tab of the **SSIS Designer** window, click **Rename**, and then modify the default name of the connection manager.  
  
    > [!NOTE]  
    >  In the **Connection Managers** tab of the **SSIS Designer** window, you won't be able to overwrite the **(project)** prefix from the connection manager name. This is by design.  

### Add SSIS Connection Manager dialog box
Use the **Add SSIS Connection Manager** dialog box to select the type of connection to add to a package.  
  
 To learn more about connection managers, see [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md).  
  
#### Options  
 **Connection manager type**  
 Select a connection type and then click **Add**, or double-click a connection type, to specify connection properties using the editor for each type of connection.  
  
 **Add**  
 Specify connection properties using the editor for each type of connection.  
   
##  <a name="parameter"></a> Create a parameter for a connection manager property  
  
1.  In the **Connection Managers** area, right-click the connection manager that you want to create a parameter for and then click **Parameterize**.  
  
2.  Configure the parameter settings in the **Parameterize** dialog box. For more information, see [Parameterize Dialog Box](https://msdn.microsoft.com/library/fac02b6d-d247-447a-8940-e8700c7ac350).  

## Delete a connection manager 
###  <a name="DeletePackageLevel"></a> Delete a connection manager from a package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Control Flow** tab, the **Data Flow** tab, or the **Event Handler** tab to make the **Connection Managers** area available.  
  
4.  Right-click the connection manager that you want to delete, and then click **Delete**.  
  
     If you delete a connection manager that a package element, such as an Execute SQL task or an OLE DB source, uses, you will experience the following results:  
  
    -   An error icon appears on the package element that used the deleted connection manager.  
  
    -   The package fails to validate.  
  
    -   The package cannot be run.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
###  <a name="DeleteProjectLevel"></a> Delete a shared connection manager (project level connection manager)  
  
1.  To delete a project-level connection manager, right-click the connection manager under **Connection Managers** node in the **Solution Explorer** window, and then click **Delete**. [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] displays the following warning message:  
  
    > [!WARNING]  
    >  When you delete a project connection manager, packages that use the connection manager might not run. You cannot undo this action. Do you want to delete the connection manager?  
  
2.  Click OK to delete the connection manager or Cancel to keep it.  
  
    > [!NOTE]  
    >  You can also delete a project level connection manager from the **Connection Manager** tab of the **SSIS Designer** window opened for any package in the project. You do so by right-clicking the connection manager in the tab and then by clicking **Delete**. 
    
## Set the Properties of a Connection Manager
All connection managers can be configured using the **Properties** window.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] also provides custom dialog boxes for modifying the different types of connection managers in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. The dialog box has a different set of options depending on the connection manager type.  
  
### Modify a connection manager using the Properties window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In SSIS Designer, click the **Control Flow** tab, the **Data Flow** tab, or the **Event Handler** tab to make the **Connection Managers** area available.  
  
4.  Right-click the connection manager and click **Properties**.  
  
5.  In the **Properties** window, edit the property values. The **Properties** window provides access to some properties that are not configurable in the standard editor for a connection manager.  
  
6.  Click **OK**.  
  
7.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### Modify a connection manager using a connection manager dialog box  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Control Flow** tab, the **Data Flow** tab, or the **Event Handler** tab to make the **Connection Managers** area available.  
  
4.  In the **Connection Managers** area, double-click the connection manager to open the **Connection Manager** dialog box. For information about specific connection manager types, and the options available for each type, see the following table.  
  
    |Connection manager|Options|  
    |------------------------|-------------|  
    |[ADO Connection Manager](../../integration-services/connection-manager/ado-connection-manager.md)|[Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md)|  
    |[ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md)|[Configure ADO.NET Connection Manager](../../integration-services/connection-manager/configure-ado-net-connection-manager.md)|  
    |[Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md)|[Add Analysis Services Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)|  
    |[Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md)|[Excel Connection Manager Editor](../../integration-services/connection-manager/excel-connection-manager-editor.md)|  
    |[File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)|[File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)|  
    |[Multiple Files Connection Manager](../../integration-services/connection-manager/multiple-files-connection-manager.md)|[Add File Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-file-connection-manager-dialog-box-ui-reference.md)|  
    |[Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md)|[Flat File Connection Manager Editor &#40;General Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-general-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-columns-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-advanced-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Preview Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-preview-page.md)|  
    |[Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md)|[Multiple Flat Files Connection Manager Editor &#40;General Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-general-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-columns-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Advanced Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-advanced-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../../integration-services/connection-manager/multiple-flat-files-connection-manager-editor-preview-page.md)|  
    |[FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md)|[FTP Connection Manager Editor](../../integration-services/connection-manager/ftp-connection-manager-editor.md)|  
    |[HTTP Connection Manager](../../integration-services/connection-manager/http-connection-manager.md)|[HTTP Connection Manager Editor &#40;Server Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-server-page.md)<br /><br /> [HTTP Connection Manager Editor &#40;Proxy Page&#41;](../../integration-services/connection-manager/http-connection-manager-editor-proxy-page.md)|  
    |[MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md)|[MSMQ Connection Manager Editor](../../integration-services/connection-manager/msmq-connection-manager-editor.md)|  
    |[ODBC Connection Manager](../../integration-services/connection-manager/odbc-connection-manager.md)|[ODBC Connection Manager UI Reference](../../integration-services/connection-manager/odbc-connection-manager-ui-reference.md)|  
    |[OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md)|[Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md)|  
    |[SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md)|[SMO Connection Manager Editor](../../integration-services/connection-manager/smo-connection-manager-editor.md)|  
    |[SMTP Connection Manager](../../integration-services/connection-manager/smtp-connection-manager.md)|[SMTP Connection Manager Editor](../../integration-services/connection-manager/smtp-connection-manager-editor.md)|  
    |[SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md)|[SQL Server Compact Edition Connection Manager Editor &#40;Connection Page&#41;](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager-editor-connection-page.md)<br /><br /> [SQL Server Compact Edition Connection Manager Editor &#40;All Page&#41;](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager-editor-all-page.md)|  
    |[WMI Connection Manager](../../integration-services/connection-manager/wmi-connection-manager.md)|[WMI Connection Manager Editor](../../integration-services/connection-manager/wmi-connection-manager-editor.md)|  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  

## Related Content  
  
-   Video, [Leverage Microsoft Attunity Connector for Oracle to enhance Package Performance](https://technet.microsoft.com/sqlserver/gg598963.aspx), on technet.microsoft.com  
  
-   Wiki articles, [SSIS Connectivity](https://social.technet.microsoft.com/wiki/contents/articles/sql-server-integration-services-ssis.aspx#Connectivity), on social.technet.microsoft.com  
  
-   Blog entry, [Connecting to MySQL from SSIS](https://go.microsoft.com/fwlink/?LinkId=217669), on blogs.msdn.com.  
  
-   Technical article, [Extracting and Loading SharePoint Data in SQL Server Integration Services](https://go.microsoft.com/fwlink/?LinkId=247826), on msdn.microsoft.com.  
  
-   Technical article, [You get "DTS_E_CANNOTACQUIRECONNECTIONFROMCONNECTIONMANAGER" error message when using Oracle connection manager in SSIS](https://go.microsoft.com/fwlink/?LinkId=233696), on support.microsoft.com.  
  
  
