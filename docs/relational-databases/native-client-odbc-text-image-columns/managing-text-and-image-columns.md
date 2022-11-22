---
description: "Managing Text and Image Columns"
title: "Managing Text and Image Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "text columns [ODBC]"
  - "SQL Server Native Client ODBC driver, image columns"
  - "SQL Server Native Client ODBC driver, text columns"
  - "data types [ODBC], text"
  - "columns [ODBC]"
  - "ODBC data types, image columns"
  - "data types [ODBC], mapping"
  - "ODBC data types, text columns"
  - "image columns [ODBC]"
ms.assetid: 7b543556-ff36-4d35-ac08-de96223d92cd
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Managing Text and Image Columns
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **text**, **ntext**, and **image** data (also referred to as long data) are character or binary string data types that can hold data values too large to fit into **char**, **varchar**, **binary**, or **varbinary** columns. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **text** data type maps to the ODBC SQL_LONGVARCHAR data type; **ntext** maps to SQL_WLONGVARCHAR; and **image** maps to SQL_LONGVARBINARY. Some data items, such as long documents or large bitmaps, may be too large to store reasonably in memory. To retrieve long data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in sequential parts, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver enables an application to call [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md). To send long data in sequential parts, the application can call [SQLPutData](../../relational-databases/native-client-odbc-api/sqlputdata.md). Parameters for which data is sent at execution time are known as data-at-execution parameters.  
  
 An application can actually write or retrieve any type of data (not just long data) with **SQLPutData** or **SQLGetData**, although only **character** and **binary** data can be sent or retrieved in parts. However, if the data is small enough to fit in a single buffer, there is generally no reason to use **SQLPutData** or **SQLGetData**. It is much easier to bind the single buffer to the parameter or column.  
  
## In This Section  
  
-   [Bound vs. Unbound Text and Image Columns](../../relational-databases/native-client-odbc-text-image-columns/bound-vs-unbound-text-and-image-columns.md)  
  
-   [Logged vs. Unlogged Modifications](../../relational-databases/native-client-odbc-text-image-columns/logged-vs-unlogged-modifications.md)  
  
-   [Data-at-Execution and Text, ntext, or Image Columns](../../relational-databases/native-client-odbc-text-image-columns/data-at-execution-and-text-ntext-or-image-columns.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)  
  
  
