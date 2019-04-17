---
title: "Working with Recordsets | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Recordset object [ADO]"
ms.assetid: bdf9a56a-de4a-44de-9111-2f11ab7b16ea
author: MightyPen
ms.author: genemi
manager: craigg
---
# Working with Recordsets
The **Recordset** object has built-in features that let you rearrange the order of the data in the result set, to search for a specific record based on criteria that you supply, and even to optimize those search operations using indexes. Whether these features are available for use depends on the provider and in some cases - such as that of the [Index](../../../ado/reference/ado-api/index-property.md) property - the structure of the data source itself.  
  
## Arranging Data  
 Frequently, the most efficient way to order the data in your **Recordset** is by specifying an ORDER BY clause in the SQL command used to return results to it. However, you might have to change the order of the data in a **Recordset** that has already been created. You can use the **Sort** property to establish the order in which rows of a **Recordset** are traversed. Additionally, the **Filter** property determines which rows are can be accessed when traversing rows.  
  
 The **Sort** property sets or returns a **String** value that indicates the field names in the **Recordset** on which to sort. Each name is separated by a comma and is optionally followed by a space and the keyword **ASC** (which sorts the field in ascending order) or **DESC** (which sorts the field in descending order). By default, if no keyword is specified, the field is sorted in ascending order.  
  
 The sort operation is efficient because data is not physically rearranged but is accessed in the order specified by an index.  
  
 The **Sort** property requires the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to be set to **adUseClient**. A temporary index will be created for each field specified in the **Sort** property if an index does not already exist.  
  
 Setting the **Sort** property to an empty string will reset the rows to their original order and delete temporary indexes. Existing indexes will not be deleted.  
  
 Suppose a **Recordset** contains three fields named *firstName*, *middleInitial*, and *lastName*. Set the **Sort** property to the string "`lastName DESC, firstName ASC`", which will order the **Recordset** by last name in descending order and then by first name in ascending order. The middle initial is ignored.  
  
 No field referenced in a sort criteria string can be named "ASC" or "DESC" because those names conflict with the keywords **ASC** and **DESC**. Give a field that has a conflicting name an alias by using the **AS** keyword in the query that returns the **Recordset**.  
  
 For more information about **Recordset** filtering, see "Filtering the Results" later in this topic.  
  
## Finding a Specific Record  
 ADO provides the [Find](../../../ado/reference/ado-api/find-method-ado.md) and [Seek](../../../ado/reference/ado-api/seek-method.md) methods for locating a particular record in a **Recordset**. The **Find** method is supported by a variety of providers but is limited to a single search criterion. The **Seek** method supports searching on multiple criteria, but is not supported by many providers.  
  
 Indexes on fields can greatly enhance the performance of the **Find** method and **Sort** and **Filter** properties of the **Recordset** object. You can create an internal index for a **Field** object by setting its dynamic [Optimize](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md) property. This dynamic property is added to the **Properties** collection of the **Field** object when you set the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to **adUseClient**. Remember that this index is internal to ADO - you cannot gain access to it or use it for any other purpose. Also, this index is distinct from the [Index](../../../ado/reference/ado-api/index-property.md) property of the **Recordset** object.  
  
 The **Find** method quickly locates a value within a column (field) of a **Recordset**. You can frequently improve the speed of the **Find** method on a column by using the **Optimize** property to create an index on it.  
  
 The **Find** method limits your search to the contents of one field. The **Seek** method requires that you have an index and has other limitations as well. If you must search on multiple fields that are not the basis of an index, or if your provider does not support indexes, you can limit your results by using the **Filter** property of the **Recordset** object.  
  
