---
title: "Manually Fail Over a Database Mirroring Session (SQL Server Management Studio)"
description: Learn how to initiate manual failover to a mirror server by using SQL Server Management Studio. The mirror database then becomes the principal database.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
helpviewer_keywords:
  - "failover [SQL Server], database mirroring"
  - "manual failover [SQL Server]"
  - "database mirroring [SQL Server], failover"
---
# Manually Fail Over a Database Mirroring Session (SQL Server Management Studio)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  When the mirrored database is synchronized (that is, when the database is in the SYNCHRONIZED state), the database owner can initiate manual failover to the mirror server.  
  
 During a manual failover, the principal and mirror server roles are swapped for the database on which the failover occurs. The mirror database becomes the principal database and the principal database becomes the mirror. For example, the following table shows the how a manual failover swaps the roles of two mirroring partners: `SQLDBENGINE0_1` and `SQLDBENGINE0_2`.  
  
|Server|Before failover|After failover|  
|------------|---------------------|--------------------|  
|`SQLDBENGINE0_1`|PRINCIPAL|MIRROR|  
|`SQLDBENGINE0_2`|MIRROR|PRINCIPAL|  
  
 Note that the server roles for other database mirroring sessions are not affected. For more information, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).  
  
### To manually fail over database mirroring  
  
1.  Connect to the principal server instance and, in the **Object Explorer** pane, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and select the database to be failed over.  
  
3.  Right-click the database, select **Tasks**, and then click **Mirror**. This opens the **Mirroring** page of the **Database Properties** dialog box.  
  
4.  Click **Failover**.  
  
     A confirmation box appears.  The principal server begins by trying to connect to the mirror server by using Windows Authentication. If Windows Authentication does not work, the principal server displays the **Connect to Server** dialog box. If the mirror server uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, select **SQL Server Authentication** in the **Authentication** box. In the **Login** text box, specify the login account to connect with on the mirror server, and in the **Password** text box, specify the password for that account.  
  
     If failover succeeds, the **Database Properties** dialog box closes. The mirror database becomes the principal database and the principal database becomes the mirror.  
  
     If failover fails, an error message is displayed and the dialog box remains open.  
  
    > [!IMPORTANT]  
    >  If you have modified any properties since opening the **Mirroring** page, those changes will not be saved.  
  
     The dialog box closes automatically.  
  
## See Also  
 [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Manually Fail Over a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-transact-sql.md)   
 [Pause or Resume a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/pause-or-resume-a-database-mirroring-session-sql-server.md)   
 [Remove Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-database-mirroring-sql-server.md)  
  
  
