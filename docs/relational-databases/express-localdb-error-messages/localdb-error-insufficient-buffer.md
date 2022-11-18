---
description: "LOCALDB_ERROR_INSUFFICIENT_BUFFER"
title: "LOCALDB_ERROR_INSUFFICIENT_BUFFER | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: "reference"
ms.assetid: ff67bda8-7e5c-4b06-8d7b-9985b6059a98
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# LOCALDB_ERROR_INSUFFICIENT_BUFFER
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
## Details  
  
| Attribute | Value |
| --------- | ----- |
|Product Name|SQL Server|  
|Event ID|276|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|The buffer passed to the Local Database instance API method has insufficient size.|  
  
## Explanation  
 The input buffer is too short, and truncation was not requested.  
  
## User Action  
 Provide a buffer of the specified size.  
  
  
