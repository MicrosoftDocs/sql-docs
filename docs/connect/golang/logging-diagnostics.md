---
title: "go-mssqldb Logging and Diagnostics"
description: "Configure logging, set log flags, and use custom loggers with the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Logging and diagnostics with go-mssqldb

The `go-mssqldb` driver provides configurable logging for troubleshooting connection issues, query problems, and performance analysis. This article describes the available log flags and how to use custom loggers.

## Log flags

Use the `log` connection parameter to enable diagnostic output. Log flags are bitmask values, meaning you can combine them by adding their integer values:

| Flag value | Category | Description |
| --- | --- | --- |
| `1` | Errors | Log error messages. |
| `2` | Messages | Log informational messages from the server. |
| `4` | Rows | Log row data. |
| `8` | SQL | Log SQL statements sent to the server. |
| `16` | Parameters | Log parameter names and values. |
| `32` | Transactions | Log transaction start, commit, and rollback events. |
| `64` | Debug | Log low-level protocol and TDS details. |
| `128` | Retries | Log connection retry attempts. |

Flags `4` (rows) and `16` (parameters) can expose application data, secrets, or personally identifiable information. Treat them as short-lived diagnostic flags, not as routine production settings.

### Examples

Log errors only:

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&log=1
```

Log errors, SQL statements, and parameters:

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&log=25
```

> [!NOTE]
> The value `25` is calculated as `1 + 8 + 16` (errors + SQL + parameters).

Log everything:

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&log=255
```

> [!WARNING]
> High log flag values (`64`, `128`, `255`) produce verbose output and can affect performance. Use them only for debugging.

## Default logger

By default, the driver logs to Go's standard `log` package, which writes to `os.Stderr`. The output includes timestamps and the log category:

```output
2026/03/28 10:15:30 mssql: login successful
2026/03/28 10:15:30 mssql: SQL: SELECT 1
```

## Custom logger with SetLogger

Use `mssql.SetLogger` to redirect driver log output to a custom logger. The logger must implement the `mssql.Logger` interface:

```go
import "github.com/microsoft/go-mssqldb"

type myLogger struct{}

func (l *myLogger) Printf(format string, v ...interface{}) {
    // Write to your preferred logging system
    fmt.Printf("[MSSQL] "+format+"\n", v...)
}

func (l *myLogger) Println(v ...interface{}) {
    fmt.Println(append([]interface{}{"[MSSQL]"}, v...)...)
}

func main() {
    mssql.SetLogger(&myLogger{})
    // ... open connection
}
```

## Context-aware logger with SetContextLogger

Use `mssql.SetContextLogger` to provide a context-aware logger. The logger must implement the `mssql.ContextLogger` interface, which has a single `Log` method that receives the context, a log category, and a message string. This approach allows you to correlate driver logs with request-scoped tracing data (for example, trace IDs):

```go
import (
    "context"
    "log/slog"
    "github.com/microsoft/go-mssqldb"
    "github.com/microsoft/go-mssqldb/msdsn"
)

type contextLogger struct{}

func (l *contextLogger) Log(ctx context.Context, category msdsn.Log, msg string) {
    slog.InfoContext(ctx, msg, "category", category)
}

func main() {
    mssql.SetContextLogger(&contextLogger{})
    // ... open connection
}
```

## Diagnostic checklist

When troubleshooting a connection or query issue:

1. Start with `log=1` for errors, or `log=3` if you also need server messages.
1. Reproduce the issue and review the error text before enabling more categories.
1. Add `8` if you need to confirm which SQL statement or procedure call was sent.
1. Add `16` or `4` only in a safe environment where parameter values and returned rows can be logged without exposing sensitive data.
1. If the issue looks protocol-level or retry-related, increase to `log=64` or add `128` for retry diagnostics.
1. Remove or reduce logging after the issue is resolved.

## Structured logging with log/slog

Go 1.21 introduced `log/slog` for structured logging. Use `SetContextLogger` to route driver output through `slog`:

```go
import (
    "context"
    "log/slog"
    "os"

    "github.com/microsoft/go-mssqldb"
    "github.com/microsoft/go-mssqldb/msdsn"
)

type slogLogger struct {
    logger *slog.Logger
}

func (l *slogLogger) Log(ctx context.Context, category msdsn.Log, msg string) {
    level := slog.LevelInfo
    if category == msdsn.LogErrors {
        level = slog.LevelError
    }
    if category == msdsn.LogDebug {
        level = slog.LevelDebug
    }

    l.logger.LogAttrs(ctx, level, msg,
        slog.Int("category", int(category)),
    )
}

