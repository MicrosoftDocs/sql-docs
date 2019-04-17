---
title: "Use Table-Valued Parameters (OLE DB) | Microsoft Docs"
description: "Use Table-Valued Parameters (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Use Table-Valued Parameters (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This sample works with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] or later. This sample does the following:  
  
-   Creates table-valued parameters by using dynamic discovery though IOpenRowset::OpenRowset.  
  
-   Sends table-valued parameter rows by using the pull model in the EmployeesRowset class. In the pull model, the consumer provides data on demand to the provider.  
  
-   Sends BLOBs as part of a table-valued parameter in the CPhotograph class.  
  
-   Uses custom parameter properties using ISSCommandWithParameters.  
  
-   Shows error handling for MSOLEDBSQL errors.  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;OLE DB Driver for SQL Server&#41;](../../oledb/features/table-valued-parameters-oledb-driver-for-sql-server.md).  
  
## Example  
 The first ( [!INCLUDE[tsql](../../../includes/tsql-md.md)]) code listing creates the database used by the sample.  
  
 Put the second code listing into a file called stdafx.h.  
  
 Put the third code listing into a file called OLEDBUtils.hpp.  
  
 Compile with ole32.lib oleaut32.lib and execute the fourth (C++) code listing. This application connects to your computer's default [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. On some Windows operating systems, you will need to change (localhost) or (local) to the name of your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. To connect to a named instance, change the connection string from L"(local)" to L"(local)\\\name" , where name is the named instance. By default, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Express installs to a named instance. Make sure your INCLUDE environment variable includes the directory that contains msoledbsql.h.  
  
 The fifth ( [!INCLUDE[tsql](../../../includes/tsql-md.md)]) code listing creates the database used by the sample.  
  
```  
create database testdb  
go  
use testdb  
go  
create table tblEmployees (  
id int identity primary key,  
name nvarchar(50) not null,  
birthday date null,  
salary int null,  
photograph varbinary(max) null  
)  
go  
  
create type tvpEmployees as table(  
name nvarchar(50) not null,  
birthday date null,  
salary int null,  
photograph varbinary(max) null  
)  
go  
  
create procedure insertEmployees @tvpEmployees tvpEmployees readonly,   
@id int output as  
insert tblEmployees(name, birthday, salary, photograph)  
select name, birthday, salary, photograph from @tvpEmployees  
select @id = coalesce(scope_identity(), -1)  
go  
```  
  
```  
// stdafx.h : include file for standard system include files,  
// or project specific include files that are used frequently, but  
// are changed infrequently  
//  
  
#pragma once  
  
// The following macros define the minimum required platform.  The minimum required platform  
// is the earliest version of Windows, Internet Explorer etc. that has the necessary features to run   
// your application.  The macros work by enabling all features available on platform versions up to and   
// including the version specified.  
  
// Modify the following defines if you have to target a platform prior to the ones specified below.  
// Refer to MSDN for the latest info on corresponding values for different platforms.  
#ifndef _WIN32_WINNT            // Specifies that the minimum required platform is Windows Vista.  
#define _WIN32_WINNT 0x0600     // Change this to the appropriate value to target other versions of Windows.  
#endif  
  
#include <stdio.h>  
#include <tchar.h>  
#include <stdlib.h>  
#include <stddef.h>  
#include <assert.h>  
#include <windows.h>  
#include <strsafe.h>  
// #defines necessary for initializing the CLSID & IIDs of OLEDB specific interfaces  
#define DBINITCONSTANTS  
#define INITGUID  
#include <oledberr.h>  
#include <msoledbsql.h>  
```  
  
