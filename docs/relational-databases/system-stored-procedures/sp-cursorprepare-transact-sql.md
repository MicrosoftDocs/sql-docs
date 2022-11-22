---
title: sp_cursorprepare (Transact-SQL)
description: "sp_cursorprepare (Transact-SQL)"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cursor_prepare_TSQL"
  - "sp_cursor_prepare"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cursor_prepare"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: "03/14/2017"
---

# sp_cursorprepare (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Compiles the cursor statement or batch into an execution plan, but does not create the cursor. The compiled statement can later be used by sp_cursorexecute. This procedure, coupled with sp_cursorexecute, has the same function as sp_cursoropen, but is split into two phases. sp_cursorprepare is invoked by specifying ID = 3 in a tabular data stream (TDS) packet.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```tsql
  
sp_cursorprepare prepared_handle OUTPUT, params , stmt , options  
    [ , scrollopt[ , ccopt]]  
```  
  
## Arguments  
 *prepared_handle*  
 A SQL Server-generated prepared *handle* identifier that returns an integer value.  
  
> [!NOTE]  
>  *prepared_handle* is subsequently supplied to a sp_cursorexecute procedure in order to open a cursor. Once a handle is created, it exists until you log off or until you explicitly remove it through a sp_cursorunprepare procedure.  
  
 *params*  
 Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value. Input a NULL value if the statement is not parameterized.  
  
> [!NOTE]  
>  Use an **ntext** string as the input value when *stmt* is parameterized and the *scrollopt* PARAMETERIZED_STMT value is ON.  
  
 *stmt*  
 Defines the cursor result set. The *stmt* parameter is required and calls for an **ntext**, **nchar** or **nvarchar** input value.  
  
> [!NOTE]  
>  The rules for specifying the *stmt* value are the same as those for sp_cursoropen, with the exception that the *stmt* string data type must be **ntext**.  
  
 *options*  
 An optional parameter that returns a description of the cursor result set columns. *options* requires the following **int** input value.  
  
|Value|Description|  
|-----------|-----------------|  
|0x0001|RETURN_METADATA|  
  
 *scrollopt*  
 Scroll Option. *scrollopt* is an optional parameter that requires one of the following **int** input values.  
  
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
  
 Because the requested value might not be appropriate for the cursor defined by *stmt*, this parameter serves as both input and output. In such cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns an appropriate value.  
  
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
  
 As with *scrollpt*, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can assign a different value from the one requested.  
  
## Remarks  
 The RPC status parameter is one of the following:  
  
|Value|Description|  
|-----------|-----------------|  
|0|Success|  
|0x0001|Failure|  
|1FF6|Could not return metadata.<br /><br /> Note: The reason for this is that the statement does not produce a result set; for example, it is an INSERT or DDL statement.|  
  
## Examples  
  The following is an example of using sp_cursorprepare and sp_cursorexecute

```sql
declare @handle int , @p5 int, @p6 int
exec sp_cursorprepare @handle OUTPUT, 
	N'@dbid int', 
	N'select * from sys.databases where database_id < @dbid',
	1,
	@p5 output,
	@p6 output


declare @p1 int  
set @P1 = @handle 
declare @p2 int   
declare @p3 int  
declare @p4 int  
set @P6 = 4 
exec sp_cursorexecute @p1, @p2 OUTPUT, @p3 output , @p4 output, @p5 OUTPUT, @p6

exec sp_cursorfetch @P2

exec sp_cursorunprepare @handle
exec sp_cursorclose @p2
```
 
 When *stmt* is parameterized and the *scrollopt* PARAMETERIZED_STMT value is ON, the format of the string is as follows:  
  
 { *\<local variable name>**\<data type>* } [ ,...*n* ]  
  
## See Also  
 [sp_cursorexecute &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorexecute-transact-sql.md)   
 [sp_cursoropen &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursoropen-transact-sql.md)   
 [sp_cursorunprepare &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorunprepare-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