func main() {
    logger := slog.New(slog.NewJSONHandler(os.Stdout, &slog.HandlerOptions{
        Level: slog.LevelInfo,
    }))

    mssql.SetContextLogger(&slogLogger{logger: logger})

    // Connection logging now produces structured JSON.
}
```

## Integration with zerolog

**[zerolog](https://github.com/rs/zerolog)** is a zero-allocation structured logger. Route driver logs through zerolog:

```go
import (
    "context"
    "os"

    "github.com/microsoft/go-mssqldb"
    "github.com/microsoft/go-mssqldb/msdsn"
    "github.com/rs/zerolog"
)

type zerologAdapter struct {
    logger zerolog.Logger
}

func (l *zerologAdapter) Log(ctx context.Context, category msdsn.Log, msg string) {
    event := l.logger.Info()
    if category == msdsn.LogErrors {
        event = l.logger.Error()
    }
    event.Int("category", int(category)).Msg(msg)
}

func main() {
    logger := zerolog.New(os.Stdout).With().Timestamp().Logger()
    mssql.SetContextLogger(&zerologAdapter{logger: logger})
}
```

## Integration with zap

[zap](https://github.com/uber-go/zap) is a high-performance structured logger. Route driver logs through zap:

```go
import (
    "context"

    "github.com/microsoft/go-mssqldb"
    "github.com/microsoft/go-mssqldb/msdsn"
    "go.uber.org/zap"
)

type zapAdapter struct {
    logger *zap.Logger
}

func (l *zapAdapter) Log(ctx context.Context, category msdsn.Log, msg string) {
    if category == msdsn.LogErrors {
        l.logger.Error(msg, zap.Int("category", int(category)))
    } else {
        l.logger.Info(msg, zap.Int("category", int(category)))
    }
}

func main() {
    logger, _ := zap.NewProduction()
    defer logger.Sync()

    mssql.SetContextLogger(&zapAdapter{logger: logger})
}
```

## Correlation ID propagation

In distributed systems, propagate a correlation ID through the context so that driver logs can be correlated with the request that triggered them:

```go
type correlationKey struct{}

func WithCorrelationID(ctx context.Context, id string) context.Context {
    return context.WithValue(ctx, correlationKey{}, id)
}

func CorrelationID(ctx context.Context) string {
    if id, ok := ctx.Value(correlationKey{}).(string); ok {
        return id
    }
    return "unknown"
}

type correlatedLogger struct {
    logger *slog.Logger
}

func (l *correlatedLogger) Log(ctx context.Context, category msdsn.Log, msg string) {
    l.logger.LogAttrs(ctx, slog.LevelInfo, msg,
        slog.String("correlation_id", CorrelationID(ctx)),
        slog.Int("category", int(category)),
    )
}
```

Then pass the correlation ID through your request context:

```go
func handleRequest(w http.ResponseWriter, r *http.Request) {
    correlationID := r.Header.Get("X-Correlation-ID")
    if correlationID == "" {
        correlationID = uuid.NewString()
    }

    ctx := WithCorrelationID(r.Context(), correlationID)

    // All database operations using this context will include the correlation ID.
    rows, err := db.QueryContext(ctx, "SELECT TOP (10) ProductID, Name FROM Production.Product")
    // ...
}
```

### How to use correlation IDs in practice

Use correlation IDs as a tracing key across logs, retries, and database calls:

1. Generate or accept a correlation ID at the request boundary.
1. Store it in the request context and include it in application and driver logs.
1. For SQL-side troubleshooting, set it in session context with `sp_set_session_context` and read it with `SESSION_CONTEXT`.

Correlation IDs aren't automatically stored in SQL Server tables. `SESSION_CONTEXT` is session-scoped, so values apply only to the current connection and don't persist after the session ends.

If you need durable history, write the correlation ID explicitly to your audit or business tables (for example, an `AuditLog` table with `correlation_id`, timestamp, operation, and status).

## Production logging configuration

In production, enable minimal logging to avoid performance overhead and prevent sensitive data from appearing in logs:

| Environment | Recommended `log` value | What it captures |
| --- | --- | --- |
| Development | `63` (errors + messages + rows + SQL + params + transactions) | Full visibility for debugging. |
| Staging | `3` (errors + messages) | Errors and server messages without query details. |
| Production | `1` (errors) or `0` (off) | Only errors, or disable driver logging entirely. |

> [!CAUTION]
> The `Parameters` flag (`16`) logs actual parameter values, which can include personally identifiable information (PII), passwords, or other sensitive data. Never enable this flag in production. For a complete risk assessment of each flag, see [Security best practices](security-best-practices.md).

### Suppress logging in production

Use environment variables to control the log level per deployment stage:

```go
if os.Getenv("APP_ENV") == "production" {
    // Use only error-level logging in production.
    connString += "&log=1"
} else {
    // Full logging in development.
    connString += "&log=63"
}
```

## Related content

- [Troubleshooting](troubleshooting.md)
- [Security best practices](security-best-practices.md)
- [Connection options](connection-options.md)
- [Connection strings](connection-strings.md)
- [Performance tuning](performance-tuning.md)
