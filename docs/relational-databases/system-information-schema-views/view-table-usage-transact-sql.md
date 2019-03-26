---
title: "VIEW_TABLE_USAGE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "VIEW_TABLE_USAGE_TSQL"
  - "VIEW_TABLE_USAGE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "INFORMATION_SCHEMA.VIEW_TABLE_USAGE view"
  - "VIEW_TABLE_USAGE view"
ms.assetid: 0aeefb3f-02ef-457e-8c42-84ddb26f1c88
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# VIEW_TABLE_USAGE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns one row for each table in the current database that is used in a view. This information schema view returns information about the objects to which the current user has permissions.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA.**_view_name_.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**VIEW_CATALOG**|**nvarchar(**128**)**|View qualifier.|  
|**VIEW_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the view.<br /><br /> **&#42;&#42; Important &#42;&#42;** Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.|  
|**VIEW_NAME**|**sysname**|View name.|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the base table.<br /><br /> **&#42;&#42; Important &#42;&#42;** Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.|  
|**TABLE_NAME**|**sysname**|Base table that the view is based on.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.sql_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-dependencies-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-views-transact-sql.md)  
  
  
