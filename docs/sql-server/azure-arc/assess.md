---
title: Configure SQL assessment for Azure Arc enabled SQL Server
titleSuffix:
description: Configure on-demand assessment for Azure Arc enabled instance of SQL Server
author: anosov1960
ms.author: sashan 
ms.reviewer: mikeray
ms.date: 09/05/2020
ms.topic: conceptual
ms.prod: sql
---
# Configure on-demand SQL assessment for Azure Arc enabled SQL Server instance

You can enable SQL assessment for your SQL Server instances by following these steps.

## Prerequisites

* Your SQL Server instance is connected to Azure Arc. Follow these the instructions to [onboard your SQL Server instance to  Arc-enabled SQL Server](connect.md).

* The MMA extension is installed and configured on the machine. Follow these the instructions to [Install Microsoft Monitoring Agent (MMA)](configure-advanced-data-security.md#install-microsoft-monitoring-agent-mma).

* You reviewed the SQL Server document at [Services Hub On-Demand Assessments Prerequisites](https://docs.microsoft.com/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents).

## Enable on-demand SQL Assessment

1. Open your SQL Server – Azure Arc resource and select __Environment Health__ in the left menu.

   :::image type="content" source="media/assess/sql-assessment-heading-sql-server-arc.png" alt-text="Enable SQL assessment":::

1. Specify a working directory on the data collection machine. During collection and analysis, data is temporarily stored under that folder. If the folder doesn't exist, it is created automatically.

1. Click on __Download configuration script__ and copy the downloaded script to the data collection  machine.

1. Execute the script by using the following command in __cmd.exe__.

   ```cmd.exe
   powershell.exe -ExecutionPolicy Bypass .\<filename>.ps1
   ```

   > [!NOTE]
   > The script schedules a task named *SQLAssessment* to run within an hour of running the previous script and then every 7 days. The task can be modified to run on a different date and time or even forced to run immediately from the task scheduler library > Microsoft > Operations Management Suite > AOI*** > Assessments > SQLAssessment. This task triggers data collection.

## View the assessment results

The button __View SQL assessment result__ on the _Environment Health_ blade is disabled until the the results are ready in Log Analytics. Once the button is activated, you can click on it to view the results. It could take up to 2 hours to see the results in Log Analytics after the data files are processed on the target machine.

:::image type="content" source="media/assess/sql-assessment-results.png" alt-text="Assessment results":::

You can see the state of data processing on the collection machine by checking the files in the working folder. After the scheduled task is completed, you should see several files with the _new._ prefix in the working directory:

:::image type="content" source="media/assess/sql-assessment-data-files-ready.png" alt-text="Data files are ready":::

The Microsoft Monitoring Agent scans the working folder every 15 minutes looking for _new.*_ files, and sends the data to the Log analytics workspace. Once the file is uploaded, the prefix will change from _new._ to _processed._:

:::image type="content" source="media/assess/sql-assessment-data-files-processed.png" alt-text="Data files are uploaded":::

## Next steps

See the SQL Server document at [Services Hub On-Demand Assessments Prerequisites](https://docs.microsoft.com/services-hub/health/assessment-prereq-docs#on-demand-assessment-prerequisite-documents) for more information.
