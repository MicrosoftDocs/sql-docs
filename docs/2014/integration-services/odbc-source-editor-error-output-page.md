---
title: "ODBC Source Editor (Error Output Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.odbcsource.errorhandling.f1"
ms.assetid: b2f6866c-db07-4cb3-9f38-889f8d2b03e6
author: janinezhang
ms.author: janinez
manager: craigg
---
# ODBC Source Editor (Error Output Page)
  Use the **Error Output** page of the **ODBC Source Editor** dialog box to select error handling options.  
  
 To learn more about the ODBC source, see [CDC Source](data-flow/cdc-source.md).  
  
## Task List  
 **To open the ODBC Source Editor Error Output Page**  
  
-   In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the ODBC source.  
  
-   On the **Data Flow** tab, double-click the ODBC source.  
  
-   In the **ODBC Source Editor**, click **Error Output**.  
  
## Options  
  
### Input/Output  
 View the name of the data source.  
  
### Column  
 Not used.  
  
### Error  
 Select how the ODBC source should handle errors in a flow: ignore the failure, redirect the row, or fail the component.  
  
### Truncation  
 Select how the ODBC source should handle truncation in a flow: ignore the failure, redirect the row, or fail the component.  
  
### Description  
 Not used.  
  
### Set this value to selected cells  
 Select how the ODBC source handles all selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
### Apply  
 Apply the error handling options to the selected cells.  
  
## Error Handling Options  
 You use the following options to configure how the ODBC source handles errors and truncations.  
  
### Fail Component  
 The Data Flow task fails when an error or a truncation occurs. This is the default behavior.  
  
### Ignore Failure  
 The error or the truncation is ignored.  
  
### Redirect Flow  
 The row that is causing the error or the truncation is directed to the error output of the ODBC source. For more information, see [ODBC Source](data-flow/odbc-source.md).  
  
## See Also  
 [ODBC Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/odbc-source-editor-connection-manager-page.md)   
 [ODBC Source Editor &#40;Columns Page&#41;](../../2014/integration-services/odbc-source-editor-columns-page.md)  
  
  
