---
title: "Create a Dimension Using the Dimension Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "dimensions [Analysis Services], creating"
ms.assetid: d84f66ae-7551-49bf-99d0-88368ca2dd0e
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a Dimension Using the Dimension Wizard
  You can create a new dimension by using the Dimension Wizard in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
### To create a new dimension  
  
1.  In **Solution Explorer**, right-click **Dimensions**, and then click **New Dimension**.  
  
2.  On the **Select Creation Method** page of the Dimension Wizard, select **Use an existing table**, and then click **Next**.  
  
    > [!NOTE]  
    >  You might occasionally have to create a dimension without using an existing table. For more information, see [Create a Dimension by Generating a Non-Time Table in the Data Source](create-a-dimension-by-generating-a-non-time-table-in-the-data-source.md) and [Create a Time Dimension by Generating a Time Table](create-a-time-dimension-by-generating-a-time-table.md).  
  
3.  On the **Specify Source Information** page, do the following procedures:  
  
    1.  In the **Data source view** list, select a data source view.  
  
    2.  In the **Main table** list, select the main dimension table.  
  
    3.  In the **Key columns** list, review the key columns that the wizard has automatically selected based on the primary key that is defined in the main dimension table. To change this default setting, specify the key columns that link the dimension table to the fact table.  
  
    4.  In the **Name column** drop-down list, review the name column that the wizard has automatically selected.  
  
         This default name is appropriate when the column contains descriptive information. However, you might want to specify a name that contains values that are more meaningful for the end user. For example, if a product category attribute in a Products dimension uses the **ProductCategoryKey** column as its key column, you can specify the **ProductCategoryName** column as its name column.  
  
         If the **Key columns** list contains multiple key columns, you must specify a name column that provides the member values for the key attribute. To do this, you can create a named calculation in the data source view and use that as the name column.  
  
    5.  Click **Next**.  
  
4.  On the **Select Related Tables** page, select the related tables that you want to include in your dimension, and then click **Next**.  
  
    > [!NOTE]  
    >  The **Select Related Tables** page appears if the main dimension table that you specified has relationships to other dimension tables.  
  
5.  On the **Select Dimension Attributes** page, select the attributes that you want to include in the dimension, and then click **Next**.  
  
     Optionally, you can change the attribute names, enable or disable browsing, and specify the attribute type.  
  
    > [!NOTE]  
    >  To make the **Enable Browsing** and **Attribute Type** fields of an attribute active, the attribute must be selected for inclusion in the dimension.  
  
6.  On the **Define Account Intelligence** page, in the **Built-In Account Types** column, select the account type, and then click **Next**.  
  
     The account type must correspond to the account type of the source table that is listed in the **Source Table Account Types** column.  
  
    > [!NOTE]  
    >  The **Define Account Intelligence** page appears if you defined an **Account Type** dimension attribute on the **Select Dimension Attributes** page of the wizard.  
  
7.  On the **Completing the Wizard** page, enter a name for the new dimension and review the dimension structure. If you want to make changes, click **Back**; otherwise, click **Finish**.  
  
    > [!NOTE]  
    >  You can use Dimension Designer after you complete the Dimension Wizard to add, remove, and configure attributes and hierarchies in the dimension.  
  
## See Also  
 [Create a Dimension by Using an Existing Table](create-a-dimension-by-using-an-existing-table.md)  
  
  
