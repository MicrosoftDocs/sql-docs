---
title: "FILEPROPERTYEX (Transact-SQL)"
description: "FILEPROPERTYEX (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/23/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

  Returns the specified extended file property value when a file name in the current database and a property name are specified. Returns NULL for files that are not in the current database or for extended file properties that do not exist. Currently, extended file properties only apply to databases that are in Azure Blob storage.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
## See Also  
 [FILEGROUPPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/filegroupproperty-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
  
