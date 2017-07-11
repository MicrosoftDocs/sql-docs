---
title: "SQLGetCursorName | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLGetCursorName function"
ms.assetid: 3a427a23-28ef-49aa-b9ec-6cab0914bdf3
caps.latest.revision: 31
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SQLGetCursorName
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  If the application does not specify a cursor name, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver generates one for the application upon cursor generation. The application can use **SQLGetCursorName** to retrieve the driver-defined cursor name for positioned UPDATE and DELETE statements. The application does not need to call **SQLSetCursorName** to take advantage of positioned data manipulation statements.  
  
## See Also  
 [SQLGetCursorName Function](http://go.microsoft.com/fwlink/?LinkId=59349)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  