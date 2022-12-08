---
description: "sp_cursoropen (Transact-SQL)"
title: "sp_cursoropen (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cursoropen"
  - "sp_cursoropen_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cursoropen"
ms.assetid: 16462ede-4393-4293-a598-ca88c48ca70b
author: markingmyname
ms.author: maghan
---
# sp_cursoropen (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Opens a cursor. sp_cursoropen defines the SQL statement associated with the cursor and cursor options, and then populates the cursor. sp_cursoropen is equivalent to the combination of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements DECLARE_CURSOR and OPEN. This procedure is invoked by specifying ID =2 in a tabular data stream (TDS) packet.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_cursoropen cursor OUTPUT, stmt  
    [, scrollopt[ OUTPUT ] [ , ccopt[ OUTPUT ]  
    [ ,rowcount OUTPUT [ ,boundparam][,...n]]] ]]  
```  
  
## Arguments  
 *cursor*  
 A SQL Server-generated cursor identifier. *cursor* is a *handle* value that must be supplied on all subsequent procedures involving the cursor, such as sp_cursorfetch. *cursor* is a required parameter with an **int** return value.  
  
 *cursor* allows multiple cursors to be active on a single database connection.  
  
 *stmt*  
 Is a required parameter that defines the cursor result set. Any valid query string (syntax and binding) of any string type (regardless of Unicode, size, etc.) can serve as a valid *stmt* value type.  
  
 *scrollopt*  
 Scroll option. *scrollopt* is an optional parameter that requires one of the following **int** input values.  
  
|Value|Description|  
|-----------|-----------------|  
|0x0001|KEYSET|  
|0x0002|DYNAMIC|  
|0x0004|FORWARD_ONLY|  
|0x0008|STATIC|  
|0x10|FAST_FORWARD|  
|0x1000|PARAMETERIZED_STMT|  
|0x2000|AUTO_FETCH|  
|0x4000|AUTO_CLOSE|  
|0x8000|CHECK_ACCEPTED_TYPES|  
|0x10000|KEYSET_ACCEPTABLE|  
|0x20000|DYNAMIC_ACCEPTABLE|  
|0x40000|FORWARD_ONLY_ACCEPTABLE|  
|0x80000|STATIC_ACCEPTABLE|  
|0x100000|FAST_FORWARD_ACCEPTABLE|  
  
 Because of the possibility that the requested value is not appropriate for the cursor defined by *stmt*, this parameter serves as both input and output. In such cases, SQL Server assigns an appropriate value.  
  
 *ccopt*  
 Concurrency control option. *ccopt* is an optional parameter that requires one of the following **int** input values.  
  
|Value|Description|  
|-----------|-----------------|  
|0x0001|READ_ONLY|  
|0x0002|SCROLL_LOCKS (previously known as LOCKCC)|  
|0x0004|**OPTIMISTIC** (previously known as OPTCC)|  
|0x0008|OPTIMISTIC (previously known as OPTCCVAL)|  
|0x2000|ALLOW_DIRECT|  
|0x4000|UPDT_IN_PLACE|  
|0x8000|CHECK_ACCEPTED_OPTS|  
|0x10000|READ_ONLY_ACCEPTABLE|  
|0x20000|SCROLL_LOCKS_ACCEPTABLE|  
|0x40000|OPTIMISTIC_ACCEPTABLE|  
|0x80000|OPTIMISITC_ACCEPTABLE|  
  
 As with *scrollopt*, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can override the requested *ccopt* values.  
  
 *rowcount*  
 The number of fetch buffer rows to use with AUTO_FETCH. The default is 20 rows. *rowcount* behaves differently when assigned as an input value versus a return value.  
  
|As input value|As return value|  
|--------------------|---------------------|  
|When the AUTO_FETCH *scrollopt* value is specified *rowcount* represents the number of rows to place into the fetch buffer.<br /><br /> Note: >0 is a valid value when AUTO_FETCH is specified, but is otherwise ignored.|Represents the number of rows in the result set, except when the *scrollopt* AUTO_FETCH value is specified.|  
  
-  
  
 *boundparam*  
 Signifies the use of additional parameters. *boundparam* is an optional parameter that should be specified if the *scrollopt* PARAMETERIZED_STMT value is set to ON.  
  
## Return Code Values  
 If no error is raised, sp_cursoropen returns one of the following values.  
  
 0  
 The procedure executed successfully.  
  
 0x0001  
 An error occurred during the execution (a minor error, not severe enough to raise an error in the operation).  
  
 0x0002  
 An asynchronous operation is in progress.  
  
 0x0002  
 A FETCH operation is in process.  
  
 A  
 This cursor has been deallocated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is unavailable.  
  
 When an error is raised, the return values may be inconsistent and the accuracy cannot be guaranteed.  
  
 When the *rowcount* parameter is specified as a return value, the following result set occurs.  
  
 -1  
 Returned if the number of rows is unknown or not applicable.  
  
 -n  
 Returned when an asynchronous population is in effect. Represents the number of rows that were placed into the fetch buffer when the *scrollopt* AUTO_FETCH value is specified.  
  
 If RPC is in use, the return values are as follows.  
  
 0  
 Procedure is successful.  
  
 1  
 Procedure failed.  
  
 2  
 A keyset cursor is being asynchronously generated.  
  
 16  
 A fast-forward cursor has been automatically closed.  
  
> [!NOTE]  
>  If the sp_cursoropen procedure executes successfully, the RPC return parameters and a result set with TDS column format information (0xa0 & 0xa1 messages) are sent. If unsuccessful, one or more TDS error messages are sent. In either case, no row data will be returned and the *done* message count will be zero. If you are using a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than 7.0, 0xa0, 0xa1 (standard for SELECT statements) are returned along with 0xa5 and 0xa4 token streams. If you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0, 0x81 is returned (standard for SELECT statements) along with the 0xa5 and 0xa4 token streams.  
  
## Remarks  
  
## stmt Parameter  
 If *stmt* specifies the execution of a stored procedure, the input parameters may either be defined as constants as part of the *stmt* string, or specified as *boundparam* arguments. Declared variables can be passed as bound parameters in this way.  
  
 The allowed contents of the *stmt* parameter depend upon whether or not the *ccopt* ALLOW_DIRECT return value has been linked by OR to the rest of the *ccopt* values, i.e.:  
  
-   If ALLOW_DIRECT is not specified, either a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT or EXECUTE statement calling for a stored procedure containing a single SELECT statement must be used. Furthermore, the SELECT statement must qualify as a cursor; that is, it cannot contain the keywords SELECT INTO or FOR BROWSE.  
  
-   If ALLOW_DIRECT is specified, this may result in one or more [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, including those that, in turn, execute other stored procedures with multiple statements. Non-SELECT statements or any SELECT statement that contains the keywords SELECT INTO or FOR BROWSE will simply be executed and will not result in the creation of a cursor. The same is true for any SELECT statement included in a batch of multiple statements. In cases where a SELECT statement contains clauses that pertain only to cursors, those clauses are ignored. For instance, when the value of *ccopt* is 0x2002, this is a request for:  
  
    -   A cursor with scroll locks, if there is only a single SELECT statement that qualifies as a cursor, or  
  
    -   A direct statement execution if there are multiple statements, a single non-SELECT statement, or a SELECT statement that does not qualify as a cursor.  
  
## scrollopt Parameter  
 The first five *scrollopt* values (KEYSEY, DYNAMIC, FORWARD_ONLY, STATIC, and FAST_FORWARD) are mutually exclusive.  
  
 PARAMETERIZED_STMT and CHECK_ACCEPTED_TYPES can be linked by OR to any of the first five values.  
  
 AUTO_FETCH and AUTO_CLOSE can be linked by OR to FAST_FORWARD.  
  
 If CHECK_ACCEPTED_TYPES is ON, at least one of the last five *scrollopt* values (KEYSET_ACCEPTABLE`,` DYNAMIC_ACCEPTABLE, FORWARD_ONLY_ACCEPTABLE, STATIC_ACCEPTABLE, or FAST_FORWARD_ACCEPTABLE) must also be ON.  
  
 STATIC cursors are always opened as READ_ONLY. This means that the underlying table cannot be updated through this cursor.  
  
## ccopt Parameter  
 The first four *ccopt* values (READ_ONLY, SCROLL_LOCKS, and both OPTIMISTIC values) are mutually exclusive.  
  
> [!NOTE]  
>  Choosing one of the first four *ccopt* values dictates whether the cursor is read-only, or if locking or optimistic methods are used to prevent lost updates. If a *ccopt* value is not specified, the default value is OPTIMISTIC.  
  
 ALLOW_DIRECT and CHECK_ACCEPTED_TYPES can be linked by OR to any of the first four values.  
  
 UPDT_IN_PLACE can be linked by OR to READ_ONLY, SCROLL_LOCKS, or either of the OPTIMISTIC values.  
  
 If CHECK_ACCEPTED_TYPES is ON, at least one of the last four *ccopt* values (READ_ONLY_ACCEPTABLE, SCROLL_LOCKS_ACCEPTABLE, and either of the OPTIMISTIC_ACCEPTABLE values) must also be ON.  
  
 Positioned UPDATE and DELETE functions may be performed only within the fetch buffer and only if the *ccopt* value equals SCROLL_LOCKS or OPTIMISTIC. If SCROLL_LOCKS is the specified value, the operation is guaranteed to succeed. If OPTIMISTIC is the specified value, the operation will fail if the row has changed since it was last fetched.  
  
 The reason for this failure is that when OPTIMISTIC is the specified value, an optimistic currency control function is performed by comparing timestamps or checksum values, as determined by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If any of these rows do not match, the operation will fail.  
  
 Specifying UPDT_IN_PLACE as the return value governs the following results:  
  
-   If not set when performing a positioned update on a table with a unique index, the cursor deletes the row from its work table and inserts it at the end of any of the key columns used by the cursor, thereby changing those columns.  
  
-   If set ON, the cursor will simply update the key columns in the work table's original row.  
  
## bound_param Parameter  
 The parameter name should be *paramdef* when PARAMETERIZED_STMT is specified, according to the error message in the code. When PARAMETERIZED_STMT is not specified, no name is specified in the error message.  
  
## RPC Considerations  
 The RPC RETURN_METADATA input flag can be set to 0x0001 to request that cursor select list metadata be returned in the TDS stream.  
  
## Examples  
  
### bound_param Parameter  
 Any parameters after the fifth are passed along to the statement plan as input parameters. The first such parameter must be a string in the form of:  
  
 *{ local variable name data type } [,...n]*  
  
 Subsequent parameters are used to pass the values to be substituted for the *local variable name* in the statement.  
  
## See Also  
 [sp_cursorfetch &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorfetch-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
