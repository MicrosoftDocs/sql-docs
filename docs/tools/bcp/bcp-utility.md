---
title: Bulk Copy with bcp Utility
description: The bulk copy program (bcp) utility bulk copies data between an instance of SQL Server and a data file in a user-specified format.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: davidengel
ms.date: 06/30/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: article
ms.collection:
  - data-tools
ms.custom:
  - peer-review-program
helpviewer_keywords:
  - "bcp utility [SQL Server]"
  - "exporting data"
  - "row exporting [SQL Server]"
  - "copying data [SQL Server], bcp utility"
  - "command prompt utilities [SQL Server], bcp"
  - "tables [SQL Server], importing data"
  - "column importing [SQL Server]"
  - "bcp utility [SQL Server], command options"
  - "file exporting [SQL Server]"
  - "bulk copy [SQL Server]"
  - "bcp utility [SQL Server], about bcp utility"
  - "tables [SQL Server], exporting data"
  - "row importing [SQL Server]"
  - "importing data, bcp utility"
  - "file importing [SQL Server]"
  - "column exporting [SQL Server]"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb || =fabric"
---
# bcp utility

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

The bulk copy program utility (**`bcp`**) bulk copies data between an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a data file in a user-specified format.

Use the **`bcp`** utility to import large numbers of new rows into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tables or to export data out of tables into data files. Except when used with the `queryout` option, the utility requires no knowledge of [!INCLUDE [tsql](../../includes/tsql-md.md)]. To import data into a table, you must either use a format file created for that table, or understand the structure of the table and the types of data that are valid for its columns.

For more information about which version of **`bcp`** is installed on your system, system requirements, and how to get **`bcp`**, see [Download and install the bcp utility](bcp-download-install.md).

> [!NOTE]  
> **`bcp`** data files don't include any schema or format information. If you use **`bcp`** to back up data, and then later drop or alter the source table, you need either an identical table definition or a format file to import the data back.

