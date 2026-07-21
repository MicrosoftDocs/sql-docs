---
title: Check Installed Version of sqlcmd Utility
description: Learn how to identify which variant of sqlcmd utility is installed on your system (the go-sqlcmd, or original ODBC variant).
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dlevy
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: install-set-up-deploy
ms.collection:
  - data-tools
ms.custom:
  - linux-related-content
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
# Check installed version of sqlcmd utility

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

The [sqlcmd utility](sqlcmd-utility.md) lets you enter Transact-SQL statements, system procedures, and script files.

## sqlcmd variants

There are two variants of **sqlcmd**:

- **sqlcmd** (Go): The `go-mssqldb`-based **sqlcmd**, sometimes styled as **go-sqlcmd**. This version is a standalone tool you can download independently of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. It runs on Windows, macOS, Linux, and in containers.

- **sqlcmd** (ODBC): The platform-aligned, ODBC-based **sqlcmd**, available with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or the Microsoft Command Line Utilities, and part of the `mssql-tools` package on Linux. It also runs on Windows, macOS, Linux, and in containers.

To determine your installed variant and version, run the following statement at the command line:

::: zone pivot="cs1-bash"

```bash
sqlcmd "-?"
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd "-?"
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -?
```

::: zone-end

## sqlcmd (Go)

If you're using the new version of **sqlcmd** (Go), the output is similar to the following example:

```output
Version: 1.8.2
```

You can use `sqlcmd --version` to determine which version is installed. You should have at least version 1.0.0 installed.

## sqlcmd (ODBC)

If you're using **sqlcmd** (ODBC), the output is similar to the following example:

```output
Microsoft (R) SQL Server Command Line Tool
Version 16.0.4025.1 NT
Copyright (C) 2022 Microsoft Corporation. All rights reserved.
```

You might have several versions of **sqlcmd** (ODBC) installed on your computer. Be sure you're using the correct version. You should have at least version 15.0.4298.1 installed.

Always Encrypted (`-g`) and Microsoft Entra authentication (`-G`) require at least version 13.1.

## Remarks

Installing **sqlcmd** (Go) via a package manager replaces **sqlcmd** (ODBC) with **sqlcmd** (Go) in your environment path. You must close and reopen any current command line sessions for this change to take effect. **sqlcmd** (ODBC) isn't removed, and can still be used by specifying the full path to the executable.

You can also update your `PATH` variable to indicate which takes precedence. To do so in Windows 11, open **System settings** and go to **About > Advanced system settings**. When **System Properties** opens, select the **Environment Variables** button. In the lower half, under **System variables**, select **Path** and then select **Edit**. If the location **sqlcmd** (Go) is saved to (`C:\Program Files\sqlcmd` is default) is listed before `C:\Program Files\Microsoft SQL Server\<version>\Tools\Binn`, then **sqlcmd** (Go) is used. You can reverse the order to make **sqlcmd** (ODBC) the default again.

## Related content

- [sqlcmd utility](sqlcmd-utility.md)
- [Download and install the sqlcmd utility](sqlcmd-download-install.md)
- [Commands in the sqlcmd utility](sqlcmd-commands.md)
- [Learn more about sqlcmd (Go) utility on GitHub](https://github.com/microsoft/go-sqlcmd)
- [Start the sqlcmd utility](sqlcmd-start-utility.md)
- [Execute T-SQL from a script file with sqlcmd](sqlcmd-run-transact-sql-script-files.md)
- [Use sqlcmd](sqlcmd-use-utility.md)
