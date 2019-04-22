---
title: "Manage Shared Datasets | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 2cbb1fa3-959e-4df6-9887-ebc93cc1b686
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Manage Shared Datasets
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], shared datasets retrieve data from shared data sources that connect to external data sources. A shared dataset provides a way to share a query to help provide a consistent set of data for multiple reports. The dataset query can include dataset parameters. You can configure a shared dataset to cache query results for specific parameter combinations on first use or by specifying a schedule. You can use shared dataset caching in combination with report caching and report data feeds to help manage access to a data source.  
  
 Shared datasets use only shared data sources, not embedded data sources. A shared dataset can be based on any data source for a supported [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data extension or on a report model.  
  
## Creating and Using Shared Datasets  
 To create a shared dataset, you must use an application that creates a shared dataset definition file (.rsd). You can use one of the following applications to create a shared dataset:  
  
-   Report Builder   Use shared dataset design mode and save the shared dataset to a report server or SharePoint site.  
  
-   Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] Create shared datasets under the Shared Dataset folder in Solution Explorer. To publish a shared dataset, deploy it to a report server or SharePoint site.  
  
-   Upload a shared dataset definition (.rsd) file   You can upload a file to the report server or SharePoint site. On a SharePoint site. An uploaded file is not validated against the schema until the shared dataset is cached or used in a report.  
  
 The shared dataset definition includes a query, dataset parameters including default values, data options such as case sensitivity, and dataset filters. Values that you set in the definition are used whenever the shared dataset is included in a report.  
  
 To use a shared dataset in a report, you open an application such as Report Builder, browse to the report server or SharePoint site, and select the shared dataset. This adds an instance of the shared dataset to the report. In the report, you cannot view or change the query or the shared data source for the shared dataset. You can specify an additional set of dataset property values that apply to the instance in the report. For example, you can add a filter or change data options such as case sensitivity. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md) in the [Report Builder documentation](https://go.microsoft.com/fwlink/?LinkId=154494) on msdn.microsoft.com.  
  
## Managing Shared Datasets  
 To manage the properties of a published shared dataset, you can use Report Manager for a native mode report server, or application pages on a SharePoint site if you deployed the report server in SharePoint integrated mode. The tasks that you can perform on a shared dataset depend on your role assignments and on site level and item level permissions, including permissions on the folder if permission inheritance is in effect. Item level security for shared datasets follow the same model as item level security for reports. For more information, see [Secure Shared Dataset Items](../security/secure-shared-dataset-items.md).  
  
 You can manage the shared dataset item properties, including the shared data source to use, independently from the report that uses the shared dataset or the shared data source that it depends on. To change the query or other dataset properties that are part of the shared dataset definition, you must edit the definition.  
  
### Manage Shared Dataset Item Properties  
 The following table lists the item properties that you can change for a shared dataset item.  
  
|||  
|-|-|  
|Edit Name|Change the name of the shared dataset. All references from dependent items will continue to work.|  
|Edit Description|Change the description of the shared dataset.|  
|Edit Query execution time out|Set the query execution timeout in seconds. Zero (0) seconds means no time out. Determines the number of seconds before the dataset query times out. To specify no timeout value, use 0. For more information, see [Setting Time-out Values for Report and Shared Dataset Processing &#40;SSRS&#41;](../report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md).|  
|View dependent items|View the items that use this shared dataset: published report parts, shared data sources, and reports.|  
  
 The following additional shared dataset properties are automatically configured:  
  
|Property|Description|  
|--------------|-----------------|  
|HasDataSourceCredentials|Whether the associated shared data source has credentials saved on the report server.|  
|HasUserProfileDependencies|Whether the report has a reference to the User global collection in its query or in filter expressions.|  
  
## Viewing or Changing the Shared Dataset Definition  
 Shared dataset properties, including the query, dataset parameters, default values, dataset filters, and data options such as collation and case sensitivity, are saved in the shared dataset definition. If you have sufficient permissions, you can view and change the definition.  
  
 To view or change the shared dataset definition, edit the shared dataset in an application such as Report Builder in shared dataset design mode. After you make changes, save the shared dataset definition back to the server or site.  
  
 Another way to view the shared dataset definition in XML is to use URL access syntax in Report Manager. For example, to view the default values for each dataset parameter, you can use the following URL access command to display a shared dataset definition named DataSet1 from the report server:  
  
```  
http://localhost/reportserver/?/DataSet1&rs:command=GetShareddatasetDefinition  
```  
  
## Controlling Access to the Shared Dataset Definition  
 By default, the following tasks apply to operations on shared datasets.  
  
-   **View Reports** View shared dataset items and item properties.  
  
-   **Consume Reports** Read shared dataset definitions.  
  
-   **Manage Reports** Create and delete shared datasets and edit shared dataset properties.  
  
-   **Set security on Items** View and modify security settings for shared datasets.  
  
 For more information about which tasks and permissions control access to data source properties on a native mode report server, see [Secure Shared Dataset Items](../security/secure-shared-dataset-items.md).  
  
 Permissions to view and edit properties for items in a SharePoint library are determined by the site administrator. For more information, see [SharePoint Site and List Permission Reference for Report Server Items](../security/sharepoint-site-and-list-permission-reference-for-report-server-items.md).  
  
## How to Work with Shared Dataset Properties on a Report Server  
 You can use a variety of tools to work with shared datasets. The following table summarizes the approaches and tools, and provides a link to additional instructions.  
  
|Task|Tool|Link|  
|----------|----------|----------|  
|Add a shared dataset or change shared dataset definition properties.|Save in Report Builder.<br /><br /> Deploy in Report Designer.<br /><br /> Upload an .rsd file in Report Manager|[Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md) in the [Report Builder documentation](https://go.microsoft.com/fwlink/?LinkId=154494) on msdn.microsoft.com<br /><br /> [Upload File Page &#40;Report Manager&#41;](../upload-file-page-report-manager.md)<br /><br /> If you upload a shared dataset before the shared data source that it depends is published, you must manually bind the shared dataset to the shared data source. For more information, see [General Properties Page, Shared Datasets &#40;Report Manager&#41;](../general-properties-page-shared-datasets-report-manager.md).|  
|Change shared dataset item properties.|Report Manager|[General Properties Page, Shared Datasets &#40;Report Manager&#41;](../general-properties-page-shared-datasets-report-manager.md)|  
|Specify additional shared dataset properties for a shared dataset instance in a report.|Report Builder Report Designer|[Dataset Properties Dialog Box, Query](../dataset-properties-dialog-box-query.md)|  
|Bind to a different shared data source for a shared dataset.|Report Manager|[Data Source Selection Page &#40;Report Manager&#41;](../data-source-selection-page-report-manager.md)|  
|Verify default values for dataset parameters.|Open in Report Builder or use URL access syntax.|For example:<br /><br /> `http://localhost/reportserver/?/DataSet1&rs:command=GetShareddatasetDefinition`|  
|Enable caching|Report Manager|[Cache Shared Datasets &#40;SSRS&#41;](../report-server/cache-shared-datasets-ssrs.md)<br /><br /> [Caching Page, Shared Datasets &#40;Report Manager&#41;](../caching-page-shared-datasets-report-manager.md)|  
|Create or edit a cache refresh plan|Report Manager|[Cache Refresh Options &#40;Report Manager&#41;](../cache-refresh-options-report-manager.md)|  
|View the shared dataset definition schema.|Report Manager|`http://<reportserver>/shareddatasetdefinition.xsd`|  
|In SharePoint integrated mode, synchronize the shared dataset definition between the report server and the SharePoint site|SharePoint application pages|Change shared dataset item properties<br /><br /> Change cache options<br /><br /> Change the shared data source|  
  
## Comparing Shared Datasets with Other Report Server Items  
 When you manage multiple types of items on a report server, it helps to understand how items are similar and how they are different from other report server items.  
  
 Shared datasets are similar to shared data sources and reports in the following ways:  
  
-   Like shared data sources, shared datasets are managed independently from the reports that they are used in. Part of managing a shared dataset on a report server is the ability to change the shared data source that it depends on without editing the shared dataset definition.  
  
-   Like reports, shared datasets can be cached. Credentials that are required by the data source must meet caching restrictions and default values must be specified for every parameter. For more information, see [Cache Shared Datasets &#40;SSRS&#41;](../report-server/cache-shared-datasets-ssrs.md).  
  
-   Like reports, each time processing occurs, the current definition of the item on the report server is used. If you make changes to a shared dataset, each report that uses it will use the current definition on the report server when the report is processed. If caching is enabled for the shared dataset and you make changes to the shared dataset definition, the changes are not used until data in the cache expires. You can use cache refresh plans to help provide a consistent set of data for multiple reports.  
  
 Shared datasets are dissimilar to published report parts in the following way:  
  
-   Unlike published report parts, changes in the shared dataset definition on a report server do not trigger update notifications when the report is opened in a report authoring client. When you run the report, the data from the current shared dataset definition on the report server is used.  
  
 Shared datasets are similar to subscriptions in the following ways:  
  
-   Shared datasets can use item-specific and shared schedules for caching.  
  
-   Shared datasets follow the same rules for specifying parameter values as subscriptions do.  
  
## See Also  
 [Report Server Content Management &#40;SSRS Native Mode&#41;](../report-server/report-server-content-management-ssrs-native-mode.md)   
 [Granting Permissions on a Native Mode Report Server](../security/granting-permissions-on-a-native-mode-report-server.md)  
  
  
