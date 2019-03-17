---
title: "Using a Modified Version of the Analysis Services Tutorial Project | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 685aa217-de1b-4df2-bf22-095228c40775
author: minewiskan
ms.author: owend
manager: craigg
---
# Using a Modified Version of the Analysis Services Tutorial Project
  The remaining lessons in this tutorial are based on an enhanced version of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project that you completed in the first three lessons. Additional tables and named calculations have been added to the **Adventure Works DW 2012** data source view, additional dimensions have been added to the project, and these new dimensions have been added to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube. In addition, a second measure group has been added, which contains measures from a second fact table. This enhanced project will enable you to continue learning how to add functionality to your business intelligence application without having to repeat the skills you have already learned.  
  
 Before you can continue with the tutorial, you must download, extract, load and process the enhanced version of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project.  Use the instructions in this lesson to ensure you have performed all the steps.  
  
## Downloading and Extracting the Project File  
  
1.  [Click here](https://go.microsoft.com/fwlink/?LinkID=221866) to go to the download page that provides the sample projects that go with this tutorial. The tutorial projects are included in the **Analysis Services Tutorial SQL Server 2012** download.  
  
2.  Click **Analysis Services Tutorial SQL Server 2012** to download the package that contains the projects for this tutorial.  
  
     By default, a .zip file is saved to the Downloads folder. You must move the .zip file to a location that has a shorter path (for example, create a C:\Tutorials folder to store the files).  You can then extract the files contained in the .zip file. If you attempt to unzip the files from the Downloads folder, which has a longer path, you will only get Lesson 1.  
  
3.  Create a subfolder at or near the root drive, for example, C:\Tutorial.  
  
4.  Move the **Analysis Services Tutorial SQL Server 2012.zip** file to the subfolder.  
  
5.  Right-click the file and select **Extract All**.  
  
6.  Browse to the **Lesson 4 Start** folder to find the **Analysis Services Tutorial.sln** file.  
  
## Loading and Processing the Enhanced Project  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **File** menu, click **Close Solution** to close files you won't be using.  
  
2.  On the **File** menu, point to **Open**, and then click **Project/Solution**.  
  
3.  Browse to the location where you extracted the tutorial project files.  
  
     Find the folder named **Lesson 4 Start**, and then double-click Analysis Services Tutorial.sln.  
  
4.  Deploy the enhanced version of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project to the local instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], or to another instance, and verify that processing completes successfully.  
  
## Understanding the Enhancements to the Project  
 The enhanced version of the project is different from the version of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project that you completed in the first three lessons. The differences are described in the following sections. Review this information before continuing with the remaining lessons in the tutorial.  
  
### Data Source View  
 The data source view in the enhanced project contains one additional fact table and four additional dimension tables from the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database.  
  
 Notice that with ten tables in the data source view, the \<All Tables> diagram is becoming crowded. This makes it difficult to easily understand the relationships between the tables and to locate specific tables. To solve this problem, the tables are organized into two logical diagrams, the **Internet Sales** diagram and the **Reseller Sales** diagram. These diagrams are each organized around a single fact table. Creating logical diagrams lets you view and work with a specific subset of the tables in a data source view instead of always viewing all the tables and their relationships in a single diagram.  
  
#### Internet Sales Diagram  
 The **Internet Sales** diagram contains the tables that are related to the sale of [!INCLUDE[ssSampleDBCoShort](../includes/sssampledbcoshort-md.md)] products directly to customers through the Internet. The tables in the diagram are the four dimension tables and one fact table that you added to the **Adventure Works DW 2012** data source view in Lesson 1. These tables are as follows:  
  
-   **Geography**  
  
-   **Customer**  
  
-   **Date**  
  
-   **Product**  
  
-   **InternetSales**  
  
#### Reseller Sales Diagram  
 The **Reseller Sales** diagram contains the tables that are related to the sale of [!INCLUDE[ssSampleDBCoShort](../includes/sssampledbcoshort-md.md)] products by resellers. This diagram contains the following seven dimension tables and one fact table from the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database:  
  
-   **Reseller**  
  
-   **Promotion**  
  
-   **SalesTerritory**  
  
-   **Geography**  
  
-   **Date**  
  
-   **Product**  
  
-   **Employee**  
  
-   **ResellerSales**  
  
 Notice that the **DimGeography**, **DimDate**, and **DimProduct** tables are used in both the **Internet Sales** diagram and the **Reseller Sales** diagram. Dimension tables can be linked to multiple fact tables.  
  
### Database and Cube Dimensions  
 The [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project contains five new database dimensions, and the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube contains these same five dimensions as cube dimensions. These dimensions have been defined to have user hierarchies and attributes that were modified by using named calculations, composition member keys, and display folders. The new dimensions are described in the following list.  
  
 Reseller Dimension  
 The Reseller dimension is based on the **Reseller** table in the **Adventure Works DW 2012** data source view.  
  
 Promotion Dimension  
 The Promotion dimension is based on the **Promotion** table in the **Adventure Works DW 2012** data source view.  
  
 Sales Territory Dimension  
 The Sales Territory dimension is based on the **SalesTerritory** table in the **Adventure Works DW 2012** data source view.  
  
 Employee Dimension  
 The Employee dimension is based on the **Employee** table in the **Adventure Works DW 2012** data source view.  
  
 Geography Dimension  
 The Geography dimension is based on the **Geography** table in the **Adventure Works DW 2012** data source view.  
  
#### Analysis Services Cube  
 The **Analysis Services Tutorial** cube now contains two measure groups, the original measure group based on the **InternetSales** table and a second measure group based on the **ResellerSales** table in the **Adventure Works DW 2012** data source view.  
  
## Next Task in Lesson  
 [Defining Parent Attribute Properties in a Parent-Child Hierarchy](lesson-4-2-defining-parent-attribute-properties-in-a-parent-child-hierarchy.md) 
  
## See Also  
 [Deploying an Analysis Services Project](../analysis-services/lesson-2-5-deploying-an-analysis-services-project.md)  
  
  
