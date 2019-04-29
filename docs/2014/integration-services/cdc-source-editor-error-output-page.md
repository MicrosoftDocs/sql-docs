---
title: "CDC Source Editor (Error Output Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.cdcsource.errorhandling.f1"
ms.assetid: 8a4c2cb8-fd2f-4c45-824f-b93473a8981e
author: janinezhang
ms.author: janinez
manager: craigg
---
# CDC Source Editor (Error Output Page)
  Use the **Error Output** page of the **CDC Source Editor** dialog box to select error handling options.  
  
 To learn more about the CDC source, see [CDC Source](data-flow/cdc-source.md).  
  
## Task List  
 **To open the CDC Source Editor Error Output Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the CDC source.  
  
2.  On the **Data Flow** tab, double-click the CDC source.  
  
3.  In the **CDC Source Editor**, click **Error Output**.  
  
## Options  
 **Input/Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **CDC Source Editor** dialog box.  
  
 **Error**  
 Select how the CDC source should handle errors in a flow: ignore the failure, redirect the row, or fail the component.  
  
 **Truncation**  
 Select how the CDC source should handle truncation in a flow: ignore the failure, redirect the row, or fail the component.  
  
 **Description**  
 Not used.  
  
 **Set this value to selected cells**  
 Select how the CDC source handles all selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling options to the selected cells.  
  
## Error Handling Options  
 You use the following options to configure how the CDC source handles errors and truncations.  
  
 **Fail Component**  
 The Data Flow task fails when an error or a truncation occurs. This is the default behavior.  
  
 **Ignore Failure**  
 The error or the truncation is ignored and the data row is directed to the CDC source output.  
  
 **Redirect Flow**  
 The error or the truncation data row is directed to the error output of the CDC source. In this case the CDC source error handling is used. For more information, see [CDC Source](data-flow/cdc-source.md).  
  
## See Also  
 [CDC Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/cdc-source-editor-connection-manager-page.md)   
 [CDC Source Editor &#40;Columns Page&#41;](../../2014/integration-services/cdc-source-editor-columns-page.md)  
  
  
