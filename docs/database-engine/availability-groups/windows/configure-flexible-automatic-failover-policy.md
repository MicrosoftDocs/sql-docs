---
title: "Configure a flexible automatic failover policy for an availability group"
description: "Describes how to configure a flexible failover policy for an Always On availability group using Transact-SQL (T-SQL), PowerShell, or SQL Server Management Studio."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], flexible failover policy"
  - "Availability Groups [SQL Server], failover"
  - "failover [SQL Server], AlwaysOn Availability Groups"
ms.assetid: 1ed564b4-9835-4245-ae35-9ba67419a4ce
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# Configure a flexible automatic failover policy for an Always On availability group

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to configure the flexible failover policy for an Always On availability group by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. A flexible failover policy provides granular control over the conditions that cause automatic failover for an availability group. By changing the failure conditions that trigger an automatic failover and the frequency of health checks, you can increase or decrease the likelihood of an automatic failover to support your SLA for high availability.  
 
    > [!NOTE]  
    >  The flexible failover policy of an availability group cannot be configured by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
  
 
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
  
         For more information about the failover condition levels, see [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/flexible-automatic-failover-policy-availability-group.md).  
  
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
  
         For more information about the failover condition levels, see [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/flexible-automatic-failover-policy-availability-group.md).  
  
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
>  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
-   [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md)   
 [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md)   
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)   
 [Failover Policy for Failover Cluster Instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)   
 [sp_server_diagnostics &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md)  
  
  
