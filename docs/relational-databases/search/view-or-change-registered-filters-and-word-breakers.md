---
title: Customize Filters and Word Breakers
titleSuffix: SQL Server Full-Text Search
description: View registered Full-Text Search filters and word breakers, and customize component registration for version 2 or version 1 full-text indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "full-text search [SQL Server], word breakers"
  - "full-text search [SQL Server], filters"
  - "filters [full-text search]"
  - "word breakers [full-text search]"
ai-usage: ai-assisted
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Customize filters and word breakers

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to view and customize Full-Text Search filters, word breakers, and stemmers on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

> [!NOTE]  
> Customization is disallowed for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] or [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)], because access to the Windows registry or host filesystem is restricted.

The customization process differs based on the full-text index version.

- **Full-text index version 1** uses Windows registry-based component registration. Instance-specific component registration is stored under `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch`, which is divided into `Filters`, `Language`, and `CLSID` subkeys. When you set `load_os_resources` to `1` via [sp_fulltext_service](../system-stored-procedures/sp-fulltext-service-transact-sql.md), Full-Text Search falls back to searching `HKEY_CLASSES_ROOT` for extensions and LCIDs missing from the instance registration. For details, see [Customize version 1 filters and word breakers](#customize-version-1-filters-and-word-breakers).

- **Full-text index version 2** simplifies the customization process. Instead of reading the Windows registry, you can provide an optional `version_overrides.json` file per instance. For details, see [Customize version 2 filters and word breakers](#customize-version-2-filters-and-word-breakers).

To check the version used by a full-text index, query the `index_version` column in [sys.fulltext_indexes](../system-catalog-views/sys-fulltext-indexes-transact-sql.md). To control the version used when creating or rebuilding indexes, use the [FULLTEXT_INDEX_VERSION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#fulltext_index_version) database scoped configuration.

## View registered components

For a conceptual overview, see [Configure and manage filters](configure-and-manage-filters-for-search.md) and [Configure and manage word breakers and stemmers](configure-and-manage-word-breakers-and-stemmers-for-search.md).

To view all linguistic components, run [sp_help_fulltext_system_components](../system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md) with the `all` argument:

```sql
EXECUTE sp_help_fulltext_system_components 'all';
```

> [!NOTE]  
> `sp_help_fulltext_system_components` reports registered components for the full-text index version specified via the `FULLTEXT_INDEX_VERSION` database scoped configuration.

## Customize version 2 filters and word breakers

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

Version 2 full-text indexes don't read the Windows registry. Follow the steps in this section to customize version 2 word breakers, stemmers, and filters.

1. The default full-text index version in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions is **version 2**. The default DLL files for version 2 are located in the `C:\Program Files\Microsoft SQL Server\MSSQL17.<instance-name>\MSSQL\Binn\ftcomponents\[filters|wordbreakers]` directory.

1. To override a default or add a new component, create a `version_overrides.json` file inside the `C:\Program Files\Microsoft SQL Server\MSSQL17.<instance-name>\MSSQL\FTData` directory.

1. To add or replace the word breaker, update the `languages` section in the JSON file. For filters, update the `doctypes` section.

   Sample JSON structure:

   ```json
   {
      "languages": {
            "en": [{
               "version": 2,
               "handler": "MSWB7.dll",
               "wbClsid": "9faed859-0b30-4434-ae65-412e14a16fb8",
               "stemmerClsid": "e1e5ef84-c4a6-4e50-8188-99aef3de2659"
            }],
            "<BCP 47 locale name>": [{ ... }],
         }

      "doctypes": {
            ".html": [{
               "version": 2,
               "handler": "nlhtml.dll",
               "clsid": "e0ca5340-4534-11cf-b952-00aa0051fe20"
            }],
            ".<extension 2>": [{ ... }],
         }
   }
   ```

   Consider the following rules for the `version_overrides.json` file:

   - All fields in the JSON file are mandatory except `stemmerClsid` (optional).

   - Locale name can be any BCP 47 locale name, following [standard locale names](/globalization/locale/standard-locale-names).

   - When duplicate entries exist for the same language and version, or extension and version, the most recent entry takes precedence.

   - Handler DLL can be a relative path to the file in relation to the `C:\Program Files\Microsoft SQL Server\MSSQL17.<instance-name>\MSSQL\Binn\ftcomponents\[filters|wordbreakers]` directory where default binaries are located. It can also be an absolute path. For example, the following JSON file enables support for indexing PDFs via the built-in Windows PDF filter:

   ```json
   {
      "doctypes": {
         ".pdf": [
            {
               "version": 2,
               "handler": "%SystemRoot%\\system32\\windows.data.pdf.dll",
               "clsid": "6C337B26-3E38-4F98-813B-FBA18BAB64F5"
            }
         ]
      }
   }
   ```

   > [!IMPORTANT]  
   > You should load only signed and verified components. Configure the correct access control lists (ACLs) on DLL files and folders containing them. Also, you should run the FDHOST Launcher (MSSQLFDLauncher) Service with the least possible privileges.

1. Use `sp_fulltext_service` to update the internal list of languages and document types for accurate DMV reporting:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   ```

1. Restart the filter daemon host processes (`fdhost.exe`) for the overrides to take effect for future queries and populations:

   ```sql
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

### Example: Support a custom document extension

Scenario: You want to add support for indexing files with your own custom extension, such as `.myextension`.

1. Find the existing installed filter handler for the associated document class via `sys.fulltext_document_types` or `sp_help_fulltext_system_components 'filter'`. For plaintext, for example:

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION
       SET FULLTEXT_INDEX_VERSION = 1;
   GO

   SELECT *
   FROM sys.fulltext_document_types
   WHERE document_type = '.txt';
   ```

1. Create or update `version_overrides.json` in the `FTData` directory with the preceding `class_id` and `path`. In this case:

   ```json
   {
      "doctypes": {
         ".myextension": [
            {
               "version": 2,
               "handler": "%SystemRoot%\\system32\\query.dll",
               "clsid": "C1243CA0-BF96-11CD-B579-08002B30BFEB"
            }
         ]
      }
   }
   ```

1. Refresh the registered component list and restart the filter daemon host processes:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

### Example: Install a third-party filter (Foxit PDF IFilter)

1. Follow the third-party installation documentation. For Foxit PDF IFilter, see [How to Download the IFilter Addon for Foxit PDF Editor](https://kb.foxit.com/s/articles/How-to-Download-the-IFilter-Addon-for-Foxit-PDF-Editor).

   Installers usually write registration information into `HKCR` and place binaries under `Program Files`.

1. Create or update `version_overrides.json` in the `FTData` directory:

   ```json
   {
      "doctypes": {
         ".pdf": [
            {
               "version": 2,
               "handler": "C:\\Program Files\\Foxit Software\\Foxit PDF IFilter\\PDFFilt.dll",
               "clsid": "987f8d1a-26e6-4554-b007-6b20e2680632"
            }
         ]
      }
   }
   ```

1. Refresh the registered component list and restart the filter daemon host processes:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

#### Find the CLSID

The easiest way to find the CLSID, if it isn't publicly documented, is to query the DMVs or `sp_help_fulltext_system_components` with `FULLTEXT_INDEX_VERSION` = `1`, or from an older SQL Server instance.

You can also check affected registry keys. A PDF filter install, for example, updates `HKCR\.pdf\PersistentHandler`. You can then take that PH CLSID and find the component CLSID as `HKEY_CLASSES_ROOT\CLSID\{PH CLSID}\PersistentAddinsRegistered\{Component CLSID}`.

## Customize version 1 filters and word breakers

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and prior versions.

Version 1 full-text indexes use the Windows registry to resolve filters, word breakers, and stemmers. This applies to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] indexes that still use `index_version = 1`, and to full-text indexes in earlier [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions.

Version 1 component lookup uses this order:

1. Instance-specific registration under `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Filters` for matching extensions and `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Languages` for matching LCIDs.
1. System registration under `HKEY_CLASSES_ROOT` (`HKCR`), if operating-system resource loading is enabled via `sp_fulltext_service 'load_os_resources'`.

Instance-specific registration has priority. Modify it when you want a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to use a component that differs from the operating-system registration or other installed instances.

### Install and load version 1 components

1. Before you install a DLL file that contains new word breakers or filters, make sure that it has a different filename from any of the existing DLL files installed on your server instance.

1. Copy the new DLL file into the directory containing the standard [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] DLL files for the server instance. The default location is:

   ```output
   C:\Program Files\Microsoft SQL Server\MSSQL.<instance_name>\MSSQL\Binn
   ```

1. Install the new word breaker or filters following their documentation.

1. Use `sp_fulltext_service` to load newly installed word breakers and filters in the server instance, as follows:

   ```sql
   EXECUTE sp_fulltext_service
       @action = 'load_os_resources',
       @value = 1;
   ```

1. Use `sp_fulltext_service` to update the list of languages, as follows:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   ```

1. Restart the filter daemon host processes (`fdhost.exe`), using `sp_fulltext_service` as follows:

   ```sql
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

### Example: Support a custom document extension

Scenario: You want to add support for indexing files with your own custom extension, such as `.myextension`, by using an existing installed filter.

1. Find the CLSID for the filter you want to reuse. For example, to reuse the plaintext filter, inspect the `.txt` registration with version 1 component reporting:

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION
       SET FULLTEXT_INDEX_VERSION = 1;
   GO

   SELECT *
   FROM sys.fulltext_document_types
   WHERE document_type = '.txt';
   ```

1. Add instance-specific registry entries for the new extension. The extension entry maps `.myextension` to the filter CLSID, and the `CLSID` entry maps that CLSID to the filter DLL.

   The following example uses the legacy plaintext filter CLSID and `msfte.dll`:

   ```console
   reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Filters\.myextension" /ve /t REG_SZ /d "{C7310720-AC80-11D1-8DF3-00C04FB6EF4F}" /f
   ```

   Replace `<InstanceRoot>` with the SQL Server instance root, such as `MSSQL16.MSSQLSERVER`.

1. Refresh the registered component list and restart the filter daemon host processes:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

### Example: Install a third-party filter (Foxit PDF IFilter)

Scenario: You want version 1 full-text indexes to use a third-party PDF IFilter that registers itself with Windows.

1. Follow the third-party installation documentation. For Foxit PDF IFilter, see [How to Download the IFilter Addon for Foxit PDF Editor](https://kb.foxit.com/s/articles/How-to-Download-the-IFilter-Addon-for-Foxit-PDF-Editor).

1. Enable operating-system resource loading so version 1 component lookup can fall back to `HKCR` when the SQL Server instance doesn't have an instance-specific registration for `.pdf`:

   ```sql
   EXECUTE sp_fulltext_service
       @action = 'load_os_resources',
       @value = 1;
   ```

1. Refresh the registered component list and restart the filter daemon host processes:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

If you don't want to rely on the `HKCR` fallback, add instance-specific `MSSearch\Filters` and `MSSearch\CLSID` entries for `.pdf` instead. The instance-specific entries take priority over system registration.

## Related content

- [Set the service account for the full-text Filter Daemon Launcher](set-the-service-account-for-the-full-text-filter-daemon-launcher.md)
- [Configure and manage filters](configure-and-manage-filters-for-search.md)
- [Configure and manage word breakers and stemmers](configure-and-manage-word-breakers-and-stemmers-for-search.md)
- [Full-text filter binaries](full-text-filter-binaries.md)
- [Full-text word breaker and stemmer binaries](full-text-word-breaker-and-stemmer-binaries.md)
- [Full-text index version upgrade](full-text-index-version-upgrade.md)
- [sys.sp_fulltext_service (Transact-SQL)](../system-stored-procedures/sp-fulltext-service-transact-sql.md)
- [sys.fulltext_indexes (Transact-SQL)](../system-catalog-views/sys-fulltext-indexes-transact-sql.md)
- [ALTER DATABASE SCOPED CONFIGURATION - FULLTEXT_INDEX_VERSION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#fulltext_index_version)
