---
title: "Model Filter Syntax and Examples (Analysis Services - Data Mining) | Microsoft Docs"
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
# Model Filter Syntax and Examples (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This section provides detailed information about the syntax for model filters, together with sample expressions.  
  
 [Filter syntax](#bkmk_Syntax)  
  
 [Filters on case attributes](#bkmk_Ex1)  
  
 [Filters on nested table attributes](#bkmk_Ex2)  
  
 [Filters on multiple nested table attributes](#bkmk_Ex3)  
  
 [Filters attributes missing in nested table](#bkmk_Ex4)  
  
 [Filters on multiple nested table values](#bkmk_Ex5)  
  
 [Filters on nested table attributes and EXISTS](#bkmk_Ex6)  
  
 [Filter combinations](#bkmk_Ex7)  
  
 [Filters on dates](#bkmk_Ex8)  
  
##  <a name="bkmk_Syntax"></a> Filter Syntax  
 Filter expressions generally are equivalent to the content of a WHERE clause. You can connect multiple conditions by using the logical operators **AND**, **OR**, and **NOT**.  
  
 In nested tables, you can also use the **EXISTS** and **NOT EXISTS** operators. An **EXISTS** condition evaluates to **true** if the subquery returns at least one row. This is useful in cases where you want to restrict the model to cases that contain a particular value in the nested table: for example, customers who have purchased an item at least once.  
  
 A **NOT EXISTS** condition evaluates to **true** if the condition specified in the subquery does not exist. An example is when you want to restrict the model to customers who have never purchased a particular item.  
  
 The general syntax is as follows:  
  
```  
<filter>::=<predicate list>  | ( <predicate list> )  
<predicate list>::= <predicate> | [<logical_operator> <predicate list>]   
<logical_operator::= AND| OR  
<predicate>::= NOT <predicate>|( <predicate> ) <avPredicate> | <nestedTablePredicate> | ( <predicate> )   
<avPredicate>::= <columnName> <operator> <scalar> | <columnName> IS [NOT] NULL  
<operator>::= = | != | <> | > | >= | < | <=  
<nestedTablePredicate>::= EXISTS (<subquery>)  
<subquery>::=SELECT * FROM <columnName>[ WHERE  <predicate list> ]  
```  
  
 *filter*  
 Contains one or more predicates, connected by logical operators.  
  
 *predicate list*  
 One or more valid filter expressions, separated by logical operators.  
  
 *columnName*  
 The name of a mining structure column.  
  
 logical operator  
 **AND**, **OR**, **NOT**  
  
 *avPredicate*  
 Filter expression that can be applied to scalar mining structure column only. An *avPredicate* expression can be used in both model filters or nested table filters.  
  
 An expression that uses any of the following operators can only be applied to a continuous column. :  
  
-   **\<** (less than)  
  
-   **>** (greater than)  
  
-   **>=** (greater than or equal to)  
  
-   **\<=** (less than or equal to)  
  
> [!NOTE]  
>  Regardless of the data type, these operators cannot be applied to a column that has the type **Discrete**, **Discretized**, or **Key**.  
  
 An expression that uses any of the following operators can be applied to a continuous, discrete, discretized, or key column:  
  
-   **=** (equals)  
  
-   **!=** (not equal to)  
  
-   **IS NULL**  
  
 If the argument, *avPredicate*, applies to a discretized column, the value used in the filter can be any value in a specific bucket.  
  
 In other words, you do not define the condition as `AgeDisc = '25-35'`, but instead compute and then use a value from that interval.  
  
 Example:  `AgeDisc = 27`  means any value in the same interval as 27, which in this case is 25-35.  
  
 *nestedTablePredicate*  
 Filter expression that applies to a nested table. Can be used in model filters only.  
  
 The sub-query argument of the argument, *nestedTablePredicate*, can only apply to a table mining structure column  
  
 subquery  
 A SELECT statement followed by a valid predicate or list of predicates.  
  
 All the predicates must be of the type described in *avPredicates*. Furthermore, the predicates can refer only to columns that are included in the current nested table, identified by the argument, *columnName*.  
  
### Limitations on Filter Syntax  
 The following restrictions apply to filters:  
  
-   A filter can contain only simple predicates. These include mathematical operators, scalars, and column names.  
  
-   User-defined functions are not supported in the filter syntax.  
  
-   Non-Boolean operators, such as the plus or minus signs, are not supported in the filter syntax.  
  
## Examples of Filters  
 The following examples demonstrate the use of filters applied to a mining model. If you create the filter expression by using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in the **Property** window and the **Expression** pane of the filter dialog box, you would see only the string that appears after the WITH FILTER keywords. Here, the definition of the mining structure is included to make it easier to understand the column type and usage.  
  
###  <a name="bkmk_Ex1"></a> Example 1: Typical Case-Level Filtering  
 This example shows a simple filter that restricts the cases used in the model to customers whose occupation is architect and whose age is over 30.  
  
```  
ALTER MINING STRUCTURE MyStructure  ADD MINING MODEL MyModel_1  
(  
CustomerId,  
Age,  
Occupation,  
MaritalStatus PREDICT  
)  
WITH FILTER (Age > 30 AND Occupation='Architect')  
```  
  
  
###  <a name="bkmk_Ex2"></a> Example 2: Case-Level Filtering using Nested Table Attributes  
 If your mining structure contains nested tables, you can either filter on the existence of a value in a nested table, or filter on nested table rows that contain a specific value. This example restricts the cases used for the model to customers over the age of 30 who made at least one purchase that included milk.  
  
 As this example shows, it is not necessary that the filter use only columns that are included in the model. The nested table **Products** is part of the mining structure, but is not included in the mining model. However, you can still filter on values and attributes in the nested table. To view the details of these cases, drillthrough must be enabled.  
  
```  
ALTER MINING STRUCTURE MyStructure  ADD MINING MODEL MyModel_2  
(  
CustomerId,  
Age,  
Occupation,  
MaritalStatus PREDICT  
)  
WITH DRILLTHROUGH,   
FILTER (Age > 30 AND EXISTS (SELECT * FROM Products WHERE ProductName='Milk')  
)  
```  
  
  
###  <a name="bkmk_Ex3"></a> Example 3: Case-Level Filtering on Multiple Nested Table Attributes  
 This example shows a three-part filter: a condition applies to the case table, another condition to an attribute in the nested table, and another condition on a specific value in one of the nested table columns.  
  
 The first condition in the filter, `Age > 30`, applies to a column in the case table. The remaining conditions apply to the nested table.  
  
 The second condition, `EXISTS (SELECT * FROM Products WHERE ProductName='Milk'`, checks for the presence of at least one purchase in the nested table that included milk. The third condition, `Quantity>=2`, means that the customer must have purchased at least two units of milk in a single transaction.  
  
```  
ALTER MINING STRUCTURE MyStructure  ADD MINING MODEL MyModel_3  
(  
CustomerId,  
Age,  
Occupation,  
MaritalStatus PREDICT,  
Products PREDICT  
(  
ProductName KEY,  
Quantity        
)  
)  
FILTER (Age > 30 AND EXISTS (SELECT * FROM Products WHERE ProductName='Milk'  AND Quantity >= 2)   
)  
```  
  
  
###  <a name="bkmk_Ex4"></a> Example 4: Case-Level Filtering On Absence of Nested Table Attributes  
 This example shows how to limit cases to customer who did not purchase a specific item, by filtering on the absence of an attribute in the nested table. In this example, the model is trained using customers over the age of 30 who have never bought milk.  
  
```  
ALTER MINING STRUCTURE MyStructure  ADD MINING MODEL MyModel_4  
(  
CustomerId,  
Age,  
Occupation,  
MaritalStatus PREDICT,  
Products PREDICT  
(  
ProductName  
)  
)  
FILTER (Age > 30 AND NOT EXISTS (SELECT * FROM Products WHERE ProductName='Milk') )  
```  
  
  
###  <a name="bkmk_Ex5"></a> Example 5: Filtering on Multiple Nested Table Values  
 The purpose of the example is to show nested table filtering. The nested table filter is applied after the case filter, and only restricts nested table rows.  
  
 This model could contain multiple cases with empty nested tables because EXISTS is not specified.  
  
```  
ALTER MINING STRUCTURE MyStructure  ADD MINING MODEL MyModel_5  
(  
CustomerId,  
Age,  
Occupation,  
MaritalStatus PREDICT,  
Products PREDICT  
(  
ProductName KEY,  
Quantity        
) WITH FILTER(ProductName='Milk' OR ProductName='bottled water')  
)  
WITH DRILLTHROUGH  
```  
  
  
###  <a name="bkmk_Ex6"></a> Example 6: Filtering on Nested Table Attributes and EXISTS  
 In this example, the filter on the nested table restricts the rows to those that contain either milk or bottled water. Then, the cases in the model are restricted by using an **EXISTS** statement. This makes sure that the nested table is not empty.  
  
```  
ALTER MINING STRUCTURE MyStructure  ADD MINING MODEL MyModel_6  
(  
CustomerId,  
Age,  
Occupation,  
MaritalStatus PREDICT,  
Products PREDICT  
(  
ProductName KEY,  
Quantity        
) WITH FILTER(ProductName='Milk' OR ProductName='bottled water')  
)  
FILTER (EXISTS (Products))  
```  
  
  
###  <a name="bkmk_Ex7"></a> Example 7: Complex Filter Combinations  
 The scenario for this model resembles that of Example 4, but is far more complex. The nested table, **ProductsOnSale**, has the filter condition `(OnSale)` meaning that the value of **OnSale** must be **true** for the product listed in **ProductName**. Here, **OnSale** is a structure column.  
  
 The second part of the filter, for **ProductsNotOnSale**, repeats this syntax, but filters on products for which the value of **OnSale** is **not true**`(!OnSale)`.  
  
 Finally, the conditions are combined and one additional restriction is added to the case table. The result is to predict purchases of products in the **ProductsNotOnSale** list, based on the cases that are included in the **ProductsOnSale** list, for all customers over the age of 25.  
  
 `ALTER MINING STRUCTURE MyStructure  ADD MINING MODEL MyModel_7`  
  
 `(`  
  
 `CustomerId,`  
  
 `Age,`  
  
 `Occupation,`  
  
 `MaritalStatus,`  
  
 `ProductsOnSale`  
  
 `(`  
  
 `ProductName KEY`  
  
 `) WITH FILTER(OnSale),`  
  
 `ProductsNotOnSale PREDICT ONLY`  
  
 `(`  
  
 `ProductName KEY`  
  
 `) WITH FILTER(!OnSale)`  
  
 `)`  
  
 `WITH DRILLTHROUGH,`  
  
 `FILTER (EXISTS (ProductsOnSale) AND EXISTS(ProductsNotOnSale) AND Age > 25)`  
  
  
###  <a name="bkmk_Ex8"></a> Example 8: Filtering on Dates  
 You can filter input columns on dates, as you would any other data. Dates contained in a column of type date/time are continuous values; therefore, you can specify a date range by using operators such as greater than (>) or less than (<). If your data source does not represent dates by a Continuous data type, but as discrete or text values, you cannot filter on a date range, but must specify individual discrete values.  
  
 However, you cannot create a filter on the date column in a time series model if the date column used for the filter is also the key column for the model. That is because, in time series models and sequence clustering models, the date column might be handled as type **KeyTime** or **KeySequence**.  
  
 If you need to filter on continuous dates in a time series model, you can create a copy of the column in the mining structure, and filter the model on the new column.  
  
 For example, the following expression represents a filter on a date column of type **Continuous** that has been added to the Forecasting model.  
  
 `=[DateCopy] > '12:31:2003:00:00:00'`  
  
> [!NOTE]  
>  Note that any extra columns that you add to the model might affect the results. Therefore, if you do not want the column to be used in computation of the series, you should add the column only to the mining structure, and not to the model. You can also set the model flag on the column to **PredictOnly** or to **Ignore**. For more information, see [Modeling Flags &#40;Data Mining&#41;](../../analysis-services/data-mining/modeling-flags-data-mining.md).  
  
 For other model types, you can use dates as input criteria or filter criteria just like you would in any other column. However, if you need to use a specific level of granularity that is not supported by a **Continuous** data type, you can create a derived value in the data source by using expressions to extract the unit to use in filtering and analysis.  
  
> [!WARNING]  
>  When you specify a dates as filter criteria, you must use the following format, regardless of the date format for the current OS: `mm/dd/yyyy`. Any other format results in an error.  
  
 For example, if you want to filter your call center results to show only weekends, you can create an expression in the data source view that extracts the weekday name for each date, and then use that weekday name value for input or as a discrete value in filtering. Just remember that repeating values can affect the model, so you should use only one of the columns, not the date column plus the derived value.  
  
  
## See Also  
 [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md)   
 [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md)  
  
  
