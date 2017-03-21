---
title: "Term Extraction Transformation Editor (Advanced Tab) | Microsoft Docs"
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
  - "sql13.dts.designer.termextraction.advanced.f1"
helpviewer_keywords: 
  - "Term Extraction Transformation Editor"
ms.assetid: 87118281-6e3c-499e-bac4-fa4c24bb12c6
caps.latest.revision: 29
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Term Extraction Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Term Extraction Transformation Editor** dialog box to specify properties for the extraction such as frequency, length, and whether to extract words or phrases.  
  
 To learn more about the Term Extraction transformation, see [Term Extraction Transformation](../../../integration-services/data-flow/transformations/term-extraction-transformation.md).  
  
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
 Specify whether to make the extraction case-sensitive. The default is **False**.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](http://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box to specify error handling for rows that cause errors.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Term Extraction Transformation Editor &#40;Term Extraction Tab&#41;](../../../integration-services/data-flow/transformations/term-extraction-transformation-editor-term-extraction-tab.md)   
 [Term Extraction Transformation Editor &#40;Exclusion Tab&#41;](../../../integration-services/data-flow/transformations/term-extraction-transformation-editor-exclusion-tab.md)   
 [Term Lookup Transformation](../../../integration-services/data-flow/transformations/term-lookup-transformation.md)  
  
  