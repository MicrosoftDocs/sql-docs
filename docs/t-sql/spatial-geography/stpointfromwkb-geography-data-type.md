---
title: "STPointFromWKB (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STPointFromWKB_TSQL"
  - "STPointFromWKB (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPointFromWKB method"
ms.assetid: b3b4e3bb-47bc-4621-99c4-c97aa60cdf8b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STPointFromWKB (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geographyPoint** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STPointFromWKB ( 'WKB_point' , SRID )  
```  
  
## Arguments  
 *WKB_point*  
 Is the WKB representation of the **geographyPoint** instance you wish to return. *WKB_point* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyPoint** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **Point**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STPointFromWKB()` to create a `geography` instance.  
  
```  
DECLARE @g geography;  
SET @g = geography::STPointFromWKB(0x010100000017D9CEF753D347407593180456965EC0, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
