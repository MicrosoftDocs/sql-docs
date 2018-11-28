---
title: "Create a Relational Mining Structure | Microsoft Docs"
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
# Create a Relational Mining Structure
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Most data mining models are based on relational data sources. The advantages of creating a relational data mining model are that you can assemble ad hoc data and train and update a model without the complexity of creating a cube.  
  
 A relational mining structure can draw data from disparate sources. The raw data can be stored in tables, files, or relational database systems, so long as the data can be defined as part of data source view. For example, you should use a relational mining structure if your data is in Excel, a SQL Server data warehouse or SQL Server reporting database, or in external sources that are accessed via the OLE DB or ODBC providers.  
  
 This topic provides an overview of how to use the Data Mining Wizard to create a relational mining structure.  
  
 [Requirements](#BKMK_Relational_Structure)  
  
 [Process for Creating a Relational Mining Structure](#BKMK_Relational_Structure)  
  
 [How to Choose Data Sources](#BKMK_ChooseRelData)  
  
 [How to Specify Content Type and Data Type](#bkmk_ContentDataType)  
  
 [Why and How to Create a Holdout Data Set](#bkmk_Holdout)  
  
 [Why and How to Enable Drillthrough](#BKMK_DrillThru)  
  
## Requirements  
 First, you must have an existing data source. You can use the Data Source designer to set up a data source, if one does not already exist. For more information, see [Create a Data Source &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/create-a-data-source-ssas-multidimensional.md).  
  
 Next, use the Data Source View Wizard to assemble required data into a single data source view. For more information about how you can select, transform, filter, or manage data with data source views, see [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md).  
  
##  <a name="BKMK_Relational_Structure"></a> Overview of Process  
 Start the Data Mining Wizard, by right-clicking the **Mining Structures** node in Solution Explorer, and selecting **Add New Mining Structure**. The wizard guides you through the following steps to create the structure for a new relational mining model:  
  
1.  **Select the Definition Method**: Here you select a data source type, and choose **From relational database or data warehouse**.  
  
2.  **Create the Data Mining Structure**: Determine whether you will build just a structure, or a structure with a mining model.  
  
     You also choose an appropriate algorithm for your initial model. For guidance on which algorithm is best for certain tasks, see [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md).  
  
3.  **Select Data Source View**: Choose a data sources view to use in training your model. The data source view can also contain data used for testing, or unrelated data. You get to pick and choose which data is actually used in the structure and in the model. You can also apply filters to the data later on.  
  
4.  **Specify Table Types**: Select the table that contains the cases used for analysis. For some data sets, especially ones used for building market basket models, you might also include a related table, to use as a nested table.  
  
     For each table, you must specify the key, so that the algorithm knows how to identify a unique record, and related records if you have added a nested table.  
  
     For more information, see [Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md).  
  
5.  **Specify the Training Data**: On this page, you choose as the *case table*, which is the table that contains the most important data for analysis.  
  
     For some data sets, especially ones used for building market basket models, you might also include a related table. The values in that nested table will be handled as multiple values that are all related to a single row (or case) in the main table.  
  
6.  **Specify Columns Content and Data Types**: For each column that you use in the structure, you must choose both a *data type* and a *content type*.  
  
     The wizard will automatically detect possible data types, but you don't need to use the data type recommended by the wizard. For example, even if your data contains numbers, they might be representative of categorical data. Columns that you specify as keys are automatically assigned the correct data type for that particular model type. For more information, see [Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md) and [Data Types &#40;Data Mining&#41;](../../analysis-services/data-mining/data-types-data-mining.md).  
  
     The *content type* that you choose for each column that you use in the model tells the algorithm how the data should be processed.  
  
     For example, you might decide to discretize numbers, rather than use continuous values. You can also ask the algorithm to automatically detect the best content type for the column. For more information, see [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md).  
  
7.  **Create Testing Set**: On this page, you can tell the wizard how much data should be set aside for use in testing the model. If your data will support multiple models, it is a good idea to create a holdout data set, so that all models can be tested on the same data.  
  
     For more information, see [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md).  
  
8.  **Completing the Wizard**: On this page, you give a name to the new mining structure and the associated mining model, and save the structure and model.  
  
     You also can set some important options, depending on the model type. For example, you can enable drillthrough on the structure.  
  
     At this point the mining structure and its model are just metadata; you will need to process them both to get results.  
  
##  <a name="BKMK_ChooseRelData"></a> How to Choose Relational Data  
 Relational mining structures can be based on any data that is available through an OLE DB data source. If the source data is contained within multiple tables, you use a data source view to assemble the tables and columns that you need in one place.  
  
 If the tables include any one-to-many relationships-for example, you have multiple purchase records for each customer that you want to analyze-you can add both tables, and then use one table as the case table, linking data on the many side of the relationship as a nested table.  
  
 The data in a mining structure is derived from whatever is in the existing data source view. You can modify data as you need within the data source view, adding relationships or derived columns that might not be present in the underlying relational data. You can also create named calculations or aggregations within the data source view. These features are very handy if you do not have control over the arrangement of data in the data source, or if you want to experiment with different aggregations of data for your data mining models.  
  
 You do not have to use all the data that is available; you can pick and choose which columns to include in the mining structure. All models that are based on that structure then can use those columns, or you can flag certain columns as **Ignore** for a particular model. You can enable users of a data mining model to drill down from the results of the mining model to see additional mining structure columns that were not included in the mining model itself.  
  
##  <a name="bkmk_ContentDataType"></a> How to Specify Content Type and Data Type  
 The data type is pretty much the same as the data types you specify in SQL Server or other application interfaces: dates and times, numbers of different sizes, Boolean values, text and other discrete data.  
  
 However, content types are important for data mining and affect the outcome of analysis. The content type tells the algorithm what it should do with the data: should numbers be treated on a continuous scale, or binned? How many potential values are there? Is each value distinct? If the value is a key, what kind of key is it - does it indicate a date/time value, a sequence, or some other kind of key?  
  
 Note that the choice of data type can limit your choice of content types. For example, you cannot discretize values that are not numeric. If you cannot see the content type that you want, you can click **Back** to return to the data type page and try a different data type.  
  
 You need not worry too much about getting the content type wrong. It is very easy to create a new model and change the content type within the model, as long as the new content type is supported by the data type set in the mining structure. It is also very common to create multiple models using different content types, either as an experiment, or to meet the requirements of a different algorithm.  
  
 For example, if your data contains an income column, you could create two different models when using the Microsoft Decision Trees algorithm, and configure the column alternately as either continuous numbers or discrete ranges. However, if you added a model using the Microsoft Naïve Bayes algorithm, you would be forced to change the column to discretized values only, because that algorithm does not support continuous numbers.  
  
##  <a name="bkmk_Holdout"></a> Why and How to Split Data into Training and Testing Sets  
 Near the end of the wizard, you must decide whether to partition your data into training and testing sets. The ability to provision a randomly sampled portion of the data for testing is very convenient, as it ensures that a consistent set of test data is available for use with all mining models associated with the new mining structure.  
  
> [!WARNING]  
>  Note that this option is not available for all model types. For example, if you create a forecasting model, you won't be able to use holdout, because the time series algorithm requires that there be no gaps in data. For a list of the model types that support holdout data sets, see [Training and Testing Data Sets](../../analysis-services/data-mining/training-and-testing-data-sets.md).  
  
 To create this holdout data set, you specify the percentage of the data you want to use for testing. All remaining data will be used for training. Optionally, you can set a maximum number of cases to use for testing, or set a seed value to use in starting the random selection process.  
  
 The definition of the holdout test set is stored with the mining structure, so that whenever you create a new model based on the structure, the testing data set will be available for assessing the accuracy of the model. If you delete the cache of the mining structure, the information about which cases were used for training and which were used for testing will be deleted as well.  
  
##  <a name="BKMK_DrillThru"></a> Why and How to Enable Drillthrough  
 Almost at the very end of the wizard, you have the option to enable *drillthrough*. It is easy to miss this option, but it's an important one. Drillthrough lets you view source data in the mining structure by querying the mining model.  
  
 Why is this useful? Suppose you are viewing the results of a clustering model, and want to see the customers who were put into a specific cluster. By using drillthrough, you can view details such as contact information.  
  
> [!WARNING]  
>  To use drillthrough, you must enable it when you create the mining structure. You can enable drillthrough on models later, by setting a property on the model, but mining structures require that this option be set at the beginning. For more information, see [Drillthrough Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/drillthrough-queries-data-mining.md).  
  
## See Also  
 [Data Mining Designer](../../analysis-services/data-mining/data-mining-designer.md)   
 [Data Mining Wizard &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-wizard-analysis-services-data-mining.md)   
 [Mining Model Properties](../../analysis-services/data-mining/mining-model-properties.md)   
 [Properties for Mining Structure and Structure Columns](../../analysis-services/data-mining/properties-for-mining-structure-and-structure-columns.md)   
 [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md)  
  
  
