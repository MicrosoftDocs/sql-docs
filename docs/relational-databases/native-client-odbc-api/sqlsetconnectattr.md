---
title: "SQLSetConnectAttr | Microsoft Docs"
ms.custom: ""
ms.date: 01/09/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLSetConnectAttr function"
ms.assetid: d21b5cf1-3724-43f7-bc96-5097df0677b4
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLSetConnectAttr

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver ignores the setting of SQL_ATTR_CONNECTION_TIMEOUT.  
  
 SQL_ATTR_TRANSLATE_LIB is also ignored; specifying another translation library is not supported. To allow applications to easily be ported to use a Microsoft ODBC driver for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], any value set with SQL_ATTR_TRANSLATE_LIB will be copied into and out of a buffer in the Driver Manager.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver implements repeatable read transaction isolation as serializable.  
  
 [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced support for a new transaction isolation attribute, SQL_COPT_SS_TXN_ISOLATION. Setting SQL_COPT_SS_TXN_ISOLATION to SQL_TXN_SS_SNAPSHOT indicates that the transaction will take place under the snapshot isolation level.  
  
> [!NOTE]  
> SQL_ATTR_TXN_ISOLATION can be used to set all other isolation levels except for SQL_TXN_SS_SNAPSHOT. If you want to use snapshot isolation, you must set SQL_TXN_SS_SNAPSHOT through SQL_COPT_SS_TXN_ISOLATION. However, you can retrieve the isolation level by using either SQL_ATTR_TXN_ISOLATION or SQL_COPT_SS_TXN_ISOLATION.  
  
 Promoting ODBC statement attributes to connection attributes can have unintended consequences. Statement attributes that request server cursors for result set processing can be promoted to the connection. For example, setting the ODBC statement attribute SQL_ATTR_CONCURRENCY to a value more restrictive than the default SQL_CONCUR_READ_ONLY directs the driver to use dynamic cursors for all statements submitted on the connection. Executing an ODBC catalog function on a statement on the connection returns SQL_SUCCESS_WITH_INFO and a diagnostic record indicating that the cursor behavior has been changed to read-only. Attempting to execute a Transact-SQL SELECT statement containing a COMPUTE clause on the same connection fails.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports a number of driver-specific extensions to ODBC connection attributes defined in sqlncli.h. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver may require that the attribute be set prior to connection, or it may ignore the attribute if it is already set. The following table lists restrictions.  
  
|SQL Server attribute|Set before or after connection to server|  
|--------------------------|----------------------------------------------|  
|SQL_COPT_SS_ANSI_NPW|Before|  
|SQL_COPT_SS_APPLICATION_INTENT|Before|  
|SQL_COPT_SS_ATTACHDBFILENAME|Before|  
|SQL_COPT_SS_BCP|Before|  
|SQL_COPT_SS_BROWSE_CONNECT|Before|  
|SQL_COPT_SS_BROWSE_SERVER|Before|  
|SQL_COPT_SS_CONCAT_NULL|Before|  
|SQL_COPT_SS_CONNECTION_DEAD|After|  
|SQL_COPT_SS_ENCRYPT|Before|  
|SQL_COPT_SS_ENLIST_IN_DTC|After|  
|SQL_COPT_SS_ENLIST_IN_XA|After|  
|SQL_COPT_SS_FALLBACK_CONNECT|Before|  
|SQL_COPT_SS_FAILOVER_PARTNER|Before|  
|SQL_COPT_SS_INTEGRATED_SECURITY|Before|  
|SQL_COPT_SS_MARS_ENABLED|Before|  
|SQL_COPT_SS_MULTISUBNET_FAILOVER|Before|  
|SQL_COPT_SS_OLDPWD|Before|  
|SQL_COPT_SS_PERF_DATA|After|  
|SQL_COPT_SS_PERF_DATA_LOG|After|  
|SQL_COPT_SS_PERF_DATA_LOG_NOW|After|  
|SQL_COPT_SS_PERF_QUERY|After|  
|SQL_COPT_SS_PERF_QUERY_INTERVAL|After|  
|SQL_COPT_SS_PERF_QUERY_LOG|After|  
|SQL_COPT_SS_PRESERVE_CURSORS|Before|  
|SQL_COPT_SS_QUOTED_IDENT|Either|  
|SQL_COPT_SS_TRANSLATE|Either|  
|SQL_COPT_SS_TRUST_SERVER_CERTIFICATE|Before|  
|SQL_COPT_SS_TXN_ISOLATION|Either|  
|SQL_COPT_SS_USE_PROC_FOR_PREP|Either|  
|SQL_COPT_SS_USER_DATA|Either|  
|SQL_COPT_SS_WARN_ON_CP_ERROR|Before|  
  
 Using a pre-connection attribute and the equivalent [!INCLUDE[tsql](../../includes/tsql-md.md)] command for the same session, database, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] state can produce unexpected behavior. For example,  
  
