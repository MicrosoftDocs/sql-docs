---
title: "Report Datasets (SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
ms.assetid: f2e42303-e355-4c1f-bb3b-3338fbdd230d
author: markingmyname
ms.author: maghan
---
# Report Datasets (SSRS)
  To add data to a report, you create datasets. Each dataset represents the result set from running a query command on a data source. The columns in the result set are the field collection. The rows in the result set are the data. A dataset does not contain the actual data. A dataset contains the information that is needed to retrieve a specific set of data from a data source.  
  
 There are two types of datasets: embedded and shared. An embedded dataset is defined in a report and used only by that report. A shared dataset is defined on the report server or SharePoint site and can be used by multiple reports. In Report Builder, you can create shared datasets in Shared Dataset mode or embedded datasets in Report Designer mode. In Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can create shared datasets as part of a project or embedded datasets as part of a report.  
  
-   **Embedded datasets.** Unlike applications such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel where you work with data directly in a worksheet, in Report Builder or Report Designer you work with metadata that represents the data that will be retrieved when the report is processed. To create an embedded dataset, select the source of data and specify a query. After you create the dataset, use the Report Data pane to view the field collection. You can display data from a dataset in a data region like a table or chart. In each data region, you can group, filter, and sort the data to organize it. After you design the report layout, you run the report to see the actual data.  
  
     In the following figure, the Report Data pane displays a data source named [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)], a dataset named DataSet1, and five fields in the dataset field collection. The Layout pane shows a table with the top row of column headings and the bottom row with table cells that contain text. The placeholder text [Name] is the metadata for the field Name. When the report runs, the placeholder text is replaced by the actual data values. The table expands as required to display all the data.  
  
     ![rs_DataDesignandPreview](../../reporting-services/report-data/media/rs-datadesignandpreview.gif "rs_DataDesignandPreview")  
  
