---
title: "SQL Server Native Client | Microsoft Docs"
ms.date: "02/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e4d4fe39-0090-42a7-8405-6378370d11cb
caps.latest.revision: 43
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SQL Server Native Client
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

The last SQL Server Native Client can be downloaded from [Microsoft® SQL Server® 2012 Feature Pack](https://www.microsoft.com/download/details.aspx?id=29065).  Developers who wish to use an OLE DB provider to connect to the latest version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must use the OLE DB provider that shipped in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Native Client.  

For more information on ODBC Driver for SQL Server, see [Microsoft ODBC Driver for SQL Server on Windows](https://msdn.microsoft.com/library/jj730314(v=sql.110).aspx).  See also, [Introducing the new Microsoft ODBC Drivers for SQL Server](https://blogs.msdn.microsoft.com/sqlnativeclient/2013/01/23/introducing-the-new-microsoft-odbc-drivers-for-sql-server/), and [ODBC Driver 13.1 for SQL Server released](https://blogs.technet.microsoft.com/dataplatforminsider/2016/08/03/odbc-driver-13-1-for-sql-server-released/).  
  
 The following topics describe significant new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client features in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
-   [SQL Server Native Client Support for LocalDB](../../relational-databases/native-client/features/sql-server-native-client-support-for-localdb.md)  
  
-   [Metadata Discovery](../../relational-databases/native-client/features/metadata-discovery.md)  
  
-   [UTF-16 Support in SQL Server Native Client 11.0](../../relational-databases/native-client/features/utf-16-support-in-sql-server-native-client-11-0.md)  
  
-   [SQL Server Native Client Support for High Availability, Disaster Recovery](../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md)  
  
-   [Accessing Diagnostic Information in the Extended Events Log](../../relational-databases/native-client/features/accessing-diagnostic-information-in-the-extended-events-log.md)  
  
 In addition, ODBC in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client now supports three features that were added to standard ODBC in the Windows 7 SDK:  
  
-   Asynchronous execution on connection-related operations. For more information, see [Asynchronous Execution](http://go.microsoft.com/fwlink/?LinkID=191493).  
  
-   C Data Type Extensibility. For more information, see [C Data Types in ODBC](http://go.microsoft.com/fwlink/?LinkID=191495).  
  
     To support this feature in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, SQLGetDescField can return **SQL_C_SS_TIME2** (for **time** types) or **SQL_C_SS_TIMESTAMPOFFSET** (for **datetimeoffset**) instead of **SQL_C_BINARY**, if your application uses ODBC 3.8. For more information, see [Data Type Support for ODBC Date and Time Improvements](../../relational-databases/native-client-odbc-date-time/data-type-support-for-odbc-date-and-time-improvements.md).  
  
-   Calling **SQLGetData** with a small buffer multiple times to retrieve a large parameter value. For more information, see [Retrieving Output Parameters Using SQLGetData](http://go.microsoft.com/fwlink/?LinkID=191494).  
  
 The following topics describe [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client behavior changes in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
-   When calling **ICommandWithParameters::SetParameterInfo**, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [ICommandWithParameters](../../relational-databases/native-client-ole-db-interfaces/icommandwithparameters.md).  
  
-   **SQLDescribeParam** will now consistently return a value that conforms to the ODBC specification. For more information, see [SQLDescribeParam](../../relational-databases/native-client-odbc-api/sqldescribeparam.md).  
  
-   [ODBC Driver Behavior Change When Handling Character Conversions](../../relational-databases/native-client/features/odbc-driver-behavior-change-when-handling-character-conversions.md)  
  
## See Also  
[Installing SQL Server Native Client](../../relational-databases/native-client/applications/installing-sql-server-native-client.md)  
 [SQL Server Native Client Features](../../relational-databases/native-client/features/sql-server-native-client-features.md)  
  
  
