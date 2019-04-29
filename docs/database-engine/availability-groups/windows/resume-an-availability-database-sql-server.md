---
title: "Resume an Availability Database (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.availabilitygroup.resumedatamove.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], resuming a database"
  - "secondary databases [SQL Server], in availability group"
  - "primary databases [SQL Server], in availability group"
  - "Availability Groups [SQL Server], databases"
ms.assetid: 20e9147b-e985-4caa-910e-fc4b38dbf9a1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Resume an Availability Database (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can resume a suspended availability database in [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. Resuming a suspended database puts the database into the SYNCHRONIZING state. Resuming the primary database also resumes any of its secondary databases that were suspended as the result of suspending the primary database. If any secondary database was suspended locally, from the server instance that hosts the secondary replica, that secondary database must be resumed locally. Once a given secondary database and the corresponding primary database are in the SYNCHRONIZING state, data synchronization resumes on the secondary database.  
  
> [!NOTE]  
>  Suspending and resuming an Always On secondary database does not directly affect the availability of the primary database. However, suspending a secondary database can impact redundancy and failover capabilities for the primary database, until the suspended secondary database is resumed. This is in contrast to database mirroring, where the mirroring state is suspended on both the mirror database and the principal database until mirroring is resumed. Suspending an Always On primary database suspends data movement on all the corresponding secondary databases, and redundancy and failover capabilities cease for that database until the primary database is resumed.  
  
  
  
## Limitations and Restrictions  
 A RESUME command returns as soon as it has been accepted by the replica that hosts the target database, but actually resuming the database occurs asynchronously.  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be connected to the server instance that hosts the database to be resumed.    
-   The availability group must be online.    
-   The primary database must be online and available.  
  
  
##  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To resume a secondary database**  
  
1.  In Object Explorer, connect to the server instance that hosts the availability replica on which you want to resume a database, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Expand the availability group.  
  
4.  Expand the **Availability Databases** node, right-click the database, and click **Resume Data Movement**.  
  
5.  In the **Resume Data Movement** dialog box, click **OK**.  
  
> [!NOTE]  
>  To resume additional databases on this replica location, repeat steps 4 and 5 for each database.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To resume a secondary database that was suspended locally**  
  
1.  Connect to the server instance that hosts the secondary replica whose database you want to resume.  
  
2.  Resume the secondary database by using the following [ALTER DATABASE](../../../t-sql/statements/alter-database-transact-sql-set-hadr.md)statement:  
  
     ALTER DATABASE *database_name* SET HADR RESUME  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To resume a secondary database**  
  
1.  Change directory (**cd**) to the server instance that hosts the replica whose database you want to resume. For more information, see [Prerequisites](#Prerequisites), earlier in this topic.  
  
2.  Use the **Resume-SqlAvailabilityDatabase** cmdlet to resume the availability group.  
  
     For example, the following command resumes data synchronization for the availability database `MyDb3` in the availability group `MyAg`.  
  
    ```  
    Resume-SqlAvailabilityDatabase `   
    -Path SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\MyAg\Databases\MyDb3  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Suspend an Availability Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/suspend-an-availability-database-sql-server.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
