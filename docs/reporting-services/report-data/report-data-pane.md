---
title: Report Data pane
author: maggiesMSFT
ms.author: maggies
manager: kfile
ms.reviewer: ""
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data
ms.topic: conceptual
ms.custom: seodec18
ms.date: 12/14/2018
---

# Report Data pane in SQL Server Reporting Services (SSRS)

  Use the **Report Data** pane to view the currently defined parameters, data sources, datasets, field collections, and images in your report. The Report Data pane displays a hierarchical view of the items that represent data in your report. The top level nodes represent built-in fields, parameters, images, and data source references. Expand each node to view the data items. For example, when you expand a data source node, the datasets defined for that data source appear. When you expand a dataset, its field collection appears. Drag items to the report design surface to link data with report items on the report page.  
  
## Options

 **Built-in Fields**  
 Represents fields provided by Reporting Services that are commonly used in a report, such as the report name or page number. For more information, see [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-in-expressions-report-builder.md).  
  
 **Parameters**  
 Represents the collection of report parameters, each of which can be single-valued or multivalued. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
 **Images**  
 Represents the set of images used in the report. For more information, see [Images &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/images-report-builder-and-ssrs.md).  
  
 **Data source**  
 Represents a single data source reference to an embedded data source or to a shared data source. In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], shared data sources appear in Solution Explorer under the Shared Data Sources folder. A data source specifies one of the data source types supported by Reporting Services. A data source is the parent node for the collection of datasets based on it. For more information, see [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md) .  
  
 **Dataset**  
 Represents a single dataset. A dataset is the parent node for the collection of fields specified by the query and including any calculated fields. Reporting Services supports query designers to help you specify a query. For more information, see [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md) and [Query Design Tools &#40;SSRS&#41;](../../reporting-services/report-data/query-design-tools-ssrs.md).  
  
## Next steps

 - [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)
 - [Grouping Pane](../../reporting-services/tools/grouping-pane.md)