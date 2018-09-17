---
title: Submit Spark Job in Azure Data Studio 
description: Submit Spark Job in Azure Data Studio
services: SQL Server 2019 Big Data Cluster spark
ms.service: SQL Server 2019 Big Data Cluster spark
author: jejiang
ms.author: jejiang
ms.reviewer: jroth
ms.custom: ""
ms.topic: conceptual
ms.date: 09/17/2018
---
# Submit Spark Job in Azure Data Studio

One of the key scenarios for SQL Server 2019 CTP 2.0 is the ability to submit Spark job. The Spark job submission feature allows you to submit a local Jar or Py files with references to SQL Server Big Data Cluster. It also enables you to execute a Jar or Py files which are already located in the HDFS file system. 

## Prerequisite 
You need to install big data tools for SQL Server and connect to a cluster before you can submit Spark job. For installation details, please refer to link [Deploy Big Data Tools](deploy-big-data-tools.md).

## Open Spark job submission dialog
There are multiple ways to open Spark job submission dialog. The ways include Dashboard, Context Menu in Object Explorer, and Command Palate.

+ Click **New Spark Job** in the dashboard to open the Spark job submission dialog.

    ![Submit menu by clicking dashboard ](./media/jenny/new-spark-job.png)
 
+ Right-click on the cluster in the Object Explorer and select **Submit Spark Job** from the context menu. This opens Spark job submission dialog.  
 
    ![Submit menu by right-click cluster](./media/jenny/submit-spark-job.png)

+ Right-click on a Jar/Py file in the Object Explorer and select **Submit Spark Job** from the context menu. This opens Spark job submission dialog with the Jar/Py field to be pre-populated. 
 
    ![Submit menu by right-click file](./media/jenny/submit-spark-job-2.png)

+ Use command **Submit Spark Job** from command palette by typing Ctrl+Shift+P (in Windows) and Cmd+Shift+P (in Mac).

    ![Submit menu command palette in windows](./media/jenny/submit-spark-job-3.png)

    ![Submit menu command palette in mac](./media/jenny/submit-spark-job-4.png)
  
 
## Submit Spark job 
The Spark job submission dialog is displayed as the following. Enter Job name, JAR/Py file path, main class and other fields. The Jar / Py file source could be from Local or from HDFS. If the Spark job has reference Jars, Py files or additional files, click **ADVANCED** tab and enter the corresponding file paths. Click **Submit** to submit Spark job.
 
![New spark job dialog](./media/jenny/submit-spark-job-section.png)

![Advanced dialog](./media/jenny/submit-spark-job-section-1.png)

## Monitor Spark job submission
After the Spark job is submitted, the Spark job submission and execution status information is displayed in the Task History on the left. And details on the progress and logs are also displayed in the **OUTPUT** window at the bottom.
+ When Spark job is in progress, the **Task History** panel and **OUTPUT** window are refreshing with the progress.

![Monitor spark job in progress](./media/jenny/monitor-spark-job-submission.png)

+ When Spark job is in complete with success, you can see Spark UI and Yarn UI links in the **OUTPUT** window. You can click the links for more information.

![Spark job link in output](./media/jenny/monitor-spark-job-submission-2.png)

## Next steps
For more information on SQL Server Big Data Cluster and related scenarios, see [What is SQL Server Big Data Cluster](big-data-cluster-overview.md)?.

