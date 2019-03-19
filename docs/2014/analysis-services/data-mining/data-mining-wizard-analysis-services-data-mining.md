---
title: "Data Mining Wizard (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "dimensions [Analysis Services], data mining"
  - "OLAP [Analysis Services], mining models"
  - "Data Mining Wizard"
  - "relational mining models [Analysis Services]"
ms.assetid: d5fea90f-5f38-4639-8851-7707f6606a12
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining Wizard (Analysis Services - Data Mining)
  The Data Mining Wizard in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] starts every time that you add a new mining structure to a data mining project. The wizard helps you choose a data source and set up a data source view that defines the data to be used for analysis, and then helps you create an initial model.  
  
 In the final phase of the wizard, you can optionally divide your data into training and testing sets, and enable features such as drillthrough.  
  
## What to Know Before You Start  
 Here are the things you need to know before you start the wizard.  
  
-   Will you build the data mining structure and models from a relational database or from an existing cube in an OLAP database?  
  
-   Which columns contain the keys that uniquely identify a case record?  
  
-   Which columns or attributes do you want to use for prediction? Which columns or attributes are good to use as input for analysis?  
  
-   Which algorithm should you use? The algorithms provided in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] all have different characteristics and produce different results. Fortunately you are not limited to one model for each set of data, so feel free to experiment by adding different models.  
  
-   Do you need to be able to test your models on a unified data set? If so, consider using the option to set some data aside for testing. You can choose a percentage, and cap that by a specified number of rows if desired.  
  
##  <a name="BKMK_Using_DM_Wizard"></a> Starting the Data Mining Wizard  
 To use the Data Mining Wizard, you must have opened a solution in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] that contains at least one data mining or OLAP project.  
  
-   If your solution is ready for data mining, you can simply right-click the **Mining Structures** node in Solution Explorer and select **New Mining Structure** to start the wizard.  
  
-   If your solution does not contain any existing projects, you can add a new data mining project. From the **File** menu, select **New**, and then select **Project**. Be sure to choose the template, **Analysis Services Multidimensional and Data Mining Project**.  
  
-   You can also use the Analysis Services Import Wizard to obtain metadata from an existing data mining solution. However, you cannot select the individual objects to import; the entire database is imported, including any cubes, data source views, etc. Also note that the new solution that is created via import is automatically configured to use the local default database. You might need to change this to another instance before you can process or browse the objects, and if you are importing from a previous version of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you might need to update references to providers.  
  
 Next, you will create the mining structure and one associated data mining model. You can also create just the mining structure and add models later, but it is generally easiest to create a test model first.  
  
###  <a name="BKMK_Relational"></a> Relational vs. OLAP Mining Models  
 The next important option that you have is whether to use a relational data source, or to base your model on multidimensional (OLAP) data.  
  
 The Data Mining Wizard branches into two paths at this point, depending on whether your data source is relational or in a cube. Everything else except the data selection process is the same-the choice of algorithm, the ability to add a holdout data set, etc.-but selecting cube data is a bit more complex than using relational data. (You also get some additional options at the end if you create a model based on a cube.)  
  
 See the following topics for a walkthrough of each option in more detail:  
  
 [Create a Relational Mining Structure](create-a-relational-mining-structure.md)  
 Walks you through the decisions you make when building a relational data mining model.  
  
 [Create an OLAP Mining Structure](create-an-olap-mining-structure.md)  
 Describes the additional options and selections to make when choosing data from an OLAP cube.  
  
> [!NOTE]  
>  You do not need to have a cube or an OLAP database to do data mining. Unless your data is already stored in a cube, or you want to mine OLAP dimensions or the results of OLAP aggregations or calculations, we recommend that you use a relational table or data source for data mining.  
  
### Choosing an Algorithm  
 Next, you must decide on which algorithm to use in processing your data. This decision can be difficult to make. Each algorithm provided in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] has different features and produces different results, so you can experiment and try several different models before determining which is most appropriate for your data and your business problem. See the following topic for an explanation of the tasks to which each algorithm is best suited:  
  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)  
  
 Again, you can create multiple models using different algorithms, or change parameters for the algorithms to create different models. You are not locked into your choice of algorithm, and it is good practice to create several different models on the same data.  
  
### Define the Data Used for Modeling  
 In addition to choosing the data from a source, you must specify which of the table in the data source view contains the *case data*. The case table will be used to train the data mining model, and as such should contain the entities that you want to analyze: for example, customers and their demographic information. Each case must be unique, and must be identifiable by a *case key*.  
  
 In addition to specifying the case table, you can include *nested tables* in your data. A nested table usually contains additional information about the entities in the case table, such as transactions conducted by the customer, or attributes that have a many-to-one relationship with the entity. For example, a nested table joined to the **Customers** case table might include a list of products purchased by each customer. In a model that analyzes traffic to a Web site, the nested table might include the sequences of pages that the user visited. For more information, see [Nested Tables &#40;Analysis Services - Data Mining&#41;](nested-tables-analysis-services-data-mining.md)  
  
### Additional Features  
 To assist you in choosing the right data, and configuring the data sources correctly, the Data Mining Wizard provides these additional features:  
  
-   **Auto -detection of data types**: The wizard will examine the uniqueness and distribution of column values and then recommend the best data type, and suggest a usage type for the data. You can override these suggestions by selecting values from a list.  
  
-   **Suggestions for variables**: You can click on a dialog box and start an analyzer that calculates correlations across the columns included in the model, and determines whether any columns are likely predictors of the outcome attribute, given the configuration of the model so far. You can override these suggestions by typing different values.  
  
-   **Feature selection**: Most algorithms will automatically detect columns that are good predictors and use those preferentially. In columns that contain too many values, *feature selection* will be applied, to reduce the cardinality of the data and improve the chances for finding a meaningful pattern. You can affect feature selection behavior by using model parameters.  
  
-   **Automatic cube slicing**: If your mining model is based on an OLAP data source, the ability to slice the model by using cube attributes is automatically provided. This is handy for crating models based on subsets of cube data.  
  
### Completing the Wizard  
 The last step in the wizard is to name the mining structure and the associated mining model. Depending on the type of model you created, you might also have the following important options:  
  
-   If you select **Allow drill through**, the ability to *drill through* is enabled in the model. With drillthrough, users who have the appropriate permissions can explore the source data that is used to build the model.  
  
-   If you are building an OLAP model, you can select the options, **Create a new data mining cube**, **or Create a data mining dimension**. Both these options make it easier to browse the completed model and drill through to the underlying data.  
  
 After you complete the Data Mining Wizard, you use Data Mining Designer to modify the mining structure and models, to view the accuracy of the model, view characteristics of the structure and models, or make predictions by using the models.  
  
 [Back to Top](#BKMK_Using_DM_Wizard)  
  
## Related Content  
 To learn more about the decisions you need to make when creating a data mining model, see the following links:  
  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)  
  
 [Content Types &#40;Data Mining&#41;](content-types-data-mining.md)  
  
 [Data Types &#40;Data Mining&#41;](data-types-data-mining.md)  
  
 [Feature Selection &#40;Data Mining&#41;](feature-selection-data-mining.md)  
  
 [Missing Values &#40;Analysis Services - Data Mining&#41;](missing-values-analysis-services-data-mining.md)  
  
 [Drillthrough on Mining Models](drillthrough-on-mining-models.md)  
  
## See Also  
 [Data Mining Tools](data-mining-tools.md)   
 [Data Mining Solutions](data-mining-solutions.md)  
  
  
