---
title: "Configure FailureConditionLevel Property Settings | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 513dd179-9a46-46da-9fdd-7632cf6d0816
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure FailureConditionLevel Property Settings
  Use the FailureConditionLevel property to set the conditions for the AlwaysOn Failover Cluster Instance (FCI) to fail over or restart. Changes to this property are applied immediately without requiring a restart of the Windows Server Failover Cluster (WSFC) service or the FCI resource.  
  
-   **Before you begin:**  [FailureConditionLevel Property Settings](#Restrictions), [Security](#Security)  
  
-   **To configure the FailureConditionLevel property settings using,** [PowerShell](#PowerShellProcedure), [Failover Cluster Manager](#WSFC), [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> FailureConditionLevel Property Settings  
 The failure conditions are set on an increasing scale. For levels 1-5, each level includes all the conditions from the previous levels in addition to its own conditions. This means that with each level, there is an increased probability of a failover or restart.  For more information, see the "Determining Failures" section of the [Failover Policy for Failover Cluster Instances](failover-policy-for-failover-cluster-instances.md) topic.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER SETTINGS and VIEW SERVER STATE permissions.  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
  
##### To configure FailureConditionLevel settings  
  
1.  Start an elevated Windows PowerShell via **Run as Administrator**.  
  
2.  Import the `FailoverClusters` module to enable cluster cmdlets.  
  
3.  Use the `Get-ClusterResource` cmdlet to find the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource, then use `Set-ClusterParameter` cmdlet to set the **FailureConditionLevel** property for a Failover Cluster Instance.  
  
> [!TIP]  
>  Every time you open a new PowerShell window, you need to import the `FailoverClusters` module.  
  
### Example (PowerShell)  
 The following example changes the FailureConditionLevel setting on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource "`SQL Server (INST1)`" to fail over or restart on critical server errors.  
  
```powershell  
Import-Module FailoverClusters  
  
$fci = "SQL Server (INST1)"  
Get-ClusterResource $fci | Set-ClusterParameter FailureConditionLevel 3  
  
```  
  
### Related Content (PowerShell)  
  
-   [Clustering and High-Availability](https://blogs.msdn.com/b/clustering/archive/2009/05/23/9636665.aspx) (Failover Clustering and Network Load Balancing Team Blog)  
  
-   [Getting Started with Windows PowerShell on a Failover Cluster](https://technet.microsoft.com/library/ee619762\(WS.10\).aspx)  
  
-   [Cluster resource commands and equivalent Windows PowerShell cmdlets](https://msdn.microsoft.com/library/ee619744.aspx#BKMK_resource)  
  
##  <a name="WSFC"></a> Using the Failover Cluster Manager Snap-in  
 **To configure FailureConditionLevel property settings:**  
  
1.  Open the Failover Cluster Manager snap-in.  
  
2.  Expand the **Services and Applications** and select the FCI.  
  
3.  Right-click the **SQL Server resource** under **Other Resources**, and then select **Properties** from the menu. The SQL Server resource **Properties** dialog box opens.  
  
4.  Select the **Properties** tab, enter the desired value for the **FaliureConditionLevel** property, and then click **OK** to apply the change.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To configure FailureConditionLevel property settings:**  
  
 Using the [ALTER SERVER CONFIGURATION](/sql/t-sql/statements/alter-server-configuration-transact-sql)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement, you can specify the FailureConditionLevel property value.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example sets the FailureConditionLevel property to 0, indicating that failover or restart will not be triggered automatically on any failure conditions.  
  
```  
ALTER SERVER CONFIGURATION SET FAILOVER CLUSTER PROPERTY FailureConditionLevel = 0;  
```  
  
## See Also  
 [sp_server_diagnostics &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql)   
 [Failover Policy for Failover Cluster Instances](failover-policy-for-failover-cluster-instances.md)  
  
  
