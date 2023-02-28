---
title: "LOCALDB_ERROR_TOO_MANY_SHARED_INSTANCES"
description: "LOCALDB_ERROR_TOO_MANY_SHARED_INSTANCES"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_TOO_MANY_SHARED_INSTANCES
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
## Details  
  
| Attribute | Value |
| --------- | ----- |
|Product Name|SQL Server|  
|Event ID|287|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|There are too many shared instance and we cannot generate unique User Instance Name. Unshare some of the existing shared instances.|  
  
## Explanation  
 There are too many shared instance and we cannot generate unique User Instance Name.  
  
## User Action  
 Unshare one or more of the shared Local Database Runtime instances and try again.  
  
  
