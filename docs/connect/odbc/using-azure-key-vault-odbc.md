---
title: "Using Azure Key Vault with the ODBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "04/26/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 1cae4db8-9775-4de2-bf3f-fa44a15e5d0b
caps.latest.revision: 19
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Using Always Key Vault with the ODBC Driver
[!INCLUDE[Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

This article provides information on how to use the Azure Key Vault to store column master keys for
[Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx) and use them with
the ODBC Driver 13.1 for SQL Server.

Always Encrypted allows client applications to encrypt sensitive data and never reveal the data or the encryption keys to SQL Server or Azure SQL Database. An Always Encrypted enabled driver, such as the ODBC Driver 13.1 for SQL Server, achieves this by transparently encrypting and decrypting sensitive data in the client application. The driver automatically determines which query parameters correspond to sensitive database columns (protected using Always Encrypted), and encrypts the values of those parameters before passing the data to SQL Server or Azure SQL Database. Similarly, the driver transparently decrypts data retrieved from encrypted database columns in query results. For more information, see [Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx).

### Prerequisites

- Configure Always Encrypted in your database. This involves provisioning Always Encrypted keys and setting up encryption for selected database columns. If you do not already have a database with Always Encrypted configured, follow the directions in [Getting Started with Always Encrypted](https://msdn.microsoft.com/library/mt163865.aspx#Anchor_5).

### Enabling Always Encrypted for application queries
The easiest way to enable the encryption of parameters, and the decryption of query results targeting the encrypted columns, is by setting the value of the `ColumnEncryption` connection string keyword to **`Enabled`**.

The following is an example of a connection string that enables Always Encrypted:
```
SQLWCHAR *connString = L"Driver={ODBC Driver 13 for SQL Server};Server={myServer};Trusted_Connection=yes;ColumnEncryption=Enabled;";
```

And, the following is an equivalent example using the SQLSetConnectAttr function to set the connection attribute.

```
 SQLSetConnectAttr(hdbc, SQL_COPT_SS_COLUMN_ENCRYPTION, (SQLPOINTER)SQL_COLUMN_ENCRYPTION_ENABLE, 0);
```

Always Encrypted can also be enabled for individual queries. See the **Controlling performance impact of Always Encrypted** section below.
Note that, enabling Always Encrypted is not sufficient for encryption or decryption to succeed. You also need to make sure:
- The application has the *VIEW ANY COLUMN MASTER KEY DEFINITION* and *VIEW ANY COLUMN ENCRYPTION KEY DEFINITION* database permissions, required to access the metadata about Always Encrypted keys in the database. For details, see [Permissions section in Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx#Anchor_7).
- The application can access the column master key that protects the column encryption keys, encrypting the queried database columns.

#### Retrieving and Modifying Data in Encrypted Columns

Once you enable Always Encrypted for application queries, you can use standard ODBC APIs (see [ODBC sample code](https://code.msdn.microsoft.com/windowsapps/ODBC-sample-191624ae/sourcecode?fileId=51137&pathId=1980325953) or [ODBC Programmer's Reference](https://msdn.microsoft.com/library/ms714177(v=vs.85).aspx)), to retrieve or modify data in encrypted database columns. Assuming your application has the required database permissions and can access the column master key, the ODBC Driver 13.1 for SQL Server will encrypt any query parameters that target encrypted columns, and will decrypt data retrieved from encrypted columns returning plaintext values of ODBC types, corresponding to the SQL Server data types set for the columns in the database schema.
If Always Encrypted is not enabled, queries with parameters that target encrypted columns will fail. Queries can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns. However, the ODBC Driver 13.1 for SQL Server will not attempt to decrypt any values retrieved from encrypted columns and the application will receive binary encrypted data (as byte arrays).

The below table summarizes the behavior of queries, depending on whether Always Encrypted is enabled or not:

|Query characteristic | Always Encrypted is enabled and application can access the keys and key metadata|Always Encrypted is enabled and application cannot access the keys or key metadata | Always Encrypted is disabled|
|:---|:---|:---|:---|
| Queries with parameters targeting encrypted columns. | Parameter values are transparently encrypted. | Error | Error|
| Queries retrieving data from encrypted columns, without parameters targeting encrypted columns.| Results from encrypted columns are transparently decrypted. The application receives plaintext values of the ODBC datatypes corresponding to the SQL Server types configured for the encrypted columns. | Error | Results from encrypted columns are not decrypted. The application receives encrypted values as byte arrays (byte[]). 

The following examples illustrate retrieving and modifying data in encrypted columns. The examples assume the target table with the below schema. Note that the SSN and BirthDate columns are encrypted.


```
CREATE TABLE [dbo].[Patients]([PatientId] [int] IDENTITY(1,1), 
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
 PRIMARY KEY CLUSTERED ([PatientId] ASC) ON [PRIMARY])
 GO
```

#### Inserting Data Example

This example inserts a row into the Patients table. Note the following:
- There is nothing specific to encryption in the sample code. The ODBC Driver 13.1 for SQL Server automatically detects and encrypts the the values of the SSN and date parameters that target encrypted columns. This makes encryption transparent to the application. 
- The values inserted into database columns, including the encrypted columns, are passed as bound parameters (See [SQLBindParameter Function](https://msdn.microsoft.com/library/ms710963(v=vs.85).aspx)). While using parameters is optional when sending values to non-encrypted columns (although, it is highly recommended because it helps prevent SQL injection), it is required for values targeting encrypted columns. If the values inserted in the SSN or BirthDate columns were passed as literals embedded in the query statement, the query would fail because the ODBC Driver 13.1 for SQL Server would not be able to determine the values in the target encrypted columns, so it would not encrypt the values. As a result, the server would reject them as incompatible with the encrypted columns.
- The data type of the parameter targeting the SSN column is set to an ANSI (non-Unicode) string, which maps to the char/varchar SQL Server data type (`rc = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, 11, 0, (SQLPOINTER)SSN, 0, &cbSSN);`). If the type of the parameter was set to a Unicode string, which maps to nchar/nvarchar, the query would fail, as Always Encrypted does not support conversions from encrypted nchar/nvarchar values to encrypted char/varchar values. See [ODBC Programmer's Reference -- Appendix D: Data Types](https://msdn.microsoft.com/library/ms713607.aspx) for information about the data type mappings.

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

#### Retrieving Plaintext Data Example

The following example demonstrates filtering data based on encrypted values, and retrieving plaintext data from encrypted columns. Note the following:
- The value used in the WHERE clause to filter on the SSN column needs to be passed using SQLBindParameter, so that the ODBC Driver 13.1 for SQL Server can transparently encrypt it before sending it to the database.
- All values printed by the program will be in plaintext, as the ODBC Driver 13.1 for SQL Server will transparently decrypt the data retrieved from the SSN and BirthDate columns.


> [!NOTE]
> Queries can perform equality comparisons on columns if they are encrypted using deterministic encryption. For more information, see the *Selecting Deterministic or Randomized encryption* section of [Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx).

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

#### Retrieving Encrypted Data Example

If Always Encrypted is not enabled, a query can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns.

The following examples illustrates retrieving binary encrypted data from encrypted columns. Note the following:

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

Always Encrypted supports few conversions for encrypted data types. See [Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx) for the detailed list of supported type conversions. To avoid data type conversion errors, make sure that:

- you set the types of parameters targeting encrypted columns, so that the SQL Server data type of the parameter is either exactly the same as the type of the target column, or a conversion of the SQL Server data type of the parameter to the target type of the column is supported.
- the precision and scale of parameters targeting columns of the decimal and numeric SQL Server data types is the same as the precision and scale configured for the target column.  
- the precision of parameters targeting columns of datetime2, datetimeoffset, or time SQL Server data types is not greater than the precision for the target column, in queries that modify values of the target column.  

##### Errors due to passing plaintext instead of encrypted values

Any value that targets an encrypted column needs to be encrypted inside the application. An attempt to insert/modify or to filter by a plaintext value on an encrypted column will result in an error. To prevent such errors, make sure:
- Always Encrypted is enabled for application queries targeting encrypted columns (in the connection string or by setting the `SQL_COPT_SS_COLUMN_ENCRYPTION` attribute for a specific connection or the `SQL_SOPT_SS_COLUMN_ENCRYPTION` for a specific statement).
- you use SQLBindParameter to send data targeting encrypted columns. The below example shows a query that incorrectly filters by a literal/constant on an encrypted column (SSN), instead of passing the literal as an argument to SQLBindParameter. 

```
string queryText = "SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE SSN='795-73-9838'";
```
### Precautions when using SQLSetPos and SQLMoreResults
#### SQLSetPos
The `SQLSetPos` API allows an application to update rows in a resultset using buffers that were bound with SQLBindCol and into which row data was previously fetched. Due to the asymmetric padding behavior of encrypted fixed-length types, it is possible to unexpectedly alter the data of these columns while performing updates on other columns in the row. With AE, fixed length character values will be padded if the value is smaller than the buffer size. To read more about padding for fixed length types with AE, see [Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx) and [SQLSetPos Function reference](https://msdn.microsoft.com/library/ms713507(v=vs.85).aspx).

To mitigate this behavior, use the `SQL_COLUMN_IGNORE` flag to ignore columns that will not be updated as part of `SQLBulkOperations` and when using `SQLSetPos` for cursor based updates.  All columns that are not being directly modified by the application should be ignored, both for performance and to avoid truncation of columns that are bound to a buffer *smaller* than their actual (DB) size. 

#### SQLMoreResults & SQLDescribeCol
Application programs may call [SQLDescribeCol](https://msdn.microsoft.com/library/ms716289(v=vs.85).aspx) to return metadata about columns in prepared statements.  When Always Encrypted is enabled, calling `SQLMoreResults` *before* calling `SQLDescribeCol` causes `sp_describe_first_result_set` to be called, which does not correctly return the plaintext metadata for encrypted columns. To avoid this issue, call `SQLDescribeCol` on prepared statements *before* calling `SQLMoreResults`.

## Working with Column Master Key Stores

To encrypt a parameter value or to decrypt data in query results, the ODBC Driver 13.1 for SQL Server needs to obtain a column encryption key that is configured for the target column. Column encryption keys are stored in encrypted form in the database metadata. Each column encryption key has a corresponding column master key that was used to encrypt the column encryption key. The database metadata does not store the column master keys. It only contains the information about a key store containing a particular column master key and the location of the key in the key store.

To obtain a plaintext value of a column encryption key, the ODBC Driver 13.1 for SQL Server first obtains the metadata about both the column encryption key and its corresponding column master key, and then it uses the information in the metadata to contact the key store containing the column master key, and to decrypt the encrypted column encryption key. The ODBC Driver 13.1 for SQL Server communicates with a key store using a column master key store provider.


### Using Built-in Column Master Key Store Providers

The ODBC Driver 13.1 for SQL Server comes with the following built-in column master key store providers, which are pre-registered with the specific provider names (used to look up the provider).


| Name | Description | Provider (lookup) name |
|:---|:---|:---|
|Windows Certificate Store| A provider for Windows Certificate Store. | `MSSQL_CERTIFICATE_STORE` |
|Azure Key Vault |A provider for a key store that is based in an Azure Key Vault  | `AZURE_KEY_VAULT` |

  
You do not need to make any application code changes to use these providers but note the following:

- You (or your DBA) need to make sure the provider name, configured in the column master key metadata, is correct and the column master key path complies with the key path format that is valid for a given provider. It is recommended that you configure the keys using tools such as SQL Server Management Studio, which automatically generates the valid provider names and key paths when issuing the [CREATE COLUMN MASTER KEY (Transact-SQL)](https://msdn.microsoft.com/library/mt146393.aspx) statement.
- You need to ensure your application can access the key in the key store. This may involve granting your application access to the key and/or the key store, depending on the key store, or performing other key store-specific configuration steps. For example, to access an Azure Key Vault, you need to make sure to provide correct credentials to the key store in the connection string.

### Using Azure Key Vault Provider

Azure Key Vault is a convenient option to store and manage column master keys for Always Encrypted (especially if your applications are hosted in Azure). The ODBC Driver 13.1 for SQL Server includes a built-in column master key store provider for Azure Key Vault. See [Azure Key Vault – Step by Step](https://blogs.technet.microsoft.com/kv/2015/06/02/azure-key-vault-step-by-step/), [Getting Started with Key Vault](https://azure.microsoft.com/documentation/articles/key-vault-get-started/), and [Creating Column Master Keys in Azure Key Vault](https://msdn.microsoft.com/library/mt723359.aspx#Anchor_2) 

ODBC Driver 13.1 for SQL Server contains new connection string keywords which are used to enable seamless integration for Azure Key Vault. We support the following mechanisms to authenticate to Azure and acquire a token for Azure Key Vault:

1. Username/Password – with this method, the credentials redeemed for an Azure Active Directory issued token are the name of an Azure Active Directory  user and a user password.

2. Client ID/Secret – with this method, the credentials redeemed for an Azure Active Directory issued token are an application client id and an application secret.

The below table captures how the new keywords support the above 4 authentication mechanisms for Azure Key Vault.

|Authentication Mechanism| `KeyStoreAuthentication` |`KeyStorePrincipalId`| `KeyStoreSecret` |
|-|-|-|-|
|Username/password| `KeyVaultPassword`| Azure Active Directory User Principle Name| Azure Active Directory  password| 
|Client Id/secret| `KeyVaultClientSecret`| Azure Active Directory Application Client ID| Application Secret|


Example Connection Strings

1. Authenticate Azure Key Vault CMK store with ClientID/Secret
```
L"DRIVER=ODBC Driver 13 for SQL Server;SERVER=myServer;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultClientSecret;KeyStorePrincipalId=<clientId>;KeyStoreSecret=<secret>";
```
2. Authenticate Azure Key Vault CMK store with Username/Password
```
L"DRIVER=ODBC Driver 13 for SQL Server;SERVER=myServer;Trusted_Connection=Yes;DATABASE=myDB;ColumnEncryption=Enabled;KeyStoreAuthentication=KeyVaultPassword;KeyStorePrincipalId=<username>;KeyStoreSecret=<password>";
```

## Controlling performance impact of Always Encrypted

Because Always Encrypted is a client-side encryption technology, most of the performance overhead is observed on the client side, not in the database. Apart from the cost of encryption and decryption operations, the other sources of performance overhead on the client side are:
- Additional round trips to the database to retrieve metadata for query parameters.
- Calls to a column master key store to access a column master key.

This section describes the built-in performance optimizations in the ODBC Driver 13.1 for SQL Server and how you can control the impact of the above two factors on performance.

### Controlling round trips to retrieve metadata for query parameters

If Always Encrypted is enabled for a connection, by default, the ODBC Driver 13.1 for SQL Server will call [sys.sp_describe_parameter_encryption](https://msdn.microsoft.com/library/mt631693.aspx) for each parameterized query, passing the query statement (without any parameter values) to SQL Server. sys.sp_describe_parameter_encryption analyzes the query statement to find out if any parameters need to be encrypted, and if so, returns the encryption-related information for each that will allow the ODBC Driver 13.1 for SQL Server to encrypt parameter values. The above behavior ensures a high-level of transparency to the client application. The application (and the application developer) does not need to be aware of which queries access encrypted columns, as long as the values targeting encrypted columns are passed to the ODBC Driver 13.1 for SQL Server in parameters.

### Setting Always Encrypted on a Statement

To control performance impact of retrieving encryption metadata for parameterized queries, you can alter Always Encrypted behavior for individual queries, after setting it up for the connection. This way, you can ensure that sys.sp_describe_parameter_encryption is invoked only for queries that you know have parameters targeting encrypted columns. Note, however, that by doing so, you reduce transparency of encryption: if you change encryption properties of your database columns, you may need to change the code of your application to align it with the schema changes.

To control the Always Encrypted behavior on a statement, you need to set call SQLSetStmtAttr with and set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` flag to one of the following:

|Value|Description|
|-|-|
|`SQL_CE_DISABLED` (0)|Always Encrypted is disabled for the statement|
|`SQL_CE_RESULTSETONLY` (1)|Decryption Only. Resultsets and return values are decrypted, and parameters are not encrypted|
|`SQL_CE_ENABLED` (3) | Always Encrypted is enabled and used for both parameters and results|

- If most queries a client application sends over a database connection access encrypted columns:
    - Set the `ColumnEncryption` connection string keyword to `Enabled`.
    - Set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` attribute to `SQL_CE_DISABLED` on statements that do not access any encrypted columns. This will disable both calling sys.sp_describe_parameter_encryption as well as an attempt to decrypt any values in the result set.
    - Set the `SQL_SOPT_SS_COLUMN_ENCRYPTION` attribute to `SQL_CE_RESULTSETONLY` on statements that do not have any parameters requiring encryption, but retrieve data from encrypted columns. This will disable calling sys.sp_describe_parameter_encryption and parameter encryption. The query will be able to decrypt the results from encrypted columns.

## Controlling behavior of Always Encrypted security settings

### Force Column Encryption setting
Enforces encryption of a parameter when using Always Encrypted. If SQL Server informs the driver that the parameter does not need to be encrypted, the query using the parameter will fail. This property provides additional protection against security attacks that involve a compromised SQL Server providing incorrect encryption metadata to the client, which may lead to data disclosure.

To control this behavior in the driver, set the `SQL_CA_SS_FORCE_ENCRYPT` through a call to the SQLSetDescField function. A non-zero value causes the driver to return an error when no encryption metadata is returned for the associated parameter.
```
SQLHDESC ipd;
SQLGetStmtAttr(hStmt, SQL_ATTR_IMP_PARAM_DESC, &ipd, 0, 0);
SQLSetDescField(ipd, 1, SQL_CA_SS_FORCE_ENCRYPT, (SQLPOINTER)TRUE, SQL_IS_SMALLINT);   
```

### Column encryption key caching

To reduce the number of calls to a column master key store to decrypt column encryption keys, the ODBC Driver 13.1 for SQL Server caches the plaintext column encryption keys in memory. After receiving the encrypted column encryption key value from database metadata, the driver first tries to find the plaintext column encryption key corresponding to the encrypted key value in the cache. The driver calls the key store containing the column master key only if it cannot find the encrypted column encryption key value in the cache.

**Note:** In ODBC Driver 13.1 for SQL Server, the column encryption key entries in the cache are evicted after a two hour timeout. This means that for a given encrypted column encryption key, the driver contacts the key store only once during the lifetime of the application or every two hours, whichever is less.

## Limitations on ODBC when using Always Encrypted
### Bulk Copy Function Usage
Use of the [SQL Bulk Copy functions](https://msdn.microsoft.com/library/ms130792.aspx) is not supported when using the ODBC driver with Always Encrypted. No transparent encryption/decryption will occur on encrypted columns that are used with the SQL Bulk Copy functions.

### Asynchronous Operations
While the ODBC driver will allow the use of [asynchronous connections](https://msdn.microsoft.com/library/ms131658.aspx) with Always Encrypted, there is a performance impact on the operations when Always Encrypted is enabled. The call to sys.sp_describe_parameter_encryption to determine encryption metadata for the statement is blocking and will cause the driver to wait while metadata is retrieved from the server for Always Encrypted columns before returning `SQL_STILL_EXECUTING`. 

## Always Encrypted API reference
**ODBC Connection String as provided to SQLDriverConnect Function**  
  
|Name|Description|  
|----------|-----------------|  
|`ColumnEncryption`|Accepted values are `Enabled`/`Disabled`.<br>`Enabled` -- enables Always Encrypted functionality for the connection.<br>`Disabled` -- disable Always Encrypted functionality for the connection. <br><br>The default is `Disabled`.|  
|`KeyStoreAuthentication` | Valid Values: `KeyVaultPassword`, `KeyVaultClientSecret` |
|`KeyStorePrincipalId` | When `KeyStoreAuthentication` = `KeyVaultPassword`, set this value to a valid Azure Active Directory User Principle Name. <br>When `KeyStoreAuthetication` = `KeyVaultClientSecret` set this value to a valid Azure Active Directory Application Client ID |
|`KeyStoreSecret` | When `KeyStoreAuthentication` = `KeyVaultPassword` set this value to the password for a valid Azure Active Directory User Principle Name. <br>When `KeyStoreAuthentication` = `KeyVaultClientSecret` set this value to the Application Secret associated with a valid Azure Active Directory Application Client ID|
  
**ODBC Connection Attributes as provided to SQLSetConnectAttr Function**  
  
|Name|Description|  
|----------|-----------------|  
|`SQL_COPT_SS_COLUMN_ENCRYPTION`|`SQL_COLUMN_ENCRYPTION_DISABLE` (0) -- Disable Always Encrypted <br>`SQL_COLUMN_ENCRYPTION_ENABLE` (1) -- Enable Always Encrypted|  

**ODBC Statement Attributes as provided to SQLSetStmtAttr Function**  
  
|Name|Description|  
|----------|-----------------|  
|`SQL_SOPT_SS_COLUMN_ENCRYPTION`|`SQL_CE_DISABLED` (0) -- Always Encrypted is disabled for the statement <br>`SQL_CE_RESULTSETONLY` (1) -- Decryption Only. Resultsets and return values are decrypted, and parameters are not encrypted <br>`SQL_CE_ENABLED` (3) -- Always Encrypted is enabled and used for both parameters and results|

**ODBC Parameter Descriptors as provided to SQLSetDescField Function**  
  
|IPD Field|Size/Type|Default Value|Description|
|-|-|-|-|  
|`SQL_CA_SS_FORCE_ENCRYPT` (1236)|WORD (2 bytes)|0|When 0 (default): decision to encrypt this parameter is determined by availability of encryption metadata.<br><br>When nonzero: if encryption metadata is available for this parameter, it is encrypted. Otherwise, the request fails with error [CE300] [Microsoft][ODBC Driver 13 for SQL Server]Mandatory encryption was specified for a parameter but no encryption metadata was provided by the server.|

## See Also

- [Always Encrypted (Database Engine)](https://msdn.microsoft.com/library/mt163865.aspx)
- [Always Encrypted blog](http://blogs.msdn.com/b/sqlsecurity/archive/tags/always-encrypted/)
[!INCLUDE[appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/appliesto-ss2016-asdb-xxxx-xxx_md.md)]
