---
description: "PreConnect:Completed Event Class"
title: "PreConnect:Completed Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "PreConnect:Completed Event Class"
ms.assetid: 7ed2f620-6511-4985-9961-d2927c2b1759
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PreConnect:Completed Event Class
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The PreConnect:Completedevent class indicates when a LOGON trigger or the Resource Governor classifier function finishes execution.  
  
## PreConnect:Completed Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|EventClass|**int**|216|27|No|  
|SPID|**int**|The ID of server process that fires this event.|12|Yes|  
|EventSubClass|**int**|1 for the user-defined classifier function.|21|Yes|  
|StartTime|**datetime**|The time when the user-defined classifier function starts.|14|Yes|  
|EndTime|**datetime**|The time when the user-defined classifier function starts.|15|Yes|  
|Duration|**bigint**|The amount of time, in microseconds, used by the classifier function.|13|Yes|  
|ObjectID|**int**|The ID of the user-defined classifier object.|22|Yes|  
|CPU|**int**|CPU usage in milliseconds.|18|Yes|  
|Reads|**int**|The number of logical reads.|16|Yes|  
|Writes|**int**|The number of logical writes.|17|Yes|  
|GroupID|**int**|The ID of the classified workload group.|66|Yes|  
|Error|**int**|The last error number if the user-defined classifier function fails to execute.|31|Yes|  
|State|**int**|The state of the last error.|30|Yes|  
|TargetUserName|**sysname**|The return value (workload group name) for the user-defined classifier function if the system can not find a corresponding active group. Otherwise, this column is set to NULL.|39|Yes|  
|ObjectName|**nvarchar(256)**|The two-part name of the classifier user-defined function. For example, dbo.classifier.|34|Yes|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [PreConnect:Starting Event Class](../../relational-databases/event-classes/preconnect-starting-event-class.md)   
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)  
  
  
