---
title: "Shared Dataset Design View (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 47c502da-d163-45d9-bf04-0849e5ba7929
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Shared Dataset Design View (Report Builder)
  The Shared Dataset Design window helps you create a dataset query that you can share with others. The window lets you select a shared data source, specify properties for the shared dataset, and create a query in the query designer.  
  
 ![rs_SharedDatasetDesignMode](../media/rs-shareddatasetdesignmode.gif "rs_SharedDatasetDesignMode")  
  
 For more information about working with data in a report, see [Add Data to a Report &#40;Report Builder and SSRS&#41;](../report-data/report-datasets-ssrs.md).  
  
##  <a name="Ribbon"></a> The Ribbon  
 The Ribbon helps you quickly find the commands that you need to complete a task. Commands are organized into the following logical groups: Connection, Dataset, and Query Designer.  
  
### Connection  
 Use the **Select** button in the Connection group to select a shared data source in your report, or browse to a shared data source on the report server.  
  
> [!NOTE]  
>  A shared dataset must be based on a shared data source. If the data source that you need is not available, you must create one on the report server. For more information, see [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../create-delete-or-modify-a-shared-data-source-report-manager.md) in the Reporting Services documentation in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)][Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
### Dataset  
 Use the **Set Options** button to set shared dataset properties. These include the following:  
  
-   Fields. You can add a field or edit a field in the field collection.  
  
-   Data options. You can set options that affect match criteria and sort order, such as case sensitivity and collation.  
  
-   Filters. You can define filters that limit the data in a report after it is retrieved from the data connection.  
  
-   Parameters. You can add a parameter or edit parameter options. For example, you can specify a default value for each parameter so that you can create a cache refresh plan for this shared dataset on the report server.  
  
 The values that you set become part of the shared dataset definition on the report server. When a report author includes this shared dataset in a report, the options that you specify apply to that dataset instance.  
  
 After a shared dataset is added to a report, a report author can override the following options: collation, case sensitivity, accent sensitivity, kanatype sensitivity, width sensitivity, subtotals. They can also create additional dataset filters to limit the data in the report.  
  
 For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
 For more information about cache refresh plans, see [Cache Shared Datasets &#40;SSRS&#41;](../report-server/cache-shared-datasets-ssrs.md) in the Reporting Services documentation in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  
### Query Designer  
 Use the query designer toolbar to help build a query that specifies which data to retrieve from the data connection. The toolbar that you see depends on the query designer that is associated with the data source type from the data connection.  
  
 For more information, see the topic that corresponds to the data source type in [Add Data from External Data Sources &#40;SSRS&#41;](../report-data/add-data-from-external-data-sources-ssrs.md) and [Query Designers &#40;Report Builder&#41;](../query-designers-report-builder.md).  
  

  
##  <a name="DesignSurface"></a> The Query Designer Surface  
 A query designer helps you to build a query in the syntax that is required by the external data source.  
  
 Some data source types provide a graphical query designer that you can use to explore the metadata on an external data source. You can interactively drag names from the metadata pane to the query design surface, or interactively select the names to use.  
  
 Some data source types support a text-based query designer that you can use to paste in queries that you have created in other tools, such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 Each data source type has specific requirements for the query that will work against the external data source. For more information, see the topic that corresponds to the data source type in [Add Data from External Data Sources &#40;SSRS&#41;](../report-data/add-data-from-external-data-sources-ssrs.md) and [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md) in the Reporting Services documentation in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  

  
##  <a name="Results"></a> Viewing Query Results  
 In shared dataset design view, you are building a query that will retrieve data from the data connection when the report is processed.  
  
 Run the query to see example data from the data connection to verify that the query returns the type of data that you expect. The columns in the result set come from the metadata for data schemas from the data connection. The column names become the dataset field collection. The values of the data that you see in the query result set is design time data. After you save the shared dataset as a shared dataset definition on the report server, only the query text is saved. The data in the query result set is not saved.  
  
 When a report author adds this shared dataset to a report, a pointer to the dataset definition on the report server is added. In the report, the dataset field collection appears in the Report Data pane. The query text is not available.  
  
 The credentials that you use to run a query are separate from the credentials that are used to preview a report or to run a report from the report server. For more information, see [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  
### Running a Report with Parameters  
 When your query includes query variables, dataset parameters are created automatically for you. In turn, when you finish building the dataset query, report parameters that are set to dataset parameters are created automatically.  
  
 If a report contains parameters, all the parameters must have default values before the report can run automatically. If a parameter does not have a default value, when you run the report you must choose a value for the parameter, and then click **View Report** on the **Run** tab.  
  
 For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../report-design/report-parameters-report-builder-and-report-designer.md).  
  

  
##  <a name="Save"></a> Saving the Shared Dataset  
 To save the query that you built, on the **Report Builder** button, click **Save** or **Save As**. Navigate to the appropriate folder on the report server and save the shared dataset definition. The shared dataset is not available to others until you save it to the report server.  
  

  
## See Also  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](../report-data/report-datasets-ssrs.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../report-design/report-parameters-report-builder-and-report-designer.md)  
  
  
