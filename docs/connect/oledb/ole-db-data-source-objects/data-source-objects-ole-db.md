---
title: "Data Source Objects (OLE DB) | Microsoft Docs"
description: "Data Source Objects (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "data access [OLE DB Driver for SQL Server], data source objects"
  - "OLE DB Driver for SQL Server, data source objects"
  - "MSOLEDBSQL, data source objects"
  - "OLE DB Driver for SQL Server, data source objects"
  - "OLE DB data source objects [OLE DB Driver for SQL Server]"
  - "data source objects [OLE DB]"
  - "CLSID"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Data Source Objects (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  OLE DB Driver for SQL Server uses the term data source for the set of OLE DB interfaces used to establish a link to a data store, such as [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Creating an instance of the data source object of the provider is the first task of a OLE DB Driver for SQL Server consumer.  
  
 Every OLE DB provider declares a class identifier (CLSID) for itself. The CLSID for the OLE DB Driver for SQL Server is the C/C++ GUID CLSID_MSOLEDBSQL (the symbol MSOLEDBSQL_CLSID will resolve to the correct progid in the msoledbsql.h file that you reference). With the CLSID, the consumer uses the OLE **CoCreateInstance** function to manufacture an instance of the data source object.  
  
 OLE DB Driver for SQL Server is an in-process server. Instances of OLE DB Driver for SQL Server objects are created using the CLSCTX_INPROC_SERVER macro to indicate the executable context.  
  
 The OLE DB Driver for SQL Server data source object exposes the OLE DB initialization interfaces that allow the consumer to connect to existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases.  
  
 Every connection made through the OLE DB Driver for SQL Server sets these options automatically:  
  
-   SET ANSI_WARNINGS ON  
  
-   SET ANSI_NULLS ON  
  
-   SET ANSI_PADDING ON  
  
-   SET ANSI_NULL_DFLT_ON ON  
  
-   SET QUOTED_IDENTIFIER ON  
  
-   SET CONCAT_OF_NULL_YIELDS_NULL ON  
  
 This example uses the class identifier macro to create an OLE DB Driver for SQL Server data source object and get a reference to its **IDBInitialize** interface.  
  
```  
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
  
 The OLE DB Driver for SQL Server makes its first connection to a specified instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] as part of a successful data source initialization. The connection is maintained as long as a reference is maintained on any data source initialization interface, or until the **IDBInitialize::Uninitialize** method is called.  
  
## In This Section  
  
-   [Data Source Properties &#40;OLE DB&#41;](../../oledb/ole-db-data-source-objects/data-source-properties-ole-db.md)  
  
-   [Data Source Information Properties](../../oledb/ole-db-data-source-objects/data-source-information-properties.md)  
  
-   [Initialization and Authorization Properties](../../oledb/ole-db-data-source-objects/initialization-and-authorization-properties.md)  
  
-   [Sessions](../../oledb/ole-db-data-source-objects/sessions.md)  
  
-   [Session Properties - OLE DB Driver for SQL Server](../../oledb/ole-db-data-source-objects/session-properties-oledb-driver-for-sql-server.md)  
  
-   [Persisted Data Source Objects](../../oledb/ole-db-data-source-objects/persisted-data-source-objects.md)  
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/ole-db/oledb-driver-for-sql-server-programming.md)  
  
  
