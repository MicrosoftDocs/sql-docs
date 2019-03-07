---
title: "Allocating a Connection Handle | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC applications, passwords"
  - "ODBC applications, connections"
  - "handles [SQL Server Native Client]"
  - "expiration [SQL Server Native Client]"
  - "passwords [SQL Server], modifying"
  - "SQL Server Native Client ODBC driver, connection handles"
  - "connection handles [SQL Server Native Client]"
  - "modifying passwords"
  - "SQLAllocHandle function"
ms.assetid: 471d8a31-199c-4f92-bb10-004fc7733b35
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Allocating a Connection Handle
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Before the application can connect to a data source or driver, it must allocate a connection handle. This is done by calling **SQLAllocHandle** with the *HandleType* parameter set to SQL_HANDLE_DBC and *InputHandle* pointing to an initialized environment handle.  
  
 The characteristics of the connection are controlled by setting connection attributes. For example, because transactions occur at the connection level, the transaction isolation level is a connection attribute. Similarly, the login time-out, or number of seconds to wait while trying to connect before timing out, is a connection attribute.  
  
 Connection attributes are set with [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md), and their current settings are retrieved with [SQLGetConnectAttr](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md). If **SQLSetConnectAttr** is called before a connection is attempted, the ODBC Driver Manager stores the attributes in its connection structure and sets them in the driver as part of the connection process. Some connection attributes must be set before the application attempts to connect; others can be set after the connection has completed. For example, SQL_ATTR_ODBC_CURSORS must be set before a connection is made, but SQL_ATTR_AUTOCOMMIT can be set after connecting.  
  
 Applications running against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 or later can sometimes improve their performance by resetting the tabular data stream (TDS) network packet size. The default packet size is set at the server, at 4 KB. A packet size of 4 KB to 8 KB generally gives the best performance. If testing shows that it performs better with a different packet size, the application can reset the packet size. ODBC applications can do this before connecting by calling **SQLSetConnectAttr** with the SQL_ATTR_PACKET_SIZE option. Some applications perform better with a larger packet size, but performance improvements are generally minimal for packet sizes larger than 8 KB.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver has a number of extended connection attributes that an application can use to increase its functionality. Some of these attributes control the same options that can be specified in data sources and used to override whatever option is set in a data source. For example, if an application uses quoted identifiers, it can set the driver-specific attribute SQL_COPT_SS_QUOTED_IDENT to SQL_QI_ON to ensure this option is always set regardless of the setting in any data source.  
  
## See Also  
 [Communicating with SQL Server &#40;ODBC&#41;](../../relational-databases/native-client-odbc-communication/communicating-with-sql-server-odbc.md)  
  
  
