---
title: Use activity logs
description: Learn how to view or download activity logs for Azure Arc-enabled SQL Server instances or databases.
author: lcwright
ms.author: lancewright
ms.reviewer: vanto
ms.date: 05/19/2025
ms.topic: how-to
---

# Use activity logs with SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Activity logs for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] provide insight into events related to [SQL Server enabled by Azure Arc](overview.md) and [View Azure Arc-enabled inventory](view-inventory.md) resources. Activity logs contain events that correspond to the creation and modification of resources. These events include SQL Server instance updates (`SqlServerInstance_Update`), SQL Server database updates (`SqlServerDatabases_Update`), and writing of tags to resources.

This feature helps in auditing operations performed on a resource. The logs provide crucial information such as the time when an operation started, the operation's status, and the party responsible for event creation.

## View an activity log

You can access an activity log from most menus in the Azure portal. Go to the Azure Arc-enabled SQL Server resource, and then select **Activity log**.

The initial filter depends on the page where you access the activity log. You can change the filter to view all other entries. To add more properties to the filter, select **Add Filter**.

:::image type="content" source="media/monitoring/activity-logs-filter.png" alt-text="Screenshot of an Azure Arc-enabled SQL Server activity log and the button for adding a filter." lightbox="media/monitoring/activity-logs-filter.png":::

## Download an activity log

To download the events in the current view, select **Download as CSV**.

:::image type="content" source="media/monitoring/download-as-csv.png" alt-text="Screenshot of the button for downloading events as a CSV file." lightbox="media/monitoring/download-as-csv.png":::

## View change history

For some events, you can view the history of changes that happened during the event time. Select an event from the activity log to gather more information. Then select **Change history** to view any changes associated with that event.

:::image type="content" source="media/monitoring/view-change-history.png" alt-text="Screenshot of the change history for an event." lightbox="media/monitoring/view-change-history.png":::

## Related log entries

The activity log includes usage upload activity events for each Arc - Machine resource with Azure Extension for SQL Server.

### Successful usage upload

The activity log entry for successful usage upload includes:

- **Operation name**: `SQL Server Usage Upload Azure.Health`
- **Event initiated by**: `<empty>`
- **Description**: Indicates if SQL Server usage data has been received in the past 24 hours. 
- **Status**: `Succeeded`
- **Level**: `Informational`

### Missing usage upload

The activity log entry for missing usage upload includes:

- **Operation name**: `SQL Server Usage Upload Azure.Health`
- **Event initiated by**: `<empty>`
- **Description**: Indicates if SQL Server usage data has been received in the past 24 hours. 
- **Status**: `Failed`
- **Level**: `Warning`

## Configure usage upload alerts

The Usage Upload alert is a custom activity log event. Because it is custom, it doesn't show up when you configure alerts from the Alert Rule wizard in Azure Monitor. But, you can set up an alert rule by selecting an existing log entry as follows.

:::image type="content" source="media/monitoring/select-log-entry.png" alt-text="Screenshot from Azure portal. Select log entry.":::

## Related content

- [Send Azure Monitor activity log data](/azure/azure-monitor/essentials/activity-log)
- [Create or edit an activity log alert rule](/azure/azure-monitor/alerts/alerts-create-activity-log-alert-rule)
