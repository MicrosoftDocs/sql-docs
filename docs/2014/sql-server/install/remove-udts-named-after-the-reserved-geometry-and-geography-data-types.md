---
title: "Remove UDTs named after the reserved GEOMETRY and GEOGRAPHY data types | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "geometry data type [SQL Server], UDTs"
  - "geography data type [SQL Server], UDTs"
ms.assetid: a167ce3a-50b4-4e77-a884-adb23b586c72
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove UDTs named after the reserved GEOMETRY and GEOGRAPHY data types
  Upgrade Advisor detected a user-defined type (UDT) that is named after a term reserved for either the `geometry` or the `geography` data types. The `geometry` and `geography` data types are part of the spatial data feature.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 The terms used for spatial data types should not be used as names for either common language runtime (CLR) or alias UDTs.  
  
## Corrective Action  
 Remove the UDT that is named after the data type and recreate the UDT with an unreserved name.  
  
## External Resources  
 [Creating a User-Defined Type](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types.md)  
  
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)  
  
  
