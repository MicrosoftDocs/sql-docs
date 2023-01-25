---
title: "Refresh Method (ADO)"
description: "Refresh Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Collection::Refresh"
  - "Parameters::Refresh"
  - "Properties::Refresh"
helpviewer_keywords:
  - "Refresh method [ADO]"
apitype: "COM"
---
# Refresh Method (ADO)
Updates the objects in a collection to reflect objects available from, and specific to, the provider.  
  
## Syntax  
  
```  
  
collection.Refresh  
```  
  
## Remarks  
 The **Refresh** method accomplishes different tasks depending on the collection from which you call it.  
  
### Parameters  
 Using the **Refresh** method on the [Parameters](./parameters-collection-ado.md) collection of a [Command](./command-object-ado.md) object retrieves provider-side parameter information for the stored procedure or parameterized query specified in the **Command** object. The collection will be empty for providers that do not support stored procedure calls or parameterized queries.  
  
 You should set the [ActiveConnection](./activeconnection-property-ado.md) property of the **Command** object to a valid [Connection](./connection-object-ado.md) object, the [CommandText](./commandtext-property-ado.md) property to a valid command, and the [CommandType](./commandtype-property-ado.md) property to **adCmdStoredProc** before calling the **Refresh** method.  
  
 If you access the **Parameters** collection before calling the **Refresh** method, ADO will automatically call the method and populate the collection for you.  
  
> [!NOTE]
>  If you use the **Refresh** method to obtain parameter information from the provider and it returns one or more variable-length data type [Parameter](./parameter-object.md) objects, ADO may allocate memory for the parameters based on their maximum potential size, which will cause an error during execution. You should explicitly set the [Size](./size-property-ado-parameter.md) property for these parameters before calling the [Execute](./execute-method-ado-command.md) method to prevent errors.  
  
### Fields  
 Using the **Refresh** method on the [Fields](./fields-collection-ado.md) collection has no visible effect. To retrieve changes from the underlying database structure, you must use either the [Requery](./requery-method.md) method or, if the [Recordset](./recordset-object-ado.md) object does not support bookmarks, the [MoveFirst](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md) method.  
  
### Properties  
 Using the **Refresh** method on a **Properties** collection of some objects populates the collection with the dynamic properties that the provider exposes. These properties provide information about functionality specific to the provider, beyond the built-in properties ADO supports.  
  
## Applies To  

:::row:::
    :::column:::
        [Axes Collection](../ado-md-api/axes-collection-ado-md.md)  
        [Columns Collection](../adox-api/columns-collection-adox.md)  
        [CubeDefs Collection](../ado-md-api/cubedefs-collection-ado-md.md)  
        [Dimensions Collection](../ado-md-api/dimensions-collection-ado-md.md)  
        [Errors Collection](./errors-collection-ado.md)  
        [Fields Collection](./fields-collection-ado.md)  
        [Groups Collection](../adox-api/groups-collection-adox.md)  
    :::column-end:::
    :::column:::
        [Hierarchies Collection](../ado-md-api/hierarchies-collection-ado-md.md)  
        [Indexes Collection](../adox-api/indexes-collection-adox.md)  
        [Keys Collection](../adox-api/keys-collection-adox.md)  
        [Levels Collection](../ado-md-api/levels-collection-ado-md.md)  
        [Members Collection](../ado-md-api/members-collection-ado-md.md)  
        [Parameters Collection](./parameters-collection-ado.md)  
    :::column-end:::
    :::column:::
        [Positions Collection](../ado-md-api/positions-collection-ado-md.md)  
        [Procedures Collection](../adox-api/procedures-collection-adox.md)  
        [Properties Collection](./properties-collection-ado.md)  
        [Tables Collection](../adox-api/tables-collection-adox.md)  
        [Users Collection](../adox-api/users-collection-adox.md)  
        [Views Collection](../adox-api/views-collection-adox.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Refresh Method Example (VB)](./refresh-method-example-vb.md)   
 [Refresh Method Example (VC++)](./refresh-method-example-vc.md)   
 [Count Property (ADO)](./count-property-ado.md)   
 [Refresh Method (RDS)](../rds-api/refresh-method-rds.md)