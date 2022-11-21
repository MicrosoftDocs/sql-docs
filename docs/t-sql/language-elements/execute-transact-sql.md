---
title: "EXECUTE (Transact-SQL)"
description: "EXECUTE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "08/07/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "EXEC"
  - "EXECUTE_TSQL"
  - "EXECUTE"
  - "EXEC_TSQL"
helpviewer_keywords:
  - "remote stored procedures [SQL Server]"
  - "command strings [SQL Server]"
  - "extended stored procedures [SQL Server], executing"
  - "stored procedures [SQL Server], executing"
  - "Transact-SQL statements, executing"
  - "strings [SQL Server], executing"
  - "statements [SQL Server], executing"
  - "context switching [SQL Server], execution context"
  - "user-defined functions [SQL Server], executing"
  - "character strings [SQL Server], executing"
  - "switching execution context"
  - "EXECUTE statement"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# EXECUTE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Executes a command string or character string within a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, or one of the following modules: system stored procedure, user-defined stored procedure, CLR stored procedure, scalar-valued user-defined function, or extended stored procedure. The EXECUTE statement can be used to send pass-through commands to linked servers. Additionally, the context in which a string or command is executed can be explicitly set. Metadata for the result set can be defined by using the WITH RESULT SETS options.
  
> [!IMPORTANT]  
>  Before you call EXECUTE with a character string, validate the character string. Never execute a command constructed from user input that has not been validated.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  

::: moniker range=">=sql-server-ver15"
The following code block shows the syntax in SQL Server 2019. Alternatively, see [syntax in SQL Server 2017 and earlier](execute-transact-sql.md?view=sql-server-2017&preserve-view=true) instead. 

```syntaxsql
-- Syntax for SQL Server 2019
  
Execute a stored procedure or function  
[ { EXEC | EXECUTE } ]  
    {   
      [ @return_status = ]  
      { module_name [ ;number ] | @module_name_var }   
        [ [ @parameter = ] { value   
                           | @variable [ OUTPUT ]   
                           | [ DEFAULT ]   
                           }  
        ]  
      [ ,...n ]  
      [ WITH <execute_option> [ ,...n ] ]  
    }  
[;]  
  
Execute a character string  
{ EXEC | EXECUTE }   
    ( { @string_variable | [ N ]'tsql_string' } [ + ...n ] )  
    [ AS { LOGIN | USER } = ' name ' ]  
[;]  
  
Execute a pass-through command against a linked server  
{ EXEC | EXECUTE }  
    ( { @string_variable | [ N ] 'command_string [ ? ]' } [ + ...n ]  
        [ { , { value | @variable [ OUTPUT ] } } [ ...n ] ]  
    )   
    [ AS { LOGIN | USER } = ' name ' ]  
    [ AT linked_server_name ]  
    [ AT DATA_SOURCE data_source_name ]  
[;]  
  
<execute_option>::=  
{  
        RECOMPILE   
    | { RESULT SETS UNDEFINED }   
    | { RESULT SETS NONE }   
    | { RESULT SETS ( <result_sets_definition> [,...n ] ) }  
}   
  
<result_sets_definition> ::=   
{  
    (  
         { column_name   
           data_type   
         [ COLLATE collation_name ]   
         [ NULL | NOT NULL ] }  
         [,...n ]  
    )  
    | AS OBJECT   
        [ db_name . [ schema_name ] . | schema_name . ]   
        {table_name | view_name | table_valued_function_name }  
    | AS TYPE [ schema_name.]table_type_name  
    | AS FOR XML   
}  
```  
::: moniker-end

::: moniker range=">=sql-server-2016"

The following code block shows the syntax in SQL Server 2017 and earlier. Alternatively, see [syntax in SQL Server 2019](execute-transact-sql.md?view=sql-server-ver15&preserve-view=true) instead.


```syntaxsql
-- Syntax for SQL Server 2017 and earlier  
  
Execute a stored procedure or function  
[ { EXEC | EXECUTE } ]  
    {   
      [ @return_status = ]  
      { module_name [ ;number ] | @module_name_var }   
        [ [ @parameter = ] { value   
                           | @variable [ OUTPUT ]   
                           | [ DEFAULT ]   
                           }  
        ]  
      [ ,...n ]  
      [ WITH <execute_option> [ ,...n ] ]  
    }  
[;]  
  
Execute a character string  
{ EXEC | EXECUTE }   
    ( { @string_variable | [ N ]'tsql_string' } [ + ...n ] )  
    [ AS { LOGIN | USER } = ' name ' ]  
[;]  
  
Execute a pass-through command against a linked server  
{ EXEC | EXECUTE }  
    ( { @string_variable | [ N ] 'command_string [ ? ]' } [ + ...n ]  
        [ { , { value | @variable [ OUTPUT ] } } [ ...n ] ]  
    )   
    [ AS { LOGIN | USER } = ' name ' ]  
    [ AT linked_server_name ]  
[;]  
  
<execute_option>::=  
{  
        RECOMPILE   
    | { RESULT SETS UNDEFINED }   
    | { RESULT SETS NONE }   
    | { RESULT SETS ( <result_sets_definition> [,...n ] ) }  
}   
  
<result_sets_definition> ::=   
{  
    (  
         { column_name   
           data_type   
         [ COLLATE collation_name ]   
         [ NULL | NOT NULL ] }  
         [,...n ]  
    )  
    | AS OBJECT   
        [ db_name . [ schema_name ] . | schema_name . ]   
        {table_name | view_name | table_valued_function_name }  
    | AS TYPE [ schema_name.]table_type_name  
    | AS FOR XML   
}  
```  
::: moniker-end

  
```syntaxsql
-- In-Memory OLTP   

Execute a natively compiled, scalar user-defined function  
[ { EXEC | EXECUTE } ]   
    {   
      [ @return_status = ]   
      { module_name | @module_name_var }   
        [ [ @parameter = ] { value   
                           | @variable   
                           | [ DEFAULT ]   
                           }  
        ]   
      [ ,...n ]   
      [ WITH <execute_option> [ ,...n ] ]   
    }  
<execute_option>::=  
{  
    | { RESULT SETS UNDEFINED }   
    | { RESULT SETS NONE }   
    | { RESULT SETS ( <result_sets_definition> [,...n ] ) }  
}  
```  
  
