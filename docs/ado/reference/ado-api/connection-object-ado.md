---
title: "Connection Object (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection"
helpviewer_keywords: 
  - "Connection object [ADO]"
ms.assetid: ef6b1824-5b12-43db-89d7-8f3d13896d4d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connection Object (ADO)
Represents an open connection to a data source.  
  
## Remarks  
 A **Connection** object represents a unique session with a data source. In a client/server database system, it may be equivalent to an actual network connection to the server. Depending on the functionality supported by the provider, some collections, methods, or properties of a **Connection** object may not be available.  
  
 With the collections, methods, and properties of a **Connection** object, you can do the following:  
  
-   Configure the connection before opening it with the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md), [ConnectionTimeout](../../../ado/reference/ado-api/connectiontimeout-property-ado.md), and [Mode](../../../ado/reference/ado-api/mode-property-ado.md) properties. **ConnectionString** is the default property of the **Connection** object.  
  
-   Set the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to client to invoke the [Microsoft Cursor Service for OLE DB](../../../ado/guide/appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md), which supports batch updates.  
  
-   Set the default database for the connection with the [DefaultDatabase](../../../ado/reference/ado-api/defaultdatabase-property.md) property.  
  
-   Set the level of isolation for the transactions opened on the connection with the [IsolationLevel](../../../ado/reference/ado-api/isolationlevel-property.md) property.  
  
-   Specify an OLE DB provider with the [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property.  
  
-   Establish, and later break, the physical connection to the data source with the [Open](../../../ado/reference/ado-api/open-method-ado-connection.md) and [Close](../../../ado/reference/ado-api/close-method-ado.md) methods.  
  
-   Execute a command on the connection with the [Execute](../../../ado/reference/ado-api/execute-method-ado-connection.md) method and configure the execution with the [CommandTimeout](../../../ado/reference/ado-api/commandtimeout-property-ado.md) property.  
  
    > [!NOTE]
    >  To execute a query without using a Command object, pass a query string to the **Execute** method of a **Connection** object. However, a [Command](../../../ado/reference/ado-api/command-object-ado.md) object is required when you want to persist the command text and re-execute it, or use query parameters.  
  
-   Manage transactions on the open connection, including nested transactions if the provider supports them, with the [BeginTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md), [CommitTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md), and [RollbackTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) methods and the [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) property.  
  
-   Examine errors returned from the data source with the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection.  
  
-   Read the version from the ADO implementation used with the [Version](../../../ado/reference/ado-api/version-property-ado.md) property.  
  
-   Obtain schema information about your database with the [OpenSchema](../../../ado/reference/ado-api/openschema-method.md) method.  
  
 You can create **Connection** objects independently of any other previously defined object.  
  
 You can execute named commands or stored procedures as if they were native methods on a **Connection** object, as shown in the next section. When a named command has the same name as that of a stored procedure, invoke the "native method call" on a **Connection** object always execute the named command instead of the store procedure.  
  
> [!NOTE]
>  Do not use this feature (calling a named command or stored procedure as if it were a native method on the **Connection** object) in a Microsoft® .NET Framework application, because the underlying implementation of the feature conflicts with the way the .NET Framework interoperates with COM.  
  
## Execute a command as a native method of a Connection object  
 To execute a command, give the command a name using the **Command** object [Name](../../../ado/reference/ado-api/name-property-ado.md) property. Set the **ActiveConnection** property of the **Command** object to the connection. Then issue a statement where the command name is used as if it were a method on the **Connection** object, followed by any parameters, and a **Recordset** object if any rows are returned. Set the **Recordset** properties to customize the resulting **Recordset**. For example:  
  
```  
Dim cnn As New ADODB.Connection  
Dim cmd As New ADODB.Command  
Dim rst As New ADODB.Recordset  
...  
cnn.Open "..."  
cmd.Name = "yourCommandName"  
cmd.ActiveConnection = cnn  
...  
'Your command name, any parameters, and an optional Recordset.  
cnn. "parameter", rst  
```  
  
## Execute a stored procedure as a native method of a Connection object  
 To execute a stored procedure, issue a statement where the stored procedure name is used as if it were a method on the **Connection** object, followed by any parameters. ADO will make a "best guess" of parameter types. For example:  
  
```  
Dim cnn As New ADODB.Connection  
...  
'Your stored procedure name and any parameters.  
cnn. "parameter"  
```  
  
 The **Connection** object is safe for scripting.  
  
 This section contains the following topic.  
  
-   [Connection Object Properties, Methods, and Events](../../../ado/reference/ado-api/connection-object-properties-methods-and-events.md)  
  
## See Also  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)   
 [Errors Collection (ADO)](../../../ado/reference/ado-api/errors-collection-ado.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)   
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)
