---
title: ":: (Scope Resolution) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 764d8f91-957b-4037-997b-a9b6b533c504
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---

# :: (Scope Resolution) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The scope resolution operator **::** provides access to static members of a compound data type. A compound data type is one that contains multiple simple data types and methods, such as the built-in CLR types and custom SQLCLR User-Defined Types (UDTs).  
  
## Examples  
 The following example shows how to use the scope resolution operator to access the `GetRoot()` member of the `hierarchyid` type.  
  
```  
DECLARE @hid hierarchyid;  
SELECT @hid = hierarchyid::GetRoot();  
PRINT @hid.ToString();  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `/`  
  
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
  
  
