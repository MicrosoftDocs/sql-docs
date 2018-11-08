---
title: "Dataset Fields Collection References (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 006c6bd3-d776-4c20-9092-32e40688ac49
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Dataset Fields Collection References (Report Builder and SSRS)
  Each dataset in a report contains one Fields collection. The Fields collection is the set of fields specified by the dataset query plus any additional calculated fields that you create. After you create a dataset, the field collection appears in the **Report Data** pane.  
  
 A simple field reference in an expression displays on the design surface as a simple expression. For example, when you drag the field `Sales` from the Report Data pane to a table cell on the design surface, `[Sales]` is displayed. This represents the underlying expression `=Fields!Sales.Value` that is set on the text box Value property. When the report runs, the report processor evaluates this expression and displays the actual data from the data source in the text box in the table cell. For more, see [Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md) and [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../report-data/dataset-fields-collection-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Displaying the Field Collection for a Dataset  
 To display the individual values for a field collection, drag each field to a table detail row and run the report. References from the detail row of a table or list data region display a value for each row in the dataset.  
  
 To display summarized values for a field, drag each numeric field to the data area of a matrix. The default aggregate function for the total row is Sum, for example, `=Sum(Fields!Sales.Value)`. You can change the default function in order to calculate different totals. For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md).  
  
 To display summarized values for a field collection in a text box directly on the design surface (not part of a data region), you must specify the dataset name as a scope for the aggregate function. For example, for a dataset named `SalesData`, the following expression specifies the total of all values for the field `Sales`: `=Sum(Fields!Sales,"SalesData")`.  
  
 When you use the **Expression** dialog box to define a simple field reference, you can select the Fields collection in the Category pane and see the list of available fields in the **Field** pane. Each field has several properties, including Value and IsMissing. The remaining properties are predefined extended field properties that may be available to the dataset depending on the data source type.  
  
### Detecting Nulls for a Dataset Field  
 To detect a field value that is null (`Nothing` in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]), you can use the function `IsNothing`. When placed in a text box in a table details row, the following expression tests the field `MiddleName` and substitutes the text "No Middle Name" when the value is null, and the field value itself when the value is not null:  
  
 `=IIF(IsNothing(Fields!MiddleName.Value),"No Middle Name",Fields!MiddleName.Value)`  
  
### Detecting Missing Fields for Dynamic Queries at Run Time  
 By default, items in the Fields collection have two properties: Value and IsMissing. The IsMissing property indicates whether a field that is defined for a dataset at design time is contained in the fields retrieved at run time. For example, your query might call a stored procedure in which the result set varies with an input parameter, or your query might be `SELECT * FROM` *\<table>* where the table definition changed.  
  
> [!NOTE]  
>  IsMissing detects changes in the dataset schema between design time and run time for any type of data source. IsMissing cannot be used to detect empty members in a multidimensional cube and is not related to the MDX query language concepts of `EMPTY` and `NON EMPTY`.  
  
 You can test the IsMissing property in custom code to determine if a field is present in the result set. You cannot test for its presence using an expression with a [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] function call such as `IIF` or `SWITCH`, because [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] evaluates all parameters in the call to the function, which results in an error when the reference to the missing is evaluated.  
  
#### Example for Controlling the Visibility of a Dynamic Column for a Missing Field  
 To set an expression that controls the visibility of a column that displays a field in a dataset, you must first define a custom code function that returns a Boolean value based on whether the field is missing. For example, the following custom code function returns true if the field is missing and false if the field exists.  
  
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
  
 The column is hidden when the field does not exist.  
  
#### Example for Controlling the Text Box Value for a Missing Field  
 To substitute text that you write in place of the value of a missing field, you must write custom code that returns text you can use in place of a field value when the field is missing. For example, the following custom code function returns the value of the field if the field exists, and the message that you specify as the second parameter if the field does not exist:  
  
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
  
### Using Extended Field Properties  
 Extended field properties are additional properties defined on a field by the data processing extension, which is determined by the data source type for the dataset. Extended field properties are predefined or specific to a data source type. For more information, see [Extended Field Properties for an Analysis Services Database &#40;SSRS&#41;](../report-data/extended-field-properties-for-an-analysis-services-database-ssrs.md).  
  
 If you specify a property that is not supported for that field, the expression evaluates to `null` (`Nothing` in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]). If a data provider does not support extended field properties, or if the field is not found when the query is executed, the value for the property is `null` (`Nothing` in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]) for properties of type `String` and `Object`, and zero (0) for properties of type `Integer`. A data processing extension may take advantage of predefined properties by optimizing queries that include this syntax.  
  
## See Also  
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](../report-data/report-datasets-ssrs.md)  
  
  
