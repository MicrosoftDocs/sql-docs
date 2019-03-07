---
title: "Create a New OLAP Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining structures [Analysis Services], OLAP"
  - "mining structures [Analysis Services], creating"
  - "OLAP [Analysis Services], mining models"
ms.assetid: 368f4273-a016-4748-bcb6-505a3e745af3
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a New OLAP Mining Structure
  You can use the Data Mining Wizard in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to create a mining structure that uses data from a multidimensional model. Mining models that are based on OLAP cubes can use the column and values in fact tables, dimensions, and measure groups as attributes for analysis.  
  
### To create a new OLAP mining structure  
  
1.  In Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], right-click the **Mining Structures** folder in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, and then click **New Mining Structure** to open the Data Mining Wizard.  
  
2.  On the **Welcome to the Data Mining Wizard** page, click **Next**.  
  
3.  On the **Select the Definition Method** page, select **From existing cube**, and then click **Next**.  
  
     If you get an error with the message, Unable to retrieve a list of supported data mining algorithms, open the **Project Properties** dialog box and verify that you have specified the name of an Analysis Services instance that supports multidimensional models. You cannot create mining models on an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that supports tabular modeling.  
  
4.  On the **Create the Data Mining Structure** page, decide whether you will create a mining structure only, or a mining structure plus one related mining model. Generally it is easier to create a mining model at the same time, so that you can be prompted to include necessary columns.  
  
     If you will create a mining model, select the data mining algorithm that you want to use, and then click **Next**. For more information about how to choose an algorithm, see [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md).  
  
5.  On the **Select the Source Cube Dimension** page, under **Select a Source Cube Dimension**, locate the dimension that contains the majority of your case data.  
  
     For example, if you are trying to identify customer groupings, you might choose the Customer dimension; if you are trying to analyze purchases across transactions, you might choose the Internet Sales Order Details dimension. You are not restricted to using only the data in this dimension, but it should contain important attributes to use in analysis.  
  
     Click **Next**.  
  
6.  On the **Select the Case Key** page, under **Attributes**, select the attribute that will be the key of the mining structure, and then click **Next**.  
  
     Typically the attribute that you use as key for the mining structure is also a key for the dimension and will be pre-selected.  
  
7.  On the **Select Case Level Columns** page, under **Related Attributes and Measures**, select the attributes and measures that contain values you want to add to the mining structure as case data. Click **Next**.  
  
8.  On the **Specify Mining Model Column Usage** page, under **Mining model structure**, first set the predictable column, and then choose columns to use as inputs.  
  
    -   Select the checkbox in the leftmost column to include the data in the mining structure. You can include columns in the structure that you will use for reference, but not use them for analysis.  
  
    -   Select the checkbox in the **Input** column to use the attribute as a variable in analysis.  
  
    -   Select the checkbox in the **Predict** column only for predictable attributes.  
  
     Note that columns you have designated as keys cannot be used for input or prediction.  
  
     Click **Next**.  
  
9. On the **Specify Mining Model Column Usage** page, you can also add and remove nested tables to the mining structure, using **Add Nested Tables** and **Nested Tables**.  
  
     In an OLAP mining model, a nested table is another set of data within the cube that has a one-to-many relationship with the dimension that represents the case attributes. Therefore, when the dialog box opens, it pre-selects measure groups that are already related to the dimension you selected as the case table. At this point, you would choose a different dimension that contains additional information useful for analysis.  
  
     For example, if you are analyzing customers, you would use the [Customer] dimension as the case table. For the nested table, you might add the reason customers cited when making a purchase, which is included in the [Sales Reason] dimension.  
  
     If you add nested data, you must specify two additional columns:  
  
    -   The key of the nested table: This should be pre-selected on the page, **Select Nested Table Key**.  
  
    -   The attributes or attributes to use for analysis: The page, **Select Nested Table Columns**, provides a list of measures and attributes in the nested table selection.  
  
        -   For each attribute that you include in the model, check the box in the left column.  
  
        -   If you want to use the attribute for analysis only, check **Input**.  
  
        -   If you want to include the column as one of the predictable attributes for the model, select **Predict**.  
  
        -   Any item that you include in the structure but do not specify as an input or predictable attribute is added to the structure with the flag `Ignore`; this means that the data is processed when you build the model but is not used in analysis, and is available only for drillthrough. This can be handy if you want to include details such as customer names but don't want to use them in analysis.  
  
     Click **Finish** to close the part of the wizard that works with nested tables. You can repeat the process to add multiple nested columns.  
  
10. On the **Specify Columns' Content and Data Type** page, under **Mining model structure**, set the content type and data type for each column.  
  
    > [!NOTE]  
    >  OLAP mining models do not support using the **Detect** feature to automatically detect whether a column contains continuous or discrete data.  
  
     Click **Next**.  
  
11. On the **Slice Source Cube** page, you can filter the data that is used to create the mining structure.  
  
     Slicing the cube lets you restrict the data that is used to build the model. For example, you could build separate models for each region by slicing on the Geography hierarchy and  
  
    -   **Dimension**: Choose a related dimension from the dropdown list.  
  
    -   **Hierarchy**:  Select the level of the dimension hierarchy at which you want to apply the filter. For example, if you are slicing by the [Geography] dimension, you would choose a hierarchy level such as [Region Country Name] .  
  
    -   **Operator**: Choose an operator from the list.  
  
    -   **Filter Expression**: Type a value or expression to serve as the filter condition, or use the dropdown list to select a value from the list of members at the specified level of the hierarchy.  
  
         For example, if you selected [Geography] as the dimension and [Region Country Name] as the hierarchy level, the dropdown list contains all the valid countries that you can use as a filter condition. You can make multiple selections. As a result, the data in the mining structure will be limited to cube data from these geographical areas.  
  
    -   **Parameters**: Ignore this check box. This dialog box supports multiple cube filtering scenarios and this option is not relevant to building a mining structure.  
  
     Click **Next**.  
  
12. On the **Split data into training and testing sets** page, specify a percentage of the mining structure data to reserve for testing, or specify the maximum number of test cases. Click **Next**.  
  
     If you specify both values, the limits are combined to use whichever is lowest.  
  
13. On the **Completing the Wizard** page, provide a name for the new OLAP mining structure and the initial mining model.  
  
14. Click **Finish**.  
  
15. On the **Completing the Wizard** page, you also have the option to create a mining model dimension and/or a cube using the mining model dimension. These options are supported only for models built using the following algorithms:  
  
    -   Microsoft Clustering algorithm  
  
    -   Microsoft Decision Trees algorithm  
  
    -   Microsoft Association Rules algorithm  
  
     **Create mining model dimension**: Select this check box and provide a type name for the mining model dimension. When you use this option, a new dimension is created within the original cube that was used to build the mining structure. You can use this dimension to drill down and conduct further analysis. Because the dimension is located within the cube, the dimension is automatically mapped to the case data dimension.  
  
     **Create cube using mining model dimension**: Select this check box, and provide a name for the new cube. When you use this option, a new cube is created that contains both the existing dimensions that were used in building the structure, and the new data mining dimension that contains the results from the model.  
  
## See Also  
 [Mining Structure Tasks and How-tos](mining-structure-tasks-and-how-tos.md)  
  
  
