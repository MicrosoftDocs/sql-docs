---
title: "Comparability for IRowsetFind | Microsoft Docs"
description: "Comparability for IRowsetFind"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "IRowsetFind comparability"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Comparability for IRowsetFind
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  For date/time types only, IRowsetFind supports the following comparisons:  
  
-   LT  
  
-   LE  
  
-   EQ  
  
-   GE  
  
-   GT  
  
-   NE  
  
-   IGNORE  
  
 If any other comparison is attempted, DB_E_BADCOMPAREOP is returned. This is consistent with the OLE DB specification.  
  
## See Also  
 [Date and Time Improvements &#40;OLE DB&#41;](../../oledb/ole-db-date-time/date-and-time-improvements-ole-db.md)  
  
  
