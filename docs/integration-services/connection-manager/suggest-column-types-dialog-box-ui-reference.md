---
description: "Suggest Column Types Dialog Box UI Reference"
title: "Suggest Column Types Dialog Box UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.suggestdatatypes.f1"
ms.assetid: 8d5652e0-cf57-483f-828b-10f00c08418b
author: chugugrace
ms.author: chugu
---
# Suggest Column Types Dialog Box UI Reference

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Suggest Column Types** dialog box to identify the data type and length of columns in a Flat File Connection Manager based on a sampling of the file content.  
  
 To learn more about the data types used by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Options  
 **Number of rows**  
 Type or select the number of rows in the sample that the algorithm uses.  
  
 **Suggest the smallest integer data type**  
 Clear this check box to skip the assessment. If selected, determines the smallest possible integer data type for columns that contain integral numeric data.  
  
 **Suggest the smallest real data type**  
 Clear this check box to skip the assessment. If selected, determines whether columns that contain real numeric data can use the smaller real data type, DT_R4.  
  
 **Identify Boolean columns using the following values**  
 Type the two values that you want to use as the Boolean values true and false. The values must be separated by a comma, and the first value represents True.  
  
 **Pad string columns**  
 Select this check box to enable string padding.  
  
 **Percent padding**  
 Type or select the percentage of the column lengths by which to increase the length of columns for character data types. The percentage must be an integer.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md)  
  
  
