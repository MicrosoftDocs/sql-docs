---
title: Setup alerts and notifications for Azure SQL Managed Instance (Azure portal)
description: Use the Azure portal to create SQL Managed Instance alerts, which can trigger notifications or automation when the conditions you specify are met.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, urmilano
ms.date: 04/29/2024
ms.service: sql-managed-instance
ms.subservice: performance
ms.topic: how-to
---
# Create alerts for Azure SQL Managed Instance using the Azure portal
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/alerts-create.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](alerts-create.md?view=azuresql-mi&preserve-view=true)

This article shows you how to set up alerts for databases in Azure SQL Managed Instance using the Azure portal. This article also provides best practices for setting alert rules.

## Set up new metric alert

You can receive an alert based on monitoring metrics for, or events on, your Azure services. For more information, see [Metric alerts](/azure/azure-monitor/alerts/alerts-types#metric-alerts).

- **Metric values** - The alert triggers when the value of a specified metric crosses a threshold you assign.
- **Activity log events** - An alert can trigger on *every* event, or, only when a certain number of events occur.
- **Resource health** - An alert related to the resource health status events, including whether the event was platform or user-initiated.
- **Log search** - Log Analytics searches on captured log data, including custom log searches or prepared sample queries.

You can configure an alert to do the following when it triggers:

- Send email notifications to the service administrator and coadministrators
- Send email to additional emails that you specify, including for an Azure Resource Manager Role.
- Call a phone number with voice prompt
- Send text message to a phone number
- Send an Azure mobile app notification
- Start an automation runbook
- Call an Azure Function
- Start an Event Hubs action
- Create an ITSM-compatible ticket
- Select a logic app action
- Call a webhook or a secure webhook

You can configure and get information about alert rules using:

- **Azure portal**, as detailed in this article.
- [PowerShell](/azure/azure-monitor/alerts/alerts-create-rule-cli-powershell-arm#create-a-new-alert-rule-using-powershell)
- [Command-line interface (CLI)](/azure/azure-monitor/alerts/alerts-create-rule-cli-powershell-arm#create-a-new-alert-rule-using-the-cli)
- [ARM template](/azure/azure-monitor/alerts/alerts-create-rule-cli-powershell-arm#create-a-new-alert-rule-using-an-arm-template)
- [Azure Monitor REST API](/rest/api/monitor/alertrules)

### <a id="alerting-metrics-available-for-managed-instance"></a> Alert metrics available for Azure SQL Managed Instance

> [!IMPORTANT]
> Alerting metrics are available for the SQL managed instance only. Alerting metrics for individual databases in a SQL managed instance are not available.
> Database diagnostics telemetry is on the other hand available in the form of [diagnostics logs](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md#diagnostic-telemetry-for-export). Alerts on diagnostics logs can be setup from within [SQL Analytics](/azure/azure-monitor/insights/azure-sql) product using [log alert scripts](/azure/azure-monitor/insights/azure-sql#create-alerts-for-sql-managed-instance) for managed instance.

The following SQL managed instance metrics are available for alerting configuration:

| Metric | Description | Unit of measure or possible values |
| :--------- | --------------------- | ----------- |
| **Average CPU percentage** | Average percentage of CPU utilization in selected time period. | 0-100 (percent) |
| **IO bytes read** | IO bytes read in the selected time period. | Bytes |
| **IO bytes written** | IO bytes written in the selected time period. | Bytes |
| **IO requests count** | Count of IO requests in the selected time period. | Numerical |
| **Storage space reserved** | Current max. storage space reserved for the managed instance. Changes with resource scaling operation. | MB (Megabytes) |
| **Storage space used** | Storage space used in the selected period. Changes with storage consumption by databases and the instance. | MB (Megabytes) |
| **Virtual core count** | vCores provisioned for the managed instance. Changes with resource scaling operation. | 4-80 (vCores) |

### Create an alert rule on a metric with the Azure portal

1. In the [Azure portal](https://portal.azure.com/), locate the managed instance you are interested in monitoring, and select it.

1. Select **Metrics** menu item in the Monitoring section.

1. On the **Metric** dropdown, select one of the metrics you wish to set up your alert on (Storage space used is shown in the example).

1. Use **Aggregation** to select the aggregation period - average, minimum, or maximum reached in the given time period (Avg, Min, or Max).

1. Select **New alert rule**.

   :::image type="content" source="media/alerts-create/mi-alerting-menu-annotated.png" alt-text="Screenshot of the metrics explorer in the Azure portal with the Storage space used metric selected." lightbox="media/alerts-create/mi-alerting-menu-annotated.png":::

1. In the **Alert logic** section:

   |Field |Description |
   |---------|---------|
   | **Threshold** | Select if threshold should be evaluated based on a static value or a dynamic value.<br />A static threshold evaluates the rule using the threshold value that you configure.<br />Dynamic Thresholds uses machine learning algorithms to continuously learn the metric behavior pattern and calculate the thresholds automatically. You can learn more about using [dynamic thresholds for metric alerts](/azure/azure-monitor/alerts/alerts-types#dynamic-thresholds). |
   | **Aggregation type** | Aggregation type options are min, max or average (in the aggregation granularity period) |
   | **Operator** | Select the operator for comparing the metric value against the threshold. |
   | **Unit** | If the selected metric signal supports different units, such as bytes, KB, MB, and GB, and if you selected a **static** threshold, enter the unit for the condition logic. |
   | **Threshold value** |If you selected a **static** threshold, enter the threshold value for the condition logic. The threshold value is the alert value that is evaluated based on the operator and aggregation criteria. |
   | **Threshold sensitivity** | If you selected a **dynamic** threshold, enter the sensitivity level. The sensitivity level affects the amount of deviation from the metric series pattern is required to trigger an alert. |
   | **Aggregation granularity** | Select the interval that is used to group the data points using the aggregation type function. Choose an **Aggregation granularity** (Period) that's greater than the **Frequency of evaluation** to reduce the likelihood of missing the first evaluation period of an added time series. |
   | **Frequency of evaluation** |Select how often the alert rule is run. Select a frequency that is smaller than the aggregation granularity to generate a sliding window for the evaluation. |

1. In the **When to evaluate** section:

    |Field |Description |
   |---------|---------|
   | **Check every** | Choose how often the alert rule checks if the condition is met. |
   | **Lookback period** | Choose the lookback period, which is the time period to look back at each time the data is checked. For example, every 1 minute you'll be looking at the past 5 minutes. |

   In the example shown in the screenshot, value of 1,840,876 MB is used representing a threshold value of 1.8 TB. As the operator in the example is set to greater than, the alert will be triggered if the storage space consumption on the SQL managed instance goes over 1.8 TB. The threshold value for storage space metrics must be expressed in MB.

   :::image type="content" source="media/alerts-create/mi-configure-signal-logic-annotated.png" alt-text="Screenshot of Condition tab of the Create alert rule dialog box in the Azure portal. The Alert logic and When to evaluate sections are highlighted." lightbox="media/alerts-create/mi-configure-signal-logic-annotated.png":::

1. Select **Next: Actions >** at the bottom of the page or the **Actions** tab.

1. In the **Actions** tab, select or create the required action group. This action defines what happens upon triggering an alert (for example, sending an email or calling you on the phone). Select an existing [Action group](/azure/azure-monitor/alerts/action-groups) from the **Select action groups** pane, or **Create action group** in your subscription.

   1. Select **+Create action group**.

      :::image type="content" source="media/alerts-create/mi-create-alert-action-group-smaller-annotated.png" alt-text="Screenshot of Actions tab of the Create alert rule dialog box in the Azure portal. The Create action group button is highlighted.":::

   1. Enter an **Action group name** and **Display name** and select the **Region**:

      | Option | Behavior |
      | ------ | -------- |
      | **Global** | The action groups service decides where to store the action group. The action group is persisted in at least two regions to ensure regional resiliency. Processing of actions might be done in any [geographic region](https://azure.microsoft.com/explore/global-infrastructure/geographies/#overview).<br /><br />Voice, SMS, and email actions performed as the result of [service health alerts](/azure/service-health/alerts-activity-log-service-notifications-portal) are resilient to Azure live-site-incidents. |
      | **Regional** | The action group is stored within the selected region. The action group is [zone-redundant](/azure/availability-zones/az-region#highly-available-services). Processing of actions is performed within the region.<br /><br />Use this option if you want to ensure that the processing of your action group is performed within a specific [geographic boundary](https://azure.microsoft.com/explore/global-infrastructure/geographies/#overview). |

      :::image type="content" source="media/alerts-create/mi-add-alerts-action-group-annotated.png" alt-text="Screenshot of the Basics tab of the Create an action group dialog box in the Azure portal." lightbox="media/alerts-create/mi-add-alerts-action-group-annotated.png":::

   1. Select **Next:Notifications>** at the bottom of the page or the **Notifications** tab.

   1. In the **Notifications** tab, define a notification to send when the alert is triggered.

         * **Notification type**: Select **Email Azure Resource Manager Role** to send an email to users who are assigned to certain subscription-level Azure Resource Manager roles or **Email/SMS message/Push/Voice** to send various notification types to specific recipients.

         * **Name**: Enter a unique name for the notification.

         * **Details**: Based on the selected notification type, enter an email address, phone number, or other information.

         * **Common alert schema**: You can choose to turn on the common alert schema, which provides the advantage of having a single extensible and unified alert payload across all the alert services in Monitor. For more information about this schema, see [Common alert schema](/azure/azure-monitor/alerts/alerts-common-schema).

       :::image type="content" source="media/alerts-create/mi-add-alerts-action-group-notifications.png" alt-text="Screenshot of the Notifications tab of the Create action group dialog box. Configuration information for an email notification is visible." lightbox="media/alerts-create/mi-add-alerts-action-group-notifications.png":::

   1. If you need to define a list of actions to trigger when an alert is triggered, select the **Actions** tab and define the actions.

      :::image type="content" source="media/alerts-create/mi-add-alerts-action-group-actions.png" alt-text="Screenshot of the Actions tab of the Create action group dialog box in the Azure portal with the Action type and Name fields highlighted.":::

   1. If you'd like to assign a key-value pair to the action group, select the **Tags** tab. Otherwise, skip this step. By using tags, you can categorize your Azure resources. Tags are available for all Azure resources, resource groups, and subscriptions.

      :::image type="content" source="media/alerts-create/mi-add-alerts-action-group-tabs.png" alt-text="Screenshot of the Tags tab of the Create action group dialog box in the Azure portal. Values are visible in the Name and Value boxes." lightbox="media/alerts-create/mi-add-alerts-action-group-tabs.png":::

   1. To review your settings, select the **Review + create** tab. This step quickly checks your inputs to make sure you've entered all required information. If there are issues, they're reported here. After you've reviewed the settings, select **Create** to create the action group.

      :::image type="content" source="media/alerts-create/mi-add-alerts-action-group-review.png" alt-text="Screenshot of the Review and create tab of the Create an action group dialog box in the Azure portal with the Create button highlighted." lightbox="media/alerts-create/mi-add-alerts-action-group-review.png":::

1. In the **Details** tab, fill in the alert rule details and settings for your records and select the severity type. You also have the option to use the **Custom properties** to add your own properties to the alert rule.

      :::image type="content" source="media/alerts-create/mi-rule-details-complete-smaller-annotated.png" alt-text="Screenshot of the Details tab of the Create alert dialog box in the Azure portal.":::

1. In the **Tags** tab, set any required tags on the alert rule resource. Otherwise, skip this step.

    :::image type="content" source="media/alerts-create/mi-add-alerts-tags.png" alt-text="Screenshot of the Tags tab of the Create an alert rule dialog box in the Azure portal. Values are visible in the Name and Value boxes." lightbox="media/alerts-create/mi-add-alerts-tags.png":::

1. In the **Review + create** tab, a validation will run and inform you of any issues. When validation passes and you've reviewed the settings, select the **Create** button at the bottom of the page.

    :::image type="content" source="media/alerts-create/mi-add-alerts-create-button.png" alt-text="Screenshot of the Review + create tab of the Create an alert rule dialog box in the Azure portal. The Create button is highlighted." lightbox="media/alerts-create/mi-add-alerts-create-button.png":::

The new alert rule will become active within a few minutes and will be triggered based on your settings.

## Create more alert rules in the Azure portal

You can also create alert rules on **Activity log events**, **Resource health**, and **Log search** for your SQL managed instance.

For more information, see:

- [Activity log alerts](/azure/azure-monitor/alerts/alerts-types#activity-log-alerts)
- [Resource health alerts](/azure/azure-monitor/alerts/alerts-types#resource-health-alerts)
- [Log search alerts](/azure/azure-monitor/alerts/alerts-types#log-alerts)

1. In the [Azure portal](https://portal.azure.com/), locate the resource you are interested in monitoring and select it.
1. In the resource menu under **Monitoring**, select **Alerts**. The text and icon might vary slightly for different resources.
1. Select the **+ Create** button, then **Alert rule**.
1. On the **Create an alert rule** page, the **Scope** is automatically configured to the resource.
   > [!NOTE]
   > To avoid sprawl of many independent alerts, you may want to configure alerts for all resources of a type in a subscription, resource group, or an Azure SQL Database logical server. Use the **Scope** tab to change the scope of the new alert rule.

<!-- redundant with above content

  ### [Metric](#tab/metric)
  
  Capture activity peaks or sustained resource stress with alerts on the **Metrics** signal category.
  
  1. On the **Condition** tab, select **See all signals** and **Select a signal** from the list of **Metrics**.
    1. Select the desired metric, for example **Average CPU percentage**. Select **Apply**.
  1. After you select a signal, the **Alert logic** options appear. A preview of recent activity in this resource for that signal is also displayed.
  1. Configure a **Threshold** type to determine when the alert will take action. Choose **Aggregation type**, **Operator**, and **Threshold value** as desired. A typical threshold is: Static, Maximum, Greater than, 80%.

     |Field |Description |
     |---------|---------|
     |**Threshold**|Select if the threshold should be evaluated based on a static value or a dynamic value.<br>A **static threshold** evaluates the rule by using the threshold value that you configure.<br>**Dynamic thresholds** use machine learning algorithms to continuously learn the metric behavior patterns and calculate the appropriate thresholds for unexpected behavior. You can learn more about using [dynamic thresholds for metric alerts](/azure/azure-monitor/alerts/alerts-types#apply-advanced-machine-learning-with-dynamic-thresholds). |
     |**Operator**|Select the operator for comparing the metric value against the threshold. <br>If you're using dynamic thresholds, alert rules can use tailored thresholds based on metric behavior for both upper and lower bounds in the same alert rule. Select one of these operators: <br> - Greater than the upper threshold or lower than the lower threshold (default) <br> - Greater than the upper threshold <br> - Lower than the lower threshold|
     |**Aggregation type**|Select the aggregation function to apply on the data points: Sum, Count, Average, Min, or Max.|
     |**Threshold value**|If you selected a **static** threshold, enter the threshold value for the condition logic.|
     |**Unit**|If the selected metric signal supports different units, such as bytes, KB, MB, and GB, and if you selected a **static** threshold, enter the unit for the condition logic.|
     |**Threshold sensitivity**|If you selected a **dynamic** threshold, enter the sensitivity level. The sensitivity level affects the amount of deviation from the metric series pattern that's required to trigger an alert. <br> - **High**: Thresholds are tight and close to the metric series pattern. An alert rule is triggered on the smallest deviation, resulting in more alerts. <br> - **Medium**: Thresholds are less tight and more balanced. There are fewer alerts than with high sensitivity (default). <br> - **Low**: Thresholds are loose, allowing greater deviation from the metric series pattern. Alert rules are only triggered on large deviations, resulting in fewer alerts.|
     |**Aggregation granularity**| Select the interval that's used to group the data points by using the aggregation type function. Choose an **Aggregation granularity** (period) that's greater than the **Frequency of evaluation** to reduce the likelihood of missing the first evaluation period of an added time series.|
     |**Frequency of evaluation**|Select how often the alert rule is to be run. Select a frequency that's smaller than the aggregation granularity to generate a sliding window for the evaluation.|
  1. Under **When to evaluate**, determine the desired frequency of evaluation. Use the **Check every** and **Lookback period** dropdown lists.
  1. Optionally, you can add multiple conditions for this alert, choose the **Add condition** alert if desired.
  1. Select **Next: Actions >**.
-->
  ### [Resource health](#tab/resource-health)
  
  In Azure SQL Managed Instance, capture events and configure alerts for resource service health in the **Resource health** signal category.
  
  1. On the **Conditions** pane, select **See all signals**.
  1. On the **Select a signal** pane, select **Resource health**. Select **Apply**.
  1. The **Event status**, **Current resource status**, **Previous resource status**, and **Resource type** dropdowns provide options to configure alerts for many possible resource health status changes. By default, all possible status and status changes are select, so that you can send alerts for the detection and resolution of service health problems.

     |Field |Description |
     |---------|---------|
     |**Event status**| Select the statuses of Resource Health events. Values are **Active**, **In Progress**, **Resolved**, and **Updated**.|
     |**Current resource status**|Select the current resource status. Values are **Available**, **Degraded**, and **Unavailable**.|
     |**Previous resource status**|Select the previous resource status. Values are **Available**, **Degraded**, **Unavailable**, and **Unknown**.|
     |**Reason type**|Select the causes of the Resource Health events. Values are **Platform Initiated**, **Unknown**, and **User Initiated**.|
  1. Select **Next: Actions >**.
  
  ### [Activity log](#tab/activity-log)
  
  Capture events and configure alerts for administrative activity in the **Activity log** signal category.
  
  1. On the **Conditions** pane, select **See all signals**.
      1. On the **Select a signal** pane, select the desired signal, for example, **All Administrative options**. Select **Apply**.
  1. After you select a signal, the **Alert logic** options appear and the **Chart period** pane opens.
     1. By default, the **Chart period** shows the last 6 hours of activity. The **Preview** chart shows you the results of your selection.
  1. Select values for each of these fields in the **Alert logic** section:

     |Field |Description |
     |---------|---------|
     |**Event level**| Select the level of the events for this alert rule. Values are **Critical**, **Error**, **Warning**, **Informational**, **Verbose**, and **All**.|
     |**Status**|Select the status levels for the alert.|
     |**Event initiated by**|Select the user or service principal that initiated the event.|
  1. Select **Next: Actions >**.

  ### [Log search](#tab/log-search)

  Searches on captured log data in Log Analytics, including custom log searches or prepared sample queries.

  1. On the **Conditions** pane, select **See all signals**. On the **Select a signal** pane.
      1. Select the desired signal, for example, the example query **Storage on managed instances above 90%**. Select **Apply**.
  1. Review the sample Kusto query. Use the **Logs** query window to sample the query's results. When you are satisfied with your KQL query, select **Continue Editing Alert**.
  1. Confirm settings for **Measurement**. Choose **Measure**, **Aggregation type**, **Aggregation granularity** as desired.
  1. Review settings under **Split by dimensions**, to monitor dimensions by time series, by a specific resource, or other context. You can use dimensions when you create log search alert rules to monitor for the same condition on multiple Azure resources.
  1. Review the **Alert logic** and provide a **Threshold value**. A typical threshold is: Greater than, 90, 5 minutes.
  1. Select **Next: Actions >**.

---

1. Select an existing [Action group](/azure/azure-monitor/alerts/action-groups) from the **Select action groups** pane, or **Create action group** in your subscription. An action group enables you to define the action to be taken when an alert condition occurs. This action defines what happens upon triggering an alert (for example, sending an email).
    1. Alternatively, use the **Use quick actions** feature to create a new action group and provide an email or Azure mobile app notification target.
1. Select **Next: Details >**.
1. Provide a **Subscription** and **Resource group**.
1. By default, an alert rule's severity is **3 - Informational**. You can adjust the alert rule's **Severity** as desired.
1. Provide the **Alert rule name**. Optionally but recommended, provide a description.
1. Provide a **Region** for the alert rule.
1. Choose an **Identity** for the alert rule. You can opt to have the alert rule use the **System assigned managed identity** (SAMI), **User assigned managed identity** (UAMI). For more information, see [Identity](/azure/azure-monitor/alerts/alerts-create-log-alert-rule#managed-id).
1. Under **Advanced options**:
    1. Make sure **Enable upon creation** for the alert rule to start running as soon as you're done creating it.
    1. The **Automatically resolve alerts** options is enabled by default. This will make the alert stateful, which means that the alert is resolved when the condition isn't met anymore.
1. Select **Next: Tags >**.
1. Consider using Azure tags. For example, the "Owner" or "CreatedBy" tag to identify who created the resource, and the "Environment" tag to identify whether this resource is in production, development, etc. For more information, see [Develop your naming and tagging strategy for Azure resources](/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging).
1. Select **Review + create**.
1. Select **Create**. Soon, the alert is active.

## <a id="verifying-alerts"></a> Verify alerts

> [!NOTE]
> To suppress noisy alerts, see [Suppression of alerts using action rules](/azure/azure-monitor/alerts/alerts-action-rules#suppression-of-alerts).

Upon setting up an alerting rule, verify that you are satisfied with the alerting trigger and its frequency. For the example shown on this page for setting up an alert on storage space used, if your alerting option was email, you might receive an email such as the following:

   :::image type="content" source="media/alerts-create/mi-email-alert-example-smaller-annotated.png" alt-text="Screenshot of an example of the email that is sent when an alert is triggered.":::

The email shows the alert name, details of the threshold and why the alert was triggered, helping you to verify and troubleshoot your alert.

- Select the **See in Azure portal** button to view the alert you received via email in the Azure portal.
- Select **View Rule >** to view or edit the alert rule that triggered the alert.
- Select **View Resource >** to view the managed instance for which the alert was triggered.

## <a id="view-suspend-activate-modify-and-delete-existing-alert-rules"></a> Manage alert rules

> [!NOTE]
> Existing alerts need to be managed from the **Alerts** menu in the Azure portal resource menu. Existing alerts cannot be modified from Managed Instance resource pane.

To view, suspend, activate, modify, and delete existing alerts:

1. Search for Alerts using Azure portal search. Select **Alerts**.

   :::image type="content" source="media/alerts-create/mi-manage-alerts-browse-smaller-annotated.png" alt-text="Screenshot of the search box in the Azure portal. The 'alerts' search term and Alerts service in the search results are highlighted.":::

   Alternatively, you could also select **Alerts** on the Azure navigation bar, if you have it configured.

1. On the **Alerts** pane, select **Alert rules**.

   :::image type="content" source="media/alerts-create/mi-manage-alert-rules-smaller-annotated.png" alt-text="Screenshot of the Alerts page in the Azure portal with the Alert rules button highlighted.":::

1. Select an individual existing alert rule to manage it. Existing active rules can be modified and tuned to your preference. Active rules can also be suspended without being deleted.

## Related content

- [Azure Monitor: Create or edit a metric alert rule](/azure/azure-monitor/alerts/alerts-create-metric-alert-rule)
- [Overview of alerts in Microsoft Azure](/azure/azure-monitor/alerts/alerts-overview)
- [Understand how metric alerts work in Azure Monitor](/azure/azure-monitor/alerts/alerts-metric-overview)
