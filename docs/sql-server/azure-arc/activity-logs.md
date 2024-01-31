---
title: Use activity logs
description: View or download and databases activity log
author: guptasnigdha12
ms.author: guptasnigdha
ms.reviewer: mikeray, rajpo
ms.date: 05/26/2023
ms.topic: conceptual
---

# Use activity logs with SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The activity logs for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] provide an insight into [SQL Server - Azure Arc](overview.md) and [SQL Server databases - Azure Arc](view-databases.md) related events. The activity logs contain events corresponding to the creation and modification of resources.
To access activity logs in Azure portal, go to the **SQL Server - Azure Arc resource > Activity Log**. The activity logs help to identify events like SQL Server instance updates, `SqlServerInstance_Update`, SQL Server Databases updates, `SqlServerDatabases_Update`, writing of tags to resources, and so on.

This feature helps in auditing different operations performed on the resource, along with other crucial information such as the time at which the operation was initiated, its status, and the party responsible for event creation.

## View the activity log

You can access the activity log from most menus in the Azure portal. The initial filter depends on the page from where the activity logs are accessed. You can change the filter to view all other entries. Select **Add Filter** to add more properties to the filter.

:::image type="content" source="media/monitoring/activity-logs-filter.png" alt-text="Screenshot of the Arc-enabled SQL Server Activity Logs with Add Filter.":::

## Download the activity log

Select **Download as CSV** to download the events in the current view.

:::image type="content" source="media/monitoring/download-as-csv.png" alt-text="Screenshot of the Arc-enabled SQL Server Activity Logs Download as CSV feature.":::

## View change history

For some events, you can view the change history, which shows what changes happened during that event time. Select an event from the activity log to gather more information. Select the **Change history** tab to view any associated changes with that event.

:::image type="content" source="media/monitoring/view-change-history.png" alt-text="Screenshot of the Change history list and tab for an event.":::

## Next steps

- For more information on the Retention Policy and Export of logs, visit [Azure Monitor activity log](/azure/azure-monitor/essentials/activity-log).
