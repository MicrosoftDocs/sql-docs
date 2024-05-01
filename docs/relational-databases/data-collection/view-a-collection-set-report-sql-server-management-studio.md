---
title: "View a collection set report (SSMS)"
description: View a collection set report in SQL Server Management Studio.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.dc.reporthistory.calendar.f1"
helpviewer_keywords:
  - "collection sets [SQL Server], viewing reports"
  - "reports [SQL Server], viewing collection set"
---
# View a collection set report (SQL Server Management Studio)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

After you have configured the management data warehouse, you can view a collection set report in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Reports are provided for the System Data collection sets that are installed during Setup. The reports include the following options:

- Disk Usage Summary
- Query Statistics History
- Server Activity History

This procedure displays the report for the **Disk Usage** collection set. You can follow the same general procedure to view the reports for the other System Data collection sets.

### View a collection set report

1. The tables for a report are created the first time that the collected data is uploaded. If you try to view a report before this first upload, an error occurs and no report is displayed. To upload data for the Disk Usage collection set, in Object Explorer, expand the **Management** folder, expand **Data Collection**, expand **System Data Collection Sets**, right-click the **Disk Usage** collection set, and then select **Collect and Upload Now**.

1. To view the report, in Object Explorer, expand the **Management** folder, right-click **Data Collection**, point to **Reports**, point to **Management Data Warehouse**, and then select **Disk Usage Summary**.

   Some reports might display a calendar button under the data collection timeline. Select this button to access the **Data Collection Report Calendar**.

#### Data collection report calendar

Use this dialog box to specify the start date, start time, and duration of the data that you want to report on. For example, you might want to report on the disk usage activity of a server for a specific 12-hour period last Wednesday.

- **Start date**: Enter a beginning date for the report data, or select one from the calendar.

- **Start time**: Enter a beginning time for the report data or specify one by selecting the arrows.

- **Duration**: Specify the time range to include in the report. The default value is 240 minutes. The possible values to select from are 15 minutes, 60 minutes, 240 minutes (4 hours), 720 minutes (12 hours), and 1440 minutes (24 hours).

## Related content

- [Data collection](data-collection.md)
- [Custom Reports in Management Studio](../../ssms/object/custom-reports-in-management-studio.md)
- [Configure the management data warehouse (SQL Server Management Studio)](configure-the-management-data-warehouse-sql-server-management-studio.md)
