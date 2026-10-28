---
title: "Overview of the Microsoft Drivers for PHP for SQL Server"
description: "Understand the difference between the SQLSRV and PDO_SQLSRV drivers for PHP, and get a structured way to decide which one to use in a new or existing application."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Overview of the Microsoft Drivers for PHP for SQL Server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The Microsoft Drivers for PHP for SQL Server include two PHP extensions, SQLSRV and PDO_SQLSRV. PDO_SQLSRV implements the PHP Data Objects (PDO) interface. Both extensions use the Microsoft ODBC Driver for SQL Server to connect to Microsoft SQL. Their PHP APIs, driver-specific options, and default PHP types differ. For example, SQLSRV returns date and time values as `DateTime` objects by default, while PDO_SQLSRV returns strings by default. For system requirements, see [System requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md).

## Choose SQLSRV or PDO_SQLSRV

| If you... | Use |
|-|-|
| Are starting a new project with no PDO code elsewhere | Choose the interface your application will use consistently. SQLSRV exposes procedural functions and explicit parameter descriptors. PDO_SQLSRV fits code organized around `PDO` and `PDOStatement` objects. |
| Already use PDO with another database (MySQL, PostgreSQL, SQLite) | PDO_SQLSRV, so your data-access layer stays consistent across databases. |
| Use a framework or library that expects a PDO connection object | PDO_SQLSRV. |
| Need manual, chunk-by-chunk control over when stream data is sent | SQLSRV, using `sqlsrv_send_stream_data()`. PDO_SQLSRV also streams large values, by binding a PHP stream resource with `PDO::PARAM_LOB`, but it sends the entire stream automatically during `execute()` rather than in caller-controlled chunks. |
| Maintain an application that already calls `sqlsrv_*` functions | SQLSRV, unless you plan to rewrite its data-access code for PDO. |

Install either extension or both. Windows downloads contain separate extension binaries; on Linux and macOS, install the `sqlsrv` and `pdo_sqlsrv` packages separately. You can load both in the same PHP process. SQLSRV returns connection and statement resources; PDO_SQLSRV returns `PDO` and `PDOStatement` objects. Don't pass a resource from one API to the other.

Using PDO doesn't make Transact-SQL (T-SQL) syntax portable to another database. PDO gives you a common PHP interface; your queries, schema, authentication, and driver-specific attributes can still depend on Microsoft SQL. Likewise, using the procedural extension doesn't require procedural organization throughout your application: you can call SQLSRV from your own classes.

## Compare the same result in both APIs

Run these programs before choosing the PHP representation your application expects. They select the same integer, date, and SQL `NULL`, but the defaults aren't identical. No tables or sample database are required.

Install and load the selected extension. Set `SQL_SERVER`, `SQL_DATABASE`, `SQL_USER`, and `SQL_PASSWORD` in the process environment for an existing SQL Server test database. The server certificate must be trusted and match the configured endpoint. For setup and passwordless Azure alternatives, see [Connecting to the server](connecting-to-the-server.md).

### SQLSRV

Save this complete program as `compare-sqlsrv.php`, and run `php compare-sqlsrv.php`.