```  
SQLSetConnectAttr(SQL_COPT_SS_QUOTED_IDENT, SQL_QI_ON) // turn ON via attribute  
SQLDriverConnect(...);  
SQLExecDirect("SET QUOTED_IDENTIFIER OFF") // turn OFF via Transact-SQL  
SQLSetConnectAttr(SQL_ATTR_CURRENT_CATALOG, ...) // restores to pre-connect attribute value  
```  

<a name="sqlcoptssansinpw"></a>
## SQL_COPT_SS_ANSI_NPW  
 SQL_COPT_SS_ANSI_NPW enables or disables the use of ISO handling of NULL in comparisons and concatenation, character data type padding, and warnings. For more information, see SET ANSI_NULLS, SET ANSI_PADDING, SET ANSI_WARNINGS, and SET CONCAT_NULL_YIELDS_NULL.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_AD_ON|Default. The connection uses ANSI default behavior for handling NULL comparisons, padding, warnings, and NULL concatenations.|  
|SQL_AD_OFF|The connection uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-defined handling of NULL, character data type padding, and warnings.|  
  
 If you use connection pooling, SQL_COPT_SS_ANSI_NPW should be set in the connection string, rather than with SQLSetConnectAttr. After a connection has been made, any attempt to change this attribute will fail silently when connection pooling is used.  

<a name="sqlcoptssapplicationintent"></a>
## SQL_COPT_SS_APPLICATION_INTENT  
 Declares the application workload type when connecting to a server. Possible values are **Readonly** and **ReadWrite**. For example:  
  
```  
SQLSetConnectAttr(hdbc, SQL_COPT_SS_APPLICATION_INTENT, TEXT("Readonly"), SQL_NTS)  
```  
  
 The default is **ReadWrite**. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client's support for [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] AGs, see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).  

<a name="sqlcoptssattachdbfilename"></a>
## SQL_COPT_SS_ATTACHDBFILENAME  
 SQL_COPT_SS_ATTACHDBFILENAME specifies the name of the primary file of an attachable database. This database is attached and becomes the default database for the connection. To use SQL_COPT_SS_ATTACHDBFILENAME you must specify the name of the database as the value of the connection attribute SQL_ATTR_CURRENT_CATALOG or in the DATABASE = parameter of a [SQLDriverConnect](../../relational-databases/native-client-odbc-api/sqldriverconnect.md). If the database was previously attached, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not reattach it.  
  
|Value|Description|  
|-----------|-----------------|  
|SQLPOINTER to a character string|The string contains the name of the primary file for the database to attach. Include the full path name of the file.|  

<a name="sqlcoptssbcp"></a>
## SQL_COPT_SS_BCP  
 SQL_COPT_SS_BCP enables bulk copy functions on a connection. For more information, see [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md).  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_BCP_OFF|Default. Bulk copy functions are not available on the connection.|  
|SQL_BCP_ON|Bulk copy functions are available on the connection.|  

<a name="sqlcoptssbrowseconnect"></a>
## SQL_COPT_SS_BROWSE_CONNECT  
 This attribute is used to customize the result set returned by [SQLBrowseConnect](../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md). SQL_COPT_SS_BROWSE_CONNECT enables or disables the return of additional information from an enumerated instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This can include information such as whether the server is a cluster, names of different instances, and the version number.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_MORE_INFO_NO|Default. Returns a list of servers.|  
|SQL_MORE_INFO_YES|**SQLBrowseConnect** returns an extended string of server properties.|  

