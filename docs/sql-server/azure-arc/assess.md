---
title: Configure best practices assessment on an Azure Arc-enabled SQL Server instance
description: Configure best practices assessment on an Azure Arc-enabled SQL Server instance
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 01/12/2023
ms.service: sql
ms.topic: conceptual
---

# Configure best practices assessment | Azure Arc-enabled SQL Server

Best practices assessment provides a mechanism to evaluate the configuration of your SQL Server. Once the best practices assessment feature is enabled, your SQL Server instance and databases are scanned to provide recommendations for things like SQL Server and database configurations, index management, deprecated features, enabled or missing trace flags, statistics, etc.

Assessment run time depends on your environment (number of databases, objects, and so on), with a duration from a few minutes, up to an hour. Similarly, the size of the assessment result also depends on your environment. Assessment runs against your instance and all databases on that instance. In our testing, we observed that an assessment run can have up to 5-10% CPU impact on the machine. In these tests, the assessment was done while a TPC-C like application was running against the SQL Server.

This article provides instructions for using best practices assessment on an instance of Azure Arc-enabled SQL Server.

The Environment Health assessment is replaced with a much richer best practices assessment (SQL BPA). You will have to re-configure the SQL BPA to continue to get SQL server assessments. You can still access the previous health assessments by querying the table SQLAssessmentRecommendation from Log Analytics workspace used by Environment Health assessments. You can also query and export the data from previous assessments into Excel. See the steps at [Integrate Log Analytics and Excel](/azure/azure-monitor/logs/log-excel).