```php
<?php
$config = [];
foreach (['SQL_SERVER', 'SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $value = getenv($name);
    if ($value === false || $value === '') {
        throw new RuntimeException("Set $name before running this example.");
    }
    $config[$name] = $value;
}
foreach (['SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $config[$name] = '{' . str_replace('}', '}}', $config[$name]) . '}';
}
$conn = sqlsrv_connect($config['SQL_SERVER'], [
    'Database' => $config['SQL_DATABASE'],
    'UID' => $config['SQL_USER'],
    'PWD' => $config['SQL_PASSWORD'],
    'Encrypt' => true,
    'TrustServerCertificate' => false,
    'LoginTimeout' => 30,
    'CharacterSet' => 'UTF-8',
]);
if ($conn === false) {
    throw new RuntimeException(print_r(sqlsrv_errors(), true));
}
$stmt = null;
try {
    $stmt = sqlsrv_query($conn, "SELECT CAST(42 AS int) AS CountValue, "
        . "CAST('2026-10-19' AS date) AS DayValue, CAST(NULL AS int) AS Missing");
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
    if ($row === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if (!is_array($row) || $row['CountValue'] !== 42
        || !($row['DayValue'] instanceof DateTime)
        || $row['DayValue']->format('Y-m-d') !== '2026-10-19'
        || $row['Missing'] !== null) {
        throw new RuntimeException('Unexpected SQLSRV values or default types.');
    }
    echo get_debug_type($row['CountValue']), ', ',
        get_debug_type($row['DayValue']), ', ', get_debug_type($row['Missing']), PHP_EOL;
} finally {
    if (is_resource($stmt)) {
        sqlsrv_free_stmt($stmt);
    }
    sqlsrv_close($conn);
}
```

Expected output:

```output
int, DateTime, null
```

### PDO_SQLSRV

Save this separate program as `compare-pdo.php`, and run `php compare-pdo.php`. It deliberately leaves numeric and date fetching at their PDO_SQLSRV defaults.

```php
<?php
$config = [];
foreach (['SQL_SERVER', 'SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $value = getenv($name);
    if ($value === false || $value === '') {
        throw new RuntimeException("Set $name before running this example.");
    }
    $config[$name] = $value;
}
foreach (['SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $config[$name] = '{' . str_replace('}', '}}', $config[$name]) . '}';
}
$server = $config['SQL_SERVER'];
$database = $config['SQL_DATABASE'];
$pdo = new PDO(
    "sqlsrv:Server=$server;Database=$database;Encrypt=true;"
    . "TrustServerCertificate=false;LoginTimeout=30",
    $config['SQL_USER'],
    $config['SQL_PASSWORD'],
    [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
        PDO::SQLSRV_ATTR_ENCODING => PDO::SQLSRV_ENCODING_UTF8,
    ]
);
$stmt = null;
try {
    $stmt = $pdo->query("SELECT CAST(42 AS int) AS CountValue, "
        . "CAST('2026-10-19' AS date) AS DayValue, CAST(NULL AS int) AS Missing");
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($row !== ['CountValue' => '42', 'DayValue' => '2026-10-19', 'Missing' => null]) {
        throw new RuntimeException('Unexpected PDO_SQLSRV values or default types.');
    }
    echo get_debug_type($row['CountValue']), ', ',
        get_debug_type($row['DayValue']), ', ', get_debug_type($row['Missing']), PHP_EOL;
} finally {
    $stmt = null;
    $pdo = null;
}
```

Expected output:

```output
string, string, null
```

Both programs explicitly request named result keys. These PHP arrays use column names such as `CountValue`, rather than zero-based numeric indexes. A SQL `NULL` becomes PHP `null` in either API. It isn't the empty string or a missing result row.

