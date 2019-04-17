---
title: "Fuzzy Grouping Transformation Editor (Advanced Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fuzzygroupingtransformation.advanced.f1"
helpviewer_keywords: 
  - "Fuzzy Grouping Transformation Editor"
ms.assetid: dd820d75-b8a7-4515-aea4-3553ba5b442e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Fuzzy Grouping Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Fuzzy Grouping Transformation Editor** dialog box to specify input and output columns, set similarity thresholds, and define delimiters.  
  
> [!NOTE]  
>  The `Exhaustive` and the `MaxMemoryUsage` properties of the Fuzzy Grouping transformation are not available in the **Fuzzy Grouping Transformation Editor**, but can be set by using the **Advanced Editor**. For more information on these properties, see the Fuzzy Grouping Transformation section of [Transformation Custom Properties](data-flow/transformations/transformation-custom-properties.md).  
  
 To learn more about the Fuzzy Grouping transformation, see [Fuzzy Grouping Transformation](data-flow/transformations/fuzzy-grouping-transformation.md).  
  
## Options  
 **Input key column name**  
 Specify the name of an output column that contains the unique identifier for each input row. The `_key_in` column has a value that uniquely identifies each row.  
  
 **Output key column name**  
 Specify the name of an output column that contains the unique identifier for the canonical row of a group of duplicate rows. The `_key_out` column corresponds to the `_key_in` value of the canonical data row.  
  
 **Similarity score column name**  
 Specify a name for the column that contains the similarity score. The similarity score is a value between 0 and 1 that indicates the similarity of the input row to the canonical row. The closer the score is to 1, the more closely the row matches the canonical row.  
  
 **Similarity threshold**  
 Set the similarity threshold by using the slider. The closer the threshold is to 1, the more the rows must resemble each other to qualify as duplicates. Increasing the threshold can improve the speed of matching because fewer candidate records have to be considered.  
  
 **Token delimiters**  
 The transformation provides a default set of delimiters for tokenizing data, but you can add or remove delimiters as needed by editing the list.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Identify Similar Data Rows by Using the Fuzzy Grouping Transformation](data-flow/transformations/identify-similar-data-rows-by-using-the-fuzzy-grouping-transformation.md)  
  
  
