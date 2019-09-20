---
title: Monitor Python and R script execution using custom reports in SSMS
ms.prod: sql
ms.technology: machine-learning
ms.date: 09/17/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Monitor Python and R script execution using custom reports in SQL Server Management Studio
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Use custom reports in SQL Server Management Studio (SSMS) to monitor the execution of external scripts (Python and R), resources used, diagnose problems, and tune performance in SQL Server Machine Learning Services.

In these reports, you can view details such as:

- Active Python or R sessions
- Configuration settings for the instance
- Execution statistics for machine learning jobs
- Extended events for R Services
- Python or R packages installed on the current instance

This article explains how to install and use the custom reports provided for SQL Server Machine Learning Services.

For more information on reports in SQL Server Management Studio, see [Custom reports in Management Studio](../../ssms/object/custom-reports-in-management-studio.md).

## How to install the reports

The reports are designed using SQL Server Reporting Services, but can be used directly from SQL Server Management Studio. Reporting Services does not have to be installed on your SQL Server instance.

To use these reports:

1. Download the RDL files from the GitHub repository for SQL Server product samples.
2. Add the files to the custom reports folder used by SQL Server Management Studio.
3. Open the reports in SQL Server Management Studio.

### Step 1. Download the reports

1. Download the [SSMS Custom Reports](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/machine-learning-services/ssms-custom-reports) for SQL Server Machine Learning Services from GitHub.

### Step 2. Copy the reports to Management Studio

2. Locate the custom reports folder used by SQL Server Management Studio. By default, custom reports are stored in this folder (where **user_name** is your Windows user name):
    
   `C:\Users\user_name\Documents\SQL Server Management Studio\Custom Reports`

   You can also specify a different folder, or create subfolders.

3. Copy the *.RDL files you downloaded to the custom reports folder.

### Step 3. Run the reports

4. In Management Studio, right-click the **Databases** node for the instance where you want to run the reports.

5. Click **Reports**, and then click **Custom Reports**.

6. In the **Open File** dialog box, locate the custom reports folder.

7. Select one of the RDL files you downloaded, and then click **Open**.

## Report list

The [SSMS Custom Reports repository in GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/machine-learning-services/ssms-custom-reports) includes the following reports:

| Report | Description |
|-|-|
| Active Sessions | Users who are currently connected to the SQL Server instance and running a Python or R script. |
| Configuration | Installation settings of Machine Learning Services and properties of the Python or R runtime. |
| Configure Instance | Configure Machine Learning Services. |
| Execution Statistics | Execution statistics of Machine Learning services. For example, you can get the total number of external scripts executions and number of parallel executions. |
| Extended Events | Extended events that are available to get more insights into external scripts execution. |
| Packages | List the R or Python packages installed on the SQL Server instance and their properties, such as version and name. |
| Resource Usage | View the CPU, Memory, IO consumption of SQL Server, and external scripts execution. You can also view the memory setting for external resource pools. |

## See also

[Monitoring services](managing-and-monitoring-r-solutions.md)

[Extended events for R Services](extended-events-for-sql-server-r-services.md)
