---
description: "STPolyFromWKB (geography Data Type)"
title: "STPolyFromWKB (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STPolyFromWKB_TSQL"
  - "STPolyFromWKB (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPolyFromWKB method"
ms.assetid: d236e0ea-dabe-4341-a6eb-ecc210d1f056
author: MladjoA
ms.author: mlandzic 
---
# STPolyFromWKB (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geographyPolygon** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STPolyFromWKB ( 'WKB_polygon' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *WKB_polygon*  
 Is the WKB representation of the **geographyPolygon** instance you wish to return. *WKB_polygon* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyPolygon** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **Polygon**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STPolyFromWKB()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;   
SET @g = geography::STPolyFromWKB(0x01030000000100000005000000F4FDD478E9965EC0DD24068195D3474083C0CAA145965EC0508D976E12D3474083C0CAA145965EC04E62105839D44740F4FDD478E9965EC04E62105839D44740F4FDD478E9965EC0DD24068195D34740, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
