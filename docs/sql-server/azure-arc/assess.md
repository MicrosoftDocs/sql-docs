---
title: Configure best practices assessment on an Azure Arc-enabled SQL Server instance
description: Configure best practices assessment on an Azure Arc-enabled SQL Server instance
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 03/03/2023
ms.service: sql
ms.topic: conceptual
---

# Configure SQL best practices assessment

Best practices assessment provides a mechanism to evaluate the configuration of your SQL Server. After you enable best practices assessment, an assessment scans your SQL Server instance and databases to provide recommendations for things like:

- SQL Server and database configurations
- Index management
- Deprecated features
- Enabled or missing trace flags
- Statistics
- & more

Assessment run time depends on your environment (number of databases, objects, and so on), with a duration from a few minutes, up to an hour. Similarly, the size of the assessment result also depends on your environment. Assessment runs against your instance and all databases on that instance. In our testing, we observed that an assessment run can have up to 5-10% CPU impact on the machine. In these tests, the assessment was done while a TPC-C like application was running against the SQL Server.

This article provides instructions for using best practices assessment on an instance of Azure Arc-enabled SQL Server.

The Environment Health assessment is replaced with a best practices assessment (SQL BPA). You need to reconfigure the SQL BPA to continue to get SQL server assessments. You can still access the previous health assessments by querying the table SQLAssessmentRecommendation from Log Analytics workspace used by Environment Health assessments. You can also query and export the data from previous assessments into Excel. See the steps at [Integrate Log Analytics and Excel](/azure/azure-monitor/logs/log-excel).

