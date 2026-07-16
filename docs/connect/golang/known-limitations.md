---
title: "go-mssqldb Limitations"
description: "Known limitations, unsupported features, and constraints of the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---
# go-mssqldb Limitations

This article lists known limitations and constraints of the `go-mssqldb` driver.

## LastInsertId is not supported

The `go-mssqldb` driver doesn't support the `database/sql` `Result.LastInsertId()` method. Calling it returns an error:

```output
LastInsertId is not supported. Please use the OUTPUT clause or add
'select ID = convert(bigint, SCOPE_IDENTITY())' to the end of your query.
```

**Workaround**: Use an `OUTPUT` clause in the INSERT statement or query `SCOPE_IDENTITY()` separately.

## Multiple Active Result Sets (MARS) isn't supported

The driver doesn't support MARS. Each connection can have only one active query or statement at a time. If you need to run concurrent queries, use separate connections from the pool.

## Temporary table scope with connection pooling

Temporary table behavior follows SQL Server scoping rules, but connection pooling changes how that behavior appears in application code:

- Local temporary tables (`#name`) are scoped to one session (connection).
- Global temporary tables (`##name`) are visible to other sessions while the creating session remains open.

With `database/sql`, separate `db.Exec` and `db.Query` calls might use different pooled connections. A local temporary table created in one call isn't visible in the next call when that next call runs on a different connection.

For global temporary tables, visibility across connections works as expected, but lifecycle and naming collisions still need care in concurrent workloads.

**Guidance**: Use `db.Conn(ctx)` to pin related operations to one connection, or wrap operations in a transaction. Use global temporary tables only when cross-session visibility is required.

For implementation patterns, see [Temporary tables and stored procedures](stored-procedures.md#temporary-tables-and-stored-procedures), [Connection pooling](connection-pooling.md), and [Temporary table not found](troubleshooting.md#temporary-table-not-found).

## Always Encrypted constraints

- **Bulk copy with encrypted columns**: The driver doesn't support bulk copy operations on tables with Always Encrypted columns.
- **Secure enclaves**: The driver doesn't support Always Encrypted with secure enclaves.
- **Provider registration**: You must import the key provider package (`localcert`, `akv`) as a side-effect import. Without it, the driver can't decrypt data.
- **Exact parameter type matching**: Encrypted parameters must closely match the SQL Server column type. A default Go `string` becomes `nvarchar`, which can fail against more specific encrypted parameter types.
- **Encrypted `char` and `varchar` text**: Inserts and updates against encrypted `char` or `varchar` columns are currently limited. Prefer encrypted `nchar` or `nvarchar` columns when you need Always Encrypted for text data.

## Older SQL Server versions and TLS

Older, out-of-support SQL Server versions might not support TLS 1.2 or later. When connecting to these versions with `encrypt=true`, the connection might fail if the server can't negotiate a compatible TLS version.

**Workaround**: Use `encrypt=false` or upgrade to a supported SQL Server version.

## Deprecated mssql driver name

The driver name `mssql` (used with `sql.Open("mssql", ...)`) is deprecated. Use `sqlserver` instead:

```go
// Deprecated
db, err := sql.Open("mssql", connString)

// Recommended
db, err := sql.Open("sqlserver", connString)
```

The `mssql` driver name performs parameter token replacement, converting `?` placeholders to `@p1`, `@p2`, and similar ordinal names. The `sqlserver` driver name requires explicit named parameters and provides more predictable behavior.

If you need connector-based `sql.OpenDB` usage and must preserve the legacy query-text rewriting behavior temporarily, use `NewConnectorWithProcessQueryText`. For new code, prefer the `sqlserver` driver name and explicit parameters.

## uniqueidentifier columns

The driver returns `uniqueidentifier` column values as raw `[]byte` arrays by default. Use `mssql.UniqueIdentifier` as the scan destination to get standard GUID-formatted strings.

## No ODBC compatibility

The driver is a pure Go implementation and doesn't use or depend on unixODBC, FreeTDS, or the Microsoft ODBC Driver. ODBC-specific features (DSN configuration, ODBC tracing) aren't available.

## Named pipe connections

Named pipe connections might require specific file system permissions on the pipe endpoint. On Linux, the SMB client must be configured for named pipe access.

## Float precision

Go's `float64` type provides approximately 15-16 significant decimal digits. SQL Server's `decimal` and `numeric` types can represent up to 38 digits of precision. When scanning `decimal`/`numeric` columns into `float64`, precision loss can occur for values with more than 15 significant digits.

**Workaround**: Scan `decimal`/`numeric` columns into `string` and use a third-party decimal library such as `shopspring/decimal` (`github.com/shopspring/decimal`) or `cockroachdb/apd` (`github.com/cockroachdb/apd`) for precise arithmetic. For patterns, see [Data type mappings](data-type-mappings.md).

## Related content

- [Support and lifecycle](support-lifecycle.md)
- [What's new](whats-new.md)
- [Troubleshooting](troubleshooting.md)
