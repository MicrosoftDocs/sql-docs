---
title: "Long (geography Data Type)"
description: "Long (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "06/02/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "Long_TSQL"
  - "Long"
helpviewer_keywords:
  - "Long method"
dev_langs:
  - "TSQL"
---

# Long (geography Data Type)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  The longitude property of the **geography** instance.  
  
## Syntax  
  
```syntaxsql
.Long  
```  

## Return Value  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 In the OpenGIS model, Long is defined only on **geography** instances composed of a single point. This property will return NULL if **geography** instances contain more than a single point. This property is precise and read-only.  
  
## Examples  
 This example creates a **Point** instance and retrieves the longitude of the point.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326);  
SELECT @g.Long;  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
