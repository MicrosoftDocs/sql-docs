---
title: "Project Settings (Conversion) (AccessToSQL)"
description: "Project Settings (Conversion) (AccessToSQL)"
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
  - "ssma.access.projectsettingsconversion.f1"
helpviewer_keywords:
  - "conversion, options described"
  - "Project Settings dialog box, Conversion"
---
# Project Settings (Conversion) (AccessToSQL)

The SQL Server Migration Assistant (SSMA) conversion project settings let you configure how objects are converted from Access database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects.

The Conversion pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.

- Use the **Project Settings** dialog box to set configuration options for the current project. To access the conversion settings, on the **Tools** menu, select **Project Settings**, select **General** at the bottom of the left pane, and then select **Conversion**.

- Use the **Default Project Settings** dialog box to set configuration options for all projects. To access the conversion settings, on the **Tools** menu, select **Default Project Settings**. From the **Migration Target Version** dropdown list, select the migration project type for which you want to view or change settings. Select **General** at the bottom of the left pane, and then select **Conversion**.

## Options

#### Add primary key

Creates a new primary key in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL table if an Access table has no primary key or unique index.

- **Default Mode**: False
- **Optimistic Mode**: False
- **Full Mode**: True (Azure SQL is True by default)

#### Add timestamp columns

Specifies whether SSMA should create a timestamp value if necessary.

- **Default Mode**: Let SSMA decide
- **Optimistic Mode**: Never
- **Full Mode**: Let SSMA decide

#### Include a data assessment report with conversion assessment reports

Includes a data assessment in the assessment report.

- **Default Mode**: True
- **Optimistic Mode**: False
- **Full Mode**: True

#### Message type when a primary key includes nullable columns

Specifies the type of message (**Warning**, **Error**, or **Nothing**) that SSMA shows in the Output pane when it finds primary keys with nullable columns.

- **Default Mode**: Warning
- **Optimistic Mode**: No message
- **Full Mode**: Error

#### Message type when foreign key columns are of different sizes

Specifies the type of message (**Warning**, **Error**, or **Nothing**) that SSMA shows in the Output pane when it finds an incorrect `TEXT` foreign key.

- **Default Mode**: Warning
- **Optimistic Mode**: No message
- **Full Mode**: Error

#### Message type when memo columns are indexed

Specifies the type of message (**Warning**, **Error**, or **Nothing**) that SSMA shows in the Output pane when it finds an index that contains a `memo` column.

- **Default Mode**: Warning
- **Optimistic Mode**: No message
- **Full Mode**: Error

#### Warn when a complex query uses a wildcard (\*)

Displays a warning in the Output pane and Error List when a column name in a `SELECT` statement is a wildcard (`*`).

- **Default Mode**: True
- **Optimistic Mode**: False
- **Full Mode**: True

#### Warn when identifier name is changed

When SSMA changes an object identifier name, it displays a message in the assessment report and in the Output pane.

- **Default Mode**: True
- **Optimistic Mode**: False
- **Full Mode**: True

#### Warn when identifier will be quoted

When SSMA needs to quote an object identifier name, it displays a message in the assessment report and in the Output pane. SSMA needs to quote an identifier if the name is a keyword or contains special characters.

- **Default Mode**: True
- **Optimistic Mode**: False
- **Full Mode**: True

## Related content

- [User interface reference](user-interface-reference-accesstosql.md)
