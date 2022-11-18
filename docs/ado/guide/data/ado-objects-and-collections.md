---
title: "ADO Objects and Collections"
description: "ADO Objects and Collections"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, objects and collections"
---
# ADO Objects and Collections
ADO consists of the following nine objects and four collections.  
  
|Object or Collection|Description|  
|--------------------------|-----------------|  
|**Connection** object|Represents a unique session with a data source. In the case of a client/server database system, it may be equivalent to an actual network connection to the server. Depending on the functionality supported by the provider, some collections, methods, or properties of a **Connection** object may not be available.|  
|**Command** object|Used to define a specific command, such as a SQL query, intended to run against a data source.|  
|**Recordset** object|Represents the entire set of records from a base table or the results of an executed command. All **Recordset** objects consist of records (rows) and fields (columns).|  
|**Record** object|Represents a single row of data, either from a **Recordset** or from the provider. This record could represent a database record or some other type of object such as a file or directory, depending upon your provider.|  
|**Stream** object|Represents a stream of binary or text data. For example, an XML document can be loaded into a stream for command input or returned from certain providers as the results of a query. A **Stream** object can be used to manipulate fields or records containing these streams of data.|  
|**Parameter** object|Represents a parameter or argument associated with a **Command** object, based on a parameterized query or stored procedure.|  
|**Field** object|Represents a column of data with a common data type. Each **Field** object corresponds to a column in the **Recordset**.|  
|**Property** object|Represents a characteristic of an ADO object that is defined by the provider. ADO objects have two types of properties: built-in and dynamic. Built-in properties are those properties implemented in ADO and immediately available to any new object. The **Property** object is a container for dynamic properties, defined by the underlying provider.|  
|**Error** object|Contains details about data access errors that pertain to a single operation involving the provider.|  
|**Fields** collection|Contains all the **Field** objects of a **Recordset** or **Record** object.|  
|**Properties** collection|Contains all the **Property** objects for a specific instance of an object.|  
|**Parameters** collection|Contains all the **Parameter** objects of a **Command** object.|  
|**Errors** collection|Contains all the **Error** objects created in response to a single provider-related failure.|  
  
## See Also  
 [ADO Object Model](../../reference/ado-api/ado-object-model.md)