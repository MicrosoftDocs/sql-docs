---
title: "Choosing Data for Data Mining | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "content type [data mining]"
  - "nested tables"
  - "key [data mining]"
  - "continuous values"
  - "key sequence [data mining]"
  - "table data type"
  - "key time [data mining]"
  - "discrete values"
  - "discretized"
ms.assetid: 7c72d80e-913c-4bbe-b258-444294a78838
author: minewiskan
ms.author: owend
manager: craigg
---
# Choosing Data for Data Mining
  As you start data mining, you might ask "How much data do I need?" or "Are there any special requirements I should know about when cleaning or formatting my data?"  
  
 In particular, people new to data mining often run into problems with Excel data, such as needing to format data consistently within columns, cleaning up missing values, or binning numbers. This section also lists data requirements for specific kinds of models.  
  
 [Choosing Data](#bkmk_ChoosingData)  
  
 [Common Data Problems](#bkmk_CommonDataProblems)  
  
 [Other Data Requirements](#bkmk_OtherRequirements)  
  
##  <a name="bkmk_ChoosingData"></a> Choosing Data  
 Selection of the data used for analysis is perhaps the most important part of the data mining process, more so even than the selection of an algorithm. The reason is that data mining is generally not hypothesis-driven, but data-driven. Rather than select and test variables in advance, as you might with traditional statistical modeling, data mining can take data and discover new correlations (or fail to discover any patterns at all). The quality and amount of your data can have a significant effect on results.  
  
 In general, observe the following rules:  
  
-   Get as much clean data as possible.  
  
-   Conduct data profiling before you try any models. You need to understand your data before you can derive meaning from it. At minimum:  
  
    1.  Use the tools in the add-ins to find your maximum and minimum values, the most common values, and average values.  
  
    2.  Fill in missing values. The add-ins (as well as some algorithms) provide tools for imputing missing values.  
  
    3.  Correct bad data whenever possible. Data mining projects often serve as the impetus for new data quality initiatives.  
  
-   Try building a test model and find data problems that way. As you look at the results, you might find, for example, that sales projections are based on anomalous data due to a currency conversion error.  
  
-   Try casting your data into different formats, or try bucketing numbers. Patterns often emerge when data is transformed.  
  
     For example, the service level at the call center might be affected by the day of the week, which you would not see if you were using only the datetime values. Forecasts might be better when generated on 10-day cycles rather than weekly or daily units.  
  
-   Put numbers in appropriate bins, to reduce the number of possible values for analysis.  
  
-   Create multiple versions of your data and build multiple models.  
  
 For additional tips on how to select, modify, and review data, see [Checklist of Preparation for Data Mining](checklist-of-preparation-for-data-mining.md).  
  
### How Much Data Do I Need?  
 A rule of thumb is to never have less than 50-100 rows of data for the simplest models types and scenarios. For example, if you are predicting a single attribute using a Naïve Bayes model and the data set is well-formed, you might be able to generate fairly accurate predictions using 50-100 rows of data.  
  
 For association models, you typically need much more data - a thousand rows might not suffice if you are analyzing many attributes, such as associations among products. If your data set is too big or too small, you can sometimes achieve better results by collapsing rows into categories. For example, instead of analyzing associations among individual products, you could categorize the products.  
  
 If you have a data set of a reasonable size, focus more on data quality rather than adding more and more data. After a point, all the patterns that are statistically valid will have been found, and adding more data does not improve their validity. Conversely, as you add more data sometimes you can introduce accidental correlations.  
  
### Discrete vs. Continuous Numbers  
 A *discrete* column contains a finite number of values. For example, text is always treated as discrete values.  
  
 There are some important attributes to discrete values. For example, if you treat numbers as discrete, no order is implied among them, and you cannot average or sum the numbers. Telephone area codes are a good example of discrete numeric data that you would never use to perform mathematical operations.  
  
 Discrete values are sometimes referred to as categorical values, because you can group a set of data by them, whereas you cannot with numbers arranged in an infinite series.  
  
 You might also decide to treat numbers as discrete when the values are clearly separated, and there is no possibility of fractional values, or fractional values are not useful.  
  
 *Continuous* numeric data can contain an infinite number of fractional values. An income column is an example of a continuous attribute column. If you specify that a column is numeric, every value in that column must be a number, except for nulls. Note that in Excel, timestamps and any other date-time representation that can be converted to an [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data type can be considered.  
  
 **Converting Numbers to Categorical Variables**  
  
 Just because a column contains numbers does not mean that you should treat them as continuous numbers. *Discretization* provides many advantages for analysis. One is that the problem space is reduced. Another is that sometimes numbers are not the appropriate way to express a result.  
  
 For example, the number of children per household can be treated either as a continuous or discrete value. Since it is not possible to have 2.5 children in the household, and households with 3 or more children can behave very differently from households with 2 children, you might get better results by treating this number as a category. However, if you are building a regression model or otherwise require an average (such as 1.357 children per household), then you would use a continuous number data type.  
  
 It is not possible to create a mining model that has continuous data and then treat the column as discrete later. The two data sets must be processed differently and are handled on the backend as separate mining structures. If you are unsure of the right way to handle the data, you should create separate models that handle the data differently. In any case, this is a good way to get a different perspective on your data, and perhaps different results.  
  
 **Converting Numbers to Text**  
  
 Very often values that should be discrete, such as Male and Female, are represented as numeric data, using the labels 1 and 2. Typically this coding is performed to simplify data entry, or to save storage space in a database, but the coding can lead to ambiguity about the nature or meaning of the values. Moreover, because discrete values are stored as numbers, as you move data between applications you may encounter data type conversion errors, and the values might be calculated or otherwise treated as continuous. To prevent such problems, before beginning data mining, you should convert the numeric labels back to discrete text labels.  
  
 **Binning Numbers**  
  
 Although all numbers in principle are infinite and are therefore continuous, when you are modeling information you might find it more effective to *discretize* or *bin* the available values.  
  
 You can bin data in many ways:  
  
-   Specify a finite number of buckets and let the algorithm sort the values into buckets.  
  
-   Pre-group them yourself, by creating some set of groupings that either has business meaning or is easier to work with. With this approach, you often miss the true distribution of values, but the ranges are easier for users to read.  
  
-   Let the algorithm determine both the optimum number of buckets and the distribution of values. This is the default in most tools, but you can override these defaults in the **Data Mining** toolbar wizards.  
  
-   Approximating values to a central mean or representative value.  
  
##  <a name="bkmk_CommonDataProblems"></a> Common Data Problems  
  
### Excel Number Formats  
 Excel is an easy tool to use because it is forgiving - you can put just about any kind of data anywhere! However, before you begin to look for patterns and analyze correlations, you need to impose some structure or constraints on your data.  
  
 By default, when you import numeric data into [!INCLUDE[msCoName](../includes/msconame-md.md)] Office Excel, the numbers are stored in a decimal format with two decimal places. If this is not an appropriate number format, you should change to another numeric format, or change the number of decimal places.  
  
 One option is to use the [Relabel](relabel-sql-server-data-mining-add-ins.md) tool to change the way that numbers are displayed or grouped.  
  
 However, if your data is too complex to process with the **Relabel** tool, you can use the numeric functions in Excel to convert your data to discrete ranges, save that result into a separate column, and then use the discretized column for classification instead.  
  
 For example, if you are analyzing race results and want to group racers by their finish times in minutes, you can round up to the nearest minute and save that rounded value to a new column. You could also extract only the minute value by using the `MINUTE` function, and then save that value to a new column for use in analysis.  
  
### Discretizing Numbers and Dates in Excel  
 By default, numeric data in Excel is stored as a `Double`. Dates and times also are stored in a numeric format. If you need to discretize numbers or dates for data mining, you should add new columns before you build your data mining model, or convert dates and numbers to another format beforehand.  
  
### Scientific Number Formats  
 The data mining tools often output probabilities in scientific notation, to represent numbers that are very large or very small. If you are not familiar with scientific notation, you can easily display these numbers in another format by simply changing the cell formatting.  
  
##### To change scientific notation to a decimal numeric format  
  
1.  In the Excel data table, highlight the column or cell that contains the number in scientific notation.  
  
2.  Right-click and select **Format cells** from the shortcut menu.  
  
3.  In the **Category** list, select **Number**.  
  
4.  Increase the number of decimal places. A probability that is represented in scientific notation is generally very small.  
  
     Only the display of the number is changed, not the underlying value.  
  
### Working with Dates and Times  
 When you have dates in an Excel table and use the column either as input or for prediction, you might receive unexpected results, depending on how the date or time information is formatted. For example, when you use **Detect Categories** or **Classify** and include a column that contains dates, the dates are categorized as numbers with many decimal places. This is not an error; it is an accurate representation of the underlying data. The data mining algorithm works with the underlying storage format, not the display format.  
  
 If you have difficulty working with dates and want to analyze dates using such common-sense groupings as month or day, you can use the DATE functions in Excel to extract the year, month, or day into a separate column and then use that column for classification instead.  
  
##  <a name="bkmk_OtherRequirements"></a> Other Data Requirements  
  
### Requirements by Algorithm Type  
 Some algorithms that are used in the add-ins require specific data types or content types to create a model.  
  
 **Naïve Bayes models**  
  
-   The [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes algorithm cannot use continuous columns as input. That means you must either bin numbers, or if there are few enough values, handle them as discrete values.  
  
-   This type of model also cannot predict continuous values. Therefore, if you want to predict a continuous number such as income (for example) you should first bin the values into meaningful ranges. If you are not sure what the appropriate ranges are, you can use the clustering algorithm to identify clumps of numbers in your data.  
  
-   When you use a wizard based on this algorithm (such as [Analyze Key Influencers &#40;Table Analysis Tools for Excel&#41;](analyze-key-influencers-table-analysis-tools-for-excel.md)), columns that are continuous will be binned by the wizard you.  
  
-   If you build a Naive Bayes model using the [Advanced Modeling &#40;Data Mining Add-ins for Excel&#41;](advanced-modeling-data-mining-add-ins-for-excel.md) option, number columns will be removed from the model. If you want to avoid this, use the [Relabel &#40;SQL Server Data Mining Add-ins&#41;](relabel-sql-server-data-mining-add-ins.md) tool to create a new column with binned values.  
  
 **Clustering Models**  
  
-   The clustering tools ([Cluster Wizard &#40;Data Mining Add-ins for Excel&#41;](cluster-wizard-data-mining-add-ins-for-excel.md) and [Detect Categories &#40;Table Analysis Tools for Excel&#41;](detect-categories-table-analysis-tools-for-excel.md)) also cannot use continuous numbers, but both of these tools will automatically bin number columns for you.  
  
-   Both tools give you the option to choose the number of output categories in the results, but if you want to control the way that values in individual columns are grouped, you should create a new column with the grouping you want.  
  
 **Forecasting Models**  
  
-   All forecasting tools require that you predict a continuous number. You cannot predict a number that has been saved as text.  
  
-   If your data contains number columns that have the wrong data type, you can use Excel functions or PowerPivot functions to make a copy of the column that has the correct numeric data type. If you do this, be sure to remove the copy of the column that has the text numbers, so that the values are not duplicated.  
  
-   If you want to create a scatter plot of a regression model, the input variables must also be continuous numbers, expressed as an appropriate data type.  
  
### Using Content Types to Make Better Models  
 A *content type* is a property you apply to a column to specify how the column data should be used by the model. The algorithm can use the content type as an instruction or hint when performing the analysis.  
  
 For example, if a column contains numbers that repeat in a specific interval to indicate the days of the week, you might specify the content type of that column as `Cyclical`.  
  
 You don't have to worry about content types if you use the wizards and tools provided in this add-ins. However, if you use the [Add Model to Structure &#40;Data Mining Add-ins for Excel&#41;](add-model-to-structure-data-mining-add-ins-for-excel.md) modeling option to add a new model to existing data, you might get an error relating to content types.  
  
 The reason is that some types of model require a certain kind of data (such as a time stamp). The tools process these columns according to specific requirements and also add a content type property. Therefore, if you re-use the data with a completely different algorithm, you might need to change the data type or content type.  
  
 The following list describes the content types that are used in data mining, and identifies the data types that support each type.  
  
 `Discrete`  
 The column contains a finite number of values with no continuum between the values. For example, a gender column is a typical discrete attribute column, in that the data represents a specific number of categories.  
  
 The `Discrete` content type can be used with all data types.  
  
 `Continuous`  
 The column contains values that represent numeric data on a scale that allows interim values. A continuous column represents scalable measurements, and it is possible for the data to contain an infinite number of fractional values. A column of temperatures is an example of a continuous attribute column.  
  
 The `Continuous` content type can be used with the following data types: `Date`, `Double`, and `Long`.  
  
 `Discretized`  
 The column contains values that represent groups of values that have been derived from a continuous column. The buckets are treated as **ordered** and discrete values.  
  
 The `Discretized` content type can be used with the following data types: `Date`, `Double`, `Long`.  
  
 **Key**  
 The column uniquely identifies a row.  
  
 Typically the key column is a numeric or text identifier that should not be used for analysis, only for tracking records. The exceptions are time series keys and sequence keys.  
  
 **Nested table keys** are used only when you get data from an external data source that has been defined as an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data source view. For more information about nested tables, see [https://msdn.microsoft.com/library/ms175659.aspx](https://msdn.microsoft.com/library/ms175659.aspx):  
  
 This content type can be used with the following data types: `Date`, `Double`, `Long`, and `Text`.  
  
 **Key Sequence**  
 The column contains values that represent a sequence of events. The values are ordered, but do not have to be an equal distance apart.  
  
 This content type is supported by the following data types: `Double`, `Long`, `Text`, and `Date`.  
  
 **Key Time**  
 The column contains values that are ordered and represent a time scale. You can use the key time content type only if the model is a time series model or a sequence clustering model.  
  
 This content type is supported by the following data types: `Double`, `Long`, and `Date`.  
  
 **Table**  
 This content type also is used only when you get data from an external data source that has been defined as an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data source view.  
  
 What it means is that each row of data actually contains a nested data table, with one or more columns and one or more rows.  
  
 Nested tables are very handy, but you can use them only with the [Advanced Modeling &#40;Data Mining Add-ins for Excel&#41;](advanced-modeling-data-mining-add-ins-for-excel.md) modeling options. For example, the sample data for the [Associate Wizard &#40;Data Mining Client for Excel&#41;](associate-wizard-data-mining-client-for-excel.md) wizard and [Shopping Basket Analysis &#40;Table AnalysisTools for Excel&#41;](shopping-basket-analysis-table-analysistools-for-excel.md) tool contains data that has been flattened from a nested table.  

  
  
