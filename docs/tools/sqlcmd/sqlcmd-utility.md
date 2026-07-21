---
title: Run Transact-SQL Commands with the sqlcmd Utility
description: The sqlcmd utility lets you enter Transact-SQL statements, system procedures, and script files using different modes, using go-mssqldb or ODBC to run T-SQL batches.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dlevy
ms.date: 01/29/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - linux-related-content
  - sfi-ropc-blocked
  - ignite-2025
helpviewer_keywords:
  - "statements [SQL Server], command prompt"
  - "go-sqlcmd"
  - "QUIT command"
  - "Transact-SQL statements, command prompt"
  - "EXIT command"
  - "sqlcmd commands"
  - "ED command"
  - "sqlcmd utility"
  - "command prompt utilities [SQL Server], sqlcmd"
  - "!! command"
  - "stored procedures [SQL Server], command prompt"
  - "system stored procedures [SQL Server], command prompt"
  - "sqlcmd utility, about sqlcmd utility"
  - "scripts [SQL Server], command prompt"
  - "RESET command"
  - "GO command"
zone_pivot_groups: cs1-command-shell
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb"
---
# sqlcmd utility

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Use the **sqlcmd** utility to enter Transact-SQL statements, system procedures, and script files through various modes:

- At the command prompt.
- In **Query Editor** in [SQLCMD mode](/ssms/scripting/sqlcmd-scripts-query-editor).
- In a Windows script file.
- In an operating system (`cmd.exe`) job step of a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job.

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## sqlcmd variants

Two variants of **sqlcmd** exist:

- **sqlcmd** (Go): The `go-mssqldb`-based **sqlcmd**, sometimes styled as **go-sqlcmd**. This version is a standalone tool you can download independently of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. It runs on Windows, macOS, Linux, and in containers.

- **sqlcmd** (ODBC): The platform-aligned, ODBC-based **sqlcmd**, available with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or the Microsoft Command Line Utilities, and part of the `mssql-tools` package on Linux. It also runs on Windows, macOS, Linux, and in containers.

To find out which variant and version of **sqlcmd** is installed on your system, see [Check installed version of sqlcmd utility](sqlcmd-installed-version.md).

For information on how to get **sqlcmd**, see [Download and install the sqlcmd utility](sqlcmd-download-install.md).

## TDS 8.0 support

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces TDS 8.0 support for the **sqlcmd** utility.

## Syntax

In this article, the terms *option*, *parameter*, *command-line argument*, and *switch* are interchangeable.

### [sqlcmd (Go)](#tab/go)

**sqlcmd** (Go) has two help modes: `--help` for modern subcommands and `-?` for ODBC-compatible flags.

#### Modern commands (--help)

```output
Usage:
  sqlcmd [flags]
  sqlcmd [command]

Examples:
# Install/Create, Query, Uninstall SQL Server
  sqlcmd create mssql --accept-eula --using https://aka.ms/AdventureWorksLT.bak
  sqlcmd open ads
  sqlcmd query "SELECT @@version"
  sqlcmd delete
# View configuration information and connection strings
  sqlcmd config view
  sqlcmd config cs

Available Commands:
  completion  Generate the autocompletion script for the specified shell
  config      Modify sqlconfig files using subcommands like "sqlcmd config use-context mssql"
  create      Install/Create SQL Server, Azure SQL, and Tools
  delete      Uninstall/Delete the current context
  help        Help about any command
  open        Open tools (e.g ADS) for current context
  query       Run a query against the current context
  start       Start current context
  stop        Stop current context

Flags:
  -?, --?                  help for backwards compatibility flags (-S, -U, -E etc.)
  -h, --help               help for sqlcmd
      --sqlconfig string   configuration file (default "/Users/<currentUser>/.sqlcmd/sqlconfig")
      --verbosity int      log level, error=0, warn=1, info=2, debug=3, trace=4 (default 2)
      --version            print version of sqlcmd

Use "sqlcmd [command] --help" for more information about a command.
```

#### ODBC-compatible flags (-?)

```output
sqlcmd
   -a packet_size
   -A (dedicated administrator connection)
   -b (terminate batch job if there is an error)
   -c batch_terminator
   -C (trust the server certificate)
   -d db_name
   -e (echo input)
   -E (use trusted connection)
   -F hostname_in_certificate
   -g (enable column encryption)
   -G (use Azure Active Directory for authentication)
   -h rows_per_header
   -H workstation_name
   -i input_file
   -I (enable quoted identifiers, always on)
   -k[1 | 2] (remove or replace control characters)
   -K application_intent
   -l login_timeout
   -L[c] (list servers, optional clean output)
   -m error_level
   -M multisubnet_failover (always enabled)
   -N[s|m|o] (encrypt connection)
   -o output_file
   -P password
   -q "cmdline query"
   -Q "cmdline query" (and exit)
   -r[0 | 1] (msgs to stderr)
   -R (ignored, client regional settings not used)
   -s col_separator
   -S [protocol:]server[instance_name][,port]
   -t query_timeout
   -u (unicode output file)
   -U login_id
   -v var = "value"
   -V error_severity_level
   -w screen_width
   -W (remove trailing spaces)
   -x (disable variable substitution)
   -X[1] (disable commands, startup script, environment variables, optional exit)
   -y variable_length_type_display_width
   -Y fixed_length_type_display_width
   -z new_password
   -Z new_password (and exit)
   --authentication-method (Azure SQL authentication method)
   --driver-logging-level (mssql driver log level)
   --vertical (print results in vertical format)
   -? (usage)
```

#### Breaking changes from sqlcmd (ODBC)

