---
title: "CREATE EXTERNAL DATA SOURCE (Transact-SQL) CONNECTION_OPTIONS argument"
description: The CREATE EXTERNAL DATA SOURCE CONNECTION_OPTIONS argument can vary depending on the external data provider. This article provides additional detail for connection options depending on the provider.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/14/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CONNECTION_OPTIONS"
  - "CREATE_EXTERNAL_DATA_SOURCE CONNECTION_OPTIONS"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azuresqledge-current"
---

# CREATE EXTERNAL DATA SOURCE (Transact-SQL) CONNECTION_OPTIONS

This article provides additional detail for CONNECTION_OPTIONS depending on the provider. The [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md) CONNECTION_OPTIONS argument can vary depending on the external data provider.

The CONNECTION_OPTIONS argument for [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md) was first introduced in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]. This document applies to [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] for Windows and Linux, as well as [!INCLUDE[ssbigdataclusters-ss-nover](../../includes/ssbigdataclusters-ver15.md)].

- The `key_value_pair` is the keyword and the value for a specific connection option.
- To use multiple connection options, separate them by a semi-colon.
- Possible key value pairs are specific to the driver.

The remainder of this article contains vendor-specific connection options.

## SQL Server external data source

You can only specify the key-value pairs that have an entry in [DSN and Connection String Keywords and Attributes](../../connect/odbc/dsn-connection-string-attribute.md) under the **DSN / Connection String Keyword** column. For example, the SQL_ATTR_TXN_ISOLATION keyword is not supported, because that is an attribute set using **SQLSetConnectAttr**, not in the connection string.

Connection string keywords and options for Microsoft OLE DB providers:

