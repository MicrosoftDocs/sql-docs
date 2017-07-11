---
title: "Row Sampling Transformation Editor (Sampling Page) | Microsoft Docs"
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
  - "sql13.DTS.DESIGNER.ROWSAMPLINGTRANSFORMATION.COLUMNS.F1"
  - "sql13.dts.designer.rowsamplingtransformation.f1"
helpviewer_keywords: 
  - "Row Sampling Transformation Editor"
ms.assetid: 544c7709-6de0-4c08-bda3-759985be9a05
caps.latest.revision: 23
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Row Sampling Transformation Editor (Sampling Page)
  Use the **Row Sampling Transformation Editor** dialog box to split a portion of an input into a sample using a specified number of rows. This transformation divides the input into two separate outputs.  
  
 To learn more about the Row Sampling transformation, see [Row Sampling Transformation](../../../integration-services/data-flow/transformations/row-sampling-transformation.md).  
  
## Options  
 **Number of rows**  
 Specify the number of rows from the input to use as a sample.  
  
 The value of this property can be specified by using a property expression.  
  
 **Sample output name**  
 Provide a unique name for the output that will include the sampled rows. The name provided will be displayed within SSIS Designer.  
  
 **Unselected output name**  
 Provide a unique name for the output that will contain the rows excluded from the sampling. The name provided will be displayed within SSIS Designer.  
  
 **Use the following random seed**  
 Specify the sampling seed for the random number generator that the transformation uses to create a sample. This is only recommended for development and testing. The transformation uses the Microsoft Windows tick count as a seed if a random seed is not specified.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Percentage Sampling Transformation](../../../integration-services/data-flow/transformations/percentage-sampling-transformation.md)  
  
  