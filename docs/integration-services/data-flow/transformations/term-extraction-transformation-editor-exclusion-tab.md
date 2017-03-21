---
title: "Term Extraction Transformation Editor (Exclusion Tab) | Microsoft Docs"
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
  - "sql13.dts.designer.termextraction.inclusionexclusion.f1"
helpviewer_keywords: 
  - "Term Extraction Transformation Editor"
ms.assetid: 90110d95-fd97-4542-9cda-832c86606130
caps.latest.revision: 28
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Term Extraction Transformation Editor (Exclusion Tab)
  Use the **Exclusion** tab of the **Term Extraction Transformation Editor** dialog box to set up a connection to an exclusion table and specify the columns that contain exclusion terms.  
  
 To learn more about the Term Extraction transformation, see [Term Extraction Transformation](../../../integration-services/data-flow/transformations/term-extraction-transformation.md).  
  
## Options  
 **Use exclusion terms**  
 Indicate whether to exclude specific terms during term extraction by specifying a column that contains exclusion terms. You must specify the following source properties if you choose to exclude terms.  
  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection to a database by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Table or view**  
 Select the table or view that contains the exclusion terms.  
  
 **Column**  
 Select the column in the table or view that contains the exclusion terms.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](http://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box to specify error handling for rows that cause errors.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Term Extraction Transformation Editor &#40;Term Extraction Tab&#41;](../../../integration-services/data-flow/transformations/term-extraction-transformation-editor-term-extraction-tab.md)   
 [Term Extraction Transformation Editor &#40;Advanced Tab&#41;](../../../integration-services/data-flow/transformations/term-extraction-transformation-editor-advanced-tab.md)   
 [Term Lookup Transformation](../../../integration-services/data-flow/transformations/term-lookup-transformation.md)  
  
  