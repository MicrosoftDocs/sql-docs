---
title: "Using Connection String Keywords with OLE DB Driver for SQL Server | Microsoft Docs"
description: "Using connection string keywords with OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "01/28/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
f1_keywords: 
  - "sql13.swb.connecttoserver.options.registeredservers.f1"
helpviewer_keywords: 
  - "data access [OLE DB Driver for SQL Server], connection string keywords"
  - "MSOLEDBSQL, connection string keywords"
  - "connection strings [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server, connection string keywords"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Using Connection String Keywords with OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Some OLE DB Driver for SQL Server APIs use connection strings to specify connection attributes. Connection strings are lists of keyword and associated values; each keyword identifies a particular connection attribute.  
  
> [!NOTE]
> OLE DB Driver for SQL Server allows ambiguity in connection strings to maintain backward compatibility (for example, some keywords may be specified more than once, and conflicting keywords may be allowed with resolution based on position or precedence). Future releases of OLE DB Driver for SQL Server might not allow ambiguity in connection strings. It is good practice when modifying applications to use OLE DB Driver for SQL Server to eliminate any dependency on connection string ambiguity.  
  
 The following sections describe the keywords that can be used with the OLE DB Driver for SQL Server, and ActiveX Data Objects (ADO) when using OLE DB Driver for SQL Server as the data provider.  

  
