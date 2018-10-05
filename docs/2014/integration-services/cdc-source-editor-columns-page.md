---
title: "CDC Source Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.cdcsource.columns.f1"
ms.assetid: bcf3030e-98d8-4445-967c-33c3f8ecb4fc
author: douglaslms
ms.author: douglasl
manager: craigg
---
# CDC Source Editor (Columns Page)
  Use the **Columns** page of the **CDC Source Editor** dialog box to map an output column to each external (source) column.  
  
 To learn more about the CDC source, see [CDC Source](data-flow/cdc-source.md).  
  
## Task List  
 **To open the CDC Source Editor Columns Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the CDC source.  
  
2.  On the **Data Flow** tab, double-click the CDC source.  
  
3.  In the **CDC Source Editor**, click **Columns**.  
  
## Options  
 **Available External Columns**  
 A list of available external columns in the data source. You cannot use this table to add or delete columns. Select the columns to use in the source. The selected columns are added to the **External Column** list in the order they are selected.  
  
 **External Column**  
 A view of the external (source) columns in the order that you see them when configuring components that consume data from the CDC source. To change this order, first clear the selected columns in the **Available External Columns** list, and then select external columns from the list in a different order. The selected columns are added to the **External Column** list in the order you select them.  
  
 **Output Column**  
 Enter a unique name for each output column. The default is the name of the selected external (source) column, however you can choose any unique, descriptive name. The name entered is displayed in the SSIS Designer.  
  
## See Also  
 [CDC Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/cdc-source-editor-connection-manager-page.md)   
 [CDC Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/cdc-source-editor-error-output-page.md)  
  
  
