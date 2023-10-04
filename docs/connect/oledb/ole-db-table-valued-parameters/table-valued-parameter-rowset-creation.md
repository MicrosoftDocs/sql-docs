---
title: Table-valued parameter rowset creation (OLE DB driver)
description: Learn about table-valued parameter rowset, an in-memory object that the OLE DB Driver for SQL Server enables consumers to create.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "table-valued parameters, rowset creation"
---
# Table-Valued Parameter Rowset Creation
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Although consumers can provide any rowset object for table-valued parameters, typical rowset objects are implemented against back-end data stores, and therefore provide limited performance. For this reason, the OLE DB Driver for SQL Server enables consumers to create a specialized rowset object on top of in-memory data. This special, in-memory rowset object is a new COM object called a table-valued parameter rowset. It provides functionality similar to parameter sets.  
  
 Table-valued parameter rowset objects are created explicitly by the consumer for input parameters through multiple session-level interfaces. There's one instance of a table-valued parameter rowset object per table-valued parameter. The consumer can create the table-valued parameter rowset objects either by providing metadata information that is already known (static scenario), or by discovering it through provider interfaces (dynamic scenario). The following sections describe these two scenarios.  
  
## Static Scenario  
 When the type information is known, the consumer uses ITableDefinitionWithConstraints::CreateTableWithConstraints to instantiate a table-valued parameter rowset object that corresponds to a table-valued parameter.  
  
 The *guid* field (*pTableID* parameter) contains the special GUID (CLSID_ROWSET_TVP). The *pwszName* member contains the name of the table-valued parameter type that the consumer wants to instantiate. The *eKind* field will be set to DBKIND_GUID_NAME. This name is required when the statement is ad hoc SQL; the name is optional if it's a procedure call.  
  
 For aggregation, the consumer passes the *pUnkOuter* parameter with the controlling IUnknown.  
  
 The table-valued parameter rowset object properties are read only, so the consumer isn't expected to set any properties in *rgPropertySets*.  
  
 For the *rgPropertySets* member of each DBCOLUMNDESC structure, the consumer can specify additional properties for each column. These properties belong to the DBPROPSET_SQLSERVERCOLUMN property set. They enable you to specify computed and default settings for each column. They also support existing column properties, such as nullability and identity.  
  
 To retrieve corresponding information from a table-valued parameter rowset object, the consumer uses IRowsetInfo::GetProperties.  
  
 To retrieve information about the null, unique, computed, and update status of each column, the consumer can use IColumnsRowset::GetColumnsRowset or IColumnsInfo::GetColumnInfo. These methods provide detailed information about each table-valued parameter rowset column.  
  
 The consumer specifies the type of each column of the table-valued parameter. It is similar to how columns are specified when a table is created in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The consumer obtains a table-valued parameter rowset object from the OLE DB Driver for SQL Server through the *ppRowset* output parameter.  
  
## Dynamic Scenario  
 When the consumer doesn't have type information, it should use IOpenRowset::OpenRowset to instantiate table-valued parameter rowset objects. All the consumer has to provide to the provider is the type name.  
  
 In this scenario, the provider obtains type information about a table-valued parameter rowset object from the server on behalf of the consumer.  
  
 The *pTableID* and *pUnkOuter* parameters should be set as in the static scenario. The OLE DB Driver for SQL Server then obtains the type information (column information and constraints) from the server, and return a table-valued parameter rowset object through the *ppRowset* parameter. This operation requires communication with the server, and therefore doesn't perform as well as the static scenario. The dynamic scenario works only with parameterized procedure calls.  
  
## See Also  
 [Table-Valued Parameters &#40;OLE DB&#41;](../../oledb/ole-db-table-valued-parameters/table-valued-parameters-ole-db.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](../../oledb/ole-db-how-to/use-table-valued-parameters-ole-db.md)  
  
  
