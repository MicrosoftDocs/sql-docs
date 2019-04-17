---
title: "Delete an Alert | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, alerts"
  - "alerts [SQL Server], deleting"
  - "deleting alerts"
  - "canceling alerts"
  - "dropping alerts"
  - "disabling alerts"
  - "removing alerts"
ms.assetid: c982b208-e2d1-4d34-8cee-940b9baf6586
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Delete an Alert
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to delete [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To delete an alert, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
    [Transact-SQL](#TsqlProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
Removing an alert also removes any notifications associated with the alert.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
By default, only members of the **sysadmin** fixed server role can delete alerts.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To delete an alert  
  
1.  In **Object Explorer,** click the plus sign to expand the server that contains the SQL Server Agent alert that you want to delete.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Alerts** folder.  
  
4.  Right-click the alert you want to delete and select **Delete**.  
  
5.  In the **Delete Object** dialog box, confirm that the correct alert is selected and click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To delete an alert  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- deletes the SQL Server Agent alert called 'Test Alert.'  
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_delete_alert  
       @name = N'Test Alert' ;  
    GO  
    ```  
  
For more information, see [sp_delete_alert (Transact-SQL)](https://msdn.microsoft.com/a831315e-793d-41c4-8333-b324bb2bc614).  
  
