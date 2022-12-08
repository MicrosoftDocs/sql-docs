---
description: "STPointFromWKB (geography Data Type)"
title: "STPointFromWKB (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STPointFromWKB_TSQL"
  - "STPointFromWKB (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPointFromWKB method"
ms.assetid: b3b4e3bb-47bc-4621-99c4-c97aa60cdf8b
author: MladjoA
ms.author: mlandzic 
---
# STPointFromWKB (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geographyPoint** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STPointFromWKB ( 'WKB_point' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
```sql
DECLARE @g geography;  
SET @g = geography::STPointFromWKB(0x010100000017D9CEF753D347407593180456965EC0, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