For the syntax conventions that are used for the **`bcp`** syntax, see [Transact-SQL syntax conventions (Transact-SQL)](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## How to use bcp

For information about how to use **`bcp`**, including example commands, see [How to use the bcp utility](bcp-use-utility.md).

## Use bcp on Linux and macOS

For information on how to install the command-line tools on macOS and Linux, see [Install the sqlcmd and bcp SQL Server command-line tools on Linux](../../linux/install-upgrade/setup-tools.md).

### Considerations for bcp on Linux and macOS

- The field terminator is a tab (`\t`).

- The line terminator is a newline (`\n`).

- For SQL Server to SQL Server transfers, use native format (`-n`). Use character format (`-c`) only when the data crosses into a non-SQL Server system or when the data file shouldn't contain extended characters. For more information, see [Character mode and native mode best practices](bcp-use-utility.md#character-mode--c-and-native-mode--n-best-practices).

- You must quote or escape a backslash (`\`) on a command-line argument. For example, to specify a newline as a custom row terminator, use one of the following mechanisms:

  - `-r\\n`
  - `-r"\n"`
  - `-r'\n'`

## TDS 8.0 support

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces TDS 8.0 support for the **`bcp`** utility.

## Syntax

```console
bcp [ database_name. ] schema. { table_name | view_name | "query" }
    { in data_file | out data_file | queryout data_file | format nul }

    [ -a packet_size ]
    [ -b batch_size ]
    [ -c ]
    [ -C { ACP | OEM | RAW | code_page } ]
    [ -d database_name ]
    [ -D ]
    [ -e err_file ]
    [ -E ]
    [ -f format_file ]
    [ -F first_row ]
    [ -G Microsoft Entra authentication ]
    [ -H hostname_in_certificate ]
    [ -h"hint [ , ...n ] " ]
    [ -i input_file ]
    [ -J server_certificate ]
    [ -k ]
    [ -K application_intent ]
    [ -l login_timeout ]
    [ -L last_row ]
    [ -m max_errors ]
    [ -n ]
    [ -N ]
    [ -o output_file ]
    [ -P password ]
    [ -q ]
    [ -r row_term ]
    [ -R ]
    [ -S [ server_name [ \instance_name ] ] ]
    [ -t field_term ]
    [ -T ]
    [ -U login_id ]
    [ -u ]
    [ -v ]
    [ -V (80 | 90 | 100 | 110 | 120 | 130 | 140 | 150 | 160 | 170) ]
    [ -w ]
    [ -x ]
    [ -Y [ s | m | o ] ]
    [ -z[0|1] ]
```

## Command-line options

The following table lists the command-line options available in **`bcp`**, and which operating systems they support.

| Command-line option | Supported on Windows | Supported on Linux and macOS |
| --- | --- | --- |
| [**Object and transfer mode**](#object-and-transfer-mode) | | |
| [\[*database_name*.\]](#database_name) [*schema*](#schema).{[*table_name*](#table_name) \| [*view_name*](#view_name) \| ["*query*"](#query)} | Yes | Yes |
| {[in](#in) [*data_file*](#data_file) \| [out](#out) [*data_file*](#data_file) \| [queryout](#queryout) [*data_file*](#data_file) \| [format](#format) nul} | Yes | Yes |
| [-q](#-q) | Yes | Yes |
| [**Connection and authentication**](#connection-and-authentication) | | |
| [-S \[*server_name*\[\\*instance_name*\]\]](#-s-server_nameinstance_name) | Yes | Yes |
| [-d *database_name*](#-d-database_name) | Yes | Yes |
| [-U *login_id*](#-u-login_id) | Yes | Yes |
| [-P *password*](#-p-password) | Yes | Yes |
| [-G Microsoft Entra authentication](#-g) | Yes | Yes |
| [-D](#-d) | Yes | Yes |
| [-K *application_intent*](#-k-application_intent) | Yes | Yes |
| [-l *login_timeout*](#-l-login_timeout) | Yes | Yes |
| [-H *hostname_in_certificate*](#-h-hostname_in_certificate) | Yes | Yes |
| [-J *server_certificate*](#-j-server_certificate) | Yes | Yes |
| [-T](#-t) | Yes | Yes |
| [-Y\[s\| m\| o\]](#-ysmo) | Yes <sup>1</sup> | Yes <sup>1</sup> |
| [**Data representation**](#data-representation) | | |
| [-c](#-c) | Yes | Yes |
| [-C { ACP \| OEM \| RAW \| *code_page* }](#-c--acp--oem--raw--code_page-) | Yes | No |
| [-n](#-n-native) | Yes | Yes |
| [-N](#-n-unicode) | Yes | No |
| [-w](#-w) | Yes | Yes |
| [-z[0\|1]](#-z) | No | Yes <sup>2</sup> |
| [**Format files**](#format-files) | | |
| [-f *format_file*](#-f-format_file) | Yes | Yes |
| [-x](#-x) | Yes | No |
| [**Batching and performance**](#batching-and-performance) | | |
| [-a *packet_size*](#-a-packet_size) | Yes | Yes |
| [-b *batch_size*](#-b-batch_size) | Yes | Yes |
| [-h"*hint* \[,...*n*\]"](#-h-hints---n) | Yes | No |
| [-m *max_errors*](#-m-max_errors) | Yes | Yes |
| [-F *first_row*](#-f-first_row) | Yes | Yes |
| [-L *last_row*](#-l-last_row) | Yes | Yes |
| [-r *row_term*](#-r-row_term) | Yes | Yes |
| [-t *field_term*](#-t-field_term) | Yes | Yes |
| [**Value handling**](#value-handling) | | |
| [-k](#-k) | Yes | Yes |
| [-E](#-e) | Yes | Yes |
| [**File I/O and logging**](#file-io-and-logging) | | |
| [-i *input_file*](#-i-input_file) | Yes | No |
| [-o *output_file*](#-o-output_file) | Yes | No |
| [-e *err_file*](#-e-err_file) | Yes | Yes |
| [**Compatibility and versioning**](#compatibility-and-versioning) | | |
| [-V (80 \| 90 \| 100 \| 110 \| 120 \| 130 \| 140 \| 150 \| 160 \| 170 )](#-v--80--90--100--110--120--130--140--150--160--170-) | Yes | No |
| [-u](#-u) | Yes <sup>1</sup> | Yes <sup>1</sup> |
| [**Miscellaneous options**](#miscellaneous-options) | | |
| [-R](#-r) | Yes | Yes |
| [-v](#-v) | Yes | Yes |

<sup>1</sup> Requires **`bcp`** version 18 or later, which ships with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].  
<sup>2</sup> ODBC 18.6.1.1 and later versions.

### Object and transfer mode

#### *database_name*

The name of the database that contains the specified table or view. If you don't specify this parameter, the default database for the user is used.

You can also explicitly specify the database name with `-d`.

#### *schema*

The name of the owner of the table or view. *schema* is optional if the user performing the operation owns the specified table or view. If you don't specify *schema*, and the user performing the operation doesn't own the specified table or view, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message, and the operation is canceled.

#### *table_name*

The name of the destination table when importing data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (`in`), and the source table when exporting data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (`out`).

#### *view_name*

The name of the destination view when copying data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (`in`), and the source view when copying data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (`out`). When used as a destination (`in`), a view is only supported if all of its columns refer to the same table. This restriction doesn't apply when the view is used as a source (`out`). For more information on the restrictions for copying data into views, see [INSERT](../../t-sql/statements/insert-transact-sql.md).

#### "*query*"

A [!INCLUDE [tsql](../../includes/tsql-md.md)] query that returns a result set. If the query returns multiple result sets, only the first result set is copied to the data file; subsequent result sets are ignored. Use double quotation marks around the query and single quotation marks around anything embedded in the query. You must also specify `queryout` when bulk copying data from a query.

The query can reference a stored procedure as long as all tables referenced inside the stored procedure exist before executing the **`bcp`** statement. For example, if the stored procedure generates a temp table, the **`bcp`** statement fails because the temp table is available only at run time and not at statement execution time. In this case, consider inserting the results of the stored procedure into a table and then use **`bcp`** to copy the data from the table into a data file.

#### in

Copies data from a file into the database table or view. Specifies the direction of the bulk copy.

#### out

Copies data from the database table or view to a file. Specifies the direction of the bulk copy.

If you specify an existing file, the file is overwritten. When the **`bcp`** utility extracts data, it represents an empty string as a null, and a null string as an empty string.

#### *data_file*

The full path of the data file. When you bulk import data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the data file contains the data to copy into the specified table or view. When you bulk export data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the data file contains the data copied from the table or view. The path can have from 1 through 255 characters. The data file can contain a maximum of 2^63 - 1 rows.

#### queryout

Copies data from a query and must be specified only when bulk copying data from a query.

#### format

Creates a format file based on the option specified (`-n`, `-c`, `-w`, or `-N`) and the table or view delimiters. When bulk copying data, the **`bcp`** command can refer to a format file, which saves you from reentering format information interactively. The `format` option requires the `-f` option; creating an XML format file also requires the `-x` option. For more information, see [Create a format file with bcp (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md). You must specify `nul` as the value (`format nul`).

#### -q

Executes the `SET QUOTED_IDENTIFIER ON` statement in the connection between the **`bcp`** utility and an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use this option to specify a database, owner, table, or view name that contains a space or a single quotation mark. Enclose the entire three-part table or view name in quotation marks (`""`).

To specify a database name that contains a space or single quotation mark, you must use the `-q` option.

`-q` doesn't apply to values passed to `-d`.

For more information, see the [Remarks](#remarks) section in this article.

### Connection and authentication

#### -S \[*server_name*\[\\*instance_name*\]\]

Specifies the name of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance to connect to, or if you use `-D`, a DSN.

If you don't specify a server, the **`bcp`** utility connects to the default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the local computer. You need this option when running a **`bcp`** command from a remote computer on the network or a local named instance. To connect to the default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a server, specify only *server_name*. To connect to a named instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], specify `<server_name>\<instance_name>`.

#### -d *database_name*

Specifies the database to connect to. By default, **`bcp`** connects to your default database. If you specify `-d <database_name>` and a three-part name (database_name.schema.table, passed as the first parameter to **`bcp`**), an error occurs because you can't specify the database name twice. If *database_name* begins with a hyphen (`-`) or a forward slash (`/`), don't add a space between `-d` and the database name.

#### -U *login_id*

Specifies the login ID used to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### -P *password*

Specifies the password for the login ID. If you don't use this option, the **`bcp`** command prompts for a password. If you use this option at the end of the command prompt without a password, **`bcp`** uses the default password (`NULL`).

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

To mask your password, don't specify the `-P` option along with the `-U` option. Instead, after specifying **`bcp`** along with the `-U` option and other switches (don't specify `-P`), press the **Enter** key, and the command prompts you for a password. This method ensures that your password is masked when it's entered.

If *password* begins with a hyphen (`-`) or a forward slash (`/`), don't add a space between `-P` and the *password* value.

On Linux and macOS, when used with the `-G` option without `-U`, `-P` specifies a file that contains a Microsoft Entra access token (v17.8 and later versions). The token file should be in UTF-16LE (no BOM) format. For more information, see [Authenticate with Microsoft Entra ID in bcp](bcp-authentication.md).

#### -G

**Applies to**: Azure SQL Database, Azure SQL Managed Instance, [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], Azure Synapse Analytics, and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

This switch is used by the client to specify that the user is authenticated with Microsoft Entra ID. The `-G` switch requires [**bcp** version 14.0.3008.27](https://go.microsoft.com/fwlink/?LinkID=825643) or later versions. To determine your version, execute `bcp -v`. For more information, see [Use Microsoft Entra authentication with SQL Database or Azure Synapse Analytics](/azure/sql-database/sql-database-aad-authentication) or [Authentication in SQL database in Microsoft Fabric](/fabric/database/sql/authentication).

For full details about Microsoft Entra authentication in **`bcp`**, see [Authenticate with Microsoft Entra ID in bcp](bcp-authentication.md).

#### -D

Causes the value passed to the `bcp -S` option to be interpreted as a data source name (DSN). `-D` can appear anywhere on the command line; ordering relative to `-S` doesn't matter.

A DSN can be used to:

- Embed driver options to simplify command lines.
- Enforce driver options that aren't otherwise accessible from the command line, such as `MultiSubnetFailover`.
- Help protect sensitive credentials from being discoverable as command-line arguments.

For more information, see [DSN support in sqlcmd and bcp](../sqlcmd/sqlcmd-utility.md#dsn-support-in-sqlcmd-and-bcp).

#### -K *application_intent*

Declares the application workload type when connecting to a server. The only value that is possible is `ReadOnly`. If you don't specify `-K`, the **`bcp`** utility doesn't support connectivity to a secondary replica in an Always On availability group. For more information, see [Offload read-only workload to secondary replica of an Always On availability group](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).

#### -l *login_timeout*

Specifies a login timeout. The `-l` option specifies the number of seconds before a login to SQL Server times out when you try to connect to a server. The default login timeout is 15 seconds. The login timeout must be a number between 0 and 65534. If the value you supply isn't numeric or doesn't fall into that range, **`bcp`** generates an error message. A value of 0 specifies an infinite timeout.

#### -H *hostname_in_certificate*

Specifies the host name to use when validating the server certificate. Use this option when the server name used for connection doesn't match the certificate subject name.

#### -J *server_certificate*

Specifies the path to a PEM file that contains the server certificate to trust for the connection.

#### -T

Specifies that the **`bcp`** utility connects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with a trusted connection using integrated security. The security credentials of the network user, *login_id*, and *password* aren't required. If you don't specify `-T`, you need to specify `-U` and `-P` to successfully connect.

> [!IMPORTANT]  
> Use `-T` only when connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with a Windows-integrated trusted connection. When connecting to Azure SQL Database or Azure Synapse Analytics, `-T` (Windows integrated authentication) isn't supported. For Microsoft Entra authentication against Azure services or [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, use `-G` (see [Authenticate with Microsoft Entra ID in bcp](bcp-authentication.md)).

#### -Y[s|m|o]

**Applies to**: **`bcp`** version 18 and later versions, which ships with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

Specifies whether connections use TLS encryption over the network. `-Y` can be `o` (for `Optional`), `m` (for `Mandatory`, the default), or `s` (for `Strict`). If you don't include `-Y`, then `-Ym` (for `Mandatory`) is the default.

#### -u

**Applies to**: **`bcp`** version 18 and later versions, which ships with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

Trust server certificate. When used with the encryption option for the connection, enables encryption using a self-signed server certificate.

### Data representation

#### -c

Performs the operation using a character data type. This option doesn't prompt for each field. It uses **char** as the storage type, without prefixes, and uses `\t` (tab character) as the field separator and `\r\n` (newline character) as the row terminator. `-c` isn't compatible with `-w`.

For more information, see [Use character format to import or export data (SQL Server)](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md).

For best practices, see [Character mode and native mode best practices](bcp-use-utility.md#character-mode--c-and-native-mode--n-best-practices).

#### -C { ACP | OEM | RAW | *code_page* }

**Applies to**: Windows only. Not supported on Linux and macOS.

Specifies the code page of the data in the data file. *code_page* is relevant only if the data contains **char**, **varchar**, or **text** columns with character values greater than 127 or less than 32.

Specify a collation name for each column in a format file, except when you want the 65001 option to have priority over the collation or code page specification.

| Code page value | Description |
| --- | --- |
| `ACP` | [!INCLUDE [vcpransi](../../includes/vcpransi-md.md)]/Microsoft Windows (ISO 1252). |
| `OEM` | Default code page used by the client. This code page is the default if you don't specify `-C`. |
| `RAW` | No conversion from one code page to another occurs. This option is the fastest because no conversion occurs. |
| `<code_page>` | Specific code page number, such as 850.<br /><br />Versions before [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] don't support code page 65001 (UTF-8 encoding). Versions beginning with 13 can import UTF-8 encoding to earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |

<a id="-n-native"></a>

#### -n

Performs the bulk-copy operation using the native (database) data types of the data. This option doesn't prompt for each field. It uses the native values.

For more information, see [Use native format to import or export data (SQL Server)](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md).

For best practices, see [Character mode and native mode best practices](bcp-use-utility.md#character-mode--c-and-native-mode--n-best-practices).

<a id="-n-unicode"></a>

#### -N

**Applies to**: Windows only. Not supported on Linux and macOS.

Performs the bulk-copy operation using the native (database) data types of the data for noncharacter data, and Unicode characters for character data. This option offers a higher performance alternative to the `-w` option, and is intended for transferring data from one instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to another using a data file. It doesn't prompt for each field. Use this option when you're transferring data that contains ANSI extended characters and you want to take advantage of the performance of native mode.

For more information, see [Use Unicode Native Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md).

If you export and then import data to the same table schema by using **`bcp`** with `-N`, you might see a truncation warning if there's a fixed length, non-Unicode character column (for example, **char(10)**).

You can ignore the warning. One way to resolve this warning is to use `-n` instead of `-N`.

#### -w

Performs the bulk copy operation by using Unicode characters. This option doesn't prompt for each field. It uses **nchar** as the storage type, no prefixes, `\t` (tab character) as the field separator, and `\n` (newline character) as the row terminator. `-w` isn't compatible with `-c`.

For more information, see [Use Unicode character format to import or export data (SQL Server)](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md).

#### -z

**Applies to**: **`bcp`** (ODBC), Linux and macOS only. Windows isn't supported.

Enables **vector** data type support in the **`bcp`** utility. This feature is currently disabled by default. When disabled, vector data is imported or exported as JSON float array strings. When enabled, and when connecting to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, vector data is imported or exported in native **vector** binary.

Use `-z0` for `float32` vector support and `-z1` for `float16` vector support. Currently, ODBC doesn't support `-z1`.

### Format files

#### -f *format_file*

Specifies the full path of a format file. The meaning of this option depends on the environment in which it's used, as follows:

- If you use `-f` with the `format` option, the specified *format_file* is created for the specified table or view. To create an XML format file, also specify the `-x` option. For more information, see [Create a format file with bcp (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md).

- If you use `-f` with the `in` or `out` option, it requires an existing format file.

  > [!NOTE]  
  > Using a format file with the `in` or `out` option is optional. If you don't specify the `-f` option and you don't specify `-n`, `-c`, `-w`, or `-N`, the command prompts for format information and lets you save your responses in a format file. The default file name is `bcp.fmt`.

If *format_file* begins with a hyphen (`-`) or a forward slash (`/`), don't include a space between `-f` and the *format_file* value.

#### -x

**Applies to**: Windows only. Not supported on Linux and macOS.

Use this option with the `format` and `-f` *format_file* options. It generates an XML-based format file instead of the default non-XML format file. The `-x` option doesn't work when importing or exporting data. It generates an error if used without both `format` and `-f` *format_file*.

### Batching and performance

#### -a *packet_size*

Specifies the number of bytes per network packet that the client sends to and receives from the server. Set this server configuration option by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the `sp_configure` system stored procedure. However, you can override the server configuration option on an individual basis by using this option. *packet_size* can be from 4,096 bytes to 65,535 bytes. The default is `4096`.

Increasing the packet size can improve the performance of bulk-copy operations. If you request a larger packet but the server can't grant it, the default is used. The performance statistics that the **`bcp`** utility generates show the packet size used.

#### -b *batch_size*

Specifies the number of rows per batch of imported data. Each batch is imported and logged as a separate transaction that imports the whole batch before being committed. By default, **`bcp`** imports all the rows in the data file as one batch. To distribute the rows among multiple batches, specify a *batch_size* that is smaller than the number of rows in the data file. If the transaction for any batch fails, only insertions from the current batch are rolled back. Batches already imported by committed transactions aren't affected by a later failure.

The `-b` and the `-h "ROWS_PER_BATCH=<bb>"` hint are mutually exclusive. Use `-b` when you want **`bcp`** to control batching explicitly, or use `ROWS_PER_BATCH` to hint the server optimizer when sending the data as a single transaction.

#### -h "*hints* [, ... *n*]"

**Applies to**: Windows only. Not supported on Linux and macOS.

Specifies the hint or hints to use during a bulk import of data into a table or view.

- **ORDER (*column* [ASC | DESC] [, ...*n*])**

  The sort order of the data in the data file. Bulk import performance improves if the data being imported is sorted according to the clustered index on the table, if any. If the data file is sorted in a different order, that is, other than the order of a clustered index key, or if there's no clustered index on the table, the `ORDER` clause is ignored. The column names you supply must be valid column names in the destination table. By default, **`bcp`** assumes the data file is unordered. For optimized bulk import, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] also validates that the imported data is sorted.

- **ROWS_PER_BATCH = *bb***

  Number of rows of data per batch (as *bb*). Used when you don't specify `-b`, resulting in the entire data file being sent to the server as a single transaction. The server optimizes the bulkload according to the value *bb*. By default, `ROWS_PER_BATCH` is unknown.

- **KILOBYTES_PER_BATCH = *cc***

  Approximate number of kilobytes of data per batch (as *cc*). By default, `KILOBYTES_PER_BATCH` is unknown.

- **TABLOCK**

  Specifies that a bulk update table-level lock is acquired during the bulkload operation; otherwise, a row-level lock is acquired. This hint significantly improves performance because holding a lock during the bulk-copy operation reduces lock contention on the table. You can load a table concurrently from multiple clients if the table has no indexes and `TABLOCK` is specified. By default, the table option `table lock on bulkload` determines locking behavior. For more information, see [sp_tableoption](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).

  > [!NOTE]  
  > If the target table is clustered columnstore index, `TABLOCK` hint isn't required for loading by multiple concurrent clients because each concurrent thread is assigned a separate rowgroup within the index and loads data into it. For more information, see [Columnstore indexes: overview](../../relational-databases/indexes/columnstore-indexes-overview.md).

- **CHECK_CONSTRAINTS**

  Specifies that all constraints on the target table or view must be checked during the bulk-import operation. Without the `CHECK_CONSTRAINTS` hint, any `CHECK` and `FOREIGN KEY` constraints are ignored, and after the operation the constraint on the table is marked as not-trusted.

  > [!NOTE]  
  > `UNIQUE`, `PRIMARY KEY`, and `NOT NULL` constraints are always enforced.

  At some point, you need to check the constraints on the entire table. If the table was nonempty before the bulk import operation, the cost of revalidating the constraint can exceed the cost of applying `CHECK` constraints to the incremental data. Therefore, you can usually enable constraint checking during an incremental bulk import.

  A situation in which you might want constraints disabled (the default behavior) is if the input data contains rows that violate constraints. By disabling `CHECK` constraints, you can import the data and then use [!INCLUDE [tsql](../../includes/tsql-md.md)] statements to remove data that isn't valid.

  > [!NOTE]  
  > The `-m` *max_errors* switch doesn't apply to constraint checking.

- **FIRE_TRIGGERS**

  When you specify this option with the *in* argument, any insert triggers defined on the destination table run during the bulk-copy operation. If you don't specify `FIRE_TRIGGERS`, no insert triggers run. `FIRE_TRIGGERS` is ignored for the `out`, `queryout`, and `format` arguments.

#### -m *max_errors*

Specifies the maximum number of syntax errors that can occur before the **`bcp`** operation is canceled. A syntax error implies a data conversion error to the target data type. The *max_errors* total excludes any errors that the server can detect only, such as constraint violations.

A row that the **`bcp`** utility can't copy is ignored and counted as one error. If you don't include this option, the default value is 10.

> [!NOTE]  
> The `-m` option doesn't apply when converting the **money** or **bigint** data types.

#### -F *first_row*

Specifies the number of the first row to export from a table or import from a data file. This parameter requires a value greater than (`>`) 0 but less than (`<`) or equal to the total number of rows. If you don't specify this parameter, the default is the first row of the file.

*first_row* can be a positive integer with a value up to 2^63-1. `-F` *first_row* uses 1-based numbering.

#### -L *last_row*

Specifies the number of the last row to export from a table or import from a data file. This parameter requires a value greater than (`>`) 0 but less than (`<`) or equal to the number of the last row. If you don't specify this parameter, the default is the last row of the file.

*last_row* can be a positive integer with a value up to 2^63-1.

#### -r *row_term*

Specifies the row terminator. The default is `\n` (newline character). Use this parameter to override the default row terminator. For more information, see [Specify field and row terminators (SQL Server)](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).

If you specify the row terminator in hexadecimal notation in a **`bcp`** command, the value is truncated at `0x00`. For example, if you specify `0x410041`, `0x41` is used.

If *row_term* begins with a hyphen (`-`) or a forward slash (`/`), don't include a space between `-r` and the *row_term* value.

#### -t *field_term*

Specifies the field terminator. The default is `\t` (tab character). Use this parameter to override the default field terminator. For more information, see [Specify field and row terminators (SQL Server)](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).

If you specify the field terminator in hexadecimal notation in a **`bcp`** command, the value is truncated at `0x00`. For example, if you specify `0x410041`, `0x41` is used.

If *field_term* begins with a hyphen (`-`) or a forward slash (`/`), don't include a space between `-t` and the *field_term* value.

### Value handling

#### -k

Specifies that empty columns keep a null value during the operation, rather than inserting any default values for the columns. For more information, see [Keep nulls or default values during bulk import (SQL Server)](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md).

#### -E

Specifies that the operation uses identity values in the imported data file for the identity column. If you don't specify `-E`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ignores the identity values for this column in the data file being imported and automatically assigns unique values based on the seed and increment values specified during table creation. For more information, see [DBCC CHECKIDENT](../../t-sql/database-console-commands/dbcc-checkident-transact-sql.md).

If the data file doesn't contain values for the identity column in the table or view, use a format file to specify that the identity column in the table or view should be skipped when importing data. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically assigns unique values for the column.

The `-E` option has a special permissions requirement. For more information, see "[Remarks](#remarks)" later in this article.

### File I/O and logging

#### -i *input_file*

**Applies to**: Windows only. Not supported on Linux and macOS.

Specifies the name of a response file. The file contains the responses to the command prompt questions for each data field when you perform a bulk copy operation using interactive mode (`-n`, `-c`, `-w`, or `-N` not specified).

If *input_file* begins with a hyphen (`-`) or a forward slash (`/`), don't include a space between `-i` and the *input_file* value.

#### -o *output_file*

**Applies to**: Windows only. Not supported on Linux and macOS.

Specifies the name of a file that receives output redirected from the command prompt.

If *output_file* begins with a hyphen (`-`) or a forward slash (`/`), don't include a space between `-o` and the *output_file* value.

#### -e *err_file*

Specifies the full path of an error file used to store any rows that the **`bcp`** utility can't transfer from the file to the database. Error messages from the **`bcp`** command go to the workstation of the user. If you don't use this option, an error file isn't created.

If *err_file* begins with a hyphen (`-`) or a forward slash (`/`), don't include a space between `-e` and the *err_file* value.

### Compatibility and versioning

#### -V { 80 | 90 | 100 | 110 | 120 | 130 | 140 | 150 | 160 | 170 }

**Applies to**: Windows only. Not supported on Linux and macOS.

Performs the bulk-copy operation using data types from an earlier version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option doesn't prompt for each field; it uses the default values.

- `80` = [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]
- `90` = [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]
- `100` = [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)]
- `110` = [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)]
- `120` = [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)]
- `130` = [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)]
- `140` = [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]
- `150` = [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]
- `160` = [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]
- `170` = [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]

For example, to generate data for types that [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] doesn't support, but were introduced in later versions, use the `-V80` option.

For more information, see [Import native and character format data from earlier versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md).

### Miscellaneous options

#### -R

Specifies that the **`bcp`** utility bulk copies currency, date, and time data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using the regional format defined for the locale setting of the client computer. By default, **`bcp`** ignores regional settings.

#### -v

Reports the **`bcp`** utility version number and copyright.

## Remarks

- The **`bcp`** utility displays only the first 512 bytes of an error message.
- The **`bcp`** utility supports native data files that are compatible with all supported versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **`bcp`** is currently in preview in [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].
- **`bcp`** can't import data in [!INCLUDE [fabric-se](../../includes/fabric-se.md)].

## Permissions

A `bcp out` operation requires `SELECT` permission on the source table.

A `bcp in` operation minimally requires `SELECT` and `INSERT` permissions on the target table. In addition, `ALTER TABLE` permission is required if any of the following conditions are true:

- Constraints exist and the `CHECK_CONSTRAINTS` hint isn't specified.

  Disabling constraints is the default behavior. To enable constraints explicitly, use the `-h` option with the `CHECK_CONSTRAINTS` hint.

- Triggers exist and the `FIRE_TRIGGERS` hint isn't specified.

  By default, triggers aren't fired. To fire triggers explicitly, use the `-h` option with the `FIRE_TRIGGERS` hint.

- You use the `-E` option to import identity values from a data file.

## Related content

- [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sp_tableoption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md)
- [Format files to import or export data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)

[!INCLUDE [get-help-options](../../includes/paragraph-content/get-help-options.md)]
