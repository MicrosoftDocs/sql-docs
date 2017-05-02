---
title: "Monitor R Services using Custom Reports in Management Studio | Microsoft Docs"
ms.custom: ""
ms.date: "02/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 5933c72c-ba63-4966-b882-75719ef8428e
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Monitor R Services using Custom Reports in Management Studio
To make it easier to manage SQL Server R Services, the product team has provided a number of sample custom reports that you can add to SQL Server Management Studio, to view R Services details such as:

- A list of the active R sessions
- The R configuration of the current instance
- Execution statistics for the R runtime
- A list of extended events for R Services
- A list of R packages installed on the current instance

This topic explains how to install and use the reports. For more information about custom reports in Management Studio, see [Custom reports in Management Studio](~/ssms/object/custom-reports-in-management-studio.md).

## How to install the reports

The reports are designed using SQL Server Reporting Services, but can be used directly from SQL Server Management Studio, even if Reporting Services is not installed on your instance. 

To use these reports:

* Download the RDL files from the GitHub repository for SQL Server product samples.
* Add the files to the custom reports folder used by SQL Server Management Studio.
* Open the reports in SQL Server Management Studio.


### Step 1. Download the reports

1. Open the GitHub repository that contains [SQL Server product samples](https://github.com/Microsoft/sql-server-samples), and download the sample reports from this page: 

   + [SSMS Custom Reports](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/r-services/ssms-custom-reports)
      
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


## Report List

The product samples repository in GitHub currently includes the following reports for SQL Server R Services:

+ **R Services - Active Sessions**

  Use this report to view the users who are currently connected to the SQL instance and running R jobs. 
  
+ **R Services - Configuration**

  Use this report to view the properties of the R runtime and configuration of R Services. The report will indicate whether a restart is required, and will check for required network protocols. 
  
  Implied authentication is required for running R in a SQL compute context, To check this, the report verifies whether a database login exists for the group SQLRUserGroup.

  > [!NOTE]
  > For more information about these fields, see [Package metadata](http://r-pkgs.had.co.nz/description.html), by Hadley Wickam. For example, the *Nickname* field for the R runtime was introduced to help differentiate between releases. 

 + **R Services - Configure Instance** 

   This report is intended to help you configure R Services after installation. You can run it from the preceding report if R Services is not configured correctly.
 
+ **R Services - Execution Statistics**

  Use this report to view the execution statistics of R Services. For example, you can get the total number of R scripts that were executed, the number of parallel executions, and the most frequently used RevoScaleR functions.
  Currently the report monitors only statistics for RevoScaleR package functions.
  Click **View SQL Script** to get the T-SQL code for this report. 

+ **R Services - Extended Events**

  Use this report to view a list of the extended events that are available for monitoring R script execution. 
  Click **View SQL Script** to get the T-SQL code for this report.

+ **R Services - Packages**

  Use this report to view a list of the R packages installed on the SQL Server instance. Currently the report includes these package properties: 
  + Package (name)
  + Version 
  + Depends
  + License
  + Built
  + Lib Path

+ **R Services - Resource Usage**

  Use this report to view consumption of CPU, memory, and I/O resources by SQL Server R scripts execution. You can also view the memory setting of external resource pools. 


## See Also

[Monitoring R Services](../../advanced-analytics/r-services/monitoring-r-services.md)

[Extended events for R Services](../../advanced-analytics/r-services/extended-events-for-sql-server-r-services.md)