```syntaxsql
-- Syntax for Azure SQL Database   
  
Execute a stored procedure or function  
[ { EXEC | EXECUTE } ]  
    {   
      [ @return_status = ]  
      { module_name  | @module_name_var }   
        [ [ @parameter = ] { value   
                           | @variable [ OUTPUT ]   
                           | [ DEFAULT ]   
                           }  
        ]  
      [ ,...n ]  
      [ WITH RECOMPILE ]  
    }  
[;]  
  
Execute a character string  
{ EXEC | EXECUTE }   
    ( { @string_variable | [ N ]'tsql_string' } [ + ...n ] )  
    [ AS {  USER } = ' name ' ]  
[;]  
  
<execute_option>::=  
{  
        RECOMPILE   
    | { RESULT SETS UNDEFINED }   
    | { RESULT SETS NONE }   
    | { RESULT SETS ( <result_sets_definition> [,...n ] ) }  
}   
  
<result_sets_definition> ::=   
{  
    (  
         { column_name   
           data_type   
         [ COLLATE collation_name ]   
         [ NULL | NOT NULL ] }  
         [,...n ]  
    )  
    | AS OBJECT   
        [ db_name . [ schema_name ] . | schema_name . ]   
        {table_name | view_name | table_valued_function_name }  
    | AS TYPE [ schema_name.]table_type_name  
    | AS FOR XML  
  
```

  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  

-- Execute a stored procedure  
[ { EXEC | EXECUTE } ]  
    procedure_name   
        [ { value | @variable [ OUT | OUTPUT ] } ] [ ,...n ] }  
[;]  
  
-- Execute a SQL string  
{ EXEC | EXECUTE }  
    ( { @string_variable | [ N ] 'tsql_string' } [ +...n ] )  
