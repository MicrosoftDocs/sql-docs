---
title: "Assessment Report (AccessToSQL)"
description: "Assessment Report (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.assessmentreport.f1"
helpviewer_keywords:
  - "Assessment Report dialog box"
  - "Conversion Report dialog box"
---
# Assessment Report (AccessToSQL)

The Assessment Report window shows the results of the conversion of database objects to [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.

To create an assessment report, select objects to convert in the source metadata explorer, right-click **Databases**, and then select **Create Report**. You can also display this report automatically after you convert schemas. However, the report name is Conversion Report. For more information, see [Project Settings (GUI)](project-settings-gui-accesstosql.md).

## Options

#### Explorer pane

Contains a hierarchy of objects in the assessment report. Expand folders to view individual objects and subcomponents. When you select a category or object, conversion statistics for that category or object appear in the details pane.

#### Details pane

Shows conversion statistics or error and warning messages for the selected object. For example, if the Tables folder is selected, the details pane shows the numbers of foreign keys, indexes, primary keys, and tables that were converted.

#### Messages pane

Shows the errors, warnings, and information messages that were generated when the assessment report was created. Messages are grouped by number.

To view message details, select either **Errors**, **Warnings**, or **Messages**, and then expand a message. SSMA displays the list of objects that have this error. Select an object to display all conversion details for the object.

## Related content

- [User interface reference](user-interface-reference-accesstosql.md)
