---
title: "Conditional Split Transformation Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.conditionalsplittransformation.f1"
helpviewer_keywords: 
  - "Conditional Split Transformation Editor"
ms.assetid: c30e1633-537a-4837-9991-6203c6f2a21e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Conditional Split Transformation Editor
  Use the **Conditional Split Transformation Editor** dialog box to create expressions, set the order in which expressions are evaluated, and name the outputs of a conditional split. This dialog box includes mathematical, string, and date/time functions and operators that you can use to build expressions. The first condition that evaluates as true determines the output to which a row is directed.  
  
> [!NOTE]  
>  The Conditional Split transformation directs each input row to one output only. If you enter multiple conditions, the transformation sends each row to the first output for which the condition is true and disregards subsequent conditions for that row. If you need to evaluate several conditions successively, you may need to concatenate multiple Conditional Split transformations in the data flow.  
  
 To learn more about the Conditional Split transformation, see [Conditional Split Transformation](data-flow/transformations/conditional-split-transformation.md).  
  
## Options  
 **Order**  
 Select a row and use the arrow keys at right to change the order in which to evaluate expressions.  
  
 **Output Name**  
 Provide an output name. The default is a numbered list of cases; however, you can choose any unique, descriptive name.  
  
 **Condition**  
 Type an expression or build one by dragging from the list of available columns, variables, functions, and operators.  
  
 The value of this property can be specified by using a property expression.  
  
 **Related topics:**  [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md), [Operators &#40;SSIS Expression&#41;](expressions/operators-ssis-expression.md), and [Functions &#40;SSIS Expression&#41;](expressions/functions-ssis-expression.md)  
  
 **Default output name**  
 Type a name for the default output, or use the default.  
  
 **Configure error output**  
 Specify how to handle errors by using the [Configure Error Output](../../2014/integration-services/configure-error-output.md) dialog box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Split a Dataset by Using the Conditional Split Transformation](data-flow/transformations/split-a-dataset-by-using-the-conditional-split-transformation.md)  
  
  
