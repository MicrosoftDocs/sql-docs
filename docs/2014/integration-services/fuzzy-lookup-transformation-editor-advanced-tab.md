---
title: "Fuzzy Lookup Transformation Editor (Advanced Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fuzzylookuptransformation.advanced.f1"
helpviewer_keywords: 
  - "Fuzzy Lookup Transformation Editor"
ms.assetid: 0a2919be-2ea7-4c06-82b8-0ffad5f0dd83
author: janinezhang
ms.author: janinez
manager: craigg
---
# Fuzzy Lookup Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Fuzzy Lookup Transformation Editor** dialog box to set parameters for the fuzzy lookup.  
  
 To learn more about the Fuzzy Lookup transformation, see [Fuzzy Lookup Transformation](data-flow/transformations/lookup-transformation.md).  
  
## Options  
 **Maximum number of matches to output per lookup**  
 Specify the maximum number of matches the transformation can return for each input row. The default is **1**.  
  
 **Similarity threshold**  
 Set the similarity threshold at the component level by using the slider. The closer the value is to 1, the closer the resemblance of the lookup value to the source value must be to qualify as a match. Increasing the threshold can improve the speed of matching since fewer candidate records need to be considered.  
  
 **Token delimiters**  
 Specify the delimiters that the transformation uses to tokenize column values.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Fuzzy Lookup Transformation Editor &#40;Reference Table Tab&#41;](../../2014/integration-services/fuzzy-lookup-transformation-editor-reference-table-tab.md)   
 [Fuzzy Lookup Transformation Editor &#40;Columns Tab&#41;](../../2014/integration-services/fuzzy-lookup-transformation-editor-columns-tab.md)  
  
  
