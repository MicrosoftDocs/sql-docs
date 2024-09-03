---
title: "STMLineFromWKB (geography Data Type)"
description: "STMLineFromWKB (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STMLineFromWKB_TSQL"
  - "STMLineFromWKB (geography Data Type)"
helpviewer_keywords:
  - "STMLineFromWKB method"
dev_langs:
  - "TSQL"
---
# STMLineFromWKB (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geographyMultiLineString** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STMLineFromWKB ( 'WKB_multilinestring' , SRID )  
```  
  
## Arguments
 *WKB_multilinestring*  
 Is the WKB representation of the **geographyMultiLineString** instance to return. *WKB_multilinestring* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyMultiLineString** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **MultiLineString**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STMLineFromWKB()` to create a `geography`instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STMLineFromWKB(0x010500000002000000010200000005000000F4FDD478E9965EC0DD24068195D3474083C0CAA145965EC0508D976E12D3474083C0CAA145965EC04E62105839D44740F4FDD478E9965EC04E62105839D44740F4FDD478E9965EC0DD24068195D34740010200000005000000022B8716D9965EC0C1CAA145B6D34740022B8716D9965EC06ABC749318D447407593180456965EC06ABC749318D447407593180456965EC03333333333D34740022B8716D9965EC0C1CAA145B6D34740, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
