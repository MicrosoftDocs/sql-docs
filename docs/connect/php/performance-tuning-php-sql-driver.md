---
title: Performance Tuning for the Microsoft Drivers for PHP for SQL Server
description: Optimize connection management, query patterns, batching, and memory use in PHP applications that connect to SQL Server, Azure SQL, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 07/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Performance tuning for the Microsoft Drivers for PHP for SQL Server

[!INCLUDE [Driver_PHP_Download](../../includes/driver_php_download.md)]

This article covers how to write fast PHP code against SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, and SQL database in Microsoft Fabric. The guidance applies to both SQLSRV and PDO_SQLSRV, which wrap the same underlying Microsoft ODBC Driver for SQL Server.

## Start with the highest-impact changes

If you can only make three changes, make these changes:

- **Enable connection pooling.** Establishing a new TLS connection to SQL Server takes tens to hundreds of milliseconds depending on network path and TLS negotiation. Reusing pooled connections eliminates that cost per request. See [Manage connections efficiently](#manage-connections-efficiently).
- **Fetch only the columns and rows you need.** `SELECT *` and unbounded queries are the most common causes of slow endpoints. See [Query only what you need](#query-only-what-you-need).
- **Use table-valued parameters for bulk inserts.** For hundreds of rows or more, table-valued parameters (TVPs) are typically much faster than row-by-row `INSERT` statements and scale linearly with row count. See [Insert data efficiently](#insert-data-efficiently).

## Manage connections efficiently

Connection establishment is the single most expensive operation the driver performs. Almost every PHP performance investigation ends with a connection-management fix.

### Enable connection pooling

Pooling reuses ODBC connections across PHP requests instead of tearing them down at request end. The connection object is discarded when your script ends, but the underlying ODBC handle stays alive in the ODBC driver manager's pool and gets reused by the next request that asks for the same connection string.

**Windows**: Connection pooling is on by default. To confirm, leave the `ConnectionPooling` option out of your DSN. To disable pooling for debugging, set `ConnectionPooling=0`.

**Linux and macOS**: Connection pooling isn't a DSN option on these platforms. Enable it in the driver manager by setting `Pooling=Yes` in the `[ODBC]` section of `odbcinst.ini`, and set a positive `CPTimeout` under the driver's stanza. For example:

```ini
[ODBC]
Pooling=Yes

[ODBC Driver 18 for SQL Server]
Description=Microsoft ODBC Driver 18 for SQL Server
Driver=/opt/microsoft/msodbcsql18/lib64/libmsodbcsql-18.<version>.so.1.1
CPTimeout=120
```

Find the actual library path with `odbcinst -q -d -n "ODBC Driver 18 for SQL Server"` or `ls /opt/microsoft/msodbcsql18/lib64/`. The filename embeds the installed ODBC driver version and changes with each release.

`CPTimeout` (in seconds) controls how long idle connections stay in the pool before they're closed. Set it high enough that most requests find a pooled connection, but low enough that stale connections to a failed-over server get retired reasonably fast. 60 to 300 seconds works well for most web workloads.

For details, see [Connection pooling](connection-pooling-microsoft-drivers-for-php-for-sql-server.md).

### Understand the first-query cost

Multiple Active Result Sets (MARS) is enabled by default. When MARS and connection pooling are both active, the driver resets the pooled connection on the *first* query, and that reset ignores any query timeout you've set for that first query. Later queries on the same connection honor the timeout normally. If you set aggressive first-query timeouts on a pooled workload, account for this behavior, or disable MARS with `MultipleActiveResultSets=false` if you don't need it. See the MARS and pooling note in [Connection pooling](connection-pooling-microsoft-drivers-for-php-for-sql-server.md).

### Persistent PDO connections aren't supported

PDO_SQLSRV rejects `PDO::ATTR_PERSISTENT`. Setting it on the constructor throws:

```output
SQLSTATE[IMSSP]: An unsupported attribute was designated on the PDO object.
```

Use [ODBC connection pooling](connection-pooling-microsoft-drivers-for-php-for-sql-server.md) for cross-request reuse. It's the driver-native mechanism, works for both PDO_SQLSRV and SQLSRV, and retires idle connections on `CPTimeout` (which also keeps Microsoft Entra token refresh honest).

### Reuse the connection within a request

Even with pooling, opening a new PDO or SQLSRV connection incurs an ODBC round trip to fetch and validate a pooled handle. Open a connection once per request and pass it into every function that needs it.

> [!TIP]
> A dependency-injection container or a lazy accessor is enough. The point is to avoid `new PDO(...)` in the middle of a request handler.

## Query only what you need

Network round trips and result set materialization dominate query latency for most PHP workloads. The fixes are the same ones that apply to every database access layer.

### Select only the columns you use

`SELECT *` pulls every column, including **varchar(max)** and **varbinary(max)** columns that dwarf the data you actually consume. Name the columns:

```php
<?php
// Slow: fetches all columns, including a 2 MB LOB column
$stmt = $conn->query("SELECT * FROM dbo.Products");

// Fast: fetches only the two columns the caller uses
$stmt = $conn->query("SELECT ProductID, Name FROM dbo.Products");
```

### Fetch only the rows you need

Push filtering to SQL Server. Never fetch a full table into PHP just to filter in a `foreach` loop.

```php
<?php
// Slow: transfers every row to PHP, then filters
$rows = $conn->query("SELECT * FROM dbo.Orders")->fetchAll(PDO::FETCH_ASSOC);
$recent = array_filter($rows, fn($r) => $r["OrderDate"] > "2026-01-01");

// Fast: filters on the server
$stmt = $conn->prepare("SELECT OrderID, CustomerID, Total FROM dbo.Orders WHERE OrderDate > ?");
$stmt->execute(["2026-01-01"]);
$recent = $stmt->fetchAll(PDO::FETCH_ASSOC);
```

### Paginate large result sets

For a list view that shows a few hundred rows out of millions, don't return all rows and let the client sort it out. Use server-side pagination with `OFFSET ... FETCH`:

```php
<?php
function fetchPage(PDO $conn, int $page, int $pageSize): array {
    $stmt = $conn->prepare(
        "SELECT OrderID, CustomerID, Total
         FROM dbo.Orders
         ORDER BY OrderID
         OFFSET ? ROWS FETCH NEXT ? ROWS ONLY"
    );
    // With native prepares, execute([...]) binds values as strings.
    // OFFSET and FETCH NEXT require integer bindings; bind explicitly.
    $stmt->bindValue(1, ($page - 1) * $pageSize, PDO::PARAM_INT);
    $stmt->bindValue(2, $pageSize, PDO::PARAM_INT);
    $stmt->execute();
    return $stmt->fetchAll(PDO::FETCH_ASSOC);
}
```

### Choose the right fetch method

- Use `fetch(PDO::FETCH_ASSOC)` in a loop for streaming iteration when you don't need all rows in memory at once.
- Use `fetchAll(PDO::FETCH_ASSOC)` when the caller genuinely needs the whole set (for example, rendering a full JSON response).
- Use `fetchColumn()` when you only care about a single scalar (a `COUNT`, `SUM`, or `MAX`).
- Use `PDO::FETCH_KEY_PAIR` or `PDO::FETCH_UNIQUE` to build lookup dictionaries without a second pass.

Numeric fetch modes (`PDO::FETCH_NUM`) are marginally faster than associative fetch modes because they skip building the column-name map. Prefer clarity; only switch when a profiler flags fetch overhead as significant.

### Prefer `SET NOCOUNT ON` in stored procedures and batches

Every `INSERT`, `UPDATE`, and `DELETE` statement returns a `DONE_IN_PROC` token with the affected rowcount, which PHP typically discards. The token doesn't add a round trip, but each one still costs bytes on the wire and a small amount of driver work. In a multi-statement procedure or batch that runs hundreds of statements per call, the savings add up. Turn it off:

```sql
CREATE OR ALTER PROCEDURE dbo.ProcessOrder
    @OrderID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Inventory SET Stock = Stock - 1 WHERE ProductID IN (SELECT ProductID FROM dbo.OrderLines WHERE OrderID = @OrderID);
    UPDATE dbo.Orders SET Status = 'Processed' WHERE OrderID = @OrderID;
END;
```

## Insert data efficiently

Choose the right insert method based on how many rows you're moving. The wrong choice can be 100x slower.

### Fewer than about 100 rows: prepared statement in a loop

For small batches, execute a single prepared statement in a loop:

```php
<?php
$stmt = $conn->prepare("INSERT INTO dbo.Products (Name, Price) VALUES (?, ?)");
foreach ($products as $p) {
    $stmt->execute([$p["name"], $p["price"]]);
}
```

Wrap the loop in a transaction so all inserts commit as one unit and the log doesn't have to flush after every row:

```php
<?php
$conn->beginTransaction();
try {
    $stmt = $conn->prepare("INSERT INTO dbo.Products (Name, Price) VALUES (?, ?)");
    foreach ($products as $p) {
        $stmt->execute([$p["name"], $p["price"]]);
    }
    $conn->commit();
} catch (PDOException $e) {
    $conn->rollBack();
    throw $e;
}
```

### Hundreds to millions of rows: table-valued parameters

Table-valued parameters (TVPs) send the entire batch to SQL Server in one round trip and let SQL Server process the set as a single statement. For batches of hundreds of rows or more, TVPs are typically much faster than a prepared-statement loop and they scale linearly with row count.

First, create a table type on the server:

```sql
CREATE TYPE dbo.ProductTableType AS TABLE (
    Name  NVARCHAR(100),
    Price DECIMAL(10, 2)
);
```

PDO_SQLSRV passes the TVP as an associative array whose key is the type name and whose value is the row set. Bind it with `PDO::PARAM_LOB`:

```php
<?php
$rows = [];
foreach ($products as $p) {
    $rows[] = [$p["name"], $p["price"]];
}
$tvpInput = ["ProductTableType" => $rows];

$stmt = $conn->prepare(
    "INSERT INTO dbo.Products (Name, Price) SELECT Name, Price FROM ?"
);
$stmt->bindParam(1, $tvpInput, PDO::PARAM_LOB);
$stmt->execute();
```

For a non-default schema, pass the schema as the array's next element: `["ProductTableType" => $rows, "Sales"]`. For the SQLSRV procedural syntax and stored procedure examples, see [Use table-valued parameters](use-table-valued-parameters.md).

### Millions of rows: bcp or BULK INSERT

For truly bulk operations (data warehouse loads, initial migrations), use [bcp](../../tools/bcp/bcp-utility.md) or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) instead of PHP. Write your data to a delimited or native-format file, then run bcp or `BULK INSERT` from a scheduled job, ETL step, or admin script.

> [!CAUTION]
> If you shell out to bcp from PHP with `shell_exec()` or `proc_open()`, never interpolate untrusted input into the command line. Use `escapeshellarg()` on every argument, and prefer running the load out-of-band instead of in a web request path.

## Reduce round trips

Every network round trip between PHP and SQL Server has a fixed cost. When you send five statements as one batch, you pay that cost once instead of five times.

### Combine related statements into a single batch

For related work that runs together, put the statements into one batch and consume every result set:

```php
<?php
$sql = "
    SELECT * FROM dbo.Customers WHERE CustomerID = ?;
    SELECT * FROM dbo.Orders WHERE CustomerID = ?;
    SELECT * FROM dbo.Addresses WHERE CustomerID = ?;
";
$stmt = $conn->prepare($sql);
$stmt->execute([$id, $id, $id]);

$customer = $stmt->fetch(PDO::FETCH_ASSOC);

$stmt->nextRowset();
$orders = $stmt->fetchAll(PDO::FETCH_ASSOC);

$stmt->nextRowset();
$addresses = $stmt->fetchAll(PDO::FETCH_ASSOC);
```

For SQLSRV, use [`sqlsrv_next_result`](sqlsrv-next-result.md) to advance between result sets.

### Enable Multiple Active Result Sets when you need it

Multiple Active Result Sets (MARS) lets a single connection have multiple active statements. Without MARS, you can't issue a new query on a connection that still has an open result set. Both drivers enable MARS by default. To turn it off, set `MultipleActiveResultSets=false` in your connection string. See [Disable Multiple Active Result Sets (MARS)](how-to-disable-multiple-active-resultsets-mars.md).

MARS is convenient but not free. Each active result set consumes server-side resources. Prefer consuming a result set fully before starting another one. Use MARS to unblock genuinely nested cursor patterns.

## Tune prepared statements

Prepared statements save the driver from re-parsing SQL on the server, and they let you safely bind untrusted input as parameters.

### Prefer native prepares

PDO_SQLSRV can prepare statements in two modes. *Native prepares* send the SQL text to the server once and reuse the parsed statement for every execution, sending only the parameter values on each `execute()`. *Emulated prepares* keep the SQL text in the client and rebuild a full SQL string with parameters interpolated on each execution.

Set `PDO::ATTR_EMULATE_PREPARES => false` so the driver uses native prepares. Native prepares let SQL Server cache and reuse the query plan, and they avoid re-parsing SQL text on every execution.

```php
<?php
$conn = new PDO($dsn, null, null, [
    PDO::ATTR_ERRMODE          => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_EMULATE_PREPARES => false,
]);
```

### Reuse prepared statements

Prepare once, execute many. Every `prepare()` call costs an ODBC handle allocation and a server-side parse. In a hot loop, keep the `$stmt` object alive and call `execute()` inside the loop:

```php
<?php
// Fast: one prepare, many executes.
$stmt = $conn->prepare("UPDATE dbo.Inventory SET Stock = Stock - ? WHERE ProductID = ?");
foreach ($orderLines as $line) {
    $stmt->execute([$line["qty"], $line["productId"]]);
}

// Slow: re-prepares the same SQL on every iteration.
foreach ($orderLines as $line) {
    $stmt = $conn->prepare("UPDATE dbo.Inventory SET Stock = Stock - ? WHERE ProductID = ?");
    $stmt->execute([$line["qty"], $line["productId"]]);
}
```

### Watch out for `TOP (?)` and `IN (?, ?, ...)`

`TOP` requires parentheses around a parameter marker, `SELECT TOP (?) ...`, so SQL Server can parse the row count as a parameter. `IN (?, ?, ?, ?)` requires a fixed placeholder count at prepare time. For dynamic `IN` list sizes, either build the placeholder string from a validated integer count, or pass the list as a table-valued parameter.

> [!CAUTION]
> Never interpolate raw user input into the SQL text (including the placeholder count). Cast the count with `(int)` before building the placeholder string, and always pass the actual values through `execute()` as parameters.

## Manage cursors and memory

The default cursor type is `PDO::CURSOR_FWDONLY`, a forward-only firehose. It streams rows to PHP one at a time and doesn't buffer, so a large result set is bounded by row-buffer memory instead of the total row count. That's usually what you want.

### Use buffered cursors only when you need to move backward or count rows

`PDO::SQLSRV_CURSOR_BUFFERED` (a client-side buffered static cursor) fetches the entire result set into PHP memory upfront. This approach lets you call `rowCount()`, seek backward, and reuse the statement. By default the buffer is capped at 10,240 KB (10 MB) via `PDO::SQLSRV_ATTR_CLIENT_BUFFER_MAX_KB_SIZE`, and a query whose result set exceeds the cap returns `false` instead of overflowing PHP memory. You can raise the cap toward the PHP memory limit, but doing so trades a `false` return for a real `Allowed memory size exhausted` fatal error when a query outgrows the new cap. Tune deliberately. See [Cursor types (PDO_SQLSRV)](cursor-types-pdo-sqlsrv-driver.md).

Server-side scrollable cursors (`PDO::SQLSRV_CURSOR_STATIC`, `PDO::SQLSRV_CURSOR_DYNAMIC`, `PDO::SQLSRV_CURSOR_KEYSET`) buffer on the server instead of the client, so they don't consume PHP memory. However, they hold server-side resources for the duration of the cursor and are slower per row than forward-only.

Use the default forward-only for streaming reads. Use buffered client-side for small result sets when you need `rowCount()` or backward scrolling. Avoid server-side scrollable cursors unless you're doing something specific.

```php
<?php
// Fast, low memory: default forward-only, one row at a time
$stmt = $conn->prepare("SELECT OrderID, Total FROM dbo.Orders");
$stmt->execute();
while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
    // ...
}

// Buffered: only when you need rowCount() or seeking
$stmt = $conn->prepare("SELECT * FROM dbo.SmallLookup", [
    PDO::ATTR_CURSOR                    => PDO::CURSOR_SCROLL,
    PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE => PDO::SQLSRV_CURSOR_BUFFERED,
]);
$stmt->execute();
$rowCount = $stmt->rowCount();
```

For a complete breakdown, see [Cursor types (PDO_SQLSRV)](cursor-types-pdo-sqlsrv-driver.md) and [Cursor types (SQLSRV)](cursor-types-sqlsrv-driver.md).

### Stream large binary and character values

For **varbinary(max)**, **varchar(max)**, **nvarchar(max)**, **xml**, and other large types, use PHP streams instead of materializing the whole value in memory:

```php
<?php
$stmt = $conn->prepare("SELECT Name, PhotoBlob FROM dbo.Products WHERE ProductID = ?");
$stmt->execute([$id]);
$stmt->bindColumn("PhotoBlob", $photo, PDO::PARAM_LOB);
$stmt->fetch(PDO::FETCH_BOUND);

// $photo is a stream resource; write it directly to disk without loading it all
$outFile = fopen("/tmp/photo.bin", "wb");
stream_copy_to_stream($photo, $outFile);
fclose($outFile);
```

For inserting or updating with large values, use `SendStreamParamsAtExec=false` in SQLSRV to send stream data in chunks after `sqlsrv_execute()`. For details, see [Send data as a stream](how-to-send-data-as-a-stream.md).

## Set appropriate timeouts

Timeouts are performance settings as much as reliability settings. Long-hanging queries hold pool connections and starve other requests.

### Statement timeout

Set a per-statement time limit so a runaway query doesn't hold a pool connection indefinitely. For PDO_SQLSRV:

```php
<?php
$stmt = $conn->prepare("SELECT ... FROM dbo.HugeTable ...");
$stmt->setAttribute(PDO::SQLSRV_ATTR_QUERY_TIMEOUT, 30); // seconds
$stmt->execute();
```

For SQLSRV, pass `"QueryTimeout" => 30` in the options array to [`sqlsrv_query`](sqlsrv-query.md) or [`sqlsrv_prepare`](sqlsrv-prepare.md).

Set a value that matches your workload. For a synchronous web request, 15 to 30 seconds is typical. For a background batch job, several minutes might be reasonable. Never set the timeout to zero (unlimited) in a web request.

### Login timeout

`LoginTimeout` in the connection string controls how long the driver waits to establish a connection. Set an explicit value when connecting to Azure SQL Database or Azure SQL Managed Instance so cold starts and failover-group failovers don't hang the client indefinitely. Values from 30 to 90 seconds work well for most cloud workloads. For details on sizing `LoginTimeout` against `ConnectRetryCount * ConnectRetryInterval` and the resulting failure modes, see [Connection timeout](troubleshooting-php-sql-driver.md#connection-timeout). For the option reference, see [Connection options](connection-options.md).

## Route read-only workloads to a replica

For read-only queries against a database in an [Always On availability group](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md), Azure SQL Managed Instance, or Azure SQL Database with a read scale-out or geo-replica, add `ApplicationIntent=ReadOnly` to your connection string:

```php
<?php
$dsn = "sqlsrv:Server=<listener>;Database=<database>;" .
       "Encrypt=true;ApplicationIntent=ReadOnly";
```

Read-only routing sends the connection to a synchronized secondary replica, offloading work from the primary. Combine with `MultiSubnetFailover=true` for the fastest connection to multi-subnet availability group listeners.

## Observe performance from the server

Client-side timing only tells you how long a query took end to end. To find out *why* it was slow, use SQL Server's built-in diagnostics.

### Query Store

Query Store captures execution plans, runtime statistics, and wait stats for every query on the database. It's enabled by default on Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric. On SQL Server, enable it per database:

```sql
ALTER DATABASE <database_name> SET QUERY_STORE = ON;
```

Then use SQL Server Management Studio's **Query Store** reports to find your slowest and most frequently executed queries. See [Monitoring performance with the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).

### Azure SQL Query Performance Insight

For Azure SQL Database, the Azure portal's Query Performance Insight automatically shows the top resource-consuming queries without any configuration. For more information, see [Query Performance Insight for Azure SQL Database](/azure/azure-sql/database/query-performance-insight-use).

### SET STATISTICS for one-time investigation

For a single query that you want to profile, run it in SQL Server Management Studio with statistics turned on:

```sql
SET STATISTICS TIME ON;
SET STATISTICS IO ON;

-- your query here
```

High logical reads almost always mean a missing or unusable index. High CPU time with low logical reads usually means a bad plan (parameter sniffing, an implicit conversion that prevents index use, or a scalar function that prevents parallelism).

### Extended Events for driver-level tracing

To see exactly what the driver sends to SQL Server (including the actual parameter values that it interpolates), capture an [Extended Events](../../relational-databases/extended-events/extended-events.md) session by using the `rpc_completed` and `sql_batch_completed` events.

## Performance checklist

Use this checklist as a pre-deployment review of any PHP application that connects to SQL Server:

| Area | Check | Reference |
| --- | --- | --- |
| Connection | Connection pooling is enabled and configured for the platform | [Manage connections efficiently](#manage-connections-efficiently) |
| Connection | The application reuses connections within a request and doesn't open connections per query | [Reuse the connection within a request](#reuse-the-connection-within-a-request) |
| Connection | `LoginTimeout` covers cold starts and failover for Azure SQL | [Login timeout](#login-timeout) |
| Query | Queries select only the columns needed, no `SELECT *` | [Select only the columns you use](#select-only-the-columns-you-use) |
| Query | Filtering happens in SQL, not in PHP with `array_filter` | [Fetch only the rows you need](#fetch-only-the-rows-you-need) |
| Query | Large result sets are paginated with `OFFSET ... FETCH` | [Paginate large result sets](#paginate-large-result-sets) |
| Query | Stored procedures set `SET NOCOUNT ON` | [Prefer `SET NOCOUNT ON`](#prefer-set-nocount-on-in-stored-procedures-and-batches) |
| Inserts | Bulk inserts use table-valued parameters, not per-row loops | [Insert data efficiently](#insert-data-efficiently) |
| Statements | `PDO::ATTR_EMULATE_PREPARES` is set to `false` | [Prefer native prepares](#prefer-native-prepares) |
| Statements | The application reuses prepared statements across executions | [Reuse prepared statements](#reuse-prepared-statements) |
| Cursors | The application uses the default forward-only cursor unless buffering is needed | [Manage cursors and memory](#manage-cursors-and-memory) |
| Memory | Large binary and character values are streamed, not materialized | [Stream large binary and character values](#stream-large-binary-and-character-values) |
| Timeouts | Statement timeout is set on all user-facing queries | [Statement timeout](#statement-timeout) |
| Routing | Read-only workloads set `ApplicationIntent=ReadOnly` where a replica exists | [Route read-only workloads](#route-read-only-workloads-to-a-replica) |
| Observability | Query Store is enabled and reviewed regularly | [Query Store](#query-store) |

## Related content

- [Connection Pooling (Microsoft Drivers for PHP for SQL Server)](connection-pooling-microsoft-drivers-for-php-for-sql-server.md)
- [Connection Options](connection-options.md)
- [Cursor Types (PDO_SQLSRV Driver)](cursor-types-pdo-sqlsrv-driver.md)
- [Cursor Types (SQLSRV Driver)](cursor-types-sqlsrv-driver.md)
- [Use table-valued parameters (PHP)](use-table-valued-parameters.md)
- [Troubleshoot the Microsoft Drivers for PHP for SQL Server](troubleshooting-php-sql-driver.md)
- [Monitor performance by using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
