---
title: "Display Column and Catalog Metadata for Sparse Columns (OLE DB)"
description: Learn how to display column and catalog metadata for sparse columns in OLE DB Driver for SQL Server with this example.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
---
# Display Column and Catalog Metadata for Sparse Columns (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This sample creates a table with three columns: a sparse column, a column that is not a sparse column, and a columnset column. The sample then displays OLE DB flags showing the column and catalog metadata for the non-sparse column and the columnset column.  
  
 This sample works with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] or later. For more information about sparse columns, see [Sparse Columns Support in OLE DB Driver for SQL Server](../../oledb/features/sparse-columns-support-in-oledb-driver-for-sql-server.md).  
  
## Example  
 Make sure your INCLUDE environment variable includes the directory that contains msoledbsql.h.  
  
```  
#include <stddef.h>  
#include <comdef.h>  
#include <msdasc.h>  
#include <msoledbsql.h>  
  
#define CHECKED(_hr) \  
   do { \  
      if (FAILED(hr =_hr)) { \  
         PrintHR(hr); \  
         goto ERR; \  
      } \  
   } while (0)  
  
#define CHECK_NOT_NULL(x) \  
   do { \  
      if((x) == NULL) { \  
         PrintHR(E_OUTOFMEMORY); \  
         goto ERR; \  
      } \  
   } while(0)  
  
#define SAFE_RELEASE(pI) \  
   do { \  
      if (pI) { \  
         pI->Release(); \  
         pI = 0; \  
      } \  
   } while(0)  
  
#define PROVIDER_FREE(pv) \  
   do { \  
      if (pv) { \  
         CoTaskMemFree(pv); \  
         pv = NULL; \  
      } \  
   } while(0)  
  
#define PRINT_FLAGS(f,m) \  
   do { \  
      if ((f & m) == m) \  
         wprintf(L"%ls, ", L#m); \  
   } while(0)  
  
#define PrintHR(hr) PrintHR_internal(hr, __FILE__, __LINE__)  
  
#define CHECKED3(_hr,_Obj,_IID) \  
   do { \  
      if (FAILED(hr = _hr)) { \  
         DumpErrorInfo(_Obj,_IID); \  
         goto ERR; \  
      } \  
   } while(0)  
  
void PrintHR_internal(HRESULT hr, char * szFile, ULONG uLine) {  
   wprintf(L"Error HR = %d (%x) %hs(%d)\n", hr, hr, szFile, uLine);  
}  
  
// DumpErrorInfo queries error interfaces, retrieving available status or error information.  
void DumpErrorInfo ( IUnknown* pObjectWithError, REFIID IID_InterfaceWithError ) {  
   // Interfaces used in the example.  
   IErrorInfo * pIErrorInfoAll = NULL;  
   IErrorInfo * pIErrorInfoRecord = NULL;  
   IErrorRecords * pIErrorRecords = NULL;  
   ISupportErrorInfo * pISupportErrorInfo = NULL;  
   ISQLErrorInfo * pISQLErrorInfo = NULL;  
   ISQLServerErrorInfo * pISQLServerErrorInfo = NULL;  
  
   // Number of error records.  
   ULONG nRecs;  
   ULONG nRec;  
  
   // Basic error information from GetBasicErrorInfo.  
   ERRORINFO errorinfo;  
  
   // IErrorInfo values.  
   BSTR bstrDescription;  
   BSTR bstrSource;  
  
   // ISQLErrorInfo parameters.  
   BSTR bstrSQLSTATE;  
   LONG lNativeError;  
  
   // ISQLServerErrorInfo parameter pointers.  
   SSERRORINFO * pSSErrorInfo = NULL;  
   OLECHAR * pSSErrorStrings = NULL;  
  
   // Hard-code an English (United States) locale for the example.  
   DWORD MYLOCALEID = 0x0409;  
  
   // Only ask for error information if the interface supports it.  
   if (FAILED(pObjectWithError->QueryInterface(IID_ISupportErrorInfo, (void**) &pISupportErrorInfo))) {  
      wprintf(L"SupportErrorErrorInfo interface not supported\n");  
      return;  
   }  
  
   if (FAILED(pISupportErrorInfo->InterfaceSupportsErrorInfo(IID_InterfaceWithError))) {  
      wprintf(L"InterfaceWithError interface not supported\n");  
      return;  
   }  
  
   // Do not test the return of GetErrorInfo. It can succeed and return  
   // a NULL pointer in pIErrorInfoAll. Simply test the pointer.  
   GetErrorInfo(0, &pIErrorInfoAll);  
  
   if (pIErrorInfoAll != NULL) {  
      // Test to see if it's a valid OLE DB IErrorInfo interface exposing a list of records.  
      if (SUCCEEDED(pIErrorInfoAll->QueryInterface(IID_IErrorRecords, (void**) &pIErrorRecords))) {  
         pIErrorRecords->GetRecordCount(&nRecs);  
  
         // Within each record, retrieve information from each of the defined interfaces.  
         for (nRec = 0; nRec < nRecs; nRec++) {  
            // From IErrorRecords, get the HRESULT and a reference to the ISQLErrorInfo interface.  
            pIErrorRecords->GetBasicErrorInfo(nRec, &errorinfo);  
            pIErrorRecords->GetCustomErrorObject(nRec,IID_ISQLErrorInfo, (IUnknown**) &pISQLErrorInfo);  
  
            // Display the HRESULT, then use the ISQLErrorInfo.  
            wprintf(L"HRESULT:\t%#X\n", errorinfo.hrError);  
  
            if (pISQLErrorInfo != NULL) {  
               pISQLErrorInfo->GetSQLInfo(&bstrSQLSTATE, &lNativeError);  
  
               // Display the SQLSTATE and native error values.  
               wprintf(L"SQLSTATE:\t%s\nNative Error:\t%ld\n", bstrSQLSTATE, lNativeError);  
  
               // SysFree BSTR references.  
               SysFreeString(bstrSQLSTATE);  
  
               // Get the ISQLServerErrorInfo interface from ISQLErrorInfo before releasing the reference.  
               pISQLErrorInfo->QueryInterface( IID_ISQLServerErrorInfo, (void**) &pISQLServerErrorInfo);   
  
               pISQLErrorInfo->Release();  
            }  
  
            // Test to ensure the reference is valid, then get error information from ISQLServerErrorInfo.  
            if (pISQLServerErrorInfo != NULL) {  
               pISQLServerErrorInfo->GetErrorInfo(&pSSErrorInfo, &pSSErrorStrings);  
  
               // ISQLServerErrorInfo::GetErrorInfo succeeds even when it has nothing to return.   
               // Test the pointers before using.  
               if (pSSErrorInfo) {  
                  // Display the state and severity from the returned information.   
                  // The error message comes from IErrorInfo::GetDescription.  
                  wprintf(L"Error state:\t%d\nSeverity:\t%d\n", pSSErrorInfo->bState, pSSErrorInfo->bClass);  
  
                  // IMalloc::Free needed to release references on returned values.  
                  CoTaskMemFree(pSSErrorStrings);  
                  CoTaskMemFree(pSSErrorInfo);  
               }  
  
               pISQLServerErrorInfo->Release();  
            }  
  
            if (SUCCEEDED(pIErrorRecords->GetErrorInfo(nRec, MYLOCALEID, &pIErrorInfoRecord))) {  
               // Get the source and description (error message) from the record's IErrorInfo.  
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
            }  
         }  
         pIErrorRecords->Release();  
      }  
      else {  
         // IErrorInfo is valid; get the source and description to see what it is.  
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
   }  
   else   
      wprintf(L"GetErrorInfo has not returned ErrorInfo.\n");  
  
   pISupportErrorInfo->Release();  
   return;  
}  
  
void Test_GetRowset(IDBSchemaRowset *pdbSchemaRowset, DBCOUNTITEM cExpectedRows) {  
   HRESULT hr;  
   IRowset * pRowset = NULL;  
   IAccessor * pAccessor = NULL;  
   HACCESSOR hAccessor = NULL;  
  
   struct sRowData {  
      wchar_t wszColumnName [128];  
      VARIANT_BOOL fIsSparse;  
      VARIANT_BOOL fIsColumnSet;  
   } rowData;  
  
   DBBINDING rgBindings[3] = {  
      {4, offsetof(sRowData, wszColumnName), 0, 0, 0, 0, 0,  
      DBPART_VALUE,DBMEMOWNER_CLIENTOWNED, DBPARAMIO_NOTPARAM, sizeof(rowData.wszColumnName),0, DBTYPE_WSTR, 0, 0},  
      {41, offsetof(sRowData, fIsSparse),0, 0, 0, 0, 0,  
      DBPART_VALUE,DBMEMOWNER_CLIENTOWNED, DBPARAMIO_NOTPARAM, sizeof(rowData.fIsSparse),   0, DBTYPE_BOOL, 0, 0},  
      {42, offsetof(sRowData, fIsColumnSet),0, 0, 0, 0, 0,  
      DBPART_VALUE,DBMEMOWNER_CLIENTOWNED, DBPARAMIO_NOTPARAM, sizeof(rowData.fIsColumnSet),0, DBTYPE_BOOL, 0, 0}  
   };  
  
   DBCOUNTITEM iRow;  
   BSTR bstrTable = SysAllocString(L"tbl_sparse_test");  
  
   CHECK_NOT_NULL(bstrTable);  
  
   tagVARIANT rgRestrictions[3];  
   VariantInit(rgRestrictions);  
   VariantInit(rgRestrictions+1);  
   VariantInit(rgRestrictions+2);  
   rgRestrictions[0].vt = VT_EMPTY;  
   rgRestrictions[1].vt = VT_EMPTY;  
   rgRestrictions[2].vt = VT_BSTR;  
   rgRestrictions[2].bstrVal = bstrTable;  
  
   wprintf(L"\nTesting %ls\n",  cExpectedRows == 3 ? L"DBSCHEMA_COLUMNS_EXTENDED" : L"DBSCHEMA_SPARSE_COLUMN_SET");  
  
   CHECKED(pdbSchemaRowset->GetRowset(   
      NULL,  
      cExpectedRows == 3 ?  
      DBSCHEMA_COLUMNS_EXTENDED: DBSCHEMA_SPARSE_COLUMN_SET,  
      3,  
      rgRestrictions,  
      IID_IRowset,  
      0,  
      NULL,  
      (IUnknown**)&pRowset)  
      );  
  
   DBCOUNTITEM cRowsObtained = 0;  
   HROW rgRows[3];  
   HROW * pRows = rgRows;  
  
   CHECKED(pRowset->GetNextRows( 0, 0, cExpectedRows, &cRowsObtained, &pRows));  
  
   if (cRowsObtained != cExpectedRows)   
      CHECKED(E_UNEXPECTED);  
  
   CHECKED(pRowset->QueryInterface(IID_IAccessor, (void**)&pAccessor));  
   CHECKED(pAccessor ->CreateAccessor( DBACCESSOR_ROWDATA, 3, rgBindings, sizeof(rowData), &hAccessor, NULL));  
  
   for (iRow = 0 ; iRow < cExpectedRows; iRow++) {  
      CHECKED(pRowset->GetData( rgRows[iRow], hAccessor, (void*)&rowData));  
  
      wprintf(L"Column %ls,\tIsSparse=%ls, \tIsColumnSet=%ls\n",  
         rowData.wszColumnName,  
         (rowData.fIsSparse    ? L"TRUE " : L"FALSE"),  
         (rowData.fIsColumnSet ? L"TRUE " : L"FALSE"));  
   }  
  
ERR:  
   if (bstrTable)   
      SysFreeString(bstrTable);  
   SAFE_RELEASE(pAccessor);  
   SAFE_RELEASE(pRowset);  
}  
  
int __cdecl main() {  
   HRESULT hr;  
   IDBInitialize * pIDBInit = NULL;  
   IDataInitialize * pIDI = NULL;  
   IDBCreateSession * pIDBCreateSession = NULL;  
   IDBCreateCommand * pIDBCreateCommand = NULL;  
   ICommandText * pCommand = NULL;  
   IDBSchemaRowset * pdbSchemaRowset = NULL;  
   IOpenRowset * pOpenRowset = NULL;  
   IColumnsInfo * pColumnsInfo = NULL;  
   ULONG cSchemas;  
   ULONG cFoundSchemas = 0;  
   GUID * rgSchemas = NULL;  
   ULONG * rgRestSupport = NULL;  
   DBID dbidSparse;  
   DBORDINAL cColumns, i;  
   WCHAR * pNameBuff = NULL;  
   DBCOLUMNINFO * prgColInfo = NULL;  
  
   CHECKED(CoInitializeEx(NULL, COINIT_MULTITHREADED));  
   CHECKED(CoCreateInstance(CLSID_MSDAINITIALIZE, NULL, CLSCTX_INPROC_SERVER, IID_IDataInitialize, (void**)&pIDI));  
   CHECKED(pIDI->GetDataSource(NULL, CLSCTX_INPROC_SERVER, L"Provider=MSOLEDBSQL; Integrated Security=SSPI; Data Source=.; Initial Catalog=tempdb", IID_IDBInitialize, (IUnknown**)&pIDBInit));  
   CHECKED(pIDBInit->Initialize());  
   CHECKED(pIDBInit->QueryInterface(IID_IDBCreateSession, (void**)&pIDBCreateSession));  
   CHECKED(pIDBCreateSession->CreateSession(NULL, IID_IDBCreateCommand, (IUnknown**)&pIDBCreateCommand));  
   CHECKED(pIDBCreateCommand->CreateCommand(NULL, IID_ICommandText, (IUnknown**)&pCommand));  
  
   // Drop the old table if it exists  
   CHECKED(pCommand->SetCommandText(DBGUID_DBSQL, L"if object_id('tempdb.dbo.tbl_sparse_test','U') is not null drop table tempdb.dbo.tbl_sparse_test"));  
   CHECKED(pCommand->Execute(NULL, IID_NULL, NULL, NULL, NULL));  
  
   // Create a new table  
   CHECKED(pCommand->SetCommandText(DBGUID_DBSQL, L"create table tempdb.dbo.tbl_sparse_test (col1 int SPARSE, col2 int, col3 XML column_set for all_sparse_columns)"));  
   CHECKED3(pCommand->Execute(NULL, IID_NULL, NULL, NULL, NULL), pCommand, IID_ICommandText);  
  
   // Insert a row into the table  
   CHECKED(pCommand->SetCommandText(DBGUID_DBSQL, L"insert tempdb.dbo.tbl_sparse_test (col1, col2) values (1,2)"));  
   CHECKED3(pCommand->Execute(NULL, IID_NULL, NULL, NULL, NULL), pCommand, IID_ICommandText);  
  
   // Let's check the schemas support  
   CHECKED(pIDBCreateCommand->QueryInterface(IID_IDBSchemaRowset, (void**)&pdbSchemaRowset));  
   CHECKED(pdbSchemaRowset->GetSchemas(&cSchemas, &rgSchemas, &rgRestSupport));  
  
   while (cSchemas--) {  
      if (rgSchemas[cSchemas]== DBSCHEMA_COLUMNS_EXTENDED ||  
         rgSchemas[cSchemas]== DBSCHEMA_SPARSE_COLUMN_SET) {  
         if (++cFoundSchemas == 2)  
            break;  
      }  
   }  
  
   if (cFoundSchemas != 2) {  
      wprintf(L"Found %d of expected 2 new DBSCHEMAs\n", cFoundSchemas);  
      goto ERR;  
   }  
  
   // Test the schemas:  
   Test_GetRowset(pdbSchemaRowset, 1);  
   Test_GetRowset(pdbSchemaRowset, 3);  
  
   // Test the column info  
   CHECKED(pIDBCreateCommand->QueryInterface(IID_IOpenRowset, (void**)&pOpenRowset));  
  
   dbidSparse.eKind = DBKIND_NAME;  
   dbidSparse.uName.pwszName = L"tbl_sparse_test";  
  
   // This will result in "select * from tbl_sparse_test"  
   CHECKED(pOpenRowset->OpenRowset( NULL, &dbidSparse, NULL, IID_IColumnsInfo, 0, NULL, (IUnknown**)&pColumnsInfo));  
  
   CHECKED(pColumnsInfo->GetColumnInfo(&cColumns, &prgColInfo, &pNameBuff));  
   wprintf(L"\nTesting GetColumnInfo:\n");  
  
   for ( i = 0 ; i < cColumns ; i++) {  
      wprintf(L"Column name: %ls, flags: ",prgColInfo[i].pwszName);  
      DWORD dwFlags = prgColInfo[i].dwFlags;  
      PRINT_FLAGS(dwFlags, DBCOLUMNFLAGS_ISFIXEDLENGTH);  
      PRINT_FLAGS(dwFlags, DBCOLUMNFLAGS_WRITEUNKNOWN);  
      PRINT_FLAGS(dwFlags, DBCOLUMNFLAGS_WRITE);  
      PRINT_FLAGS(dwFlags, DBCOLUMNFLAGS_MAYBENULL);  
      PRINT_FLAGS(dwFlags, DBCOLUMNFLAGS_ISLONG);  
      PRINT_FLAGS(dwFlags, DBCOLUMNFLAGS_ISNULLABLE);  
      PRINT_FLAGS(dwFlags, DBCOLUMNFLAGS_SS_ISCOLUMNSET);  
  
      wprintf(L"\n");  
   }  
  
ERR:  
   PROVIDER_FREE(prgColInfo);  
   PROVIDER_FREE(pNameBuff);  
   SAFE_RELEASE(pColumnsInfo);  
   SAFE_RELEASE(pOpenRowset);  
   PROVIDER_FREE(rgSchemas);  
   PROVIDER_FREE(rgRestSupport);  
   SAFE_RELEASE(pdbSchemaRowset);  
   SAFE_RELEASE(pCommand);  
   SAFE_RELEASE(pIDBCreateCommand);  
   SAFE_RELEASE(pIDBCreateSession);  
   SAFE_RELEASE(pIDI);  
   SAFE_RELEASE(pIDBInit);  
   CoUninitialize();  
}  
```  
  
  
