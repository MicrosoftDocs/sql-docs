---
title: "Troubleshoot Data Retrieval issues with Reporting Services Reports"
description: In this article, diagnose and fix problems that occur when you retrieve report data by previewing a report locally or running a report on the report server.
ms.date: 02/27/2016
ms.service: reporting-services
ms.subservice: troubleshooting


ms.topic: conceptual
ms.assetid: 7680946a-1660-4b59-a03a-c4d474cd8ed3
author: maggiesMSFT
ms.author: maggies
---
# Troubleshoot Data Retrieval issues with Reporting Services Reports
The first step during report processing is to retrieve the report data for each dataset by running the dataset query. When you preview a report locally, your data source connections and credentials must use sufficient permissions to retrieve the data to your computer. When you run a report on the report server, the data source connections and credentials must use sufficient permissions to retrieve the data on the report server. Use this topic to help troubleshoot issues about report data retrieval.   
  
## I cannot create a connection to a data source  
When you create a data source, run a dataset query, or preview a report, you might get the following message: Cannot create a connection to data source `<data source name>`.   
    
### Data Source is Not Available.  
The data source is offline or unavailable for some other reason.   
  
Verify that you have access to the data source and that it is available. For example, use Sql Server Management Studio to connect to the data source. For relational databases and multidimensional database, use the **Test** button on the **Connection Properties** dialog box to verify the connection and permissions to the data source.   
  
### Data Source Credentials are Not Valid.  
The credentials that you are using to connect to the data source have insufficient permissions to retrieve the data specified in the query.  
  
Verify that the credentials that you are using the correct credentials. For example, you may have permission to retrieve data from a Table or View, but not for a specific column; or you might not have sufficient permissions to run a stored procedure that populates a view.   
  
> [!NOTE]  
> Permissions that you use to retrieve data for previewing a report may be different than permissions that are needed to retrieve data after a report is published to a report server.   
  
### Not a Valid Password  
For data sources with prompted credentials or credentials specified in the connection string, the characters for the password are passed to the underlying data source drivers. If the password or string contains special characters like punctuation marks, some data source drivers cannot validate the special characters.   
  
Verify that the password does not include special characters. If changing the password is impractical, work with your database administrator to store the appropriate credentials locally and on the server as part of a system ODBC data source name (DSN). For more information, see "OdbcConnection.ConnectionString" in the .NET Framework SDK documentation on MSDN.   
  
> [!NOTE]  
>It is recommended that you do not add login information such as passwords to the connection string. Report Designer provides a **Credentials** page on the **Data Source Properties** or the **Shared Data Source Properties** dialog boxes that you can use to enter credentials. These credentials are stored securely on the report authoring computer.  
  
## Why do I see no data when I run my query in the query designer?  
When you create a dataset, the dataset field collection appears in the Report Data pane. Sometimes the dataset field collection does not appear as expected.   
  
### Import Query Does Not Import Calculated Fields  
  
Although calculated fields are saved in a report definition, they are not included when you import a dataset query from another report. Only fields specified by the dataset query appear in the Report Data pane after you create a dataset by importing a query from another report.   
  
To view calculated fields in the Report Data pane, you must define them for each report in which they are used.   
  
### Some Data Providers Do Not Support Automatic Population of the Dataset Field Collection  
When you define a query in the Dataset Properties dialog box, and then close the dialog box, the dataset field collection usually appears in the Report Data pane. For some data sources, the dataset field collection is not automatically populated.   
  
To populate the dataset field collection, do the following:  
* Make sure that you have permissions to retrieve field information from the database. For some data sources, you might have permissions to access the data source but not the table or column. You may have permission to access a view but not the permissions to run the stored procedures that create the view. To validate your access to specific tables or columns in a database, verify your query results in a separate application such as SQL Server Management Studio using the same permissions you use for the report. If you cannot see the results that you want for your query, work with the system administrator to adjust your permissions to the data.   
* Run the query in the query pane of the **Dataset Properties** dialog box. For more information, see [Report Datasets (Report Builder 3.0 and SSRS)](../../reporting-services/report-data/report-datasets-ssrs.md).  
* Add fields manually. For more information, see [How to: Add, Edit, Refresh Fields in the Report Data Pane (Report Builder 3.0 and SSRS)](../../reporting-services/report-data/add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).   
  
## See Also  
[Errors and events (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)  
  
  

[!INCLUDE[feedback_stackoverflow_msdn_connect](../../includes/feedback-stackoverflow-msdn-connect-md.md)]



