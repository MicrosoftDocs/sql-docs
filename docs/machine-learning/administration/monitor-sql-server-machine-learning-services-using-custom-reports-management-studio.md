---
title: Monitor scripts with custom reports
description: Use custom reports in SQL Server Management Studio (SSMS) to monitor the execution of external scripts (Python and R), resources used, diagnose problems, and tune performance in SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 10/14/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Monitor Python and R script execution using custom reports in SQL Server Management Studio
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

Use custom reports in [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) to monitor the execution of external scripts (Python and R), resources used, diagnose problems, and tune performance in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).

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

To use these reports, follow these steps:

1. Download the [SSMS Custom Reports](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/machine-learning-services/ssms-custom-reports) for SQL Server Machine Learning Services from GitHub.

   ::: moniker range="=azuresqldb-mi-current"
   >[!NOTE]
   > The custom report **ML Services - Configure Instance** is not supported on Azure SQL Managed Instance.
   ::: moniker-end

2. Copy the reports to Management Studio

    1. Locate the custom reports folder used by SQL Server Management Studio. By default, custom reports are stored in this folder (where **user_name** is your Windows user name):

        `C:\Users\user_name\Documents\SQL Server Management Studio\Custom Reports`

       You can also specify a different folder, or create subfolders.

    2. Copy the *.RDL files you downloaded to the custom reports folder.

3. Run the reports in Management Studio

    1. In Management Studio, right-click the **Databases** node for the instance where you want to run the reports.

    2. Click **Reports**, and then click **Custom Reports**.

    3. In the **Open File** dialog box, locate the custom reports folder.

    4. Select one of the RDL files you downloaded, and then click **Open**.

## Reports

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

## Next steps

- [Monitor SQL Server Machine Learning Services using dynamic management views (DMVs)](monitor-sql-server-machine-learning-services-using-dynamic-management-views.md)
- [Monitor Python and R scripts with extended events in SQL Server Machine Learning Services](extended-events.md)
