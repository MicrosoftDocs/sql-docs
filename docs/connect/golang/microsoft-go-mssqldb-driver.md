---
title: "Microsoft go-mssqldb Driver for SQL Server"
description: "The go-mssqldb driver is a pure Go database/sql driver for Microsoft SQL Server, Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/08/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Microsoft go-mssqldb driver for SQL Server

The `go-mssqldb` driver is the official Microsoft Go driver for SQL Server. It's a pure Go implementation of the TDS (Tabular Data Stream) protocol that uses the standard `database/sql` interface. It doesn't require ODBC or other C libraries.

The driver connects Go applications to all supported versions of SQL Server, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric, Fabric Data Warehouse, and Azure Synapse Analytics on Windows, Linux, and macOS.

## Choose your starting point

- To get a local SQL Server sample running quickly on Windows, start with [Quickstart: Connect and query](quickstart.md).
- To connect to Azure SQL with passwordless authentication, start with [Azure SQL Database](azure-sql.md) and [Microsoft Entra ID authentication](entra-authentication.md).
- To migrate from PostgreSQL or MySQL, start with [Migrate from other drivers](migration-guide.md).
- To move large volumes of data efficiently, go to [Bulk operations](bulk-operations.md) or [Table-valued parameters](table-valued-parameters.md).
- To troubleshoot TLS, certificate, or login issues, go to [Encryption and certificates](encryption-certificates.md) and [Troubleshooting](troubleshooting.md).

## Production baseline for Azure SQL

Use this sample as a starting point for a production-oriented Azure SQL connection. It combines managed identity, explicit TLS settings, bounded pool settings, request-scoped timeouts, jittered transient retry logic, and structured logging.

