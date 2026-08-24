---
title: "EnvelopeAngle (geography Data Type)"
description: "EnvelopeAngle (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "EnvelopeAngle"
  - "EnvelopeAngle_TSQL"
helpviewer_keywords:
  - "EnvelopeAngle method"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# EnvelopeAngle (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns the maximum angle between the point returned by `EnvelopeCenter()` and a point in the **geography** instance in degrees.  
  
 This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
EnvelopeAngle( )  
```  

## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
 CLR return type: **SqlDouble**  
  
## Remarks  
 This method returns a point in the **geography** instance in degrees. When used with EnvelopeCenter(), `EnvelopeAngle()` returns a bounding circle of a **geography** instance.  
  
 In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this method has been extended to **FullGlobe** instances.  
  
 The hemisphere limitation applied to `EnvelopeAngle()` in [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] has been removed. However, for instances with angles greater than 90 degrees, 180 degrees will be returned. `EnvelopeAngle()` is not precise for **geography** instances that span more than one hemisphere.  
  
## Examples  
  
```sql
DECLARE @g geography = 'LINESTRING(-120 45, -120 0, -90 0)';   
SELECT @g.EnvelopeAngle();  
```  
  
## Related content

- [Extended methods on geography instances](extended-methods-on-geography-instances.md)
- [EnvelopeCenter (geography Data Type)](envelopecenter-geography-data-type.md)
