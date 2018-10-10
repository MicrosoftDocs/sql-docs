---
title: "STMPointFromText (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STMPointFromText (geography Data Type)"
  - "STMPointFromText_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STMPointFromText method"
ms.assetid: fe91a9f5-8de6-464e-88db-00650eae79b0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STMPointFromText (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STMPointFromText ( 'multipoint_tagged_text', SRID )  
```  
  
## Arguments  
 *multipoint_tagged_text*  
 Is the WKT representation of the **geographyMultiPoint** instance you wish to return. *multipoint_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyMultiPoint** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **MultiPoint**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STMPointFromText()` to create a `geography` instance.  
  
```  
DECLARE @g geography;   
SET @g = geography::STMPointFromText('MULTIPOINT(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
