---
title: "Establishing a Connection to a Data Source | Microsoft Docs"
description: "Establishing a connection to a data source using OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.component: "oledb-driver-for-sql-server"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: connectivity
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "data sources [OLE DB Driver for SQL Server]"
  - "connections [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server, data source connections"
  - "CoCreateInstance method"
  - "OLE DB data sources [OLE DB Driver for SQL Server]"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: craigg
---
# Establishing a Connection to a Data Source
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  To access the OLE DB Driver for SQL Server, the consumer must first create an instance of a data source object by calling the **CoCreateInstance** method. A unique class identifier (CLSID) identifies each OLE DB provider. For the OLE DB Driver for SQL Server, the class identifier is CLSID_MSOLEDBSQL. You can also use the symbol MSOLEDBSQL_CLSID that will resolve to the OLE DB Driver for SQL Server that is used in the msoledbsql.h that you reference.  
  
 The data source object exposes the **IDBProperties** interface, which the consumer uses to provide basic authentication information such as server name, database name, user ID, and password. The **IDBProperties::SetProperties** method is called to set these properties.  
  
 If there are multiple instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] running on the computer, the server name is specified as ServerName\InstanceName.  
  
 The data source object also exposes the **IDBInitialize** interface. After the properties are set, connection to the data source is established by calling the **IDBInitialize::Initialize** method. For example:  
  
```  
CoCreateInstance(CLSID_MSOLEDBSQL,   
                 NULL,   
                 CLSCTX_INPROC_SERVER,  
                 IID_IDBInitialize,   
                 (void **) &pIDBInitialize)  
```  
  
 This call to **CoCreateInstance** creates a single object of the class associated with CLSID_MSOLEDBSQL (CSLID associated with the data and code that will be used to create the object). IID_IDBInitialize is a reference to the identifier of the interface (**IDBInitialize**) to be used to communicate with the object.  
  
 The following is a sample function that initializes and establishes a connection to the data source.  
  
```  
#include "msoledbsql.h"

void InitializeAndEstablishConnection() {
    IDBInitialize   *pIDBInitialize = NULL;
    IDBProperties   *pIDBProperties = NULL;
    DBPROP          InitProperties[4];
    DBPROPSET       rgInitPropSet[1];
    HRESULT         hr;
    int             i;

   // Initialize the COM library.  
   CoInitialize(NULL);  

   // Obtain access to the OLE DB Driver for SQL Server.  
   hr = CoCreateInstance(CLSID_MSOLEDBSQL,   
                         NULL,   
                         CLSCTX_INPROC_SERVER,  
                         IID_IDBInitialize,   
                         (void **) &pIDBInitialize);  
   // Initialize property values needed to establish connection.  
   for (i = 0 ; i < 4 ; i++)   
      VariantInit(&InitProperties[i].vValue);  

   // Server name.  
   // See DBPROP structure for more information on InitProperties  
   InitProperties[0].dwPropertyID  = DBPROP_INIT_DATASOURCE;  
   InitProperties[0].vValue.vt    = VT_BSTR;  
   InitProperties[0].vValue.bstrVal=   
                     SysAllocString(L"Server");  
   InitProperties[0].dwOptions    = DBPROPOPTIONS_REQUIRED;  
   InitProperties[0].colid       = DB_NULLID;  

   // Database.  
   InitProperties[1].dwPropertyID  = DBPROP_INIT_CATALOG;  
   InitProperties[1].vValue.vt    = VT_BSTR;  
   InitProperties[1].vValue.bstrVal= SysAllocString(L"database");  
   InitProperties[1].dwOptions    = DBPROPOPTIONS_REQUIRED;  
   InitProperties[1].colid       = DB_NULLID;  

   // Username (login).  
   InitProperties[2].dwPropertyID  = DBPROP_AUTH_INTEGRATED;  
   InitProperties[2].vValue.vt    = VT_BSTR;  
   InitProperties[2].vValue.bstrVal= SysAllocString(L"SSPI");  
   InitProperties[2].dwOptions    = DBPROPOPTIONS_REQUIRED;  
   InitProperties[2].colid       = DB_NULLID;  
   InitProperties[3].dwOptions    = DBPROPOPTIONS_REQUIRED;  
   InitProperties[3].colid       = DB_NULLID;  

   // Construct the DBPROPSET structure(rgInitPropSet). The   
   // DBPROPSET structure is used to pass an array of DBPROP   
   // structures (InitProperties) to the SetProperties method.  
   rgInitPropSet[0].guidPropertySet = DBPROPSET_DBINIT;  
   rgInitPropSet[0].cProperties   = 4;  
   rgInitPropSet[0].rgProperties   = InitProperties;  

   // Set initialization properties.  
   hr = pIDBInitialize->QueryInterface(IID_IDBProperties,   
                           (void **)&pIDBProperties);  
   hr = pIDBProperties->SetProperties(1, rgInitPropSet);   
   pIDBProperties->Release();  

   // Now establish the connection to the data source.  
   pIDBInitialize->Initialize();  
```  
  
## See Also  
 [Creating an OLE DB Driver for SQL Server Application](../../oledb/ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md)  
  
  
