---
description: "sys.fn_helpcollations (Transact-SQL)"
title: "sys.fn_helpcollations (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "fn_helpcollations"
  - "fn_helpcollations_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.fn_helpcollations function"
  - "collations [SQL Server], supported"
  - "fn_helpcollations function"
ms.assetid: b5082e81-1fee-4e2c-b567-5412eaee41c1
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016|| = azure-sqldw-latest ||=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_helpcollations (Transact-SQL)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a list of all supported collations.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```
fn_helpcollations ()  
```  
  
## Tables Returned

 **fn_helpcollations** returns the following information.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|Name|**sysname**|Standard collation name|  
|Description|**nvarchar(1000)**|Description of the collation|  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports Windows collations. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also supports a limited number (<80) of collations called [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations, that were developed before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supported Windows collations. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations are still supported for backward compatibility, but shouldn't be used for new development work. For more information about Windows collations, see [Windows Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/windows-collation-name-transact-sql.md). For more information about collations, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples

 The following example returns all collation names starting with the letter `L` and that are binary sort collations.

> [!Note]
> Azure Synapse Analytics queries against fn_helpcollations() must be run in the master database.  
  
```sql  
SELECT Name, Description FROM fn_helpcollations()  
WHERE Name like 'L%' AND Description LIKE '% binary sort';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Name                   Description  
 -------------------    ------------------------------------  
 Lao_100_BIN            Lao-100, binary sort  
 Latin1_General_BIN     Latin1-General, binary sort  
 Latin1_General_100_BIN Latin1-General-100, binary sort  
 Latvian_BIN            Latvian, binary sort  
 Latvian_100_BIN        Latvian-100, binary sort  
 Lithuanian_BIN         Lithuanian, binary sort  
 Lithuanian_100_BIN     Lithuanian-100, binary sort  
  
 (7 row(s) affected)  
 ```
  
## See Also

[COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md)   
[COLLATIONPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/collation-functions-collationproperty-transact-sql.md)      
[Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md)  
[Database collation support for Azure Synapse Analytics](https://azure.microsoft.com/blog/database-collation-support-for-azure-sql-data-warehouse-2)  
