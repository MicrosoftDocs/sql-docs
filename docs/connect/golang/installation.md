---
title: "Install the go-mssqldb Driver"
description: "Install the go-mssqldb driver for Go and verify your development environment for SQL Server connectivity."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/23/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Install the go-mssqldb driver

The `go-mssqldb` driver is a Go module that you install by using `go get`. This article covers system requirements, installation steps, and verification.

## Prerequisites

| Component | Requirement |
| --- | --- |
| Go | 1.23 or later. Download from [go.dev/dl](https://go.dev/dl/). |
| SQL Server | All supported versions of SQL Server, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric, Fabric Data Warehouse, or Azure Synapse Analytics. |
| Module support | Your project must use [Go modules](https://go.dev/ref/mod) (a `go.mod` file in the project root). |

## Install the driver module

1. Open a terminal in your Go project directory.

1. Run `go get` to download the driver and its dependencies:

   ```bash
   go get github.com/microsoft/go-mssqldb
   ```

1. The command automatically updates your `go.mod` and `go.sum` files.

## Install optional packages

The driver has optional packages for specific scenarios. Install them only when you need the corresponding feature.

### Microsoft Entra ID authentication

Install this package to use Microsoft Entra ID (formerly Azure AD) credential types:

```bash
go get github.com/microsoft/go-mssqldb/azuread
```

This package registers a driver named `azuresql` that supports all Microsoft Entra ID credential types. For more information, see [Microsoft Entra ID authentication](entra-authentication.md).

### Always Encrypted - Azure Key Vault provider

Install this package to use Azure Key Vault as the column master key store:

```bash
go get github.com/microsoft/go-mssqldb/aecmk/akv
```

This package registers the Azure Key Vault column master key provider. For more information, see [Always Encrypted](always-encrypted.md).

### Always Encrypted - local certificate provider

Install this package to use local certificates or PFX files as the column master key store:

```bash
go get github.com/microsoft/go-mssqldb/aecmk/localcert
```

This package registers the local certificate and PFX file column master key providers. For more information, see [Always Encrypted](always-encrypted.md).

## Verify the installation

Create a file named `verify.go` and paste the following code:

```go
package main

import (
    "database/sql"
    "fmt"
    "log"

    _ "github.com/microsoft/go-mssqldb"
)

func main() {
    connString := "sqlserver://<server>?database=AdventureWorks2025&trusted_connection=yes"
    db, err := sql.Open("sqlserver", connString)
    if err != nil {
        log.Fatal("Error creating connection: ", err.Error())
    }
    defer db.Close()

    err = db.Ping()
    if err != nil {
        log.Fatal("Error pinging database: ", err.Error())
    }

    fmt.Println("Connected successfully.")
}
```

Run the file:

```bash
go run verify.go
```

If the output shows `Connected successfully.`, the driver is installed and working.

> [!NOTE]
> The preceding example uses Windows integrated authentication (`trusted_connection=yes`), which works only on Windows. On Linux or macOS, use SQL authentication, NTLM, Kerberos, or Microsoft Entra ID instead. For examples, see [Authentication](authentication.md) and [Connection strings](connection-strings.md).

## Upgrade the driver

To upgrade to the latest version:

```bash
go get -u github.com/microsoft/go-mssqldb
```

To upgrade to a specific version:

```bash
go get github.com/microsoft/go-mssqldb@v1.10.0
```

After upgrading, run `go mod tidy` to remove unused indirect dependencies.

## Related content

- [Quickstart: Connect and query](quickstart.md)
- [Connection strings](connection-strings.md)
- [What's new](whats-new.md)
