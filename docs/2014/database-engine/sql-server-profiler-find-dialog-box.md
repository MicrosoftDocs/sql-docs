---
title: "SQL Server Profiler - Find Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.pro.find.f1"
helpviewer_keywords: 
  - "Find dialog box"
ms.assetid: dfaeec04-93d3-4214-9fc1-38b80179b36b
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Profiler - Find Dialog Box
  Use the **Find** dialog box to search a trace for specific characters or words. To cancel a search in progress, press ESC.  
  
 To open this dialog box in [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)], on the **Edit** menu, click **Find**.  
  
## Options  
 **Find what**  
 Enter the text that you want to search for. The search matches any string containing the specified string. For example, searching for "Completed" matches "SQL:BatchCompleted." Wild card characters (*, ?, etc.) are not supported.  
  
 **Search in column**  
 Click a data column to search, or click **\<All columns>** to search all the data columns in the trace.  
  
 **Match case**  
 Finds text that has the same case as the **Find what** box. Clear this check box to find examples in the trace that are in both uppercase and lowercase text characters.  
  
 **Match whole word**  
 Restricts the search to entire words. Clear the **Match whole word** check box to search for characters within a word.  
  
 **Find Next**  
 Finds the next example of the characters in the **Find what** box.  
  
 **Find Previous**  
 Searches backwards in the trace, to find the previous example of the characters in the **Find what** box.  
  
## See Also  
 [Find a Value or Data Column While Tracing &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/find-a-value-or-data-column-while-tracing-sql-server-profiler.md)   
 [Create a Trace &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)   
 [Open a Trace Table &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/open-a-trace-table-sql-server-profiler.md)   
 [Open a Trace File &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/open-a-trace-file-sql-server-profiler.md)   
 [SQL Server Profiler Templates and Permissions](../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [SQL Server Profiler](../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
