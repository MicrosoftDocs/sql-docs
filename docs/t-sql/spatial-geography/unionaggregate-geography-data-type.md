---
title: "UnionAggregate (geography Data Type)"
description: "UnionAggregate (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "UnionAggregate"
  - "UnionAggregate_TSQL"
helpviewer_keywords:
  - "UnionAggregate method (geography)"
dev_langs:
  - "TSQL"
---
# UnionAggregate (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Performs a union operation on a set of geography objects.
  
## Syntax  
  
```  
  
UnionAggregate ( geography_operand )  
```  

## Arguments
 *geography_operand*  
 Is a **geography** type table column that holds the set of **geography** objects on which to perform a union operation.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
## Remarks  
 Method returns **null** if the input has different SRIDs. See [Spatial Reference Identifiers &#40;SRIDs&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md).  
  
 Method ignores **null** inputs.  
  
> [!NOTE]  
>  Method returns **null** if all inputted values are **null**.  
  
## Examples  
 The following example performs a `UnionAggregate` on a set of **geography** location points within a city.  
  
 ```sql
 USE AdventureWorks2022  
 GO  
 SELECT City,  
 geography::UnionAggregate(SpatialLocation) AS SpatialLocation  
 FROM Person.Address  
 WHERE PostalCode LIKE('981%')  
 GROUP BY City;
 ```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  
