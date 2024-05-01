---
title: "Comparability for IRowsetFind"
description: "SQL Server Native Client Comparability for IRowsetFind"
author: markingmyname
ms.author: maghan
ms.date: "03/04/2017"
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "IRowsetFind comparability [ODBC]"
---
# SQL Server Native Client Comparability for IRowsetFind
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

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
 [Date and Time Improvements &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-date-time/date-and-time-improvements-ole-db.md)  
  
  