## OLE DB Driver Connection String Keywords  
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
|**Address**|SSPROP_INIT_NETWORKADDRESS|The network address of the server running an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. **Address** is usually the network name of the server, but can be other names such as a pipe, an IP address, or a TCP/IP port and socket address.<br /><br /> If you specify an IP address, make sure that the TCP/IP or named pipes protocols are enabled in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> The value of **Address** takes precedence over the value passed to **Server** in connection strings when using OLE DB Driver for SQL Server. Also note that `Address=;` will connect to the server specified in the **Server** keyword, whereas `Address= ;, Address=.;`, `Address=localhost;`, and `Address=(local);` all cause a connection to the local server.<br /><br /> The complete syntax for the **Address** keyword is as follows:<br /><br /> [_protocol_**:**]_Address_[**,**_port &#124;\pipe\pipename_]<br /><br /> _protocol_ can be **tcp** (TCP/IP), **lpc** (shared memory), or **np** (named pipes). For more information about protocols, see [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md).<br /><br /> If neither _protocol_ nor the **Network** keyword is specified, OLE DB Driver for SQL Server will use the protocol order specified in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> *port* is the port to connect to, on the specified server. By default, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses port 1433.|   
|**APP**|SSPROP_INIT_APPNAME|The string identifying the application.|  
|**ApplicationIntent**|SSPROP_INIT_APPLICATIONINTENT|Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.<br /><br /> The default is **ReadWrite**. For more information about OLE DB Driver for SQL Server's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|  
|**AttachDBFileName**|SSPROP_INIT_FILENAME|The name of the primary file (include the full path name) of an attachable database. To use **AttachDBFileName**, you must also specify the database name with the provider string Database keyword. If the database was previously attached, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not reattach it (it uses the attached database as the default for the connection).|  
|**Authentication**<a href="#table1_1"><sup id="table1_authmode">**1**</sup></a>|SSPROP_AUTH_MODE|Specifies the SQL or Active Directory authentication used. Valid values are:<br/><ul><li>`(not set)`: Authentication mode determined by other keywords.</li><li>`ActiveDirectoryPassword:` Active Directory authentication using login ID and password.</li><li>`ActiveDirectoryIntegrated:` Integrated authentication to Active Directory using the currently logged-in user's Windows account credentials.</li><br/>**NOTE:** It's **recommended** that applications using `Integrated Security` (or `Trusted_Connection`) authentication keywords or their corresponding properties set the value of the `Authentication` keyword (or its corresponding property) to `ActiveDirectoryIntegrated` to enable new encryption and certificate validation behavior.<br/><br/><li>`SqlPassword:` Authentication using login ID and password.</li><br/>**NOTE:** It's **recommended** that applications using `SQL Server` authentication set the value of the `Authentication` keyword (or its corresponding property) to `SqlPassword` to enable new encryption and certificate validation behavior.</ul>|
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE|Synonym for "AutoTranslate".|  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE|Configures OEM/ANSI character translation. Recognized values are "yes" and "no".|  
|**Database**|DBPROP_INIT_CATALOG|The database name.|  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY|Specifies the mode of data type handling to use. Recognized values are "0" for provider data types and "80" for SQL Server 2000 data types.|  
|**Encrypt**<a href="#table1_1"><sup>**1**</sup></a>|SSPROP_INIT_ENCRYPT|Specifies whether data should be encrypted before sending it over the network. Possible values are "yes" and "no". The default value is "no".|  
|**FailoverPartner**|SSPROP_INIT_FAILOVERPARTNER|The name of the failover server used for database mirroring.|  
|**FailoverPartnerSPN**|SSPROP_INIT_FAILOVERPARTNERSPN|The SPN for the failover partner. The default value is an empty string. An empty string causes OLE DB Driver for SQL Server to use the default, provider-generated SPN.|  
|**Language**|SSPROPT_INIT_CURRENTLANGUAGE|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] language.|  
|**MarsConn**|SSPROP_INIT_MARSCONNECTION|Enables or disables multiple active result sets (MARS) on the connection if the server is [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later. Possible values are "yes" and "no". The default value is "no".|  
|**MultiSubnetFailover**|SSPROP_INIT_MULTISUBNETFAILOVER|Always specify **MultiSubnetFailover=Yes** when connecting to the availability group listener of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] availability group or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instance. **MultiSubnetFailover=Yes** configures OLE DB Driver for SQL Server to provide faster detection of and connection to the (currently) active server. Possible values are **Yes** and **No**. The default is **No**. For example:<br /><br /> `MultiSubnetFailover=Yes`<br /><br /> For more information about OLE DB Driver for SQL Server's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see  [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|  
|**Net**|SSPROP_INIT_NETWORKLIBRARY|Synonym for "Network".|  
|**Network**|SSPROP_INIT_NETWORKLIBRARY|The network library used to establish a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.|  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY|Synonym for "Network".|  
|**PacketSize**|SSPROP_INIT_PACKETSIZE|Network packet size. The default is 4096.|  
|**PersistSensitive**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO|Accepts the strings "yes" and "no" as values. When "no", the data source object is not allowed to persist sensitive authentication information|  
|**PWD**|DBPROP_AUTH_PASSWORD|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login password.|  
|**Server**|DBPROP_INIT_DATASOURCE|The name of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The value must be either the name of a server on the network, an IP address, or the name of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager alias.<br /><br /> When not specified, a connection is made to the default instance on the local computer.<br /><br /> The **Address** keyword overrides the **Server** keyword.<br /><br /> You can connect to the default instance on the local server by specifying one of the following:<br /><br /> **Server=;**<br /><br /> **Server=.;**<br /><br /> **Server=(local);**<br /><br /> **Server=(local);**<br /><br /> **Server=(localhost);**<br /><br /> **Server=(localdb)\\** *instancename* **;**<br /><br /> For more information about LocalDB support, see [OLE DB Driver for SQL Server Support for LocalDB](../../oledb/features/oledb-driver-for-sql-server-support-for-localdb.md).<br /><br /> To specify a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], append **\\**_InstanceName_.<br /><br /> When no server is specified, a connection is made to the default instance on the local computer.<br /><br /> If you specify an IP address, make sure that the TCP/IP or named pipes protocols are enabled in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> The complete syntax for the **Server** keyword is as follows:<br /><br /> **Server=**[_protocol_**:**]*Server*[**,**_port_]<br /><br /> _protocol_ can be **tcp** (TCP/IP), **lpc** (shared memory), or **np** (named pipes).<br /><br /> The following is an example of specifying a named pipe:<br /><br /> `np:\\.\pipe\MSSQL$MYINST01\sql\query`<br /><br /> This line specifies named pipe protocol, a named pipe on the local machine (`\\.\pipe`), the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance (`MSSQL$MYINST01`), and the default name of the named pipe (`sql/query`).<br /><br /> If neither a *protocol* nor the **Network** keyword is specified, OLE DB Driver for SQL Server will use the protocol order specified in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.<br /><br /> *port* is the port to connect to, on the specified server. By default, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses port 1433.<br /><br /> Spaces are ignored at the beginning of the value passed to **Server** in connection strings when using OLE DB Driver for SQL Server.|   
|**ServerSPN**|SSPROP_INIT_SERVERSPN|The SPN for the server. The default value is an empty string. An empty string causes OLE DB Driver for SQL Server to use the default, provider-generated SPN.|  
|**Timeout**|DBPROP_INIT_TIMEOUT|The amount of time (in seconds) to wait for data source initialization to complete.|  
|**Trusted_Connection**|DBPROP_AUTH_INTEGRATED|When "yes", instructs the OLE DB Driver for SQL Server to use Windows Authentication Mode for login validation. Otherwise instructs the OLE DB Driver for SQL Server to use a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] username and password for login validation, and the UID and PWD keywords must be specified.|  
|**TrustServerCertificate**<a href="#table1_1"><sup>**1**</sup></a>|SSPROP_INIT_TRUST_SERVER_CERTIFICATE|Accepts the strings "yes" and "no" as values. The default value is "no", which means that the server certificate will be validated.|  
|**UID**|DBPROP_AUTH_USERID|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login name.|  
|**UseFMTONLY**|SSPROP_INIT_USEFMTONLY|Controls how metadata is retrieved when connecting to [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] and newer. Possible values are "yes" and "no". The default value is "no".<br /><br />By default, the OLE DB Driver for SQL Server uses [sp_describe_first_result_set](../../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) and [sp_describe_undeclared_parameters](../../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md) stored procedures to retrieve metadata. These stored procedures have some limitations (e.g. they will fail when operating on temporary tables). Setting **UseFMTONLY** to "yes" instructs the driver to use [SET FMTONLY](../../../t-sql/statements/set-fmtonly-transact-sql.md) for metadata retrieval instead.|  
|**UseProcForPrepare**|SSPROP_INIT_USEPROCFORPREP|This keyword is deprecated, and its setting is ignored by the OLE DB Driver for SQL Server.|  
|**WSID**|SSPROP_INIT_WSID|The workstation identifier.|  
  
