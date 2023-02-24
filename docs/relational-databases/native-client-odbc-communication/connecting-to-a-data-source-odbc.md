---
description: "Connecting to a Data Source (ODBC)"
title: "Connecting to a Data Source (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "checking connection states"
  - "ODBC data sources, connections"
  - "data sources [SQL Server Native Client]"
  - "SQLBrowseConnect function"
  - "ODBC applications, connections"
  - "ODBC applications, data sources"
  - "connections [SQL Server Native Client]"
  - "SQLConnect function"
  - "SQLDriveConnect function"
  - "verifying connection states"
  - "SQL Server Native Client ODBC driver, data sources"
  - "SQL Server Native Client ODBC driver, connections"
ms.assetid: ae30dd1d-06ae-452b-9618-8fd8cd7ba074
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Connecting to a Data Source (ODBC)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  After allocating environment and connection handles and setting any connection attributes, the application connects to the data source or driver. There are three functions you can use to connect:  
  
-   **SQLConnect**  
  
-   **SQLDriverConnect**  
  
-   **SQLBrowseConnect**  
  
 For more information about making connections to a data source, including the various connection string options available, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
## SQLConnect  
 **SQLConnect** is the simplest connection function. It accepts three parameters: a data source name, a user ID, and a password. Use **SQLConnect** when these three parameters contain all the information needed to connect to the database. To do this, build a list of data sources using **SQLDataSources**; prompt the user for a data source, user ID, and password; and then call **SQLConnect**.  
  
 **SQLConnect** assumes that a data source name, user ID, and password are sufficient to connect to a data source and that the ODBC data source contains all other information the ODBC driver needs to make the connection. Unlike [SQLDriverConnect](../../relational-databases/native-client-odbc-api/sqldriverconnect.md) and [SQLBrowseConnect](../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md), **SQLConnect** does not use a connection string.  
  
## SQLDriverConnect  
 **SQLDriverConnect** is used when more information than the data source name, user ID, and password is required. One of the parameters to **SQLDriverConnect** is a connection string containing driver-specific information. You might use **SQLDriverConnect** instead of **SQLConnect** for the following reasons:  
  
-   To specify driver-specific information at connect time.  
  
-   To request that the driver prompt the user for connection information.  
  
-   To connect without using an ODBC data source.  
  
 The **SQLDriverConnect** connection string contains a series of keyword-value pairs that specify all connection information supported by an ODBC driver. Each driver supports the standard ODBC keywords (DSN, FILEDSN, DRIVER, UID, PWD, and SAVEFILE) in addition to driver-specific keywords for all connection information supported by the driver. **SQLDriverConnect** can be used to connect without a data source. For example, an application that is designed to make a "DSN-less" connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can call **SQLDriverConnect** with a connection string that defines the login ID, password, network library, server name to connect to, and default database to use.  
  
 When using **SQLDriverConnect**, there are two options for prompting the user for any needed connection information:  
  
-   Application dialog box  
  
     You can create an application dialog box that prompts for connection information, and then calls **SQLDriverConnect** with a NULL window handle and *DriverCompletion* set to SQL_DRIVER_NOPROMPT. These parameter settings prevent the ODBC driver from opening its own dialog box. This method is used when it is important to control the user interface of the application.  
  
-   Driver dialog box  
  
     You can code the application to pass a valid window handle to **SQLDriverConnect** and set the *DriverCompletion* parameter to SQL_DRIVER_COMPLETE, SQL_DRIVER_PROMPT, or SQL_DRIVER_COMPLETE_REQUIRED. The driver then generates a dialog box to prompt the user for connection information. This method simplifies the application code.  
  
## SQLBrowseConnect  
 **SQLBrowseConnect**, like **SQLDriverConnect**, uses a connection string. However, by using **SQLBrowseConnect**, an application can construct a complete connection string iteratively with the data source at run time. This allows the application to do two things:  
  
-   Build its own dialog boxes to prompt for this information, thereby retaining control over its user interface.  
  
-   Browse the system for data sources that can be used by a particular driver, possibly in several steps.  
  
     For example, the user might first browse the network for servers and, after choosing a server, browse the server for databases accessible by the driver.  
  
 When **SQLBrowseConnect** completes a successful connection, it returns a connection string that can be used on subsequent calls to **SQLDriverConnect**.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver always returns SQL_SUCCESS_WITH_INFO on a successful **SQLConnect**, **SQLDriverConnect**, or **SQLBrowseConnect**. When an ODBC application calls **SQLGetDiagRec** after getting SQL_SUCCESS_WITH_INFO, it can receive the following messages:  
  
 5701  
 Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] put the user's context into the default database defined in the data source, or into the default database defined for the login ID used in the connection if the data source did not have a default database.  
  
 5703  
 Indicates the language being used on the server.  
  
 The following example shows the message returned on a successful connection by the system administrator:  
  
```  
szSqlState = "01000", *pfNativeError = 5701,  
szErrorMsg="[Microsoft][SQL Server Native Client][SQL Server]  
       Changed database context to 'pubs'."  
szSqlState = "01000", *pfNativeError = 5703,  
szErrorMsg="[Microsoft][SQL Server Native Client][SQL Server]  
       Changed language setting to 'us_english'."  
```  
  
 You can ignore messages 5701 and 5703; they are only informational. You should not, however, ignore a SQL_SUCCESS_WITH_INFO return code because messages other than 5701 or 5703 may be returned. For example, if a driver connects to a server running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with outdated catalog stored procedures, one of the errors returned through **SQLGetDiagRec** after a SQL_SUCCESS_WITH_INFO is:  
  
```  
SqlState:   01000  
pfNative:   0  
szErrorMsg: "[Microsoft][SQL Server Native Client]The ODBC  
            catalog stored procedures installed on server  
            my65server are version 06.50.0193; version 07.00.0205  
            or later is required to ensure proper operation.  
            Please contact your system administrator."  
```  
  
 The error handling function of an application for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections should call **SQLGetDiagRec** until it returns SQL_NO_DATA. It should then act on any messages other than the ones with a *pfNative* code of 5701 or 5703.  
  
## See Also  
 [Communicating with SQL Server &#40;ODBC&#41;](../../relational-databases/native-client-odbc-communication/communicating-with-sql-server-odbc.md)  
  
  
