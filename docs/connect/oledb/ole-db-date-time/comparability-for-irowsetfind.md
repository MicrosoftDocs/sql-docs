---
title: "Comparability for IRowsetFind"
description: Learn about the comparisons that IRowsetFind supports for date/time types in OLE DB Driver for SQL Server. DB_E_BADCOMPAREOP is returned for other comparisons.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "IRowsetFind comparability"
---
# Comparability for IRowsetFind
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

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
  
  
