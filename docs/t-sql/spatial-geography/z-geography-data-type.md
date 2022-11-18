---
description: "Z (geography Data Type)"
title: "Z (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "Z (geography Data Type)"
  - "Z_(geography_Data_Type)_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Z method"
ms.assetid: 9abc79c5-43c9-4cc2-b37f-d2ecdec7c234
author: MladjoA
ms.author: mlandzic 
---
# Z (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  The Z (elevation) value of the instance. The semantics of the elevation value are user-defined.  
  
## Syntax  
  
```  
  
.Z  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property is null if the **geography** instance is not a point, as well as for any **Point** instance for which it is not set.  
  
 This property is read-only.  
  
 Z-coordinates are not used in any calculations made by the library and are not carried through any library calculations.  
  
## Examples  
 The following example creates a `Point` instance with Z (elevation) and M (measure) values and uses `Z` to fetch the Z value of the instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POINT(-122.34900 47.65100 10.3 12)', 4326);  
SELECT @g.Z;  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [M &#40;geography Data Type&#41;](../../t-sql/spatial-geography/m-geography-data-type.md)   
 [AsTextZM &#40;geography Data Type&#41;](../../t-sql/spatial-geography/astextzm-geography-data-type.md)  
  
  
