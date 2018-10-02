---
title: "Connect to a Microsoft Excel File (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.connexcelfile.f1"
ms.assetid: 126f7d6b-d270-40e7-b23e-8d114f87065b
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to a Microsoft Excel File (SSAS)
  This page of the **Table Import Wizard** enables you to connect to a Microsoft Excel file stored on the local machine. To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
 To connect to a Microsoft Excel file, you must have the appropriate ACE provider installed on your computer. For more information, see [Data Sources Supported &#40;SSAS Tabular&#41;](tabular-models/data-sources-supported-ssas-tabular.md).  
  
> [!NOTE]  
>  The credentials of the current user are used when selecting a file in this page. However, import will not succeed if the user specified in the Impersonation Information page does not have sufficient privileges to read from the selected file.  
  
## UIElement List  
 **Friendly connection name**  
 Type a unique name for this data source connection. This is a required field.  
  
 **Excel File Path**  
 Specify a full path for the Excel file.  
  
 **Browse**  
 Navigate to a location where an Excel file is available.  
  
 **Advanced**  
 Set additional connection properties by using the **Set Advanced Properties** dialog box..  
  
 **Use first row as column headers**  
 Specify whether to use the first data row as the column headers of the destination table.  
  
 **Test Connection**  
 Attempt to establish a connection to the data source using the current settings. A message is displayed indicating whether the connection is successful.  
  
  
