---
title: "Term Extraction Transformation Editor (Advanced Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.termextraction.advanced.f1"
helpviewer_keywords: 
  - "Term Extraction Transformation Editor"
ms.assetid: 87118281-6e3c-499e-bac4-fa4c24bb12c6
author: janinezhang
ms.author: janinez
manager: craigg
---
# Term Extraction Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Term Extraction Transformation Editor** dialog box to specify properties for the extraction such as frequency, length, and whether to extract words or phrases.  
  
 To learn more about the Term Extraction transformation, see [Term Extraction Transformation](data-flow/transformations/term-extraction-transformation.md).  
  
## Options  
 **Noun**  
 Specify that the transformation extracts individual nouns only.  
  
 **Noun phrase**  
 Specify that the transformation extracts noun phrases only.  
  
 **Noun and noun phrase**  
 Specify that the transformation extracts both nouns and noun phrases.  
  
 **Frequency**  
 Specify that the score is the frequency of the term.  
  
 **TFIDF**  
 Specify that the score is the TFIDF value of the term. The TFIDF score is the product of Term Frequency and Inverse Document Frequency, defined as: TFIDF of a Term T = (frequency of T) * log( (#rows in Input) / (#rows having T) )  
  
 **Frequency threshold**  
 Specify the number of times a word or phrase must occur before extracting it. The default value is 2.  
  
 **Maximum length of term**  
 Specify the maximum length of a phrase in words. This option affects noun phrases only. The default value is 12.  
  
 **Use case-sensitive term extraction**  
 Specify whether to make the extraction case-sensitive. The default is `False`.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](../../2014/integration-services/configure-error-output.md) dialog box to specify error handling for rows that cause errors.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Term Extraction Transformation Editor &#40;Term Extraction Tab&#41;](../../2014/integration-services/term-extraction-transformation-editor-term-extraction-tab.md)   
 [Term Extraction Transformation Editor &#40;Exclusion Tab&#41;](../../2014/integration-services/term-extraction-transformation-editor-exclusion-tab.md)   
 [Term Lookup Transformation](data-flow/transformations/lookup-transformation.md)  
  
  
