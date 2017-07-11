---
title: "Comparability for IRowsetFind | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "IRowsetFind comparability [ODBC]"
ms.assetid: 7d148b56-9bbe-4e55-b31f-43f115705402
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Comparability for IRowsetFind
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

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
  
  