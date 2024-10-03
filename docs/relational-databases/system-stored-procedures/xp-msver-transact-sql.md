---
title: "xp_msver (Transact-SQL)"
description: "Returns version information about SQL Server."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_msver_TSQL"
  - "xp_msver"
helpviewer_keywords:
  - "xp_msver"
dev_langs:
  - "TSQL"
---
# xp_msver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns version information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. `xp_msver` also returns information about the actual build number of the server and information about the server environment. The information that `xp_msver` returns can be used within [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, batches, stored procedures, and so on, to enhance logic for platform-independent code.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_msver [ 'optname' ]
```

## Arguments

#### '*optname*'

The name of an option, and can be one of the following values.

| Option or column name | Description |
| --- | --- |
| **ProductName** | Product name; for example, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **ProductVersion** | Product version. |
| **Language** | The language version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **Platform** | Operating-system name, manufacturer name, and chip family name for the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **Comments** | Miscellaneous information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **CompanyName** | Company name that produces [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]; for example, [!INCLUDE [msCoName](../../includes/msconame-md.md)] Corporation. |
| **FileDescription** | The operating system. |
| **FileVersion** | Version of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] executable. |
| **InternalName** | [!INCLUDE [msCoName](../../includes/msconame-md.md)] internal name for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]; for example, SQLSERVR. |
| **LegalCopyright** | Legal copyright information required for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]; for example, Copyright© [!INCLUDE [msCoName](../../includes/msconame-md.md)] Corp. 1988-2005. |
| **LegalTrademarks** | Legal trademark information required for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For example, [!INCLUDE [msCoName](../../includes/msconame-md.md)] is a registered trademark of [!INCLUDE [msCoName](../../includes/msconame-md.md)] Corporation. |
| **OriginalFilename** | File name executed at [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] startup; for example, Sqlservr.exe. |
| **PrivateBuild** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| **SpecialBuild** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| **WindowsVersion** | Version of Windows that is installed on the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **ProcessorCount** | The number of processors in the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **ProcessorActiveMask** | Indicates the processors installed in the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are started and usable by [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows. |
| **ProcessorType** | Processor type. Similar to **Platform**. |
| **PhysicalMemory** | Amount in megabytes (MB) of RAM installed on the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **Product ID** | Product ID (PID) number. This is specified during installation. This number is located on a sticker on the original [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] CD case. |

## Return code values

`1` (success).

## Result set

`xp_msver`, without any parameters, returns a four-column result set that lists all the option values. `xp_msver`, for any parameter, returns the four-column result set with values for that option.

## Permissions

Requires membership in the **public** role.

## Related content

- [System Functions by category for Transact-SQL](../system-functions/system-functions-category-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
- [@@VERSION - Transact SQL Configuration Functions](../../t-sql/functions/version-transact-sql-configuration-functions.md)
