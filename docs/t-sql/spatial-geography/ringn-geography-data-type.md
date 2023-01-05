---
description: "RingN (geography Data Type)"
title: "RingN (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "RingN"
  - "RingN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "RingN method"
ms.assetid: 30f47275-2727-4d22-bbec-c0c54bcb3ac2
author: MladjoA
ms.author: mlandzic 
---
# RingN (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the specified ring of the **geography** instance: `1 ≤ n ≤ NumRings()`.  
  
## Syntax  
  
```syntaxsql
.RingN (expression )
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is an **int** expression between 1 and the number of rings in a **polygon** instance.  
  
## Return Value  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 If the value of the ring index **n** is less than 1, this method throws an **ArgumentOutOfRangeException.** The ring index value must be greater than or equal to 1 and should be less than or equal to the number returned by `NumRings()`.  
  
## Examples  
 This example creates a `Polygon` instance with two rings and returns the second ring.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653), (-122.357 47.654, -122.357 47.657, -122.349 47.657, -122.349 47.650, -122.357 47.654))', 4326);  
SELECT @g.RingN(2).ToString();  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [NumRings &#40;geography Data Type&#41;](../../t-sql/spatial-geography/numrings-geography-data-type.md)  
  
  
