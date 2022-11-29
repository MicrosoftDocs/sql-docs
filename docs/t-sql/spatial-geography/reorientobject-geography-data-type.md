---
description: "ReorientObject (geography Data Type)"
title: "ReorientObject (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "ReorientObject"
  - "ReorientObject_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ReorientObject method (geography)"
ms.assetid: e2a1a4f1-211b-4e82-abed-03fc7140a83c
author: MladjoA
ms.author: mlandzic 
---
# ReorientObject (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance with interchanged interior regions and exterior regions.  
  
This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```syntaxsql
.ReorientObject (geography)  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
_geography_  
Is another **geography** instance on which `ReorientObject()` is invoked.  
  
## Return Value  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
CLR return type: **SqlGeography**  
  
## Remarks  
This method changes the ring orientation of all **Polygons** in a **GeometryCollection** but doesn't remove or change any **Points** or **LineStrings** in the given collection.  
  
If you pass a **GeometryCollection** to this method, each instance in the collection reorients as a result, but the whole collection doesn't reorient.  
  
## Examples  
  
```sql
DECLARE @R GEOGRAPHY = GEOGRAPHY::Parse('Polygon((-10 -10, -10 10, 10 10, 10 -10, -10 -10))');  
SELECT @R.ReorientObject().STAsText();  
--Result: POLYGON ((10 10, -10 10, -10 -10, 10 -10, 10 10))  
```  
  
## See Also  
[Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
