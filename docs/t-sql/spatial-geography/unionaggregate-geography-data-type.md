---
description: "UnionAggregate (geography Data Type)"
title: "UnionAggregate (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "UnionAggregate"
  - "UnionAggregate_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "UnionAggregate method (geography)"
ms.assetid: 1a3aeef1-5b0e-4ae8-aeb7-c4aab22f42ab
author: MladjoA
ms.author: mlandzic 
---
# UnionAggregate (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Performs a union operation on a set of geography objects.
  
## Syntax  
  
```  
  
UnionAggregate ( geography_operand )  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
 USE AdventureWorks2012  
 GO  
 SELECT City,  
 geography::UnionAggregate(SpatialLocation) AS SpatialLocation  
 FROM Person.Address  
 WHERE PostalCode LIKE('981%')  
 GROUP BY City;
 ```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  
