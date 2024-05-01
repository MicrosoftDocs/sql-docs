---
title: "Using Autofetch with ODBC Cursors"
description: "Using Autofetch with ODBC Cursors"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "ODBC cursors, autofetch"
  - "autofetch option"
  - "cursors [ODBC], autofetch"
---
# Using Autofetch with ODBC Cursors
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  When connected to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports an autofetch option when using any server cursor type. With autofetch, the **SQLExecute** or **SQLExecDirect** function that opens the cursor also has an implicit [SQLFetchScroll](../../../relational-databases/native-client-odbc-api/sqlfetchscroll.md)(SQL_FIRST) function. The rows comprising the first rowset are returned to the bound application variables as part of the statement execution, saving another roundtrip across the network to the server. [SQLGetData](../../../relational-databases/native-client-odbc-api/sqlgetdata.md) is not supported when the autofetch option is enabled; the result set columns must be bound to program variables.  
  
 Applications request autofetch by setting the driver-specific SQL_SOPT_SS_CURSOR_OPTIONS statement attribute to SQL_CO_AF.  
  
## See Also  
 [Cursor Programming Details &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-cursors/programming/cursor-programming-details-odbc.md)  
  
  
