---
title: "ADO Objects and Interfaces | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "ADO, objects and interfaces"
  - "objects [ADO]"
ms.assetid: d0b7e254-c89f-4406-b846-a060ef038c30
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Objects and Interfaces
The relationships between these objects are represented in the [ADO Object Model](../../../ado/reference/ado-api/ado-object-model.md).  
  
 Each object can be contained in its corresponding collection. For example, an [Error](../../../ado/reference/ado-api/error-object.md) object can be contained in an [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection. For more information, see [ADO Collections](../../../ado/reference/ado-api/ado-collections.md) or a specific collection topic.  
  
|||  
|-|-|  
|[IADOCommandConstruction](https://msdn.microsoft.com/library/windows/desktop/aa965677.aspx)|Used to retrieve the underlying OLEDB Command from an ADOCommand object.|  
|[ADORecordConstruction](../../../ado/reference/ado-api/adorecordconstruction-interface.md)|Constructs an ADO **Record** object from an OLE DB **Row** object in a C/C++ application.|  
|[ADORecordsetConstruction](../../../ado/reference/ado-api/adorecordsetconstruction-interface.md)|Constructs an ADO **Recordset** object from an OLE DB **Rowset** object in a C/C++ application.|  
|[ADOStreamConstruction Interface](../../../ado/reference/ado-api/adostreamconstruction-interface.md)|Constructs an ADO **Stream** object from an OLE DB **IStream** object in a C/C++ application.|  
|[Command](../../../ado/reference/ado-api/command-object-ado.md)|Defines a specific command that you intend to execute against a data source.<br /><br /> The **Command** object is not safe for scripting.|  
|[Connection](../../../ado/reference/ado-api/connection-object-ado.md)|Represents an open connection to a data source.<br /><br /> The **Connection** object is safe for scripting.|  
|[IDSOShapeExtensions Interface](../../../ado/reference/ado-api/idsoshapeextensions-interface.md)|Gets the underlying OLEDB Data Source object for the SHAPE provider.|  
|[Error](../../../ado/reference/ado-api/error-object.md)|Contains details about data access errors that pertain to a single operation involving the provider.<br /><br /> The **Error** object is not safe for scripting.|  
|[Field](../../../ado/reference/ado-api/field-object.md)|Represents a column of data with a common data type.|  
|[Parameter](../../../ado/reference/ado-api/parameter-object.md)|Represents a parameter or argument associated with a **Command** object based on a parameterized query or stored procedure.<br /><br /> The **Parameter** object is not safe for scripting.|  
|[Property](../../../ado/reference/ado-api/property-object-ado.md)|Represents a dynamic characteristic of an ADO object that is defined by the provider.|  
|[Record](../../../ado/reference/ado-api/record-object-ado.md)|Represents a row of a **Recordset**, or a directory or file in a file system. The **Record** object is safe for scripting.|  
|[Recordset](../../../ado/reference/ado-api/recordset-object-ado.md)|Represents the set of records from a base table or the results of an executed command. At any time, the **Recordset** object refers to only a single record within the set as the current record.<br /><br /> The **Recordset** object is safe for scripting.|  
|[Stream](../../../ado/reference/ado-api/stream-object-ado.md)|Represents a binary stream of data.<br /><br /> The **Stream** object is safe for scripting.|  
  
## See Also  
 [ADO API Reference](../../../ado/reference/ado-api/ado-api-reference.md)   
 [ADO Collections](../../../ado/reference/ado-api/ado-collections.md)   
 [ADO Dynamic Properties](../../../ado/reference/ado-api/ado-dynamic-properties.md)   
 [ADO Enumerated Constants](../../../ado/reference/ado-api/ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../../ado/guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](../../../ado/reference/ado-api/ado-events.md)   
 [ADO Methods](../../../ado/reference/ado-api/ado-methods.md)   
 [ADO Object Model](../../../ado/reference/ado-api/ado-object-model.md)   
 [ADO Properties](../../../ado/reference/ado-api/ado-properties.md)