>[!IMPORTANT]
>Best practices assessment is available only for SQL Servers purchased through either [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) or [pay-as-you-go (PAYG)](https://www.microsoft.com/sql-server/sql-server-2022-pricing) licensing options.

## Prerequisites

- Your Windows-based SQL Server instance is connected to Azure. Follow the instructions to [onboard your SQL Server instance to Arc-enabled SQL Server](connect.md).

  > [!NOTE]
  > Best practices assessment is currently limited to SQL Server running on Windows machines. This will not work for SQL on Linux machines.

- Make sure that the version of Azure Extension for SQL Server (WindowsAgent.SqlServerI is "**1.1.2202.47**" or above. Learn how to [check the Azure Extension for SQL Server and update to the latest.](/azure/azure-arc/servers/manage-vm-extensions-portal)
- [A Log Analytics workspace](/azure/azure-monitor/logs/quick-create-workspace?tabs=azure-portal) in the same subscription as your Arc-enabled SQL Server resource to upload assessment results to.
- The user configuring SQL BPA must have following permissions.

  - Log Analytics Contributor role on Resource Group or Subscription of the Log Analytics workspace.
  - Azure Connected Machine Resource Administrator role on the Resource Group or Subscription of the Arc-enabled SQL Server.
  - Monitoring Contributor role on the Resource group or Subscription of Log Analytics Workspace &
Resource group or Subscription of Arc Machine.

    Users can be assigned to built-in roles such as Contributor or Owner. These roles have sufficient permissions. For more information, review [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal) for more information.

- The SQL Server built-in login **NT AUTHORITY\SYSTEM** must be the member of SQL Server **sysadmin** server role. 

- If outbound connectivity is restricted by your firewall or proxy server, make sure the URLs from target SQL Server machine, make sure the URLs listed below allowed access to Azure Arc over TCP port 443.

  - `global.handler.control.monitor.azure.com`
  - `<virtual-machine-region-name>.handler.control.monitor.azure.com`
  - `<log-analytics-workspace-id>.ods.opinsights.azure.com`

- Your SQL Server instance must have the [TCP/IP protocol enabled](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

- The [SQL Server browser service](../../tools/configuration-manager/sql-server-browser-service.md) must be running if you're operating a named instance of SQL Server.

- Upgrade the Azure SQL Server extension version. Use the latest version from the [release notes](/sql/sql-server/azure-arc/release-notes). You can upgrade to latest version by updating the extension, in the extension management of Arc-Server. For details, see [Upgrade extensions](/en-us/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions).
   
## Enable best practices assessment

1. Sign into the [Azure portal](https://portal.azure.com/) and go to your [Arc-enabled SQL Server resource](https://portal.azure.com/#view/Microsoft_Azure_HybridCompute/AzureArcCenterBlade/~/sqlServers)

1. Open your Arc-enabled SQL Server resource and select **Best practices assessment** in the left pane or **Best practices assessment**  tab in the **Capabilities** tab of the **Overview** page.

   :::image type="content" source="media/assess/sql-best-practices-assessment-launch.png" alt-text="Screenshot showing how to enable the best practices assessment screen of an Arc-enabled SQL Server resource.":::

   If the Log Analytics Workspace is not created or current user does not have Log Analytics Contributor role assigned for the Resource Group or Subscription, you can't initiate the on-demand SQL Assessment.  Review the Prerequisites section above.

   :::image type="content" source="media/assess/enable-log-analytics-workspace.png" alt-text="Screenshot showing how to specify the log analytics workspace for SQL Server best practices assessment..":::


1. Select the  **Log Analytics Workspace** from the drop-down menu and select **Enable assessment**.

   :::image type="content" source="media/assess/click-on-enable.png" alt-text="Screenshot showing the enable best practices assessment screen of an Arc-enabled SQL Server resource.":::

   > [!NOTE]
   > After you enable the assessment, setup and configuration can take a few minutes.

1. Upon successful best practices assessment deployment, the assessment is scheduled to run every Sunday 12:00 AM local time by default.

   :::image type="content" source="media/assess/sql-best-practices-assessment-enabled.png" alt-text="Screenshot showing the successful enablement of best practices assessment of an Arc-enabled SQL Server resource.":::

## Manage best practices assessment

After you have enabled best practices assessment, you can do the following additional tasks:

- To run assessment on-demand, select **Run Assessment**.
- To change the default schedule, select **Configuration** and **Schedule assessment**.

   :::image type="content" source="media/assess/sql-best-practices-assessment-change-schedule.png" alt-text="Screenshot showing how to change the schedule of best practices assessment and scheduled screen of an Arc-enabled SQL Server resource.":::

- To disable an assessment select **Configuration** and **Disable assessment**.

   :::image type="content" source="media/assess/sql-best-practices-assessment-disable.png" alt-text="Screenshot showing how to disable the best practices assessment of an Arc-enabled SQL Server resource.":::

## View best practices assessment results

- On the **Best practices assessment** pane, select the **View assessment results** button.

  The **View assessment results** button remains disabled until the results are ready in Log Analytics. This process might take up to two hours after the data files are processed on the target machine.

  :::image type="content" source="media/assess/sql-best-practices-assessment-view.png" alt-text="Screenshot showing the View Assessment results.":::

## Results page

The **Results** page reports all the issues categorized based on their severity. The recommendations are organized into **All**, **New** and **Resolved** tabs. The tabs can be used to view all the recommendations from the currently selected run, the newer recommendations compared to the previous run, and the resolved recommendations from the previous runs respectively. The tabs help to keep track of the progress between the runs. The **Insights** tab identifies the most recurring issues and the databases with the maximum number of issues.

The graph groups assessment results in different categories of severity - high, medium, low, and information. Select each category to see the list of recommendations, or search for key phrases in the search box. It's best to start with the most severe recommendations and go down the list.
The first grid shows each recommendation and the affected instances in the environment with the reported issues. When a row is selected in the first grid, the second grid lists all the affected instances for that particular recommendation. If no recommendation is selected, then the second grid shows all the recommendations. In case of a large number of reported recommendations, the results can be filtered using the drop-downs above the grid, namely **Name, Severity, Tags, and Check Id**. The **Export to Excel** and **Open the last run query in the Logs view** options at the top right of the grid can also be used to download the results and open the results in the Log Analytics workspace blade respectively.

The **passed** section of the graph identifies recommendations your system already follows.
View detailed information for each recommendation by selecting the **Message** field, such as a long description, and relevant online resources.

## Trends page

There are three charts on the **Trends** page to show changes over time: all issues, new issues, and resolved issues. The charts help you see your progress. Ideally, the number of recommendations should go down while the number of resolved issues goes up. The legend shows the average number of issues for each severity level. Hover over the bars to see the individual vales for each run.

If there are multiple runs in a single day, only the latest run is included in the graphs on the **Trends** page.

## Known issues

- Best practices assessment is currently limited to SQL Server running on Windows machines. This will not work for SQL on Linux machines.

- The assessment is enabled on a default instance if present, otherwise on the very first named instance in registry. The assessment results of the instance where the assessment was enabled, is duplicated for every instance on that SQL Server resource.

- It may take few seconds to populate the history of the previous execution of the assessments on best practices home page.

## Next steps

- [Connect your SQL Server to Azure Arc](connect.md).

- [Connect SQL Server instances to Azure at scale](connect-at-scale.md)

- Review the [rich set of nearly 500 rules](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-assessment-api/DefaultRuleset.csv) best practices assessment applies.

- To obtain comprehensive support of the best practices assessment feature, a Premier or Unified support subscription is required. For details, see [Azure Premier Support](https://azure.microsoft.com/support/plans/premier).

- [View SQL Server databases - Azure Arc](view-databases.md)