---
title: "Conditional Split Transformation Editor | Microsoft Docs"
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
  - "sql13.dts.designer.conditionalsplittransformation.f1"
helpviewer_keywords: 
  - "Conditional Split Transformation Editor"
ms.assetid: c30e1633-537a-4837-9991-6203c6f2a21e
caps.latest.revision: 33
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Conditional Split Transformation Editor
  Use the **Conditional Split Transformation Editor** dialog box to create expressions, set the order in which expressions are evaluated, and name the outputs of a conditional split. This dialog box includes mathematical, string, and date/time functions and operators that you can use to build expressions. The first condition that evaluates as true determines the output to which a row is directed.  
  
> [!NOTE]  
>  The Conditional Split transformation directs each input row to one output only. If you enter multiple conditions, the transformation sends each row to the first output for which the condition is true and disregards subsequent conditions for that row. If you need to evaluate several conditions successively, you may need to concatenate multiple Conditional Split transformations in the data flow.  
  
 To learn more about the Conditional Split transformation, see [Conditional Split Transformation](../../../integration-services/data-flow/transformations/conditional-split-transformation.md).  
  
## Options  
 **Order**  
 Select a row and use the arrow keys at right to change the order in which to evaluate expressions.  
  
 **Output Name**  
 Provide an output name. The default is a numbered list of cases; however, you can choose any unique, descriptive name.  
  
 **Condition**  
 Type an expression or build one by dragging from the list of available columns, variables, functions, and operators.  
  
 The value of this property can be specified by using a property expression.  
  
 **Related topics:**  [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md), [Operators &#40;SSIS Expression&#41;](../../../integration-services/expressions/operators-ssis-expression.md), and [Functions &#40;SSIS Expression&#41;](../../../integration-services/expressions/functions-ssis-expression.md)  
  
 **Default output name**  
 Type a name for the default output, or use the default.  
  
 **Configure error output**  
 Specify how to handle errors by using the [Configure Error Output](http://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Split a Dataset by Using the Conditional Split Transformation](../../../integration-services/data-flow/transformations/split-a-dataset-by-using-the-conditional-split-transformation.md)  
  
  