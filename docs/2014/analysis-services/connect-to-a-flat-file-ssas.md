---
title: "Connect to a Flat File (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.connflatfile.f1"
ms.assetid: a365991e-eded-4cd8-89c0-0daf6d658d15
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to a Flat File (SSAS)
  This page of the **Table Import Wizard** enables you to connect to a flat file (.txt), tab-separated file (.tab), or a comma-separated file (.csv). To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
 To connect to a flat file, you must have the ACE provider installed on your computer. For more information, see [Data Sources Supported &#40;SSAS Tabular&#41;](tabular-models/data-sources-supported-ssas-tabular.md).  
  
> [!NOTE]  
>  The credentials of the current user are used when selecting a file in this page. However, import will not succeed if the user specified in the Impersonation Information page does not have sufficient privileges to read from the selected file.  
  
## UIElement List  
 **Friendly connection name**  
 Type a unique name for this data source connection. This is a required field.  
  
 **File Path**  
 Specify a full path for the file.  
  
 **Browse**  
 Navigate to a location where a file is available.  
  
 **Column Separator**  
 Select from a list of available column separators. Choose a separator that is not likely to occur in the text.  
  
|Value|Description|  
|-----------|-----------------|  
|Tab (t)|Columns are separated by a tab (t).|  
|Comma (,)|Columns are separated by a comma (,).|  
|Semicolon (;)|Columns are separated by a semicolon (;).|  
|Space ( )|Columns are separated by a space ( ).|  
|Colon (:)|Columns are separated by a colon (:).|  
|Vertical Bar (&#124;)|Columns are separated by a vertical bar (&#124;).|  
  
 **Advanced**  
 Specify the encoding and locale options for the flat file.  
  
 **Use first row as column headers**  
 Specify whether to use the first data row as the column headers of the destination table.  
  
 **Data preview**  
 Preview the data in the selected file, and use the following options to modify the data import.  
  
> [!NOTE]  
>  Only the first 50 rows in the file are displayed in this preview.  
  
|Option|Description|  
|------------|-----------------|  
|**Checkbox in the column header**|Select the checkbox to include the column in the data import. Clear the checkbox to remove the column from the data import.|  
|**Down-arrow button in the column header**|Sort and filter data in the column.|  
  
 **Clear Row Filters**  
 Remove all filters that were applied to the data in the columns.  
  
  
