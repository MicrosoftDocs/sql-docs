---
title: "Name Matching (Data Source View Wizard) (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.datasourceviewwizard.namematchingcriteria.f1"
ms.assetid: 7f811e02-0fe6-45c9-a7b7-29c61032d96b
author: minewiskan
ms.author: owend
manager: craigg
---
# Name Matching (Data Source View Wizard) (Analysis Services)
  Use the **Name Matching** page to select the criterion to use for detecting possible relationships between the tables that you select for the data source view and the other tables in the schema. If no physical foreign key relationships exist between the tables, this criterion helps you identify and add related tables to the data source view. The logical relationships that are identified by name matching are also added to the data source view.  
  
> [!NOTE]  
>  This page appears only if you select a data source that has multiple tables but no foreign key relationships between any one of the tables.  
  
## Options  
 **Create logical relationships by matching columns**  
 Select to use a name-matching criterion to detect possible logical dependencies and relationships between the tables that you select to include in the data source view and the other tables in the schema. If you clear this check box, no name-matching criterion is used to identify logical relationships between tables in the data source.  
  
 **Foreign key matches**  
 Select the criterion to use for creating logical relationships between tables and views in the data source. Non-alphanumeric characters are ignored in matching strings. For example, "Customer ID", "Customer_ID", "CustomerID" all match. Select one of the options in the following table to create relationships under the specified conditions.  
  
|Select|To create|  
|------------|---------------|  
|**Same name as primary key**|A logical relationship to any table with a column name that matches the name of the primary key column in a selected table.|  
|**Same name as destination table name**|A logical relationship to any table with a column name that matches the name of a selected table.|  
|**Destination table name + primary key name**|A logical relationship to any table in which a column name matches the selected table name concatenated with the name of the primary key column for the selected table, in that order. Non-alphanumeric characters within the concatenation are ignored (for example, "Product ID", "Product_ID" and "ProductID" all match).|  
  
 **Description and sample**  
 View a description and a sample of the selected criterion.  
  
  
