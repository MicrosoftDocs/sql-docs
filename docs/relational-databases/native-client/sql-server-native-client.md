---
title: "About"
description: Learn about the features of SQL Server Native Client (SNAC). SQL Server Native Client refers to ODBC and OLE DB drivers for SQL Server.
ms.date: "04/14/2017"
ms.service: sql
ms.reviewer: ""
ms.custom: ""
ms.subservice: native-client
ms.topic: conceptual
ms.assetid: e4d4fe39-0090-42a7-8405-6378370d11cb
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL Server Native Client, also known as SNAC or SQLNCLI, is used to refer to the ODBC and OLE DB drivers for SQL Server.

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)] 

> [!NOTE]
> For more information and to download the SNAC or ODBC Drivers, see the [SNAC lifecycle explained blog post](/archive/blogs/sqlreleaseservices/snac-lifecycle-explained).  For SQLNCLI that ships as a component of SQL Server Database engine, see this [Support Lifecycle exception](./applications/support-policies-for-sql-server-native-client.md#support-lifecycle-exception). 
> For more information on ODBC Driver for SQL Server, see [Microsoft ODBC Driver for SQL Server](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md).

 Information on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client features released with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the last available version of SQL Server native Client:

-   [SQL Server Native Client Support for LocalDB](../../relational-databases/native-client/features/sql-server-native-client-support-for-localdb.md)  

-   [Metadata Discovery](../../relational-databases/native-client/features/metadata-discovery.md)  

-   [UTF-16 Support in SQL Server Native Client 11.0](../../relational-databases/native-client/features/utf-16-support-in-sql-server-native-client-11-0.md)  

-   [SQL Server Native Client Support for High Availability, Disaster Recovery](../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md)  

-   [Accessing Diagnostic Information in the Extended Events Log](../../relational-databases/native-client/features/accessing-diagnostic-information-in-the-extended-events-log.md)  

ODBC in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client supports three features that were added to standard ODBC in the Windows 7 SDK:  

-   Asynchronous execution on connection-related operations. For more information, see [Asynchronous Execution](../../odbc/reference/develop-app/asynchronous-execution-polling-method.md).  

-   C Data Type Extensibility. For more information, see [C Data Types in ODBC](../../odbc/reference/develop-app/c-data-types-in-odbc.md).  

     To support this feature in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, SQLGetDescField can return **SQL_C_SS_TIME2** (for **time** types) or **SQL_C_SS_TIMESTAMPOFFSET** (for **datetimeoffset**) instead of **SQL_C_BINARY**, if your application uses ODBC 3.8. For more information, see [Data Type Support for ODBC Date and Time Improvements](../../relational-databases/native-client-odbc-date-time/data-type-support-for-odbc-date-and-time-improvements.md).  

-   Calling **SQLGetData** with a small buffer multiple times to retrieve a large parameter value. For more information, see [Retrieving Output Parameters Using SQLGetData](../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  

 The following topics describe [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client behavior changes in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  

-   When calling **ICommandWithParameters::SetParameterInfo**, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [ICommandWithParameters](../../relational-databases/native-client-ole-db-interfaces/icommandwithparameters.md).  

-   **SQLDescribeParam** will consistently return an ODBC specification conforming value. For more information, see [SQLDescribeParam](../../relational-databases/native-client-odbc-api/sqldescribeparam.md).  

-   [ODBC Driver Behavior Change When Handling Character Conversions](../../relational-databases/native-client/features/odbc-driver-behavior-change-when-handling-character-conversions.md)  

## See also  
[Install SQL Server Native Client](../../relational-databases/native-client/applications/installing-sql-server-native-client.md)  
 [SQL Server Native Client Features](../../relational-databases/native-client/features/sql-server-native-client-features.md)