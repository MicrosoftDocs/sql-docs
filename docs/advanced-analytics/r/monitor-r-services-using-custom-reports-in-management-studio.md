---
title: Monitor R Services using custom reports in Management Studio - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Monitor Machine Learning Services using custom reports in Management Studio
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

To make it easier to manage instance used for machine learning, the product team has provided a number of sample custom reports that you can add to SQL Server Management Studio. In these reports, you can view details such as:

- Active R or Python sessions
- Configuration settings for the instance
- Execution statistics for machine learning jobs
- Extended events for R Services
- R or Python packages installed on the current instance

This article explains how to install and use the custom reports provided specifically for machine leaerning. 

For a general introduction to reports in Management Studio, see [Custom reports in Management Studio](../../ssms/object/custom-reports-in-management-studio.md).

## How to install the reports

The reports are designed using SQL Server Reporting Services, but can be used directly from SQL Server Management Studio, even if Reporting Services is not installed on your instance. 

To use these reports:

* Download the RDL files from the GitHub repository for SQL Server product samples.
* Add the files to the custom reports folder used by SQL Server Management Studio.
* Open the reports in SQL Server Management Studio.


### Step 1. Download the reports

1. Open the GitHub repository that contains [SQL Server product samples](https://github.com/Microsoft/sql-server-samples), and download the sample reports. 

    + [SSMS custom reports](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/machine-learning-services/ssms-custom-reports)

    > [!NOTE]
    > The reports can be used with either SQL Server 2017 Machiine Learning Services, or SQL Server 2016 R Services.

2. To download the samples, you can also log into GitHub and make a local fork of the samples. 

### Step 2. Copy the reports to Management Studio

3. Locate the custom reports folder used by SQL Server Management Studio. By default, custom reports are stored in this folder:
    
   `C:\Users\user_name\Documents\SQL Server Management Studio\Custom Reports`

   However, you can specify a different folder, or create subfolders.

4. Copy the *.RDL files to the custom reports folder.


### Step 3. Run the reports

5. In Management Studio, right-click the **Databases** node for the instance where you want to run the reports.
6. Click **Reports**, and then click **Custom Reports**.
7. In the **Open File** dialog box, locate the custom reports folder.
8. Select one of the RDL files you downloaded, and then click **Open**.

> [!IMPORTANT]
> On some computers, such as those with display devices with high DPI or greater than 1080p resolution, or in some remote desktop sessions, these reports cannot be used. There is a bug in the report viewer control in SSMS that crashes the report.

## Report list

The product samples repository in GitHub currently includes the following reports:

+ **R Services - Active Sessions**

  Use this report to view the users who are currently connected to the SQL Server instance and running machine learning jobs. 
  
+ **R Services - Configuration**

  Use this report to view the configuration of the external script runtime and related services. The report will indicate whether a restart is required, and will check for required network protocols. 
  
  Implied authentication is required for machine learning tasks that run in SQL Server as a compute context. To verify that implied authentication is configured, the report verifies whether a database login exists for the group SQLRUserGroup.

 + **R Services - Configure Instance** 

   This report is intended to help you configure machine learning. You can also run this report to fix configuration errors found in the preceding report.
 
+ **R Services - Execution Statistics**

  Use this report to view execution statistics for machine learning jobs. For example, you can get the total number of R scripts that were executed, the number of parallel executions, and the most frequently used RevoScaleR functions. Click **View SQL Script** to get the complete T-SQL code behind this report.

  Currently the report monitors only statistics for RevoScaleR package functions.

+ **R Services - Extended Events**

  Use this report to view a list of the extended events that are available for monitoring tasks related to external script runtimes. Click **View SQL Script** to get the complete T-SQL code behind this report.

+ **R Services - Packages**

  Use this report to view a list of the R or Python packages installed on the SQL Server instance.

+ **R Services - Resource Usage**

  Use this report to view consumption of CPU, memory, and I/O resources by external script execution. You can also view the memory setting of external resource pools.

## See also

[Monitoring services](managing-and-monitoring-r-solutions.md)

[Extended events for R Services](extended-events-for-sql-server-r-services.md)
