---
title: "Use Always On Policies to View the Health of an Availability Group | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: 6f1bcbc3-1220-4071-8e53-4b957f5d3089
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Use Always On Policies to View the Health of an Availability Group (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic describes how to determine the operational health of an Always On availability group by using an Always On policy in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. For information about Always On Policy Based Management, see [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md).  
  
> [!IMPORTANT]  
>  For Always On policies, the category names are used as IDs. Changing the name of an Always On category would break its health-evaluation functionality. Therefore, the names of Always On category should never be modified.  
  
  
  
##  <a name="Permissions"></a> Permissions  
 Requires CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions.  
  
##  <a name="SSMSProcedure"></a> Using the Always On Dashboard  
 **To open the Always On Dashboard**  
  
1.  In Object Explorer, connect to the server instance that hosts one of the availability replicas. To view information about all of the availability replicas in an availability group, use to the server instance that hosts the primary replica.  
  
2.  Click the server name to expand the server tree.  
  
3.  Expand the **Always On High Availability** node.  
  
     Either right-click the **Availability Groups** node or expand this node and right-click a specific availability group.  
  
4.  Select the **Show Dashboard** command.  
  
 For information about how to use the Always On Dashboard, see [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](~/database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md).  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **Use Always On policies to view the health of an availability group**  
  
1.  Set default (**cd**) to a server instance that hosts one of the availability replicas. To view information about all of the availability replicas in an availability group, use to the server instance that hosts the primary replica.  
  
2.  Use the following cmdlets:  
  
     **Test-SqlAvailabilityGroup**  
     Assesses the health of an availability group by evaluating SQL Server policy based management (PBM) policies. You must have CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions to execute this cmdlet.  
  
     For example, the following command shows all availability groups with a health state of "Error" on the server instance `Computer\Instance`.  
  
    ```  
    Get-ChildItem SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups `   
    | Test-SqlAvailabilityGroup | Where-Object { $_.HealthState -eq "Error" }  
    ```  
  
     **Test-SqlAvailabilityReplica**  
     Assesses the health of availability replicas by evaluating SQL Server policy based management (PBM) policies. You must have CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions to execute this cmdlet.  
  
     For example, the following command evaluates the health of the availability replica named `MyReplica` in the availability group `MyAg` and outputs a brief summary.  
  
    ```  
    Test-SqlAvailabilityReplica `   
    -Path SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\MyAg\AvailabilityReplicas\MyReplica  
    ```  
  
     **Test-SqlDatabaseReplicaState**  
     Assesses the health of an availability database on all joined availability replicas by evaluating SQL Server policy based management (PBM) policies.  
  
     For example, the following command evaluates the health of all availability databases in the availability group `MyAg` and outputs a brief summary for each database.  
  
    ```  
    Get-ChildItem SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\MyAg\DatabaseReplicaStates `   
     | Test-SqlDatabaseReplicaState  
    ```  
  
     These cmdlets accept the following options:  
  
    |Option|Description|  
    |------------|-----------------|  
    |**AllowUserPolicies**|Runs user policies found in the Always On policy categories.|  
    |**InputObject**|A collection of objects that, represent availability groups, availability replicas, or availability database states (depending on which cmdlet you are using). The cmdlet will compute the health of the specified objects.|  
    |**NoRefresh**|When this parameter is set, the cmdlet will not manually refresh the objects specified by the **-Path** or **-InputObject** parameter.|  
    |**Path**|The path to the availability group, one or more availability replicas, or database replica cluster state of the availability database (depending on which cmdlet you are using). This is an optional parameter. If not specified, the value of this parameter defaults to the current working location.|  
    |**ShowPolicyDetails**|Shows the result of each policy evaluation performed by this cmdlet. The cmdlet outputs one object per policy evaluation, and this object has fields describing the results of evaluation (whether the policy passed or not, the policy name and category, and so forth).|  
  
     For example, the following **Test-SqlAvailabilityGroup** command specifies the **-ShowPolicyDetails** parameter to show the result of each policy evaluation performed by this cmdlet for each policy-based management (PBM) policy that was executed on the availability group named `MyAg`.  
  
    ```  
    Test-SqlAvailabilityGroup `   
    -Path SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\AgName `  
    -ShowPolicyDetails  
  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
-   [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md)  
  
##  <a name="RelatedContent"></a> Related Content  
 **SQL Server Always On Team Blogs-Monitoring Always On Health with PowerShell:**  
  
-   [Part 1: Basic Cmdlet Overview](https://blogs.msdn.microsoft.com/sqlalwayson/2012/02/13/monitoring-alwayson-health-with-powershell-part-1-basic-cmdlet-overview/)  
  
-   [Part 2: Advanced Cmdlet Usage](https://blogs.msdn.microsoft.com/sqlalwayson/2012/02/13/monitoring-alwayson-health-with-powershell-part-2-advanced-cmdlet-usage/)  
  
-   [Part 3: A Simple Monitoring Application](https://blogs.msdn.microsoft.com/sqlalwayson/2012/02/14/monitoring-alwayson-health-with-powershell-part-3-a-simple-monitoring-application/)  
  
-   [Part 4: Integration with SQL Server Agent](https://blogs.msdn.microsoft.com/sqlalwayson/2012/02/15/monitoring-alwayson-health-with-powershell-part-4-integration-with-sql-server-agent/)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Administration of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/administration-of-an-availability-group-sql-server.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/monitoring-of-availability-groups-sql-server.md)   
 [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md)  
  
  


