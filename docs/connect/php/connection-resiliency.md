---
title: Idle Connection Resiliency
description: Learn what idle connection resiliency is, which connection keywords control it, and how it behaves in the Microsoft Drivers for PHP for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Idle connection resiliency

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

[Connection resiliency](../odbc/connection-resiliency.md) lets the Microsoft ODBC Driver for SQL Server restore a broken *idle* connection when the server and session state support recovery. The PHP extensions pass `ConnectRetryCount` and `ConnectRetryInterval` to ODBC; they don't implement the reconnection protocol. The ODBC documentation lists support for SQL Server 2014 and later versions, Azure SQL Database, and SQL database in Microsoft Fabric. Don't infer Warehouse support from support for SQL database in Fabric.

|Keyword|Values|Default|Description|
|-|-|-|-|
|`ConnectRetryCount`| Integer between 0 and 255 (inclusive)|1|The maximum number of attempts to reestablish a broken connection before giving up. A value of `0` disables reconnection.|
|`ConnectRetryInterval`| Integer between 1 and 60 (inclusive)|10| The number of seconds to wait between attempts. Ignored if `ConnectRetryCount` is `0`.|

The statement's query timeout limits how long ODBC can spend recovering an idle connection during execution. Set `QueryTimeout` in SQLSRV statement options or `PDO::SQLSRV_ATTR_QUERY_TIMEOUT` for PDO_SQLSRV. `LoginTimeout` controls establishing a connection; increasing it doesn't extend the statement's recovery budget.

Set the same retry keywords in a SQLSRV connection options array, such as `["ConnectRetryCount" => 2, "ConnectRetryInterval" => 5]`, or in a PDO_SQLSRV data source name (DSN), such as `ConnectRetryCount=2;ConnectRetryInterval=5`. These settings don't replay a failed transaction or retry the application's initial connection call.

