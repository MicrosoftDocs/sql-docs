---
title: "Embedded and Shared Datasets (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: adc95cc0-d15a-413d-bc5a-302eab37a069
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Embedded and Shared Datasets (Report Builder and SSRS)
  In a report, a dataset represents report data that is returned from running a query on an external data source. The dataset depends on the data connection that contains information about the external data source. The data itself is not included in the report definition. The dataset contains a query command, a field collection, parameters, filters, and data options that include case sensitivity and collation. There are two types of datasets:  
  
-   **Shared datasets.** A shared dataset is published on a report server and can be used by multiple reports. A shared dataset must be based on a shared data source. A shared dataset can be cached and scheduled by creating a cache refresh plan.  
  
-   **Embedded datasets.** Embedded datasets are defined in and used by a single report.  
  
 The difference between the two is in how they are created, stored, and managed.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Shared Datasets  
 Use a shared dataset to provide a query that can be used by more than one report. Shared datasets are stored on the report server and managed separately from reports or shared data sources. For example, a report server administrator might update the query to take advantage of improved indexing or other query performance optimization.  
  
 We recommend that you use shared datasets as much as possible. You can optimize a query or cache query results to benefit report performance. Shared datasets make data access easier to manage, and help to keep reports and the datasets they access more secure and more performant.  
  
 In Report Designer, you can create shared datasets as part of a report project, and control whether to deploy them to a report server. You cannot browse to a report server and select a shared dataset to add to your report.  
  
 In Report Builder, you can do the following:  
  
1.  To create a shared dataset, use Shared Dataset Design View. You can save it to a report server or SharePoint site to share with other reports. You can also browse to the report server and edit and existing shared dataset. In this view, you can build a query and set all dataset options. For more information, see [Shared Dataset Design View &#40;Report Builder&#41;](../report-builder/shared-dataset-design-view-report-builder.md).  
  
2.  To add a shared dataset to your report, open Report Builder in Report Design View. From a wizard or from the Report Data pane, browse to the report server and select the shared dataset to add to your report. In this view, you cannot change the query except to add fields. You can override other data options and add filters. You cannot remove filters.  
  
3.  The following table compares the properties that can be configured for the definition of the shared dataset on the report server and the instance of the shared dataset in the report definition.  
  
    |Property|Configuration Notes for the Definition|Configuration Notes for the Instance|  
    |--------------|--------------------------------------------|------------------------------------------|  
    |Query text|Configure the query, including defining it as expression.|Cannot change the query.|  
    |Query parameters|Cannot reference report parameters<br /><br /> Includes default values<br /><br /> Includes a Read Only flag|Configure parameters that are not marked Read Only in the definition|  
    |Filters|Define filters|Cannot view or change dataset filters that are part of the definition<br /><br /> Can create additional filters|  
    |Data Source|Must be a shared data source|Cannot change the data source|  
    |Fields|Fields from the query command<br /><br /> Calculated fields are not part of the dataset definition|View fields, but cannot change them<br /><br /> The field collection is static based on the query at the time you added the shared dataset to the report. To update, click **Refresh Fields** in the **Dataset Properties** dialog box. The actual field collection is whatever the current query in the definition returns.<br /><br /> Add calculated fields|  
    |Dataset|Data options such as case sensitivity|Override data options in the instance|  
  
## Embedded Datasets  
 Use an embedded dataset when you want to get data from an external data source to be used only in one report. Embedded datasets are useful when you want to create a query that has no other dependencies and that you do not need to use for multiple reports.  
  
 To create or edit an embedded dataset, use the Report Data pane. After you create a dataset, you can configure the properties in the **Dataset Properties** dialog box.  
  
## See Also  
 [Embedded and Shared Data Connections or Data Sources &#40;Report Builder and SSRS&#41;](../embedded-and-shared-data-connections-or-data-sources-report-builder-and-ssrs.md)   
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)   
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)   
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](dataset-fields-collection-report-builder-and-ssrs.md)   
 [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md)   
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md)  
  
  
