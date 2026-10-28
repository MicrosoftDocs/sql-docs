---
title: "Programming Guide for the Microsoft Drivers for PHP for SQL Server"
description: "Build a complete SQLSRV or PDO_SQLSRV workflow with typed parameters, a transaction, result checks, rollback, and cleanup. Find guidance for your next PHP database task."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Programming guide for the Microsoft Drivers for PHP for SQL Server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

After you have a working connection from [Getting started](getting-started-with-the-php-sql-driver.md), use this guide to write, commit, and read a row with either PHP API. Each program includes its own setup and cleanup. Use the task links to extend the workflow rather than copying options from one API into the other.

## Prepare a test connection

Use an existing SQL Server test database and a login that can connect and create local temporary tables. The examples use SQL authentication. For supported authentication choices and passwordless Azure onboarding, see [Connecting to the server](connecting-to-the-server.md).

Load the extension you choose, and set `SQL_SERVER`, `SQL_DATABASE`, `SQL_USER`, and `SQL_PASSWORD` in the environment of the PHP process. `SQL_SERVER` can include an explicit port, such as `<server>,1433`. The server must present a certificate trusted by the client and valid for that endpoint. Keep passwords outside the program and source control.

Store the actual database name, username, and password in the environment variables. Each program doubles closing braces and encloses these values in braces once for the PHP driver's ODBC-style connection handling. Escaping preserves special characters in connection values; it isn't query parameterization. For more information, see [Connecting to the server](connecting-to-the-server.md#before-you-connect).

The Transact-SQL (T-SQL) statements create `#PhpGuide`, a temporary table visible only to the connection that creates it. The programs don't create or change a permanent application table. Keep the same connection throughout the workflow.

## Write and read a row

The programs insert an integer, UTF-8 text, and SQL `NULL` through parameters. They require exactly one affected row, commit the transaction, and verify the returned values and PHP types. The final query also checks that no transaction remains open.

### SQLSRV

Save this complete program as `workflow-sqlsrv.php`, and run `php workflow-sqlsrv.php`.

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
$inTransaction = false;
$failure = null;
try {
    $stmt = sqlsrv_query($conn, 'CREATE TABLE #PhpGuide '
        . '(Id int PRIMARY KEY, Title nvarchar(80), OptionalCount int NULL)');
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    sqlsrv_free_stmt($stmt);
    $stmt = null;
    if (!sqlsrv_begin_transaction($conn)) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $inTransaction = true;
    $id = 1;
    $title = "Caf\u{00E9}";
    $optionalCount = null;
    $params = [
        [&$id, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_INT, SQLSRV_SQLTYPE_INT],
        [&$title, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_STRING('UTF-8'), SQLSRV_SQLTYPE_NVARCHAR(80)],
        [&$optionalCount, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_INT, SQLSRV_SQLTYPE_INT],
    ];
    $stmt = sqlsrv_prepare($conn,
        'INSERT INTO #PhpGuide (Id, Title, OptionalCount) VALUES (?, ?, ?)', $params);
    if ($stmt === false || !sqlsrv_execute($stmt)) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $affected = sqlsrv_rows_affected($stmt);
    if ($affected === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if ($affected !== 1) {
        throw new RuntimeException('Expected exactly one inserted row.');
    }
    sqlsrv_free_stmt($stmt);
    $stmt = null;
    if (!sqlsrv_commit($conn)) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $inTransaction = false;
    $stmt = sqlsrv_query($conn,
        'SELECT Id, Title, OptionalCount, @@TRANCOUNT AS TransactionCount FROM #PhpGuide');
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
    if ($row === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if ($row !== ['Id' => 1, 'Title' => $title, 'OptionalCount' => null, 'TransactionCount' => 0]) {
        throw new RuntimeException('Unexpected committed values, types, or transaction state.');
    }
    $end = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
    if ($end === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if ($end !== null) {
        throw new RuntimeException('Expected exactly one result row.');
    }
} catch (Throwable $error) {
    $failure = $error;
    if ($inTransaction && !sqlsrv_rollback($conn)) {
        $failure = new RuntimeException(
            'Rollback failed: ' . print_r(sqlsrv_errors(), true), 0, $error);
    }
    throw $failure;
} finally {
    if (is_resource($stmt)) {
        sqlsrv_free_stmt($stmt);
    }
    try {
        $cleanup = sqlsrv_query($conn, 'DROP TABLE IF EXISTS #PhpGuide');
        if ($cleanup === false) {
            throw new RuntimeException(
                'Cleanup failed: ' . print_r(sqlsrv_errors(), true), 0, $failure);
        }
        sqlsrv_free_stmt($cleanup);
    } finally {
        sqlsrv_close($conn);
    }
}
echo "Inserted 1 row; read committed UTF-8 text and null; no open transaction.", PHP_EOL;
```

### PDO_SQLSRV

Save this separate program as `workflow-pdo.php`, and run `php workflow-pdo.php`.

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
        PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE => true,
    ]
);
$stmt = null;
$failure = null;
try {
    $pdo->exec('CREATE TABLE #PhpGuide '
        . '(Id int PRIMARY KEY, Title nvarchar(80), OptionalCount int NULL)');
    $pdo->beginTransaction();
    $title = "Caf\u{00E9}";
    $stmt = $pdo->prepare(
        'INSERT INTO #PhpGuide (Id, Title, OptionalCount) VALUES (?, ?, ?)');
    $stmt->bindValue(1, 1, PDO::PARAM_INT);
    $stmt->bindValue(2, $title, PDO::PARAM_STR);
    $stmt->bindValue(3, null, PDO::PARAM_NULL);
    $stmt->execute();
    if ($stmt->rowCount() !== 1) {
        throw new RuntimeException('Expected exactly one inserted row.');
    }
    $stmt = null;
    $pdo->commit();
    $stmt = $pdo->query(
        'SELECT Id, Title, OptionalCount, @@TRANCOUNT AS TransactionCount FROM #PhpGuide');
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($row !== ['Id' => 1, 'Title' => $title, 'OptionalCount' => null, 'TransactionCount' => 0]) {
        throw new RuntimeException('Unexpected committed values, types, or transaction state.');
    }
    if ($stmt->fetch(PDO::FETCH_ASSOC) !== false || $pdo->inTransaction()) {
        throw new RuntimeException('Expected one result row and no open transaction.');
    }
} catch (Throwable $error) {
    $failure = $error;
    if ($pdo->inTransaction()) {
        try {
            $pdo->rollBack();
        } catch (PDOException $rollbackError) {
            $failure = new RuntimeException(
                'Rollback failed: ' . $rollbackError->getMessage(), 0, $error);
        }
    }
    throw $failure;
} finally {
    $stmt = null;
    try {
        $pdo->exec('DROP TABLE IF EXISTS #PhpGuide');
    } catch (PDOException $cleanupError) {
        throw new RuntimeException(
            'Cleanup failed: ' . $cleanupError->getMessage(), 0, $failure ?? $cleanupError);
    } finally {
        $pdo = null;
    }
}
echo "Inserted 1 row; read committed UTF-8 text and null; no open transaction.", PHP_EOL;
```

Expected output for either program:

```output
Inserted 1 row; read committed UTF-8 text and null; no open transaction.
```

The output appears only after the checks and cleanup succeed. A connection, write, fetch, rollback, or table-cleanup failure raises an exception instead.

## Understand the choices

Use `?` placeholders for values rather than concatenating input into T-SQL. The examples keep the table and column names fixed: a value parameter can't replace an identifier. Prepared statements also let you execute the same statement again with different values. For more information, see [Compare query execution functions](comparing-execution-functions.md).

| Choice | Reason |
| --- | --- |
| Explicit transaction methods | The example treats the write as a unit that must either commit or roll back. SQLSRV uses `sqlsrv_begin_transaction`, `sqlsrv_commit`, and `sqlsrv_rollback`; PDO uses its own transaction methods. |
| SQLSRV parameter descriptors | Each descriptor specifies direction, PHP type, and SQL type. References retain the input variables, including the variable holding `null`. |
| PDO `bindValue` | The statement receives each current value with an explicit PDO parameter type. `bindParam` is appropriate when later executions must read a variable by reference. |
| UTF-8 plus **nvarchar** | PHP supplies UTF-8 bytes; the extension and ODBC driver convert them for the SQL Unicode column. The fetched text must match the original PHP string byte for byte. |
| Explicit numeric fetching for PDO | The result comparison expects PHP integers. Without the PDO_SQLSRV attribute, these SQL integer results use the driver's default string representation. |
| Named fetch keys | `SQLSRV_FETCH_ASSOC` and `PDO::FETCH_ASSOC` return arrays indexed by column name. The API constants aren't interchangeable. |
| `PDO::exec` for table setup | It executes the table-creation statement directly. This approach avoids creating a temporary table inside the scope of a server-prepared statement. |
| Forward-only results | The programs read each row once. They don't request a buffered result or use a SELECT row count as proof that fetching succeeded. |

SQLSRV returns `null` when its array fetch reaches the end and `false` on a fetch error. PDO's fetch returns `false` at the end; with exception mode configured, a database fetch error throws. In both APIs, the `OptionalCount` field itself contains PHP `null`. That field value isn't an end-of-results marker.

The write commits before the verification query. If a later read fails, the earlier committed write can't be rolled back. In this example, the row belongs only to a temporary table that cleanup drops. In an application with permanent tables, report the failure accurately and don't blindly repeat an insert after an uncertain commit outcome.

## Handle failure and cleanup

The `catch` block rolls back an active transaction and rethrows the failure. The `finally` block releases the statement, drops the temporary table, and releases the connection. Cleanup runs on the success path and the failure path. If rollback or table cleanup also fails, the program preserves the original exception as a previous exception.

For a command-line exercise, an uncaught exception stops PHP with a nonzero exit code and an error message. In a web application, catch it in the code that handles the request. Remove sensitive values before recording diagnostics in your application log, and return an appropriate error response. Don't expose connection details, credentials, or raw database errors to the browser.

| Failure | What to check |
| --- | --- |
| Extension function or driver is missing | Confirm the PHP process loads the intended extension with the matching runtime and configuration. A working command-line PHP configuration doesn't prove your web server uses it. |
| Connection or certificate failure | Check the endpoint, authentication, permissions, and client certificate trust before changing query code. Don't disable certificate validation as a production remedy. |
| Temporary table isn't found | Keep setup, writes, and reads on one connection. For PDO setup, keep the demonstrated `exec` path instead of substituting a default prepared `query` call. |
| Unexpected value or PHP type | Check fetch attributes and parameter descriptors. Don't replace strict assertions with loose comparisons to hide the difference. |
| Write or rollback failure | Capture SQLSRV's errors immediately, or the PDO exception. Report rollback failures too; don't claim the write was undone if rollback failed. |
| Cleanup failure after connection loss | The program can't confirm the server-side drop. Releasing PHP handles doesn't prove the underlying pooled physical connection closed immediately. |

The PHP extensions provide the binding and fetch APIs. ODBC communicates with the server and manages connection pooling. SQL Server controls temporary-table scope, transactions, constraints, and committed data. Drop the table explicitly rather than relying on the physical connection closing for cleanup.

After the exercise, delete `workflow-sqlsrv.php` and `workflow-pdo.php` if you no longer need them. Clear the credential environment variables from the test process when you finish. Neither program leaves a permanent table to remove.

These walkthroughs target SQL Server. For Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Fabric, and Warehouse in Fabric, first check the target's authentication and T-SQL support. Both Fabric products require Microsoft Entra ID authentication, and Warehouse requires multiple active result sets (MARS) to be disabled. These SQL Server programs don't establish service-specific transaction or temporary-table support.

<a id="in-this-section"></a>

## Choose your next task

Extend the workflow with the article for the behavior you need.

| Task | Guidance |
| --- | --- |
| Choose authentication or manage connections | [Connecting to the server](connecting-to-the-server.md). |
| Reuse a prepared statement with different values | [Compare query execution functions](comparing-execution-functions.md). |
| Choose PDO direct or prepared execution | [Direct statement execution and prepared statement execution in the PDO_SQLSRV driver](direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md). |
| Fetch values and choose result shapes | [Retrieving data](retrieving-data.md). |
| Scroll or buffer a result | [Specifying a cursor type and selecting rows](specifying-a-cursor-type-and-selecting-rows.md). |
| Change application data | [Updating data](updating-data-microsoft-drivers-for-php-for-sql-server.md). |
| Control PHP and SQL type conversions | [Converting data types](converting-data-types.md). |
| Diagnose failures and enable driver logging | [Handling errors and warnings](handling-errors-and-warnings.md) and [Logging activity](logging-activity.md). |
| Protect sensitive columns | [Using Always Encrypted with the Microsoft Drivers for PHP for SQL Server](using-always-encrypted-php-drivers.md). |
| Harden a production application | [Security considerations for the Microsoft Drivers for PHP for SQL Server](security-considerations-for-php-sql-driver.md). |

## Related content

- [Overview of the Microsoft Drivers for PHP for SQL Server](overview-of-the-php-sql-driver.md)
- [Getting started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)
- [SQLSRV driver API reference](sqlsrv-driver-api-reference.md)
- [Constants (Microsoft Drivers for PHP for SQL Server)](constants-microsoft-drivers-for-php-for-sql-server.md)
- [Example application (SQLSRV driver)](example-application-sqlsrv-driver.md)
