---
description: "SQL Server Native Client ODBC Data Sources"
title: "ODBC Data Sources"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC data sources, about data sources"
  - "ODBC data sources, names"
  - "data sources [SQL Server Native Client]"
  - "names [ODBC]"
  - "ODBC applications, data sources"
  - "SQL Server Native Client ODBC driver, data sources"
  - "ODBC data sources"
ms.assetid: a6a50fd0-d439-43fd-b76f-16ec02f478c5
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client ODBC Data Sources
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source name (DSN) identifies an ODBC data source containing all of the information that an ODBC application needs to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database on a specific server. There are two ways you can define an ODBC data source name:  
  
-   On a client computer, open Administrative Tools in Control Panel, and double-click **Data Sources (ODBC)**. This will open the ODBC Data Source Administrator, which you can use to create a DSN.  
  
-   In an ODBC application, call [SQLConfigDataSource](../../relational-databases/native-client-odbc-api/sqlconfigdatasource.md).  
  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source contains:  
  
-   The name of the data source.  
  
-   Any information needed to connect to a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   The default database to use on a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (optional).  
  
-   Settings such as which ANSI options to use, whether to log performance statistics, and so on (optional).  
  
 An ODBC application is not required to connect through a data source. However, the application must provide the same connectivity information to an ODBC connect function that the driver would otherwise find in a DSN.  
  
## See Also  
 [Communicating with SQL Server &#40;ODBC&#41;](../../relational-databases/native-client-odbc-communication/communicating-with-sql-server-odbc.md)  
  
  
