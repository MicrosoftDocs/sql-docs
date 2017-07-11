---
title: "Conditional Split Transformation | Microsoft Docs"
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
  - "sql13.dts.designer.conditionalsplittrans.f1"
helpviewer_keywords: 
  - "Conditional Split transformation"
  - "route rows to different outputs [Integration Services]"
ms.assetid: 3f8b5825-226f-413c-ba8f-0bb931ca3770
caps.latest.revision: 51
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Conditional Split Transformation
  The Conditional Split transformation can route data rows to different outputs depending on the content of the data. The implementation of the Conditional Split transformation is similar to a CASE decision structure in a programming language. The transformation evaluates expressions, and based on the results, directs the data row to the specified output. This transformation also provides a default output, so that if a row matches no expression it is directed to the default output.  
  
## Configuration of the Conditional Split Transformation  
 You can configure the Conditional Split transformation in the following ways:  
  
-   Provide an expression that evaluates to a Boolean for each condition you want the transformation to test.  
  
-   Specify the order in which the conditions are evaluated. Order is significant, because a row is sent to the output corresponding to the first condition that evaluates to true.  
  
-   Specify the default output for the transformation. The transformation requires that a default output be specified.  
  
 Each input row can be sent to only one output, that being the output for the first condition that evaluates to true. For example, the following conditions direct any rows in the **FirstName** column that begin with the letter *A* to one output, rows that begin with the letter *B* to a different output, and all other rows to the default output.  
  
 Output 1  
  
 `SUBSTRING(FirstName,1,1) == "A"`  
  
 Output 2  
  
 `SUBSTRING(FirstName,1,1) == "B"`  
  
 [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes functions and operators that you can use to create the expressions that evaluate input data and direct output data. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md).  
  
 The Conditional Split transformation includes the **FriendlyExpression** custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Use Property Expressions in Packages](../../../integration-services/expressions/use-property-expressions-in-packages.md) and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
 This transformation has one input, one or more outputs, and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Conditional Split Transformation Editor** dialog box, see [Conditional Split Transformation Editor](../../../integration-services/data-flow/transformations/conditional-split-transformation-editor.md).  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Split a Dataset by Using the Conditional Split Transformation](../../../integration-services/data-flow/transformations/split-a-dataset-by-using-the-conditional-split-transformation.md)  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Related Tasks  
 [Split a Dataset by Using the Conditional Split Transformation](../../../integration-services/data-flow/transformations/split-a-dataset-by-using-the-conditional-split-transformation.md)  
  
## See Also  
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  