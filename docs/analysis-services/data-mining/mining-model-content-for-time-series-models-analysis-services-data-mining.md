---
title: "Mining Model Content for Time Series Models (Analysis Services - Data Mining) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Mining Model Content for Time Series Models (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  All mining models use the same structure to store their content. This structure is defined according to the data mining content schema rowset. However, within that standard structure, the nodes that contain information are arranged in different ways to represent various kinds of trees. This topic describes how the nodes are organized, and what each node means, for mining models that are based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm.  
  
 For an explanation of general mining model content that applies to all model types, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
 When reviewing this topic, you might find it useful follow along by browsing the contents of a time series model. You can create a time series model by completing the Basic Data Mining tutorial. The model you create in the tutorial is a mixed model that trains data by using both the ARIMA and ARTXP algorithms. For information about how to view the contents of a mining model, see [Data Mining Model Viewers](../../analysis-services/data-mining/data-mining-model-viewers.md).  
  
## Understanding the Structure of a Time Series Model  
 A time series model has a single parent node that represents the model and its metadata. Underneath that parent node, there are one or two time series trees, depending on the algorithm that you used to create the model.  
  
 If you create a mixed model, two separate trees are added to the model, one for ARIMA and one for ARTXP. If you choose to use only the ARTXP algorithm or only the ARIMA algorithm, you will have a single tree that corresponds to that algorithm. You specify which algorithm to use by setting the FORECAST_METHOD parameter. For more information about whether to use ARTXP, ARIMA, or a mixed model, see [Microsoft Time Series Algorithm](../../analysis-services/data-mining/microsoft-time-series-algorithm.md).  
  
 The following diagram shows an example of a time series data mining model that was created with the default settings, to create a mixed model. So that you can more easily compare the differences between the two models, here the ARTXP model is shown on the left side of the diagram and the ARIMA model is shown in the right side of the diagram.  Whereas ARTXP is a tree-like structure that splits into smaller and smaller branches, the structure created by the ARIMA algorithm is more like a pyramid built upwards from smaller components.  
  
 ![Structure of model content for time series models](../../analysis-services/data-mining/media/modelcontentstructure-ts.gif "Structure of model content for time series models")  
  
 The important point to remember is that information is arranged within the ARIMA and ARTXP trees in completely different ways, and you should consider the two trees as related only at the root node. Although the two representations are presented in one model for convenience, they should be treated as two independent models. ARTXP represents an actual tree structure, but ARIMA does not.  
  
 When you use the Microsoft Generic Model Content Tree Viewer to view a model that uses both ARIMA and ARTXP, the nodes for the ARTXP and ARIMA models are all presented as child nodes of the parent time series model. However, you can easily tell them apart by the labels applied to the nodes.  
  
-   The first set of nodes is labeled (All), and represents the results of analysis by the ARTXP algorithm.  
  
-   The second set of nodes is labeled ARIMA, and represents the results of analysis by the ARIMA algorithm.  
  
> [!WARNING]  
>  The name (All) on the ARTXP tree is retained only for backward compatibility. Prior to SQL Server 2008, the Time Series algorithm used a single algorithm for analysis, the ARTXP algorithm.  
  
 The following sections explain how the nodes are arranged within each of these model types.  
  
### Structure of an ARTXP Model  
 The ARTXP algorithm creates a model similar to a decision trees model. It groups predictable attributes and splits them whenever significant differences are found. Therefore, each ARTXP model contains a separate branch for each predictable attribute. For example, the Basic Data Mining tutorial creates a model that predicts the amount of sales for several regions. In this case, **[Amount]** is the predictable attribute and a separate branch is created for each region. If you had two predictable attributes, **[Amount]** and **[Quantity]**, a separate branch would be created for each combination of an attribute and a region.  
  
 The top node for the ARTXP branch contains the same information that is in a decision tree root node. This includes the number of children for that node (CHILDREN_CARDINALITY), the number of cases that meet the conditions of this node (NODE_SUPPORT), and a variety of descriptive statistics (NODE_DISTRIBUTION).  
  
 If the node does not have any children, this means that no significant conditions were found that would justify dividing the cases into further subgroups. The branch ends at this point and the node is termed a *leaf node*. The leaf node contains the attributes, coefficients, and values that are the building blocks of the ARTXP formula.  
  
 Some branches may have additional splits, similar to a decision trees model. For example, the branch of the tree that represents sales for the Europe region splits into two branches. A split occurs when a condition is found that causes a significant difference between the two groups. The parent node tells you the name of the attribute that caused the split, such as [Amount], and how many cases there are in the parent node. The leaf nodes provide more detail: the value of the attribute, such as [Sales] >10,000 vs. [Sales] < 10,000), the number of cases that support each condition, and the ARTXP formula.  
  
