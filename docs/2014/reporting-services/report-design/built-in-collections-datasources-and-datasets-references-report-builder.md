---
title: "DataSources and DataSets Collection References (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: f951a4aa-da55-4e43-8579-4a5d4480d11f
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# DataSources and DataSets Collection References (Report Builder and SSRS)
  The `DataSources` collection represents all the data sources used in a report. Similarly, the `DataSets` collection represents all the datasets for all the data sources in a report. Use the **Report Data** pane for a hierarchical view of report datasets organized under the data source they reference. If you include references to these collections, you will not see values when previewing your report. These collections are only available after the report has been published to a report server.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## DataSources  
 The `DataSources` collection represents the data sources referenced in a published report definition. You may choose to include this information in your report to document the source of the report data. This collection is not available in **Preview** mode. The following table describes the variables within the `DataSources` collection.  
  
|**Variable**|`Type`|**Description**|  
|------------------|--------------|---------------------|  
|`DataSourceReference`|`String`|The full path of the data source definition on the report server. For example, you might include a list of all the data sources a report used as part of a report history. The following example shows the full path for the data source named AdventureWorks2012:<br /><br /> `/DataSources/AdventureWorks2012`.|  
|`Type`|`String`|The type of data provider for the data source. For example, `SQL`.|  
  
## DataSets  
 The `DataSets` collection represents the datasets referenced in a report definition. You may choose to include the query in the report in a text box, so a user interested in exactly which data is in the report can see the original command text. This collection is not available in **Preview** mode. The following table describes the members of the `DataSets` collection.  
  
|**Member**|`Type`|**Description**|  
|----------------|--------------|---------------------|  
|`CommandText`|`String`|For database data sources, this is the query used to retrieve data from the data source. If the query is an expression, this is the evaluated expression.|  
|`RewrittenCommandText`|`String`|The data provider's expanded CommandText value. This is typically used for reports with query parameters that are mapped to report parameters. The data provider sets this property when expanding the command text parameter references into the constant values selected for the mapped report parameters.|  
  
### Using Query Expressions  
 You can use expressions to define the query that is contained in a dataset. You can use this feature to design reports in which the query changes based on input from the user, data in other datasets, or other variables. For more information about queries, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
## See Also  
 [Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)  
  
  