### Find  
 The **Find** method searches a **Recordset** for the row that satisfies a specified criterion. Optionally, the direction of the search, starting row, and offset from the starting row may be specified. If the criterion is met, the current row position is set on the found record; otherwise, the position is set to the end (or start) of the **Recordset**, depending on search direction.  
  
 Only a single-column name can be specified for the criterion. In other words, this method does not support multicolumn searches.  
  
 The comparison operator for the criterion can be "**>**" (greater than), "**\<**" (less than), "=" (equal), ">=" (greater than or equal), "<=" (less than or equal), "<>" (not equal), or "LIKE" (pattern matching).  
  
 The criterion value can be a string, floating-point number, or date. String values are delimited with single quotation marks or "#" (number sign) marks (for example, "state = 'WA'" or "state = #WA#"). Date values are delimited with "#" (number sign) marks (for example, "start_date > #7/22/97#").  
  
 If the comparison operator is "like", the string value can contain an asterisk (*) to find one or more occurrences of any character or substring. For example, "state like 'M\*'" matches Maine and Massachusetts. You can also use leading and trailing asterisks to find a substring that is contained within the values. For example, "state like '\*as\*'" matches Alaska, Arkansas, and Massachusetts.  
  
 Asterisks can be used only at the end of a criteria string or together at both the beginning and end of a criteria string, as shown earlier. You cannot use the asterisk as a leading wildcard ('*str') or embedded wildcard ('s\*r'). This will cause an error.  
  
### Seek and Index  
 Use the **Seek** method together with the **Index** property if the underlying provider supports indexes on the **Recordset** object. Use the [Supports](../../../ado/reference/ado-api/supports-method.md)**(adSeek)** method to determine whether the underlying provider supports **Seek**, and the **Supports(adIndex)** method to determine whether the provider supports indexes. (For example, the [OLE DB Provider for Microsoft Jet](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-jet.md) supports **Seek** and **Index**.)  
  
 If **Seek** does not find the desired row, no error occurs, and the row is positioned at the end of the **Recordset**. Set the **Index** property to the desired index before executing this method.  
  
 This method is supported only with server-side cursors. Seek is not supported when the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property value of the **Recordset** object is **adUseClient**.  
  
 This method can be used only when the **Recordset** object has been opened with a [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) value of **adCmdTableDirect**.  
  
## Filtering the Results  
 The **Find** method limits your search to the contents of one field. The **Seek** method requires that you have an index and has other limitations as well. If you must search on multiple fields that are not the basis of an index or if your provider does not support indexes, you can limit your results by using the **Filter** property of the **Recordset** object.  
  
 Use the **Filter** property to selectively screen out records in a **Recordset** object. The filtered **Recordset** becomes the current cursor, which means that records that do not satisfy the **Filter** criteria are not available in the **Recordset** until the **Filter** is removed. Other properties that return values based on the current cursor are affected, such as **AbsolutePosition**, **AbsolutePage**, **RecordCount**, and **PageCount**. This is because setting the **Filter** property to a specific value will move the current record to the first record that satisfies the new value.  
  
 The **Filter** property takes a variant argument. This value represents one of three methods for using the **Filter** property: a criteria string, a **FilterGroupEnum** constant, or an array of bookmarks. For more information, see Filtering with a Criteria String, Filtering with a Constant, and Filtering with Bookmarks later in this topic.  
  
> [!NOTE]
>  When you know the data that you want to select, it is usually more efficient to open a **Recordset** with an SQL statement that effectively filters the result set, instead of relying on the **Filter** property.  
  
 To remove a filter from a **Recordset**, use the **adFilterNone** constant. Setting the **Filter** property to a zero-length string ("") has the same effect as using the **adFilterNone** constant.  
  
### Filtering with a Criteria String  
 The criteria string consists of clauses in the form *FieldName Operator Value* (for example, `"LastName = 'Smith'"`). You can create compound clauses by concatenating individual clauses with **AND** (for example, `"LastName = 'Smith' AND FirstName = 'John'"`) and **OR** (for example, `"LastName = 'Smith' OR LastName = 'Jones'"`). Use the following guidelines for criteria strings:  
  
-   *FieldName* must be a valid field name from the **Recordset**. If the field name contains spaces, you must enclose the name in square brackets.  
  
-   *Operator* must be one of the following: **\<**, **>**, **\<=**, **>=**, **<>**, **=**, or **LIKE**.  
  
