---
title: "LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: "reference"
ms.assetid: c178a308-8d99-47fc-8a49-5a480dc592f6
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|260|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|The full path length of the Local Database instance folder is longer than MAX_PATH. The instance must be stored in folder: %%LOCALAPPDATA%%\Microsoft\Microsoft SQL Server Local DB\Instances\\<instance name\>.|  
  
## Explanation  
 The path where the instance should be stored is longer than MAX_PATH.  
  
## User Action  
 Create a new path that is shorter than MAX_PATH.  
  
  
