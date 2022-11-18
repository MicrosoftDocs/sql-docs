---
title: "FILEGROUPPROPERTY (Transact-SQL)"
description: "FILEGROUPPROPERTY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FILEGROUPPROPERTY_TSQL"
  - "FILEGROUPPROPERTY"
helpviewer_keywords:
  - "filegroups [SQL Server], property values"
  - "FILEGROUPPROPERTY function"
  - "viewing filegroup properties"
  - "displaying filegroup properties"
dev_langs:
  - "TSQL"
---
# FILEGROUPPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function returns the filegroup property value for a specified name  and filegroup value.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
FILEGROUPPROPERTY ( filegroup_name, property )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *filegroup_name*  
An expression of type **sysname** that represents the filegroup name for which `FILEGROUPPROPERTY` returns the named property information.  
  
 *property*  
An expression of type **varchar(128)** that returns the name of the filegroup property. *Property* can return one of these values:  
  
|Value|Description|Value returned|  
|-----------|-----------------|--------------------|  
|**IsReadOnly**|Filegroup is read-only.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Invalid input.|  
|**IsUserDefinedFG**|Filegroup is a user-defined filegroup.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Invalid input.|  
|**IsDefault**|Filegroup is the default filegroup.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Invalid input.|  
  
## Return Types  
**int**  
  
## Remarks  
*filegroup_name* corresponds to the **name** column from the **sys.filegroups** catalog view.  
  
## Examples  
This example returns the `IsDefault` property setting for the primary filegroup in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT FILEGROUPPROPERTY('PRIMARY', 'IsDefault') AS 'Default Filegroup';  
GO  
```  

 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]   
```  
Default Filegroup   
---------------------   
1  
  
(1 row(s) affected)  
```  
  
## See Also  
 [FILEGROUP_ID &#40;Transact-SQL&#41;](../../t-sql/functions/filegroup-id-transact-sql.md)   
 [FILEGROUP_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/filegroup-name-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)  
  
  