-   **Shared datasets.** Create a shared dataset when you want to use a dataset in more than one report. To create and save a shared dataset to a report server or SharePoint site, use Report Builder in shared dataset design view. To create a shared dataset as part of a project that can be deployed to a server or site, use Report Designer.  
  
     The following illustration shows Shared Dataset Design view in Report Builder. You can select or modify the data connection, the dataset properties, the query, filters, and optionally mark filters as parameters, and view the query results. You then save the changes back to the server or site.  
  
     ![rs_SharedDatasetDesignMode](../../reporting-services/report-builder/media/rs-shareddatasetdesignmode.gif "rs_SharedDatasetDesignMode")  
  
 For more information, see [Embedded and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/embedded-and-shared-datasets-report-builder-and-ssrs.md) and [Embedded and Shared Data Connections or Data Sources &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/f417782c-b85a-4c4d-8a40-839176daba56).  
  
 You can also add datasets to a report by adding report parts that include the datasets they depend on. [!INCLUDE[ssRBrptparts](../../includes/ssrbrptparts-md.md)]  
  
 To learn how to create a report that displays data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, see [Tutorial: Creating a Basic Table Report &#40;Report Builder&#41;](../../reporting-services/tutorial-creating-a-basic-table-report-report-builder.md). To build a report that includes its own data, see [Tutorial: Create a Quick Chart Report Offline &#40;Report Builder&#41;](../../reporting-services/report-builder/tutorial-create-a-quick-chart-report-offline-report-builder.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="Methods"></a> Adding Report Data  
 In Report Builder, you can add report data in the following ways.  
  
-   Add report parts from a report server to your report. Each report part is self-contained and includes dependent datasets. The datasets are predefined.  
  
-   Use the Table/Matrix, Chart, and Map wizards. From the wizards, you can select shared data sources and shared datasets, or create new datasets, and continue to design the report.  
  
-   Add shared datasets from a report server. Shared datasets are predefined and specify which data to use from a predefined data source. When you add a shared dataset to your report, you add a dataset reference that points to the shared dataset definition.  
  
 In Report Builder or Report Designer, you can add data in the following ways.  
  
-   Add embedded datasets based on shared data sources.  
  
-   Add embedded datasets based on embedded data sources.  
  
> [!NOTE]  
>  On a report server, shared items are secured individually or by inheriting permissions from the folder where they are published. To enable other users to have access to shared datasets that you save, you must understand how permissions are granted. For more information, see [Security &#40;Report Builder&#41;](../../reporting-services/report-builder/security-report-builder.md) or [Secure Shared Dataset Items](../../reporting-services/security/secure-shared-dataset-items.md).  
  
 After you add data to a report, you can organize the data on the report page with data regions, modify report parts and share those changes with others, and enable users to limit or sort the data they see in the report. For more information, see the following related topics:  
  
-   [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
-   [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)  
  
-   [Sparklines and Data Bars &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/sparklines-and-data-bars-report-builder-and-ssrs.md)  
  
-   [Indicators &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/indicators-report-builder-and-ssrs.md)  
  
-   [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)  
  
-   [Report Parts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md)  
  
-   [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)  
  
  
##  <a name="QuickStart"></a> Adding Data with Report Parts  
 Report parts contain the datasets that they depend on. These datasets are built on shared data sources that are available on the report server. In Report Builder, when you add a report part to your report, the dependent datasets are added to your report, just as if you had added them manually. For example, a predefined chart contains a dataset. To see the data, preview the report.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBrptparts](../../includes/ssrbrptparts-md.md)]  
  
 Report parts, shared data sources, and shared datasets are defined in advance and saved on a report server. To access them, you must open Report Builder in server mode by connecting to the report server. You can use these to create new versions of your own if you have write permissions to the report server.  
  
-   For more information, see [Report Parts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md) and [Report Parts in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/report-parts-in-report-designer-ssrs.md).  
  
  
##  <a name="Queries"></a> Queries and Query Designers  
 To specify which data you want from a data source, you build a query command. Each data source type provides a related *query designer* to help you build the query. The query designer can be graphical or text-based. In a graphical query designer, you view metadata that represents the data on the external data source and interactively build a query by dragging fields or entities to the query design surface. In a text-based query designer, you write or import queries in the query syntax that is supported by the external data source.  
  
 In the query designer, you can run the query to view example data and validate the query command syntax. Column names in the result set become the field names that you see in the Report Data pane. The result set must be a single set of rows and columns where the same number of values exist for each row of data. Multiple results sets from a single query are not supported. Ragged hierarchies, which do not have a constant number of columns and can produce different number of data values for each row, are not supported.  
  
 To run a query, you must have design time credentials. For more information, see [Specify Credentials in Report Builder](https://msdn.microsoft.com/library/7412ce68-aece-41c0-8c37-76a0e54b6b53) and [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
 Communication between a data extension and the external data source is handled by data providers. Support for query command syntax, query parameters, and data types for values in the result set is determined by each data provider. For more information, see the topic for the specific type of data extension and [Query Designers &#40;Report Builder&#41;](https://msdn.microsoft.com/library/553f0d4e-8b1d-4148-9321-8b41a1e8e1b9).  
  
  
##  <a name="HowTo"></a> How-To Topics  
 [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)  
  
 [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md)  
  
 [Build a Query in the Relational Query Designer &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/build-a-query-in-the-relational-query-designer-report-builder-and-ssrs.md)  
  
 [Show Hidden Datasets for Parameter Values for Multidimensional Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/show-hidden-datasets-for-parameter-values-multidimensional-data.md)  
  
 [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md)  
  
 [Set a No Data Message for a Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/set-a-no-data-message-for-a-data-region-report-builder-and-ssrs.md)  
  
 [Associate a Query Parameter with a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/associate-a-query-parameter-with-a-report-parameter-report-builder-and-ssrs.md)  
  
 [Define Parameters in the MDX Query Designer for Analysis Services &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/define-parameters-in-the-mdx-query-designer-for-analysis-services.md)  
  
  
##  <a name="Section"></a> In This Section  
 [Report Parts and Datasets in Report Builder](../../reporting-services/report-data/report-parts-and-datasets-in-report-builder.md)  
  
 [Data Connections, Data Sources, and Connection Strings in Report Builder](https://msdn.microsoft.com/library/7e103637-4371-43d7-821c-d269c2cc1b34)  
  
 [Specify Credentials in Report Builder](https://msdn.microsoft.com/library/7412ce68-aece-41c0-8c37-76a0e54b6b53)  
  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)  
  
  
## See Also  
 [Report Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/report-design-view-report-builder.md)   
 [Report Authoring Concepts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-authoring-concepts-report-builder-and-ssrs.md)  
  
  
