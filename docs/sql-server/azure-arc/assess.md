---
title: Run on-demand SQL Assessment on a SQL Server instance with Azure Arc enabled
description: Run on-demand SQL Assessment on a SQL Server instance with Azure Arc enabled
author: anosov1960
ms.author: sashan 
ms.reviewer: mikeray
ms.date: 09/10/2020
ms.topic: conceptual
ms.prod: sql
---
# Run SQL Assessment on a SQL Server instance with Azure Arc enabled

SQL Assessment provides a mechanism to evaluate the configuration of your SQL Server. This article provides instructions for using SQL Assessment on an Azure Arc enabled SQL Server instance.

## Prerequisites

* Your SQL Server instance must be connected to Azure Arc. For instructions, see the [Connect your SQL Server to Azure Arc](connect.md) article.

* The Microsoft Monitoring Agent (MMA) extension must be installed and configured on the machine. View the [Install MMA](configure-advanced-data-security.md#install-microsoft-monitoring-agent-mma) article for instructions. You can also get more information on the [Log Analytics Agent](/azure/azure-monitor/platform/log-analytics-agent) article.

* Your SQL Server must have the [TCP/IP protocol enabled](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

* The [SQL Server browser service](../../tools/configuration-manager/sql-server-browser-service.md) must be running if you're operating a named instance of SQL Server.

* Make sure you've reviewed the SQL Server document at [Services Hub On-Demand Assessments Prerequisites](/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents).

## Run SQL Assessment on demand

1. Open your SQL Server – Azure Arc resource and select **Environment Health** in the left pane.

   :::image type="content" source="media/assess/sql-assessment-heading-sql-server-arc.png" alt-text="Screenshot showing the Environment Health screen of a SQL Server - Azure Arc resource." lightbox="media/assess/sql-assessment-heading-sql-server-arc.png":::

1. Specify a working directory on the data collection machine. By default, `C:\sql_assessment\work_dir` is used. During collection and analysis, data is temporarily stored in that folder. If the folder doesn't exist, it's created automatically.

1. Select **Download configuration script**. Copy the downloaded script to the target machine.

1. Launch an admin instance of **powershell.exe** and execute one of the following code blocks:

   * _Domain account_ -  You'll be prompted for the user account and password.

      ```powershell
      Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force
      & '.\AddSqlAssessment.ps1'
      ```

   * _Managed Service Account (MSA)_

      ```powershell
      Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force
      & '.\AddSqlAssessment.ps1' -ManagedServiceAccountName <MSA account name>
      ```

> [!NOTE]
> The script schedules a task named *SQLAssessment*, which triggers data collection. This task executes within an hour after you've run the script. It then repeats every seven days.

> [!TIP]
> You can modified the task to run on a different date and time or even force it to run immediately. In the the task scheduler library, find > **Microsoft** > **Operations Management Suite** > **AOI\*\*\*** > **Assessments** > **SQLAssessment**.

## View SQL Assessment results

* On the _Environment Health_ pane, select the **View SQL Assessment results** button.

   > [!NOTE]
   > The **View SQL Assessment results** button remains disabled until the results are ready in Log Analytics. This process might take up to two hours after the data files are processed on the target machine.

   :::image type="content" source="media/assess/sql-assessment-results.png" alt-text="Screenshot showing the SQL Assessment results." lightbox="media/assess/sql-assessment-results.png":::

* You can see the state of data processing on the collection machine by checking the files in the working folder. After the scheduled task is completed, you should see several files with the _new._ prefix in the working directory.

   :::image type="content" source="media/assess/sql-assessment-data-files-ready.png" alt-text="Screenshot showing a File Manager window displaying new data files in the working folder.":::

* The Microsoft Monitoring Agent scans the working folder every 15 minutes. It looks for _new.*_ files and sends the data to the Log Analytics workspace. After MMA uploads the file, it changes the prefix change from _new._ to _processed._

   :::image type="content" source="media/assess/sql-assessment-data-files-processed.png" alt-text="Screenshot showing a File Manager window displaying processed data files.":::

## Next steps

* Get more information by viewing the prerequisite documents at [Services Hub On-Demand Assessments](/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents).

* To obtain comprehensive support of the on-demand SQL Assessment feature, a Premier or Unified support subscription is required. For details, see [Azure Premier Support](https://azure.microsoft.com/support/plans/premier).