```  
// OLEDBUtils.hpp  
#pragma once  
  
// Utility Macros & Functions  
#define CHKHR_GOTO(hr, Label) \  
   { if (FAILED(hr)) { wprintf(L"Error in file %S at line %d.\n", __FILE__, __LINE__); goto Label;} };  
  
#define CHKHR_GOTO_MSG(hr, Label, wszMessage) \  
{ if (FAILED(hr)) { wprintf(L"Error in file %S at line %d.\nError Message: %s\n", __FILE__, __LINE__, wszMessage); goto Label;} };  
  
#define CHKHR_OLEDB_GOTO(hr, Label, pItf, IID_Itf) \  
{ if (FAILED(hr)) { wprintf(L"Error in file %S at line %d.\n", __FILE__, __LINE__); DumpErrorInfo(pItf, IID_Itf); goto Label;} };  
  
#define NUMELEM(arr) (sizeof(arr) / sizeof(arr[0]))   
  
// Template function that checks the NULL-ness of a COM interface and if it is non-NULL releases it & also sets it to NULL  
template<class T>  
void Release(T** pUnkCOMItf) {  
if (*pUnkCOMItf) {  
(*pUnkCOMItf)->Release();  
*pUnkCOMItf = NULL;  
}  
}  
  
// Utility routine for displaying OLEDB errors  
void DumpErrorInfo (  
    IUnknown* pObjectWithError,  
    REFIID IID_InterfaceWithError  
);  
  
// COM Load/Unload Helper  
class CCOMLoader {  
public:  
HRESULT Load() {  
return CoInitializeEx(NULL, COINIT_MULTITHREADED);  
}  
~CCOMLoader() {  
CoUninitialize();  
}  
};  
  
// Represents an OLEDB data source, used for connection & session creation  
class CMSOLEDBSQLDataSource {  
private:  
bool m_fIsConnected;  
IDBInitialize* m_pIDBInitialize;   // Data Source Initialization interface  
  
public:  
CMSOLEDBSQLDataSource() : m_pIDBInitialize(NULL), m_fIsConnected(false) {}  
  
      HRESULT Connect(const wchar_t* wszDataSource, const wchar_t* wszCatalog) {  
         HRESULT hr = S_OK;  
         IDBProperties* pIDBProperties = NULL;  
  
         const ULONG INIT_PROPS = 3;   
         DBPROP rgInitProps[INIT_PROPS] = {0};  
         const ULONG INIT_PROPSETS = 1;  
         DBPROPSET rgInitPropSets[INIT_PROPSETS] = {0};  
  
         //Obtain access to the MSOLEDBSQL provider.  
         hr = CoCreateInstance(CLSID_MSOLEDBSQL, NULL, CLSCTX_INPROC_SERVER, IID_IDBInitialize, reinterpret_cast<void **>(&m_pIDBInitialize));  
         CHKHR_GOTO_MSG(hr, _Exit, L"Unable to load MSOLEDBSQL");  
  
         // Set initialization property values  
         SetPropertyBSTR(DBPROP_INIT_DATASOURCE, wszDataSource,  &rgInitProps[0]);  
         SetPropertyBSTR(DBPROP_INIT_CATALOG,    wszCatalog, &rgInitProps[1]);  
         SetPropertyBSTR(DBPROP_AUTH_INTEGRATED, L"SSPI", &rgInitProps[2]);  
  
         // Setup the initialization property sets  
         InitializePropSet(  
            rgInitPropSets,  
            NUMELEM(rgInitProps),  
            DBPROPSET_DBINIT,  
            rgInitProps);  
  
         hr = m_pIDBInitialize->QueryInterface(IID_IDBProperties, reinterpret_cast<void**>(&pIDBProperties));  
         CHKHR_GOTO_MSG(hr, _Exit, L"Failure to QI IDBInitialize.");  
  
         hr = pIDBProperties->SetProperties(NUMELEM(rgInitPropSets), rgInitPropSets);  
         CHKHR_OLEDB_GOTO(hr, _Exit, pIDBProperties, IID_IDBProperties);  
  
         hr = m_pIDBInitialize->Initialize();  
         CHKHR_OLEDB_GOTO(hr, _Exit, m_pIDBInitialize, IID_IDBInitialize);  
  
         m_fIsConnected = true;  
  
_Exit:  
         Release(&pIDBProperties);  
         CleanPropSet(rgInitPropSets);  
         return hr;  
      }  
  
HRESULT GetSession(IOpenRowset** ppIOpenRowset) {  
assert(m_pIDBInitialize);  
assert(m_fIsConnected);  
  
HRESULT hr = S_OK;  
IDBCreateSession* pIDBCreateSession = NULL;  
  
        if (m_pIDBInitialize)  
           hr = m_pIDBInitialize->QueryInterface(IID_IDBCreateSession, reinterpret_cast<void**>(&pIDBCreateSession));  
CHKHR_GOTO_MSG(hr, _Exit, L"Failure to QI IDBCreateSession.");  
  
        if (pIDBCreateSession)  
           hr = pIDBCreateSession->CreateSession(NULL, IID_IOpenRowset, reinterpret_cast<IUnknown**>(ppIOpenRowset));  
CHKHR_OLEDB_GOTO(hr, _Exit, pIDBCreateSession, IID_IDBCreateSession);  
_Exit:  
Release(&pIDBCreateSession);  
return hr;  
}  
  
    ~CMSOLEDBSQLDataSource() {  
if (m_fIsConnected) {  
assert(m_pIDBInitialize);  
            HRESULT hr = S_OK;  
            if (m_pIDBInitialize)  
               hr = m_pIDBInitialize->Uninitialize();  
CHKHR_OLEDB_GOTO(hr, _Exit, m_pIDBInitialize, IID_IDBInitialize);  
}  
_Exit:   
Release(&m_pIDBInitialize);  
}  
  
private:  
void InitializePropSet( DBPROPSET* pPropSet, ULONG cProps, GUID guidPropSet, DBPROP* pProps ) {  
pPropSet->cProperties     = cProps;  
pPropSet->guidPropertySet = guidPropSet;  
pPropSet->rgProperties    = pProps;  
}  
  
void CleanPropSet (DBPROPSET*  pPropSet) {  
for (ULONG idxProp = 0; idxProp < pPropSet->cProperties; idxProp++)  
if (pPropSet->rgProperties[idxProp].vValue.vt == VT_BSTR)  
SysFreeString(pPropSet->rgProperties[idxProp].vValue.bstrVal);  
}  
  
void SetPropertyBSTR (DBPROPID propID, const wchar_t*  wszValue, DBPROP* pProperty) {  
pProperty->dwPropertyID    = propID;  
pProperty->dwOptions       = DBPROPOPTIONS_REQUIRED;  
pProperty->colid           = DB_NULLID;  
pProperty->vValue.vt       = VT_BSTR;  
pProperty->vValue.bstrVal  = SysAllocStringLen(wszValue, (UINT)wcslen(wszValue));  
}  
  
void SetPropertyBool( DBPROP* pProperty, DBPROPID dwPropID, VARIANT_BOOL boolValue) {  
pProperty->dwPropertyID     = dwPropID;  
pProperty->dwOptions        = DBPROPOPTIONS_REQUIRED;  
pProperty->colid            = DB_NULLID;  
pProperty->vValue.vt        = VT_BOOL;  
pProperty->vValue.boolVal   = boolValue;  
}  
  
void SetPropertyI8 ( DBPROP* pProperty, DBPROPID dwPropID, LONGLONG i8Value ) {  
pProperty->dwPropertyID     = dwPropID;  
pProperty->dwOptions        = DBPROPOPTIONS_REQUIRED;  
pProperty->colid            = DB_NULLID;  
pProperty->vValue.vt        = VT_I8;  
pProperty->vValue.llVal     = i8Value;  
}  
};  
  
class CPhotograph : public ISequentialStream {  
private:  
    DBREFCOUNT  m_cRef;  
    BYTE*       m_pbStream;  
    size_t      m_cbStreamLength;  
    size_t      m_idxStreamOffset;  
  
public:  
    CPhotograph(size_t cbStreamLength) : m_cbStreamLength(cbStreamLength), m_idxStreamOffset(0), m_cRef(1) {  
        m_pbStream = new BYTE[m_cbStreamLength];  
  
// Generate random data for the photograph stream  
for (size_t i = 0; i < m_cbStreamLength; i++)  
m_pbStream[i] = static_cast<BYTE>(rand() % 256);  
    }  
  
    ~CPhotograph() {  
        delete [] m_pbStream;  
    }  
  
    STDMETHODIMP QueryInterface(REFIID riid, LPVOID* ppv) {  
        if (ppv == NULL)  
           return E_INVALIDARG;  
  
        if (riid == IID_IUnknown || riid == IID_ISequentialStream)  
            *ppv = reinterpret_cast<void*>(this);  
        else  
            *ppv = NULL;  
  
        if (*ppv) {  
            (reinterpret_cast<IUnknown*>(*ppv))->AddRef();  
            return S_OK;  
        }  
  
        return E_NOINTERFACE;     
    }  
  
    // @cmember Increments the Reference count  
    STDMETHODIMP_(DBREFCOUNT) AddRef() {  
        return InterlockedIncrement((long*)&m_cRef);  
    }  
  
    STDMETHODIMP_(DBREFCOUNT) Release() {  
        assert(m_cRef > 0);  
        ULONG cRef = InterlockedDecrement((long*) &m_cRef);  
  
        if (!cRef)  
            delete this;  
  
        return cRef;  
    }  
  
    STDMETHODIMP Read(void* pBuffer, ULONG cb, ULONG* pcb) {  
        if (pcb)   
           *pcb = 0;  
  
        if (m_idxStreamOffset == m_cbStreamLength)  
            return S_FALSE;  
  
        size_t cbRemainingBytes = m_cbStreamLength - m_idxStreamOffset;  
  
        if (pcb)   
           *pcb = min(cb, static_cast<ULONG>(cbRemainingBytes));  
  
        memcpy(pBuffer, m_pbStream + m_idxStreamOffset,  min(cb, cbRemainingBytes));   
        return S_OK;  
    }  
  
    STDMETHODIMP Write(const void*, ULONG, ULONG* /*pcb*/ ) {  
        return E_NOTIMPL;  
    }  
};  
  
void DumpErrorInfo (IUnknown*   pObjectWithError, REFIID IID_InterfaceWithError) {  
    // Interfaces used in the example.  
    IErrorInfo*             pIErrorInfoAll          = NULL;  
    IErrorInfo*             pIErrorInfoRecord       = NULL;  
    IErrorRecords*          pIErrorRecords          = NULL;  
    ISupportErrorInfo*      pISupportErrorInfo      = NULL;  
    ISQLErrorInfo*          pISQLErrorInfo          = NULL;  
    ISQLServerErrorInfo*    pISQLServerErrorInfo    = NULL;  
  
    // Number of error records.  
    ULONG                   nRecs;  
    ULONG                   nRec;  
  
    // Basic error information from GetBasicErrorInfo.  
    ERRORINFO               errorinfo;  
  
    // IErrorInfo values.  
    BSTR                    bstrDescription;  
    BSTR                    bstrSource;  
  
    // ISQLErrorInfo parameters.  
    BSTR                    bstrSQLSTATE;  
    LONG                    lNativeError;  
  
    // ISQLServerErrorInfo parameter pointers.  
    SSERRORINFO*            pSSErrorInfo    = NULL;  
    OLECHAR*                pSSErrorStrings = NULL;  
  
    // Obtain the default locale ID  
    DWORD                   MYLOCALEID = GetUserDefaultLCID();  
  
    // Only ask for error information if the interface supports it.  
    if (FAILED(pObjectWithError->QueryInterface(IID_ISupportErrorInfo, (void**) &pISupportErrorInfo)))  
        return;  
  
    if (FAILED(pISupportErrorInfo->InterfaceSupportsErrorInfo(IID_InterfaceWithError)))  
        return;  
  
    // Do not test the return of GetErrorInfo. It can succeed and return  
    // a NULL pointer in pIErrorInfoAll. Simply test the pointer.  
    if (GetErrorInfo(0, &pIErrorInfoAll) == S_FALSE) {  
        pISupportErrorInfo->Release();  
        pISupportErrorInfo = NULL;  
        return;  
    }  
  
    if (pIErrorInfoAll != NULL) {  
        // Test to see if it's a valid OLE DB IErrorInfo interface   
        // exposing a list of records.  
        if (SUCCEEDED(pIErrorInfoAll->QueryInterface(IID_IErrorRecords,(void**) &pIErrorRecords))) {  
            pIErrorRecords->GetRecordCount(&nRecs);  
  
            // Within each record, retrieve information from each  
            // of the defined interfaces.  
            for (nRec = nRecs - 1; (long)nRec >= 0; nRec--) {  
                // From IErrorRecords, get the HRESULT and a reference  
                // to the ISQLErrorInfo interface.  
                pIErrorRecords->GetBasicErrorInfo(nRec, &errorinfo);  
                pIErrorRecords->GetCustomErrorObject(nRec,IID_ISQLErrorInfo, (IUnknown**) &pISQLErrorInfo);  
  
                if (pISQLErrorInfo != NULL) {  
                    pISQLErrorInfo->GetSQLInfo(&bstrSQLSTATE, &lNativeError);  
  
                    if (bstrSQLSTATE[0] == '0' && bstrSQLSTATE[1] == '1') {}  
                    else {  
                        // Display the SQLSTATE and native error values.  
                        wprintf(L"SQLSTATE:\t%s\nNative Error:\t%ld\n",  
                            bstrSQLSTATE, lNativeError);  
  
                        // SysFree BSTR references.  
                        SysFreeString(bstrSQLSTATE);  
                    }  
  
                    // Get the ISQLServerErrorInfo interface from  
                    // ISQLErrorInfo before releasing the reference.  
                    pISQLErrorInfo->QueryInterface(IID_ISQLServerErrorInfo, (void**) &pISQLServerErrorInfo);  
  
                    pISQLErrorInfo->Release();  
                    pISQLErrorInfo = NULL;  
                }  
  
                // Test to ensure the reference is valid, then  
                // get error information from ISQLServerErrorInfo.  
                if (pISQLServerErrorInfo != NULL) {  
                    pISQLServerErrorInfo->GetErrorInfo(&pSSErrorInfo,&pSSErrorStrings);  
  
                    // ISQLServerErrorInfo::GetErrorInfo succeeds  
                    // even when it has nothing to return. Test the  
                    // pointers before using.  
                    if (pSSErrorInfo) {  
                        // Display the state and severity from the  
                        // returned information. The error message comes  
                        // from IErrorInfo::GetDescription.  
                        wprintf(L"Error state:\t%d\nSeverity:\t%d\n",  
                                pSSErrorInfo->bState,  
                                pSSErrorInfo->bClass);  
  
// IMalloc::Free needed to release references  
                        // on returned values. For the example, assume  
                        // the g_pIMalloc pointer is valid.  
                        CoTaskMemFree(pSSErrorStrings);  
                        CoTaskMemFree(pSSErrorInfo);  
                    }  
  
                    pISQLServerErrorInfo->Release();  
                    pISQLServerErrorInfo = NULL;  
                }  
  
                if (SUCCEEDED(pIErrorRecords->GetErrorInfo(nRec,MYLOCALEID, &pIErrorInfoRecord))) {  
                    // Get the source and description (error message)  
                    // from the record's IErrorInfo.  
                    pIErrorInfoRecord->GetSource(&bstrSource);  
                    pIErrorInfoRecord->GetDescription(&bstrDescription);  
  
                    if (bstrSource != NULL) {  
                        wprintf(L"Source:\t\t%s\n", bstrSource);  
  
                        SysFreeString(bstrSource);  
                    }  
                    if (bstrDescription != NULL) {  
                        wprintf(L"Error message:\t%s\n", bstrDescription);  
                        SysFreeString(bstrDescription);  
                    }  
                    pIErrorInfoRecord->Release();  
                    pIErrorInfoRecord = NULL;  
                }  
            }  
  
            pIErrorRecords->Release();  
            pIErrorRecords = NULL;  
        }  
        else {  
            // IErrorInfo is valid; get the source and  
            // description to see what it is.  
            pIErrorInfoAll->GetSource(&bstrSource);  
            pIErrorInfoAll->GetDescription(&bstrDescription);  
  
            if (bstrSource != NULL) {  
                wprintf(L"Source:\t\t%s\n", bstrSource);  
                SysFreeString(bstrSource);  
            }  
            if (bstrDescription != NULL) {  
                wprintf(L"Error message:\t%s\n", bstrDescription);  
                SysFreeString(bstrDescription);  
            }  
        }  
  
        pIErrorInfoAll->Release();  
        pIErrorInfoAll = NULL;  
    }  
  
    pISupportErrorInfo->Release();  
    pISupportErrorInfo = NULL;  
}  
```  
  
