---
title: "Command Object (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Command"
helpviewer_keywords: 
  - "Command object [ADO]"
ms.assetid: a02c22fb-542d-465e-a629-30fd59dcbebf
author: MightyPen
ms.author: genemi
manager: craigg
---
# Command Object (ADO)
Defines a specific command that you intend to execute against a data source.  
  
## Remarks  
 Use a **Command** object to query a database and return records in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object, to execute a bulk operation, or to manipulate the structure of a database. Depending on the functionality of the provider, some **Command** collections, methods, or properties may generate an error when they are referenced.  
  
 With the collections, methods, and properties of a **Command** object, you can do the following:  
  
-   Define the executable text of the command (for example, an SQL statement) with the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) property. Alternatively, for command or query structures other than simple strings (for example, XML template queries) define the command with the [CommandStream](../../../ado/reference/ado-api/commandstream-property-ado.md) property.  
  
-   Optionally, indicate the command dialect used in the **CommandText** or **CommandStream** with the [Dialect](../../../ado/reference/ado-api/dialect-property.md) property.  
  
-   Define parameterized queries or stored-procedure arguments with [Parameter](../../../ado/reference/ado-api/parameter-object.md) objects and the [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection.  
  
-   Indicate whether parameter names should be passed to the provider with the [NamedParameters](../../../ado/reference/ado-api/namedparameters-property-ado.md) property.  
  
-   Execute a command and return a **Recordset** object if appropriate with the [Execute](../../../ado/reference/ado-api/execute-method-ado-command.md) method.  
  
-   Specify the type of command with the [CommandType](../../../ado/reference/ado-api/commandtype-property-ado.md) property prior to execution to optimize performance.  
  
-   Control whether the provider saves a prepared (or compiled) version of the command prior to execution with the [Prepared](../../../ado/reference/ado-api/prepared-property-ado.md) property.  
  
-   Set the number of seconds that a provider will wait for a command to execute with the [CommandTimeout](../../../ado/reference/ado-api/commandtimeout-property-ado.md) property.  
  
-   Associate an open connection with a **Command** object by setting its [ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md) property.  
  
-   Set the [Name](../../../ado/reference/ado-api/name-property-ado.md) property to identify the **Command** object as a method on the associated [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object.  
  
-   Pass a **Command** object to the [Source](../../../ado/reference/ado-api/source-property-ado-recordset.md) property of a **Recordset** to obtain data.  
  
-   Access provider-specific attributes with the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
> [!NOTE]
>  To execute a query without using a **Command** object, pass a query string to the [Execute](../../../ado/reference/ado-api/execute-method-ado-connection.md) method of a **Connection** object or to the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) method of a **Recordset** object. However, a **Command** object is required when you want to persist the command text and re-execute it, or use query parameters.  
  
 To create a **Command** object independently of a previously defined **Connection** object, set its **ActiveConnection** property to a valid connection string. ADO still creates a **Connection** object, but it does not assign that object to an object variable. However, if you are associating multiple **Command** objects with the same connection, you should explicitly create and open a **Connection** object; this assigns the **Connection** object to an object variable. Make sure that the **Connection** object was opened successfully before you assign it to the **ActiveConnection** property of the **Command** object, because assigning a closed **Connection** object causes an error. If you do not set the **ActiveConnection** property of the **Command** object to this object variable, ADO creates a new **Connection** object for each **Command** object, even if you use the same connection string.  
  
 To execute a **Command**, call it by its [Name](../../../ado/reference/ado-api/name-property-ado.md) property on the associated **Connection** object. The **Command** must have its **ActiveConnection** property set to the **Connection** object. If the **Command** has parameters, pass their values as arguments to the method.  
  
 If two or more **Command** objects are executed on the same connection and either **Command** object is a stored procedure with output parameters, an error occurs. To execute each **Command** object, use separate connections or disconnect all other **Command** objects from the connection.  
  
 The **Parameters** collection is the default member of the **Command** object. As a result, the following two code statements are equivalent.  
  
```  
objCmd.Parameters.Item(0)  
objCmd(0)  
```  
  
-   The **Command** object is not safe for scripting.  
  
 This section contains the following topic.  
  
-   [Command Object Properties, Methods, and Events](../../../ado/reference/ado-api/command-object-properties-methods-and-events.md)  
  
## See Also  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [Parameters Collection (ADO)](../../../ado/reference/ado-api/parameters-collection-ado.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)   
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)
