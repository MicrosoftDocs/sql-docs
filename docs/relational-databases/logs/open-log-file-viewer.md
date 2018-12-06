---
title: "Open Log File Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Log File Viewer, opening"
ms.assetid: a86b89cb-0432-4648-895a-05ecc5450e45
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Open Log File Viewer
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can use Log File Viewer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to access information about errors and events that are captured in the following logs:  
  
-   Audit Collection  
  
-   Data Collection  
  
-   Database Mail  
  
-   Job History  
  
-   SQL Server  
  
-   SQL Server Agent  
  
-   Windows events (These Windows events can also be accessed from Event Viewer.)  
  
 Beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], you can use Registered Servers to view [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log files from local or remote instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By using Registered Servers, you can view the log files when the instances are either online or offline. For more information about online access, see the procedure "To view online log files from Registered Servers" later in this topic. For more information about how to access offline [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log files, see [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md).  
  
 You can open Log File Viewer in several ways, depending on the information that you want to view.  
  
##  <a name="BeforeYouBegin"></a> Permissions  
 To access log files for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are online, this requires membership in the securityadmin fixed server role.  
  
 To access log files for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are offline, you must have read access to both the **Root\Microsoft\SqlServer\ComputerManagement10** WMI namespace, and to the folder where the log files are stored. For more information, see the Security section of the topic [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md).  
  
### Security  
 Requires membership in the securityadmin fixed server role.  
  
### View Log Files  
  
##### To view logs that are related to general SQL Server activity  
  
1.  In Object Explorer, expand **Management**.  
  
2.  Do either of the following:  
  
    -   Right-click **SQL Server Logs**, point to **View**, and then click either **SQL Server Log** or **SQL Server and Windows Log**.  
  
    -   Expand **SQL Server Logs**, right-click any log file, and then click **View SQL Server Log**. You can also double-click any log file.  
  
     The logs include **Database Mail**, **SQL Server**, **SQL Server Agent**, and **Windows NT**.  
  
##### To view logs that are related to jobs  
  
-   In Object Explorer, expand **SQL Server Agent**, right-click **Jobs**, and then click **View History**.  
  
     The logs include **Database Mail**, **Job History**, and **SQL Server Agent**.  
  
##### To view logs that are related to maintenance plans  
  
-   In Object Explorer, expand **Management**, right-click **Maintenance Plans**, and then click **View History**.  
  
     The logs include **Database Mail**, **Job History**, **Maintenance Plans**, **Remote Maintenance Plans**, and **SQL Server Agent**.  
  
##### To view logs that are related to Data Collection  
  
-   In Object Explorer, expand **Management**, right-click **Data Collection**, and then click **View Logs**.  
  
     The logs include **Data Collection**, **Job History**, and **SQL Server Agent**.  
  
##### To view logs that are related to Database Mail  
  
-   In Object Explorer, expand **Management**, right-click **Database Mail**, and then click **View Database Mail Log**.  
  
     The logs include **Database Mail, Job History**, **Maintenance Plans**, **Remote Maintenance Plans**, **SQL Server**, **SQL Server Agent**, and **Windows NT**.  
  
##### To view logs that are related to audits collections  
  
-   In Object Explorer, expand **Security**, expand **Audits**, right-click an audit, and then click **View Audit Logs**.  
  
     The logs include **Audit Collection** and **Windows NT**.  
  
##### To view logs that are related to audits collections  
  
-   In Object Explorer, expand **Security**, expand **Audits**, right-click an audit, and then click **View Audit Logs**.  
  
     The logs include **Audit Collection** and **Windows NT**.  
  
## See Also  
 [Log File Viewer](../../relational-databases/logs/log-file-viewer.md)   
 [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md)   
 [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md)  
  
  
