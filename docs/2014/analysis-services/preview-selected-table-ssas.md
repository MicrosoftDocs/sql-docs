---
title: "Preview Selected Table (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.previewselecttable.f1"
ms.assetid: b6b34b5a-43b3-4a75-9f3b-b2ad1084b1b6
author: minewiskan
ms.author: owend
manager: craigg
---
# Preview Selected Table (SSAS)
  This page of the **Table Import Wizard** enables you to preview the data in the selected table, to select the columns to include in the data import, and to filter data in the selected columns. To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
 Not all the rows in the table are displayed. However, the filters that you set will be applied to all of the data in the table during import.  
  
 For data sources using Windows authentication, the credentials of the current user are used to fetch the data displayed in the Preview and Filter dialog. For other data sources, the credentials supplied in the connection string are used to fetch the data.  
  
 The appearance of data on this page does not guarantee import will succeed. If the user name specified in the Impersonation Information page does not have sufficient privileges to read from the selected database, import will fail.  
  
## UIElement List  
 **Checkbox in the column header**  
 Select the checkbox to include the column in the data import. Clear the checkbox to remove the column from the data import.  
  
 **Down-arrow button in the column header**  
 Filter data in the column.  
  
 **Clear Row Filters**  
 Remove all filters that were applied to the data in the columns.  
  
  
