---
title: "Filter equation examples in a paginated report"
description: Find out how to create a paginated report filter. Consider these examples of often-used filters to create your own.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "filtering data [Reporting Services], filter equation examples"
---
# Filter equation examples in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  To create a filter in a paginated report, you must specify one or more filter equations. A filter equation includes an expression, a data type, an operator, and a value. This topic provides examples of commonly used filters.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  

## Pattern Options  

Built-in pattern matching provides a versatile tool for string comparisons. The pattern-matching features allow you to match each character in `string` against a specific character, a wildcard character, a character list, or a character range. The following table shows the characters allowed in `pattern` and what they match.  
  
|Characters in `pattern`|Matches in `string`|  
|-----------------------------|-------------------------|  
|`?`|Any single character|  
|`*`|Zero or more characters|  
|`#`|Any single digit (0–9)|  
|`[charlist]`|Any single character in `charlist`|  
|`[!charlist]`|Any single character not in `charlist`|  

> [!NOTE]  
>  To search for the above reserved characters use the `*[ ]*` escape pattern. For example, to search for # use `*[#]*`

## Filter Examples  
 The following table shows examples of filter equations that use different data types and different operators. The scope for the comparison is determined by report item for which a filter is defined. For example, for a filter defined on a dataset, **TOP % 10** is the top 10 percent of values in the dataset; for a filter defined on a group, **TOP % 10** is the top 10 percent of values in the group.  
  
|Simple Expression|Data Type|Operator|Value|Description|  
|-----------------------|---------------|--------------|-----------|-----------------|  
|`[SUM(Quantity)]`|**Integer**|**>**|`7`|Includes data values that are greater than 7.|  
|`[SUM(Quantity)]`|**Integer**|**TOP N**|`10`|Includes the top 10 data values.|  
|`[SUM(Quantity)]`|**Integer**|**TOP %**|`20`|Includes the top 20% of data values.|  
|`[Sales]`|**Text**|**>**|`=CDec(100)`|Includes all values of type System.Decimal (SQL "money" data types) greater than $100.|  
|`[OrderDate]`|**DateTime**|**>**|`2008-01-01`|Includes all dates from January 1, 2008 to the present date.|  
|`[OrderDate]`|**DateTime**|**BETWEEN**|`2008-01-01`<br /><br /> `2008-02-01`|Includes dates from January 1, 2008 up to and including February 1, 2008.|  
|`[Territory]`|**Text**|**LIKE**|`*east`|All territory names that end in "east".|  
|`[Territory]`|**Text**|**LIKE**|`%o%th*`|All territory names that include North and South at the beginning of the name.|  
|`=LEFT(Fields!Subcat.Value,1)`|**Text**|**IN**|`B, C, T`|All subcategory values that begin with the letters B, C, or T.|  
  
## Related content

- [Paginated report parameters in Report Builder](report-parameters-report-builder-and-report-designer.md)
- [Add dataset filters, data region filters, and group filters to a paginated report (Report Builder)](add-dataset-filters-data-region-filters-and-group-filters.md)
- [Data types in expressions in a paginated report (Report Builder)](data-types-in-expressions-report-builder-and-ssrs.md)
- [Expression uses in paginated reports (Report Builder)](expression-uses-in-reports-report-builder-and-ssrs.md)
- [Expression examples in Report Builder paginated reports](expression-examples-report-builder-and-ssrs.md)