Several switches and behaviors are different in the **sqlcmd** (Go) utility. For the most up-to-date list of missing flags for backward compatibility, see the [Prioritize implementation of back-compat flags](https://github.com/microsoft/go-sqlcmd/discussions/292) GitHub discussion.

- **sqlcmd** (Go) supports the `-P` switch. For SQL Server Authentication, you can provide passwords through these mechanisms:
  - The `-P` command-line switch
  - The `SQLCMDPASSWORD` environment variable
  - The `:CONNECT` command
  - When prompted, type the password to complete a connection

- The `-r` switch requires a `0` or `1` argument.

- The `-R` switch is ignored. The Go runtime doesn't provide access to user locale information.

- The `-I` switch is ignored. Quoted identifiers are always enabled. To disable quoted identifier behavior, add `SET QUOTED IDENTIFIER OFF` in your scripts.

- The `-M` switch is ignored. **sqlcmd** (Go) always enables multi-subnet failover.

- The `-N` takes a string value to specify the encryption choice, which is one of `s[trict]`, `t[rue]`/`m[andatory]`/`yes`/`1`, `o[ptional]`/`no`/`0`/`f[alse]`, or `disable`.
  - If you don't provide `-N` and `-C`, **sqlcmd** negotiates authentication with the server without validating the server certificate.
  - If you provide `-N` but not `-C`, **sqlcmd** requires validation of the server certificate. A `false` value for encryption could still lead to the encryption of the login packet.
  - If you provide both `-N` and `-C`, **sqlcmd** uses their values for encryption negotiation.
  - For more information about client/server encryption negotiation, see [MS-TDS PRELOGIN](/openspecs/windows_protocols/ms-tds/60f56408-0188-4cd5-8b90-25c6f2423868).

  > [!IMPORTANT]  
  > In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], `-N` can be `o` (for `optional`), `m` (for `mandatory`, the default), or `s` (for `strict`). If you don't include `-N`, `-Nm` (for `mandatory`) is the default. This behavior is a breaking change from [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions.

- With the `-u` switch, the generated Unicode output file is prefixed with the UTF-16 little-endian byte-order mark (BOM).

- Some behaviors that were kept to maintain compatibility with `OSQL` might have changed, such as alignment of column headers for some data types.

- All commands must fit on one line, even `EXIT`. Interactive mode doesn't check for open parentheses or quotes for commands, and doesn't prompt for successive lines. This behavior is different from the ODBC version, which allows the query run by `EXIT(query)` to span multiple lines.

**sqlcmd** (Go) supports shared memory, named pipes, and TCP transport. Use the appropriate protocol prefix on the server name to force a protocol:

- `lpc` for shared memory (localhost only)
- `np` for named pipes, or use the UNC named pipe path as the server name
- `tcp` for TCP

If you don't specify a protocol, **sqlcmd** tries to dial in this order: `lpc` > `np` > `tcp`. When connecting to a remote host, `lpc` is skipped.

#### Enhancements

- `:Connect` has an optional `-G` parameter to select one of the authentication methods for Azure SQL Database - `SqlAuthentication`, `ActiveDirectoryDefault`, `ActiveDirectoryIntegrated`, `ActiveDirectoryServicePrincipal`, `ActiveDirectoryManagedIdentity`, `ActiveDirectoryPassword`. For more information, see [Authenticate with Microsoft Entra ID in sqlcmd](sqlcmd-authentication.md). If `-G` isn't provided, Integrated security or SQL Server authentication is used, depending on the presence of a `-U` user name parameter.

- The `--driver-logging-level` command line parameter allows you to see traces from the `go-mssqldb` driver. Use `64` to see all traces.

- **sqlcmd** (Go) can print results using a vertical format. Use the `--vertical` command line switch to set it. The `SQLCMDFORMAT` scripting variable also controls it.

### [sqlcmd (ODBC)](#tab/odbc)

```output
sqlcmd
   -a packet_size
   -A (dedicated administrator connection)
   -b (terminate batch job if there is an error)
   -c batch_terminator
   -C (trust the server certificate)
   -d db_name
   -D
   -e (echo input)
   -E (use trusted connection)
   -f codepage | i:codepage[,o:codepage] | o:codepage[,i:codepage]
   -F hostname_in_certificate
   -g (enable column encryption)
   -G (use Azure Active Directory for authentication)
   -h rows_per_header
   -H workstation_name
   -i input_file
   -I (enable quoted identifiers)
   -j (Print raw error messages)
   -J server_certificate
   -k[1 | 2] (remove or replace control characters)
   -K application_intent
   -l login_timeout
   -L[c] (list servers, optional clean output)
   -m error_level
   -M multisubnet_failover
   -N[s|m|o] (encrypt connection)
   -o output_file
   -p[1] (print statistics, optional colon format)
   -P password
   -q "cmdline query"
   -Q "cmdline query" (and exit)
   -r[0 | 1] (msgs to stderr)
   -R (use client regional settings)
   -s col_separator
   -S [protocol:]server[instance_name][,port]
   -t query_timeout
   -u (unicode output file)
   -U login_id
   -v var = "value"
   -V error_severity_level
   -w screen_width
   -W (remove trailing spaces)
   -x (disable variable substitution)
   -X[1] (disable commands, startup script, environment variables, optional exit)
   -y variable_length_type_display_width
   -Y fixed_length_type_display_width
   -z new_password
   -Z new_password (and exit)
   -? (usage)
```

Currently, **sqlcmd** doesn't require a space between the command-line option and the value. However, a future release might require a space between the command-line option and the value.

---

## Command-line options

The following table lists the command-line options available in **sqlcmd**, and which operating systems they support.

