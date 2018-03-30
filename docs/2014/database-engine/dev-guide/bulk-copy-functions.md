---
title: "Bulk Copy Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, bulk copy"
  - "bulk copy [ODBC], functions"
  - "ODBC, bulk copy operations"
  - "functions [ODBC]"
ms.assetid: 6526b892-1d58-4f55-8335-f09887f6ea02
caps.latest.revision: 39
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Bulk Copy Functions
  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific bulk-copy API extension of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver allows client applications to rapidly add data rows to, or extract data rows from, a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.  
  
 When using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, you can reference the bulk copy functions (BCP) in SQLNCLI11.LIB and SQLNCLI.H.  
  
 An application that uses BCP API function calls should link with the library (.lib) that shipped with the driver (.dll) used by the application. A BCP application should not link with more than one driver library.  
  
## In This Section  
  
-   [bcp_batch](../../../2014/database-engine/dev-guide/bcp-batch.md)  
  
-   [bcp_bind](../../../2014/database-engine/dev-guide/bcp-bind.md)  
  
-   [bcp_colfmt](../../../2014/database-engine/dev-guide/bcp-colfmt.md)  
  
-   [bcp_collen](../../../2014/database-engine/dev-guide/bcp-collen.md)  
  
-   [bcp_colptr](../../../2014/database-engine/dev-guide/bcp-colptr.md)  
  
-   [bcp_columns](../../../2014/database-engine/dev-guide/bcp-columns.md)  
  
-   [bcp_control](../../../2014/database-engine/dev-guide/bcp-control.md)  
  
-   [bcp_done](../../../2014/database-engine/dev-guide/bcp-done.md)  
  
-   [bcp_exec](../../../2014/database-engine/dev-guide/bcp-exec.md)  
  
-   [bcp_getcolfmt](../../../2014/database-engine/dev-guide/bcp-getcolfmt.md)  
  
-   [bcp_gettypename](../../../2014/database-engine/dev-guide/bcp-gettypename.md)  
  
-   [bcp_init](../../../2014/database-engine/dev-guide/bcp-init.md)  
  
-   [bcp_moretext](../../../2014/database-engine/dev-guide/bcp-moretext.md)  
  
-   [bcp_readfmt](../../../2014/database-engine/dev-guide/bcp-readfmt.md)  
  
-   [bcp_sendrow](../../../2014/database-engine/dev-guide/bcp-sendrow.md)  
  
-   [bcp_setbulkmode](../../../2014/database-engine/dev-guide/bcp-setbulkmode.md)  
  
-   [bcp_setcolfmt](../../../2014/database-engine/dev-guide/bcp-setcolfmt.md)  
  
-   [bcp_writefmt](../../../2014/database-engine/dev-guide/bcp-writefmt.md)  
  
## See Also  
 [SQL Server Driver Extensions](../../../2014/database-engine/dev-guide/sql-server-driver-extensions.md)   
 [Performing Bulk Copy Operations &#40;ODBC&#41;](../../../2014/database-engine/dev-guide/performing-bulk-copy-operations-odbc.md)  
  
  