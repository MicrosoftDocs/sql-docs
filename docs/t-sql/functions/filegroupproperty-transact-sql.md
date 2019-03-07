---
title: "FILEGROUPPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FILEGROUPPROPERTY_TSQL"
  - "FILEGROUPPROPERTY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "filegroups [SQL Server], property values"
  - "FILEGROUPPROPERTY function"
  - "viewing filegroup properties"
  - "displaying filegroup properties"
ms.assetid: b3a930e6-df05-4034-929c-f681f5f6fc6e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# FILEGROUPPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

This function returns the filegroup property value for a specified name  and filegroup value.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
FILEGROUPPROPERTY ( filegroup_name, property )  
```  
  
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
  
```  
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
  
  
