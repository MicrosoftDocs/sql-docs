---
title: "MSSQLSERVER_2814 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "2814 (Database Engine error)"
ms.assetid: 22800748-9be9-4511-9428-6b8b40e5bef9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2814
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2814|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PR_POSSIBLE_INFINITE_RECOMPILE|  
|Message Text|A possible infinite recompile was detected for SQLHANDLE %hs, PlanHandle %hs, starting offset %d, ending offset %d. The last recompile reason was %d.|  
  
## Explanation  
 One or more statements caused the query batch to recompile at least 50 times. The specified statement should be corrected to avoid further recompilations.  
  
 The following table lists the reasons for recompilation.  
  
|Reason code|Description|  
|-----------------|-----------------|  
|1|Schema changed|  
|2|Statistics changed|  
|3|Deferred compile|  
|4|Set option changed|  
|5|Temp table changed|  
|6|Remote rowset changed|  
|7|For Browse permissions changed|  
|8|Query notification environment changed|  
|9|Partition view changed|  
|10|Cursor options changed|  
|11|Option (recompile) requested|  
  
## User Action  
  
1.  View the statement causing the recompilation by running the following query. Replace the *sql_handle*, *starting_offset*, *ending_offset*, and *plan_handle* placeholders with the values specified in the error message. Note that the **database_name** and **object_name** columns will be NULL for ad hoc and prepared [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
     SELECT DB_NAME(st.dbid) AS database_name  
  
     , OBJECT_NAME(st.objectid) AS object_name  
  
     , st.text  
  
     FROM sys.dm_exec_query_stats AS qs  
  
     CROSS APPLY sys.dm_exec_sql_text (*sql_handle*) AS st  
  
     WHERE qs.statement_start_offset = *starting_offset*  
  
     AND qs.statement_end_offset = *ending_offset*  
  
     AND qs.plan_handle = *plan_handle*;  
  
2.  Based on the reason code description, modify the statement, batch, or procedure to avoid recompilations. For example, a stored procedure may contain one or more SET statements. These statements should be removed from the procedure. For additional examples of recompilation causes and resolutions, see [Batch Compilation, Recompilation, and Plan Caching Issues in SQL Server 2005](https://go.microsoft.com/fwlink/?LinkId=69175).  
  
3.  If the problem persists, contact Microsoft Customer Support Services.  
  
## See Also  
 [SQL:StmtRecompile Event Class](../event-classes/sql-stmtrecompile-event-class.md)  
  
  
