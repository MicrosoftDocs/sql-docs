---
description: "STGeomCollFromWKB (geography Data Type)"
title: "STGeomCollFromWKB (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STGeomCollFromWKB (geography Data Type)"
  - "STGeomCollFromWKB_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STMGeomCollFromWKB method"
ms.assetid: bbed028c-9cd6-4236-b5e5-8e914a21f2e4
author: MladjoA
ms.author: mlandzic 
---
# STGeomCollFromWKB (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **GeometryCollection**instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STGeomCollFromWKB ( 'WKB_geometrycollection' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *WKB_geometrycollection*  
 Is the WKB representation of the **GeometryCollection** instance you wish to return. *WKB_geometrycollection* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **GeometryCollection** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 The OGC type of the **geography** instance returned by STGeomCollFromWKB() is set to **GeometryCollection**, **MultiPolygon**, **MultiLineString**, or **MultiPoint**, depending on the corresponding WKB input.  
  
 This method throws a **FormatException** exception if the input is not well-formatted.  
  
## Examples  
 The following example uses `STGeomCollFromWKB()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomCollFromWKB(0x01070000000200000001010000007593180456965EC017D9CEF753D34740010200000002000000D7A3703D0A975EC08716D9CEF7D34740CBA145B6F3955EC08716D9CEF7D34740, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
