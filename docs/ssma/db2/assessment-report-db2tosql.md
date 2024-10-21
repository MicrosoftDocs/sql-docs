---
title: "Assessment Report (Db2ToSQL)"
description: Learn how to view the results of the conversion of database objects to Transact-SQL syntax (Db2ToSQL).
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.assessmentreport.f1"
---
# Assessment Report (Db2ToSQL)

The Assessment Report window shows the results of the conversion of database objects to [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.

To access the Assessment Report, select objects to convert in source metadata explorer, right-click **Schemas** or **Synonyms**, and then select **Create Report**.

## Options

| Term | Definition |
| --- | --- |
| **Conversion statistics** | Shows the conversion statistics by statement type. This pane is visible when a group object, such as a schema, or an object without code is selected in the left pane. |
| **Objects by Categories** | Shows the number of objects by category. This pane is visible only when a group object, such as a schema, or an object without code is selected in the left pane. |
| **Statistics** | Shows the conversion statistics for the selected object. This pane is visible only when an individual object with code is selected in the left pane. You might have to expand **Statistics**, which is immediately above the **Source** pane, to view this pane. |
| **Source** | Shows the Db2 code for the selected object, and highlights code that wasn't converted to [!INCLUDE [tsql](../../includes/tsql-md.md)]. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Select the line numbers to set or clear bookmarks. To navigate through the code, use the buttons at the top of the pane. |
| **Target** | Shows the conversion's resulting [!INCLUDE [tsql](../../includes/tsql-md.md)] code for the selected object, and error messages for code that wasn't converted. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Select the line numbers to set or clear bookmarks. To navigate through the code, use the buttons at the top of the pane. |
| **Messages pane** | Shows the errors, warnings, and informational messages that were generated while creating the assessment report. Messages are grouped by number. To view the code that caused the error, select **Errors**, **Warnings**, or **Info**, expand the category of messages, and then select a message. |
