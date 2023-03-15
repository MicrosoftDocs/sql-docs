---
title: "Change Proxy Account for Utility Collection on Managed SQL Server"
description: Learn how to use SQL Server Management Studio to change the proxy account for the Utility Collection Set on a managed instance of SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---
# Change Proxy Account for Utility Collection on  Managed SQL Server
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to change the proxy account for the Utility Collection Set on a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  

[!INCLUDE[instances-managed-by-utility](../../includes/instances-managed-by-utility.md)]

##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To change the proxy account for the utility collection set on a managed instance of SQL Server  
  
1.  Remove the managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
    -   In **Utility Explorer** in SSMS, click on the **Managed Instances** node.  
  
    -   In the **Utility Explorer** list view, right-click the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name, and select **Remove Managed Instance...**. For more information, see [Remove an Instance of SQL Server from the SQL Server Utility](../../relational-databases/manage/remove-an-instance-of-sql-server-from-the-sql-server-utility.md).  
  
2.  Enroll the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility again.  

    -   In **Utility Explorer** in SSMS, right-click on the **Managed Instances** node, and select **Add Managed Instance...**.  
  
    -   Follow prompts to complete the wizard, specifying the new proxy account.  
  
## See Also  
 [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)   
 [Troubleshoot the SQL Server Utility](/previous-versions/sql/sql-server-2016/ee210592(v=sql.130))  
  
