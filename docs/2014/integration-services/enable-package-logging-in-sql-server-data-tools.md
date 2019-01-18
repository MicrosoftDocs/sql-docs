---
title: "Enable Package Logging in SQL Server Data Tools | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [Integration Services], enabling"
ms.assetid: b69a8593-5bb0-4f04-87d2-f8e7bd7eb4fc
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Enable Package Logging in SQL Server Data Tools
  This procedure describes how to add logs to a package, configure package-level logging, and save the logging configuration to an XML file. You can add logs only at the package level, but the package does not have to perform logging to enable logging in the containers that the package includes.  
  
> [!IMPORTANT]  
>  If you deploy the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server, the logging level that you set for the package execution overrides the package logging that you configure using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
 By default, the containers in the package use the same logging configuration as their parent container. For information about setting logging options for individual containers, see [Configure Logging by Using a Saved Configuration File](../../2014/integration-services/configure-logging-by-using-a-saved-configuration-file.md).  
  
### To enable logging in a package  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  On the **SSIS** menu, click **Logging**.  
  
3.  Select a log provider in the **Provider type** list, and then click **Add**.  
  
4.  In the **Configuration** column, select a connection manager or click **\<New connection>** to create a new connection manager of the appropriate type for the log provider. Depending on the selected provider, use one of the following connection managers:  
  
    -   For Text files, use a File connection manager. For more information, see [File Connection Manager](connection-manager/file-connection-manager.md)  
  
    -   For [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)], use a File connection manager.  
  
    -   For [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], use an OLE DB connection manager. For more information, see [OLE DB Connection Manager](connection-manager/ole-db-connection-manager.md).  
  
    -   For Windows Event Log, do nothing. [!INCLUDE[ssIS](../includes/ssis-md.md)] automatically creates the log.  
  
    -   For XML files, use a File connection manager.  
  
5.  Repeat steps 3 and 4 for each log to use in the package.  
  
    > [!NOTE]  
    >  A package can use more than one log of each type.  
  
6.  Optionally, select the package-level check box, select the logs to use for package-level logging, and then click the **Details** tab.  
  
7.  On the **Details** tab, select **Events** to log all log entries, or clear **Events** to select individual events.  
  
8.  Optionally, click **Advanced** to specify which information to log.  
  
    > [!NOTE]  
    >  By default, all information is logged.  
  
9. On the **Details** tab, click **Save.** The **Save As** dialog box appears. Locate the folder in which to save the logging configuration, type a file name for the new log configuration, and then click **Save**.  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md)   
 [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md)  
  
  
