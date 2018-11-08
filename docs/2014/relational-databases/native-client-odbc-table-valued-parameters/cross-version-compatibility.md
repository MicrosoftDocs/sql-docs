---
title: "Cross-Version Compatibility | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC), cross-version compatibility"
ms.assetid: 5f14850b-b85c-41e2-8116-6f5b3f5e0856
author: MightyPen
ms.author: genemi
manager: craigg
---
# Cross-Version Compatibility
  Cross-version conflicts can occur when client or server instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] are expected to process table-valued parameters.  
  
 In general, table-valued parameter functionality is only available to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] clients (using SQL Server Native Client 10.0) or later that are connected to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later) servers. New columns in catalog function result sets will only be present when connected to a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later) server.  
  
 If a client application compiled with an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client executes statements that expect table-valued parameters, the server detects this condition through a data conversion error, and ODBC returns this as a SQLSTATE 07006 and the message "Restricted data type attribute violation".  
  
 If a client application that was compiled with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client 10.0 or later tries to use table-valued parameters when connected to a server instance earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client will detect this, and SQLBindCol, SQLBindParameter, SQLSetDescFields, and SQLSetDescRec calls will fail with SQLSTATE 07006 and the message "Restricted data type attribute violation (the version of SQL Server for this connection does not support table-valued parameters)".  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](table-valued-parameters-odbc.md)  
  
  
