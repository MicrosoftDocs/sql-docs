---
title: "bit (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/22/2017
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "bit_TSQL"
  - "bit"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "bit data type"
ms.assetid: 40adfd08-a31c-49cb-a172-386bcaa6edee
caps.latest.revision: 33
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# bit (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  An integer data type that can take a value of 1, 0, or NULL.  
  
## Remarks  
The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] optimizes storage of **bit** columns. If there are 8 or less **bit** columns in a table, the columns are stored as 1 byte. If there are from 9 up to 16 **bit** columns, the columns are stored as 2 bytes, and so on.
  
The string values TRUE and FALSE can be converted to **bit** values: TRUE is converted to 1 and FALSE is converted to 0.
  
Converting to bit promotes any nonzero value to 1.
  
## See also
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)
[Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)
[sys.types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)
  
  
