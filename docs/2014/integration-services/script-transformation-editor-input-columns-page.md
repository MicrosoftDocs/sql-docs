---
title: "Script Transformation Editor (Input Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.scriptcomponent.inputcolumn.f1"
helpviewer_keywords: 
  - "Script Transformation Editor"
ms.assetid: d6e4ce84-3335-48e6-82d3-1c359ed87f63
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Script Transformation Editor (Input Columns Page)
  Use the **Input Columns** page of the **Script Transformation Editor** dialog box to set properties on input columns.  
  
> [!NOTE]  
>  The **Input Columns** page is not displayed for Source components, which have outputs but no inputs.  
  
 To learn more about the Script component, see [Script Component](data-flow/transformations/script-component.md) and [Configuring the Script Component in the Script Component Editor](extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md). To learn about programming the Script component, see [Extending the Data Flow with the Script Component](extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md).  
  
## Options  
 **Input name**  
 Select from the list of available inputs.  
  
 **Available Input Columns**  
 Using the check boxes, specify the columns that the script transformation will use.  
  
 **Input Column**  
 Select from the list of available input columns for each row. Your selections are reflected in the check box selections in the **Available Input Columns**table.  
  
 **Output Alias**  
 Type an alias for each output column. The default is the name of the input column; however, you can choose any unique, descriptive name.  
  
 **Usage Type**  
 Specify whether the Script Transformation will treat each column as `ReadOnly` or `ReadWrite`.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Select Script Component Type](../../2014/integration-services/select-script-component-type.md)   
 [Script Transformation Editor &#40;Inputs and Outputs Page&#41;](../../2014/integration-services/script-transformation-editor-inputs-and-outputs-page.md)   
 [Script Transformation Editor &#40;Script Page&#41;](../../2014/integration-services/script-transformation-editor-script-page.md)   
 [Script Transformation Editor &#40;Connection Managers Page&#41;](../../2014/integration-services/script-transformation-editor-connection-managers-page.md)   
 [Additional Script Component Examples](extending-packages-scripting-data-flow-script-component-examples/additional-script-component-examples.md)  
  
  
