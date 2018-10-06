---
title: "ODBC Source Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.odbcsource.columns.f1"
ms.assetid: 565984eb-8318-4be7-bebc-262209cf5065
author: douglaslms
ms.author: douglasl
manager: craigg
---
# ODBC Source Editor (Columns Page)
  Use the **Columns** page of the **ODBC Source Editor** dialog box to map an output column to each external (source) column.  
  
 To learn more about the ODBC source, see [ODBC Source](data-flow/odbc-source.md).  
  
## Task List  
 **To open the ODBC Source Editor Columns Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the ODBC source.  
  
2.  On the **Data Flow** tab, double-click the ODBC source.  
  
3.  In the **ODBC Source Editor**, click **Columns**.  
  
## Options  
  
### Available External Columns  
 A list of available external columns in the data source. You cannot use this table to add or delete columns. Select the columns to use from the source. The selected columns are added to the **External Column** list in the order they are selected.  
  
 Select the **Select All** check box to select all of the columns.  
  
### External Column  
 A view of the external (source) columns in the order that you see them when configuring components that consume data from the ODBC source.  
  
### Output Column  
 Enter a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name entered is displayed in the SSIS Designer.  
  
## See Also  
 [ODBC Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/odbc-source-editor-connection-manager-page.md)   
 [ODBC Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/odbc-source-editor-error-output-page.md)  
  
  
