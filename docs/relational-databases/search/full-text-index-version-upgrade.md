---
title: Full-text index version upgrade
titleSuffix: SQL Server Full-Text Search
description: Learn about SQL Server 2025 full-text index versions, behavior changes, migration steps, and component binaries.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/02/2026
ms.service: sql
ms.subservice: search
ms.topic: upgrade-and-migration-article
helpviewer_keywords:
  - "full-text search [SQL Server], word breakers"
  - "full-text search [SQL Server], filters"
  - "filters [full-text search]"
  - "word breakers [full-text search]"
ai-usage: ai-assisted
monikerRange: "=azuresqldb-current || >=sql-server-linux-ver17 || >=sql-server-ver17 || =azuresqldb-mi-current"
---
# Full-text index version upgrade

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes the full-text index version change in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions. It covers behavior changes, required migration steps, and new component binaries.

## Full-text index version changes in SQL Server 2025

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] removes legacy word breaker, stemmer, and filter binaries from the SQL Server installation. These components are rebuilt with a modern toolset and offer expanded support for more languages and document types. Components installed with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] are called **version 2**. Components installed with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions are called **version 1**.

After an in-place upgrade, existing full-text indexes have `index_version = 1` in [sys.fulltext_indexes](../system-catalog-views/sys-fulltext-indexes-transact-sql.md). Newly created indexes use version 2 and the new components, unless you specify otherwise by using the [FULLTEXT_INDEX_VERSION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#fulltext_index_version) database scoped configuration.

## Version 2 component changes

Version 2 components add language and document type support, use a new customization model, and can return different tokenization results than version 1 components.

### Support for new languages

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] adds support for full-text indexing in three new languages:

- Finnish (LCID 1035)
- Hungarian (LCID 1038)
- Estonian (LCID 1061)

### Support for new document types

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] adds support for indexing the following document extensions by default.

| Filter | Extension |
| --- | --- |
| `msgfilt02.dll` | `.msg` |
| `odffilt02.dll` | `.odp`, `.ods`, `.odt` |
| `offfilt02.dll` | `.doc`, `.dot`, `.obd`, `.obt`, `.pot`, `.pps`, `.ppt`, `.xlb`, `.xlc`, `.xls`, `.xlt` |
| `offfiltx02.dll` | `.docm`, `.docx`, `.dotx`, `.pptm`, `.pptx`, `.xlsb`, `.xlsm`, `.xlsx`, `.zip` |
| `onfilter02.dll` | `.one` |

### Unexpected results

New components in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] might return unexpected results to applications. For example, consider the English (LCID 1033) word breaker:

| Term | Results with previous word breaker | Results with new word breaker |
| --- | --- | --- |
| `cat_dog` | `cat_dog` | `cat_dog`<br />`cat`<br />`dog` |
| `$100` | `$100`<br />`nn100usd` | `\$100`<br />`nn100\$` |
| `2026-01-09` | `2026-01-09`<br />`2026`<br />`nn2026`<br />`01`<br />`09` | `2026-01-09`<br />`dd20260109`<br />`2026`<br />`01`<br />`09` |

### Customization model

Version 2 full-text indexes no longer read component handlers from the Windows registry. You control customization through an instance-specific JSON file. For more information, see [Customize filters and word breakers](view-or-change-registered-filters-and-word-breakers.md).

## Upgrade and migration options

Because [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] removes all version 1 binaries, full-text queries and populations that use version 1 indexes fail after an in-place upgrade. For more information, see [Breaking changes to Database Engine features in SQL Server 2025](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md#full-text-queries-and-populations-fail-after-upgrade).

Use one of the following approaches after upgrading to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] or later versions, or when preparing version 1 indexes for deprecation in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

### Find version 1 indexes

Run the following query in each database that uses Full-Text Search to find indexes that still use version 1 components:

