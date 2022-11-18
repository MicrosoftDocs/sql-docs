---
title: "Configure a flexible automatic failover policy for an availability group"
description: "Describes how to configure a flexible failover policy for an Always On availability group using Transact-SQL (T-SQL), PowerShell, or SQL Server Management Studio."
author: MashaMSFT
ms.author: mathoma
ms.date: "11/05/2019"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Availability Groups [SQL Server], flexible failover policy"
  - "Availability Groups [SQL Server], failover"
  - "failover [SQL Server], AlwaysOn Availability Groups"
  - "failover [SQL Server], Always On Availability Groups"
monikerRange: ">=sql-server-2016"
---
# Configure a flexible automatic failover policy for an Always On availability group

[!INCLUDE[sql windows only](../../../includes/applies-to-version/sql-windows-only.md)]

  This topic describes how to configure the flexible failover policy for an Always On availability group by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] or PowerShell in SQL Server. A flexible failover policy provides granular control over the conditions that cause [automatic failover](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md) for an availability group. By changing the failure conditions that trigger an automatic failover and the frequency of health checks, you can increase or decrease the likelihood of an automatic failover to support your SLA for high availability.  

   The flexible failover policy of an availability group is defined by its failure-condition level and health-check timeout threshold. On detecting that an availability group has exceeded its failure condition level or its health-check timeout threshold, the availability group's resource DLL responds back to the Windows Server Failover Clustering (WSFC) cluster. The WSFC cluster then initiates an automatic failover to the secondary replica.  
 
  > [!NOTE]  
  > The flexible failover policy of an availability group cannot be configured by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
  
 
## <a name="Limitations"></a> Limitations on Automatic Failovers  
  
-   For an automatic failover to occur, the current primary replica and one secondary replica must be configured for synchronous-commit availability mode with automatic failover and the secondary replica must be synchronized with the primary replica.  
  
-   Only three replicas are supported for automatic failover.  
  
-   If an availability group exceeds its WSFC failure threshold, the WSFC cluster will not attempt an automatic failover for the availability group. Furthermore, the WSFC resource group of the availability group remains in a failed state until either the cluster administrator manually brings the failed resource group online or the database administrator performs a manual failover of the availability group. The *WSFC failure threshold* is defined as the maximum number of failures supported for the availability group during a given time period. The default time period is six hours, and the default value for the maximum number of failures during this period is *n*-1, where *n* is the number of WSFC nodes. To change the failure-threshold values for a given availability group, use the WSFC Failover Manager Console.  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be connected to the server instance that hosts the primary replica.  
   
##  <a name="Permissions"></a> Permissions  
  
|Task|Permissions|  
|----------|-----------------|  
|To configure the flexible failover policy for a new availability group|Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.|  
|To modify the policy of an existing availability group|Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.|  

##  <a name="HCtimeout"></a> Health-Check Timeout Threshold  
 WSFC resource DLL of the availability group performs a *health check* of the primary replica by calling the [sp_server_diagnostics](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) stored procedure on the instance of SQL Server that hosts the primary replica. **sp_server_diagnostics** returns results at an interval that equals 1/3 of the health-check timeout threshold for the availability group. The default health-check timeout threshold is 30 seconds, which causes **sp_server_diagnostics** to return at a 10-second interval. If **sp_server_diagnostics** is slow or is not returning information, the resource DLL will wait for the full interval of the health-check timeout threshold before determining that the primary replica is unresponsive. If the primary replica is unresponsive, an automatic failover is initiated, if currently supported.  
  
> [!IMPORTANT]  
>  **sp_server_diagnostics** does not perform health checks at the database level.  
  
##  <a name="FClevel"></a> Failure-Condition Level  
 Whether the diagnostic data and health information returned by **sp_server_diagnostics** warrants an automatic failover depends on the failure-condition level of the availability group. The *failure-condition level* specifies what failure conditions trigger an automatic failover. There are five failure-condition levels, which range from the least restrictive (level one) to the most restrictive (level five). A given level encompasses the less restrictive levels. Thus, the strictest level, five, includes the four less restrictive conditions, and so forth.  
  
> [!IMPORTANT]  
>  Damaged databases and suspect databases are not detected by any failure-condition level. Therefore, a database that is damaged or suspect (whether due to a hardware failure, data corruption, or other issue) never triggers an automatic failover.  
  
 The following table describes the failure-conditions that corresponds to each level.  
  
|Level|Failure Condition|[!INCLUDE[tsql](../../../includes/tsql-md.md)] Value|PowerShell Value|  
|-----------|-----------------------|------------------------------|----------------------|  
|One|On server down. Specifies that an automatic failover is initiated when one the following occurs:<br /><br /> The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is down.<br /><br /> The lease of the availability group for connecting to the WSFC cluster expires because no ACK is received from the server instance. For more information, see [How It Works: SQL Server Always On Lease Timeout](https://techcommunity.microsoft.com/t5/sql-server-support/how-it-works-sql-server-alwayson-lease-timeout/ba-p/317268).<br /><br /> <br /><br /> This is the least restrictive level.|1|**OnServerDown**|  
|Two|On server unresponsive. Specifies that an automatic failover is initiated when one of the following occurs:<br /><br /> The instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not connect to cluster, and the user-specified health check timeout threshold of the availability group is exceeded.<br /><br /> The availability replica is in failed state.|2|**OnServerUnresponsive**|  
|Three|On critical server error. Specifies that an automatic failover is initiated on critical [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] internal errors, such as orphaned spinlocks, serious write-access violations, or too much dumping.<br /><br /> This is the default level.|3|**OnCriticalServerError**|  
|Four|On moderate server error. Specifies that an automatic failover is initiated on moderate [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] internal errors, such as a persistent out-of-memory condition in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] internal resource pool.|4|**OnModerateServerError**|  
|Five|On any qualified failure conditions. Specifies that an automatic failover is initiated on any qualified failure conditions, including:<br /><br /> Detection of Scheduler deadlock.<br /><br /> Detection of an unsolvable deadlock.<br /><br /> <br /><br /> This is the most restrictive level.|5|**OnAnyQualifiedFailureConditions**|  
  
