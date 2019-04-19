---
title: "Configure read-only access to a secondary replica of an availability group"
description: "Configure your secondary replica to allow only read access for your Always On availability group. "
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "connection access to availability replicas"
  - "Availability Groups [SQL Server], readable secondary replicas"
  - "active secondary replicas [SQL Server], read-only access to"
  - "Availability Groups [SQL Server], read-only routing"
  - "Availability Groups [SQL Server], client connectivity"
ms.assetid: 22387419-22c4-43fa-851c-5fecec4b049b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure read-only access to a secondary replica of an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  By default both read-write and read-intent access are allowed to the primary replica and no connections are allowed to secondary replicas of an Always On availability group. This topic describes how to configure connection access on an availability replica of an Always On availability group in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell.  
  
 For information about the implications of enabling read-only access for a secondary replica and for an introduction to connection access, see [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md) and [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).  
  
 
##  <a name="Prerequisites"></a> Prerequisites and Restrictions  
  
-   To configure different connection access, you must be connected to the server instance that hosts the primary replica.  
  
##  <a name="Permissions"></a> Permissions  
  
|Task|Permissions|  
|----------|-----------------|  
|To configure replicas when creating an availability group|Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.|  
|To modify an availability replica|Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.|  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To configure access on an availability replica**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Click the availability group whose replica you want to change.  
  
4.  Right-click the availability replica, and click **Properties**.  
  
5.  In the **Availability Replica Properties** dialog box, you can change the connection access for the primary role and for the secondary role, as follows:  
  
    -   For the secondary role, select a new value from the **Readable secondary** drop list, as follows:  
  
         **No**  
         No user connections are allowed to secondary databases of this replica. They are not available for read access. This is the default setting.  
  
         **Read-intent only**  
         Only read-only connections are allowed to secondary databases of this replica. The secondary database(s) are all available for read access.  
  
         **Yes**  
         All connections are allowed to secondary databases of this replica, but only for read access. The secondary database(s) are all available for read access.  
  
    -   For the primary role, select a new value from the **Connections in primary role** drop list, as follows:  
  
         **Allow all connections**  
         All connections are allowed to the databases in the primary replica. This is the default setting.  
  
         **Allow read/write connections**  
         When the Application Intent property is set to **ReadWrite** or the Application Intent connection property is not set, the connection is allowed. Connections where the Application Intent connection property is set to **ReadOnly** are not allowed. This can help prevent customers from connecting a read-intent work load to the primary replica by mistake. For more information about Application Intent connection property, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To configure access on an availability replica**  
  
