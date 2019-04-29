---
title: "Excel Source Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.excelsourceadapter.columns.f1"
helpviewer_keywords: 
  - "Excel Source Editor"
ms.assetid: 50024686-a18d-4824-b20e-c84123f89d0b
author: janinezhang
ms.author: janinez
manager: craigg
---
# Excel Source Editor (Columns Page)
  Use the **Columns** page of the **Excel Source Editor** dialog box to map an output column to each external (source) column.  
  
 To learn more about the Excel source, see [Excel Source](data-flow/excel-source.md).  
  
## Options  
 **Available External Columns**  
 View the list of available external columns in the data source. You cannot use this table to add or delete columns.  
  
 **External Column**  
 View external (source) columns in the order in which the task will read them. You can change this order by first clearing the selected columns in the table discussed above, and then selecting external columns from the list in a different order.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name provided will be displayed within [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Excel Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/excel-source-editor-connection-manager-page.md)   
 [Excel Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/excel-source-editor-error-output-page.md)   
 [Excel Connection Manager](connection-manager/excel-connection-manager.md)   
 [Loop through Excel Files and Tables by Using a Foreach Loop Container](control-flow/foreach-loop-container.md)  
  
  
