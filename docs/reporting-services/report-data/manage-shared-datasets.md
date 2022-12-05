---
title: "Manage Shared Datasets | Microsoft Docs"
description: Learn how to manage shared datasets in Reporting Services so you can share a query to help provide a consistent set of data for multiple reports.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
ms.assetid: 2cbb1fa3-959e-4df6-9887-ebc93cc1b686
author: maggiesMSFT
ms.author: maggies
---
# Manage Shared Datasets
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], shared datasets retrieve data from shared data sources that connect to external data sources. A shared dataset provides a way to share a query to help provide a consistent set of data for multiple reports. The dataset query can include dataset parameters. You can configure a shared dataset to cache query results for specific parameter combinations on first use or by specifying a schedule. You can use shared dataset caching in combination with report caching and report data feeds to help manage access to a data source.  
  
 Shared datasets use only shared data sources, not embedded data sources. A shared dataset can be based on any data source for a supported [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data extension or on a report model.  
  
## Creating and using shared datasets  
 To create a shared dataset, you must use an application that creates a shared dataset definition file (.rsd). You can use one of the following applications to create a shared dataset:  
  
-   Report Builder   Use shared dataset design mode and save the shared dataset to a report server or SharePoint site.  
  
-   Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] / Visual Studio to create shared datasets in the Datasets folder in Solution Explorer. To publish a shared dataset, deploy it to a report server or SharePoint site.  
  
-   Upload a shared dataset definition (.rsd) file   You can upload a file to the report server or SharePoint site. On a SharePoint site. An uploaded file is not validated against the schema until the shared dataset is cached or used in a report.  
  
 The shared dataset definition includes a query, dataset parameters including default values, data options such as case sensitivity, and dataset filters. Values that you set in the definition are used whenever the shared dataset is included in a report.  
  
 To use a shared dataset in a report, you open an application such as Report Builder, browse to the report server or SharePoint site, and select the shared dataset. This adds an instance of the shared dataset to the report. In the report, you cannot view or change the query or the shared data source for the shared dataset. You can specify an additional set of dataset property values that apply to the instance in the report. For example, you can add a filter or change data options such as case sensitivity. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
## Managing shared datasets  
 To manage the properties of a published shared dataset, you can use the web portal of a native mode report server, or application pages on a SharePoint site, if you deployed the report server in SharePoint integrated mode. The tasks that you can perform on a shared dataset depend on your role assignments and on site level and item level permissions, including permissions on the folder if permission inheritance is in effect. Item level security for shared datasets follow the same model as item level security for reports. For more information, see [Secure Shared Dataset Items](../../reporting-services/security/secure-shared-dataset-items.md).  
  
 You can manage the shared dataset item properties, including the shared data source to use, independently from the report that uses the shared dataset or the shared data source that it depends on. To change the query or other dataset properties that are part of the shared dataset definition, you must edit the definition.  
  
### Manage shared dataset item properties  
 The following table lists the item properties that you can change for a shared dataset item.  
  
