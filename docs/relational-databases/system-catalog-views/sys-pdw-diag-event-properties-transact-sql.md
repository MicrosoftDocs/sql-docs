---
title: "sys.pdw_diag_event_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.service: "sql-data-warehouse"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: e3944f48-8074-43aa-9840-3d5230faedd3
caps.latest.revision: 7
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# sys.pdw_diag_event_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw_md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Holds information about which properties are associated with diagnostic events.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|**event_name**|**nvarchar(255)**|Name of the specific diagnostics event.||  
|**property_name**|**nvarchar(255)**|Name of a property of the event.||  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  