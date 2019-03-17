---
title: "Bulk Copy Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, bulk copy"
  - "bulk copy [ODBC], functions"
  - "ODBC, bulk copy operations"
  - "functions [ODBC]"
ms.assetid: 6526b892-1d58-4f55-8335-f09887f6ea02
author: MightyPen
ms.author: genemi
manager: craigg
---
# Bulk Copy Functions
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific bulk-copy API extension of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver allows client applications to rapidly add data rows to, or extract data rows from, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 When using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, you can reference the bulk copy functions (BCP) in SQLNCLI11.LIB and SQLNCLI.H.  
  
 An application that uses BCP API function calls should link with the library (.lib) that shipped with the driver (.dll) used by the application. A BCP application should not link with more than one driver library.  
  
## In This Section  
  
-   [bcp_batch](bcp-batch.md)  
  
-   [bcp_bind](bcp-bind.md)  
  
-   [bcp_colfmt](bcp-colfmt.md)  
  
-   [bcp_collen](bcp-collen.md)  
  
-   [bcp_colptr](bcp-colptr.md)  
  
-   [bcp_columns](bcp-columns.md)  
  
-   [bcp_control](bcp-control.md)  
  
-   [bcp_done](bcp-done.md)  
  
-   [bcp_exec](bcp-exec.md)  
  
-   [bcp_getcolfmt](bcp-getcolfmt.md)  
  
-   [bcp_gettypename](bcp-gettypename.md)  
  
-   [bcp_init](bcp-init.md)  
  
-   [bcp_moretext](bcp-moretext.md)  
  
-   [bcp_readfmt](bcp-readfmt.md)  
  
-   [bcp_sendrow](bcp-sendrow.md)  
  
-   [bcp_setbulkmode](bcp-setbulkmode.md)  
  
-   [bcp_setcolfmt](bcp-setcolfmt.md)  
  
-   [bcp_writefmt](bcp-writefmt.md)  
  
## See Also  
 [SQL Server Driver Extensions](../../database-engine/dev-guide/sql-server-driver-extensions.md)   
 [Performing Bulk Copy Operations &#40;ODBC&#41;](../native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md)  
  
  