> [!NOTE]  
>  If you want to view the formulas, you can find the complete regression formula at the leaf node level, but not in an intermediate or root node.  
  
### Structure of an ARIMA Model  
 The ARIMA algorithm creates a single piece of information for each combination of a data series (such as **[Region]**) and a predictable attribute (such as **[Sales Amount]**)-the equation that describes the change of the predictable attribute over time.  
  
 The equation for each series is derived from multiple components, one for each periodic structure that was found in the data. For example, if you have sales data that is collected on a monthly basis, the algorithm might detect monthly, quarterly, or yearly periodic structures.  
  
 The algorithm outputs a separate set of parent and child nodes for each periodicity it finds. The default periodicity is 1, for a single time slice, and is automatically added into all models. You can specify possible periodic structures by entering multiple values in the PERIODICITY_HINT parameter. However, if the algorithm does not detect a periodic structure, it will not output results for that hint.  
  
 Each periodic structure that is output in the model content contains the following component nodes:  
  
-   A node for the *autoregressive order* (AR)  
  
-   A node for the *moving average* (MA)  
  
 For information about the meaning of these terms, see [Microsoft Time Series Algorithm](../../analysis-services/data-mining/microsoft-time-series-algorithm.md).  
  
 The *difference order* is an important part of the formula, and is represented in the equation. For more information about how the difference order is used, see [Microsoft Time Series Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md).  
  
## Model Content for Time Series  
 This section provides detail and examples only for those columns in the mining model content that have particular relevance for time series models.  
  
 For information about general-purpose columns in the schema rowset, such as MODEL_CATALOG and MODEL_NAME, or for explanations of mining model terminology, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
 MODEL_CATALOG  
 Name of the database where the model is stored.  
  
 MODEL_NAME  
 Name of the model.  
  
 ATTRIBUTE_NAME  
 The predictable attribute for the data series represented in the node. (The same value as for MSOLAP_MODEL_COLUMN.)  
  
 NODE_NAME  
 The name of the node.  
  
 Currently, this column contains the same value as NODE_UNIQUE_NAME, although this might change in future releases.  
  
 NODE_UNIQUE_NAME  
 The unique name of the node. The model parent node is always named **TS**.  
  
 **ARTXP:** Each node is represented by TS followed by a hexadecimal numeric value. The order of the nodes is unimportant.  
  
 For example, the ARTXP nodes directly under the TS tree might be numbered TS00000001-TS0000000b.  
  
 **ARIMA:** Each node in an ARIMA tree is represented by TA followed by a hexadecimal numeric value. The child nodes contain the unique name of the parent node followed by another hexadecimal number indicating the sequence within the node.  
  
 All ARIMA trees are structured exactly the same. Each root contains the nodes and naming convention exemplified in the following table:  
  
|ARIMA Node ID and type|Example of node name|  
|----------------------------|--------------------------|  
|ARIMA Root (27)|TA0000000b|  
|ARIMA Periodic Structure (28)|TA0000000b00000000|  
|ARIMA Auto Regressive (29)|TA0000000b000000000|  
|ARIMA Moving Average (30)|TA0000000b000000001|  
  
 NODE_TYPE  
 A time series model outputs the following node types, depending on the algorithm.  
  
 **ARTXP:**  
  
