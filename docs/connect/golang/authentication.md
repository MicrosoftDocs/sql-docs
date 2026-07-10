---
title: "go-mssqldb SQL Server and Windows Authentication"
description: "Configure SQL Server authentication, Windows integrated authentication, NTLM, and Kerberos with the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# SQL Server and Windows authentication with go-mssqldb

The `go-mssqldb` driver supports SQL Server authentication, Windows integrated authentication (SSPI), NTLM, and Kerberos. This article covers each method with connection string examples and code samples.

For Microsoft Entra ID authentication, see [Microsoft Entra ID authentication](entra-authentication.md).

## Choose an authentication method

Use the strongest method that fits your environment and identity platform:

| Prefer this order | Method | Use it when |
| --- | --- | --- |
| 1 | Microsoft Entra ID | You're connecting to Azure SQL and can use managed identity, workload identity, or another Entra flow without storing passwords. |
| 2 | Windows integrated authentication (SSPI) | Your app runs on Windows and can use the current Windows identity directly. |
| 3 | Kerberos | You're on Linux or macOS and already have Kerberos tickets, keytabs, or a managed domain environment. |
| 4 | NTLM | You need domain authentication but Kerberos isn't available or practical. |
| 5 | SQL Server authentication | You don't have an integrated identity option and must use a SQL login and password. |

If more than one method works, prefer the one that avoids long-lived secrets and matches the host platform's native identity system.

## SQL Server authentication

SQL Server authentication uses a login name and password stored in SQL Server. Provide the `user id` and `password` parameters:

### URL format

> [!NOTE]
> For URL connection strings, percent-encode special characters in the username or password. Windows authentication usernames use `%5C` for the domain separator, such as `sqlserver://DOMAIN%5Cusername:password@host`. In ADO and ODBC connection strings, use the literal `DOMAIN\username` form.

Specify the user name and password in the URL:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true
```

### ADO format

Use the `user id` and `password` keys in an ADO-style connection string:

```text
server=<server>;user id=<user>;password=<password>;database=AdventureWorks2025;Encrypt=true
```

### Code example

Connect with SQL Server authentication and verify the connection:

```go
package main

import (
    "database/sql"
    "log"

    _ "github.com/microsoft/go-mssqldb"
)

func main() {
    db, err := sql.Open("sqlserver",
        "sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true")
    if err != nil {
        log.Fatal(err)
    }
    defer db.Close()

    if err = db.Ping(); err != nil {
        log.Fatal(err)
    }
}
```

## Windows integrated authentication (SSPI)

On Windows, the driver uses SSPI (Security Support Provider Interface) to authenticate by using the current Windows user's credentials. The connection string doesn't need a user name or password.

```text
sqlserver://<server>?database=AdventureWorks2025&trusted_connection=yes&encrypt=true
```

> [!NOTE]
> Windows integrated authentication through SSPI works only when the Go application runs on Windows and the current user has access to the SQL Server instance.

## NTLM authentication

NTLM authentication works on all platforms (Windows, Linux, and macOS). Provide a domain-qualified user name and password:

### NTLM URL format

URL-encode the backslash in `DOMAIN\<user>` as `%5C`:

```text
sqlserver://DOMAIN%5C<user>:<password>@<server>?database=AdventureWorks2025&encrypt=true
```

> [!NOTE]
> In URL format, you must URL-encode the backslash in `DOMAIN\<user>` as `%5C`.

### NTLM ADO format

The backslash in the user name signals NTLM authentication to the driver:

```text
server=<server>;user id=DOMAIN\<user>;password=<password>;database=AdventureWorks2025;Encrypt=true
```

The driver detects the backslash in the user name and uses NTLM automatically.

## Kerberos authentication

Kerberos authentication works on Linux and macOS when the system has a valid Kerberos configuration. The driver uses the `krb5` package for Kerberos authentication. There are three methods to provide credentials.

### Prerequisites

- A valid `krb5.conf` file (default path: `/etc/krb5.conf`).
- The SQL Server must have a registered Service Principal Name (SPN).

### Method 1: Keytab file

A keytab file contains encrypted Kerberos principal keys. Specify the keytab file path and the user's realm:

```text
sqlserver://<user>@MYREALM@<server>:1433?database=AdventureWorks2025&krb5-realm=MYREALM&krb5-keytabfile=/path/to/user.keytab&encrypt=true
```

### Method 2: Credential cache

Use an existing Kerberos credential cache created by `kinit`.

```text
sqlserver://<user>@MYREALM@<server>:1433?database=AdventureWorks2025&krb5-realm=MYREALM&krb5-credcachefile=/tmp/krb5cc_1000&encrypt=true
```

### Method 3: Raw credentials

Provide the password directly in the connection string. The driver uses the provided credentials to perform the Kerberos authentication exchange.

```text
sqlserver://<user>@MYREALM:<password>@<server>:1433?database=AdventureWorks2025&krb5-realm=MYREALM&encrypt=true
```

### Kerberos parameters

| Parameter | Default | Description |
| --- | --- | --- |
| `krb5-configfile` | `/etc/krb5.conf` | Path to the Kerberos configuration file. |
| `krb5-realm` | - | Kerberos realm name, such as `MYDOMAIN.COM`. |
| `krb5-keytabfile` | - | Path to the keytab file. |
| `krb5-credcachefile` | - | Path to the credential cache file. |
| `krb5-dnslookupkdc` | `true` | Use DNS SRV records to locate the KDC. |
| `krb5-udppreferencelimit` | `1` | Maximum message size before switching from UDP to TCP. |
| `ServerSPN` | - | Override the automatically generated SPN. |

## Related content

- [Microsoft Entra ID authentication](entra-authentication.md)
- [Connection strings](connection-strings.md)
- [Encryption and certificates](encryption-certificates.md)
- [Linux and macOS](linux-macos.md)
