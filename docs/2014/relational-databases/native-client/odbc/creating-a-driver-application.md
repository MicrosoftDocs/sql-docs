---
title: "Creating a SQL Server Native Client ODBC Driver Application | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, architecture"
  - "SQL Server Native Client ODBC driver, creating applications"
  - "ODBC function calls"
  - "ODBC, header files"
  - "ODBC applications"
  - "ODBC applications, creating"
  - "SQL Server Native Client ODBC driver, extensions"
  - "applications [SQL Server Native Client]"
  - "SQL Server Native Client ODBC driver, ODBC architecture"
  - "extensions [ODBC]"
  - "ODBC, driver extensions"
  - "function calls [ODBC]"
ms.assetid: c83c36e2-734e-4960-bc7e-92235910bc6f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Creating a SQL Server Native Client ODBC Driver Application
  ODBC architecture has four components that perform the following functions.  
  
|Component|Function|  
|---------------|--------------|  
|Application|Calls ODBC functions to communicate with an ODBC data source, submits SQL statements, and processes result sets.|  
|Driver Manager|Manages communication between an application and all ODBC drivers used by the application.|  
|Driver|Processes all ODBC function calls from the application, connects to a data source, passes SQL statements from the application to the data source, and returns results to the application. If necessary, the driver translates ODBC SQL from the application to native SQL used by the data source.|  
|Data source|Contains all information a driver needs to access a specific instance of data in a DBMS.|  
  
 An application that uses the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver to communicate with an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] performs the following tasks:  
  
-   Connects with a data source  
  
-   Sends SQL statements to the data source  
  
-   Processes the results of statements from the data source  
  
-   Processes errors and messages  
  
-   Terminates the connection to the data source  
  
 A more complex application written for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver might also perform the following tasks:  
  
-   Use cursors to control location in a result set  
  
-   Request commit or rollback operations for transaction control  
  
-   Perform distributed transactions involving two or more servers  
  
-   Run stored procedures on the remote server  
  
-   Call catalog functions to inquire about the attributes of a result set  
  
-   Perform bulk copy operations  
  
-   Manage large data (**varchar(max)**, **nvarchar(max)**, and **varbinary(max)** columns) operations  
  
-   Use reconnection logic to facilitate failover when database mirroring is configured  
  
-   Log performance data and long-running queries  
  
 To make ODBC function calls, a C or C++ application must include the sql.h, sqlext.h, and sqltypes.h header files. To make calls to the ODBC installer API functions, an application must include the odbcinst.h header file. A Unicode ODBC application must include the sqlucode.h header file. ODBC applications must be linked with the odbc32.lib file. ODBC applications that call the ODBC installer API functions must be linked with the odbccp32.lib file. These files are included in the Windows Platform SDK.  
  
 Many ODBC drivers, including the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver, offer driver-specific ODBC extensions. To take advantage of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver-specific extensions, an application should include the sqlncli.h header file. This header file contains:  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver-specific connection attributes.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver-specific statement attributes.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver-specific column attributes.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific data types.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific user-defined data types.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver-specific [SQLGetInfo](../../native-client-odbc-api/sqlgetinfo.md) types.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver diagnostics fields.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific diagnostic dynamic function codes.  
  
-   C/C++ type definitions for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific native C data types (returned when columns bound to C data type SQL_C_BINARY).  
  
-   Type definition for the SQLPERF data structure.  
  
-   Bulk copy macros and prototypes to support bulk copy API usage through an ODBC connection.  
  
-   Call the distributed query metadata API functions for lists of linked servers and their catalogs.  
  
 Any C or C++ ODBC application that uses the bulk copy feature of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver must be linked with the sqlncli11.lib file. Applications calling the distributed query metadata API functions must also be linked with sqlncli11.lib. The sqlncli.h and sqlncli11.lib files are distributed as part of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] developer's tools. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Include and Lib directories should be in the compiler's INCLUDE and LIB paths as in the following:  
  
```  
LIB=c:\Program Files\Microsoft Data Access SDK 2.8\Libs\x86\lib;C:\Program Files\Microsoft SQL Server\100\Tools\SDK\Lib;  
INCLUDE=c:\Program Files\Microsoft Data Access SDK 2.8\inc;C:\Program Files\Microsoft SQL Server\100\Tools\SDK\Include;  
```  
  
 One design decision made early in the process of building an application is whether the application needs to have multiple ODBC calls outstanding at the same time. There are two methods for supporting multiple concurrent ODBC calls, which are described in the remaining topics in this section. For more information, see the [ODBC Programmer's Reference](https://go.microsoft.com/fwlink/?LinkId=45250).  
  
## In This Section  
  
-   [Asynchronous Mode and SQLCancel](../../native-client-odbc-api/sqlcancel.md)  
  
-   [Multithreaded Applications](creating-a-driver-application-multithreaded-applications.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](sql-server-native-client-odbc.md)  
  
  
