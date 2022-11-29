---
title: ":: (Scope Resolution) (Transact-SQL)"
description: ":: (Scope Resolution) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
dev_langs:
  - "TSQL"
---

# :: (Scope Resolution) (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  The scope resolution operator **::** provides access to static members of a compound data type. A compound data type is one that contains multiple simple data types and methods. Compound data types include the built-in CLR types and custom SQLCLR User-Defined Types (UDTs).  
  
## Examples  
 The following example shows how to use the scope resolution operator to access the `GetRoot()` member of the `hierarchyid` type.  
  
```sql  
DECLARE @hid hierarchyid;  
SELECT @hid = hierarchyid::GetRoot();  
PRINT @hid.ToString();  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `/`  
  
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
 
