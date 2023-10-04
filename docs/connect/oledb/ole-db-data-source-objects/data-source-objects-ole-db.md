---
title: Data source objects (OLE DB driver)
description: Learn how an OLE DB Driver for SQL Server consumer creates an instance of a data source object for a provider.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "data access [OLE DB Driver for SQL Server], data source objects"
  - "OLE DB Driver for SQL Server, data source objects"
  - "MSOLEDBSQL, data source objects"
  - "OLE DB data source objects [OLE DB Driver for SQL Server]"
  - "data source objects [OLE DB]"
  - "CLSID"
---
# Data Source Objects (OLE DB)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

OLE DB Driver for SQL Server uses the term data source for the set of OLE DB interfaces used to establish a link to a data store, such as [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Creating an instance of the data source object of the provider is the first task of an OLE DB Driver for SQL Server consumer.

Every OLE DB provider declares a class identifier (CLSID) for itself. The CLSID for the OLE DB Driver for SQL Server is the C/C++ GUID CLSID_MSOLEDBSQL (the symbol MSOLEDBSQL_CLSID will resolve to the correct progid in the msoledbsql.h file that you reference). With the CLSID, the consumer uses the OLE `CoCreateInstance` function to manufacture an instance of the data source object.

OLE DB Driver for SQL Server is an in-process server. Instances of OLE DB Driver for SQL Server objects are created using the CLSCTX_INPROC_SERVER macro to indicate the executable context.

The OLE DB Driver for SQL Server data source object exposes the OLE DB initialization interfaces that allow the consumer to connect to existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases.

Every connection made through the OLE DB Driver for SQL Server sets these options automatically:

- SET ANSI_WARNINGS ON
- SET ANSI_NULLS ON
- SET ANSI_PADDING ON
- SET ANSI_NULL_DFLT_ON ON
- SET QUOTED_IDENTIFIER ON
- SET CONCAT_OF_NULL_YIELDS_NULL ON

This example uses the class identifier macro to create an OLE DB Driver for SQL Server data source object and get a reference to its `IDBInitialize` interface.

```cpp
IDBInitialize*   pIDBInitialize;
HRESULT          hr;

hr = CoCreateInstance(CLSID_MSOLEDBSQL, NULL, CLSCTX_INPROC_SERVER,
    IID_IDBInitialize, (void**) &pIDBInitialize);

if (SUCCEEDED(hr))
{
    //  Perform necessary processing with the interface.
    pIDBInitialize->Uninitialize();
    pIDBInitialize->Release();
}
else
{
    // Display error from CoCreateInstance.
}
```

With successful creation of an instance of an OLE DB Driver for SQL Server data source object, the consumer application can continue by initializing the data source and creating sessions. OLE DB sessions present the interfaces that allow data access and manipulation.

The OLE DB Driver for SQL Server makes its first connection to a specified instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] as part of a successful data source initialization. The connection is maintained as long as a reference is maintained on any data source initialization interface, or until the `IDBInitialize::Uninitialize` method is called.

## In This Section

- [Data Source Properties &#40;OLE DB&#41;](data-source-properties-ole-db.md)
- [Data Source Information Properties](data-source-information-properties.md)
- [Initialization and Authorization Properties](initialization-and-authorization-properties.md)
- [Sessions](sessions.md)
- [Session Properties - OLE DB Driver for SQL Server](session-properties-oledb-driver-for-sql-server.md)
- [Persisted Data Source Objects](persisted-data-source-objects.md)

## See Also

[OLE DB Driver for SQL Server Programming](../ole-db/oledb-driver-for-sql-server-programming.md)
