---
title: "Metadata Functions (Transact-SQL)"
description: "Metadata Functions (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "metadata [SQL Server], functions"
  - "functions [SQL Server], metadata"
dev_langs:
  - "TSQL"
---
# Metadata Functions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

The following scalar functions return information about the database and database objects:  
  
- [@@PROCID](../../t-sql/functions/procid-transact-sql.md)
- [APP_NAME](../../t-sql/functions/app-name-transact-sql.md)
- [APPLOCK_MODE](../../t-sql/functions/applock-mode-transact-sql.md)
- [APPLOCK_TEST](../../t-sql/functions/applock-test-transact-sql.md)
- [ASSEMBLYPROPERTY](../../t-sql/functions/assemblyproperty-transact-sql.md)
- [COL_LENGTH](../../t-sql/functions/col-length-transact-sql.md)
- [COL_NAME](../../t-sql/functions/col-name-transact-sql.md)
- [COLUMNPROPERTY](../../t-sql/functions/columnproperty-transact-sql.md)
- [DATABASE_PRINCIPAL_ID](../../t-sql/functions/database-principal-id-transact-sql.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DB_ID](../../t-sql/functions/db-id-transact-sql.md)
- [DB_NAME](../../t-sql/functions/db-name-transact-sql.md)
- [FILE_ID](../../t-sql/functions/file-id-transact-sql.md)
- [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md)
- [FILE_NAME](../../t-sql/functions/file-name-transact-sql.md)
- [FILEGROUP_ID](../../t-sql/functions/filegroup-id-transact-sql.md)
- [FILEGROUP_NAME](../../t-sql/functions/filegroup-name-transact-sql.md)
- [FILEGROUPPROPERTY](../../t-sql/functions/filegroupproperty-transact-sql.md)
- [FILEPROPERTY](../../t-sql/functions/fileproperty-transact-sql.md)
- [FULLTEXTCATALOGPROPERTY](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md)
- [FULLTEXTSERVICEPROPERTY](../../t-sql/functions/fulltextserviceproperty-transact-sql.md)
- [INDEX_COL](../../t-sql/functions/index-col-transact-sql.md)  
- [INDEXKEY_PROPERTY](../../t-sql/functions/indexkey-property-transact-sql.md)  
- [INDEXPROPERTY](../../t-sql/functions/indexproperty-transact-sql.md)  
- [NEXT VALUE FOR](../../t-sql/functions/next-value-for-transact-sql.md)  
- [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md)  
- [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md)  
- [OBJECT_NAME](../../t-sql/functions/object-name-transact-sql.md)  
- [OBJECT_SCHEMA_NAME](../../t-sql/functions/object-schema-name-transact-sql.md)  
- [OBJECTPROPERTY](../../t-sql/functions/objectproperty-transact-sql.md)  
- [OBJECTPROPERTYEX](../../t-sql/functions/objectpropertyex-transact-sql.md)  
- [ORIGINAL_DB_NAME](../../t-sql/functions/original-db-name-transact-sql.md)  
- [PARSENAME](../../t-sql/functions/parsename-transact-sql.md)  
- [SCHEMA_ID](../../t-sql/functions/schema-id-transact-sql.md)  
- [SCHEMA_NAME](../../t-sql/functions/schema-name-transact-sql.md)  
- [SCOPE_IDENTITY](../../t-sql/functions/scope-identity-transact-sql.md)  
- [SERVERPROPERTY](../../t-sql/functions/serverproperty-transact-sql.md)  
- [STATS_DATE](../../t-sql/functions/stats-date-transact-sql.md)  
- [TYPE_ID](../../t-sql/functions/type-id-transact-sql.md)  
- [TYPE_NAME](../../t-sql/functions/type-name-transact-sql.md)  
- [TYPEPROPERTY](../../t-sql/functions/typeproperty-transact-sql.md)  
- [VERSION](../../t-sql/functions/version-transact-sql-metadata-functions.md)  
  
All metadata functions are nondeterministic. This means these functions do not always return the same results every time they are called, even with the same set of input values.  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  

