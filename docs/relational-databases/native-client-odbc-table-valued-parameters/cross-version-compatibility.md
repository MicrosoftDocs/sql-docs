---
title: "Cross-Version Compatibility"
description: "Cross-Version Compatibility"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "table-valued parameters (ODBC), cross-version compatibility"
---
# Cross-Version Compatibility
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Cross-version conflicts can occur when client or server instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] are expected to process table-valued parameters.  
  
 In general, table-valued parameter functionality is only available to [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] clients (using SQL Server Native Client 10.0) or later that are connected to [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] (or later) servers. New columns in catalog function result sets will only be present when connected to a [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] (or later) server.  
  
 If a client application compiled with an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client executes statements that expect table-valued parameters, the server detects this condition through a data conversion error, and ODBC returns this as a SQLSTATE 07006 and the message "Restricted data type attribute violation".  
  
 If a client application that was compiled with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client 10.0 or later tries to use table-valued parameters when connected to a server instance earlier than [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client will detect this, and SQLBindCol, SQLBindParameter, SQLSetDescFields, and SQLSetDescRec calls will fail with SQLSTATE 07006 and the message "Restricted data type attribute violation (the version of SQL Server for this connection does not support table-valued parameters)".  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)  
  
  
