---
title: "Remove the Witness from a Database Mirroring Session (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "witness [SQL Server], turning off"
  - "witness [SQL Server], removing"
  - "database mirroring [SQL Server], witness"
ms.assetid: f3ce7afc-8936-4d35-80ce-d0f8fbc318d3
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Remove the Witness from a Database Mirroring Session (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to remove a witness from a database mirroring session in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. At any time during a database mirroring session, the database owner can turn off the witness for a database mirroring session.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To Replace remove the witness, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After Removing the Witness](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To remove the witness  
  
1.  Connect to the principal server instance and, in the **Object Explorer** pane, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and select the database whose witness you want to remove.  
  
3.  Right-click the database, select **Tasks**, and then click **Mirror**. This opens the **Mirroring** page of the **Database Properties** dialog box.  
  
4.  To remove the witness, delete its server network address from the **Witness** field.  
  
    > [!NOTE]  
    >  If you switch from high-safety mode with automatic failover to high-performance mode, the **Witness** field is automatically cleared.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To remove the witness  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on either partner server instance.  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Issue the following statement:  
  
     [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md) *database_name* SET WITNESS OFF  
  
     where *database_name* is the name of the mirrored database.  
  
     The following example removes the witness from the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
    ```  
    ALTER DATABASE AdventureWorks2012 SET WITNESS OFF ;  
    ```  
  
##  <a name="FollowUp"></a> Follow Up: After Removing the Witness  
 Turning off the witness changes the [operating mode](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)in accordance with the transaction-safety setting:  
  
-   If transaction safety is set to FULL (the default), the session uses high-safety, synchronous mode without automatic failover.  
  
-   If transaction safety is set to OFF, the session operates asynchronously (in high-performance mode) without requiring quorum. Whenever transaction safety is turned off, we strongly recommend also turning the witness off.  
  
> [!TIP]  
>  The transaction safety setting of the database is recorded on each partner in the [sys.database_mirroring](../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md) catalog view in the **mirroring_safety_level** and **mirroring_safety_level_desc** columns.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Add a Database Mirroring Witness Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/add-a-database-mirroring-witness-using-windows-authentication-transact-sql.md)  
  
-   [Add or Replace a Database Mirroring Witness &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/add-or-replace-a-database-mirroring-witness-sql-server-management-studio.md)  
  
## See Also  
 [ALTER DATABASE Database Mirroring &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)   
 [Change Transaction Safety in a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/change-transaction-safety-in-a-database-mirroring-session-transact-sql.md)   
 [Add a Database Mirroring Witness Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/add-a-database-mirroring-witness-using-windows-authentication-transact-sql.md)   
 [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md)  
  
  
