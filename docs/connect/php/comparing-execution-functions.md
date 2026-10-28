---
title: "Compare Query Execution Functions"
description: "Compare the query execution functions in the SQLSRV and PDO_SQLSRV drivers for PHP, and choose between a single-execution call and a prepared statement."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
helpviewer_keywords:
  - "executing queries"
ai-usage: ai-assisted
---
# Comparing execution functions

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Both drivers can run a statement once or prepare a statement for repeated execution. Use parameters for values supplied by users, even when you run a query only once.

Choose the execution API separately from how you fetch results. Preparing a query doesn't fetch its rows, and buffering a result doesn't make a query parameterized. The following examples prepare a read-only query once, execute it twice with different values, and consume each result before the next execution.

| Execution style | SQLSRV | PDO_SQLSRV |
|-|-|-|
| Run a query once, without parameters | [sqlsrv_query](sqlsrv-query.md) | [PDO::query](pdo-query.md) for results, or [PDO::exec](pdo-exec.md) for affected-row counts |
| Run a query once, with parameters | `sqlsrv_query($conn, $sql, $params)` | `PDO::prepare()` followed by `PDOStatement::execute()` |
| Prepare once, execute repeatedly with different parameters | [sqlsrv_prepare](sqlsrv-prepare.md) + [sqlsrv_execute](sqlsrv-execute.md) | [PDO::prepare](pdo-prepare.md) + [PDOStatement::execute](pdostatement-execute.md) |

`sqlsrv_query()` prepares and executes in one call and returns a statement resource. `sqlsrv_prepare()` returns a statement resource without executing it; call `sqlsrv_execute()` for each execution. PDO's `query()` executes and returns `PDOStatement`, while `exec()` returns an affected-row count instead of a result object. A successful `exec()` can return `0`, so compare a failure return with `=== false`, not `if (!$count)`.

`PDO::query()` doesn't accept an array of parameter values. Its optional arguments configure fetching. For parameterized PDO_SQLSRV queries, use `prepare()` and `execute()`.

Preparing once lets you reuse a statement handle with different values. This isn't a guarantee that the server never recompiles its execution plan. PDO_SQLSRV uses prepared execution by default, including for `PDO::query()`; its [direct-query attribute](direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md) controls that separate choice.

## Prepare once and execute repeatedly

These are separate, complete command-line programs. Install and load the selected extension, and set `SQL_SERVER`, `SQL_DATABASE`, `SQL_USER`, and `SQL_PASSWORD` in the PHP process environment. Use a SQL Server test database and a server certificate trusted by the client. For the environment setup and passwordless alternatives, see [Connecting to the server](connecting-to-the-server.md).

Store the actual database name, username, and password in the environment variables. Both programs escape these values once by doubling closing braces and enclosing each value in braces, as required by the PHP driver's ODBC-style connection handling. Escaping connection values is separate from binding query parameters.

The programs execute a Transact-SQL (T-SQL) query over two inline rows without creating a table or changing data. Save each program in its own `.php` file and run it with `php <file>`. No sample database is required.

<a id="sqlsrv-execution-functions"></a>

### SQLSRV

Pass `$productId` by reference with `&$productId`. Each `sqlsrv_execute()` then uses its current value. `SQLSRV_FETCH_ASSOC` returns an array keyed by column name. A `null` result means there are no more rows; `false` indicates an error.

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

