---
title: "STMPolyFromWKB (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STMPolyFromWKB (geography Data Type)"
  - "STMPolyFromWKB_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STMPolyFromWKB method"
ms.assetid: c4d0e649-0abb-4343-a3f0-3a702c8bbbdb
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STMPolyFromWKB (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geographyMultiPolygon** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STMPolyFromWKB ( 'WKB_multipolygon' , SRID )  
```  
  
## Arguments  
 *WKB_multipolygon*  
 Is the WKB representation of the **geographyMultiPolygon** instance you wish to return. *WKB_multipolygon* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyMultiPolygon** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **MultiPolygon**  
  
## Examples  
 The following example uses `STMPolyFromWKB()` to create a `geography` instance.  
  
```  
DECLARE @g geography;  
SET @g = geography::STMPolyFromWKB(0x01060000000200000001030000000100000004000000F4FDD478E9965EC0DD24068195D3474083C0CAA145965EC0508D976E12D34740F4FDD478E9965EC04E62105839D44740F4FDD478E9965EC0DD24068195D3474001030000000100000004000000E7FBA9F1D2955EC08716D9CEF7D34740E7FBA9F1D2955EC0F853E3A59BD447405839B4C876965EC0F853E3A59BD44740E7FBA9F1D2955EC08716D9CEF7D34740, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
