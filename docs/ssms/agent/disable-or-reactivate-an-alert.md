---
description: "Disable or Reactivate an Alert"
title: "Disable or Reactivate an Alert"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, alerts"
  - "alerts [SQL Server], reactivating"
  - "deleting alerts"
  - "canceling alerts"
  - "dropping alerts"
  - "disabling alerts"
  - "alerts [SQL Server], disabling"
  - "reactivating alerts"
  - "removing alerts"
ms.assetid: 4cb37dc6-1134-405d-8590-58b44dcf63b2
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Disable or Reactivate an Alert
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to disable or reactivate a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alert in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
By default, members of the **sysadmin** fixed server role can edit information in an alert. Other users must be granted the **SQLAgentOperatorRole** fixed database role in the **msdb** database.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To disable or reactivate an alert  
  
1.  In **Object Explorer**, click the plus sign to expand server that contains the alert you wish to disable or reactivate.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Alerts** folder.  
  
4.  Right-click the alert you wish to enable and select **Enable** To disable an alert, right-click the alert you want to disable and select **Disable**.  
  
5.  The **Disable Alert** or **Enable Alert** dialog box displays showing the status of the process. When finished, click **Close**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To disable or reactivate an alert  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- changes the enabled setting of Test Alert to 0  
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_update_alert  
        @name = N'Test Alert',  
        @enabled = 0 ;  
    GO  
    ```  
  
For more information, see [sp_update_alert (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-update-alert-transact-sql.md).  
