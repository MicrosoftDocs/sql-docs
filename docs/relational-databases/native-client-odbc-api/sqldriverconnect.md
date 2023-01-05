---
title: "SQLDriverConnect | Microsoft Docs"
description: Learn about SQLDriverConnect connection attributes and support for high availability/disaster recovery and SPNs in the SQL Server Native Client ODBC driver.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLDriverConnect function"
ms.assetid: a1e38e2c-3a97-42d1-9c45-a0ca3282ffd1
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLDriverConnect
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver defines connection attributes that either replace or enhance connection-string keywords. Several connection-string keywords have default values specified by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
 For a list of the keywords available in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection attributes and driver default behaviors, see [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md).  
  
 For a discussion of connection string keywords that are valid for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 When the **SQLDriverConnect**_DriverCompletion_ parameter value is SQL_DRIVER_PROMPT, SQL_DRIVER_COMPLETE, or SQL_DRIVER_COMPLETE_REQUIRED, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver retrieves keyword values from the displayed dialog box. If the keyword value is passed in the connection string and the user does not alter the value for the keyword in the dialog box, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver uses the value from the connection string. If the value is not set in the connection string and the user makes no assignment in the dialog box, the driver uses the default.  
  
 **SQLDriverConnect** must be given a valid *WindowHandle* when any *DriverCompletion* value requires (or could require) the display of the driver's connection dialog box. An invalid handle returns SQL_ERROR.  
  
 Specify either the DRIVER or DSN keywords. ODBC states that a driver uses the leftmost of these two keywords and ignores the other if both are specified. If DRIVER is specified, or is the leftmost of the two, and the **SQLDriverConnect**_DriverCompletion_ parameter value is SQL_DRIVER_NOPROMPT, the SERVER keyword and an appropriate value are required.  
  
 When SQL_DRIVER_NOPROMPT is specified, user authentication keywords must be present with values. The driver ensures that either the string "Trusted_Connection=yes" or both the UID and PWD keywords are present.  
  
 If the *DriverCompletion* parameter value is SQL_DRIVER_NOPROMPT or SQL_DRIVER_COMPLETE_REQUIRED and the language or database comes from the connection string and either is invalid, **SQLDriverConnect** returns SQL_ERROR.  
  
 If the *DriverCompletion* parameter value is SQL_DRIVER_NOPROMPT or SQL_DRIVER_COMPLETE_REQUIRED and the language or database comes from the ODBC data source definitions and either is invalid, **SQLDriverConnect** uses the default language or database for the specified user ID and returns SQL_SUCCESS_WITH_INFO.  
  
 If the *DriverCompletion* parameter value is SQL_DRIVER_COMPLETE or SQL_DRIVER_PROMPT and if the language or database is invalid, **SQLDriverConnect** redisplays the dialog box.  
  
## SQLDriverConnect Support for High Availability, Disaster Recovery  
 For more information about using **SQLDriverConnect** to connect to a [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] cluster, see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).  
  
## SQLDriverConnect Support for Service Principal Names (SPNs)  
 SQLDDriverConnect will use the ODBC Login dialog boxwhen prompting is enabled. This allows SPNs to be entered for both the principal server and its failover partner.  
  
 SQLDriverConnect will accept the new connection string keywords **ServerSPN** and **FailoverPartnerSPN**, and will recognize the new connection attributes SQL_COPT_SS_SERVER_SPN and SQL_COPT_SS_FAILOVER_PARTNER_SPN.  
  
 When a connection attribute value is specified more than once, a value that is set programmatically takes precedence over the value in a DSN and a value in a connection string. A value in a DSN takes precedence over a value in a connection string.  
  
 When a connection is opened, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client sets SQL_COPT_SS_MUTUALLY_AUTHENTICATED and SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD to the authentication method used to open the connection.  
  
 For more information about SPNs, see [Service Principal Names &#40;SPNs&#41; in Client Connections &#40;ODBC&#41;](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md).  
  
## Examples  
 The following call illustrates the least amount of data required for **SQLDriverConnect**:  
  
```  
SQLDriverConnect(hdbc, hwnd,  
    (SQLTCHAR*) TEXT("DRIVER={SQL Server Native Client 10};"), SQL_NTS, szOutConn,  
    MAX_CONN_OUT, &cbOutConn, SQL_DRIVER_COMPLETE);  
```  
  
 The following connection strings illustrate minimum required data when the *DriverCompletion* parameter value is SQL_DRIVER_NOPROMPT:  
  
```  
"DSN=Human Resources;Trusted_Connection=yes"  
  
"FILEDSN=HR_FDSN;Trusted_Connection=yes"  
  
"DRIVER={SQL Server Native Client 10};SERVER=(local);Trusted_Connection=yes"  
```  
  
## See Also  
 [SQLDriverConnect Function](../../odbc/reference/syntax/sqldriverconnect-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)   
 [SET ANSI_NULLS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-nulls-transact-sql.md)   
 [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md)   
 [SET ANSI_WARNINGS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-warnings-transact-sql.md)  
  
