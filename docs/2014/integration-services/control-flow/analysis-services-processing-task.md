---
title: "Analysis Services Processing Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.asprocessingtask.f1"
helpviewer_keywords: 
  - "Analysis Services Processing task"
  - "processing objects [Integration Services]"
ms.assetid: e5748836-b4ce-4e17-ab6b-617a336f02f4
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Analysis Services Processing Task
  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task processes [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects such as tabular models, cubes, dimensions, and mining models.  
  
 When processing tabular models, keep the following in mind:  
  
-   You cannot perform impact analysis on tabular models.  
  
-   Some processing options for tabular mode are not exposed, such as Process Defrag and Process Recalc. You can perform these functions by using the Execute DDL task.  
  
-   The options Process Index and Process Update are not appropriate for tabular models and should not be used.  
  
-   Batch settings are ignored for tabular models.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a number of tasks that perform business intelligence operations, such as running Data Definition Language (DDL) statements and data mining prediction queries. For more information about related business intelligence tasks, click one of the following topics:  
  
-   [Analysis Services Execute DDL Task](analysis-services-execute-ddl-task.md)  
  
-   [Data Mining Query Task](data-mining-query-task.md)  
  
## Object Processing  
 Multiple objects can be processed at the same time. When processing multiple objects, you define settings that apply to the processing of all the objects in the batch.  
  
 Objects in a batch can be processed in sequence or in parallel. If the batch does not contain objects for which processing sequence is important, parallel processing can speed up processing. If objects in the batch are processed in parallel, you can configure the task to let it determine how many objects to process in parallel, or you can manually specify the number of objects to process at the same time. If objects are processed sequentially, you can set a transaction attribute on the batch either by enlisting all objects in one transaction or by using a separate transaction for each object in the batch.  
  
 When you process analytic objects, you might also want to process the objects that depend on them. The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task includes an option to process any dependent objects in addition to the selected objects.  
  
 Typically, you process dimension tables before processing fact tables. You may encounter errors if you try to process fact tables before processing the dimension tables.  
  
 This task also lets you configure the handling of errors in dimension keys. For example, the task can ignore errors or stop after a specified number of errors occur. The task can use the default error configuration, or you can construct a custom error configuration. In the custom error configuration, you specify how the task handles errors and the error conditions. For example, you can specify that the task should stop running when the fourth error occurs, or specify how the task should handle **Null** key values. The custom error configuration can also include the path of an error log.  
  
> [!NOTE]  
>  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task can process only analytic objects created by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools.  
  
 This task is frequently used in combination with a Bulk Insert task that loads data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, or a Data Flow task that implements a data flow that loads data into a table. For example, the Data Flow task might have a data flow that extracts data from an online transactional database (OLTP) database and loads it into a fact table in a data warehouse, after which the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task is called to process the cube built on the data warehouse.  
  
 The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task uses an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager to connect to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information, see [Analysis Services Connection Manager](../connection-manager/analysis-services-connection-manager.md).  
  
## Error Handling  
  
## Configuration of the Analysis Services Processing Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Analysis Services Processing Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Analysis Services Processing Task Editor &#40;Analysis Services Page&#41;](../analysis-services-processing-task-editor-analysis-services-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about setting these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Programmatic Configuration of the Analysis Services Processing Task  
 For more information about programmatically setting these properties, click one of the following topics:  
  
-   <xref:Microsoft.DataTransformationServices.Tasks.DTSProcessingTask.DTSProcessingTask>  
  
  
