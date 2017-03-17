---
title: "Analysis Services Processing Task Editor (Analysis Services Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.asprocessingtask.as.f1"
helpviewer_keywords: 
  - "Analysis Services Processing Task Editor"
ms.assetid: 5612be78-57cf-4e4e-92cf-6bfa9f971040
caps.latest.revision: 29
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Analysis Services Processing Task Editor (Analysis Services Page)
  Use the **Analysis Services** page of the **Analysis Services Processing Task Editor** dialog box to specify an Analysis Services connection manager, select the analytic objects to process, and set processing and error handling options.  
  
 When processing tabular models, keep the following in mind:  
  
1.  You cannot perform impact analysis on tabular models.  
  
2.  Some processing options for tabular mode are not exposed, such as Process Defrag and Process Recalc. You can perform these functions by using the Execute DDL task.  
  
3.  Some processing options provided, such as process indexes, are not appropriate for tabular models and should not be used.  
  
4.  Batch settings are ignored for tabular models.  
  
 To learn about this task, see [Analysis Services Processing Task](../../integration-services/control-flow/analysis-services-processing-task.md).  
  
## Options  
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
  
 **Related Topics:** [Impact Analysis Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/208268eb-4e14-44db-9c64-6f74b776adb6)  
  
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
  
 **Related Topics:** [Change Settings Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/0041e042-d7ce-48f9-a690-a6dc65471ff3)  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Analysis Services Processing Task Editor &#40;General Page&#41;](../../integration-services/control-flow/analysis-services-processing-task-editor-general-page.md)   
 [Analysis Services Execute DDL Task](../../integration-services/control-flow/analysis-services-execute-ddl-task.md)  
  
  