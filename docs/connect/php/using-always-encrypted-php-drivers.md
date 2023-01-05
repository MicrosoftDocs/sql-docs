---
title: Using Always Encrypted
description: Learn how to use Always Encrypted with the PHP Drivers for SQL Server to protect sensitive data in your application.
author: David-Engel
ms.author: v-davidengel
ms.date: 12/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using Always Encrypted with the PHP Drivers for SQL Server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

## Applicable to

- Microsoft Drivers 5.2 for PHP for SQL Server

## Introduction

This article provides information on how to develop PHP applications using [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) and the [PHP Drivers for SQL Server](Microsoft-php-driver-for-sql-server.md).

Always Encrypted allows client applications to encrypt sensitive data and never reveal the data or the encryption keys to SQL Server or Azure SQL Database. An Always Encrypted enabled driver, such as the ODBC Driver for SQL Server, transparently encrypts and decrypts sensitive data in the client application. The driver automatically determines which query parameters correspond to sensitive database columns (protected using Always Encrypted), and encrypts the values of those parameters before passing the data to SQL Server or Azure SQL Database. Similarly, the driver transparently decrypts data retrieved from encrypted database columns in query results. For more information, see [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md). The PHP Drivers for SQL Server use the ODBC Driver for SQL Server to encrypt sensitive data.

## Prerequisites

- Configure Always Encrypted in your database. This configuration involves provisioning Always Encrypted keys and setting up encryption for selected database columns. If you don't already have a database with Always Encrypted configured, follow the directions in [Tutorial: Getting started with Always Encrypted](../../relational-databases/security/encryption/always-encrypted-tutorial-getting-started.md). In particular, your database should contain the metadata definitions for a Column Master Key (CMK), a Column Encryption Key (CEK), and a table containing one or more columns encrypted using that CEK.
- Make sure ODBC Driver for SQL Server version 17 or higher is installed on your development machine. For details, see [ODBC Driver for SQL Server](../odbc/microsoft-odbc-driver-for-sql-server.md).

## Enabling Always Encrypted in a PHP application

The easiest way to enable the encryption of parameters targeting the encrypted columns and the decryption of query results is by setting the value of the `ColumnEncryption` connection string keyword to `Enabled`. The following are examples of enabling Always Encrypted in the SQLSRV and PDO_SQLSRV drivers:

SQLSRV:

```php
$connectionInfo = array("Database"=>$databaseName, "UID"=>$uid, "PWD"=>$pwd, "ColumnEncryption"=>"Enabled");
$conn = sqlsrv_connect($server, $connectionInfo);
```

PDO_SQLSRV:

```php
$connectionInfo = "Database = $databaseName; ColumnEncryption = Enabled;";
$conn = new PDO("sqlsrv:server = $server; $connectionInfo", $uid, $pwd);
```

Enabling Always Encrypted isn't sufficient for encryption or decryption to succeed; you also need to make sure that:

- The application has the VIEW ANY COLUMN MASTER KEY DEFINITION and VIEW ANY COLUMN ENCRYPTION KEY DEFINITION database permissions, required to access the metadata about Always Encrypted keys in the database. For details, see [Database Permission](../../relational-databases/security/encryption/always-encrypted-database-engine.md#database-permissions).
- The application can access the CMK that protects the CEKs for the queried encrypted columns. This requirement is dependent on the key store provider that stores the CMK. For more information, see [Working with Column Master Key Stores](#working-with-column-master-key-stores).

## Retrieving and modifying data in encrypted columns

Once you enable Always Encrypted on a connection, you can use standard SQLSRV APIs (see [SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)) or PDO_SQLSRV APIs (see [PDO_SQLSRV Driver API Reference](pdo-sqlsrv-driver-reference.md)) to retrieve or modify data in encrypted database columns. Assuming your application has the required database permissions and can access the column master key, the driver encrypts any query parameters that target encrypted columns and decrypt data retrieved from encrypted columns, behaving transparently to the application as if the columns weren't encrypted.

If Always Encrypted isn't enabled, queries with parameters that target encrypted columns fail. Data can still be retrieved from encrypted columns, as long as the query has no parameters targeting encrypted columns. However, the driver doesn't attempt any decryption and the application receives the binary encrypted data (as byte arrays).

The following table summarizes the behavior of queries, depending on whether Always Encrypted is enabled or not:

|Query characteristic|Always Encrypted is enabled and application can access the keys and key metadata|Always Encrypted is enabled and application can't access the keys or key metadata|Always Encrypted is disabled|
|---|---|---|---|
|Parameters targeting encrypted columns.|Parameter values are transparently encrypted.|Error|Error|
|Retrieving data from encrypted columns, without parameters targeting encrypted columns.|Results from encrypted columns are transparently decrypted. The application receives plaintext column values. |Error|Results from encrypted columns aren't decrypted. The application receives encrypted values as byte arrays.|

The following examples illustrate retrieving and modifying data in encrypted columns. The examples assume a table with the following schema. The SSN and BirthDate columns are encrypted.

```sql
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
 PRIMARY KEY CLUSTERED ([PatientId] ASC) ON [PRIMARY])
 GO
```

### Data insertion example

The following examples demonstrate how to use the SQLSRV and PDO_SQLSRV drivers to insert a row into the Patient table. Note the following points:

- There's nothing specific to encryption in the sample code. The driver automatically detects and encrypts the values of the SSN and BirthDate parameters, which target encrypted columns. This mechanism makes encryption transparent to the application.
- The values inserted into database columns, including the encrypted columns, are passed as bound parameters. While using parameters is optional when sending values to non-encrypted columns (although it's highly recommended because it helps prevent SQL injection), it's required for values targeting encrypted columns. If the values inserted in the SSN or BirthDate columns were passed as literals embedded in the query statement, the query would fail because the driver doesn't attempt to encrypt or otherwise process literals in queries. As a result, the server would reject them as incompatible with the encrypted columns.
- When inserting values using bind parameters, a SQL type that is identical to the data type of the target column or whose conversion to the data type of the target column is supported must be passed to the database. This requirement is because Always Encrypted supports few type conversions (for details, see [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md)). The two PHP drivers, SQLSRV and PDO_SQLSRV, each has a mechanism to help the user determine the SQL type of the value. As such, the user doesn't have to provide the SQL type explicitly.
  - For the SQLSRV driver, the user has two options:
    - Rely on the PHP driver to determine and set the right SQL type. In this case, the user must use `sqlsrv_prepare` and `sqlsrv_execute` to execute a parameterized query.
    - Set the SQL type explicitly.
  - For the PDO_SQLSRV driver, the user can't explicitly set the SQL type of a parameter. The PDO_SQLSRV driver automatically helps the user determine the SQL type when binding a parameter.
- For the drivers to determine the SQL type, some limitations apply:
  - SQLSRV Driver:
    - If the user wants the driver to determine the SQL types for the encrypted columns, the user must use `sqlsrv_prepare` and `sqlsrv_execute`.
    - If `sqlsrv_query` is preferred, the user is responsible for specifying the SQL types for all parameters. The specified SQL type must include the string length for string types, and the scale and precision for decimal types.
  - PDO_SQLSRV Driver:
    - The statement attribute `PDO::SQLSRV_ATTR_DIRECT_QUERY` isn't supported in a parameterized query.
    - The statement attribute `PDO::ATTR_EMULATE_PREPARES` isn't supported in a parameterized query.

SQLSRV driver and [sqlsrv_prepare](sqlsrv-prepare.md):

```php
// insertion into encrypted columns must use a parameterized query
$query = "INSERT INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (?, ?, ?, ?)";
$ssn = "795-73-9838";
$firstName = "Catherine";
$lastName = "Abel;
$birthDate = "1996-10-19";
$params = array($ssn, $firstName, $lastName, $birthDate);
// during sqlsrv_prepare, the driver determines the SQL types for each parameter and pass them to SQL server
$stmt = sqlsrv_prepare($conn, $query, $params);
sqlsrv_execute($stmt);
```

SQLSRV driver and [sqlsrv_query](sqlsrv-query.md):

```php
// insertion into encrypted columns must use a parameterized query
$query = "INSERT INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (?, ?, ?, ?)";
$ssn = "795-73-9838";
$firstName = "Catherine";
$lastName = "Abel";
$birthDate = "1996-10-19";
// need to provide the SQL types for ALL parameters
// note the SQL types (including the string length) have to be the same at the column definition
$params = array(array(&$ssn, null, null, SQLSRV_SQLTYPE_CHAR(11)),
                array(&$firstName, null, null, SQLSRV_SQLTYPE_NVARCHAR(50)),
                array(&$lastName, null, null, SQLSRV_SQLTYPE_NVARCHAR(50)),
                array(&$birthDate, null, null, SQLSRV_SQLTYPE_DATE));
sqlsrv_query($conn, $query, $params);
```

PDO_SQLSRV driver and [PDO::prepare](pdo-prepare.md):

```php
// insertion into encrypted columns must use a parameterized query
$query = "INSERT INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (?, ?, ?, ?)";
$ssn = "795-73-9838";
$firstName = "Catherine";
$lastName = "Able";
$birthDate = "1996-10-19";
// during PDO::prepare, the driver determines the SQL types for each parameter and pass them to SQL server
$stmt = $conn->prepare($query);
$stmt->bindParam(1, $ssn);
$stmt->bindParam(2, $firstName);
$stmt->bindParam(3, $lastName);
$stmt->bindParam(4, $birthDate);
$stmt->execute();
```

### Plaintext data retrieval example

The following examples demonstrate filtering data based on encrypted values, and retrieving plaintext data from encrypted columns using the SQLSRV and PDO_SQLSRV drivers. Note the following points:

- The value used in the WHERE clause to filter on the SSN column needs to be passed using bind parameter, so that the driver can transparently encrypt it before sending it to the server.
- When executing a query  with bound parameters, the PHP drivers automatically determines the SQL type for the user unless the user explicitly specifies the SQL type when using the SQLSRV driver.
- All values printed by the program are in plaintext, since the driver transparently decrypts the data retrieved from the SSN and BirthDate columns.

> [!NOTE]
> Queries can perform equality comparisons on encrypted columns only if the encryption is [deterministic](../../relational-databases/security/encryption/always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption).

SQLSRV:

```php
// since SSN is an encrypted column, need to pass the value in the WHERE clause through bind parameter
$query = "SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [SSN] = ?";
$ssn = "795-73-9838";
$stmt = sqlsrv_prepare($conn, $query, array(&$ssn));
// during sqlsrv_execute, the driver encrypts the ssn value and passes it to the database
sqlsrv_execute($stmt);
// fetch like usual
$row = sqlsrv_fetch_array($stmt);
```

PDO_SQLSRV:

```php
// since SSN is an encrypted column, need to pass the value in the WHERE clause through bind parameter
$query = "SELET [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [SSN] = ?";
$ssn = "795-73-9838";
$stmt = $conn->prepare($query);
$stmt->bindParam(1, $ssn);
// during PDOStatement::execute, the driver encrypts the ssn value and passes it to the database
$stmt->execute();
// fetch like usual
$row = $stmt->fetch();
```

### Ciphertext data retrieval example

If Always Encrypted isn't enabled, a query can still retrieve data from encrypted columns, as long as the query has no parameters targeting encrypted columns.

The following examples illustrate retrieving binary encrypted data from encrypted columns using the SQLSRV and PDO_SQLSRV drivers. Note the following points:

- As Always Encrypted isn't enabled in the connection string, the query returns encrypted values of SSN and BirthDate as byte arrays (the program converts the values to strings).
- A query retrieving data from encrypted columns with Always Encrypted disabled can have parameters, as long as none of the parameters target an encrypted column. The following query filters by LastName, which isn't encrypted in the database. If the query filters by SSN or BirthDate, the query would fail.

SQLSRV:

```php
$query = "SELET [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [LastName] = ?";
$lastName = "Abel";
$stmt = sqlsrv_prepare($conn, $query, array(&$lastName));
sqlsrv_execute($stmt);
$row = sqlsrv_fetch_array($stmt);
```

PDO_SQLSRV:

```php
$query = "SELET [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [LastName] = ?";
$lastName = "Abel";
$stmt = $conn->prepare($query);
$stmt->bindParam(1, $lastName);
$stmt->execute();
$row = $stmt->fetch();
```

### Avoiding common problems when querying encrypted columns

This section describes common categories of errors when querying encrypted columns from PHP applications and a few guidelines on how to avoid them.

#### Unsupported data type conversion errors

Always Encrypted supports few conversions for encrypted data types. See [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) for the detailed list of supported type conversions. Do the following to avoid data type conversion errors:

- When using the SQLSRV driver with `sqlsrv_prepare` and `sqlsrv_execute` the SQL type, along with the column size and the number of decimal digits of the parameter is automatically determined.
- When using the PDO_SQLSRV driver to execute a query, the SQL type with the column size and the number of decimal digits of the parameter is also automatically determined
- When using the SQLSRV driver with `sqlsrv_query` to execute a query:
  - The SQL type of the parameter is either exactly the same as the type of the targeted column, or the conversion from the SQL type to the type of the column is supported.
  - The precision and scale of the parameters targeting columns of the `decimal` and `numeric` SQL Server data types is the same as the precision and scale configure for the target column.
  - The precision of parameters targeting columns of `datetime2`, `datetimeoffset`, or `time` SQL Server data types isn't greater than the precision for the target column, in queries that modify the target column.
- Don't use PDO_SQLSRV statement attributes `PDO::SQLSRV_ATTR_DIRECT_QUERY` or `PDO::ATTR_EMULATE_PREPARES` in a parameterized query

#### Errors due to passing plaintext instead of encrypted values

Any value that targets an encrypted column needs to be encrypted before being sent to the server. An attempt to insert, modify, or filter by a plaintext value on an encrypted column results in an error. To prevent such errors, make sure that:

- Always Encrypted is enabled (in the connection string, set the `ColumnEncryption` keyword to `Enabled`).
- You use bind parameter to send data targeting encrypted columns. The following example shows a query that incorrectly filters by a literal/constant on an encrypted column (SSN):

```php
$query = "SELET [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE SSN='795-73-9838'";
```

## Controlling performance impact of Always Encrypted

Because Always Encrypted is a client-side encryption technology, most of the performance overhead is observed on the client side, not in the database. Apart from the cost of encryption and decryption operations, the other sources of performance overhead on the client side are:

- Extra round-trips to the database to retrieve metadata for query parameters.
- Calls to a column master key store to access a column master key.

### Round-trips to retrieve metadata for query parameters

If Always Encrypted is enabled for a connection, the ODBC Driver will, by default, call [sys.sp_describe_parameter_encryption](../../relational-databases/system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md) for each parameterized query, passing the query statement (without any parameter values) to SQL Server. This stored procedure analyzes the query statement to find out if any parameters need to be encrypted, and if so, returns the encryption-related information for each parameter to allow the driver to encrypt them.

Since the PHP drivers allow the user to bind a parameter in a prepared statement without providing the SQL type, when binding a parameter in an Always Encrypted enabled connection, the PHP Drivers call [SQLDescribeParam](../../odbc/reference/syntax/sqldescribeparam-function.md) on the parameter to get the SQL type, column size, and decimal digits. The metadata is then used to call [SQLBindParameter]( ../../odbc/reference/syntax/sqlbindparameter-function.md). These extra `SQLDescribeParam` calls don't require extra round-trips to the database as the ODBC Driver has already stored the information on the client side when `sys.sp_describe_parameter_encryption` was called.

The preceding behaviors ensure a high level of transparency to the client application (and the application developer) doesn't need to be aware of which queries access encrypted columns, as long as the values targeting encrypted columns are passed to the driver in parameters.

Unlike the ODBC Driver for SQL Server, enabling Always Encrypted at the statement/query-level isn't yet supported in the PHP drivers.

### Column encryption key caching

To reduce the number of calls to a column master key store to decrypt column encryption keys (CEK), the driver caches the plaintext CEKs in memory. After receiving the encrypted CEK (ECEK) from database metadata, the ODBC driver first tries to find the plaintext CEK corresponding to the encrypted key value in the cache. The driver calls the key store containing the CMK only if it can't find the corresponding plaintext CEK in the cache.

Note: In the ODBC Driver for SQL Server, the entries in the cache are evicted after a two-hour timeout. This behavior means that for a given ECEK, the driver contacts the key store only once during the lifetime of the application or every two hours, whichever is less.

## Working with Column Master Key stores

To encrypt or decrypt data, the driver needs to obtain a CEK that is configured for the target column. CEKs are stored in encrypted form (ECEKs) in the database metadata. Each CEK has a corresponding CMK that was used to encrypt it. The [database metadata](../../t-sql/statements/create-column-master-key-transact-sql.md) doesn't store the CMK itself; it only contains the name of the key store and information that the key store can use to locate the CMK.

To obtain the plaintext value of an ECEK, the driver first obtains the metadata about both the CEK and its corresponding CMK, and then it uses this information to contact the key store containing the CMK and requests it to decrypt the ECEK. The driver communicates with a key store using a key store provider.

For Microsoft Driver 5.3.0 for PHP for SQL Server, only Windows Certificate Store Provider and Azure Key Vault are supported. The other Keystore Provider supported by the ODBC Driver (Custom Keystore Provider) isn't yet supported.

### Using the Windows Certificate Store provider

The ODBC Driver for SQL Server on Windows includes a built-in column master key store provider for the Windows Certificate Store, named `MSSQL_CERTIFICATE_STORE`. (This provider isn't available on macOS or Linux.) With this provider, the CMK is stored locally on the client machine and no other configuration by the application is necessary to use it with the driver. However, the application must have access to the certificate and its private key in the store. For more information, see [Create and Store Column Master Keys (Always Encrypted)](../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md).

### Using Azure Key Vault

Azure Key Vault offers a way to store encryption keys, passwords, and other secrets using Azure and can be used to store keys for Always Encrypted. The ODBC Driver for SQL Server (version 17 and higher) includes a built-in master key store provider for Azure Key Vault. The following connection options handle Azure Key Vault configuration: `KeyStoreAuthentication`, `KeyStorePrincipalId`, and `KeyStoreSecret`.

- `KeyStoreAuthentication` can take one of two possible string values: `KeyVaultPassword` and `KeyVaultClientSecret`. These values control what kind of authentication credentials are used with the other two keywords.
- `KeyStorePrincipalId` takes a string representing an identifier for the account seeking to access the Azure Key Vault.
  - If `KeyStoreAuthentication` is set to `KeyVaultPassword`, then `KeyStorePrincipalId` must be the name of an Azure ActiveDirectory user.
  - If `KeyStoreAuthentication` is set to `KeyVaultClientSecret`, then `KeyStorePrincipalId` must be an application client ID.
- `KeyStoreSecret` takes a string representing a credential secret.
  - If `KeyStoreAuthentication` is set to `KeyVaultPassword`, then `KeyStoreSecret` must be the user's password.
  - If `KeyStoreAuthentication` is set to `KeyVaultClientSecret`, then `KeyStoreSecret` must be the application secret associated with the application client ID.

All three options must be present in the connection string to use Azure Key Vault. Also, `ColumnEncryption` must be set to `Enabled`. If `ColumnEncryption` is set to `Disabled` but the Azure Key Vault options are present, the script will continue without errors but no encryption will be performed.

The following examples show how to connect to SQL Server using Azure Key Vault.

SQLSRV:

Using an Azure Active Directory account:

```php
$connectionInfo = array("Database"=>$databaseName, "UID"=>$uid, "PWD"=>$pwd, "ColumnEncryption"=>"Enabled", "KeyStoreAuthentication"=>"KeyVaultPassword", "KeyStorePrincipalId"=>$AADUsername, "KeyStoreSecret"=>$AADPassword);
$conn = sqlsrv_connect($server, $connectionInfo);
```

Using an Azure application client ID and secret:

```php
$connectionInfo = array("Database"=>$databaseName, "UID"=>$uid, "PWD"=>$pwd, "ColumnEncryption"=>"Enabled", "KeyStoreAuthentication"=>"KeyVaultClientSecret", "KeyStorePrincipalId"=>$applicationClientID, "KeyStoreSecret"=>$applicationClientSecret);
$conn = sqlsrv_connect($server, $connectionInfo);
```

PDO_SQLSRV:
Using an Azure Active Directory account:

```php
$connectionInfo = "Database = $databaseName; ColumnEncryption = Enabled; KeyStoreAuthentication = KeyVaultPassword; KeyStorePrincipalId = $AADUsername; KeyStoreSecret = $AADPassword;";
$conn = new PDO("sqlsrv:server = $server; $connectionInfo", $uid, $pwd);
```

Using an Azure application client ID and secret:

```php
$connectionInfo = "Database = $databaseName; ColumnEncryption = Enabled; KeyStoreAuthentication = KeyVaultClientSecret; KeyStorePrincipalId = $applicationClientID; KeyStoreSecret = $applicationClientSecret;";
$conn = new PDO("sqlsrv:server = $server; $connectionInfo", $uid, $pwd);
```

## Limitations of the PHP drivers when using Always Encrypted

SQLSRV and PDO_SQLSRV:

- Linux/macOS don't support Windows Certificate Store Provider
- Forcing parameter encryption
- Enabling Always Encrypted at the statement level
- When using the Always Encrypted feature and non-UTF8 locales on Linux and macOS (such as "en_US.ISO-8859-1"), inserting null data or an empty string into an encrypted char(n) column may not work unless Code Page 1252 has been installed on your system

SQLSRV only:

- Using `sqlsrv_query` for binding parameter without specifying the SQL type
- Using `sqlsrv_prepare` for binding parameters in a batch of SQL statements

PDO_SQLSRV only:

- `PDO::SQLSRV_ATTR_DIRECT_QUERY` statement attribute specified in a parameterized query
- `PDO::ATTR_EMULATE_PREPARE` statement attribute specified in a parameterized query
- binding parameters in a batch of SQL statements

The PHP drivers also inherit the limitations imposed by the ODBC Driver for SQL Server and the database. See [Limitations of the ODBC driver when using Always Encrypted](../odbc/using-always-encrypted-with-the-odbc-driver.md) and [Always Encrypted limitations](../../relational-databases/security/encryption/always-encrypted-database-engine.md#limitations).

## See also

[Programming Guide for PHP SQL Driver](programming-guide-for-php-sql-driver.md)  
[SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)  
[PDO_SQLSRV Driver API Reference](pdo-sqlsrv-driver-reference.md)  
