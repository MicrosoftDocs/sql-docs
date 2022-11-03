---
title: Setup alerts and notifications for Managed Instance (Azure portal)
description: Use the Azure portal to create SQL Managed Instance alerts, which can trigger notifications or automation when the conditions you specify are met.
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma, wiassaf
ms.date: 05/04/2020
ms.service: sql-managed-instance
ms.subservice: performance
ms.topic: how-to
---
# Create alerts for Azure SQL Managed Instance using the Azure portal
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article shows you how to set up alerts for databases in Azure SQL Managed Instance Database using the Azure portal. Alerts can send you an email, call a web hook, execute Azure Function, runbook, call an external ITSM compatible ticketing system, call you on the phone or send a text message when some metric, such as for example instance storage size or CPU usage, reaches a predefined threshold. This article also provides best practices for setting alert periods.


## Overview

You can receive an alert based on monitoring metrics for, or events on, your Azure services.

* **Metric values** - The alert triggers when the value of a specified metric crosses a threshold you assign in either direction. That is, it triggers both when the condition is first met and then afterwards when that condition is no longer being met.

You can configure an alert to do the following when it triggers:

* Send email notifications to the service administrator and coadministrators
* Send email to additional emails that you specify.
* Call a phone number with voice prompt
* Send text message to a phone number
* Call a webhook
* Call Azure Function
* Call Azure runbook
* Call an external ticketing ITSM compatible system

You can configure and get information about alert rules using [the Azure portal, PowerShell or the Azure CLI](/azure/azure-monitor/alerts/alerts-classic-portal) or [Azure Monitor REST API](/rest/api/monitor/alertrules). 

## Alerting metrics available for managed instance

