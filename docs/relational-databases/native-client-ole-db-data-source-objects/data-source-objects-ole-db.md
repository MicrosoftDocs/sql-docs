---
description: "SQL Server Native Client Data Source Objects (OLE DB)"
title: "Data source objects (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "data access [SQL Server Native Client], data source objects"
  - "SQL Server Native Client, data source objects"
  - "SQLNCLI, data source objects"
  - "SQL Server Native Client OLE DB provider, data source objects"
  - "OLE DB data source objects [SQL Server Native Client]"
  - "data source objects [OLE DB]"
  - "CLSID"
ms.assetid: c1d4ed20-ad3b-4e33-a26b-38d7517237b7
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
#  SQL Server Native Client Data Source Objects (OLE DB)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client uses the term data source for the set of OLE DB interfaces used to establish a link to a data store, such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Creating an instance of the data source object of the provider is the first task of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client consumer.  
  
 Every OLE DB provider declares a class identifier (CLSID) for itself. The CLSID for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider is the C/C++ GUID CLSID_SQLNCLI10 (the symbol SQLNCLI_CLSID will resolve to the correct progid in the sqlncli.h file that you reference). With the CLSID, the consumer uses the OLE **CoCreateInstance** function to manufacture an instance of the data source object.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is an in-process server. Instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider objects are created using the CLSCTX_INPROC_SERVER macro to indicate the executable context.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider data source object exposes the OLE DB initialization interfaces that allow the consumer to connect to existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
 Every connection made through the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider sets these options automatically:  
  
-   SET ANSI_WARNINGS ON  
  
-   SET ANSI_NULLS ON  
  
-   SET ANSI_PADDING ON  
  
-   SET ANSI_NULL_DFLT_ON ON  
  
-   SET QUOTED_IDENTIFIER ON  
  
-   SET CONCAT_OF_NULL_YIELDS_NULL ON  
  
 This example uses the class identifier macro to create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider data source object and get a reference to its **IDBInitialize** interface.  
  
```  
IDBInitialize*   pIDBInitialize;  
HRESULT          hr;  
  
hr = CoCreateInstance(CLSID_SQLNCLI10, NULL, CLSCTX_INPROC_SERVER,  
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
  
 With successful creation of an instance of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider data source object, the consumer application can continue by initializing the data source and creating sessions. OLE DB sessions present the interfaces that allow data access and manipulation.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider makes its first connection to a specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as part of a successful data source initialization. The connection is maintained as long as a reference is maintained on any data source initialization interface, or until the **IDBInitialize::Uninitialize** method is called.  
  
## In This Section  
  
-   [Data Source Properties &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-data-source-objects/data-source-properties-ole-db.md)  
  
-   [Data Source Information Properties](../../relational-databases/native-client-ole-db-data-source-objects/data-source-information-properties.md)  
  
-   [Initialization and Authorization Properties](../../relational-databases/native-client-ole-db-data-source-objects/initialization-and-authorization-properties.md)  
  
-   [Sessions](../../relational-databases/native-client-ole-db-data-source-objects/sessions.md)  
  
-   [Session Properties - SQL Server Native Client OLE DB provider](../../relational-databases/native-client-ole-db-data-source-objects/session-properties-sql-server-native-client-ole-db-provider.md)  
  
-   [Persisted Data Source Objects](../../relational-databases/native-client-ole-db-data-source-objects/persisted-data-source-objects.md)  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
  
  
