---
title: "Retrieve Result Set Information (ODBC)"
description: "Processing Results - Retrieve Result Set Information"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "result sets [ODBC]"
  - "result sets [ODBC], fetching"
---
# Processing Results - Retrieve Result Set Information
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

    
### To get information about a result set  
  
1.  Call [SQLNumResultCols](../../relational-databases/native-client-odbc-api/sqlnumresultcols.md) to get the number of columns in the result set.  
  
2.  For each column in the result set:  
  
    -   Call [SQLDescribeCol](../../relational-databases/native-client-odbc-api/sqldescribecol.md) to get information about the result column.  
  
     Or  
  
    -   Call [SQLColAttribute](../../relational-databases/native-client-odbc-api/sqlcolattribute.md) to get specific descriptor information about the result column.  
  
## See Also  
[Process Results &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/processing-results-process-results.md)

[Determining the Characteristics of a Result Set &#40;ODBC&#41;](../../relational-databases/native-client-odbc-results/determining-the-characteristics-of-a-result-set-odbc.md)  
  
  
