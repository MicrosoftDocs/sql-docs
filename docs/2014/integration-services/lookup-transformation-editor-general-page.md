---
title: "Lookup Transformation Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.lookuptransformation.general.f1"
ms.assetid: 4bd15e48-0feb-4f95-be91-5df58105dbfb
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Lookup Transformation Editor (General Page)
  Use the **General** page of the Lookup Transformation Editor dialog box to select the cache mode, select the connection type, and specify how to handle rows with no matching entries.  
  
 To learn more about the Lookup transformation, see [Lookup Transformation](data-flow/transformations/lookup-transformation.md).  
  
## Options  
 **Full cache**  
 Generate and load the reference dataset into cache before the Lookup transformation is executed.  
  
 **Partial cache**  
 Generate the reference dataset during the execution of the Lookup transformation. Load the rows with matching entries in the reference dataset and the rows with no matching entries in the dataset into cache.  
  
 **No cache**  
 Generate the reference dataset during the execution of the Lookup transformation. No data is loaded into cache.  
  
 **Cache connection manager**  
 Configure the Lookup transformation to use a Cache connection manager. This option is available only if the Full cache option is selected.  
  
 **OLE DB connection manager**  
 Configure the Lookup transformation to use an OLE DB connection manager.  
  
 **Specify how to handle rows with no matching entries**  
 Select an option for handling rows that do not match at least one entry in the reference dataset.  
  
 When you select **Redirect rows to no match output**, the rows are redirected to a no match output and are not handled as errors. The **Error** option on the **Error Output** page of the **Lookup Transformation Editor** dialog box is not available.  
  
 When you select any other option in the **Specify how to handle rows with no matching entries** list box, the rows are handled as errors. The **Error** option on the **Error Output** page is available.  
  
## External Resources  
 Blog entry, [Lookup cache modes](https://go.microsoft.com/fwlink/?LinkId=219518) on blogs.msdn.com  
  
## See Also  
 [Cache Connection Manager](connection-manager/cache-connection-manager.md)   
 [Lookup Transformation Editor &#40;Connection Page&#41;](../../2014/integration-services/lookup-transformation-editor-connection-page.md)   
 [Lookup Transformation Editor &#40;Columns Page&#41;](../../2014/integration-services/lookup-transformation-editor-columns-page.md)   
 [Lookup Transformation Editor &#40;Advanced Page&#41;](../../2014/integration-services/lookup-transformation-editor-advanced-page.md)   
 [Lookup Transformation Editor &#40;Error Output Page&#41;](../../2014/integration-services/lookup-transformation-editor-error-output-page.md)  
  
  
