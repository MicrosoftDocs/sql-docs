---
title: "Retrieve Result Set Information (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "result sets [ODBC]"
  - "result sets [ODBC], fetching"
ms.assetid: 34f235e4-f80b-4123-8764-9deb18506f14
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Processing Results - Retrieve Result Set Information
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

    
### To get information about a result set  
  
1.  Call [SQLNumResultCols](../../relational-databases/native-client-odbc-api/sqlnumresultcols.md) to get the number of columns in the result set.  
  
2.  For each column in the result set:  
  
    -   Call [SQLDescribeCol](../../relational-databases/native-client-odbc-api/sqldescribecol.md) to get information about the result column.  
  
     Or  
  
    -   Call [SQLColAttribute](../../relational-databases/native-client-odbc-api/sqlcolattribute.md) to get specific descriptor information about the result column.  
  
## See Also  
[Process Results &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/processing-results-process-results.md)

[Determining the Characteristics of a Result Set &#40;ODBC&#41;](../../relational-databases/native-client-odbc-results/determining-the-characteristics-of-a-result-set-odbc.md)  
  
  