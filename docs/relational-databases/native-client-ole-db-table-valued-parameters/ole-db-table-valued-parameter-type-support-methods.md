---
title: "OLE DB Table-Valued Parameter Type Support (Methods) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (OLE DB), API support (methods)"
ms.assetid: e3c2a450-8fd4-44cb-93d8-affe1b65c68e
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# OLE DB Table-Valued Parameter Type Support (Methods)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The following standard OLE DB methods support table-valued parameters:  
  
|Method|Table-valued parameter support|  
|------------|-------------------------------------|  
|ITableDefinitionWithConstraints::CreateTableWithConstraints|Used when you know type information of table-valued parameter, and want to instantiate a table-valued parameter rowset object based on the type-information.<br /><br /> For more information, see "Static Scenario" in [Table-Valued Parameter Rowset Creation](../../relational-databases/native-client-ole-db-table-valued-parameters/table-valued-parameter-rowset-creation.md).|  
|IOpenRowset::OpenRowset|Used when you do not know the type information of a table-valued parameter, and want to instantiate a table-valued parameter rowset object based on metadata information retrieved from the server.<br /><br /> For more information, see "Dynamic Scenario" in [Table-Valued Parameter Rowset Creation](../../relational-databases/native-client-ole-db-table-valued-parameters/table-valued-parameter-rowset-creation.md).|  
|ISSCommandWithParameters::SetParameterInfo|To specify a table-valued parameter command parameter, the consumer specifies the type of the parameter as "table" or "DBTYPE_TABLE" in the *pwszName* member of DBPARAMBINDINFO structure. The *ulParamSize* is set to ~0. For more information, see "Table-Valued Parameter Specification" in [Executing Commands Containing Table-Valued Parameters](../../relational-databases/native-client-ole-db-table-valued-parameters/executing-commands-containing-table-valued-parameters.md).|  
|ISSCommandWithParameters::SetParameterProperties|Sets properties specific to table-valued parameters, such as schema name, type name, column order, and default columns.<br /><br /> The consumer specifies the ordinal of the parameter in the *iOrdinal* of the SSPARAMPROPS structure. The property set requested is DBPROPSET_SQLSERVERPARAMETER.|  
|ISSCommandWithParameters::GetParameterInfo|Gets the types of all the parameters to a specified command.<br /><br /> For table-valued parameters, the *wType* field in DBPARAMINFO structure will have type DBTYPE_TABLE. The *ulParamSize* field will be set to ~0 to indicate unknown length.|  
|ISSCommandWithParameters::GetParameterProperties|Gets additional type information for parameters of the DBTYPE_TABLE type.<br /><br /> The consumer specifies the ordinal of the parameter in the *iOrdinal* member of the SSPARAMPROPS structure. The consumer can request any of the properties in the DBPROPSET_SQLSERVERPARAMETER property set that are listed under ISSCommandWithParameters::SetParameterProperties.<br /><br /> Because the consumer does not know the table-valued parameter type, the provider must set the SSPROP_PARAM_TYPE_TYPENAME, SSPROP_PARAM_TYPE_SCHEMANAME, and SSPROP_PARAM_TYPE_CATALOGNAME to their correct values. The remaining properties, SSPROP_PARAM_TABLE_DEFAULT_COLUMNS and SSPROP_PARAM_TABLE_COLUMN_SORT_ORDER, will have their default values. After the consumer has discovered the table-valued parameter type name, it uses IOpenRowset::OpenRowset to create an instance of this table-valued parameter, specifying the name of table-valued parameter type. For more information, see [Table-Valued Parameter Type Discovery](../../relational-databases/native-client-ole-db-table-valued-parameters/table-valued-parameter-type-discovery.md).|  
|IRowsetInfo::GetProperties|Gets table-valued parameter rowset properties. The consumer can use these properties to optimally set up bindings.|  
|IColumnsRowset::GetColumnsRowset|Retrieves metadata information about a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. For table-valued parameters, this same interface provides detailed metadata information about each column, such as the following:<br /><br /> DBCOLUMN_FLAGS indicates nullability through the DBCOLUMNFLAGS_ISNULLABLE bit.<br /><br /> DBCOLUMN_ISUNIQUE indicates whether the column is an identity column.<br /><br /> DBCOLUMN_COMPUTEMODE indicates whether the column is computed.|  
|IAccessor::CreateAccessor|To bind a table-valued parameter rowset object to a command parameter, you create an accessor with its *wType* member set to DBTYPE_TABLE. The DBOBJECT structure will contain IID_IRowset or any other valid rowset object interface in the *iid* member. The rest of the fields are treated similarly to DBTYPE_IUNKNOWN.|  
  
## See Also  
 [OLE DB Table-Valued Parameter Type Support](../../relational-databases/native-client-ole-db-table-valued-parameters/ole-db-table-valued-parameter-type-support.md)   
 [Table-Valued Parameter Rowset Creation](../../relational-databases/native-client-ole-db-table-valued-parameters/table-valued-parameter-rowset-creation.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-how-to/use-table-valued-parameters-ole-db.md)  
  
  
