---
title: "Add dataset filters, data region filters, and group filters to a paginated report"
description: Find out how filters can help you control which data is displayed and processed in your paginated reports in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add dataset filters, data region filters, and group filters to a paginated report (Report Builder)


[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In a paginated report, a filter is a part of a dataset, a data region, or a data region group that you create to limit the data that is used in the report. Filters are a way to help you control report data if you can't change the dataset query. For example, you might want a filter if you're using a shared dataset.  
  
 Filters help you control which data is displayed and processed in a report. You can specify filters for a dataset, a data region, or a group, in any combination.  
  
 For more information, see [Add a filter to a dataset &#40;Report Builder&#41;](../../reporting-services/report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md) and [Filter equation examples &#40;Report Builder&#41;](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="When"></a> Choose when to set a filter  
 Specify filters for report items when you can't filter data at the source. For example, use report filters when the data source doesn't support query parameters. Or, use report filters when you must run stored procedures and can't modify the query, or a parameterized report snapshot displays customized data for different users.  
  
 You can filter report data before or after you retrieve it for a report dataset. To filter data before you retrieve it, change the query for each dataset. When you filter data in the query, you filter data at the data source, which reduces the amount data that must be retrieved and processed in a report. To filter data after you retrieve it, create filter expressions in the report. You can set filter expressions for a dataset, a data region, or a group, including detail groups. You can also include parameters in filter expressions. This action provides a way to filter data for specific values or for specific users. For example, you can filter on a value that identifies the user viewing the report.  
  
##  <a name="Where"></a> Choose where to set a filter  
 Determine where you want to set a filter by the effect you want to achieve in your report. At run time, the report processor applies filters in the following order: on the dataset, and then on the data region, and then on groups from the top down in each group hierarchy. On a table, matrix, and list, filters for row groups, column groups, and adjacent groups are applied independently. On a chart, filters for category groups and series groups are applied independently. When the report processor applies the filter, all filter equations are applied in the order they're defined on the **Filter** page of the **Properties** dialog for each report item, which is the equivalent of combining them with Boolean AND operations.  
  
 The following list compares the effect of setting filters on different report items:  
  
-   **On the dataset**: Set a filter on the dataset when you want one or more data regions that are bound to a single dataset to be filtered in the same way. For example, set the filter on the dataset that is bound to both a table that displays sales data and a chart that displays the same data.  
  
-   **On the data region**: Set a filter on the data region when you want one or more data regions that are bound to a single dataset to provide a different view of the dataset. For example, set the filter on one Table data region to display the top 10 stores for sales and a different Table data region to display the bottom 10 stores for sales in the same report.  
  
-   **On the row or column groups in a Tablix data region**: Set a filter on a group when you want to include or exclude certain values for a group expression to control which group values appear in the table, matrix, or list.  
  
-   **On the details group in a Tablix data region**: Set a filter on the details group when you have multiple detail groups for a data region and want each detail group to display a different set of data from the dataset.  
  
-   **On the series or category groups in a Chart data region**: Set a filter on a series or category group when you want to include or exclude certain values for a group expression to control which values appear in the chart.  
  
 Back to Top  
  
##  <a name="FilterEquations"></a> Understand a filter equation  
 At run time, the report processor converts the value to the specified data type, and then uses the specified operator to compare the expression and value. The following list describes each part of the filter equation:  
  
-   **Expression**: Defines what you are filtering on. Typically, this field is a dataset field.  
  
-   **Data Type**: Specifies the data type to use when the filter equation is evaluated at run time by the report processor. The data type you select must be one of the data types supported by the report definition schema.  
  
-   **Operator**: Defines how to compare the two parts of the filter equation.  
  
-   **Value**: Defines the expression to use in the comparison.  
  
 The following sections describe each part of the filter equation.  
  
### Expression  
 When the report processor evaluates the filter equation at run time, the data types for the expression and the value must be the same. The data type of the field you select for **Expression** is determined by the data processing extension or data provider that is used to retrieve data from the data source. The data type of the expression that you enter for **Value** is determined by defaults. The data types supported for a report definition determine the choices of data type. The data provider might convert the database values to a CLR type.  
  
### Data type  
 For the report processor to compare two values, the data types must be the same. The following table lists the mapping between CLR data types and report definition data types. Data that you retrieve from a data source might be converted to a data type that is different by the time the data is report data.  
  
|**Report Definition Schema Data Type**|**CLR Type(s)**|  
|--------------------------------------------|-----------------------|  
|**Boolean**|**Boolean**|  
|**DateTime**|**DateTime**, **DateTimeOffset**|  
|**Integer**|**Int16**, **Int32**, **UInt16**, **Byte**, **SByte**|  
|**Float**|**Single**, **Double**, **Decimal**|  
|**Text**|**String**, **Char**, **GUID**, **Timespan**|  
  
 In cases where you must specify a data type, you can specify your own conversion in the `Value` part of the expression.  
  
### Operator  
 The following table lists the operators that you can use in a filter equation, and what the report processor uses to evaluate the filter equation.  
  
|Operator|Action|  
|--------------|------------|  
|**Equal, Like, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual**|Compares the expression to one value.|  
|**TopN, BottomN**|Compares the expression to one **Integer** value.|  
|**TopPercent, BottomPercent**|Compares the expression to one **Integer** or **Float** value.|  
|**Between**|Tests whether the expression is between two values, inclusive.|  
|**In**|Tests whether the expression is contained in a set of values.|  
  
### Value  
 The `Value` expression specifies the final part of the filter equation. The report processor converts the evaluated expression to the data type that you specified, and then evaluates the entire filter equation to determine if the data specified in Expression passes through the filter.  
  
 To convert to a data type that isn't a standard CLR data type, you must modify the expression to explicitly convert to a data type. You can use the conversion functions listed in the **Expression** dialog under **Common Functions**, **Conversion**. For example, for a field `ListPrice` that represents data that is stored as a **money** data type on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source, the data processing extension returns the field value as a <xref:System.Decimal> data type. To set a filter to use only values greater than **$50000.00** in the report currency, convert the value to decimal by using the expression `=CDec(50000.00)`.  
  
 This value can also include a parameter reference to allow a user to interactively select a value on which to filter.  
  
 Back to Top  
  
## Related content

- [Expression uses in reports &#40;Report Builder&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)
- [Report parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)
