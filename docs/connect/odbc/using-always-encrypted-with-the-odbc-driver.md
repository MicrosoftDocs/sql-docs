---
title: "Using Always Encrypted with the ODBC Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: 09/01/2018
ms.prod: sql
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 02e306b8-9dde-4846-8d64-c528e2ffe479
ms.author: "v-chojas"
manager: craigg
author: MightyPen
---
# Using Always Encrypted with the ODBC Driver for SQL Server
[!INCLUDE[Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

### Applicable to

- ODBC Driver 13.1 for SQL Server
- ODBC Driver 17 for SQL Server

### Introduction

This article provides information on how to develop ODBC applications using [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) and the [ODBC Driver for SQL Server](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md).

Always Encrypted allows client applications to encrypt sensitive data and never reveal the data or the encryption keys to SQL Server or Azure SQL Database. An Always Encrypted enabled driver, such as the ODBC Driver for SQL Server, achieves this by transparently encrypting and decrypting sensitive data in the client application. The driver automatically determines which query parameters correspond to sensitive database columns (protected using Always Encrypted), and encrypts the values of those parameters before passing the data to SQL Server or Azure SQL Database. Similarly, the driver transparently decrypts data retrieved from encrypted database columns in query results. For more information, see [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md).

### Prerequisites

Configure Always Encrypted in your database. This involves provisioning Always Encrypted keys and setting up encryption for selected database columns. If you do not already have a database with Always Encrypted configured, follow the directions in [Getting Started with Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md#getting-started-with-always-encrypted). In particular, your database should contain the metadata definitions for a Column Master Key (CMK), a Column Encryption Key (CEK), and a table containing one or more columns encrypted using that CEK.

### Enabling Always Encrypted in an ODBC Application

The easiest way to enable both parameter encryption and resultset encrypted column decryption is by setting the value of the `ColumnEncryption` connection string keyword to **Enabled**. The following is an example of a connection string which enables Always Encrypted:

```
SQLWCHAR *connString = L"Driver={ODBC Driver 13 for SQL Server};Server={myServer};Trusted_Connection=yes;ColumnEncryption=Enabled;";
```

Always Encrypted may also be enabled in the DSN configuration, using the same key and value (which will be overridden by the connection string setting, if present), or programmatically with the `SQL_COPT_SS_COLUMN_ENCRYPTION` pre-connection attribute. Setting it this way overrides the value set in the connection string or DSN:

```
 SQLSetConnectAttr(hdbc, SQL_COPT_SS_COLUMN_ENCRYPTION, (SQLPOINTER)SQL_COLUMN_ENCRYPTION_ENABLE, 0);
```

Once enabled for the connection, the behavior of Always Encrypted may be adjusted for individual queries. See [Controlling the Performance Impact of Always Encrypted](#controlling-the-performance-impact-of-always-encrypted) below for more information.

Note that enabling Always Encrypted is not sufficient for encryption or decryption to succeed; you also need to make sure that:

- The application has the *VIEW ANY COLUMN MASTER KEY DEFINITION* and *VIEW ANY COLUMN ENCRYPTION KEY DEFINITION* database permissions, required to access the metadata about Always Encrypted keys in the database. For details, see [Database Permissions](../../relational-databases/security/encryption/always-encrypted-database-engine.md#database-permissions).

- The application can access the CMK which protects the CEKs for the queried encrypted columns. This is dependent on the keystore provider which stores the CMK. See [Working with Column Master Key Stores](#working-with-column-master-key-stores) for more information.

### Retrieving and Modifying Data in Encrypted Columns

Once you enable Always Encrypted on a connection, you can use standard ODBC APIs (see [ODBC sample code](https://code.msdn.microsoft.com/windowsapps/ODBC-sample-191624ae/sourcecode?fileId=51137&pathId=1980325953) or [ODBC Programmer's Reference](https://msdn.microsoft.com/library/ms714177(v=vs.85).aspx)) to retrieve or modify data in encrypted database columns. Assuming your application has the required database permissions and can access the column master key, the driver will encrypt any query parameters which target encrypted columns and decrypt data retrieved from encrypted columns, behaving transparently to the application as if the columns were not encrypted.

If Always Encrypted is not enabled, queries with parameters which target encrypted columns will fail. Data can still be retrieved from encrypted columns, as long as the query has no parameters targeting encrypted columns. However, the driver will not attempt any decryption and the application will receive the binary encrypted data (as byte arrays).

The table below summarizes the behavior of queries, depending on whether Always Encrypted is enabled or not:

|Query characteristic | Always Encrypted is enabled and application can access the keys and key metadata|Always Encrypted is enabled and application cannot access the keys or key metadata | Always Encrypted is disabled|
|:---|:---|:---|:---|
| Parameters targeting encrypted columns. | Parameter values are transparently encrypted. | Error | Error|
| Retrieving data from encrypted columns, without parameters targeting encrypted columns.| Results from encrypted columns are transparently decrypted. The application receives plaintext column values. | Error | Results from encrypted columns are not decrypted. The application receives encrypted values as byte arrays.

The following examples illustrate retrieving and modifying data in encrypted columns. The examples assume a table with the following schema. Note that the SSN and BirthDate columns are encrypted.

```
CREATE TABLE [dbo].[Patients](
 [PatientId] [int] IDENTITY(1,1),
 [SSN] [char](11) COLLATE Latin1_General_BIN2
 ENCRYPTED WITH (ENCRYPTION_TYPE = DETERMINISTIC,
 ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256',
 COLUMN_ENCRYPTION_KEY = CEK1) NOT NULL,
 [FirstName] [nvarchar](50) NULL,
 [LastName] [nvarchar](50) NULL,
 [BirthDate] [date]
 ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED,
 ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256',
 COLUMN_ENCRYPTION_KEY = CEK1) NOT NULL
 PRIMARY KEY CLUSTERED ([PatientId] ASC) ON [PRIMARY] )
 GO
```

#### Data Insertion Example

This example inserts a row into the Patients table. Note the following:

- There is nothing specific to encryption in the sample code. The driver automatically detects and encrypts the values of the SSN and date parameters, which target encrypted columns. This makes encryption transparent to the application.

- The values inserted into database columns, including the encrypted columns, are passed as bound parameters (see [SQLBindParameter Function](https://msdn.microsoft.com/library/ms710963(v=vs.85).aspx)). While using parameters is optional when sending values to non-encrypted columns (although it is highly recommended because it helps prevent SQL injection), it is required for values targeting encrypted columns. If the values inserted in the SSN or BirthDate columns were passed as literals embedded in the query statement, the query would fail because the driver does not attempt to encrypt or otherwise process literals in queries. As a result, the server would reject them as incompatible with the encrypted columns.

- The SQL type of the parameter inserted into the SSN column is set to SQL_CHAR, which maps to the **char** SQL Server data type (`rc = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, 11, 0, (SQLPOINTER)SSN, 0, &cbSSN);`). If the type of the parameter was set to SQL_WCHAR, which maps to **nchar**, the query would fail, as Always Encrypted does not support server-side conversions from encrypted nchar values to encrypted char values. See [ODBC Programmer's Reference -- Appendix D: Data Types](https://msdn.microsoft.com/library/ms713607.aspx) for information about the data type mappings.

```
    SQL_DATE_STRUCT date;
    SQLLEN cbdate;   // size of date structure  

    SQLCHAR SSN[12];
    strcpy_s((char*)SSN, _countof(SSN), "795-73-9838");

    SQLWCHAR* firstName = L"Catherine";
    SQLWCHAR* lastName = L"Abel";
    SQLINTEGER cbSSN = SQL_NTS, cbFirstName = SQL_NTS, cbLastName = SQL_NTS;

    // Initialize the date structure  
    date.day = 10;
    date.month = 9;
    date.year = 1996;

    // Size of structures   
    cbdate = sizeof(SQL_DATE_STRUCT);

    SQLRETURN rc = 0;

    string queryText = "INSERT INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (?, ?, ?, ?) ";

    rc = SQLPrepare(hstmt, (SQLCHAR *)queryText.c_str(), SQL_NTS);

    //SSN
    rc = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, 11, 0, (SQLPOINTER)SSN, 0, &cbSSN);
    //FirstName
    rc = SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT, SQL_C_WCHAR, SQL_WCHAR, 50, 0, (SQLPOINTER)firstName, 0, &cbFirstName);
    //LastName
    rc = SQLBindParameter(hstmt, 3, SQL_PARAM_INPUT, SQL_C_WCHAR, SQL_WCHAR, 50, 0, (SQLPOINTER)lastName, 0, &cbLastName);
    //BirthDate
    rc = SQLBindParameter(hstmt, 4, SQL_PARAM_INPUT, SQL_C_TYPE_DATE, SQL_TYPE_DATE, 10, 0, (SQLPOINTER)&date, 0, &cbdate);

    rc = SQLExecute(hstmt);
```

#### Plaintext Data Retrieval Example

The following example demonstrates filtering data based on encrypted values, and retrieving plaintext data from encrypted columns. Note the following:

- The value used in the WHERE clause to filter on the SSN column needs to be passed using SQLBindParameter, so that the driver can transparently encrypt it before sending it to the server.

- All values printed by the program will be in plaintext, since the driver will transparently decrypt the data retrieved from the SSN and BirthDate columns.

> [!NOTE]
> Queries can perform equality comparisons on encrypted columns only if the encryption is deterministic. For more information, see [Selecting Deterministic or Randomized encryption](../../relational-databases/security/encryption/always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption).

```
SQLCHAR SSN[12];
strcpy_s((char*)SSN, _countof(SSN), "795-73-9838");

SQLWCHAR* firstName = L"Catherine";
SQLWCHAR* lastName = L"Abel";
SQLINTEGER cbSSN = SQL_NTS, cbFirstName = SQL_NTS, cbLastName = SQL_NTS;

SQLRETURN rc = 0;
string empty = "";
string queryText = "SELECT [SSN], [FirstName], [LastName], [BirthDate] " + empty +
    "FROM  [dbo].[Patients]" +
    "WHERE " +
    "[SSN] = ? ";

rc = SQLPrepare(hstmt, (SQLCHAR *)queryText.c_str(), SQL_NTS);

//SSN
rc = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, 11, 0, (SQLPOINTER)SSN, 0, &cbSSN);

rc = SQLExecute(hstmt);
HandleDiagnosticRecord(hstmt, SQL_HANDLE_STMT, rc);

SQL_DATE_STRUCT dateVal;
SQLWCHAR firstNameVal[50];
SQLWCHAR lastNameVal[50];
SQLCHAR SSNVal[12];
SQLLEN cbdate;   // size of date structure  

int rowcount = 0;
while (SQL_SUCCEEDED(SQLFetch(hstmt)))
{
    rowcount++;
    SQLGetData(hstmt, 1, SQL_C_CHAR, &SSNVal, 11, &cbSSN);
    SQLGetData(hstmt, 2, SQL_C_WCHAR, &firstNameVal, 50, &cbFirstName);
    SQLGetData(hstmt, 3, SQL_C_WCHAR, &lastNameVal, 50, &cbLastName);
    SQLGetData(hstmt, 4, SQL_C_TYPE_DATE, &dateVal, 10, &cbdate);        
}
```

#### Ciphertext Data Retrieval Example

If Always Encrypted is not enabled, a query can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns.

The following example illustrates retrieving binary encrypted data from encrypted columns. Note the following:

- As Always Encrypted is not enabled in the connection string, the query will return encrypted values of SSN and BirthDate as byte arrays (the program converts the values to strings).
- A query retrieving data from encrypted columns with Always Encrypted disabled can have parameters, as long as none of the parameters target an encrypted column. The above query filters by LastName, which is not encrypted in the database. If the query filtered by SSN or BirthDate, the query would fail.


```
SQLCHAR SSN[12];
strcpy_s((char*)SSN, _countof(SSN), "795-73-9838");

SQLWCHAR* firstName = L"Catherine";
SQLWCHAR* lastName = L"Abel";
SQLINTEGER cbSSN = SQL_NTS, cbFirstName = SQL_NTS, cbLastName = SQL_NTS;

SQLRETURN rc = 0;
string empty = "";
string queryText = "SELECT [SSN], [FirstName], [LastName], [BirthDate] " + empty +
    "FROM  [dbo].[Patients]" +
    "WHERE " +
    "[LastName] = ?";

rc = SQLPrepare(hstmt, (SQLCHAR *)queryText.c_str(), SQL_NTS);

//LastName
rc = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_WCHAR, SQL_WCHAR, 50, 0, (SQLPOINTER)lastName, 0, &cbLastName);

rc = SQLExecute(hstmt);
HandleDiagnosticRecord(hstmt, SQL_HANDLE_STMT, rc);

SQL_DATE_STRUCT dateVal;
SQLWCHAR firstNameVal[50];
SQLWCHAR lastNameVal[50];
SQLCHAR SSNVal[12];
SQLLEN cbdate;   // size of date structure  

int rowcount = 0;
while (SQL_SUCCEEDED(SQLFetch(hstmt)))
{
    rowcount++;
    SQLGetData(hstmt, 1, SQL_C_CHAR, &SSNVal, 11, &cbSSN);
    SQLGetData(hstmt, 2, SQL_C_WCHAR, &firstNameVal, 50, &cbFirstName);
    SQLGetData(hstmt, 3, SQL_C_WCHAR, &lastNameVal, 50, &cbLastName);
    SQLGetData(hstmt, 4, SQL_C_TYPE_DATE, &dateVal, 10, &cbdate);        
}
```

#### Avoiding Common Problems when Querying Encrypted Columns

This section describes common categories of errors when querying encrypted columns from ODBC applications and a few guidelines on how to avoid them.

##### Unsupported data type conversion errors

Always Encrypted supports few conversions for encrypted data types. See [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) for the detailed list of supported type conversions. To avoid data type conversion errors, make sure that you observe the following points when using SQLBindParameter with parameters targeting encrypted columns:

- The SQL type of the parameter is either exactly the same as the type of the targeted column, or the conversion from the SQL type to the type of the column is supported.

- The precision and scale of parameters targeting columns of the `decimal` and `numeric` SQL Server data types is the same as the precision and scale configured for the target column.

- The precision of parameters targeting columns of `datetime2`, `datetimeoffset`, or `time` SQL Server data types is not greater than the precision for the target column, in queries that modify the target column.  

##### Errors due to passing plaintext instead of encrypted values

Any value that targets an encrypted column needs to be encrypted before being sent to the server. An attempt to insert, modify, or filter by a plaintext value on an encrypted column will result in an error. To prevent such errors, make sure that:

- Always Encrypted is enabled (in the DSN, the connection string, before connecting by setting the `SQL_COPT_SS_COLUMN_ENCRYPTION` connection attribute for a specific connection, or the `SQL_SOPT_SS_COLUMN_ENCRYPTION` statement attribute for a specific statement).

- You use SQLBindParameter to send data targeting encrypted columns. The example below shows a query that incorrectly filters by a literal/constant on an encrypted column (SSN), instead of passing the literal as an argument to SQLBindParameter.

```
string queryText = "SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE SSN='795-73-9838'";
```

### Precautions when using SQLSetPos and SQLMoreResults

#### SQLSetPos

The `SQLSetPos` API allows an application to update rows in a resultset using buffers that were bound with SQLBindCol and into which row data was previously fetched. Due to the asymmetric padding behavior of encrypted fixed-length types, it is possible to unexpectedly alter the data of these columns while performing updates on other columns in the row. With AE, fixed length character values will be padded if the value is smaller than the buffer size.

To mitigate this behavior, use the `SQL_COLUMN_IGNORE` flag to ignore columns that will not be updated as part of `SQLBulkOperations` and when using `SQLSetPos` for cursor based updates.  All columns that are not being directly modified by the application should be ignored, both for performance and to avoid truncation of columns that are bound to a buffer *smaller* than their actual (DB) size. For more information, see [SQLSetPos Function reference](https://msdn.microsoft.com/library/ms713507(v=vs.85).aspx).

#### SQLMoreResults & SQLDescribeCol

Application programs may call [SQLDescribeCol](https://msdn.microsoft.com/library/ms716289(v=vs.85).aspx) to return metadata about columns in prepared statements.  When Always Encrypted is enabled, calling `SQLMoreResults` *before* calling `SQLDescribeCol` causes [sp_describe_first_result_set](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) to be called, which does not correctly return the plaintext metadata for encrypted columns. To avoid this issue, call `SQLDescribeCol` on prepared statements *before* calling `SQLMoreResults`.

## Controlling the Performance Impact of Always Encrypted

Because Always Encrypted is a client-side encryption technology, most of the performance overhead is observed on the client side, not in the database. Apart from the cost of encryption and decryption operations, the other sources of performance overhead on the client side are:

- Additional round trips to the database to retrieve metadata for query parameters.

- Calls to a column master key store to access a column master key.

This section describes the built-in performance optimizations in the ODBC Driver for SQL Server and how you can control the impact of the above two factors on performance.

### Controlling Round-trips to Retrieve Metadata for Query Parameters

If Always Encrypted is enabled for a connection, the driver will, by default, call [sys.sp_describe_parameter_encryption](../../relational-databases/system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md) for each parameterized query, passing the query statement (without any parameter values) to SQL Server. This stored procedure analyzes the query statement to find out if any parameters need to be encrypted, and if so, returns the encryption-related information for each parameter to allow the driver to encrypt them. The above behavior ensures a high-level of transparency to the client application: The application (and the application developer) does not need to be aware of which queries access encrypted columns, as long as the values targeting encrypted columns are passed to the driver in parameters.

### Per-Statement Always Encrypted Behavior

To control the performance impact of retrieving encryption metadata for parameterized queries, you can alter the Always Encrypted behavior for individual queries if it has been enabled on the connection. This way, you can ensure that `sys.sp_describe_parameter_encryption` is invoked only for queries that you know have parameters targeting encrypted columns. Note, however, that by doing so, you reduce transparency of encryption: if you encrypt additional columns in your database, you may need to change the code of your application to align it with the schema changes.

To control the Always Encrypted behavior of a statement, call SQLSetStmtAttr to set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` statement attribute to one of the following values:

|Value|Description|
|-|-|
|`SQL_CE_DISABLED` (0)|Always Encrypted is disabled for the statement|
|`SQL_CE_RESULTSETONLY` (1)|Decryption Only. Resultsets and return values are decrypted, and parameters are not encrypted|
|`SQL_CE_ENABLED` (3) | Always Encrypted is enabled and used for both parameters and results|

New statement handles created from a connection with Always Encrypted enabled default to SQL_CE_ENABLED. Those created from a connection with it disabled default to SQL_CE_DISABLED (and it is not possible to enable Always Encrypted on them.)

If most of the queries of a client application access encrypted columns, the following is recommended:

- Set the `ColumnEncryption` connection string keyword to `Enabled`.

- Set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` attribute to `SQL_CE_DISABLED` on statements which do not access any encrypted columns. This will disable both calling `sys.sp_describe_parameter_encryption` as well as attempts to decrypt any values in the result set.
    
- Set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` attribute to `SQL_CE_RESULTSETONLY` on statements which do not have any parameters requiring encryption, but retrieve data from encrypted columns. This will disable calling `sys.sp_describe_parameter_encryption` and parameter encryption. Results containing encrypted columns will continue to be decrypted.

## Always Encrypted Security Settings

### Force Column Encryption

To enforce the encryption of a parameter, set the `SQL_CA_SS_FORCE_ENCRYPT` implementation parameter descriptor (IPD) field through a call to the SQLSetDescField function. A non-zero value causes the driver to return an error when no encryption metadata is returned for the associated parameter.

```
SQLHDESC ipd;
SQLGetStmtAttr(hStmt, SQL_ATTR_IMP_PARAM_DESC, &ipd, 0, 0);
SQLSetDescField(ipd, paramNum, SQL_CA_SS_FORCE_ENCRYPT, (SQLPOINTER)TRUE, SQL_IS_SMALLINT);   
```

If SQL Server informs the driver that the parameter does not need to be encrypted, queries using that parameter will fail. This provides additional protection against security attacks which involve a compromised SQL Server providing incorrect encryption metadata to the client, which may lead to data disclosure.

### Column Encryption Key Caching

To reduce the number of calls to a column master key store to decrypt column encryption keys, the driver caches the plaintext CEKs in memory. The CEK cache is global to the driver and not associated with any one connection. After receiving the ECEK from database metadata, the driver first tries to find the plaintext CEK corresponding to the encrypted key value in the cache. The driver calls the key store containing the CMK only if it cannot find the corresponding plaintext CEK in the cache.

> [!NOTE]
> In the ODBC Driver for SQL Server, the entries in the cache are evicted after a two hour timeout. This means that for a given ECEK, the driver contacts the key store only once during the lifetime of the application or every two hours, whichever is less.

Starting with the ODBC Driver 17.1 for SQL Server, the CEK cache timeout can be adjusted using the `SQL_COPT_SS_CEKCACHETTL` connection attribute, which specifies the number of seconds a CEK will remain in the cache. Due to the global nature of the cache, this attribute can be adjusted from any connection handle valid for the driver. When the cache TTL is decreased, existing CEKs which would exceed the new TTL are also evicted. If it is 0, no CEKs are cached.

### Trusted Key Paths

Starting with the ODBC Driver 17.1 for SQL Server, the `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attribute allows an application to require that Always Encrypted operations only use a specified list of CMKs, identified by their key paths. By default, this attribute is NULL, which means that the driver accepts any key path. To use this feature, set `SQL_COPT_SS_TRUSTEDCMKPATHS` to point to a null-delimited, null-terminated wide-character string which lists the allowed key path(s). The memory pointed to by this attribute must remain valid during encryption or decryption operations using the connection handle on which it is set --- upon which the driver will check if the CMK path as specified by the server metadata is case-insensitively in this list. If the CMK path is not in the list, the operation fails. The application can change the contents of memory this attribute points at, to change its list of trusted CMKs, without setting the attribute again.

## Working with Column Master Key Stores

To encrypt or decrypt data, the driver needs to obtain a CEK that is configured for the target column. CEKs are stored in encrypted form (ECEKs) in the database metadata. Each CEK has a corresponding CMK that was used to encrypt it. The [database metadata](../../t-sql/statements/create-column-master-key-transact-sql.md) does not store the CMK itself; it only contains the name of the keystore and information which the keystore can use to locate the CMK.

To obtain the plaintext value of an ECEK, the driver first obtains the metadata about both the CEK and its corresponding CMK, and then it uses this information to contact the keystore containing the CMK and requests it to decrypt the ECEK. The driver communicates with a keystore using a keystore provider.

### Built-in Keystore Providers

The ODBC Driver for SQL Server comes with the following built-in keystore providers:

| Name | Description | Provider (metadata) name |Availability|
|:---|:---|:---|:---|
|Azure Key Vault |Stores CMKs in an Azure Key Vault | `AZURE_KEY_VAULT` |Windows, macOS, Linux|
|Windows Certificate Store|Stores CMKs locally in the Windows keystore| `MSSQL_CERTIFICATE_STORE`|Windows|

- You (or your DBA) need to make sure that the provider name, configured in the column master key metadata, is correct and the column master key path complies with the key path format for the given provider. It is recommended that you configure the keys using tools such as SQL Server Management Studio, which automatically generates the valid provider names and key paths when issuing the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../t-sql/statements/create-column-master-key-transact-sql.md) statement.

- You need to ensure your application can access the key in the keystore. This may involve granting your application access to the key and/or the keystore, depending on the keystore, or performing other keystore-specific configuration steps. For example, to access an Azure Key Vault, you need to provide the correct credentials to the keystore.

### Using the Azure Key Vault Provider

Azure Key Vault is a convenient option to store and manage column master keys for Always Encrypted (especially if your applications are hosted in Azure). The ODBC Driver for SQL Server on Linux, macOS, and Windows includes a built-in column master key store provider for Azure Key Vault. See [Azure Key Vault - Step by Step](https://blogs.technet.microsoft.com/kv/2015/06/02/azure-key-vault-step-by-step/), [Getting Started with Key Vault](https://azure.microsoft.com/documentation/articles/key-vault-get-started/), and [Creating Column Master Keys in Azure Key Vault](https://msdn.microsoft.com/library/mt723359.aspx#Anchor_2) for more information on configuring an Azure Key Vault for Always Encrypted.

> [!NOTE]
> On Linux and macOS, for driver version 17.2 and later, `libcurl` is required to use this provider, but is not an explicit dependency since other operations with the driver do not require it. If you encounter an error regarding `libcurl`, ensure it is installed.

The driver supports authenticating to Azure Key Vault using the following credential types:

- Username/Password - with this method, the credentials are the name of an Azure Active Directory user and its password.

- Client ID/Secret - with this method, the credentials are an application client ID and an application secret.

- Managed Service Identity - with this method, the credentials are system-assigned identity or user-assigned identity. For user-assigned identity, UID is set to the object ID of the user identity.

To allow the driver to use CMKs stored in AKV for column encryption, use the following connection-string-only keywords:

|Credential Type| `KeyStoreAuthentication` |`KeyStorePrincipalId`| `KeyStoreSecret` |
|-|-|-|-|
|Username/password| `KeyVaultPassword`|User Principal Name|Password|
|Client ID/secret| `KeyVaultClientSecret`|Client ID|Secret|

#### Example Connection Strings

The following connection strings show how to authenticate to Azure Key Vault with the two credential types:

**ClientID/Secret**:

```
DRIVER=ODBC Driver 13 for SQL Server;SERVER=myServer;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultClientSecret;KeyStorePrincipalId=<clientId>;KeyStoreSecret=<secret>
```

**Username/Password**:

```
DRIVER=ODBC Driver 13 for SQL Server;SERVER=myServer;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultPassword;KeyStorePrincipalId=<username>;KeyStoreSecret=<password>
```

No other ODBC application changes are required to use AKV for CMK storage.

### Using the Windows Certificate Store Provider

The ODBC Driver for SQL Server on Windows includes a built-in column master key store provider for the Windows Certificate Store, named `MSSQL_CERTIFICATE_STORE`. (This provider is not available on macOS or Linux.) With this provider, the CMK is stored locally on the client machine and no additional configuration by the application is necessary to use it with the driver. However, the application must have access to the certificate and its private key in the store. See [Create and Store Column Master Keys (Always Encrypted)](https://docs.microsoft.com/sql/relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted) for more information.

### Using Custom Keystore Providers

The ODBC Driver for SQL Server also supports custom third-party keystore providers using the CEKeystoreProvider interface. This allows an application to load, query, and configure keystore providers so that they can be used by the driver to access encrypted columns. Applications may also directly interact with a keystore provider in order to encrypt CEKs for storage in SQL Server and perform tasks beyond accessing encrypted columns with ODBC; for more information, see [Custom Keystore Providers](../../connect/odbc/custom-keystore-providers.md).

Two connection attributes are used to interact with custom keystore providers. They are:

- `SQL_COPT_SS_CEKEYSTOREPROVIDER`

- `SQL_COPT_SS_CEKEYSTOREDATA`

The former is used to load and enumerate loaded keystore providers, while the latter enables application-provider communications. These connection attributes may be used at any time, before or after establishing a connection, since application-provider interaction does not involve communication with SQL Server. However, because the driver has not been loaded yet, setting and getting these attributes before connecting will cause them to be processed by the Driver Manager, and may not yield the expected results.

#### Loading a Keystore Provider

Setting the `SQL_COPT_SS_CEKEYSTOREPROVIDER` connection attribute enables a client application to load a provider library, making available for use the keystore providers contained therein.

```
SQLRETURN SQLSetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER StringLength);
```

| Argument | Description |
|:---|:---|
|`ConnectionHandle`|[Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process.|
|`Attribute`|[Input] Attribute to set: the `SQL_COPT_SS_CEKEYSTOREPROVIDER` constant.|
|`ValuePtr`|[Input] Pointer to a null-terminated character string specifying the filename of the provider library. For SQLSetConnectAttrA, this is an ANSI (multibyte) string. For SQLSetConnectAttrW, this is a Unicode (wchar_t) string.|
|`StringLength`|[Input] The length of the ValuePtr string, or SQL_NTS.|

The driver attempts to load the library identified by the ValuePtr parameter using the platform-defined dynamic library loading mechanism (`dlopen()` on Linux and macOS, `LoadLibrary()` on Windows), and adds any providers defined therein to the list of providers known to the driver. The following errors may occur:

| Error | Description |
|:--|:--|
|`CE203`|The dynamic library could not be loaded.|
|`CE203`|The "CEKeyStoreProvider" exported symbol was not found in the library.|
|`CE203`|One or more providers in the library are already loaded.|

`SQLSetConnectAttr` returns the usual error or success values, and additional information is available for any errors which occurred via the standard ODBC diagnostic mechanism.

> [!NOTE]
> The application programmer must ensure that any custom providers are loaded before any query requiring them is sent over any connection. Failure to do so results in the error:

| Error | Description |
|:--|:--|
|`CE200`|Keystore provider %1 not found. Ensure that the appropriate keystore provider library has been loaded.|

> [!NOTE]
> Keystore provider implementors should avoid the use of `MSSQL` in the name of their custom providers. This term is reserved exclusively for Microsoft use and may cause conflicts with future built-in providers. Using this term in the name of a custom provider may result in an ODBC warning.

#### Getting the List of Loaded Providers

Getting this connection attribute enables a client application to determine the keystore providers currently loaded in the driver (including those built in.) This can only be performed after connecting.

```
SQLRETURN SQLGetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER BufferLength, SQLINTEGER * StringLengthPtr);
```

| Argument | Description |
|:---|:---|
|`ConnectionHandle`|[Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process.|
|`Attribute`|[Input] Attribute to retrieve: the `SQL_COPT_SS_CEKEYSTOREPROVIDER` constant.|
|`ValuePtr`|[Output] A pointer to memory in which to return the next loaded provider name.|
|`BufferLength`|[Input] The length of the buffer ValuePtr.|
|`StringLengthPtr`|[Output] A pointer to a buffer in which to return the total number of bytes (excluding the null-termination character) available to return in \*ValuePtr. If ValuePtr is a null pointer, no length is returned. If the attribute value is a character string and the number of bytes available to return is greater than BufferLength minus the length of the null-termination character, the data in \*ValuePtr is truncated to BufferLength minus the length of the null-termination character and is null-terminated by the driver.|

To allow retrieving the entire list, every Get operation returns the current provider's name, and increments an internal counter to the next one. Once this counter reaches the end of the list, an empty string ("") is returned, and the counter is reset; successive Get operations then proceed again from the beginning of the list.

### Communicating with Keystore Providers

The `SQL_COPT_SS_CEKEYSTOREDATA` connection attribute enables a client application to communicate with loaded keystore providers for configuring additional parameters, keying material, etc. The communication between a client application and a provider follows a simple request-response protocol, based on Get and Set requests using this connection attribute. Communication is initiated only by the client application.

> [!NOTE]
> Due to the nature of the ODBC calls CEKeyStoreProvider's respond to (SQLGet/SetConnectAttr), the ODBC interface only supports setting data at the resolution of the connection context.

The application communicates with keystore providers through the driver via the CEKeystoreData structure:

```
typedef struct CEKeystoreData {
wchar_t *name;
unsigned int dataSize;
char data[];
} CEKEYSTOREDATA;
```

| Argument | Description |
|:---|:---|
|`name`|[Input] Upon Set, the name of the provider to which the data is sent. Ignored upon Get. Null-terminated, wide-character string.|
|`dataSize`|[Input] The size of the data array following the structure.|
|`data`|[InOut] Upon Set, the data to be sent to the provider. This may be arbitrary data; the driver makes no attempt to interpret it. Upon Get, the buffer to receive the data read from the provider.|

#### Writing data to a provider

A `SQLSetConnectAttr` call using the `SQL_COPT_SS_CEKEYSTOREDATA` attribute writes a "packet" of data to the specified keystore provider.
```
SQLRETURN SQLSetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER StringLength);
```

| Argument | Description |
|:---|:---|
|`ConnectionHandle`| [Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process.|
|`Attribute`|[Input] Attribute to set: the `SQL_COPT_SS_CEKEYSTOREDATA` constant.|
|`ValuePtr`|[Input] Pointer to a CEKeystoreData structure. The name field of the structure identifies the provider for which the data is intended.|
|`StringLength`|[Input] SQL_IS_POINTER constant|

Additional detailed error information may be obtained via [SQLGetDiacRec](https://msdn.microsoft.com/library/ms710921(v=vs.85).aspx).

> [!NOTE]
> The provider can use the connection handle to associate the written data to a specific connection, if it so desires. This is useful for implementing per-connection configuration. It may also ignore the connection context and treat the data identically regardless of the connection used to send the data. See [Context Association](../../connect/odbc/custom-keystore-providers.md#context-association) for more information.

#### Reading data from a provider

A call to `SQLGetConnectAttr` using the `SQL_COPT_SS_CEKEYSTOREDATA` attribute reads a "packet" of data from *the last-written-to* provider. If there was none, a Function Sequence Error occurs. Keystore provider implementers are encouraged to support "dummy writes" of 0 bytes as a way of selecting the provider for read operations without causing other side-effects, if it makes sense to do so.

```
SQLRETURN SQLGetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER BufferLength, SQLINTEGER * StringLengthPtr);
```

| Argument | Description |
|:---|:---|
|`ConnectionHandle`|[Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process.|
|`Attribute`|[Input] Attribute to retrieve: the `SQL_COPT_SS_CEKEYSTOREDATA` constant.|
|`ValuePtr`|[Output] A pointer to a CEKeystoreData structure in which the data read from the provider is placed.|
|`BufferLength`|[Input] SQL_IS_POINTER constant|
|`StringLengthPtr`|[Output] A pointer to a buffer in which to return BufferLength. If *ValuePtr is a null pointer, no length is returned.|

The caller must ensure that a buffer of sufficient length following the CEKEYSTOREDATA structure is allocated for the provider to write into. Upon return, its dataSize field is updated with the actual length of data read from the provider. Additional detailed error information may be obtained via [SQLGetDiacRec](https://msdn.microsoft.com/library/ms710921(v=vs.85).aspx).

This interface places no additional requirements on the format of data transferred between an application and a keystore provider. Each provider can define its own protocol/data format, depending on its needs.

For an example of implementing your own keystore provider, see [Custom Keystore Providers](../../connect/odbc/custom-keystore-providers.md)

## Limitations of the ODBC driver when using Always Encrypted

### Asynchronous Operations
While the ODBC driver will allow the use of [asynchronous operations](../../relational-databases/native-client/odbc/creating-a-driver-application-asynchronous-mode-and-sqlcancel.md) with Always Encrypted, there is a performance impact on the operations when Always Encrypted is enabled. The call to `sys.sp_describe_parameter_encryption` to determine encryption metadata for the statement is blocking and will cause the driver to wait for the server to return the metadata before returning `SQL_STILL_EXECUTING`.

### Retrieve data in parts with SQLGetData
Before ODBC Driver 17 for SQL Server, encrypted character and binary columns cannot be retrieved in parts with SQLGetData. Only one call to SQLGetData can be made, with a buffer of sufficient length to contain the entire column's data.

### Send data in parts with SQLPutData
Data for insertion or comparison cannot be sent in parts with SQLPutData. Only one call to SQLPutData can be made, with a buffer containing the entire data. For inserting long data into encrypted columns, use the Bulk Copy API, described in the next section, with an input data file.

### Encrypted money and smallmoney
Encrypted **money** or **smallmoney** columns cannot be targeted by parameters, since there is no specific ODBC data type which maps to those types, resulting in Operand Type Clash errors.

## Bulk Copy of Encrypted Columns

Use of the [SQL Bulk Copy functions](../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md) and the **bcp** utility is supported with Always Encrypted since ODBC Driver 17 for SQL Server. Both plaintext (encrypted on insertion and decrypted on retrieval) and ciphertext (transferred verbatim) can be inserted and retrieved using the Bulk Copy (bcp_&#42;) APIs and the **bcp** utility.

- To retrieve ciphertext in varbinary(max) form (e.g. for bulk loading into a different database), connect without the `ColumnEncryption` option (or set it to `Disabled`) and perform a BCP OUT operation.

- To insert and retrieve plaintext, and let the driver transparently perform encryption and decryption as required, setting `ColumnEncryption` to `Enabled` is sufficient. The functionality of the BCP API is otherwise unchanged.

- To insert ciphertext in varbinary(max) form (e.g. as retrieved above), set the `BCPMODIFYENCRYPTED` option to TRUE and perform a BCP IN operation. In order for the resulting data to be decryptable, ensure that the destination column's CEK is the same as that from which the ciphertext was originally obtained.

When using the **bcp** utility: To control the `ColumnEncryption` setting, use the -D option and specify a DSN containing the desired value. To insert ciphertext, ensure the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` setting of the user is enabled.

The following table provides a summary of the actions when operating on an encrypted column:

|`ColumnEncryption`|BCP Direction|Description|
|----------------|-------------|-----------|
|`Disabled`|OUT (to client)|Retrieves ciphertext. The observed datatype is **varbinary(max)**.|
|`Enabled`|OUT (to client)|Retrieves plaintext. The driver will decrypt the column data.|
|`Disabled`|IN (to server)|Inserts ciphertext. This is intended for opaquely moving encrypted data without requiring it to be decrypted. The operation will fail if the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option is not set on the user, or BCPMODIFYENCRYPTED is not set on the connection handle. See below for more information.|
|`Enabled`|IN (to server)|Inserts plaintext. The driver will encrypt the column data.|

### The BCPMODIFYENCRYPTED option

To prevent data corruption, the server normally does not allow inserting ciphertext directly into an encrypted column, and thus attempts to do so will fail; however, for bulk loading of encrypted data using the BCP API, setting the `BCPMODIFYENCRYPTED` [bcp_control](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-control.md) option to TRUE will allow ciphertext to be inserted directly, and reduces the risk of corrupting encrypted data over setting the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option on the user account. Nonetheless, the keys must match the data and it is a good idea to perform some read-only checks of the inserted data after the bulk insertion and before further use.

See [Migrate Sensitive Data Protected by Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md) for more information.

## Always Encrypted API Summary

### Connection String Keywords

|Name|Description|  
|----------|-----------------|  
|`ColumnEncryption`|Accepted values are `Enabled`/`Disabled`.<br>`Enabled` -- enables Always Encrypted functionality for the connection.<br>`Disabled` -- disable Always Encrypted functionality for the connection. <br><br>The default is `Disabled`.|  
|`KeyStoreAuthentication` | Valid Values: `KeyVaultPassword`, `KeyVaultClientSecret` |
|`KeyStorePrincipalId` | When `KeyStoreAuthentication` = `KeyVaultPassword`, set this value to a valid Azure Active Directory User Principal Name. <br>When `KeyStoreAuthetication` = `KeyVaultClientSecret` set this value to a valid Azure Active Directory Application Client ID |
|`KeyStoreSecret` | When `KeyStoreAuthentication` = `KeyVaultPassword` set this value to the password for the corresponding user name. <br>When `KeyStoreAuthentication` = `KeyVaultClientSecret` set this value to the Application Secret associated with a valid Azure Active Directory Application Client ID |


### Connection Attributes

|Name|Type|Description|  
|----------|-------|----------|  
|`SQL_COPT_SS_COLUMN_ENCRYPTION`|Pre-connect|`SQL_COLUMN_ENCRYPTION_DISABLE` (0) -- Disable Always Encrypted <br>`SQL_COLUMN_ENCRYPTION_ENABLE` (1) -- Enable Always Encrypted|
|`SQL_COPT_SS_CEKEYSTOREPROVIDER`|Post-connect|[Set] Attempt to load CEKeystoreProvider<br>[Get] Return a CEKeystoreProvider name|
|`SQL_COPT_SS_CEKEYSTOREDATA`|Post-connect|[Set] Write data to CEKeystoreProvider<br>[Get] Read data from CEKeystoreProvider|
|`SQL_COPT_SS_CEKCACHETTL`|Post-connect|[Set] Set the CEK cache TTL<br>[Get] Get the current CEK cache TTL|
|`SQL_COPT_SS_TRUSTEDCMKPATHS`|Post-connect|[Set] Set the trusted CMK paths pointer<br>[Get] Get the current trusted CMK paths pointer|

### Statement Attributes

|Name|Description|  
|----------|-----------------|  
|`SQL_SOPT_SS_COLUMN_ENCRYPTION`|`SQL_CE_DISABLED` (0) -- Always Encrypted is disabled for the statement <br>`SQL_CE_RESULTSETONLY` (1) -- Decryption Only. Resultsets and return values are decrypted, and parameters are not encrypted <br>`SQL_CE_ENABLED` (3) -- Always Encrypted is enabled and used for both parameters and results|

### Descriptor Fields

|IPD Field|Size/Type|Default Value|Description|
|-|-|-|-|  
|`SQL_CA_SS_FORCE_ENCRYPT` (1236)|WORD (2 bytes)|0|When 0 (default): decision to encrypt this parameter is determined by availability of encryption metadata.<br><br>When nonzero: if encryption metadata is available for this parameter, it is encrypted. Otherwise, the request fails with error [CE300] [Microsoft][ODBC Driver 13 for SQL Server]Mandatory encryption was specified for a parameter but no encryption metadata was provided by the server.|

### bcp_control Options

|Option Name|Default Value|Description|
|-|-|-|
|`BCPMODIFYENCRYPTED` (21)|FALSE|When TRUE, allows varbinary(max) values to be inserted into an encrypted column. When FALSE, prevents insertion unless correct type and encryption metadata is supplied.|

## See Also

- [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Always Encrypted blog](https://blogs.msdn.com/b/sqlsecurity/archive/tags/always-encrypted/)

