---
title: "Connect to Microsoft SQL from PHP"
description: "Learn how to open a connection to SQL Server, Azure SQL Database, and Fabric SQL targets with the SQLSRV and PDO_SQLSRV drivers for PHP, and which connection option to use."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Connecting to the server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Use this guide to open a connection, verify the values PHP receives, and release the connection when the work finishes. The programs use SQL Server authentication for a local or remote SQL Server test database. For passwordless Azure and Fabric connections, use [Connect to Azure SQL and Fabric SQL](connecting-to-microsoft-azure-sql-database.md).

SQLSRV accepts a server name and an associative array of connection options. PDO_SQLSRV implements PHP Data Objects (PDO) and accepts a data source name (DSN) string, separate credential arguments, and an array of PDO attributes. Both use the Microsoft ODBC Driver for SQL Server. A connection working with one API doesn't make the other API's option names or PHP types interchangeable.

## Before you connect

1. Install a [supported PHP runtime and Microsoft ODBC Driver](system-requirements-for-the-php-sql-driver.md), and [load the PHP extension](loading-the-php-sql-driver.md) you intend to use.
1. Check the PHP process that runs your application. Run `php --ini` to find the command-line configuration and `php -m` to list its loaded extensions. SQLSRV requires `sqlsrv`. PDO_SQLSRV requires `PDO` and `pdo_sqlsrv`. A web server can use a different `php.ini`, runtime, and account from your terminal.
1. Obtain the server address, database name, and an identity with permission to connect. A database login doesn't automatically grant permission to every table.
1. Configure network access and a trusted server certificate. These programs request encryption and certificate validation explicitly. Connect by the host name on the certificate rather than substituting an IP address that the certificate doesn't cover.

Set these variables in the environment of the PHP process, using your shell, development tool, or hosting configuration. Don't commit a password file or print the environment.

| Variable | Value |
| --- | --- |
| `SQL_SERVER` | The server host name, optionally prefixed with `tcp:` and followed by `,<port>`. |
| `SQL_DATABASE` | An existing database that your login can access. |
| `SQL_USER` | A SQL Server login. |
| `SQL_PASSWORD` | That login's password, supplied through protected configuration. |

Read server and database settings from trusted application configuration, not web request parameters. The PDO example places those settings in a DSN. Keep its connection keywords under application control.

Store the actual database name, username, and password in the environment variables, without adding or doubling braces yourself. Both PHP extensions require ODBC-style escaping even for SQLSRV array options and PDO constructor arguments: double each closing brace and enclose the value in braces. Each program does this once before passing these values to the driver. The outer braces preserve literal braces at the beginning and end of a value and keep semicolons inside the database name from separating PDO DSN settings. This escaping is separate from Transact-SQL (T-SQL) parameter binding.

## Open, query, and close with SQLSRV

Save this program as `connect-sqlsrv.php`, and run `php connect-sqlsrv.php`. It executes a T-SQL query without creating tables or changing data. `CharacterSet` selects UTF-8 for character data; it doesn't change the database's column types or collation.

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
    $sql = "SELECT CAST(42 AS int) AS Value, N'caf' + NCHAR(233) AS Label, "
        . "CAST(NULL AS int) AS Missing";
    $stmt = sqlsrv_query($conn, $sql, [], ['QueryTimeout' => 30]);
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
    if ($row === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if ($row !== ['Value' => 42, 'Label' => "caf\u{00e9}", 'Missing' => null]) {
        throw new RuntimeException('Unexpected SQLSRV values or PHP types.');
    }
    echo json_encode($row, JSON_THROW_ON_ERROR), PHP_EOL;
} finally {
    if (is_resource($stmt)) {
        sqlsrv_free_stmt($stmt);
    }
    sqlsrv_close($conn);
}
```

Expected output:

```output
{"Value":42,"Label":"caf\u00e9","Missing":null}
```

The connection and statement are PHP *resources*, handles you pass to `sqlsrv_*` functions. Check for `false` before using either handle. Retrieve `sqlsrv_errors()` immediately after a failure because a later SQLSRV call can replace the error collection.

`SQLSRV_FETCH_ASSOC` returns an associative array with column names as keys. `SQLSRV_FETCH_NUMERIC` returns zero-based numeric keys; the default `SQLSRV_FETCH_BOTH` includes both forms. For `sqlsrv_fetch_array()`, `null` means no more rows and `false` means an error. A SQL `NULL` inside a row is PHP `null`; it doesn't mean the row is missing. Use `===` when distinguishing these values.

## Open, query, and close with PDO_SQLSRV

Save this separate program as `connect-pdo.php`, and run `php connect-pdo.php` with the same environment. `sqlsrv:` selects PDO_SQLSRV, not PHP's generic PDO_ODBC driver.

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
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
        PDO::SQLSRV_ATTR_ENCODING => PDO::SQLSRV_ENCODING_UTF8,
    ]
);

$stmt = null;
try {
    $sql = "SELECT CAST(42 AS int) AS Value, N'caf' + NCHAR(233) AS Label, "
        . "CAST(NULL AS int) AS Missing";
    $stmt = $pdo->prepare($sql, [PDO::SQLSRV_ATTR_QUERY_TIMEOUT => 30]);
    $stmt->execute();
    $row = $stmt->fetch();
    if ($row !== ['Value' => '42', 'Label' => "caf\u{00e9}", 'Missing' => null]) {
        throw new RuntimeException('Unexpected PDO_SQLSRV values or PHP types.');
    }
    echo json_encode($row, JSON_THROW_ON_ERROR), PHP_EOL;
} finally {
    $stmt = null;
    $pdo = null;
}
```

