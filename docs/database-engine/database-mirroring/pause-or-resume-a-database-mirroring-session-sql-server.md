---
title: "Pause & resume a database mirroring session"
description: Learn how to pause and resume a SQL Server database mirroring session using SQL Server Management Studio or Transact-SQL (T-SQL).
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "resuming database mirroring"
  - "database mirroring [SQL Server], sessions"
  - "database mirroring [SQL Server], pausing"
  - "database mirroring [SQL Server], resuming"
  - "pausing database mirroring"
---
# Pause or Resume a Database Mirroring Session (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to pause or resume database mirroring in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To ReplaceThisText, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After Pausing or Resuming Database Mirroring](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 At any time, you can suspend a database mirroring session, which might improve performance during bottlenecks, and you can resume a suspended session at any time.  
  
> [!CAUTION]  
>  After a forced service, when the original principal server reconnects, mirroring is suspended. Resuming mirroring in this situation could possibly cause data loss on the original principal server. For information about managing the potential data loss, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 To pause or resume a database mirroring session use the **Database Properties Mirroring** page.  
  
#### To pause or resume database mirroring  
  
1.  During a database mirroring session, connect to the principal server instance, in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and select the database.  
  
3.  Right-click the database, select **Tasks**, and then click **Mirror**. This opens the **Mirroring** page of the **Database Properties** dialog box.  
  
4.  To pause the session, click **Pause**.  
  
     A prompt asks for confirmation; if you click **Yes**, the session is paused, and the button changes to **Resume**.  
  
     For more information about the impact of pausing a session, see [Pausing and Resuming Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/pausing-and-resuming-database-mirroring-sql-server.md).  
  
5.  To resume the session, click **Resume**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To pause database mirroring  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for either partner.  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Issue the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
     ALTER DATABASE *database_name* SET PARTNER SUSPEND  
  
     where *database_name* is the mirrored database whose session you want to you want to suspend.  
  
     The following example pauses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.  
  
    ```  
    ALTER DATABASE AdventureWorks2012 SET PARTNER SUSPEND;  
    ```  
  
##### To resume database mirroring  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for either partner.  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Issue the following Transact-SQL statement:  
  
     ALTER DATABASE *database_name* SET PARTNER RESUME  
  
     where *database_name* is the mirrored database whose session you want to resume.  
  
     The following example pauses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.  
  
    ```  
    ALTER DATABASE AdventureWorks2012 SET PARTNER RESUME;  
    ```  
  
##  <a name="FollowUp"></a> Follow Up: After Pausing or Resuming Database Mirroring  
  
-   **After pausing database mirroring**  
  
     On the primary database, take precautions to avoid a full transaction log. For more information, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md).  
  
-   **After resuming database mirroring**  
  
     Resuming database mirroring places the mirror database in the SYNCHRONIZING state. If the safety level is FULL, the mirror catches up with the principal and the mirror database enters the SYNCHRONIZED state. At this point, failover becomes possible. If the witness is present and ON, automatic failover is possible. In the absence of a witness, manual failover is possible.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Remove Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-database-mirroring-sql-server.md)  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)  
  
  
