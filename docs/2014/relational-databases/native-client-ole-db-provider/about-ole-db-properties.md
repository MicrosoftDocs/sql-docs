---
title: "About OLE DB Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB, properties"
  - "SQL Server Native Client OLE DB provider, properties"
  - "properties [OLE DB]"
  - "property values [SQL Server Native Client]"
ms.assetid: 0b36a61e-b542-400d-a3d2-e6f643caf2c6
author: MightyPen
ms.author: genemi
manager: craigg
---
# About OLE DB Properties
  Consumers set property values to request specific object behavior. For example, consumers use properties to specify the interfaces to be exposed by a rowset. Consumers get the property values to determine the capabilities of an object, such as a rowset, a session, or a data source object.  
  
 Each property has a value, type, description, and read/write attribute, and for rowset properties, an indicator of whether it can be applied on a column-by-column basis.  
  
 A property is identified by a GUID and an integer representing the property ID. A property set is a set of all properties that share the same GUID. In addition to the predefined OLE DB property sets, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider implements provider-specific property sets and properties in them. Each property belongs to one or more property groups. A property group is the group of all properties that apply to a particular object. Some property groups include the initialization property group, data source property group, session property group, rowset property group, table property group, and column property group. There are properties in each of these property groups.  
  
 Setting property values involves:  
  
1.  Determining the properties for which to set values.  
  
2.  Determining the property sets that contain the identified properties.  
  
3.  Allocating an array of DBPROPSET structures, one for each identified property set.  
  
4.  Allocating an array of DBPROP structures for each property set. The number of elements in each array is the number of properties (identified in Step 1) that belong to that property set.  
  
5.  Filling in the DBPROP structure for each property.  
  
6.  Filling in information (property set GUID, count of number of elements, and a pointer to the corresponding DBPROP array) in the DBPROPSET structure for each property set.  
  
7.  Calling a method to set properties and passing the count and the array of DBPROPSET structures.  
  
## See Also  
 [Creating a SQL Server Native Client OLE DB Provider Application](creating-a-sql-server-native-client-ole-db-provider-application.md)   
 [Properties (OLE DB)](https://go.microsoft.com/fwlink/?LinkId=112207)  
  
  