Expected output:

```output
{"Value":"42","Label":"caf\u00e9","Missing":null}
```

PDO_SQLSRV returns this **int** column as a PHP string by default. To request PHP numeric types for supported numeric columns, set `PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE` to `true`. Don't use a PHP cast indiscriminately for **decimal** values; a floating-point conversion can lose precision.

PDO attributes are settings on the connection or statement object. `PDO::ATTR_ERRMODE` with `PDO::ERRMODE_EXCEPTION` makes database-operation failures throw `PDOException`. Connection construction throws on failure regardless of that setting. `PDO::FETCH_ASSOC` gives named keys, `PDO::FETCH_NUM` gives zero-based numeric keys, and `PDO::FETCH_BOTH` gives both. `fetch()` returns `false` at the end of the result; SQL `NULL` values within a row remain PHP `null`.

These command-line programs let failures stop execution. In a web application, catch exceptions in the code that handles the request. Record diagnostic details in protected logs and return a generic error response. Don't send `sqlsrv_errors()`, exception traces, connection strings, or tokens to a browser.

## Choose an authentication mode

| Mode | Use for | See |
|-|-|-|
| SQL Server authentication | On-premises SQL Server, containers, and Azure SQL when Microsoft Entra ID isn't available | [Connect using SQL Server authentication](how-to-connect-using-sql-server-authentication.md) |
| Integrated authentication (no username or password in the connection settings) | Windows identities, or Linux/macOS clients configured for Kerberos | [Connect using Windows authentication](how-to-connect-using-windows-authentication.md) |
| Microsoft Entra ID (`Authentication` or `AccessToken` keyword) | Azure SQL and Fabric SQL targets configured for Microsoft Entra ID | [Connect using Microsoft Entra authentication](azure-active-directory.md) |

For SQL authentication, SQLSRV takes `UID` and `PWD` in its connection options array. PDO_SQLSRV takes the username and password as the second and third arguments to `new PDO()`, not as DSN keywords. For an access token, SQLSRV takes `AccessToken` in the options array, while PDO_SQLSRV takes it in the DSN.

If you leave out the username, password, `Authentication`, and `AccessToken`, both drivers request integrated authentication from ODBC by using `Trusted_Connection=Yes`. On Linux and macOS, this request requires a valid Kerberos ticket and [ODBC integrated-authentication configuration](../odbc/linux-mac/using-integrated-authentication.md). It doesn't give an arbitrary local account access to a remote server.

## Configure the connection and its statements

Connection settings apply to the session you open. Statement settings apply to a particular query. Set timeouts and result types explicitly when your application needs values other than the defaults.

| Purpose | SQLSRV | PDO_SQLSRV | Behavior owner |
| --- | --- | --- | --- |
| Endpoint and database | Server argument and `Database` option. | `Server` and `Database` DSN keywords. | ODBC and the SQL target. |
| SQL credentials | `UID` and `PWD` options. | Constructor username and password arguments. | Extension credential handling and server authentication. |
| Microsoft Entra authentication | `Authentication` option, or the separate `AccessToken` option. | `Authentication` or `AccessToken` DSN keyword, with credentials omitted for an external token. | ODBC authentication; the application acquires external tokens. |
| Encryption | `Encrypt => true`. | `Encrypt=true` in the DSN. | ODBC transport security. |
| Certificate validation | `TrustServerCertificate => false`. | `TrustServerCertificate=false` in the DSN. | ODBC and the operating system's certificate trust configuration. |
| Connection establishment timeout | `LoginTimeout` connection option, in seconds. | `LoginTimeout` DSN keyword, in seconds. | ODBC. It isn't the statement execution timeout. |
| Query timeout | `QueryTimeout` statement option, in seconds. | `PDO::SQLSRV_ATTR_QUERY_TIMEOUT`, on the connection or statement. | Extension and ODBC statement handling. |
| Character encoding | `CharacterSet => 'UTF-8'` connection option. | `PDO::SQLSRV_ATTR_ENCODING => PDO::SQLSRV_ENCODING_UTF8`. | PHP extension conversion. PDO_SQLSRV defaults to UTF-8; SQLSRV defaults to the system encoding. |
| Date and time objects | `ReturnDatesAsStrings` is `false` by default, so date and time columns return `DateTime` objects. | Strings by default; use `PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE` to request objects. | PHP extension type conversion. |
| Multiple Active Result Sets (MARS) | `MultipleActiveResultSets` connection option. | `MultipleActiveResultSets` DSN keyword. | ODBC and target support. Both extensions enable it by default; disable it for Warehouse in Fabric. |
| Idle recovery | `ConnectRetryCount` and `ConnectRetryInterval` connection options. | The same DSN keywords. | ODBC. They don't retry arbitrary application operations. |

