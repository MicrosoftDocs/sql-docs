---
title: "LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG"
description: "LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
## Details  
  
| Attribute | Value |
| --------- | ----- | 
|Product Name|SQL Server|  
|Event ID|260|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|The full path length of the Local Database instance folder is longer than MAX_PATH. The instance must be stored in folder: %%LOCALAPPDATA%%\Microsoft\Microsoft SQL Server Local DB\Instances\\<instance name\>.|  
  
## Explanation  
 The path where the instance should be stored is longer than MAX_PATH.  
  
## User Action  
 Create a new path that is shorter than MAX_PATH.  
  
  