<b id="table1_1">[1]:</b> To improve security, encryption and certificate validation behavior is modified when using Authentication/Access Token initialization properties or their corresponding connection string keywords. For details, see [Encryption and certificate validation](../features/using-azure-active-directory.md#encryption-and-certificate-validation).

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
|**Access Token**<a href="#table2_1"><sup id="table2_accesstoken">**1**</sup></a>|SSPROP_AUTH_ACCESS_TOKEN|The access token used to authenticate to Azure Active Directory. <br/><br/>**NOTE:** It's an error to specify this keyword and also `UID`, `PWD`, `Trusted_Connection`, or `Authentication` connection string keywords or their corresponding properties/keywords.|
|**Application Name**|SSPROP_INIT_APPNAME|The string identifying the application.|  
|**Application Intent**|SSPROP_INIT_APPLICATIONINTENT|Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.<br /><br /> The default is **ReadWrite**. For more information about OLE DB Driver for SQL Server's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|  
|**Authentication**<a href="#table2_1"><sup>**1**</sup></a>|SSPROP_AUTH_MODE|Specifies the SQL or Active Directory authentication used. Valid values are:<br/><ul><li>`(not set)`: Authentication mode determined by other keywords.</li><li>`ActiveDirectoryPassword:` Active Directory authentication using login ID and password.</li><li>`ActiveDirectoryIntegrated:` Integrated authentication to Active Directory using the currently logged-in user's Windows account credentials.</li><br/>**NOTE:** It's **recommended** that applications using `Integrated Security` (or `Trusted_Connection`) authentication keywords or their corresponding properties set the value of the `Authentication` keyword (or its corresponding property) to `ActiveDirectoryIntegrated` to enable new encryption and certificate validation behavior.<br/><br/><li>`SqlPassword:` Authentication using login ID and password.</li><br/>**NOTE:** It's **recommended** that applications using `SQL Server` authentication set the value of the `Authentication` keyword (or its corresponding property) to `SqlPassword` to enable new encryption and certificate validation behavior.</ul>|
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE|Synonym for "AutoTranslate".|  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE|Configures OEM/ANSI character translation. Recognized values are "true" and "false".|  
|**Connect Timeout**|DBPROP_INIT_TIMEOUT|The amount of time (in seconds) to wait for data source initialization to complete.|  
|**Current Language**|SSPROPT_INIT_CURRENTLANGUAGE|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] language name.|  
|**Data Source**|DBPROP_INIT_DATASOURCE|The name of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> When not specified, a connection is made to the default instance on the local computer.<br /><br /> For more information about valid address syntax, see the description of the **Server** keyword, in this topic.|  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY|Specifies the mode of data type handling to use. Recognized values are "0" for provider data types and "80" for [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] data types.|  
|**Failover Partner**|SSPROP_INIT_FAILOVERPARTNER|The name of the failover server used for database mirroring.|  
|**Failover Partner SPN**|SSPROP_INIT_FAILOVERPARTNERSPN|The SPN for the failover partner. The default value is an empty string. An empty string causes OLE DB Driver for SQL Server to use the default, provider-generated SPN.|  
|**Initial Catalog**|DBPROP_INIT_CATALOG|The database name.|  
|**Initial File Name**|SSPROP_INIT_FILENAME|The name of the primary file (include the full path name) of an attachable database. To use **AttachDBFileName**, you must also specify the database name with the provider string DATABASE keyword. If the database was previously attached, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not reattach it (it uses the attached database as the default for the connection).|  
|**Integrated Security**|DBPROP_AUTH_INTEGRATED|Accepts the value "SSPI" for Windows Authentication.|  
|**MARS Connection**|SSPROP_INIT_MARSCONNECTION|Enables or disables multiple active result sets (MARS) on the connection. Recognized values are "true" and "false". The default is "false".|  
|**MultiSubnetFailover**|SSPROP_INIT_MULTISUBNETFAILOVER|Always specify **MultiSubnetFailover=True** when connecting to the availability group listener of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] availability group or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instance. **MultiSubnetFailover=True** configures OLE DB Driver for SQL Server to provide faster detection of and connection to the (currently) active server. Possible values are **True** and **False**. The default is **False**. For example:<br /><br /> `MultiSubnetFailover=True`<br /><br /> For more information about OLE DB Driver for SQL Server's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see  [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|  
|**Network Address**|SSPROP_INIT_NETWORKADDRESS|The network address of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> For more information about valid address syntax, see the description of the **Address** keyword, in this topic.|  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY|The network library used to establish a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.|  
|**Packet Size**|SSPROP_INIT_PACKETSIZE|Network packet size. The default is 4096.|  
|**Password**|DBPROP_AUTH_PASSWORD|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login password.|  
|**Persist Security Info**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO|Accepts the strings "true" and "false" as values. When "false", the data source object is not allowed to persist sensitive authentication information|  
|**Provider**||For OLE DB Driver for SQL Server, this should be "MSOLEDBSQL".|  
|**Server SPN**|SSPROP_INIT_SERVERSPN|The SPN for the server. The default value is an empty string. An empty string causes OLE DB Driver for SQL Server to use the default, provider-generated SPN.|  
|**Trust Server Certificate**<a href="#table2_1"><sup>**1**</sup></a>|SSPROP_INIT_TRUST_SERVER_CERTIFICATE|Accepts the strings "true" and "false" as values. The default value is "false", which means that the server certificate will be validated.|  
|**Use Encryption for Data**<a href="#table2_1"><sup>**1**</sup></a>|SSPROP_INIT_ENCRYPT|Specifies whether data should be encrypted before sending it over the network. Possible values are "true" and "false". The default value is "false".|  
|**Use FMTONLY**|SSPROP_INIT_USEFMTONLY|Controls how metadata is retrieved when connecting to [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] and newer. Possible values are "true" and "false". The default value is "false".<br /><br />By default, the OLE DB Driver for SQL Server uses [sp_describe_first_result_set](../../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) and [sp_describe_undeclared_parameters](../../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md) stored procedures to retrieve metadata. These stored procedures have some limitations (e.g. they will fail when operating on temporary tables). Setting **Use FMTONLY** to "true" instructs the driver to use [SET FMTONLY](../../../t-sql/statements/set-fmtonly-transact-sql.md) for metadata retrieval instead.|  
|**User ID**|DBPROP_AUTH_USERID|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login name.|  
|**Workstation ID**|SSPROP_INIT_WSID|The workstation identifier.|  
  
