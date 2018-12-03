---
title: "XTP Phantom Processor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: 0f691b3d-a8fd-4459-ad21-2cfc8574a8c0
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# XTP Phantom Processor
  The XTP Phantom Processor performance object contains counters related to the XTP engine's phantom processing subsystem. This component is responsible for detecting phantom rows in transactions running at the SERIALIZABLE isolation level.  
  
 This table describes the **XTP Phantom Processor** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Dusty corner scan retries/sec (Phantom-issued)**|The number of scan retries due to write conflicts during dusty corner sweeps issued by the phantom processor (on average), per second. This is a very low-level counter, not intended for customer use.|  
|**Phantom expired rows removed/sec**|The number of expired rows removed by phantom scans (on average), per second.|  
|**Phantom expired rows touched/sec**|The number of expired rows touched by phantom scans (on average), per second.|  
|**Phantom expiring rows touched/sec**|The number of expiring rows touched by phantom scans (on average), per second.|  
|**Phantom rows touched/sec**|The number of rows touched by phantom scans (on average), per second.|  
|**Phantom scans started/sec**|The number of phantom scans started (on average), per second.|  
  
## See Also  
 [XTP &#40;In-Memory OLTP&#41; Performance Counters](../../integration-services/performance/performance-counters.md)  
  
  
