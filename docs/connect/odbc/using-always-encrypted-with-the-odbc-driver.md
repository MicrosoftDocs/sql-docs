---
title: Using Always Encrypted
description: Learn how to develop ODBC applications using Always Encrypted and the Microsoft ODBC Driver for SQL Server.
author: v-chojas
ms.author: v-chojas
ms.reviewer: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using Always Encrypted with the ODBC Driver for SQL Server

[!INCLUDE[Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

## Applicable to

- ODBC Driver 13.1+ for SQL Server

## Introduction

This article provides information on how to develop ODBC applications using [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) or [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md) and the [ODBC Driver for SQL Server](microsoft-odbc-driver-for-sql-server.md).

Always Encrypted allows client applications to encrypt sensitive data and never reveal the data or the encryption keys to SQL Server or Azure SQL Database. An Always Encrypted enabled driver, such as the ODBC Driver for SQL Server, achieves this security by transparently encrypting and decrypting sensitive data in the client application. The driver automatically determines which query parameters correspond to sensitive database columns (protected using Always Encrypted), and encrypts the values of those parameters before passing the data to SQL Server or Azure SQL Database. Similarly, the driver transparently decrypts data retrieved from encrypted database columns in query results. Always Encrypted *with secure enclaves* extends this feature to enable richer functionality on sensitive data while keeping the data confidential.

For more information, see [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) and [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

### Prerequisites

Configure Always Encrypted in your database. This process involves provisioning Always Encrypted keys and setting up encryption for selected database columns. If you don't already have a database with Always Encrypted configured, follow the directions in [Tutorial: Getting started with Always Encrypted](../../relational-databases/security/encryption/always-encrypted-tutorial-getting-started.md). In particular, your database should contain the metadata definitions for a Column Master Key (CMK), a Column Encryption Key (CEK), and a table containing one or more columns encrypted using that CEK.

If you're using Always Encrypted with secure enclaves, see [Develop applications using Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves-client-development.md) for more prerequisites.

### Enabling Always Encrypted in an ODBC application

The easiest way to enable both parameter encryption and resultset encrypted column decryption is by setting the value of the `ColumnEncryption` connection string keyword to **Enabled**. The following code is an example of a connection string that enables Always Encrypted:

```cpp
SQLWCHAR *connString = L"Driver={ODBC Driver 18 for SQL Server};Server={myServer};Encrypt=yes;Trusted_Connection=yes;ColumnEncryption=Enabled;";
```

Always Encrypted may also be enabled in the DSN configuration, using the same key and value (which will be overridden by the connection string setting, if present), or programmatically with the `SQL_COPT_SS_COLUMN_ENCRYPTION` pre-connection attribute. Setting it this way overrides the value set in the connection string or DSN:

```cpp
 SQLSetConnectAttr(hdbc, SQL_COPT_SS_COLUMN_ENCRYPTION, (SQLPOINTER)SQL_COLUMN_ENCRYPTION_ENABLE, 0);
```

Once enabled for the connection, the behavior of Always Encrypted may be adjusted for individual queries. For more information, see [Controlling the Performance Impact of Always Encrypted](#controlling-the-performance-impact-of-always-encrypted) below.

Enabling Always Encrypted isn't sufficient for encryption or decryption to succeed; you also need to make sure that:

- The application has the *VIEW ANY COLUMN MASTER KEY DEFINITION* and *VIEW ANY COLUMN ENCRYPTION KEY DEFINITION* database permissions, required to access the metadata about Always Encrypted keys in the database. For details, see [Database Permissions](../../relational-databases/security/encryption/always-encrypted-database-engine.md#database-permissions).

- The application can access the CMK that protects the CEKs for the queried encrypted columns. This behavior is dependent on the keystore provider that stores the CMK. For more information, see [Working with Column Master Key Stores](#working-with-column-master-key-stores).

### Enabling Always Encrypted with secure enclaves

> [!NOTE]
> On Linux and macOS, OpenSSL version 1.0.1 or later is required to use Always Encrypted with secure enclaves.

Beginning with version 17.4, the driver supports Always Encrypted with secure enclaves. To enable the use of the enclave when connecting to a database, set the `ColumnEncryption` DSN key, connection string keyword, or connection attribute to the following value: `<attestation protocol>,<attestation URL>`, where:

- `<attestation protocol>` - specifies a protocol used for enclave attestation.
  - If you're using [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), `<attestation protocol>` should be `VBS-HGS`.
  - If you're using [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and Microsoft Azure Attestation, `<attestation protocol>` should be `SGX-AAS`.
  - If you do not require attestation, `<attestation-protocol>` should be `VBS-NONE`. (Version 18.1+)


- `<attestation URL>` - specifies an attestation URL (an attestation service endpoint). You need to obtain an attestation URL for your environment from your attestation service administrator.

  - If you're using [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), see [Determine and share the HGS attestation URL](../../relational-databases/security/encryption/always-encrypted-enclaves-host-guardian-service-deploy.md#step-6-determine-and-share-the-hgs-attestation-url).
  - If you're using [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and Microsoft Azure Attestation, see [Determine the attestation URL for your attestation policy](../../relational-databases/security/encryption/always-encrypted-enclaves.md#secure-enclave-attestation).
  - If you do not require attestation, do not specify an attestation URL (nor the preceding comma). (Version 18.1+)

Examples of connection strings enabling enclave computations for a database connection:

- [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)]:
  
   ```cpp
   "Driver=ODBC Driver 18 for SQL Server;Server=myServer.myDomain;Encrypt=yes;Database=myDataBase;Trusted_Connection=Yes;ColumnEncryption=VBS-HGS,http://myHGSServer.myDomain/Attestation"
   ```

- [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]:
  
   ```cpp
   "Driver=ODBC Driver 18 for SQL Server;Server=myServer.database.windows.net;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Encrypt=yes;ColumnEncryption=SGX-AAS,https://myAttestationProvider.uks.attest.azure.net/"
   ```

- No attestation (v18.1+):
  
   ```cpp
   "Driver=ODBC Driver 18 for SQL Server;Server=myServer.database.windows.net;Database=myDataBase;Uid=myUsername;Pwd=myPassword;Encrypt=yes;ColumnEncryption=VBS-NONE"
   ```

If the server and attestation service are configured correctly along with enclave-enabled CMKs and CEKs for the encrypted columns, you can execute queries that use the enclave such as in-place encryption and rich computations, in addition to the existing functionality provided by Always Encrypted. For more information, see [Configure Always Encrypted with secure enclaves](../../relational-databases/security/encryption/configure-always-encrypted-enclaves.md).

### Retrieving and modifying data in encrypted columns

Once you enable Always Encrypted on a connection, you can use standard ODBC APIs. The ODBC APIs can retrieve or modify data in encrypted database columns. The following documentation items might be helpful:

- [ODBC sample code](cpp-code-example-app-connect-access-sql-db.md)
- [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md)

Your application must have the required database permissions, and must be able to access the column master key. Then the driver encrypts any query parameters that target encrypted columns. The driver also decrypts data retrieved from encrypted columns. The driver does all this encrypting and decrypting without any assistance from your source code. To your program, it's as if the columns aren't encrypted.

If Always Encrypted isn't enabled, queries with parameters that target encrypted columns will fail. Data can still be retrieved from encrypted columns, as long as the query has no parameters targeting encrypted columns. However, the driver won't attempt any decryption and the application will receive the binary encrypted data (as byte arrays).

The table below summarizes the behavior of queries, depending on whether Always Encrypted is enabled or not:

| Query characteristic | Always Encrypted is enabled and application can access the keys and key metadata | Always Encrypted is enabled and application can't access the keys or key metadata | Always Encrypted is disabled |
|--|--|--|--|
| Parameters targeting encrypted columns. | Parameter values are transparently encrypted. | Error | Error |
| Retrieving data from encrypted columns, without parameters targeting encrypted columns. | Results from encrypted columns are transparently decrypted. The application receives plaintext column values. | Error | Results from encrypted columns aren't decrypted. The application receives encrypted values as byte arrays. |

The following examples illustrate retrieving and modifying data in encrypted columns. The examples assume a table with the following schema. The SSN and BirthDate columns are encrypted.

```tsql
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

#### Data insertion example

This example inserts a row into the Patients table. Note the following details:

- There's nothing specific to encryption in the sample code. The driver automatically detects and encrypts the values of the SSN and date parameters, which target encrypted columns. This behavior makes encryption transparent to the application.

- The values inserted into database columns, including the encrypted columns, are passed as bound parameters (see [SQLBindParameter Function](../../odbc/reference/syntax/sqlbindparameter-function.md)). While using parameters is optional when sending values to non-encrypted columns (although it's highly recommended because it helps prevent SQL injection), it's required for values targeting encrypted columns. If the values inserted in the SSN or BirthDate columns were passed as literals embedded in the query statement, the query would fail because the driver doesn't attempt to encrypt or otherwise process literals in queries. As a result, the server would reject them as incompatible with the encrypted columns.

- The SQL type of the parameter inserted into the SSN column is set to SQL_CHAR, which maps to the **char** SQL Server data type (`rc = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, 11, 0, (SQLPOINTER)SSN, 0, &cbSSN);`). If the type of the parameter was set to SQL_WCHAR, which maps to **nchar**, the query would fail, as Always Encrypted doesn't support server-side conversions from encrypted nchar values to encrypted char values. See [ODBC Programmer's Reference -- Appendix D: Data Types](../../odbc/reference/appendixes/appendix-d-data-types.md) for information about the data type mappings.

```cpp
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

#### Plaintext data retrieval example

The following example demonstrates filtering data based on encrypted values, and retrieving plaintext data from encrypted columns. Note the following details:

- The value used in the WHERE clause to filter on the SSN column needs to be passed using SQLBindParameter, so that the driver can transparently encrypt it before sending it to the server.

- All values printed by the program will be in plaintext, since the driver will transparently decrypt the data retrieved from the SSN and BirthDate columns.

> [!NOTE]
> Queries can perform equality comparisons on encrypted columns only if the encryption is [deterministic](../../relational-databases/security/encryption/always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption), or if the secure enclave is enabled.

```cpp
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

#### Ciphertext data retrieval example

If Always Encrypted isn't enabled, a query can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns.

The following example illustrates retrieving binary encrypted data from encrypted columns. Note the following details:

- As Always Encrypted isn't enabled in the connection string, the query will return encrypted values of SSN and BirthDate as byte arrays (the program converts the values to strings).
- A query retrieving data from encrypted columns with Always Encrypted disabled can have parameters, as long as none of the parameters target an encrypted column. The above query filters by LastName, which isn't encrypted in the database. If the query filtered by SSN or BirthDate, the query would fail.

```cpp
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

#### Money/SmallMoney encryption

Starting with driver version 17.7 it's possible to use Always Encrypted
with MONEY and SMALLMONEY. However there are some extra steps to take.
When inserting into encrypted MONEY or SMALLMONEY columns, use one of the following C types:

```cpp
SQL_C_CHAR
SQL_C_WCHAR
SQL_C_SHORT
SQL_C_LONG
SQL_C_FLOAT
SQL_C_DOUBLE
SQL_C_BIT
SQL_C_TINYINT
SQL_C_SBIGINT
SQL_C_NUMERIC
```

and a SQL type of either `SQL_NUMERIC` or `SQL_DOUBLE` (precision may be lost when using this type).

##### Binding the variable

Whenever binding a MONEY/SMALLMONEY variable in an encrypted column the following
descriptor field(s) must be set:

```cpp
// n is the descriptor record of the MONEY/SMALLMONEY parameter
// the type is assumed to be SMALLMONEY if isSmallMoney is true and MONEY otherwise

SQLHANDLE ipd = 0;
SQLGetStmtAttr(hStmt, SQL_ATTR_IMP_PARAM_DESC, (SQLPOINTER)&ipd, SQL_IS_POINTER, NULL);
SQLSetDescField(ipd, n, SQL_CA_SS_SERVER_TYPE, isSmallMoney ? (SQLPOINTER)SQL_SS_TYPE_SMALLMONEY :
                                                              (SQLPOINTER)SQL_SS_TYPE_MONEY, SQL_IS_INTEGER);

// If the variable is bound as SQL_NUMERIC, additional descriptor fields have to be set
// var is SQL_NUMERIC_STRUCT containing the value to be inserted

SQLHDESC   hdesc = NULL;
SQLGetStmtAttr(hStmt, SQL_ATTR_APP_PARAM_DESC, &hdesc, 0, NULL);
SQLSetDescField(hdesc, n, SQL_DESC_PRECISION, (SQLPOINTER)(var.precision), 0);
SQLSetDescField(hdesc, n, SQL_DESC_SCALE, (SQLPOINTER)(var.scale), 0);
SQLSetDescField(hdesc, n, SQL_DESC_DATA_PTR, &var, 0);
```

#### Avoiding common problems when querying encrypted columns

This section describes common categories of errors when querying encrypted columns from ODBC applications and a few guidelines on how to avoid them.

##### Unsupported data type conversion errors

Always Encrypted supports few conversions for encrypted data types. See [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) for the detailed list of supported type conversions. To avoid data type conversion errors, make sure that you observe the following points when using SQLBindParameter with parameters targeting encrypted columns:

- The SQL type of the parameter is either exactly the same as the type of the targeted column, or the conversion from the SQL type to the type of the column is supported.

- The precision and scale of parameters targeting columns of the `decimal` and `numeric` SQL Server data types is the same as the precision and scale configured for the target column.

- The precision of parameters targeting columns of `datetime2`, `datetimeoffset`, or `time` SQL Server data types isn't greater than the precision for the target column, in queries that modify the target column.  

##### Errors due to passing plaintext instead of encrypted values

Any value that targets an encrypted column needs to be encrypted before being sent to the server. An attempt to insert, modify, or filter by a plaintext value on an encrypted column will result in an error. To prevent such errors, make sure that:

- Always Encrypted is enabled (in the DSN, the connection string, before connecting by setting the `SQL_COPT_SS_COLUMN_ENCRYPTION` connection attribute for a specific connection, or the `SQL_SOPT_SS_COLUMN_ENCRYPTION` statement attribute for a specific statement).

- You use SQLBindParameter to send data targeting encrypted columns. The example below shows a query that incorrectly filters by a literal/constant on an encrypted column (SSN), instead of passing the literal as an argument to SQLBindParameter.

```cpp
string queryText = "SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE SSN='795-73-9838'";
```

### Precautions when using SQLSetPos and SQLMoreResults

#### SQLSetPos

The `SQLSetPos` API allows an application to update rows in a resultset using buffers that were bound with SQLBindCol and into which row data was previously fetched. Because of the asymmetric padding behavior of encrypted fixed-length types, it's possible to unexpectedly alter the data of these columns while performing updates on other columns in the row. With AE, fixed-length character values will be padded if the value is smaller than the buffer size.

To mitigate this behavior, use the `SQL_COLUMN_IGNORE` flag to ignore columns that won't be updated as part of `SQLBulkOperations` and when using `SQLSetPos` for cursor-based updates.  All columns that are not being directly modified by the application should be ignored, both for performance and to avoid truncation of columns that are bound to a buffer *smaller* than their actual (DB) size. For more information, see [SQLSetPos Function reference](../../odbc/reference/syntax/sqlsetpos-function.md).

#### SQLMoreResults & SQLDescribeCol

Application programs may call [SQLDescribeCol](../../odbc/reference/syntax/sqldescribecol-function.md) to return metadata about columns in prepared statements.  When Always Encrypted is enabled, calling `SQLMoreResults` *before* calling `SQLDescribeCol` causes [sp_describe_first_result_set](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) to be called, which doesn't correctly return the plaintext metadata for encrypted columns. To avoid this issue, call `SQLDescribeCol` on prepared statements *before* calling `SQLMoreResults`.

## Controlling the performance impact of Always Encrypted

Because Always Encrypted is a client-side encryption technology, most of the performance overhead is observed on the client side, not in the database. Apart from the cost of encryption and decryption operations, the other sources of performance overhead on the client side are:

- Extra round trips to the database to retrieve metadata for query parameters.

- Calls to a column master key store to access a column master key.

This section describes the built-in performance optimizations in the ODBC Driver for SQL Server and how you can control the impact of the above two factors on performance.

### Controlling round-trips to retrieve metadata for query parameters

If Always Encrypted is enabled for a connection, the driver will, by default, call [sys.sp_describe_parameter_encryption](../../relational-databases/system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md) for each parameterized query, passing the query statement (without any parameter values) to SQL Server. This stored procedure analyzes the query statement to find out if any parameters need to be encrypted, and if so, returns the encryption-related information for each parameter to allow the driver to encrypt them. The above behavior ensures a high level of transparency to the client application: The application (and the application developer) doesn't need to be aware of which queries access encrypted columns, as long as the values targeting encrypted columns are passed to the driver in parameters.

Beginning in version 17.6, the driver also caches the encryption metadata for prepared statements, improving performance by allowing future calls to `SQLExecute` to not require an extra round trip to retrieve the encryption metadata.

### Per-statement Always Encrypted behavior

To control the performance impact of retrieving encryption metadata for parameterized queries, you can alter the Always Encrypted behavior for individual queries if it has been enabled on the connection. This way, you can ensure that `sys.sp_describe_parameter_encryption` is invoked only for queries that you know have parameters targeting encrypted columns. Note, however, that by doing so, you reduce transparency of encryption: if you encrypt more columns in your database, you may need to change the code of your application to align it with the schema changes.

To control the Always Encrypted behavior of a statement, call SQLSetStmtAttr to set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` statement attribute to one of the following values:

| Value | Description |
|--|--|
| `SQL_CE_DISABLED` (0) | Always Encrypted is disabled for the statement |
| `SQL_CE_RESULTSETONLY` (1) | Decryption Only. Result sets and return values are decrypted, and parameters aren't encrypted |
| `SQL_CE_ENABLED` (3) | Always Encrypted is enabled and used for both parameters and results |

New statement handles created from a connection with Always Encrypted enabled default to SQL_CE_ENABLED. Handles created from a connection with it disabled default to SQL_CE_DISABLED (and it isn't possible to enable Always Encrypted on them.)

If most of the queries of a client application access encrypted columns, the following points are recommended:

- Set the `ColumnEncryption` connection string keyword to `Enabled`.

- Set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` attribute to `SQL_CE_DISABLED` on statements that don't access any encrypted columns. This setting will disable both calling `sys.sp_describe_parameter_encryption` and attempts to decrypt any values in the result set.

- Set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` attribute to `SQL_CE_RESULTSETONLY` on statements that don't have any parameters requiring encryption, but retrieve data from encrypted columns. This setting will disable calling `sys.sp_describe_parameter_encryption` and parameter encryption. Results containing encrypted columns will continue to be decrypted.

- Use prepared statements for queries that will be executed more than once; prepare the query with `SQLPrepare` and save the statement handle, reusing it with `SQLExecute` each time it's executed. This method is the preferred approach for performance even when there are no encrypted columns, and allows the driver to take advantage of cached metadata.

## Always Encrypted security settings

### Force column encryption

To enforce the encryption of a parameter, set the `SQL_CA_SS_FORCE_ENCRYPT` implementation parameter descriptor (IPD) field through a call to the SQLSetDescField function. A non-zero value causes the driver to return an error when no encryption metadata is returned for the associated parameter.

```cpp
SQLHDESC ipd;
SQLGetStmtAttr(hStmt, SQL_ATTR_IMP_PARAM_DESC, &ipd, 0, 0);
SQLSetDescField(ipd, paramNum, SQL_CA_SS_FORCE_ENCRYPT, (SQLPOINTER)TRUE, SQL_IS_SMALLINT);   
```

If SQL Server informs the driver that the parameter doesn't need to be encrypted, queries using that parameter will fail. This behavior provides extra protection against security attacks that involve a compromised SQL Server providing incorrect encryption metadata to the client, which may lead to data disclosure.

### Column encryption key caching

To reduce the number of calls to a column master key store to decrypt column encryption keys, the driver caches the plaintext CEKs in memory. The CEK cache is global to the driver and not associated with any one connection. After receiving the ECEK from database metadata, the driver first tries to find the plaintext CEK corresponding to the encrypted key value in the cache. The driver calls the key store containing the CMK only if it can't find the corresponding plaintext CEK in the cache.

> [!NOTE]
> In the ODBC Driver for SQL Server, the entries in the cache are evicted after a two hour timeout. This behavior means that for a given ECEK, the driver contacts the key store only once during the lifetime of the application or every two hours, whichever is less.

Starting with the ODBC Driver 17.1 for SQL Server, the CEK cache timeout can be adjusted using the `SQL_COPT_SS_CEKCACHETTL` connection attribute, which specifies the number of seconds a CEK will remain in the cache. Because of the global nature of the cache, this attribute can be adjusted from any connection handle valid for the driver. When the cache TTL is lowered, existing CEKs that would exceed the new TTL are also evicted. If it's 0, no CEKs are cached.

### Trusted key paths

Starting with the ODBC Driver 17.1 for SQL Server, the `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attribute allows an application to require that Always Encrypted operations only use a specified list of CMKs, identified by their key paths. By default, this attribute is NULL, which means that the driver accepts any key path. To use this feature, set `SQL_COPT_SS_TRUSTEDCMKPATHS` to point to a null-delimited, null-terminated wide-character string that lists the allowed key path(s). The memory pointed to by this attribute must remain valid during encryption or decryption operations using the connection handle on which it's set --- upon which the driver will check if the CMK path as specified by the server metadata is case-insensitively in this list. If the CMK path isn't in the list, the operation fails. The application can change the contents of memory this attribute points at, to change its list of trusted CMKs, without setting the attribute again.

## Working with column master key stores

To encrypt or decrypt data, the driver needs to obtain a CEK that is configured for the target column. CEKs are stored in encrypted form (ECEKs) in the database metadata. Each CEK has a corresponding CMK that was used to encrypt it. The [database metadata](../../t-sql/statements/create-column-master-key-transact-sql.md) doesn't store the CMK itself; it only contains the name of the keystore and information that the keystore can use to locate the CMK.

To obtain the plaintext value of an ECEK, the driver first obtains the metadata about both the CEK and its corresponding CMK, and then it uses this information to contact the keystore containing the CMK and requests it to decrypt the ECEK. The driver communicates with a keystore using a keystore provider.

### Built-in keystore providers

The ODBC Driver for SQL Server comes with the following built-in keystore providers:

| Name | Description | Provider (metadata) name | Availability |
|--|--|--|--|
| Azure Key Vault | Stores CMKs in an Azure Key Vault | `AZURE_KEY_VAULT` | Windows, macOS, Linux |
| Windows Certificate Store | Stores CMKs locally in the Windows keystore | `MSSQL_CERTIFICATE_STORE` | Windows |

- You (or your DBA) need to make sure that the provider name, configured in the column master key metadata, is correct and the column master key path complies with the key path format for the given provider. It's recommended that you configure the keys using tools such as SQL Server Management Studio, which automatically generates the valid provider names and key paths when issuing the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../t-sql/statements/create-column-master-key-transact-sql.md) statement.

- Ensure your application can access the key in the keystore. This process may involve granting your application access to the key and/or the keystore, depending on the keystore, or doing other keystore-specific configuration steps. For example, to access an Azure Key Vault, you must provide the correct credentials to the keystore.

### Using the Azure Key Vault provider

Azure Key Vault (AKV) is a convenient option to store and manage column master keys for Always Encrypted (especially if your applications are hosted in Azure). The ODBC Driver for SQL Server on Linux, macOS, and Windows includes a built-in column master key store provider for Azure Key Vault. For more information on configuring an Azure Key Vault for Always Encrypted, see [Azure Key Vault - Step by Step](/archive/blogs/kv/azure-key-vault-step-by-step), [Getting Started with Key Vault](/azure/key-vault/general/overview), and [Creating Column Master Keys in Azure Key Vault](../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md#creating-column-master-keys-in-azure-key-vault).

> [!NOTE]
> The ODBC Driver only supports AKV authentication directly against Azure Active Directory. If you are using Azure Active Directory authentication to AKV and your Active Directory configuration requires authentication against an Active Directory Federation Services endpoint, authentication may fail.
> On Linux and macOS, for driver version 17.2 and later, `libcurl` is required to use this provider, but is not an explicit dependency since other operations with the driver do not require it. If you encounter an error regarding `libcurl`, ensure it is installed.

The driver supports authenticating to Azure Key Vault using the following credential types:

- Username/Password - with this method, the credentials are the name of an Azure Active Directory user and its password.

- Client ID/Secret - with this method, the credentials are an application client ID and an application secret.

- Managed Identity (17.5.2+) - either system or user-assigned; for more information, see [Managed Identities for Azure resources](/azure/active-directory/managed-identities-azure-resources/).

- Azure Key Vault Interactive (17.7+ Windows drivers) - with this method, the credentials are authenticated through Azure Active Directory with Login ID.

To allow the driver to use CMKs stored in AKV for column encryption, use the following connection-string-only keywords:

| Credential Type | <code>KeyStoreAuthentication</code> | <code>KeyStorePrincipalId</code> | <code>KeyStoreSecret</code> |
|--|--|--|--|
| Username/password | `KeyVaultPassword` | User Principal Name | Password |
| Client ID/secret | `KeyVaultClientSecret` | Client ID | Secret |
| Managed Identity | `KeyVaultManagedIdentity` | Object ID (optional, for user-assigned only) | (not specified) |
| AKV Interactive | `KeyVaultInteractive` | (not set) | (not set) |

Starting in v17.8, the KeystoreAuthentication and KeystorePrincipalId can be edited using the DSN configuration UI in the ODBC Datasource Administrator.

#### Example connection strings

The following connection strings show how to authenticate to Azure Key Vault with the two credential types:

##### ClientID/Secret

```cpp
"DRIVER=ODBC Driver 18 for SQL Server;SERVER=myServer;Encrypt=yes;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultClientSecret;KeyStorePrincipalId=<clientId>;KeyStoreSecret=<secret>"
```

##### Username/Password

```cpp
"DRIVER=ODBC Driver 18 for SQL Server;SERVER=myServer;Encrypt=yes;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultPassword;KeyStorePrincipalId=<username>;KeyStoreSecret=<password>"
```

##### Managed Identity (system-assigned)

```cpp
"DRIVER=ODBC Driver 18 for SQL Server;SERVER=myServer;Encrypt=yes;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultManagedIdentity"
```

##### Managed Identity (user-assigned)

```cpp
"DRIVER=ODBC Driver 18 for SQL Server;SERVER=myServer;Encrypt=yes;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultManagedIdentity;KeyStorePrincipalId=<objectID>"
```

##### AKV Interactive

```cpp
"DRIVER=ODBC Driver 18 for SQL Server;SERVER=myServer;Encrypt=yes;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultInteractive;UID=<userID>;PWD=<password>"
```

No other ODBC application changes are required to use AKV for CMK storage.

> [!NOTE]
> The driver contains a list of AKV endpoints which it trusts. Starting with driver version 17.5.2, this list is configurable: set the `AKVTrustedEndpoints` property in the driver or DSN's ODBCINST.INI or ODBC.INI registry key (Windows) or `odbcinst.ini` or `odbc.ini` file section (Linux/macOS) to a semicolon-delimited list. Setting it in the DSN takes precedence over a setting in the driver. If the value begins with a semicolon, it extends the default list; otherwise, it replaces the default list. The default list (as of 17.5) is `vault.azure.net;vault.azure.cn;vault.usgovcloudapi.net;vault.microsoftazure.de`. Starting with 17.7, the list also includes `managedhsm.azure.net;managedhsm.azure.cn;managedhsm.usgovcloudapi.net;managedhsm.microsoftazure.de`.

> [!NOTE]
> The Azure Key Vault provider built in to the ODBC driver supports both [Vaults and Managed HSMs in Azure Key Vault](/azure/key-vault/keys/about-keys).

### Using the Windows Certificate Store provider

The ODBC Driver for SQL Server on Windows includes a built-in column master key store provider for the Windows Certificate Store, named `MSSQL_CERTIFICATE_STORE`. (This provider isn't available on macOS or Linux.) With this provider, the CMK is stored locally on the client machine and no extra configuration by the application is necessary to use it with the driver. However, the application must have access to the certificate and its private key in the store. For more information, see [Create and Store Column Master Keys (Always Encrypted)](../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md).

### Using custom keystore providers

The ODBC Driver for SQL Server also supports custom third-party keystore providers using the CEKeystoreProvider interface. This feature allows an application to load, query, and configure keystore providers so that they can be used by the driver to access encrypted columns. Applications may also directly interact with a keystore provider to encrypt CEKs for storage in SQL Server and perform tasks beyond accessing encrypted columns with ODBC; for more information, see [Custom Keystore Providers](custom-keystore-providers.md).

Two connection attributes are used to interact with custom keystore providers. They are:

- `SQL_COPT_SS_CEKEYSTOREPROVIDER`

- `SQL_COPT_SS_CEKEYSTOREDATA`

The former is used to load and enumerate loaded keystore providers, while the latter enables application-provider communications. These connection attributes may be used at any time, before or after establishing a connection, since application-provider interaction doesn't involve communication with SQL Server. However, because the driver hasn't been loaded yet, setting and getting these attributes before connecting will cause them to be processed by the Driver Manager, and may not yield the expected results.

#### Loading a keystore provider

Setting the `SQL_COPT_SS_CEKEYSTOREPROVIDER` connection attribute enables a client application to load a provider library, making available for use the keystore providers contained there.

```cpp
SQLRETURN SQLSetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER StringLength);
```

| Argument | Description |
|--|--|
| `ConnectionHandle` | [Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process. |
| `Attribute` | [Input] Attribute to set: the `SQL_COPT_SS_CEKEYSTOREPROVIDER` constant. |
| `ValuePtr` | [Input] Pointer to a null-terminated character string specifying the filename of the provider library. For SQLSetConnectAttrA, this value is an ANSI (multibyte) string. For SQLSetConnectAttrW, this value is a Unicode (wchar_t) string. |
| `StringLength` | [Input] The length of the ValuePtr string, or SQL_NTS. |

The driver attempts to load the library identified by the ValuePtr parameter using the platform-defined dynamic library loading mechanism (`dlopen()` on Linux and macOS, `LoadLibrary()` on Windows), and adds any providers defined there to the list of providers known to the driver. The following errors may occur:

| Error | Description |
|--|--|
| `CE203` | The dynamic library couldn't be loaded. |
| `CE203` | The "CEKeyStoreProvider" exported symbol wasn't found in the library. |
| `CE203` | One or more providers in the library are already loaded. |

`SQLSetConnectAttr` returns the usual error or success values, and more information is available for any errors that occurred via the standard ODBC diagnostic mechanism.

> [!NOTE]
> The application programmer must ensure that any custom providers are loaded before any query requiring them is sent over any connection. Failure to do so results in the error:

| Error | Description |
|--|--|
| `CE200` | Keystore provider %1 not found. Ensure that the appropriate keystore provider library has been loaded. |

> [!NOTE]
> Keystore provider implementors should avoid the use of `MSSQL` in the name of their custom providers. This term is reserved exclusively for Microsoft use and may cause conflicts with future built-in providers. Using this term in the name of a custom provider may result in an ODBC warning.

#### Getting the list of loaded providers

Getting this connection attribute enables a client application to determine the keystore providers currently loaded in the driver (including those providers built-in.) This process can only be performed after connecting.

```cpp
SQLRETURN SQLGetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER BufferLength, SQLINTEGER * StringLengthPtr);
```

| Argument | Description |
|--|--|
| `ConnectionHandle` | [Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process. |
| `Attribute` | [Input] Attribute to retrieve: the `SQL_COPT_SS_CEKEYSTOREPROVIDER` constant. |
| `ValuePtr` | [Output] A pointer to memory in which to return the next loaded provider name. |
| `BufferLength` | [Input] The length of the buffer ValuePtr. |
| `StringLengthPtr` | [Output] A pointer to a buffer in which to return the total number of bytes (excluding the null-termination character) available to return in \*ValuePtr. If ValuePtr is a null pointer, no length is returned. If the attribute value is a character string and the number of bytes available to return is greater than BufferLength minus the length of the null-termination character, the data in \*ValuePtr is truncated to BufferLength minus the length of the null-termination character and is null-terminated by the driver. |

To allow retrieving the entire list, every Get operation returns the current provider's name, and increments an internal counter to the next one. Once this counter reaches the end of the list, an empty string ("") is returned, and the counter is reset; successive Get operations then continue again from the beginning of the list.

### Communicating with keystore providers

The `SQL_COPT_SS_CEKEYSTOREDATA` connection attribute enables a client application to communicate with loaded keystore providers for configuring more parameters, keying material, and so on. The communication between a client application and a provider follows a simple request-response protocol, based on Get and Set requests using this connection attribute. Communication is initiated only by the client application.

> [!NOTE]
> Due to the nature of the ODBC calls CEKeyStoreProvider's respond to (SQLGet/SetConnectAttr), the ODBC interface only supports setting data at the resolution of the connection context.

The application communicates with keystore providers through the driver via the CEKeystoreData structure:

```cpp
typedef struct CEKeystoreData {
wchar_t *name;
unsigned int dataSize;
char data[];
} CEKEYSTOREDATA;
```

| Argument | Description |
|--|--|
| `name` | [Input] Upon Set, the name of the provider to which the data is sent. Ignored upon Get. Null-terminated, wide-character string. |
| `dataSize` | [Input] The size of the data array following the structure. |
| `data` | [InOut] Upon Set, the data to be sent to the provider. This data may be arbitrary; the driver makes no attempt to interpret it. Upon Get, the buffer to receive the data read from the provider. |

#### Writing data to a provider

A `SQLSetConnectAttr` call using the `SQL_COPT_SS_CEKEYSTOREDATA` attribute writes a "packet" of data to the specified keystore provider.

```cpp
SQLRETURN SQLSetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER StringLength);
```

| Argument | Description |
|--|--|
| `ConnectionHandle` | [Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process. |
| `Attribute` | [Input] Attribute to set: the `SQL_COPT_SS_CEKEYSTOREDATA` constant. |
| `ValuePtr` | [Input] Pointer to a CEKeystoreData structure. The name field of the structure identifies the provider for which the data is intended. |
| `StringLength` | [Input] SQL_IS_POINTER constant |

More detailed error information may be obtained via [SQLGetDiacRec](../../odbc/reference/syntax/sqlgetdescrec-function.md).

> [!NOTE]
> The provider can use the connection handle to associate the written data to a specific connection, if it so desires. This feature is useful for implementing per-connection configuration. It may also ignore the connection context and treat the data identically regardless of the connection used to send the data. For more information, see [Context Association](custom-keystore-providers.md#context-association).

#### Reading data from a provider

A call to `SQLGetConnectAttr` using the `SQL_COPT_SS_CEKEYSTOREDATA` attribute reads a "packet" of data from *the last-written-to* provider. If there was none, a Function Sequence Error occurs. Keystore provider implementers are encouraged to support "dummy writes" of 0 bytes as a way of selecting the provider for read operations without causing other side-effects, if it makes sense to do so.

```cpp
SQLRETURN SQLGetConnectAttr( SQLHDBC ConnectionHandle, SQLINTEGER Attribute, SQLPOINTER ValuePtr, SQLINTEGER BufferLength, SQLINTEGER * StringLengthPtr);
```

| Argument | Description |
|--|--|
| `ConnectionHandle` | [Input] Connection handle. Must be a valid connection handle, but providers loaded via one connection handle are accessible from any other in the same process. |
| `Attribute` | [Input] Attribute to retrieve: the `SQL_COPT_SS_CEKEYSTOREDATA` constant. |
| `ValuePtr` | [Output] A pointer to a CEKeystoreData structure in which the data read from the provider is placed. |
| `BufferLength` | [Input] SQL_IS_POINTER constant |
| `StringLengthPtr` | [Output] A pointer to a buffer in which to return BufferLength. If *ValuePtr is a null pointer, no length is returned. |

The caller must ensure that a buffer of sufficient length following the CEKEYSTOREDATA structure is allocated for the provider to write into. Upon return, its dataSize field is updated with the actual length of data read from the provider. More detailed error information may be obtained via [SQLGetDiacRec](../../odbc/reference/syntax/sqlgetdescrec-function.md).

This interface places no extra requirements on the format of data transferred between an application and a keystore provider. Each provider can define its own protocol/data format, depending on its needs.

For an example of implementing your own keystore provider, see [Custom Keystore Providers](custom-keystore-providers.md)

## Limitations of the ODBC driver when using Always Encrypted

### Asynchronous operations

While the ODBC driver will allow the use of [asynchronous operations](../../relational-databases/native-client/odbc/creating-a-driver-application-asynchronous-mode-and-sqlcancel.md) with Always Encrypted, there's a performance impact on the operations when Always Encrypted is enabled. The call to `sys.sp_describe_parameter_encryption` to determine encryption metadata for the statement is blocking and will cause the driver to wait for the server to return the metadata before returning `SQL_STILL_EXECUTING`.

### Retrieve data in parts with SQLGetData

Before ODBC Driver 17 for SQL Server, encrypted character and binary columns cannot be retrieved in parts with SQLGetData. Only one call to SQLGetData can be made, with a buffer of sufficient length to contain the entire column's data.

### Send data in parts with SQLPutData

Before ODBC Driver 17.3 for SQL Server, data for insertion or comparison cannot be sent in parts with SQLPutData. Only one call to SQLPutData can be made, with a buffer containing the entire data. For inserting long data into encrypted columns, use the Bulk Copy API, described in the next section, with an input data file.

### Encrypted money and smallmoney

Encrypted **money** or **smallmoney** columns cannot be targeted by parameters, since there's no specific ODBC data type that maps to those types, resulting in Operand Type Clash errors.

## Bulk copy of encrypted columns

Use of the [SQL Bulk Copy functions](../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md) and the **bcp** utility is supported with Always Encrypted since ODBC Driver 17 for SQL Server. Both plaintext (encrypted on insertion and decrypted on retrieval) and ciphertext (transferred verbatim) can be inserted and retrieved using the Bulk Copy (bcp_&#42;) APIs and the **bcp** utility.

- To retrieve ciphertext in varbinary(max) form (for example, for bulk loading into a different database), connect without the `ColumnEncryption` option (or set it to `Disabled`) and perform a BCP OUT operation.

- To insert and retrieve plaintext, and let the driver transparently perform encryption and decryption as required, setting `ColumnEncryption` to `Enabled` is sufficient. The functionality of the BCP API is otherwise unchanged.

- To insert ciphertext in varbinary(max) form (for example, as retrieved above), set the `BCPMODIFYENCRYPTED` option to TRUE and perform a BCP IN operation. In order for the resulting data to be decrypted, ensure that the destination column's CEK is the same CEK as the one from which the ciphertext was originally obtained.

When using the **bcp** utility: To control the `ColumnEncryption` setting, use the -D option and specify a DSN containing the desired value. To insert ciphertext, ensure the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` setting of the user is enabled.

The following table provides a summary of the actions when operating on an encrypted column:

| <code>ColumnEncryption</code> | BCP Direction | Description |
|--|--|--|
| `Disabled` | OUT (to client) | Retrieves ciphertext. The observed datatype is **varbinary(max)**. |
| `Enabled` | OUT (to client) | Retrieves plaintext. The driver will decrypt the column data. |
| `Disabled` | IN (to server) | Inserts ciphertext. This setting is intended for opaquely moving encrypted data without requiring it to be decrypted. The operation will fail if the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option isn't set on the user, or BCPMODIFYENCRYPTED isn't set on the connection handle. For more information, see below. |
| `Enabled` | IN (to server) | Inserts plaintext. The driver will encrypt the column data. |

### The BCPMODIFYENCRYPTED option

To prevent data corruption, the server normally doesn't allow inserting ciphertext directly into an encrypted column, and thus attempts to do so will fail; however, for bulk loading of encrypted data using the BCP API, setting the `BCPMODIFYENCRYPTED` [bcp_control](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-control.md) option to TRUE will allow ciphertext to be inserted directly, and reduces the risk of corrupting encrypted data over setting the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option on the user account. Nonetheless, the keys must match the data and it's a good idea to do some read-only checks of the inserted data after the bulk insertion and before further use.

For more information, see [Migrate Sensitive Data Protected by Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).

## Always Encrypted API summary

### Connection string keywords

| Name | Description |
|--|--|
| `ColumnEncryption` | Accepted values are `Enabled`/`Disabled`.<br>`Enabled` - enables Always Encrypted functionality for the connection.<br>`Disabled` - disable Always Encrypted functionality for the connection.<br>*attestation protocol*, *attestation URL* - (version 17.4 and later) enables Always Encrypted with secure enclave using the specified attestation protocol and the attestation URL. <br><br>The default is `Disabled`. |
| `KeyStoreAuthentication` | Valid Values: `KeyVaultPassword`, `KeyVaultClientSecret`, `KeyVaultInteractive`, `KeyVaultManagedIdentity` |
| `KeyStorePrincipalId` | When `KeyStoreAuthentication` = `KeyVaultPassword`, set this value to a valid Azure Active Directory User Principal Name. <br>When `KeyStoreAuthetication` = `KeyVaultClientSecret` set this value to a valid Azure Active Directory Application Client ID <br>When `KeyStoreAuthetication` = `KeyVaultManagedIdentity`, set this value to the Object ID of a user-assigned identity. Otherwise, the system-assigned identity is used. |
| `KeyStoreSecret` | When `KeyStoreAuthentication` = `KeyVaultPassword` set this value to the password for the corresponding user name. <br>When `KeyStoreAuthentication` = `KeyVaultClientSecret` set this value to the Application Secret associated with a valid Azure Active Directory Application Client ID |

### Connection attributes

| Name | Type | Description |
|--|--|--|
| `SQL_COPT_SS_COLUMN_ENCRYPTION` | Pre-connect | `SQL_COLUMN_ENCRYPTION_DISABLE` (0) - Disable Always Encrypted <br>`SQL_COLUMN_ENCRYPTION_ENABLE` (1) - Enable Always Encrypted<br> pointer to `*attestation protocol*,*attestation URL*` string - (version 17.4 and later) enable with secure enclave |
| `SQL_COPT_SS_CEKEYSTOREPROVIDER` | Post-connect | [Set] - Attempt to load CEKeystoreProvider<br>[Get] - Return a CEKeystoreProvider name |
| `SQL_COPT_SS_CEKEYSTOREDATA` | Post-connect | [Set] - Write data to CEKeystoreProvider<br>[Get] - Read data from CEKeystoreProvider |
| `SQL_COPT_SS_CEKCACHETTL` | Post-connect | [Set] - Set the CEK cache TTL<br>[Get] - Get the current CEK cache TTL |
| `SQL_COPT_SS_TRUSTEDCMKPATHS` | Post-connect | [Set] - Set the trusted CMK paths pointer<br>[Get] - Get the current trusted CMK paths pointer |

### Statement attributes

| Name | Description |
|--|--|
| `SQL_SOPT_SS_COLUMN_ENCRYPTION` | `SQL_CE_DISABLED` (0) - Always Encrypted is disabled for the statement <br>`SQL_CE_RESULTSETONLY` (1) - Decryption Only. Result sets and return values are decrypted, and parameters aren't encrypted <br>`SQL_CE_ENABLED` (3) - Always Encrypted is enabled and used for both parameters and results |

### Descriptor fields

| IPD Field | Size/Type | Default Value | Description |
|--|--|--|--|--|
| `SQL_CA_SS_FORCE_ENCRYPT` (1236) | WORD (2 bytes) | 0 | When 0 (default): decision to encrypt this parameter is determined by availability of encryption metadata.<br><br>When nonzero: if encryption metadata is available for this parameter, it's encrypted. Otherwise, the request fails with error [CE300] [Microsoft][ODBC Driver 17 for SQL Server]Mandatory encryption was specified for a parameter but no encryption metadata was provided by the server. |

### bcp_control options

| Option Name | Default Value | Description |
|--|--|--|
| `BCPMODIFYENCRYPTED` (21) | FALSE | When TRUE, allows varbinary(max) values to be inserted into an encrypted column. When FALSE, prevents insertion unless correct type and encryption metadata is supplied. |

## Troubleshooting

When having difficulties using Always Encrypted, start by checking the following points:

- The CEK that encrypts the desired column is present and accessible on the server.

- The CMK that encrypts the CEK has accessible metadata on the server and is also accessible from the client.

- `ColumnEncryption` is enabled in the DSN, connection string, or connection attribute, and if using the secure enclave, has the correct format.

Additionally, when using the secure enclave, attestation failures identify the step in the attestation process where the failure occurred, according to the following table:

| Step | Description |
|--|--|
| 0-99 | Invalid attestation response, or signature verification error. |
| 100-199 | Error retrieving certificates from attestation URL. Ensure `<attestation URL>/v2.0/signingCertificates` is valid and accessible. |
| 200-299 | Unexpected or incorrect format of enclave's identity. |
| 300-399 | Error establishing secure channel with enclave. |

## See also

- [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md)
