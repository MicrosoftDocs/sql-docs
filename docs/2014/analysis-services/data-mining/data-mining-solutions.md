---
title: "Data Mining Solutions | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], about data mining"
  - "data mining [Analysis Services], development"
ms.assetid: 84f6548d-ebb0-4e10-9b29-66253fa0a04a
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining Solutions
  A data mining solution is an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] solution that contains one or more data mining projects.  
  
 The topics in this section provide information about how to design and implement an integrated data mining solution by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For an overview of the data mining design process and related tools, see [Data Mining Concepts](data-mining-concepts.md).  
  
 For more information about additional projects types that are useful for data mining, see [Related Projects for Data Mining Solutions](data-mining-solutions.md).  
  
 [Relational vs. Multidimensional Solutions](#bkmk_RelMD)  
  
 [Deploying Data Mining Solutions](#bkmk_Deploy)  
  
 [Solution Walkthroughs](#bkmk_Walkthru)  
  
##  <a name="bkmk_RelMD"></a> Relational vs. Multidimensional Solutions  
 A data mining solution can be based either on multidimensional data-that is, an existing cube-or on purely relational data, such as the tables and views in a data warehouse, or on text files, Excel workbooks, or other external data sources.  
  
-   You can create data mining objects within an existing multidimensional database solution.  
  
     Typically you would create a solution like this if you have already created a cube and want to perform data mining by using the cube as a data source. When you move and backup models based on a cube, the cube must also be moved or copied.  
  
-   You can create a data mining solution that contains only data mining objects, including the supporting data sources and data source views, and that uses relational data source only.  
  
     This is the preferred method for creating data mining models, as processing and querying is generally fastest against relational data sources. You can also easily move and backup models between servers by using the EXPORT and IMPORT commands.  
  
##  <a name="bkmk_Deploy"></a> Deploying Data Mining Solutions  
 The instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to which you deploy the solution must be running in a mode that supports multidimensional objects and data mining objects; that is, you cannot deploy data mining objects to an instance that hosts tabular models or PowerPivot data.  
  
 Therefore, when you create a data mining solution in Visual Studio, be sure to use the template, **Analysis Services Multidimensional and Data Mining Project**.  
  
 When you deploy the solution, the objects used for data mining are created in the specified [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, in a database with the same name as the solution file.  
  
 For more information about how to deploy both relational and multidimensional solutions, see [Deployment of Data Mining Solutions](deployment-of-data-mining-solutions.md).  
  
##  <a name="bkmk_Walkthru"></a> Solution Walkthrough  
 Provides an overview of how to create data mining solutions by using the Data Mining Wizard.  
  
 [Create a Relational Mining Structure](create-a-relational-mining-structure.md)  
 Create a mining structure from relational data, text files, and other sources that can be combined in a data source view.  
  
 [Create an OLAP Mining Structure](create-an-olap-mining-structure.md)  
 Create a mining structure based on data in an OLAP cube. Models that you create from OLAP data can be saved as a data mining dimension, or you can save the set of data and your models as a new cube.  
  
## In This Section  
 [Data Mining Projects](data-mining-projects.md)  
  
 [Processing Data Mining Objects](processing-data-mining-objects.md)  
  
 [Related Projects for Data Mining Solutions](data-mining-solutions.md)  
  
 [Deployment of Data Mining Solutions](deployment-of-data-mining-solutions.md)  
  
## Related Tasks and Topics  
 After you have created a basic data mining solution, including data sources and a mining structure, you can build on the solution by adding new models, testing and comparing models, creating predictions, and experimenting with subsets of data.  
  
 For more information, see the following links:  
  
|Tasks|Topics|  
|-----------|------------|  
|Test the models you create, validate the quality of your training data, and create charts that represent the accuracy of data mining models.|[Testing and Validation &#40;Data Mining&#41;](testing-and-validation-data-mining.md)|  
|Train the model by populating the structure and related models with data. Update and extend models with new data.|[Processing Data Mining Objects](processing-data-mining-objects.md)|  
|Customize a mining model by applying filters to the training data, choosing a different algorithm, or setting advanced algorithm parameters.|[Customize Mining Models and Structure](customize-mining-models-and-structure.md)|  
|Customize a mining model by applying filters to the data used in training the mode.|[Add Mining Models to a Structure &#40;Analysis Services - Data Mining&#41;](add-mining-models-to-a-structure-analysis-services-data-mining.md)|  
|Update and manage data mining solutions.|Link TBD|  
  
## See Also  
 [Data Mining Tutorials &#40;Analysis Services&#41;](../data-mining-tutorials-analysis-services.md)  
  
  
