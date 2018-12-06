---
title: "Set the Polling Interval for Target Servers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "interval for polling [SQL Server]"
  - "target servers [SQL Server], polling interval"
  - "polling interval [SQL Server]"
ms.assetid: 4ffbbefa-77fb-442e-a77c-cb8c6cab9f3c
author: stevestein
ms.author: sstein
manager: craigg
---
# Set the Polling Interval for Target Servers
  This topic describes how to set the frequency that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent refreshes information from the master to the target servers. A job is a specified series of actions that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent performs. A multiserver job is a job that a master server runs on one or more target servers.  
  
-   **Before you begin:**  [Security](#Security)  
  
-   **To set the polling interval for target servers, using:**  [SQL Server Management Studio](#SSMS), [Transact-SQL](#TSQL)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 Each target server can run one instance of the same job at the same time. Each target server periodically polls the master server, downloads a copy of any new jobs assigned to the target server, and then disconnects. The target server runs the job locally and then reconnects to the master server to upload the job outcome status.  
  
> [!NOTE]  
>  If the master server is inaccessible when the target server tries to upload job status, the job status is spooled until the master server can be accessed.  
  
###  <a name="Security"></a> Security  
 For detailed information, see [Implement SQL Server Agent Security](implement-sql-server-agent-security.md) and [Choose the Right SQL Server Agent Service Account for Multiserver Environments](choose-the-right-sql-server-agent-service-account-for-multiserver-environments.md).  
  
##  <a name="SSMS"></a> Using SQL Server Management Studio  
 **To set the polling interval for target servers**  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Right-click **SQL Server Agent**, point to **Multi Server Administration**, and then click **Manage Target Servers**.  
  
3.  On the **Target Server Status** tab, click **Post Instructions**.  
  
4.  In the **Instruction type** list, select **Set polling interval**.  
  
5.  In the **Polling interval** box, enter the number of seconds from 10 through 28,800 that must pass before the target server polls the master server.  
  
6.  Under **Recipients**, do one of the following:  
  
    1.  Click **All target servers** if all target servers share the same polling interval.  
  
    2.  Click **These target servers** if not all target servers share the same polling interval, and then select each target server that will use this polling interval.  
  
##  <a name="TSQL"></a> Using Transact-SQL  
 **To set the polling interval for target servers**  
  
1.  In Object Explorer, connect to an instance of the Database Engine, and then expand that instance.  
  
2.  On the toolbar, click **New Query**.  
  
3.  In the query window, use the [sp_post_msx_operation &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-post-msx-operation-transact-sql) system stored procedure to set the polling interval for target servers.  
  
## See Also  
 [dbo.sysdownloadlist &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/dbo-sysdownloadlist-transact-sql)  
  
  
