---
description: "AsTextZM (geography Data Type)"
title: "AsTextZM (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "AsTextZM_(geography Data Type)"
  - "AsTextZM_(geography_Data_Type)_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "AsTextZM method"
ms.assetid: e9dc27f6-e945-4457-8498-7644db34008e
author: MladjoA
ms.author: mlandzic 
---
# AsTextZM (geography Data Type)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a **geography** instance augmented with any **Z** (elevation) and **M** (measure) values carried by the instance.  
  
## Syntax  
  
```sql  
.AsTextZM ()  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
CLR return type: **SqlChars**  
  
## Remarks  
  
## Examples  

The following example creates a `Point` instance that contains **Z** (elevation) and **M** (measure) values. `STAsText()` selects the WKT values, (-122.34900 47.65100); `AsTextZM()` selects the same WKT values and also returns the values for **Z** and **M**, yielding (-122.34900 47.65100 10.3 12).  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POINT(-122.34900 47.65100 10.3 12)', 4326);  
SELECT @g.STAsText();  
SELECT @g.AsTextZM();  
```  
  
## See Also  

- [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)
- [M &#40;geography Data Type&#41;](../../t-sql/spatial-geography/m-geography-data-type.md)
- [Z &#40;geography Data Type&#41;](../../t-sql/spatial-geography/z-geography-data-type.md)