---
title: "Execute a User-Defined Function and Process Return Code (OLE DB)"
description: See how to run user-defined function and print a return code using OLE DB Driver for SQL Server. This example can use any existing database.
author: David-Engel
ms.author: v-davidengel
ms.date: "02/18/2022"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "user-defined functions [OLE DB]"
---
# Execute a User-Defined Function and Process Return Code (OLE DB)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../../includes/driver_oledb_download.md)]

  In this example, a user-defined function is executed, and the return code is printed. This sample is not supported on IA64.  
  
 This sample uses the `oledbtest` database as an example. Please substitute it with any SQL Server database you have.
  
> [!IMPORTANT]  
>  When possible, use Windows Authentication. If Windows Authentication is not available, prompt users to enter their credentials at run time. Avoid storing credentials in a file. If you must persist credentials, you should encrypt them with the [Win32 crypto API](/windows/win32/seccrypto/cryptography-reference).  
  
## Example  
 Execute the first ( [!INCLUDE[tsql](../../../../includes/tsql-md.md)]) code listing to create the stored procedure used by the application.  
  
 Compile with ole32.lib oleaut32.lib and execute the second (C++) code listing. This application connects to your computer's default [!INCLUDE[ssNoVersion](../../../../includes/ssnoversion-md.md)] instance. On some Windows operating systems, you will need to change (localhost) or (local) to the name of your [!INCLUDE[ssNoVersion](../../../../includes/ssnoversion-md.md)] instance. To connect to a named instance, change the connection string from L"(local)" to L"(local)\\\name" , where name is the named instance. By default, [!INCLUDE[ssNoVersion](../../../../includes/ssnoversion-md.md)] Express installs to a named instance. Make sure your INCLUDE environment variable includes the directory that contains msoledbsql.h.  
  
 Execute the third ( [!INCLUDE[tsql](../../../../includes/tsql-md.md)]) code listing to delete the stored procedure used by the application.  
  
```sql
if exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[fn_RectangleArea]'))  
   drop function fn_RectangleArea  
go  
  
CREATE FUNCTION fn_RectangleArea  
   (@Width int,   
@Height int )  
RETURNS int  
AS  
BEGIN  
   RETURN ( @Width * @Height )  
END  
GO  
```  
  
