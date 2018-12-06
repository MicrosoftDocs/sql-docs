---
title: "Lesson 5: Create Relationships | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: abac1a00-f827-4c3e-a473-6db5c8a3a66f
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 5: Create Relationships
  In this lesson, you will verify the relationships that were created automatically when you imported data and add new relationships between different tables. A relationship is a connection between two tables that establishes how the data in those tables should be correlated. For example, the Product table and the Product Subcategory table have a relationship based on the fact that each product belongs to a subcategory. To learn more, see [Relationships &#40;SSAS Tabular&#41;](tabular-models/relationships-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **10 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 3: Rename Columns](rename-columns.md).  
  
## Review Existing Relationships and Add New Relationships  
 When you imported data by using the Table Import Wizard, you imported seven tables from the AdventureWorksDW database. Generally, if you import data from a relational source, existing relationships are automatically imported together with the data. However, before you proceed with authoring your model you should verify those relationships between tables were created properly. For this tutorial, you will also add three new relationships.  
  
#### To review existing relationships  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click on the **Model** menu, then point to **Model View**, and then click **Diagram View**.  
  
     The model designer now appears in Diagram View, a graphical format displaying all of the tables you imported with lines between them. The lines between tables indicate the relationships that were automatically created when you imported the data.  
  
     Use the minimap controls in the upper-right corner of the model designer to adjust the view to include as many of the tables as possible. You can also click and drag tables to different locations, bringing tables closer together, or putting them in a particular order. Moving tables does not affect the relationships already between the tables. To view all of the columns in a particular table, click and drag on a table edge to expand or make it smaller.  
  
2.  Click on the solid line between the **Customer** table and the **Geography** table. The solid line between these two tables show this relationship is active, that is, it is used by default when calculating DAX formulas.  
  
     Notice the **Geography Id** column in the **Customer** table and the **Geography Id** column in the **Geography** table now both each appear within a box. This shows these are the columns used in the relationship. The relationship's properties now also appear in the **Properties** window.  
  
    > [!TIP]  
    >  In addition to using the model designer in diagram view, you can also use the **Manage Relationships** dialog box to show the relationships between all tables in a table format. Click on the **Table** menu, and then click **Manage Relationships**. The **Manage Relationships** dialog box shows the relationships that were automatically created when you imported data.  
  
3.  Use the model designer in diagram view, or the **Manage Relationships** dialog box, to verify the following relationships were created when each of the tables were imported from the AdventureWorksDW database:  
  
    |Active|Table|Related Lookup Table|  
    |------------|-----------|--------------------------|  
    |Yes|**Customer [Geography Id]**|**Geography [Geography Id]**|  
    |Yes|**Product [Product Subcategory Id]**|**Product Subcategory [Product Subcategory Id]**|  
    |Yes|**Product Subcategory [Product Category Id]**|**Product Category [Product Category Id]**|  
    |Yes|**Internet Sales [Customer Id]**|**Customer [Customer Id]**|  
    |Yes|**Internet Sales [Product Id]**|**Product [Product Id]**|  
  
 If any of the relationships in the table above are missing, verify that your model includes the following tables: Customer, Date, Geography, Product, Product Category, Product Subcategory, and Internet Sales. If tables from the same data source connection are imported at separate times, any relationships between those tables will not be created and must be created manually.  
  
 In some cases, you may need to create additional relationships between tables in your model to support certain business logic. For this tutorial, you need to create three additional relationships between the Internet Sales table and the Date table.  
  
#### To add new relationships between tables  
  
1.  In the model designer, in the **Internet Sales** table, click and hold on the **Order Date** column, then drag the cursor to the **Date** column in the **Date** table, and then release.  
  
     A solid line appears showing you have created an active relationship between the **Order Date** column in the **Internet Sales** table and the **Date** column in the **Date** table.  
  
    > [!NOTE]  
    >  When creating relationships, the order between the primary table and the related lookup table is automatically put in the correct order.  
  
2.  In the **Internet Sales** table, click and hold on the **Due Date** column, then drag the cursor to the **Date** column in the **Date** table, and then release.  
  
     A dotted line appears showing you have created an inactive relationship between the **Due Date** column in the **Internet Sales** table and the **Date** column in the **Date** table. You can have multiple relationships between tables, but only one relationship can be active at a time.  
  
3.  Finally, create one more relationship; in the **Internet Sales** table, click and hold on the **Ship Date** column, then drag the cursor to the **Date** column in the **Date** table, and then release.  
  
     A dotted line appears showing you have created an inactive relationship between the **Ship Date** column in the **Internet Sales** table and the **Date** column in the **Date** table.  
  
## Next Step  
 To continue this lesson, go to the next lesson: [Lesson 6: Create Calculated Columns](lesson-5-create-calculated-columns.md).  
  
  
