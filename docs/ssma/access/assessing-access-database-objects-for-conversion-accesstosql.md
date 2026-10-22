---
title: "Assessing Access Database Objects for Conversion (AccessToSQL)"
description: "Assessing Access Database Objects for Conversion (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.assessment.f1"
helpviewer_keywords:
  - "assessing SQL"
  - "assessing syntax"
  - "assessment reports"
  - "creating assessment reports"
  - "estimating migration effort"
  - "reports"
  - "SQL, assessing"
  - "syntax, assessing"
---
# Assess Access database objects for conversion (AccessToSQL)

Before you load objects and migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, determine how much of the migration is successful and how long the conversion might take. SQL Server Migration Assistant (SSMA) can create an assessment report that shows the percentage of objects that it successfully converted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL syntax. It also provides time estimates for performing the migration. SSMA also lets you view the specific problems that caused conversion failures.

## Create assessment reports

When SSMA creates an assessment report, it converts the selected Access database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL syntax, and then shows the results.

1. In Access Metadata Explorer, select the database or databases that you want to assess.

1. To omit individual objects, clear the check boxes next to the objects you don't want to assess.

1. Right-click **Databases**, and then select **Create Report**.

   You can also analyze individual objects by right-clicking an object and then selecting **Create Report**.

   SSMA shows progress in the status bar at the bottom of the window. If the Output pane is visible, you also see messages in the Output pane.

When the assessment finishes, the **Assessment Report** window appears.

## Use assessment reports

The Assessment Report window contains three panes: an explorer, a details pane, and a message pane.

- The explorer pane lets you browse the assessed objects. You can select items in this pane to drill down to individual tables, indexes, and keys.

- The details pane shows the conversion statistics for the selected object.

- The message pane shows the errors, warnings, and informational messages for the conversion, and time estimates for performing the migration and individual error correction steps.

You should correct errors before you run the assessment report again or convert schemas. To find errors, select the **Errors** button in the messages pane, and then expand each error to view a list of objects where the error occurred. If you select an object in the messages pane, all errors and warnings for that object appear in the details pane.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Convert Access database objects](converting-access-database-objects-accesstosql.md)
