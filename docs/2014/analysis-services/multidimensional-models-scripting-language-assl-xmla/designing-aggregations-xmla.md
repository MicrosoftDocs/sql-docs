---
title: "Designing Aggregations (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "statistical information [XML for Analysis]"
  - "batches [XML for Analysis]"
  - "aggregations [Analysis Services], XML for Analysis"
  - "XMLA, aggregations"
  - "queries [XMLA]"
  - "XML for Analysis, aggregations"
  - "iterative aggregation process [XMLA]"
ms.assetid: 4dd27afa-10c7-408d-bc24-ca74217ddbcb
author: minewiskan
ms.author: owend
manager: craigg
---
# Designing Aggregations (XMLA)
  Aggregation designs are associated with the partitions of a particular measure group to make sure that the partitions use the same structure when storing aggregations. Using the same storage structure for partitions lets you to easily define partitions that can be later merged using the [MergePartitions](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/mergepartitions-element-xmla) command. For more information about aggregation designs, see [Aggregations and Aggregation Designs](../multidimensional-models-olap-logical-cube-objects/aggregations-and-aggregation-designs.md).  
  
 To define aggregations for an aggregation design, you can use the [DesignAggregations](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/designaggregations-element-xmla) command in XML for Analysis (XMLA). The `DesignAggregations` command has properties that identify which aggregation design to use as a reference and how to control the design process based upon that reference. Using the `DesignAggregations` command and its properties, you can design aggregations iteratively or in batch, and then view the resulting design statistics to evaluate the design process.  
  
## Specifying an Aggregation Design  
 The [Object](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/object-element-xmla) property of the `DesignAggregations` command must contain an object reference to an existing aggregation design. The object reference contains a database identifier, cube identifier, measure group identifier, and aggregation design identifier. If the aggregation design does not already exist, an error occurs.  
  
## Controlling the Design Process  
 You can use the following properties of the `DesignAggregations` command to control the algorithm used to define aggregations for the aggregation design:  
  
-   The [Steps](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/steps-element-xmla) property determines how many iterations the `DesignAggregations` command should take before it returns control to the client application.  
  
-   The [Time](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/time-element-xmla) property determines how many milliseconds the `DesignAggregations` command should take before it returns control to the client application.  
  
-   The [Optimization](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/optimization-element-xmla) property determines the estimated percentage of performance improvement the `DesignAggregations` command should try to achieve. If you are iteratively designing aggregations, you only have to send this property on the first command.  
  
-   The [Storage](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/storage-element-xmla) property determines the estimated amount of disk storage, in bytes, used by the `DesignAggregations` command. If you are iteratively designing aggregations, you only have to send this property on the first command.  
  
-   The [Materialize](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/materialize-element-xmla) property determines whether the `DesignAggregations` command should create the aggregations defined during the design process. If you are iteratively designing aggregations, this property should be set to false until you are ready to save the designed aggregations. When set to true, the current design process ends and the defined aggregations are added to the specified aggregation design.  
  
## Specifying Queries  
 The DesignAggregations command supports usage-based optimization command by including one or more `Query` elements in the [Queries](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/queries-element-xmla) property. The `Queries` property can contain one or more [Query](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/query-element-xmla) elements. If the `Queries` property does not contain any `Query` elements, the aggregation design specified in the `Object` element uses a default structure that contains a general set of aggregations. This general set of aggregations is designed to meet the criteria specified in the `Optimization` and `Storage` properties of the `DesignAggregations` command.  
  
 Each `Query` element represents a goal query that the design process uses to define aggregations that target the most frequently used queries. You can either specify your own goal queries, or you can use the information stored by an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in the query log to retrieve information about the most frequently used queries. The Usage-Based Optimization Wizard uses the query log to retrieve goal queries based on time, usage, or a specified user when it sends a `DesignAggregations` command. For more information, see [Usage-Based Optimization Wizard F1 Help](../usage-based-optimization-wizard-f1-help.md).  
  
 If you are iteratively designing aggregations, you only have to pass goal queries in the first `DesignAggregations` command because the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance stores these goal queries and uses these queries during subsequent `DesignAggregations` commands. After you pass goal queries in the first `DesignAggregations` command of an iterative process, any subsequent `DesignAggregations` command that contains goal queries in the `Queries` property generates an error.  
  
 The `Query` element contains a comma-delimited value that contains the following arguments:  
  
 *Frequency*,*Dataset*[,*Dataset*...]  
  
 *Frequency*  
 A weighting factor that corresponds to the number of times that the query has previously been executed. If the `Query` element represents a new query, the *Frequency* value represents the weighting factor used by the design process to evaluate the query. As the frequency value becomes larger, the weight that is put on the query during the design process increases.  
  
 *Dataset*  
 A numeric string that specifies which attributes from a dimension are to be included in the query. This string must have the same number of characters as the number of attributes in the dimension. Zero (0) indicates that the attribute in the specified ordinal position is not included in the query for the specified dimension, while one (1) indicates that the attribute in the specified ordinal position is included in the query for the specified dimension.  
  
 For example, the string "011" would refer to a query involving a dimension with three attributes, from which the second and third attributes are included in the query.  
  
