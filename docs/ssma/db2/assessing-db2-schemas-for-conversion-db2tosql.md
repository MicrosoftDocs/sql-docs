---
title: "Assess Db2 schemas for conversion (Db2ToSQL)"
description: Determine how complex a migration is going to be, and how much time the migration will take (Db2ToSQL).
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
  - "ssma.db2.assessment.f1"
---
# Assess Db2 schemas for conversion (Db2ToSQL)

Before you load objects and migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you should determine how complex the migration is going to be, and how much time the migration will take. SQL Server Migration Assistant (SSMA) can create an assessment report that shows the percentage of objects that will be successfully converted. SSMA also lets you view the specific issues that cause conversion failures.

## Create assessment reports

When it creates this assessment report, SSMA converts the selected Db2 database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax, and then shows the results.

1. In Db2 Metadata Explorer, select the schemas to assess.

1. To omit individual objects, clear the check boxes next to them.

1. Right-click **Schemas**, and then select **Create Report**.

   You can also analyze individual objects by right-clicking an object, and then selecting **Create Report**.

   SSMA shows progress in the status bar at the bottom of the window. If the Output pane is visible, you also see messages in the Output pane.

   When the assessment is complete, the SQL Server Migration Assistant for Db2: Assessment Report window appears.

## Use assessment reports

The Assessment Report window contains three panes:

- The left pane contains the *hierarchy of objects* that are included in the assessment report. You can browse the hierarchy, and select objects and categories of objects to view conversion statistics and code.

- The contents of the right pane depends on the item that is selected in the left pane.

  If a group of objects is selected, such a schema, or if a table is selected, the right pane contains a Conversion statistics pane and an Objects by Categories pane. The Conversion Statistics pane shows the conversion statistics for the selected objects. The Objects by Categories pane shows the conversion statistics for the object or categories of objects.

  If a function, package, procedure, sequence, or view is selected, the right pane contains statistics, source code, and target code.

  - The top area shows the overall statistics for the object. You might have to expand **Statistics** to view this information.

  - The Source area shows the source code of the object that is selected in the left pane. The highlighted areas show problematic source code.

  - The Target area shows the converted code. Red text shows problematic code and error messages.

- The bottom pane shows *conversion messages*, grouped by message number. You can select **Errors**, **Warnings**, or **Info** to view categories of messages, and then expand a group of messages. Select an individual message to select the object in the left pane and display the details in the right pane.

## Analyze conversion problems by using the Assessment Report

The Conversion Statistics pane shows the conversion statistics. If the percentage for any category is less than 100 percent, you should determine why the conversion wasn't successful.

1. Create the assessment report by using the instructions in the previous procedure.

1. In the left pane, expand schemas or folders that have a red error icon. Continue expanding items until you select an individual item that failed conversion.

1. At the top of the Source pane, select **Next Problem**.

   The problematic code is highlighted, as is the related code in the Target Navigation pane.

1. Review any error messages, and then determine what you want to do with the object that caused the conversion problem:

   - Update the Db2 syntax in SSMA. You can update the syntax for procedures, functions, triggers, packaged functions, and packaged procedures. To update the syntax, select the object in the Db2 Metadata Explorer pane, select the **SQL** tab, and then modify the SQL code. When you navigate away from the item, you're prompted save the updated syntax. You can view the reported errors for the object on the **Report** tab.

   - In Db2, you can modify the Db2 object to remove or revise problematic code. To load the updated code into SSMA, you have to update the metadata. For more information, see [Connect to Db2 database](connecting-to-db2-database-db2tosql.md).

   - You can exclude the object from migration. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer and Db2 Metadata Explorer, clear the check box next to the item before you load the objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data from Db2.

## Related content

- [Migrate Db2 Databases to SQL Server (Db2ToSQL)](migrating-db2-databases-to-sql-server-db2tosql.md)
- [Convert Db2 schemas (Db2ToSQL)](converting-db2-schemas-db2tosql.md)
