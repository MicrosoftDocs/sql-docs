---
title: Report Builder functions - Lookup function in a paginated report (Report Builder)
description: Learn how to use the Lookup function to return the first matching value for the specified name from a dataset that contains name or value pairs.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/26/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: concept-article
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want learn how to use the Lookup function so that I can programatically find data in my datasets.
---
# Report Builder functions - Lookup function in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

**Lookup** returns the first matching value for the specified name from a dataset that contains name/value pairs in a paginated report.  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
``` basic
Lookup(source_expression, destination_expression, result_expression, dataset)  
```  
  
### Parameters  

|Parameter               |Definition                                                                                                                                                                                                        |
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|*source_expression*     |(**Variant**) An expression that evaluates in the current scope and that specifies the name or key to look up. For example, `=Fields!ProdID.Value`.                                                            |
|*destination_expression*|(**Variant**) An expression that evaluates for each row in a dataset and that specifies the name or key to match on. For example, `=Fields!ProductID.Value`.                                                   |
|*result_expression*     |(**Variant**) An expression that evaluates for the row in the dataset where *source_expression* = *destination_expression* and that specifies the value to retrieve. For example, `=Fields!ProductName.Value`.|
|*dataset*               |A constant that specifies the name of a dataset in the report. "Products" might be an example of a dataset you use.                                                                                               |
  
## Return  

**Lookup** returns a **Variant**, or it returns **Nothing** if there's no match.  
  
## Remarks  

Use **Lookup** to retrieve the value from the specified dataset for a name/value pair where there's a 1-to-1 relationship. For example, for an **ID** field in a table, you can use **Lookup** to retrieve the corresponding **Name** field from a dataset that isn't bound to the data region.  
  
**Lookup** does the following:  
  
- Evaluates the source expression in the current scope.  
  
- Evaluates the destination expression for each row of the specified dataset after filters are applied, based on the collation of the specified dataset.  
  
- On the first match of source expression and destination expression, evaluates the result expression for that row in the dataset.  
  
- Returns the result expression value.  
  
To retrieve multiple values for a single name or key field where there's a 1-to-many relationship, use [Report Builder functions - LookupSet function in a paginated report (Report Builder)](../../reporting-services/report-design/report-builder-functions-lookupset-function.md). To call **Lookup** for a set of values, use [Keep headers visible when scrolling through a paginated report (Report Builder)](../../reporting-services/report-design/report-builder-functions-multilookup-function.md).  
  
The following restrictions apply:  
  
- **Lookup** is evaluated after all filter expressions are applied.  
  
- Only one level of lookup is supported. A source, destination, or result expression can't include a reference to a lookup function.  
  
- Source and destination expressions must evaluate to the same data type. The return type is the same as the data type of the evaluated result expression.  
  
- Source, destination, and result expressions can't include references to report or group variables.  
  
- **Lookup** can't be used as an expression for the following report items:  
  
  - Dynamic connection strings for a data source.  
  
  - Calculated fields in a dataset.  
  
  - Query parameters in a dataset.  
  
  - Filters in a dataset.  
  
  - Report parameters.  
  
  - The **Report.Language** property.  
  
For more information, see [Report Builder functions - aggregate functions reference in paginated reports (Report Builder)](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md) and [Expression scope for totals, aggregates, and built-in collections in a paginated report (Report Builder)](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
## Example  

In the following example, assume that a table is bound to a dataset that includes a field for the product identifier **ProductID**. A separate dataset called "Product" contains the corresponding product identifier ID and the product's name.
  
In the following expression, **Lookup** compares the value of **ProductID** to the ID in each row of the dataset called "Product" and, when a match is found, returns the value of the **Name** field for that row.  
  
``` basic
=Lookup(Fields!ProductID.Value, Fields!ID.Value, Fields!Name.Value, "Product")  
```  
  
## Related content

- [Expression uses in paginated reports (Report Builder)](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)
- [Expression examples in paginated reports (Report Builder)](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)
- [Data types in expressions in a paginated report (Report Builder)](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md)