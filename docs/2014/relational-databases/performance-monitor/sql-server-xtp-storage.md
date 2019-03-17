---
title: "XTP Storage | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: 4070580b-880d-4f4c-abcc-626a4fe0c9a2
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# XTP Storage
  The XTP Storage performance object contains counters related to XTP storage in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 This table describes the **XTP Storage** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Checkpoints Closed**|Count of checkpoints closed done by online agent.|  
|**Checkpoints Completed**|Count of checkpoints processed by offline checkpoint thread.|  
|**Core Merges Completed**|The number of core merges completed by the merge worker thread. These merges still need to be installed.|  
|**Merge Policy Evaluations**|The number of merge policy evaluations since the server started.|  
|**Merge Requests Outstanding**|The number of merge requests outstanding since the server started.|  
|**Merges Abandoned**|The number of merges abandoned due to failure.|  
|**Merges Installed**|The number of merges successfully installed.|  
|**Total Files Merged**|The total number of source files merged. This count can be used to find the average number of source files in the merge.|  
  
## See Also  
 [XTP &#40;In-Memory OLTP&#41; Performance Counters](../../integration-services/performance/performance-counters.md)  
  
  
