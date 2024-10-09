---
title: "Dataset Fields collection references in a paginated report"
description: Create a dataset for display of individual or summarized values in the Report Data pane of Report Builder in a paginated report.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Dataset Fields collection references in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Each dataset in a paginated report contains one **Fields** collection. The **Fields** collection is the set of fields specified by the dataset query plus any other calculated fields that you create. After you create a dataset, the field collection appears in the **Report Data** pane.  
  
 A simple field reference in an expression displays on the design surface as a simple expression. For example, when you drag the field `Sales` from the Report Data pane to a table cell on the design surface, `[Sales]` is displayed. This value represents the underlying expression `=Fields!Sales.Value` that is set on the text box **Value** property. When the report runs, the report processor evaluates this expression and displays the actual data from the data source in the text box in the table cell. For more, see [Expressions &#40;Report Builder&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md) and [Dataset Fields collection &#40;Report Builder&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Display the field collection for a dataset  
 To display the individual values for a field collection, drag each field to a table detail row and run the report. References from the detail row of a table or list data region display a value for each row in the dataset.  
  
 To display summarized values for a field, drag each numeric field to the data area of a matrix. The default aggregate function for the total row is Sum, for example, `=Sum(Fields!Sales.Value)`. You can change the default function in order to calculate different totals. For more information, see [Aggregate functions reference &#40;Report Builder&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md).  
  
 To display summarized values for a field collection in a text box directly on the design surface that isn't part of a data region, you must specify the dataset name as a scope for the aggregate function. For example, for a dataset named `SalesData`, the following expression specifies the total of all values for the field `Sales`: `=Sum(Fields!Sales,"SalesData")`.  
  
 When you use the **Expression** dialog to define a field reference, you can select the **Fields** collection in the **Category** pane and see the list of available fields in the **Field** pane. Each field has several properties, including **Value** and **IsMissing**. The remaining properties are predefined extended field properties that might be available to the dataset depending on the data source type.  
  
### Detect nulls for a dataset field  
 To detect a field value that is null (**Nothing** in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]), you can use the function **IsNothing**. When placed in a text box in a table details row, the following expression tests the field `MiddleName` and substitutes the text "No Middle Name" when the value is null, and the field value itself when the value isn't null:  
  
 `=IIF(IsNothing(Fields!MiddleName.Value),"No Middle Name",Fields!MiddleName.Value)`  
  
### Detect missing fields for dynamic queries at run time  
 By default, items in the Fields collection have two properties: **Value** and **IsMissing**. The **IsMissing** property indicates whether a field that is defined for a dataset at design time is contained in the fields retrieved at run time. For example, your query might call a stored procedure in which the result set varies with an input parameter, or your query might be `SELECT * FROM <table>` where the table definition changed.  
  
> [!NOTE]  
>  **IsMissing** detects changes in the dataset schema between design time and run time for any type of data source. IsMissing can't be used to detect empty members in a multidimensional cube and is not related to the MDX query language concepts of **EMPTY** and **NON EMPTY**.  
  
 You can test the IsMissing property in custom code to determine if a field is present in the result set. You can't test for its presence using an expression with a [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] function call such as **IIF** or **SWITCH**, because [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] evaluates all parameters in the call to the function, which results in an error when the reference to the missing is evaluated.  
  
#### Example for controlling the visibility of a dynamic column for a missing field  
To set an expression controlling the visibility of a column displaying a field in a dataset, you must first define a custom code function. This function should return a Boolean value. The value is based on whether the field is missing. For example, the following custom code function returns true if the field is missing and false if the field exists.  
  
```  
Public Function IsFieldMissing(field as Field) as Boolean  
 If (field.IsMissing) Then  
 Return True  
  Else   
  Return False  
 End If  
End Function  
```  
  
 To use this function to control visibility of a column, set the Hidden property of the column to the following expression:  
  
 `=Code.IsFieldMissing(Fields!FieldName)`  
  
 The column is hidden when the field doesn't exist.  
  
#### Example for controlling the text box value for a missing field  
 To substitute text in place of the value of a missing field, you must write custom code. This code should return the text you can use as a replacement when the field is missing. For example, the following custom code function returns the value of the field if the field exists. Also, the code returns the message that you specify as the second parameter if the field doesn't exist:  
  
```  
Public Function IsFieldMissingThenString(field as Field, strMessage as String) as String  
 If (field.IsMissing) Then  
  Return strMessage  
 Else   
  Return field.Value  
  End If  
End Function  
```  
  
 To use this function in a text box, add the following expression to the Value property:  
  
 `=Code.IsFieldMissingThenString(Fields!FieldName,"Missing")`  
  
 The text box displays either the field value or the text that you specified.  
  
### Use extended field properties  
 Extended field properties are other properties defined on a field by the data processing extension. The data source type determines for the dataset. Extended field properties are predefined or specific to a data source type. For more information, see [Extended field properties for an analysis services database &#40;SSRS&#41;](../../reporting-services/report-data/extended-field-properties-for-an-analysis-services-database-ssrs.md).  
  
 If you specify a property that isn't supported for that field, the expression evaluates to **null** (**Nothing** in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]). If a data provider doesn't support extended field properties, or if the field isn't found when the query is executed, the value for the property is **null** (**Nothing** in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]) for properties of type **String** and **Object**, and zero (0) for properties of type **Integer**. A data processing extension might take advantage of predefined properties by optimizing queries that include this syntax.  
  
## Related content

- [Expression examples &#40;Report Builder&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)
- [Report datasets](../../reporting-services/report-data/report-datasets-ssrs.md)
