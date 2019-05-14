---
title: "Analysis Services Processing Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.asprocessingtask.f1"
  - "sql13.dts.designer.asprocessingtask.general.f1"
  - "sql13.dts.designer.asprocessingtask.as.f1"
helpviewer_keywords: 
  - "Analysis Services Processing task"
  - "processing objects [Integration Services]"
ms.assetid: e5748836-b4ce-4e17-ab6b-617a336f02f4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Analysis Services Processing Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task processes [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects such as tabular models, cubes, dimensions, and mining models.  
  
 When processing tabular models, keep the following in mind:  
  
-   You cannot perform impact analysis on tabular models.  
  
-   Some processing options for tabular mode are not exposed, such as Process Defrag and Process Recalc. You can perform these functions by using the Execute DDL task.  
  
-   The options Process Index and Process Update are not appropriate for tabular models and should not be used.  
  
-   Batch settings are ignored for tabular models.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a number of tasks that perform business intelligence operations, such as running Data Definition Language (DDL) statements and data mining prediction queries. For more information about related business intelligence tasks, click one of the following topics:  
  
-   [Analysis Services Execute DDL Task](../../integration-services/control-flow/analysis-services-execute-ddl-task.md)  
  
-   [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md)  
  
## Object Processing  
 Multiple objects can be processed at the same time. When processing multiple objects, you define settings that apply to the processing of all the objects in the batch.  
  
 Objects in a batch can be processed in sequence or in parallel. If the batch does not contain objects for which processing sequence is important, parallel processing can speed up processing. If objects in the batch are processed in parallel, you can configure the task to let it determine how many objects to process in parallel, or you can manually specify the number of objects to process at the same time. If objects are processed sequentially, you can set a transaction attribute on the batch either by enlisting all objects in one transaction or by using a separate transaction for each object in the batch.  
  
 When you process analytic objects, you might also want to process the objects that depend on them. The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task includes an option to process any dependent objects in addition to the selected objects.  
  
 Typically, you process dimension tables before processing fact tables. You may encounter errors if you try to process fact tables before processing the dimension tables.  
  
 This task also lets you configure the handling of errors in dimension keys. For example, the task can ignore errors or stop after a specified number of errors occur. The task can use the default error configuration, or you can construct a custom error configuration. In the custom error configuration, you specify how the task handles errors and the error conditions. For example, you can specify that the task should stop running when the fourth error occurs, or specify how the task should handle **Null** key values. The custom error configuration can also include the path of an error log.  
  
> [!NOTE]  
>  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task can process only analytic objects created by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools.  
  
 This task is frequently used in combination with a Bulk Insert task that loads data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, or a Data Flow task that implements a data flow that loads data into a table. For example, the Data Flow task might have a data flow that extracts data from an online transactional database (OLTP) database and loads it into a fact table in a data warehouse, after which the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task is called to process the cube built on the data warehouse.  
  
 The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task uses an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager to connect to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information, see [Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md).  
  
## Error Handling  
  
## Configuration of the Analysis Services Processing Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For more information about setting these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Programmatic Configuration of the Analysis Services Processing Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.DataTransformationServices.Tasks.DTSProcessingTask.DTSProcessingTask>  
  
## Analysis Services Processing Task Editor (General Page)
  Use the **General** pageof the **Analysis Services Processing Task Editor** dialog box to name and describe the Analysis Services Processing task.  
  
### Options  
 **Name**  
 Provide a unique name for the Analysis Services Processing task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Analysis Services Processing task.  
  
## Analysis Services Processing Task Editor (Analysis Services Page)
  Use the **Analysis Services** page of the **Analysis Services Processing Task Editor** dialog box to specify an Analysis Services connection manager, select the analytic objects to process, and set processing and error handling options.  
  
 When processing tabular models, keep the following in mind:  
  
1.  You cannot perform impact analysis on tabular models.  
  
2.  Some processing options for tabular mode are not exposed, such as Process Defrag and Process Recalc. You can perform these functions by using the Execute DDL task.  
  
3.  Some processing options provided, such as process indexes, are not appropriate for tabular models and should not be used.  
  
4.  Batch settings are ignored for tabular models.  
  
### Options  
 **Analysis Services connection manager**  
 Select an existing Analysis Services connection manager in the list or click **New** to create a new connection manager.  
  
 **New**  
 Create a new Analysis Services connection manager.  
  
 **Related Topics:** [Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md), [Add Analysis Services Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)  
  
 **Object list**  
 |Property|Description|  
|--------------|-----------------|  
|**Object Name**|Lists the specified object names.|  
|**Type**|Lists the types of the specified objects.|  
|**Process Options**|Select a processing option in the list.<br /><br /> **Related Topics**: [Processing a multidimensional model &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)|  
|**Settings**|Lists processing settings for the specified objects.|  
  
 **Add**  
 Add an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object to the list.  
  
 **Remove**  
 Select an object, and then click **Delete**.  
  
 **Impact Analysis**  
 Perform impact analysis on the selected object.  
  
 **Related Topics:** [Impact Analysis Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](https://msdn.microsoft.com/library/208268eb-4e14-44db-9c64-6f74b776adb6)  
  
 **Batch Settings Summary**  
 |Property|Description|  
|--------------|-----------------|  
|**Processing order**|Specifies whether objects are processed sequentially or in a batch; if parallel processing is used, specifies the number of objects to process concurrently.|  
|**Transaction mode**|Specifies the transaction mode for sequential processing.|  
|**Dimension errors**|Specifies the task behavior when errors occur.|  
|**Dimension key error log path**|Specifies the path of the file to which errors are logged.|  
|**Process affected objects**|Indicates whether dependent or affected objects are also processed.|  
  
 **Change Settings**  
 Change the processing options and the handling of errors in dimension keys.  
  
 **Related Topics:** [Change Settings Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](https://msdn.microsoft.com/library/0041e042-d7ce-48f9-a690-a6dc65471ff3)  
  