[;]  
```  


  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 @*return_status*  
 Is an optional integer variable that stores the return status of a module. This variable must be declared in the batch, stored procedure, or function before it is used in an EXECUTE statement.  
  
 When used to invoke a scalar-valued user-defined function, the @*return_status* variable can be of any scalar data type.  
  
 *module_name*  
 Is the fully qualified or nonfully qualified name of the stored procedure or scalar-valued user-defined function to call. Module names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). The names of extended stored procedures are always case-sensitive, regardless of the collation of the server.  
  
 A module that has been created in another database can be executed if the user running the module owns the module or has the appropriate permission to execute it in that database. A module can be executed on another server running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if the user running the module has the appropriate permission to use that server (remote access) and to execute the module in that database. If a server name is specified but no database name is specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] looks for the module in the default database of the user.  
  
 ;*number*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Is an optional integer that is used to group procedures of the same name. This parameter is not used for extended stored procedures.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 For more information about procedure groups, see [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md).  
  
 @*module_name_var*  
 Is the name of a locally defined variable that represents a module name.  
  
 This can be a variable that holds the name of a natively compiled, scalar user-defined function.  
  
 @*parameter*  
 Is the parameter for *module_name*, as defined in the module. Parameter names must be preceded by the at sign (@). When used with the @*parameter_name*=*value* form, parameter names and constants do not have to be supplied in the order in which they are defined in the module. However, if the @*parameter_name*=*value* form is used for any parameter, it must be used for all subsequent parameters.  
  
 By default, parameters are nullable.  
  
 *value*  
 Is the value of the parameter to pass to the module or pass-through command. If parameter names are not specified, parameter values must be supplied in the order defined in the module.  
  
 When executing pass-through commands against linked servers, the order of the parameter values depends on the OLE DB provider of the linked server. Most OLE DB providers bind values to parameters from left to right.  
  
 If the value of a parameter is an object name, character string, or qualified by a database name or schema name, the whole name must be enclosed in single quotation marks. If the value of a parameter is a keyword, the keyword must be enclosed in double quotation marks.  
  
If you pass a single word that does not begin with `@` and that's not enclosed in quotation marks - for example, if you forget `@` on a parameter name - the word is treated as an nvarchar string, in spite of the missing quotation marks.

 If a default is defined in the module, a user can execute the module without specifying a parameter.  
  
 The default can also be NULL. Generally, the module definition specifies the action that should be taken if a parameter value is NULL.  
  
 @*variable*  
 Is the variable that stores a parameter or a return parameter.  
  
 OUTPUT  
 Specifies that the module or command string returns a parameter. The matching parameter in the module or command string must also have been created by using the keyword OUTPUT. Use this keyword when you use cursor variables as parameters.  
  
 If *value* is defined as OUTPUT of a module executed against a linked server, any changes to the corresponding @*parameter* performed by the OLE DB provider will be copied back to the variable at the end of the execution of module.  
  
 If OUTPUT parameters are being used and the intent is to use the return values in other statements within the calling batch or module, the value of the parameter must be passed as a variable, such as  @*parameter* = @*variable*. You cannot execute a module by specifying OUTPUT for a parameter that is not defined as an OUTPUT parameter in the module. Constants cannot be passed to module by using OUTPUT; the return parameter requires a variable name. The data type of the variable must be declared and a value assigned before executing the procedure.  
  
 When EXECUTE is used against a remote stored procedure, or to execute a pass-through command against a linked server, OUTPUT parameters cannot be any one of the large object (LOB) data types.  
  
 Return parameters can be of any data type except the LOB data types.  
  
 DEFAULT  
 Supplies the default value of the parameter as defined in the module. When the module expects a value for a parameter that does not have a defined default and either a parameter is missing or the DEFAULT keyword is specified, an error occurs.  
  
 @*string_variable*  
 Is the name of a local variable. @*string_variable* can be any **char**, **varchar**, **nchar**, or **nvarchar** data type. These include the **(max)** data types.  
  
 [N] '*tsql_string*'  
 Is a constant string. *tsql_string* can be any **nvarchar** or **varchar** data type. If the N is included, the string is interpreted as **nvarchar** data type.  
  
 AS \<context_specification>  
 Specifies the context in which the statement is executed.  
  
 LOGIN  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Specifies the context to be impersonated is a login. The scope of impersonation is the server.  
  
 USER  
 Specifies the context to be impersonated is a user in the current database. The scope of impersonation is restricted to the current database. A context switch to a database user does not inherit the server-level permissions of that user.  
  
> [!IMPORTANT]  
>  While the context switch to the database user is active, any attempt to access resources outside the database will cause the statement to fail. This includes USE *database* statements, distributed queries, and queries that reference another database by using three- or four-part identifiers.  
  
 '*name*'  
 Is a valid user or login name. *name* must be a member of the sysadmin fixed server role or exist as a principal in [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md) or [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md), respectively.  
  
 *name* cannot be a built-in account, such as NT AUTHORITY\LocalService, NT AUTHORITY\NetworkService, or NT AUTHORITY\LocalSystem.  
  
 For more information, see [Specifying a User or Login Name](#_user) later in this topic.  
  
 [N] '*command_string*'  
 Is a constant string that contains the command to be passed through to the linked server. If the N is included, the string is interpreted as **nvarchar** data type.  
  
 [?]  
 Indicates parameters for which values are supplied in the \<arg-list> of pass-through commands that are used in an EXEC('...', \<arg-list>) AT \<linkedsrv> statement.  
  
 AT *linked_server_name*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Specifies that *command_string* is executed against *linked_server_name* and results, if any, are returned to the client. *linked_server_name* must refer to an existing linked server definition in the local server. Linked servers are defined by using [sp_addlinkedserver](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).  
  
 WITH \<execute_option>  
 Possible execute options. The RESULT SETS options cannot be specified in an INSERT...EXEC statement.  
 
AT DATA_SOURCE data_source_name 
**Applies to**: [!INCLUDE[sssql19](../../includes/sssql19-md.md)] and later
  
 Specifies that *command_string* is executed against *data_source_name* and results, if any, are returned to the client. *data_source_name* must refer to an existing EXTERNAL DATA SOURCE definition in the database. Only data sources that point to SQL Server are supported. Additionally, for SQL Server big data cluster data sources that point to compute pool, data pool or storage pool are supported. Data sources are defined by using [CREATE EXTERNAL DATA SOURCE](../statements/create-external-data-source-transact-sql.md).  
  
 WITH \<execute_option>  
 Possible execute options. The RESULT SETS options cannot be specified in an INSERT...EXEC statement.  
  
|Term|Definition|  
|----------|----------------|  
|RECOMPILE|Forces a new plan to be compiled, used, and discarded after the module is executed. If there is an existing query plan for the module, this plan remains in the cache.<br /><br /> Use this option if the parameter you are supplying is atypical or if the data has significantly changed. This option is not used for extended stored procedures. We recommend that you use this option sparingly because it is expensive.<br /><br /> **Note:** You can not use WITH RECOMPILE when calling a stored procedure that uses OPENDATASOURCE syntax. The WITH RECOMPILE option is ignored when a four-part object name is specified.<br /><br /> **Note:** RECOMPILE is not supported with natively compiled, scalar user-defined functions. If you need to recompile, use [sp_recompile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-recompile-transact-sql.md).|  
|**RESULT SETS UNDEFINED**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> This option provides no guarantee of what results, if any, will be returned, and no definition is provided. The statement executes without error if any results are returned or no results are returned. RESULT SETS UNDEFINED is the default behavior if a result_sets_option is not provided.<br /><br /> For interpreted scalar user-defined functions, and natively compiled scalar user-defined functions, this option is not operational because the functions never return a result set.|  
|RESULT SETS NONE|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> Guarantees that the execute statement will not return any results. If any results are returned the batch is aborted.<br /><br /> For interpreted scalar user-defined functions, and natively compiled scalar user-defined functions, this option is not operational because the functions never return a result set.|  
|*\<result_sets_definition>*|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> Provides a guarantee that the result will come back as specified in the result_sets_definition. For statements that return multiple result sets, provide multiple *result_sets_definition* sections. Enclose each *result_sets_definition* in parentheses, separated by commas. For more information, see \<result_sets_definition> later in this topic.<br /><br /> This option always results in an error for natively compiled, scalar user-defined functions because the functions never return a result set.|
  
\<result_sets_definition>
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]
  
 Describes the result sets returned by the executed statements. The clauses of the result_sets_definition have the following meaning  
  
|Term|Definition|  
|----------|----------------|  
|{<br /><br /> column_name<br /><br /> data_type<br /><br /> [ COLLATE collation_name]<br /><br /> [NULL &#124; NOT NULL]<br /><br /> }|See the table below.|  
|db_name|The name of the database containing the table, view or table valued function.|  
|schema_name|The name of the schema owning the table, view or table valued function.|  
|table_name &#124; view_name &#124; table_valued_function_name|Specifies that the columns returned will be those specified in the table, view or table valued function named. Table variables, temporary tables, and synonyms are not supported in the AS object syntax.|  
|AS TYPE [schema_name.]table_type_name|Specifies that the columns returned will be those specified in the table type.|  
|AS FOR XML|Specifies that the XML results from the statement or stored procedure called by the EXECUTE statement will be converted into the format as though they were produced by a SELECT ... FOR XML ... statement. All formatting from the type directives in the original statement are removed, and the results returned are as though no type directive was specified. AS FOR XML does not convert non-XML tabular results from the executed statement or stored procedure into XML.|  
  
|Term|Definition|  
|----------|----------------|  
|column_name|The names of each column. If the number of columns differs from the result set, an error occurs and the batch is aborted. If the name of a column differs from the result set, the column name returned will be set to the name defined.|  
|data_type|The data types of each column. If the data types differ, an implicit conversion to the defined data type is performed. If the conversion fails the batch is aborted|  
|COLLATE collation_name|The collation of each column. If there is a collation mismatch, an implicit collation is attempted. If that fails, the batch is aborted.|  
|NULL &#124; NOT NULL|The nullability of each column. If the defined nullability is NOT NULL and the data returned contains NULLs an error occurs and the batch is aborted. If not specified, the default value conforms to the setting of the ANSI_NULL_DFLT_ON and ANSI_NULL_DFLT_OFF options.|  
  
 The actual result set being returned during execution can differ from the result defined using the WITH RESULT SETS clause in one of the following ways: number of result sets, number of columns, column name, nullability, and data type. If the number of result sets differs, an error occurs and the batch is aborted.  
  
## Remarks  
 Parameters can be supplied either by using *value* or by using @*parameter_name*=*value.* A parameter is not part of a transaction; therefore, if a parameter is changed in a transaction that is later rolled back, the value of the parameter does not revert to its previous value. The value returned to the caller is always the value at the time the module returns.  
  
 Nesting occurs when one module calls another or executes managed code by referencing a common language runtime (CLR) module, user-defined type, or aggregate. The nesting level is incremented when the called module or managed code reference starts execution, and it is decremented when the called module or managed code reference has finished. Exceeding the maximum of 32 nesting levels causes the complete calling chain to fail. The current nesting level is stored in the @@NESTLEVEL system function.  
  
 Because remote stored procedures and extended stored procedures are not within the scope of a transaction (unless issued within a BEGIN DISTRIBUTED TRANSACTION statement or when used with various configuration options), commands executed through calls to them cannot be rolled back. For more information, see [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md) and [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md).  
  
 When you use cursor variables, if you execute a procedure that passes in a cursor variable with a cursor allocated to it an error occurs.  
  
 You do not have to specify the EXECUTE keyword when executing modules if the statement is the first one in a batch.  
  
 For additional information specific to CLR stored procedures, see CLR Stored Procedures.  
  
## Using EXECUTE with Stored Procedures  
 You do not have to specify the EXECUTE keyword when you execute stored procedures when the statement is the first one in a batch.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system stored procedures start with the characters sp_. They are physically stored in the [Resource database](../../relational-databases/databases/resource-database.md), but logically appear in the sys schema of every system and user-defined database. When you execute a system stored procedure, either in a batch or inside a module such as a user-defined stored procedure or function, we recommend that you qualify the stored procedure name with the sys schema name.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system extended stored procedures start with the characters xp_, and these are contained in the dbo schema of the master database. When you execute a system extended stored procedure, either in a batch or inside a module such as a user-defined stored procedure or function, we recommend that you qualify the stored procedure name with master.dbo.  
  
 When you execute a user-defined stored procedure, either in a batch or inside a module such as a user-defined stored procedure or function, we recommend that you qualify the stored procedure name with a schema name. We do not recommend that you name a user-defined stored procedure with the same name as a system stored procedure. For more information about executing stored procedures, see [Execute a Stored Procedure](../../relational-databases/stored-procedures/execute-a-stored-procedure.md).  
  
## Using EXECUTE with a Character String  
 In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], character strings are limited to 8,000 bytes. This requires concatenating large strings for dynamic execution. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **varchar(max)** and **nvarchar(max)** data types can be specified that allow for character strings to be up to 2 gigabytes of data.  
  
 Changes in database context last only until the end of the EXECUTE statement. For example, after the `EXEC` in this following statement is run, the database context is master.  
  
```sql  
USE master; EXEC ('USE AdventureWorks2012; SELECT BusinessEntityID, JobTitle FROM HumanResources.Employee;');  
```  
  
## Context Switching  
 You can use the `AS { LOGIN | USER } = ' name '` clause to switch the execution context of a dynamic statement. When the context switch is specified as `EXECUTE ('string') AS <context_specification>`, the duration of the context switch is limited to the scope of the query being executed.  
  
###  <a name="_user"></a> Specifying a User or Login Name  
 The user or login name specified in `AS { LOGIN | USER } = ' name '` must exist as a principal in sys.database_principals or sys.server_principals, respectively, or the statement will fail. Additionally, IMPERSONATE permissions must be granted on the principal. Unless the caller is the database owner or is a member of the sysadmin fixed server role, the principal must exist even when the user is accessing the database or instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through a Windows group membership. For example, assume the following conditions:  
  
-   CompanyDomain\SQLUsers group has access to the Sales database.  
  
-   CompanyDomain\SqlUser1 is a member of SQLUsers and, therefore, has implicit access to the Sales database.  
  
 Although CompanyDomain\SqlUser1 has access to the database through membership in the SQLUsers group, the statement `EXECUTE @string_variable AS USER = 'CompanyDomain\SqlUser1'` will fail because `CompanyDomain\SqlUser1` does not exist as a principal in the database.  
  
### Best Practices  
 Specify a login or user that has the least privileges required to perform the operations that are defined in the statement or module. For example, do not specify a login name, which has server-level permissions, if only database-level permissions are required; or do not specify a database owner account unless those permissions are required.  
  
## Permissions  
 Permissions are not required to run the EXECUTE statement. However, permissions are required on the securables that are referenced within the EXECUTE string. For example, if the string contains an INSERT statement, the caller of the EXECUTE statement must have INSERT permission on the target table. Permissions are checked at the time EXECUTE statement is encountered, even if the EXECUTE statement is included within a module.  
  
 EXECUTE permissions for a module default to the owner of the module, who can transfer them to other users. When a module is run that executes a string, permissions are checked in the context of the user who executes the module, not in the context of the user who created the module. However, if the same user owns the calling module and the module being called, EXECUTE permission checking is not performed for the second module.  
  
 If the module accesses other database objects, execution succeeds when you have EXECUTE permission on the module and one of the following is true:  
  
-   The module is marked EXECUTE AS USER or SELF, and the module owner has the corresponding permissions on the referenced object. For more information about impersonation within a module, see [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md).  
  
-   The module is marked EXECUTE AS CALLER, and you have the corresponding permissions on the object.  
  
-   The module is marked EXECUTE AS *user_name*, and *user_name* has the corresponding permissions on the object.  
  
### Context Switching Permissions  
 To specify EXECUTE AS on a login, the caller must have IMPERSONATE permissions on the specified login name. To specify EXECUTE AS on a database user, the caller must have IMPERSONATE permissions on the specified user name. When no execution context is specified, or EXECUTE AS CALLER is specified, IMPERSONATE permissions are not required.  
  
## Examples: SQL Server
  
### A. Using EXECUTE to pass a single parameter  
 The `uspGetEmployeeManagers` stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database expects one parameter (`@EmployeeID`). The following examples execute the `uspGetEmployeeManagers` stored procedure with `Employee ID 6` as its parameter value.  
  
```sql    
EXEC dbo.uspGetEmployeeManagers 6;  
GO  
```  
  
 The variable can be explicitly named in the execution:  
  
```sql    
EXEC dbo.uspGetEmployeeManagers @EmployeeID = 6;  
GO  
```  
  
 If the following is the first statement in a batch or an **osql** or **sqlcmd** script, EXEC is not required.  
  
```sql    
dbo.uspGetEmployeeManagers 6;  
GO  
--Or  
dbo.uspGetEmployeeManagers @EmployeeID = 6;  
GO  
```  
  
### B. Using multiple parameters  
 The following example executes the `spGetWhereUsedProductID` stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. It passes two parameters: the first parameter is a product ID (`819`) and the second parameter, `@CheckDate,` is a `datetime` value.  
  
```sql    
DECLARE @CheckDate DATETIME;  
SET @CheckDate = GETDATE();  
EXEC dbo.uspGetWhereUsedProductID 819, @CheckDate;  
GO  
```  
  
### C. Using EXECUTE 'tsql_string' with a variable  
 The following example shows how `EXECUTE` handles dynamically built strings that contain variables. This example creates the `tables_cursor` cursor to hold a list of all user-defined tables in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, and then uses that list to rebuild all indexes on the tables.  
  
```sql    
DECLARE tables_cursor CURSOR  
   FOR  
   SELECT s.name, t.name   
   FROM sys.objects AS t  
   JOIN sys.schemas AS s ON s.schema_id = t.schema_id  
   WHERE t.type = 'U';  
