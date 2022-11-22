---
description: "MSSQLSERVER_2814"
title: "MSSQLSERVER_2814 | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "2814 (Database Engine error)"
ms.assetid: 22800748-9be9-4511-9428-6b8b40e5bef9
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_2814
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
|---------------|---------------|  
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
  
1.  View the statement causing the recompilation by running the following query. Replace the *sql_handle*, *starting_offset*, *ending_offset*, and *plan_handle* placeholders with the values specified in the error message. The **database_name** and **object_name** columns are NULL for ad hoc and prepared [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
    ```sql   
    SELECT DB_NAME(st.dbid) AS database_name,  
        OBJECT_NAME(st.objectid) AS object_name,  
        st.text  
    FROM sys.dm_exec_query_stats AS qs  
    CROSS APPLY sys.dm_exec_sql_text (0x01000600B74C2A1300D2582A2100000000000000000000000000000000000000000000000000000000000000 /* replace the 0x01000600B... value with the actual sql_handle*/) AS st  
    WHERE qs.statement_start_offset = 123 /*replace 123 with actual starting_offset value*/  
    AND qs.statement_end_offset = 456 /*replace 456 with actual ending_offset value*/
    AND qs.plan_handle = 0x06000100A27E7C1FA821B10600 /*replace 0x06000100A27E7C1FA821B10600with actual plan_handle value*/;
    ```
  
2.  Based on the reason code description, modify the statement, batch, or procedure to avoid recompilations. For example, a stored procedure may contain one or more SET statements. These statements should be removed from the procedure. For additional examples of recompilation causes and resolutions, see [Batch Compilation, Recompilation, and Plan Caching Issues in SQL Server 2005](/previous-versions/sql/sql-server-2005/administrator/cc966425(v=technet.10)).  
  
3.  If the problem persists, contact Microsoft Customer Support Services.  
  
## See Also  
[SQL:StmtRecompile Event Class](../event-classes/sql-stmtrecompile-event-class.md)  
  
