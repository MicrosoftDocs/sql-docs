---
title: "go-mssqldb Bulk Operations"
description: "Perform high-performance bulk insert operations with the go-mssqldb driver using the CopyIn function."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Bulk operations with go-mssqldb

The `go-mssqldb` driver supports high-performance bulk insert operations by using the `mssql.CopyIn` function. Bulk insert bypasses the normal row-by-row `INSERT` path and streams data directly to the server by using the TDS bulk copy protocol.

## Choose bulk copy, TVP, or JSON

Use the following guide when you need to send multiple rows or complex payloads to SQL Server:

| Choose... | When it fits best | Tradeoff |
| --- | --- | --- |
| Bulk copy with `mssql.CopyIn` | You need the fastest way to load many rows into one target table. | Best throughput, but it targets table loads rather than stored procedure contracts or mixed-shape payloads. |
| A [table-valued parameter](table-valued-parameters.md) | You need to pass a strongly typed set of rows into a stored procedure or parameterized command. | Preserves schema and procedure boundaries, but requires a user-defined table type and matching field order. |
| JSON with `OPENJSON` or `FOR JSON` | Your application already exchanges JSON, or the payload shape is nested or flexible. | More portable for app code, but usually slower and less type-safe than TVPs or bulk copy for structured inserts. |

If you're loading large batches into a staging or destination table, start with bulk copy. If you're calling stored procedures with structured rowsets, start with TVPs. If you need nested documents or loose schemas, start with JSON.

Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database. Bulk copy examples target `HumanResources.Department` and `Production.ProductCategory`.

## Basic bulk insert

Use `mssql.CopyIn` to create a bulk copy statement, and then use `Exec` to send rows:

```go
import (
    "database/sql"
    "log"

    "github.com/microsoft/go-mssqldb"
)

func bulkInsert(db *sql.DB) error {
    txn, err := db.Begin()
    if err != nil {
        return err
    }
    defer txn.Rollback()

    stmt, err := txn.Prepare(mssql.CopyIn("HumanResources.Department", mssql.BulkOptions{},
        "Name", "GroupName"))
    if err != nil {
        return err
    }

    // Add rows
    _, err = stmt.Exec("Data Science", "Research and Development")
    if err != nil {
        return err
    }
    _, err = stmt.Exec("Cloud Ops", "Information Technology")
    if err != nil {
        return err
    }
    _, err = stmt.Exec("Developer Relations", "Sales and Marketing")
    if err != nil {
        return err
    }

    // Flush and finalize the bulk copy
    result, err := stmt.Exec()
    if err != nil {
        return err
    }

    if err = stmt.Close(); err != nil {
        return err
    }

    rowsAffected, _ := result.RowsAffected()
    log.Printf("Bulk inserted %d rows\n", rowsAffected)

    return txn.Commit()
}
```

The final `stmt.Exec()` call with no arguments flushes any remaining rows and completes the bulk copy operation.

## BulkOptions

The `mssql.BulkOptions` struct configures bulk copy behavior:

| Field | Type | Description |
| --- | --- | --- |
| `CheckConstraints` | `bool` | Check constraints during the bulk insert. |
| `FireTriggers` | `bool` | Fire `INSERT` triggers on the target table. |
| `KeepNulls` | `bool` | Preserve null values instead of inserting default values. |
| `KilobytesPerBatch` | `int` | Kilobytes per batch. `0` uses the server default. |
| `RowsPerBatch` | `int` | Rows per batch. `0` uses the server default. |
| `Order` | `[]string` | `ORDER` hint for the target clustered index (for example, `[]string{"Id ASC"}`). |
| `Tablock` | `bool` | Acquire a table-level lock for the duration of the bulk copy. |

### Example with options

Pass `BulkOptions` to control constraint checks, triggers, and locking:

```go
stmt, err := txn.Prepare(mssql.CopyIn("HumanResources.Department",
    mssql.BulkOptions{
        CheckConstraints: true,
        FireTriggers:     true,
        Tablock:          true,
        RowsPerBatch:     1000,
    },
    "Name", "GroupName"))
```

## Error handling

If any row fails, the entire bulk copy operation fails. Check errors from both each row's `Exec` call and the final flush `Exec`:

```go
for _, emp := range employees {
    _, err = stmt.Exec(emp.Name, emp.GroupName)
    if err != nil {
        txn.Rollback()
        return err
    }
}

// Final flush
_, err = stmt.Exec()
if err != nil {
    txn.Rollback()
    return err
}
```

### Detect and log failed rows

When a bulk copy operation fails, the error message from SQL Server indicates the constraint or data issue but doesn't identify the specific row. To identify failed rows, use a batching approach:

```go
func bulkInsertWithRowTracking(db *sql.DB, departments []Department) error {
    txn, err := db.Begin()
    if err != nil {
        return err
    }

    stmt, err := txn.Prepare(mssql.CopyIn("HumanResources.Department",
        mssql.BulkOptions{RowsPerBatch: 500}, "Name", "GroupName"))
    if err != nil {
        txn.Rollback()
        return err
    }

    for i, dept := range departments {
        _, err = stmt.Exec(dept.Name, dept.GroupName)
        if err != nil {
            txn.Rollback()
            log.Printf("Bulk copy failed at row %d (Name=%q): %v", i, dept.Name, err)
            return fmt.Errorf("bulk copy failed at row %d: %w", i, err)
        }
    }

    _, err = stmt.Exec()
    if err != nil {
        txn.Rollback()
        return fmt.Errorf("bulk copy flush failed: %w", err)
    }

    if err = stmt.Close(); err != nil {
        txn.Rollback()
        return err
    }

    return txn.Commit()
}
```