<a name="sqlcoptssbrowseserver"></a>
## SQL_COPT_SS_BROWSE_SERVER  
 This attribute is used to customize the result set returned by **SQLBrowseConnect**. SQL_COPT_SS_BROWSE_SERVER specifies the server name for which **SQLBrowseConnect** returns the information.  
  
|Value|Description|  
|-----------|-----------------|  
|computername|**SQLBrowseConnect** returns a list of instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the specified computer. Double backslashes (\\\\) should not be used for the server name (for example, instead of \\\MyServer, MyServer should be used).|  
|NULL|Default. **SQLBrowseConnect** returns information for all servers in the domain.|  

<a name="sqlcoptssconcatnull"></a>
## SQL_COPT_SS_CONCAT_NULL  
 SQL_COPT_SS_CONCAT_NULL enables or disables the use of ISO handling of NULL when concatenating strings. For more information, see SET CONCAT_NULL_YIELDS_NULL.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_CN_ON|Default. The connection uses ISO default behavior for handling NULL values when concatenating strings.|  
|SQL_CN_OFF|The connection uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-defined behavior for handling NULL values when concatenating strings.|  

<a name="sqlcoptssencrypt"></a>
## SQL_COPT_SS_ENCRYPT  
 Controls encryption for a connection.  
  
 Encryption uses the certificate on the server. This must be verified by a certificate authority, unless the connection attribute SQL_COPT_SS_TRUST_SERVER_CERTIFICATE is set to SQL_TRUST_SERVER_CERTIFICATE_YES or the connection string contains "TrustServerCertificate=yes". If either of these conditions is true, a certificate generated and signed by the server can be used to encrypt the connection if no certificate is on the server.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_EN_ON|The connection will be encrypted.|  
|SQL_EN_OFF|The connection will not be encrypted. This is the default.|  

<a name="sqlcoptssenlistindtc"></a>
## SQL_COPT_SS_ENLIST_IN_DTC  
 The client calls the Microsoft Distributed Transaction Coordinator (MS DTC) OLE DB **ITransactionDispenser::BeginTransaction** method to begin an MS DTC transaction and create an MS DTC transaction object that represents the transaction. The application then calls **SQLSetConnectAttr** with the SQL_COPT_SS_ENLIST_IN_DTC option to associate the transaction object with the ODBC connection. All related database activity will be performed under the protection of the MS DTC transaction. The application calls **SQLSetConnectAttr** with SQL_DTC_DONE to end the connection's DTC association.  
  
|Value|Description|  
|-----------|-----------------|  
|DTC object*|The MS DTC OLE transaction object that specifies the transaction to export to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|SQL_DTC_DONE|Delimits the end of a DTC transaction.|  

<a name="sqlcoptssenlistinxa"></a>
## SQL_COPT_SS_ENLIST_IN_XA  
 To begin an XA transaction with an XA-compliant Transaction Processor (TP), the client calls the Open Group **tx_begin** function. The application then calls **SQLSetConnectAttr** with a SQL_COPT_SS_ENLIST_IN_XA parameter of TRUE to associate the XA transaction with the ODBC connection. All related database activity will be performed under the protection of the XA transaction. To end an XA association with an ODBC connection, the client must call **SQLSetConnectAttr** with a SQL_COPT_SS_ENLIST_IN_XA parameter of FALSE. For more information, see the Microsoft Distributed Transaction Coordinator documentation.  
  
## SQL_COPT_SS_FALLBACK_CONNECT  
 This attribute is no longer supported.  

<a name="sqlcoptssfailoverpartner"></a>
## SQL_COPT_SS_FAILOVER_PARTNER  
 Used to specify or retrieve the name of the failover partner used for database mirroring in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and it is a null terminated character string which must be set before the connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is initially made.  
  
 After making the connection, the application can query this attribute using [SQLGetConnectAttr](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md) to determine the identity of the failover partner. If the primary server has no failover partner this property will return an empty string. This allows a smart application to cache the most recently determined backup server, but such applications should be aware that the information is only updated when the connection is first established, or reset, if pooled, and can become out of date for long term connections.  
  
 For more information, see [Using Database Mirroring](../../relational-databases/native-client/features/using-database-mirroring.md).  

