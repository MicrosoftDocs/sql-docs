---
title: "Load Data with Prepared Statements (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a48b48d6-15a9-4a1e-a8db-1882f5e33ebf
caps.latest.revision: 6
author: BarbKess
---
# Load Data with Prepared Statements (SQL Server PDW)
You can use SQL Server PDW prepared statements to load data into distributed and replicated tables. When the input data does not match the target data type, an implicit conversion is performed. The implicit conversions supported by SQL Server PDW prepared statements are a subset of conversions supported by SQL Server. That is, only a subset of conversions are supported, but the supported conversions match SQL Server implicit conversions. Regardless of whether the target table to be loaded is defined as a distributed or replicated table, implicit conversions are applied (if needed) to all columns that exist in target table. For more information about prepared statements, see [Running Prepared Statements &#40;SQL Server PDW&#41;](../sqlpdw/running-prepared-statements-sql-server-pdw.md).  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
