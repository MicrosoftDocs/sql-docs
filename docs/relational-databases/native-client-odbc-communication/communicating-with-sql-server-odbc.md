---
title: "Communicating with SQL Server (ODBC) | Microsoft Docs"
description: Learn how an ODBC application communicates with an instance of SQL Server by using connections and connection resources.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, communicating with SQL Server"
  - "ODBC applications, communicating with SQL Server"
  - "ODBC, communicating with SQL Server"
ms.assetid: cca3dcf0-2a7e-465a-84de-7ce055360eb6
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Communicating with SQL Server (ODBC)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  For an ODBC application to communicate with an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it must allocate environment and connection handles and connect to the data source. After a connection is established, the application can send queries to the server and process any result sets. When the application has finished using the data source, it disconnects from the data source and frees the connection handle. When the application has freed all its connection handles, it frees the environment handle.  
  
 An application can connect to any number of data sources. The application can use a combination of drivers and data sources, the same driver and a combination of data sources, or even the same driver and multiple connections to the same data source.  
  
 You can download [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC samples from the [SQL Server Downloads](../../connect/odbc/download-odbc-driver-for-sql-server.md).  
  
## In This Section  
  
-   [Allocating an Environment Handle](../../relational-databases/native-client-odbc-communication/allocating-an-environment-handle.md)  
  
-   [Allocating a Connection Handle](../../relational-databases/native-client-odbc-communication/allocating-a-connection-handle.md)  
  
-   [SQL Server Native Client ODBC Data Sources](../../relational-databases/native-client-odbc-communication/sql-server-native-client-odbc-data-sources.md)  
  
-   [Connecting to a Data Source &#40;ODBC&#41;](../../relational-databases/native-client-odbc-communication/connecting-to-a-data-source-odbc.md)  
  
-   [Disconnecting from a Data Source](../../relational-databases/native-client-odbc-communication/disconnecting-from-a-data-source.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)   
 [SQLSetEnvAttr](../../relational-databases/native-client-odbc-api/sqlsetenvattr.md)  
  
