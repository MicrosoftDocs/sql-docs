---
title: "Execute a stored procedure"
description: Learn how to execute a stored procedure by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 01/25/2024
ms.service: sql
ms.subservice: stored-procedures
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.executeprocedure.general.f1"
  - "sql13.swb.executeprocedure.f1"
helpviewer_keywords:
  - "stored procedures [SQL Server], parameters"
  - "extended stored procedures [SQL Server], executing"
  - "system stored procedures [SQL Server], executing"
  - "stored procedures [SQL Server], executing"
  - "user-defined stored procedures [SQL Server]"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Execute a stored procedure
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to execute a stored procedure in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
There are different ways to execute a stored procedure. The first and most common approach is for an application or user to call the procedure. Another approach is to set the stored procedure to run automatically when an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts.  
  
When a procedure is called by an application or user, the [!INCLUDE[tsql](../../includes/tsql-md.md)] EXECUTE or EXEC keyword is explicitly stated in the call. The procedure can be called and executed without the EXEC keyword if the procedure is the first statement in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch.
  
## <a id="Restrictions"></a> Limitations and restrictions
  
The calling database collation is used when matching system procedure names. For this reason, always use the exact case of system procedure names in procedure calls. For example, this code fails if executed in the context of a database that has a case-sensitive collation:  
  
```sql  
EXEC SP_heLP; -- Fails to resolve because SP_heLP doesn't equal sp_help  
```  

To display the exact system procedure names, query the [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md) and [sys.system_parameters](../../relational-databases/system-catalog-views/sys-system-parameters-transact-sql.md) catalog views.  
  
If a user-defined procedure has the same name as a system procedure, the user-defined procedure might not ever execute.  
  
## <a id="Recommendations"></a> Recommendations
  
Use the following recommendations for executing stored procedures.

### System stored procedures
  
System procedures begin with the prefix `sp_`. Because they logically appear in all user- and system- defined databases, system procedures can be executed from any database without having to fully qualify the procedure name. However, it's best to schema-qualify all system procedure names with the `sys` schema name to prevent name conflicts. The following example shows the recommended method of calling a system procedure.  
  
```sql  
EXEC sys.sp_who;  
```  
  
### User-defined stored procedures
  
When executing a user-defined procedure, it's best to qualify the procedure name with the schema name. This practice gives a small performance boost because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] doesn't have to search multiple schemas. Using the schema name also prevents executing the wrong procedure if a database has procedures with the same name in multiple schemas.  

The following examples demonstrate the recommended method to execute a user-defined procedure. This procedure accepts two input parameters. For information about specifying input and output parameters, see [Specify parameters in a stored procedure](../../relational-databases/stored-procedures/specify-parameters.md).  
  
```sql  
EXECUTE SalesLT.uspGetCustomerCompany @LastName = N'Cannon', @FirstName = N'Chris';
GO
```  

Or:  

```sql  
EXEC SalesLT.uspGetCustomerCompany N'Cannon', N'Chris';
GO  
```  

If a nonqualified user-defined procedure is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] searches for the procedure in the following order:  

1. The `sys` schema of the current database.  

1. The caller's default schema if the procedure executes in a batch or in dynamic SQL. If the nonqualified procedure name appears inside the body of another procedure definition, the schema that contains this other procedure is searched next.  

1. The `dbo` schema in the current database.  
  
### <a id="Security"></a> Security

For security information, see [EXECUTE AS (Transact-SQL)](../../t-sql/statements/execute-as-transact-sql.md) and [EXECUTE AS Clause (Transact-SQL)](../../t-sql/statements/execute-as-clause-transact-sql.md).  
  
### <a id="Permissions"></a> Permissions

For permissions information, see [Permissions](../../t-sql/language-elements/execute-transact-sql.md#permissions) in [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md).
  
## Stored procedure execution

You can use the [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) user interface or [!INCLUDE[tsql](../../includes/tsql-md.md)] in an SSMS query window to execute a stored procedure. Always use the latest version of SSMS.

### <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)], expand that instance, and then expand **Databases**.  
  
1. Expand the database that you want, expand **Programmability**, and then expand **Stored Procedures**.  
  
1. Right-click the stored procedure that you want to run and select **Execute Stored Procedure**.  
  
1. In the **Execute Procedure** dialog box, **Parameter** indicates the name of each parameter, **Data Type** indicates its data type, and **Output Parameter** indicates whether it's an output parameter.

   For each parameter:
   
   - Under **Value**, type the value to use for the parameter.  
   - Under **Pass Null Value**, select whether to pass a NULL as the value of the parameter.  
  
1. Select **OK** to execute the stored procedure. If the stored procedure doesn't have any parameters, just select **OK**.

   The stored procedure runs, and results appear in the **Results** pane.
   
   For example, to run the `SalesLT.uspGetCustomerCompany` stored procedure from the [Create a stored procedure](create-a-stored-procedure.md) article, enter *Cannon* for the **@LastName** parameter and *Chris* for the **@FirstName** parameter, and select **OK**. The procedure returns `FirstName` **Chris**, `LastName` **Cannon**, and `CompanyName` **Outdoor Sporting Goods**.
  
### <a id="TsqlProcedure"></a> Use [!INCLUDE[tsql](../../includes/tsql-md.md)] in a query window
  
1. In SSMS, connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)].  
  
