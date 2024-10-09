---
title: "Troubleshoot data retrieval issues with Reporting Services reports"
description: In this article, diagnose and fix problems that occur when you retrieve report data by previewing a report locally or running a report on the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Troubleshoot data retrieval issues with Reporting Services reports
The first step during report processing is to retrieve the report data for each dataset by running the dataset query. When you preview a report locally, your data source connections and credentials must use sufficient permissions to retrieve the data to your computer. When you run a report on the report server, the data source connections and credentials must use sufficient permissions to retrieve the data on the report server. Use this article to help troubleshoot issues about report data retrieval.   
  
## I can't create a connection to a data source  
When you create a data source, run a dataset query, or preview a report, you might get the following message: "Can't create a connection to data source `<data source name>`."   
    
### Data source isn't available.  
The data source is offline or unavailable for some other reason.   
  
Verify that you have access to the data source and that it's available. For example, use Sql Server Management Studio to connect to the data source. For relational databases and multidimensional database, use the **Test** button on the **Connection Properties** dialog box to verify the connection and permissions to the data source.   
  
### Data source credentials aren't valid.  
The credentials that you used to connect to the data source have insufficient permissions to retrieve the data specified in the query.  
  
Verify that the credentials that you use are the correct credentials. For example, you might have permission to retrieve data from a Table or View, but not for a specific column. Or, you might not have sufficient permissions to run a stored procedure that populates a view.   
  
> [!NOTE]  
> Permissions that you use to retrieve data for previewing a report may be different than permissions that are needed to retrieve data after a report is published to a report server.   
  
### Not a valid password  
Data sources with prompted credentials or credentials specified in the connection string pass the characters for the password to the underlying data source drivers.  If the password or string contains special characters like punctuation marks, some data source drivers can't validate the special characters.   
  
Verify that the password doesn't include special characters. If changing the password is impractical, work with your database administrator to store the appropriate credentials locally and on the server as part of a system ODBC data source name (DSN). For more information, see "OdbcConnection.ConnectionString" in the .NET Framework SDK documentation on MSDN.   
  
> [!NOTE]  
>It is recommended that you do not add login information such as passwords to the connection string. Report Designer provides a **Credentials** page on the **Data Source Properties** or the **Shared Data Source Properties** dialog boxes that you can use to enter credentials. These credentials are stored securely on the report authoring computer.  
  
## Why do I see no data when I run my query in the query designer?  
When you create a dataset, the dataset field collection appears in the Report Data pane. Sometimes the dataset field collection doesn't appear as expected.   
  
### Import query doesn't import calculated fields  
  
Although calculated fields are saved in a report definition, they aren't included when you import a dataset query from another report. Only fields specified by the dataset query appear in the Report Data pane after you create a dataset by importing a query from another report.   
  
To view calculated fields in the Report Data pane, you must define them for each report in which they're used.   
  
### Some data providers don't support automatic population of the dataset field collection  
When you define a query in the Dataset Properties dialog box, and then close the dialog box, the dataset field collection usually appears in the Report Data pane. For some data sources, the dataset field collection isn't automatically populated.   
  
To populate the dataset field collection, do the following tasks:  
* Make sure that you have permissions to retrieve field information from the database. For some data sources, you might have permissions to access the data source but not the table or column. You might have permission to access a view but not the permissions to run the stored procedures that create the view. To validate your access to specific tables or columns in a database, verify your query results in a separate application such as SQL Server Management Studio. Verify the results by using the same permissions you use for the report. If you can't see the results that you want for your query, work with the system administrator to adjust your permissions to the data.   
* Run the query in the query pane of the **Dataset Properties** dialog box. For more information, see [Report datasets (Report Builder 3.0 and SSRS)](../../reporting-services/report-data/report-datasets-ssrs.md).  
* Add fields manually. For more information, see [How to: Add, edit, refresh fields in the Report Data pane (Report Builder 3.0 and SSRS)](../../reporting-services/report-data/add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).   
  
## Related content

- [Errors and events (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)

[!INCLUDE [feedback-qa-stackoverflow-md](../../includes/feedback-qa-stackoverflow-md.md)]
