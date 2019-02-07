---
title: Run Spark jobs in Azure Data Studio 
titleSuffix: SQL Server 2019 big data clusters
description: Submit Spark jobs on SQL Server big data clusters in Azure Data Studio.
author: jejiang
ms.author: jejiang
ms.reviewer: jroth
ms.date: 12/06/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Submit Spark jobs on SQL Server big data clusters in Azure Data Studio

One of the key scenarios for big data clusters is the ability to submit Spark jobs for SQL Server 2019 preview. The Spark job submission feature allows you to submit a local Jar or Py files with references to SQL Server 2019 big data cluster. It also enables you to execute a Jar or Py files, which are already located in the HDFS file system. 

## Prerequisites

- [SQL Server 2019 big data tools](deploy-big-data-tools.md):
   - **Azure Data Studio**
   - **SQL Server 2019 extension**
   - **kubectl**

- [Connect Azure Data Studio to the HDFS/Spark gateway of your big data cluster](connect-to-big-data-cluster.md).

## Open Spark job submission dialog
There are several ways to open Spark job submission dialog. The ways include Dashboard, Context Menu in Object Explorer, and Command Palate.

+ Click **New Spark Job** in the dashboard to open the Spark job submission dialog.

    ![Submit menu by clicking dashboard ](./media/submit-spark-job/new-spark-job.png)
 
+ Right-click on the cluster in the Object Explorer and select **Submit Spark Job** from the context menu. Spark job submission dialog will open.  
 
    ![Submit menu by right-click cluster](./media/submit-spark-job/submit-spark-job.png)

+ Right-click on a Jar/Py file in the Object Explorer and select **Submit Spark Job** from the context menu. Spark job submission dialog with the Jar/Py field to be pre-populated will open. 
 
    ![Submit menu by right-click file](./media/submit-spark-job/submit-spark-job-2.png)

+ Use command **Submit Spark Job** from command palette by typing Ctrl+Shift+P (in Windows) and Cmd+Shift+P (in Mac).

    ![Submit menu command palette in windows](./media/submit-spark-job/submit-spark-job-3.png)

    ![Submit menu command palette in mac](./media/submit-spark-job/submit-spark-job-4.png)
  
 
## Submit Spark job 
The Spark job submission dialog is displayed as the following. Enter Job name, JAR/Py file path, main class, and other fields. The Jar / Py file source could be from Local or from HDFS. If the Spark job has reference Jars, Py files or additional files, click **ADVANCED** tab and enter the corresponding file paths. Click **Submit** to submit Spark job.
 
![New spark job dialog](./media/submit-spark-job/submit-spark-job-section.png)

![Advanced dialog](./media/submit-spark-job/submit-spark-job-section-1.png)

## Monitor Spark job submission
After the Spark job is submitted, the Spark job submission and execution status information are displayed in the Task History on the left. And details on the progress and logs are also displayed in the **OUTPUT** window at the bottom.
+ When Spark job is in progress, the **Task History** panel and **OUTPUT** window are refreshing with the progress.

![Monitor spark job in progress](./media/submit-spark-job/monitor-spark-job-submission.png)

+ When Spark job is in complete with success, you can see Spark UI and Yarn UI links in the **OUTPUT** window. You can click the links for more information.

![Spark job link in output](./media/submit-spark-job/monitor-spark-job-submission-2.png)

## Next steps
For more information on SQL Server big data cluster and related scenarios, see [What is SQL Server big data cluster](big-data-cluster-overview.md)?

