---
title: Data Migration Report
description: Learn about the Data Migration Report dialog box that shows migration results for each table.
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
# Data Migration Report

The **Data Migration Report** dialog box appears after you migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, using SQL Server Migration Assistant (SSMA).

## Options

#### Status

Shows the status of the data migration from the source to the target database.

#### From

The source table.

#### To

The target table.

#### Total number of rows

The number of rows of data in the source table.

#### Number of successfully migrated rows

The number of rows of data successfully migrated to the target table.

#### Ratio

The percentage of rows successfully migrated.

#### Details

If any data migration failed, select to display migration details for the selected row in the report. SSMA displays the reason for the failure.

#### Save Report

Saves the report to a `.csv` (comma-separated values) file, which can be examined using Microsoft Excel.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
