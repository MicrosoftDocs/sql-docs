---
title: "COLLATIONPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "COLLATIONPROPERTY_TSQL"
  - "COLLATIONPROPERTY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "collations [SQL Server], properties"
  - "COLLATIONPROPERTY function"
ms.assetid: f5029e74-a1db-4f69-b0f5-5ee920c3311d
caps.latest.revision: 44
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Collation Functions - COLLATIONPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the property of a specified collation in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
COLLATIONPROPERTY( collation_name , property )  
```  
  
## Arguments  
*collation_name*  
Is the name of the collation. *collation_name* is **nvarchar(128)**, and has no default.
  
*property*  
Is the property of the collation. *property* is **varchar(128)**, and can be any one of the following values:
  
|Property name|Description|  
|---|---|
|**CodePage**|Non-Unicode code page of the collation.|  
|**LCID**|Windows LCID of the collation.|  
|**ComparisonStyle**|Windows comparison style of the collation. Returns 0 for all binary collations .|  
|**Version**|The version of the collation, derived from the version field of the collation ID. Returns 2, 1, or 0.<br /><br /> Collations with "100" in the name) return 2.<br /><br /> Collations with "90" in the name) return 1.<br /><br /> All other collations return 0.|  
  
## Return types
**sql_variant**
  
## Examples  
  
```sql
SELECT COLLATIONPROPERTY('Traditional_Spanish_CS_AS_KS_WS', 'CodePage');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
1252   
```  
  
[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
```sql
SELECT COLLATIONPROPERTY('Traditional_Spanish_CS_AS_KS_WS', 'CodePage')  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
1252   
```  
  
## See also
[sys.fn_helpcollations &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)
  
  

