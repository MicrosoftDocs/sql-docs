---
title: "FILEPROPERTYEX (Transact-SQL)"
description: "FILEPROPERTYEX (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/23/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "FILEPROPERTYEX_TSQL"
  - "FILEPROPERTYEX"
helpviewer_keywords:
  - "viewing file properties"
  - "displaying file properties"
  - "file properties [SQL Server]"
  - "FILEPROPERTYEX function"
  - "file names [SQL Server], FILEPROPERTYEX"
dev_langs:
  - "TSQL"
---
# FILEPROPERTYEX (Transact-SQL)
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/asdb-asmi-fabricsqldb.md)]

  Returns the specified extended file property value when a file name in the current database and a property name are specified. Returns NULL for files that are not in the current database or for extended file properties that do not exist. Currently, extended file properties only apply to databases that are in Azure Blob storage.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
FILEPROPERTYEX ( name , property )  
```  
  
## Arguments  
 *name*  
 Is an expression that contains the name of the file associated with the current database for which to return property information. *file_name* is **nchar(128)**.  
  
 *property*  
 Is an expression that contains the name of the file property to return. *property* is **varchar(128)**, and can be one of the following values.  


  
|Value|Description|
|-----------|-----------------|  
|**BlobTier**|The tier of target Azure page blob. Applies only to Standard and GeneralPurpose databases that uses Azure page blob storage.|
|**AccountType**|The storage account type indicating whether it is blob storage or file storage and whether it is premium or standard storage.|
|**IsInferredTier**|Indicates whether the tier is an implicit (inferred) tier which could grow with data size, or an explicit (fixed) tier.|
|**IsPageBlob**|Indicates whether the target blob is page blob or not.|
  
## Return Types  
 **sql_variant**  
  
## Remarks  
 *file_name* corresponds to the **name** column in the **sys.master_files** or **sys.database_files** catalog view.  
  
## Examples  
 The following example returns the setting for database files:
```sql
SELECT s.file_id,
       s.type_desc,
       s.name,
       FILEPROPERTYEX(s.name, 'BlobTier') AS BlobTier,
       FILEPROPERTYEX(s.name, 'AccountType') AS AccountType,
       FILEPROPERTYEX(s.name, 'IsInferredTier') AS IsInferredTier,
       FILEPROPERTYEX(s.name, 'IsPageBlob') AS IsPageBlob
FROM sys.database_files AS s
WHERE s.type_desc IN ('ROWS', 'LOG');
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
file_id  type_desc  name  BlobTier  AccountType  IsInferredTier  IsPageBlob
--------------------------------------------------------------------------------------
1     ROWS      data_0  P30  PremiumBlobStorage  0   1
2     LOG       log     P30  PremiumBlobStorage  0   1

(2 rows affected)
```  
  
## Related content

- [FILEGROUPPROPERTY (Transact-SQL)](filegroupproperty-transact-sql.md)
- [Metadata functions (Transact-SQL)](metadata-functions-transact-sql.md)
- [sys.sp_spaceused (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [sys.database_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.master_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)
