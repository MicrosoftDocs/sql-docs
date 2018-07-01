---
title: "Create a Cube Using the Cube Wizard | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create a Cube Using the Cube Wizard
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can create a new cube by using the Cube Wizard in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
### To create a new cube  
  
1.  In **Solution Explorer**, right-click **Cubes**, and then click **New Cube**.  
  
2.  On the **Select Creation Method** page of the Cube Wizard, select **Use existing tables**, and then click **Next**.  
  
    > [!NOTE]  
    >  You might occasionally have to create a cube without using existing tables. To create an empty cube, select **Create an empty cube**. To generate tables, select **Generate tables in the data source**.  
  
3.  On the **Select Measure Group Tables** page, do the following procedures:  
  
    1.  In the **Data source view** list, select a data source view.  
  
    2.  In the **Measure group tables** list, select the tables that will be used to create measure groups.  
  
    3.  Click **Next**.  
  
4.  On the **Select Measures** page, select the measures that you want to include in the cube, and then click **Next**.  
  
     Optionally, you can change the names of the measures and measure groups.  
  
5.  On the **Select Existing Dimensions** page, select the existing dimensions to include in the cube, and then click **Next**.  
  
    > [!NOTE]  
    >  The **Select Existing Dimensions** page appears if dimensions already exist in the database for any of the selected measure groups.  
  
6.  On the **Select New Dimensions** page, select the new dimensions to create, and then click **Next**.  
  
    > [!NOTE]  
    >  The **Select New Dimensions** page appears if any tables would be good candidates for dimension tables, and the tables have not already been used by existing dimensions.  
  
7.  On the **Select Missing Dimension Keys** page, select a key for the dimension, and then click **Next**.  
  
    > [!NOTE]  
    >  The **Select Missing Dimension Keys** page appears if any of the dimension tables that you specified do not have a key defined.  
  
8.  On the **Completing the Wizard** page, enter a name for the new cube and review the cube structure. If you want to make changes, click **Back**; otherwise, click **Finish**.  
  
    > [!NOTE]  
    >  You can use Cube Designer after you complete the Cube Wizard to configure the cube. You can also use Dimension Designer to add, remove, and configure attributes and hierarchies in the dimensions that you created.  
  
  