|Node Type ID|Description|  
|------------------|-----------------|  
|1 (Model)|Time series|  
|3 (Interior)|Represents an interior branch within an ARTXP time series tree.|  
|16 (Time series tree)|Root of ARTXP tree that corresponds to a predictable attribute and series.|  
|15 (Time series)|Leaf node in ARTXP tree.|  
  
 **ARIMA:**  
  
|Node Type ID|Description|  
|------------------|-----------------|  
|27 (ARIMA Root)|The top node of an ARIMA tree.|  
|28 (ARIMA Periodic Structure)|Component of an ARIMA tree that describes a single periodic structure.|  
|29 (ARIMA Autoregressive)|Contains a coefficient for a single periodic structure.|  
|30 (ARIMA Moving Average)|Contains a coefficient for a single periodic structure.|  
  
 NODE_CAPTION  
 A label or caption that is associated with the node.  
  
 This property is primarily for display purposes.  
  
 **ARTXP:** Contains the split condition for the node, displayed as a combination of attribute and value range.  
  
 **ARIMA:** Contains the short form of the ARIMA equation.  
  
 For information about the format of the ARIMA equation, see [Mining Legend for ARIMA](#bkmk_ARIMA_2).  
  
 CHILDREN_CARDINALITY  
 The number of direct children that the node has.  
  
 PARENT_UNIQUE_NAME  
 The unique name of the node's parent. NULL is returned for any nodes at the root level.  
  
 NODE_DESCRIPTION  
 A description in text of the rules, splits, or formulas in the current node.  
  
 **ARTXP:** For more information, see [Understanding the ARTXP Tree](#bkmk_ARTXP_1).  
  
 **ARIMA:** For more information, see [Understanding the ARIMA Tree](#bkmk_ARIMA_1).  
  
 NODE_RULE  
 An XML description of the rules, splits, or formulas in the current node.  
  
 **ARTXP:** The NODE_RULE generally corresponds to the NODE_CAPTION.  
  
 **ARIMA:** For more information, see [Understanding the ARIMA Tree](#bkmk_ARIMA_1).  
  
 MARGINAL_RULE  
 An XML description of the split or content that is specific to that node.  
  
 **ARTXP:** The MARGINAL_RULE generally corresponds to the NODE_DESCRIPTION.  
  
 **ARIMA:** Always blank; use NODE_RULE instead.  
  
 NODE_PROBABILITY  
 **ARTXP:** For tree nodes, always 1. For leaf nodes, the probability of reaching the node from the model root node.  
  
 **ARIMA:** Always 0.  
  
 MARGINAL_PROBABILITY  
 **ARTXP:** For tree nodes, always 1. For leaf nodes, the probability of reaching the node from the immediate parent node.  
  
 **ARIMA:** Always 0.  
  
 NODE_DISTRIBUTION  
 A table that contains the probability histogram of the node. In a time series model, this nested table contains all the components required to assemble the actual regression formula.  
  
 For more information about the node distribution table in an ARTXP tree, see [Understanding the ARTXP Tree](#bkmk_ARTXP_1).  
  
 For more information about the node distribution table in an ARIMA tree, see [Understanding the ARIMA Tree](#bkmk_ARIMA_1).  
  
 If you wish to see all the constants and other components composed into a readable format, use the [Time Series Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-time-series-viewer.md), click the node, and open the **Mining Legend**.  
  
 NODE_SUPPORT  
 The number of cases that support this node.  
  
 **ARTXP:** For the **(All)** node, indicates the total number of time slices included in the branch.  
  
 For terminal nodes, indicates the number of time slices that are included in the range that is described by NODE_CAPTION. The number of time slices in the terminal nodes always sums to the NODE_SUPPORT value of the branch **(All)** node.  
  
 **ARIMA:** A count of cases that support the current periodic structure. The value for support is repeated in all nodes of the current periodic structure.  
  
 MSOLAP_MODEL_COLUMN  
 The predictable attribute for the data series represented in the node. (The same value as for ATTRIBUTE_NAME.)  
  
 MSOLAP_NODE_SCORE  
 A numeric value that characterizes the information value of the tree or split.  
  
 **ARTXP:** Value is always 0.0 for nodes without a split. For nodes with a split, the value represents the interestingness score of the split.  
  
 For more information about scoring methods, see [Feature Selection &#40;Data Mining&#41;](../../analysis-services/data-mining/feature-selection-data-mining.md).  
  
 **ARIMA:**  The Bayesian Information Criterion (BIC) score of the ARIMA model. The same score is set on all the ARIMA nodes related to the equation.  
  
 MSOLAP_NODE_SHORT_CAPTION  
 **ARTXP:**  Same information as the NODE_DESCRIPTION.  
  
 **ARIMA:** Same information as the NODE_CAPTION: that is, the short form of the ARIMA equation.  
  
##  <a name="bkmk_ARTXP_1"></a> Understanding the ARTXP Tree  
 The ARTXP model clearly separates the areas of the data that are linear from the areas of the data that split on some other factor. Wherever the changes in the predictable attribute can be directly represented as a function of the independent variables, a regression formula is calculated to represent that relationship  
  
 For example, if there is a direct correlation between time and sales for most of the data series, each series would be contained within a time series tree (NODE_TYPE =16) that has no child nodes for each data series, only a regression equation. However, if the relationship is not linear, an ARTXP time series tree can split on conditions into child nodes, just like a decision tree model. By viewing the model content in the **Microsoft Generic Content Tree Viewer** you can see where the splits occur, and how it affects the trend line.  
  
 To better understand this behavior, you can review the time series model created in the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c). This model, based on the AdventureWorks data warehouse, does not use particularly complex data. Therefore, there are not many splits in the ARTXP tree. However, even this relatively simple model illustrates three different kinds of splits:  
  
-   The [Amount] trend line for the Pacific region splits on the time key. A split on the time key means that there is a change in the trend at a certain point in time. The trend line was linear only up to a certain point, and then the curve assumed a different shape. For example, one time series might continue until August 6, 2002, and another time series start after that date.  
  
-   The [Amount] trend line for the North America region splits on another variable. In this case, the trend for North America splits based on the value for the same model in the Europe region. In other words, the algorithm detected that when the value for Europe changes, the value for North America A also changes.  
  
-   The trend line for Europe region splits on itself.  
  
 What does each split mean? Interpreting the information conveyed by the model content is an art that requires a deep understanding of the data and its meaning in the business context.  
  
-   The apparent link between the trends for the North America and Europe regions may signify only that the data series for Europe has more entropy, which causes the trend for the North America to appear weaker. Or, there might be no significant difference in the scoring for the two, and the correlation could be accidental, based simply on computing Europe before computing North America. However, you might want to review the data and make sure whether the correlation is false, or investigate to see if some other factor might involved.  
  
-   The split on the time key means that there is a statistically significant change in the gradient of the line. This might have been caused by mathematical factors such as the support for each range, or the calculations of entropy required for the split. Thus, this split might not be interesting in terms of the model's meaning in the real world. However, when you review the time period indicated in the split, you might find interesting correlations that are not represented in the data, such a sales promotion or other event that began at that time and that may have affected the data.  
  
 If the data contained other attributes, you would very likely see more interesting examples of branching in the tree. For example, if you tracked weather information and used that as an attribute for analysis, you might see multiple splits in the tree that represent the complex interaction of sales and weather.  
  
 In short, data mining is useful for providing hints about where potentially interesting phenomena occur, but further investigation and the expertise of the business users is necessary to accurately interpret the worth of the information in context.  
  
### Elements of the ARTXP Time Series Formula  
 To view the complete formula for an ARTXP tree or branch, we recommend that you use the **Mining Legend** of the [Microsoft Time Series Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-time-series-viewer.md), which presents all of the constants in a readable format.  
  
-   [View the Formula for a Time Series Model &#40;Data Mining&#41;](../../analysis-services/data-mining/view-the-formula-for-a-time-series-model-data-mining.md)  
  
 The following section presents a sample equation and explains the basic terms.  
  
#### Mining Legend for an ARTXP Formula  
 The following example shows the ARTXP formula for one part of the model, as displayed in the **Mining Legend**. To view this formula, open the [Forecasting] model that you created in the Basic Data Mining Tutorial in the Microsoft Time Series viewer, click the **Model** tab, and select the tree for the R250: Europe data series.  
  
 To view the equation used for this example, click the node that represents the date series on or after 7/5/2003.  
  
 Example of tree node equation:  
  
 `Quantity = 21.322 -0.293 * Quantity(R250 North America,-7) + 0.069 * Quantity(R250 Europe,-1) + 0.023 * Quantity(R250 Europe,-3) -0.142 * Quantity(R750 Europe,-8)`  
  
 In this case, the value 21.322 represents the value that is predicted for Quantity as a function of the following elements of the equation.  
  
 For example, one element is `Quantity(R250 North America,-7)`. This notation means the quantity for the North America region at `t-7`, or seven time slices before the current time slice. The value for this data series is multiplied by the coefficient -0.293. The coefficient for each element is derived during the training process and is based on trends in the data.  
  
 There are multiple elements in this equation because the model has calculated that the quantity of the R250 model in the Europe region is dependent on the values of several other data series.  
  
#### Model Content for an ARTXP Formula  
 The following table shows the same information for the formula, using the contents of the relevant node as displayed in the [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](http://msdn.microsoft.com/library/751b4393-f6fd-48c1-bcef-bdca589ce34c).  
  
|ATTRIBUTE_NAME|ATTRIBUTE_VALUE|SUPPORT|PROBABILITY|VARIANCE|VALUETYPE|  
|---------------------|----------------------|-------------|-----------------|--------------|---------------|  
|Quantity(R250 Europe,y-intercept)|21.3223433563772|11|0|1.65508795539661|11 (Intercept)|  
|Quantity(R250 Europe,-1)|0.0691694140876526|0|0|0|7 (Coefficient)|  
|Quantity(R250 Europe,-1)|20.6363635858123|0|0|182.380682874818|9 (Statistics)|  
|Quantity(R750 Europe,-8)|-0.1421203048299|0|0|0|7 (Coefficient)|  
|Quantity(R750 Europe,-8)|22.5454545333019|0|0|104.362130048408|9 (Statistics)|  
|Quantity(R250 Europe,-3)|0.0234095979448281|0|0|0|7 (Coefficient)|  
|Quantity(R250 Europe,-3)|24.8181818883176|0|0|176.475304989169|9 (Statistics)|  
|Quantity(R250 North America,-7)|-0.292914186039869|0|0|0|7 (Coefficient)|  
|Quantity(R250 North America,-7)|10.36363640433|0|0|701.882534898676|9 (Statistics)|  
  
 As you can see from comparing these examples, the mining model content contains the same information that is available in the **Mining Legend**, but with additional columns for *variance* and *support*. The value for support indicates the count of cases that support the trend described by this equation.  
  
### Using the ARTXP Time Series Formula  
 For most business users, the value of the ARTXP model content is that it combines both a tree view and a linear representation of the data.  
  
-   If the changes in the predictable attribute can be represented as a linear function of the independent variables, the algorithm will automatically compute the regression equation and output that series in a separate node  
  
-   Whenever the relationship cannot be expressed as a linear correlation, the time series branches like a decision tree.  
  
 By browsing the model content in the [Microsoft Time Series Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-time-series-viewer.md) you can see where the split occurs, and how it affects the trend line.  
  
 If a direct correlation exists between time and sales for any part of the data series, the easiest way to get the formula is to copy the formula from the **Mining Legend**, and then paste it into a document or presentation to help explain the model. Alternatively, you could extract the mean, coefficient, and other information from the NODE_DISTRIBUTION table for that tree and use it to compute extensions of the trend. If the entire series exhibits a consistent linear relationship, the equation is contained in the (All) node. If there is any branching in the tree, the equation is contained in the leaf node.  
  
 The following query returns all the ARTXP leaf nodes from a mining model, together with the nested table, NODE_DISTRIBUTION, which contains the equation.  
  
```  
SELECT MODEL_NAME, ATTRIBUTE_NAME, NODE_NAME,  
NODE_CAPTION,   
(SELECT ATTRIBUTE_NAME, ATTRIBUTE_VALUE, [VARIANCE], VALUETYPE  
FROM NODE_DISTRIBUTION) as t  
FROM Forecasting.CONTENT  
WHERE NODE_TYPE = 15  
```  
  
##  <a name="bkmk_ARIMA_1"></a> Understanding the ARIMA Tree  
 Each structure in an ARIMA model corresponds to a *periodicity* or *periodic structure*. A periodic structure is a pattern of data that repeats throughout the data series. Some minor variation in the pattern is allowed, within statistical limits. Periodicity is measured according to the default time units that were used in the training data. For example, if the training data provides sales data for each day, the default time unit is one day, and all periodic structures are defined as a specified number of days.  
  
 Each period that is detected by the algorithm gets its own structure node. For example, if you are analyzing daily sales data, the model might detect periodic structures that represent weeks. In this case, the algorithm will create two periodic structures in the finished model: one for the default daily period, denoted as {1}, and one for weeks, indicated by {7}.  
  
 For example, the following query returns all the ARIMA structures from a mining model.  
  
```  
SELECT MODEL_NAME, ATTRIBUTE_NAME, NODE_NAME, NODE_CAPTION  
FROM Forecasting.CONTENT  
WHERE NODE_TYPE = 27  
```  
  
 Example results:  
  
|MODEL_NAME|ATTRIBUTE_NAME|NODE_NAME|NODE_TYPE|NODE_CAPTION|  
|-----------------|---------------------|----------------|----------------|-------------------|  
|Forecasting|M200 Europe:Quantity|TA00000000|27|ARIMA (1,0,1)|  
|Forecasting|M200 North America:Quantity|TA00000001|27|ARIMA (1,0,4) X (1,1,4)(6)|  
|Forecasting|M200 Pacific:Quantity|TA00000002|27|ARIMA (2,0,8) X (1,0,0)(4)|  
|Forecasting|M200 Pacific:Quantity|TA00000002|27|ARIMA (2,0,8) X (1,0,0)(4)|  
|Forecasting|R250 Europe:Quantity|TA00000003|27|ARIMA (1,0,7)|  
|Forecasting|R250 North America:Quantity|TA00000004|27|ARIMA (1,0,2)|  
|Forecasting|R250 Pacific:Quantity|TA00000005|27|ARIMA (2,0,2) X (1,1,2)(12)|  
|Forecasting|R750 Europe:Quantity|TA00000006|27|ARIMA (2,1,1) X (1,1,5)(6)|  
|Forecasting|T1000 Europe:Quantity|TA00000009|27|ARIMA (1,0,1)|  
|Forecasting|T1000 North America:Quantity|TA0000000a|27|ARIMA (1,1,1)|  
|Forecasting|T1`000 Pacific:Quantity|TA0000000b|27|ARIMA (1,0,3)|  
  
 From these results, which you can also browse by using the [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](http://msdn.microsoft.com/library/751b4393-f6fd-48c1-bcef-bdca589ce34c), you can tell at a glance which series are completely linear, which have multiple periodic structures, and what the discovered periodicities are.  
  
 For example, the short form of the ARIMA Equation for the M200 Europe series tells you that only the default, or daily, cycle was detected. The short form of the equation is provided in the NODE_CAPTION column.  
  
 However, for the M200 North America series, an additional periodic structure was found. The node TA00000001 has two child nodes, one with the equation, (1,0,4), and one with the equation, (1,1,4)(6). These equations are concatenated and presented in the parent node.  
  
 For each periodic structure, the model content also provides the *order* and the *moving average* as child nodes. For example, the following query retrieves the child nodes of one of the nodes listed in the previous example. Notice that the column, PARENT_UNIQUE_NAME, must be enclosed in brackets to distinguish it from the reserved keyword of the same name.  
  
```  
SELECT *   
FROM Forecasting.CONTENT  
WHERE [PARENT_UNIQUE_NAME] = ' TA00000001'  
```  
  
 Because this is an ARIMA tree, not an ARTXP tree, you cannot use the [IsDescendant &#40;DMX&#41;](../../dmx/isdescendant-dmx.md) function to return the child nodes of this periodic structure. Instead, you can use the attribute and node types to filter the results and return the child nodes that provide more detail about how the equation was built, including the moving averages and difference order.  
  
```  
SELECT MODEL_NAME, ATTRIBUTE_NAME, NODE_UNIQUE_NAME,  
NODE_TYPE,  NODE_CAPTION  
FROM Forecasting.CONTENT  
WHERE [MSOLAP_MODEL_COLUMN] ='M200 North America:Quantity'  
AND (NODE_TYPE = 29 or NODE_TYPE = 30)  
```  
  
 Example results:  
  
|MODEL_NAME|ATTRIBUTE_NAME|NODE_UNIQUE_NAME|NODE_TYPE|NODE_CAPTION|  
|-----------------|---------------------|------------------------|----------------|-------------------|  
|Forecasting|M200 North America:Quantity|TA00000001000000010|29|ARIMA {1,0.961832044807041}|  
|Forecasting|M200 North America:Quantity|TA00000001000000011|30|ARIMA {1,-3.51073103693271E-02,2.15731642954099,-0.220314343327742,-1.33151478258758}|  
|Forecasting|M200 North America:Quantity|TA00000001000000000|29|ARIMA {1,0.643565911081657}|  
|Forecasting|M200 North America:Quantity|TA00000001000000001|30|ARIMA {1,1.45035399809581E-02,-4.40489283927752E-02,-0.19203901352577,0.242202497643993}|  
  
 These examples illustrate that the further you drill down into the ARIMA tree, the more detail is revealed, but the important information is combined and presented in the parent node as well.  
  
### Time Series Formula for ARIMA  
 To view the complete formula for any ARIMA node, we recommend that you use the **Mining Legend** of the [Microsoft Time Series Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-time-series-viewer.md), which presents the autoregressive order, moving averages, and other elements of the equation already composed in a consistent format.  
  
-   [View the Formula for a Time Series Model &#40;Data Mining&#41;](../../analysis-services/data-mining/view-the-formula-for-a-time-series-model-data-mining.md)  
  
 This section presents a sample equation and explains the basic terms.  
  
####  <a name="bkmk_ARIMA_2"></a> Mining Legend for ARIMA Formula  
 The following example shows the ARIMA formula for one part of the model, as displayed in the Mining Legend. To view this formula, open the **Forecasting** model by using the **Microsoft Time Series viewer**, click the **Model** tab, select the tree for the **R250: Europe** data series, and then click the node that represents the date series on or after 7/5/2003. The mining legend composes all of the constants in a readable format, shown in this example:  
  
 ARIMA equation:  
  
`ARIMA ({1,1},0,{1,1.49791920964142,1.10640053499397,0.888873034670339,-5.05429403071953E-02,-0.905265316720334,-0.961908900643379,-0.649991020901922}) Intercept:56.8888888888889`  
  
 This equation is the long ARIMA format, which includes the values of the coefficients and the intercept. The short format for this equation would be {1,0,7}, where 1 indicates the period as a count of time slices, 0 indicates the term difference order, and 7 indicates the number of coefficients.  
  
> [!NOTE]  
>  A constant is calculated by Analysis Services for computing variance, but the constant itself is not displayed anywhere in the user interface. However, you can view the variance for any point in the series as a function of this constant if you select **Show Deviations,** in **Chart** view. The Tooltip for each data series shows the variance for a specific predicted point.  
  
#### Model Content for ARIMA Formula  
 An ARIMA model follows a standard structure, with different information contained in nodes of different types. To view the model content for the ARIMA model, change the viewer to the **Microsoft Generic Content Tree Viewer**, and then expand the node that has the attribute name, **R250 Europe: Quantity**.  
  
 An ARIMA model for a data series contains the basic periodic equation in four different formats, which you can choose from depending on the application.  
  
 **NODE_CAPTION:** Displays the short format of the equation. The short format tells you how many periodic structures are represented, and how many coefficients they have. For example, if the short format of the equation is `{4,0,6}`, the node represents one periodic structure with 6 coefficients. If the short format is something like `{2,0,8} x {1,0,0}(4)`, the node contains two periodic structures.  
  
 **NODE DESCRIPTION:** Displays the long format of the equation, which is also the form of the equation that appears in the **Mining Legend**. The long form of the equation is similar to the short form, except that the actual values of the coefficients are displayed instead of being counted.  
  
 **NODE_RULE:** Displays an XML representation of the equation. Depending on the node type, the XML representation can include single or multiple periodic structures. The following table illustrates how XML nodes are rolled up to higher levels of the ARIMA model.  
  
|Node Type|XML content|  
|---------------|-----------------|  
|27 (ARIMA Root)|Includes all periodic structures for the data series, and the content of all child nodes for each periodic structure.|  
|28 (ARIMA Periodic Structure)|Defines a single periodic structure, including its autoregressive term node and its moving average coefficients.|  
|29 (ARIMA Autoregressive)|Lists the terms for a single periodic structure.|  
|30 (ARIMA Moving Average)|Lists the coefficients for a single periodic structure.|  
  
 **NODE_DISTRIBUTION:** Displays terms of the equation in a nested table, which you can query to obtain specific terms. The node distribution table follows the same hierarchical structure as the XML rules. That is, the root node of the ARIMA series (NODE_TYPE = 27) contains the intercept value and the periodicities for the complete equation, which can include multiple periodicities, whereas the child nodes contain only information specific to a certain periodic structure or to the child nodes of that periodic structure.  
  
|Node Type|Attribute|Value type|  
|---------------|---------------|----------------|  
|27 (ARIMA Root)|Intercept<br /><br /> Periodicity|11|  
|28 (ARIMA Periodic Structure)|Periodicity<br /><br /> Auto Regressive order<br /><br /> Difference order<br /><br /> Moving average order|12<br /><br /> 13<br /><br /> 15<br /><br /> 14|  
|29 (ARIMA Autoregressive)|Coefficient<br /><br /> (complement of coefficient)|7|  
|30 (ARIMA Moving Average)|Value at t<br /><br /> Value at t-1<br /><br /> ...<br /><br /> Value at t-n|7|  
  
 The value for the *moving average order* indicates the number of moving averages in a series. Generally the moving average is calculated `n-1` times if there are `n` terms in a series, but the number can be reduced for easier computation.  
  
 The value for *autoregressive order* indicates the number of autoregressive series.  
  
 The value for *difference order* indicates how many times the series are compared, or differenced.  
  
 For an enumeration of the possible value types, see <xref:Microsoft.AnalysisServices.AdomdServer.MiningValueType>.  
  
### Using the ARIMA Tree Information  
 If you use predictions that are based on the ARIMA algorithm in a business solution, you might want to paste the equation into a report to demonstrate the method that was used to create the prediction. You can use the caption to present the formulas in short format, or the description to present the formulas in long format.  
  
 If you are developing an application that uses time series predictions, you might find it useful to obtain the ARIMA equation from the model content and then make your own predictions. To obtain the ARIMA equation for any particular output, you can query the ARIMA root for that particular attribute directly, as shown in the previous examples.  
  
 If you know the ID of the node that contains the series you want, you have two options to retrieve the components of the equation:  
  
-   Nested table format: Use a DMX query or query via OLEDB client.  
  
-   XML representation: Use an XML query.  
  
## Remarks  
 It can be difficult to retrieve information from an ARTXP tree, because information for each split is in a different place within the tree. Therefore, with an ARTXP model, you must get all the pieces and then do some processing to reconstitute the complete formula. Retrieving an equation from an ARIMA model is easier because the formula has been made available throughout the tree. For information about how to create a query to retrieve this information, see [Time Series Model Query Examples](../../analysis-services/data-mining/time-series-model-query-examples.md).  
  
## See Also  
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md)   
 [Microsoft Time Series Algorithm](../../analysis-services/data-mining/microsoft-time-series-algorithm.md)   
 [Time Series Model Query Examples](../../analysis-services/data-mining/time-series-model-query-examples.md)   
 [Microsoft Time Series Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)  
  
  
