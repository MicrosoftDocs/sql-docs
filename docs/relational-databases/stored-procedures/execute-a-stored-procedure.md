---
title: "Execute a Stored Procedure"
description: Learn how to execute a stored procedure by using SQL Server Management Studio or Transact-SQL.
ms.custom: FY22Q2Fresh
ms.date: "11/11/2021"
ms.service: sql
ms.reviewer: ""
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
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Execute a stored procedure
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to execute a stored procedure in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 There are two different ways to execute a stored procedure. The first and most common approach is for an application or user to call the procedure. The second approach is to set the procedure to run automatically when an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts. When a procedure is called by an application or user, the [!INCLUDE[tsql](../../includes/tsql-md.md)] EXECUTE or EXEC keyword is explicitly stated in the call. The procedure can be called and executed without the EXEC keyword if the procedure is the first statement in the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch.
  
##  <a name="BeforeYouBegin"></a> Before you begin  
  
###  <a name="Restrictions"></a> Limitations and restrictions  
  
The calling database collation is used when matching system procedure names. For this reason, always use the exact case of system procedure names in procedure calls. For example, this code will fail if executed in the context of a database that has a case-sensitive collation:  
  
```sql  
EXEC SP_heLP; -- Will fail to resolve because SP_heLP does not equal sp_help  
```  

To display the exact system procedure names, query the [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md) and [sys.system_parameters](../../relational-databases/system-catalog-views/sys-system-parameters-transact-sql.md) catalog views.  
  
If a user-defined procedure has the same name as a system procedure, the user-defined procedure might not ever execute.  
  
###  <a name="Recommendations"></a> Recommendations  
  
#### Execute system stored procedures  
  
System procedures begin with the prefix `sp_`. Because they logically appear in all user- and system- defined databases, they can be executed from any database without having to fully qualify the procedure name. However, we recommend schema-qualifying all system procedure names with the `sys` schema name to prevent name conflicts. The following example demonstrates the recommended method of calling a system procedure.  
  
```sql  
EXEC sys.sp_who;  
```  
  
#### Execute user-defined stored procedures
  
When executing a user-defined procedure, we recommend qualifying the procedure name with the schema name. This practice gives a small performance boost because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not have to search multiple schemas. It also prevents executing the wrong procedure if a database has procedures with the same name in multiple schemas.  

The following example demonstrates the recommended method to execute a user-defined procedure. Notice that the procedure accepts one input parameter. For information about specifying input and output parameters, see [Specify Parameters](../../relational-databases/stored-procedures/specify-parameters.md).  
  
```sql  
USE AdventureWorks2019;  
GO  
EXEC dbo.uspGetEmployeeManagers @BusinessEntityID = 50;
GO
```  

-Or-  

```sql  
EXEC AdventureWorks2019.dbo.uspGetEmployeeManagers 50;  
GO  
```  

If a nonqualified user-defined procedure is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] searches for the procedure in the following order:  

1. The `sys` schema of the current database.  

2. The caller's default schema if it is executed in a batch or in dynamic SQL. Or, if the nonqualified procedure name appears inside the body of another procedure definition, the schema that contains this other procedure is searched next.  

3. The `dbo` schema in the current database.  
  
#### Execute stored procedures automatically  

Procedures marked for automatic execution are executed every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts and the `master` database is recovered during that startup process. Setting up procedures to execute automatically can be useful for performing database maintenance operations or for having procedures run continuously as background processes. Another use for automatic execution is to have the procedure perform system or maintenance tasks in `tempdb`, such as creating a global temporary table. This makes sure that such a temporary table will always exist when `tempdb` is re-created during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.  

A procedure that is automatically executed operates with the same permissions as members of the sysadmin fixed server role. Any error messages generated by the procedure are written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  

There is no limit to the number of startup procedures you can have, but be aware that each consumes one worker thread while executing. If you must execute multiple procedures at startup but do not need to execute them in parallel, make one procedure the startup procedure and have that procedure call the other procedures. This uses only one worker thread.  

> [!TIP]  
>  Do not return any result sets from a procedure that is executed automatically. Because the procedure is being executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instead of an application or user, there is nowhere for the result sets to go.  
  
#### Set, clear, and control automatic execution  
  
Only the system administrator (`sa`) can mark a procedure to execute automatically. In addition, the procedure must be in the `master` database, owned by `sa`, and cannot have input or output parameters.  

Use [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) to:  

1. Designate an existing procedure as a startup procedure.  

2. Stop a procedure from executing at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup.  
  
### <a name="Security"></a> Security

 For more information, see [EXECUTE AS &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-transact-sql.md) and [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md).  
  
####  <a name="Permissions"></a> Permissions

 For more information, see the "Permissions" section in [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md).
  
## <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
### Execute a stored procedure  
  
1. In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], expand that instance, and then expand **Databases**.  
  
2. Expand the database that you want, expand **Programmability**, and then expand **Stored Procedures**.  
  
3. Right-click the user-defined stored procedure that you want and select **Execute Stored Procedure**.  
  
4. In the **Execute Procedure** dialog box, specify a value for each parameter and whether it should pass a null value.  
  
     **Parameter**  
     Indicates the name of the parameter.  
  
     **Data Type**  
     Indicates the data type of the parameter.  
  
     **Output Parameter**  
     Indicates if this is an output parameter.  
  
     **Pass Null Value**  
     Pass a NULL as the value of the parameter.  
  
     **Value**  
     Type the value for the parameter when calling the procedure.  
  
5. To execute the stored procedure, select **OK**.  
  
## <a name="TsqlProcedure"></a> Using Transact-SQL  
  
### Execute a stored procedure
  
1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2. From the Standard bar, select **New Query**.  
  
3. Copy and paste the following example into the query window and select **Execute**. This example shows how to execute a stored procedure that expects one parameter. The example executes the `uspGetEmployeeManagers` stored procedure with the value  6 specified as the `@EmployeeID` parameter.  
  
```sql  
USE AdventureWorks2019;  
GO  
EXEC dbo.uspGetEmployeeManagers 6;  
GO  
```  

### Set or clear a procedure for executing automatically  

Startup procedures must be in the `master` database and cannot contain INPUT or OUTPUT parameters. Execution of the stored procedures starts when all databases are recovered and the "Recovery is completed" message is logged at startup.  

For more information, see [sp_procoption &#40;Transact-SQL&#41;](../system-stored-procedures/sp-procoption-transact-sql.md).
  
1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2. From the Standard bar, select **New Query**.  
  
3. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) to set a procedure for automatic execution.  
  
```sql  
EXEC sp_procoption @ProcName = N'<procedure name>'   
    , @OptionName = 'startup'   
    , @OptionValue = 'on';
GO
```  
  
### Stop a procedure from executing automatically
  
1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2. From the Standard bar, select **New Query**.  
  
3. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_procoption](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md) to stop a procedure from executing automatically.  
  
```sql  
EXEC sp_procoption @ProcName = N'<procedure name>'      
    , @OptionName = 'startup'
    , @OptionValue = 'off';
GO
```  
  
## Next steps

- [Specify Parameters](../../relational-databases/stored-procedures/specify-parameters.md)
- [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)
- [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)
- [Stored Procedures &#40;Database Engine&#41;](../../relational-databases/stored-procedures/stored-procedures-database-engine.md)
- [sp_procoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md)
- [Configure the scan for startup procs Server Configuration Option](../../database-engine/configure-windows/configure-the-scan-for-startup-procs-server-configuration-option.md)
