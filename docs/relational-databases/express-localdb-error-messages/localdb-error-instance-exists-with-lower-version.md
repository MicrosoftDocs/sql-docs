---
title: "LOCALDB_ERROR_INSTANCE_EXISTS_WITH_LOWER_VERSION"
description: "LOCALDB_ERROR_INSTANCE_EXISTS_WITH_LOWER_VERSION"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_INSTANCE_EXISTS_WITH_LOWER_VERSION
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
## Details  
  
| Attribute | Value |
| --------- | ----- |
|Product Name|SQL Server|  
|Event ID|258|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|Unable to create the Local Database instance with specified version. An instance with the same name already exists, but it has lower version than the specified version.|  
  
## Explanation  
 The specified instance already exists but its version is lower than requested.  
  
## User Action  
 Delete the existing instance and retry the operation.  
  
  
