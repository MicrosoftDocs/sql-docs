---
title: Configure advance notifications for planned maintenance events
titleSuffix: Azure SQL Database
description: Learn how to configure advance notifications before planned maintenance windows in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: scottkim, urosmil, mathoma
ms.date: 06/19/2024
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: how-to
ms.custom:
  - azure-sql-split
monikerRange: "=azuresql||=azuresql-db"
---
# Configure advance notifications for planned maintenance events in Azure SQL Database
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](advance-notifications.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/advance-notifications.md?view=azuresql-mi&preserve-view=true)

Advance notifications are available for databases configured to use a nondefault [maintenance window](maintenance-window.md). Advance notifications enable customers to configure notifications to be sent up to 24 hours in advance of any planned event.

Notifications can be configured so you can get texts, emails, Azure push notifications, and voicemails when planned maintenance is due to begin in the next 24 hours. Follow-up notifications are sent when maintenance begins and when maintenance ends.

> [!IMPORTANT]
> For Azure SQL Database, advance notifications cannot be configured for the **System default** maintenance window option. Choose a maintenance window other than the **System default** to configure and enable Advance notifications.

[Maintenance windows](maintenance-window.md) are generally available. Advance notifications for maintenance windows are generally available for Azure SQL Database and Azure SQL Managed Instance.

## Configure an advance notification

Complete the following steps to enable a notification in the Azure portal **Service Health** page.

> [!IMPORTANT]
> Service health is rolling out new experiences in phases. Some users will see the [updated Azure Service Health portal experience](/azure/service-health/service-health-portal-update), others will still see the classic Service Health portal experience. In case that you still see the classic Service Health portal, for **Region** don't select Global as an option, but rather the specific region or all regions.

