---
title: "Remove an Instance of SQL Server from the SQL Server Utility | Microsoft Docs"
description: Learn how to remove an instance of SQL Server from the SQL Server Utility. You can run a PowerShell script or use SQL Server Management Studio for this task.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.utility.remove.f1"
ms.assetid: ae1d126a-46d2-47bf-b339-17c743df6491
author: MikeRayMSFT
ms.author: mikeray
---
# Remove an Instance of SQL Server from the SQL Server Utility
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the following steps to remove a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. This procedure removes the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the UCP list view and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility data collection stops. The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not uninstalled.  
  
> [!IMPORTANT]  
>  Before you use this procedure to remove an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, make sure that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and SQL Server Agent services are running on the instance to remove.  
  
1.  From the Utility Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], click on **Managed Instances**. Observe the list view of managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the Utility Explorer content pane.  
  
2.  In the **SQL Server Instance Name** column of the list view, select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to remove from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. Right-click on the instance to remove, and select **Remove Managed Instance...**.  
  
3.  Specify credentials with administrator privileges for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: Click **Connect...**, verify the information in the **Connect to Server** dialog box, then click **Connect**. You will see the login information on the **Remove Managed Instance** dialog.  
  
4.  To confirm the operation, click **OK**. To quit the operation, click **Cancel**.  

## Manually Remove a Managed Instance of SQL Server from a SQL Server Utility  
 This procedure removes the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the UCP list view and stops [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility data collection. The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not uninstalled.  
  
 To use PowerShell to remove a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. This script performs the following operations:  
  
-   Gets the UCP by server instance name.  
  
-   Removes the managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
```  
# Get Ucp connection  
$UcpServerInstanceName = "ComputerName\InstanceName";  
$UtilityInstance = new-object -Type Microsoft.SqlServer.Management.Smo.Server $UcpServerInstanceName;  
$UcpConnection = new-object -Type Microsoft.SqlServer.Management.Sdk.Sfc.SqlStoreConnection $UtilityInstance.ConnectionContext.SqlConnectionObject;  
$Utility = [Microsoft.SqlServer.Management.Utility.Utility]::Connect($UcpConnection);  
  
# Now remove the ManagedInstance from the SQL Server Utility  
$ServerInstanceName = "ComputerName\InstanceName";  
$Instance = new-object -Type Microsoft.SqlServer.Management.Smo.Server $ServerInstanceName;  
$InstanceConnection = new-object -Type Microsoft.SqlServer.Management.Sdk.Sfc.SqlStoreConnection $Instance.ConnectionContext.SqlConnectionObject;  
$ManagedInstance = $Utility.ManagedInstances[$ServerInstanceName];  
$ManagedInstance.Remove($InstanceConnection);  
```  
  
 Note that it is important to refer to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name exactly as it is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. On a case-sensitive instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must specify the instance name using the exact casing as returned by @@SERVERNAME. To get the instance name for the managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], run this query on the managed instance:  
  
```  
select @@SERVERNAME AS instance_name  
```  
  
 At this point, the managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is fully removed from the UCP. It disappears from the list view the next time you refresh data for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. This state is identical to a user successfully going through the remove managed instance operation in the SSMS user interface.  
  
## See Also  
 [Use Utility Explorer to Manage the SQL Server Utility](../../relational-databases/manage/use-utility-explorer-to-manage-the-sql-server-utility.md)   
 [Troubleshoot the SQL Server Utility](/previous-versions/sql/sql-server-2016/ee210592(v=sql.130))  
  
