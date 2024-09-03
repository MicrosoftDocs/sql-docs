---
title: "CollectionAggregate (geography Data Type)"
description: "CollectionAggregate (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "05/18/2021"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "CollectionAggregate method (geography)"
dev_langs:
  - "TSQL"
---
# CollectionAggregate (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates a **GeometryCollection** instance from a set of **geography** objects.
  
## Syntax  
  
```  
  
CollectionAggregate ( geography_operand )  
```  
  
## Arguments
 *geography_operand*  
 Is a **geography** type table column that represents a set of **geography** objects to be listed in the **GeometryCollection** instance.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
## Exception  
 Throws a `FormatException` when there are input values that are not valid. See [STIsValid &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stisvalid-geography-data-type.md)  
  
## Remarks  
 Method returns **null** when the input is empty or the input has different SRIDs. See [Spatial Reference Identifiers &#40;SRIDs&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md)  
  
 Method ignores **null** inputs.  
  
> [!NOTE]  
>  Method returns **null** if all inputted values are **null**.  
  
## Examples  
 The following example returns a `GeometryCollection` instance that contains a set of **geography** objects.  
  
 ```sql
 USE AdventureWorks2022  
 GO  
 SELECT geography::CollectionAggregate(SpatialLocation).ToString() AS SpatialLocation  
 FROM Person.Address  
 WHERE City LIKE ('Bothell')
 ```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  
