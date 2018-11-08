---
title: "Flat File Source Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.flatfilesourceadapter.columns.f1"
helpviewer_keywords: 
  - "Flat File Source Editor"
ms.assetid: b5af5f65-c087-44fd-b5ae-d0441245fef2
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Flat File Source Editor (Columns Page)
  Use the **Columns** node of the **Flat File Source Editor** dialog box to map an output column to each external (source) column.  
  
> [!NOTE]  
>  The `FileNameColumnName` property of the Flat File source and the `FastParse` property of its output columns are not available in the **Flat File Source Editor**, but can be set by using the **Advanced Editor**. For more information on these properties, see the Flat File Source section of [Flat File Custom Properties](data-flow/flat-file-custom-properties.md).  
  
 To learn more about the Flat File source, see [Flat File Source](data-flow/flat-file-source.md).  
  
## Options  
 **Available External Columns**  
 View the list of available external columns in the data source. You cannot use this table to add or delete columns.  
  
 **External Column**  
 View external (source) columns in the order in which the task will read them. You can change this order by first clearing the selected columns in the table, and then selecting external columns from the list in a different order.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name provided will be displayed within [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Flat File Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/flat-file-source-editor-connection-manager-page.md)   
 [Flat File Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/flat-file-source-editor-error-output-page.md)   
 [Flat File Connection Manager](connection-manager/file-connection-manager.md)  
  
  
