---
title: "Filter Property | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "03/20/2018"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::Filter"
helpviewer_keywords: 
  - "Filter property"
ms.assetid: 80263a7a-5d21-45d1-84fc-34b7a9be4c22
author: MightyPen
ms.author: genemi
manager: craigg
---
# Filter Property
Indicates a filter for data in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
## Settings and Return Values

Sets or returns a **Variant** value, which can contain one of the following items:  
  
-   **Criteria string:** A string made up of one or more individual clauses concatenated with **AND** or **OR** operators.  
  
-   **Array of bookmarks:** An array of unique bookmark values that point to records in the **Recordset** object.  
  
-   A [FilterGroupEnum](../../../ado/reference/ado-api/filtergroupenum.md) value.  
  
## Remarks

Use the **Filter** property to selectively screen out records in a **Recordset** object. The filtered **Recordset** becomes the current cursor. Other properties that return values based on the current **cursor** are affected, such as [AbsolutePosition Property (ADO)](../../../ado/reference/ado-api/absoluteposition-property-ado.md), [AbsolutePage Property (ADO)](../../../ado/reference/ado-api/absolutepage-property-ado.md), [RecordCount Property (ADO)](../../../ado/reference/ado-api/recordcount-property-ado.md), and [PageCount Property (ADO)](../../../ado/reference/ado-api/pagecount-property-ado.md). Setting the **Filter** property to a specific new value moves the current record to the first record that satisfies the new value.
  
The criteria string is made up of clauses in the form *FieldName-Operator-Value* (for example, `"LastName = 'Smith'"`). You can create compound clauses by concatenating individual clauses with **AND** (for example, `"LastName = 'Smith' AND FirstName = 'John'"`) or **OR** (for example, `"LastName = 'Smith' OR LastName = 'Jones'"`). For criteria strings, Use the following guidelines:

-   *FieldName* must be a valid field name from the **Recordset**. If the field name contains spaces, you must enclose the name in square brackets.  
  
-   Operator must be one of the following: \<, >, \<=, >=, <>, =, or **LIKE**.  
  
-   Value is the value with which you will compare the field values (for example, 'Smith', #8/24/95#, 12.345, or $50.00). Use single quotes with strings and pound signs (#) with dates. For numbers, you can use decimal points, dollar signs, and scientific notation. If Operator is **LIKE**, Value can use wildcards. Only the asterisk (*) and percent sign (%) wild cards are allowed, and they must be the last character in the string. Value cannot be null.  
  
> [!NOTE]
>  To include single quotation marks (') in the filter Value, use two single quotation marks to represent one. For example, to filter on O'Malley, the criteria string should be `"col1 = 'O''Malley'"`. To include single quotation marks at both the beginning and the end of the filter value, enclose the string with pound signs (#). For example, to filter on '1', the criteria string should be `"col1 = #'1'#"`.  
  
-   There is no precedence between AND and OR. Clauses can be grouped within parentheses. However, you cannot group clauses joined by an OR and then join the group to another clause with an AND, as in the following code snippet:  
 `(LastName = 'Smith' OR LastName = 'Jones') AND FirstName = 'John'`  
  
-   Instead, you would construct this filter as  
 `(LastName = 'Smith' AND FirstName = 'John') OR (LastName = 'Jones' AND FirstName = 'John')`  
  
-   In a **LIKE** clause, you can use a wildcard at the beginning and end of the pattern. For example, you can use `LastName Like '*mit*'`. Or with **LIKE** you can use a wildcard only at the end of the pattern. For example, `LastName Like 'Smit*'`.  
  
 The filter constants make it easier to resolve individual record conflicts during batch update mode by allowing you to view, for example, only those records that were affected during the last [UpdateBatch Method](../../../ado/reference/ado-api/updatebatch-method.md) method call.  
  
Setting the **Filter** property itself might fail due to a conflict with the underlying data. For example, this failure can happen when a record has already been deleted by another user. In such a case, the provider returns warnings to the [Errors Collection (ADO)](../../../ado/reference/ado-api/errors-collection-ado.md) collection, but does not halt program execution. An error at run time occurs only if there are conflicts on all the requested records. Use the [Status Property (ADO Recordset)](../../../ado/reference/ado-api/status-property-ado-recordset.md) property to locate records with conflicts.  
  
Setting the **Filter** property to a zero-length string ("") has the same effect as using the **adFilterNone** constant.
  
Whenever the **Filter** property is set, the current record position moves to the first record in the filtered subset of records in the **Recordset**. Similarly, when the **Filter** property is cleared, the current record position moves to the first record in the **Recordset**.

Suppose that a **Recordset** is filtered based on a field of some variant type, such as the type sql_variant. An error (DISP_E_TYPEMISMATCH or 80020005) occurs when the subtypes of the field and filter values used in the criteria string do not match. For example, suppose:

- A **Recordset** object (rs) contains a column (C) of the sql_variant type.
- And a field of this column has been assigned a value of 1 of the I4 type. The criteria string is set to `rs.Filter = "C='A'"` on the field.

This configuration produces the error during run time. However, `rs.Filter = "C=2"` applied on the same field will not produce any error. And the field is filtered out of the current record set.

See the [Bookmark Property (ADO)](../../../ado/reference/ado-api/bookmark-property-ado.md) property for an explanation of bookmark values from which you can build an array to use with the Filter property.

Only Filters in the form of criteria strings affect the contents of a persisted **Recordset**. An example of a criteria string is `OrderDate > '12/31/1999'`. Filters created with an array of bookmarks, or using a value from the **FilterGroupEnum**, do not affect the contents of the persisted **Recordset**. These rules apply to Recordsets created with either client-side or server-side cursors.
  
> [!NOTE]
>  When you apply the adFilterPendingRecords flag to a filtered and modified **Recordset** in the batch update mode, the resultant **Recordset** is empty if the filtering was based on the key field of a single-keyed table and the modification was made on the key field values. The resultant **Recordset** will be non-empty if one of the following statements is true:  
  
-   The filtering was based on non-key fields in a single-keyed table.  
  
-   The filtering was based on any fields in a multiple-keyed table.  
  
-   Modifications were made on non-key fields in a single-keyed table.  
  
-   Modifications were made on any fields in a multiple-keyed table.  
  
The following table summarizes the effects of **adFilterPendingRecords** in different combinations of filtering and modifications. The left column shows the possible modifications. Modifications can be made on any of the non-keyed fields, on the key field in a single-keyed table, or on any of the key fields in a multiple-keyed table. The top row shows the filtering criterion. Filtering can be based on any of the non-keyed fields, the key field in a single-keyed table, or any of the key fields in a multiple-keyed table. The intersecting cells show the results. A **+** plus sign means that applying **adFilterPendingRecords** results in a non-empty **Recordset**. A **-** minus sign means an empty **Recordset**.  
  
||Non keys|Single Key|Multiple Keys|
|-|--------------|----------------|-------------------|
|**Non keys**|+|+|+|
|**Single Key**|+|-|N/A|
|**Multiple Keys**|+|N/A|+|
|||||
  
## Applies To

[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also

[Filter and RecordCount Properties Example (VB)](../../../ado/reference/ado-api/filter-and-recordcount-properties-example-vb.md)
[Filter and RecordCount Properties Example (VC++)](../../../ado/reference/ado-api/filter-and-recordcount-properties-example-vc.md)
[Clear Method (ADO)](../../../ado/reference/ado-api/clear-method-ado.md)
[Optimize Property-Dynamic (ADO)](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md)