-   *Value* is the value with which you will compare the field values (for example, `'Smith'`, `#8/24/95#`, `12.345`, or `$50.00`). Use single quotation marks (') with strings and pound signs (`#`) with dates. For numbers, you can use decimal points, dollar signs, and scientific notation. If *Operator* is **LIKE**, *Value* can use wildcard characters. Only the asterisk (\*) and percent sign (%) wildcard characters are allowed, and they must be the last character in the string. *Value* cannot be null.  
  
    > [!NOTE]
    >  To include single quotation marks (') in the filter *Value*, use two single quotation marks to represent one. For example, to filter on *O'Malley*, the criteria string should be `"col1 = 'O''Malley'"`. To include single quotation marks at both the beginning and the end of the filter value, enclose the string in pound signs (#). For example, to filter on *'1'*, the criteria string should be `"col1 = #'1'#"`.  
  
 There is no precedence between **AND** and **OR**. Clauses can be grouped within parentheses. However, you cannot group clauses joined by an **OR** and then join the group to another clause with an AND, as follows.  
  
```  
(LastName = 'Smith' OR LastName = 'Jones') AND FirstName = 'John'  
```  
  
 Instead, you would construct this filter as follows.  
  
```  
(LastName = 'Smith' AND FirstName = 'John') OR (LastName = 'Jones' AND FirstName = 'John')  
```  
  
 In a **LIKE** clause, you can use a wildcard at the beginning and end of the pattern (for example, `LastName Like '*mit*'`) or only at the end of the pattern (for example, `LastName Like 'Smit*'`).  
  
### Filtering with a Constant  
 The following constants are available for filtering **Recordsets**.  
  
|Constant|Description|  
|--------------|-----------------|  
|**adFilterAffectedRecords**|Filters for viewing only records affected by the last **Delete**, **Resync**, **UpdateBatch**, or **CancelBatch** call.|  
|**adFilterConflictingRecords**|Filters for viewing the records that failed the last batch update.|  
|**adFilterFetchedRecords**|Filters for viewing the records in the current cache - that is, the results of the last call to retrieve records from the database.|  
|**adFilterNone**|Removes the current filter and restores all records for viewing.|  
|**adFilterPendingRecords**|Filters for viewing only records that have changed but have not yet been sent to the server. Applicable only for batch update mode.|  
  
 The filter constants make it easier to resolve individual record conflicts during batch update mode by allowing you to view, for example, only those records that were affected during the last **UpdateBatch** method call, as shown in the following example.  
  
 `Attribute VB_Name = "modExaminingData"`  
  
### Filtering with Bookmarks  
 Finally, you can pass a variant array of bookmarks to the **Filter** property. The resulting cursor will contain only those records whose bookmark was passed in to the property. The following code example creates an array of bookmarks from the records in a **Recordset** which have a "B" in the *ProductName* field. It then passes the array to the **Filter** property and displays information about the resulting filtered **Recordset**.  
  
```  
'BeginFilterBkmk  
Dim vBkmkArray() As Variant  
Dim i As Integer  
  
'Recordset created using "SELECT * FROM Products" as command.  
'So, we will check to see if ProductName has a capital B, and  
'if so, add to the array.  
i = 0  
Do While Not objRs.EOF  
    If InStr(1, objRs("ProductName"), "B") Then  
        ReDim Preserve vBkmkArray(i)  
        vBkmkArray(i) = objRs.Bookmark  
        i = i + 1  
        Debug.Print objRs("ProductName")  
    End If  
    objRs.MoveNext  
Loop  
  
'Filter using the array of bookmarks.  
objRs.Filter = vBkmkArray  
  
objRs.MoveFirst  
Do While Not objRs.EOF  
    Debug.Print objRs("ProductName")  
    objRs.MoveNext  
Loop  
'EndFilterBkmk  
```  
  
## Creating a Clone of a Recordset  
 Use the **Clone** method to create multiple, duplicate **Recordset** objects, especially if you want to maintain more than one current record in a given set of records. Using the **Clone** method is more efficient than creating and opening a new **Recordset** object with the same definition as the original.  
  
 The current record of a newly created clone is originally set to the first record. The current record pointer in a cloned **Recordset** is not synchronized with the original or vice versa. You can navigate independently in each **Recordset**.  
  
 Changes you make to one **Recordset** object are visible in all of its clones regardless of cursor type. However, after you execute [Requery](../../../ado/reference/ado-api/requery-method.md) on the original **Recordset**, the clones will no longer be synchronized to the original.  
  
 Closing the original **Recordset** does not close its copies, nor does closing a copy close the original or any of the other copies.  
  
 You can clone a **Recordset** object only if it supports bookmarks. Bookmark values are interchangeable; that is, a bookmark reference from one **Recordset** object refers to the same record in any of its clones.
