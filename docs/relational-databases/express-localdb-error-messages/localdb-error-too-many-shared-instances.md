---
description: "LOCALDB_ERROR_TOO_MANY_SHARED_INSTANCES"
title: "LOCALDB_ERROR_TOO_MANY_SHARED_INSTANCES | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: "reference"
ms.assetid: c1595263-6264-4a43-9535-5eb76ece3a57
author: WilliamDAssafMSFT
ms.author: wiassaf
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
  
  