> [!IMPORTANT]
> Alerting metrics are available for managed instance only. Alerting metrics for individual databases in managed instance are not available. 
> Database diagnostics telemetry is on the other hand available in the form of [diagnostics logs](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md#diagnostic-telemetry-for-export). Alerts on diagnostics logs can be setup from within [SQL Analytics](/azure/azure-monitor/insights/azure-sql) product using [log alert scripts](/azure/azure-monitor/insights/azure-sql#create-alerts-for-sql-managed-instance) for managed instance.

The following managed instance metrics are available for alerting configuration:

| Metric | Description | Unit of measure \ possible values |
| :--------- | --------------------- | ----------- |
| Average CPU percentage | Average percentage of CPU utilization in selected time period. | 0-100 (percent) |
| IO bytes read | IO bytes read in the selected time period. | Bytes |
| IO bytes written | IO bytes written in the selected time period. | Bytes |
| IO requests count | Count of IO requests in the selected time period. | Numerical |
| Storage space reserved | Current max. storage space reserved for the managed instance. Changes with resource scaling operation. | MB (Megabytes) |
| Storage space used | Storage space used in the selected period. Changes with storage consumption by databases and the instance. | MB (Megabytes) |
| Virtual core count | vCores provisioned for the managed instance. Changes with resource scaling operation. | 4-80 (vCores) |

## Create an alert rule on a metric with the Azure portal

1. In Azure [portal](https://portal.azure.com/), locate the managed instance you are interested in monitoring, and select it.

2. Select **Metrics** menu item in the Monitoring section.

3. On the **Metric** drop-down menu, select one of the metrics you wish to set up your alert on (Storage space used is shown in the example).

4. Use **Aggregation** to select the aggregation period - average, minimum, or maximum reached in the given time period (Avg, Min, or Max).

5. Select **New alert rule**.

   :::image type="content" source="./media/alerts-create/mi-alerting-menu-annotated.png" lightbox="./media/alerts-create/mi-alerting-menu-annotated.png" alt-text="Screenshot of the metrics explorer in the Azure portal with the Storage space used metric selected.":::

6. In the **Alert logic** section:

   |Field |Description |
   |---------|---------|
   | Threshold | Select if threshold should be evaluated based on a static value or a dynamic value.<br>A static threshold evaluates the rule using the threshold value that you configure.<br>Dynamic Thresholds uses machine learning algorithms to continuously learn the metric behavior pattern and calculate the thresholds automatically. You can learn more about using [dynamic thresholds for metric alerts](/azure/azure-monitor/alerts/alerts-types#dynamic-thresholds). |
   | Aggregation type | Aggregation type options are min, max or average (in the aggregation granularity period) |
   | Operator | Select the operator for comparing the metric value against the threshold. |
   | Unit | If the selected metric signal supports different units, such as bytes, KB, MB, and GB, and if you selected a **static** threshold, enter the unit for the condition logic. |
   | Threshold value |If you selected a **static** threshold, enter the threshold value for the condition logic. The threshold value is the alert value which will be evaluated based on the operator and aggregation criteria. |
   | Threshold sensitivity | If you selected a **dynamic** threshold, enter the sensitivity level. The sensitivity level affects the amount of deviation from the metric series pattern is required to trigger an alert. |
   | Aggregation granularity | Select the interval that is used to group the data points using the aggregation type function. Choose an **Aggregation granularity** (Period) that's greater than the **Frequency of evaluation** to reduce the likelihood of missing the first evaluation period of an added time series. |
   | Frequency of evaluation |Select how often the alert rule is be run. Select a frequency that is smaller than the aggregation granularity to generate a sliding window for the evaluation. |

7. In the **When to evaluate** section:

    |Field |Description |
   |---------|---------|
   | Check every | Choose how often the alert rule will check if the condition is met. |
   | Lookback period | Choose the lookback period, which is the time period to look back at each time the data is checked. For example, every 1 minute you'll be looking at the past 5 minutes. |

   In the example shown in the screenshot, value of 1840876 MB is used representing a threshold value of 1.8 TB. As the operator in the example is set to greater than, the alert will be created if the storage space consumption on the managed instance goes over 1.8 TB. Note that the threshold value for storage space metrics must be expressed in MB.

   :::image type="content" source="./media/alerts-create/mi-configure-signal-logic-annotated.png" lightbox="./media/alerts-create/mi-configure-signal-logic-annotated.png" alt-text="Screenshot of Condition tab of the Create alert rule dialog box in the Azure portal. The Alert logic and When to evaluate sections are highlighted.":::

8. Select **Next: Actions>** at the bottom of the page or the **Actions** tab.

9. In the **Actions** tab, select or create the required action group. This action defines what will happen upon triggering an alert (for example, sending an email or calling you on the phone). If you need to create a new action group:

   1. Select **+Create action group**.

      :::image type="content" source="./media/alerts-create/mi-create-alert-action-group-smaller-annotated.png" lightbox="./media/alerts-create/mi-create-alert-action-group-smaller-annotated.png" alt-text="Screenshot of Actions tab of the Create alert rule dialog box in the Azure portal. The Create action group button is highlighted.":::

   1. Enter an **Action group name** and **Display name** and select the **Region**:

      | Option | Behavior |
      | ------ | -------- |
      | Global | The action groups service decides where to store the action group. The action group is persisted in at least two regions to ensure regional resiliency. Processing of actions may be done in any [geographic region](https://azure.microsoft.com/explore/global-infrastructure/geographies/#overview).<br></br>Voice, SMS and email actions performed as the result of [service health alerts](/azure/azure-monitor/service-health/alerts-activity-log-service-notifications-portal.md) are resilient to Azure live-site-incidents. |
      | Regional | The action group is stored within the selected region. The action group is [zone-redundant](/azure/availability-zones/az-region#highly-available-services). Processing of actions is performed within the region.</br></br>Use this option if you want to ensure that the processing of your action group is performed within a specific [geographic boundary](https://azure.microsoft.com/explore/global-infrastructure/geographies/#overview). |

      :::image type="content" source="./media/alerts-create/mi-add-alerts-action-group-annotated.png" lightbox="./media/alerts-create/mi-add-alerts-action-group-annotated.png" alt-text="Screenshot of Basics tab of the Create an action group dialog box in the Azure portal.":::

   1. Select **Next:Notifications>** at the bottom of the page or the **Notifications** tab.

   1. In the **Notifications** tab, define a notification to send when the alert is triggered.

         * **Notification type**: Select **Email Azure Resource Manager Role** to send an email to users who are assigned to certain subscription-level Azure Resource Manager roles or **Email/SMS message/Push/Voice** to send various notification types to specific recipients.

         * **Name**: Enter a unique name for the notification.

         * **Details**: Based on the selected notification type, enter an email address, phone number, or other information.

         * **Common alert schema**: You can choose to turn on the common alert schema, which provides the advantage of having a single extensible and unified alert payload across all the alert services in Monitor. For more information about this schema, see [Common alert schema](/azure/azure-monitor/alerts/alerts-common-schema.md).

       :::image type="content" source="./media/alerts-create/mi-add-alerts-action-group-notifications.png" lightbox="./media/alerts-create/mi-add-alerts-action-group-notifications.png" alt-text="Screenshot of the Notifications tab of the Create action group dialog box. Configuration information for an email notification is visible.":::

   1. If you need to define a list of actions to trigger when an alert is triggered, select the **Actions** tab and define the actions such as execute a webhook, Azure function, or runbook or create an ITSM ticket in your compatible system.

      :::image type="content" source="./media/alerts-create/mi-add-alerts-action-group-actions.png" lightbox="./media/alerts-create/mi-add-alerts-action-group-actions.png" alt-text="Screenshot of the Actions tab of the Create action group dialog box in the Azure portal with the Action type and Name fields highlighted.":::

   1. If you'd like to assign a key-value pair to the action group, select the **Tags** tab. Otherwise, skip this step. By using tags, you can categorize your Azure resources. Tags are available for all Azure resources, resource groups, and subscriptions.

      :::image type="content" source="./media/alerts-create/mi-add-alerts-action-group-tabs.png" lightbox="./media/alerts-create/mi-add-alerts-action-group-tabs.png" alt-text="Screenshot of the Tags tab of the Create action group dialog box in the Azure portal. Values are visible in the Name and Value boxes.":::

   1. To review your settings, select the **Review + create** tab. This step quickly checks your inputs to make sure you've entered all required information. If there are issues, they're reported here. After you've reviewed the settings, select **Create** to create the action group.

      :::image type="content" source="./media/alerts-create/mi-add-alerts-action-group-review.png" lightbox="./media/alerts-create/mi-add-alerts-action-group-review.png" alt-text="Screenshot of the Review and create tab of the Create an action group dialog box in the Azure portal with the Create button highlighted.":::

10. In the **Details** tab, fill in the alert rule details and settings for your records and select the severity type. You also have the option to use the **Custom properties** to add your own properties to the alert rule.

      :::image type="content" source="./media/alerts-create/mi-rule-details-complete-smaller-annotated.png" lightbox="./media/alerts-create/mi-rule-details-complete-smaller-annotated.png" alt-text="Screenshot of the Details tab of the Create alert dialog box in the Azure portal.":::

11. In the **Tags** tab, set any required tags on the alert rule resource. Otherwise, skip this step.

    :::image type="content" source="./media/alerts-create/mi-add-alerts-tags.png" lightbox="./media/alerts-create/mi-add-alerts-tags.png" alt-text="Screenshot of the Tags tab of the Create an alert rule dialog box in the Azure portal. Values are visible in the Name and Value boxes.":::


12. In the **Review + create** tab, a validation will run and inform you of any issues. When validation passes and you've reviewed the settings, select the **Create** button at the bottom of the page.

    :::image type="content" source="./media/alerts-create/mi-add-alerts-create-button.png" lightbox="./media/alerts-create/mi-add-alerts-create-button.png" alt-text="Screenshot of the Review + create tab of the Create an alert rule dialog box in the Azure portal. The Create button is highlighted.":::

The new alert rule will become active within a few minutes and will be triggered based on your settings.

## Verifying alerts

> [!NOTE]
> To supress noisy alerts, see [Supression of alerts using action rules](/azure/azure-monitor/alerts/alerts-action-rules#suppression-of-alerts).

Upon setting up an alerting rule, verify that you are satisfied with the alerting trigger and its frequency. For the example shown on this page for setting up an alert on storage space used, if your alerting option was email, you might receive an email such as the one shown below.

   :::image type="content" source="./media/alerts-create/mi-email-alert-example-smaller-annotated.png" lightbox="./media/alerts-create/mi-email-alert-example-smaller-annotated.png" alt-text="Screenshot of an example of the email that is sent when an alert is triggered.":::

The email shows the alert name, details of the threshold and why the alert was triggered, helping you to verify and troubleshoot your alert. 

* Select the **See in Azure portal** button to view the alert you received via email in the Azure portal. 
* Select **View Rule >** to view or edit the alert rule that triggered the alert.
* Select **View Resource >** to view the managed instance for which the alert was triggered.

## View, suspend, activate, modify and delete existing alert rules

> [!NOTE]
> Existing alerts need to be managed from the Alerts menu from Azure portal dashboard. Existing alerts cannot be modified from Managed Instance resource blade.

To view, suspend, activate, modify and delete existing alerts:

1. Search for Alerts using Azure portal search. Click on Alerts.

   :::image type="content" source="./media/alerts-create/mi-manage-alerts-browse-smaller-annotated.png" lightbox="./media/alerts-create/mi-manage-alerts-browse-smaller-annotated.png" alt-text="Screenshot of the search box in the Azure portal. The 'alerts' search term and Alerts service in the search results are highlighted.":::

   Alternatively, you could also click on Alerts on the Azure navigation bar, if you have it configured.

2. On the Alerts pane, select **Alert rules**.

   :::image type="content" source="./media/alerts-create/mi-manage-alert-rules-smaller-annotated.png" lightbox="./media/alerts-create/mi-manage-alert-rules-smaller-annotated.png" alt-text="Screenshot of the Alerts page in the Azure portal with the Alert rules button highlighted.":::

   List of existing alerts will show up. Select an individual existing alert rule to manage it. Existing active rules can be modified and tuned to your preference. Active rules can also be suspended without being deleted. 

## Next steps

* Learn about Azure Monitor alerting system, see [Overview of alerts in Microsoft Azure](/azure/azure-monitor/alerts/alerts-overview)
* Learn more about metric alerts, see [Understand how metric alerts work in Azure Monitor](/azure/azure-monitor/alerts/alerts-metric-overview)
* Learn about configuring a webhook in alerts, see [Call a webhook with a classic metric alert](/azure/azure-monitor/alerts/alerts-webhooks)
* Learn about configuring and managing alerts using PowerShell, see [Action rules](/powershell/module/az.monitor/add-azmetricalertrulev2)
* Learn about configuring and managing alerts using API, see [Azure Monitor REST API reference](/rest/api/monitor/)
