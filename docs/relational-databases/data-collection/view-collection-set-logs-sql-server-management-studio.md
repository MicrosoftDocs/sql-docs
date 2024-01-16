---
title: "View collection set logs (SSMS)"
description: View collection set logs in SQL Server Management Studio.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "logs [SQL Server], viewing"
  - "collection sets [SQL Server], viewing logs"
---
# View collection set logs (SQL Server Management Studio)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You can view all the collection set logs or individual collection set logs from [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

### View collection set logs

1. In Object Explorer, expand the **Management** folder.

1. Right-click **Data Collection**, and then select **View Logs**.

   This opens the **Log File Viewer**.All the log files for each collection set are listed and preselected under the **Data Collection** node in the viewer.

1. To view specific collection set logs, clear the check box next to each collection set whose log you don't want to view. The log information for that collection set is removed from the **Log File Viewer** details pane.

### View a specific collection set log file

1. In Object Explorer, expand the **Management** folder, and then expand **Data Collection**.

1. Right-click the collection set whose log you want to view, and then select **View Logs**.

   The **Log File Viewer** opens, displaying only the log file for the collection set that you selected.

## Related content

- [Data collection](data-collection.md)
- [Manage data collection](manage-data-collection.md)
