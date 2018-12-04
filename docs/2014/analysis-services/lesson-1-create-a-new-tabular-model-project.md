---
title: "Lesson 1: Create a New Tabular Model Project | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 0d2eb34d-78c8-41ff-b92d-49b62c16b2ac
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 1: Create a New Tabular Model Project
  In this lesson, you will create a new, blank tabular model project in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. Once your new project is created, you can begin adding data by using the Table Import Wizard. In addition to creating a new project, this lesson also includes a brief introduction to the tabular model authoring environment in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)].  
  
 To learn more about the different types of tabular model projects, see [Tabular Model Projects &#40;SSAS Tabular&#41;](tabular-models/tabular-model-projects-ssas-tabular.md). To learn more about the tabular model authoring environment, see [Tabular Model Designer &#40;SSAS Tabular&#41;](tabular-model-designer-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **10 minutes**  
  
## Prerequisites  
 This topic is the first lesson in a tabular model authoring tutorial. To complete this lesson, you must have the AdventureWorksDW database installed on a SQL Server instance. For more information, see [Tabular Modeling &#40;Adventure Works Tutorial&#41;](tabular-modeling-adventure-works-tutorial.md).  
  
## Create a New Tabular Model Project  
  
#### To create a new tabular model project  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **File** menu, click **New**, and then click **Project**.  
  
2.  In the **New Project** dialog box, under **Installed Templates**, click **Business Intelligence**, then click **Analysis Services**, and then click **Analysis Services Tabular Project**.  
  
3.  In  **Name**, type `AW Internet Sales Tabular Model`, then specify a location for the project files.  
  
     By default, **Solution Name** will be the same as the project name; however, you can type a different solution name.  
  
4.  Click **OK**.  
  
## Understanding the SQL Server Data Tools Tabular Model Authoring Environment  
 Now that you've created a new tabular model project, let's take a moment to explore the tabular model authoring environment in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] (Visual Studio 2010 or later).  
  
 After your project is created, it opens in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]. An empty model will appear in the model designer and the **Model.bim** file will be selected in the **Solution Explorer** window. When you add data, tables and columns will appear in the designer. If you don't see the designer (the empty window with the Model.bim tab), in **Solution Explorer**, under `AW Internet Sales Tabular Model`, double click the **Model.bim** file.  
  
 You can view the basic project properties in the **Properties** window. In **Solution Explorer**, click `AW Internet Sales Tabular Model`. Notice in the **Properties** window, in **Project File**, you will see **AW Internet Sales Tabular Model.smproj**. This is the project file name, and in **Project Folder**, you will see the project file location.  
  
 In **Solution Explorer**, right-click the `AW Internet Sales Tabular Model` project, and then click **Properties**. The **AW Internet Sales Tabular Model Property Pages** dialog box appears. These are the advanced project properties. You will set some of these properties later when you are ready to deploy your model.  
  
 Now, let's look at the model properties. In **Solution Explorer**, click **Model.bim**. In the **Properties** window, you will now see the model properties, most important of which is the **DirectQuery Mode** property. This property specifies whether or not the model is deployed in In-Memory mode (Off) or DirectQuery mode (On). For this tutorial, you will author and deploy your model in In-Memory mode.  
  
 When you create a new model, certain model properties are set automatically according to the Data Modeling settings that can be specified in the Tools\Options dialog box. Data Backup, Workspace Retention, and Workspace Server properties specify how and where the workspace database (your model authoring database) is backed up, retained in-memory, and built. You can change these settings later if necessary, but for now, just leave these properties as they are.  
  
 When you installed [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], several new menu items were added to the Visual Studio environment. Let's look at the new menu items that are specific to authoring tabular models. Click on the **Model** menu. From here, you can launch the Table Import Wizard, view and edit existing connections, refresh workspace data, browse your model in [!INCLUDE[msCoName](../includes/msconame-md.md)] Excel with the Analyze in Excel feature, create perspectives and roles, select the model view, and set calculation options.  
  
 Click on the **Table** menu. Here, you can create and manage relationships between tables, create and manage, specify date table settings, create partitions, and edit table properties.  
  
 Click on the **Column** menu. Here, you can add and delete columns in a table, freeze columns, and specify sort order. You can also use the AutoSum feature to create a standard aggregation measure for a selected column. Other toolbar buttons provide quick access to frequently used features and commands.  
  
 Explore some of the dialogs and locations for various features specific to authoring tabular models. While some items will not yet be active, you can get a good idea of the tabular model authoring environment.  
  
## Next Steps  
 To continue this tutorial, go to the next lesson: [Lesson 2: Add Data](lesson-2-add-data.md).  
  
  