| Command-line option | Supported on Windows | Supported on Linux and macOS |
| --- | --- | --- |
| [Login-related options](#login-related-options) | | |
| **[-A](#-a)** | Yes | No |
| **[-C](#-c)** | Yes | Yes |
| **[-d *db_name*](#-d-db_name)** | Yes | Yes |
| **[-D](#-d)** | Yes | Yes |
| **[-l *login_timeout*](#-l-login_timeout)** | Yes | Yes |
| **[-E](#-e-trusted)** | Yes | Yes |
| **[-F *hostname_in_certificate*](#-f-hostname_in_certificate)** | Yes | Yes |
| **[-g](#-g-column)** | Yes | Yes |
| **[-G](#-g-entra)** | Yes | Yes |
| **[-H *workstation_name*](#-h-workstation_name)** | Yes | Yes |
| **[-j](#-j)** | Yes | Yes |
| **[-J *server_certificate*](#-j-server_certificate)** | No | Yes |
| **[-K *application_intent*](#-k-application_intent)** | Yes | Yes |
| **[-M *multisubnet_failover*](#-m-multisubnet_failover)** | Yes | Yes |
| **[-N\[s\|m\|o\]](#-nsmo)** | Yes | Yes |
| **[-P *password*](#-p-password)** | Yes | Yes |
| **[-S \[*protocol*:\]*server*\[\\*instance_name*\]\[,*port*\]](#-s-protocolserverinstance_nameport)** | Yes | Yes |
| **[-U *login_id*](#-u-login_id)** | Yes | Yes |
| **[-z *new_password*](#-z-new_password)** | Yes | Yes |
| **[-Z *new_password*](#-z-exit)** | Yes | Yes |
| [Input/output options](#input-and-output-options) | | |
| **[-f *codepage* \| i:*codepage*\[,o:*codepage*\] \| o:*codepage*\[,i:*codepage*\]](#-f-codepage--icodepageocodepage--ocodepageicodepage)** | Yes | Yes |
| **[-i *input_file*\[,*input_file2*...\]](#-i-input_fileinput_file2)** | Yes | Yes |
| **[-o *output_file*](#-o-output_file)** | Yes | Yes |
| **[-r\[0 \| 1\]](#-r0--1)** | Yes | Yes |
| **[-R](#-r)** | Yes | Yes |
| **[-u](#-u)** | Yes | Yes |
| [Query execution options](#query-execution-options) | | |
| **[-e](#-e-stdout)** | Yes | Yes |
| **[-I](#-i)** | Yes | Yes |
| **[-q "*cmdline query*"](#-q-cmdline-query)** | Yes | Yes |
| **[-Q "*cmdline query*"](#-q-exit)** | Yes | Yes |
| **[-t *query_timeout*](#-t-query_timeout)** | Yes | Yes |
| **[-v var = *value* \[ var = *value*... \]](#-v-var--value--var--value-)** | Yes | No |
| **[-x](#-x)** | Yes | Yes |
| [Format options](#format-options) | | |
| **[-h *headers*](#-h-headers)** | Yes | Yes |
| **[-k \[1 \| 2\]](#-k-1--2)** | Yes | Yes |
| **[-s *col_separator*](#-s-col_separator)** | Yes | Yes |
| **[-w *screen_width*](#-w-screen_width)** | Yes | Yes |
| **[-W](#-w)** | Yes | Yes |
| **[-y *variable_length_type_display_width*](#-y-variable_length_type_display_width)** | Yes | Yes |
| **[-Y *fixed_length_type_display_width*](#-y-fixed_length_type_display_width)** | Yes | Yes |
| [Error reporting options](#error-reporting-options) | | |
| **[-b](#-b)** | Yes | Yes |
| **[-m *error_level*](#-m-error_level)** | Yes | Yes |
| **[-V *error_severity_level*](#-v-error_severity_level)** | Yes | Yes |
| [Miscellaneous options](#miscellaneous-options) | | |
| **[-a *packet_size*](#-a-packet_size)** | Yes | Yes |
| **[-c *batch_terminator*](#-c-batch_terminator)** | Yes | Yes |
| **[-L\[c\]](#-lc)** | Yes | No |
| **[-p\[1\]](#-p1)** | Yes | Yes |
| **[-X\[1\]](#-x1)** | Yes | Yes |
| **[-?](#-version)** | Yes | Yes |

### Login-related options

#### -A

**Applies to**: Windows only. Linux and macOS aren't supported.

Signs in to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with a dedicated administrator connection (DAC). Use this kind of connection to troubleshoot a server. This connection works only with server computers that support DAC. If DAC isn't available, **sqlcmd** generates an error message, and then exits. For more information about DAC, see [Diagnostic connection for database administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md). The `-A` option isn't supported with the `-G` option. When connecting to Azure SQL Database by using `-A`, you must be an administrator on the logical SQL server. DAC isn't available for a Microsoft Entra administrator.

> [!NOTE]  
> For information on how to make a dedicated administrator connection (DAC) on macOS or Linux, see [Programming Guidelines](../../connect/odbc/linux-mac/programming-guidelines.md).

#### -C

Use this option to configure the client to implicitly trust the server certificate without validation. This option is equivalent to the ADO.NET option `TRUSTSERVERCERTIFICATE = true`.

For the **sqlcmd** (Go) utility, the following conditions also apply:

- If you don't provide `-N` and `-C`, **sqlcmd** negotiates authentication with the server without validating the server certificate.
- If you provide `-N` but not `-C`, **sqlcmd** requires validation of the server certificate. A `false` value for encryption could still lead to the encryption of the login packet.
- If you provide both `-N` and `-C`, **sqlcmd** uses their values for encryption negotiation.

#### -d *db_name*

Issues a `USE <db_name>` statement when you start **sqlcmd**. This option sets the **sqlcmd** scripting variable `SQLCMDDBNAME`. This parameter specifies the initial database. The default is your login's default-database property. If the database doesn't exist, an error message is generated and **sqlcmd** exits.

#### -D

Interprets the server name provided to `-S` as a DSN instead of a hostname. For more information, see [DSN support in sqlcmd and bcp](#dsn-support-in-sqlcmd-and-bcp).

> [!NOTE]  
> The `-D` option is only available on Linux and macOS clients. On Windows clients, it refers to an obsolete option which is removed, and is ignored.

#### -l *login_timeout*

Specifies the number of seconds before a **sqlcmd** login to the ODBC driver times out when you try to connect to a server. This option sets the **sqlcmd** scripting variable `SQLCMDLOGINTIMEOUT`. The default time-out for login to **sqlcmd** is 8 seconds. When using the `-G` option to connect to Azure SQL Database or Azure Synapse Analytics and authenticate with Microsoft Entra ID, a timeout value of at least 30 seconds is recommended. The login timeout must be a number between `0` and `65534`. If the value isn't numeric, or doesn't fall into that range, **sqlcmd** generates an error message. A value of `0` specifies time-out to be infinite.

<a id="-e-trusted"></a>

#### -E

Uses a trusted connection instead of using a user name and password to sign in to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. By default, without `-E` specified, **sqlcmd** uses the trusted connection option.

The `-E` option ignores possible user name and password environment variable settings such as `SQLCMDPASSWORD`. If the `-E` option is used together with the `-U` option or the `-P` option, an error message is generated.

> [!NOTE]  
> For more information about making trusted connections that use integrated authentication from a Linux or macOS client, see [Using Integrated Authentication](../../connect/odbc/linux-mac/using-integrated-authentication.md).

<a id="-g-column"></a>

#### -F *hostname_in_certificate*

Specifies a different, expected Common Name (CN) or Subject Alternate Name (SAN) in the server certificate to use during server certificate validation. Without this option, certificate validation ensures that the CN or SAN in the certificate matches the server name to which you're connecting. You can use this parameter when the server name doesn't match the CN or SAN, for example, when using DNS aliases.

For example:

```console
sqlcmd -S server01 -Q "SELECT TOP 100 * FROM WideWorldImporters.Sales.Orders" -A -Ns -F server01.adventure-works.com
```

> [!NOTE]  
> **sqlcmd** (Go) also uses `-F` to specify the host name in the server certificate. To print results in vertical format, **sqlcmd** (Go) uses the `--vertical` switch instead.

#### -g

Sets the Column Encryption setting to `Enabled`. For more information, see [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md). Only master keys stored in Windows Certificate Store are supported. The `-g` option requires at least **sqlcmd** version [13.1](https://go.microsoft.com/fwlink/?LinkID=825643). To determine your version, run `sqlcmd -?`.

<a id="-g-entra"></a>

#### -G

Use this option to authenticate with Microsoft Entra when connecting to Azure SQL Database or Azure Synapse Analytics. This option sets the **sqlcmd** scripting variable `SQLCMDUSEAAD = true`. The `-G` option requires at least **sqlcmd** version [13.1](https://go.microsoft.com/fwlink/?LinkID=825643). To determine your version, run `sqlcmd -?`. For more information, see [Microsoft Entra authentication for Azure SQL](/azure/azure-sql/database/authentication-aad-overview). The `-A` option isn't supported with the `-G` option.

The `-G` option only applies to Azure SQL Database and Azure Synapse Analytics.

Microsoft Entra interactive authentication isn't currently supported on Linux or macOS. Microsoft Entra integrated authentication requires [Download ODBC Driver for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md) version 17.6.1 or higher and a [properly configured Kerberos environment](../../connect/odbc/linux-mac/using-integrated-authentication.md).

For more information about Microsoft Entra authentication, see [Authenticate with Microsoft Entra ID in sqlcmd](sqlcmd-authentication.md).

#### -H *workstation_name*

A workstation name. This option sets the **sqlcmd** scripting variable `SQLCMDWORKSTATION`. The workstation name appears in the `hostname` column of the `sys.sysprocesses` catalog view, and can be returned by using the `sp_who` stored procedure. If you don't specify this option, the default is the current computer name. Use this name to identify different **sqlcmd** sessions.

#### -j

Prints raw error messages to the screen.

#### -J *server_certificate*

**Applies to**: **sqlcmd** (ODBC), Linux and macOS only. Windows isn't supported.

Specifies the path to a server certificate file. This file is matched against the server's connection encryption certificate. The match is done instead of standard certificate validation (expiry, host name, trust chain, and so on). The accepted certificate formats are PEM, DER, and CER.

Use this option when connecting to servers that use self-signed certificates or certificates issued by a private certificate authority. If encryption is enabled and certificate validation fails, the connection fails.

For example:

```console
sqlcmd -S server01 -Q "SELECT TOP 100 * FROM WideWorldImporters.Sales.Orders" -A -Ns -J /etc/ssl/certs/server_certificate.cer
```

#### -K *application_intent*

Declares the application workload type when connecting to a server. The only currently supported value is `ReadOnly`. If you don't specify `-K`, **sqlcmd** doesn't support connectivity to a secondary replica in an availability group. For more information, see [Offload read-only workload to secondary replica of an Always On availability group](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).

> [!NOTE]  
> `-K` isn't supported in SUSE Linux Enterprise Server (SLES). You can, however, specify the `ApplicationIntent=ReadOnly` keyword in a DSN file passed to **sqlcmd**. For more information, see [DSN Support in sqlcmd and bcp](#dsn-support-in-sqlcmd-and-bcp) later in this article.

For more information, see [High availability and disaster recovery on Linux and macOS](../../connect/odbc/linux-mac/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).

#### -M *multisubnet_failover*

Always specify `-M` when connecting to the availability group listener of a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] availability group or a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Failover Cluster Instance. `-M` provides for faster detection of and connection to the (currently) active server. If you don't specify `-M`, `-M` is off.

For more information, see:

- [Connect to an Always On availability group listener](../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)
- [Reference for the creation and configuration of Always On availability groups](../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)
- [Failover Clustering and Always On availability groups (SQL Server)](../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)
- [Offload read-only workload to secondary replica of an Always On availability group](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)

> [!NOTE]  
> `-M` isn't supported in SUSE Linux Enterprise Server (SLES). You can, however, specify the `MultiSubnetFailover=Yes` keyword in a DSN file passed to **sqlcmd**. For more information, see [DSN Support in sqlcmd and bcp](#dsn-support-in-sqlcmd-and-bcp) later in this article.

For more information, see [High availability and disaster recovery on Linux and macOS](../../connect/odbc/linux-mac/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).

#### -N[s|m|o]

The client uses this option to request an encrypted connection.

The `-N` switch can be `o` (for `optional`), `m` (for `mandatory`, the default), or `s` (for `strict`). If you don't include `-N`, the default is `-Nm` (for `mandatory`). This default is a breaking change from [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, where the default is `-No`.

For the **sqlcmd** (Go) utility, `-N` takes a string value that can be one of `true`, `false`, or `disable` to specify the encryption choice. (`default` is the same as omitting the parameter):

- If you don't provide `-N` and `-C`, **sqlcmd** negotiates authentication with the server without validating the server certificate.

- If you provide `-N` but not `-C`, **sqlcmd** requires validation of the server certificate. A `false` value for encryption could still lead to the encryption of the login packet.

- If you provide both `-N` and `-C`, **sqlcmd** uses their values for encryption negotiation.

#### -P *password*

A user-specified password. Passwords are case-sensitive. If you use the `-U` option, but don't use the `-P` option or set the `SQLCMDPASSWORD` environment variable, **sqlcmd** prompts the user for a password. Don't use a null (blank) password, but you can specify the null password by using a pair of contiguous double-quotation marks for the parameter value (`""`).

> [!IMPORTANT]  
> Using `-P` is insecure. Avoid giving the password on the command line. Alternatively, use the `SQLCMDPASSWORD` environment variable, or interactively input the password by omitting the `-P` option.

Use a [strong password](../../relational-databases/security/strong-passwords.md).

The password prompt is displayed by printing the password prompt to the console, as follows: `Password:`

User input is hidden. This means that nothing is displayed and the cursor stays in position.

The `SQLCMDPASSWORD` environment variable lets you set a default password for the current session. Therefore, passwords don't have to be hard-coded into batch files. The following example first sets the `SQLCMDPASSWORD` variable at the command prompt and then accesses the **sqlcmd** utility.

At the command prompt, type the following command. Replace `<password>` with a valid password.

::: zone pivot="cs1-bash"

```bash
SET SQLCMDPASSWORD=<password>
sqlcmd
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
SET SQLCMDPASSWORD=<password>
sqlcmd
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
SET SQLCMDPASSWORD=<password>
sqlcmd
```

::: zone-end

If the user name and password combination is incorrect, an error message is generated.

> [!NOTE]  
> The `OSQLPASSWORD` environment variable is kept for backward compatibility. The `SQLCMDPASSWORD` environment variable takes precedence over the `OSQLPASSWORD` environment variable. This means that **sqlcmd** and **osql** can be used next to each other without interference. Old scripts continue to work.

If you use the `-P` option with the `-E` option, an error message is generated.

If you use the `-P` option with more than one argument, an error message is generated and the program exits.

A password containing special characters can generate an error message. You should escape special characters when using `-P`, or use the `SQLCMDPASSWORD` environment variable instead.

On Linux and macOS, when used with the `-G` option without `-U`, `-P` specifies a file that contains an access token (v17.8+). The token file should be in UTF-16LE (no BOM) format.

Access tokens can be obtained via various methods. You must ensure the access token is correct byte-for-byte, because it's sent as-is. The following example command obtains an access token. The command uses the Azure CLI and Linux commands and saves it to a file in the proper format. If your system or terminal's default encoding isn't ASCII or UTF-8, you might need to adjust the `iconv` options. Be sure to carefully secure the resulting file and delete it when it's no longer required.

```azurecli
az account get-access-token --resource https://database.windows.net --output tsv | cut -f 1 | tr -d '\n' | iconv -f ascii -t UTF-16LE > /tmp/tokenFile
```

#### -S [*protocol*:]*server*[\\*instance_name*][,*port*]

Specifies the instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to connect to. This option sets the **sqlcmd** scripting variable `SQLCMDSERVER`.

Specify *server_name* to connect to the default instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on that server computer. Specify *server_name*[\\*instance_name*] to connect to a named instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on that server computer. If you don't specify a server computer, **sqlcmd** connects to the default instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on the local computer. This option is required when you run **sqlcmd** from a remote computer on the network.

*protocol* can be `tcp` (TCP/IP), `lpc` (shared memory), or `np` (named pipes).

If you don't specify a *server_name*[\\*instance_name*] when you start **sqlcmd**, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] checks for and uses the `SQLCMDSERVER` environment variable.

> [!NOTE]  
> The `OSQLSERVER` environment variable is kept for backward compatibility. The `SQLCMDSERVER` environment variable takes precedence over the `OSQLSERVER` environment variable. This means that **sqlcmd** and **osql** can be used next to each other without interference. Old scripts continue to work.

The ODBC driver on Linux and macOS requires `-S`. The only valid protocol value is `tcp`.

#### -U *login_id*

The login name or contained database user name. For contained database users, you must provide the database name option (`-d`).

> [!NOTE]  
> The `OSQLUSER` environment variable is kept for backward compatibility. The `SQLCMDUSER` environment variable takes precedence over the `OSQLUSER` environment variable. This means that **sqlcmd** and **osql** can be used next to each other without interference. Old scripts continue to work.

If you don't specify either the `-U` option or the `-P` option, **sqlcmd** tries to connect by using Windows Authentication mode. Authentication is based on the Windows account of the user who is running **sqlcmd**.

If you use the `-U` option with the `-E` option (described later in this article), an error message is generated. If the `-U` option is followed by more than one argument, an error message is generated and the program exits.

#### -z *new_password*

Change the password. Replace `<oldpassword>` with the old password, and `<newpassword>` with the new password.

::: zone pivot="cs1-bash"

```bash
sqlcmd -U someuser -P <oldpassword> -z <newpassword>
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd -U someuser -P <oldpassword> -z <newpassword>
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -U someuser -P <oldpassword> -z <newpassword>
```

::: zone-end

<a id="-z-exit"></a>

#### -Z *new_password*

Change the password and exit. Replace `<oldpassword>` with the old password, and `<newpassword>` with the new password.

::: zone pivot="cs1-bash"

```bash
sqlcmd -U someuser -P <oldpassword> -Z <newpassword>
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd -U someuser -P <oldpassword> -Z <newpassword>
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -U someuser -P <oldpassword> -Z <newpassword>
```

::: zone-end

### Input and output options

#### -f *codepage* | i:*codepage*[,o:*codepage*] | o:*codepage*[,i:*codepage*]

Specifies the input and output code pages. The codepage number is a numeric value that specifies an installed Windows code page.

**Code-page conversion rules:**

- If you don't specify a code page, **sqlcmd** uses the current code page for both input and output files, unless the input file is a Unicode file, in which case no conversion is required.

- **sqlcmd** automatically recognizes both big-endian and little-endian Unicode input files. If you specify the `-u` option, the output is always little-endian Unicode.

- If you don't specify an output file, the output code page is the console code page. This approach enables the output to be displayed correctly on the console.

- Multiple input files are assumed to use same code page. Unicode and non-Unicode input files can be mixed.

Enter `chcp` at the command prompt to verify the code page of `cmd.exe`.

> [!NOTE]  
> On Linux, the codepage number is a numeric value that specifies an installed Linux code page (available since 17.5.1.1).

#### -i *input_file*[,*input_file2*...]

Identifies the file that contains a batch of Transact-SQL statements or stored procedures. You can specify multiple files that **sqlcmd** reads and processes in order. Don't use any spaces between file names. **sqlcmd** first checks to see whether all the specified files exist. If one or more files don't exist, **sqlcmd** exits. The `-i` option and the `-Q`/`-q` options are mutually exclusive.

> [!NOTE]  
> If you use the `-i` option followed by one or more extra parameters, you must use a space between the parameter and the value. This requirement is a known issue in **sqlcmd** (Go).

Path examples:

```output
-i C:\<filename>
-i \\<Server>\<Share$>\<filename>
-i "C:\Some Folder\<file name>"
```

File paths that contain spaces must be enclosed in quotation marks.

You can use this option more than once:

::: zone pivot="cs1-bash"

```bash
sqlcmd -i <input_file1> -i <input_file2>
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd -i <input_file1> -i <input_file2>
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -i <input_file1> -i <input_file2>
```

::: zone-end

#### -o *output_file*

Identifies the file that receives output from **sqlcmd**.

If you specify `-u`, **sqlcmd** stores the *output_file* in Unicode format. If the file name isn't valid, **sqlcmd** generates an error message and exits. **sqlcmd** doesn't support concurrent writing of multiple **sqlcmd** processes to the same file. If this happens, consider the file output corrupted or incorrect. The `-f` option is also relevant to file formats. **sqlcmd** creates this file if it doesn't exist. **sqlcmd** overwrites a file of the same name from a previous session. The file specified here isn't the `stdout` file. If you specify a `stdout` file, **sqlcmd** doesn't use this file.

Path examples:

```output
-o C:< filename>
-o \\<Server>\<Share$>\<filename>
-o "C:\Some Folder\<file name>"
 ```

File paths that contain spaces must be enclosed in quotation marks.

#### -r[0 | 1]

Redirects the error message output to the screen (`stderr`). If you don't specify a parameter or if you specify `0`, only error messages that have a severity level of 11 or higher are redirected. If you specify `1`, all error message output including `PRINT` is redirected. This option has no effect if you use `-o`. By default, messages are sent to `stdout`.

> [!NOTE]  
> For the **sqlcmd** (Go) utility, `-r` requires a `0` or `1` argument.

#### -R

**Applies to**: **sqlcmd** (ODBC) only.

Causes **sqlcmd** to localize numeric, currency, date, and time columns retrieved from [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] based on the client's locale. By default, these columns are displayed using the server's regional settings.

> [!NOTE]  
> On Linux and macOS, `-R` currently only uses `en_US` (US English) formatting.

#### -u

Specifies that *output_file* is stored in Unicode format, regardless of the format of *input_file*.

> [!NOTE]  
> For the **sqlcmd** (Go) utility, the generated Unicode output file is prefixed with the UTF-16 little-endian byte-order mark (BOM).

### Query execution options

<a id="-e-stdout"></a>

#### -e

Writes input scripts to the standard output device (`stdout`).

#### -I

**Applies to**: **sqlcmd** (ODBC) only.

Sets the `SET QUOTED_IDENTIFIER` connection option to `ON`. The default setting is `OFF`. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

> [!NOTE]  
> To disable quoted identifier behavior in the **sqlcmd** (Go) utility, add `SET QUOTED IDENTIFIER OFF` in your scripts.

#### -q "*cmdline query*"

Executes a query when **sqlcmd** starts, but doesn't exit **sqlcmd** when the query finishes. You can run multiple queries delimited with a semicolon. Use quotation marks around the query, as shown in the following example.

At the command prompt, type:

::: zone pivot="cs1-bash"

```bash
sqlcmd -d AdventureWorks2025 -q "SELECT FirstName, LastName FROM Person.Person WHERE LastName LIKE 'Whi%';"
sqlcmd -d AdventureWorks2025 -q "SELECT TOP 5 FirstName FROM Person.Person;SELECT TOP 5 LastName FROM Person.Person;"
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd -d AdventureWorks2025 -q "SELECT FirstName, LastName FROM Person.Person WHERE LastName LIKE 'Whi%';"
sqlcmd -d AdventureWorks2025 -q "SELECT TOP 5 FirstName FROM Person.Person;SELECT TOP 5 LastName FROM Person.Person;"
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -d AdventureWorks2025 -q "SELECT FirstName, LastName FROM Person.Person WHERE LastName LIKE 'Whi%';"
sqlcmd -d AdventureWorks2025 -q "SELECT TOP 5 FirstName FROM Person.Person;SELECT TOP 5 LastName FROM Person.Person;"
```

::: zone-end

> [!IMPORTANT]  
> Don't use the `GO` terminator in the query.

If you specify `-b` together with this option, **sqlcmd** exits with an error. The `-b` option is described [elsewhere](#-b) in this article.

<a id="-q-exit"></a>

#### -Q "*cmdline query*"

Executes a query when **sqlcmd** starts and then immediately exits **sqlcmd**. You can execute multiple queries delimited with a semicolon.

Use quotation marks around the query, as shown in the following example.

At the command prompt, type:

::: zone pivot="cs1-bash"

```bash
sqlcmd -d AdventureWorks2025 -Q "SELECT FirstName, LastName FROM Person.Person WHERE LastName LIKE 'Whi%';"
sqlcmd -d AdventureWorks2025 -Q "SELECT TOP 5 FirstName FROM Person.Person;SELECT TOP 5 LastName FROM Person.Person;"
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd -d AdventureWorks2025 -Q "SELECT FirstName, LastName FROM Person.Person WHERE LastName LIKE 'Whi%';"
sqlcmd -d AdventureWorks2025 -Q "SELECT TOP 5 FirstName FROM Person.Person;SELECT TOP 5 LastName FROM Person.Person;"
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -d AdventureWorks2025 -Q "SELECT FirstName, LastName FROM Person.Person WHERE LastName LIKE 'Whi%';"
sqlcmd -d AdventureWorks2025 -Q "SELECT TOP 5 FirstName FROM Person.Person;SELECT TOP 5 LastName FROM Person.Person;"
```

::: zone-end

> [!IMPORTANT]  
> Don't use the `GO` terminator in the query.

If you specify `-b` together with this option, **sqlcmd** exits with an error. The `-b` option is described [elsewhere](#-b) in this article.

#### -t *query_timeout*

Specifies the number of seconds before a command (or Transact-SQL statement) times out. This option sets the **sqlcmd** scripting variable `SQLCMDSTATTIMEOUT`. If you don't specify a *query_timeout* value, the command doesn't time out. The *query_timeout* must be a number between `1` and `65534`. If you provide a value that's not numeric or doesn't fall into that range, **sqlcmd** generates an error.

> [!NOTE]  
> The actual timeout value can vary from the specified *query_timeout* value by several seconds.

#### -v var = *value* [ var = *value*... ]

**Applies to**: Windows only. Linux and macOS aren't supported.

Creates a **sqlcmd** scripting variable for use in a **sqlcmd** script.

### [Windows](#tab/windows-support)

Enclose the value in quotation marks if the value contains spaces. You can specify multiple `<var>="<value>"` values. If any of the values you specify contain errors, **sqlcmd** generates an error message and then exits.

::: zone pivot="cs1-bash"

```bash
sqlcmd -v MyVar1=something MyVar2="some thing"
sqlcmd -v MyVar1=something -v MyVar2="some thing"
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd -v MyVar1=something MyVar2="some thing"
sqlcmd -v MyVar1=something -v MyVar2="some thing"
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -v MyVar1=something MyVar2="some thing"
sqlcmd -v MyVar1=something -v MyVar2="some thing"
```

::: zone-end

#### [Linux and macOS](#tab/linux-support)

You can use the following alternative method: Put the parameters inside one file, which you can then append to another file. This method helps you use a parameter file to replace the values. For example, create a file called `a.sql` (the parameter file) with the following content:

```bash
:setvar ColumnName object_id
:setvar TableName sys.objects
```

Then create a file called `b.sql`, with the parameters for replacement:

```sql
SELECT $(ColumnName) FROM $(TableName)
```

At the command line, combine `a.sql` and `b.sql` into `c.sql` using the following commands:

```bash
cat a.sql > c.sql
cat b.sql >> c.sql
```

Run **sqlcmd** and use `c.sql` as input file:

```bash
sqlcmd -S<...> -P<..> -U<..> -I c.sql
```

---

#### -x

Causes **sqlcmd** to ignore scripting variables. This parameter is useful when a script contains many `INSERT` statements that might contain strings that have the same format as regular variables, such as `$(<variable_name>)`.

### Format options

#### -h *headers*

Specifies the number of rows to print between the column headings. The default setting prints the headings once for each set of query results. This option sets the **sqlcmd** scripting variable `SQLCMDHEADERS`. Use `-1` to specify that headers aren't printed. An invalid value causes **sqlcmd** to generate an error message and then exit.

#### -k [1 | 2]

Removes all control characters, such as tabs and new line characters from the output. This parameter preserves column formatting when data is returned.

- `-k` removes control characters.
- `-k1` replaces each control character with a space.
- `-k2` replaces consecutive control characters with a single space.

#### -s *col_separator*

Specifies the column-separator character. The default is a blank space. This option sets the **sqlcmd** scripting variable `SQLCMDCOLSEP`. To use characters that have special meaning to the operating system, such as the ampersand (`&`) or semicolon (`;`), enclose the character in quotation marks (`"`). The column separator can be any 8-bit character.

#### -w *screen_width*

Specifies the screen width for output. This option sets the **sqlcmd** scripting variable `SQLCMDCOLWIDTH`. The column width must be a number greater than `8` and less than `65536`. If the specified column width doesn't fall into that range, **sqlcmd** generates an error message. The default width is 80 characters. When an output line exceeds the specified column width, it wraps on to the next line.

#### -W

Removes trailing spaces from a column. Use this option together with the `-s` option when preparing data that you want to export to another application. Can't be used with the `-y` or `-Y` options.

#### -y *variable_length_type_display_width*

Sets the **sqlcmd** scripting variable `SQLCMDMAXVARTYPEWIDTH`. The default value is `256`. It limits the number of characters that are returned for the large variable length data types:

- **varchar(max)**
- **nvarchar(max)**
- **varbinary(max)**
- **xml**
- **user-defined data types** (UDTs)
- **text**
- **ntext**
- **image**

UDTs can be of fixed length depending on the implementation. If this length of a fixed length UDT is shorter than *display_width*, the value of the UDT returned isn't affected. However, if the length is longer than *display_width*, the output is truncated.

> [!CAUTION]  
> Use the `-y 0` option with extreme caution, because it can cause significant performance issues on both the server and the network, depending on the size of data returned.

#### -Y *fixed_length_type_display_width*

Sets the **sqlcmd** scripting variable `SQLCMDMAXFIXEDTYPEWIDTH`. The default value is `0` (unlimited). Limits the number of characters that are returned for the following data types:

- **char(*n*)**, where 1 <= *n* <= 8000
- **nchar(*n*)**, where 1 <= *n* <= 4000
- **varchar(*n*)**, where 1 <= *n* <= 8000
- **nvarchar(*n*)**, where 1 <= *n* <= 4000
- **varbinary(*n*)**, where 1 <= *n* <= 4000
- **sql_variant**

### Error reporting options

#### -b

Specifies that **sqlcmd** exits and returns a `DOS ERRORLEVEL` value when an error occurs. The value that **sqlcmd** returns to the `ERRORLEVEL` variable is `1` when the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] error message has a severity level greater than 10. Otherwise, the value returned is `0`. If you set the `-V` option, in addition to `-b`, **sqlcmd** doesn't report an error if the severity level is lower than the values set by the `-V` option. Command prompt batch files can test the value of `ERRORLEVEL` and handle the error appropriately. **sqlcmd** doesn't report errors for severity level 10 (informational messages).

If the **sqlcmd** script contains an incorrect comment, syntax error, or is missing a scripting variable, the `ERRORLEVEL` returned is `1`.

#### -m *error_level*

Controls which error messages are sent to `stdout`. **sqlcmd** sends messages that have a severity level greater than or equal to this level. When you set this value to `-1`, **sqlcmd** sends all messages, including informational messages. Don't include spaces between the `-m` and `-1`. For example, `-m-1` is valid, and `-m -1` isn't.

This option also sets the **sqlcmd** scripting variable `SQLCMDERRORLEVEL`. This variable has a default of `0`.

#### -V *error_severity_level*

Controls the severity level that **sqlcmd** uses to set the `ERRORLEVEL` variable. Error messages that have severity levels greater than or equal to this value set `ERRORLEVEL`. Values that are less than 0 are reported as `0`. You can use batch and CMD files to test the value of the `ERRORLEVEL` variable.

### Miscellaneous options

#### -a *packet_size*

Requests a packet of a different size. This option sets the **sqlcmd** scripting variable `SQLCMDPACKETSIZE`. *packet_size* must be a value between `512` and `32767`. The default is `4096`. A larger packet size can enhance performance for execution of scripts that have many Transact-SQL statements between `GO` commands. You can request a larger packet size. However, if the request is denied, **sqlcmd** uses the server default for packet size.

#### -c *batch_terminator*

Specifies the batch terminator. By default, you must terminate commands and send them to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] using the word `GO` on a line by itself, followed by **Enter**. When you reset the batch terminator, don't use Transact-SQL reserved keywords or characters that have special meaning to the operating system, even if they're preceded by a backslash.

#### -L[c]

**Applies to**: Windows only. Linux and macOS aren't supported.

Lists the locally configured server computers, and the names of the server computers that are broadcasting on the network. You can't use this parameter in combination with other parameters. The maximum number of server computers that can be listed is 3,000. If the server list is truncated because of the size of the buffer, a warning message is displayed.

> [!NOTE]  
> Because of the nature of broadcasting on networks, **sqlcmd** might not receive a timely response from all servers. Therefore, the list of servers returned can vary for each invocation of this option.

If you specify the optional parameter `c`, the output appears without the `Servers:` header line, and each server line is listed without leading spaces. This presentation is referred to as clean output. Clean output improves the processing performance of scripting languages.

#### -p[1]

Prints performance statistics for every result set. The following display is an example of the format for performance statistics:

```output
Network packet size (bytes): n

x xact[s]:

Clock Time (ms.): total       t1  avg       t2 (t3 xacts per sec.)
```

Where:

- `x` = Number of transactions processed by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].
- `t1` = Total time for all transactions.
- `t2` = Average time for a single transaction.
- `t3` = Average number of transactions per second.

All times are in milliseconds.

If you specify the optional parameter `1`, the output format of the statistics is in colon-separated format that can be imported easily into a spreadsheet or processed by a script.

If you specify the optional parameter as any value other than `1`, an error is generated and **sqlcmd** exits.

#### -X[1]

Disables commands that might compromise system security when **sqlcmd** is executed from a batch file. The disabled commands are still recognized; **sqlcmd** issues a warning message and continues. If you specify the optional parameter `1`, **sqlcmd** generates an error message and then exits. The following commands are disabled when the `-X` option is used:

- `ED`
- `!!` *command*

If you specify the `-X` option, it prevents environment variables from being passed on to **sqlcmd**. It also prevents the startup script specified by using the `SQLCMDINI` scripting variable from being executed. For more information about **sqlcmd** scripting variables, see [sqlcmd - Use with scripting variables](sqlcmd-use-scripting-variables.md).

<a id="-version"></a>

#### -?

Displays the version of **sqlcmd** and a syntax summary of **sqlcmd** options.

> [!NOTE]  
> On macOS, run `sqlcmd '-?'` (with quotation marks) instead.

## Remarks

You don't have to use options in the order shown in the syntax section.

> [!NOTE]  
> If you use the `-i` option followed by one or more extra parameters, you must use a space between the parameter and the value. This requirement is a known issue in **sqlcmd** (Go).

**sqlcmd** prints a blank line between multiple result sets in a batch. In addition, the `<x> rows affected` message doesn't appear when it doesn't apply to the running statement.

To use **sqlcmd** interactively, type `sqlcmd` at the command prompt with any one or more of the options described earlier in this article. For more information, see [Use sqlcmd](sqlcmd-use-utility.md).

> [!NOTE]  
> The options `-l`, `-Q`, `-Z`, or `-i` cause **sqlcmd** to exit after execution.

The underlying operating system determines the total length of the **sqlcmd** command line in the command environment (for example, `cmd.exe` or `bash`), including all arguments and expanded variables.

## DSN support in sqlcmd and bcp

You can specify a data source name (DSN) instead of a server name in the **sqlcmd** or `bcp -S` option (or `sqlcmd :Connect` command) if you specify `-D`. If you use the `-D` option, **sqlcmd** and **bcp** connect to the server specified in the DSN by the `-S` option.

System DSNs are stored in the `odbc.ini` file in the ODBC `SysConfigDir` directory (`/etc/odbc.ini` on standard installations). User DSNs are stored in `.odbc.ini` in a user's home directory (`~/.odbc.ini`).

On Windows systems, System and User DSNs are stored in the registry and managed via `odbcad32.exe`. **bcp** and **sqlcmd** don't support file DSNs.

For a list of entries that the driver supports, see [DSN and Connection String Keywords and Attributes](../../connect/odbc/dsn-connection-string-attribute.md).

In a DSN, only the `DRIVER` entry is required. To connect to a remote server, **sqlcmd** or **bcp** needs a value in the `SERVER` element. If the `SERVER` element is empty or not present in the DSN, **sqlcmd** or **bcp** attempt to connect to the default instance on the local system.

When you use **bcp** on Windows systems, [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and earlier versions require the SQL Native Client 11 driver (`sqlncli11.dll`), while [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions require the Microsoft ODBC Driver 17 for SQL Server driver (`msodbcsql17.dll`).

If you specify the same option in both the DSN and the **sqlcmd** or **bcp** command line, the command line option overrides the value used in the DSN. For example, if the DSN has a `DATABASE` entry and the **sqlcmd** command line includes `-d`, the value passed to `-d` is used. If you specify `Trusted_Connection=yes` in the DSN, Kerberos authentication is used; user name (`-U`) and password (`-P`), if provided, are ignored.

You can modify existing scripts that invoke `isql` to use **sqlcmd**, by defining the following alias: `alias isql="sqlcmd -D"`.

## sqlcmd best practices

Use the following practices to help maximize security and efficiency:

- Use integrated security.

- Use `-X[1]` in automated environments.

- Secure input and output files by using appropriate file system permissions.

- To increase performance, do as much as possible in one **sqlcmd** session, instead of using a series of sessions.

- Set timeout values for batch or query execution higher than the expected execution time for the batch or query.

Use the following practices to help maximize correctness:

- Use `-V 16` to log any [severity 16 level messages](../../relational-databases/errors-events/database-engine-error-severities.md#levels-of-severity). Severity 16 messages indicate general errors that you can correct.

- Check the exit code and `DOS ERRORLEVEL` variable after the process exits. **sqlcmd** returns `0` normally. Otherwise, it sets the `ERRORLEVEL` as configured by `-V`. In other words, don't expect `ERRORLEVEL` to be the same value as the error number reported from [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. The error number is a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]-specific value corresponding to the system function [@@ERROR](../../t-sql/functions/error-transact-sql.md). `ERRORLEVEL` is a **sqlcmd**-specific value to indicate why **sqlcmd** terminated. Its value is influenced by specifying the `-b` parameter.

Using `-V 16` in combination with checking the exit code and `DOS ERRORLEVEL` can help catch errors in automated environments, particularly quality gates before a production release.

## Related content

- [Check installed version of sqlcmd utility](sqlcmd-installed-version.md)
- [Download and install the sqlcmd utility](sqlcmd-download-install.md)
- [Commands in the sqlcmd utility](sqlcmd-commands.md)
- [Use sqlcmd with scripting variables](sqlcmd-use-scripting-variables.md)
- [Quickstart: Run SQL Server Linux container images with Docker](../../linux/quickstart-install-connect-docker.md)
- [Start the sqlcmd utility](sqlcmd-start-utility.md)
- [Execute T-SQL from a script file with sqlcmd](sqlcmd-run-transact-sql-script-files.md)
- [Use sqlcmd](sqlcmd-use-utility.md)
- [Connect to SQL Server with sqlcmd](sqlcmd-connect-database-engine.md)
- [Edit SQLCMD scripts with Query Editor](/ssms/scripting/sqlcmd-scripts-query-editor)
- [Create a CmdExec Job Step](/ssms/agent/create-a-cmdexec-job-step)
