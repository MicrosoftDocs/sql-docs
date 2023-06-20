---
title: "Troubleshoot best practices assessment on Azure Arc-enabled SQL Server."
description: "Describes how to troubleshoot best practices assessment on Azure Arc-enabled SQL Server."
author: nhebbar2011
ms.author: nhebbar
ms.reviewer: mikeray
ms.date: 06/16/2023
ms.service: sql
ms.topic: troubleshooting
---

# Troubleshoot best practices assessment on SQL Server

Before you start, ensure that you have met all the necessary [prerequisites](assess.md#prerequisites) for a successful assessment.

Check the logs location. The extension log is created in this folder:

`C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\ExtensionLog_0.log`

Azure Monitor Agent creates logs in this folder:

`C:\ProgramData\GuestConfig\extension_logs\Microsoft.Azure.Monitor.AzureMonitorWindowsAgent\Extension.1.log`

You might encounter the following issues when you enable best practices assessment.

## No Log analytics workspace is available in the drop-down menu

:::image type="content" source="media/assess/sql-best-practices-assessment-no-workspace.png" alt-text="Screenshot showing the error message when no value is visible in the Log Analytics workspace selector dropdown.":::

Verify that the user has the following roles for at least one Log Analytics workspace either at the resource group's scope or at the subscription's scope:

1. Log Analytics Contributor.
2. Contributor.
3. Owner.

## Error notifications requiring users to wait five minutes

:::image type="content" source="media/assess/sql-best-practices-assessment-error-notification.png" alt-text="Screenshot showing the error notifications notifying the users to wait for five minutes.":::

If such a notification appears and you keep the page open, the portal automatically retries the operation after five minutes. If the page is refreshed, the portal advises you to wait for five minutes before retrying the operation. In case the same error persists after a long time, verify the state of the **WindowsAgent.SqlServer** extension and ensure that the extension isn't stuck in an **Updating** state. In case the extension is still stuck, verify the connectivity of the Arc machine.

## Assessment run failed

In case the assessment run fails, select the corresponding row to open a page that displays the error message.

1. SQL Connection test failed

    :::image type="content" source="media/assess/sql-best-practices-assessment-connection-failed.png" alt-text="Screenshot showing the error message that SQL Server is offline.":::

    [Troubleshoot SQL Server connectivity failures](/troubleshoot/sql/database-engine/connect/resolve-connectivity-errors-overview)

2. The server principal isn't able to access the database under the current security context.

    :::image type="content" source="media/assess/sql-best-practices-assessment-connection-failed-model-database.png" alt-text="Screenshot showing the error message that server principal isn't able to access the database.":::

   Assign the SQL Server sysadmin server role to SQL Server built-in login NT AUTHORITY\SYSTEM for all the SQL Server instances running on the machine.

3. Azure Monitor Agent (AMA) upload failed

    Verify that the Azure Monitor Agent is provisioned correctly and the rest of the setup hasn't been deleted. The following components must be configured correctly to ensure that the agent can upload logs to the workspace:

   1. The linked Log Analytics workspace must have a table named `SqlAssessment_CL`.
      1. Navigate to the **Tables** tab under the linked Log Analytics workspace.
      2. The `SqlAssessment_CL` table should be present.
   1. Azure Monitor Agent (version >= 1.10.0) should be successfully provisioned.
      1. Navigate to the **Extensions** tab under the Arc resource.
      2. AMA with required version should be successfully provisioned.
   1. The data collection rule (DCR) and data collection endpoint (DCE) must be in the same location as the LA workspace.
      1. Navigate to the Overview tab of the resource group to which the Log Analytics workspace belongs.
      2. Under the list of resources, the **DCR** and the **DCE** can be identified by their prefixes, **sql-bpa-**.
      3. Verify that the **DCR** and **DCE** are in the same location as the LA workspace.
   1. The data collection Rule (DCR) should be configured correctly.
      1. Navigate to The **Resources** tab under the relevant DCR. The Arc machine name should be present on the list.
      1. Navigate to The **Data Sources** tab under the relevant DCR. Select the entry **Custom Text Logs**.
         1. Under the **Data Sources** tab, the table name should be `SqlAssessment_CL`.
         2. Under the **Data Sources** tab, the configured log collection path should be `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent\Assessment\*.csv`.
         3. Under the **Destination** tab, the LA workspace name should be present.

    In case any of the components are missing:

      1. Disable assessment by selecting **Configuration** > **Disable assessment**.
      2. Confirm that you have the required permissions to enable assessment.
      3. Enable assessment by selecting **Enable assessment**.

## Assessment deployment failed

1. Navigate to the deployment and troubleshoot the error.
2. If there are any issues with the deployment of the Azure Monitor Agent, verify that the Arc machine is connected.
3. The deployment can always be retriggered with the same LA Workspace by clicking on the **Enable assessment** button.

For more assistance, create a support ticket with Microsoft and attach the log files. Visit,  [Create an Azure support request](/azure/azure-portal/supportability/how-to-create-azure-support-request)
