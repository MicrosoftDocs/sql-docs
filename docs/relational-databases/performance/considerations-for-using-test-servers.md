---
title: "Considerations for Using Test Servers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "overhead [Database Engine Tuning Advisor]"
  - "tuning overhead [SQL Server]"
  - "reducing production server tuning load"
  - "Database Engine Tuning Advisor [SQL Server], test servers"
  - "xp_msver"
  - "test servers [Database Engine Tuning Advisor]"
  - "production servers [SQL Server]"
  - "offload tuning overhead [SQL Server]"
ms.assetid: 94e6c3e5-1f09-4616-9da2-4e44d066d494
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Considerations for Using Test Servers
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Using a test server to tune a database on a production server is an important benefit of [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor. Using this feature, you can offload tuning overhead to a test server without copying the actual data over to the test server from the production server.  
  
> [!NOTE]  
>  The test server tuning feature is not supported in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor graphical user interface (GUI).  
  
 To use this feature successfully, review the considerations listed in the following sections.  
  
## Setting Up the Test Server/Production Server Environment  
  
-   The user who wants to use a test server to tune a database on a production server must exist on both servers, or this scenario will not work.  
  
-   The extended stored procedure, **xp_msver**, must be enabled to use the test server/production server scenario. [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor uses this extended stored procedure to fetch the number of processors and the available memory of the production server to use while tuning the test server. If **xp_msver** is not enabled, [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor assumes the hardware characteristics of the computer where [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor is running. If the hardware characteristics of the computer where [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor is running are not available, one processor and 1024 megabytes (MBs) of memory are assumed. This extended stored procedure is turned on by default when you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md) and [xp_msver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-msver-transact-sql.md).  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor expects the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be the same on both the test server and the production server. If there are two different editions, the edition on the test server takes precedence. For example, if the test server is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard, [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor will not include indexed views, partitioning, and online operations in its recommendations even if the production server is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise.  
  
## About Test Server/Production Server Behavior  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor takes into account hardware differences between the production and the test server when creating recommendations. The recommendation is the same as though the tuning was done on the production server alone.  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor may impose some load on the production server for gathering metadata as well as creation of statistics necessary for tuning.  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor does not copy actual data from the production server to the test server. It only copies the metadata of the databases and the necessary statistics.  
  
-   All session information is stored in **msdb** on the production server. This allows you to exploit any available test server for tuning, and information about all sessions is available in one place (the production server).  
  
## Issues Related to the Shell Database  
  
-   After tuning, [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor should remove any metadata that it created on the test server during the tuning process. This includes the shell database. If you are performing a series of tuning sessions with the same production and test servers, you may want to retain this shell database to save time. In the XML input file, specify the **RetainShellDB** subelement with the other sub elements under the **TuningOptions** parent element. Using these options causes [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor to retain the shell database. For more information, see [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md).  
  
-   Shell databases may be left behind on the test server after a successful test server/production server tuning session even if you have not specified the **RetainShellDB** subelement. These unwanted shell databases may interfere with subsequent tuning sessions and should be dropped before performing another test server/production server tuning session. In addition, if a tuning session exits unexpectedly, the shell databases on the test server and the objects within those databases may be left behind on the test server. You should also delete these databases and objects before starting a new test server/production server tuning session.  
  
## Issues Related to the Tuning Process  
  
-   The user must check the tuning log for tuning errors that result from differences between the production and test servers, and for errors that result from copying metadata from the production to the test server. For example, a user login may not exist on the test server. If a user login does not exist on the test server, those events in the workload that are issued by that user login may not be tunable. Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor GUI to view the tuning log. For more information, see [View and Work with the Output from the Database Engine Tuning Advisor](../../relational-databases/performance/view-and-work-with-the-output-from-the-database-engine-tuning-advisor.md)  
  
-   If [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor cannot tune many events because objects are missing in the shell database that [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor creates on the test server, the user must check the tuning log. Events that cannot be tuned are listed in the log. To successfully tune the database on the test server, the user must create the missing objects in the shell database, and then start a new tuning session.  
  
-   If a database with the same name already exists on the test server, [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor does not copy metadata, but continues tuning and gathers statistics as necessary. This is useful if the user has already created a database on the test server and has copied the appropriate metadata before invoking [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor.  
  
-   If the DATE_CORRELATION_OPTIMIZATION option is turned on for a database on the production server, metadata and the data associated with this option are not completely scripted while tuning the test server. When tuning is performed for a test server/production server scenario, the following issues may apply:  
  
    -   Users can have different query plans on the servers for queries that use the DATE_CORRELATION_OPTIMIZATION option.  
  
    -   [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor may suggest dropping indexed views that enforce the DATE_CORRELATION_OPTIMIZATION option in the recommendation script.  
  
     Therefore, you may want to ignore any recommendations that [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor makes about the indexed views that hold correlation statistics because [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor knows their costs but not their benefits. [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor may not recommend selection of certain indexes such as clustered indexes on **datetime** columns, which could be beneficial when DATE_CORRELATION_OPTIMIZATION is enabled.  
  
     To determine if a view is based on correlation statistics, select the **is_date_correlation_view** column of the [sys.views](../../relational-databases/system-catalog-views/sys-views-transact-sql.md) catalog view.  
  
  
