---
title: Configure on-demand SQL Assessment on an Azure Arc-enabled SQL Server instance
description: Configure on-demand SQL Assessment on an Azure Arc-enabled SQL Server instance
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 09/12/2022
ms.service: sql
ms.topic: conceptual
---
# Configure SQL Assessment | Azure Arc-enabled SQL Server

SQL Assessment provides a mechanism to evaluate the configuration of your SQL Server. This article provides instructions for using SQL Assessment on an instance of Azure Arc-enabled SQL Server.

> [!NOTE]
> The environment health assessment will be upgraded to a much richer SQL Server Best Practice Assessment (SQL BPA). At that point, you may have to re-configure the SQL BPA assessment and continue to get SQL Server assessments.  You can still access the previous health assessments by querying the table SQLAssessmentRecommendation from Log Analytical workspace used by Environment Health assessments.  You can also query and export the previous assessments data into Excel. See the steps at [Integrate Log Analytics and Excel](/azure/azure-monitor/logs/log-excel).

## Prerequisites

- Your Windows-based SQL Server instance is connected to Azure. Follow the instructions to [onboard your SQL Server instance to Arc-enabled SQL Server](connect.md).

  > [!NOTE]
  > On-demand SQL Assessment is currently limited to SQL Server running on Windows machines. This will not work for SQL on Linux machines.

- The Microsoft Monitoring Agent (MMA) must be installed and configured on the machine. View the [Install MMA](configure-advanced-data-security.md#install-log-analytics-agent) article for instructions. You can also get more information on the [Log Analytics Agent](/azure/azure-monitor/platform/log-analytics-agent) article.

- Your SQL Server instance must have the [TCP/IP protocol enabled](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

- The [SQL Server browser service](../../tools/configuration-manager/sql-server-browser-service.md) must be running if you're operating a named instance of SQL Server.

- Make sure you've reviewed the SQL Server document at [Services Hub On-Demand Assessments Prerequisites](/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents).

## Run on-demand SQL Assessment

1. Open your Arc-enabled SQL Server resource and select **Environment Health** in the left pane.

   :::image type="content" source="media/assess/sql-assessment-heading-sql-server-arc.png" alt-text="Screenshot showing the Environment Health screen of an Arc-enabled SQL Server resource.":::

   If the MMA extension isn't installed, you can't initiate the on-demand SQL Assessment.

2. Select the  account type. If you have a Managed service account, it will allow you to initiate SQL Assessment directly from the portal. Specify the account name.

   Specifying a *Managed service account* activates the **Configure SQL Assessment** button so you can initiate the assessment from the portal by deploying a *CustomScriptExtension*. Because you can only deploy one *CustomScriptExtension* at a time, the script extension for SQL Assessment will be automatically removed after execution.

   If you already have another *CustomScriptExtension* deployed to the hosting machine,  the **Configure SQL Assessment** button won't be activated.

3. Specify a working directory on the data collection machine if you want to change the default. By default, `C:\sql_assessment\work_dir` is used. During collection and analysis, the assessment temporarily stores data in that folder. If the folder doesn't exist, the assessment creates it automatically.

4. If you initiate SQL Assessment from the portal by selecting **Configure SQL Assessment**, the portal presents a standard deployment bubble.

   :::image type="content" source="media/assess/sql-assessment-custom-script-deployment.png" alt-text="Screenshot showing deployment of the CustomScriptExtension.":::

   Alternatively, you can initiate SQL Assessment from the target machine. Select **Download configuration script**, copy the downloaded script to the target machine and execute one of the following code blocks in an admin instance of **powershell.exe**:

   - *Domain account*:  You'll be prompted for the user account and password.

     ```powershell
     Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force
     & '.\AddSqlAssessment.ps1'
     ```

   - *Managed Service Account (MSA)*

     ```powershell
     Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force
     & '.\AddSqlAssessment.ps1' -ManagedServiceAccountName <MSA account name>
     ```

   The script schedules a task named *SQLAssessment*, which triggers data collection. This task executes within an hour after you've run the script. It then repeats every seven days.

   You can modify the task to run on a different date and time or even force it to run immediately. In the task scheduler library, find **Microsoft** > **Operations Management Suite** > **AOI\*\*\*** > **Assessments** > **SQLAssessment**.

## View SQL Assessment results

- On the *Environment Health* pane, select the **View SQL Assessment results** button.

  The **View SQL Assessment results** button remains disabled until the results are ready in Log Analytics. This process might take up to two hours after the data files are processed on the target machine.

  :::image type="content" source="media/assess/sql-assessment-results.png" alt-text="Screenshot showing the SQL Assessment results.":::

- You can see the state of data processing on the collection machine by checking the files in the working folder. After the scheduled task is completed, you should see several files with the *new.* prefix in the working directory.

  :::image type="content" source="media/assess/sql-assessment-data-files-ready.png" alt-text="Screenshot showing a File Manager window displaying new data files in the working folder.":::

- The Microsoft Monitoring Agent scans the working folder every 15 minutes. It looks for _new.*_ files and sends the data to the Log Analytics workspace. After MMA uploads the file, it changes the prefix change from *new.* to *processed.*

  :::image type="content" source="media/assess/sql-assessment-data-files-processed.png" alt-text="Screenshot showing a File Manager window displaying processed data files.":::

## Next steps

- Get more information by viewing the prerequisite documents at [Services Hub On-Demand Assessments](/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents).

- To obtain comprehensive support of the on-demand SQL Assessment feature, a Premier or Unified support subscription is required. For details, see [Azure Premier Support](https://azure.microsoft.com/support/plans/premier).

- [View SQL Server databases - Azure Arc](view-databases.md)