---
title: "Dataset Properties Dialog Box, Query (Report Builder) | Microsoft Docs"
ms.date: 08/17/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: reference
f1_keywords: 
  - "10024"
  - "sql13.rtp.rptdesigner.datasetproperties.query.f1"
  - "10160"
ms.assetid: 75432318-0b00-4797-917c-0a2e74f9d951
author: maggiesMSFT
ms.author: maggies
---
# Dataset Properties Dialog Box, Query (Report Builder)
 
Select **Query** on the **Dataset Properties** dialog box to choose a shared dataset from a report server or to create an embedded dataset. For an embedded dataset, you must choose a data source and build a query.  
  
## Options  
 **Name**  
 Type a name for the dataset. The name cannot be the same as a name for any data region or group in the report.  
  
 **Use a shared dataset**  
 Select this option to use a predefined dataset from the report server.  
  
 **Browse**  
 Browse to a folder on a report server or SharePoint site and select a shared dataset (.rsd).  
  
 **Use an embedded dataset in my report**  
 Select this option to create a dataset for use only by this report.  
  
 **Data Source**  
 Select the data source on which to base the dataset. To create a new data source, click **New**.  
  
 **Query type**  
 Select the type of command or query to use for the dataset. Select **Text** to run a query to retrieve data from the database. Select **Table** to use the **TableDirect** feature of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to select all the fields within a table. Select **Stored Procedure** to run a stored procedure by name. **Text** is selected by default and is used for most queries. To edit the selected data source query, click **Query Designer**.  
  
> [!NOTE]  
>  Not all query types are supported by all data sources. For example, **Table** is supported only by data source types **OLE DB** and **ODBC**.  
  
 **Query**  
 This option appears when you choose the **Text** command type option. Type a query or import a pre-existing query by clicking **Import**. Click the **Expression** (*fx*) button to edit the expression.  
  
> [!NOTE]  
>  If you use a query designer to build a query, the text of the query appears in this box.  
  
**Table name**  
This option appears when you select **Table**. Enter the name of the table that you want to use as a dataset.   
  
**Select or enter stored procedure name**  
This option appears when you choose the Stored Procedure command type option. Type or choose the name of the stored procedure that you want to use. Click the **Expression** (*fx*) button to edit the expression.   
  
 **Time out (in seconds)**  
 Type the number of seconds until the query times out. The default is 30 seconds. The value for **Time out** must be empty or greater than zero. If it is empty, the query does not time out.  
  
 **Refresh Fields**  
 Run the query command to update the list of fields in the **Dataset Properties Dialog Box, Fields**page.  
  
## See Also  
[Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
[Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)  
[Query Designers &#40;Report Builder&#41;](https://msdn.microsoft.com/library/553f0d4e-8b1d-4148-9321-8b41a1e8e1b9)  
  
  
