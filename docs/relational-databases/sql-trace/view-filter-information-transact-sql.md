---
title: "View Filter Information (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "displaying filter information"
  - "filters [SQL Server], viewing"
  - "filters [SQL Server], traces"
  - "traces [SQL Server], filters"
  - "viewing filter information"
ms.assetid: b7e52c13-8c83-47c2-8cd0-af7a49eceb5c
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# View Filter Information (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to use built-in functions to view trace filter information.  
  
### To view filter information  
  
1.  Execute **fn_trace_getfilterinfo** by specifying the ID of the trace about which filter information is needed. This function returns a table that lists the filters, the columns on which the filters are applied, and the values on which the filter is applied.  
  
     Invoke the function this way:  
  
    ```  
    SELECT *  
    FROM ::fn_trace_getfilterinfo(trace_id)  
    ```  
  
## See Also  
 [sys.fn_trace_getfilterinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getfilterinfo-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [SQL Server Profiler Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)  
  
  
