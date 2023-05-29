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

The Azure activity logs for Arc-enabled SQL Server provide an insight into [SQL Server - Azure Arc](overview.md) and [SQL Server databases - Azure Arc](view-databases.md) related events. The activity logs contain events corresponding to the creation and modification of resources.
One can access these activity logs by going to the **SQL Server - Azure Arc resource > Activity Log**. With the help of activity logs, one can identify events like SQL Server instance updates, *SqlServerInstance_Update* SQL Server Databases updates, *SqlServerDatabases_Update*, writing of tags to resources, and so on.

This helps in auditing different operations performed on the resource, along with other crucial information such as the time at which the operation was initiated, its status, and the party responsible for event creation.

## View the activity log
One can access the activity log from most menus in the Azure portal. The menu from where the logs are opened determines its initial filter. The filter can be changed to view all other entries. Select **Add Filter** to add more properties to the filter.

:::image type="content" source="media/monitoring/activity-logs-filter.png" alt-text="Screenshot of the Arc-enabled SQL Server Activity Logs with Add Filter.":::

## Download the activity log

Select **Download as CSV** to download the events in the current view.

:::image type="content" source="media/monitoring/download-as-csv.png" alt-text="Screenshot of the the Arc-enabled SQL Server Activity Logs Download as CSV feature.":::

## View change history

For some events, one can view the change history, which shows what changes happened during that event time. Select an event from the activity log to gather more information. Select the **Change history** tab to view any associated changes with that event.

One can see a list of changes that are associated with the event. Selecting a change opens the Change history page. This page displays the changes to the resource.

:::image type="content" source="media/monitoring/view-change-history.png" alt-text="Screenshot of the Change history list and tab for an event.":::

## Next steps

- For more information on the Retention Policy and Export of logs visit [Azure Monitor activity log].(/azure/azure-monitor/essentials/activity-log)