OPEN tables_cursor;  
DECLARE @schemaname sysname;  
DECLARE @tablename sysname;  
FETCH NEXT FROM tables_cursor INTO @schemaname, @tablename;  
WHILE (@@FETCH_STATUS <> -1)  
BEGIN;  
   EXECUTE ('ALTER INDEX ALL ON ' + @schemaname + '.' + @tablename + ' REBUILD;');  
   FETCH NEXT FROM tables_cursor INTO @schemaname, @tablename;  
END;  
PRINT 'The indexes on all tables have been rebuilt.';  
CLOSE tables_cursor;  
DEALLOCATE tables_cursor;  
GO  
  
```  
  
### D. Using EXECUTE with a remote stored procedure  
 The following example executes the `uspGetEmployeeManagers` stored procedure on the remote server `SQLSERVER1` and stores the return status that indicates success or failure in `@retstat`.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
```sql    
DECLARE @retstat INT;  
EXECUTE @retstat = SQLSERVER1.AdventureWorks2012.dbo.uspGetEmployeeManagers @BusinessEntityID = 6;  
```  
  
### E. Using EXECUTE with a stored procedure variable  
 The following example creates a variable that represents a stored procedure name.  
  
```sql  
DECLARE @proc_name VARCHAR(30);  
SET @proc_name = 'sys.sp_who';  
EXEC @proc_name;  
  
