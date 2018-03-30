---
title: "Metadata Discovery | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: ec3c0f4f-f838-43ce-85f2-cf2761e2aac5
caps.latest.revision: 13
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Metadata Discovery
  The metadata discovery improvement in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] allows [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client applications to ensure that column or parameter metadata returned from the execution of a query is identical to or compatible with the metadata format you specified before you executed the query. You will receive an error if the metadata returned after query execution is not compatible with the metadata format you specified before query execution.  
  
 In bcp and ODBC functions, and IBCPSession and IBCPSession2 interfaces, you can now specify a delayed read (delayed metadata discovery) to avoid metadata discovery for query out operations. This improves performance and eliminates metadata discovery failures.  
  
 If you develop an application using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] but connect to a server version earlier than [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], metadata discovery functionality will correspond to the version of the server.  
  
## Remarks  
 The following bcp functions have been enhanced in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] to provide improved metadata discovery:  
  
-   [bcp_columns](../../../2014/database-engine/dev-guide/bcp-columns.md)  
  
-   [bcp_control](../../../2014/database-engine/dev-guide/bcp-control.md)  
  
-   [bcp_getcolfmt](../../../2014/database-engine/dev-guide/bcp-getcolfmt.md)  
  
-   [bcp_readfmt](../../../2014/database-engine/dev-guide/bcp-readfmt.md)  
  
-   [bcp_setcolfmt](../../../2014/database-engine/dev-guide/bcp-setcolfmt.md)  
  
 You will also see a performance improvement when specifying metadata format using [bcp_setbulkmode](../../../2014/database-engine/dev-guide/bcp-setbulkmode.md).  
  
 [bcp_control](../../../2014/database-engine/dev-guide/bcp-control.md) has a new *eOption* to control the behavior of bcp_readfmt: `BCPDELAYREADFMT`.  
  
 The following ODBC functions have been enhanced in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] to provide improved metadata discovery:  
  
-   [SQLNumResultCols](../../../2014/database-engine/dev-guide/sqlnumresultcols.md)  
  
-   [SQLDescribeCol](../../../2014/database-engine/dev-guide/sqldescribecol.md)  
  
-   [SQLNumParams](../../../2014/database-engine/dev-guide/sqlnumparams.md)  
  
-   [SQLDescribeParam](../../../2014/database-engine/dev-guide/sqldescribeparam.md)  
  
 The following OLE DB member functions have been enhanced in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] to provide improved metadata discovery:  
  
-   IColumnsInfo::GetColumnInfo  
  
-   IColumnsRowset::GetColumnsRowset  
  
-   ICommandWithParameters::GetParameterInfo (see [ICommandWithParameters](../../../2014/database-engine/dev-guide/icommandwithparameters.md) for more information)  
  
 You will also see a performance improvement when specifying metadata format using IBCPSession::BCPSetBulkMode  
  
 The improved metadata discovery in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is possible because of the addition of two stored procedures in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]:  
  
-   sp_describe_first_result_set  
  
-   sp_describe_undeclared_parameters  
  
## See Also  
 [SQL Server Native Client Features](../../../2014/database-engine/dev-guide/sql-server-native-client-features.md)  
  
  