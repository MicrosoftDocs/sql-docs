---
description: "Fetching rows (Native Client OLE DB provider)"
title: "Fetching rows (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "fetching rows"
  - "OLE DB rowsets, fetching"
  - "rowsets [OLE DB], fetching"
  - "IRowset interface"
  - "SQL Server Native Client OLE DB provider, fetching"
ms.assetid: 5e6dbe36-b682-464d-adfa-8e886f9bd452
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Fetching Rows (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The **IRowset** interface is the base rowset interface. The **IRowset** interface provides methods for fetching rows sequentially, getting the data from those rows, and managing rows. Consumers use the methods in **IRowset** for all basic rowset operations. This includes fetching and releasing rows and getting column values.  
  
 When a consumer obtains an interface pointer on a rowset, the first step is ordinarily to determine the capabilities of the rowset by using the **IRowsetInfo::GetProperties** method. This returns information about the interfaces exposed by the rowset and also capabilities of the rowset that do not show up as distinct interfaces, such as the maximum number of active rows and the number of rows that can have pending updates at the same time.  
  
 The next step for consumers is to determine the characteristics, or metadata, of the columns in the rowset. For this they use the **IColumnsInfo** method for simple column information or the **IColumnsRowset** method for extended column information. The **GetColumnInfo** method returns the following information:  
  
-   The number of columns in the result set.  
  
-   An array of DBCOLUMNINFO structures, one per column.  
  
     The order of the structures is the order in which the columns appear in the rowset. Each DBCOLUMNINFO structure includes column metadata, such as column name, ordinal of the column, maximum possible length of a value in the column, data type of the column, precision, and length.  
  
-   The pointer to a storage for all string values within a single allocation block.  
  
 The consumer determines which columns it needs either from the metadata or based on the text command that generated the rowset. It determines the ordinals of the needed columns from the ordering of the column information returned by **IColumnsInfo** or from the ordinals in the column metadata rowset returned by **IColumnsRowset**.  
  
 The **IColumnsInfo** and **IColumnsRowset** interfaces are used to extract information about the columns in the rowset. The **IColumnsInfo** interface returns a limited set of information, whereas **IColumnsRowset** provides all the metadata.  
  
> [!NOTE]  
>  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 and earlier, the optional metadata column DBCOLUMN_COMPUTEMODE returned by **IColumnsInfo::GetColumnsInfo** returns DBSTATUS_S_ISNULL (instead of the values describing whether the column is computed) because it cannot be determined whether the underlying column is computed.  
  
 The ordinals are used to specify a binding to a column. A binding is a structure that associates an element of the consumer's structure with a column. The binding can bind the data value, length, and status value of the column.  
  
 A set of bindings is gathered together in an accessor. This is created by using the **IAccessor::CreateAccessor** method. An accessor can contain multiple bindings so that the data for multiple columns can be retrieved or set in a single call. The consumer can create several accessors to match different usage patterns in different parts of the application. It can create and release accessors while the rowset remains in existence.  
  
 To fetch rows from the database, the consumer calls a method, such as **IRowset::GetNextRows** or **IRowsetLocate::GetRowsAt**. These fetch operations put row data from the server into the row buffer of the provider. The consumer does not have direct access to the row buffer of the provider. The consumer uses **IRowset::GetData** to copy data from the buffer of the provider to the consumer buffer and **IRowsetChange::SetData** to copy data changes from the consumer buffer to the provider buffer.  
  
 The consumer calls the **GetData** method and passes the handle to a row, the handle to an accessor, and a pointer to a consumer-allocated buffer. **GetData** converts the data and returns the columns as specified in the bindings used to create the accessor. The consumer can call **GetData** more than one time for a row, using different accessors and buffers and therefore the consumer can obtain multiple copies of the same data.  
  
 Data from variable-length columns can be treated several ways. First, such columns can be bound to a finite section of the consumer's structure. This causes truncation when the length of the data exceeds the length of the buffer. The consumer can determine that truncation has occurred by checking for the status DBSTATUS_S_TRUNCATED. The returned length is always the true length in bytes, so that the consumer also can determine how much data was truncated.  
  
 When the consumer has finished fetching or updating rows, it releases them with the **ReleaseRows** method. This releases resources from the copy of the rows in the rowset and makes room for new rows. The consumer can then repeat its cycle of fetching or creating rows and accessing the data in them.  
  
 When the consumer is finished with the rowset, it calls the **IAccessor::ReleaseAccessor** method to release any accessor. It calls the **IUnknown::Release** method on all interfaces exposed by the rowset to release the rowset. When the rowset is released, it forces the release of any remaining rows or accessors the consumer may hold.  
  
## In This Section  
  
-   [Next Fetch Position](../../relational-databases/native-client-ole-db-rowsets/fetching-rows-next-fetch-position.md)  
  
## See Also  
 [Rowsets](../../relational-databases/native-client-ole-db-rowsets/rowsets.md)  
  
  