$sql = "SELECT Name FROM (VALUES (1, 'Bike'), (2, 'Helmet')) AS Product(ProductId, Name) WHERE ProductId = ?";
$productId = 1;
$stmt = null;
try {
    $stmt = sqlsrv_prepare($conn, $sql, [&$productId], ['QueryTimeout' => 30]);
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    foreach ([1 => 'Bike', 2 => 'Helmet'] as $productId => $expected) {
        if (!sqlsrv_execute($stmt)) {
            throw new RuntimeException(print_r(sqlsrv_errors(), true));
        }
        $names = [];
        while (($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) !== null) {
            if ($row === false) {
                throw new RuntimeException(print_r(sqlsrv_errors(), true));
            }
            $names[] = $row['Name'];
        }
        if ($names !== [$expected]) {
            throw new RuntimeException('Expected exactly one matching product.');
        }
        echo $names[0], PHP_EOL;
    }
} finally {
    if (is_resource($stmt)) {
        sqlsrv_free_stmt($stmt);
    }
    sqlsrv_close($conn);
}
```

<a id="pdo_sqlsrv-execution-functions"></a>

### PDO_SQLSRV

Bind `$productId` once with `bindParam()`. Each `execute()` reads its current value. `PDO::PARAM_INT` specifies the parameter's PHP/PDO binding type; it doesn't control the PHP type of columns fetched from the result. Set exception mode so a database error interrupts execution rather than being mistaken for an empty result.

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
$sql = "SELECT Name FROM (VALUES (1, 'Bike'), (2, 'Helmet')) AS Product(ProductId, Name) WHERE ProductId = ?";
$stmt = null;
try {
    $stmt = $pdo->prepare($sql, [PDO::SQLSRV_ATTR_QUERY_TIMEOUT => 30]);
    $productId = 1;
    $stmt->bindParam(1, $productId, PDO::PARAM_INT);
    foreach ([1 => 'Bike', 2 => 'Helmet'] as $productId => $expected) {
        $stmt->execute();
        $names = $stmt->fetchAll(PDO::FETCH_COLUMN);
        if ($names !== [$expected]) {
            throw new RuntimeException('Expected exactly one matching product.');
        }
        echo $names[0], PHP_EOL;
        $stmt->closeCursor();
    }
} finally {
    $stmt = null;
    $pdo = null;
}
```

Each example prints:

```output
Bike
Helmet
```

Values in an array passed to PDO's `execute()` are treated as `PDO::PARAM_STR`. When SQL requires an integer parameter, such as a parameterized `TOP` count, call `bindValue()` with `PDO::PARAM_INT` and then call `execute()` without an array.

## Bind values without changing the query

A parameter marker represents a value, not a table name, column name, keyword, or sort direction. Don't place quotes around `?`. For a dynamic identifier or `ASC`/`DESC`, select from an application-controlled allow list instead of concatenating untrusted text. For an `IN` list, supply one marker per value.

| Requirement | SQLSRV | PDO_SQLSRV |
| --- | --- | --- |
| Bind values for one execution | Pass the parameter array to `sqlsrv_query()`. | Pass an array to `execute()` after `prepare()`, or bind values first. |
| Reuse a variable's current value | Include `&$variable` in the array supplied to `sqlsrv_prepare()`. | Call `bindParam()` with the variable, then `execute()` without an array. PHP passes the variable by reference; don't add `&` at the call site. |
| Bind a value at the time of binding | Use a value in the parameter array for the execution. | Call `bindValue()`; later changes to the original variable don't change that bound value. |
| Specify an integer input | Use a parameter descriptor with `SQLSRV_PARAM_IN`, `SQLSRV_PHPTYPE_INT`, and `SQLSRV_SQLTYPE_INT` when an explicit type is needed. | Bind with `PDO::PARAM_INT`. Positional `bindParam()` and `bindValue()` indexes start at `1`, not `0`. |
| Send SQL `NULL` | Bind PHP `null`, with an explicit SQL type when required by the operation. | Bind with `PDO::PARAM_NULL`. |
| Receive output parameters | Use a referenced variable with `SQLSRV_PARAM_OUT` or `SQLSRV_PARAM_INOUT`. | Use `bindParam()` with `PDO::PARAM_INPUT_OUTPUT` and the required type and length. |

Keep referenced variables alive while executing the prepared statement. The SQLSRV program changes the bound variable rather than replacing the parameter array. The PDO program deliberately calls `execute()` without an array after binding; don't mix that pattern with array-based execution.

Output values from stored procedures require their own handling, including completing the procedure's results. Don't infer output-parameter behavior from these input-only examples. For more information, see [Using directional parameters](using-directional-parameters.md).

## Configure execution and fetch behavior

