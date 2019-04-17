---
title: "Inserting Data into Table-Valued Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters, inserting data into"
ms.assetid: 9c1a3234-4675-40d3-b473-8df06208f880
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Inserting Data into Table-Valued Parameters
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider supports two models for the consumer to specify data for table valued parameter rows: a push model and a pull model. A sample that demonstrates the pull model is available; see [SQL Server Data Programming Samples](https://msftdpprodsamples.codeplex.com/).  
  
> [!NOTE]  
>  A table-valued parameter column must have either non-default values in all rows or default values in all rows. It is not possible to have default values in some rows but not others. Therefore, in table-valued parameter bindings, the only status values allowed for table-valued parameter rowset column data are DBSTATUS_S_ISNULL and DBSTATUS_S_OK. DBSTATUS_S_DEFAULT will result in a failure and the bound status value will be set to DBSTATUS_E_BADSTATUS.  
  
## Push Model (Loads All Table-Valued Paremeter Data in Memory)  
 The push model is similar to the use of parameter sets (that is, the DBPARAMS parameter in ICommand::Execute). The push model is only used if table-valued parameter rowset objects are used without a customized implementation of IRowset interfaces. The push model is recommended when the number of rows in the table-valued parameter rowset is small and is not expected to put excessive memory pressure on the application. This is simpler than the pull model, because it does not require any more functionality from the consumer application than what is currently common in typical OLE DB applications.  
  
 The consumer is expected to provide all the table-valued parameter data to the provider before executing a command. To provide the data, the consumer populates a table-valued parameter rowset object for each table-valued parameter. The table-valued parameter rowset object exposes rowset Insert, Set, and Delete operations, which the consumer will use to manipulate the table-valued parameter data. The provider will fetch the data from this table-valued parameter rowset object at execution time.  
  
 When a table-valued parameter rowset object is provided to the consumer, the consumer can process it as a rowset object. The consumer can obtain the type information of each column (type, maximum length, precision, and scale) by using the IColumnsInfo::GetColumnInfo or IColumnsRowset::GetColumnsRowset interface method. The consumer then creates an accessor to specify the bindings for the data. The next step is to insert rows of data into the table-valued parameter rowset. This can be done by using IRowsetChange::InsertRow. IRowsetChange::SetData or IRowsetChange::DeleteRows can also be used on the table-valued parameter rowset object if you have to manipulate the data. Table-valued parameter rowset objects are reference counted, similar to stream objects.  
  
 If IColumnsRowset::GetColumnsRowset is used, there will be subsequent calls to IRowset::GetNextRows, IRowset::GetData, and IRowset::ReleaseRows methods on the rowset object of the resulting column.  
  
 After the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider begins executing the command, the table-valued parameter values will be fetched from this table-valued parameter rowset object and sent to the server.  
  
 The push model requires minimal work by the consumer, but uses more memory than the pull model, because all the table-valued parameter data has to be in memory at execution time.  
  
## Pull Model (Obtaining Table-Valued Parameter Data on Demand from the Consumer)  
 The pull model is useful for two scenarios:  
  
-   To stream rows.  
  
-   If a rowset from another provider is being used as the table-valued parameter value.  
  
 In the pull model, the consumer provides data on demand to the provider. Use this approach if your application has many data insertions, and table-valued parameter rowset data in memory would result in excessive memory access. If multiple OLE DB providers are used, the consumer pull model enables the consumer to provide any rowset object as the table-valued parameter value.  
  
 To use the pull model, consumers have to provide their own implementation of a rowset object. When using the pull model with table-valued parameter rowsets (CLSID_ROWSET_TVP), the consumer is required to aggregate the table-valued parameter rowset object that the provider exposes through the ITableDefinitionWithConstraints::CreateTableWithConstraints method or the IOpenRowset::OpenRowset method. The consumer object is only expected to override the IRowset interface implementation. You must override the following functions:  
  
-   IRowset::GetNextRows  
  
-   IRowset::AddRefRows  
  
-   IRowset::GetData  
  
-   IRowset::ReleaseRows  
  
-   IRowset::RestartPosition  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider will read one or more rows at a time from the consumer rowset object to support streaming behavior in table-valued parameters. For example, the user might have the table-valued parameter rowset data on disk (not in memory) and might implement the functionality to read data from the disk when required by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider.  
  
 The consumer will communicate its data format to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider by using IAccessor::CreateAccessor on the table-valued parameter rowset object. When reading data from the consumer buffer, the provider verifies that all the writable and non-default columns are available through at least one accessor handle, and uses the corresponding handles to read columns data. To avoid ambiguity, there should be a one-to-one correspondence between a table-valued parameter rowset column and a binding. Duplicate bindings to the same column will result in an error. Also, each accessor is expected to have the *iOrdinal* member of DBBindings in sequence. There will be as many calls to IRowset::GetData as the number of accessors per row, and the order of calls will be based on the order of *iOrdinal* value from lower to higher values.  
  
 The provider is expected to implement most of the interfaces exposed by the table-valued parameter rowset object. The consumer will implement a rowset object with minimal interfaces (IRowset). Because of blind aggregation, the remaining mandatory rowset object interfaces will be implemented by the table-valued parameter rowset object.  
  
 For any other rowset object, such as rowset objects obtained for any OLE DB provider, the consumer-provided rowset must implement all the mandatory rowset object interfaces as specified in the OLE DB specification.  
  
 At the time of execution, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider will call back to the rowset object to fetch rows and read column data.  
  
## See Also  
 [Table-Valued Parameters &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-table-valued-parameters/table-valued-parameters-ole-db.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-how-to/use-table-valued-parameters-ole-db.md)  
  
  
