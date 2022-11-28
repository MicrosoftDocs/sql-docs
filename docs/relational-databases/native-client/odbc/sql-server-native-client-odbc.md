---
title: "ODBC"
description: SQL Server supports ODBC, by using the SQL Server Native Client ODBC driver, as a native API for C and C++ applications that communicate with SQL Server.
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLNCLI, ODBC"
  - "SQL Server Native Client ODBC driver, about SQL Server Native Client ODBC driver"
  - "data access [SQL Server Native Client], ODBC"
  - "SQL Server Native Client ODBC driver"
  - "ODBC"
  - "SQL Server Native Client, ODBC"
  - "ODBC, about SQL Server Native Client ODBC driver"
ms.assetid: 811d5ba3-a2b8-48c0-adbc-8c91f041f458
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client (ODBC)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  ODBC is a standard definition of an application programming interface (API) used to access data in relational or indexed sequential access method (ISAM) databases. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports ODBC, via the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver, as one of the native APIs for writing C and C++ applications that communicate with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] programs that are written using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver communicate with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] through C function calls. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific versions of the ODBC functions are implemented in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver. The driver passes SQL statements to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and returns the results of the statements to the application.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver complies with the Microsoft Win32 ODBC 3.51 specification. The driver supports applications written using earlier versions of ODBC in the manner defined in the ODBC 3.51 specification.  
  
## In This Section  
  
-   [Data Source Names and 64-Bit Operating Systems](../../../relational-databases/native-client/odbc/data-source-names-and-64-bit-operating-systems.md)  
  
-   [Creating a SQL Server Native Client ODBC Driver Application](../../../relational-databases/native-client/odbc/creating-a-driver-application.md)  
  
-   [Communicating with SQL Server &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-communication/communicating-with-sql-server-odbc.md)  
  
-   [Executing Queries &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-queries/executing-queries-odbc.md)  
  
-   [Processing Results &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-results/processing-results-odbc.md)  
  
-   [Using Cursors &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md)  
  
-   [Performing Transactions &#40;ODBC&#41;](./performing-transactions-in-odbc.md)  
  
-   [Handling Errors and Messages](../../../relational-databases/native-client-odbc-error-messages/handling-errors-and-messages.md)  
  
-   [Running Stored Procedures](../../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)  
  
-   [Using Catalog Functions](../../../relational-databases/native-client/odbc/using-catalog-functions.md)  
  
-   [Performing Bulk Copy Operations &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md)  
  
-   [Managing Text and Image Columns](../../../relational-databases/native-client-odbc-text-image-columns/managing-text-and-image-columns.md)  
  
-   [Profiling ODBC Driver Performance](../../../relational-databases/native-client/odbc/profiling-odbc-driver-performance.md)  
  
-   [Table-Valued Parameters &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)  
  
-   [Date and Time Improvements &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md)  
  
-   [Large CLR User-Defined Types &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md)  
  
-   [FILESTREAM Support &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/filestream-support-odbc.md)  
  
-   [Service Principal Names &#40;SPNs&#41; in Client Connections &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md)  
  
-   [Sparse Columns Support &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/sparse-columns-support-odbc.md)  
  
-   [SQL Server Native Client &#40;ODBC&#41; Reference]()  
  
-   [ODBC How-to Topics](../../../relational-databases/native-client-odbc-how-to/odbc-how-to-topics.md)  
  
## See Also  
 [SQL Server Native Client Programming](../../../relational-databases/native-client/sql-server-native-client-programming.md)   
 [Installing SQL Server Native Client](../../../relational-databases/native-client/applications/installing-sql-server-native-client.md)  
  
