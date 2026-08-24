---
title: Configure advance notifications for planned maintenance events
titleSuffix: Azure SQL Managed Instance
description: Learn how to configure advance notifications before planned maintenance windows in Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: scottkim, mathoma, urosmil
ms.date: 07/22/2025
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: how-to
monikerRange: "=azuresql||=azuresql-mi"
ms.custom:
  - azure-sql-split
  - sfi-image-nochange
---
# Configure advance notifications for planned maintenance events in Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/advance-notifications.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](advance-notifications.md?view=azuresql-mi&preserve-view=true)

Advance notifications enable you to configure notifications to send up to 24 hours in advance of any planned event.

You can configure notifications so you get texts, emails, Azure push notifications, and voicemails when planned maintenance is due to begin in the next 24 hours. Follow-up notifications are sent when maintenance begins and when maintenance ends.

## Configure an advance notification

> [!IMPORTANT]
> Service health is rolling out new experiences in phases. Some users will see the [updated Azure Service Health portal experience](/azure/service-health/service-health-portal-update), others will still see the classic Service Health portal experience. In case that you still see the classic Service Health portal, for **Region** don't select Global as an option, but rather the specific region or all regions.

Complete the following steps to enable a notification in the Azure portal **Service Health** page.

1. Go to the [Service Health](https://portal.azure.com/#view/Microsoft_Azure_Health/AzureHealthBrowseBlade/~/healthAlerts) page. Under **Alerts**, select **Health alerts**. Then select **Create**.

    :::image type="content" source="media/advance-notifications/health-alerts.png" alt-text="Screenshot of the Health alerts page in the Azure portal. Add service health alert button is boxed in red." lightbox="media/advance-notifications/health-alerts.png":::

1. In the **Scope** section, select subscription.

1. In the **Condition** section, configure the services to be alerted for, regions, and criteria. For a more generic alert, select all values. To narrow down the alert, select Azure SQL Managed Instance as a service, select the regions where you deployed those services, and select **Planned maintenance** for the event type.

    :::image type="content" source="media/advance-notifications/define-condition-services.png" alt-text="Screenshot of the Azure portal page where you define conditions for the health alert and define services to be notified for." lightbox="media/advance-notifications/define-condition-services.png":::

    :::image type="content" source="media/advance-notifications/define-condition-regions.png" alt-text="Screenshot of the Azure portal page where you define conditions for the health alert and define regions to be notified for." lightbox="media/advance-notifications/define-condition-regions.png":::

    :::image type="content" source="media/advance-notifications/define-condition-event-types.png" alt-text="Screenshot of the Azure portal page where you define conditions for the health alert and define event types to be notified for.":::

1. In the **Actions** section, select the existing action group or create a new one.

    :::image type="content" source="media/advance-notifications/add-action-group.png" alt-text="Screenshot of the Azure portal page where you add or create action groups." lightbox="media/advance-notifications/add-action-group.png":::

1. In the **Details** section, enter the name for your alert and specify the resource group where you want to deploy it.

    :::image type="content" source="media/advance-notifications/define-alert-details.png" alt-text="Screenshot of the Azure portal page where you define alert details." lightbox="media/advance-notifications/define-alert-details.png":::

1. Select **Tags**. Consider using Azure tags. For example, use the "Owner" or "CreatedBy" tag to identify who created the resource, and the "Environment" tag to identify whether this resource is in production, development, and so on. For more information, see [Develop your naming and tagging strategy for Azure resources](/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging).

1. Select **Review + create**. Your alert is created in the next few minutes.

You're all set. Next time there's a planned Azure SQL maintenance event, you receive an advance notification. To learn more about creating health alerts, visit [Azure Service Health](/azure/service-health/service-health-portal-update).

## <a id="tracking-azure-sql-managed-instance-maintenance-events"></a> Track Azure SQL Managed Instance maintenance events

[Azure Service Health](/azure/service-health/service-health-overview) combines three separate smaller services to keep you informed about the health of your cloud resources. This information includes current and upcoming issues such as service affecting events, planned maintenance, and other changes that might affect your availability. Service health (a sub-service) provides a personalized view of the health of the Azure services and regions you're using. This view is the best place to look for service affecting planned maintenance activities.

Under the Service Health, you can find a **Planned maintenance** page that lists all maintenance activities happening on your subscriptions. You can use built-in filters to narrow the list of events and scope them to tenant, subscription, region, or resource type. 

:::image type="content" source="media/advance-notifications/service-health-planned-maintenance.png" alt-text="Screenshot of the Azure Monitor Service Health page where you can list all maintenance events." lightbox="media/advance-notifications/service-health-planned-maintenance.png":::

From the list of events on the **Service Health | Planned Maintenance** page, you can go into each maintenance event and check the details. All maintenance events with the same tracking ID appear in the **Issue Updates** tab. On the **Impacted Resources** tab, the portal lists resources impacted by published maintenance events. 

:::image type="content" source="media/advance-notifications/service-health-planned-maintenance-event.png" alt-text="Screenshot of the Azure Monitor Service Health page with opened single maintenance event containing full event details." lightbox="media/advance-notifications/service-health-planned-maintenance-event.png":::

## <a id="receiving-notifications"></a> Receive notifications

The following table shows the general information notifications you might receive: 

|Status|Description|
|:---|:---|
|**Planned**| Received 24 hours before the maintenance event. Maintenance is planned on DATE between 5pm - 8am<sup>1</sup> (local time) in region *region_name*. |
|**InProgress** | Maintenance for databases in region *region_name* is starting. | 
|**Complete** | Maintenance of databases in region *region_name* is complete. |

<sup>1</sup> Start and end time depend on the selected [maintenance window](maintenance-window.md).

The following table shows additional notifications that might be sent while maintenance is ongoing:

|Status|Description|
|:---|:---|
|**Rescheduled** | - Maintenance is in progress but didn't complete inside maintenance window. <br/>- There was a problem during maintenance and it couldn't start.<br/> - Planned maintenance has started but couldn't progress to the end and will continue in next maintenance window. |
|**Canceled**| Maintenance for databases in region *region_name* is canceled and will be rescheduled for later. |

## Permissions

While Advance Notifications can be sent to any email address, Azure subscription role-based access control (RBAC) policy determines who can access the links in the email. Querying resource graph is covered by [Azure RBAC](/azure/role-based-access-control/overview) access management.  To enable read access, each recipient should have resource group level read access. For more information, see [Steps to assign an Azure role](/azure/role-based-access-control/role-assignments-steps).

## Programmatically retrieve the list of impacted resources

[Azure Resource Graph](/azure/governance/resource-graph/overview) is an Azure service designed to extend Azure Resource Management. The Azure Resource Graph Explorer provides efficient and performant resource exploration. You can query at scale across a given set of subscriptions, so that you can effectively govern your environment.

You can use the Azure Resource Graph Explorer to query for maintenance events. For an introduction on how to run these queries, see [Quickstart: Run your first Resource Graph query using Azure Resource Graph Explorer](/azure/governance/resource-graph/first-query-portal).

When you receive the advanced notification for planned maintenance, you get a link that opens Azure Resource Graph and executes the query for the exact event. The `notificationId` value is unique per maintenance event. The query looks similar to the following example:   

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

In Azure Resource Graph (ARG) explorer, you might find values for the status of deployment that are different from the ones displayed in the notification content.

|Status|Description|
|:---|:---|
|**Pending**|- Maintenance is planned on upcoming date.<br/> - Previously planned maintenance is rescheduled and is waiting to start in the next window.</br> - Maintenance started but didn't complete in previous window and will continue in the next one. |
|**InProgress** | Maintenance for resource *region_name* is starting or is in progress. | 
|**Completed** | Maintenance for resource *region_name* is complete. |
|**NoUpdatesPending** | Previously planned maintenance for resource *region_name* is canceled and will be rescheduled for later. |
|**RetryLater** | Planned maintenance for resource *region_name* has started but couldn't progress to the end and will continue in next maintenance window. |

For the full reference of the sample queries and how to use them across tools like PowerShell or Azure CLI, see [Azure Resource Graph sample queries for Azure Service Health](/azure/service-health/resource-graph-samples).

## Related content

- [Maintenance window](maintenance-window.md)
- [Maintenance window FAQ](maintenance-window-faq.yml)
- [Overview of alerts in Microsoft Azure](/azure/azure-monitor/alerts/alerts-overview)
- [Email Azure Resource Manager Role](/azure/azure-monitor/alerts/action-groups#email-azure-resource-manager-role)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
