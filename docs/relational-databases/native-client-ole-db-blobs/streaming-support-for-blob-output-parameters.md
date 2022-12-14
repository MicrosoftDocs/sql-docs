---
description: "Streaming Support for BLOB Output Parameters in SQL Server Native Client"
title: Streaming support for BLOB output parameters (Native Client OLE DB provider)
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, BLOBs"
ms.assetid: b55fccbe-643e-42f1-bf9e-99509c4281af
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Streaming Support for BLOB Output Parameters in SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  This topic contains a sample that shows streaming support for BLOB output parameters, which binds BLOB output parameters as ISequentialStreams.  
  
 You can become blocked on output parameters in IMultipleResults::GetResult, receiving DB_E_OBJECTOPEN return value. The caller should to check for pending blob parameters, and either read them completely or release them.  
  
## Example  
  
### Code  
  
```  
// blob_out_param.cpp  
// compile with: /EHsc ole32.lib oleaut32.lib  
#define UNICODE  
#define DBINITCONSTANTS  
#define INITGUID  
#define OLEDBVER 0x0250    
#include <windows.h>  
  
// Change the following to match your server and test database,   
// and also the 'use' statement in the setup SQL that follows  
  
const WCHAR* g_wchServerName = L".";  
const WCHAR* g_wchDBName     = L"TestDB";  
const WCHAR* g_wchSecurity   = L"SSPI";  
  
/*  
// Execute the following before running the program   
use TestDB  
go  
  
drop proc Scenario1  
go  
  
drop table tScenario1  
go  
  
create table tScenario1 (c1 int, c2 varchar(20))  
go  
  
insert into tScenario1 values (1, 'abcdefgh')  
insert into tScenario1 values (2, 'ijklm')  
insert into tScenario1 values (3, 'nopqr')  
insert into tScenario1 values (4, 'stuvwxyz')  
go  
  
create proc Scenario1 @p1 int output, @p2 varchar(max) output, @p3 xml output, @p4 varchar(20) output  
as  
begin  
set @p1 = @p1 + 5  
set @p2 = replicate(convert(varchar(max), 'a'), 10000)  
set @p3 = '<tag1>Some text</tag1>'  
set @p4 = '@p4 varchar(20)'  
  
select * from tScenario1  
  
return datalength(@p2)  
end  
go  
  
------------------------- TESTING CODE -------------------------  
  
declare @v1 int  
set @v1 = 16  
declare @v2 varchar(max)  
declare @v3 xml  
declare @v4 varchar(20)  
declare @ret_val int  
exec @ret_val = Scenario1 @v1 output, @v2 output, @v3 output, @v4 output  
print @v1  
print @v2  
print convert(nchar, @v3)  
print @v4  
print @ret_val  
go  
*/  
  
#include <stdio.h>  
#include <stddef.h>  
#include <iostream>  
#include <oledb.h>  
#include <oledberr.h>  
#include <sqlncli.h>  
#include <iostream>  
#include <tchar.h>  
  
using namespace std;  
  
#define CHKHR_GOTO(hr, lblExit, szMsg) \  
   do \  
{ \  
   if (FAILED(hr)) \  
{ \  
   cout << szMsg << endl; \  
   goto lblExit; \  
} \  
} \  
   while (0)  
  
#define RELEASE_ITF(pItf) \  
   if (pItf) \  
{ \  
   pItf->Release(); \  
   pItf = NULL; \  
}   
  
#define INIT_BINDINGS(acDBBinding, nBindings) \  
   for(int uIndex = 0; uIndex < nBindings; uIndex++) { \  
   acDBBinding[uIndex].iOrdinal    = uIndex + 1; \  
   acDBBinding[uIndex].obLength    = 0; \  
   acDBBinding[uIndex].obStatus    = 0; \  
   acDBBinding[uIndex].pTypeInfo   = NULL; \  
   acDBBinding[uIndex].pObject     = NULL; \  
   acDBBinding[uIndex].pBindExt    = NULL; \  
   acDBBinding[uIndex].dwPart      = DBPART_VALUE; \  
   acDBBinding[uIndex].dwMemOwner  = DBMEMOWNER_CLIENTOWNED; \  
   acDBBinding[uIndex].dwFlags     = 0; \  
   acDBBinding[uIndex].bScale      = 0; \  
   }   
  
IDBInitialize* pIDBInitialize = NULL;  
  
HRESULT Connect(ICommandText** ppICommandText) {  
   HRESULT             hr;  
   IDBProperties*      pIDBProperties      = NULL;  
   IDBCreateSession*   pIDBCreateSession   = NULL;  
   IDBCreateCommand*   pIDBCreateCommand   = NULL;  
   ICommandText*       pICommandText       = NULL;  
   const ULONG         nInitProps          = 3;  
   const ULONG         nPropSet            = 1;  
  
   DBPROP     InitProperties[nInitProps];  
   DBPROPSET  InitPropSet [nPropSet];  
  
   *ppICommandText = NULL;  
  
   hr = CoInitialize(NULL);  
   CHKHR_GOTO(hr, _ExitConnect, "Unable to Initialize COM.");  
  
   hr = CoCreateInstance(CLSID_SQLNCLI11, NULL, CLSCTX_INPROC_SERVER, IID_IDBInitialize, (void **) &pIDBInitialize);  
   CHKHR_GOTO(hr, _ExitConnect, "Unable to Load SQLNCLI11.");  
  
   for(ULONG uIndexProp = 0; uIndexProp < nInitProps; uIndexProp++) {  
      VariantInit(&InitProperties[uIndexProp].vValue);  
      InitProperties[uIndexProp].vValue.vt = VT_BSTR;  
      InitProperties[uIndexProp].dwOptions = DBPROPOPTIONS_REQUIRED;  
      InitProperties[uIndexProp].colid     = DB_NULLID;  
   }  
  
   InitProperties[0].dwPropertyID   = DBPROP_INIT_DATASOURCE;  
   InitProperties[0].vValue.bstrVal = SysAllocString(g_wchServerName);  
   InitProperties[1].dwPropertyID   = DBPROP_INIT_CATALOG;  
   InitProperties[1].vValue.bstrVal = SysAllocString(g_wchDBName);  
   InitProperties[2].dwPropertyID   = DBPROP_AUTH_INTEGRATED;  
   InitProperties[2].vValue.bstrVal = SysAllocString(g_wchSecurity);  
  
   InitPropSet[0].guidPropertySet   = DBPROPSET_DBINIT;  
   InitPropSet[0].cProperties       = 3;  
   InitPropSet[0].rgProperties      = InitProperties;  
  
   hr = pIDBInitialize->QueryInterface(IID_IDBProperties, (void **)&pIDBProperties);  
   CHKHR_GOTO(hr, _ExitConnect, "Failed to obtain IDBProperties interface.");  
  
   hr = pIDBProperties->SetProperties(nPropSet, InitPropSet);  
   CHKHR_GOTO(hr, _ExitConnect, "Failed to Set Initialization properties.");  
  
   hr = pIDBInitialize->Initialize();  
   CHKHR_GOTO(hr, _ExitConnect, "Failed to Connect to the Server.");  
  
   //Create a new activity from the data source object.  
   hr = pIDBInitialize->QueryInterface(IID_IDBCreateSession, (void**) &pIDBCreateSession);  
   CHKHR_GOTO(hr, _ExitConnect, "Failed to obtain IDBCreateSession interface.");  
  
   hr = pIDBCreateSession->CreateSession(NULL, IID_IDBCreateCommand, (IUnknown**) &pIDBCreateCommand);  
   CHKHR_GOTO(hr, _ExitConnect, "Failed to create a new Session.");  
  
   hr = pIDBCreateCommand->CreateCommand(NULL, IID_ICommandText, (IUnknown**) &pICommandText);  
   CHKHR_GOTO(hr, _ExitConnect, "Failed to create a new Command.");  
  
   *ppICommandText = pICommandText;  
  
_ExitConnect:  
   RELEASE_ITF(pIDBCreateCommand);  
  
   for(ULONG uIndexProp = 0; uIndexProp < nInitProps; uIndexProp++)  
      VariantClear(&InitProperties[uIndexProp].vValue);  
  
   RELEASE_ITF(pIDBProperties);  
  
   return hr;  
}  
  
HRESULT DisConnect() {  
   HRESULT hr = S_OK;  
  
   if (pIDBInitialize)  
      hr = pIDBInitialize->Uninitialize();  
  
   RELEASE_ITF(pIDBInitialize);  
  
   return hr;  
}  
  
int main() {  
   HRESULT hr;  
  
   const WCHAR*    g_wchCmdString   = L"{? = call Scenario1 ( ?, ?, ?, ?)};{? = call Scenario1 ( ?, ?, ?, ?)}";  
   ICommandText*   pICommandText    = NULL;  
   IAccessor*      pIAccessor       = NULL;  
   HACCESSOR       hAccessor        = NULL;  
   IRowset*        pIRowset         = NULL;  
  
   IMultipleResults*    pIMultipleResults = NULL;  
   ISequentialStream*   pIStream          = NULL;  
  
   const ULONG nParams = 10;   
   ULONG       uIndex  = 0;  
  
   DBBINDING  acDBBinding [nParams];  
   DBBINDSTATUS  acDBBindStatus[nParams];  
  
   typedef struct tagSPROCPARAMS {  
      long        lReturnValue;  
      long        lInt;  
      IUnknown*   streamVarcharMax;  
      IUnknown*   streamXML;  
      char        chVarChar[21];  
      long        lReturnValue2;  
      long        lInt2;  
      IUnknown*   streamVarcharMax2;  
      IUnknown*   streamXML2;  
      char        chVarChar2[21];  
   } SPROCPARAMS;  
  
   SPROCPARAMS sprocparams[] = {{ 6, 17, NULL, NULL, "", 4, 20, NULL, NULL, ""},{ 5, 23, NULL, NULL, "", 7, 11, NULL, NULL, ""}};  
  
   const int    nBlobBindingIndexes   = 8;  
   IUnknown**   ppBoundBlobs[nBlobBindingIndexes];  
   int          idxBlobBindingCurrent = 0;  
  
   ppBoundBlobs[0] = &(sprocparams[0].streamVarcharMax);  
   ppBoundBlobs[1] = &(sprocparams[0].streamXML);  
   ppBoundBlobs[2] = &(sprocparams[0].streamVarcharMax2);  
   ppBoundBlobs[3] = &(sprocparams[0].streamXML2);  
   ppBoundBlobs[4] = &(sprocparams[1].streamVarcharMax);  
   ppBoundBlobs[5] = &(sprocparams[1].streamXML);  
   ppBoundBlobs[6] = &(sprocparams[1].streamVarcharMax2);  
   ppBoundBlobs[7] = &(sprocparams[1].streamXML2);  
  
   LONG      cNumRows;  
   DBPARAMS  Params;  
  
   hr = Connect(&pICommandText);  
   if (FAILED(hr))  
      goto _Exit;  
  
   hr = pICommandText->SetCommandText(DBGUID_DBSQL, g_wchCmdString);  
   CHKHR_GOTO(hr, _Exit, "Failed to Set Command Text.");  
  
   hr = pICommandText->QueryInterface(IID_IAccessor, (void**)&pIAccessor);  
   CHKHR_GOTO(hr, _Exit, "Failed to get IAccessor interface on Command object.");  
  
   INIT_BINDINGS(acDBBinding, nParams);  
  
   acDBBinding[0].obValue    = offsetof(SPROCPARAMS, lReturnValue);  
   acDBBinding[0].eParamIO   = DBPARAMIO_OUTPUT;  
   acDBBinding[0].cbMaxLen   = sizeof(sprocparams[0].lReturnValue);  
   acDBBinding[0].wType      = DBTYPE_I4;  
   acDBBinding[0].bPrecision    = 11;  
  
   acDBBinding[1].obValue    = offsetof(SPROCPARAMS, lInt);  
   acDBBinding[1].eParamIO   = DBPARAMIO_INPUT | DBPARAMIO_OUTPUT;  
   acDBBinding[1].cbMaxLen   = sizeof(sprocparams[0].lInt);  
   acDBBinding[1].wType      = DBTYPE_I4;  
   acDBBinding[1].bPrecision   = 11;  
  
   acDBBinding[2].obValue    = offsetof(SPROCPARAMS, streamVarcharMax);  
   acDBBinding[2].eParamIO   = DBPARAMIO_OUTPUT;  
   acDBBinding[2].cbMaxLen   = sizeof(sprocparams[0].streamVarcharMax);  
   acDBBinding[2].wType      = DBTYPE_IUNKNOWN;  
   acDBBinding[2].pObject    = new DBOBJECT;  
   acDBBinding[2].pObject->dwFlags = STGM_READ;  
   acDBBinding[2].pObject->iid     = IID_ISequentialStream;  
   acDBBinding[2].bPrecision       = 255;  
  
   acDBBinding[3].obValue          = offsetof(SPROCPARAMS, streamXML);  
   acDBBinding[3].eParamIO         = DBPARAMIO_OUTPUT;  
   acDBBinding[3].cbMaxLen         = sizeof(sprocparams[0].streamXML);  
   acDBBinding[3].wType            = DBTYPE_IUNKNOWN;  
   acDBBinding[3].pObject          = new DBOBJECT;  
   acDBBinding[3].pObject->dwFlags = STGM_READ;  
   acDBBinding[3].pObject->iid     = IID_ISequentialStream;  
   acDBBinding[3].bPrecision       = 255;  
  
   acDBBinding[4].obValue          = offsetof(SPROCPARAMS, chVarChar);  
   acDBBinding[4].eParamIO         = DBPARAMIO_OUTPUT;  
   acDBBinding[4].cbMaxLen         = sizeof(sprocparams[0].chVarChar);  
   acDBBinding[4].wType            = DBTYPE_STR;  
   acDBBinding[4].bPrecision       = 255;  
  
   acDBBinding[5].obValue    = offsetof(SPROCPARAMS, lReturnValue2);  
   acDBBinding[5].eParamIO   = DBPARAMIO_OUTPUT;  
   acDBBinding[5].cbMaxLen   = sizeof(sprocparams[0].lReturnValue2);  
   acDBBinding[5].wType      = DBTYPE_I4;  
   acDBBinding[5].bPrecision   = 11;  
  
   acDBBinding[6].obValue    = offsetof(SPROCPARAMS, lInt2);  
   acDBBinding[6].eParamIO   = DBPARAMIO_INPUT | DBPARAMIO_OUTPUT;  
   acDBBinding[6].cbMaxLen   = sizeof(sprocparams[0].lInt2);  
   acDBBinding[6].wType      = DBTYPE_I4;  
   acDBBinding[6].bPrecision   = 11;  
  
   acDBBinding[7].obValue   = offsetof(SPROCPARAMS, streamVarcharMax2);  
   acDBBinding[7].eParamIO  = DBPARAMIO_OUTPUT;  
   acDBBinding[7].cbMaxLen  = sizeof(sprocparams[0].streamVarcharMax2);  
   acDBBinding[7].wType     = DBTYPE_IUNKNOWN;  
   acDBBinding[7].pObject   = new DBOBJECT;  
   acDBBinding[7].pObject->dwFlags = STGM_READ;  
   acDBBinding[7].pObject->iid     = IID_ISequentialStream;  
   acDBBinding[7].bPrecision       = 255;  
  
   acDBBinding[8].obValue          = offsetof(SPROCPARAMS, streamXML2);  
   acDBBinding[8].eParamIO         = DBPARAMIO_OUTPUT;  
   acDBBinding[8].cbMaxLen         = sizeof(sprocparams[0].streamXML2);  
   acDBBinding[8].wType            = DBTYPE_IUNKNOWN;  
   acDBBinding[8].pObject          = new DBOBJECT;  
   acDBBinding[8].pObject->dwFlags = STGM_READ;  
   acDBBinding[8].pObject->iid     = IID_ISequentialStream;  
   acDBBinding[8].bPrecision       = 255;  
  
   acDBBinding[9].obValue          = offsetof(SPROCPARAMS, chVarChar2);  
   acDBBinding[9].eParamIO         = DBPARAMIO_OUTPUT;  
   acDBBinding[9].cbMaxLen         = sizeof(sprocparams[0].chVarChar2);  
   acDBBinding[9].wType            = DBTYPE_STR;  
   acDBBinding[9].bPrecision       = 255;  
  
   hr = pIAccessor->CreateAccessor(   
      DBACCESSOR_PARAMETERDATA,  
      nParams,   
      acDBBinding,   
      sizeof(SPROCPARAMS),   
      &hAccessor,  
      acDBBindStatus);  
  
   CHKHR_GOTO(hr, _Exit, "Failed to Create Accessor for the defined parameters.");  
  
   Params.pData        = &sprocparams;  
   Params.cParamSets   = sizeof(sprocparams)/sizeof(SPROCPARAMS);  
   Params.hAccessor    = hAccessor;  
  
   hr = pICommandText->Execute(NULL, IID_IMultipleResults, &Params, &cNumRows, (IUnknown**)&pIMultipleResults);  
   CHKHR_GOTO(hr, _Exit, "Failed to Execute Command.");  
  
   cout << "Command executed successfully, retrieving results." << endl;  
  
   idxBlobBindingCurrent = 0;  
  
   if (pIMultipleResults) {  
      for (;;) {  
         hr = pIMultipleResults->GetResult(NULL, 0, IID_IRowset, NULL, (IUnknown**)&pIRowset);  
  
         if (hr == DB_S_NORESULT) {  
            cout << "All results processed." << endl;  
            break;  
         }  
  
         if (FAILED(hr)) {  
            // In this case, user has open rowset or open blobs  
            if (hr == DB_E_OBJECTOPEN) {  
               IUnknown* pIUnkCurrent = *ppBoundBlobs[idxBlobBindingCurrent];  
  
               if (pIUnkCurrent) {  
                  hr = pIUnkCurrent->QueryInterface(IID_ISequentialStream, (void**)&pIStream);  
                  if (FAILED(hr)) {  
                     cout << "Failed to get a stream object. " << endl;  
                     goto _Exit;  
                  }  
  
                  RELEASE_ITF(pIUnkCurrent);  
  
                  // Display the data  
                  char    chBuff [51];  
                  wchar_t wchBuff[51];  
  
                  ULONG cbRead  = 0;  
  
                  do {  
                     if (idxBlobBindingCurrent % 2) {  
                        hr = pIStream->Read(wchBuff, 50 * sizeof(wchar_t), &cbRead);  
  
                        if (hr == E_UNEXPECTED)  
                           break;  
  
                        wchBuff[cbRead / 2] = 0;  
  
                        printf_s("%S", wchBuff[0] == 0xFEFF ? wchBuff+1 : wchBuff);  
  
                        if (hr == S_FALSE || cbRead < 50 * sizeof(wchar_t)) {  
                           cout << endl;  
                           RELEASE_ITF(pIStream);  
                           break;  
                        }  
                     }  
  
                     else {  
                        hr = pIStream->Read(chBuff, 50, &cbRead);  
  
                        if (hr == E_UNEXPECTED)  
                           break;  
  
                        chBuff[cbRead] = 0;  
  
                        printf_s("%s", chBuff);  
  
                        if (hr == S_FALSE || cbRead < 50) {  
                           cout << endl;  
                           RELEASE_ITF(pIStream);  
                           break;  
                        }  
                     }  
                  } while (SUCCEEDED(hr));  
  
                  if (FAILED(hr) && hr != E_UNEXPECTED) {  
                     cout << "Stream data read failed." << endl;  
                     goto _Exit;  
                  }  
  
                  idxBlobBindingCurrent++;  
                  continue;  
               }  
            }  
  
            else {  
               cout << "GetResult failed with unknown error." << endl;  
               break;  
            }  
         }  
  
         else {  
            if (pIRowset) {  
               DBCOUNTITEM cRowsObtained;  
               DBCOUNTITEM cRowsTotal = 0;  
               HROW hRow[1];  
               HROW* phRow = hRow;  
               do {  
                  hr = pIRowset->GetNextRows(0, 0, 1, &cRowsObtained, &phRow);  
                  cRowsTotal += cRowsObtained;  
  
                  pIRowset->ReleaseRows(cRowsObtained, hRow, NULL, NULL, NULL);  
               } while (SUCCEEDED(hr) && hr != DB_S_ENDOFROWSET);  
  
               cout << "Total number of Rows: " << cRowsTotal << endl;  
            }  
  
            RELEASE_ITF(pIRowset);     
         }  
      }  
   }  
  
   // Now display the rest of parameters  
   printf_s(" lReturnValue     : %d\n", sprocparams[0].lReturnValue);  
   printf_s(" lInt             : %d\n", sprocparams[0].lInt);  
   printf_s(" chVarChar        : %s\n", sprocparams[0].chVarChar);  
   printf_s(" lReturnValue     : %d\n", sprocparams[0].lReturnValue2);  
   printf_s(" lInt             : %d\n", sprocparams[0].lInt2);  
   printf_s(" chVarChar        : %s\n", sprocparams[0].chVarChar);  
   printf_s(" lReturnValue     : %d\n", sprocparams[1].lReturnValue);  
   printf_s(" lInt             : %d\n", sprocparams[1].lInt);  
   printf_s(" chVarChar        : %s\n", sprocparams[1].chVarChar);  
   printf_s(" lReturnValue     : %d\n", sprocparams[1].lReturnValue2);  
   printf_s(" lInt             : %d\n", sprocparams[1].lInt2);  
   printf_s(" chVarChar        : %s\n", sprocparams[1].chVarChar);  
  
_Exit:  
   RELEASE_ITF(pIStream);     
   RELEASE_ITF(pIRowset);     
   RELEASE_ITF(pIMultipleResults);     
  
   if (pIAccessor) {  
      pIAccessor->ReleaseAccessor(hAccessor, NULL);  
      RELEASE_ITF(pIAccessor);     
   }  
  
   RELEASE_ITF(pICommandText);     
  
   DisConnect();  
}  
```  
  
## See Also  
 [BLOBs and OLE Objects](../../relational-databases/native-client-ole-db-blobs/blobs-and-ole-objects.md)  
  
  
