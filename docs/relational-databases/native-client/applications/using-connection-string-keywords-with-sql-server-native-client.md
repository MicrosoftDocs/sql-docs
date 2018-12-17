---
title: "Using Connection String Keywords with SQL Server Native Client | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
f1_keywords: 
  - "sql13.swb.connecttoserver.options.registeredservers.f1"
helpviewer_keywords: 
  - "data access [SQL Server Native Client], connection string keywords"
  - "SQLNCLI, connection string keywords"
  - "connection strings [SQL Server Native Client]"
  - "SQL Server Native Client, connection string keywords"
ms.assetid: 16008eec-eddf-4d10-ae99-29db26ed6372
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Connection String Keywords with SQL Server Native Client
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  Some [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client APIs use connection strings to specify connection attributes. Connection strings are lists of keyword and associated values; each keyword identifies a particular connection attribute.  
  
> **NOTE:** [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client allows ambiguity in connection strings to maintain backward compatibility (for example, some keywords may be specified more than once, and conflicting keywords may be allowed with resolution based on position or precedence). Future releases of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client might not allow ambiguity in connection strings. It is good practice when modifying applications to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to eliminate any dependency on connection string ambiguity.  
  
 The following sections describe the keywords that can be used with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver, and ActiveX Data Objects (ADO) when using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client as the data provider.  
  
## ODBC Driver connection string keywords  
 ODBC applications use connection strings as a parameters to the [SQLDriverConnect](../../../relational-databases/native-client-odbc-api/sqldriverconnect.md) and [SQLBrowseConnect](../../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md) functions.  
  
 Connection strings used by ODBC have the following syntax:  
  
 `connection-string ::= empty-string[;] | attribute[;] | attribute; connection-string`  
  
 `empty-string ::=`  
  
 `attribute ::= attribute-keyword=[{]attribute-value[}]`  
  
 `attribute-value ::= character-string`  
  
 `attribute-keyword ::= identifier`  
  
 Attribute values can optionally be enclosed in braces, and it is good practice to do so. This avoids problems when attribute values contain non-alphanumeric characters. The first closing brace in the value is assumed to terminate the value, so values cannot contain closing brace characters.  
  
 The following table describes the keywords that may be used with an ODBC connection string.  
  
|Keyword|Description|  
|-------------|-----------------|  
|**Addr**|Synonym for "Address".|  
|**Address**|The network address of the server running an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. **Address** is usually the network name of the server, but can be other names such as a pipe, an IP address, or a TCP/IP port and socket address.<br /><br /> If you specify an IP address, make sure that the TCP/IP or named pipes protocols are enabled in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> The value of **Address** takes precedence over the value passed to **Server** in ODBC connection strings when using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. Also note that `Address=;` will connect to the server specified in the **Server** keyword, whereas `Address= ;, Address=.;`, `Address=localhost;`, and `Address=(local);` all cause a connection to the local server.<br /><br /> The complete syntax for the **Address** keyword is as follows:<br /><br /> [_protocol_**:**]*Address*[**,**_port &#124;\pipe\pipename_]<br /><br /> *protocol* can be **tcp** (TCP/IP), **lpc** (shared memory), or **np** (named pipes). For more information about protocols, see [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md).<br /><br /> If neither *protocol* nor the **Network** keyword is specified, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will use the protocol order specified in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> *port* is the port to connect to, on the specified server. By default, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses port 1433.|  
|**AnsiNPW**|When "yes", the driver uses ANSI-defined behaviors for handling NULL comparisons, character data padding, warnings, and NULL concatenation. When "no", ANSI defined behaviors are not exposed. For more information about ANSI NPW behaviors, see [Effects of ISO Options](../../../relational-databases/native-client-odbc-queries/executing-statements/effects-of-iso-options.md).|  
|**APP**|Name of the application calling [SQLDriverConnect](../../../relational-databases/native-client-odbc-api/sqldriverconnect.md) (optional). If specified, this value is stored in the **master.dbo.sysprocesses** column **program_name** and is returned by [sp_who](../../../relational-databases/system-stored-procedures/sp-who-transact-sql.md) and the [APP_NAME](../../../t-sql/functions/app-name-transact-sql.md) functions.|  
|**ApplicationIntent**|Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**. The default is **ReadWrite**.  For example:<br /><br /> `ApplicationIntent=ReadOnly`<br /><br /> For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).|  
|**AttachDBFileName**|Name of the primary file of an attachable database. Include the full path and escape any \ characters if using a C character string variable:<br /><br /> `AttachDBFileName=c:\\MyFolder\\MyDB.mdf`<br /><br /> This database is attached and becomes the default database for the connection. To use **AttachDBFileName** you must also specify the database name in either the [SQLDriverConnect](../../../relational-databases/native-client-odbc-api/sqldriverconnect.md) DATABASE parameter or the SQL_COPT_CURRENT_CATALOG connection attribute. If the database was previously attached, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not reattach it; it uses the attached database as the default for the connection.|  
|**AutoTranslate**|When "yes", ANSI character strings sent between the client and server are translated by converting through Unicode to minimize problems in matching extended characters between the code pages on the client and the server.<br /><br /> Client SQL_C_CHAR data sent to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **char**, **varchar**, or **text** variable, parameter, or column is converted from character to Unicode using the client ANSI code page (ACP), then converted from Unicode to character using the ACP of the server.<br /><br /> [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **char**, **varchar**, or **text** data sent to a client SQL_C_CHAR variable is converted from character to Unicode using the server ACP, then converted from Unicode to character using the client ACP.<br /><br /> These conversions are performed on the client by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver. This requires that the same ANSI code page (ACP) used on the server be available on the client.<br /><br /> These settings have no effect on the conversions that occur for these transfers:<br /><br /> \* Unicode SQL_C_WCHAR client data sent to **char**, **varchar**, or **text** on the server.<br /><br /> &#42; **char**, **varchar**, or **text** server data sent to a Unicode SQL_C_WCHAR variable on the client.<br /><br /> \* ANSI SQL_C_CHAR client data sent to Unicode **nchar**, **nvarchar**, or **ntext** on the server.<br /><br /> \* Unicode **nchar**, **nvarchar**, or **ntext** server data sent to an ANSI SQL_C_CHAR variable on the client.<br /><br /> When "no", character translation is not performed.<br /><br /> The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver does not translate client ANSI character SQL_C_CHAR data sent to **char**, **varchar**, or **text** variables, parameters, or columns on the server. No translation is performed on **char**, **varchar**, or **text** data sent from the server to SQL_C_CHAR variables on the client.<br /><br /> If the client and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] are using different ACPs, extended characters may be misinterpreted.|  
|**Database**|Name of the default [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database for the connection. If **Database** is not specified, the default database defined for the login is used. The default database from the ODBC data source overrides the default database defined for the login. The database must be an existing database unless **AttachDBFileName** is also specified. If **AttachDBFileName** is also specified, the primary file it points to is attached and given the database name specified by **Database**.|  
|**Driver**|Name of the driver as returned by [SQLDrivers](../../../relational-databases/native-client-odbc-api/sqldrivers.md). The keyword value for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver is "{SQL Server Native Client 11.0}". The **Server** keyword is required if **Driver** is specified and **DriverCompletion** is set to SQL_DRIVER_NOPROMPT.<br /><br /> For more information about driver names, see [Using the SQL Server Native Client Header and Library Files](../../../relational-databases/native-client/applications/using-the-sql-server-native-client-header-and-library-files.md).|  
|**DSN**|Name of an existing ODBC user or system data source. This keyword overrides any values that might be specified in the **Server**, **Network**, and **Address** keywords.|  
|**Encrypt**|Specifies whether data should be encrypted before sending it over the network. Possible values are "yes" and "no". The default value is "no".|  
|**Fallback**|This keyword is deprecated, and its setting is ignored by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.|  
|**Failover_Partner**|Name of the failover partner server to be used if a connection cannot be made to the primary server.|  
|**FailoverPartnerSPN**|The SPN for the failover partner. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, driver-generated SPN.|  
|**FileDSN**|Name of an existing ODBC file data source.|  
|**Language**|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] language name (optional). [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can store messages for multiple languages in **sysmessages**. If connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with multiple languages, **Language** specifies which set of messages are used for the connection.|  
|**MARS_Connection**|Enables or disables multiple active result sets (MARS) on the connection. Recognized values are "yes" and "no". The default is "no".|  
|**MultiSubnetFailover**|Always specify **multiSubnetFailover=Yes** when connecting to the availability group listener of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] availability group or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instance. **multiSubnetFailover=Yes** configures [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to provide faster detection of and connection to the (currently) active server. Possible values are **Yes** and **No**. The default is **No**. For example:<br /><br /> `MultiSubnetFailover=Yes`<br /><br /> For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).|  
|**Net**|Synonym for "Network".|  
|**Network**|Valid values are **dbnmpntw** (named pipes) and **dbmssocn** (TCP/IP).<br /><br /> It is an error to specify both a value for the **Network** keyword and a protocol prefix on the **Server** keyword.|  
|**PWD**|The password for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login account specified in the UID parameter. **PWD** need not be specified if the login has a NULL password or when using Windows Authentication (`Trusted_Connection = yes`).|  
|**QueryLog_On**|When "yes", logging long-running query data is enabled on the connection. When "no", long-running query data is not logged.|  
|**QueryLogFile**|Full path and file name of a file to use to log data on long-running queries.|  
|**QueryLogTime**|Digit character string specifying the threshold (in milliseconds) for logging long-running queries. Any query that does not get a response in the time specified is written to the long-running query log file.|  
|**QuotedId**|When "yes", QUOTED_IDENTIFIERS is set ON for the connection, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses the ISO rules regarding the use of quotation marks in SQL statements. When no, QUOTED_IDENTIFIERS is set OFF for the connection. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] then follows the legacy [!INCLUDE[tsql](../../../includes/tsql-md.md)] rules regarding the use of quotation marks in SQL statements. For more information, see [Effects of ISO Options](../../../relational-databases/native-client-odbc-queries/executing-statements/effects-of-iso-options.md).|  
|**Regional**|When "yes", the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver uses client settings when converting currency, date, and time data to character data. The conversion is one way only; the driver does not recognize non-ODBC standard formats for date strings or currency values within; for example, a parameter used in an INSERT or UPDATE statement. When "no", the driver uses ODBC standard strings to represent currency, date, and time data that is converted to character data.|  
|**SaveFile**|Name of an ODBC data source file into which the attributes of the current connection are saved if the connection is successful.|  
|**Server**|The name of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The value must be either the name of a server on the network, an IP address, or the name of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager alias.<br /><br /> The **Address** keyword overrides the **Server** keyword.<br /><br /> You can connect to the default instance on the local server by specifying one of the following:<br /><br /> **Server=;**<br /><br /> **Server=.;**<br /><br /> **Server=(local);**<br /><br /> **Server=(local);**<br /><br /> **Server=(localhost);**<br /><br /> **Server=(localdb)\\** _instancename_ **;**<br /><br /> For more information about LocalDB support, see [SQL Server Native Client Support for LocalDB](../../../relational-databases/native-client/features/sql-server-native-client-support-for-localdb.md).<br /><br /> To specify a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], append **\\**_InstanceName_.<br /><br /> When no server is specified, a connection is made to the default instance on the local computer.<br /><br /> If you specify an IP address, make sure that the TCP/IP or named pipes protocols are enabled in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> The complete syntax for the **Server** keyword is as follows:<br /><br /> **Server=**[_protocol_**:**]*Server*[**,**_port_]<br /><br /> *protocol* can be **tcp** (TCP/IP), **lpc** (shared memory), or **np** (named pipes).<br /><br /> The following is an example of specifying a named pipe:<br /><br /> `np:\\.\pipe\MSSQL$MYINST01\sql\query`<br /><br /> This line specifies named pipe protocol, a named pipe on the local machine (`\\.\pipe`), the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance (`MSSQL$MYINST01`), and the default name of the named pipe (`sql/query`).<br /><br /> If neither a *protocol* nor the **Network** keyword is specified, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will use the protocol order specified in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> *port* is the port to connect to, on the specified server. By default, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses port 1433.<br /><br /> Spaces are ignored at the beginning of the value passed to **Server** in ODBC connection strings when using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.|  
|**ServerSPN**|The SPN for the server. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, driver-generated SPN.|  
|**StatsLog_On**|When "yes", enables the capture of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver performance data. When "no", [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver performance data is not available on the connection.|  
|**StatsLogFile**|Full path and file name of a file used to record [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver performance statistics.|  
|**Trusted_Connection**|When "yes", instructs the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver to use Windows Authentication Mode for login validation. Otherwise instructs the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver to use a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] username and password for login validation, and the UID and PWD keywords must be specified.|  
|**TrustServerCertificate**|When used with **Encrypt**, enables encryption using a self-signed server certificate.|  
|**UID**|A valid [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login account. UID need not be specified when using Windows Authentication.|  
|**UseProcForPrepare**|This keyword is deprecated, and its setting is ignored by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC Driver.|  
|**WSID**|The workstation ID. Typically, this is the network name of the computer on which the application resides (optional). If specified, this value is stored in the **master.dbo.sysprocesses** column **hostname** and is returned by [sp_who](../../../relational-databases/system-stored-procedures/sp-who-transact-sql.md) and the [HOST_NAME](../../../t-sql/functions/host-name-transact-sql.md) function.|  
  
> **NOTE:** Regional conversion settings apply to currency, numeric, date, and time data types. The conversion setting is only applicable to output conversion and is only visible when currency, numeric, date, or time values are converted to character strings.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver uses the locale registry settings for the current user. The driver does not honor the current thread's locale if the application sets it after connection by, for example, calling **SetThreadLocale**.  
  
 Altering the regional behavior of a data source can cause application failure. An application that parses date strings, and expects date strings to appear as defined by ODBC, could be adversely affected by altering this value.  
  
## OLE DB Provider Connection String Keywords  
 There are two ways OLE DB applications can initialize data source objects:  
  
-   **IDBInitialize::Initialize**  
  
-   **IDataInitialize::GetDataSource**  
  
 In the first case, a provider string can be used to initialize connection properties by setting the property DBPROP_INIT_PROVIDERSTRING in the DBPROPSET_DBINIT property set. In the second case, an initialization string can be passed to **IDataInitialize::GetDataSource** method to initialize connection properties. Both methods initialize the same OLE DB connection properties, but different sets of keywords are used. The set of keywords used by **IDataInitialize::GetDataSource** is at minimum the description of properties within the initialization property group.  
  
 Any provider string setting that has a corresponding OLE DB property set to some default value or explicitly set to a value, the OLE DB property value will override the setting in the provider string.  
  
 Boolean properties set in provider strings via DBPROP_INIT_PROVIDERSTRING values are set using the values "yes" and "no". Boolean properties set in initialization strings using **IDataInitialize::GetDataSource** are set using the values "true" and "false".  
  
 Applications using **IDataInitialize::GetDataSource** can also use the keywords used by **IDBInitialize::Initialize** but only for properties that do not have a default value. If an application uses both the **IDataInitialize::GetDataSource** keyword and the **IDBInitialize::Initialize** keyword in the initialization string, the **IDataInitialize::GetDataSource** keyword setting is used. It is strongly recommended that applications do not use **IDBInitialize::Initialize** keywords in **IDataInitialize:GetDataSource** connection strings, as this behavior may not be maintained in future releases.  
  
> [!NOTE]  
>  A connection string passed through **IDataInitialize::GetDataSource** is converted into properties and applied via **IDBProperties::SetProperties**. If component services found the property description in **IDBProperties::GetPropertyInfo**, this property will be applied as a stand-alone property. Otherwise, it will be applied through DBPROP_PROVIDERSTRING property. For example, if you specify connection string **Data Source=server1;Server=server2**, **Data Source** will be set as a property, but **Server** will go into a provider string.  
  
 If you specify multiple instances of the same provider-specific property, the first value of the first property will be used.  
  
 Connection strings used by OLE DB applications using DBPROP_INIT_PROVIDERSTRING with **IDBInitialize::Initialize** have the following syntax:  
  
 `connection-string ::= empty-string[;] | attribute[;] | attribute; connection-string`  
  
 `empty-string ::=`  
  
 `attribute ::= attribute-keyword=[{]attribute-value[}]`  
  
 `attribute-value ::= character-string`  
  
 `attribute-keyword ::= identifier`  
  
 Attribute values can optionally be enclosed in braces, and it is good practice to do so. This avoids problems when attribute values contain non-alphanumeric characters. The first closing brace in the value is assumed to terminate the value, so values cannot contain closing brace characters.  
  
 A space character after the = sign of a connection string keyword will be interpreted as a literal, even if the value is enclosed in quotation marks.  
  
 The following table describes the keywords that may be used with DBPROP_INIT_PROVIDERSTRING.  
  
|Keyword|Initialization property|Description|  
|-------------|-----------------------------|-----------------|  
|**Addr**|SSPROP_INIT_NETWORKADDRESS|Synonym for "Address".|  
|**Address**|SSPROP_INIT_NETWORKADDRESS|The network address of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> For more information about valid address syntax, see the description of the **Address** ODBC keyword, later in this topic.|  
|**APP**|SSPROP_INIT_APPNAME|The string identifying the application.|  
|**ApplicationIntent**|SSPROP_INIT_APPLICATIONINTENT|Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.<br /><br /> The default is **ReadWrite**. For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).|  
|**AttachDBFileName**|SSPROP_INIT_FILENAME|The name of the primary file (include the full path name) of an attachable database. To use **AttachDBFileName**, you must also specify the database name with the provider string Database keyword. If the database was previously attached, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not reattach it (it uses the attached database as the default for the connection).|  
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE|Synonym for "AutoTranslate".|  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE|Configures OEM/ANSI character translation. Recognized values are "yes" and "no".|  
|**Database**|DBPROP_INIT_CATALOG|The database name.|  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY|Specifies the mode of data type handling to use. Recognized values are "0" for provider data types and "80" for SQL Server 2000 data types.|  
|**Encrypt**|SSPROP_INIT_ENCRYPT|Specifies whether data should be encrypted before sending it over the network. Possible values are "yes" and "no". The default value is "no".|  
|**FailoverPartner**|SSPROP_INIT_FAILOVERPARTNER|The name of the failover server used for database mirroring.|  
|**FailoverPartnerSPN**|SSPROP_INIT_FAILOVERPARTNERSPN|The SPN for the failover partner. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|**Language**|SSPROPT_INIT_CURRENTLANGUAGE|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] language.|  
|**MarsConn**|SSPROP_INIT_MARSCONNECTION|Enables or disables multiple active result sets (MARS) on the connection if the server is [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later. Possible values are "yes" and "no". The default value is "no".|  
|**Net**|SSPROP_INIT_NETWORKLIBRARY|Synonym for "Network".|  
|**Network**|SSPROP_INIT_NETWORKLIBRARY|The network library used to establish a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.|  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY|Synonym for "Network".|  
|**PacketSize**|SSPROP_INIT_PACKETSIZE|Network packet size. The default is 4096.|  
|**PersistSensitive**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO|Accepts the strings "yes" and "no" as values. When "no", the data source object is not allowed to persist sensitive authentication information|  
|**PWD**|DBPROP_AUTH_PASSWORD|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login password.|  
|**Server**|DBPROP_INIT_DATASOURCE|The name of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> When not specified, a connection is made to the default instance on the local computer.<br /><br /> For more information about valid address syntax, see the description of the **Server** ODBC keyword, in this topic.|  
|**ServerSPN**|SSPROP_INIT_SERVERSPN|The SPN for the server. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|**Timeout**|DBPROP_INIT_TIMEOUT|The amount of time (in seconds) to wait for data source initialization to complete.|  
|**Trusted_Connection**|DBPROP_AUTH_INTEGRATED|When "yes", instructs the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider to use Windows Authentication Mode for login validation. Otherwise instructs the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider to use a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] username and password for login validation, and the UID and PWD keywords must be specified.|  
|**TrustServerCertificate**|SSPROP_INIT_TRUST_SERVER_CERTIFICATE|Accepts the strings "yes" and "no" as values. The default value is "no", which means that the server certificate will be validated.|  
|**UID**|DBPROP_AUTH_USERID|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login name.|  
|**UseProcForPrepare**|SSPROP_INIT_USEPROCFORPREP|This keyword is deprecated, and its setting is ignored by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB Provider.|  
|**WSID**|SSPROP_INIT_WSID|The workstation identifier.|  
  
 Connection strings used by OLE DB applications using **IDataInitialize::GetDataSource** have the following syntax:  
  
 `connection-string ::= empty-string[;] | attribute[;] | attribute; connection-string`  
  
 `empty-string ::=`  
  
 `attribute ::= attribute-keyword=[quote]attribute-value[quote]`  
  
 `attribute-value ::= character-string`  
  
 `attribute-keyword ::= identifier`  
  
 `quote ::= " | '`  
  
 Property use must conform to the syntax permitted in its scope.  For example, **WSID** uses curly braces (**{}**) quotation characters and **Application Name** uses single (**'**) or double (**"**) quotation characters. Only string properties can be quoted. Attempting to quote an integer or enumerated property will result in a "Connection String does not conform to OLE DB specification" error.  
  
 Attribute values can optionally be enclosed in single or double quotes, and it is good practice to do so. This avoids problems when values contain non-alphanumeric characters. The quote character used can also appear in values, provided that it is doubled.  
  
 A space character after the = sign of a connection string keyword will be interpreted as a literal, even if the value is enclosed in quotation marks.  
  
 If a connection string has more than one of the properties listed in the following table, the value of the last property will be used.  
  
 The following table describes the keywords that may be used with **IDataInitialize::GetDataSource**:  
  
|Keyword|Initialization property|Description|  
|-------------|-----------------------------|-----------------|  
|**Application Name**|SSPROP_INIT_APPNAME|The string identifying the application.|  
|**Application Intent**|SSPROP_INIT_APPLICATIONINTENT|Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.<br /><br /> The default is **ReadWrite**. For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).|  
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE|Synonym for "AutoTranslate".|  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE|Configures OEM/ANSI character translation. Recognized values are "true" and "false".|  
|**Connect Timeout**|DBPROP_INIT_TIMEOUT|The amount of time (in seconds) to wait for data source initialization to complete.|  
|**Current Language**|SSPROPT_INIT_CURRENTLANGUAGE|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] language name.|  
|**Data Source**|DBPROP_INIT_DATASOURCE|The name of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> When not specified, a connection is made to the default instance on the local computer.<br /><br /> For more information about valid address syntax, see the description of the **Server** ODBC keyword, later in this topic.|  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY|Specifies the mode of data type handling to use. Recognized values are "0" for provider data types and "80" for [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] data types.|  
|**Failover Partner**|SSPROP_INIT_FAILOVERPARTNER|The name of the failover server used for database mirroring.|  
|**Failover Partner SPN**|SSPROP_INIT_FAILOVERPARTNERSPN|The SPN for the failover partner. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|**Initial Catalog**|DBPROP_INIT_CATALOG|The database name.|  
|**Initial File Name**|SSPROP_INIT_FILENAME|The name of the primary file (include the full path name) of an attachable database. To use **AttachDBFileName**, you must also specify the database name with the provider string DATABASE keyword. If the database was previously attached, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not reattach it (it uses the attached database as the default for the connection).|  
|**Integrated Security**|DBPROP_AUTH_INTEGRATED|Accepts the value "SSPI" for Windows Authentication.|  
|**MARS Connection**|SSPROP_INIT_MARSCONNECTION|Enables or disables multiple active result sets (MARS) on the connection. Recognized values are "true" and "false". The default is "false".|  
|**Network Address**|SSPROP_INIT_NETWORKADDRESS|The network address of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> For more information about valid address syntax, see the description of the **Address** ODBC keyword, later in this topic.|  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY|The network library used to establish a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.|  
|**Packet Size**|SSPROP_INIT_PACKETSIZE|Network packet size. The default is 4096.|  
|**Password**|DBPROP_AUTH_PASSWORD|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login password.|  
|**Persist Security Info**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO|Accepts the strings "true" and "false" as values. When "false", the data source object is not allowed to persist sensitive authentication information|  
|**Provider**||For [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, this should be "SQLNCLI11".|  
|**Server SPN**|SSPROP_INIT_SERVERSPN|The SPN for the server. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|**Trust Server Certificate**|SSPROP_INIT_TRUST_SERVER_CERTIFICATE|Accepts the strings "true" and "false" as values. The default value is "false", which means that the server certificate will be validated.|  
|**Use Encryption for Data**|SSPROP_INIT_ENCRYPT|Specifies whether data should be encrypted before sending it over the network. Possible values are "true" and "false". The default value is "false".|  
|**User ID**|DBPROP_AUTH_USERID|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login name.|  
|**Workstation ID**|SSPROP_INIT_WSID|The workstation identifier.|  
  
 **Note** In the connection string, the "Old Password" property sets SSPROP_AUTH_OLD_PASSWORD, which is the current (possibly expired) password that is not available via a provider string property.  
  
## ActiveX Data Objects (ADO) Connection String Keywords  
 ADO applications set the **ConnectionString** property of **ADODBConnection** objects or supply a connection string as a parameter to the **Open** method of **ADODBConnection** objects.  
  
 ADO applications can also use the keywords used by the OLE DB **IDBInitialize::Initialize** method, but only for properties that do not have a default value. If an application uses both the ADO keywords and the **IDBInitialize::Initialize** keywords in the initialization string, the ADO keyword setting will be used. It is strongly recommended that applications only use ADO connection string keywords.  
  
 Connection strings used by ADO have the following syntax:  
  
 `connection-string ::= empty-string[;] | attribute[;] | attribute; connection-string`  
  
 `empty-string ::=`  
  
 `attribute ::= attribute-keyword=["]attribute-value["]`  
  
 `attribute-value ::= character-string`  
  
 `attribute-keyword ::= identifier`  
  
 Attribute values can optionally be enclosed in double quotes, and it is good practice to do so. This avoids problems when values contain non-alphanumeric characters. Attribute values cannot contain double quotes.  
  
 The following table describes the keywords that may be used with an ADO connection string:  
  
|Keyword|Initialization property|Description|  
|-------------|-----------------------------|-----------------|  
|**Application Intent**|SSPROP_INIT_APPLICATIONINTENT|Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.<br /><br /> The default is **ReadWrite**. For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).|  
|**Application Name**|SSPROP_INIT_APPNAME|The string identifying the application.|  
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE|Synonym for "AutoTranslate".|  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE|Configures OEM/ANSI character translation. Recognized values are "true" and "false".|  
|**Connect Timeout**|DBPROP_INIT_TIMEOUT|The amount of time (in seconds) to wait for data source initialization to complete.|  
|**Current Language**|SSPROPT_INIT_CURRENTLANGUAGE|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] language name.|  
|**Data Source**|DBPROP_INIT_DATASOURCE|The name of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> When not specified, a connection is made to the default instance on the local computer.<br /><br /> For more information about valid address syntax, see the description of the **Server** ODBC keyword, in this topic.|  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY|Specifies the mode of data type handling that will be used. Recognized values are "0" for provider data types and "80" for SQL Server 2000 data types.|  
|**Failover Partner**|SSPROP_INIT_FAILOVERPARTNER|The name of the failover server used for database mirroring.|  
|**Failover Partner SPN**|SSPROP_INIT_FAILOVERPARTNERSPN|The SPN for the failover partner. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|**Initial Catalog**|DBPROP_INIT_CATALOG|The database name.|  
|**Initial File Name**|SSPROP_INIT_FILENAME|The name of the primary file (include the full path name) of an attachable database. To use **AttachDBFileName**, you must also specify the database name with the provider string DATABASE keyword. If the database was previously attached, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not reattach it (it uses the attached database as the default for the connection).|  
|**Integrated Security**|DBPROP_AUTH_INTEGRATED|Accepts the value "SSPI" for Windows Authentication.|  
|**MARS Connection**|SSPROP_INIT_MARSCONNECTION|Enables or disables multiple active result sets (MARS) on the connection if the server is [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later. Recognized values are "true" and "false".The default is "false".|  
|**Network Address**|SSPROP_INIT_NETWORKADDRESS|The network address of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> For more information about valid address syntax, see the description of the **Address** ODBC keyword, in this topic.|  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY|The network library used to establish a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.|  
|**Packet Size**|SSPROP_INIT_PACKETSIZE|Network packet size. The default is 4096.|  
|**Password**|DBPROP_AUTH_PASSWORD|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login password.|  
|**Persist Security Info**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO|Accepts the strings "true" and "false" as values. When "false" the data source object is not allowed to persist sensitive authentication information.|  
|**Provider**||For [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, this should be "SQLNCLI11".|  
|**Server SPN**|SSPROP_INIT_SERVERSPN|The SPN for the server. The default value is an empty string. An empty string causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|**Trust Server Certificate**|SSPROP_INIT_TRUST_SERVER_CERTIFICATE|Accepts the strings "true" and "false" as values. The default value is "false", which means that the server certificate will be validated.|  
|**Use Encryption for Data**|SSPROP_INIT_ENCRYPT|Specifies whether data should be encrypted before sending it over the network. Possible values are "true" and "false". The default value is "false".|  
|**User ID**|DBPROP_AUTH_USERID|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login name.|  
|**Workstation ID**|SSPROP_INIT_WSID|The workstation identifier.|  
  
 **Note** In the connection string, the "Old Password" property sets SSPROP_AUTH_OLD_PASSWORD, which is the current (possibly expired) password that is not available via a provider string property.  
  
## See also  
 [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
  
