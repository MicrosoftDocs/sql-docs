---
title: "Display the Number of Concurrency Slots Needed for a Waiting Request (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 74749862-4b8d-47d8-887d-d3b5107a29d7
caps.latest.revision: 3
author: BarbKess
---
# Display the Number of Concurrency Slots Needed for a Waiting Request (SQL Server PDW)
Describes how to figure out the number of concurrency slots are needed by a request that is waiting to run on SQL Server PDW.  
  
For more information, see [Workload Management &#40;SQL Server PDW&#41;](../sqlpdw/workload-management-sql-server-pdw.md)  
  
## Display the Number of Concurrency Slots Needed for a Waiting Request  
A request could be waiting too long without getting executed. One of the ways to troubleshoot this is to look at the number of concurrency slots the request needs.  The following example shows the number of concurrency slots needed by each waiting request.  
  
```  
--Display the number of concurrency slots required   
--for each request that is waiting to run.  
SELECT request_id, concurrency_slots_used AS [Slots Needed], resource_class AS [Resource Class]  
FROM sys.dm_pdw_resource_waits;  
```  
  