```  
// compile with: /D "_UNICODE" /D "UNICODE" ole32.lib oleaut32.lib  
#include "stdafx.h"  
#include "OLEDBUtils.hpp"  
  
class BaseAggregatingRowset : public IRowset {  
public:  
   BaseAggregatingRowset(DBCOUNTITEM cTotalRows) : m_cRef(0), m_idxRow(1), m_cTotalRows(cTotalRows), m_pUnkInnerMSOLEDBSQLRowset(NULL) {  
      m_hAccessor[0] = 0;  
   }  
  
      virtual HRESULT SetupAccessors(IAccessor* pIAccessorTVP) = 0;  
  
      STDMETHODIMP_(ULONG) AddRef() {  
         ULONG cRef = InterlockedIncrement((long*)&m_cRef);  
         return cRef;  
      }  
  
      STDMETHODIMP_(ULONG) Release() {  
         assert(m_cRef > 0);  
  
         ULONG cRef = InterlockedDecrement((long *) &m_cRef);  
  
         if (!cRef)  
            delete this;  
  
         return cRef;  
      }  
  
      // In QueryInterface, delegate to Inner Rowset for anything but IRowset & IUnknown  
      STDMETHODIMP QueryInterface (REFIID  riid, LPVOID* ppv ) {  
         if (riid == IID_IUnknown)   
            *ppv = static_cast<IUnknown*>(this);  
         else {  
            // If we are not initialized yet and somebody is asking for non-Unk interface  
            if (!m_pUnkInnerMSOLEDBSQLRowset) {  
               *ppv = NULL;  
               return E_NOINTERFACE;  
            }  
  
            if (riid == IID_IRowset)   
               *ppv = static_cast<IUnknown*>(this);  
            else  
               return m_pUnkInnerMSOLEDBSQLRowset->QueryInterface(riid, ppv);  
         }  
  
         (reinterpret_cast<IUnknown*>(*ppv))->AddRef();  
         return S_OK;  
      }  
  
      STDMETHODIMP AddRefRows (DBCOUNTITEM, const HROW[], DBREFCOUNT[], DBROWSTATUS[]) {  
         // Never gets called, so we can return E_NOTIMPL  
         return E_NOTIMPL;  
      }  
  
      // Read the data from storage, allocate row handles and give   
      // them back to the caller.  
      STDMETHODIMP GetNextRows( HCHAPTER, DBROWOFFSET cRowsToSkip, DBROWCOUNT cRows, DBCOUNTITEM* pcRowsObtained, HROW** prghRows) {  
         assert(cRowsToSkip == 0);  
         assert(cRows == 1);  
         assert(*prghRows);  
  
         *pcRowsObtained = 0;  
  
         // If we still have rows to give back  
         if (m_idxRow <= m_cTotalRows) {  
            *pcRowsObtained = 1;  
  
            // For us, row handle is simply an index in our row list  
            HROW* phRows = *prghRows;  
            *phRows = m_idxRow;  
            m_idxRow++;  
  
            return S_OK;  
         }  
         else  
            return DB_S_ENDOFROWSET;  
      }  
  
      // Release data that is not needed corresponding to row handle  
      STDMETHODIMP ReleaseRows(DBCOUNTITEM cRows, const HROW rghRows[], DBROWOPTIONS[], DBREFCOUNT[], DBROWSTATUS[]) {  
         assert(cRows == 1);  
         assert(rghRows[0] <= m_cTotalRows);  
         return S_OK;  
      }  
  
      STDMETHODIMP GetData(HROW, HACCESSOR hAccessor, void*) {  
#ifdef _DEBUG  
         DBORDINAL idxAccessor;  
         for (idxAccessor = 0; idxAccessor < 1; idxAccessor++) {  
            if (m_hAccessor[idxAccessor] == hAccessor)  
               break;  
         }  
         assert(idxAccessor < 1);  
#endif  
         return S_OK;  
      }  
  
      STDMETHODIMP RestartPosition( HCHAPTER) {  
         m_idxRow = 1;  
         return S_OK;  
      }  
  
protected:  
   DBCOUNTITEM m_idxRow;  
   IUnknown* m_pUnkInnerMSOLEDBSQLRowset;  
  
   // Make the destructor private, so that the object is only creatable on the heap  
   virtual ~BaseAggregatingRowset() {  
      HRESULT hr = S_OK;  
      if (m_hAccessor[0]) {  
         IAccessor* pIAccessor = NULL;  
         hr = m_pUnkInnerMSOLEDBSQLRowset->QueryInterface(IID_IAccessor, reinterpret_cast<void**>(&pIAccessor));  
         assert(SUCCEEDED(hr));  
         hr = pIAccessor->ReleaseAccessor(m_hAccessor[0], NULL);  
         assert(SUCCEEDED(hr));  
      }  
      ::Release(&m_pUnkInnerMSOLEDBSQLRowset);  
   }  
  
   // Save the handle of the accessor that we create, the indexing is 0 based  
   void SetAccessorHandle(DBORDINAL idxAccessor, HACCESSOR hAccessor) {  
      m_hAccessor[idxAccessor] = hAccessor;  
   }  
  
private:  
   ULONG m_cRef;  
   DBCOUNTITEM  m_cTotalRows;  
   // Defining as an array because in general there can be as many accessors as necessary  
   // the reading rules from the provider for such scenarios are describe in the Books online  
   HACCESSOR m_hAccessor[1];  
};  
  
// There is just 1 accessor for this Rowset  
class EmployeesRowset : public BaseAggregatingRowset {  
private:  
   struct EmployeeData {  
      DBLENGTH  nameLength;  
      DBSTATUS  nameStatus;  
      wchar_t   nameValue[50 + 1];  
      DBLENGTH  birthdayLength;  
      DBSTATUS  birthdayStatus;  
      DBDATE    birthdayValue;  
      DBLENGTH  salaryLength;  
      DBSTATUS  salaryStatus;  
      long      salaryValue;  
      DBLENGTH  photographLength;  
      DBSTATUS  photographStatus;  
      IUnknown* photographValue;  
   };  
  
protected:  
   // Make the destructor private, so that the object is only creatable on the heap  
   virtual ~EmployeesRowset() {}  
  
public:  
   EmployeesRowset ( DBCOUNTITEM cTotalRows ) : BaseAggregatingRowset(cTotalRows) {  
      // For the random number generator, used for producing dummy random data  
      srand(123456);  
   }  
  
   // Set up aggregator & aggregatee relationship here  
   HRESULT Initialize(IOpenRowset* pIOpenRowset) {  
      HRESULT hr = S_OK;  
  
      IUnknown* pUnkOuter = static_cast<IUnknown*>(this);  
      IAccessor* pIAccessorEmployees = NULL;  
  
      DBID dbidEmployees;  
      dbidEmployees.eKind = DBKIND_GUID_NAME;  
      dbidEmployees.uGuid.guid = CLSID_ROWSET_TVP;  
      dbidEmployees.uName.pwszName = L"tvpEmployees";  
  
      hr = pIOpenRowset->OpenRowset(pUnkOuter, &dbidEmployees, NULL, IID_IUnknown, 0, NULL, &m_pUnkInnerMSOLEDBSQLRowset);  
      CHKHR_OLEDB_GOTO(hr, _Exit, pIOpenRowset, IID_IOpenRowset);  
  
      hr = pUnkOuter->QueryInterface(IID_IAccessor, reinterpret_cast<void**>(&pIAccessorEmployees));  
      CHKHR_GOTO_MSG(hr, _Exit, L"Failed to QI IAccessor.");  
  
      hr = SetupAccessors(pIAccessorEmployees);  
      CHKHR_GOTO(hr, _Exit);  
  
_Exit:  
      ::Release(&pIAccessorEmployees);  
      return hr;  
   }  
  
   STDMETHODIMP GetData ( HROW hRow, HACCESSOR hAccessor, void* pData ) {  
      // The base implementation just does validation, could have possibly  
      // been made an abstract virtual function  
      BaseAggregatingRowset::GetData(hRow, hAccessor, pData);  
  
      // Use m_hAccessor, to figure out which accessor caller specified, and write the columns data   
      // for columns corresponding to those accessors into pData. Fetch the data into provided buffer,   
      // we will know the format of these accessors, because we created them.  
  
      EmployeeData* pCurrentEmployee = reinterpret_cast<EmployeeData*>(pData);  
      FillRowData(pCurrentEmployee);  
  
      return S_OK;  
   }  
private:  
  
   HRESULT SetupAccessors(IAccessor* pIAccessorEmployees) {  
      HRESULT hr = S_OK;  
  
      DBBINDING bindingsEmployees[4];  
      FillBindingsAndSetupRowBuffer(bindingsEmployees);  
  
      HACCESSOR hAccessorEmployees;  
      DBBINDSTATUS bindStatusEmployees[4] = {DBBINDSTATUS_OK, DBBINDSTATUS_OK, DBBINDSTATUS_OK, DBBINDSTATUS_OK};  
  
      hr = pIAccessorEmployees->CreateAccessor(  
         DBACCESSOR_ROWDATA,   
         4,   
         bindingsEmployees,   
         sizeof(EmployeeData),  
         &hAccessorEmployees,   
         bindStatusEmployees);   
      CHKHR_OLEDB_GOTO(hr, _Exit, pIAccessorEmployees, IID_IAccessor);  
  
      SetAccessorHandle(0, hAccessorEmployees);  
_Exit:  
      return hr;  
   }  
  
   // This routine does the job of populating data for each row, in real world scenarios, hRow could  
   // possibly be passed here, in order to identify the particular row of data & it could be read  
   // from some persistent medium like disk/network/UI-controls etc  
   void FillRowData(EmployeeData* pCurrentEmployee) {  
      pCurrentEmployee->birthdayStatus = DBSTATUS_S_OK;  
      pCurrentEmployee->birthdayLength = sizeof(DBDATE);  
      pCurrentEmployee->birthdayValue.day   = 15;  
      pCurrentEmployee->birthdayValue.month = 5;  
      pCurrentEmployee->birthdayValue.year  = 1980;  
  
      pCurrentEmployee->salaryLength = sizeof(long);  
      pCurrentEmployee->salaryStatus = DBSTATUS_S_OK;  
      pCurrentEmployee->salaryValue  = 100000;  
  
      pCurrentEmployee->nameLength = static_cast<DBLENGTH>(wcslen(L"John Doe") * sizeof(wchar_t));  
      pCurrentEmployee->nameStatus = DBSTATUS_S_OK;  
      StringCchCopy(pCurrentEmployee->nameValue, sizeof(pCurrentEmployee->nameValue) / sizeof(wchar_t), L"John Doe");  
  
      pCurrentEmployee->photographLength = 2000 + (rand() % 2000);  
      pCurrentEmployee->photographStatus = DBSTATUS_S_OK;  
      pCurrentEmployee->photographValue  = new CPhotograph(pCurrentEmployee->photographLength);  
   }  
  
   void FillBindingsAndSetupRowBuffer(DBBINDING* pBindingsEmployees) {  
      for (DBORDINAL i = 0; i < 4; i++) {  
         pBindingsEmployees[i].pTypeInfo = NULL;  
         pBindingsEmployees[i].pObject = NULL;  
         pBindingsEmployees[i].pBindExt = NULL;  
         pBindingsEmployees[i].eParamIO = DBPARAMIO_NOTPARAM;  
         pBindingsEmployees[i].iOrdinal = i + 1;  
         pBindingsEmployees[i].dwPart = DBPART_LENGTH | DBPART_VALUE | DBPART_STATUS;  
         pBindingsEmployees[i].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
         pBindingsEmployees[i].dwFlags = 0;  
      }  
  
      pBindingsEmployees[0].wType = DBTYPE_WSTR;  
      pBindingsEmployees[0].cbMaxLen = (50 + 1) * sizeof(wchar_t);  
      pBindingsEmployees[0].obLength = offsetof(EmployeeData, nameLength);  
      pBindingsEmployees[0].obStatus = offsetof(EmployeeData, nameStatus);  
      pBindingsEmployees[0].obValue = offsetof(EmployeeData, nameValue);  
      pBindingsEmployees[1].wType = DBTYPE_DBDATE;  
      pBindingsEmployees[1].cbMaxLen = sizeof(DBDATE);  
      pBindingsEmployees[1].obLength = offsetof(EmployeeData, birthdayLength);  
      pBindingsEmployees[1].obStatus = offsetof(EmployeeData, birthdayStatus);  
      pBindingsEmployees[1].obValue = offsetof(EmployeeData, birthdayValue);  
      pBindingsEmployees[2].wType = DBTYPE_I4;  
      pBindingsEmployees[2].cbMaxLen = sizeof(long);  
      pBindingsEmployees[2].obLength = offsetof(EmployeeData, salaryLength);  
      pBindingsEmployees[2].obStatus = offsetof(EmployeeData, salaryStatus);  
      pBindingsEmployees[2].obValue = offsetof(EmployeeData, salaryValue);  
      pBindingsEmployees[3].wType = DBTYPE_IUNKNOWN;  
      pBindingsEmployees[3].cbMaxLen = sizeof(IUnknown*);  
      pBindingsEmployees[3].obLength = offsetof(EmployeeData, photographLength);  
      pBindingsEmployees[3].obStatus = offsetof(EmployeeData, photographStatus);  
      pBindingsEmployees[3].obValue = offsetof(EmployeeData, photographValue);  
   }  
};  
  
HRESULT PopulateEmployees(IDBCreateCommand* pIDBCreateCommand, IRowset* pIRowsetEmployees) {  
   HRESULT hr = S_OK;  
  
   // Create the RPC call  
   ICommandText* pICommandText = NULL;  
   ISSCommandWithParameters* pISSCommandWithParameters = NULL;  
   IAccessor* pIAccessorCmd = NULL;  
   HACCESSOR hAccessorCmd;  
   DBBINDING bindingsCmd  [2] = {0};  
   DBBINDSTATUS bindStatusCmd[2] = {DBBINDSTATUS_OK, DBBINDSTATUS_OK};  
   DBOBJECT dbObjTVP = {STGM_READ, IID_IRowset};  
  
   hr = pIDBCreateCommand->CreateCommand(NULL, IID_ICommandText, reinterpret_cast<IUnknown**>(&pICommandText));  
   CHKHR_OLEDB_GOTO(hr, _Exit, pIDBCreateCommand, IID_IDBCreateCommand);  
  
   hr = pICommandText->SetCommandText(DBGUID_DEFAULT, L"{call insertEmployees(?, ?)}");  
   CHKHR_OLEDB_GOTO(hr, _Exit, pICommandText, IID_ICommandText);  
  
   hr = pICommandText->QueryInterface(IID_ISSCommandWithParameters, reinterpret_cast<void**>(&pISSCommandWithParameters));  
   CHKHR_GOTO_MSG(hr, _Exit, L"Failed to QI IUnknown for ISSCommandWithParameters.");  
  
   // Give the parameter information to the provider  
   const DB_UPARAMS rgParamOrdinalsEmployees[2] = {1, 2};  
   DBPARAMBINDINFO rgParamBindInfoEmployees[2] = {0};  
  
   rgParamBindInfoEmployees[0].pwszDataSourceType = L"table";  
   rgParamBindInfoEmployees[0].pwszName = L"@tvpEmployees";  
   rgParamBindInfoEmployees[0].ulParamSize = ~0UL;  
   rgParamBindInfoEmployees[0].dwFlags = DBPARAMFLAGS_ISINPUT;  
  
   rgParamBindInfoEmployees[1].pwszDataSourceType = L"DBTYPE_I4";  
   rgParamBindInfoEmployees[1].pwszName = L"@id";  
   rgParamBindInfoEmployees[1].ulParamSize = sizeof(long);  
   rgParamBindInfoEmployees[1].dwFlags = DBPARAMFLAGS_ISOUTPUT;  
  
   hr = pISSCommandWithParameters->SetParameterInfo(2, rgParamOrdinalsEmployees, rgParamBindInfoEmployees);  
   CHKHR_OLEDB_GOTO(hr, _Exit, pISSCommandWithParameters, IID_ISSCommandWithParameters);  
  
   DBPROP ssPropParam [1] = {0};  
   DBPROPSET ssPropsetParam [1];  
   SSPARAMPROPS ssParamProps [1];  
  
   ssPropParam[0].dwPropertyID = SSPROP_PARAM_TYPE_TYPENAME;  
   ssPropParam[0].vValue.vt = VT_BSTR;  
   ssPropParam[0].vValue.bstrVal = SysAllocString(L"tvpEmployees");  
  
   ssPropsetParam[0].cProperties = 1;  
   ssPropsetParam[0].guidPropertySet = DBPROPSET_SQLSERVERPARAMETER;  
   ssPropsetParam[0].rgProperties = ssPropParam;  
  
   ssParamProps[0].cPropertySets = 1;  
   ssParamProps[0].iOrdinal = 1;  
   ssParamProps[0].rgPropertySets = ssPropsetParam;  
  
   hr = pISSCommandWithParameters->SetParameterProperties(1, ssParamProps);  
   SysFreeString(ssPropParam[0].vValue.bstrVal);  
   CHKHR_OLEDB_GOTO(hr, _Exit, pISSCommandWithParameters, IID_ISSCommandWithParameters);  
  
   struct PARAMDATA {  
      DBLENGTH employeesLength;  
      DBSTATUS employeesStatus;  
      IUnknown * employeesValue;  
      DBLENGTH idLength;  
      DBSTATUS idStatus;  
      long idValue;  
   };  
  
   PARAMDATA cmdParamData;  
  
   hr = pICommandText->QueryInterface(IID_IAccessor, reinterpret_cast<void**>(&pIAccessorCmd));  
   CHKHR_GOTO_MSG(hr, _Exit, L"Failed to QI IUnknown for IAccessor.");  
  
   // Define the binding information  
   bindingsCmd[0].wType = DBTYPE_TABLE;  
   bindingsCmd[0].cbMaxLen = sizeof(IUnknown*);  
   bindingsCmd[0].pObject = &dbObjTVP;  
   bindingsCmd[0].eParamIO = DBPARAMIO_INPUT;  
   bindingsCmd[0].iOrdinal = 1;  
   bindingsCmd[0].dwPart = DBPART_LENGTH | DBPART_VALUE | DBPART_STATUS;  
   bindingsCmd[0].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
   bindingsCmd[0].obLength = offsetof(PARAMDATA, employeesLength);  
   bindingsCmd[0].obStatus = offsetof(PARAMDATA, employeesStatus);  
   bindingsCmd[0].obValue = offsetof(PARAMDATA, employeesValue);  
   bindingsCmd[1].wType = DBTYPE_I4;  
   bindingsCmd[1].cbMaxLen = sizeof(long);  
   bindingsCmd[1].pObject = NULL;  
   bindingsCmd[1].eParamIO = DBPARAMIO_OUTPUT;  
   bindingsCmd[1].iOrdinal = 2;  
   bindingsCmd[1].dwPart = DBPART_LENGTH | DBPART_VALUE | DBPART_STATUS;  
   bindingsCmd[1].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
   bindingsCmd[1].obLength = offsetof(PARAMDATA, idLength);  
   bindingsCmd[1].obStatus = offsetof(PARAMDATA, idStatus);  
   bindingsCmd[1].obValue = offsetof(PARAMDATA, idValue);  
  
   hr = pIAccessorCmd->CreateAccessor(  
      DBACCESSOR_PARAMETERDATA,   
      2,   
      bindingsCmd,   
      sizeof(PARAMDATA),   
      &hAccessorCmd,   
      bindStatusCmd);  
   CHKHR_OLEDB_GOTO(hr, _Exit, pIAccessorCmd, IID_IAccessor);  
  
   // Fill cmdParamData with parameter values  
   cmdParamData.employeesLength = sizeof(IUnknown*);  
   cmdParamData.employeesStatus = DBSTATUS_S_OK;  
   cmdParamData.employeesValue = pIRowsetEmployees;  
   cmdParamData.idLength = sizeof(long);  
   cmdParamData.idStatus = DBSTATUS_S_OK;  
   cmdParamData.idValue = 0;  
  
   // Execute the command  
   DBPARAMS cmdParams;  
   cmdParams.cParamSets = 1;  
   cmdParams.hAccessor = hAccessorCmd;  
   cmdParams.pData = &cmdParamData;  
  
   hr = pICommandText->Execute(NULL, IID_NULL, &cmdParams, NULL, NULL);  
   CHKHR_OLEDB_GOTO(hr, _Exit, pICommandText, IID_ICommandText);  
  
   wprintf(L"Employee table population completed. ID : %d.\n", cmdParamData.idValue);  
  
_Exit:  
   Release(&pIAccessorCmd);  
   Release(&pISSCommandWithParameters);  
   Release(&pICommandText);  
  
   return hr;  
}  
  
int main() {  
   HRESULT hr = S_OK;  
  
   CCOMLoader comLoader;  
   CMSOLEDBSQLDataSource dso;  
  
   IOpenRowset*pIOpenRowset = NULL;  
   IDBCreateCommand* pIDBCreateCommand = NULL;  
   IRowset* pIRowsetEmployees = NULL;  
  
   hr = comLoader.Load();  
   CHKHR_GOTO_MSG(hr, _Exit, L"Unable to Load COM.");  
  
   hr = dso.Connect(L"localhost", L"testdb");  
   CHKHR_GOTO(hr, _Exit);  
  
   hr = dso.GetSession(&pIOpenRowset);  
   CHKHR_GOTO(hr, _Exit);  
  
   hr = pIOpenRowset->QueryInterface(IID_IDBCreateCommand, reinterpret_cast<void**>(&pIDBCreateCommand));  
   CHKHR_GOTO_MSG(hr, _Exit, L"Failed to QI for IDBCreateCommand.");  
  
   EmployeesRowset* pEmployeesRowset = new EmployeesRowset(20);  
   if (pEmployeesRowset == NULL) {  
      hr = E_OUTOFMEMORY;  
      CHKHR_GOTO_MSG(hr, _Exit, L"Out of memory.");  
   }  
  
   // Do an extra AddRef. This IUnknown will be automatically released by the command execution code  
   pEmployeesRowset->AddRef();  
  
   hr = pEmployeesRowset->Initialize(pIOpenRowset);  
   CHKHR_GOTO(hr, _Exit);  
  
   hr = pEmployeesRowset->QueryInterface(IID_IRowset, reinterpret_cast<void**>(&pIRowsetEmployees));  
   CHKHR_GOTO_MSG(hr, _Exit, L"Failed to QI IRowset for Employees Rowset.");  
  
   hr = PopulateEmployees(pIDBCreateCommand, pIRowsetEmployees);  
   CHKHR_GOTO(hr, _Exit);  
  
_Exit:  
   Release(&pIRowsetEmployees);  
   Release(&pIDBCreateCommand);  
   Release(&pIOpenRowset);  
   return SUCCEEDED(hr) ? EXIT_SUCCESS : EXIT_FAILURE;  
}  
```  
  
```  
use master  
IF EXISTS (SELECT name FROM master..sysdatabases WHERE name = 'testdb')  
    DROP DATABASE [testdb]  
go  
```  
  
  