|Property|Description|  
|--------|-----------|  
|Edit Name|Change the name of the shared dataset. All references from dependent items will continue to work.|  
|Edit Description|Change the description of the shared dataset.|  
|Edit Query execution time out|Set the query execution timeout in seconds. Zero (0) seconds means no time out. Determines the number of seconds before the dataset query times out. To specify no timeout value, use 0. For more information, see [Setting Time-out Values for Report and Shared Dataset Processing &#40;SSRS&#41;](../../reporting-services/report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md).|  
|View dependent items|View the items that use this shared dataset: published report parts, shared data sources, and reports.|  
  
 The following additional shared dataset properties are automatically configured:  
  
|Property|Description|  
|--------------|-----------------|  
|HasDataSourceCredentials|Whether the associated shared data source has credentials saved on the report server.|  
|HasUserProfileDependencies|Whether the report has a reference to the User global collection in its query or in filter expressions.|  
  
## Viewing or changing the shared dataset definition  
 Shared dataset properties, including the query, dataset parameters, default values, dataset filters, and data options such as collation and case sensitivity, are saved in the shared dataset definition. If you have sufficient permissions, you can view and change the definition.  
  
 To view or change the shared dataset definition, edit the shared dataset in an application such as Report Builder in shared dataset design mode. After you make changes, save the shared dataset definition back to the server or site.  
  
 Another way to view the shared dataset definition in XML is to use URL access syntax in the web portal. For example, to view the default values for each dataset parameter, you can use the following URL access command to display a shared dataset definition named DataSet1 from the report server:  
  
## Controlling access to the shared dataset definition  
 By default, the following tasks apply to operations on shared datasets.  
  
-   **View Reports** View shared dataset items and item properties.  
  
-   **Consume Reports** Read shared dataset definitions.  
  
-   **Manage Reports** Create and delete shared datasets and edit shared dataset properties.  
  
-   **Set security on Items** View and modify security settings for shared datasets.  
  
 For more information about which tasks and permissions control access to data source properties on a native mode report server, see [Secure Shared Dataset Items](../../reporting-services/security/secure-shared-dataset-items.md).  
  
 Permissions to view and edit properties for items in a SharePoint library are determined by the site administrator. For more information, see [SharePoint Site and List Permission Reference for Report Server Items](../../reporting-services/security/sharepoint-site-and-list-permission-reference-for-report-server-items.md).  
  
## How to work with shared dataset properties on a Report Server  
 You can use a variety of tools to work with shared datasets. The following table summarizes the approaches and tools, and provides a link to additional instructions.  
  
|Task      |Tool      |Link      |  
|----------|----------|----------|  
|Add a shared dataset or change shared dataset definition properties.|Save in Report Builder.<br /><br /> Deploy in Report Designer.<br /><br /> Upload an .rsd file in the web portal|[Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)]<br /><br /> [Upload a File or Report in the report server](../../reporting-services/reports/upload-a-file-or-report-report-manager.md)<br /><br /> If you upload a shared dataset before the shared data source that it depends is published, you must manually bind the shared dataset to the shared data source. For more information, see [../../reporting-services/Work with shared datasets - web portal](../work-with-shared-datasets-web-portal.md).|  
|Change shared dataset item properties.|web portal|[Work with shared datasets - web portal](../../reporting-services/work-with-shared-datasets-web-portal.md)|  
|Specify additional shared dataset properties for a shared dataset instance in a report.|Report Builder Report Designer|[Dataset Properties Dialog Box, Query (Report Builder)](../../reporting-services/report-data/dataset-properties-dialog-box-query-report-builder.md)|  
|Bind to a different shared data source for a shared dataset.|web portal|[Configure Data Source Properties for a Paginated Report - SSRS](../../reporting-services/report-data/configure-data-source-properties-for-a-report-report-manager.md)|  
|Verify default values for dataset parameters.|Open in Report Builder or use URL access syntax.|For example:<br /><br /> `https://localhost/reportserver/?/Datasets/Dataset1&rs:command=GetShareddatasetDefinition`
|Enable caching|web portal|[Cache Shared Datasets &#40;SSRS&#41;](../../reporting-services/report-server/cache-shared-datasets-ssrs.md)|  
|Create or edit a cache refresh plan|web portal|[Cache a Shared Dataset](../../reporting-services/report-server/cache-a-shared-dataset.md)|  
|In SharePoint integrated mode, synchronize the shared dataset definition between the report server and the SharePoint site|SharePoint application pages|Change shared dataset item properties<br /><br /> Change cache options<br /><br /> Change the shared data source|  
  
## Comparing shared datasets with other report server items  
 When you manage multiple types of items on a report server, it helps to understand how items are similar and how they are different from other report server items.  
  
 Shared datasets are similar to shared data sources and reports in the following ways:  
  
-   Like shared data sources, shared datasets are managed independently from the reports that they are used in. Part of managing a shared dataset on a report server is the ability to change the shared data source that it depends on without editing the shared dataset definition.  
  
-   Like reports, shared datasets can be cached. Credentials that are required by the data source must meet caching restrictions and default values must be specified for every parameter. For more information, see [Cache Shared Datasets &#40;SSRS&#41;](../../reporting-services/report-server/cache-shared-datasets-ssrs.md).  
  
-   Like reports, each time processing occurs, the current definition of the item on the report server is used. If you make changes to a shared dataset, each report that uses it will use the current definition on the report server when the report is processed. If caching is enabled for the shared dataset and you make changes to the shared dataset definition, the changes are not used until data in the cache expires. You can use cache refresh plans to help provide a consistent set of data for multiple reports.  
  
 Shared datasets are dissimilar to published report parts in the following way:  
  
-   Unlike published report parts, changes in the shared dataset definition on a report server do not trigger update notifications when the report is opened in a report authoring client. When you run the report, the data from the current shared dataset definition on the report server is used.  

    [!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]
  
 Shared datasets are similar to subscriptions in the following ways:  
  
-   Shared datasets can use item-specific and shared schedules for caching.  
  
-   Shared datasets follow the same rules for specifying parameter values as subscriptions do.  
  
## See also  
 [Report Server Content Management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  
