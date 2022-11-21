---
description: "Delete an Operator"
title: "Delete an Operator"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, operators"
  - "canceling operators"
  - "removing operators"
  - "deleting operators"
  - "dropping operators"
  - "jobs [SQL Server Agent], operators"
  - "operators (users) [Database Engine], deleting with Management Studio"
ms.assetid: 2b7b8627-082d-4189-8584-abd3a9b604cf
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Delete an Operator
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to remove an operator so they no longer receive [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alert notifications in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions 
When an operator is removed, all the notifications associated with the operator are also removed.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Members of the **sysadmin** fixed server role can delete operators.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To delete an operator  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the operator you want to delete.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Operators** folder.  
  
4.  Right-click the operator that you want to delete and select **Delete**.  
  
5.  In the **Delete Object** dialog box, make sure that the correct operator is selected, and then click **OK**. If you want another operator to receive the alerts and jobs sent to the deleted operator, check **Reassign to** and select an operator in the list.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To delete an operator  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- deletes operator 'Test Operator' and reassigns all alerts and jobs
    --  sent to that operator to 'François Ajenstat'  
    USE msdb ;  
    GO  
  
    EXEC sp_delete_operator @name = 'Test Operator',  
        @reassign_to_operator = 'François Ajenstat';  
    GO  
    ```  
  
For more information, see [sp_delete_operator (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-delete-operator-transact-sql.md).  
