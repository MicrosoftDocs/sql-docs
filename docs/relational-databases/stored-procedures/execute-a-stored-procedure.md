---
title: "Execute a stored procedure"
description: Learn how to execute a stored procedure by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/22/2023
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
  
 There are two different ways to execute a stored procedure. The first and most common approach is for an application or user to call the procedure. The second approach is to set the procedure to run automatically when an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts. When a procedure is called by an application or user, the [!INCLUDE[tsql](../../includes/tsql-md.md)] EXECUTE or EXEC keyword is explicitly stated in the call. The procedure can be called and executed without the EXEC keyword if the procedure is the first statement in the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch.
  
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
  
System procedures begin with the prefix `sp_`. Because they logically appear in all user- and system- defined databases, system procedures can be executed from any database without having to fully qualify the procedure name. However, it's best to schema-qualify all system procedure names with the `sys` schema name to prevent name conflicts. The following example demonstrates the recommended method of calling a system procedure.  
  
```sql  
EXEC sys.sp_who;  
```  
  
### User-defined stored procedures
  
When executing a user-defined procedure, it's best to qualify the procedure name with the schema name. This practice gives a small performance boost because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] doesn't have to search multiple schemas. This practice also prevents executing the wrong procedure if a database has procedures with the same name in multiple schemas.  

The following example demonstrates the recommended method to execute a user-defined procedure. Notice that the procedure accepts one input parameter. For information about specifying input and output parameters, see [Specify parameters in a stored procedure](../../relational-databases/stored-procedures/specify-parameters.md).  
  
```sql  
EXECUTE dbo.uspLogError @ErrorLogID = 1;
GO
```  

-Or-  

```sql  
EXEC AdventureWorksLT.dbo.uspLogError 1;  
GO  
```  

If a nonqualified user-defined procedure is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] searches for the procedure in the following order:  

1. The `sys` schema of the current database.  

1. The caller's default schema if it executes in a batch or in dynamic SQL. Or if the nonqualified procedure name appears inside the body of another procedure definition, the schema that contains this other procedure is searched next.  

1. The `dbo` schema in the current database.  
  
### <a id="Security"></a> Security

For more information, see [EXECUTE AS (Transact-SQL)](../../t-sql/statements/execute-as-transact-sql.md) and [EXECUTE AS Clause (Transact-SQL)](../../t-sql/statements/execute-as-clause-transact-sql.md).  
  
### <a id="Permissions"></a> Permissions

For more information, see the "Permissions" section in [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md).
  
## <a id="SSMSProcedure"></a> SQL Server Management Studio

You can use [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) to execute a stored procedure. Always use the latest version of SSMS.

1. In SSMS **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], expand that instance, and then expand **Databases**.  
  
1. Expand the database that you want, expand **Programmability**, and then expand **Stored Procedures**.  
  
1. Right-click the user-defined stored procedure that you want and select **Execute Stored Procedure**.  
  
1. In the **Execute Procedure** dialog box, specify a value for each parameter and whether it should pass a null value.  
  
   - **Parameter** indicates the name of the parameter.  
   - **Data Type** indicates the data type of the parameter.  
   - **Output Parameter** indicates if the parameter is an output parameter.  
   - **Pass Null Value**: Specify whether to pass a NULL as the value of the parameter.  
   - **Value**: Type the value to use for the parameter when calling the procedure.  
  
1. Select **OK** to execute the stored procedure.  
  
## <a id="TsqlProcedure"></a> Transact-SQL
  
You can also use Transact-SQL to execute a stored procedure.  
  
1. In SSMS, connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1. From the Standard toolbar, select **New Query**.  
  
1. Enter the following statements into the query window:
  
   ```sql  
   EXEC <stored procedure name> <parameter 1 value>, <parameter n value>;  
   GO  
   ```  
  
1. In the Standard toolbar, select **Execute**.

The following example shows how to execute a stored procedure that expects one parameter. The example executes the `uspLogError` stored procedure with the value `1` specified as the `@ErrorLogID` parameter.  
  
```sql  
EXEC dbo.uspLogError 1;  
GO  
```  

## Automatic execution
  
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

For [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] databases, you can use Transact-SQL [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) to:

- Designate an existing stored procedure as a startup procedure.

- Stop a stored procedure from executing at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.
  
Procedures marked for automatic execution execute every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts and the `master` database is recovered during that startup process. Setting up procedures to execute automatically can be useful for performing database maintenance operations or for having procedures run continuously as background processes. Another use for automatic execution is to have the procedure perform system or maintenance tasks in `tempdb`, such as creating a global temporary table. Automatic execution ensures that such a temporary table always exists when `tempdb` is recreated during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.  

An automatically executed procedure operates with the same permissions as members of the sysadmin fixed server role. Any error messages generated by the procedure are written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  

Only the system administrator (`sa`) can mark a procedure to execute automatically. The procedure must be in the `master` database, must be owned by `sa`, and can't have input or output parameters. For more information, see [sp_procoption (Transact-SQL)](../system-stored-procedures/sp-procoption-transact-sql.md).  
  
There's no limit to the number of startup procedures you can have, but each startup procedure consumes one worker thread while executing. If you must execute multiple procedures at startup but don't need to execute them in parallel, make one procedure the startup procedure and have that procedure call the other procedures. This method uses only one worker thread.  

> [!TIP]  
> Don't return any result sets from a procedure that's executed automatically. Because the procedure is being executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instead of an application or user, there's nowhere for result sets to go.  
  
> [!NOTE]
> For Azure SQL Database automation options, see the following resources:
> 
> - [Elastic jobs in Azure SQL Database (preview)](../../../azure-sql/database/elastic-jobs-overview.md)
> - [Manage databases in Azure SQL Database by using Azure Automation](../../../azure-sql/database/automation-manage.md)

### Set a procedure to execute automatically

Startup procedures must be in the `master` database and can't contain INPUT or OUTPUT parameters. Execution of the stored procedures starts when all databases are recovered and the "Recovery is completed" message is logged at startup.  
  
1. In SSMS, connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1. From the Standard toolbar, select **New Query**.  
  
1. Enter the following commands into the query window. These commands use [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) to set a procedure for automatic execution.  
  
   ```sql  
   EXEC sp_procoption @ProcName = N'<stored procedure name>'   
       , @OptionName = 'startup'   
       , @OptionValue = 'on';
   GO
   ```  
  
1. In the toolbar, select **Execute**.

### Stop a procedure from executing automatically

You can use [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) to stop a procedure from executing at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.  
  
1. In SSMS, connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1. From the Standard toolbar, select **New Query**.  
  
1. Enter the following commands into the query window.  
  
   ```sql  
   EXEC sp_procoption @ProcName = N'<stored procedure name>'      
       , @OptionName = 'startup'
       , @OptionValue = 'off';
   GO
   ```  
  
## Next steps

- [Specify parameters in a stored procedure](../../relational-databases/stored-procedures/specify-parameters.md)
- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)
- [Stored Procedures (Database Engine)](../../relational-databases/stored-procedures/stored-procedures-database-engine.md)
- [sp_procoption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md)
- [Configure the scan for startup procs (server configuration option)](../../database-engine/configure-windows/configure-the-scan-for-startup-procs-server-configuration-option.md)