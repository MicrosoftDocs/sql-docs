---
title: "View Filter Information (Transact-SQL)"
description: "View Filter Information (Transact-SQL)"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/06/2017"
ms.service: sql
ms.topic: language-reference
helpviewer_keywords:
  - "displaying filter information"
  - "filters [SQL Server], viewing"
  - "filters [SQL Server], traces"
  - "traces [SQL Server], filters"
  - "viewing filter information"
---
# View Filter Information (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to use built-in functions to view trace filter information.  
  
### To view filter information  
  
1.  Execute **fn_trace_getfilterinfo** by specifying the ID of the trace about which filter information is needed. This function returns a table that lists the filters, the columns on which the filters are applied, and the values on which the filter is applied.  
  
     Invoke the function this way:  
  
    ```  
    SELECT *  
    FROM ::fn_trace_getfilterinfo(trace_id)  
    ```  
  
## Related content

- [sys.fn_trace_getfilterinfo (Transact-SQL)](../system-functions/sys-fn-trace-getfilterinfo-transact-sql.md)
- [System stored procedures (Transact-SQL)](../system-stored-procedures/system-stored-procedures-transact-sql.md)
- [SQL Server Profiler stored procedures (Transact-SQL)](../system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)
