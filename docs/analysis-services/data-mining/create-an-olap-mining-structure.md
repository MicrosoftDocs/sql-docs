---
title: "Create an OLAP Mining Structure | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create an OLAP Mining Structure
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  There are many advantages to creating a data mining model based on an OLAP cube or other multidimensional data store. An OLAP solution already contains huge amounts of data that is well organized, cleaned and properly formatted; however, the complexity of the data is such that users are unlikely to find meaningful patterns by ad hoc exploration. Data mining offers the ability to discover new correlations and provide actionable insight.  
  
 This topic describes how to create an OLAP mining structure, based on a dimension and related measures in an existing multidimensional solution.  
  
 [Requirements](#bkmk_Reqs)  
  
 [Overview of OLAP Data Mining Process](#bkmk_Overview)  
  
 [Scenarios for Using Data Mining in OLAP Solutions](#bkmk_OLAP_Scenarios)  
  
 [Filters](#bkmk_Filters)  
  
 [Using Nested Tables](#bkmk_Nested)  
  
 [Data Mining Dimensions](#bkmk_DMDimension)  
  
##  <a name="bkmk_Reqs"></a> Requirements for OLAP Mining Structure and Models  
 If you are designing an OLAP mining model, your data source already exists, in the database that was used to build the cube. You cannot connect to a remote cube and build data mining objects; the cube objects must be available within the same solution as the database as the mining structure that you will build.  
  
 If you do not have the original project files, or do not wish to alter them, you can use the option in Visual Studio, **Import from Server (Multidimensional or Data Mining)**, to get a copy of the metadata and solution objects. You can then modify the deployment target, edit data sources, and work with the cube objects without affecting the existing objects.  
  
 For more information, see [Import a Data Mining Project using the Analysis Services Import Wizard](../../analysis-services/data-mining/import-a-data-mining-project-using-the-analysis-services-import-wizard.md).  
  
##  <a name="bkmk_Overview"></a> Overview of OLAP Data Mining Process  
 Start the Data Mining Wizard by right-clicking the **Mining Structures** node in Solution Explorer, and selecting  **New Mining Structure**. The wizard guides you through the following steps to create the structure for a new structure and model:  
  
1.  **Select the Definition Method**: Here you select a data source type, and choose **From existing cube**.  
  
    > [!NOTE]  
    >  The OLAP cube that you use as a source must exist within the same database as the mining structure, as described above. Also, you cannot use a cube created by the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel add-in as a source for data mining.  
  
2.  **Create the Data Mining Structure**: Determine whether you will build just a structure, or a structure with a mining model.  
  
     You must also choose an appropriate algorithm for analyzing your data. For guidance on which algorithm is best for certain tasks, see  HYPERLINK "ms-help://SQL111033/as_1devconc/html/ed1fc83b-b98c-437e-bf53-4ff001b92d64.htm" Data Mining Algorithms (Analysis Services - Data Mining).  
  
3.  **Select the Source Cube Dimension**: This step is the same as selecting a data source. You need to choose the single dimension that contains the most important data used for in training your model. You can add data from other dimensions later, or filter the dimension.  
  
4.  **Select the Case Key**: Within the dimension that you just selected, choose an attribute (column) to serve as the unique identifier for your case data.  
  
     Typically a column will be pre-selected for you, but you can change the column if in fact there are multiple keys.  
  
5.  **Selecting Case Level Columns**: Here you choose the attributes from the selected dimension, and the related measures, that are relevant to your analysis. This step is equivalent to selecting columns from a table.  
  
     The wizard automatically includes for your review and selection any measures that were created using attributes from the selected dimension.  
  
     For example, if your cube contains a measure that calculates freight cost based on the customer's geographical location, and you chose the Customer dimension as your main data source for modeling, the measure will be proposed as a candidate for adding to the model. Beware of adding too many measures that are already directly based on attributes, as there is already one implicit relationship between the columns, as defined in the measure formula, and the strength of this (expected) correlation can obscure other relationships that you might otherwise discover.  
  
6.  **Specify Mining Model Column Usage**: For each attribute or measure that you added to the structure, you must specify whether the attribute should be used for prediction, or used as input. If you do not select either of these options, the data will be processed but will not be used for analysis; however, it will be available as background data in case you later enable drillthrough.  
  
7.  **Add nested tables**: Click to add related tables. In the **Select a Measure Group Dimension** dialog box, you can choose a single dimension from among the dimensions that are related to the current dimension.  
  
     Next, you use the **Select a Nested Table Key** dialog box to define how the new dimension is related to the dimension that contains the case data.  
  
     Use the **Select Nested Table Columns** dialog box to choose the attributes and measures from the new dimension that you want to use in analysis. You must also specify whether the nested attribute will be used for prediction.  
  
     After you have added all the nested attributes you might need, return to the page, **Specify Mining Model Column Usage**, and click **Next**.  
  
8.  **Specify Columns Content and Data Type**: By this point, you have added all the data that will be used for analysis, and must specify the *data type* and *content type* for each attribute.  
  
     In an OLAP model, you do not have the option to automatically detect data types, because the data type is already defined by the multidimensional solution and cannot be changed. Keys are also automatically identified. For more information, see  [Data Types &#40;Data Mining&#41;](../../analysis-services/data-mining/data-types-data-mining.md).  
  
     The *content type* that you choose for each column that you use in the model tells the algorithm how the data should be processed. For more information, see [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md).  
  
9. **Slicing the source cube**: Here you can define filters in a cube to select just a subset of data and train models that are more targeted.  
  
     You filter a cube by choosing the dimension to filter on, selecting the level of the hierarchy that contains the criteria you want to use, and then typing a condition to use as the filter.  
  
10. **Create Testing Set**: On this page, you can tell the wizard how much data should be set aside for use in testing the model. If your data will support multiple models, it is a good idea to create a holdout data set, so that all models can be tested on the same data.  
  
     For more information, see [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md).  
  
11. **Completing the Wizard**: On this page, you give a name to the new mining structure and the associated mining model, and save the structure and model.  
  
     On this page, you can also can set the following options:  
  
    -   **Allow drillthrough**  
  
    -   **Create mining model dimension**  
  
    -   **Create cube using mining model dimension**  
  
     To learn more about these options, see the section later in this topic, [Understanding Data Mining Dimensions and Drillthrough](#bkmk_DMDimension).  
  
 At this point the mining structure and its model are just metadata; you will need to process them both to get results.  
  
##  <a name="bkmk_OLAP_Scenarios"></a> Scenarios for Use of Data Mining with OLAP Data  
 OLAP cubes frequently contain so many members and dimensions that it can be difficult to know where to begin with data mining. To help identify the patterns that the cubes contain, typically you identify a single dimension of interest, and then begin to explore patterns related to that dimension. The following table lists several common OLAP data mining tasks, describes sample scenarios in which you might apply each task, and identifies the data mining algorithm to use for each task.  
  
|Task|Sample scenario|Algorithm|  
|----------|---------------------|---------------|  
|Group members into clusters|Segment a customer dimension based on customer member properties, the products that the customers buy, and the amount of money that the customers spend.|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering Algorithm|  
|Find interesting or abnormal members|Identify interesting or abnormal stores in a store dimension based on sales, profit, store location, and store size.|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees Algorithm|  
|Find interesting or abnormal cells|Identify store sales that go against typical trends over time.|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series Algorithm|  
|Find correlations|Identify factors that are related to server downtime, including region, machine type, OS, or purchase date.|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Naïve Bayes algorithm|  
  
##  <a name="bkmk_Filters"></a> Slicing a Cube vs. Filtering Models  
 Slicing the cube while you are building a model is like creating a filter on a relational mining model. In a relational model, the filter on the data source is defined as a WHERE clause on a SQL statement; in a cube, you use the editor to create filter statements using MDX.  
  
 For example, a cube might contains information about purchases of products worldwide, but for your marketing campaign, you want to create a model based on analysis of female customers over 30 who live in the United Kingdom.  
  
 In this scenario, you would create two filters:  
  
-   For the first filter, you would choose the Geography dimension, choose the hierarchy for Region, and then use the **Filter Expression** list to choose "United Kingdom" from the possible values.  
  
-   For the second filter, you would choose the Customer dimension, select the Gender attribute, and select "Female" from the list of attribute values.  
  
 After the mining structure has been created, you can modify both the definition of the cube data and the filter criteria. For more information, see [Filters for Mining Models](~/analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md).  
  
 Both the **Mining Structure** tab and the **Mining Model** tab provide an option to add a filter to an existing mining structure, by clicking **Define a Cube Slice**. The **Slice Cube** dialog box helps you build a valid MDX filter expression by choosing value from dropdown lists.  
  
> [!WARNING]  
>  Note that the interface for designing and browsing cubes has been changed in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [Browse data and metadata in Cube](../../analysis-services/multidimensional-models/browse-data-and-metadata-in-cube.md).  
  
 You can add as many filters on the cube as are required to return the data that you need for the mining model. You can also define slices on individual cube slices. For example, if your structure contains two nested tables that are based on products, you could slice one table on March 2004 and the other table on April 2004. The resulting model could then be used to predict purchases made in April based on the purchases that were made in March.  
  
##  <a name="bkmk_Nested"></a> Using Nested Tables in an OLAP Mining Model  
 When you use the Data Mining Wizard to build a model based on cube data, you can add nested tables by specifying the names of related dimensions and then choosing the attributes or measures to add to the model  
  
 For example, if the main dimension used for case data is Customer, you might add as a related dimension the Products dimension, because you expect that a customer might have ordered multiple products over time, and the cube already links each customer to the many products via the order fact tables.  
  
 You add nested tables in the **Specify Mining Model Column Usage** page of the wizard, by clicking **Add Nested Tables**. A dialog box opens that guides you through process of choosing a related dimension, as well as any measures. The case and nested dimensions must be related by a foreign key, and measures must use one of the attributes that are already included in the case or nested tables. Unfortunately, these restrictions really don't do much to narrow the scope, so you must be careful to select only those attributes that are useful for modeling.  
  
 For each attribute or measure that you add to the nested table, you must specify whether the nested attribute will be used for prediction or not, by selecting **Predictable** or **Input** in the **Select Nested Table Columns** dialog box. If you do not select either of these options, the data will be added to the mining structure but not used for analysis.  
  
 For each attribute and measure, you must also specify whether the attribute is discrete, discretized, or continuous. The wizard will preselect a default based on the data type of the attribute, but you might need to change these, depending on the algorithm requirements. If you choose a content type that is not compatible with the algorithm you have chosen (for example, you use a continuous numeric type with a Naïve Bayes model), you won't get an error message till you try to process the model.  
  
 When you are done setting these options, the wizard adds the nested table to the case table. The default name for the nested table is the nested dimension name, but you can rename the nested table and its columns. You can repeat this process to add multiple nested tables to the mining structure.  
  
 The ability to use nested table data like this is a feature of SQL Server data mining that is particularly powerful, and in a cube, there are almost limitless possibilities for using related subsets of data.  
  
##  <a name="bkmk_DMDimension"></a> Understanding Data Mining Dimensions and Drillthrough  
 The option, **Allow drillthrough**, lets you run queries against the underlying cube data while you are browsing the model. The data is not contained in the new data mining dimension, but the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database can use the data bindings to retrieve the information from the source cube.  
  
 The option, **Create mining model dimension**, lets you generate a new dimension within the existing cube that contains the patterns discovered by the algorithm. The hierarchy within the new dimension is determined largely by the model type. For example, the representation of a clustering model is fairly flat, with the (All) node at the top of the hierarchy and each cluster in the next level. In contrast, the dimension that is created for a decision tree model might have a very deep hierarchy, representing the branching of the tree.  
  
 The option, **Create cube using mining model dimension**, lets you export the new data mining dimension into a new cube. Any objects that are required for drillthrough on the data mining dimension will be included automatically.  
  
> [!WARNING]  
>  Only these model types support the creation of data mining dimensions: models based on the Microsoft Clustering algorithm, the Microsoft Decision Trees algorithm, or the Microsoft Association algorithm.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md)   
 [Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md)   
 [Mining Model Properties](../../analysis-services/data-mining/mining-model-properties.md)   
 [Properties for Mining Structure and Structure Columns](../../analysis-services/data-mining/properties-for-mining-structure-and-structure-columns.md)  
  
  
