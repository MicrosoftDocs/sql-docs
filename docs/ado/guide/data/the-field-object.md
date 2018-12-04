---
title: "The Field Object | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Field object [ADO]"
ms.assetid: 7d1c4ad5-4be3-42ab-b516-e7133ca300bc
author: MightyPen
ms.author: genemi
manager: craigg
---
# The Field Object
Each **Field** object usually corresponds to a column in a database table. However, a **Field** can also represent a pointer to another **Recordset**, called a chapter. Exceptions, such as chapter columns, will be covered later in this guide.  
  
 Use the **Value** property of **Field** objects to set or return data for the current record. Depending on the functionality the provider exposes, some collections, methods, or properties of a **Field** object may not be available.  
  
 With the collections, methods, and properties of a **Field** object, you can do the following:  
  
-   Return the name of a field by using the **Name** property.  
  
-   View or change the data in the field by using the **Value** property. **Value** is the default property of the **Field** object.  
  
-   Return the basic characteristics of a field by using the **Type**, **Precision**, and **NumericScale** properties.  
  
-   Return the declared size of a field by using the **DefinedSize** property.  
  
-   Return the actual size of the data in a given field by using the **ActualSize** property.  
  
-   Determine what types of functionality are supported for a given field by using the **Attributes** property and **Properties** collection.  
  
-   Manipulate the values of fields containing long binary or long character data by using the **AppendChunk** and **GetChunk** methods.  
  
 Resolve discrepancies in field values during batch updating by using the **OriginalValue** and **UnderlyingValue** properties, if the provider supports batch updates.  
  
## Describing a Field  
 The topics that follow will discuss properties of the [Field](../../../ado/reference/ado-api/field-object.md) object that represent information that describes the **Field** object itself - that is, metadata about the field. This information can be used to determine much about the schema of the **Recordset**. These properties include **Type**, **DefinedSize** and **ActualSize**, **Name**, and **NumericScale** and **Precision**.  
  
### Discovering the Data Type  
 The **Type** property indicates the data type of the field. The data type enumerated constants that are supported by ADO are described in [DataTypeEnum](../../../ado/reference/ado-api/datatypeenum.md) in the *ADO Programmer's Reference*.  
  
 For floating point numeric types such **adNumeric**, you can obtain more information. The **NumericScale** property indicates how many digits to the right of the decimal point will be used to represent values for the **Field**. The **Precision** property specifies the maximum number of digits used to represent values for the **Field**.  
  
### Determining Field Size  
 Use the **DefinedSize** property to determine the data capacity of a **Field** object.  
  
 Use the **ActualSize** property to return the actual length of a **Field** object's value. For all fields, the **ActualSize** property is read-only. If ADO cannot determine the length of the **Field** object's value, the **ActualSize** property returns **adUnknown**.  
  
 The **DefinedSize** and **ActualSize** properties have different purposes. For example, consider a **Field** object with a declared type of **adVarChar** and a **DefinedSize** property value of 50, containing a single character. The **ActualSize** property value it returns is the length in bytes of the single character.  
  
### Determining Field Contents  
 The identifier of the column from the data source is represented by the **Name** property of the **Field**. The **Value** property of the **Field** object returns or sets the actual data content of the field. This is the default property.  
  
 To change the data in a field, set the **Value** property equal to a new value of the correct type. Your cursor type must support updates to change the contents of a field. Database validation is not done here in batch mode, so you will need to check for errors when you call **UpdateBatch** in such a case. Some providers also support the ADO **Field** object's **UnderlyingValue** and **OriginalValue** properties to assist you with resolving conflicts when you attempt to perform batch updates. For details about how to resolve such conflicts, see [Editing Data](../../../ado/guide/data/editing-data.md).  
  
> [!NOTE]
>  **Recordset Field** values cannot be set when appending new **Fields** to a **Recordset**. Rather, new **Fields** can be appended to a closed **Recordset**. Then the **Recordset** must be opened, and only then can values be assigned to these **Fields**.  
  