Both programs read the actual database name, username, and password from the environment. They escape these values once for the PHP driver's ODBC-style connection handling. Doubling closing braces and enclosing the value in braces preserves special characters; it doesn't rename the database or change the stored password. For more information, see [Connecting to the server](connecting-to-the-server.md#before-you-connect).

The examples stop on errors and release resources in `finally`. SQLSRV reports database failures through `false` and `sqlsrv_errors()`; PDO_SQLSRV throws for connection failures and uses exceptions for query failures with the configured error mode. In a web application, catch these exceptions in the code that handles the request. Record diagnostic details in protected logs instead of returning them to a browser.

After the exercise, delete `compare-sqlsrv.php` and `compare-pdo.php` if you no longer need them. Clear the credential environment variables from the test process when you finish.

<a id="plan-the-application-contract"></a>

## Choose types, encoding, and error handling

Decide which PHP types your application expects, how it handles text and errors, and when it releases database resources.

| Decision | SQLSRV | PDO_SQLSRV | Effect on application code |
| --- | --- | --- | --- |
| Integer result types | Native PHP integers for SQL **int** columns. | Strings by default; `PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE` requests supported numeric PHP types. | Strict comparisons and serialized output can differ. Don't assume every SQL numeric type fits a PHP integer or float. |
| Date and time results | `DateTime` objects by default; `ReturnDatesAsStrings` selects strings. | Strings by default; `PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE` selects objects. | Normalize the representation before serializing or passing it to application code. |
| Character encoding | Set `CharacterSet => 'UTF-8'` explicitly. | UTF-8 is the default; configure it with `PDO::SQLSRV_ATTR_ENCODING`. | Don't transfer the SQLSRV option into a PDO DSN. |
| Error handling | Check `false` and capture the complete SQLSRV error collection immediately. | Configure `PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION`. | A missing row and a failed query need different handling. |
| Parameter reuse | Bind variables by reference in `sqlsrv_prepare()` descriptors. | Use `bindParam()` for a variable or `bindValue()` for its current value. | Reassigning the variable affects a reference binding, not a prior value binding. |
| Cleanup | Free statements, then close the connection resource. | Release statement references, then the PDO object. | A statement can keep a PDO connection alive after another reference is set to `null`. |

If you migrate an existing application, test these behaviors with representative values before changing its database code. Include non-ASCII text, SQL `NULL`, dates, exact decimal values, and stored-procedure output parameters. Also compare how your framework configures PDO attributes. Replacing function names alone doesn't migrate error handling, reference binding, or returned PHP types.

For a worked write-and-read workflow with both APIs, use the [programming guide](programming-guide-for-php-sql-driver.md). For repeated execution and typed inputs, use [Compare query execution functions](comparing-execution-functions.md).

<a id="understand-the-implementation-layers"></a>

## Know what each component does

Your extension choice changes the PHP API and its type conversions. Both extensions use ODBC to communicate with the server, encrypt connections, and authenticate. The operating system supplies trusted certificates and services such as Windows authentication or Kerberos. The SQL target controls permissions, schema, and supported T-SQL statements.

The comparison programs use SQL authentication to SQL Server. For Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Fabric, or Warehouse in Fabric, configure the service endpoint and supported authentication before reusing the fetch pattern. Both Fabric products require Microsoft Entra ID authentication; Warehouse also requires disabling multiple active result sets (MARS). A successful type comparison on SQL Server doesn't establish that a different target supports every feature listed in this overview.

## What both extensions provide

- Integrated authentication, SQL authentication, and Microsoft Entra ID authentication, depending on the target's support. Managed identity doesn't use an application password; `ActiveDirectoryServicePrincipal` uses a client ID and client secret. For more information, see [Connecting to the server](connecting-to-the-server.md).
- Parameterized queries, prepared statements, and directional (input/output) parameters. For more information, see [Comparing execution functions](comparing-execution-functions.md) and [Using directional parameters](using-directional-parameters.md).
- Transactions, stream input parameters for large character and binary values, and table-valued parameters.
- Always Encrypted, including secure enclaves where the ODBC driver and SQL Server target support them. For more information, see [Using Always Encrypted with the Microsoft Drivers for PHP for SQL Server](using-always-encrypted-php-drivers.md).
- Connections to SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric. Authentication and SQL features vary by target; see [Azure and Fabric connections](connecting-to-microsoft-azure-sql-database.md).

<a id="in-this-section"></a>

## Related content

- [Release notes for the Microsoft Drivers for PHP for SQL Server](release-notes-php-sql-driver.md)
- [Support resources for the Microsoft Drivers for PHP for SQL Server](support-resources-for-the-php-sql-driver.md)
- [About code examples in the documentation](about-code-examples-in-the-documentation.md)
- [SQLSRV driver API reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV driver reference](pdo-sqlsrv-driver-reference.md)
- [Constants (Microsoft Drivers for PHP for SQL Server)](constants-microsoft-drivers-for-php-for-sql-server.md)
- [Getting started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)
- [Programming guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
- [Example application (SQLSRV driver)](example-application-sqlsrv-driver.md)