```sql
SELECT fc.[name] AS catalog_name,
       OBJECT_SCHEMA_NAME(fi.object_id) AS schema_name,
       OBJECT_NAME(fi.object_id) AS table_name,
       fi.object_id,
       fi.*
FROM sys.fulltext_indexes AS fi
     INNER JOIN sys.fulltext_catalogs AS fc
         ON fi.fulltext_catalog_id = fc.fulltext_catalog_id
WHERE fi.index_version = 1;
```

### Rebuild existing indexes with version 2 components

Rebuild existing full-text indexes to use version 2 components. Verify that `FULLTEXT_INDEX_VERSION` is set to `2`, and then rebuild full-text catalogs.

```sql
SELECT *
FROM sys.database_scoped_configurations
WHERE [name] = 'FULLTEXT_INDEX_VERSION';
```

```sql
ALTER FULLTEXT CATALOG [FtCatalog] REBUILD;
```

> [!NOTE]  
> A catalog rebuild operation rebuilds all full-text indexes. If you want to control the order of the index build, or reduce resource requirements, [drop and recreate](../../t-sql/statements/drop-fulltext-index-transact-sql.md) the full-text indexes individually.

### Keep using version 1 components

Use this option only for SQL Server instances where you can manage files in the SQL Server installation.

> [!IMPORTANT]  
> Version 1 is deprecated for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux. In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, the `mssql-server-fts` package doesn't include version 1 binaries. Attempting to install mismatched versions of the `mssql-server-fts` and `mssql-server` packages is unsupported, and results in full-text failures.

If you need to remain on version 1 for application compatibility, set `FULLTEXT_INDEX_VERSION = 1` to avoid an unintended upgrade to version 2 during rebuild.

```sql
ALTER DATABASE SCOPED CONFIGURATION
    SET FULLTEXT_INDEX_VERSION = 1;
```

Next, copy the legacy word breaker, stemmer, and filter binaries from an older instance's `Binn` folder to the target instance's `Binn` folder. For a reference of which DLL and dependent libraries you need to copy per language or document type, see [Full-text filter binaries](full-text-filter-binaries.md) and [Full-text word breaker and stemmer binaries](full-text-word-breaker-and-stemmer-binaries.md).

To bulk copy the complete set of version 1 binaries, copy the following script to a file named `Copy-FulltextV1Components.ps1`.

