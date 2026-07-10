---
title: "go-mssqldb Test Patterns"
description: "Integration testing patterns and test database setup for applications using the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Test patterns for go-mssqldb

This article covers patterns for writing integration tests against SQL Server when using the `go-mssqldb` driver.

## Choose the right test type

Prefer testing against a real SQL Server instance. A SQL Server container (via `testcontainers-go`, Docker Compose, or a CI service) catches SQL syntax errors, type mismatches, and transaction behavior that mocks can't detect. This approach is the same approach the `go-mssqldb` driver uses for its own test suite. On Windows, LocalDB is a lightweight alternative that doesn't require Docker.

Fall back to `go-sqlmock` only for fast inner-loop unit tests where container startup time would dominate execution. For example, use it for testing application-layer retry logic or result mapping.

| Test type | Use it for | Avoid it when |
| --- | --- | --- |
| Integration tests with `testcontainers-go` | Reproducible CI runs and suites that need a real SQL Server instance without managing shared infrastructure. | Fast inner-loop tests where container startup time would dominate execution. |
| Integration tests against a shared or local SQL Server | Stored procedures, schema objects, transaction behavior, temp tables, and end-to-end driver behavior. | Tests need isolated infrastructure or must run consistently in CI without external dependencies. |
| Unit tests with `go-sqlmock` | Application-layer logic like retry loops, result mapping, and error branching when you don't need to validate SQL syntax. | You need to verify driver behavior, SQL syntax against SQL Server, or transaction semantics. |

## Test database setup

Use environment variables to configure the test connection string for integration tests. This approach keeps credentials out of source code and makes CI/CD integration straightforward:

```go
package myapp_test

import (
    "database/sql"
    "os"
    "testing"

    _ "github.com/microsoft/go-mssqldb"
)

var testDB *sql.DB

func TestMain(m *testing.M) {
    connString := os.Getenv("TEST_MSSQL_URL")
    if connString == "" {
        panic("TEST_MSSQL_URL is not set")
    }

    var err error
    testDB, err = sql.Open("sqlserver", connString)
    if err != nil {
        panic("Failed to open test DB: " + err.Error())
    }
    defer testDB.Close()

    if err = testDB.Ping(); err != nil {
        panic("Failed to connect to test DB: " + err.Error())
    }

    os.Exit(m.Run())
}
```

Set the environment variable before running tests:

```bash
export TEST_MSSQL_URL="sqlserver://<user>:<password>@<server>:1433?database=<database>&encrypt=true&TrustServerCertificate=true"
go test ./...
```

## Use transactions for test isolation

Wrap each test in a transaction and roll back at the end. This approach keeps the database clean between tests:

```go
func TestInsertDepartment(t *testing.T) {
    tx, err := testDB.Begin()
    if err != nil {
        t.Fatal(err)
    }
    defer tx.Rollback() // Always roll back - never commits

    _, err = tx.Exec(
        "INSERT INTO HumanResources.Department (Name, GroupName) VALUES (@p1, @p2)",
        sql.Named("p1", "TestDept"),
        sql.Named("p2", "TestGroup"))
    if err != nil {
        t.Fatal(err)
    }

    var count int
    err = tx.QueryRow("SELECT COUNT(*) FROM HumanResources.Department WHERE Name = @p1",
        sql.Named("p1", "TestDept")).Scan(&count)
    if err != nil {
        t.Fatal(err)
    }

    if count != 1 {
        t.Errorf("Expected 1 row, got %d", count)
    }
}
```

This pattern works best for tests that exercise repository code inside a single transaction boundary. It isn't a good fit for code that opens and commits its own transactions internally, or for tests that need to validate behavior across multiple connections.

## SQL Server in Docker for CI/CD

Use a SQL Server Linux container for integration tests in CI pipelines:

```yaml
# GitHub Actions example
services:
  mssql:
    image: mcr.microsoft.com/mssql/server:2025-latest
    env:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "<password>"
    ports:
      - 1433:1433
```

Then set the test connection string:

```yaml
env:
    TEST_MSSQL_URL: "sqlserver://sa:<password>@localhost:1433?database=AdventureWorks2025"
```

## Skip tests when no database is available

For projects where a SQL Server instance might not always be available, skip integration tests gracefully:

```go
func TestQueryEmployees(t *testing.T) {
    if os.Getenv("TEST_MSSQL_URL") == "" {
        t.Skip("TEST_MSSQL_URL not set, skipping integration test")
    }
    // ... test body
}
```

## Test helper: create and drop tables

Create a helper function that sets up a test table and cleans it up after the test:

```go
func withTestTable(t *testing.T, db *sql.DB, fn func()) {
    t.Helper()

    _, err := db.Exec(`
        IF OBJECT_ID('dbo.TestItems', 'U') IS NOT NULL DROP TABLE dbo.TestItems;
        CREATE TABLE dbo.TestItems (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(50));
    `)
    if err != nil {
        t.Fatal("Setup failed:", err)
    }

    defer func() {
        db.Exec("DROP TABLE IF EXISTS dbo.TestItems")
    }()

    fn()
}
```

