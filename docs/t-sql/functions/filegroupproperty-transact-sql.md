---
title: "FILEGROUPPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 22
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# FILEGROUPPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the specified filegroup property value when supplied with a filegroup and property name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
FILEGROUPPROPERTY ( filegroup_name , property )  
```  
  
## Arguments  
 *filegroup_name*  
 Is an expression of type **sysname** that represents the name of the filegroup for which to return the named property information.  
  
 *property*  
 Is an expression of type **varchar(128)** contains the name of the filegroup property to return. *property* can be one of these values.  
  
|Value|Description|Value returned|  
|-----------|-----------------|--------------------|  
|**IsReadOnly**|Filegroup is read-only.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Input is not valid.|  
|**IsUserDefinedFG**|Filegroup is a user-defined filegroup.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Input is not valid.|  
|**IsDefault**|Filegroup is the default filegroup.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Input is not valid.|  
  
## Return Types  
 **int**  
  
## Remarks  
 *filegroup_name* corresponds to the **name** column in the **sys.filegroups** catalog view.  
  
## Examples  
 This example returns the setting for the `IsDefault` property for the primary filegroup in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
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
  
  