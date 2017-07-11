---
title: "Multicast Transformation | Microsoft Docs"
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
  - "sql13.dts.designer.multicasttrans.f1"
helpviewer_keywords: 
  - "multiple outputs"
  - "Multicast transformation"
  - "datasets [Integration Services], multiple outputs"
  - "multiple transformations"
ms.assetid: 32194784-1684-40cd-9f91-1aba4d8360d3
caps.latest.revision: 45
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Multicast Transformation
  The Multicast transformation distributes its input to one or more outputs. This transformation is similar to the Conditional Split transformation. Both transformations direct an input to multiple outputs. The difference between the two is that the Multicast transformation directs every row to every output, and the Conditional Split directs a row to a single output. For more information, see [Conditional Split Transformation](../../../integration-services/data-flow/transformations/conditional-split-transformation.md).  
  
 You configure the Multicast transformation by adding outputs.  
  
 Using the Multicast transformation, a package can create logical copies of data. This capability is useful when the package needs to apply multiple sets of transformations to the same data. For example, one copy of the data is aggregated and only the summary information is loaded into its destination, while another copy of the data is extended with lookup values and derived columns before it is loaded into its destination.  
  
 This transformation has one input and multiple outputs. It does not support an error output.  
  
## Configuration of the Multicast Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in the **Multicast Transformation Editor** dialog box, see [Multicast Transformation Editor](../../../integration-services/data-flow/transformations/multicast-transformation-editor.md).  
  
 For information about the properties that you can set programmatically, see [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796).  
  
## Related Tasks  
 For information about how to set properties of this component, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## See Also  
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  