---
title: "Data Type Precedence (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "precedence [SQL Server]"
  - "data types [SQL Server], converting"
  - "data types [SQL Server], precedence"
  - "converting data types [SQL Server], precedence"
  - "precedence [SQL Server], data types"
ms.assetid: f4c804ab-ed3f-43b1-a024-c9ac6944b66b
caps.latest.revision: 21
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Data Type Precedence (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  When an operator combines two expressions of different data types, the rules for data type precedence specify that the data type with the lower precedence is converted to the data type with the higher precedence. If the conversion is not a supported implicit conversion, an error is returned. When both operand expressions have the same data type, the result of the operation has that data type.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the following precedence order for data types:  
  
1.  user-defined data types (highest)  
  
2.  **sql_variant**  
  
3.  **xml**  
  
4.  **datetimeoffset**  
  
5.  **datetime2**  
  
6.  **datetime**  
  
7.  **smalldatetime**  
  
8.  **date**  
  
9. **time**  
  
10. **float**  
  
11. **real**  
  
12. **decimal**  
  
13. **money**  
  
14. **smallmoney**  
  
15. **bigint**  
  
16. **int**  
  
17. **smallint**  
  
18. **tinyint**  
  
19. **bit**  
  
20. **ntext**  
  
21. **text**  
  
22. **image**  
  
23. **timestamp**  
  
24. **uniqueidentifier**  
  
25. **nvarchar** (including **nvarchar(max)** )  
  
26. **nchar**  
  
27. **varchar** (including **varchar(max)** )  
  
28. **char**  
  
29. **varbinary** (including **varbinary(max)** )  
  
30. **binary** (lowest)  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  