```cpp
// compile with: ole32.lib oleaut32.lib
#include <iostream>
#include <atlbase.h>    // CComPtr
#include "msoledbsql.h"

HRESULT InitializeAndEstablishConnection(CComPtr<IDBInitialize>& pIDBInitialize);
HRESULT ExecuteFunction(const CComPtr<IDBInitialize>& pIDBInitialize);

int main()
{
    HRESULT hr = S_OK;

    // Initialize the COM library.
    CoInitialize(nullptr);

    // All interfaces must be freed before CoUninitialize is called,
    // thus limiting the scope of pIDBInitialize
    {
        CComPtr<IDBInitialize> pIDBInitialize;

        // All the initialization stuff in a separate function.
        hr = InitializeAndEstablishConnection(pIDBInitialize);
        if (FAILED(hr))
        {
            std::cout << "Failed to connect\n";
            goto EXIT;
        }

        hr = ExecuteFunction(pIDBInitialize);
        if (FAILED(hr))
        {
            std::cout << "Failed in executing function\n";
            goto EXIT;
        }

        if (FAILED(pIDBInitialize->Uninitialize()))
        {
            // Uninitialize is not required, but it fails if an interface
            // has not been released.  This can be used for debugging.
            std::cout << "Problem uninitializing\n";
        }
    }
EXIT:
    CoUninitialize();
    return (FAILED(hr));
}

HRESULT InitializeAndEstablishConnection(CComPtr<IDBInitialize>& pIDBInitialize)
{
    HRESULT hr = S_OK;

    // Obtain access to the OLE DB Driver for SQL Server.
    hr = CoCreateInstance(CLSID_MSOLEDBSQL,
                          nullptr,
                          CLSCTX_INPROC_SERVER,
                          IID_IDBInitialize,
                          reinterpret_cast<LPVOID *>(&pIDBInitialize));
    if (FAILED(hr))
    {
        std::cout << "Failed in CoCreateInstance()\n";
        return hr;
    }

    const ULONG nInitProps1 = 3;
    const ULONG nInitProps2 = 1;
    const ULONG nPropSets = 2;
    CComBSTR server(L"(local)");
    CComBSTR database(L"oledbtest");
    CComBSTR auth(L"SSPI");
    CComBSTR encrypt(L"Mandatory");
    DBPROP InitProperties1[nInitProps1] = {};
    DBPROP InitProperties2[nInitProps2] = {};
    DBPROPSET rgInitPropSet[nPropSets] = {};

    // Initialize the property values needed to establish the connection.
    for (ULONG i = 0; i < nInitProps1; i++)
        VariantInit(&InitProperties1[i].vValue);

    // Specify server name.
    InitProperties1[0].dwPropertyID = DBPROP_INIT_DATASOURCE;
    InitProperties1[0].vValue.vt = VT_BSTR;

    // Replace "MySqlServer" with proper value.
    InitProperties1[0].vValue.bstrVal = server;
    InitProperties1[0].dwOptions = DBPROPOPTIONS_REQUIRED;
    InitProperties1[0].colid = DB_NULLID;

    // Specify database name.
    InitProperties1[1].dwPropertyID = DBPROP_INIT_CATALOG;
    InitProperties1[1].vValue.vt = VT_BSTR;
    InitProperties1[1].vValue.bstrVal = database;
    InitProperties1[1].dwOptions = DBPROPOPTIONS_REQUIRED;
    InitProperties1[1].colid = DB_NULLID;

    InitProperties1[2].dwPropertyID = DBPROP_AUTH_INTEGRATED;
    InitProperties1[2].vValue.vt = VT_BSTR;
    InitProperties1[2].vValue.bstrVal = auth;
    InitProperties1[2].dwOptions = DBPROPOPTIONS_REQUIRED;
    InitProperties1[2].colid = DB_NULLID;

    // Data should be encrypted before sending it over the network
    VariantInit(&InitProperties2[0].vValue);
    InitProperties2[0].dwPropertyID = SSPROP_INIT_ENCRYPT;
    InitProperties2[0].vValue.vt = VT_BSTR;
    InitProperties2[0].vValue.bstrVal = encrypt;
    InitProperties2[0].dwOptions = DBPROPOPTIONS_REQUIRED;
    InitProperties2[0].colid = DB_NULLID;

    // Now that properties are set, construct the DBPROPSET structure
    // (rgInitPropSet).  The DBPROPSET structure is used to pass an array
    // of DBPROP structures (InitProperties) to SetProperties method.
    rgInitPropSet[0].guidPropertySet = DBPROPSET_DBINIT;
    rgInitPropSet[0].cProperties = nInitProps1;
    rgInitPropSet[0].rgProperties = InitProperties1;

    rgInitPropSet[1].guidPropertySet = DBPROPSET_SQLSERVERDBINIT;
    rgInitPropSet[1].cProperties = nInitProps2;
    rgInitPropSet[1].rgProperties = InitProperties2;

    // Set initialization properties.
    CComPtr<IDBProperties> pIDBProperties;
    hr = pIDBInitialize->QueryInterface(IID_IDBProperties,
                                        reinterpret_cast<LPVOID *>(&pIDBProperties));
    if (FAILED(hr))
    {
        std::cout << "Failed to obtain IDBProperties interface.\n";
        return hr;
    }

    hr = pIDBProperties->SetProperties(nPropSets, rgInitPropSet);
    if (FAILED(hr)) {
        std::cout << "Failed to set initialization properties\n";
        return hr;
    }

    // Now we establish connection to the data source.
    if (FAILED(hr = pIDBInitialize->Initialize())) {
        std::cout << "Problem in initializing\n";
    }

    return hr;
}

HRESULT ExecuteFunction(const CComPtr<IDBInitialize>& pIDBInitialize)
{
    HRESULT hr = S_OK;

    CComPtr<IDBCreateSession> pIDBCreateSession;
    // Let us create a new session from the data source object.
    if (FAILED(hr = pIDBInitialize->QueryInterface(IID_IDBCreateSession,
                                                   reinterpret_cast<LPVOID *>(&pIDBCreateSession))))
    {
        std::cout << "Failed to access IDBCreateSession interface\n";
        return hr;
    }

    CComPtr<IDBCreateCommand> pIDBCreateCommand;
    if (FAILED(hr = pIDBCreateSession->CreateSession(NULL,
                                                     IID_IDBCreateCommand,
                                                     reinterpret_cast<IUnknown **>(&pIDBCreateCommand))))
    {
        std::cout << "Failed to obtain IDBCreateCommand interface\n";
        return hr;
    }

    // Create a Command
    CComPtr<ICommandText> pICommandText;
    if (FAILED(hr = pIDBCreateCommand->CreateCommand(NULL,
                                                     IID_ICommandText,
                                                     reinterpret_cast<IUnknown **>(&pICommandText))))
    {
        std::cout << "Failed to access ICommand interface\n";
        return hr;
    }

    // The following buffer is used to store parameter values.
    typedef struct tagSPROCPARAMS
    {
        long lReturnValue;
        long inParam1;
        long inParam2;
    } SPROCPARAMS;

    // Set the command text.
    if (FAILED(hr = pICommandText->SetCommandText(DBGUID_DBSQL, L"{? = CALL fn_RectangleArea(?, ?) }")))
    {
        std::cout << "Failed to set command text\n";
        return hr;
    }

    // Set the parameters information.
    CComPtr<ICommandWithParameters> pICommandWithParams;
    if (FAILED(hr = pICommandText->QueryInterface(IID_ICommandWithParameters,
                                                  reinterpret_cast<LPVOID *>(&pICommandWithParams))))
    {
        std::cout << "Failed to obtain ICommandWithParameters\n";
        return hr;
    }

    const ULONG nParams = 3;   // No. of parameters in the command
    DBPARAMBINDINFO ParamBindInfo[nParams] = {};
    DB_UPARAMS ParamOrdinals[nParams] = {};
    DBROWCOUNT cNumRows = 0;
    
    // Describe the command parameters (parameter name, provider specific name
    // of the parameter's data type etc.) in an array of DBPARAMBINDINFO
    // structures.  This information is then used by SetParameterInfo().
    ParamBindInfo[0].pwszDataSourceType = const_cast<LPOLESTR>(L"DBTYPE_I4");
    ParamBindInfo[0].pwszName = NULL;
    ParamBindInfo[0].ulParamSize = sizeof(long);
    ParamBindInfo[0].dwFlags = DBPARAMFLAGS_ISOUTPUT;
    ParamBindInfo[0].bPrecision = 11;
    ParamBindInfo[0].bScale = 0;
    ParamOrdinals[0] = 1;

    ParamBindInfo[1].pwszDataSourceType = const_cast<LPOLESTR>(L"DBTYPE_I4");
    ParamBindInfo[1].pwszName = NULL;   // L"@inparam1";
    ParamBindInfo[1].ulParamSize = sizeof(long);
    ParamBindInfo[1].dwFlags = DBPARAMFLAGS_ISINPUT;
    ParamBindInfo[1].bPrecision = 11;
    ParamBindInfo[1].bScale = 0;
    ParamOrdinals[1] = 2;

    ParamBindInfo[2].pwszDataSourceType = const_cast<LPOLESTR>(L"DBTYPE_I4");
    ParamBindInfo[2].pwszName = NULL;   // L"@inparam2";
    ParamBindInfo[2].ulParamSize = sizeof(long);
    ParamBindInfo[2].dwFlags = DBPARAMFLAGS_ISINPUT;
    ParamBindInfo[2].bPrecision = 11;
    ParamBindInfo[2].bScale = 0;
    ParamOrdinals[2] = 3;

    if (FAILED(hr = pICommandWithParams->SetParameterInfo(nParams,
                                                          ParamOrdinals,
                                                          ParamBindInfo)))
    {
        std::cout << "Failed in setting parameter info.(SetParameterInfo)\n";
        return hr;
    }

    HACCESSOR hAccessor = 0;
    SPROCPARAMS sprocparams = {0,5,10};

    // Declare array of DBBINDING structures, one for each parameter in the command
    DBBINDING acDBBinding[nParams] = {};
    
    // Describe the consumer buffer; initialize the array of DBBINDING structures.
    // Each binding associates a single parameter to the consumer's buffer.
    for (ULONG i = 0; i < nParams; i++)
    {
        acDBBinding[i].obLength = 0;
        acDBBinding[i].obStatus = 0;
        acDBBinding[i].pTypeInfo = NULL;
        acDBBinding[i].pObject = NULL;
        acDBBinding[i].pBindExt = NULL;
        acDBBinding[i].dwPart = DBPART_VALUE;
        acDBBinding[i].dwMemOwner = DBMEMOWNER_CLIENTOWNED;
        acDBBinding[i].dwFlags = 0;
        acDBBinding[i].bScale = 0;
    }   // for

    acDBBinding[0].iOrdinal = 1;
    acDBBinding[0].obValue = offsetof(SPROCPARAMS, lReturnValue);
    acDBBinding[0].eParamIO = DBPARAMIO_OUTPUT;
    acDBBinding[0].cbMaxLen = sizeof(long);
    acDBBinding[0].wType = DBTYPE_I4;
    acDBBinding[0].bPrecision = 11;

    acDBBinding[1].iOrdinal = 2;
    acDBBinding[1].obValue = offsetof(SPROCPARAMS, inParam1);
    acDBBinding[1].eParamIO = DBPARAMIO_INPUT;
    acDBBinding[1].cbMaxLen = sizeof(long);
    acDBBinding[1].wType = DBTYPE_I4;
    acDBBinding[1].bPrecision = 11;

    acDBBinding[2].iOrdinal = 3;
    acDBBinding[2].obValue = offsetof(SPROCPARAMS, inParam2);
    acDBBinding[2].eParamIO = DBPARAMIO_INPUT;
    acDBBinding[2].cbMaxLen = sizeof(long);
    acDBBinding[2].wType = DBTYPE_I4;
    acDBBinding[2].bPrecision = 11;

    // Let us create an accessor from the above set of bindings.
    CComPtr<IAccessor> pIAccessor;
    hr = pICommandWithParams->QueryInterface(IID_IAccessor,
                                             reinterpret_cast<LPVOID *>(&pIAccessor));
    if (FAILED(hr))
    {
        std::cout << "Failed to get IAccessor interface\n";
        return hr;
    }

    DBBINDSTATUS acDBBindStatus[nParams] = {};
    hr = pIAccessor->CreateAccessor(DBACCESSOR_PARAMETERDATA,
                                    nParams,
                                    acDBBinding,
                                    sizeof(SPROCPARAMS),
                                    &hAccessor,
                                    acDBBindStatus);
    if (FAILED(hr))
    {
        std::cout << "Failed to create accessor for the defined parameters\n";
        return hr;
    }

    // Initialize DBPARAMS structure for command execution. DBPARAMS specifies the
    // parameter values in the command.  DBPARAMS is then passed to Execute.
    DBPARAMS Params = {nullptr, 0, 0};
    Params.pData = &sprocparams;
    Params.cParamSets = 1;
    Params.hAccessor = hAccessor;

    // Execute the command.
    if (SUCCEEDED(hr = pICommandText->Execute(nullptr,
                                        IID_NULL,
                                        &Params,
                                        &cNumRows,
                                        nullptr)))
    {
        printf("Return value = %d\n", sprocparams.lReturnValue);
    }
    else 
    {
        std::cout << "Failed to execute command\n";
    }

    // Release memory.
    pIAccessor->ReleaseAccessor(hAccessor, nullptr); 
    return hr;
}
```  
  
```sql
drop function fn_RectangleArea  
go  
```  
  
## See Also  
 [Processing Results How-to Topics &#40;OLE DB&#41;](../../../oledb/ole-db-how-to/results/processing-results-how-to-topics-ole-db.md)  
  
