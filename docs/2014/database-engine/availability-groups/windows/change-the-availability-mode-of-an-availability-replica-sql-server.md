---
title: "Change the Availability Mode of an Availability Replica (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], deploying"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], availability modes"
ms.assetid: c4da8f25-fb1b-45a4-8bf2-195df6df634c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Change the Availability Mode of an Availability Replica (SQL Server)
  This topic describes how to change the availability mode of an availability replica in an AlwaysOn availability group in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell. The availability mode is a replica property that controls the whether the replica commits asynchronously or synchronously. *Asynchronous-commit mode* maximizes performance at the expense of high availability and supports only forced manual failover (with possible data loss), typically called *forced failover*. *Synchronous-commit mode* emphasizes high availability over performance and, once the secondary replica is synchronized, supports manual failover and, optionally, automatic failover.  
  

  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be connected to the server instance that hosts the primary replica.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To change the availability mode of an availability group**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
2.  Expand the **AlwaysOn High Availability** node and the **Availability Groups** node.  
  
3.  Click the availability group whose replica you want to change.  
  
4.  Right-click the replica, and click **Properties**.  
  
5.  In the **Availability Replica Properties** dialog box, use the **Availability mode** drop list to change the availability mode of this replica.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To change the availability mode of an availability group**  
  
1.  Connect to the server instance that hosts the primary replica.  
  
2.  Use the [ALTER AVAILABILITY GROUP](/sql/t-sql/statements/alter-availability-group-transact-sql) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name* MODIFY REPLICA ON '*server_name*'  
  
     WITH ( {  
  
     AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT }  
  
     | FAILOVER_MODE = { AUTOMATIC | MANUAL }  
  
     } )  
  
     where *group_name* is the name of the availability group and *server_name* is the name of the server instance that hosts the replica to be modified.  
  
    > [!NOTE]  
    >  FAILOVER_MODE = AUTOMATIC is supported only if you also specify AVAILABILITY_MODE = SYNCHRONOUS_COMMIT.  
  
     The following example, entered on the primary replica of the `AccountsAG` availability group, changes the availability and failover modes to synchronous commit and automatic failover, respectively, for the replica hosted by the `INSTANCE09` server instance.  
  
    ```  
  
    ALTER AVAILABILITY GROUP AccountsAG MODIFY REPLICA ON 'INSTANCE09'  
       WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);  
    ALTER AVAILABILITY GROUP AccountsAG MODIFY REPLICA ON 'INSTANCE09'  
       WITH (FAILOVER_MODE = AUTOMATIC);  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To change the availability mode of an availability group**  
  
1.  Change directory (`cd`) to the server instance that hosts the primary replica.  
  
2.  Use the `Set-SqlAvailabilityReplica` cmdlet with the `AvailabilityMode` parameter and, optionally, the `FailoverMode` parameter.  
  
     For example, the following command modifies the replica `MyReplica` in the availability group `MyAg` to use synchronous-commit availability mode and to support automatic failover.  
  
    ```  
    Set-SqlAvailabilityReplica -AvailabilityMode "SynchronousCommit" -FailoverMode "Automatic" `   
    -Path SQLSERVER:\Sql\PrimaryServer\InstanceName\AvailabilityGroups\MyAg\AvailabilityReplicas\MyReplica  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Modes (AlwaysOn Availability Groups)](availability-modes-always-on-availability-groups.md)   
 [Failover and Failover Modes &#40;AlwaysOn Availability Groups&#41;](failover-and-failover-modes-always-on-availability-groups.md)  
  
  
