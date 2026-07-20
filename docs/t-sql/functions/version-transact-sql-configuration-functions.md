---
title: "@@VERSION (Transact-SQL)"
description: Returns SQL Server installation system and build information.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan, randolphwest
ms.date: 11/25/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "@@VERSION"
  - "@@VERSION_TSQL"
  - "sql13.swb.tsqlquery.f1"
helpviewer_keywords:
  - "@@VERSION function"
  - "current SQL Server installation information"
  - "versions [SQL Server], @@VERSION"
  - "processors [SQL Server], types"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# @@VERSION (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

The `@@VERSION` configuration function returns information about the system and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] build information.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!IMPORTANT]  
> The [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] version numbers for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] aren't comparable with each other, and represent internal build numbers for these separate products. For more information, see the [Remarks](#remarks) section.

## Syntax

```syntaxsql
@@VERSION
```

## Return types

**nvarchar**

## Remarks

- The [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] version numbers for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] aren't comparable with each other, and represent internal build numbers for these separate products. The [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is based on the same code base as the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. Most importantly, the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] always has the newest SQL [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] bits. For example, version 12 of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is newer than version 16 of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- The `@@VERSION` results appear as one **nvarchar** string. Use the [SERVERPROPERTY](serverproperty-transact-sql.md) function to get the individual property values.

- For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the `@@VERSION` results include:

  - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] version
  - Processor architecture
  - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] build date
  - Copyright statement
  - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition
  - Operating system version

    The operating system version information comes from the host, virtual machine, or container where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed. It doesn't necessarily reflect the retail version of the underlying operating system. For information about querying Windows version information using the [WMI Query Language (WQL)](/windows/win32/wmisdk/wql-sql-for-wmi), see [Win32_OperatingSystem class](/windows/win32/cimwin32prov/win32-operatingsystem).

- For [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], the `@@VERSION` results include:

  - Edition: "Microsoft SQL Azure"

  - Product level: "(RTM)"

  - Product version

  - Build date

  - Copyright statement

## Examples

### A: Return the current version of SQL Server

The following example shows the version information for an installation of [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. Depending on the underlying host, virtual machine, or container operating system, the command returns different information.

```sql
SELECT @@VERSION AS 'SQL Server Version';
```

- Windows Server 2019 virtual machine:

  ```output
  Microsoft SQL Server 2025 (RTM) - 17.0.1000.7 (X64)
  Oct 21 2025 12:05:57
  Copyright (C) 2025 Microsoft Corporation
  Enterprise Developer Edition (64-bit) on Windows Server 2019 Standard 10.0 <X64> (Build 17763: ) (Hypervisor)
  ```

- Windows 11 virtual machine:

  ```output
  Microsoft SQL Server 2025 (RTM) - 17.0.1000.7 (X64)
  Oct 21 2025 12:05:57
  Copyright (C) 2025 Microsoft Corporation
  Enterprise Developer Edition (64-bit) on Windows 10 Enterprise 10.0 <X64> (Build 26220: ) (VM)
  ```

  In this example, the output doesn't necessarily reflect the retail version of the operating system.

- Ubuntu Linux 24.04:

  ```output
  Microsoft SQL Server 2025 (RTM) - 17.0.1000.7 (X64)
  Oct 21 2025 12:05:57
  Copyright (C) 2025 Microsoft Corporation
  Enterprise Developer Edition (64-bit) on Linux (Ubuntu 24.04.3 LTS) <X64>
  ```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### B. Return the current version of Azure Synapse Analytics

```sql
SELECT @@VERSION AS 'SQL Server PDW Version';
```

## Related content

- [SERVERPROPERTY (Transact-SQL)](serverproperty-transact-sql.md)
