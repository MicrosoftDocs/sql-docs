---
title: "Refresh Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Collection::Refresh"
  - "Parameters::Refresh"
  - "Properties::Refresh"
helpviewer_keywords: 
  - "Refresh method [ADO]"
ms.assetid: 089b7ca7-684f-4259-8032-5bd1ecc54426
author: MightyPen
ms.author: genemi
manager: craigg
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
 Using the **Refresh** method on the [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection of a [Command](../../../ado/reference/ado-api/command-object-ado.md) object retrieves provider-side parameter information for the stored procedure or parameterized query specified in the **Command** object. The collection will be empty for providers that do not support stored procedure calls or parameterized queries.  
  
 You should set the [ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md) property of the **Command** object to a valid [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object, the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) property to a valid command, and the [CommandType](../../../ado/reference/ado-api/commandtype-property-ado.md) property to **adCmdStoredProc** before calling the **Refresh** method.  
  
 If you access the **Parameters** collection before calling the **Refresh** method, ADO will automatically call the method and populate the collection for you.  
  
> [!NOTE]
>  If you use the **Refresh** method to obtain parameter information from the provider and it returns one or more variable-length data type [Parameter](../../../ado/reference/ado-api/parameter-object.md) objects, ADO may allocate memory for the parameters based on their maximum potential size, which will cause an error during execution. You should explicitly set the [Size](../../../ado/reference/ado-api/size-property-ado-parameter.md) property for these parameters before calling the [Execute](../../../ado/reference/ado-api/execute-method-ado-command.md) method to prevent errors.  
  
### Fields  
 Using the **Refresh** method on the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection has no visible effect. To retrieve changes from the underlying database structure, you must use either the [Requery](../../../ado/reference/ado-api/requery-method.md) method or, if the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object does not support bookmarks, the [MoveFirst](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md) method.  
  
### Properties  
 Using the **Refresh** method on a **Properties** collection of some objects populates the collection with the dynamic properties that the provider exposes. These properties provide information about functionality specific to the provider, beyond the built-in properties ADO supports.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Axes Collection](../../../ado/reference/ado-md-api/axes-collection-ado-md.md)|[Columns Collection](../../../ado/reference/adox-api/columns-collection-adox.md)|[CubeDefs Collection](../../../ado/reference/ado-md-api/cubedefs-collection-ado-md.md)|  
|[Dimensions Collection](../../../ado/reference/ado-md-api/dimensions-collection-ado-md.md)|[Errors Collection](../../../ado/reference/ado-api/errors-collection-ado.md)|[Fields Collection](../../../ado/reference/ado-api/fields-collection-ado.md)|  
|[Groups Collection](../../../ado/reference/adox-api/groups-collection-adox.md)|[Hierarchies Collection](../../../ado/reference/ado-md-api/hierarchies-collection-ado-md.md)|[Indexes Collection](../../../ado/reference/adox-api/indexes-collection-adox.md)|  
|[Keys Collection](../../../ado/reference/adox-api/keys-collection-adox.md)|[Levels Collection](../../../ado/reference/ado-md-api/levels-collection-ado-md.md)|[Members Collection](../../../ado/reference/ado-md-api/members-collection-ado-md.md)|  
|[Parameters Collection](../../../ado/reference/ado-api/parameters-collection-ado.md)|[Positions Collection](../../../ado/reference/ado-md-api/positions-collection-ado-md.md)|[Procedures Collection](../../../ado/reference/adox-api/procedures-collection-adox.md)|  
|[Properties Collection](../../../ado/reference/ado-api/properties-collection-ado.md)|[Tables Collection](../../../ado/reference/adox-api/tables-collection-adox.md)|[Users Collection](../../../ado/reference/adox-api/users-collection-adox.md)|  
|[Views Collection](../../../ado/reference/adox-api/views-collection-adox.md)|||  
  
## See Also  
 [Refresh Method Example (VB)](../../../ado/reference/ado-api/refresh-method-example-vb.md)   
 [Refresh Method Example (VC++)](../../../ado/reference/ado-api/refresh-method-example-vc.md)   
 [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)   
 [Refresh Method (RDS)](../../../ado/reference/rds-api/refresh-method-rds.md)
