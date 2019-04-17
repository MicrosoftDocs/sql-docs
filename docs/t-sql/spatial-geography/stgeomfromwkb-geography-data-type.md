---
title: "STGeomFromWKB (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STGeomFromWKB_TSQL"
  - "STGeomFromWKB (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeomFromWKB method"
ms.assetid: 79d39d88-5440-49a7-9247-190eafce3f4f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STGeomFromWKB (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.
  
## Syntax  
  
```  
  
STGeomFromWKB ( 'WKB_geography' , SRID )  
```  
  
## Arguments  
 *WKB_geography*  
 Is the WKB representation of the **geography** instance to return. *WKB_geography* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geography** instance to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 The OGC type of the **geography** instance returned by `STGeomFromText()` is set to the corresponding WKB input.  
  
 This method throws a **FormatException** if the input is not well-formatted.  
  
 This method will throw **ArgumentException** if the input contains an antipodal edge.  
  
## Examples  
 The following example uses `STGeomFromWKB()` to create a `geography` instance.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromWKB(0x010200000002000000D7A3703D0A975EC08716D9CEF7D34740CBA145B6F3955EC08716D9CEF7D34740, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