<a name="sqlcoptssintegratedsecurity"></a>
## SQL_COPT_SS_INTEGRATED_SECURITY  
 SQL_COPT_SS_INTEGRATED_SECURITY forces use of Windows Authentication for access validation on server login. When Windows Authentication is used, the driver ignores user identifier and password values provided as part of **SQLConnect**, [SQLDriverConnect](../../relational-databases/native-client-odbc-api/sqldriverconnect.md), or [SQLBrowseConnect](../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md) processing.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_IS_OFF|Default. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is used to validate user identifier and password on login.|  
|SQL_IS_ON|Windows Authentication Mode is used to validate a user's access rights to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  

<a name="sqlcoptssmarsenabled"></a>
## SQL_COPT_SS_MARS_ENABLED  
 This attribute enables or disables Multiple Active Result Sets (MARS). By default, MARS is disabled. This attribute should be set before making a connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Once the connection [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is opened, MARS will remain enabled or disabled for the life of the connection.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_MARS_ENABLED_NO|Default. Multiple Active Result Sets (MARS) is disabled.|  
|SQL_MARS_ENABLED_YES|MARS is enabled.|  
  
 For more information about MARS, see [Using Multiple Active Result Sets &#40;MARS&#41;](../../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md).  

<a name="sqlcoptssmultisubnetfailover"></a>
## SQL_COPT_SS_MULTISUBNET_FAILOVER  
 If your application is connecting to a [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] availability group (AG) on different subnets, this connection property configures [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client to provide faster detection of and connection to the (currently) active server. For example:  
  
```  
SQLSetConnectAttr(hdbc, SQL_COPT_SS_MULTISUBNET_FAILOVER, SQL_IS_ON, SQL_IS_INTEGER)  
```  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client's support for [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] AGs, see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_IS_ON|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client provides faster reconnection if there is a failover.|  
|SQL_IS_OFF|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client will not provide faster reconnection if there is a failover.|  

<a name="sqlcoptssoldpwd"></a>
## SQL_COPT_SS_OLDPWD  
 Password expiration for SQL Server Authentication was introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. The SQL_COPT_SS_OLDPWD attribute has been added to allow the client to provide both the old and the new password for the connection. When this property is set, the provider will not use the connection pool for the first connection or for subsequent connections, since the connection string will contain the "old password" which has now changed.  
  
 For more information, see [Changing Passwords Programmatically](../../relational-databases/native-client/features/changing-passwords-programmatically.md).  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_COPT_SS_OLD_PASSWORD|SQLPOINTER to a character string containing the old password. This value is write-only, and must be set before connection to the server.|  

<a name="sqlcoptssperfdata"></a>
## SQL_COPT_SS_PERF_DATA  
 SQL_COPT_SS_PERF_DATA starts or stops performance data logging. The data log file name must be set prior to starting data logging. See SQL_COPT_SS_PERF_DATA_LOG below.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_PERF_START|Starts the driver sampling performance data.|  
|SQL_PERF_STOP|Stops the counters from sampling performance data.|  
  
 For more information, see [SQLGetConnectAttr](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md).  

<a name="sqlcoptssperfdatalog"></a>
## SQL_COPT_SS_PERF_DATA_LOG  
 SQL_COPT_SS_PERF_DATA_LOG assigns the name of the log file used to record performance data. The log file name is an ANSI or Unicode, null-terminated string depending upon application compilation. The *StringLength* argument should be SQL_NTS.  

<a name="sqlcoptssperfdatalognow"></a>
## SQL_COPT_SS_PERF_DATA_LOG_NOW  
 SQL_COPT_SS_PERF_DATA_LOG_NOW instructs the driver to write a statistics log entry to disk. The *StringLength* argument should be SQL_NTS.  

<a name="sqlcoptssperfquery"></a>
## SQL_COPT_SS_PERF_QUERY  
 SQL_COPT_SS_PERF_QUERY starts or stops logging for long running queries. The query log file name must be supplied prior to starting logging. The application can define "long running" by setting the interval for logging.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_PERF_START|Starts long running query logging.|  
|SQL_PERF_STOP|Stops logging of long running queries.|  
  
 For more information, see [SQLGetConnectAttr](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md).  

<a name="sqlcoptssperfqueryinterval"></a>
## SQL_COPT_SS_PERF_QUERY_INTERVAL  
 SQL_COPT_SS_PERF_QUERY_INTERVAL sets the query logging threshold in milliseconds. Queries that do not resolve within the threshold are recorded in the long running query log file. There is no upper limit on the query threshold. A query threshold value of zero causes logging of all queries.  

<a name="sqlcoptssperfquerylog"></a>
## SQL_COPT_SS_PERF_QUERY_LOG  
 SQL_COPT_SS_PERF_QUERY_LOG assigns the name of a log file for recording long running query data. The log file name is an ANSI or Unicode, null-terminated string depending upon application compilation. The *StringLength* argument should be SQL_NTS or the length of the string in bytes.  

<a name="sqlcoptsspreservecursors"></a>
## SQL_COPT_SS_PRESERVE_CURSORS  
 This attribute allows you to query and set whether or not the connection will preserve the cursor(s) when you commit/rollback a transaction. The setting is either SQL_PC_ON or SQL_PC_OFF. The default value is SQL_PC_OFF. This setting controls whether or not the driver will close the cursor(s) for you when you call [SQLEndTran](../../relational-databases/native-client-odbc-api/sqlendtran.md) (or SQLTransact).  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_PC_OFF|Default. Cursors are closed when transaction is committed or rolled back using **SQLEndTran**.|  
|SQL_PC_ON|Cursors are not closed when transaction is committed or rolled back using **SQLEndTran**, except when using a static or keyset cursor in asynchronous mode. If a rollback is issued while the population of the cursor is not complete, the cursor is closed.|  

<a name="sqlcoptssquotedident"></a>
## SQL_COPT_SS_QUOTED_IDENT  
 SQL_COPT_SS_QUOTED_IDENT allows quoted identifiers in ODBC and Transact-SQL statements submitted on the connection. By supplying quoted identifiers, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver allows otherwise invalid object names such as "My Table", which contains a space character in the identifier. For more information, see SET QUOTED_IDENTIFIER.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_QI_OFF|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection does not allow quoted identifiers in submitted [!INCLUDE[tsql](../../includes/tsql-md.md)].|  
|SQL_QI_ON|Default. The connection allows quoted identifiers in submitted [!INCLUDE[tsql](../../includes/tsql-md.md)].|  

<a name="sqlcoptsstranslate"></a>
## SQL_COPT_SS_TRANSLATE  
 SQL_COPT_SS_TRANSLATE causes the driver to translate characters between the client and server code pages as MBCS data is exchanged. The attribute affects only data stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**char**, **varchar**, and **text** columns.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_XL_OFF|The driver does not translate characters from one code page to another in character data exchanged between the client and the server.|  
|SQL_XL_ON|Default. The driver translates characters from one code page to another in character data exchanged between the client and the server. The driver automatically configures the character translation, determining the code page installed on the server and that in use by the client.|  

<a name="sqlcoptsstrustservercertificate"></a>
## SQL_COPT_SS_TRUST_SERVER_CERTIFICATE  
 SQL_COPT_SS_TRUST_SERVER_CERTIFICATE causes the driver to enable or disable certificate validation when using encryption. This attribute is a read/write value, but setting it after a connection has been established has no effect.  
  
 Client applications can query this property after a connection has been opened to determine the actual encryption and validation settings in use.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_TRUST_SERVER_CERTIFICATE_NO|Default. Encryption without certificate validation is not enabled.|  
|SQL_TRUST_SERVER_CERTIFICATE_YES|Encryption without certificate validation is enabled.|  

<a name="sqlcoptsstxnisolation"></a>
## SQL_COPT_SS_TXN_ISOLATION  
 SQL_COPT_SS_TXN_ISOLATION sets the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] specific snapshot isolation attribute. Snapshot isolation cannot be set using SQL_ATTR_TXN_ISOLATION because the value is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] specific. However, it can be retrieved using either SQL_ATTR_TXN_ISOLATION or SQL_COPT_SS_TXN_ISOLATION.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_TXN_SS_SNAPSHOT|Indicates that from one transaction you cannot see changes made in other transactions and that you cannot see changes even when requerying.|  
  
 For more information about snapshot isolation, see [Working with Snapshot Isolation](../../relational-databases/native-client/features/working-with-snapshot-isolation.md).  
  
## SQL_COPT_SS_USE_PROC_FOR_PREP

 This attribute is no longer supported.  

<a name="sqlcoptssuserdata"></a>
## SQL_COPT_SS_USER_DATA  
 SQL_COPT_SS_USER_DATA sets the user data pointer. User data is client-owned memory recorded per connection.  
  
 For more information, see [SQLGetConnectAttr](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md).  

<a name="sqlcoptsswarnoncperror"></a>
## SQL_COPT_SS_WARN_ON_CP_ERROR  
 This attribute determines whether you will get a warning if there is a loss of data during a code page conversion. This applies to only data coming from the server.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_WARN_YES|Generate warnings when data loss is encountered during codepage conversion.|  
|SQL_WARN_NO|(Default) Do not generate warnings when data loss is encountered during codepage conversion.|  
  
## SQLSetConnectAttr Support for Service Principal Names (SPNs)

 SQLSetConnectAttr can be used to set the value of the new connection attributes SQL_COPT_SS_SERVER_SPN and SQL_COPT_SS_FAILOVER_PARTNER_SPN. These attributes cannot be set when a connection is open; if you attempt to set these attributes when a connection is open, error HY011 is returned with the message "Operation invalid at this time". (SQLSetConnectOption can also be used to set these values.)  
  
 For more information about SPNs, see [Service Principal Names &#40;SPNs&#41; in Client Connections &#40;ODBC&#41;](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md).  

<a name="sqlcoptssconnectiondead"></a>
## SQL_COPT_SS_CONNECTION_DEAD  
 This is a read-only attribute.  
  
 For more information about SQL_COPT_SS_CONNECTION_DEAD, see [SQLGetConnectAttr](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md) and [Connecting to a Data Source &#40;ODBC&#41;](../../relational-databases/native-client-odbc-communication/connecting-to-a-data-source-odbc.md).  
  
## Example

 This example logs performance data.  
  
```  
SQLPERF*     pSQLPERF;  
SQLINTEGER   nValue;  
  
// See if you are already logging. SQLPERF* will be NULL if not.  
SQLGetConnectAttr(hDbc, SQL_COPT_SS_PERF_DATA, &pSQLPERF,  
    sizeof(SQLPERF*), &nValue);  
  
if (pSQLPERF == NULL)  
    {  
    // Set the performance log file name.  
    SQLSetConnectAttr(hDbc, SQL_COPT_SS_PERF_DATA_LOG,  
        (SQLPOINTER) "\\My LogDirectory\\MyServerLog.txt", SQL_NTS);  
  
    // Start logging...  
    SQLSetConnectAttr(hDbc, SQL_COPT_SS_PERF_DATA,  
        (SQLPOINTER) SQL_PERF_START, SQL_IS_INTEGER);  
    }  
else  
    {  
    // Take a snapshot now so that your performance statistics are discernible.  
    SQLSetConnectAttr(hDbc, SQL_COPT_SS_PERF_DATA_LOG_NOW, NULL, 0);  
    }  
  
    // ...perform some action...  
  
// ...take a performance data snapshot...  
SQLSetConnectAttr(hDbc, SQL_COPT_SS_PERF_DATA_LOG_NOW, NULL, 0);  
  
    // ...perform more actions...  
  
// ...take another snapshot...  
SQLSetConnectAttr(hDbc, SQL_COPT_SS_PERF_DATA_LOG_NOW, NULL, 0);  
  
// ...and disable logging.  
SQLSetConnectAttr(hDbc, SQL_COPT_SS_PERF_DATA,  
    (SQLPOINTER) SQL_PERF_STOP, SQL_IS_INTEGER);  
  
// Continue on...  
```  
  
## See Also

 [SQLSetConnectAttr Function](https://go.microsoft.com/fwlink/?LinkId=59368)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)   
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)   
 [SET ANSI_NULLS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-nulls-transact-sql.md)   
 [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md)   
 [SET ANSI_WARNINGS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-warnings-transact-sql.md)   
 [SET CONCAT_NULL_YIELDS_NULL &#40;Transact-SQL&#41;](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md)   
 [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md)   
 [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360)   
 [SQLGetInfo](../../relational-databases/native-client-odbc-api/sqlgetinfo.md)  
  
  
