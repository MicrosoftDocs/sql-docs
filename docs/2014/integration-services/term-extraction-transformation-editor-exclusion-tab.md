---
title: "Term Extraction Transformation Editor (Exclusion Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.termextraction.inclusionexclusion.f1"
helpviewer_keywords: 
  - "Term Extraction Transformation Editor"
ms.assetid: 90110d95-fd97-4542-9cda-832c86606130
author: janinezhang
ms.author: janinez
manager: craigg
---
# Term Extraction Transformation Editor (Exclusion Tab)
  Use the **Exclusion** tab of the **Term Extraction Transformation Editor** dialog box to set up a connection to an exclusion table and specify the columns that contain exclusion terms.  
  
 To learn more about the Term Extraction transformation, see [Term Extraction Transformation](data-flow/transformations/term-extraction-transformation.md).  
  
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
 Use the [Configure Error Output](../../2014/integration-services/configure-error-output.md) dialog box to specify error handling for rows that cause errors.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Term Extraction Transformation Editor &#40;Term Extraction Tab&#41;](../../2014/integration-services/term-extraction-transformation-editor-term-extraction-tab.md)   
 [Term Extraction Transformation Editor &#40;Advanced Tab&#41;](../../2014/integration-services/term-extraction-transformation-editor-advanced-tab.md)   
 [Term Lookup Transformation](data-flow/transformations/lookup-transformation.md)  
  
  
