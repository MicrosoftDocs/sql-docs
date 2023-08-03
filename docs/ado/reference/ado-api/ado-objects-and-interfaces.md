---
title: "ADO Objects and Interfaces"
description: "ADO Objects and Interfaces"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ADO, objects and interfaces"
  - "objects [ADO]"
---
# ADO Objects and Interfaces
The relationships between these objects are represented in the [ADO Object Model](./ado-object-model.md).  
  
 Each object can be contained in its corresponding collection. For example, an [Error](./error-object.md) object can be contained in an [Errors](./errors-collection-ado.md) collection. For more information, see [ADO Collections](./ado-collections.md) or a specific collection topic.  
  
|Object or interface|Description|  
|-|-|  
|[IADOCommandConstruction](/previous-versions/windows/desktop/aa965677(v=vs.85))|Used to retrieve the underlying OLEDB Command from an ADOCommand object.|  
|[ADORecordConstruction](./adorecordconstruction-interface.md)|Constructs an ADO **Record** object from an OLE DB **Row** object in a C/C++ application.|  
|[ADORecordsetConstruction](./adorecordsetconstruction-interface.md)|Constructs an ADO **Recordset** object from an OLE DB **Rowset** object in a C/C++ application.|  
|[ADOStreamConstruction Interface](./adostreamconstruction-interface.md)|Constructs an ADO **Stream** object from an OLE DB **IStream** object in a C/C++ application.|  
|[Command](./command-object-ado.md)|Defines a specific command that you intend to execute against a data source.<br /><br /> The **Command** object is not safe for scripting.|  
|[Connection](./connection-object-ado.md)|Represents an open connection to a data source.<br /><br /> The **Connection** object is safe for scripting.|  
|[IDSOShapeExtensions Interface](./idsoshapeextensions-interface.md)|Gets the underlying OLEDB Data Source object for the SHAPE provider.|  
|[Error](./error-object.md)|Contains details about data access errors that pertain to a single operation involving the provider.<br /><br /> The **Error** object is not safe for scripting.|  
|[Field](./field-object.md)|Represents a column of data with a common data type.|  
|[Parameter](./parameter-object.md)|Represents a parameter or argument associated with a **Command** object based on a parameterized query or stored procedure.<br /><br /> The **Parameter** object is not safe for scripting.|  
|[Property](./property-object-ado.md)|Represents a dynamic characteristic of an ADO object that is defined by the provider.|  
|[Record](./record-object-ado.md)|Represents a row of a **Recordset**, or a directory or file in a file system. The **Record** object is safe for scripting.|  
|[Recordset](./recordset-object-ado.md)|Represents the set of records from a base table or the results of an executed command. At any time, the **Recordset** object refers to only a single record within the set as the current record.<br /><br /> The **Recordset** object is safe for scripting.|  
|[Stream](./stream-object-ado.md)|Represents a binary stream of data.<br /><br /> The **Stream** object is safe for scripting.|  
  
## See Also  
 [ADO API Reference](./ado-api-reference.md)   
 [ADO Collections](./ado-collections.md)   
 [ADO Dynamic Properties](./ado-dynamic-properties.md)   
 [ADO Enumerated Constants](./ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](./ado-events.md)   
 [ADO Methods](./ado-methods.md)   
 [ADO Object Model](./ado-object-model.md)   
 [ADO Properties](./ado-properties.md)