---
title: "Integration Services Transformations | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "transformations [Integration Services], listed"
  - "transformations [Integration Services], types"
  - "transformations [Integration Services]"
  - "data flow [Integration Services], transformations"
  - "business intelligence transformations [Integration Services]"
  - "join transformations"
  - "split transformations [Integration Services]"
  - "custom transformations [Integration Services]"
  - "row transformations [Integration Services]"
  - "rowset transformations [Integration Services]"
ms.assetid: c70c4f6e-82dd-4948-b923-fd5193f67f7e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services Transformations
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] transformations are the components in the data flow of a package that aggregate, merge, distribute, and modify data. Transformations can also perform lookup operations and generate sample datasets. This section describes the transformations that [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes and explains how they work.  
  
## Business Intelligence Transformations  
 The following transformations perform business intelligence operations such as cleaning data, mining text, and running data mining prediction queries.  
  
|Transformation|Description|  
|--------------------|-----------------|  
|[Slowly Changing Dimension Transformation](../../../integration-services/data-flow/transformations/slowly-changing-dimension-transformation.md)|The transformation that configures the updating of a slowly changing dimension.|  
|[Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/fuzzy-grouping-transformation.md)|The transformation that standardizes values in column data.|  
|[Fuzzy Lookup Transformation](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation.md)|The transformation that looks up values in a reference table using a fuzzy match.|  
|[Term Extraction Transformation](../../../integration-services/data-flow/transformations/term-extraction-transformation.md)|The transformation that extracts terms from text.|  
|[Term Lookup Transformation](../../../integration-services/data-flow/transformations/term-lookup-transformation.md)|The transformation that looks up terms in a reference table and counts terms extracted from text.|  
|[Data Mining Query Transformation](../../../integration-services/data-flow/transformations/data-mining-query-transformation.md)|The transformation that runs data mining prediction queries.|  
|[DQS Cleansing Transformation](../../../integration-services/data-flow/transformations/dqs-cleansing-transformation.md)|The transformation that corrects data from a connected data source by applying rules that were created for the data source.|  
  
## Row Transformations  
 The following transformations update column values and create new columns. The transformation is applied to each row in the transformation input.  
  
|Transformation|Description|  
|--------------------|-----------------|  
|[Character Map Transformation](../../../integration-services/data-flow/transformations/character-map-transformation.md)|The transformation that applies string functions to character data.|  
|[Copy Column Transformation](../../../integration-services/data-flow/transformations/copy-column-transformation.md)|The transformation that adds copies of input columns to the transformation output.|  
|[Data Conversion Transformation](../../../integration-services/data-flow/transformations/data-conversion-transformation.md)|The transformation that converts the data type of a column to a different data type.|  
|[Derived Column Transformation](../../../integration-services/data-flow/transformations/derived-column-transformation.md)|The transformation that populates columns with the results of expressions.|  
|[Export Column Transformation](../../../integration-services/data-flow/transformations/export-column-transformation.md)|The transformation that inserts data from a data flow into a file.|  
|[Import Column Transformation](../../../integration-services/data-flow/transformations/import-column-transformation.md)|The transformation that reads data from a file and adds it to a data flow.|  
|[Script Component](../../../integration-services/data-flow/transformations/script-component.md)|The transformation that uses script to extract, transform, or load data.|  
|[OLE DB Command Transformation](../../../integration-services/data-flow/transformations/ole-db-command-transformation.md)|The transformation that runs SQL commands for each row in a data flow.|  
  
## Rowset Transformations  
 The following transformations create new rowsets. The rowset can include aggregate and sorted values, sample rowsets, or pivoted and unpivoted rowsets.  
  
|Transformation|Description|  
|--------------------|-----------------|  
|[Aggregate Transformation](../../../integration-services/data-flow/transformations/aggregate-transformation.md)|The transformation that performs aggregations such as AVERAGE, SUM, and COUNT.|  
|[Sort Transformation](../../../integration-services/data-flow/transformations/sort-transformation.md)|The transformation that sorts data.|  
|[Percentage Sampling Transformation](../../../integration-services/data-flow/transformations/percentage-sampling-transformation.md)|The transformation that creates a sample data set using a percentage to specify the sample size.|  
|[Row Sampling Transformation](../../../integration-services/data-flow/transformations/row-sampling-transformation.md)|The transformation that creates a sample data set by specifying the number of rows in the sample.|  
|[Pivot Transformation](../../../integration-services/data-flow/transformations/pivot-transformation.md)|The transformation that creates a less normalized version of a normalized table.|  
|[Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md)|The transformation that creates a more normalized version of a nonnormalized table.|  
  
## Split and Join Transformations  
 The following transformations distribute rows to different outputs, create copies of the transformation inputs, join multiple inputs into one output, and perform lookup operations.  
  
|Transformation|Description|  
|--------------------|-----------------|  
|[Conditional Split Transformation](../../../integration-services/data-flow/transformations/conditional-split-transformation.md)|The transformation that routes data rows to different outputs.|  
|[Multicast Transformation](../../../integration-services/data-flow/transformations/multicast-transformation.md)|The transformation that distributes data sets to multiple outputs.|  
|[Union All Transformation](../../../integration-services/data-flow/transformations/union-all-transformation.md)|The transformation that merges multiple data sets.|  
|[Merge Transformation](../../../integration-services/data-flow/transformations/merge-transformation.md)|The transformation that merges two sorted data sets.|  
|[Merge Join Transformation](../../../integration-services/data-flow/transformations/merge-join-transformation.md)|The transformation that joins two data sets using a FULL, LEFT, or INNER join.|  
|[Lookup Transformation](../../../integration-services/data-flow/transformations/lookup-transformation.md)|The transformation that looks up values in a reference table using an exact match.|  
|[Cache Transform](../../../integration-services/data-flow/transformations/cache-transform.md)|The transformation that writes data from a connected data source in the data flow to a Cache connection manager that saves the data to a cache file. The Lookup transformation performs lookups on the data in the cache file.|  
|[Balanced Data Distributor Transformation](../../../integration-services/data-flow/transformations/balanced-data-distributor-transformation.md)|The transformation distributes buffers of incoming rows uniformly across outputs on separate threads to improve performance of SSIS packages running on multi-core and multi-processor servers.|  
  
## Auditing Transformations  
 [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes the following transformations to add audit information and count rows.  
  
|Transformation|Description|  
|--------------------|-----------------|  
|[Audit Transformation](../../../integration-services/data-flow/transformations/audit-transformation.md)|The transformation that makes information about the environment available to the data flow in a package.|  
|[Row Count Transformation](../../../integration-services/data-flow/transformations/row-count-transformation.md)|The transformation that counts rows as they move through it and stores the final count in a variable.|  
  
## Custom Transformations  
 You can also write custom transformations. For more information, see [Developing a Custom Transformation Component with Synchronous Outputs](../../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-synchronous-outputs.md) and [Developing a Custom Transformation Component with Asynchronous Outputs](../../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-asynchronous-outputs.md).  
  
  