>[!IMPORTANT]
>Best practices assessment is available only for SQL Servers purchased through either [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) or [pay-as-you-go (PAYG)](https://www.microsoft.com/sql-server/sql-server-2022-pricing) licensing options.
>
>For instructions to configure the appropriate license type, review [Manage SQL Server license and billing options](manage-license-type.md).

## Prerequisites

- Your Windows-based SQL Server instance is connected to Azure. Follow the instructions to [onboard your SQL Server instance to Arc-enabled SQL Server](connect.md).

  > [!NOTE]
  > Best practices assessment is currently limited to SQL Server running on Windows machines. This will not work for SQL on Linux machines.

- If SQL Server is hosting a single SQL Server instance, make sure that the version of Azure Extension for SQL Server (`WindowsAgent.SqlServer`) is "**1.1.2202.47**" or later.  In the case of SQL Server hosting multiple SQL Server instances, make sure that the version of Azure Extension for SQL Server (`WindowsAgent.SqlServer`) is greater than "**1.1.2231.59".** Learn how to [check the](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions)**[Azure Extension for SQL Server](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions)**[ version and update to the latest.](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions)
- [A Log Analytics workspace](/azure/azure-monitor/logs/quick-create-workspace?tabs=azure-portal) in the same subscription as your Arc-enabled SQL Server resource to upload assessment results to.
- The user configuring SQL BPA must have the following permissions.
  - Log Analytics Contributor role on resource group or subscription of the Log Analytics workspace.
  - Azure Connected Machine Resource Administrator role on the resource group or subscription of the Arc-enabled SQL Server.
  - Monitoring Contributor role on the Resource group or subscription of Log Analytics Workspace & Resource group or subscription of Arc Machine.
  - Users can be assigned to built-in roles such as Contributor or Owner. These roles have sufficient permissions. For more information, review [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal) for more information.

- The minimum permissions required to access or read the assessment report are:
  - Reader role on the resource group or subscription of the Arc-enabled SQL Server resource.
  - [Log analytics reader](/azure/azure-monitor/logs/manage-access?tabs=portal#log-analytics-reader).
  - [Monitoring reader](/azure/role-based-access-control/built-in-roles#monitoring-reader) on resource group/subscription of Log analytics workspace.
  - The SQL Server built-in login **NT AUTHORITY\SYSTEM** must be the member of SQL Server **sysadmin** server role for all the SQL Server instances running on the machine.
  - If your firewall or proxy server restricts outbound connectivity, make sure they allow to Azure Arc over TCP port 443 for these URLs.

    - `global.handler.control.monitor.azure.com`
    - `<virtual-machine-region-name>.handler.control.monitor.azure.com`
    - `<log-analytics-workspace-id>.ods.opinsights.azure.com`

- Your SQL Server instance must have the [TCP/IP protocol enabled](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

- The [SQL Server browser service](../../tools/configuration-manager/sql-server-browser-service.md) must be running if you're operating a named instance of SQL Server.

- If you use Azure policy, *Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment.* , to enable assessment at [scale](#enable-best-practices-assessment-at-scale-using-azure-policy), you need below.

   To create an Azure Policy assignment, your subscription requires the Resource Policy Contributor role assignment for the scope that you're targeting. The scope may be either subscription or resource group. Further, if you are going to create a new user assigned managed identity, you need the User Access Administrator role assignment in the subscription.

## Enable best practices assessment

1. Sign into the [Azure portal](https://portal.azure.com/) and go to your [Arc-enabled SQL Server resource](https://portal.azure.com/#view/Microsoft_Azure_HybridCompute/AzureArcCenterBlade/~/sqlServers)

1. Open your Arc-enabled SQL Server resource and select **Best practices assessment** in the left pane or **Best practices assessment**  tab in the **Capabilities** tab of the **Overview** page.

   :::image type="content" source="media/assess/sql-best-practices-assessment-launch.png" alt-text="Screenshot showing how to enable the best practices assessment screen of an Arc-enabled SQL Server resource.":::

1. If the Log Analytics Workspace is not created or the current user does not have Log Analytics Contributor role assigned for the resource group or subscription, you can't initiate the on-demand SQL Assessment.  Review the [Prerequisites](#prerequisites).

   :::image type="content" source="media/assess/enable-log-analytics-workspace.png" alt-text="Screenshot showing how to specify the log analytics workspace for SQL Server best practices assessment.":::

1. Select the  **Log Analytics Workspace** from the drop-down menu and select **Enable assessment**.

   :::image type="content" source="media/assess/click-on-enable.png" alt-text="Screenshot showing the enable best practices assessment screen of an Arc-enabled SQL Server resource.":::

   > [!NOTE]
   > After you enable the assessment, setup and configuration can take a few minutes.
   >
   > Best practices assessment is enabled for all SQL Server instances running on the machine and assess the SQL Server host comprehensively.

1. Upon successful best practices assessment deployment, the assessment is scheduled to run every Sunday 12:00 AM local time by default.

   :::image type="content" source="media/assess/sql-best-practices-assessment-enabled.png" alt-text="Screenshot showing the successful enablement of best practices assessment of an Arc-enabled SQL Server resource.":::

## Enable best practices assessment at scale using Azure policy

You can automatically enable best practices assessment on multiple Arc-enabled SQL Server instances at scale using an Azure policy definition called *Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment.*  This policy definition is not assigned to a scope by default. If you assign this policy definition to a scope of your choice, it enables the SQL best practices assessment on all the Arc-enabled SQL Servers within the defined scope, and auto schedule to every Sunday 12:00 AM local time by default.


>[!IMPORTANT]
>The policy enables best practices assessment only for SQL Servers purchased through either [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) or [pay-as-you-go (PAYG)](https://www.microsoft.com/sql-server/sql-server-2022-pricing) licensing options.
>
>For instructions to configure the appropriate license type, review [Manage SQL Server license and billing options](manage-license-type.md).

1. Navigate to **Azure Policy** in the Azure portal and choose **Definitions**. 
1. Search for *Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment.* and click on the policy.
1. Select **Assign**. 
1. Choose a Scope.
1. Select **Next**.
1. On the "Parameters" tab, select **Only show parameters that need input for review**, if the checkbox not already selected.
   1. Select **Log Analytics Workspace**, **Log Analytics Workspace location**, from drop-down menus respectively.
   1. Provide **Enablement** value to *true* for enabling the best practices assessment. Provide *false* to disable the assessment.
   1. Select **Next**
1. On the **Remediation** tab, click **Create a remediation task**.
1. Choose **System assigned managed identity** (recommended) or **User assigned managed identity** .
1. Click **Review + Create**.
1. Click **Create**.

See [Azure Policy documentation](/azure/governance/policy) for general instructions about how to assign an Azure policy using Azure portal or an API of your choice.


### Modify license type

If an instance of SQL Server is configured with a license only type of license, you will need to change the license type to configure best practices assessment. For more information, see [Manage SQL Server license and billing options](manage-license-type.md).

:::image type="content" source="media/assess/change-license-type.png" alt-text="Screenshot of Azure portal change license type.":::

## Manage best practices assessment

After you have enabled the best practices assessment, you can run, or configure the assessment as required.

- To run the assessment on demand from the portal, select **Run assessment**.

   :::image type="content" source="media/assess/run-assessment.png" alt-text="Screenshot showing run assessment.":::

   > [!NOTE]
   > Performing any of the tasks below on a specific SQL Server instance will be applied to all SQL Server instances running on the machine.

   The **View assessment results** is inactive until the results are ready in Log Analytics. This process might take up to two hours after the data files are processed on the target machine.

   :::image type="content" source="media/assess/configure-schedule.png" alt-text="Screen shot showing configuration control and schedule control. ":::

- To schedule assessments, select **Configuration** > **Schedule assessment**.

   :::image type="content" source="media/assess/configure-disable.png" alt-text="Screen shot showing configuration control and disable assessment control. ":::

- To disable an assessment select **Configuration** > **Disable assessment**.

## View best practices assessment results

- On the **Best practices assessment** pane, select **View assessment results**.

## Results page

The **Results** page reports all the issues categorized based on their severity for all the SQL Server instances running on the machine.  You can switch the results view between the SQL Server instances running on the machine and assessment execution times  using the top-down menus "Instance name" and "Collected at" respectively.  The recommendations are organized into **All**, **New,** and **Resolved** tabs. The tabs can be used to view all the recommendations from the currently selected run, the newer recommendations compared to the previous run, and the resolved recommendations from the previous runs respectively. The tabs help to keep track of the progress between the runs. The **Insights** tab identifies the most recurring issues and the databases with the maximum number of issues.

The graph groups assessment results in different categories of severity - high, medium, low, and information. Select each category to see the list of recommendations, or search for key phrases in the search box. It's best to start with the most severe recommendations and go down the list.

The first grid shows each recommendation and the affected instances in the environment with the reported issues. When a row is selected in the first grid, the second grid lists all the affected instances for that particular recommendation. If no recommendation is selected, then the second grid shows all the recommendations. If the assessment reports a large number of recommendations, you can filter the results. 

To filter results, use the drop-down menu above the grid. Namely:

- **Name**
- **Severity**
- **Tags**
- **Check Id**. 

To download results, use **Export to Excel**.

To open the results in Log Analytics, use **Open the last run query in the Logs view**.

The **passed** section of the graph identifies recommendations your system already follows.
View detailed information for each recommendation by selecting the **Message** field, such as a long description, and relevant online resources.

## Trends page

There are three charts on the **Trends** page to show changes over time: all issues, new issues, and resolved issues. The charts help you see your progress. Ideally, the number of recommendations should decrease while the number of resolved issues increases. The legend shows the average number of issues for each severity level. Hover over the bars to see the individual values for each run.

If there are multiple runs in a single day, only the latest run is included in the graphs on the **Trends** page.

## Known issues

- Best practices assessment is currently limited to SQL Server running on Windows machines. This will not work for SQL on Linux machines.
- It may take a few seconds to populate the history of the previous execution of the assessments on the best practices home page.
- A failure on one or more SQL Server instances, mark the assessment failure for all SQL Server instances running on the same machine.  You can view the assessment results for successful SQL Server instances by directly querying the log analytical workspaces.  You can find a few sample queries [here](https://techcommunity.microsoft.com/t5/sql-server-blog/best-practices-assessment-arc-enabled-sql-server/ba-p/3715776).
- If Log analytics workspace is selected from a different resource group than the Arc-enabled SQL Server resource, the scope of the Azure policy must be the whole subscription.
- Do not make any other extension configuration changes while the Azure policy is remediating the noncomplaint Arc-enabled SQL Server resources. [Track Azure policy remediation task progress.](/azure/governance/policy/how-to/remediate-resources?tabs=azure-portal#step-3-track-remediation-task-progress)

## Next steps

- [Connect your SQL Server to Azure Arc](connect.md).

- [Connect SQL Server instances to Azure at scale](connect-at-scale-policy.md)

- Review the [rich set of nearly 500 rules](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-assessment-api/DefaultRuleset.csv) best practices assessment applies.

- To obtain comprehensive support of the best practices assessment feature, a Premier or Unified support subscription is required. For details, see [Azure Premier Support](https://azure.microsoft.com/support/plans/premier).

- [View SQL Server databases - Azure Arc](view-databases.md)

