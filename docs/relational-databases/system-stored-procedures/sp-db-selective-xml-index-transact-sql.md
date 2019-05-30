---
title: "sp_db_selective_xml_index (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_db_selective_xml_index_TSQL"
  - "sp_db_selective_xml_index"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_db_selective_xml_index procedure"
ms.assetid: 017301a2-4a23-4e68-82af-134f3d4892b3
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_db_selective_xml_index (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Enables and disables Selective XML Index functionality on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. If called without any parameters, the stored procedure returns 1 if the Selective XML Index is enabled on a particular database.  
  
> [!NOTE]  
>  In order to disable the Selective XML Index using this stored procedure, the database must be put in simple recovery mode by using the [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md) command.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
  
      sys.sp_db_selective_xml_index[[ @db_name = ] 'db_name'],   
[[ @action = ] 'action']  
```  
  
## Arguments  
`[ @ db_name = ] 'db_name'`
 The name of the database to enable or disable Selective XML Index on. If *db_name* is NULL, the current database is assumed.  
  
`[ @action = ] 'action'`
 Determines whether to enable or disable the index. If another value except 'on', 'true', 'off', or 'false' is passed, an error will be raised.  
  
```  
  
Allowed values: 'on', 'off', 'true', 'false'  
```  
  
## Return Code Values  
 **1** if the Selective XML Index is enabled on a particular database.  
  
## Examples  
  
### A. Enable Selective XML Index functionality  
 The following example enables Selective XML Index on the current database.  
  
```  
EXECUTE sys.sp_db_selective_xml_index  
    @db_name = NULL  
  , @action = N'on';  
GO  
```  
  
 The following example enables Selective XML Index on the AdventureWorks2012 database.  
  
```  
EXECUTE sys.sp_db_selective_xml_index  
    @db_name = N'AdventureWorks2012'  
  , @action = N'true';  
GO  
```  
  
### B. Disable Selective XML Index functionality  
 The following example disables Selective XML Index on the current database.  
  
```  
EXECUTE sys.sp_db_selective_xml_index  
    @db_name = NULL  
  , @action = N'off';  
GO  
```  
  
 The following example disables Selective XML Index on the AdventureWorks2012 database.  
  
```  
EXECUTE sys.sp_db_selective_xml_index  
    @db_name = N'AdventureWorks2012'  
  , @action = N'false';  
GO  
```  
  
### C. Detect if Selective XML Index is enabled  
 The following example detects if Selective XML Index is enabled. Returns 1 if Selective XML Index is enabled.  
  
```  
EXECUTE sys.sp_db_selective_xml_index;  
GO  
```  
  
## See Also  
 [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md)  
  
  
