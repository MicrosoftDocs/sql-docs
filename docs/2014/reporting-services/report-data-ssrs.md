---
title: "Report Data (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e22b7c24-edab-42d6-82f6-95068e1c6043
caps.latest.revision: 14
author: "douglaslM"
ms.author: "douglasl"
manager: "mblythe"
---
# Report Data (SSRS)
  Report data can come from multiple sources of data in your organization. Your first step in designing a report is to create data sources and datasets that represent the underlying report data. Each data source includes data connection information. Each dataset includes a query command that defines the set of fields to use as data from a data source. To visualize data from each dataset, add a data region, such as a table, matrix, chart, or map. When the report is processed, the queries run on the data source, and each data region expands as needed to display the query results for the dataset.  
  
##  <a name="BkMk_ReportDataTerms"></a> Terms  
 If you are unfamiliar with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] concepts, review the following terms in [Reporting Services Concepts &#40;SSRS&#41;](../../2014/reporting-services/reporting-services-concepts-ssrs.md): *data connection*, *embedded data sources*, *shared data sources*, *embedded datasets*, *shared datasets*, *dataset queries*, *report parts*, and *data alerts*.  
  
##  <a name="BkMk_ReportDataTips"></a> Tips for Specifying Report Data  
 Use the following information to design your report data strategy.  
  
-   **Data sources** Data sources can be published and managed independently from reports on a report server or SharePoint site. For each data source, you or the database owner can manage connection information in one place. Data source credentials are stored securely on the report server; you do not include passwords in the connection string. You can redirect a data source from a test server to a production server. You can disable a data source to suspend all reports that use it. For a list of supported data sources, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md).  
  
-   **Datasets** Datasets can be published and managed independently from reports or the shared data sources that they depend on. You or the database owner can provide optimized queries for report authors to use. When you change the query, all reports that use the shared dataset use the updated query. You can enable dataset caching to improve performance. You can schedule query caching for a specific time or use a shared schedule.  
  
-   **Data used by report parts** Report parts can include the data that they depend on. For more information about report parts, see [Report Parts in Report Designer &#40;SSRS&#41;](../../2014/reporting-services/report-parts-in-report-designer-ssrs.md).  
  
-   **Filter data** Report data can be filtered in the query or in the report. You can use datasets and query variables to create cascading parameters, and provide a user the ability to narrow choices from thousands of selections to a more manageable number. You can filter data in a table or chart based on parameter values or other values that you specify.  
  
-   **Parameters** Dataset query commands that include query variables automatically create matching report parameters. You can also manually create parameters. When you view a report, the report toolbar displays the parameters. Users can select values to control report data or report appearance. To customize report data for specific audiences, you can create sets of report parameters with different default values linked to the same report definition, or use the built-in `UserID` field. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../2014/reporting-services/report-parameters-report-builder-and-report-designer.md) and [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/built-in-collections-in-expressions-report-builder-and-ssrs.md).  
  
-   **Data alerts** After a report is published, you can create alerts based on report data, and receive email messages when it meets rules that you specify.  
  
-   **Group and aggregate data** Report data can be grouped and aggregated in the query or in the report. If you aggregate values in the query, you can continue to combine values in the report within the constraints of what is meaningful.  For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/filter-group-and-sort-data-report-builder-and-ssrs.md) and [Aggregate Function &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/aggregate-function-report-builder-and-ssrs.md).  
  
-   **Sort data** Report data can be sorted in the query or in the report. In tables, you can also add an interactive sort button to let the user control the sort order.  
  
-   **Expression-based data** Because most report properties can be expression-based, and expressions can include references to dataset fields and report parameters, you can write powerful expressions to control report data and appearance. You can provide a user the ability to control the data they see by defining parameters.  
  
-   **Display data from a dataset** Data from a dataset is typically displayed on one or more data regions, for example, a table and a chart.  
  
-   **Display data from multiple datasets**  You can write expressions in a data region based on one dataset that look up values or aggregates in other datasets. You can include subreports in a table based on one dataset to display data from a different data source.  
  
## Data Connections, Data Sources, and Datasets  
 Use the following list to help define sources of data for a report.  
  
-   Consider whether to use embedded or shared data sources and datasets. Collaborate with owners of sources of data to implement and use authentication and authorization technology that is appropriate for your organization.  
  
-   Understand the software data layer architecture for your organization and the potential issues arising from data types. Understand how data extensions and data processing extensions can affect query results. Data types differ among the source of data, data providers, and the data types stored in the report definition (.rdl) file.  
  
-   Understand the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] client/server architectures and tools. For example, in Report Designer, you author reports on a client machine that uses built-in data source types. When you publish a report, the data source types must be supported on the report server or SharePoint site.  For more information, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../2014/reporting-services/data-sources-supported-by-reporting-services-ssrs.md).  
  
-   Data sources and datasets are authored in a report and published to a report server or SharePoint site from a client authoring tool. Data sources can be created directly on the report server. After they are published, you can configure credentials and other properties on the report server. For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md) and [Reporting Services Tools](../../2014/reporting-services/reporting-services-tools.md).  
  
-   The data sources you can use depend on which [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data extensions are installed. Support for data sources can differ by client authoring tool, report server version, and report server platform. For more information, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../2014/reporting-services/data-sources-supported-by-reporting-services-ssrs.md).  
  
-   Data source credentials vary based on data source type and on whether you are viewing reports on your client or report server or SharePoint site. For more information, see [Set Permissions for Report Server Items on a SharePoint Site &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../2014/reporting-services/set-permissions-for-report-server-items-on-a-sharepoint-site.md), [Specify Credential and Connection Information for Report Data Sources](../integration-services/connection-manager/data-sources.md), and credential information specific to each tool in [Reporting Services Tools](../../2014/reporting-services/reporting-services-tools.md).  
  
## Related Tasks  
 Tasks related to creating data connections, adding data from external sources, datasets, and queries.  
  
|||  
|-|-|  
|**Common Tasks**|**Links**|  
|Create data connections|[Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md)|  
|Create datasets and queries|[Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)|  
|Manage data sources after they are published|[Manage Report Data Sources](../../2014/reporting-services/manage-report-data-sources.md)|  
|Manage shared datasets after they are published|[Manage Shared Datasets](../../2014/reporting-services/manage-shared-datasets.md)|  
|Create and manage data alerts|[Reporting Services Data Alerts](../ssms/agent/alerts.md)|  
|Cache a shared dataset|[Cache Shared Datasets &#40;SSRS&#41;](../../2014/reporting-services/cache-shared-datasets-ssrs.md)|  
|Schedule a shared dataset to preload the cache|[Schedules](../../2014/reporting-services/schedules.md)|  
|Add a data extension|[Implementing a Data Processing Extension](../../2014/reporting-services/dev-guide/implementing-a-data-processing-extension.md)|  
  
## Related Content  
  