```go
package main

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
	"log/slog"
	"math/rand"
	"net/url"
	"os"
	"time"

	mssql "github.com/microsoft/go-mssqldb"
	_ "github.com/microsoft/go-mssqldb/azuread"
)

const (
	maxAttempts    = 4
	baseBackoff    = 200 * time.Millisecond
	maxBackoff     = 3 * time.Second
	queryTimeout   = 2 * time.Second
	startupTimeoutDefault = 30 * time.Second
)

func main() {
	baseLogger := slog.New(slog.NewJSONHandler(os.Stdout, &slog.HandlerOptions{
		Level: slog.LevelInfo,
	}))

	server, ok := getenvRequired("SQL_SERVER")
	if !ok {
		baseLogger.Error("invalid configuration", "error", "missing SQL_SERVER")
		os.Exit(1)
	}

	database, ok := getenvRequired("SQL_DATABASE")
	if !ok {
		baseLogger.Error("invalid configuration", "error", "missing SQL_DATABASE")
		os.Exit(1)
	}

	fedAuth := getenvOrDefault("SQL_FEDAUTH", "ActiveDirectoryManagedIdentity")
	appName := getenvOrDefault("SQL_APP_NAME", "go-mssqldb-example")
	startupTimeout := getenvDurationOrDefault("SQL_STARTUP_TIMEOUT", startupTimeoutDefault)
	logger := baseLogger.With(
		slog.String("server", server),
		slog.String("database", database),
		slog.String("fedauth", fedAuth),
		slog.String("appName", appName),
	)

	connString := buildConnString(server, database, appName, fedAuth)

	db, err := sql.Open("azuresql", connString)
	if err != nil {
		logger.Error("open database", "error", err)
		os.Exit(1)
	}
	defer db.Close()

	db.SetMaxOpenConns(20)
	db.SetMaxIdleConns(10)
	db.SetConnMaxIdleTime(2 * time.Minute)
	db.SetConnMaxLifetime(5 * time.Minute)

	rootCtx := context.Background()

	startupCtx, cancel := context.WithTimeout(rootCtx, startupTimeout)
	defer cancel()
	err = withRetry(startupCtx, logger, func(ctx context.Context) error {
		return db.PingContext(ctx)
	})
	if err != nil {
		logger.Error("ping database", "error", err)
		os.Exit(1)
	}

	var databaseName string
	err = withRetry(rootCtx, logger, func(ctx context.Context) error {
		queryCtx, cancel := context.WithTimeout(ctx, queryTimeout)
		defer cancel()

		return db.QueryRowContext(queryCtx, "SELECT DB_NAME()").Scan(&databaseName)
	})
	if err != nil {
		logger.Error("query database", "error", err)
		os.Exit(1)
	}

	logger.Info("database ready", "database", databaseName)
}

func withRetry(ctx context.Context, logger *slog.Logger, fn func(context.Context) error) error {
	var lastErr error

	for attempt := 1; attempt <= maxAttempts; attempt++ {
		lastErr = fn(ctx)
		if lastErr == nil {
			return nil
		}

		if !isTransient(lastErr) || attempt == maxAttempts {
			return lastErr
		}

		delay := backoffWithJitter(baseBackoff, maxBackoff, attempt)
		logger.WarnContext(ctx, "transient SQL error; retrying",
			"attempt", attempt,
			"delay", delay.String(),
			"error", lastErr,
		)

		timer := time.NewTimer(delay)
		select {
		case <-ctx.Done():
			timer.Stop()
			return ctx.Err()
		case <-timer.C:
		}
	}

	return lastErr
}

func backoffWithJitter(baseDelay, maxDelay time.Duration, attempt int) time.Duration {
	delay := baseDelay * time.Duration(1<<(attempt-1))
	if delay > maxDelay {
		delay = maxDelay
	}

	jitterFraction := 0.20
	multiplier := (1 - jitterFraction) + rand.Float64()*(2*jitterFraction)
	return time.Duration(float64(delay) * multiplier)
}

func buildConnString(server, database, appName, fedAuth string) string {
	query := url.Values{
		"database":               []string{database},
		"fedauth":                []string{fedAuth},
		"encrypt":                []string{"true"},
		"TrustServerCertificate": []string{"false"},
		"app name":               []string{appName},
		"log":                    []string{"1"},
	}

	return fmt.Sprintf("sqlserver://%s?%s", server, query.Encode())
}

func getenvRequired(key string) (string, bool) {
	value := os.Getenv(key)
	if value == "" {
		return "", false
	}
	return value, true
}

func getenvOrDefault(key, defaultValue string) string {
	value, ok := getenvRequired(key)
	if !ok {
		return defaultValue
	}
	return value
}

func getenvDurationOrDefault(key string, defaultValue time.Duration) time.Duration {
	value := os.Getenv(key)
	if value == "" {
		return defaultValue
	}

	parsed, err := time.ParseDuration(value)
	if err != nil {
		return defaultValue
	}

	return parsed
}

func isTransient(err error) bool {
	var sqlErr mssql.Error
	if !errors.As(err, &sqlErr) {
		return false
	}

	switch sqlErr.Number {
	case
		// Connection-establishment and transport transient errors.
		64, 233, 4060, 4221,
		10053, 10054,
		10928, 10929,

		// Azure SQL failover, throttling, and availability errors.
		40020, 40143, 40166,
		40197, 40501, 40540, 40613,
		42108, 42109,
		49918, 49919, 49920,

		// Common retryable statement-level contention errors.
		1205, 1222:
		return true
	default:
		return false
	}
}
```

This sample expects `SQL_SERVER` and `SQL_DATABASE` environment variables.

Optional environment variables:

- `SQL_FEDAUTH` (default: `ActiveDirectoryManagedIdentity`)
- `SQL_APP_NAME` (default: `go-mssqldb-example`)
- `SQL_STARTUP_TIMEOUT` (default: `30s`, parsed with `time.ParseDuration`)

For more information about each part of this sample, see [Azure SQL Database](azure-sql.md), [Connection pooling](connection-pooling.md), [Error handling and retry patterns](error-handling.md), and [Logging and diagnostics](logging-diagnostics.md).

## Key features

- **Pure Go**: No CGo or external C dependencies required.
- **Three connection string formats**: URL (`sqlserver://`), ADO (`key=value`), and ODBC (`odbc:key=value`).
- **Microsoft Entra ID authentication**: Multiple credential types via the `azuread` package, including managed identity, service principal, and workload identity.
- **SQL Server and Windows authentication**: Supports SQL auth, NTLM, Kerberos, and single sign-on (SSO) on Windows.
- **Always Encrypted**: Client-side encryption with local certificate, Windows Certificate Store, and Azure Key Vault key providers.
- **Bulk copy**: High-performance bulk insert operations.
- **Table-valued parameters (TVP)**: Pass structured data to stored procedures.
- **Multiple protocols**: TCP, named pipes, shared memory, and Dedicated Administrator Connection (DAC).
- **TDS 8.0 encryption**: End-to-end encryption with strict mode.

