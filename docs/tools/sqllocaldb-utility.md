---
title: "SqlLocalDB Utility"
description: The SqlLocalDB utility command line tool allows users and developers to create and manage an instance of SQL Server Express LocalDB.
author: cwebb-msft
ms.author: chweb
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
helpviewer_keywords:
  - "SqlLocalDB utility [SQL Server]"
  - "local database runtime utility"
  - "LocalDB, SqlLocalDB Utility"
---
# SqlLocalDB utility

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

Use the **SqlLocalDB** utility to create an instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB. The **SqlLocalDB** utility (SqlLocalDB.exe) is a command line tool to enable users and developers to create and manage an instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB. For information about how to use LocalDB, see [SQL Server Express LocalDB](../database-engine/configure-windows/sql-server-express-localdb.md).

## Syntax

```console
SqlLocalDB.exe
{
      [ create   | c ] <instance-name> <instance-version> [ -s ]
    | [ delete   | d ] <instance-name>
    | [ start    | s ] <instance-name>
    | [ stop     | p ] <instance-name>  [ -i ] [ -k ]
    | [ share    | h ] [ "<user_SID>" | "<user_account>" ] "<private-name>" "<shared-name>"
    | [ unshare  | u ] "<shared-name>"
    | [ info     | i ] <instance-name>
    | [ versions | v ]
    | [ trace    | t ] [ on | off ]
    | [ help     | -? ]
}
```

## Arguments

#### [ create | c ] *\<instance-name>* *\<instance-version>* [ -s ]

Creates a new of instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB. **SqlLocalDB** uses the version of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] binaries specified by *\<instance-version>* argument. The version number is specified in numeric format with at least one decimal. The minor version numbers (service packs) are optional. For example the following two version numbers are both acceptable: 11.0, or 11.0.1186. The specified version must be installed on the computer. If not specified, the version number defaults to the version of the **SqlLocalDB** utility. Adding `-s` starts the new instance of LocalDB.

#### [ share | h ]

Shares the specified private instance of LocalDB using the specified shared name. If the user SID or account name is omitted, it defaults to the current user.

#### [ unshare | u ]

Stops the sharing of the specified shared instance of LocalDB.

#### [ delete | d ] *\<instance-name>*

Deletes the specified instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB.

#### [ start | s ] "*\<instance-name>*"

Starts the specified instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB. When successful the statement returns the named pipe address of the LocalDB.

#### [ stop | p ] *\<instance-name>* [ -i ] [ -k ]

Stops the specified instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB. Adding `-i` requests the instance shutdown with the `NOWAIT` option. Adding `-k` kills the instance process without contacting it.

#### [ info | i ] [ *\<instance-name>* ]

Lists all instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB owned by the current user.

*\<instance-name>* returns the name, version, state (Running or Stopped), last start time for the specified instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB, and the local pipe name of the LocalDB.

#### [ trace | t ] *{ on | off }*

`trace on` enables tracing for the **SqlLocalDB** API calls for the current user. `trace off` disables tracing.

#### `-?`

Returns brief descriptions of each **SqlLocalDB** option.

## Remarks

The *instance name* argument must follow the rules for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] identifiers or it must be enclosed in double quotes.

Executing **SqlLocalDB** without arguments returns the help text.

Operations other than start can only be performed on an instance belonging to the currently signed-in user. A `SQLLOCALDB` instance, when shared, can only be started and stopped by the owner of the instance.

## Examples

### A. Create an instance of LocalDB

The following example creates an instance of [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] LocalDB named `DEPARTMENT` using the [!INCLUDE [ssnoversion](../includes/ssnoversion-md.md)] binaries and starts the instance.

```console
SqlLocalDB.exe create "DEPARTMENT" 12.0 -s
```

### B. Work with a shared instance of LocalDB

Open a command prompt using Administrator privileges. Replace `<password>` with a valid password.

```console
SqlLocalDB.exe create "DeptLocalDB"
SqlLocalDB.exe share "DeptLocalDB" "DeptSharedLocalDB"
SqlLocalDB.exe start "DeptLocalDB"
SqlLocalDB.exe info "DeptLocalDB"
REM The previous statement outputs the Instance pipe name for the next step
sqlcmd -S np:\\.\pipe\LOCALDB#<use your pipe name>\tsql\query
CREATE LOGIN NewLogin WITH PASSWORD = '<password>';
GO
CREATE USER NewLogin;
GO
EXIT
```

Execute the following code to connect to the shared instance of LocalDB using the `NewLogin` login. Replace `<password>` with a valid password.

```console
sqlcmd -S (localdb)\.\DeptSharedLocalDB -U NewLogin -P <password>
```

## Related content

- [SQL Server Express LocalDB](../database-engine/configure-windows/sql-server-express-localdb.md)
- [Command-Line management tool: SqlLocalDB.exe](../relational-databases/express-localdb-instance-apis/command-line-management-tool-sqllocaldb-exe.md)
