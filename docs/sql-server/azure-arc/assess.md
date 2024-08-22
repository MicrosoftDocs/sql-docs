---
title: Configure best practices assessment
description: Learn how to configure best practices assessment on an instance of SQL Server enabled by Azure Arc.
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 06/14/2023
ms.topic: conceptual
---

# Configure best practices assessment for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The *best practices assessment* feature provides a mechanism to evaluate the configuration of your SQL Server instance. After you enable the feature, an assessment scans your SQL Server instance and databases to provide recommendations for things like:

- SQL Server and database configurations
- Index management
- Deprecated features
- Enabled or missing trace flags
- Statistics

The duration of an assessment run can be a few minutes to an hour, depending on your environment (for example, number of databases and objects). The size of an assessment result also depends on your environment.

An assessment runs against your instance and all databases on that instance. In our testing, we observed that an assessment run can have up to 10% CPU impact on the machine. In these tests, we ran the assessment while an application similar to the TPC-C benchmark ran against the SQL Server instance.

This article provides instructions for using best practices assessment on an instance of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].

> [!IMPORTANT]
> Best practices assessment is available only for SQL Server instances purchased through either [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) or [pay-as-you-go](https://www.microsoft.com/sql-server/sql-server-2022-pricing) licensing options.
>
> For instructions to configure the appropriate license type, review [Configure SQL Server enabled by Azure Arc](manage-configuration.md).

## Prerequisites

[!INCLUDE [best-practices-prerequisites](includes/best-practices-prerequisites.md)]

## Enable best practices assessment

1. Sign in to the [Azure portal](https://portal.azure.com/) and go to your [Azure Arc-enabled SQL Server resource](https://portal.azure.com/#view/Microsoft_Azure_HybridCompute/AzureArcCenterBlade/~/sqlServers).

1. On the left pane, select **Best practices assessment**.

   :::image type="content" source="media/assess/sql-best-practices-assessment-launch.png" alt-text=" Screenshot that shows how to open settings for best practices assessment for an Azure Arc-enabled SQL Server resource.":::

   An alternative is to select **Overview** on the left pane, select the **Capabilities** tab, and then select **Best practices assessment**.

1. In the **Log Analytics Workspace** dropdown list, select your workspace.

   :::image type="content" source="media/assess/enable-log-analytics-workspace.png" alt-text=" Screenshot that shows the box for specifying a Log Analytics workspace for SQL Server best practices assessment.":::

   If you didn't create a Log Analytics workspace or you don't have the Log Analytics Contributor role assigned for the resource group or subscription, you can't initiate the on-demand SQL Server assessment. Review the [prerequisites](#prerequisites).

1. Select **Enable assessment**.

   :::image type="content" source="media/assess/click-on-enable.png" alt-text=" Screenshot that shows the button for enabling best practices assessment for an Azure Arc-enabled SQL Server resource.":::

   Setup and configuration can take a few minutes. After the process finishes, best practices assessment is enabled for all SQL Server instances running on the machine and can assess the SQL Server host comprehensively.

1. Confirm that you successfully enabled the feature. By default, the assessment is scheduled to run every Sunday at 12:00 AM local time.

   :::image type="content" source="media/assess/sql-best-practices-assessment-enabled.png" alt-text=" Screenshot that shows the successful enablement of best practices assessment for an Azure Arc-enabled SQL Server resource.":::

## Enable best practices assessment at scale by using Azure Policy

You can automatically enable best practices assessment on multiple Azure Arc-enabled SQL Server instances at scale by using an Azure Policy definition called **Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment**.

This policy definition is not assigned to a scope by default. If you assign this policy definition to a scope of your choice, it enables the best practices assessment on all SQL Server instances enabled for Azure Arc within the defined scope. By default, the assessment is scheduled to run every Sunday at 12:00 AM local time.

# [Portal](#tab/portal)

1. In the Azure portal, go to **Azure Policy** > **Definitions**.

1. Search for **Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment** and select the policy.

1. Select **Assign**.

1. Choose a scope.

1. Select **Next**.

1. On the **Parameters** tab:

   1. Select **Only show parameters that need input for review**, if the checkbox isn't already selected.
   1. Select **Log Analytics workspace** and **Log Analytics workspace location** from the respective dropdown menus.
   1. Set the **Enablement** value to **true** to enable the best practices assessment. (Setting this value to **false** disables the assessment.)
   1. Select **Next**.

1. On the **Remediation** tab:

   1. Select **Create a remediation task**.
   1. Choose **System assigned managed identity** (recommended) or **User assigned managed identity**.

1. Select **Review + Create**.

1. Select **Create**.

# [PowerShell](#tab/powershell)

```powershell
# Define the resource group and policy 
$rg = Get-AzResourceGroup -Name "<Resource Group Name>"
$policyAssignmentName = "SQLBestPracticesAssessmentAssignment"
$policyDefinitionName = "Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment."
$policyDefinition = Get-AzPolicyDefinition |
  Where-Object { $_.Properties.DisplayName -eq 'Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment.'}

# Assign policy parameters
$policyParameterObj = @{
    "effect" = "DeployIfNotExists"
    "laWorkspaceId" = "<Log Analytics Workspace ID>"
    "laWorkspaceLocation" = "<Log Analytics Workspace Location>"
    "isEnabled" = $true 
}

# Assign the policy
New-AzPolicyAssignment -Name $policyAssignmentName `
    -DisplayName $policyDefinitionName `
    -PolicyDefinition $policyDefinition `
    -Scope $rg.ResourceId `
    -PolicyParameterObject $policyParameterObj `
    -IdentityType 'SystemAssigned' `
    -Location $rg.Location

# Verify the policy assignment
Get-AzPolicyAssignment -Name $policyAssignmentName -Scope $rg.ResourceId
```

----

For general instructions about how to assign an Azure policy by using the Azure portal or an API of your choice, see the [Azure Policy documentation](/azure/governance/policy).

> [!NOTE]
> If you select the Log Analytics workspace from a different resource group than the SQL Server resource, the scope of the Azure policy must be the whole subscription.

### Modify the license type

If an instance of SQL Server is configured with a **License only** type of license, you need to change the license type to configure best practices assessment. On the **Best practices assessment** pane of the portal, select **Change license type**. For more information, see [Configure SQL Server enabled by Azure Arc](manage-configuration.md).

:::image type="content" source="media/assess/change-license-type.png" alt-text="Screenshot of the button for changing the license type in the Azure portal.":::

## Manage best practices assessment

After you enable best practices assessment, you can run or configure the assessment as required on the **Best practices assessment** pane.

> [!NOTE]
> When you perform any of the following tasks on a specific SQL Server instance, the task is applied to all SQL Server instances running on the machine.

- To run the assessment on demand from the portal, select **Run assessment**.

   :::image type="content" source="media/assess/run-assessment.png" alt-text=" Screenshot that shows the button for running an assessment.":::

- To view assessment results, select the **View assessment results** button.

  **View assessment results** is inactive until the results are ready in the Log Analytics workspace. This process might take up to two hours after the data files are processed on the target machine.

- To schedule an assessment, select **Configuration**, change the information as needed, and then select **Schedule assessment**.

   :::image type="content" source="media/assess/configure-schedule.png" alt-text=" Screenshot that shows the pane for configuring an assessment schedule. ":::

- To disable an assessment, select **Configuration** > **Disable assessment**.

   :::image type="content" source="media/assess/configure-disable.png" alt-text=" Screenshot that shows the button for disabling an assessment. ":::

## View results of best practices assessment

To view results, you can select any of the row items on the **Best practices assessment** pane.

### Results

The **Results** pane reports all the issues, categorized based on their severity, for all the SQL Server instances running on the machine. You can switch the results view between the SQL Server instances running on the machine and assessment execution times by using the **Instance name** and **Collected at** menus, respectively.

The recommendations are organized into these tabs that help you keep track of the progress between runs:

- **All**: All the recommendations from the currently selected run.
- **New**: Newer recommendations compared to the previous run.
- **Resolved**: Resolved recommendations from previous runs.
- **Insights**: The most recurring issues and the databases with the maximum number of issues.

The graph groups assessment results into categories of severity: **High**, **Medium**, **Low**, and **Information**. Select each category to see the list of recommendations, or search for key phrases in the search box. It's best to start with the most severe recommendations and go down the list.

The first grid shows each recommendation and the affected instances in the environment with the reported issues. When you select a row in the first grid, the second grid lists all the affected instances for that particular recommendation. If no recommendation is selected, the second grid shows all the recommendations.

You can perform any of these actions:

- If the assessment reports a large number of recommendations, you can filter the results. To filter results, use the dropdown menu above the grid to select **Name**, **Severity**, or **Check Id**.

- To download results, use **Export to Excel**.

- To open the results in Log Analytics, use **Open the last run query in the Logs view**.

- To view recommendations that your system already follows, check the **Passed** section of the graph.

- To view detailed information for each recommendation, such as a long description and relevant online resources, select **Message**.

### Trends

The **Trends** pane uses three charts to show changes over time: all issues, new issues, and resolved issues. The charts help you see your progress.

Ideally, the number of recommendations should decrease while the number of resolved issues increases. The legend shows the average number of issues for each severity level. Hover over the bars to see the individual values for each run.

If there are multiple runs in a single day, only the latest run is included in the graphs on the **Trends** pane.

## Considerations

- Best practices assessment is currently limited to SQL Server running on Windows machines. The assessment doesn't work for SQL Server on Linux machines.

- It might take a few seconds to populate the history of the previous execution of the assessment on the **Best practices assessment** pane.

- You can also view the assessment results by directly querying the Log Analytics workspaces. For example queries, see the [blog post on best practices assessment for Azure Arc-enabled SQL Server resources](https://techcommunity.microsoft.com/t5/sql-server-blog/best-practices-assessment-arc-enabled-sql-server/ba-p/3715776).

- Don't make any other extension configuration changes while the Azure policy is remediating noncompliant Azure Arc-enabled SQL Server resources. [Track remediation task progress for a policy](/azure/governance/policy/how-to/remediate-resources?tabs=azure-portal#step-3-track-remediation-task-progress).

## Troubleshooting

See the [troubleshooting guide](troubleshoot-assessment.md).

## Related content

- Review the [set of nearly 500 rules](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-assessment-api/DefaultRuleset.csv) that best practices assessment applies.
- Learn about [Microsoft Unified](https://www.microsoft.com/en-us/microsoft-unified), which can provide comprehensive support of the best practices assessment feature.
- Learn how to [inventory and view SQL Server databases as Azure Arc-enabled resources](view-databases.md).
