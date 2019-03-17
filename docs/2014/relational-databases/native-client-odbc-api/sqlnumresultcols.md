---
title: "SQLNumResultCols | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLNumResultCols function"
ms.assetid: f79d8b3c-521e-4e50-a564-21d73ae5767b
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLNumResultCols
  For executed statements, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does not visit the server to report the number of columns in a result set. In this case, `SQLNumResultCols` does not cause a server roundtrip. Like [SQLDescribeCol](sqldescribecol.md) and [SQLColAttribute](sqlcolattribute.md), calling `SQLNumResultCols` on prepared but not executed statements generates a server roundtrip.  
  
 When a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or statement batch returns multiple result row sets, it is possible for the number of result set columns to change from one set to another. `SQLNumResultCols` should be called for each set. When the number of columns changes, the application should rebind data values prior to fetching row results. For more information about handling multiple result set returns, see [SQLMoreResults](sqlmoreresults.md).  
  
 Improvements in the database engine starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] allow SQLNumResultCols to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by SQLNumResultCols in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../native-client/features/metadata-discovery.md).  
  
## See Also  
 [SQLNumResultCols Function](https://go.microsoft.com/fwlink/?LinkId=59359)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
