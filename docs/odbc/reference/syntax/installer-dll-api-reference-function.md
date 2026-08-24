---
title: "Installer DLL API reference"
description: "Learn about the ODBC Installer DLL API functions for managing data sources, drivers, translators, and configuration."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: 02/05/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ms.custom: intro-installation
ai-usage: ai-assisted
helpviewer_keywords:
  - "installer DLL [ODBC]"
---
# Installer DLL API reference

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The Installer DLL API provides functions for programmatic management of ODBC data sources, drivers, and translators. Applications and setup programs use these functions to install, configure, and remove ODBC components, and to manage registry information. Microsoft writes and redistributes the Installer DLL.

The API consists of 25 functions. Three of these functions, `SQLGetTranslator`, `SQLRemoveDSNFromIni`, and `SQLWriteDSNToIni`, are called only by setup DLLs. Setup and administration programs call the other functions.

Each function is labeled with the version of ODBC in which it was introduced.

## Data source configuration

These functions create, modify, and remove ODBC data sources programmatically. Use them when building custom setup programs or applications that need to configure data source connections without user interaction.

| Function | Description |
| --- | --- |
| [SQLConfigDataSource](sqlconfigdatasource-function.md) | Adds, modifies, or deletes a data source. Calls the driver setup DLL to perform the actual configuration. |
| [SQLCreateDataSource](sqlcreatedatasource-function.md) | Displays a dialog box that allows users to add a data source interactively. |
| [SQLManageDataSources](sqlmanagedatasources.md) | Displays a dialog box for managing data sources and tracing options, similar to the ODBC Data Source Administrator. |
| [SQLValidDSN](sqlvaliddsn-function.md) | Checks the length and validity of a data source name before it's written to the registry. |
| [SQLReadFileDSN](sqlreadfiledsn-function.md) | Reads connection information from a file-based data source name (.dsn file). |
| [SQLWriteFileDSN](sqlwritefiledsn-function.md) | Writes connection information to a file-based data source name (.dsn file). |
| [SQLRemoveDefaultDataSource](sqlremovedefaultdatasource-function.md) | Removes the default data source from the system information. |

## Driver installation and removal

These functions install and remove ODBC drivers from the system. They manage registry entries and usage counts to support multiple applications sharing the same drivers.

| Function | Description |
| --- | --- |
| [SQLInstallDriverEx](sqlinstalldriverex-function.md) | Adds driver information to the registry and increments the driver's usage count. Returns the target directory for driver files. |
| [SQLInstallDriverManager](sqlinstalldrivermanager-function.md) | Returns the target directory for installing ODBC core components. Deprecated in ODBC 3.0 because the Driver Manager is part of Windows. |
| [SQLRemoveDriver](sqlremovedriver-function.md) | Removes driver information from the registry. Decrements the driver's usage count and removes registry entries when count reaches zero. |
| [SQLRemoveDriverManager](sqlremovedrivermanager-function.md) | Decrements the ODBC core component usage count. Deprecated because the Driver Manager is now part of Windows. |
| [SQLConfigDriver](sqlconfigdriver-function.md) | Loads a driver's setup DLL and calls its `ConfigDriver` function to perform driver-specific configuration tasks. |

## Translator installation and removal

These functions manage ODBC translators, which convert data between character sets (for example, ANSI to Unicode). Use them when your application requires data translation during communication with a data source.

| Function | Description |
| --- | --- |
| [SQLInstallTranslatorEx](sqlinstalltranslatorex-function.md) | Adds translator information to the registry and increments the translator's usage count. |
| [SQLInstallTranslator](sqlinstalltranslator-function.md) | Installs a translator. Deprecated; use `SQLInstallTranslatorEx` instead. |
| [SQLRemoveTranslator](sqlremovetranslator-function.md) | Removes translator information from the registry. Decrements the usage count and removes entries when count reaches zero. |
| [SQLGetTranslator](sqlgettranslator-function.md) | Displays a dialog box for selecting a translator. Called by driver setup DLLs to prompt users for translator selection. |

## Registry and configuration utilities

These functions read and write configuration data in the ODBC section of the Windows registry. Other installer functions and driver setup DLLs use them internally.

| Function | Description |
| --- | --- |
| [SQLWriteDSNToIni](sqlwritedsntoini-function.md) | Adds a data source name to the registry. Called by driver setup DLLs when creating a new data source. |
| [SQLRemoveDSNFromIni](sqlremovedsnfromini-function.md) | Removes a data source name from the registry. Called by driver setup DLLs when deleting a data source. |
| [SQLGetPrivateProfileString](sqlgetprivateprofilestring-function.md) | Reads a value from a data source specification subkey in the registry. |
| [SQLWritePrivateProfileString](sqlwriteprivateprofilestring-function.md) | Writes a value to a data source specification subkey in the registry. |
| [SQLGetInstalledDrivers](sqlgetinstalleddrivers-function.md) | Returns a list of installed ODBC drivers from the registry. |
| [SQLGetConfigMode](sqlgetconfigmode-function.md) | Retrieves the configuration mode that indicates which registry location (user or system) is used for data source entries. |
| [SQLSetConfigMode](sqlsetconfigmode-function.md) | Sets the configuration mode that indicates where data source entries are written in the registry. |

## Error handling

These functions provide error information when installer functions fail.

| Function | Description |
| --- | --- |
| [SQLInstallerError](sqlinstallererror-function.md) | Returns error or status information for installer functions. Each function in the Installer DLL posts zero or more errors that can be retrieved by this function. |
| [SQLPostInstallerError](sqlpostinstallererror-function.md) | Allows driver setup DLLs to report errors to the installer error queue so that `SQLInstallerError` can return them. |

## Related content

- [Installer DLL](../install/installer-dll.md)
- [ODBC Overview](../odbc-overview.md)
- [Configuration Components](../install/configuration-components.md)
