---
title: "AsGml (geography Data Type)"
description: "AsGml (geography Data Type)"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "AsGml_(geography_Data_Type)_TSQL"
  - "AsGml_(geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "AsGml method"
author: MladjoA
ms.author: mlandzic 
ms.reviewer: ""
ms.custom: ""
ms.date: "03/14/2017"
---

# AsGml - geography Data Type

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the Geography Markup Language (GML) representation of a **geography** instance.  
  
For more information on Geography Markup Language, see the Open Geospatial Consortium Specification: [OGC Specifications, Geography Markup Language.](https://go.microsoft.com/fwlink/?LinkId=93629)  
  
## Syntax  
  
```  
  
.AsGml ( )  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **xml**  
  
 CLR return type: **SqlXml**  
  
## Remarks  
  
## Examples  
 The following example creates a `LineString` instance and uses `AsGML()` to return the GML description of the instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.AsGml();  
```  
  
 This method returns the description as a `LineString` instance.  
  
```xml
<LineString xmlns="https://www.opengis.net/gml"><posList>47.656 -122.36 47.656 -122.343</posList></LineString>  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
