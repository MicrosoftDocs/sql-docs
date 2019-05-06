---
title: "ODBC Destination Editor (Error Output Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.odbcdest.errorhandling.f1"
ms.assetid: 0a743f8d-2a51-4296-9976-8104f5ca22d3
author: janinezhang
ms.author: janinez
manager: craigg
---
# ODBC Destination Editor (Error Output Page)
  Use the **Error Output** page of the **ODBC Destination Editor** dialog box to select error handling options.  
  
 To learn more about the ODBC destination, see [ODBC Destination](data-flow/odbc-destination.md).  
  
 **To open the ODBC Destination Editor Error Output Page**  
  
## Task List  
  
-   In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the ODBC destination.  
  
-   On the **Data Flow** tab, double-click the ODBC destination.  
  
-   In the **ODBC Destination Editor**, click **Error Output**.  
  
## Options  
  
### Input/Output  
 View the name of the data source.  
  
### Column  
 Not used.  
  
### Error  
 Select how the ODBC destination should handle errors in a flow: ignore the failure, redirect the row, or fail the component.  
  
### Truncation  
 Select how the ODBC destination should handle truncation in a flow: ignore the failure, redirect the row, or fail the component.  
  
### Description  
 View a description of the error.  
  
### Set this value to selected cells  
 Select how the ODBC destination handles all selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
### Apply  
 Apply the error handling options to the selected cells.  
  
## Error Handling Options  
 You use the following options to configure how the ODBC destination handles errors and truncations.  
  
### Fail Component  
 The Data Flow task fails when an error or a truncation occurs. This is the default behavior.  
  
### Ignore Failure  
 The error or the truncation is ignored.  
  
### Redirect Flow  
 The row that is causing the error or the truncation is directed to the error output of the ODBC destination. For more information, see ODBC Destination.  
  
## See Also  
 [ODBC Destination Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/odbc-destination-editor-connection-manager-page.md)   
 [ODBC Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/odbc-destination-editor-mappings-page.md)  
  
  
