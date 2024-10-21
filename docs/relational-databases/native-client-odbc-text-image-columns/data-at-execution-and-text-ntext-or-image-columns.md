---
title: "Data-at-Execution and Text, ntext, Image"
description: "Data-at-Execution and Text, ntext, or Image Columns"
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "text columns [ODBC]"
  - "SQL Server Native Client ODBC driver, image columns"
  - "SQL Server Native Client ODBC driver, text columns"
  - "data types [ODBC], image"
  - "data types [ODBC], text"
  - "columns [ODBC]"
  - "ODBC data types, image columns"
  - "ODBC data types, text columns"
  - "data-at-execution"
  - "ODBC data-at-execution"
  - "image columns [ODBC]"
---
# Data-at-Execution and Text, ntext, or Image Columns
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  ODBC data-at-execution is a feature that enables applications to work with extremely large amounts of data on bound columns or parameters. When retrieving very large **text**, **ntext**, or **image** columns, an application may not be able to simply allocate a huge buffer, bind the column into the buffer, and fetch the row. When updating very large **text**, **ntext**, or **image** columns, the application might not be able to simply allocate a huge buffer, bind it to a parameter marker in a SQL statement, and then execute the statement. In these cases, the application must use [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md) or [SQLPutData](../../relational-databases/native-client-odbc-api/sqlputdata.md) with its data-at-execution options.  
  
## See Also  
 [Managing Text and Image Columns](../../relational-databases/native-client-odbc-text-image-columns/managing-text-and-image-columns.md)  
  
  
