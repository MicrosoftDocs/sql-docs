---
description: "HasZ (geography Data Type)"
title: "HasZ (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "05/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "HasZ"
  - "HasZ_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "HasZ geography"
ms.assetid: 4c5e1669-a987-4dda-9ebf-f573ce615c34
author: MladjoA
ms.author: mlandzic 
---
# HasZ (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns 1 (true) if a spatial object contains at least one Z value; otherwise, it returns 0 (false).  
  
## Syntax  
  
```  
  
.HasZ  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **Boolean**  
  
## Remarks  
  
## Examples  
  
```sql  
DECLARE @p GEOGRAPHY = 'Point(1 1 1 1)'  
SELECT @p.HasZ   
--Returns: 1 (true)  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [Z &#40;geography Data Type&#41;](../../t-sql/spatial-geography/z-geography-data-type.md)  
  
  
