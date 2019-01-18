---
title: "Select Tables and Views (Data Source View Wizard) (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.datasourceviewwizard.selecttablesandviews.f1"
ms.assetid: ea7d1232-f213-46e9-90d9-0fd616ca003d
author: minewiskan
ms.author: owend
manager: craigg
---
# Select Tables and Views (Data Source View Wizard) (Analysis Services)
  Use the **Select Tables and Views** page to select the tables or views from the data source that you want to include in the data source view.  
  
## Options  
 **Available objects**  
 Lists the tables and views in the data source schema. The schema name prefixes the name of every object if more than one schema is listed. If only one schema is listed, the schema's name does not prefix the object names.  
  
 To reorder the list in ascending or descending order, click either **Name** or **Type**.  
  
 **Included objects**  
 Lists the tables and views to include in the data source view.  
  
 To reorder the list in ascending or descending order, click either **Name** or **Type**.  
  
 **Filter**  
 Filters the objects listed under **Available objects**. Type a string, and then click **Filter** to list only the names that contain a specified string. To find an exact string of characters, enclose the string between double quotation marks. The filter is not case sensitive.  
  
 You can include the wildcard characters listed in the following table anywhere in the filter string.  
  
|Wildcard character|Value|  
|------------------------|-----------|  
|**\***|Any string of characters|  
|**%**|Any string of characters|  
|**?**|A single character|  
|**"** *string* **"**|A literal string of characters. This wildcard character will match any substring within the object name.|  
  
 **Show system objects**  
 Displays system objects under **Available objects**. This option is available only if the data source provider exposes system objects. Removing a system object from the **Included objects** list automatically selects this option.  
  
 **Add related tables**  
 Adds all the tables that are related to those listed under **Included objects**. This option does not add views. However, this option does add partitioned tables. If you select name-matching criteria on the **Name Matching** page of the wizard, this option also includes logically related tables according to the criteria you select. Tables are also included if they are related to the newly added related tables, and if they have an identical structure to the original table.  
  
## See Also  
 [Data Source View Wizard F1 Help &#40;Analysis Services&#41;](data-source-view-wizard-f1-help-analysis-services.md)   
 [Data Source Views in Multidimensional Models](multidimensional-models/data-source-views-in-multidimensional-models.md)  
  
  
