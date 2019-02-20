---
title: "Use AlwaysOn Policies to View the Health of an Availability Group (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: 01/19/2019
ms.prod: "sql-server-2014"
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
# Use AlwaysOn Policies to View the Health of an Availability Group (SQL Server)
  This topic describes how to determine the operational health of an AlwaysOn availability group by using an AlwaysOn policy in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. For information about AlwaysOn Policy Based Management, see [AlwaysOn Policies for Operational Issues with AlwaysOn Availability Groups (SQL Server)](always-on-policies-for-operational-issues-always-on-availability.md).  
  
> [!IMPORTANT]  
>  For AlwaysOn policies, the category names are used as IDs. Changing the name of an AlwaysOn category would break its health-evaluation functionality. Therefore, the names of AlwaysOn category should never be modified.  
  

  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions.  
  
##  <a name="SSMSProcedure"></a> Using the AlwaysOn Dashboard  
 **To open the AlwaysOn Dashboard**  
  
1.  In Object Explorer, connect to the server instance that hosts one of the availability replicas. To view information about all of the availability replicas in an availability group, use to the server instance that hosts the primary replica.  
  
2.  Click the server name to expand the server tree.  
  
3.  Expand the **AlwaysOn High Availability** node.  
  
     Either right-click the **Availability Groups** node or expand this node and right-click a specific availability group.  
  
4.  Select the **Show Dashboard** command.  
  
 For information about how to use the AlwaysOn Dashboard, see [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md).  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **Use AlwaysOn policies to view the health of an availability group**  
  
1.  Set default (`cd`) to a server instance that hosts one of the availability replicas. To view information about all of the availability replicas in an availability group, use to the server instance that hosts the primary replica.  
  
2.  Use the following cmdlets:  
  
     `Test-SqlAvailabilityGroup`  
     Assesses the health of an availability group by evaluating SQL Server policy based management (PBM) policies. You must have CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions to execute this cmdlet.  
  
     For example, the following command shows all availability groups with a health state of "Error" on the server instance `Computer\Instance`.  
  
    ```powershell
    Get-ChildItem SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups |
        Test-SqlAvailabilityGroup |
        Where-Object { $_.HealthState -eq "Error" }  
    ```  
  
     `Test-SqlAvailabilityReplica`  
     Assesses the health of availability replicas by evaluating SQL Server policy based management (PBM) policies. You must have CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions to execute this cmdlet.  
  
     For example, the following command evaluates the health of the availability replica named `MyReplica` in the availability group `MyAg` and outputs a brief summary.  
  
    ```powershell
    Test-SqlAvailabilityReplica `   
    -Path SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\MyAg\AvailabilityReplicas\MyReplica  
    ```  
  
     `Test-SqlDatabaseReplicaState`  
     Assesses the health of an availability database on all joined availability replicas by evaluating SQL Server policy based management (PBM) policies.  
  
     For example, the following command evaluates the health of all availability databases in the availability group `MyAg` and outputs a brief summary for each database.  
  
    ```powershell
    Get-ChildItem SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\MyAg\DatabaseReplicaStates |
        Test-SqlDatabaseReplicaState  
    ```  
  
     These cmdlets accept the following options:  
  
    |Option|Description|  
    |------------|-----------------|  
    |`AllowUserPolicies`|Runs user policies found in the AlwaysOn policy categories.|  
    |`InputObject`|A collection of objects that, represent availability groups, availability replicas, or availability database states (depending on which cmdlet you are using). The cmdlet will compute the health of the specified objects.|  
    |`NoRefresh`|When this parameter is set, the cmdlet will not manually refresh the objects specified by the `-Path` or `-InputObject` parameter.|  
    |`Path`|The path to the availability group, one or more availability replicas, or database replica cluster state of the availability database (depending on which cmdlet you are using). This is an optional parameter. If not specified, the value of this parameter defaults to the current working location.|  
    |`ShowPolicyDetails`|Shows the result of each policy evaluation performed by this cmdlet. The cmdlet outputs one object per policy evaluation, and this object has fields describing the results of evaluation (whether the policy passed or not, the policy name and category, and so forth).|  
  
     For example, the following `Test-SqlAvailabilityGroup` command specifies the `-ShowPolicyDetails` parameter to show the result of each policy evaluation performed by this cmdlet for each policy-based management (PBM) policy that was executed on the availability group named `MyAg`.  
  
    ```powershell
    Test-SqlAvailabilityGroup `   
    -Path SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\AgName `  
    -ShowPolicyDetails  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
-   [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md)  
  
##  <a name="RelatedContent"></a> Related Content  
 **SQL Server AlwaysOn Team Blogs-Monitoring AlwaysOn Health with PowerShell:**  
  
-   [Part 1: Basic Cmdlet Overview](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/13/monitoring-alwayson-health-with-powershell-part-1.aspx)  
  
-   [Part 2: Advanced Cmdlet Usage](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/13/monitoring-alwayson-health-with-powershell-part-2.aspx)  
  
-   [Part 3: A Simple Monitoring Application](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/15/monitoring-alwayson-health-with-powershell-part-3.aspx)  
  
-   [Part 4: Integration with SQL Server Agent](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/15/the-always-on-health-model-part-4.aspx)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Administration of an Availability Group &#40;SQL Server&#41;](administration-of-an-availability-group-sql-server.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](monitoring-of-availability-groups-sql-server.md)   
 [AlwaysOn Policies for Operational Issues with AlwaysOn Availability Groups (SQL Server)](always-on-policies-for-operational-issues-always-on-availability.md) 
  
  
