---
title: "Microsoft OLE DB Remoting Provider (ADO Service Provider) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "OLE DB remoting provider [ADO]"
  - "providers [ADO], OLE DB remoting provider"
  - "remoting provider [ADO]"
ms.assetid: a4360ed4-b70f-4734-9041-4025d033346b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft OLE DB Remoting Provider Overview
The Microsoft OLE DB Remoting Provider enables a local user on a client machine to invoke data providers on a remote machine. Specify the data provider parameters for the remote machine as you would if you were a local user on the remote machine. Then specify the parameters used by the Remoting Provider to access the remote machine. You can then access the remote machine as if you were a local user.

> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to  [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).

## Provider Keyword
 To invoke the OLE DB Remoting Provider, specify the following keyword and value in the connection string. (Note the blank space in the provider name.)

```vb
"Provider=MS Remote"
```

## Additional Keywords
 When this service provider is invoked, the following additional keywords are relevant.

|Keyword|Description|
|-------------|-----------------|
|**Data Source**|Specifies the name of the remote data source. It is passed to the OLE DB Remoting Provider for processing.<br /><br /> This keyword is equivalent to the [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object's [Connect](../../../ado/reference/rds-api/connect-property-rds.md) property.|

## Dynamic Properties
 When this service provider is invoked, the following dynamic properties are added to the [Connection](../../../ado/reference/ado-api/connection-object-ado.md)object's [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.

|Dynamic Property Name|Description|
|---------------------------|-----------------|
|**DFMode**|Indicates the DataFactory Mode. A string that specifies the desired version of the [DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md) object on the server. Set this property before opening a connection to request a particular version of the **DataFactory**. If the requested version is not available, an attempt will be made to use the preceding version. If there is no preceding version, an error will occur. If **DFMode** is less than the available version, an error will occur. This property is read-only after a connection is made.<br /><br /> Can be one of the following valid string values:<br /><br /> -   "25"-Version 2.5 (Default)<br />-   "21"-Version 2.1<br />-   "20"-Version 2.0<br />-   "15"-Version 1.5|
|**Command Properties**|Indicates values that will be added to the string of command (rowset) properties sent to the server by the MS Remote provider. The default value for this string is vt_empty.|
|**Current DFMode**|Indicates the actual version number of the **DataFactory** on the server. Check this property to see if the version requested in the **DFMode** property was honored.<br /><br /> Can be one of the following valid Long integer values:<br /><br /> -   25-Version 2.5 (Default)<br />-   21-Version 2.1<br />-   20-Version 2.0<br />-   15-Version 1.5<br /><br /> Adding "DFMode=20;" to your connection string when using the **MSRemote** provider can improve your server's performance when updating data. With this setting, the **RDSServer.DataFactory** object on the server uses a less resource-intensive mode. However, the following features are not available in this configuration:<br /><br /> -   Using parameterized queries.<br />-   Getting parameter or column information before calling the **Execute** method.<br />-   Setting **Transact Updates** to **True**.<br />-   Getting row status.<br />-   Calling the **Resync** method.<br />-   Refreshing (explicitly or automatically) via the **Update Resync** property.<br />-   Setting **Command** or **Recordset** properties.<br />-   Using **adCmdTableDirect**.|
|**Handler**|Indicates the name of a server-side customization program (or handler) that extends the functionality of the [RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md), and any parameters used by the handler*,* all separated by commas (","). A **String** value.|
|**Internet Timeout**|Indicates the maximum number of milliseconds to wait for a request to travel to and from the server. (The default is 5 minutes.)|
|**Remote Provider**|Indicates the name of the data provider to be used on the remote server.|
|**Remote Server**|Indicates the server name and communication protocol to be used by this connection. This property is equivalent to the [RDS.DataContro](../../../ado/reference/rds-api/datacontrol-object-rds.md) object [Server](../../../ado/reference/rds-api/server-property-rds.md) property.|
|**Transact Updates**|When set to **True**, this value indicates that when [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) is performed on the server, it will be done inside a transaction. The default value for this Boolean dynamic property is **False**.|

 You may also set writable dynamic properties by specifying their names as keywords in the connection string. For example, set the **Internet Timeout** dynamic property to five seconds by specifying:

```vb
Dim cn as New ADODB.Connection
cn.Open "Provider=MS Remote;Internet Timeout=5000"
```

 You may also set or retrieve a dynamic property by specifying its name as the index to the **Properties** property. The following example shows how to get and print the current value of the **Internet Timeout** dynamic property, and then set a new value:

```vb
Debug.Print cn.Properties("Internet Timeout")
cn.Properties("Internet Timeout") = 5000
```

## Remarks
 In ADO 2.0, the OLE DB Remoting Provider could only be specified in the *ActiveConnection* parameter of the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object **Open** method. Starting with ADO 2.1, the provider may also be specified in the *ConnectionString* parameter of the [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object **Open** method.

 The equivalent of the **RDS.DataControl** object [SQL](../../../ado/reference/rds-api/sql-property.md) property is not available. The [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object **Open** method *Source* argument is used instead.

 **Note** Specifying "...;Remote Provider=MS Remote;..." would create a four-tier scenario. Scenarios beyond three tiers have not been tested and should not be needed.

## Example
 This example performs a query on the **Authors** table of the **Pubs** database on a server named *YourServer*. The names of the remote data source and remote server are provided in the [Open](../../../ado/reference/ado-api/open-method-ado-connection.md) method of the[Connection](../../../ado/reference/ado-api/connection-object-ado.md) object, and the SQL query is specified in the[Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) method of the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object. A **Recordset** object is returned, edited, and used to update the data source.

```vb
Dim rs as New ADODB.Recordset
Dim cn as New ADODB.Connection
cn.Open  "Provider=MS Remote;Data Source=pubs;" & _
         "Remote Server=https://YourServer"
rs.Open "SELECT * FROM authors", cn
...                'Edit the recordset
rs.UpdateBatch     'Equivalent of RDS SubmitChanges
...
```

## See Also
 [Overview of the OLE DB Remoting Provider](https://msdn.microsoft.com/4083b72f-68c4-4252-b366-abb70db5ca2b)
