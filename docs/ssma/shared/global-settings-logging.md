---
title: Global Settings (Logging)
description: Learn how to configure logging settings in SQL Server Migration Assistant (SSMA).
author: cwebb-msft
ms.author: chweb
ms.date: 04/16/2026
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ai-usage: ai-assisted
---
# Global Settings (Logging)

Use the **Global Settings** dialog box to specify the logging settings for SQL Server Migration Assistant (SSMA). Typically, you change these settings only when working with product support.

To access this dialog box, navigate to **Tools** > **Global Settings**, and then select the **Logging** button at the bottom of the left pane.

## Options

#### Messages level

The following options are available under **Messages Level**:

| Option | Description |
| --- | --- |
| **[all categories]** | Used to set the logging level for all of the following options. |
| **Collector** | Collects metadata about the source schema and saves it to the project. |
| **Converter** | Converts structures of source database objects, such as tables and stored procedures, into corresponding [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] structures. |
| **Data migrator** | Migrates data from the source database into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **Formatter** | Subcomponent of the Converter that generates scripts for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] schema. |
| **Graphical user interface** | Messages that appear when you use the SSMA tool. |
| **Linker** | Resolves SQL identifiers and provides information to other components. |
| **Other** | All messages that aren't in any other category. |
| **Parser** | Parses the source schema. |
| **Synchronizer** | Loads source database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| **TreeConverter** | Converts objects in the source metadata into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] metadata. |

For each option under **Messages Level**, configure one of the following logging levels for SSMA:

| Level | Description |
| --- | --- |
| **Fatal Error** | Write only fatal error messages to the log. |
| **Error** | Write error and fatal error messages to the log. |
| **Warning** | Write warning, error, and fatal error messages to the log. |
| **Info** | Write informational, warning, error, and fatal error messages to the log. |
| **Debug** | Write all messages, including debugging messages, to the log. |

#### Log file path

The file path and name of the SSMA log files. To specify a different name, select the current path, and then select the Browse (**...**) button.

#### Log file size

The maximum size of the log file in KB. The minimum size is 10 KB. The default size is 10240 KB.

#### Total number of log files

When one log fills, SSMA renames the log file and starts a new one. By using this setting, specify the maximum number of log files to keep. The minimum is 2.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