- Microsoft OLE DB Driver for SQL Server: [Using Connection String Keywords with OLE DB Driver for SQL Server](../../connect/oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- SQL Native Client (deprecated) [Native Client OLE DB (SQLNCLI) Using connection string keywords](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md)

Connection string keywords and options for Microsoft ODBC DB providers:

- Microsoft ODBC Driver for SQL Server: [DSN and Connection String Keywords and Attributes](../../connect/odbc/dsn-connection-string-attribute.md)
- SQL Native Client (deprecated) [ODBC Driver connection string keywords](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md#odbc-driver-connection-string-keywords)

## Oracle

You can only specify the key-value pairs that have an entry in the Oracle wire protocol table as follows:

|Attribute (Short Name) | Default |
|:--|:--|
| AccountingInfo (AI) | None
| Action (ACT) | None
| AlternateServers (ASRV) | None |  
| AllowedOpenSSLVersions (AOV) | latest |  
| ApplicationName (AN) | None
| ApplicationUsingThreads (AUT) | 1 (Enabled) |  
| ArraySize (AS) | 60000
| AuthenticationMethod (AM) | 1 (Encrypt Password) |  
| BatchFailureReturnsError (BFRE) | 0 (Disabled) |  
| BindParamsAsUnicode (BPAU) | 0 (Disabled) |  
| BulkBinaryThreshold (BBT) | 32 |  
| BulkCharacterThreshold (BCT) | -1 |  
| BulkLoadBatchSize (BLBS) | 1024 |  
| BulkLoadFieldDelimiter (BLFD) | None |  
| BulkLoadOptions (BLO) | 0
| BulkLoadRecordDelimiter (BLRD) | None |  
| CachedCursorLimit (CCL) | 32 |  
| CachedDescriptionLimit (CDL) | 0 |  
| CatalogIncludesSynonyms (CIS) | 1 (Enabled) |  
| CatalogOptions (CO) | 0 (Disabled) |  
| ClientHostName (CHN) | None
| ClientID (CID) | None
| ClientUser (CU) | None
| ConnectionReset (CR) | 0 (Disabled) |  
| ConnectionRetryCount (CRC) | 0 |  
| ConnectionRetryDelay (CRD) | 3 |  
| CredentialsWalletEntry (CWE) | None |  
| CredentialsWalletPassword (CWPWD) | None |  
| CredentialsWalletPath (CWPATH) | None |  
| CryptoProtocolVersion (CPV) | TLSv1.2,TLSv1.1,TLSv1 |  
| CryptoLibName (CLN) | Empty string |  
| DataIntegrityLevel (DIL) | 1 (Accepted) |  
| DataIntegrityTypes (DIT) | MD5, SHA1, SHA256, SHA384, SHA512 |  
| DataSourceName (DSN) | None
| DefaultLongDataBuffLen (DLDBL) | 1024 |  
| DescribeAtPrepare (DAP) | 0 (Disabled) |  
| Description (n/a) | None
| EditionName (EN) | None
| EnableBulkLoad (EBL) | 0 (Disabled) |  
| EnableDescribeParam (EDP) | 0 (Disabled) |  
| EnableNcharSupport (ENS) (deprecated.) | None |  
| EnableScrollableCursors (ESC) | 1 (Enabled) |  
| EnableServerResultCache (ESRC) | 0 (Disabled) |  
| EnableStaticCursorsForLongData (ESCLD) | 0 (Disabled) |  
| EnableTimestampwithTimezone (ETWT) (deprecated) | None |  
| EncryptionLevel (EL) | 1 (Accepted) |  
| EncryptionMethod (EM) | 0 (No Encryption) |  
| EncryptionTypes (ET) | No encryption methods are specified. The driver sends a list of all of the encryption methods to the Oracle server. |  
| FailoverGranularity (FG) | 0 (Non-Atomic) |  
| FailoverMode (FM) | 0 (Connection) |  
| FailoverPreconnect (FP) | 0 (Disabled) |  
| FetchTSWTZasTimestamp (FTSWTZAT) | 0 (Disabled) |  
| GSSClient (GSSC) | native
| HostName (HOST) | None
| HostNameInCertificate (HNIC) | None |  
| IANAAppCodePage (IACP) (UNIX and Linux only) | 4 (ISO 8559-1 Latin-1) |  
| ImpersonateUser (IU) | None
| InitializationString (IS) | None |  
| KeepAlive (KA) | 0 (Disabled) |  
| KeyPassword (KP) | None
| Keystore (KS) | None
| KeystorePassword (KSP) | None |  
| LDAPDistinguishedName (LDAPDN) | None |  
| LoadBalanceTimeout (LBT) | 0 |  
| LoadBalancing (LB) | 0 (Disabled) |  
| LOBPrefetchSize (LPS) | 4000 |  
| LocalTimezoneOffset (LTZO) | "" (Empty String) |
| LockTimeout (LTO) | -1 |
| LoginTimeout (LT) | 15 |
| LogonID (UID) | None
| MaxPoolSize (MXPS) | 100 |
| MinPoolSize (MNPS) | 0 |
| Module (MOD) | None
| Password (PWD) | None |
| Pooling (POOL) | 0 (Disabled) |
| PortNumber (PORT) | None |
| PRNGSeedFile (PSF) (UNIX and Linux only) | /dev/random |
| PRNGSeedSource (PSS) (UNIX and Linux only) | 0 (File) |
| ProcedureRetResults (PRR) | 0 (Disabled) |
| ProgramID (PID) | None |
| ProxyHost (PXHN) | Empty string |
| ProxyMode (PXM) | 0 (NONE) |
| ProxyPassword (PXPW) | Empty string |
| ProxyPort (PXPT) | 0
| ProxyUser (PXU) | Empty string |
| QueryTimeout (QT) | 0 |
| ReportCodepageConversionErrors (RCCE) | 0 (Ignore Errors)|
| ReportRecycleBin (RRB) | 0 (Disabled)|
| SDUSize (SDU) | 16384|
| ServerName (SRVR) | None|
| ServerType (ST) | 0 (Server Default)|
| ServiceName (SN) | None. If no value is specified for either the SID, Service Name, or TNSNames option, the driver attempts to connect to the ORCL SID by default.|
| SID (SID) | None. If no value is specified for either the SID, Service Name, or TNSNames option, the driver attempts to connect to the ORCL SID by default.|
| SSLLibName (SLN) | Empty string|
| SupportBinaryXML (SBX) | 0 (Disabled)|
| TimestampEscapeMapping (TEM) | 0 (Oracle Version Specific)|
| TNSNamesFile (TNF) | None. If no value is specified for either the SID, Service Name, or TNSNames option, the driver attempts to connect to the ORCL SID by default.|  
| Truststore (TS) | None |
| TruststorePassword (TSP) | None |
| UseCurrentSchema (UCS) | 1 (Enabled) |
| UseDefaultEncryptionOptions | 1 (Enabled) |
| ValidateServerCertificate (VSC) | 1 (Enabled) |
| WireProtocolMode (WPM) | 2 |

## Teradata

You can only specify the key-value pairs that have an entry in the connector configuration options provided in the [Teradata Connector Configuration Options](https://docs.teradata.com/r/ODBC-Driver-for-Teradata-User-Guide/May-2017/ODBC-Application-Development/ODBC-Connection-Functions-and-Dialog). 

## MongoDB API for Cosmos DB

You can only specify the key-value pairs that have an entry in the driver configuration options below.

| Key name | Default | Required | Description |
|:--|:--|:--|:--|
|DefaultStringColumnLength|255|No| The maximum number of characters that can be contained in STRING columns. The maximum value that you can set for this option is 2147483647.|
|noCursorTimeout|False | No | This option specifies whether the driver allows active cursors on the data source server to expire. When FALSE, the data source server will time out idle cursors after the threshold inactivity period set on the server. When set to TRUE, the driver prevents the data source server from timing out idle cursors, and there is a risk that if the driver should quit or lose the connection to the server unexpectedly, the cursor will remain open on the server indefinitely. You can adjust the threshold for idle cursor timeouts on the MongoDB server, see https://docs.mongodb.com/v3.0/reference/parameters/ for details.|
|SamplingLimit|100|No|The maximum number of records that the driver can sample to generate a temporary schema definition. When this option is set to 0, the driver samples every document in the database.<br /><br />Make sure to configure the driver to sample all the necessary data. Documents that are not sampled do not get included in the schema definition, and consequently do not become available in ODBC applications.<br /><br />Typically, sampling a large number of documents results in a schema definition that is more accurate and better able to represent all the data in the database. However, the sampling process may take longer than expected when many documents are sampled, especially if the database contains complex, nested data structures.|
|SamplingStrategy|Forward|No|This option specifies how the driver samples data when generating a temporary schema definition.<br /><br />**Forward**: The driver samples data starting from the first record in the database, then samples the next record, and so on.<br />**Backward**: The driver samples data starting from the last record in the database, then samples the preceding record, and so on.<br />**Random**: The driver selects sample records from the data source at random until the SamplingLimit is reached.|
| SSL | Clear (0) | No | This option specifies whether the driver uses SSL to connect to the server. Enabled (1): The driver uses SSL to connect to the server.Disabled (0): The driver does not use SSL to connect to the server. |


## Generic ODBC

Valid CONNECTION_OPTIONS that you can specify for PolyBase Generic ODBC External Data Source are driver specific. If not using a Microsoft-provided ODBC provider (see previous section), consult the driver's documentation for valid key-value pairs.

There are some valid key-value pairs in PolyBase that are available to all generic ODBC drivers. *The following keys were added to SQL Server 2019 in CU5.*

| Key | Possible values | Description |  
|:--|:--|:--|
| PolyBaseOdbcSupportsRowCount | true, FALSE |Indicates whether or not the driver supports the SQLRowCount function being called on ODBC catalog functions. Default is false. For example: `CONNECTION_OPTIONS='PolyBaseOdbcSupportsRowCount=TRUE'`.|
| PolyBaseOdbcSupportsMetadataIdAttributes | true, FALSE | Indicates whether or not the driver supports setting the METADATA_ID statement attribute. Default is false. For example: `CONNECTION_OPTIONS='PolyBaseOdbcSupportsMetadataIdAttributes=TRUE'`.|
| PolyBaseOdbcSupportsBindOffset | true, FALSE | Indicates whether or not the driver supports bind offsets for row-wise binding of result sets. If not, use column binding. Default is false. For example: `CONNECTION_OPTIONS='PolyBaseOdbcSupportsBindOffset=TRUE'`.|
| PolyBaseQoTopPushdownSyntax | TOP, LIMIT | Contains information specifying how to push down the TOP operator to the backend. Default is empty string, indicating a lack of support for TOP pushdown. If the user specifies TOP, `top {0}` is used as the format string. If the user specifies LIMIT, `limit {0}` is used as the format string. This implementation is driver-specific, consult the external data source and/or driver documentation. For example: `CONNECTION_OPTIONS= PolyBaseQoTopPushdownSyntax=TOP'`.|

## Next steps

- [Introducing data virtualization with PolyBase](../../relational-databases/polybase/polybase-guide.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md)
- [PolyBase Frequently asked questions](../../relational-databases/polybase/polybase-faq.yml)
