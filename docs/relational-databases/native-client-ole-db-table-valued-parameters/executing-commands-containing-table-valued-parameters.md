---
description: "Executing SQL Server Native Client Commands Containing Table-Valued Parameters"
title: "Commands with Table-Valued Parameters"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters, executing commands containing"
ms.assetid: 7ecba6f6-fe7a-462a-9aa3-d5115b6d4529
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing SQL Server Native Client Commands Containing Table-Valued Parameters
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Executing a command that contains table-valued parameters requires two phases:  
  
1.  Specify the parameter types.  
  
2.  Bind the parameter data.  

## Table-Valued Parameter Specification  
 The consumer can specify the type of the table-valued parameter. This information includes the table-valued parameter type name. It also includes the schema name, if the user-defined table type for the table-valued parameter is not in the current default schema for the connection. Depending on server support, the consumer can also specify optional metadata information, such as ordering of columns, and can specify that all rows for particular columns have the default values.  
  
 To specify a table-valued parameter, the consumer calls ISSCommandWithParameter::SetParameterInfo, and optionally calls ISSCommandWithParameters::SetParameterProperties. For a table-valued parameter, the *pwszDataSourceType* field in the DBPARAMBINDINFO structure has a value of DBTYPE_TABLE. The *ulParamSize* field is set to ~0 to indicate that length is unknown. Particular properties for table-valued parameters, such as schema name, type name, column order, and default columns, can be set through ISSCommandWithParameters::SetParameterProperties.  
  
## Table-Valued Parameter Binding  
 A table-valued parameter can be any rowset object. The provider reads from this object while sending table-valued parameters to the server during execution.  
  
 To bind the table-valued parameter, the consumer calls IAccessor::CreateAccessor. The *wType* field of the DBBINDING structure for the table-valued parameter is set to DBTYPE_TABLE. The *pObject* member of the DBBINDING structure is non-NULL, and the *pObject*'s *iid* member is set to IID_IRowset or any other table-valued parameter rowset object interfaces. The remaining fields in the DBBINDING structure should be set the same way they are set for streamed BLOBs.  
  
 In the bindings for the table-valued parameter and the rowset object associated with a table-valued parameter, the following restrictions apply:  
  
-   The only status values allowed for table-valued parameter rowset column data are DBSTATUS_S_ISNULL and DBSTATUS_S_OK. DBSTATUS_S_DEFAULT will result in a failure, and the bound status value will be set to DBSTATUS_E_BADSTATUS.  
  
-   A table-valued parameter can be marked with the status DBSTATUS_S_DEFAULT. The only valid values are DBSTATUS_S_DEFAULT and DBSTATUS_S_OK. When the status is set to DBSTATUS_S_DEFAULT, the value of the table-valued parameter corresponds to an empty table.  
  
-   Read-only columns in table-valued parameters (identity or computed columns) must be marked as default by using the SSPROP_PARAM_TABLE_DEFAULT_COLUMNS property. Columns that have a default value must also be marked as default through SSPROP_PARAM_TABLE_DEFAULT_COLUMNS property to allow the default value to be used for the column's data values for a particular table-valued parameter. The provider will ignore the data values bound for the columns marked as default.  
  
-   Data will be sent to the server for columns with DBPROP_COL_AUTOINCREMENT or SSPROP_COL_COMPUTED, unless SSPROP_PARAM_TABLE_DEFAULT is also set.  
  
## See Also  
 [Table-Valued Parameters &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-table-valued-parameters/table-valued-parameters-ole-db.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-how-to/use-table-valued-parameters-ole-db.md)  
  
  
