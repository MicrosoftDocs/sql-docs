---
title: "Troubleshoot best practices assessment on Azure Arc-enabled SQL Server."
description: "Describes how to troubleshoot best practices assessment on Azure Arc-enabled SQL Server."
author: MikeRayMSFT
ms.author: nhebbar2011
ms.date: 08/06/2023
ms.service: sql
ms.topic: troubleshooting-assessment
---

# Troubleshoot best practices assessment on SQL Server

Before you start, note the logs location. The extension log is created in this folder:

`C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\ExtensionLog_0.log`

The following issues might be encountered while enabling best practices assessment.

## No Log analytics workspace is available in the drop-down menu

   :::image type="content" source="media/assess/sql-best-practices-assessment-no-workspace.png" alt-text="Screenshot showing the error message when no value is visible in the Log Analytics workspace selector dropdown.":::

Verify that the user has the following roles for at least one Log Analytics workspace either at the resource group's scope or at the subscription's scope:

1. Log Analytics Contributor.
2. Contributor.
3. Owner.

## Error notifications requiring users to wait five minutes

    :::image type="content" source="media/assess/sql-best-practices-assessment-error-notification.png" alt-text="Screenshot showing the error notifications notifying the users to wait for five minutes.":::

If such a notification appears and the user keeps the page open, the operation is automatically retriggered after five minutes. If the page is refreshed, the user is advised to wait for five minutes before retrying the operation. In case the same error persists after a long time, verify the state of the **WindowsAgent.SqlServer** extension and ensure that the extension isn't stuck in an **Updating** state. In case the extension is still stuck, verify the connectivity of the Arc machine.

## Assessment run failed

In case the assessment run fails, select the "Failed" hyperlink that should open a page that displays the error message.

1. SQL Connection test failed
    :::image type="content" source="media/assess/sql-best-practices-assessment-connection-failed.png" alt-text="Screenshot showing the error message that SQL Server is offline.":::

    - Start the process and make sure the SQL Server is accessible via the network.
2. AMA upload failed

    - Verify that the Azure Monitor Agent is provisioned correctly and verify that the rest of the setup[label](https://microsoft-ce-csi.acrolinx.cloud/htmldata/en/rules/4842fecd80f7ca65b1a967ad39bb2a19f99dd2ae.html) hasn't been deleted. The following components must be configured correctly to ensure that the agent can upload logs to the workspace:

        1. The linked Log Analytics workspace must have a table named **SqlAssessment_CL**.
        2. Azure Monitor Agent (version >= 1.10.0) must be successfully provisioned.
        3. The Data Collection Rule and Data Collection Endpoint must be in the same location as the LA workspace. If the resources were deployed using the automated deployment, then the resources would have the prefix **sql-bpa-**
        4. The log collection path: **C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent\Assessment\*.cs** must be correctly configured in the Data Collection Rule.
        5. The **Resources** tab under the Data Collection Rule must have the Arc Machine present.
        6. **SqlAssessment_CL** with the selected LA Workspace must be present in the **Data Sources** tab for Data Collection Rule.

## Assessment deployment failed

1. Navigate to the deployment and troubleshoot the error.
2. If there are any issues with the deployment of the Azure Monitor Agent, verify that the Arc Machine is connected.
3. The deployment can always be retriggered with the same LA Workspace by clicking on the **Enable assessment** button.
