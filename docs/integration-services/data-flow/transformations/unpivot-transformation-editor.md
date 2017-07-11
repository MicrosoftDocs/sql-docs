---
title: "Unpivot Transformation Editor | Microsoft Docs"
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
  - "sql13.dts.designer.unpivottransformation.f1"
helpviewer_keywords: 
  - "Unpivot Transformation Editor"
ms.assetid: 72a36ef0-4070-4f6c-9c74-0720109617dd
caps.latest.revision: 35
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Unpivot Transformation Editor
  Use the **Unpivot Transformation Editor** dialog box to select the columns to pivot into rows, and to specify the data column and the new pivot value output column.  
  
> [!NOTE]  
>  This topic relies on the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md) to illustrate the use of the options.  
  
 To learn more about the Unpivot transformation, see [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md).  
  
## Options  
 **Available Input Columns**  
 Using the check boxes, specify the columns to pivot into rows.  
  
 **Name**  
 View the name of the available input column.  
  
 **Pass Through**  
 Indicate whether to include the column in the unpivoted output.  
  
 **Input Column**  
 Select from the list of available input columns for each row. Your selections are reflected in the check box selections in the **Available Input Columns** table.  
  
 In the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md), the Input Columns are the **Ham**, **Soda**, **Milk**, **Beer**, and **Chips** columns.  
  
 **Destination Column**  
 Provide a name for the data column.  
  
 In the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md), the Destination Column is the quantity (**Qty**) column.  
  
 **Pivot Key Value**  
 Provide a name for the pivot value. The default is the name of the input column; however, you can choose any unique, descriptive name.  
  
 The value of this property can be specified by using a property expression.  
  
 In the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md), the Pivot Values will appear in the new Product column designated by the **Pivot Key Value Column Name** option, as the text values **Ham**, **Soda**, **Milk**, **Beer**, and **Chips**.  
  
 **Pivot Key Value Column Name**  
 Provide a name for the pivot value column. The default is "Pivot Key Value"; however, you can choose any unique, descriptive name.  
  
 In the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md), the Pivot Key Value Column Name is **Product** and designates the new **Product** column into which the **Ham**, **Soda**, **Milk**, **Beer**, and **Chips** columns are unpivoted.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Pivot Transformation](../../../integration-services/data-flow/transformations/pivot-transformation.md)  
  
  