```powershell
<#
.SYNOPSIS
    Copies the Full-Text V1 components from one SQL install's Binn folder to another.
    Existing files are never overwritten; each file reports OK, SKIP or FAIL.

.EXAMPLE
    .\Copy-FulltextV1Components.ps1 `
        -SourceBinn 'C:\Program Files\Microsoft SQL Server\MSSQL16.INST1\MSSQL\Binn' `
        -TargetBinn 'C:\Program Files\Microsoft SQL Server\MSSQL17.INST2\MSSQL\Binn'
#>
param(
    [Parameter(Mandatory)] [string] $SourceBinn,
    [Parameter(Mandatory)] [string] $TargetBinn
)

$components = @(
    'infosoft.dll',
    'LangWrbk.dll',
    'korwbrkr.dll',
    'korwbrkr.lex',
    'msfte.dll',
    'xmlfilt.dll',
    'MsWb7.dll',
    'MsWb70011.dll',
    'MsWb7001e.dll',
    'MsWb70404.dll',
    'MsWb70804.dll',
    'NaturalLanguage6.dll',
    'NL7Data0011.dll',
    'NL7Data001e.dll',
    'NL7Data0404.dll',
    'NL7Data0804.dll',
    'NL7Lexicons0011.dll',
    'NL7Lexicons001e.dll',
    'NL7Lexicons0404.dll',
    'NL7Lexicons0804.dll',
    'NL7Models0011.dll',
    'NL7Models001e.dll',
    'NL7Models0404.dll',
    'NL7Models0804.dll',
    'nlhtml.dll',
    'nls400.dll',
    'NlsData0000.dll',
    'NlsData0002.dll',
    'NlsData0003.dll',
    'NlsData000a.dll',
    'NlsData000c.dll',
    'NlsData000d.dll',
    'NlsData000f.dll',
    'NlsData0010.dll',
    'NlsData0018.dll',
    'NlsData001a.dll',
    'NlsData001b.dll',
    'NlsData001D.dll',
    'NlsData0020.dll',
    'NlsData0021.dll',
    'NlsData0022.dll',
    'NlsData0024.dll',
    'NlsData0026.dll',
    'NlsData0027.dll',
    'NlsData002a.dll',
    'NlsData0039.dll',
    'NlsData003e.dll',
    'NlsData0045.dll',
    'NlsData0046.dll',
    'NlsData0047.dll',
    'NlsData0049.dll',
    'NlsData004a.dll',
    'NlsData004b.dll',
    'NlsData004c.dll',
    'NlsData004e.dll',
    'NlsData0414.dll',
    'NlsData0416.dll',
    'NlsData0816.dll',
    'NlsData081a.dll',
    'NlsData0c1a.dll',
    'Nlsdl.dll',
    'NlsLexicons0002.dll',
    'NlsLexicons0003.dll',
    'NlsLexicons000a.dll',
    'NlsLexicons000c.dll',
    'NlsLexicons000d.dll',
    'NlsLexicons000f.dll',
    'NlsLexicons0010.dll',
    'NlsLexicons0018.dll',
    'NlsLexicons001a.dll',
    'NlsLexicons001b.dll',
    'NlsLexicons001D.dll',
    'NlsLexicons0020.dll',
    'NlsLexicons0021.dll',
    'NlsLexicons0022.dll',
    'NlsLexicons0024.dll',
    'NlsLexicons0026.dll',
    'NlsLexicons0027.dll',
    'NlsLexicons002a.dll',
    'NlsLexicons0039.dll',
    'NlsLexicons003e.dll',
    'NlsLexicons0045.dll',
    'NlsLexicons0046.dll',
    'NlsLexicons0047.dll',
    'NlsLexicons0049.dll',
    'NlsLexicons004a.dll',
    'NlsLexicons004b.dll',
    'NlsLexicons004c.dll',
    'NlsLexicons004e.dll',
    'NlsLexicons0414.dll',
    'NlsLexicons0416.dll',
    'NlsLexicons0816.dll',
    'NlsLexicons081a.dll',
    'NlsLexicons0c1a.dll',
    'Prm0001.bin',
    'Prm0005.bin',
    'Prm0006.bin',
    'Prm0007.bin',
    'Prm0008.bin',
    'Prm0009.bin',
    'Prm0013.bin',
    'Prm0015.bin',
    'Prm0019.bin',
    'Prm001f.bin'
)

if (-not (Test-Path -LiteralPath $SourceBinn -PathType Container)) { throw "Source Binn folder not found: $SourceBinn" }
if (-not (Test-Path -LiteralPath $TargetBinn -PathType Container)) { throw "Target Binn folder not found: $TargetBinn" }

if ((Split-Path -Leaf $SourceBinn) -ne 'Binn') { throw "Source path must be a Binn folder: $SourceBinn" }
if ((Split-Path -Leaf $TargetBinn) -ne 'Binn') { throw "Target path must be a Binn folder: $TargetBinn" }

$ok = 0; $skip = 0; $fail = 0
foreach ($name in $components) {
    $srcFile = Join-Path $SourceBinn $name
    $dstFile = Join-Path $TargetBinn $name

    if (-not (Test-Path -LiteralPath $srcFile)) {
        Write-Host "[FAIL] $name (source not found)" -ForegroundColor Red
        $fail++
    }
    elseif (Test-Path -LiteralPath $dstFile) {
        Write-Warning "[SKIP] $name already exists in target; not overwritten"
        $skip++
    }
    else {
        try {
            Copy-Item -LiteralPath $srcFile -Destination $dstFile -ErrorAction Stop
            Write-Host "[ OK ] $name" -ForegroundColor Green
            $ok++
        }
        catch {
            Write-Host "[FAIL] $name ($($_.Exception.Message))" -ForegroundColor Red
            $fail++
        }
    }
}

Write-Host ""
Write-Host "Copied $ok, skipped $skip, failed $fail of $($components.Count)."
```