```  
  
### F. Using EXECUTE with DEFAULT  
 The following example creates a stored procedure with default values for the first and third parameters. When the procedure is run, these defaults are inserted for the first and third parameters when no value is passed in the call or when the default is specified. Note the various ways the `DEFAULT` keyword can be used.  
  
```sql    
IF OBJECT_ID(N'dbo.ProcTestDefaults', N'P')IS NOT NULL  
   DROP PROCEDURE dbo.ProcTestDefaults;  
GO  
-- Create the stored procedure.  
CREATE PROCEDURE dbo.ProcTestDefaults (  
@p1 SMALLINT = 42,   
@p2 CHAR(1),   
@p3 VARCHAR(8) = 'CAR')  
AS   
   SET NOCOUNT ON;  
   SELECT @p1, @p2, @p3  
;  
GO  
```  
  
 The `Proc_Test_Defaults` stored procedure can be executed in many combinations.  
  
```sql    
-- Specifying a value only for one parameter (@p2).  
EXECUTE dbo.ProcTestDefaults @p2 = 'A';  
-- Specifying a value for the first two parameters.  
EXECUTE dbo.ProcTestDefaults 68, 'B';  
-- Specifying a value for all three parameters.  
EXECUTE dbo.ProcTestDefaults 68, 'C', 'House';  
-- Using the DEFAULT keyword for the first parameter.  
EXECUTE dbo.ProcTestDefaults @p1 = DEFAULT, @p2 = 'D';  
-- Specifying the parameters in an order different from the order defined in the procedure.  
EXECUTE dbo.ProcTestDefaults DEFAULT, @p3 = 'Local', @p2 = 'E';  
-- Using the DEFAULT keyword for the first and third parameters.  
EXECUTE dbo.ProcTestDefaults DEFAULT, 'H', DEFAULT;  
EXECUTE dbo.ProcTestDefaults DEFAULT, 'I', @p3 = DEFAULT;  
  
