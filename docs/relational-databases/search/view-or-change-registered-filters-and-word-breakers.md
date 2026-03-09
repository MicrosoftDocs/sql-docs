---
title: "View or Change Registered Filters and Word Breakers"
titleSuffix: SQL Server Full-Text Search
description: View the currently registered word breaker or filters, and register newly installed word breakers and filters.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 12/08/2025
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "full-text search [SQL Server], word breakers"
  - "full-text search [SQL Server], filters"
  - "filters [full-text search]"
  - "word breakers [full-text search]"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# View or change registered filters and word breakers (SQL Server Search)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

After you install or uninstall word breakers or filters on a system, the changes don't automatically take effect on server instances. This article describes how to view the currently registered word breakers or filters and how to register newly installed word breakers and filters on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] supports viewing registered filters and word breakers, but changing them isn't supported. You can only use preinstalled ones. Third party filters and word breakers aren't supported on [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

## View a list of languages whose word breakers are currently registered

Use the [sys.fulltext_languages](../system-catalog-views/sys-fulltext-languages-transact-sql.md) catalog view, as follows:

```sql
SELECT *
FROM sys.fulltext_languages;
```

## View a list of the filters that are currently registered

Use the [sp_help_fulltext_system_components](../system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md) system stored procedure, as follows:

```sql
EXECUTE sp_help_fulltext_system_components 'filter';
```

## Register newly installed word breakers and filters

Use the [sp_fulltext_service](../system-stored-procedures/sp-fulltext-service-transact-sql.md) system stored procedure to update the list of languages, as follows:

```sql
EXECUTE sp_fulltext_service 'update_languages';
```

## Unregister uninstalled word breakers and filters

1. Use the `sp_fulltext_service` to update the list of languages, as follows:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   ```

1. Use the `sp_fulltext_service` to restart the filter daemon host processes (fdhost.exe), as follows:

   ```sql
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

## Add or replace word breakers and filters

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

1. The default full-text index version in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions is **version 2**. The default DLL files for version 2 are located in the `C:\Program Files\Microsoft SQL Server\MSSQL17.<instance-name>\MSSQL\Binn\Ftcomponents\[Filters|wordbreakers]` directory.

1. To override default or add new component, create a `version_overrides.json` file inside the `C:\Program Files\Microsoft SQL Server\MSSQL17.<instance-name>\MSSQL\FTData` directory.

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

   - When duplicate entries exist for same language and version, or extension and version, the most recent entry takes precedence.

   - Handler DLL can be a relative path to the file in relation to the `C:\Program Files\Microsoft SQL Server\MSSQL17.<instance-name>\MSSQL\Binn\Ftcomponents\[Filters|wordbreakers]` directory where default binaries are located. It can also be an absolute path. For example, to override filter with a system binary, handler can be specified as follows:

     ```output
     "handler": "%SystemRoot%\\system32\\windows.data.pdf.dll"
     ```

     > [!IMPORTANT]  
     > You should load only signed and verified components. Configure the correct access control lists (ACLs) on DLL files and folders containing them. Also, you should run the FDHOST Launcher (MSSQLFDLauncher) Service with the least possible privileges.

1. Use `sp_fulltext_service` to update the list of languages, as follows:

   ```sql
   EXECUTE sp_fulltext_service 'update_languages';
   ```

1. Restart the filter daemon host processes (`fdhost.exe`), using `sp_fulltext_service` as follows:

   ```sql
   EXECUTE sp_fulltext_service 'restart_all_fdhosts';
   ```

## Replace existing word breakers or filters when installing new ones

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions.

1. Before you install a DLL file that contains new word breakers or filters, make sure that it has a different filename from any of the existing DLL files installed on your server instance.

1. Copy the new DLL file into the directory containing the standard [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] DLL files for the server instance. The default location is:

   ```output
   C:\Program Files\Microsoft SQL Server\MSSQL.<instance_name>\MSSQL\Binn
   ```

1. Install the new word breaker or filters.

   **To install and load Microsoft Filter Pack IFilters**

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

## Related content

- [Set the Service Account for the Full-text Filter Daemon Launcher](set-the-service-account-for-the-full-text-filter-daemon-launcher.md)
- [Configure and Manage Filters for Search](configure-and-manage-filters-for-search.md)
- [Configure and manage word breakers and stemmers for search (SQL Server)](configure-and-manage-word-breakers-and-stemmers-for-search.md)
