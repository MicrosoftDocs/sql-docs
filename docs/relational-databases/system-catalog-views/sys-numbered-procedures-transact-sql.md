---
title: "sys.numbered_procedures (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.numbered_procedures_TSQL"
  - "numbered_procedures"
  - "sys.numbered_procedures"
  - "numbered_procedures_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.numbered_procedures catalog view"
ms.assetid: 5b6d6498-bac6-4266-94b9-d16ef5089cf0
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.numbered_procedures (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Contains a row for each SQL Server stored procedure that was created as a numbered procedure. This does not show a row for the base (number = 1) stored procedure. Entries for the base stored procedures can be found in views such as **sys.objects** and **sys.procedures**.  
  
> [!IMPORTANT]  
>  Numbered procedures are deprecated. Use of numbered procedures is discouraged. A DEPRECATION_ANNOUNCEMENT event is fired when a query that uses this catalog view is compiled.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object of the stored procedure.|  
|**procedure_number**|**smallint**|Number of this procedure within the object, 2 or greater.|  
|**definition**|**nvarchar(max)**|The SQL Server text that defines this procedure.<br /><br /> NULL = encrypted.|  
  
> [!NOTE]  
>  XML and CLR parameters are not supported for numbered procedures.  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