1. From the toolbar, select **New Query**.  
  
1. Enter an EXECUTE statement with the following syntax into the query window, providing values for all expected parameters:
   
   ```sql  
   EXECUTE <ProcedureName> N'<Parameter 1 value>, N'<Parameter x value>;  
   GO  
   ```  
  
   For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement executes the `uspGetCustomerCompany` stored procedure and with `Cannon` as the `@LastName` parameter value and `Chris` as the `@FirstName` parameter value:
  
   ```sql  
   EXEC SalesLT.uspGetCustomerCompany N'Cannon', N'Chris';
   GO  
   ```  

1. From the toolbar, select **Execute**. The stored procedure runs.

### Options for parameter values

There are multiple ways to provide parameters and values in stored procedure EXECUTE statements. The following examples show several different options for the EXECUTE statement.

- If you provide the parameter values in the same order as they're defined in the stored procedure, you don't need to state the parameter names. For example:

  ```sql
  EXEC SalesLT.uspGetCustomerCompany N'Cannon', N'Chris';
  ```

- If you provide parameter names in the `@parameter_name=value` pattern, you don't have to specify the parameter names and values in the same order as they're defined. For example, either of the following statements are valid:

  ```sql  
  EXEC SalesLT.uspGetCustomerCompany @FirstName = N'Chris', @LastName = N'Cannon';
  ```

  or:

  ```sql  
  EXEC SalesLT.uspGetCustomerCompany @LastName = N'Cannon', @FirstName = N'Chris';
  ```

- If you use the `@parameter_name=value` form for any parameter, you must use it for all subsequent parameters in that statement. For example, you can't use `EXEC SalesLT.uspGetCustomerCompany1 @FirstName = N'Chris', N'Cannon';`.

## Automatic execution at startup
  
**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a member of the `sysadmin` server role can use [sp_procoption](../system-stored-procedures/sp-procoption-transact-sql.md) to set or clear a procedure for automatic execution at startup. Startup procedures must be in the `master` database, must be owned by `sa`, and can't have input or output parameters. For more information, see [sp_procoption (Transact-SQL)](../system-stored-procedures/sp-procoption-transact-sql.md).  
  
Procedures marked for automatic execution at startup execute every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts and the `master` database is recovered during that startup process. Setting up procedures to execute automatically can be useful for performing database maintenance operations or for having procedures run continuously as background processes.  

Another use for automatic execution is to have the procedure perform system or maintenance tasks in `tempdb`, such as creating a global temporary table. Automatic execution ensures that such a temporary table always exists when `tempdb` is recreated during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.  
  
An automatically executed procedure operates with the same permissions as members of the `sysadmin` fixed server role. Any error messages generated by the procedure write to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  

There's no limit to the number of startup procedures you can have, but each startup procedure consumes one worker thread while executing. If you need to execute multiple procedures at startup but don't need to execute them in parallel, make one procedure the startup procedure and have that procedure call the other procedures. This method uses only one worker thread.  

> [!TIP]  
> Don't return any result sets from a procedure that's executed automatically. Because the procedure is being executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instead of an application or user, there's nowhere for result sets to go.  
  
> [!NOTE]
> Azure SQL Database is designed to isolate features from dependencies on the `master` database. As such, [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that configure server-level options aren't available in Azure SQL. You can often find appropriate alternatives from other Azure services such as [Elastic jobs](/azure/azure-sql/database/elastic-jobs-overview) or [Azure Automation](/azure/azure-sql/database/automation-manage).  
  
### Set a procedure to execute automatically at startup
  
Only the system administrator (`sa`) can mark a procedure to execute automatically.  
  
1. In SSMS, connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1. From the Standard toolbar, select **New Query**.  
  
1. Enter the following [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) commands to set a stored procedure to automatically execute at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.  
  
   ```sql  
   EXEC sp_procoption @ProcName = N'<stored procedure name>'   
       , @OptionName = 'startup'   
       , @OptionValue = 'on';
   GO
   ```  
  
1. In the toolbar, select **Execute**.

### Stop a procedure from executing automatically at startup

A `sysadmin` can use [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) to stop a procedure from automatically executing at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.  
  
1. In SSMS, connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1. From the Standard toolbar, select **New Query**.  
  
1. Enter the following commands into the query window.  
  
   ```sql  
   EXEC sp_procoption @ProcName = N'<stored procedure name>'      
       , @OptionName = 'startup'
       , @OptionValue = 'off';
   GO
   ```
   
1. In the toolbar, select **Execute**.
  
## Related content

- [Stored Procedures (Database Engine)](stored-procedures-database-engine.md)
- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [Create a stored procedure](create-a-stored-procedure.md)
- [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)
- [Specify parameters in a stored procedure](specify-parameters.md)
- [sp_procoption (Transact-SQL)](../system-stored-procedures/sp-procoption-transact-sql.md)
- [Configure the scan for startup procs (server configuration option)](../../database-engine/configure-windows/configure-the-scan-for-startup-procs-server-configuration-option.md)