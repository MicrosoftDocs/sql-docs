---
title: "Troubleshoot best practices assessment"
description: "Describes how to troubleshoot best practices assessment on SQL Server enabled by Azure Arc."
author: nhebbar2011
ms.author: nhebbar
ms.reviewer: mikeray, randolphwest
ms.date: 08/07/2024
ms.topic: troubleshooting
---

# Troubleshoot best practices assessment on SQL Server

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Before you proceed, verify all the necessary [prerequisites](assess.md#prerequisites) are met.

## Log file locations

### Extension log

The extension log file is at:

   `C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\`

The log file name depends on the version Azure Extension for SQL Server. For the latest version of Azure Extension for SQL Server, the log file is:

   `unifiedagent.log`

For extension version `1.1.24724.69` and earlier, the log file is:

   `ExtensionLog_0.log`

### Azure monitor agent log

The Azure monitor agent log is at:

`C:\ProgramData\GuestConfig\extension_logs\Microsoft.Azure.Monitor.AzureMonitorWindowsAgent\Extension.1.log`

You might encounter the following issues when you enable best practices assessment.

## No Log Analytics workspace is available in the dropdown list menu

:::image type="content" source="media/assess/sql-best-practices-assessment-no-workspace.png" alt-text="Screenshot showing the error message when no value is visible in the Log Analytics workspace selector dropdown." lightbox="media/assess/sql-best-practices-assessment-no-workspace.png":::

Ensure that the user configuring SQL BPA must have Log Analytics Contributor role on the resource group or subscription of the Log Analytics workspace. The list of prerequisites can be found [here](assess.md#prerequisites).

## Error notifications requiring users to wait five minutes

:::image type="content" source="media/assess/sql-best-practices-assessment-error-notification.png" alt-text="Screenshot showing the error notifications notifying the users to wait for five minutes." lightbox="media/assess/sql-best-practices-assessment-error-notification.png":::

If such a notification appears and you keep the page open, the portal automatically retries the operation after five minutes. If the page is refreshed, the portal advises you to wait for five minutes before retrying the operation. In case the same error persists after a long time, verify the state of the **WindowsAgent.SqlServer** extension and ensure that the extension isn't stuck in an **Updating** state. In case the extension is still stuck, verify the connectivity of the Arc machine.

## Assessment run failed

In case the assessment run fails, select the corresponding row to open a page that displays the error message.

### SQL Server connection failures, general network error

#### Description

**Connection test for SQL Assessment failed** indicates that the assessment failed to connect to the instance of SQL Server. It returns:

   :::image type="content" source="media/assess/sql-best-practices-assessment-connection-failed.png" alt-text="Screenshot showing the error message that SQL Server is offline.":::

#### Resolution

Follow the steps at [Troubleshoot connectivity issues in SQL Server](/troubleshoot/sql/database-engine/connect/resolve-connectivity-errors-overview).

### Server principal is not able to access model database

#### Description

**The server principal `NT Authority/SYSTEM` is not able to access the database "model" under the current security context.**

The server principal isn't able to access the database under the current security context returns this error in the portal.

   :::image type="content" source="media/assess/sql-best-practices-assessment-connection-failed-model-database.png" alt-text="Screenshot showing the error message that server principal isn't able to access the database.":::

#### Resolution

Ensure the SQL Server built-in login NT AUTHORITY\SYSTEM is a member of the SQL Server sysadmin server role for all the SQL Server instances running on the machine.

If this isn't allowed, you can configure a least privilege account for the Azure extension for SQL Server service on your SQL Server machine. Least privilege account is available for preview.

To configure your server, follow the steps in [Operate SQL Server enabled by Azure Arc with least privilege](configure-least-privilege.md).

### Azure Monitor Agent upload failed

If the error states that the upload failed for Azure Monitor Agent (AMA), verify that the AMA is provisioned and configured correctly. The following components must be configured correctly to ensure that the agent can upload logs to the workspace:

1. The linked Log Analytics workspace must have a table named `SqlAssessment_CL`.
   1. Navigate to the **Tables** tab under the linked Log Analytics workspace.
   1. The `SqlAssessment_CL` table should be present.
1. Azure Monitor Agent (version >= 1.10.0) should be successfully provisioned.
   1. Navigate to the **Extensions** tab under the Arc resource.
   1. AMA with required version should be successfully provisioned.
1. The data collection rule (DCR) and data collection endpoint (DCE) must be in the same location as the Log Analytics workspace.
   1. Navigate to the Overview tab of the resource group to which the Log Analytics workspace belongs.
   1. Under the list of resources, the **DCR** and the **DCE** can be identified by their prefixes, **sqlbpa-**.
   1. Verify that the **DCR** and **DCE** are in the same location as the Log Analytics workspace.
1. The data collection Rule (DCR) should be configured correctly.
   1. Navigate to The **Resources** tab under the relevant DCR. The Arc machine name should be present on the list.
   1. Navigate to The **Data Sources** tab under the relevant DCR. Select the entry **Custom Text Logs**.
      1. Under the **Data Sources** tab, the table name should be `SqlAssessment_CL`.
      1. Under the **Data Sources** tab, the configured log collection path should be `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent\Assessment\*.csv`.
      1. Under the **Destination** tab, the Log Analytics workspace name should be present.

In case any of the components are missing, do the following:

1. Disable assessment by selecting **Configuration** > **Disable assessment**.
1. Confirm that you have the required permissions to enable assessment.
1. Enable assessment by selecting **Enable assessment**.

## Assessment deployment failed

1. Navigate to the deployment and troubleshoot the error.
1. If there are any issues with the deployment of the Azure Monitor Agent, verify that the Arc machine is connected.
1. The deployment can always be retriggered with the same Log Analytics workspace by selecting the **Enable assessment** button.

## Change the Log Analytics workspace

To change the Log Analytics workspace that is linked for the best practices assessment, follow the steps below.

1. Disable best practices assessment if it's currently enabled via the Azure portal.
1. Make a GET call to the API and get the Azure extension for SQL Server settings. For more information, review [How to call Azure REST APIs with curl](/rest/api/azure/#how-to-call-azure-rest-apis-with-curl).

   In order to complete this task, you need to obtain the bearer token in order to perform this action against the resource in Azure portal. From Azure portal:

   1. Navigate to the corresponding **SQL Server - Azure Arc** resource.
   1. Select Ctrl+Shift+I together, go to **Network** tab.
   1. Select **Overview** for the **SQL Server - Azure Arc** resource.
   1. In the name column, locate and select the entry for **ArcServer name?api-version**.
   1. On the right window, go to **Request Headers**.
   1. Copy the complete text for **Authorization: Bearer** to get the bearer authorization token.

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
      "WorkspaceResourceId": null,
      "WorkspaceLocation": null,
      "ResourceNamePrefix": null,
      "settingsSaveTime": 1673278632
    }
    ```

1. Update the workspace related settings to null as follows.

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

1. Make a `PATCH` call to the API, to update the Azure extension for SQL Server assessment settings.

   ```rest
   PATCH https://management.azure.com/subscriptions/ <subscription-id>/resourceGroups/<resource-group-name>/providers/Microsoft.HybridCompute/machines/<arc-resource-name>/extensions/WindowsAgent.SqlServer?api-version=2022-08-11-preview
   ```

1. Go to **Best Practices Assessment** on your Arc-enabled SQL Server resource page in the Azure portal and re-enable best practices assessment and select a new log analytics workspace.

For more assistance, create a support ticket with Microsoft and attach the log files. Visit, [Create an Azure support request](/azure/azure-portal/supportability/how-to-create-azure-support-request)

## Related content

- [Configure SQL best practices assessment - SQL Server enabled by Azure Arc](assess.md)
- [View SQL Server databases - Azure Arc](view-databases.md)
- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)
- [Use activity logs with SQL Server enabled by Azure Arc](activity-logs.md)
- [Data collection and reporting for SQL Server enabled by Azure Arc](data-collection.md)