> [!TIP]
> If you need to skip bad rows and continue, use individual `INSERT` statements or a staging table pattern: bulk copy into a staging table without constraints, then use a `MERGE` or `INSERT...SELECT` with error handling to move data to the target table.

## Stream from CSV files

For large CSV files, stream rows directly from the file into bulk copy without loading the entire file into memory:

```go
import (
    "encoding/csv"
    "io"
    "os"
)

func bulkInsertFromCSV(db *sql.DB, filePath string) error {
    f, err := os.Open(filePath)
    if err != nil {
        return err
    }
    defer f.Close()

    reader := csv.NewReader(f)

    // Skip the header row.
    _, err = reader.Read()
    if err != nil {
        return err
    }

    txn, err := db.Begin()
    if err != nil {
        return err
    }

    stmt, err := txn.Prepare(mssql.CopyIn("HumanResources.Department",
        mssql.BulkOptions{Tablock: true, RowsPerBatch: 5000},
        "Name", "GroupName"))
    if err != nil {
        txn.Rollback()
        return err
    }

    var rowCount int
    for {
        record, err := reader.Read()
        if err == io.EOF {
            break
        }
        if err != nil {
            txn.Rollback()
            return fmt.Errorf("CSV read error at row %d: %w", rowCount+1, err)
        }

        _, err = stmt.Exec(record[0], record[1])
        if err != nil {
            txn.Rollback()
            return fmt.Errorf("row %d: %w", rowCount+1, err)
        }
        rowCount++
    }

    // Flush remaining rows.
    result, err := stmt.Exec()
    if err != nil {
        txn.Rollback()
        return err
    }
    if err = stmt.Close(); err != nil {
        txn.Rollback()
        return err
    }

    affected, _ := result.RowsAffected()
    log.Printf("Bulk inserted %d rows from CSV", affected)

    return txn.Commit()
}
```

## Performance comparison

Bulk copy is significantly faster than individual inserts for large data loads. The following table shows approximate performance characteristics for inserting 100,000 rows:

| Method | Relative speed | Network round trips | Locking |
| --- | --- | --- | --- |
| Individual `INSERT` | Slowest (1x) | 100,000 | Row-level per insert. |
| Batched `INSERT` (1,000 rows per statement) | Medium (5-10x) | 100 | Row-level per batch. |
| Bulk copy without `TABLOCK` | Fast (20-50x) | Depends on batch size | Row-level bulk. |
| Bulk copy with `TABLOCK` | Fastest (50-100x) | Depends on batch size | Table-level lock, minimal logging. |

> [!NOTE]
> Actual performance varies based on network latency, server configuration, table indexes, and whether minimal logging is available. Benchmark with your specific workload using `testing.B`. See [Performance tuning](performance-tuning.md).

## Column ordering and type mapping

Columns in `mssql.CopyIn` must match the order and types expected by the target table. The driver doesn't perform column name matching; it uses positional mapping.

### Common type issues

| Go type | SQL Server column | Issue | Solution |
| --- | --- | --- | --- |
| `string` | `varchar` | Implicit `nvarchar` conversion. | Use `mssql.VarChar` wrapper. |
| `float64` | `decimal(18,4)` | Precision loss. | Pass as `string`. |
| `time.Time` | `datetime2` | Time zone conversion. | Use UTC times. |
| `nil` | Any nullable column | Requires `KeepNulls: true`. | Set `KeepNulls` in `BulkOptions`. |

### Example with explicit types

Specify column types explicitly when the default type mapping doesn't match your schema:

```go
stmt, err := txn.Prepare(mssql.CopyIn("Production.ProductCategory",
    mssql.BulkOptions{KeepNulls: true},
    "Name"))
if err != nil {
    return err
}

for _, p := range categories {
    _, err = stmt.Exec(p.Name)
    if err != nil {
        return err
    }
}
```

## Performance tips

- **Use `Tablock`** for large inserts into empty tables. This option reduces lock contention and enables minimal logging.
- **Set `RowsPerBatch`** to control how often the driver sends data. Larger batches reduce round trips but use more memory.
- **Increase `packet size`** in the connection string (up to 32767) to reduce network overhead.
- **Order the data** to match the target table's clustered index and set the `Order` option. This approach avoids a server-side sort.
- **Drop nonclustered indexes** before large bulk loads, then rebuild them after. Index maintenance during bulk insert adds overhead.
- **Use `CheckConstraints: false`** (the default) for trusted data to skip constraint checking during the bulk copy.

## Limitations

- Bulk copy doesn't support columns protected by Always Encrypted. For more information, see [Limitations](known-limitations.md).
- TDS-level bulk copy isn't supported on Azure SQL Database. For Azure SQL Database, use batched `INSERT` statements or a staging-table pattern instead.

## Related content

- [Performance tuning with go-mssqldb](performance-tuning.md)
- [Queries and statements with go-mssqldb](queries-statements.md)
- [go-mssqldb data type mappings](data-type-mappings.md)
- [Migrate to go-mssqldb from other drivers](migration-guide.md)
- [Error handling and retry patterns with go-mssqldb](error-handling.md)