For application-level retry of transient errors that occur *while* a query is executing (which idle connection resiliency doesn't cover), see the retry pattern in [Microsoft Drivers for PHP for SQL Server](microsoft-php-driver-for-sql-server.md#production-baseline-for-azure-sql).

## Choose the recovery policy

Idle connection recovery preserves a recoverable session so the application can continue using its PHP connection handle. It isn't a general instruction to execute a command again.

| Failure | Does idle connection resiliency handle it? | Application action |
| --- | --- | --- |
| The initial connection can't be established | No. There is no established session to restore. | Inspect the connection error. Retry only a transient failure with a bounded policy. |
| An idle, recoverable connection breaks | ODBC can attempt recovery when the connection is used again. | Allow enough statement timeout for the intended recovery attempts; still handle a final failure. |
| A statement fails during execution | Don't assume recovery or replay. | Inspect the error and determine whether the operation completed. Retry a read only if repeating it is acceptable to your application. |
| A connection breaks during a transaction | Don't assume the transaction can continue. | Abandon the broken session and determine whether the transaction committed before deciding on another attempt. |
| The client explicitly closes its PHP connection | No. Closing a handle is application cleanup, not an idle disconnect. | Open a new connection if more work is needed. |

Keep recovery intervals and query deadlines consistent. For example, the programs use two reconnect attempts with a 5-second interval and a 30-second statement timeout. Connection attempts also take time; multiplying count by interval isn't a guaranteed total recovery duration. A final timeout still requires error handling.

## Remarks

Connection resiliency only applies when the connection is idle. A failure during a transaction doesn't trigger reconnection. The following session states also prevent reconnection:

* Temporary tables.
* Global and local cursors.
* Transaction context and session-level transaction locks.
* Application locks.
* `EXECUTE AS`/`REVERT` security context.
* Object Linking and Embedding (OLE) automation handles.
* Prepared Extensible Markup Language (XML) handles.
* Trace flags.

## Example

Each program opens two connections to a test SQL Server instance. The second connection terminates the first session with `KILL`. A subsequent query on the first connection exercises ODBC's idle-connection recovery. The query uses inline rows, so no table setup is required. The programs release each statement before breaking the connection and verify both the recovered query's values and a changed server login time. A session ID alone isn't proof of recovery because SQL Server can reuse it.

> [!CAUTION]
> Run this example only against a test instance. The login needs permission to terminate a session with [KILL](../../t-sql/language-elements/kill-transact-sql.md). The code terminates only the session it just opened. Don't substitute another application's session ID.

Set `SQL_SERVER`, `SQL_DATABASE`, `SQL_USER`, and `SQL_PASSWORD` in the process environment. Configure a trusted server certificate whose name matches `SQL_SERVER`. This example uses SQL authentication to a test SQL Server instance. It isn't a Fabric or managed-identity authentication example.

Store the actual database name, username, and password in the environment variables. Each program escapes these values once for the PHP driver's ODBC-style connection handling by doubling closing braces and enclosing each value in braces. Both connections reuse the escaped values.

### SQLSRV

Save the program as `recover-sqlsrv.php`, and run `php recover-sqlsrv.php`. The helper throws on SQLSRV failure; it doesn't retry any application statement.

```php
<?php
function requireSqlsrv($result)
{
    if ($result === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    return $result;
}

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
$connectionInfo = [
    'Database' => $config['SQL_DATABASE'],
    'UID' => $config['SQL_USER'],
    'PWD' => $config['SQL_PASSWORD'],
    'Encrypt' => true,
    'TrustServerCertificate' => false,
    'LoginTimeout' => 30,
    'ConnectRetryCount' => 2,
    'ConnectRetryInterval' => 5,
];
$conn = requireSqlsrv(sqlsrv_connect($config['SQL_SERVER'], $connectionInfo));
$breaker = null;
try {
    $breaker = requireSqlsrv(sqlsrv_connect($config['SQL_SERVER'], $connectionInfo));
    $sessionQuery = "SELECT @@SPID AS SessionId, "
        . "CONVERT(varchar(33), login_time, 126) AS LoginTime "
        . "FROM sys.dm_exec_sessions WHERE session_id = @@SPID";
    $idStatement = requireSqlsrv(sqlsrv_query($conn, $sessionQuery));
    $before = requireSqlsrv(sqlsrv_fetch_array($idStatement, SQLSRV_FETCH_ASSOC));
    sqlsrv_free_stmt($idStatement);
    if (!is_array($before) || !is_int($before['SessionId']) || $before['SessionId'] <= 0) {
        throw new RuntimeException('Expected the current session identity.');
    }
    $spid = $before['SessionId'];

    $query = 'SELECT id FROM (VALUES (1), (2), (3)) AS Demo(id) ORDER BY id';
    $options = ['Scrollable' => SQLSRV_CURSOR_CLIENT_BUFFERED, 'QueryTimeout' => 30];
    $stmt = requireSqlsrv(sqlsrv_query($conn, $query, [], $options));
    if (requireSqlsrv(sqlsrv_num_rows($stmt)) !== 3) {
        throw new RuntimeException('Expected three rows before the disconnect.');
    }
    sqlsrv_free_stmt($stmt);

    // KILL doesn't accept a parameter marker; $spid is a checked server-returned integer.
    $kill = requireSqlsrv(sqlsrv_query($breaker, 'KILL ' . $spid));
    sqlsrv_free_stmt($kill);
    // Allow the simulated disconnect to reach the client before the next query.
    sleep(1);

    $stmt = requireSqlsrv(sqlsrv_query($conn, $query, [], $options));
    $ids = [];
    while (($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) !== null) {
        $row = requireSqlsrv($row);
        $ids[] = $row['id'];
    }
    if ($ids !== [1, 2, 3]) {
        throw new RuntimeException('Unexpected recovered rows or PHP types.');
    }
    sqlsrv_free_stmt($stmt);

    $idStatement = requireSqlsrv(sqlsrv_query($conn, $sessionQuery));
    $after = requireSqlsrv(sqlsrv_fetch_array($idStatement, SQLSRV_FETCH_ASSOC));
    sqlsrv_free_stmt($idStatement);
    if (!is_array($after) || $after['LoginTime'] === $before['LoginTime']) {
        throw new RuntimeException('Expected a new physical session.');
    }
    echo "Recovered connection: 3 matching rows; login time changed.\n";
} finally {
    if (is_resource($breaker)) {
        sqlsrv_close($breaker);
    }
    sqlsrv_close($conn);
}
```

### PDO_SQLSRV

Save this separate program as `recover-pdo.php`, and run `php recover-pdo.php` with the same environment and permissions. It sets numeric fetching explicitly so the integer assertions match the SQLSRV program. PDO has no `sqlsrv_errors()` call; operation failures throw `PDOException` in exception mode.

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
$dsn = "sqlsrv:Server=$server;Database=$database;Encrypt=true;"
    . "TrustServerCertificate=false;LoginTimeout=30;"
    . "ConnectRetryCount=2;ConnectRetryInterval=5";
$attributes = [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE => true,
    PDO::SQLSRV_ATTR_QUERY_TIMEOUT => 30,
];
$pdo = new PDO($dsn, $config['SQL_USER'], $config['SQL_PASSWORD'], $attributes);
$breaker = null;
$stmt = null;
try {
    $breaker = new PDO($dsn, $config['SQL_USER'], $config['SQL_PASSWORD'], $attributes);
    $sessionQuery = "SELECT @@SPID AS SessionId, "
        . "CONVERT(varchar(33), login_time, 126) AS LoginTime "
        . "FROM sys.dm_exec_sessions WHERE session_id = @@SPID";
    $stmt = $pdo->query($sessionQuery);
    $before = $stmt->fetch(PDO::FETCH_ASSOC);
    $stmt = null;
    if (!is_array($before) || !is_int($before['SessionId']) || $before['SessionId'] <= 0) {
        throw new RuntimeException('Expected the current session identity.');
    }
    $spid = $before['SessionId'];
    $query = 'SELECT id FROM (VALUES (1), (2), (3)) AS Demo(id) ORDER BY id';
    $stmt = $pdo->query($query);
    if ($stmt->fetchAll(PDO::FETCH_COLUMN) !== [1, 2, 3]) {
        throw new RuntimeException('Unexpected initial rows or PHP types.');
    }
    $stmt = null;

    // KILL requires a checked integer, not a parameter marker.
    $stmt = $breaker->query('KILL ' . $spid);
    $stmt = null;
    sleep(1);

    $stmt = $pdo->query($query);
    if ($stmt->fetchAll(PDO::FETCH_COLUMN) !== [1, 2, 3]) {
        throw new RuntimeException('Unexpected recovered rows or PHP types.');
    }
    $stmt = null;
    $stmt = $pdo->query($sessionQuery);
    $after = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!is_array($after) || $after['LoginTime'] === $before['LoginTime']) {
        throw new RuntimeException('Expected a new physical session.');
    }
    echo "Recovered connection: 3 matching rows; login time changed.\n";
} finally {
    $stmt = null;
    $breaker = null;
    $pdo = null;
}
```

Expected output from either program when ODBC restores the connection:

```output
Recovered connection: 3 matching rows; login time changed.
```

The test terminates a deliberately idle session. It doesn't test a network partition, service failover, active transaction recovery, write replay, or Azure throttling. Don't interpret a pass as proof that every interrupted application operation is safe to repeat.

## Diagnose a failed recovery

| Symptom | Check |
| --- | --- |
| `KILL` is denied | Use a permitted test identity or ask the administrator to perform the controlled test. Don't substitute another session ID. |
| The first query already fails | Fix the connection, certificate, query, and permissions before testing recovery. |
| The query after `KILL` fails | Check `ConnectRetryCount`, the query timeout, ODBC diagnostics, and whether the session state is recoverable. Don't hide the error with an unconditional retry. |
| A temporary table disappears after opening a new connection | An application-created replacement connection isn't the old session. Redesign session-dependent work rather than assuming automatic state recreation. |
| A write returns a connection error | Establish its outcome before retrying. The client error alone doesn't prove that the server rolled it back. |

Keep diagnostics in protected logs. SQLSRV exposes an error collection that can contain more than one record; PDO supplies SQLSTATE and driver details through the exception. Record the operation and its timeout settings without recording passwords or tokens. For more information, see [Logging activity](logging-activity.md).

## Target applicability

ODBC and the SQL target perform the automatic reconnection. PHP passes settings and reports success or failure. It doesn't recreate temporary objects, restore your application's variables, or replay a transaction.

The controlled programs use SQL Server and require permission to terminate their own test session through a second connection. Azure SQL Database and SQL database in Fabric have their own service permissions and authentication requirements. Don't treat those programs as drop-in tests for Azure SQL Managed Instance, Azure Synapse Analytics, or Warehouse in Fabric, or infer their recovery support from the fact that an ordinary connection succeeds. Check the selected service's recovery support before relying on it. Warehouse and SQL database in Fabric remain distinct targets.

## Related content

- [Connection resiliency in the ODBC driver](../odbc/connection-resiliency.md)
- [Microsoft Drivers for PHP for SQL Server: production baseline for Azure SQL](microsoft-php-driver-for-sql-server.md#production-baseline-for-azure-sql)