## Unit tests with go-sqlmock

If container startup time is too slow for your inner development loop, `go-sqlmock` creates an in-memory `*sql.DB` that returns predefined results. Use it for application-layer logic (retry loops, result mapping, error branching) where you don't need to validate SQL syntax against a real server:

```bash
go get github.com/DATA-DOG/go-sqlmock
```

### Mock a query

Set up expected queries and verify the application handles the results correctly:

> [!NOTE]
> `sqlmock.ExpectQuery` treats its input as a regular expression, not a plain SQL string. Characters such as `(`, `)`, `+`, and `.` must be escaped to match them literally in SQL text. In Go string literals, these escapes appear doubled (for example, `\\(` for a literal `(` in the regex).

```go
package myapp_test

import (
    "testing"
    "github.com/DATA-DOG/go-sqlmock"
)

func TestGetEmployee(t *testing.T) {
    db, mock, err := sqlmock.New()
    if err != nil {
        t.Fatal(err)
    }
    defer db.Close()

    rows := sqlmock.NewRows([]string{"BusinessEntityID", "Name", "Location"}).
        AddRow(1, "Alice", "Canada")

    mock.ExpectQuery("SELECT TOP \\(1\\) BusinessEntityID, FirstName \\+ ' ' \\+ LastName AS Name, CountryRegionName AS Location FROM Sales\\.vSalesPerson WHERE BusinessEntityID = @p1").
        WithArgs(1).
        WillReturnRows(rows)

    emp, err := GetEmployee(db, 1)
    if err != nil {
        t.Fatal(err)
    }
    if emp.Name != "Alice" {
        t.Errorf("Expected Alice, got %s", emp.Name)
    }

    if err := mock.ExpectationsWereMet(); err != nil {
        t.Errorf("Unmet expectations: %v", err)
    }
}
```

### Mock an error

Return an error from the mock to test error handling paths:

```go
func TestGetEmployeeNotFound(t *testing.T) {
    db, mock, err := sqlmock.New()
    if err != nil {
        t.Fatal(err)
    }
    defer db.Close()

    mock.ExpectQuery("SELECT").
        WithArgs(999).
        WillReturnError(sql.ErrNoRows)

    _, err = GetEmployee(db, 999)
    if err == nil {
        t.Error("Expected error for nonexistent employee")
    }

    if err := mock.ExpectationsWereMet(); err != nil {
        t.Errorf("Unmet expectations: %v", err)
    }
}
```

> [!TIP]
> Design your data access functions to accept `*sql.DB` (or an interface) as a parameter rather than using a package-level global. This pattern makes it straightforward to substitute `go-sqlmock` databases in tests.

## Integration tests with testcontainers-go

`testcontainers-go` spins up a SQL Server container per test suite and tears it down automatically. This approach is recommended for most test suites because it validates real SQL Server behavior without managing shared infrastructure:

```bash
go get github.com/testcontainers/testcontainers-go
go get github.com/testcontainers/testcontainers-go/modules/mssql
```

To run this example locally:

1. Make sure Docker Desktop or another local Docker engine is running.
1. Save the test in a `_test.go` file in your module.
1. Run `go test -run TestWithContainer -v ./...` from the module root.

Use this approach for most of your test suite. Container startup adds a few seconds, but you get real SQL Server validation that catches problems mocks miss.

```go
package myapp_test

import (
    "context"
    "database/sql"
    "testing"

    _ "github.com/microsoft/go-mssqldb"
    "github.com/testcontainers/testcontainers-go/modules/mssql"
)

func TestWithContainer(t *testing.T) {
    ctx := context.Background()

    container, err := mssql.Run(ctx,
        "mcr.microsoft.com/mssql/server:2025-latest",
        mssql.WithAcceptEULA(),
        mssql.WithPassword("<password>"))
    if err != nil {
        t.Fatal(err)
    }
    defer container.Terminate(ctx)

    connStr, err := container.ConnectionString(ctx)
    if err != nil {
        t.Fatal(err)
    }

    db, err := sql.Open("sqlserver", connStr)
    if err != nil {
        t.Fatal(err)
    }
    defer db.Close()

    // Create schema.
    _, err = db.ExecContext(ctx, `
        CREATE TABLE dbo.TestDepartments (
            Id INT IDENTITY PRIMARY KEY,
            Name NVARCHAR(50),
            GroupName NVARCHAR(50)
        )`)
    if err != nil {
        t.Fatal(err)
    }

    // Run tests against the real database.
    _, err = db.ExecContext(ctx,
        "INSERT INTO dbo.TestDepartments (Name, GroupName) VALUES (@p1, @p2)",
        sql.Named("p1", "Data Science"),
        sql.Named("p2", "Research and Development"))
    if err != nil {
        t.Fatal(err)
    }

    var count int
    err = db.QueryRowContext(ctx, "SELECT COUNT(*) FROM dbo.TestDepartments").Scan(&count)
    if err != nil {
        t.Fatal(err)
    }
    if count != 1 {
        t.Errorf("Expected 1 row, got %d", count)
    }
}
```

## Testcontainers: x509 certificate error

