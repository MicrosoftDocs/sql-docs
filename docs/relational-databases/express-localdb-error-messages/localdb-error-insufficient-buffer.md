---
title: "LOCALDB_ERROR_INSUFFICIENT_BUFFER | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: "reference"
ms.assetid: ff67bda8-7e5c-4b06-8d7b-9985b6059a98
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# LOCALDB_ERROR_INSUFFICIENT_BUFFER
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|276|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|The buffer passed to the Local Database instance API method has insufficient size.|  
  
## Explanation  
 The input buffer is too short, and truncation was not requested.  
  
## User Action  
 Provide a buffer of the specified size.  
  
  
