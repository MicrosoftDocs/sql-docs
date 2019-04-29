---
title: "SQLNativeSql | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLNativeSql function"
ms.assetid: 2d999fec-9e22-4514-ad5f-22a64b82f95b
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLNativeSql
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver satisfies **SQLNativeSql** requests without visiting the server. The function efficiently tests the syntax of SQL statements. Syntax checking does not determine if identifiers or the results of expressions in the SQL statements are valid, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native SQL returned by **SQLNativeSql** can fail to run.  
  
## See Also  
 [SQLNativeSql Function](https://go.microsoft.com/fwlink/?LinkID=59358)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