<b id="table2_1">[1]:</b> To improve security, encryption and certificate validation behavior is modified when using Authentication/Access Token initialization properties or their corresponding connection string keywords. For details, see [Encryption and certificate validation](../features/using-azure-active-directory.md#encryption-and-certificate-validation).

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
|**Access Token**<a href="#table3_1"><sup id="table3_accesstoken">**1**</sup></a>|SSPROP_AUTH_ACCESS_TOKEN|The access token used to authenticate to Azure Active Directory.<br/><br/>**NOTE:** It's an error to specify this keyword and also `UID`, `PWD`, `Trusted_Connection`, or `Authentication` connection string keywords or their corresponding properties/keywords.|
|**Application Intent**|SSPROP_INIT_APPLICATIONINTENT|Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.<br /><br /> The default is **ReadWrite**. For more information about OLE DB Driver for SQL Server's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|  
|**Application Name**|SSPROP_INIT_APPNAME|The string identifying the application.|  
|**Authentication**<a href="#table3_1"><sup>**1**</sup></a>|SSPROP_AUTH_MODE|Specifies the SQL or Active Directory authentication used. Valid values are:<br/><ul><li>`(not set)`: Authentication mode determined by other keywords.</li><li>`ActiveDirectoryPassword:` Active Directory authentication using login ID and password.</li><li>`ActiveDirectoryIntegrated:` Integrated authentication to Active Directory using the currently logged-in user's Windows account credentials.</li><br/>**NOTE:** It's **recommended** that applications using `Integrated Security` (or `Trusted_Connection`) authentication keywords or their corresponding properties set the value of the `Authentication` keyword (or its corresponding property) to `ActiveDirectoryIntegrated` to enable new encryption and certificate validation behavior.<br/><br/><li>`SqlPassword:` Authentication using login ID and password.</li><br/>**NOTE:** It's **recommended** that applications using `SQL Server` authentication set the value of the `Authentication` keyword (or its corresponding property) to `SqlPassword` to enable new encryption and certificate validation behavior.</ul>|
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE|Synonym for "AutoTranslate".|  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE|Configures OEM/ANSI character translation. Recognized values are "true" and "false".|  
|**Connect Timeout**|DBPROP_INIT_TIMEOUT|The amount of time (in seconds) to wait for data source initialization to complete.|  
|**Current Language**|SSPROPT_INIT_CURRENTLANGUAGE|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] language name.|  
|**Data Source**|DBPROP_INIT_DATASOURCE|The name of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> When not specified, a connection is made to the default instance on the local computer.<br /><br /> For more information about valid address syntax, see the description of the **Server** keyword, in this topic.|  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY|Specifies the mode of data type handling that will be used. Recognized values are "0" for provider data types and "80" for SQL Server 2000 data types.|  
|**Failover Partner**|SSPROP_INIT_FAILOVERPARTNER|The name of the failover server used for database mirroring.|  
|**Failover Partner SPN**|SSPROP_INIT_FAILOVERPARTNERSPN|The SPN for the failover partner. The default value is an empty string. An empty string causes OLE DB Driver for SQL Server to use the default, provider-generated SPN.|  
|**Initial Catalog**|DBPROP_INIT_CATALOG|The database name.|  
|**Initial File Name**|SSPROP_INIT_FILENAME|The name of the primary file (include the full path name) of an attachable database. To use **AttachDBFileName**, you must also specify the database name with the provider string DATABASE keyword. If the database was previously attached, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not reattach it (it uses the attached database as the default for the connection).|  
|**Integrated Security**|DBPROP_AUTH_INTEGRATED|Accepts the value "SSPI" for Windows Authentication.|  
|**MARS Connection**|SSPROP_INIT_MARSCONNECTION|Enables or disables multiple active result sets (MARS) on the connection if the server is [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later. Recognized values are "true" and "false".The default is "false".|  
|**MultiSubnetFailover**|SSPROP_INIT_MULTISUBNETFAILOVER|Always specify **MultiSubnetFailover=True** when connecting to the availability group listener of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] availability group or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instance. **MultiSubnetFailover=True** configures OLE DB Driver for SQL Server to provide faster detection of and connection to the (currently) active server. Possible values are **True** and **False**. The default is **False**. For example:<br /><br /> `MultiSubnetFailover=True`<br /><br /> For more information about OLE DB Driver for SQL Server's support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see  [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|  
|**Network Address**|SSPROP_INIT_NETWORKADDRESS|The network address of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.<br /><br /> For more information about valid address syntax, see the description of the **Address** keyword, in this topic.|  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY|The network library used to establish a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the organization.|  
|**Packet Size**|SSPROP_INIT_PACKETSIZE|Network packet size. The default is 4096.|  
|**Password**|DBPROP_AUTH_PASSWORD|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login password.|  
|**Persist Security Info**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO|Accepts the strings "true" and "false" as values. When "false" the data source object is not allowed to persist sensitive authentication information.|  
|**Provider**||For OLE DB Driver for SQL Server, this should be "MSOLEDBSQL".|  
|**Server SPN**|SSPROP_INIT_SERVERSPN|The SPN for the server. The default value is an empty string. An empty string causes OLE DB Driver for SQL Server to use the default, provider-generated SPN.|  
|**Trust Server Certificate**<a href="#table3_1"><sup>**1**</sup></a>|SSPROP_INIT_TRUST_SERVER_CERTIFICATE|Accepts the strings "true" and "false" as values. The default value is "false", which means that the server certificate will be validated.|  
|**Use Encryption for Data**<a href="#table3_1"><sup>**1**</sup></a>|SSPROP_INIT_ENCRYPT|Specifies whether data should be encrypted before sending it over the network. Possible values are "true" and "false". The default value is "false".|  
|**Use FMTONLY**|SSPROP_INIT_USEFMTONLY|Controls how metadata is retrieved when connecting to [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] and newer. Possible values are "true" and "false". The default value is "false".<br /><br />By default, the OLE DB Driver for SQL Server uses [sp_describe_first_result_set](../../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) and [sp_describe_undeclared_parameters](../../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md) stored procedures to retrieve metadata. These stored procedures have some limitations (e.g. they will fail when operating on temporary tables). Setting **Use FMTONLY** to "true" instructs the driver to use [SET FMTONLY](../../../t-sql/statements/set-fmtonly-transact-sql.md) for metadata retrieval instead.|  
|**User ID**|DBPROP_AUTH_USERID|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login name.|  
|**Workstation ID**|SSPROP_INIT_WSID|The workstation identifier.|  
  
<b id="table3_1">[1]:</b> To improve security, encryption and certificate validation behavior is modified when using Authentication/Access Token initialization properties or their corresponding connection string keywords. For details, see [Encryption and certificate validation](../features/using-azure-active-directory.md#encryption-and-certificate-validation).

 **Note** In the connection string, the "Old Password" property sets SSPROP_AUTH_OLD_PASSWORD, which is the current (possibly expired) password that is not available via a provider string property.  
  
## See also  
 [Building Applications with OLE DB Driver for SQL Server](../../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)  
  
  
