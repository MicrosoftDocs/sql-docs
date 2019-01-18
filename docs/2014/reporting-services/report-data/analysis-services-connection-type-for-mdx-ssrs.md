---
title: "Analysis Services Connection Type for MDX (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: bd2e7148-3124-4e07-9734-22333127c3be
author: markingmyname
ms.author: maghan
manager: craigg
---
# Analysis Services Connection Type for MDX (SSRS)
  To include data from an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cube in your report, you must have a dataset that is based on a report data source of type [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. This built-in data source type is based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data extension. You can retrieve metadata about dimensions, hierarchies, levels, key performance indicators (KPIs), measures, and attributes from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cube for use as report data.  
  
 This data processing extension supports multivalue parameters, server aggregates, and credentials that are managed separately from the connection string.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
##  <a name="Connection"></a> Connection String  
 When you connect to a [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cube, you are connecting to the database object in an Analysis Services instance on a server. The database might have multiple cubes. You specify the cube in the query designer when you build the query. The following example shows a connection string:  
  
```  
data source=<server name>;initial catalog=<database name>  
```  
  
 For more connection string examples, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
 From a report authoring client, the following options are available to specify credentials:  
  
-   Current Windows user (also known as integrated security).  
  
-   Use a stored user name and password.  
  
-   Prompt the user for credentials. This option only supports Windows integrated security.  
  
-   No credentials are required. To use this option, you must have the unattended execution account configured on the report server. For more information, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md) in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in on msdn.microsoft.com.  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md) or [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  
  
  
##  <a name="Query"></a> Queries  
 After you have a data connection to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source, you create a dataset and define a Multidimensional Expression (MDX) query that specifies which data to retrieve from the cube. Use the MDX graphical query designer browse and selecting from the underlying data structures on the data source.  
  
 You can specify a query in the following ways:  
  
-   Build a query interactively. The Analysis Services MDX Query Designer supports the following views:  
  
    -   **Design View** Drag dimensions, members, member properties, measures, and KPIs from the metadata browser to the **Data** pane to build an MDX query. Drag calculated members from the CalculatedMembers pane to the Data pane to define additional dataset fields.  
  
    -   **Query View** Drag dimensions, members, member properties, measures, and KPIs from the metadata browser to the Query pane to build an MDX query. You can edit MDX text directly in the Query pane. Drag calculated members from the CalculatedMembers pane to the Query pane to define additional dataset fields.  
  
     For more information, see [Analysis Services MDX Query Designer User Interface &#40;Report Builder&#41;](../analysis-services-mdx-query-designer-user-interface-report-builder.md).  
  
-   Import an existing MDX query from a report. Use the **Import** query button to browse to an .rdl file and import a query. You can import a query from a report that contains an embedded dataset that is based on a [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source. Importing an MDX query directly from an .mdx file is not supported.  
  
 At design time, run the query to view a result set. The query results are automatically retrieved as a flattened rowset. The columns in the result set for a query populate the field collection for a dataset. After you build the query, view the dataset field collection that is generated from the metadata in the Report Data pane. When the report runs, the actual data is returned from the external data source.  
  
 The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data processing extension supports extended dataset field properties. These are values that are available from the external data source but that do not appear in the Report Data pane. You can use extended field properties supported by the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data processing extension in your report through the built-in `Fields` collection. For properties that have values on the data source, you can access predefined property values such as `FormattedValue`, `Color`, or `UniqueName`. For more information, see [Extended Field Properties for an Analysis Services Database &#40;SSRS&#41;](extended-field-properties-for-an-analysis-services-database-ssrs.md)).  
  
  
  
##  <a name="Parameters"></a> Parameters  
 To include query parameters, create a filter in the filter area in the query designer, and mark the filter as a parameter. For each filter, a dataset is automatically created to provide the available values. By default, these datasets do not appear in the Report Data pane. For more information, see [Define Parameters in the MDX Query Designer for Analysis Services &#40;Report Builder and SSRS&#41;](define-parameters-in-the-mdx-query-designer-for-analysis-services.md) and [Show Hidden Datasets for Parameter Values for Multidimensional Data &#40;Report Builder and SSRS&#41;](show-hidden-datasets-for-parameter-values-multidimensional-data.md).  
  
 By default, each report parameter has data type **Text**. After the report parameters are created, you might have to change default values. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../report-design/report-parameters-report-builder-and-report-designer.md).  
  
  
  
##  <a name="Remarks"></a> Remarks  
 The Analysis Services data extension is based on the XMLA (XML for Analysis) protocol. Result sets from cubes are retrieved through the XMLA protocol as a flattened row set. Ragged hierarchies are not supported. For more information, see [Ragged Hierarchies](../../analysis-services/multidimensional-models/user-defined-hierarchies-ragged-hierarchies.md).  
  
 You can also retrieve data from an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cube from the OLE DB data source type. For more information, see [OLE DB Connection Type &#40;SSRS&#41;](ole-db-connection-type-ssrs.md).  
  
 For more information about version support, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md) in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  
  
  
##  <a name="Related"></a> Related Sections  
 These sections of the documentation provide in-depth conceptual information about report data, as well as procedural information about how to define, customize, and use parts of a report that are related to data.  
  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)  
 Provides an overview of accessing data for your report.  
  
 [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md)  
 Provides information about data connections and data sources.  
  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
 Provides information about embedded and shared datasets.  
  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](dataset-fields-collection-report-builder-and-ssrs.md)  
 Provides information about the dataset field collection generated by the query.  
  
 [Extended Field Properties for an Analysis Services Database &#40;SSRS&#41;](extended-field-properties-for-an-analysis-services-database-ssrs.md)  
 Provides information about extra fields that are available through the XMLA data provider.  
  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md) in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
 Provides in-depth information about platform and version support for each data extension.  
  
  
  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../report-design/report-parameters-report-builder-and-report-designer.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../report-design/expressions-report-builder-and-ssrs.md)  
  
  