### Getting More Field Information  
 ADO objects have two types of properties: built-in and dynamic. To this point, only the built-in properties of the **Field** object have been discussed.  
  
 Built-in properties are those properties implemented in ADO and immediately available to any new object, using the `MyObject.Property` syntax. They do not appear as **Property** objects in an object's **Properties** collection.  
  
 Dynamic properties are defined by the underlying data provider, and appear in the **Properties** collection for the appropriate ADO object. For example, a property specific to the provider may indicate if a **Recordset** object supports transactions or updating. These additional properties will appear as **Property** objects in that **Recordset** object's **Properties** collection. Dynamic properties can be referenced only through the collection, using the syntax `MyObject.Properties(0)` or `MyObject.Properties("Name")`.  
  
 You cannot delete either kind of property.  
  
 A dynamic **Property** object has four built-in properties of its own:  
  
-   The **Name** property is a string that identifies the property.  
  
-   The **Type** property is an integer that specifies the property data type.  
  
-   The **Value** property is a variant that contains the property setting. **Value** is the default property for a **Property** object.  
  
-   The **Attributes** property is a **Long** value that indicates characteristics of the property specific to the provider.  
  
 The **Properties** collection for the **Field** object contains additional metadata about the field. The contents of this collection vary depending upon the provider. The following code example examines the **Properties** collection of the sample **Recordset** introduced at the beginning of this section. It first looks at the contents of the collection. This code uses the [OLE DB Provider for SQL Server](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md), so the **Properties** collection contains information relevant to that provider.  
  
```  
'BeginFieldProps  
    Dim objProp As ADODB.Property  
  
    For intLoop = 0 To (objFields.Count - 1)  
        Debug.Print objFields.Item(intLoop).Name  
  
        For Each objProp In objFields(intLoop).Properties  
            Debug.Print vbTab & objProp.Name & " = " & objProp.Value  
        Next objProp  
    Next intLoop  
'EndFieldProps  
```  
  
### Dealing with Binary Data  
 Use the [AppendChunk](../../../ado/reference/ado-api/appendchunk-method-ado.md) method on a **Field** object to fill it with long binary or character data. In situations where system memory is limited, you can use the **AppendChunk** method to manipulate long values in portions rather than in their entirety.  
  
 If the **adFldLong** bit in the **Attributes** property of a **Field** object is set to **True**, you can use the **AppendChunk** method for that field.  
  
 The first **AppendChunk** call on a **Field** object writes data to the field, overwriting any existing data. Subsequent **AppendChunk** calls add to existing data. If you are appending data to one field and then you set or read the value of another field in the current record, ADO assumes that you are finished appending data to the first field. If you call the **AppendChunk** method on the first field again, ADO interprets the call as a new **AppendChunk** operation and overwrites the existing data. Accessing fields in other **Recordset** objects that are not clones of the first **Recordset** object will not disrupt **AppendChunk** operations.  
  
 Use the **GetChunk** method on a **Field** object to retrieve part or all of its long binary or character data. In situations where system memory is limited, you can use the **GetChunk** method to manipulate long values in portions, rather than in their entirety.  
  
 The data that a **GetChunk** call returns is assigned to *variable*. If *Size* is greater than the remaining data, the **GetChunk** method returns only the remaining data without padding *variable* with empty spaces. If the field is empty, the **GetChunk** method returns a null value.  
  
 Each subsequent **GetChunk** call retrieves data starting from where the previous **GetChunk** call left off. However, if you are retrieving data from one field and then set or read the value of another field in the current record, ADO assumes you have finished retrieving data from the first field. If you call the **GetChunk** method on the first field again, ADO interprets the call as a new **GetChunk** operation and starts reading from the beginning of the data. Accessing fields in other **Recordset** objects that are not clones of the first **Recordset** object will not disrupt **GetChunk** operations.  
  
 If the **adFldLong** bit in the **Attributes** property of a **Field** object is set to **True**, you can use the **GetChunk** method for that field.  
  
 If there is no current record when you use the **GetChunk** or **AppendChunk** method on a **Field** object, error 3021 (no current record) occurs.  
  
 For an example of using these methods to manipulate binary data, see the [AppendChunk Method](../../../ado/reference/ado-api/appendchunk-method-ado.md) and [GetChunk Method](../../../ado/reference/ado-api/getchunk-method-ado.md) examples in the *ADO Programmer's Reference*.
