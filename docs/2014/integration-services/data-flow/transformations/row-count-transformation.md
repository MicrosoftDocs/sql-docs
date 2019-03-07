---
title: "Row Count Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.rowcounttrans.f1"
helpviewer_keywords: 
  - "updating variables"
  - "Row Count transformation"
  - "number of rows"
  - "variables [Integration Services], updating"
  - "counting rows"
ms.assetid: b68293b9-a68c-40be-9d81-77342da1be29
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Row Count Transformation
  The Row Count transformation counts rows as they pass through a data flow and stores the final count in a variable.  
  
 A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] package can use row counts to update the variables used in scripts, expressions, and property expressions. (For example, the variable that stores the row count can update the message text in an e-mail message to include the number of rows.) The variable that the Row Count transformation uses must already exist, and it must be in the scope of the Data Flow task to which the data flow with the Row Count transformation belongs.  
  
 The transformation stores the row count value in the variable only after the last row has passed through the transformation. Therefore, the value of the variable is not updated in time to use the updated value in the data flow that contains the Row Count transformation. You can use the updated variable in a separate data flow.  
  
 This transformation has one input and one output. It does not support an error output.  
  
## Configuration of the Row Count Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
## Related Tasks  
 For information about how to set the properties of this component, see [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md).  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](../../integration-services-ssis-variables.md)   
 [Data Flow](../data-flow.md)   
 [Integration Services Transformations](integration-services-transformations.md)  
  
  
