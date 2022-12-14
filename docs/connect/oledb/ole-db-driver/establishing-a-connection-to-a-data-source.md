---
title: Establish connection to data source (OLE DB driver)
description: Learn how a consumer establishes a connection to a data source by using OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "data sources [OLE DB Driver for SQL Server]"
  - "connections [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server, data source connections"
  - "CoCreateInstance method"
  - "OLE DB data sources [OLE DB Driver for SQL Server]"
---
# Establishing a Connection to a Data Source

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

To access the OLE DB Driver for SQL Server, the consumer must first create an instance of a data source object by calling the `CoCreateInstance` method. A unique class identifier (CLSID) identifies each OLE DB provider. For the OLE DB Driver for SQL Server, the class identifier is CLSID_MSOLEDBSQL. You can also use the symbol, MSOLEDBSQL_CLSID, that resolves to the OLE DB Driver for SQL Server used in the referenced `msoledbsql.h` file.

The data source object exposes the `IDBProperties` interface, which the consumer uses to provide basic authentication information such as server name, database name, user ID, and password. The `IDBProperties::SetProperties` method is called to set these properties.

If there are multiple instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] running on the computer, the server name is specified as ServerName\InstanceName.

The data source object also exposes the `IDBInitialize` interface. After the properties are set, connection to the data source is established by calling the `IDBInitialize::Initialize` method. For example:

```cpp
CoCreateInstance(CLSID_MSOLEDBSQL,
                 NULL,
                 CLSCTX_INPROC_SERVER,
                 IID_IDBInitialize,
                 (void **) &pIDBInitialize)
```

 This call to `CoCreateInstance` creates a single object of the class associated with CLSID_MSOLEDBSQL (CSLID associated with the data and code that will be used to create the object). IID_IDBInitialize is a reference to the identifier of the interface (`IDBInitialize`) to be used to communicate with the object.

 The following sample shows how to initialize and establish a connection to the data source.

```cpp
#include "msoledbsql.h"
#include <stdio.h>

HRESULT InitializeAndEstablishConnection(IDBInitialize *&pIDBInitialize);

void main() {
    IDBInitialize       *pIDBInitialize = nullptr;
    HRESULT             hr = S_OK;

    // Initialize The Component Object Module Library
    CoInitialize(nullptr);

    hr = InitializeAndEstablishConnection(pIDBInitialize);
    if (FAILED(hr)) {
        printf("Failed to establish connection.\r\n");
        goto _ExitMain;
    }

    // Insert code that uses the established connection

_ExitMain:
    // Free Up All Allocated Memory
    if (pIDBInitialize)
    {
        pIDBInitialize->Uninitialize();
        pIDBInitialize->Release();
        pIDBInitialize = nullptr;
    }

    // Release The Component Object Module Library
    CoUninitialize();
}

HRESULT InitializeAndEstablishConnection(IDBInitialize *&pIDBInitialize) {
    IDBProperties   *pIDBProperties = nullptr;
    DBPROP          InitProperties[3] = { 0 };
    DBPROPSET       rgInitPropSet[1] = { 0 };
    HRESULT         hr = S_OK;

    // Obtain access to the OLE DB Driver for SQL Server.
    hr = CoCreateInstance(CLSID_MSOLEDBSQL,
                          NULL,
                          CLSCTX_INPROC_SERVER,
                          IID_IDBInitialize,
                          (void **)&pIDBInitialize);
    if (FAILED(hr)) {
        printf("Failed to obtain access to the OLE DB Driver.\r\n");
        goto _ExitInitialize;
    }
    // Initialize property values needed to establish connection.
    for (int i = 0; i < 3; i++) {
        VariantInit(&InitProperties[i].vValue);
    }

    // Server name.
    // See DBPROP structure for more information on InitProperties
    InitProperties[0].dwPropertyID = DBPROP_INIT_DATASOURCE;
    InitProperties[0].vValue.vt = VT_BSTR;
    InitProperties[0].vValue.bstrVal = SysAllocString(L"Server");
    InitProperties[0].dwOptions = DBPROPOPTIONS_REQUIRED;
    InitProperties[0].colid = DB_NULLID;

    // Database.
    InitProperties[1].dwPropertyID = DBPROP_INIT_CATALOG;
    InitProperties[1].vValue.vt = VT_BSTR;
    InitProperties[1].vValue.bstrVal = SysAllocString(L"database");
    InitProperties[1].dwOptions = DBPROPOPTIONS_REQUIRED;
    InitProperties[1].colid = DB_NULLID;

    // Username (login).
    InitProperties[2].dwPropertyID = DBPROP_AUTH_INTEGRATED;
    InitProperties[2].vValue.vt = VT_BSTR;
    InitProperties[2].vValue.bstrVal = SysAllocString(L"SSPI");
    InitProperties[2].dwOptions = DBPROPOPTIONS_REQUIRED;
    InitProperties[2].colid = DB_NULLID;

    // Construct the DBPROPSET structure(rgInitPropSet). The
    // DBPROPSET structure is used to pass an array of DBPROP
    // structures (InitProperties) to the SetProperties method.
    rgInitPropSet[0].guidPropertySet = DBPROPSET_DBINIT;
    rgInitPropSet[0].cProperties = 3;
    rgInitPropSet[0].rgProperties = InitProperties;

    // Set initialization properties.
    hr = pIDBInitialize->QueryInterface(IID_IDBProperties,
                                        (void **)&pIDBProperties);
    if (FAILED(hr)) {
        printf("Failed to obtain an IDBProperties interface.\r\n");
        goto _ExitInitialize;
    }
    hr = pIDBProperties->SetProperties(1, rgInitPropSet);
    if (FAILED(hr)) {
        printf("Failed to set initialization properties.\r\n");
        goto _ExitInitialize;
    }

    // Now establish the connection to the data source.
    hr = pIDBInitialize->Initialize();
    if (FAILED(hr)) {
        printf("Failed to establish connection with the server.\r\n");
        goto _ExitInitialize;
    }

_ExitInitialize:
    if (pIDBProperties)
    {
        pIDBProperties->Release();
        pIDBProperties = nullptr;
    }

    if (FAILED(hr))
    {
        if (pIDBInitialize)
        {
            pIDBInitialize->Release();
            pIDBInitialize = nullptr;
        }
    }

    return hr;
}
```

## See Also

[Creating an OLE DB Driver for SQL Server Application](creating-a-oledb-driver-for-sql-server-application.md)