> [!NOTE]  
>  Some attributes are excluded from consideration in the dataset. For more information about excluded attributes, see [Query Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/query-element-xmla).  
  
 Each dimension in the measure group that contains the aggregation design is represented by a *Dataset* value in the `Query` element. The order of *Dataset* values must match the order of dimensions included in the measure group.  
  
## Designing Aggregations Using Iterative or Batch Processes  
 You can use the `DesignAggregations` command as part of an iterative process or a batch process, depending on the interactivity required by the design process.  
  
### Designing Aggregations Using an Iterative Process  
 To iteratively design aggregations, you send multiple `DesignAggregations` commands to provide fine control over the design process. The Aggregation Design Wizard uses this same approach to provide fine control over the design process. For more information, see [Aggregation Design Wizard F1 Help](../aggregation-design-wizard-f1-help.md).  
  
> [!NOTE]  
>  An explicit session is required to iteratively design aggregations. For more information about explicit sessions, see [Managing Connections and Sessions &#40;XMLA&#41;](managing-connections-and-sessions-xmla.md).  
  
 To start the iterative process, you first send a `DesignAggregations` command that contains the following information:  
  
-   The `Storage` and `Optimization` property values on which the whole design process is targeted.  
  
-   The `Steps` and `Time` property values on which the first step of the design process is limited.  
  
-   If you want usage-based optimization, the `Queries` property that contains the goal queries on which the whole design process is targeted.  
  
-   The `Materialize` property set to false. Setting this property to false indicates that the design process does not save the defined aggregations to the aggregation design when the command is completed.  
  
 When the first `DesignAggregations` command finishes, the command returns a rowset that contains design statistics. You can evaluate these design statistics to determine whether the design process should continue or whether the design process is finished. If the process should continue, you then send another `DesignAggregations` command that contains the `Steps` and `Time` values with which this step of the design process is limited. You evaluate the resulting statistics and then determine whether the design process should continue. This iterative process of sending `DesignAggregations` commands and evaluating the results continues until you reach your goals and have a appropriate set of aggregations defined.  
  
 After you have reached the set of aggregations that you want, you send one final `DesignAggregations` command. This final `DesignAggregations` command should have its `Steps` property set to 1 and its `Materialize` property set to true. By using these settings, this final `DesignAggregations` command completes the design process and saves the defined aggregation to the aggregation design.  
  
### Designing Aggregations Using a Batch Process  
 You can also design aggregations in a batch process by sending a single `DesignAggregations` command that contains the `Steps`, `Time`, `Storage`, and `Optimization` property values on which the whole design process is targeted and limited. If you want usage-based optimization, the goal queries on which the design process is targeted should also be included in the `Queries` property. Also make sure that the `Materialize` property is set to true, so that the design process saves the defined aggregations to the aggregation design when the command finishes.  
  
 You can design aggregations using a batch process in either an implicit or explicit session. For more information about implicit and explicit sessions, see [Managing Connections and Sessions &#40;XMLA&#41;](managing-connections-and-sessions-xmla.md).  
  
## Returning Design Statistics  
 When the `DesignAggregations` command returns control to the client application, the command returns a rowset that contains a single row representing the design statistics for the command. The rowset contains the columns listed in the following table.  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|Steps|Integer|The number of steps taken by the command before returning control to the client application.|  
|Time|Long integer|The number of milliseconds taken by the command before returning control to the client application.|  
|Optimization|Double|The estimated percentage of performance improvement achieved by the command before returning control to the client application.|  
|Storage|Long integer|The estimated number of bytes taken by the command before returning control to the client application.|  
|Aggregations|Long integer|The number of aggregations defined by the command before returning control to the client application.|  
|LastStep|Boolean|Indicates whether the data in the rowset represents the last step in the design process. If the `Materialize` property of the command was set to true,  the value of this column is set to true.|  
  
 You can use the design statistics that are contained in the rowset returned after each `DesignAggregations` command in both iterative and batch design. In iterative design, you can use the design statistics to determine and display progress. When you are designing aggregations in batch, you can use the design statistics to determine the number of aggregations created by the command.  
  
## See Also  
 [Developing with XMLA in Analysis Services](developing-with-xmla-in-analysis-services.md)  
  
  