```  
  
### G. Using EXECUTE with AT linked_server_name  
 The following example passes a command string to a remote server. It creates a linked server `SeattleSales` that points to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and executes a DDL statement (`CREATE TABLE`) against that linked server.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
```sql    
EXEC sp_addlinkedserver 'SeattleSales', 'SQL Server'  
GO  
EXECUTE ( 'CREATE TABLE AdventureWorks2012.dbo.SalesTbl   
(SalesID int, SalesName varchar(10)) ; ' ) AT SeattleSales;  
GO  
```  
  
### H. Using EXECUTE WITH RECOMPILE  
 The following example executes the `Proc_Test_Defaults` stored procedure and forces a new query plan to be compiled, used, and discarded after the module is executed.  
  
```sql    
EXECUTE dbo.Proc_Test_Defaults @p2 = 'A' WITH RECOMPILE;  
GO  
```  
  
### I. Using EXECUTE with a user-defined function  
 The following example executes the `ufnGetSalesOrderStatusText` scalar user-defined function in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. It uses the variable `@returnstatus` to store the value returned by the function. The function expects one input parameter, `@Status`. This is defined as a **tinyint** data type.  
  
```sql    
DECLARE @returnstatus NVARCHAR(15);  
SET @returnstatus = NULL;  
EXEC @returnstatus = dbo.ufnGetSalesOrderStatusText @Status = 2;  
PRINT @returnstatus;  
GO  
```  
  
### J. Using EXECUTE to query an Oracle database on a linked server  
 The following example executes several `SELECT` statements at the remote Oracle server. The example begins by adding the Oracle server as a linked server and creating linked server login.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
```sql    
-- Setup the linked server.  
EXEC sp_addlinkedserver    
        @server='ORACLE',  
        @srvproduct='Oracle',  
        @provider='OraOLEDB.Oracle',   
        @datasrc='ORACLE10';  
  
EXEC sp_addlinkedsrvlogin   
    @rmtsrvname='ORACLE',  
    @useself='false',   
    @locallogin=null,   
    @rmtuser='scott',   
    @rmtpassword='tiger';  
  
EXEC sp_serveroption 'ORACLE', 'rpc out', true;  
GO  
  
-- Execute several statements on the linked Oracle server.  
EXEC ( 'SELECT * FROM scott.emp') AT ORACLE;  
GO  
EXEC ( 'SELECT * FROM scott.emp WHERE MGR = ?', 7902) AT ORACLE;  
GO  
DECLARE @v INT;   
SET @v = 7902;  
EXEC ( 'SELECT * FROM scott.emp WHERE MGR = ?', @v) AT ORACLE;  
GO   
```  
  
### K. Using EXECUTE AS USER to switch context to another user  
 The following example executes a [!INCLUDE[tsql](../../includes/tsql-md.md)] string that creates a table and specifies the `AS USER` clause to switch the execution context of the statement from the caller to `User1`. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will check the permissions of `User1` when the statement is run. `User1` must exist as a user in the database and must have permission to create tables in the `Sales` schema, or the statement fails.  
  
```sql    
EXECUTE ('CREATE TABLE Sales.SalesTable (SalesID INT, SalesName VARCHAR(10));')  
AS USER = 'User1';  
GO  
```  
  
### L. Using a parameter with EXECUTE and AT linked_server_name  
 The following example passes a command string to a remote server by using a question mark (`?`) placeholder for a parameter. The example creates a linked server `SeattleSales` that points to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and executes a `SELECT` statement against that linked server. The `SELECT` statement uses the question mark as a place holder for the `ProductID` parameter (`952`), which is provided after the statement.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
```sql    
-- Setup the linked server.  
EXEC sp_addlinkedserver 'SeattleSales', 'SQL Server'  
GO  
-- Execute the SELECT statement.  
EXECUTE ('SELECT ProductID, Name   
    FROM AdventureWorks2012.Production.Product  
    WHERE ProductID = ? ', 952) AT SeattleSales;  
GO  
```  
  
### M. Using EXECUTE to redefine a single result set  
 Some of the previous examples executed `EXEC dbo.uspGetEmployeeManagers 6;` which returned 7 columns. The following example demonstrates using the `WITH RESULT SET` syntax to change the names and data types of the returning result set.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]
  
```sql    
EXEC uspGetEmployeeManagers 16  
WITH RESULT SETS  
(   
   ([Reporting Level] INT NOT NULL,  
    [ID of Employee] INT NOT NULL,  
    [Employee First Name] NVARCHAR(50) NOT NULL,  
    [Employee Last Name] NVARCHAR(50) NOT NULL,  
    [Employee ID of Manager] NVARCHAR(max) NOT NULL,  
    [Manager First Name] NVARCHAR(50) NOT NULL,  
    [Manager Last Name] NVARCHAR(50) NOT NULL )  
);  
  
```  
  
### N. Using EXECUTE to redefine a two result sets  
 When executing a statement that returns more than one result set, define each expected result set. The following example in [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] creates a procedure that returns two result sets. Then the procedure is executed using the **WITH RESULT SETS** clause, and specifying two result set definitions.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]
  
```sql    
--Create the procedure  
CREATE PROC Production.ProductList @ProdName NVARCHAR(50)  
AS  
-- First result set  
SELECT ProductID, Name, ListPrice  
    FROM Production.Product  
    WHERE Name LIKE @ProdName;  
-- Second result set   
SELECT Name, COUNT(S.ProductID) AS NumberOfOrders  
    FROM Production.Product AS P  
    JOIN Sales.SalesOrderDetail AS S  
        ON P.ProductID  = S.ProductID   
    WHERE Name LIKE @ProdName  
    GROUP BY Name;  
GO  
  
-- Execute the procedure   
EXEC Production.ProductList '%tire%'  
WITH RESULT SETS   
(  
    (ProductID INT,   -- first result set definition starts here  
    Name NAME,  
    ListPrice MONEY)  
    ,                 -- comma separates result set definitions  
    (Name NAME,       -- second result set definition starts here  
    NumberOfOrders INT)  
);  
  
```  
  ### O. Using EXECUTE with AT DATA_SOURCE data_source_name to query a remote SQL Server 
  
 The following example passes a command string to an external data source pointing to a SQL Server instance. 
  
**Applies to**: [!INCLUDE[sssql19](../../includes/sssql19-md.md)] and later
  
```sql    
EXECUTE ( 'SELECT @@SERVERNAME' ) AT DATA_SOURCE my_sql_server;  
GO  
```  
  
### P. Using EXECUTE with AT DATA_SOURCE data_source_name to query compute pool in SQL Server Big Data Cluster 

 The following example passes a command string to an external data source pointing to a compute pool in SQL Server Big Data Cluster. The example creates a data source `SqlComputePool` against a compute pool in SQL Server Big Data Cluster and executes a `SELECT` statement against the data source. 
  
**Applies to**: [!INCLUDE[sssql19](../../includes/sssql19-md.md)] and later
  
```sql  
CREATE EXTERNAL DATA SOURCE SqlComputePool 
WITH (LOCATION = 'sqlcomputepool://controller-svc/default');
EXECUTE ( 'SELECT @@SERVERNAME' ) AT DATA_SOURCE SqlComputePool;  
GO  
```  

### Q. Using EXECUTE with AT DATA_SOURCE data_source_name to query data pool in SQL Server Big Data Cluster 
 The following example passes a command string to an external data source pointing to compute pool in SQL Server big data cluster. The example creates a data source `SqlDataPool` against a data pool in SQL Server big data cluster and executes a `SELECT` statement against the data source. 
  
**Applies to**: [!INCLUDE[sssql19](../../includes/sssql19-md.md)] and later
  
```sql  
CREATE EXTERNAL DATA SOURCE SqlDataPool 
WITH (LOCATION = 'sqldatapool://controller-svc/default');
EXECUTE ( 'SELECT @@SERVERNAME' ) AT DATA_SOURCE SqlDataPool;  
GO  
```

### R. Using EXECUTE with AT DATA_SOURCE data_source_name to query storage pool in SQL Server Big Data Cluster 

 The following example passes a command string to an external data source pointing to compute pool in SQL Server Big Data Cluster. The example creates a data source `SqlStoragePool` against a data pool in SQL Server Big Data Cluster and executes a `SELECT` statement against the data source. 
  
**Applies to**: [!INCLUDE[sssql19](../../includes/sssql19-md.md)] and later
  
```sql  
CREATE EXTERNAL DATA SOURCE SqlStoragePool
WITH (LOCATION = 'sqlhdfs://controller-svc/default');
EXECUTE ( 'SELECT @@SERVERNAME' ) AT DATA_SOURCE SqlStoragePool;  
GO  
```

  
## Examples: Azure Synapse Analytics 
  
### A: Basic Procedure Execution  
 Executing a stored procedure:  
  
```sql  
EXEC proc1;  
```  
  
 Calling a stored procedure with name determined at runtime:  
  
```sql    
EXEC ('EXEC ' + @var);  
```  
  
 Calling a stored procedure from within a stored procedure:  
  
```sql   
CREATE sp_first AS EXEC sp_second; EXEC sp_third;  
```  
  
### B: Executing Strings  
 Executing a SQL string:  
  
```sql   
EXEC ('SELECT * FROM sys.types');  
```  
  
 Executing a nested string:  
  
```sql  
EXEC ('EXEC (''SELECT * FROM sys.types'')');  
```  
  
 Executing a string variable:  
  
```sql  
DECLARE @stringVar NVARCHAR(100);  
SET @stringVar = N'SELECT name FROM' + ' sys.sql_logins';  
EXEC (@stringVar);  
```  
  
### C: Procedures with Parameters  

 The following example creates a procedure with parameters and demonstrates 3 ways to execute the procedure:  
  
```sql  
-- Uses AdventureWorks  
  
CREATE PROC ProcWithParameters  
    @name NVARCHAR(50),  
@color NVARCHAR(15)  
AS   
SELECT ProductKey, EnglishProductName, Color FROM [dbo].[DimProduct]  
WHERE EnglishProductName LIKE @name  
AND Color = @color;  
GO  
  
-- Executing using positional parameters  
EXEC ProcWithParameters N'%arm%', N'Black';  
-- Executing using named parameters in order  
EXEC ProcWithParameters @name = N'%arm%', @color = N'Black';  
-- Executing using named parameters out of order  
EXEC ProcWithParameters @color = N'Black', @name = N'%arm%';  
GO  
```  
  
## See Also  
 [@@NESTLEVEL &#40;Transact-SQL&#41;](../../t-sql/functions/nestlevel-transact-sql.md)   
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md)   
 [osql Utility](../../tools/osql-utility.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [REVERT &#40;Transact-SQL&#41;](../../t-sql/statements/revert-transact-sql.md)   
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)   
 [SUSER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/suser-name-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [USER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/user-name-transact-sql.md)   
 [OPENDATASOURCE &#40;Transact-SQL&#41;](../../t-sql/functions/opendatasource-transact-sql.md)   
 [Scalar User-Defined Functions for In-Memory OLTP](../../relational-databases/in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md)  
  
  
