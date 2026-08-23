---
title: "Assess Oracle Schemas for Conversion (OracleToSQL)"
description: Learn how to assess Oracle schemas for conversion.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.oracle.assessmentreport.f1"
helpviewer_keywords:
  - "Analyzing Conversion Problems"
---

# Assess Oracle schemas for conversion (OracleToSQL)

Before you load objects and migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you should determine how complex the migration will be and how much time the migration will take. Microsoft SQL Server Migration Assistant (SSMA) for Oracle can create an assessment report that shows the percentage of objects that will be successfully converted. With SSMA, you can also view the specific issues that cause conversion failures.

## Create assessment reports

When SSMA creates an assessment report, it converts the selected Oracle database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax, and then shows the results.

To create an assessment report:

1. In Oracle Metadata Explorer, select the schemas that you want to assess.

1. Clear the checkboxes next to any individual objects you want to exclude.

1. Right-click **Schemas**, and then select **Create Report**. You can also analyze individual objects by right-clicking an object, and then selecting **Create Report**.

   SSMA shows progress in the status bar at the bottom of the window. If the **Output** pane is visible, you also see messages there. When the assessment is complete, the **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for Oracle: Assessment Report** window appears.

## Use assessment reports

The **Assessment Report** window contains two panes:

- The left pane contains the hierarchy of objects that are included in the assessment report. To view conversion statistics and code, you can browse the hierarchy and select objects and categories of objects.
- The contents of the right pane correlate to the item you selected on the left pane.

If a group of objects is selected, such as a schema, or if a table is selected, the right pane contains a **Conversion Statistics** pane and an **Objects by Categories** pane. The **Conversion Statistics** pane shows the conversion statistics for the selected objects. The **Objects by Categories** pane shows the conversion statistics for the object or the categories of objects.

If a function, package, procedure, sequence, or view is selected, the right pane contains statistics, source code, and target code.

- The top area shows the overall statistics for the object. You might have to expand **Statistics** to view this information.
- The **Source** area shows the source code of the object you selected on the left pane. The highlighted areas show problematic source code.
- The **Target** area shows the converted code. Problematic code and error messages are displayed in red text.
- The bottom pane shows conversion messages, grouped by message number. You can select **Errors**, **Warnings**, or **Info** to view categories of messages, and then expand a group of messages. Select an individual message. Then select the object on the left pane and display the details on the right pane.

## Analyze conversion problems by using the assessment report

The **Conversion Statistics** pane shows the conversion statistics. If the percentage for any category is less than 100 percent, you should determine why the conversion wasn't successful.

### View conversion problems

1. Create the assessment report by using the instructions in the previous procedure.

1. On the left pane, expand schemas or folders that have a red error icon. Continue expanding items until you select an individual item that failed conversion.

1. At the top of the **Source** pane, select **Next Problem**.

   The problematic code is highlighted. The related code is also highlighted on the **Target Navigation** pane.

1. Review any error messages, and then determine what you want to do with the object that caused the conversion problem. You can:

   - Update the Oracle syntax in SSMA. You can update the syntax for procedures, functions, triggers, packaged functions, and packaged procedures.

     To update the syntax, select the object on the Oracle Metadata Explorer pane, select the **SQL** tab, and then modify the SQL code. When you navigate away from the item, you're prompted to save the updated syntax. You can view the reported errors for the object on the **Report** tab.

   - In Oracle, you can modify the Oracle object to remove or revise problematic code. To load the updated code into SSMA, you have to update the metadata. For more information, see [Connecting to Oracle Database](connecting-to-oracle-database-oracletosql.md).

   - You can exclude the object from migration. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer and Oracle Metadata Explorer, clear the checkbox next to the item. Then load the objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data from Oracle.

## Next step

> [!div class="nextstepaction"]
> [Convert Oracle schemas](converting-oracle-schemas-oracletosql.md)