| Setting | SQLSRV | PDO_SQLSRV |
| --- | --- | --- |
| Execution timeout | `QueryTimeout` in the statement options array. | `PDO::SQLSRV_ATTR_QUERY_TIMEOUT` on the connection or statement. |
| Named result keys | `SQLSRV_FETCH_ASSOC` passed to `sqlsrv_fetch_array()`. | `PDO::FETCH_ASSOC` passed to a fetch method or set as the connection's default fetch mode. |
| Forward-only results | Default cursor; fetch in order. | Default cursor; fetch in order. |
| Client-buffered results | `Scrollable => SQLSRV_CURSOR_CLIENT_BUFFERED`. | `PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL` with `PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE => PDO::SQLSRV_CURSOR_BUFFERED`. |
| Direct versus prepared execution | No PDO direct-query attribute. Choose the combined `sqlsrv_query()` call or the explicit prepare/execute pair. | `PDO::SQLSRV_ATTR_DIRECT_QUERY` selects direct execution versus the driver's default prepared execution. |
| Parameter emulation | No PDO emulation attribute. | `PDO::ATTR_EMULATE_PREPARES` is separate from the direct-query attribute. Don't use it as a substitute for binding the correct types. |

The examples return small results. `fetchAll()` loads every row into PHP memory; use a fetch loop for a large result. A query returning no matches isn't an execution error. SQLSRV reports end-of-result as `null` from `sqlsrv_fetch_array()`; PDO reports it as `false` from `fetch()`. A SQL `NULL` within a fetched row remains PHP `null`.

Don't use a `SELECT` row count as your only success check. Forward-only cursors don't provide the same row-count capabilities as buffered or scrollable cursors. Fetch and count the rows you need, or use a separate `COUNT` query. For cursor-specific options and limitations, see [Specify a cursor type and select rows](specifying-a-cursor-type-and-selecting-rows.md).

## Handle failures and finish results

Check SQLSRV prepare, execute, and fetch operations separately. A successful prepare doesn't prove that execution will succeed: the server can reject parameter values, permissions, or the statement at execution time. Capture `sqlsrv_errors()` immediately when an operation returns `false`. SQLSRV treats many warnings as errors by default, so inspect the complete collection.

For PDO, connection failures and database errors in exception mode throw `PDOException`. Inspect its SQLSTATE and driver error information in protected application diagnostics; don't return exception text or credentials in an HTTP response. `prepare()` succeeding isn't evidence that the statement has already run.

| Symptom | Action |
| --- | --- |
| Every execution uses the first value | For SQLSRV, bind a variable by reference. For PDO, use `bindParam()` for a changing variable or call `bindValue()` for each new value. |
| A numeric parameter is rejected, such as a `TOP` count | Bind the required integer type explicitly. PDO `execute([$value])` uses string parameters even if the PHP value is an integer. |
| A second command reports pending results | Consume results or release them before reusing the connection. For PDO, use `closeCursor()` when you don't need the remaining result. |
| Expected output parameters haven't changed | Complete all procedure results and verify parameter direction, PHP type, and output-buffer length. |
| Memory use grows with result size | Replace `fetchAll()` or unnecessary client buffering with sequential fetching. |

The examples free their statements and release their connections in `finally`. They don't begin a transaction. Use an explicit [transaction](how-to-perform-transactions.md) when multiple writes must either commit together or all roll back. Reusing a prepared statement doesn't group the writes into a transaction.

## Target and implementation differences

The prepare/execute APIs and PHP reference rules belong to the extensions and PHP/PDO. The SQL target decides which T-SQL statements and types are supported, and ODBC performs the underlying communication. Preparing once can reduce repeated work, but it doesn't guarantee a particular server execution plan or speedup.

The input-binding patterns apply to SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric. These programs use SQL authentication to SQL Server. For Azure onboarding, prefer Microsoft Entra authentication. Both Fabric products require it, and Warehouse also requires disabling Multiple Active Result Sets (MARS). Check the selected target's support for schema changes, transactions, and stored procedures before reusing SQL Server code. For endpoint and authentication differences, see [Azure and Fabric connections](connecting-to-microsoft-azure-sql-database.md).

## Related content

- [SQLSRV driver API reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV driver reference](pdo-sqlsrv-driver-reference.md)
- [Programming guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
