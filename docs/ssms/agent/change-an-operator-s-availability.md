---
description: "Change an Operator's Availability"
title: "Change an Operator's Availability"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, operators"
  - "canceling operators"
  - "reactivating operators"
  - "removing operators"
  - "deleting operators"
  - "available operators [SQL Server]"
  - "dropping operators"
  - "jobs [SQL Server Agent], operators"
  - "notifications [SQL Server], job status"
  - "disabling operators"
  - "operators (users) [Database Engine], changing availability with Management Studio"
ms.assetid: 10d58b92-b67b-47e2-af9c-9f9fd6968bba
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Change an Operator's Availability
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to change an operator's schedule for receiving alert notifications in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Only members of the **sysadmin** fixed server role can edit operators.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To change an operator's availability  
  
1.  In **Object Explorer**, click the plus sign to expand server that contains the operator that you want to enable or disable.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Operators** folder.  
  
4.  Right-click the operator that you want to enable or disable and select **Properties**, then click the **General** tab.  
  
5.  In the _operator\_name_**Properties** dialog box, select or clear the **Enabled** check box.  
  
6.  Click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To change an operator's availability  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- disables the 'François Ajenstat' operator  
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_update_operator   
        @name = N'François Ajenstat',  
        @enabled = 0;  
    GO  
    ```  
  
For more information, see [sp_update_operator (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-update-operator-transact-sql.md).  