> [!NOTE]  
>  Lack of response by an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to client requests is irrelevant to availability groups.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To configure the flexible failover policy**  
  
1.  Connect to the server instance that hosts the primary replica.  
  
2.  For a new availability group, use the [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement. If you are modifying an existing availability group, use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement.  
  
    -   To set the failover condition level, use the FAILURE_CONDITION_LEVEL = *n* option, where, *n* is an integer from 1 to 5.  
  
         For example, the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement changes the failure-condition level of an existing availability group, `AG1`, to level one:  
  
        ```  
  
        ALTER AVAILABILITY GROUP AG1 SET (FAILURE_CONDITION_LEVEL = 1);  
        ```  
  
         The relationship of these integer values to the failure condition levels is as follows:  
  
        |[!INCLUDE[tsql](../../../includes/tsql-md.md)] Value|Level|Automatic Is Failover Initiated When...|  
        |------------------------------|-----------|-------------------------------------------|  
        |1|One|On server down. The SQL Server service stops because of a failover or restart.|  
        |2|Two|On server unresponsive. Any condition of lower value is satisfied, the SQL Server service is connected to the cluster and the health check timeout threshold is exceeded, or the current primary replica is in a failed state.|  
        |3|Three|On critical server error. Any condition of lower value is satisfied or an internal critical server error occurs.<br /><br /> This is the default level.|  
        |4|Four|On moderate server error. Any condition of lower value is satisfied or a moderate Server error occurs.|  
        |5|Five|On any qualified failure conditions. Any condition of lower value is satisfied or a qualifying failure condition occurs.|  
    
         For more information about the failover condition levels, see [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md#FClevel).  
  
  
    -   To configure the health check timeout threshold, use the HEALTH_CHECK_TIMEOUT = *n* option, where, *n* is an integer from 15000 milliseconds (15 seconds) to 4294967295 milliseconds. The default value is 30000 milliseconds (30 seconds)  
  
         For example, the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement changes the health-check timeout threshold of an existing availability group, `AG1`, to 60,000 milliseconds (one minute).  
  
        ```  
  
        ALTER AVAILABILITY GROUP AG1 SET (HEALTH_CHECK_TIMEOUT = 60000);  
        ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To configure the flexible failover policy**  
  
1.  Set default (**cd**) to the server instance that hosts the primary replica.  
  
2.  When adding an availability replica to an availability group, use the **New-SqlAvailabilityGroup** cmdlet. When modifying an existing availability replica, use the **Set-SqlAvailabilityGroup** cmdlet.  
  
    -   To set the failover condition level, use the **FailureConditionLevel**_level_ parameter, where, *level* is one of the following values:  
  
        |Value|Level|Automatic Is Failover Initiated When...|  
        |-----------|-----------|-------------------------------------------|  
        |**OnServerDown**|One|On server down. The SQL Server service stops because of a failover or restart.|  
        |**OnServerUnresponsive**|Two|On server unresponsive. Any condition of lower value is satisfied, the SQL Server service is connected to the cluster and the health check timeout threshold is exceeded, or the current primary replica is in a failed state.|  
        |**OnCriticalServerError**|Three|On critical server error. Any condition of lower value is satisfied or an internal critical server error occurs.<br /><br /> This is the default level.|  
        |**OnModerateServerError**|Four|On moderate server error. Any condition of lower value is satisfied or a moderate Server error occurs.|  
        |**OnAnyQualifiedFailureConditions**|Five|On any qualified failure conditions. Any condition of lower value is satisfied or a qualifying failure condition occurs.|  
         
         For more information about the failover condition levels, see [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md#FClevel).   
  
         For example, the following command changes the failure-condition level of an existing availability group, `AG1`, to level one.  
  
        ```  
        Set-SqlAvailabilityGroup `   
        -Path SQLSERVER:\Sql\PrimaryServer\InstanceName\AvailabilityGroups\MyAg `   
        -FailureConditionLevel OnServerDown  
        ```  
  
    -   To set the health check timeout threshold, use the **HealthCheckTimeout**_n_ parameter, where, *n* is an integer from 15000 milliseconds (15 seconds) to 4294967295 milliseconds. The default value is 30000 milliseconds (30 seconds).  
  
         For example, the following command changes the health-check timeout threshold of an existing availability group, `AG1`, to 120,000 milliseconds (two minutes).  
  
        ```  
        Set-SqlAvailabilityGroup `   
        -Path SQLSERVER:\Sql\PrimaryServer\InstanceName\AvailabilityGroups\MyAG `   
        -HealthCheckTimeout 120000  
        ```  
  
> [!NOTE]  
>  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
-   [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md)  

##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure automatic failover**  
  
-   [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-availability-mode-of-an-availability-replica-sql-server.md) (automatic failover requires synchronous-commit availability mode)  
  
-   [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
-   [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [How It Works: SQL Server Always On Lease Timeout](https://techcommunity.microsoft.com/t5/sql-server-support/how-it-works-sql-server-alwayson-lease-timeout/ba-p/317268)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md)   
 [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md)   
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)   
 [Failover Policy for Failover Cluster Instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)   
 [sp_server_diagnostics &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md)  
  
