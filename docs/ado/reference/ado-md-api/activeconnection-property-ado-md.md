---
title: "ActiveConnection Property (ADO MD)"
description: "ActiveConnection Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ActiveConnection"
  - "Cellset::ActiveConnection"
  - "Catalog::ActiveConnection"
helpviewer_keywords:
  - "ActiveConnection property [ADO MD]"
apitype: "COM"
---
# ActiveConnection Property (ADO MD)
Indicates to which ADO [Connection](../ado-api/connection-object-ado.md) object the current cellset or catalog currently belongs.  
  
## Settings and Return Values  
 Sets or returns a **Variant** that contains a string defining a connection or **Connection** object. The default is empty.  
  
## Remarks  
 You can set this property to a valid ADO **Connection** object or to a valid connection string. When this property is set to a connection string, the provider creates a new **Connection** object using this definition and opens the connection.  
  
 If you use the *ActiveConnection* argument of the [Open](./open-method-ado-md.md) method to open a [Cellset](./cellset-object-ado-md.md) object, the **ActiveConnection** property will inherit the value of the argument.  
  
 Setting the **ActiveConnection** property of a [Catalog](./catalog-object-ado-md.md) object to **Nothing** releases the associated data, including data in the [CubeDefs](./cubedefs-collection-ado-md.md) collection and any related [Dimension](./dimension-object-ado-md.md), [Hierarchy](./hierarchy-object-ado-md.md), [Level](./level-object-ado-md.md), and [Member](./member-object-ado-md.md) objects. Closing a **Connection** object that was used to open a **Catalog** has the same effect as setting the **ActiveConnection** property to **Nothing**.  
  
 Changing the default database of the connection referenced by the **ActiveConnection** property of a **Catalog** object invalidates the contents of the **Catalog**.  
  
 An error will occur if you attempt to change the **ActiveConnection** property for an open **Cellset** object.  
  
> [!NOTE]
>  In Visual Basic, remember to use the **Set** keyword when setting the **ActiveConnection** property to a **Connection** object. If you omit the **Set** keyword, you will actually be setting the **ActiveConnection** property equal to the **Connection** object's default property, **ConnectionString**. The code will work; however, you will create an additional connection to the data source, which may have negative performance implications.  
  
 When using the MSOLAP data provider, set the data source in a connection string to a server name and set the initial catalog to the name of a catalog from the data source. To connect to a cube file that is disconnected from a server, set the location to the full path to the .CUB file. In either case, set the provider to the provider name. For example, the following string uses the MSOLAP Provider to connect to a catalog named Bobs Video Store on a server named **Servername**:  
  
```  
"Data Source=Servername;Initial Catalog=Bobs Video Store;Provider=msolap"  
```  
  
 The following string connects to a local cube file at the location C:\MSDASDK\samples\oledb\olap\data\bobsvid.cub:  
  
```  
"Location=C:\MSDASDK\samples\oledb\olap\data\bobsvid.cub;Provider=msolap"  
```  
  
## Applies To  

:::row:::
    :::column:::
        [Catalog Object (ADO MD)](./catalog-object-ado-md.md)  
    :::column-end:::
    :::column:::
        [Cellset Object (ADO MD)](./cellset-object-ado-md.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Cellset Example (VB)](./cellset-example-vb.md)   
 [Connection Object (ADO)](../ado-api/connection-object-ado.md)   
 [Open Method (ADO MD)](./open-method-ado-md.md)