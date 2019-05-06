---
title: "Dataset Properties Dialog Box, Query (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10024"
ms.assetid: 75432318-0b00-4797-917c-0a2e74f9d951
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Dataset Properties Dialog Box, Query (Report Builder)
  Select **Query** on the **Dataset Properties** dialog box to choose a shared dataset from a report server or to create an embedded dataset. For an embedded dataset, you must choose a data source and build a query.  
  
 The **Dataset Properties** dialog box includes the following:  
  
-   [Dataset Properties Dialog Box, Parameters &#40;Report Builder&#41;](../dataset-properties-dialog-box-parameters-report-builder.md)  
  
-   [Dataset Properties Dialog Box, Fields &#40;Report Builder&#41;](../dataset-properties-dialog-box-fields-report-builder.md)  
  
-   [Dataset Properties Dialog Box, Options &#40;Report Builder&#41;](dataset-properties-dialog-box-options-report-builder.md)  
  
-   [Dataset Properties Dialog Box, Filters &#40;Report Builder&#41;](../dataset-properties-dialog-box-filters-report-builder.md)  
  
 For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
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
>  If you used a query designer to build a query, the text of the query appears in this box.  
  
 **Table name**  
 Enter the name of the table that you want to use as a dataset. This option appears when you select **Table**.  
  
 **Select or enter stored procedure name**  
 Type or choose the name of the stored procedure that you want to use. Click the **Expression** (*fx*) button to edit the expression. This option appears when you choose the Stored Procedure command type option.  
  
 **Time out (in seconds)**  
 Type the number of seconds until the query times out. The default is 30 seconds. The value for **Time out** must be empty or greater than zero. If it is empty, the query does not time out.  
  
 **Refresh Fields**  
 Run the query command to update the list of fields in the [Dataset Properties Dialog Box, Fields](../dataset-properties-dialog-box-fields-report-builder.md) page.  
  
## See Also  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)   
 [Report Builder Help for Dialog Boxes, Panes, and Wizards](../report-builder-help-for-dialog-boxes-panes-and-wizards.md)   
 [Query Designers &#40;Report Builder&#41;](../query-designers-report-builder.md)  
  
  
