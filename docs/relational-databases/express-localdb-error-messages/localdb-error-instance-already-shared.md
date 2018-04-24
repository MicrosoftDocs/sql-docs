---
title: "LOCALDB_ERROR_INSTANCE_ALREADY_SHARED | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "localdb"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 

ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 35b4d6fa-ebb9-49d3-aaab-d4e37b6f3760
caps.latest.revision: 7
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
ms.workload: "Inactive"
---
# LOCALDB_ERROR_INSTANCE_ALREADY_SHARED
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|284|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|The specified Local Database Instance is already shared.|  
  
## Explanation  
 The specified Local Database Instance is already shared with a different shared name.  
  
## User Action  
 Unshare the shared instance before sharing it again with a different shared name.  
  
  