1. Go to the [Service Health Planned maintenance](https://portal.azure.com/#blade/Microsoft_Azure_Health/AzureHealthBrowseBlade/plannedMaintenance) page, select **Health alerts**, then **Add service health alert**.

    :::image type="content" source="media/advance-notifications/health-alerts.png" alt-text="Screenshot of the Health alerts page in the Azure portal. Add service health alert button is boxed in red." lightbox="media/advance-notifications/health-alerts.png":::

1. In the **Scope** section, select subscription.

    :::image type="content" source="media/advance-notifications/select-subscription.png" alt-text="Screenshot of the Azure portal page where you select the subscription where you configure the health alert." lightbox="media/advance-notifications/select-subscription.png":::

1. In the **Condition** section, configure service(s) to be alerted for, region(s) and criteria. For more generic alert, select all values. To narrow down, select Azure SQL Database as a service, region(s) where you have those services deployed, and **Planned maintenance** for the event type.

    :::image type="content" source="media/advance-notifications/define-condition-services.png" alt-text="Screenshot of the Azure portal page where you define conditions for the health alert and define services to be notified for." lightbox="media/advance-notifications/define-condition-services.png":::

    :::image type="content" source="media/advance-notifications/define-condition-regions.png" alt-text="Screenshot of the Azure portal page where you define conditions for the health alert and define regions to be notified for." lightbox="media/advance-notifications/define-condition-regions.png":::

    :::image type="content" source="media/advance-notifications/define-condition-event-types.png" alt-text="Screenshot of the Azure portal page where you define conditions for the health alert and define event types to be notified for." lightbox="media/advance-notifications/define-condition-event-types.png":::

1. In the **Actions** section, select the existing action group or create a new one.

    :::image type="content" source="media/advance-notifications/add-action-group.png" alt-text="Screenshot of the Azure portal page where you add or create action groups." lightbox="media/advance-notifications/add-action-group.png":::

1. In the **Details** section, define the name for your alert and specify resource group where it should be deployed.

    :::image type="content" source="media/advance-notifications/define-alert-details.png" alt-text="Screenshot of the Azure portal page where you define alert details." lightbox="media/advance-notifications/define-alert-details.png":::

1. Select **Review + create**. Your alert is created.

1. You're all set. Next time there's a planned Azure SQL maintenance event, you'll receive an advance notification.

To learn more about creating health alerts, visit [Azure Service Health](/azure/service-health/service-health-portal-update)

## <a id="receiving-notifications"></a> Receive notifications

The following table shows the general information notifications you might receive: 

|Status|Description|
|:---|:---|
|**Planned**| Received 24 hours prior to the maintenance event. Maintenance is planned on DATE between 5pm - 8am<sup>1</sup> (local time) in region *region_name*. |
|**InProgress** | Maintenance for database(s) in region *region_name* is starting. | 
|**Complete** | Maintenance of database(s) in region *region_name* is complete. |

<sup>1</sup> Start and end time depend on the selected [maintenance window](maintenance-window.md). 

The following table shows additional notifications that might be sent while maintenance is ongoing: 

|Status|Description|
|:---|:---|
|**Rescheduled** | - Maintenance is in progress but didn't complete inside maintenance window. <br/>- There was a problem during maintenance and it could not start.<br/> - Planned maintenance has started but couldn't progress to the end and will continue in next maintenance window. | 
|**Canceled**| Maintenance for database(s) in region *region_name* is canceled and will be rescheduled for later. |

## Permissions

While Advance Notifications can be sent to any email address, Azure subscription role-based access control (RBAC) policy determines who can access the links in the email. Querying resource graph is covered by [Azure RBAC](/azure/role-based-access-control/overview) access management.  To enable read access, each recipient should have resource group level read access. For more information, see [Steps to assign an Azure role](/azure/role-based-access-control/role-assignments-steps).

## Retrieve the list of impacted resources

[Azure Resource Graph](/azure/governance/resource-graph/overview) is an Azure service designed to extend Azure Resource Management. The Azure Resource Graph Explorer provides efficient and performant resource exploration. You can query at scale across a given set of subscriptions, so that you can effectively govern your environment.

You can use the Azure Resource Graph Explorer to query for maintenance events. For an introduction on how to run these queries, see [Quickstart: Run your first Resource Graph query using Azure Resource Graph Explorer](/azure/governance/resource-graph/first-query-portal).

When the advanced notification for planned maintenance is received, you will get a link that opens Azure Resource Graph and executes the query for the exact event, similar to the following. The `notificationId` value is unique per maintenance event.  

```kusto
resources
| project resource = tolower(id)
| join kind=inner (
    maintenanceresources
    | where type == "microsoft.maintenance/updates"
    | extend p = parse_json(properties)
    | mvexpand d = p.value
    | where d has 'notificationId' and d.notificationId == 'LNPN-R9Z'
    | project resource = tolower(name), status = d.status, resourceGroup, location, startTimeUtc = d.startTimeUtc, endTimeUtc = d.endTimeUtc, impactType = d.impactType
) on resource
| project resource, status, resourceGroup, location, startTimeUtc, endTimeUtc, impactType
```

In Azure Resource Graph (ARG) explorer, you might find values for the status of deployment that are bit different than the ones displayed in the notification content.

|Status|Description|
|:---|:---|
|**Pending**|- Maintenance is planned on upcoming date.<br/> - Previously planned maintenance was rescheduled and is waiting to start in the next window.</br> - Maintenance started but didn't complete in previous window and will continue in the next one. |
|**InProgress** | Maintenance for resource *region_name* is starting or is in progress. | 
|**Completed** | Maintenance for resource *region_name* is complete. |
|**NoUpdatesPending** | Previously planned maintenance for resource *region_name* is canceled and will be rescheduled for later. |
|**RetryLater** | Planned maintenance for resource *region_name* has started but couldn't progress to the end and will continue in next maintenance window. |

For the full reference of the sample queries and how to use them across tools like PowerShell or Azure CLI, visit [Azure Resource Graph sample queries for Azure Service Health](/azure/service-health/resource-graph-samples).

## Related content

- [Maintenance window](maintenance-window.md)
- [Maintenance window FAQ](maintenance-window-faq.yml)
- [Overview of alerts in Microsoft Azure](/azure/azure-monitor/alerts/alerts-overview)
- [Email Azure Resource Manager Role](/azure/azure-monitor/alerts/action-groups#email-azure-resource-manager-role)
