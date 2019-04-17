---
title: "Using Autofetch with ODBC Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC cursors, autofetch"
  - "autofetch option"
  - "cursors [ODBC], autofetch"
ms.assetid: 57bd55f4-8945-4d8d-9f58-d30c81d2a514
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Autofetch with ODBC Cursors
  When connected to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports an autofetch option when using any server cursor type. With autofetch, the **SQLExecute** or **SQLExecDirect** function that opens the cursor also has an implicit [SQLFetchScroll](../../native-client-odbc-api/sqlfetchscroll.md)(SQL_FIRST) function. The rows comprising the first rowset are returned to the bound application variables as part of the statement execution, saving another roundtrip across the network to the server. [SQLGetData](../../native-client-odbc-api/sqlgetdata.md) is not supported when the autofetch option is enabled; the result set columns must be bound to program variables.  
  
 Applications request autofetch by setting the driver-specific SQL_SOPT_SS_CURSOR_OPTIONS statement attribute to SQL_CO_AF.  
  
## See Also  
 [Cursor Programming Details &#40;ODBC&#41;](cursor-programming-details-odbc.md)  
  
  
