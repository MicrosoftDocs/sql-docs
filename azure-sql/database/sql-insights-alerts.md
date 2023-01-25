---
title: Create alerts with SQL Insights (preview)
description: Create alerts with SQL Insights (preview) in Azure Monitor
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/29/2022
ms.service: sql-db-mi
ms.topic: conceptual
ms.custom: subject-monitoring
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Create alerts with SQL Insights (preview)
[!INCLUDE [sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

SQL Insights (preview) includes a set of alert rule templates you can use to create [alert rules in Azure Monitor](/azure/azure-monitor/alerts/alerts-overview) for common SQL issues. The alert rules in SQL Insights (preview) are log alert rules based on performance data stored in the *InsightsMetrics* table in Azure Monitor Logs.  

> [!NOTE]
> To create an alert for SQL Insights (preview) using a resource manager template, see [Resource Manager template samples for SQL Insights (preview)](/azure/azure-monitor/insights/resource-manager-sql-insights#create-an-alert-rule-for-sql-insights).


> [!NOTE]
> If you have requests for more SQL Insights (preview) alert rule templates, please send feedback using the link at the bottom of this page or using the SQL Insights (preview) feedback link in the Azure portal.

## Enable alert rules 
Use the following steps to enable the alerts in Azure Monitor from the Azure portal. The alert rules that are created will be scoped to all of the SQL resources monitored under the selected monitoring profile.  When an alert rule is triggered, it will trigger on the specific SQL instance or database.

> [!NOTE]
> You can also create custom [log alert rules](/azure/azure-monitor/alerts/alerts-log) by running queries on the data sets in the *InsightsMetrics* table and then saving those queries as an alert rule. 

Select **SQL (preview)** from the **Insights** section of the Azure Monitor menu in the Azure portal. Select **Alerts**.

:::image type="content" source="media/sql-insights-alerts/alerts-button.png" alt-text="Screenshot of the Azure Monitor page for SQL in the Azure portal. The Alerts button is highlighted.":::

The **Alerts** pane opens on the right side of the page. By default, it will display fired alerts for SQL resources in the selected monitoring profile based on the alert rules you've already created. Select **Alert templates**, which will display the list of available templates you can use to create an alert rule.

:::image type="content" source="media/sql-insights-alerts/alert-templates.png" alt-text="Screenshot of the Alerts page for Azure Monitor in the Azure portal. In the tab for Alert templates, a Create rule link for one of the alerts is highlighted.":::

On the **Create Alert rule** page, review the default settings for the rule and edit them as needed. You can also select an [action group](/azure/azure-monitor/alerts/action-groups) to create notifications and actions when the alert rule is triggered. Select **Enable alert rule** to create the alert rule once you've verified all of its properties.


:::image type="content" source="media/sql-insights-alerts/alert-rule.png" alt-text="Screenshot of the Alert rules page for Azure Monitor in the Azure portal. The Create Alert rule (step 1 of 2) page shows the thresholds to use for alerts, the name and severity of the alert, and an alert group for notifications (optional).":::

To deploy the alert rule immediately, select **Deploy alert rule**. Select **View Template** if you want to view the rule template before actually deploying it.

:::image type="content" source="media/sql-insights-alerts/alert-rule-deploy.png" alt-text="Screenshot of the Deploy alert rule page for Azure Monitor in the Azure portal. The Create Alert rule (step 2 of 2) page shows 'Ready to deploy your log alert' and the Deploy alert rule button is highlighted.":::

If you choose to view the templates, select **Deploy** from the template page to create the alert rule.

:::image type="content" source="media/sql-insights-alerts/view-template-deploy.png" alt-text="Screenshot of the Deploy from view template page for Azure Monitor in the Azure portal, displaying the json of the alert. The Deploy menu button is highlighted. ":::


## Next steps

Learn more about [alerts in Azure Monitor](/azure/azure-monitor/alerts/alerts-overview).

