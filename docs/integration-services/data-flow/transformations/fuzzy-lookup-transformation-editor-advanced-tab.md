---
title: "Fuzzy Lookup Transformation Editor (Advanced Tab) | Microsoft Docs"
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
  - "sql13.dts.designer.fuzzylookuptransformation.advanced.f1"
helpviewer_keywords: 
  - "Fuzzy Lookup Transformation Editor"
ms.assetid: 0a2919be-2ea7-4c06-82b8-0ffad5f0dd83
caps.latest.revision: 27
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Fuzzy Lookup Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Fuzzy Lookup Transformation Editor** dialog box to set parameters for the fuzzy lookup.  
  
 To learn more about the Fuzzy Lookup transformation, see [Fuzzy Lookup Transformation](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation.md).  
  
## Options  
 **Maximum number of matches to output per lookup**  
 Specify the maximum number of matches the transformation can return for each input row. The default is **1**.  
  
 **Similarity threshold**  
 Set the similarity threshold at the component level by using the slider. The closer the value is to 1, the closer the resemblance of the lookup value to the source value must be to qualify as a match. Increasing the threshold can improve the speed of matching since fewer candidate records need to be considered.  
  
 **Token delimiters**  
 Specify the delimiters that the transformation uses to tokenize column values.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Fuzzy Lookup Transformation Editor &#40;Reference Table Tab&#41;](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation-editor-reference-table-tab.md)   
 [Fuzzy Lookup Transformation Editor &#40;Columns Tab&#41;](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation-editor-columns-tab.md)  
  
  