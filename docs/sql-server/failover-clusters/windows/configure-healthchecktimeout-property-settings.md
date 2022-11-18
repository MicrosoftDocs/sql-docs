---
title: "Configure HealthCheckTimeout for availability group"
description: Configure HealthCheckTimeout for an Always On availability group, which specifies how long the SQL Server resource DLL waits before reporting unresponsiveness.
ms.custom: "seo-lt-2019"
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: failover-cluster-instance
ms.topic: how-to
ms.assetid: 3bbeb979-e6fc-4184-ad6e-cca62108de74
author: MashaMSFT
ms.author: mathoma
---
# Configure HealthCheckTimeout Property Settings
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The HealthCheckTimeout setting is used to specify the length of time, in milliseconds, that the SQL Server resource DLL should wait for information returned by the [sp_server_diagnostics](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) stored procedure before reporting the Always On Failover Cluster Instance (FCI) as unresponsive. Changes that are made to the timeout settings are effective immediately and do not require a restart of the SQL Server resource.  
  
-   **Before you begin:**  [Limitations and Restrictions](#Limits), [Security](#Security)  
  
-   **To Configure the HeathCheckTimeout setting, using:**  [PowerShell](#PowerShellProcedure), [Failover Cluster Manager](#WSFC), [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Limits"></a> Limitations and Restrictions  
 The default value for this property is 30,000 milliseconds (30 seconds). The minimum value is 15,000 milliseconds (15 seconds).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER SETTINGS and VIEW SERVER STATE permissions.  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
  
##### To configure HealthCheckTimeout settings  
  
1.  Start an elevated Windows PowerShell via **Run as Administrator**.  
  
2.  Import the **FailoverClusters** module to enable cluster cmdlets.  
  
3.  Use the **Get-ClusterResource** cmdlet to find the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource, then use **Set-ClusterParameter** cmdlet to set the **HealthCheckTimeout** property for the failover cluster instance.  
  
> [!TIP]  
>  Every time you open a new PowerShell window, you need to import the **FailoverClusters** module.  
  
### Example (PowerShell)  
 The following example changes the HealthCheckTimeout setting on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource "`SQL Server (INST1)`" to 60000 milliseconds.  
  
```powershell  
Import-Module FailoverClusters  
  
$fci = "SQL Server (INST1)"  
Get-ClusterResource $fci | Set-ClusterParameter HealthCheckTimeout 60000  
  
```  
  
### Related Content (PowerShell)  
  
-   [Clustering and High-Availability](https://techcommunity.microsoft.com/t5/failover-clustering/bg-p/FailoverClustering) (Failover Clustering and Network Load Balancing Team Blog)  
  
-   [Getting Started with Windows PowerShell on a Failover Cluster](https://technet.microsoft.com/library/ee619762\(WS.10\).aspx)  
  
-   [Cluster resource commands and equivalent Windows PowerShell cmdlets](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee619744(v=ws.10)#BKMK_resource)  
  
##  <a name="WSFC"></a> Using the Failover Cluster Manager Snap-in  
 **To configure HealthCheckTimeout setting**  
  
1.  Open the Failover Cluster Manager snap-in.  
  
2.  Expand **Services and Applications** and select the FCI.  
  
3.  Right-click the **SQL Server resource** under **Other Resources** and select **Properties** from the right-click menu. The SQL Server resource **Properties** dialog box opens.  
  
4.  Select the **Properties** tab, enter the desired value for the **HealthCheckTimeout** property, and then click **OK** to apply the change.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Using the [ALTER SERVER CONFIGURATION](../../../t-sql/statements/alter-server-configuration-transact-sql.md)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement, you can specify the HealthCheckTimeOut property value.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example sets the HealthCheckTimeout option to 15,000 milliseconds (15 seconds).  
  
```  
ALTER SERVER CONFIGURATION   
SET FAILOVER CLUSTER PROPERTY HealthCheckTimeout = 15000;  
```  
  
## See Also  
 [Failover Policy for Failover Cluster Instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)  
  