Run the script in an admin PowerShell window, where `SourceBinn` is the path to the `Binn` folder of a [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] or earlier instance containing version 1 binaries, and `TargetBinn` is the path to the `Binn` folder of a [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] or later instance that needs continued version 1 support.

```powershell
.\Copy-FulltextV1Components.ps1 `
    -SourceBinn 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Binn' `
    -TargetBinn 'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Binn'
```

## Version 1 status and deprecation timeline by environment

| Environment | Status | Action Required |
| --- | --- | --- |
| [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions | Version 1 binaries are removed from the SQL Server installation. Version 1 queries and populations fail after in-place upgrade. | Rebuild or recreate full-text indexes with version 2 components. If you need version 1 for compatibility, use version 1 only where supported. |
| [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] on the SQL Server 2025 [update policy](/azure/azure-sql/managed-instance/update-policy#sql-server-2025-update-policy) | Version 1 deprecation is in staged rollout. Affected customers receive periodic email reminders before deprecation. | Rebuild or recreate full-text indexes with version 2 components before your instance is affected. If you can't upgrade immediately, contact Microsoft support. |
| [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] on the "Always-up-to-date" update policy | Version 1 indexes are still supported today, but removal is planned. Newly created or rebuilt indexes will begin to default to using version 2. | Inventory version 1 indexes and plan rebuilds with version 2 components in advance to minimize downtime. |

### Azure SQL deprecation timeline

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

[!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] don't remove version 1 binaries yet. These offerings undergo a staged deprecation to allow time for rebuilding existing indexes, starting with [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] on the SQL Server 2025 [update policy](/azure/azure-sql/managed-instance/update-policy#sql-server-2025-update-policy). Affected customers receive periodic email reminders in advance of deprecation.

After version 1 is deprecated, queries on version 1 indexes fail with error message:

```output
Msg 30011, Level 16, State 1, Line 37
Full-text index version 1 is not supported by this instance configuration. Rebuild or recreate the index with database scoped configuration FULLTEXT_INDEX_VERSION = 2. For more information, see https://aka.ms/fts-version-upgrade. If unable to upgrade, contact support for assistance.
```

And populations fail with the following errors in the crawl log:

```output
Error: 30011, Severity: 16, State: 1.
Full-text index version 1 is not supported by this instance configuration. Rebuild or recreate the index with database scoped configuration FULLTEXT_INDEX_VERSION = 2. For more information, see https://aka.ms/fts-version-upgrade. If unable to upgrade, contact support for assistance.

Error: 30059, Severity: 16, State: 1.
A fatal error occurred during a full-text population and caused the population to be cancelled. Population type is: <population_type>; database name is <database_name> (id: <database_id>); catalog name is <catalog_name> (id: <catalog_id>); table name <table_name> (id: <table_id>). Fix the errors that are logged in the full-text crawl log. Then, resume the population. The basic Transact-SQL syntax for this is: ALTER FULLTEXT INDEX ON table_name RESUME POPULATION.
```

## Related content

- [Customize filters and word breakers](view-or-change-registered-filters-and-word-breakers.md)
- [Configure and manage filters](configure-and-manage-filters-for-search.md)
- [Configure and manage word breakers and stemmers](configure-and-manage-word-breakers-and-stemmers-for-search.md)
- [sys.fulltext_indexes (Transact-SQL)](../system-catalog-views/sys-fulltext-indexes-transact-sql.md)
- [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#fulltext_index_version)
- [Full-text filter binaries](full-text-filter-binaries.md)
- [Full-text word breaker and stemmer binaries](full-text-word-breaker-and-stemmer-binaries.md)
