---
title: "Lookup Transformation Editor (Advanced Page) | Microsoft Docs"
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
  - "sql13.dts.designer.lookuptransformation.advanced.f1"
helpviewer_keywords: 
  - "Lookup Transformation Editor"
ms.assetid: f3395c65-0320-47f9-8d83-daaa082d8713
caps.latest.revision: 42
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Lookup Transformation Editor (Advanced Page)
  Use the **Advanced** page of the **Lookup Transformation Editor** dialog box to configure partial caching and to modify the SQL statement for the Lookup transformation.  
  
 To learn more about the Lookup transformation, see [Lookup Transformation](../../../integration-services/data-flow/transformations/lookup-transformation.md).  
  
## Options  
 **Cache size (32-bit)**  
 Adjust the  cache size (in megabytes) for 32-bit computers. The default value is 5 megabytes.  
  
 **Cache size (64-bit)**  
 Adjust the cache size (in megabytes) for 64-bit computers. The default value is 5 megabytes.  
  
 **Enable cache for rows with no matching entries**  
 Cache rows with no matching entries in the reference dataset.  
  
 **Allocation from cache**  
 Specify the percentage of the cache to allocate for rows with no matching entries in the reference dataset.  
  
 **Modify the SQL statement**  
 Modify the SQL statement that is used to generate the reference dataset.  
  
> [!NOTE]  
>  The optional SQL statement that you specify on this page overrides and replaces the table name that you specified on the **Connection** page of the **Lookup Transformation Editor**. For more information, see [Lookup Transformation Editor &#40;Connection Page&#41;](../../../integration-services/data-flow/transformations/lookup-transformation-editor-connection-page.md).  
  
 **Set Parameters**  
 Map input columns to parameters by using the **Set Query Parameters** dialog box.  
  
## External Resources  
 Blog entry, [Lookup cache modes](http://go.microsoft.com/fwlink/?LinkId=219518) on blogs.msdn.com  
  
## See Also  
 [Lookup Transformation Editor &#40;General Page&#41;](../../../integration-services/data-flow/transformations/lookup-transformation-editor-general-page.md)   
 [Lookup Transformation Editor &#40;Connection Page&#41;](../../../integration-services/data-flow/transformations/lookup-transformation-editor-connection-page.md)   
 [Lookup Transformation Editor &#40;Columns Page&#41;](../../../integration-services/data-flow/transformations/lookup-transformation-editor-columns-page.md)   
 [Lookup Transformation Editor &#40;Error Output Page&#41;](../../../integration-services/data-flow/transformations/lookup-transformation-editor-error-output-page.md)   
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Fuzzy Lookup Transformation](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation.md)  
  
  