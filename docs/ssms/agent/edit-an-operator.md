---
description: "Edit an Operator"
title: "Edit an Operator"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, operators"
  - "modifying operators"
  - "jobs [SQL Server Agent], operators"
  - "operators (users) [Database Engine], modifying with Management Studio"
ms.assetid: b2ba2168-ca0b-4b59-9007-4e1e4c30679e
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Edit an Operator
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to edit an operators' availability for receiving notifications and their e-mail, pager, and net send addresses in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   The Pager and **net send** options will be removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features.  
  
-   Note that SQL Server Agent must be configured to use Database Mail to send e-mail and pager notifications to operators. For more information, see [Assign Alerts to an Operator](assign-alerts-to-an-operator.md).  
  
-   SQL Server Management Studio provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Only members of the **sysadmin** fixed server role can edit operators.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To edit an operator  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the operator you want to edit.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Operators** folder.  
  
4.  Right-click the operator that you want to edit and select **Properties**.  
  
    For more information on the available options contained in the _operator\_name_**Properties** dialog box, see:  
  
    -   [Operator Properties - New Operator &#40;General Page&#41;](../../ssms/agent/operator-properties-new-operator-general-page.md)  
  
    -   [Operator Properties - New Operator &#40;Notifications Page&#41;](../../ssms/agent/operator-properties-new-operator-notifications-page.md)  
  
    -   [Operator Properties &#40;History Page&#41;](../../ssms/agent/operator-properties-history-page.md)  
  
5.  When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To edit an operator  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- updates the operator status to enabled, and sets the days   
    -- (from Monday through Friday, from 8 A.M. through 5 P.M.)
    --  when the operator can be paged.   
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_update_operator   
        @name = N'François Ajenstat',  
        @enabled = 1,  
        @email_address = N'françoisa',  
        @pager_address = N'5551290AW@pager.Adventure-Works.com',  
        @weekday_pager_start_time = 080000,  
        @weekday_pager_end_time = 170000,  
        @pager_days = 64 ;  
    GO  
    ```  
  
For more information, see [sp_update_operator (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-update-operator-transact-sql.md).  
