---
title: "Bitwise Operators (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ef3e5941-a0c3-496f-acf9-9c0e7fccb1ba
caps.latest.revision: 4
author: BarbKess
---
# Bitwise Operators (SQL Server PDW)
Bitwise operators perform bit manipulations between two expressions of any of the data types of the integer data type category.  
  
|Operator|Meaning|  
|------------|-----------|  
|[& &#40;Bitwise AND&#41; &#40;SQL Server PDW&#41;](../sqlpdw/bitwise-and-sql-server-pdw.md)|Bitwise AND (two operands).|  
|[&#124; &#40;Bitwise OR&#41; &#40;SQL Server PDW&#41;](../sqlpdw/bitwise-or-sql-server-pdw.md)|Bitwise OR (two operands).|  
|[^ &#40;Bitwise Exclusive OR&#41; &#40;SQL Server PDW&#41;](../sqlpdw/bitwise-exclusive-or-sql-server-pdw.md)|Bitwise exclusive OR (two operands).|  
  
The operands for bitwise operators can be any one of the data types of the integer or binary string data type categories (except for the **image** data type), except that both operands cannot be any one of the data types of the binary string data type category. The following table shows the supported operand data types.  
  
|Left operand|Right operand|  
|----------------|-----------------|  
|**binary**|**int**, **smallint**, or **tinyint**|  
|**bit**|**int**, **smallint**, **tinyint**, or **bit**|  
|**int**|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|  
|**smallint**|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|  
|**tinyint**|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|  
|**varbinary**|**int**, **smallint**, or **tinyint**|  
  
For more information about data types, see [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
## See Also  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
  
