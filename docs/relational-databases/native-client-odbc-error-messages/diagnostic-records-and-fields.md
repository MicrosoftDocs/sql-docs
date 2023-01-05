---
description: "Diagnostic Records and Fields"
title: "Diagnostic Records and Fields | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "header records [ODBC]"
  - "SQL Server Native Client ODBC driver, errors"
  - "messages [ODBC], diagnostic records"
  - "ODBC error handling, diagnostic records"
  - "SQLGetDiagField function"
  - "diagnostic records [ODBC]"
  - "errors [ODBC], diagnostic records"
  - "fields [ODBC]"
  - "status information [ODBC]"
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Diagnostic Records and Fields
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Diagnostic records are associated with ODBC environment, connection, statement, or descriptor handles. When any ODBC function raises a return code other than SQL_SUCCESS or SQL_INVALID_HANDLE, the handle called by the function has associated diagnostic records that contain informational or error messages. These records are retained until another function is called using that handle, at which time they are discarded. There is no limit to the number of diagnostic records that can be associated with a handle at any one time.  
  
 There are two types of diagnostic records: header and status. The header record is record 0; when there are status records, they are records 1 and later. Diagnostic records contain different fields for the header record and the status records. ODBC components can also define their own diagnostic record fields.  
  
 Fields in the header record contain general information about a function's execution, including the return code, row count, number of status records, and type of statement executed. The header record is always created unless an ODBC function returns SQL_INVALID_HANDLE. For a complete list of fields in the header record, see [SQLGetDiagField](../../relational-databases/native-client-odbc-api/sqlgetdiagfield.md).  
  
 Fields in the status records contain information about specific errors or warnings returned by the ODBC Driver Manager, driver, or data source, including the SQLSTATE, native error number, diagnostic message, column number, and row number. Status records are created only if the function returns SQL_ERROR, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_NEED_DATA, or SQL_STILL_EXECUTING. For a complete list of fields in the status records, see **SQLGetDiagField**.  
  
 **SQLGetDiagRec** retrieves a single diagnostic record along with its ODBC SQLSTATE, native error number, and diagnostic-message fields. This functionality is similar to the ODBC 2._x_**SQLError** function. The simplest error-handling function in ODBC 3.*x* is to repeatedly call **SQLGetDiagRec** starting with the *RecNumber* parameter set to 1 and incrementing *RecNumber* by 1 until **SQLGetDiagRec** returns SQL_NO_DATA. This is equivalent to an ODBC 2.*x* application calling **SQLError** until it returns SQL_NO_DATA_FOUND.  
  
 ODBC 3.*x* supports much more diagnostic information than ODBC 2.*x*. This information is stored in additional fields in diagnostic records retrieved by using **SQLGetDiagField**.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver has driver-specific diagnostic fields that can be retrieved with **SQLGetDiagField**. Labels for these driver-specific fields are defined in sqlncli.h. Use these labels to retrieve the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] state, severity level, server name, procedure name, and line number associated with each diagnostic record. Also, sqlncli.h contains definitions of the codes the driver uses to identify Transact-SQL statements if an application calls **SQLGetDiagField** with *DiagIdentifier* set to SQL_DIAG_DYNAMIC_FUNCTION_CODE.  
  
 **SQLGetDiagField** is processed by the ODBC Driver Manager using error information it caches from the underlying driver. The ODBC Driver Manager does not cache driver-specific diagnostic fields until after a successful connection has been made. **SQLGetDiagField** returns SQL_ERROR if it is called to get driver-specific diagnostic fields before a successful connection has been completed. If an ODBC connect function returns SQL_SUCCESS_WITH_INFO, the driver-specific diagnostic fields for the connect function are not yet available. You can start calling **SQLGetDiagField** for driver-specific diagnostic fields only after you have made another ODBC function call after the connect function.  
  
 Most errors reported by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver can be effectively diagnosed using only the information returned by **SQLGetDiagRec**. In some cases, however, the information returned by the driver-specific diagnostic fields is important in diagnosing an error. When coding an ODBC error handler for applications using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, it is a good idea to also use **SQLGetDiagField** to retrieve at least the SQL_DIAG_SS_MSGSTATE and SQL_DIAG_SS_SEVERITY driver-specific fields. If a particular error can be raised at several locations in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] code, SQL_DIAG_SS_MSGSTATE indicates to a Microsoft support engineer specifically where an error was raised, which sometimes aids in diagnosing a problem.  
  
## See Also  
 [Handling Errors and Messages](../../relational-databases/native-client-odbc-error-messages/handling-errors-and-messages.md)  
  
  