For the complete list of keywords, allowed values, and defaults, see [Connection options](connection-options.md). Don't place `CharacterSet`, `QueryTimeout`, or `PDO::` constant names in a PDO DSN.

## Manage connection lifetime

Open a connection when a request or job needs database work. Reuse it throughout that request or job, then release it when finished. Don't connect and disconnect for every row. In a long-running worker, release statements and complete each transaction before accepting the next job.

SQLSRV closes associated statements when you close its connection. Explicitly freeing statements earlier releases resources without ending the connection. For PDO, assigning `$pdo = null` removes only that variable's reference. A `PDOStatement` or another variable can still retain the connection. Release those references too. `closeCursor()` releases pending results so the statement can be reused; it isn't a connection-close method.

Both APIs start in autocommit mode: a successful statement commits without a separate application commit unless you begin a transaction. For a group of writes that must succeed or fail together, use `sqlsrv_begin_transaction()` or `$pdo->beginTransaction()`, then commit or roll back explicitly. Closing a connection with an outstanding transaction attempts a rollback, not a commit. Don't rely on shutdown cleanup to implement your transaction policy. For complete examples, see [Perform transactions](how-to-perform-transactions.md).

Releasing a PHP connection handle doesn't necessarily close its physical network connection. ODBC pooling can retain it for reuse. Pooling isn't a reason to retain PHP resources indefinitely, and PDO_SQLSRV doesn't support `PDO::ATTR_PERSISTENT => true`. For platform-specific pooling configuration, see [Connection pooling](connection-pooling-microsoft-drivers-for-php-for-sql-server.md).

## Apply the guide to your SQL target

The handle, fetch, and cleanup patterns apply to both extensions on supported SQL targets. Connection endpoints, authentication, network access, and SQL capabilities vary.

| Target | Adaptation |
| --- | --- |
| SQL Server | The programs use a SQL login. Integrated authentication is another choice when you configure the PHP process identity. |
| Azure SQL Database | Use the logical server host name and explicit database. Prefer Microsoft Entra authentication instead of the SQL credentials in these programs. |
| Azure SQL Managed Instance | Use the instance endpoint and its network access requirements. Don't assume a private endpoint is reachable from your workstation. |
| Azure Synapse Analytics | Use the SQL pool endpoint and check its T-SQL support before adapting queries that create or modify objects. |
| SQL database in Microsoft Fabric | Use its database connection details and Microsoft Entra authentication. Don't substitute the SQL analytics endpoint. |
| Warehouse in Microsoft Fabric | Use its warehouse endpoint and database, Microsoft Entra authentication, and `MultipleActiveResultSets=false`. |

For service-specific setup, see [Azure and Fabric connections](connecting-to-microsoft-azure-sql-database.md).

## Troubleshoot the connection

Start with the step that failed. A successful Transmission Control Protocol (TCP) check doesn't prove authentication or certificate validation succeeded.

| Symptom | Check |
| --- | --- |
| `Call to undefined function sqlsrv_connect()` | The executing PHP process didn't load `sqlsrv`. Check its `php.ini` and startup warnings. |
| PDO reports `could not find driver` | Confirm `pdo_sqlsrv` is loaded in that process and `sqlsrv` appears in `PDO::getAvailableDrivers()`. |
| ODBC driver can't be found | Install the Microsoft ODBC Driver and check its registration and architecture. Installing a PHP extension doesn't install ODBC. |
| Connection timeout or server not found | Check the server name, port, name resolution, routing, firewall rules, and whether SQL Server listens on TCP. |
| Certificate chain or host-name validation fails | Install the correct trust chain and use the certificate's host name. Don't treat `TrustServerCertificate=true` as a production fix. |
| Login fails or the database can't be opened | Check the authentication mode, database name, login/user mapping, and permissions. Omitting credentials requests integrated authentication; it doesn't select SQL authentication. |
| Terminal succeeds but web requests fail | Compare the PHP runtime, loaded extensions, process identity, environment variables, and network access. Don't expose a public `phpinfo()` page containing configuration details. |
| A later operation fails on a closed handle | Retain the connection through all statement work. Create a new connection after closing one; an old handle doesn't reopen itself. |

<a id="in-this-section"></a>

## Other connection articles

- [Connect on a specified port](how-to-connect-on-a-specified-port.md)
- [Connection options](connection-options.md)
- [Connection pooling](connection-pooling-microsoft-drivers-for-php-for-sql-server.md)
- [Disable Multiple Active Result Sets (MARS)](how-to-disable-multiple-active-resultsets-mars.md)
- [Support for LocalDB](php-driver-for-sql-server-support-for-localdb.md)
- [Support for high availability and disaster recovery](php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)
- [Connecting to Microsoft Azure SQL Database](connecting-to-microsoft-azure-sql-database.md)
- [Idle connection resiliency](connection-resiliency.md)

## Related content

- [Programming guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
- [Example application (SQLSRV driver)](example-application-sqlsrv-driver.md)
