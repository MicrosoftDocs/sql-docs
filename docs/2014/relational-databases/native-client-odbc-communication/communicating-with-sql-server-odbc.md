---
title: "Communicating with SQL Server (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, communicating with SQL Server"
  - "ODBC applications, communicating with SQL Server"
  - "ODBC, communicating with SQL Server"
ms.assetid: cca3dcf0-2a7e-465a-84de-7ce055360eb6
author: MightyPen
ms.author: genemi
manager: craigg
---
# Communicating with SQL Server (ODBC)
  For an ODBC application to communicate with an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it must allocate environment and connection handles and connect to the data source. After a connection is established, the application can send queries to the server and process any result sets. When the application has finished using the data source, it disconnects from the data source and frees the connection handle. When the application has freed all its connection handles, it frees the environment handle.  
  
 An application can connect to any number of data sources. The application can use a combination of drivers and data sources, the same driver and a combination of data sources, or even the same driver and multiple connections to the same data source.  
  
 You can download [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC samples from the [SQL Server Downloads](https://go.microsoft.com/fwlink/?LinkId=62796) page on MSDN.  
  
## In This Section  
  
-   [Allocating an Environment Handle](allocating-an-environment-handle.md)  
  
-   [Allocating a Connection Handle](allocating-a-connection-handle.md)  
  
-   [SQL Server Native Client ODBC Data Sources](../../integration-services/connection-manager/data-sources.md)  
  
-   [Connecting to a Data Source &#40;ODBC&#41;](connecting-to-a-data-source-odbc.md)  
  
-   [Disconnecting from a Data Source](disconnecting-from-a-data-source.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../native-client/odbc/sql-server-native-client-odbc.md)   
 [SQLSetEnvAttr](../native-client-odbc-api/sqlsetenvattr.md)  
  
  
