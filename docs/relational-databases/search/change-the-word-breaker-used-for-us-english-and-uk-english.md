---
title: Change the Word Breaker and Filter in SQL Server 2025
titleSuffix: SQL Server Full-Text Search
description: Change the word breaker used for US English and UK English in SQL Server 2025.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 03/06/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Change the word breaker and filter in SQL Server 2025 (SQL Server Search)

[!INCLUDE [sssql25-md](../../includes/applies-to-version/sqlserver2025.md)]

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] installs new word breakers and filters, replacing all the previous versions of these components. For more information, see [Behavior changes in Full-Text Search](behavior-changes-full-text-search.md).

Binaries installed with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] are called **version 2**, and binaries installed with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions are called **version 1**. This article describes how to switch between version 2 and version 1 components.

## Switch from version 2 to version 1 components

> [!NOTE]  
> You should use the default version 2 word breakers and filters installed with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. They're enhanced, more secure, and compliant with security scans.

The default database scoped configuration for full-text indexes is version 2. You can change it to version 1 with the following Transact-SQL statement.

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION SET FULLTEXT_INDEX_VERSION = 1;
   ```

Legacy version 1 word breakers and filters are removed from [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. You must copy these binaries from a previous instance's `C:\Program Files\Microsoft SQL Server\MSSQL<nn>.<instance-name>\MSSQL\Binn` directory, where `<nn>` is the [product version](../../sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md#shared-files-for-all-instances-of-sql-server). For more information, see [Filters and word breakers - SQL Server Full-Text Search](full-text-index-binaries.md).

For existing indexes, verify the version using the [sys.fulltext_indexes](../system-catalog-views/sys-fulltext-indexes-transact-sql.md) catalog view. If the index is already version 1, you don't need to rebuild it. For a version 2 index, rebuild the catalog to switch back to version 1.

## Switch from version 1 to version 2 components

For instances upgraded from [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you must rebuild full-text indexes with version 1 components using version 2 components.

```sql
ALTER FULLTEXT CATALOG [FtCatalog] REBUILD;
```

> [!NOTE]  
> A catalog rebuild operation rebuilds all full-text indexes. If you want to control the order of the index build, or reduce resource requirements, [drop and recreate](../../t-sql/statements/drop-fulltext-index-transact-sql.md) the full-text indexes individually.

## Change the word breaker used for US English and UK English

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

[!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] installs an updated version of the word breaker and stemmer for the English language, replacing the previous version of these components. For information about the changed behavior of the updated components, see [Behavior changes in Full-Text Search](/previous-versions/sql/2014/database-engine/behavior-changes-to-full-text-search#behavior-changes-in-full-text-search-in--1).

This article describes how to switch from the updated version of these components to the previous version, or to switch back from the previous version to the updated version. For cluster installations, make these changes on all nodes.

Some previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] used different word breakers represented by different CLSIDs for US English (LCID 1033) and UK English (LCID 2057). In [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions, both locale identifiers (LCIDs) use the same components with the same CLSIDs, as shown in the following table:

| LCID | Word breaker installed by previous versions (version 12.0.6828.0) | Stemmer installed by previous versions | Word breaker installed with SQL Server 2012 and higher versions (version 14.0.4999.1038) | Stemmer installed with SQL Server 2012 and higher versions |
| --- | --- | --- | --- | --- |
| `1033` (US English) | `188d6cc5-cb03-4c01-912e-47d21295d77e` | `eeed4c20-7f1b-11ce-be57-00aa0051fe20` | `9faed859-0b30-4434-ae65-412e14a16fb8` | `e1e5ef84-c4a6-4e50-8188-99aef3de2659` |
| `2057` (UK English) | `173c97e2-aebe-437c-9445-01b237abf2f6` | `d99f7670-7f1a-11ce-be57-00aa0051fe20` | `9faed859-0b30-4434-ae65-412e14a16fb8` | `e1e5ef84-c4a6-4e50-8188-99aef3de2659` |

The components described in this article are DLL files that are installed in the `MSSQL\Binn` folder for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. The full path is typically `C:\Program Files\Microsoft SQL Server\<instance>\MSSQL\Binn`.

For more information about word breakers and stemmers, see [Configure and manage word breakers and stemmers for search (SQL Server)](configure-and-manage-word-breakers-and-stemmers-for-search.md).

## Switch from the current English word breaker to the previous English word breakers

This example uses `MSSQL17.MSSQLSERVER` for the `<InstanceRoot>` value, which is the default instance for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. Adjust this value to match your environment.

### [Switch from current version of US English](#tab/en-us)

The following commands add or update keys in the Windows registry, to set the COM ClassIDs for the previous US English word breaker and stemmer interfaces for LCID 1033 (`enu`).

Run these commands from an elevated command prompt:

```console
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{188D6CC5-CB03-4C01-912E-47D21295D77E}" /ve /t REG_SZ /d "langwrbk.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{EEED4C20-7F1B-11CE-BE57-00AA0051FE20}" /ve /t REG_SZ /d "infosoft.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\enu" /v "WBreakerClass" /t REG_SZ /d "{188D6CC5-CB03-4C01-912E-47D21295D77E}" /f
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\enu" /v "StemmerClass" /t REG_SZ /d "{EEED4C20-7F1B-11CE-BE57-00AA0051FE20}" /f
```

Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use these word breaker settings.

### [Switch from current version of UK English](#tab/en-uk)

The following commands add or update keys in the Windows registry, to set the COM ClassIDs for the previous UK English word breaker and stemmer interfaces for LCID 2057 (`eng`).

Run these commands from an elevated command prompt:

```console
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{173C97E2-AEBE-437C-9445-01B237ABF2F6}" /ve /t REG_SZ /d "langwrbk.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{D99F7670-7F1A-11CE-BE57-00AA0051FE20}" /ve /t REG_SZ /d "infosoft.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\eng" /v "WBreakerClass" /t REG_SZ /d "{173C97E2-AEBE-437C-9445-01B237ABF2F6}" /f
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\eng" /v "StemmerClass" /t REG_SZ /d "{D99F7670-7F1A-11CE-BE57-00AA0051FE20}" /f
```

Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use these word breaker settings.

---

## Switch back from the previous English word breakers to the current English word breaker

This example uses `MSSQL17.MSSQLSERVER` for the `<InstanceRoot>` value, which is the default instance for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. Adjust this value to match your environment.

### [Switch back from US English](#tab/en-us)

The following commands add or update keys in the Windows registry, to set the COM ClassIDs back from US English for LCID 1033 (`enu`).

Run these commands from an elevated command prompt:

```console
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{9FAED859-0B30-4434-AE65-412E14A16FB8}" /ve /t REG_SZ /d "MsWb7.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{E1E5EF84-C4A6-4E50-8188-99AEF3DE2659}" /ve /t REG_SZ /d "MsWb7.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\enu" /v "WBreakerClass" /t REG_SZ /d "{9FAED859-0B30-4434-AE65-412E14A16FB8}" /f
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\enu" /v "StemmerClass" /t REG_SZ /d "{E1E5EF84-C4A6-4E50-8188-99AEF3DE2659}" /f
```

Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use these word breaker settings.

### [Switch back from UK English](#tab/en-uk)

The following commands add or update keys in the Windows registry, to set the COM ClassIDs back from UK English for LCID 2057 (`eng`).

Run these commands from an elevated command prompt:

```console
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{9FAED859-0B30-4434-AE65-412E14A16FB8}" /ve /t REG_SZ /d "MsWb7.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\CLSID\{E1E5EF84-C4A6-4E50-8188-99AEF3DE2659}" /ve /t REG_SZ /d "MsWb7.dll"
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\eng" /v "WBreakerClass" /t REG_SZ /d "{9FAED859-0B30-4434-AE65-412E14A16FB8}" /f
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSearch\Language\eng" /v "StemmerClass" /t REG_SZ /d "{E1E5EF84-C4A6-4E50-8188-99AEF3DE2659}" /f
```

Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use these word breaker settings.

---

## Related content

- [Revert word breakers used by Full-Text Search to previous version](revert-the-word-breakers-used-by-search-to-the-previous-version.md)
- [Full-Text Search](full-text-search.md)
