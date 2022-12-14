---
description: "sys.fn_trace_gettable (Transact-SQL)"
title: "sys.fn_trace_gettable (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "fn_trace_gettable"
  - "fn_trace_gettable_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_trace_gettable function"
  - "sys.fn_trace_gettable function"
ms.assetid: c2590159-6ec5-4510-81ab-e935cc4216cd
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_trace_gettable (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the content of one or more trace files in tabular form.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_trace_gettable ( 'filename' , number_files )  
```  
  
## Arguments  
 '*filename*'  
 Specifies the initial trace file to be read. *filename* is **nvarchar(256)**, with no default.  
  
 *number_files*  
 Specifies the number of rollover files to be read. This number includes the initial file specified in *filename*. *number_files* is an **int**.  
  
## Remarks  
 If *number_files* is specified as **default**, **fn_trace_gettable** reads all rollover files until it reaches the end of the trace. **fn_trace_gettable** returns a table with all the columns valid for the specified trace. For more information, see [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md).  
  
 Be aware that the fn_trace_gettable function will not load rollover files (when this option is specified by using the *number_files* argument) where the original trace file name ends with an underscore and a numeric value. (This does not apply to the underscore and number that are automatically appended when a file rolls over.) As a workaround, you can rename the trace files to remove the underscores in the original file name. For example, if the original file is named **Trace_Oct_5.trc** and the rollover file is named **Trace_Oct_5_1.trc**, you can rename the files to **TraceOct5.trc** and **TraceOct5_1.trc**.  
  
 This function can read a trace that is still active on the instance on which it is executed.  
  
## Permissions  
 Requires ALTER TRACE permission on the server.  
  
## Examples  
  
### A. Using fn_trace_gettable to import rows from a trace file  
 The following example calls `fn_trace_gettable` inside the `FROM` clause of a `SELECT...INTO` statement.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT * INTO temp_trc  
FROM fn_trace_gettable('c:\temp\mytrace.trc', default);  
GO  
```  
  
### B. Using fn_trace_gettable to return a table with an IDENTITY column that can be loaded into a SQL Server table  
 The following example calls the function as part of a `SELECT...INTO` statement and returns a table with an `IDENTITY` column that can be loaded into the table `temp_trc`.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT IDENTITY(int, 1, 1) AS RowNumber, * INTO temp_trc  
FROM fn_trace_gettable('c:\temp\mytrace.trc', default);  
GO  
```  
  
## See Also  
 [sp_trace_generateevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md)   
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql.md)  
  
  
