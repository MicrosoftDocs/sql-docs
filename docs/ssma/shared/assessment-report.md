---
title: Assessment Report
description: Learn how to view the results of database object conversion to Transact-SQL syntax in SQL Server Migration Assistant (SSMA).
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
# Assessment Report

The Assessment Report window in SQL Server Migration Assistant (SSMA) shows the results of the conversion of database objects to [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.

To access the Assessment Report, select objects to convert in the source metadata explorer, right-click **Schemas**, and then select **Create Report**.

## Options

| Term | Definition |
| --- | --- |
| **Conversion statistics** | Shows the conversion statistics by statement type. This pane is visible when a group object, such as a schema, or an object without code is selected in the left pane. |
| **Objects by Categories** | Shows the number of objects by category. This pane is visible only when a group object, such as a schema, or an object without code is selected in the left pane. |
| **Statistics** | Shows the conversion statistics for the selected object. This pane is visible only when an individual object with code is selected in the left pane. You might have to expand **Statistics**, which is immediately above the **Source** pane, to view this pane. |
| **Source** | Shows the source code for the selected object, and highlights code that wasn't converted to [!INCLUDE [tsql](../../includes/tsql-md.md)]. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Select the line numbers to set or clear bookmarks. To navigate through the code, use the buttons at the top of the pane. |
| **Target** | Shows the conversion's resulting [!INCLUDE [tsql](../../includes/tsql-md.md)] code for the selected object, and error messages for code that wasn't converted. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Select the line numbers to set or clear bookmarks. To navigate through the code, use the buttons at the top of the pane. |
| **Messages pane** | Shows the errors, warnings, and informational messages that were generated while creating the assessment report. Messages are grouped by number. To view the code that caused the error, select **Errors**, **Warnings**, or **Info**, expand the category of messages, and then select a message. |

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
