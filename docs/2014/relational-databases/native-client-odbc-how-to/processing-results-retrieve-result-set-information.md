---
title: "Retrieve Result Set Information (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "result sets [ODBC]"
  - "result sets [ODBC], fetching"
ms.assetid: 34f235e4-f80b-4123-8764-9deb18506f14
author: MightyPen
ms.author: genemi
manager: craigg
---
# Retrieve Result Set Information (ODBC)
    
### To get information about a result set  
  
1.  Call [SQLNumResultCols](../native-client-odbc-api/sqlnumresultcols.md) to get the number of columns in the result set.  
  
2.  For each column in the result set:  
  
    -   Call [SQLDescribeCol](../native-client-odbc-api/sqldescribecol.md) to get information about the result column.  
  
     Or  
  
    -   Call [SQLColAttribute](../native-client-odbc-api/sqlcolattribute.md) to get specific descriptor information about the result column.  
  
## See Also  
 [Processing Results How-to Topics &#40;ODBC&#41;](../../database-engine/dev-guide/processing-results-how-to-topics-odbc.md)   
 [Determining the Characteristics of a Result Set &#40;ODBC&#41;](../native-client-odbc-results/determining-the-characteristics-of-a-result-set-odbc.md)  
  
  
