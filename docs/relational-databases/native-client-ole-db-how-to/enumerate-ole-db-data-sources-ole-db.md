---
description: "Enumerate SQL Server Native Client OLE DB Data Sources (OLE DB)"
title: "Enumerate OLE DB data sources (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "data sources [OLE DB]"
ms.assetid: ba240060-3237-4fb8-b2fb-b87fda2b1e7a
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Enumerate SQL Server Native Client OLE DB Data Sources (OLE DB)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  This sample shows how to use the enumerator object to list the data sources available.  
  
 To list the data sources visible to the SQLOLEDB enumerator, the consumer calls the [ISourcesRowset::GetSourcesRowset](/previous-versions/windows/desktop/ms711200(v=vs.85)) method. This method returns a rowset of information about the currently visible data sources.  
  
 Depending on the network library used, the appropriate domain is searched for the data sources. For named pipes, it is the domain to which the client is logged on. For AppleTalk, it is the default zone. For SPX/IPX, it is the list of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installations found in the bindery. For Banyan VINES, it is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installations found on the local network. Multiprotocol and TCP/IP sockets are not supported.  
  
 When the server is turned off or on, it can take few minutes to update the information in these domains.  
  
 This sample requires the AdventureWorks sample database, which you can download from the [Microsoft SQL Server Samples and Community Projects](https://go.microsoft.com/fwlink/?LinkID=85384) home page.  
  
> [!IMPORTANT]  
>  When possible, use Windows Authentication. If Windows Authentication is not available, prompt users to enter their credentials at run time. Avoid storing credentials in a file. If you must persist credentials, you should encrypt them with the [Win32 crypto API](/windows/win32/seccrypto/cryptography-reference).  
  
### To enumerate OLE DB data sources  
  
1.  Retrieve the source rowset by calling **ISourceRowset::GetSourcesRowset**.  
  
2.  Find the description of the enumerators rowset by calling **GetColumnInfo::IColumnInfo**.  
  
3.  Create the binding structures from the column information.  
  
4.  Create the rowset accessor by calling **IAccessor::CreateAccessor**.  
  
5.  Fetch the rows by calling **IRowset::GetNextRows**.  
  
6.  Retrieve data from the rowset's copy of the row by calling **IRowset::GetData**, and process it.  
  
## Example  
 Compile with ole32.lib and execute the following C++ code listing. This application connects to your computer's default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. On some Windows operating systems, you will need to change (localhost) or (local) to the name of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. To connect to a named instance, change the connection string from L"(local)" to L"(local)\\\name", where name is the named instance. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express installs to a named instance. Make sure your INCLUDE environment variable includes the directory that contains sqlncli.h.  
  
```  
// compile with: ole32.lib  
#define UNICODE  
#define _UNICODE  
#define DBINITCONSTANTS  
#define INITGUID  
#define OLEDBVER 0x0250   // to include correct interfaces  
  
#include <windows.h>  
#include <stddef.h>  
#include <oledb.h>  
#include <oledberr.h>  
#include <sqlncli.h>  
#include <stdio.h>  
  
#define NUMROWS_CHUNK  5  
  
// AdjustLen supports binding on four-byte boundaries.  
_inline DBLENGTH AdjustLen(DBLENGTH cb) {   
   return ( (cb + 3) & ~3 );  
}  
  
// Get the characteristics of the rowset (the IColumnsInfo interface).  
HRESULT GetColumnInfo ( IRowset* pIRowset,   
                        DBORDINAL* pnCols,   
                        DBCOLUMNINFO** ppColumnsInfo,  
                        OLECHAR** ppColumnStrings ) {  
   IColumnsInfo* pIColumnsInfo;  
   HRESULT hr;  
  
   *pnCols = 0;  
   if (FAILED(pIRowset->QueryInterface(IID_IColumnsInfo, (void**) &pIColumnsInfo)))  
      return (E_FAIL);  
  
   hr = pIColumnsInfo->GetColumnInfo(pnCols, ppColumnsInfo, ppColumnStrings);  
  
   if (FAILED(hr)) {}   /* Process error */   
  
   pIColumnsInfo->Release();  
   return (hr);  
}  
  
// Create binding structures from column information. Binding structures  
// will be used to create an accessor that allows row value retrieval.  
void CreateDBBindings ( DBORDINAL nCols,   
                        DBCOLUMNINFO* pColumnsInfo,   
                        DBBINDING** ppDBBindings,  
                        BYTE** ppRowValues ) {  
   ULONG nCol;  
   DBLENGTH cbRow = 0;  
   DBLENGTH cbCol;  
   DBBINDING* pDBBindings;  
   BYTE* pRowValues;  
  
   pDBBindings = new DBBINDING[nCols];  
   if (!(pDBBindings /* = new DBBINDING[nCols] */ ))  
      return;  
  
   for ( nCol = 0 ; nCol < nCols ; nCol++ ) {  
      pDBBindings[nCol].iOrdinal = nCol + 1;  
      pDBBindings[nCol].pTypeInfo = NULL;  
      pDBBindings[nCol].pObject = NULL;  
      pDBBindings[nCol].pBindExt = NULL;  
      pDBBindings[nCol].dwPart = DBPART_VALUE;  
      pDBBindings[nCol].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
      pDBBindings[nCol].eParamIO = DBPARAMIO_NOTPARAM;  
      pDBBindings[nCol].dwFlags = 0;  
      pDBBindings[nCol].wType = pColumnsInfo[nCol].wType;  
      pDBBindings[nCol].bPrecision = pColumnsInfo[nCol].bPrecision;  
      pDBBindings[nCol].bScale = pColumnsInfo[nCol].bScale;  
  
      cbCol = pColumnsInfo[nCol].ulColumnSize;  
  
      switch (pColumnsInfo[nCol].wType) {  
      case DBTYPE_STR: {  
            cbCol += 1;  
            break;  
         }  
  
      case DBTYPE_WSTR: {  
            cbCol = (cbCol + 1) * sizeof(WCHAR);  
            break;  
         }  
  
      default:  
         break;  
      }  
  
      pDBBindings[nCol].obValue = cbRow;  
      pDBBindings[nCol].cbMaxLen = cbCol;  
      cbRow += AdjustLen(cbCol);  
   }  
  
   pRowValues = new BYTE[cbRow];  
   *ppDBBindings = pDBBindings;  
   *ppRowValues = pRowValues;  
}  
  
int main() {  
   ISourcesRowset* pISourceRowset = NULL;      
   IRowset* pIRowset = NULL;          
   IAccessor* pIAccessor = NULL;  
   DBBINDING* pDBBindings = NULL;              
  
   HROW* pRows = new HROW[500];      
   HACCESSOR hAccessorRetrieve = NULL;          
   ULONG DSSeqNumber = 0;  
  
   HRESULT hr;  
   DBORDINAL nCols;  
   DBCOLUMNINFO* pColumnsInfo = NULL;  
   OLECHAR* pColumnStrings = NULL;  
   DBBINDSTATUS* pDBBindStatus = NULL;  
  
   BYTE* pRowValues = NULL;  
   DBCOUNTITEM cRowsObtained;  
   ULONG iRow;  
   char* pMultiByte = NULL;  
   short* psSourceType = NULL;  
   BYTE* pDatasource = NULL;  
  
   if (!pRows)  
      return (0);  
  
   // Initialize COM library.  
   CoInitialize(NULL);  
  
   // Initialize the enumerator.  
   if (FAILED(CoCreateInstance(CLSID_SQLNCLI11_ENUMERATOR,   
                               NULL,  
                               CLSCTX_INPROC_SERVER,   
                               IID_ISourcesRowset,   
                               (void**)&pISourceRowset))) {  
      // Process error.  
      return TRUE;  
   }  
  
   // Retrieve the source rowset.  
   hr = pISourceRowset->GetSourcesRowset(NULL, IID_IRowset, 0, NULL, (IUnknown**)&pIRowset);  
  
   pISourceRowset->Release();  
   if (FAILED(hr)) {  
      // Process error.  
      return TRUE;  
   }  
  
   // Get the description of the enumerator's rowset.  
   if (FAILED(hr = GetColumnInfo(pIRowset, &nCols, &pColumnsInfo, &pColumnStrings))) {  
      // Process error.  
      goto SAFE_EXIT;  
   }  
  
   // Create the binding structures.  
   CreateDBBindings(nCols, pColumnsInfo, &pDBBindings, &pRowValues);  
   pDBBindStatus = new DBBINDSTATUS[nCols];  
  
   if (sizeof(TCHAR) != sizeof(WCHAR))  
      pMultiByte = new char[pDBBindings[0].cbMaxLen];  
  
   if (FAILED(pIRowset->QueryInterface(IID_IAccessor, (void**)&pIAccessor))) {  
      // Process error.  
      goto SAFE_EXIT;  
   }  
  
   // Create the rowset accessor.  
   if (FAILED(hr = pIAccessor->CreateAccessor(DBACCESSOR_ROWDATA,   
                                              nCols,  
                                              pDBBindings,   
                                              0,   
                                              &hAccessorRetrieve,   
                                              pDBBindStatus))) {  
      // Process error.  
      goto SAFE_EXIT;  
   }  
  
   // Process all the rows, NUMROWS_CHUNK rows at a time.  
   while (SUCCEEDED(hr)) {  
      hr = pIRowset->GetNextRows(NULL, 0, NUMROWS_CHUNK, &cRowsObtained, &pRows);  
      if( FAILED(hr)) {  
         // process error  
      }  
      if (cRowsObtained == 0 || FAILED(hr))  
         break;  
  
      for (iRow = 0 ; iRow < cRowsObtained ; iRow++) {  
         // Get the rowset data.  
         if (SUCCEEDED(hr = pIRowset->GetData(pRows[iRow], hAccessorRetrieve, pRowValues))) {  
            psSourceType = (short *)(pRowValues + pDBBindings[3].obValue);  
  
            if (*psSourceType == DBSOURCETYPE_DATASOURCE) {  
               DSSeqNumber = DSSeqNumber + 1;   // Data source counter.  
               pDatasource = (pRowValues + pDBBindings[0].obValue);  
  
               if ( sizeof(TCHAR) != sizeof(WCHAR) ) {  
                  WideCharToMultiByte(CP_ACP,   
                                      0,  
                                      (WCHAR*)pDatasource,   
                                      -1,   
                                      pMultiByte,  
                                      static_cast<int>(pDBBindings[0].cbMaxLen),   
                                      NULL,   
                                      NULL);  
  
                  printf( "DataSource# %d\tName: %S\n",   
                          DSSeqNumber,   
                          (WCHAR *) pMultiByte );  
               }  
               else {  
                  printf( "DataSource# %d\tName: %S\n",   
                          DSSeqNumber,   
                          (WCHAR *) pDatasource );  
               }  
            }  
         }  
      }  
      pIRowset->ReleaseRows(cRowsObtained, pRows, NULL, NULL, NULL);  
   }  
  
   // Release COM library.  
   CoUninitialize();  
  
SAFE_EXIT:  
   // Do the clean-up.  
   return TRUE;  
}  
```  
  
