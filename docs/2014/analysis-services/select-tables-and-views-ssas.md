---
title: "Select Tables and Views (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.seltablesviews.f1"
ms.assetid: 5e8121cc-03f0-4168-98cf-63c5c032bb0b
author: minewiskan
ms.author: owend
manager: craigg
---
# Select Tables and Views (SSAS)
  This page of the **Table Import Wizard** enables you to select the tables and views that you want to import data from. To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
 The appearance of tables and views on this page does not guarantee that import will succeed. If the user specified in the Impersonation Information page does not have sufficient privileges to read from the selected database, import will fail.  
  
 For data sources using Windows authentication, the credentials of the current user are used to fetch the tables and views in the Select Tables and Views dialog. For other data sources, the credentials supplied in the connection string are used to fetch the data.  
  
## UIElement List  
 **Server**  
 Displays the server that you are connected to.  
  
 **Database**  
 Displays the database that you selected.  
  
 **Tables and Views**  
 Lists the tables and views in the database. Select the checkbox beside each table and view that you want to import.  
  
 **Source Table**  
 Specifies the name of the source table based on the type of data source.  
  
 **Schema**  
 Specifies the schema in which the source table is contained. Depending on the type of database, a schema functions as a container for other objects, such as tables, and can also indicate ownership of those objects.  
  
 **Friendly Name**  
 Specifies the friendly name of the source table. By default, the column displays the name of the source table that appears in the **Source Table** column. Change the name if you want to use a different name than the name used in the source database.  
  
 **Filter Details**  
 When a filter has been applied to the data that is being imported, displays the data import filter in the **Filter Details** dialog box. For more information, see [Filter Details &#40;SSAS&#41;](filter-details-ssas.md).  
  
 **Preview and Filter**  
 Displays the **Preview Selected Table** dialog box that is used to apply a filter to the data that is being imported. For more information, see [Preview Selected Table &#40;SSAS&#41;](preview-selected-table-ssas.md).  
  
 **Select Related Tables**  
 Selects for import those tables and views that are related to the tables and views that you have already selected.  
  
  
