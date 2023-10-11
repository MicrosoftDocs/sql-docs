---
title: "Troubleshoot best practices assessment on Azure Arc-enabled SQL Server."
description: "Describes how to troubleshoot best practices assessment on Azure Arc-enabled SQL Server."
author: nhebbar2011
ms.author: nhebbar
ms.reviewer: mikeray
ms.date: 06/16/2023
ms.topic: troubleshooting
---

# Troubleshoot best practices assessment on SQL Server

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Before you start, ensure that you have met all the necessary [prerequisites](assess.md#prerequisites) for a successful assessment.

Check the logs location. The extension log is created in this folder:

`C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\ExtensionLog_0.log`

Azure Monitor Agent creates logs in this folder:

`C:\ProgramData\GuestConfig\extension_logs\Microsoft.Azure.Monitor.AzureMonitorWindowsAgent\Extension.1.log`

You might encounter the following issues when you enable best practices assessment.

## No Log Analytics workspace is available in the drop-down menu

:::image type="content" source="media/assess/sql-best-practices-assessment-no-workspace.png" alt-text="Screenshot showing the error message when no value is visible in the Log Analytics workspace selector dropdown.":::

Ensure that the user configuring SQL BPA must have Log Analytics Contributor role on the resource group or subscription of the Log Analytics workspace. The list of prerequisites can be found [here](assess.md#prerequisites).

## Error notifications requiring users to wait five minutes

:::image type="content" source="media/assess/sql-best-practices-assessment-error-notification.png" alt-text="Screenshot showing the error notifications notifying the users to wait for five minutes.":::

If such a notification appears and you keep the page open, the portal automatically retries the operation after five minutes. If the page is refreshed, the portal advises you to wait for five minutes before retrying the operation. In case the same error persists after a long time, verify the state of the **WindowsAgent.SqlServer** extension and ensure that the extension isn't stuck in an **Updating** state. In case the extension is still stuck, verify the connectivity of the Arc machine.

## Assessment run failed

In case the assessment run fails, select the corresponding row to open a page that displays the error message.

### SQL Server connection failures, general network error

#### Description

**Connection test for SQL Assessment failed** indicates that he assessment failed to connect to the instance of SQL Server. It returns:

   :::image type="content" source="media/assess/sql-best-practices-assessment-connection-failed.png" alt-text="Screenshot showing the error message that SQL Server is offline.":::

#### Resolution

Follow the steps at [Troubleshoot SQL Server connectivity failures](/troubleshoot/sql/database-engine/connect/resolve-connectivity-errors-overview).

### Server principal is not able to access model database

#### Description

**The server principal `NT Authority/SYSTEM` is not able to access the database "model" under the current security context.**

The server principal isn't able to access the database under the current security context returns this error in the portal.

   :::image type="content" source="media/assess/sql-best-practices-assessment-connection-failed-model-database.png" alt-text="Screenshot showing the error message that server principal isn't able to access the database.":::

#### Resolution

Ensure the SQL Server built-in login NT AUTHORITY\SYSTEM is a member of the SQL Server sysadmin server role for all the SQL Server instances running on the machine. If this isn't allowed, we have implemented the *least privileged account* for running the Azure extension for SQL Server service on your SQL Server machine. Least Privilege account is available for preview. To participate in the preview, please open a support case to set up a preview with a least privileged account for you to test.

> [!NOTE]
> This feature implements the principle of least privilege. It's available as a limited preview. To participate in the preview, contact Microsoft support for assistance configuring the solution.

### Azure Monitor Agent upload failed

If the error states that the Azure Monitor Agent (AMA) upload failed, verify that the AMA is provisioned and configured correctly. The following components must be configured correctly to ensure that the agent can upload logs to the workspace:

1. The linked Log Analytics workspace must have a table named `SqlAssessment_CL`.
   1. Navigate to the **Tables** tab under the linked Log Analytics workspace.
   1. The `SqlAssessment_CL` table should be present.
1. Azure Monitor Agent (version >= 1.10.0) should be successfully provisioned.
   1. Navigate to the **Extensions** tab under the Arc resource.
   2. AMA with required version should be successfully provisioned.
1. The data collection rule (DCR) and data collection endpoint (DCE) must be in the same location as the Log Analytics workspace.
   1. Navigate to the Overview tab of the resource group to which the Log Analytics workspace belongs.
   2. Under the list of resources, the **DCR** and the **DCE** can be identified by their prefixes, **sql-bpa-**.
   3. Verify that the **DCR** and **DCE** are in the same location as the Log Analytics workspace.
1. The data collection Rule (DCR) should be configured correctly.
   1. Navigate to The **Resources** tab under the relevant DCR. The Arc machine name should be present on the list.
   1. Navigate to The **Data Sources** tab under the relevant DCR. Select the entry **Custom Text Logs**.
      1. Under the **Data Sources** tab, the table name should be `SqlAssessment_CL`.
      2. Under the **Data Sources** tab, the configured log collection path should be `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent\Assessment\*.csv`.
      3. Under the **Destination** tab, the Log Analytics workspace name should be present.

In case any of the components are missing, do the following:

1. Disable assessment by selecting **Configuration** > **Disable assessment**.
2. Confirm that you have the required permissions to enable assessment.
3. Enable assessment by selecting **Enable assessment**.

## Assessment deployment failed

1. Navigate to the deployment and troubleshoot the error.
2. If there are any issues with the deployment of the Azure Monitor Agent, verify that the Arc machine is connected.
3. The deployment can always be retriggered with the same Log Analytics workspace by clicking on the **Enable assessment** button.

## Change the Log Analytics workspace

To change the Log Analytics workspace that is linked for the best practices assessment, follow the steps below. 

1. Disable best practices assessment if it's currently enabled.
1. Make a GET call to the API and get the Azure extension for SQL Server settings

   ```rest
   GET https://edge.management.azure.com/subscriptions/ <subscription-id>/resourceGroups/<resource-group-name>/providers/Microsoft.HybridCompute/machines/<arc-resource-name>/extensions/WindowsAgent.SqlServer?api-version=2022-03-10
   ```

   The best practices assessment settings before the change.

    ```json
    "AssessmentSettings": {
      "Enable": true,
      "RunImmediately": true,
      "schedule": {
        "dayOfWeek": "Sunday",
        "Enable": true,
        "monthlyOccurrence": null,
        "StartDate": null,
        "startTime": "00:00",
        "WeeklyInterval": 1
      },
      "WorkspaceResourceId": "/subscriptions/<subscriptionID>/resourceGroups/<Resource group name>/providers/Microsoft.OperationalInsights/workspaces/shivgupta-bpa-test-la-ws",
      "WorkspaceLocation": "<Region>",
      "ResourceNamePrefix": "<Log analytics workspace name>",
      "settingsSaveTime": 1673278632
    }
    ```

1. Update the workspace related settings to null as below.

   ```json
   "AssessmentSettings": {
     "Enable": false,
     "RunImmediately": true,
     "schedule": {
       "dayOfWeek": "Sunday",
       "Enable": true,
       "monthlyOccurrence": null,
       "StartDate": null,
       "startTime": "00:00",
       "WeeklyInterval": 1
     },
     "WorkspaceResourceId": null,
     "WorkspaceLocation": null,
     "ResourceNamePrefix": null,
     "SettingsSaveTime": 1673278632
   }
   ```

1. Make a `PATCH` call below to the API to update the Azure extension for SQL Server assessment settings.

   ```rest
   PATCH https://management.azure.com/subscriptions/ <subscription-id>/resourceGroups/<resource-group-name>/providers/Microsoft.HybridCompute/machines/<arc-resource-name>/extensions/WindowsAgent.SqlServer?api-version=2022-08-11-preview
   ```

1. Go to **Best Practices Assessment** on your Arc-enabled SQL Server resource page in the Azure portal and re-enable best practices assessment and select a new log analytics workspace.

For more assistance, create a support ticket with Microsoft and attach the log files. Visit,  [Create an Azure support request](/azure/azure-portal/supportability/how-to-create-azure-support-request)

## Next steps

- [Configure SQL best practices assessment](assess.md)
- [View SQL Server databases - Azure Arc](view-databases.md)
- [Manage SQL Server license and billing options](manage-configuration.md)
- [Azure Arc-enabled SQL Server and Databases activity logs](activity-logs.md)
- [Data collected by Arc enabled SQL Server](data-collection.md)