---
title: "LOCALDB_ERROR_INSUFFICIENT_BUFFER | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: ff67bda8-7e5c-4b06-8d7b-9985b6059a98
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# LOCALDB_ERROR_INSUFFICIENT_BUFFER
    
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
  
  