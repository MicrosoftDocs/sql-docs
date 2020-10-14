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

* Your SQL Server instance must be connected to Azure Arc. See [onboard your SQL Server instance to Azure Arc-enabled SQL Server](connect.md) for instructions.

* The Microsoft Monitoring Agent (MMA) extension must be installed and configured on the machine. For instructions, go to [Install MMA](configure-advanced-data-security.md#install-microsoft-monitoring-agent-mma). For more information, see [Log Analytics Agent](/azure/azure-monitor/platform/log-analytics-agent).

* Your SQL Server must have the [TCP/IP protocol enabled](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

* The [SQL Server browser](../../tools/configuration-manager/sql-server-browser-service.md) must be running if you're operating a named instance of SQL Server.

* Make sure you've reviewed the SQL Server document at [Services Hub On-Demand Assessments Prerequisites](/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents).

## Run SQL Assessment on demand

1. Open your SQL Server – Azure Arc resource and select **Environment Health** in the left menu.

   ![Screenshot showing the Environment Health screen of a SQL Server - Azure Arc resource.](media/assess/sql-assessment-heading-sql-server-arc.png)

1. Specify a working directory on the data collection machine. By default, `C:\sql_assessment\work_dir` is used. During collection and analysis, data is temporarily stored under that folder. If the folder doesn't exist, it's created automatically.

1. Select **Download configuration script**. Copy the downloaded script to the target machine.

1. Launch an admin instance of **powershell.exe** and do run one of the following code blocks:

   * If you're using a domain account, run the following commands. You'll be prompted for the user account and password.

      ```powershell
      Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force
      & '.\AddSqlAssessment.ps1'
      ```

   * If you're using a Managed Service Account (MSA) run the following commands.

      ```powershell
      Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force
      & '.\AddSqlAssessment.ps1' -ManagedServiceAccountName <MSA account name>
      ```

> [!NOTE]
> The script schedules a task named *SQLAssessment*, which triggers data collection. This task executes within an hour after you've run the script. It then repeats every seven days.

> [!TIP]
> You can modified the task to run on a different date and time or even force it to run immediately. Go to the task scheduler library > Microsoft > Operations Management Suite > AOI*** > Assessments > SQLAssessment.

## View SQL Assessment results

The **View SQL Assessment results** button on the _Environment Health_ screen remains disabled until the results are ready in Log Analytics. After the button is enabled, select it to view the results.

> [!NOTE]
>It might take up to two hours to see the results in Log Analytics after the data files are processed on the target machine.

![Screenshot showing the SQL Assessment results.](media/assess/sql-assessment-results.png)

You can see the state of data processing on the collection machine by checking the files in the working folder. After the scheduled task is completed, you should see several files with the _new._ prefix in the working directory.

![Screenshot showing a File Manager window displaying new data files in the working folder.](media/assess/sql-assessment-data-files-ready.png)

The Microsoft Monitoring Agent scans the working folder every 15 minutes. It looks for _new.*_ files and sends the data to the Log Analytics workspace. After MMA uploads the file, it changes the prefix change from _new._ to _processed._

![Screenshot showing a File Manager window displaying processed data files.](media/assess/sql-assessment-data-files-processed.png)

## Next steps

* Get more information by viewing [Services Hub On-Demand Assessments Prerequisites](/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents).

* To obtain comprehensive support of the On-demand SQL Assessment, a Premier or Unified support subscription is required. For details, see [Azure Premier Support](https://azure.microsoft.com/support/plans/premier).
