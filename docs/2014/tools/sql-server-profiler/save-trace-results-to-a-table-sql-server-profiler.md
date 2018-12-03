---
title: "Save Trace Results to a Table (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "saving traces"
  - "traces [SQL Server], saving"
ms.assetid: edbecf74-683b-4e43-a1ef-7a3d5f5e27f6
author: stevestein
ms.author: sstein
manager: craigg
---
# Save Trace Results to a Table (SQL Server Profiler)
  This topic describes how to save trace results to a database table by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To save trace results to a table  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The **Trace Properties**dialog box appears.  
  
    > [!NOTE]  
    >  If **Start tracing immediately after making connection**is selected, the **Trace Properties**dialog box fails to appear and the trace begins instead. To turn off this setting, on the **Tools**menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the **Trace name** box, type a name for the trace, and then click **Save to table**.  
  
3.  In the **Connect to server** dialog box, connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that will contain the trace table.  
  
4.  In the **Destination Table** dialog box, select a database from the **Database**list.  
  
5.  In the **Owner** list, select the owner for the trace.  
  
6.  In the **Table** list, type or select the table name for the trace results. Click **OK.**  
  
7.  In the **Trace Properties** dialog box, select the **Set maximum rows (in thousands)**check box to specify the maximum number of rows to save.  
  
## See Also  
 [SQL Server Profiler](sql-server-profiler.md)  
  
  
