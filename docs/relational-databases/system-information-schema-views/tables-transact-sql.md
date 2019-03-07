---
title: "TABLES (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "TABLES_TSQL"
  - "TABLES"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TABLES view"
  - "INFORMATION_SCHEMA.TABLES view"
ms.assetid: 723a9e63-8f6e-4d6e-b570-468cfaf03201
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# TABLES (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns one row for each table in the current database for which the current user has permissions.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA.**_view_name_.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the table.<br /><br /> <strong>\*\* Important \*\*</strong> Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of an object is to query the sys.objects catalog view. INFORMATION_SCHEMA views could be incomplete since they are not updated for all new features.|  
|**TABLE_NAME**|**sysname**|Table name.|  
|**TABLE_TYPE**|**varchar(**10**)**|Type of table. Can be VIEW or BASE TABLE.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)  
  
  
