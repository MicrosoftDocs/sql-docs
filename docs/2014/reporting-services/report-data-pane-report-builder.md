---
title: "Report Data Pane (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10435"
helpviewer_keywords: 
  - "Report Data pane"
ms.assetid: 1492aa39-aeb1-4509-ab97-b9edd0901b7e
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Report Data Pane (Report Builder)
  Use the **Report Data** pane to view the currently defined parameters, data sources, datasets, field collections, and images in your report. The Report Dane displays a hierarchical view of the items that represent data in your report. The top level nodes represent built-in fields, parameters, images, and data source references. Expand each node to view the data items. For example, when you expand a data source node, the datasets defined for that data source appear. When you expand a dataset, its field collection appears. Drag items to the report design surface or to the Grouping pane to link data with selected report items on the report page. For more information, see [Report Design View &#40;Report Builder&#41;](report-builder/report-design-view-report-builder.md).  
  
## Options  
 **Built-in Fields**  
 Represents commonly used fields in a report, such as the report name or page number. For more information, see [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](report-design/built-in-collections-in-expressions-report-builder.md).  
  
 **Parameters**  
 Represents the collection of report parameters, each of which can be single-valued or multivalued. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](report-design/report-parameters-report-builder-and-report-designer.md).  
  
 **Images**  
 Represents the set of images used in the report. For more information, see [Images &#40;Report Builder and SSRS&#41;](report-design/images-report-builder-and-ssrs.md).  
  
 **Data sources**  
 Represents an embedded data source or a reference to a shared data source. A data source represents a source of data for the report. A data source is the parent node for the collection of datasets that use it. For more information, see [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-data/report-datasets-ssrs.md) and [Data Connections, Data Sources, and Connection Strings in Report Builder](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
 **Datasets**  
 Represents the data that is retrieved from a data source by running one command, for example, a [!INCLUDE[tsql](../includes/tsql-md.md)] query that retrieves data from a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database. A dataset is the parent node for the collection of fields that is specified by the query, and also includes calculated fields. Report Builder supports query designers that help you specify a query. For more information, see [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-data/report-datasets-ssrs.md).  
  
## See Also  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](report-data/dataset-fields-collection-report-builder-and-ssrs.md)   
 [Report Builder Help for Dialog Boxes, Panes, and Wizards](../../2014/reporting-services/report-builder-help-for-dialog-boxes-panes-and-wizards.md)   
 [Grouping Pane &#40;Report Builder&#41;](report-design/grouping-pane-report-builder.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)  
  
  
