---
title: "dbo.syscategories (Transact-SQL)"
description: dbo.syscategories (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.syscategories_TSQL"
  - "syscategories"
  - "syscategories_TSQL"
  - "dbo.syscategories"
helpviewer_keywords:
  - "syscategories system table"
dev_langs:
  - "TSQL"
ms.assetid: eb2cb75c-dc58-4a5b-b329-664e9fe20ce0
---
# dbo.syscategories (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains the categories used by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to organize jobs, alerts, and operators. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**category_id**|**int**|ID of the category|  
|**category_class**|**int**|Type of item in the category:<br /><br /> **1** = Job<br /><br /> **2** = Alert<br /><br /> **3** = Operator|  
|**category_type**|**tinyint**|Type of category:<br /><br /> **1** = Local<br /><br /> **2** = Multiserver<br /><br /> **3** = None|  
|**name**|**sysname**|Name of the category|  
  
  
