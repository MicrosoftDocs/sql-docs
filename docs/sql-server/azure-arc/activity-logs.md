---
title: Azure activity log
description: View Azure Arc-enabled SQL Server and Databases activity log
author: guptasnigdha
ms.author: guptasnigdha
ms.reviewer: mikeray, rajpo
ms.date: 05/26/2023
ms.service: sql
ms.topic: conceptual
---

# Azure Arc-enabled SQL Server and Databases Activity logs

The Azure activity logs for Arc-enabled SQL Server provide an insight into [SQL Server - Azure Arc](overview.md) and [SQL Server databases - Azure Arc](view-databases.md) related events. The activity log includes information like when a resource is created or modified.

You can access these activity logs by going to the **SQL Server - Azure Arc resource > Activity Log**. With the help of activity logs, you can identify events like SQL Server instance updates, *SqlServerInstance_Update* SQL Server Databases updates, *SqlServerDatabases_Update*, writing of tags to resources, and so on.

This helps in auditing different operations performed on the resource, along with other crucial information such as the time at which operation was initiated, its status, and party responsible for event creation. 

## View the activity log
You can access the activity log from most menus in the Azure portal. The menu that you open it from determines its initial filter. You can always change the filter to view all other entries. Select **Add Filter** to add more properties to the filter.

:::image type="content" source="media/monitoring/activity-logs-filter.png" alt-text="Screenshot of the Arc-enabled SQL Server Activity Logs with Add Filter.":::

## Download the activity log

Select **Download as CSV** to download the events in the current view.

:::image type="content" source="media/monitoring/download-as-csv.png" alt-text="Screenshot of the the Arc-enabled SQL Server Activity Logs Download as CSV feature.":::

## View change history

For some events, you can view the change history, which shows what changes happened during that event time. Select an event from the activity log you want to look at more deeply. Select the **Change history** tab to view any associated changes with that event.

If any changes are associated with the event, you'll see a list of changes that you can select. Selecting a change opens the Change history page. This page displays the changes to the resource.

:::image type="content" source="media/monitoring/view-change-history.png" alt-text="Screenshot of the Change history list and tab for an event.":::

## Next steps

- [Azure Monitor activity log](https://learn.microsoft.com/azure/azure-monitor/essentials/activity-log)
