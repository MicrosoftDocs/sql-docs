---
title: "Analysis Services Processing Task Editor (Analysis Services Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.asprocessingtask.as.f1"
helpviewer_keywords: 
  - "Analysis Services Processing Task Editor"
ms.assetid: 5612be78-57cf-4e4e-92cf-6bfa9f971040
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Analysis Services Processing Task Editor (Analysis Services Page)
  Use the **Analysis Services** page of the **Analysis Services Processing Task Editor** dialog box to specify an Analysis Services connection manager, select the analytic objects to process, and set processing and error handling options.  
  
 When processing tabular models, keep the following in mind:  
  
1.  You cannot perform impact analysis on tabular models.  
  
2.  Some processing options for tabular mode are not exposed, such as Process Defrag and Process Recalc. You can perform these functions by using the Execute DDL task.  
  
3.  Some processing options provided, such as process indexes, are not appropriate for tabular models and should not be used.  
  
4.  Batch settings are ignored for tabular models.  
  
 To learn about this task, see [Analysis Services Processing Task](control-flow/analysis-services-processing-task.md).  
  
## Options  
 **Analysis Services connection manager**  
 Select an existing Analysis Services connection manager in the list or click **New** to create a new connection manager.  
  
 **New**  
 Create a new Analysis Services connection manager.  
  
 **Related Topics:** [Analysis Services Connection Manager](connection-manager/analysis-services-connection-manager.md), [Add Analysis Services Connection Manager Dialog Box UI Reference](connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)  
  
 **Object list**  
 |Property|Description|  
|--------------|-----------------|  
|**Object Name**|Lists the specified object names.|  
|**Type**|Lists the types of the specified objects.|  
|**Process Options**|Select a processing option in the list.<br /><br /> **Related Topics**: [Multidimensional Model Object Processing](../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)|  
|**Settings**|Lists processing settings for the specified objects.|  
  
 **Add**  
 Add an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] object to the list.  
  
 **Remove**  
 Select an object, and then click **Delete**.  
  
 **Impact Analysis**  
 Perform impact analysis on the selected object.  
  
 **Related Topics:** [Impact Analysis Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](../../2014/analysis-services/impact-analysis-dialog-box-analysis-services-multidimensional-data.md)  
  
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
  
 **Related Topics:** [Change Settings Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](../../2014/analysis-services/change-settings-dialog-box-analysis-services-multidimensional-data.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Analysis Services Processing Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Analysis Services Execute DDL Task](control-flow/analysis-services-execute-ddl-task.md)  
  
  
