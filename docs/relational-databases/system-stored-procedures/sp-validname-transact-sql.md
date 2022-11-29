---
description: "sp_validname (Transact-SQL)"
title: "sp_validname (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_validname"
  - "sp_validname_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_validname"
ms.assetid: d51c53c2-1332-407f-b725-4983f2e710eb
author: VanMSFT
ms.author: vanto
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_validname (Transact-SQL)
[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

  Checks for valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier names. All nonbinary and nonzero data, including Unicode data that can be stored by using the **nchar**, **nvarchar**, or **ntext** data types, are accepted as valid characters for identifier names.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_validname [@name =] 'name'   
     [, [@raise_error =] raise_error]  
```  
  
## Arguments  
`[ @name = ] 'name'`
 Is the name of the [identifiers](../../relational-databases/databases/database-identifiers.md) for which to check validity. *name* is **sysname**, with no default. *name* cannot be NULL, cannot be an empty string, and cannot contain a binary-zero character.  
  
`[ @raise_error = ] raise_error`
 Specifies whether to raise an error. *raise_error* is **bit**, with a default of 1. This means that errors will appear. 0 causes no error messages to appear.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [NCHAR &#40;Transact-SQL&#41;](../../t-sql/functions/nchar-transact-sql.md)   
 [nchar and nvarchar &#40;Transact-SQL&#41;](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md)   
 [ntext, text, and image &#40;Transact-SQL&#41;](../../t-sql/data-types/ntext-text-and-image-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