> [!NOTE]  
>  For an example of this procedure, see [Example (Transact-SQL)](#TsqlExample), later in this section.  
  
1.  Connect to the server instance that hosts the primary replica.  
  
2.  If you are specifying a replica for a new availability group, use the [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement. If you are adding or modifying a replica of an existing availability group, use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement.  
  
    -   To configure connection access for the secondary role, in the ADD REPLICA or MODIFY REPLICA WITH clause, specify the SECONDARY_ROLE option, as follows:  
  
         SECONDARY_ROLE **(** ALLOW_CONNECTIONS **=** { NO | READ_ONLY | ALL } **)**  
  
         where,  
  
         NO  
         No direct connections are allowed to secondary databases of this replica. They are not available for read access. This is the default setting.  
  
         READ_ONLY  
         Only read-only connections are allowed to secondary databases of this replica. The secondary database(s) are all available for read access.  
  
         ALL  
         All connections are allowed to secondary databases of this replica, but only for read access. The secondary database(s) are all available for read access.  
  
3.  To configure connection access for the primary role, in the ADD REPLICA or MODIFY REPLICA WITH clause, specify the PRIMARY_ROLE option, as follows:  
  
     PRIMARY_ROLE **(** ALLOW_CONNECTIONS **=** { READ_WRITE | ALL } **)**  
  
     where,  
  
     READ_WRITE  
     Connections where the Application Intent connection property is set to **ReadOnly** are disallowed.  When the Application Intent property is set to **ReadWrite** or the Application Intent connection property is not set, the connection is allowed. For more information about Application Intent connection property, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
     ALL  
     All connections are allowed to the databases in the primary replica. This is the default setting.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example adds a secondary replica to an availability group named *AG2*. A stand-alone server instance, *COMPUTER03\HADR_INSTANCE*, is specified to host the new availability replica. This replica configured to allow only read-write connections for the primary role and to allow only read-intent connections for secondary role.  
  
```  
ALTER AVAILABILITY GROUP AG2   
   ADD REPLICA ON   
      'COMPUTER03\HADR_INSTANCE' WITH   
         (  
         ENDPOINT_URL = 'TCP://COMPUTER03:7022',  
         PRIMARY_ROLE ( ALLOW_CONNECTIONS = READ_WRITE ),  
         SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY )  
         );   
GO  
```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To configure access on an availability replica**  
  
> [!NOTE]  
>  For a code example, see [Example (PowerShell)](#PSExample), later in this section.  
  
1.  Change directory (**cd**) to the server instance that hosts the primary replica.  
  
2.  When adding an availability replica to an availability group, use the **New-SqlAvailabilityReplica** cmdlet. When modifying an existing availability replica, use the **Set-SqlAvailabilityReplica** cmdlet. The relevant parameters are as follows:  
  
    -   To configure connection access for the secondary role, specify the **ConnectionModeInSecondaryRole**_secondary_role_keyword_ parameter, where *secondary_role_keyword* equals one of the following values:  
  
         **AllowNoConnections**  
         No direct connections are allowed to the databases in the secondary replica and the databases are not available for read access. This is the default setting.  
  
         **AllowReadIntentConnectionsOnly**  
         Connections are allowed only to the databases in the secondary replica where the Application Intent property is set to **ReadOnly**. For more information about this property, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
         **AllowAllConnections**  
         All connections are allowed to the databases in the secondary replica for read-only access.  
  
    -   To configure connection access for the primary role, specify **ConnectionModeInPrimaryRole**_primary_role_keyword_, where *primary_role_keyword* equals one of the following values:  
  
         **AllowReadWriteConnections**  
         Connections where the Application Intent connection property is set to ReadOnly are disallowed. When the Application Intent property is set to ReadWrite or the Application Intent connection property is not set, the connection is allowed. For more information about Application Intent connection property, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
         **AllowAllConnections**  
         All connections are allowed to the databases in the primary replica. This is the default setting.  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
###  <a name="PSExample"></a> Example (PowerShell)  
 The following example, sets the both the **ConnectionModeInSecondaryRole** and **ConnectionModeInPrimaryRole** parameters to **AllowAllConnections**.  
  
```  
Set-Location SQLSERVER:\SQL\PrimaryServer\default\AvailabilityGroups\MyAg  
$primaryReplica = Get-Item "AvailabilityReplicas\PrimaryServer"  
Set-SqlAvailabilityReplica -ConnectionModeInSecondaryRole "AllowAllConnections" `   
-InputObject $primaryReplica  
Set-SqlAvailabilityReplica -ConnectionModeInPrimaryRole "AllowAllConnections" `   
-InputObject $primaryReplica  
  
```  
  
##  <a name="FollowUp"></a> Follow Up: After Configuring Read-Only Access for an Availability Replica  
 **Read-only access to a readable secondary replica**  
  
-   When using the [bcp Utility](../../../tools/bcp-utility.md) or [sqlcmd Utility](../../../tools/sqlcmd-utility.md), you can specify read-only access to any secondary replica that is enabled for read-only access by specifying the **-K ReadOnly** switch.  
  
-   To enable client applications to connect to readable secondary replicas:  
  
    ||Prerequisite|Link|  
    |-|------------------|----------|  
    |![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Ensure that the availability group has a listener.|[Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)|  
    |![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Configure read-only routing for the availability group.|[Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)|  
  
 **Factors that might affect triggers and jobs after a failover**  
  
 If you have triggers and jobs that will fail when running on a non-readable secondary database or on a readable secondary database, you need to script the triggers and jobs to check on a given replica to determine whether the database is a primary database or is a readable secondary database. To obtain this information, use the [DATABASEPROPERTYEX](../../../t-sql/functions/databasepropertyex-transact-sql.md) function to return the **Updateability** property of the database. To identify a read-only database, specify READ_ONLY as the value, as follows:  
  
```  
DATABASEPROPERTYEX([db name],'UpdateAbility') = N'READ_ONLY'  
```  
  
 To identify a read-write database, specify READ_WRITE as the value.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Always On: Value Proposition of Readable Secondary](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-value-proposition-of-readable-secondary.aspx)  
  
-   [Always On: Why there are two options to enable a secondary replica for read workload?](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-why-there-are-two-options-to-enable-a-secondary-replica-for-read-workload.aspx)  
  
-   [Always On: Setting up Readable Seconary Replica](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-setting-up-readable-seconary-replica.aspx)  
  
-   [Always On: I just enabled Readable Secondary but my query is blocked?](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-i-just-enabled-readble-secondary-but-my-query-is-blocked.aspx)  
  
-   [Always On: Making latest statistics available on Readable Secondary, Read-Only database and Database Snapshot](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-making-upto-date-statistics-available-on-readable-secondary-read-only-database-and-database-snapshot.aspx)  
  
-   [Always On: Challenges with statistics on ReadOnly database, Database Snapshot and Secondary Replica](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-challenges-with-statistics-on-readonly-database-database-snapshot-and-secondary-replica.aspx)  
  
-   [Always On: Impact on the primary workload when you run reporting workload on the secondary replica](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-impact-on-the-primary-workload-when-you-run-reporting-workload-on-the-secondary-replica.aspx)  
  
-   [Always On: Impact of mapping reporting workload on Readable Secondary to Snapshot Isolation](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-impact-of-mapping-reporting-workload-to-snapshot-isolation-on-readable-secondary.aspx)  
  
-   [Always On: Minimizing blocking of REDO thread when running reporting workload on Secondary Replica](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On-minimizing-blocking-of-redo-thread-when-running-reporting-workload-on-secondary-replica.aspx)  
  
-   [Always On: Readable Secondary and data latency](https://blogs.msdn.com/b/sqlserverstorageengine/archive/2011/12/22/Always%20On.aspx)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)   
 [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md)  
  
  
