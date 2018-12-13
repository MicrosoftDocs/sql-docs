---
title: "What&#39;s New in SQL Server Native Client | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: e4d4fe39-0090-42a7-8405-6378370d11cb
author: MightyPen
ms.author: genemi
manager: craigg
---
# What&#39;s New in SQL Server Native Client
  [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] installs [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Native Client. There is no [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Native Client.  
  
 There will be no more updates to the ODBC driver in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. The successor to the ODBC driver in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, which is called the [!INCLUDE[msCoName](../../includes/msconame-md.md)] ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, is installed with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. For more information about the [!INCLUDE[msCoName](../../includes/msconame-md.md)] ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, see [Microsoft ODBC Driver 11 for SQL Server - Windows](https://www.microsoft.com/download/details.aspx?id=36434).  
  
 The OLE DB Provider in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client was last updated in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Native Client. Developers who wish to use an OLE DB provider to connect to the latest version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must use the OLE DB provider that shipped in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Native Client.  
  
 The following topics describe significant new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client features in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
-   [SQL Server Native Client Support for LocalDB](features/sql-server-native-client-support-for-localdb.md)  
  
-   [Metadata Discovery](features/metadata-discovery.md)  
  
-   [UTF-16 Support in SQL Server Native Client 11.0](features/utf-16-support-in-sql-server-native-client-11-0.md)  
  
-   [SQL Server Native Client Support for High Availability, Disaster Recovery](features/sql-server-native-client-support-for-high-availability-disaster-recovery.md)  
  
-   [Accessing Diagnostic Information in the Extended Events Log](features/accessing-diagnostic-information-in-the-extended-events-log.md)  
  
 In addition, ODBC in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client now supports three features that were added to standard ODBC in the Windows 7 SDK:  
  
-   Asynchronous execution on connection-related operations. For more information, see [Asynchronous Execution](https://go.microsoft.com/fwlink/?LinkID=191493).  
  
-   C Data Type Extensibility. For more information, see [C Data Types in ODBC](https://go.microsoft.com/fwlink/?LinkID=191495).  
  
     To support this feature in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, SQLGetDescField can return `SQL_C_SS_TIME2` (for `time` types) or `SQL_C_SS_TIMESTAMPOFFSET` (for `datetimeoffset`) instead of `SQL_C_BINARY`, if your application uses ODBC 3.8. For more information, see [Data Type Support for ODBC Date and Time Improvements](features/date-and-time-improvements.md).  
  
-   Calling `SQLGetData` with a small buffer multiple times to retrieve a large parameter value. For more information, see [Retrieving Output Parameters Using SQLGetData](https://go.microsoft.com/fwlink/?LinkID=191494).  
  
 The following topics describe [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client behavior changes in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
-   When calling `ICommandWithParameters::SetParameterInfo`, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [ICommandWithParameters](../native-client-ole-db-interfaces/icommandwithparameters.md).  
  
-   `SQLDescribeParam` will now consistently return a value that conforms to the ODBC specification. For more information, see [SQLDescribeParam](../native-client-odbc-api/sqldescribeparam.md).  
  
-   [ODBC Driver Behavior Change When Handling Character Conversions](features/odbc-driver-behavior-change-when-handling-character-conversions.md)  
  
## See Also  
 [SQL Server Native Client Features](features/sql-server-native-client-features.md)  
  
  
