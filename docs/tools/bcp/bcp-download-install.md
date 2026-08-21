---
title: Download and Install the bcp Utility
description: Learn how to download and install the bulk copy program (bcp) utility on Windows, Linux, and macOS.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon
ms.date: 06/30/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - linux-related-content
  - peer-review-program
helpviewer_keywords:
  - "bcp utility [SQL Server], download"
  - "bcp utility [SQL Server], install"
  - "command prompt utilities [SQL Server], bcp"
  - "Microsoft Command Line Utilities for SQL Server"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb || =fabric"
---
# Download and install the bcp utility

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

The [bulk copy program utility (bcp)](bcp-utility.md) bulk copies data between an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a data file in a user-specified format.

- For detailed information about using **`bcp`** with Azure Synapse Analytics, see [Load data with bcp](/azure/sql-data-warehouse/sql-data-warehouse-load-with-bcp).
- **`bcp`** is currently in preview in [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].
- **`bcp`** can't import data in [!INCLUDE [fabric-se](../../includes/fabric-se.md)].

## Identify installed version

To determine the installed version of **`bcp`**, run the following command:

```console
bcp -v
```

If multiple versions of **`bcp`** are installed on Windows, the `PATH` environment variable determines which one runs. To list every copy of `bcp.exe` on the search path, use the following command:

```console
where bcp.exe
```

For information about how to set the command path in the `PATH` environment variable, see [Environment variables](/windows/win32/shell/user-environment-variables).

## bcp versioning

The **`bcp`** utility is versioned independently of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] release it ships with:

| `bcp` major version | Distribution |
| --- | --- |
| `18` | Ships with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. Adds `-Y` (TLS encryption mode) and `-u` (trust server certificate) switches. |
| `15` | Distributed as Microsoft Command Line Utilities 15 for SQL Server, and bundled with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] tools. |

## Download the latest version

The following instructions are for **`bcp`** running on Windows. For instructions on installing **`bcp`** on Linux and macOS, as well as system requirements, see [Install the sqlcmd and bcp SQL Server command-line tools on Linux](../../linux/install-upgrade/setup-tools.md).

| Package | Platform |
| --- | --- |
| Microsoft Command Line Utilities for SQL Server | [x64](https://go.microsoft.com/fwlink/?linkid=2370127)&emsp;[x86](https://go.microsoft.com/fwlink/?linkid=2370024) |

The Microsoft Command Line Utilities package contains both **`bcp`** and **`sqlcmd`** (ODBC). It also installs (or requires) the [Microsoft ODBC Driver for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md).

> [!NOTE]  
> The standalone **`bcp`** download might not have the same release and build number as the **`bcp`** that ships with the latest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] cumulative update (CU). This behavior is expected. The standalone download still contains all fixes included in the latest CU.

## System requirements

The following system requirements are for **`bcp`** running on Windows.

- Windows 10 and later versions
- Windows Server 2016 and later versions
- [Microsoft ODBC Driver for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md) (driver 18 is recommended)

## Related content

- [bcp utility](bcp-utility.md)
- [How to use the bcp utility](bcp-use-utility.md)
- [Authenticate with Microsoft Entra ID in bcp](bcp-authentication.md)
- [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [Format files to import or export data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)

[!INCLUDE [get-help-options](../../includes/paragraph-content/get-help-options.md)]
