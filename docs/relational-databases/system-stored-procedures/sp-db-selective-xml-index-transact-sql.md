---
description: "sp_db_selective_xml_index (Transact-SQL)"
title: "sp_db_selective_xml_index (Transact-SQL)"
ms.custom: ""
ms.date: "02/11/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_db_selective_xml_index_TSQL"
  - "sp_db_selective_xml_index"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_db_selective_xml_index procedure"
author: markingmyname
ms.author: maghan
---
# sp_db_selective_xml_index (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Enables and disables Selective XML Index functionality on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. If called without any parameters, the stored procedure returns 1 if the Selective XML Index is enabled on a particular database.  

> [!NOTE]  
> Starting with [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)], the Selective XML Index functionality cannot be disabled. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] In [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)], in order to disable the Selective XML Index feature using this stored procedure, the database must be put in the SIMPLE recovery model by using the [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md) command.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
      sys.sp_db_selective_xml_index[[ @dbname = ] 'dbname'],   
[[ @selective_xml_index = ] 'selective_xml_index']  
```  
  
## Arguments  
`[ @ dbname = ] 'dbname'`
 The name of the database to enable or disable Selective XML Index on. If *dbname* is NULL, the current database is assumed. *@dbname* is **sysname**.


`[ @selective_xml_index = ] 'selective_xml_index'`
 Determines whether to enable or disable the index. Allowed values: 'on', 'off', 'true', 'false'. If another value except 'on', 'true', 'off', or 'false' is passed, an error will be raised. *@selective_xml_index* is **varchar(6)**.

  
## Return Code Values  
 **1** if the Selective XML Index is enabled on a particular database, **0** if disabled.  
  
## Examples  
  
### A. Enable Selective XML Index functionality  
 The following example enables Selective XML Index on the current database.  
  
```sql
EXECUTE sys.sp_db_selective_xml_index  
    @dbname = NULL  
  , @selective_xml_index = N'on';  
GO  
```  
  
 The following example enables Selective XML Index on the AdventureWorks2012 database.  
  
```sql
EXECUTE sys.sp_db_selective_xml_index  
    @dbname = N'AdventureWorks2012'  
  , @selective_xml_index = N'true';  
GO  
```  
  
### B. Disable Selective XML Index functionality  
 The following example disables Selective XML Index on the current database.  
  
```sql
EXECUTE sys.sp_db_selective_xml_index  
    @dbname = NULL  
  , @selective_xml_index = N'off';  
GO  
```  
  
 The following example disables Selective XML Index on the AdventureWorks2012 database.  
  
```sql
EXECUTE sys.sp_db_selective_xml_index  
    @dbname = N'AdventureWorks2012'  
  , @selective_xml_index = N'false';  
GO  
```  
  
### C. Detect if Selective XML Index is enabled  
 The following example detects if Selective XML Index is enabled. Returns 1 if Selective XML Index is enabled.  
  
```sql
EXECUTE sys.sp_db_selective_xml_index;  
GO  
```  
  
## See Also  
 [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md)  
   