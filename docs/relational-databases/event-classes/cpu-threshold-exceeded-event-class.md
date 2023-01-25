---
description: "CPU Threshold Exceeded Event Class"
title: "CPU Threshold Exceeded Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "CPU Threshold Exceeded Event Class"
ms.assetid: eb106f7d-baa3-4a2b-96b2-f9fe0844057d
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CPU Threshold Exceeded Event Class
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The CPU Threshold Exceeded event class indicates that Resource Governor detects a query that exceeds the CPU threshold specified for REQUEST_MAX_CPU_TIME_SEC.  
  
> [!NOTE]  
>  The detection interval for this event is five seconds. It is guaranteed that an event will be generated if a query exceeds the specified limit by at least five seconds. However, if a query exceeds the specified threshold by less than five seconds, its detection might be missed depending on the timing of the query and the time of last detection sweep.  
  
## CPU Threshold Exceeded Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|CPU|**int**|CPU usage in milliseconds.|18|Yes|  
|EventClass|**int**|214|27|No|  
|EventSubClass|**int**|CPU limit violation.|21|Yes|  
|GroupID|**int**|Group ID where the violation occurred.|66|Yes|  
|OwnerID|**int**|SPID of the process that caused the violation.|58|Yes|  
|SPID|**int**|ID of the server process that fires this event.<br /><br /> Note: This can differ from the actual user SPID if a system thread validates CPU usage as a background task.|12|Yes|  
|StartTime|**datetime**|The time when this event fired.|14|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
