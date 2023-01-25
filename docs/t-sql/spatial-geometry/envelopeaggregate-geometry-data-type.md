---
description: "EnvelopeAggregate (geometry Data Type)"
title: "EnvelopeAggregate (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "EnvelopeAggregate method (geometry)"
ms.assetid: c4c15abe-0fe9-441d-9d42-6572e264869c
author: MladjoA
ms.author: mlandzic 
---
# EnvelopeAggregate (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a bounding box for a given set of **geometry** objects.
  
## Syntax  
  
```  
  
EnvelopeAggregate ( geometry_operand )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *geometry_operand*  
 Is a **geometry** type table column that represents the set of **geometry** objects.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
## Exceptions  
 Throws a `FormatException` when there are input values that are not valid. See [STIsValid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stisvalid-geometry-data-type.md)  
  
## Remarks  
 Method returns **null** when the input is empty or the input has different SRIDs. See [Spatial Reference Identifiers &#40;SRIDs&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md)  
  
 Method ignores **null** inputs.  
  
> [!NOTE]  
>  Method returns **null** if all inputted values are **null**.  
  
## Examples  
 The following example returns a bounding box for a set of objects in a table variable column.  
  
 ```sql
 -- Setup table variable for EnvelopeAggregate example 
DECLARE @Geom TABLE 
( 
shape geometry, 
shapeType nvarchar(50) 
) 
INSERT INTO @Geom(shape,shapeType) VALUES('CURVEPOLYGON(CIRCULARSTRING(2 3, 4 1, 6 3, 4 5, 2 3))', 'Circle'), 
('POLYGON((1 1, 4 1, 4 5, 1 5, 1 1))', 'Rectangle'); 
-- Perform EnvelopeAggregate on @Geom.shape column 
SELECT geometry::EnvelopeAggregate(shape).ToString() 
FROM @Geom;
```  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  

