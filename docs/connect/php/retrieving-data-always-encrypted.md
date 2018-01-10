---
title: "Retrieving Data with Always Encrypted | Microsoft Docs"
ms.date: "01/08/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "php"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "drivers"
ms.topic: "article"
author: "v-kaywon"
ms.author: "v-kaywon"
manager: "mbarwin"
ms.workload: "Inactive"
---
# Retrieving Data with Always Encrypted
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Notable items when retrieving sensitive data from an encrypted column:
 -   If the column targeted by a WHERE clause is encrypted, the value must be passed to the database by binding parameter. This allows the driver to encrypt the value before sending it to the database.
 -   If the connection option `ColumnEncryption` is enabled, the driver transparently decrypts the data before sending it back to the user.
 -   If the connection option `ColumnEncryption` is disabled, the return value is still encrypted and is returned as byte array.
 -   If the connection option `ColumnEncryption` is disabled and a WHERE filter is applied on an encrypted column, the query would fail since the value passed to the database is not encrypted.
 
The following table schema is used for the examples below:
```
// table schema
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
 PRIMARY KEY CLUSTERED ([PatientId] ASC) ON [PRIMARY] )";
```

The following example retrieves data from the Patients table using the SQLSRV driver:
```
// since SSN is an encrypted column, need to pass the value in the WHERE clause through bind parameter
$query = "SELET [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [SSN] = ?";
$ssn = "795-73-9838";
$stmt = sqlsrv_prepare($conn, $query, array(&$ssn));
// during sqlsrv_execute, the driver encrypts the ssn value and passes it to the database
sqlsrv_execute($stmt);

// fetch like usual
$row = sqlsrv_fetch_array($stmt);
```
If `ColumnEncryption` is disabled, the query would fail since the `$ssn` value cannot be encrypted. The user can use a non-encrypted column in the WHERE clause instead:
```
// $query = "SELET [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [LastName] = ?";
// in fact, bind parameter isn't necessary
$query = "SELECT [SSN], [FirstName], [LastName], [BirthDate] FROM [dbo].[Patients] WHERE [LastName] = 'Abel'";
```

The following example retrieves data from the Patients table using the PDO_SQLSRV driver:
```
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
  
## See Also  
[Programming Guide for PHP SQL Driver](../../connect/php/programming-guide-for-php-sql-driver.md)
[sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md)
[sqlsrv_fetch_array](../../connect/php/sqlsrv-fetch-array.md)
[pdo::prepare](../../connect/php/pdo-prepare.md)
[PDOStatement::bindParam](../../connect/php/pdostatement-bindparam.md)
[PDOStatement::fetch](../../connect/php/pdostatement-fetch.md)
[Constants (Microsoft Drivers for PHP for SQL Server)](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)  