## Get started

| Article | Description |
| --- | --- |
| [Installation and system requirements](installation.md) | Install the driver module and verify your Go environment. |
| [Quickstart: Connect and query](quickstart.md) | Connect to a local or test SQL Server instance and run your first query in minutes. |
| [Migrate from other drivers](migration-guide.md) | Migrate from lib/pq, pgx, or go-sql-driver/mysql to go-mssqldb. |

## Configure connections

| Article | Description |
| --- | --- |
| [Connection strings](connection-strings.md) | URL, ADO, and ODBC connection string formats with examples. |
| [Connection options](connection-options.md) | Timeouts, packet size, failover, `SessionInitSQL`, and `NewConnector`. |
| [Encryption and certificates](encryption-certificates.md) | TLS encryption modes, certificate validation, and TDS 8.0. |
| [SQL Server and Windows authentication](authentication.md) | SQL auth, NTLM, Kerberos, and SSO configuration. |
| [Microsoft Entra ID authentication](entra-authentication.md) | Microsoft Entra ID connection options, credential flows, and token provider examples. |
| [Security best practices](security-best-practices.md) | SQL injection prevention, secrets management, encryption, and least privilege. |

## Work with data

| Article | Description |
| --- | --- |
| [Data type mappings](data-type-mappings.md) | Go-to-SQL type conversion table and driver-specific types. |
| [Queries and statements](queries-statements.md) | Parameterized queries, `Exec`, `Query`, `QueryRow`, and multiple result sets. |
| [Transactions](transactions.md) | Isolation levels, savepoints, deadlock detection, and retry patterns. |
| [Error handling and retry patterns](error-handling.md) | SQL Server error structure, transient error detection, and exponential backoff. |
| [Stored procedures](stored-procedures.md) | Output parameters, `ReturnStatus`, and result sets from procedures. |
| [Bulk operations](bulk-operations.md) | High-performance bulk insert with `CopyIn`. |
| [Table-valued parameters](table-valued-parameters.md) | Pass structured data to stored procedures with `mssql.TVP`. |
| [JSON and XML data](json-xml-data.md) | Query, insert, and transform JSON and XML data with `FOR JSON`, `OPENJSON`, and `FOR XML`. |
| [Always Encrypted](always-encrypted.md) | Client-side encryption with local certificate, Windows Certificate Store, and Azure Key Vault providers. |

## Deploy and operate

| Article | Description |
| --- | --- |
| [Connection pooling](connection-pooling.md) | Configure the `database/sql` connection pool. |
| [Concurrent programming](concurrent-programming.md) | Goroutine safety, worker pools, parallel queries, and graceful shutdown. |
| [Performance tuning](performance-tuning.md) | Pool tuning, packet size, prepared statements, and bulk copy. |
| [Azure SQL Database](azure-sql.md) | Passwordless authentication, connection limits, throttling, and failover handling. |
| [Troubleshooting](troubleshooting.md) | Common errors, logging configuration, and certificate diagnostics. |
| [Testing](testing.md) | Integration testing patterns and test database setup. |

## Platform and protocol guides

| Article | Description |
| --- | --- |
| [Linux and macOS](linux-macos.md) | Cross-platform setup, Kerberos, NTLM, and certificate paths. |
| [Protocols](protocols.md) | TCP, named pipes, shared memory, DAC, and SQL Browser. |
| [Logging and diagnostics](logging-diagnostics.md) | Log flags, `SetLogger`, and `SetContextLogger`. |

## Reference

| Article | Description |
| --- | --- |
| [Limitations](known-limitations.md) | Driver limitations, including `LastInsertId`, temporary tables, and Always Encrypted constraints. |
| [What's new](whats-new.md) | Version history and release highlights for the Microsoft fork of the driver. |
| [Support and lifecycle](support-lifecycle.md) | Go and SQL Server version compatibility matrix. |

## Related content

- [go-mssqldb on GitHub](https://github.com/microsoft/go-mssqldb)
- [go-mssqldb API reference on pkg.go.dev](https://pkg.go.dev/github.com/microsoft/go-mssqldb)
- [SQL connection libraries](../sql-connection-libraries.md)
- [Quickstart: Use Golang to query Azure SQL Database](/azure/azure-sql/database/connect-query-go)
