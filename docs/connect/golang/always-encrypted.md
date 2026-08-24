---
title: "go-mssqldb Always Encrypted"
description: "Use Always Encrypted with the go-mssqldb driver and local certificate, Windows Certificate Store, or Azure Key Vault key providers."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Always Encrypted with go-mssqldb

The `go-mssqldb` driver supports [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md) for client-side encryption and decryption of sensitive data. When enabled, the driver automatically decrypts data from encrypted columns and encrypts parameter values sent to encrypted columns.

## Enable Always Encrypted

Set the `columnencryption` connection parameter to `true`:

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&columnencryption=true
```

You also need to import at least one column master key (CMK) provider package. Without a provider, the driver can't access the encryption keys.

## Column master key providers

The driver supports three key store providers. Import the provider package as a side-effect import to register it.

### Local certificate (PFX)

The `localcert` provider reads private keys from PFX (PKCS #12) files on the local file system. The provider name in the CMK metadata is `pfx`.

```go
import (
    _ "github.com/microsoft/go-mssqldb"
    _ "github.com/microsoft/go-mssqldb/aecmk/localcert"
)
```

When configuring the CMK in SQL Server, set the key path to the PFX file location:

```sql
CREATE COLUMN MASTER KEY MyCMK
WITH (
    KEY_STORE_PROVIDER_NAME = 'pfx',
    KEY_PATH = '/path/to/certificate.pfx'
);
```

If the PFX file is password-protected, set the password as an environment variable or through the `pfxpassword` connection parameter.

### Windows Certificate Store

The `MSSQL_CERTIFICATE_STORE` provider accesses certificates in the Windows Certificate Store. This provider works only on Windows.

```go
import (
    _ "github.com/microsoft/go-mssqldb"
    _ "github.com/microsoft/go-mssqldb/aecmk/localcert"
)
```

> [!NOTE]
> The `localcert` import also registers the Windows Certificate Store provider on Windows. No separate import is needed.

The CMK key path format is `CurrentUser/My/<thumbprint>` or `LocalMachine/My/<thumbprint>`:

```sql
CREATE COLUMN MASTER KEY MyCMK
WITH (
    KEY_STORE_PROVIDER_NAME = 'MSSQL_CERTIFICATE_STORE',
    KEY_PATH = 'CurrentUser/My/<CERTIFICATE_THUMBPRINT>'
);
```

### Azure Key Vault

The `akv` provider retrieves column master keys from Azure Key Vault.

```go
import (
    _ "github.com/microsoft/go-mssqldb"
    _ "github.com/microsoft/go-mssqldb/aecmk/akv"
)
```

The CMK key path is the Azure Key Vault key identifier URL:

```sql
CREATE COLUMN MASTER KEY MyCMK
WITH (
    KEY_STORE_PROVIDER_NAME = 'AZURE_KEY_VAULT',
    KEY_PATH = 'https://<VAULT_NAME>.vault.azure.net/keys/<KEY_NAME>/<KEY_VERSION>'
);
```

The Azure Key Vault provider uses `azidentity.DefaultAzureCredential` for authentication. Configure credentials through environment variables, managed identity, Azure CLI, or other methods supported by the Azure Identity library. For more information, see [Microsoft Entra ID authentication](entra-authentication.md).

## Query encrypted columns

With Always Encrypted enabled and a provider registered, queries work transparently:

```go
import (
    "context"
    "database/sql"
    "fmt"
    "log"

    _ "github.com/microsoft/go-mssqldb"
    _ "github.com/microsoft/go-mssqldb/aecmk/localcert"
)

func main() {
    db, err := sql.Open("sqlserver",
        "sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&columnencryption=true")
    if err != nil {
        log.Fatal(err)
    }
    defer db.Close()

    ctx := context.Background()

    // Reads automatically decrypt encrypted columns
    var ssn string
    err = db.QueryRowContext(ctx,
        "SELECT SSN FROM Patients WHERE Id = @p1",
        sql.Named("p1", 1)).Scan(&ssn)
    if err != nil {
        log.Fatal(err)
    }
    fmt.Println("SSN:", ssn)

    // Parameters are automatically encrypted for encrypted columns
    _, err = db.ExecContext(ctx,
        "INSERT INTO Patients (Name, SSN) VALUES (@p1, @p2)",
        sql.Named("p1", "Alice"),
        sql.Named("p2", "123-45-6789"))
    if err != nil {
        log.Fatal(err)
    }
}
```

## Match parameter types exactly

Always Encrypted parameter encryption is stricter than ordinary parameter binding. The driver asks SQL Server for encryption metadata before sending values, so the Go parameter type must match the SQL Server column type closely.

- A Go `string` parameter is sent as `nvarchar` by default.
- Use driver-specific types such as `mssql.NVarCharMax`, `mssql.DateTime1`, or `mssql.DateTimeOffset` when the encrypted column uses a more specific SQL Server type.
- If the parameter type doesn't match the encrypted column type, the query can fail during parameter-encryption metadata discovery with a type mismatch error.

For example, if an encrypted column is `nvarchar(max)`, prefer `mssql.NVarCharMax` when you need an exact large-string match:

```go
_, err := db.ExecContext(ctx,
    "INSERT INTO Patients (Notes) VALUES (@p1)",
    sql.Named("p1", mssql.NVarCharMax("Sensitive note text")))
```

For general parameter type guidance, see [Data type mappings](data-type-mappings.md).

## Limitations

- Always Encrypted doesn't work with bulk copy operations.
- The `azuresql` driver and Always Encrypted can be combined, but you must import both the `azuread` and the key provider packages.
- Encrypted `char` and `varchar` inserts and updates are currently limited. Prefer encrypted `nchar` or `nvarchar` columns for text data that must use Always Encrypted.
- Secure enclaves aren't supported.

## Related content

- [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [go-mssqldb connection options](connection-options.md)
- [Microsoft Entra ID authentication with go-mssqldb](entra-authentication.md)
