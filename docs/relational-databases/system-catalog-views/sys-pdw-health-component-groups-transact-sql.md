---
title: "sys.pdw_health_component_groups (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "pdw"
ms.service: ""
ms.component: "system-catalog-views"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 5ba27432-7a29-4420-b73d-def621c0b3ac
caps.latest.revision: 6
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
ms.workload: "Inactive"
---
# sys.pdw_health_component_groups (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Stores information about logical groupings of components and devices.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|group_id|**int**|Unique identifier for components and devices.<br /><br /> Key for this view.|NOT NULL|  
|group_name|**nvarchar(255)**|Logical group name for the components and devices.|NOT NULL|  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
