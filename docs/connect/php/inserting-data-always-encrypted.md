---
title: "Inserting and Modifying Data with Always Encrypted using the PHP Drivers for SQL Server | Microsoft Docs"
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
# Inserting and Modifying Data with Always Encrypted
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Notable items when inserting sensitive data in an encrypted column:
 -   The data being inserted into an encrypted column must be passed by binding parameters. If data targeted for an encrypted column is inserted using a direct query, the query would fail because the driver does not explicitly parse out the value and process it. The database would reject the query as incompatible with the encrypted column (that is, trying to insert non-encrypted data into an encrypted column fails).
 -   The database has to know the SQL type of the value to be inserted. This SQL type must be identical to the SQL type specified in the column definition of the targeted column. This requirement is because server-side datatypes conversions are not supported with Always Encrypted enabled. The PHP drivers are designed to help the user determine the SQL type of the value, thus the user does not have to provide the SQL type explicitly.
 -   For the drivers to determine the SQL type, however, some limitations apply:

## SQLSRV Driver:
  -   If the user wants the driver to determine the SQL types for the encrypted columns, the user must use `sqlsrv_prepare` and `sqlsrv_execute`.
  -   If `sqlsrv_query` is preferred, the user is responsible for specifying the SQL types of all parameters. The specified SQL type must include the exact string length for string types, and the scale and precision for decimal types.
  
## PDO_SQLSRV Driver:
  -   The statement attribute `PDO::SQLSRV_ATTR_DIRECT_QUERY` is not supported in a parameterized query.
  -   The statement attribute `PDO::ATTR_EMULATE_PREPARES` is not supported in a parameterized query.

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

The following example inserts data into encrypted columns using the SQLSRV driver and `sqlsrv_prepare`:
```
// insertion into encrypted columns must use a parameterized query
$query = "INSET INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (?, ?, ?, ?)";
$ssn = "795-73-9838";
$firstName = "Catherine";
$lastName = "Able";
$birthDate = "1996-10-19";
$params = array(&$ssn, &$firstName, &$lastName, &$birthDate);
$stmt = sqlsrv_prepare($conn, $query, $params);
// during sqlsrv_execute, the driver determines the SQL types for each parameter and pass them to SQL server
sqlsrv_execute($stmt);
```

The following examples inserts data using `sqlsrv_query` instead:
```
// insertion into encrypted columns must use a parameterized query
$query = "INSERT INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (?, ?, ?, ?)";
$ssn = "795-73-9838";
$firstName = "Catherine";
$lastName = "Abel";
$birthDate = "1996-10-19";
// need to provide the SQL types for ALL parameters
// note the SQL types (including the string length) have to be the same at the column definition
$params = array(array(&$ssn, null, null, SQLSRV_SQLTYPE_CHAR(11)),
                array(&$firstName, null, null, SQLSRV_SQLTYPE_NVARCHAR(60)),
                array(&$lastName, null, null, SQLSRV_SQLTYPE_NVARCHAR(60)),
                array(&$birthDate, null, null, SQLSRV_SQLTYPE_DATE));
sqlsrv_query($conn, $query, $params);
```

The following example inserts data into encrypted columns using the PDO_SQLSRV driver:
```
// insertion into encrypted columns must use a parameterized query
$query = "INSERT INTO [dbo].[Patients] ([SSN], [FirstName], [LastName], [BirthDate]) VALUES (?, ?, ?, ?)";
$ssn = "795-73-9838";
$firstName = "Catherine";
$lastName = "Able";
$birthDate = "1996-10-19";
$stmt = $conn->prepare($query);
$stmt->bindParam(1, $ssn);
$stmt->bindParam(2, $firstName);
$stmt->bindParam(3, $lastName);
$stmt->bindParam(4, $birthDate);
// during PDO::Statement::execute, the driver determines the SQL types for each parameter and pass them to SQL server
$stmt->execute();
```
  
When binding parameter in the PDO_SQLSRV driver, there is no option for the user to specify the SQL type. The PDO_SQLSRV driver needs to conform with the PDO layer, and the PDO layer does not have such options.
  
## See Also  
[Programming Guide for PHP SQL Driver](../../connect/php/programming-guide-for-php-sql-driver.md)
[sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md)
[sqlsrv_query](../../connect/php/sqlsrv-query.md)
[pdo::prepare](../../connect/php/pdo-prepare.md)
[PDOStatement::bindParam](../../connect/php/pdostatement-bindparam.md)
[Constants (Microsoft Drivers for PHP for SQL Server)](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)  
