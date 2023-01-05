---
title: "About OLE DB connection properties"
description: In OLE DB Driver for SQL Server, consumers set property values to request specific object behavior. Learn about setting properties.
author: David-Engel
ms.author: v-davidengel
ms.date: "05/20/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB, properties"
  - "OLE DB Driver for SQL Server, properties"
  - "properties [OLE DB]"
  - "property values [OLE DB Driver for SQL Server]"
---
# About OLE DB Properties
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

<!--
NOTE , GeneMi , 2020/May/20:
    This SQL 2016+ article is a nearly exact duplicate of another SQL 2016+ article.
    This article resides under docs/connect/oledb/ole-db-driver/.

    The other article resides under docs/relational-databases/native-client-ole-db-provider/.
    And, the other article has a SQL 2014 counterpart having its same GitHub directory path and filename, to support SQL 2014 OPS Versioning with SQL 2016+.

    This path-file name is:
'\sql-docs-pr\docs\connect\oledb\ole-db-driver\about-ole-db-properties.md'.

    The other path-file is named:
'\sql-docs-pr\docs\relational-databases\native-client-ole-db-provider\about-ole-db-properties.md'.

    Therefore, maybe this docs/connect/oledb/... file should be deleted?

1611957:  This NOTE relates to SEO content bug 1611957 about metadata 'title:' value duplication:
    https://mseng.visualstudio.com/TechnicalContent/_workitems/edit/1611957

PR 15068:  This HTML comment is being added by PR...
    https://github.com/MicrosoftDocs/sql-docs-pr/pull/15068
-->

  Consumers set property values to request specific object behavior. For example, consumers use properties to specify the interfaces to be exposed by a rowset. Consumers get the property values to determine the capabilities of an object, such as a rowset, a session, or a data source object.  
  
 Each property has a value, type, description, and read/write attribute, and for rowset properties, an indicator of whether it can be applied on a column-by-column basis.  
  
 A property is identified by a GUID and an integer representing the property ID. A property set is a set of all properties that share the same GUID. In addition to the predefined OLE DB property sets, the OLE DB Driver for SQL Server implements provider-specific property sets and properties in them. Each property belongs to one or more property groups. A property group is the group of all properties that apply to a particular object. Some property groups include the initialization property group, data source property group, session property group, rowset property group, table property group, and column property group. There are properties in each of these property groups.  
  
 Setting property values involves:  
  
1.  Determining the properties for which to set values.  
  
2.  Determining the property sets that contain the identified properties.  
  
3.  Allocating an array of DBPROPSET structures, one for each identified property set.  
  
4.  Allocating an array of DBPROP structures for each property set. The number of elements in each array is the number of properties (identified in Step 1) that belong to that property set.  
  
5.  Filling in the DBPROP structure for each property.  
  
6.  Filling in information (property set GUID, count of number of elements, and a pointer to the corresponding DBPROP array) in the DBPROPSET structure for each property set.  
  
7.  Calling a method to set properties and passing the count and the array of DBPROPSET structures.  
  
## See Also  
 [Creating an OLE DB Driver for SQL Server Application](../../oledb/ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md)   
 [Properties (OLE DB)](/previous-versions/windows/desktop/ms722734(v=vs.85))  
  
