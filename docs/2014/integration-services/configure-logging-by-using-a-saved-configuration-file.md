---
title: "Configure Logging by Using a Saved Configuration File | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "containers [Integration Services], logs"
  - "logs [Integration Services], containers"
ms.assetid: e5fdbbcb-94ca-4912-aa7c-0d89cebbd308
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Configure Logging by Using a Saved Configuration File
  This procedure describes how to configure logging for new containers in a package by loading a previously saved logging configuration file.  
  
 By default, all containers in a package use the same logging configuration as their parent container. For example, the tasks in a Foreach Loop use the same logging configuration as the Foreach Loop.  
  
### To configure logging for a container  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  On the **SSIS** menu, click **Logging**.  
  
3.  Expand the package tree view and select the container to configure.  
  
4.  On the **Providers and Logs** tab, select the logs to use for the container.  
  
    > [!NOTE]  
    >  You can create logs only at the package level. For more information, see [Enable Package Logging in SQL Server Data Tools](../../2014/integration-services/enable-package-logging-in-sql-server-data-tools.md).  
  
5.  Click the **Details** tab and click **Load**.  
  
6.  Locate the logging configuration file you want to use and click **Open**.  
  
7.  Optionally, select a different log entry to log by selecting its check box in the **Events** column. Click **Advanced** to select the type of information to log for this entry.  
  
    > [!NOTE]  
    >  The new container may include additional log entries that are not available for the container originally used to create the logging configuration. These additional log entries must be selected manually if you want them to be logged.  
  
8.  To save the updated version of the logging configuration, click **Save**.  
  
9. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md)  
  
  
