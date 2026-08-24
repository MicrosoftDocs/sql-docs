---
title: "go-mssqldb on Linux and macOS"
description: "Cross-platform setup, Kerberos authentication, NTLM, and certificate configuration for the go-mssqldb driver on Linux and macOS."
author: dlevy-msft
ms.author: dlevy
ms.date: 03/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# go-mssqldb on Linux and macOS

The `go-mssqldb` driver is a pure Go library and compiles natively on Linux and macOS without external C dependencies. This article covers platform-specific configuration for authentication and certificates.

## Installation

Installation is the same on all platforms:

```bash
go get github.com/microsoft/go-mssqldb
```

No ODBC driver, FreeTDS, or other C libraries are needed.

## SQL authentication

SQL authentication works identically on Linux, macOS, and Windows:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025
```

## NTLM authentication

NTLM authentication is supported on all platforms. Provide a domain-qualified user name:

### URL format

URL-encode the backslash in `DOMAIN\user` as `%5C`:

```text
sqlserver://CONTOSO%5C<user>:<password>@<server>:1433?database=mydb
```

### ADO format

The backslash in the user name triggers NTLM authentication:

```text
server=<server>;user id=DOMAIN\<user>;password=<password>;database=AdventureWorks2025
```

The driver detects the backslash in the user name and uses NTLM automatically. No additional configuration is required on Linux or macOS.

## Kerberos authentication

Kerberos authentication on Linux and macOS uses the `krb5` package built into the driver. There are no external library dependencies.

### Prerequisites

1. A valid Kerberos configuration file. The default path is `/etc/krb5.conf`:

   ```ini
   [libdefaults]
       default_realm = MYDOMAIN.COM
       dns_lookup_kdc = true

   [realms]
       MYDOMAIN.COM = {
           kdc = dc1.mydomain.com
       }
   ```

1. The SQL Server instance must have a Service Principal Name (SPN) registered (for example, `MSSQLSvc/<server>.mydomain.com:1433`).

### Connect with a keytab

Specify the keytab file path and Kerberos realm in the connection string:

```text
sqlserver://<user>@MYDOMAIN.COM@<server>:1433?database=AdventureWorks2025&krb5-realm=MYDOMAIN.COM&krb5-keytabfile=/etc/<user>.keytab
```

### Connect with a credential cache

First, get a ticket:

```bash
kinit <user>@MYDOMAIN.COM
```

Then connect by using the cached credential:

```text
sqlserver://<user>@MYDOMAIN.COM@<server>:1433?database=AdventureWorks2025&krb5-realm=MYDOMAIN.COM&krb5-credcachefile=/tmp/krb5cc_1000
```

### Kerberos parameters

| Parameter | Default | Description |
| --- | --- | --- |
| `krb5-configfile` | `/etc/krb5.conf` | Path to the Kerberos configuration file. |
| `krb5-realm` | - | Kerberos realm (for example, `MYDOMAIN.COM`). |
| `krb5-keytabfile` | - | Path to the keytab file. |
| `krb5-credcachefile` | - | Path to the credential cache file. |
| `krb5-dnslookupkdc` | `true` | Use DNS SRV records to find the Key Distribution Center (KDC). |
| `krb5-udppreferencelimit` | `1` | Maximum UDP message size before switching to TCP. |

For more information, see [SQL Server and Windows authentication](authentication.md).

## Windows authentication (SSPI) isn't available

On Linux and macOS, Windows integrated authentication (SSPI / `trusted_connection=yes`) isn't available. Use one of the following alternatives:

| Method | Connection string |
| --- | --- |
| NTLM | `sqlserver://DOMAIN%5Cuser:password@host?database=db` |
| Kerberos (keytab) | `sqlserver://user@REALM@host?database=db&krb5-realm=REALM&krb5-keytabfile=/path` |
| Kerberos (cache) | `sqlserver://user@REALM@host?database=db&krb5-realm=REALM&krb5-credcachefile=/path` |
| SQL auth | `sqlserver://user:password@host?database=db` |
| Microsoft Entra ID | `sqlserver://host?database=db&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false` (use `azuresql` driver) |

## Certificate configuration

On Linux and macOS, TLS certificates used with the `certificate` or `serverCertificate` parameters must be PEM-encoded files. The driver reads them by using Go's `crypto/tls` package directly.

### System trust store

The driver uses the operating system's default certificate trust store. On Linux, this trust store is typically located at `/etc/ssl/certs/` or `/etc/pki/tls/certs/`. On macOS, the system keychain is used.


To add a custom CA certificate to the system store:

**Ubuntu/Debian:**

```bash
sudo cp myca.crt /usr/local/share/ca-certificates/
sudo update-ca-certificates
```

**RHEL/Fedora:**

```bash
sudo cp myca.crt /etc/pki/ca-trust/source/anchors/
sudo update-ca-trust
```

**macOS:**

```bash
sudo security add-trusted-cert -d -r trustRoot -k /Library/Keychains/System.keychain myca.crt
```

Alternatively, use the `certificate` connection parameter to specify the CA certificate directly without modifying the system store:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true&certificate=/path/to/ca-cert.pem
```

## Related content

- [SQL Server and Windows authentication with go-mssqldb](authentication.md)
- [go-mssqldb encryption and certificates](encryption-certificates.md)
- [go-mssqldb protocols](protocols.md)
- [Install the go-mssqldb driver](installation.md)
