---
title: "Percentage Sampling Transformation Editor | Microsoft Docs"
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
  - "sql13.dts.designer.percentagesamplingtransformation.f1"
helpviewer_keywords: 
  - "Percentage Sampling Transformation Editor"
ms.assetid: 2c40d804-26a3-4d35-809b-bc923d83d451
caps.latest.revision: 25
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Percentage Sampling Transformation Editor
  Use the **Percentage Sampling Transformation Editor** dialog box to split part of an input into a sample using a specified percentage of rows. This transformation divides the input into two separate outputs.  
  
 To learn more about the percentage sampling transformation, see [Percentage Sampling Transformation](../../../integration-services/data-flow/transformations/percentage-sampling-transformation.md).  
  
## Options  
 **Percentage of rows**  
 Specify the percentage of rows in the input to use as a sample.  
  
 The value of this property can be specified by using a property expression.  
  
 **Sample output name**  
 Provide a unique name for the output that will include the sampled rows. The name provided will be displayed within the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
 **Unselected output name**  
 Provide a unique name for the output that will contain the rows excluded from the sampling. The name provided will be displayed within the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
 **Use the following random seed**  
 Specify the sampling seed for the random number generator that the transformation uses to create a sample. This is only recommended for development and testing. The transformation uses the Microsoft Windows tick count if a random seed is not specified.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Row Sampling Transformation](../../../integration-services/data-flow/transformations/row-sampling-transformation.md)  
  
  