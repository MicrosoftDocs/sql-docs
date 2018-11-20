---
title: "Microsoft Cursor Service for OLE DB (ADO Service Component) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "providers [ADO], cursor service for OLE DB"
  - "cursor service for OLE DB [ADO]"
ms.assetid: 420d0989-7cfb-4c66-a7b5-f4199d13165d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft Cursor Service for OLE DB Overview
The Microsoft Cursor Service for OLE DB supplements the cursor support functions of data providers. As a result, the user perceives relatively uniform functionality from all data providers.

 The Cursor Service makes dynamic properties available and enhances the behavior of certain methods. For example, the [Optimize](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md) dynamic property enables the creation of temporary indexes to facilitate certain operations, such as the [Find](../../../ado/reference/ado-api/find-method-ado.md) method.

 The Cursor Service enables support for batch updating in all cases. It also simulates more capable cursor types, such as dynamic cursors, when a data provider can only supply less capable cursors, such as static cursors.

## Keyword
 To invoke this service component, set the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) or [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object's [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to **adUseClient**.

```vb
connection.CursorLocation=adUseClient
recordset.CursorLocation=adUseClient
```

## Dynamic Properties
 When the Cursor Service for OLE DB is invoked, the following dynamic properties are added to the **Recordset** object's [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection. The full list of **Connection** and **Recordset** object dynamic properties is listed in the [ADO Dynamic Property Index](../../../ado/reference/ado-api/ado-dynamic-property-index.md). The associated OLE DB property names, where appropriate, are included in parentheses after the ADO property name.

 Changes to some dynamic properties are not visible to the underlying data source after the Cursor Service has been invoked. For example, setting the *Command Time out* property on a **Recordset** will not be visible to the underlying data provider.

```vb

Recordset1.CursorLocation = adUseClient     'invokes cursor service
Recordset1.Open "authors", _
    "Provider=SQLOLEDB;Data Source=DBServer;User Id=MyUserID;" & _
    "Password=MyPassword;Initial Catalog=pubs;",,adCmdTable
Recordset1.Properties.Item("Command Time out") = 50
' 'Command Time out' property on DBServer is still default (30).

```

 If your application requires the Cursor Service, but you need to set dynamic properties on the underlying provider, set the properties before invoking the Cursor Service. Command object property settings are always passed to the underlying data provider regardless of cursor location. Therefore, you can also use a command object to set the properties at any time.

> [!NOTE]
>  The dynamic property DBPROP_SERVERDATAONINSERT is not supported by the cursor service, even if it is supported by the underlying data provider.

|Property Name|Description|
|-------------------|-----------------|
|Auto Recalc (DBPROP_ADC_AUTORECALC)|For recordsets created with the Data Shaping Service, this value indicates how often calculated and aggregate columns are calculated. The default (value=1) is to recalculate whenever the Data Shaping Service determines that the values have changed. If the value is 0, the calculated or aggregate columns are only calculated when the hierarchy is initially built.|
|Batch Size (DBPROP_ADC_BATCHSIZE)|Indicates the number of update statements that can be batched before being sent to the data store. The more statements in a batch, the fewer round trips to the data store.|
|Cache Child Rows (DBPROP_ADC_CACHECHILDROWS)|For recordsets created with the Data Shaping Service, this value indicates whether child recordsets are stored in a cache for later use.|
|Cursor Engine Version (DBPROP_ADC_CEVER)|Indicates the version of the Cursor Service being used.|
|Maintain Change Status (DBPROP_ADC_MAINTAINCHANGESTATUS)|Indicates the text of the command used for resynchronizing a one or more rows in a multiple table join.|
|[Optimize](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md)|Indicates whether an index should be created. When set to **True**, authorizes the temporary creation of indexes to improve the execution of certain operations.|
|[Reshape Name](../../../ado/reference/ado-api/reshape-name-property-dynamic-ado.md)|Indicates the name of the **Recordset**. Can be referenced within the current, or subsequent, data shaping commands.|
|[Resync Command](../../../ado/reference/ado-api/resync-command-property-dynamic-ado.md)|Indicates a custom command string that is used by the [Resync](../../../ado/reference/ado-api/resync-method.md) method when the [Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) property is in effect.|
|[Unique Catalog](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md)|Indicates the name of the database containing the table referenced in the **Unique Table** property.|
|[Unique Schema](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md)|Indicates the name of the owner of the table referenced in the **Unique Table** property.|
|[Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md)|Indicates the name of the one table in a **Recordset** created from multiple tables that can be modified by insertions, updates, or deletions.|
|Update Criteria (DBPROP_ADC_UPDATECRITERIA)|Indicates which fields in the **WHERE** clause are used to handle collisions occurring during an update.|
|[Update Resync](../../../ado/reference/ado-api/update-resync-property-dynamic-ado.md) (DBPROP_ADC_UPDATERESYNC)|Indicates whether the **Resync** method is implicitly invoked after the [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) method (and its behavior), when the **Unique Table** property is in effect.|

 You can also set or retrieve a dynamic property by specifying its name as the index to the **Properties** collection. For example, get and print the current value of the [Optimize](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md) dynamic property, then set a new value, as follows:

```vb
Debug.Print rs.Properties("Optimize")
rs.Properties("Optimize") = True
```

## Built-in Property Behavior
 The Cursor Service for OLE DB also affects the behavior of certain built-in properties.

|Property Name|Description|
|-------------------|-----------------|
|[CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md)|Supplements the types of cursors that are available for a **Recordset**.|
|[LockType](../../../ado/reference/ado-api/locktype-property-ado.md)|Supplements the types of locks available for a **Recordset**. Enables batch updates.|
|[Sort](../../../ado/reference/ado-api/sort-property.md)|Specifies one or more field names that the **Recordset** is sorted on, and whether each field is sorted in ascending or descending order.|

## Method Behavior
 The Cursor Service for OLE DB enables or affects the behavior of the [Field](../../../ado/reference/ado-api/field-object.md) object's [Append](../../../ado/reference/ado-api/append-method-ado.md) method; and the **Recordset** object's [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md), [Resync](../../../ado/reference/ado-api/resync-method.md), [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md), and [Save](../../../ado/reference/ado-api/save-method.md) methods.