With Go 1.23 and later, you might see this error when connecting to a SQL Server container:

```output
x509: negative serial number
```

Go 1.23 enforces RFC 5280 strictly, and the self-signed certificate generated by SQL Server in Docker uses a negative serial number. Since test containers don't need production-level TLS, add `TrustServerCertificate=true` or `encrypt=disable` to the test connection string:

```go
connStr, err := container.ConnectionString(ctx, "TrustServerCertificate=true")
if err != nil {
    t.Fatal(err)
}
```

> [!CAUTION]
> Use `TrustServerCertificate=true` or `encrypt=disable` only in test environments. For production connections, use proper certificate validation. See [Encryption and certificates](encryption-certificates.md).

For more information, see [Troubleshooting](troubleshooting.md).

## Performance benchmarks with testing.B

Use Go's built-in benchmark framework to measure database operation performance:

```go
func BenchmarkInsert(b *testing.B) {
    connString := os.Getenv("TEST_MSSQL_URL")
    if connString == "" {
        b.Skip("TEST_MSSQL_URL not set")
    }

    db, err := sql.Open("sqlserver", connString)
    if err != nil {
        b.Fatalf("open database: %v", err)
    }
    defer db.Close()

    ctx := context.Background()
    db.ExecContext(ctx, `
        IF OBJECT_ID('dbo.BenchItems', 'U') IS NOT NULL DROP TABLE dbo.BenchItems;
        CREATE TABLE dbo.BenchItems (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(100))`)

    b.ResetTimer()
    for i := 0; i < b.N; i++ {
        db.ExecContext(ctx,
            "INSERT INTO dbo.BenchItems (Name) VALUES (@p1)",
            sql.Named("p1", fmt.Sprintf("item-%d", i)))
    }

    b.StopTimer()
    db.ExecContext(ctx, "DROP TABLE IF EXISTS dbo.BenchItems")
}
```

Run benchmarks:

```bash
go test -bench=BenchmarkInsert -benchmem -count=5
```

## Complete GitHub Actions workflow

This example shows a complete CI pipeline that sets up a SQL Server container, creates a test schema, and runs both unit and integration tests:

```yaml
name: Go SQL Server Tests
on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest

    services:
      mssql:
        image: mcr.microsoft.com/mssql/server:2025-latest
        env:
          ACCEPT_EULA: "Y"
          MSSQL_SA_PASSWORD: "<password>"
        ports:
          - 1433:1433
        options: >-
          --health-cmd "/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P '<password>' -C -Q 'SELECT 1'"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

    steps:
      - uses: actions/checkout@v4

      - uses: actions/setup-go@v5
        with:
          go-version: "1.22"

      - name: Create test schema
        run: |
          /opt/mssql-tools18/bin/sqlcmd \
            -S localhost -U sa -P "<password>" -C \
                        -Q "CREATE DATABASE AdventureWorks2025"

          /opt/mssql-tools18/bin/sqlcmd \
            -S localhost -U sa -P "<password>" -C \
                        -d AdventureWorks2025 \
            -i ./schema/setup.sql

      - name: Run unit tests
        run: go test -v -short ./...

      - name: Run integration tests
        env:
                    TEST_MSSQL_URL: "sqlserver://sa:<password>@localhost:1433?database=AdventureWorks2025"
        run: go test -v -race -count=1 ./...
```

## Test error paths and retry logic

Test that your application correctly handles transient errors and retries:

```go
func TestRetryOnTransientError(t *testing.T) {
    db, mock, err := sqlmock.New()
    if err != nil {
        t.Fatal(err)
    }
    defer db.Close()

    // First call fails with a transient error.
    mock.ExpectQuery("SELECT").WillReturnError(fmt.Errorf("mssql: timeout"))

    // Second call succeeds.
    rows := sqlmock.NewRows([]string{"Id"}).AddRow(1)
    mock.ExpectQuery("SELECT").WillReturnRows(rows)

    result, err := queryWithRetry(db, "SELECT ProductID FROM Production.Product WHERE ProductID = @p1", 1)
    if err != nil {
        t.Fatalf("Expected success after retry, got: %v", err)
    }
    if result != 1 {
        t.Errorf("Expected 1, got %d", result)
    }
}
```

## Test strategy comparison

| Strategy | Speed | Real DB | Dependencies | Best for |
| --- | --- | --- | --- | --- |
| `testcontainers-go` | Medium (seconds) | Yes | Docker | Most test suites (recommended). |
| Docker in CI | Medium (seconds) | Yes | Docker | CI/CD pipelines with GitHub Actions. |
| Transaction rollback | Fast (ms) | Yes | SQL Server | Integration tests on a shared database. |
| `go-sqlmock` | Fast (ms) | No | None | Inner-loop unit tests for app logic only. |
| `t.Skip` with env var | Instant | No | None | Graceful degradation when no DB is available. |

## Related content

- [Installation and system requirements](installation.md)
- [Connection strings](connection-strings.md)
- [Queries and statements](queries-statements.md)
- [Error handling and retry patterns](error-handling.md)
- [Troubleshooting](troubleshooting.md)
