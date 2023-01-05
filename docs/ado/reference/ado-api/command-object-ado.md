---
title: "Command Object (ADO)"
description: "Command Object (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Command"
helpviewer_keywords:
  - "Command object [ADO]"
apitype: "COM"
---
# Command Object (ADO)
Defines a specific command that you intend to execute against a data source.  
  
## Remarks  
 Use a **Command** object to query a database and return records in a [Recordset](./recordset-object-ado.md) object, to execute a bulk operation, or to manipulate the structure of a database. Depending on the functionality of the provider, some **Command** collections, methods, or properties may generate an error when they are referenced.  
  
 With the collections, methods, and properties of a **Command** object, you can do the following:  
  
-   Define the executable text of the command (for example, an SQL statement) with the [CommandText](./commandtext-property-ado.md) property. Alternatively, for command or query structures other than simple strings (for example, XML template queries) define the command with the [CommandStream](./commandstream-property-ado.md) property.  
  
-   Optionally, indicate the command dialect used in the **CommandText** or **CommandStream** with the [Dialect](./dialect-property.md) property.  
  
-   Define parameterized queries or stored-procedure arguments with [Parameter](./parameter-object.md) objects and the [Parameters](./parameters-collection-ado.md) collection.  
  
-   Indicate whether parameter names should be passed to the provider with the [NamedParameters](./namedparameters-property-ado.md) property.  
  
-   Execute a command and return a **Recordset** object if appropriate with the [Execute](./execute-method-ado-command.md) method.  
  
-   Specify the type of command with the [CommandType](./commandtype-property-ado.md) property prior to execution to optimize performance.  
  
-   Control whether the provider saves a prepared (or compiled) version of the command prior to execution with the [Prepared](./prepared-property-ado.md) property.  
  
-   Set the number of seconds that a provider will wait for a command to execute with the [CommandTimeout](./commandtimeout-property-ado.md) property.  
  
-   Associate an open connection with a **Command** object by setting its [ActiveConnection](./activeconnection-property-ado.md) property.  
  
-   Set the [Name](./name-property-ado.md) property to identify the **Command** object as a method on the associated [Connection](./connection-object-ado.md) object.  
  
-   Pass a **Command** object to the [Source](./source-property-ado-recordset.md) property of a **Recordset** to obtain data.  
  
-   Access provider-specific attributes with the [Properties](./properties-collection-ado.md) collection.  
  
> [!NOTE]
>  To execute a query without using a **Command** object, pass a query string to the [Execute](./execute-method-ado-connection.md) method of a **Connection** object or to the [Open](./open-method-ado-recordset.md) method of a **Recordset** object. However, a **Command** object is required when you want to persist the command text and re-execute it, or use query parameters.  
  
 To create a **Command** object independently of a previously defined **Connection** object, set its **ActiveConnection** property to a valid connection string. ADO still creates a **Connection** object, but it does not assign that object to an object variable. However, if you are associating multiple **Command** objects with the same connection, you should explicitly create and open a **Connection** object; this assigns the **Connection** object to an object variable. Make sure that the **Connection** object was opened successfully before you assign it to the **ActiveConnection** property of the **Command** object, because assigning a closed **Connection** object causes an error. If you do not set the **ActiveConnection** property of the **Command** object to this object variable, ADO creates a new **Connection** object for each **Command** object, even if you use the same connection string.  
  
 To execute a **Command**, call it by its [Name](./name-property-ado.md) property on the associated **Connection** object. The **Command** must have its **ActiveConnection** property set to the **Connection** object. If the **Command** has parameters, pass their values as arguments to the method.  
  
 If two or more **Command** objects are executed on the same connection and either **Command** object is a stored procedure with output parameters, an error occurs. To execute each **Command** object, use separate connections or disconnect all other **Command** objects from the connection.  
  
 The **Parameters** collection is the default member of the **Command** object. As a result, the following two code statements are equivalent.  
  
```  
objCmd.Parameters.Item(0)  
objCmd(0)  
```  
  
-   The **Command** object is not safe for scripting.  
  
 This section contains the following topic.  
  
-   [Command Object Properties, Methods, and Events](./command-object-properties-methods-and-events.md)  
  
## See Also  
 [Connection Object (ADO)](./connection-object-ado.md)   
 [Parameters Collection (ADO)](./parameters-collection-ado.md)   
 [Properties Collection (ADO)](./properties-collection-ado.md)   
 [Appendix A: Providers](../../guide/appendixes/appendix-a-providers.md)