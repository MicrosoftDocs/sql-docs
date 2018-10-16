---
title: "Retrieve Data from a FILESTREAM Column Using ISequentialStream (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 814cb31e-6fd1-4eb7-afe3-25b520638815
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Retrieve Data from a FILESTREAM Column Using ISequentialStream (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  This sample shows uses an ISequentialStream interface in an ICommandText interface to retrieve a single record from a Filestream column.  
  
 For more information on the filestream feature, see [FILESTREAM Support &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/filestream-support-ole-db.md).  
  
## Example  
 Make sure your INCLUDE environment variable includes the directory that contains sqlncli.h.  
  
 Use one of the following samples to create the table that this sample reads from:  
  
-   [Send Data to a FILESTREAM Column Using ISequentialStream Bound to ICommandText Parameter &#40;OLE DB&#41;](../../../relational-databases/native-client-ole-db-how-to/filestream/send-data-to-filestream-isequentialstream-bound-to-icommandtext.md)  
  
-   [Send Data to a FILESTREAM Column Using IRowsetFastUpload &#40;OLE DB&#41;](../../../relational-databases/native-client-ole-db-how-to/filestream/send-data-to-a-filestream-column-using-irowsetfastupload-ole-db.md)  
  
 Copy the first code listing and paste it into a file called ISSHelper.h.  
  
 Copy the second code listing and paste it into a file called ISSHelper.cpp.  
  
 Copy the third code listing and paste it into a file called ICommandDownload.cpp.  
  
 Compile ICommandDownload.cpp, ISSHelper.cpp, ole32.lib, and oleaut32.lib.  
  
 When this sample is run, you must pass a server name (or server\instance_name).  
  
```  
// ISSHelper.h: interface for the CISSHelper class.  
  
#if !defined(AFX_ISSHELPER_H__7B88E5F3_263F_11D2_9D1F_00C04F96B8B2__INCLUDED_)  
#define AFX_ISSHELPER_H__7B88E5F3_263F_11D2_9D1F_00C04F96B8B2__INCLUDED_  
  
#if _MSC_VER > 1000  
#pragma once  
#endif // _MSC_VER > 1000  
  
class CISSHelper : public ISequentialStream {  
public:  
  
   // Constructor/destructor.  
   CISSHelper(__int64 i64LobBytes);  
   virtual ~CISSHelper();  
  
   // Helper function to clean up memory.  
   virtual void Clear();  
  
   // ISequentialStream interface implementation.  
   STDMETHODIMP_(ULONG)AddRef(void);  
   STDMETHODIMP_(ULONG)Release(void);  
   STDMETHODIMP QueryInterface(REFIID riid, LPVOID *ppv);  
   STDMETHODIMP Read(   
      /* [out] */ void __RPC_FAR *pv,  
      /* [in] */ ULONG cb,  
      /* [out] */ ULONG __RPC_FAR *pcbRead);  
   STDMETHODIMP Write(   
      /* [in] */ const void __RPC_FAR *pv,  
      /* [in] */ ULONG cb,  
      /* [out] */ ULONG __RPC_FAR *pcbWritten);  
  
public:  
   void * m_pBuffer;   // Buffer  
   ULONG m_ulLength;   // Total buffer size.  
   ULONG m_ulStatus;   // Column status.  
  
   __int64 m_i64BytesUploaded;  
   __int64 m_i64LastPrintUploaded;  
   __int64 m_i64ThreshHold;  
   DWORD m_dwStartTick;  
   DWORD m_dwParamOrdinal;  
  
private:  
   ULONG m_cRef;  // Reference count (not used).  
   ULONG m_iReadPos;   // Current index position for reading from the buffer.  
   ULONG m_iWritePos;   // Current index position for writing to the buffer.  
};  
  
#endif // !defined(AFX_ISSHELPER_H__7B88E5F3_263F_11D2_9D1F_00C04F96B8B2__INCLUDED_)  
```  
  
```  
// ISSHelper.cpp: implementation of the CISSHelper class.  
#pragma once  
  
#define WIN32_LEAN_AND_MEAN// Exclude rarely-used stuff from Windows headers  
#include <windows.h>  
#include <stdio.h>// printf(...)  
#include <stddef.h>// offsetof(...)  
#include <conio.h>// _getch()  
#include <oledb.h>// OLEDB  
#include <oledberr.h>// OLEDB  
#include <objbase.h>// CoInitializeEx()  
#include <msdasc.h>// IDataInitialize   
#include <msdadc.h>// OLEDB conversion library header.  
#include <msdaguid.h>// OLEDB conversion library guids.  
#include <tchar.h>  
  
// TODO: reference additional headers your program requires here  
#define _SQLNCLI_OLEDB_ 1  
#include <sqlncli.h>  
  
#include "ISSHelper.h"  
  
#ifdef _DEBUG  
#undef THIS_FILE  
static char THIS_FILE[]=__FILE__;  
#define new DEBUm_NEW  
#endif  
  
// Implementation of ISequentialStream interface  
CISSHelper::CISSHelper(__int64 i64LobBytes) {  
   m_cRef= 0;  
   m_pBuffer= NULL;  
   m_ulLength= 0;  
   m_ulStatus   = DBSTATUS_S_OK;  
   m_iReadPos= 0;  
   m_iWritePos= 0;  
   m_i64ThreshHold = i64LobBytes;  
   m_i64BytesUploaded = 0;  
   m_i64LastPrintUploaded = 0;  
   m_dwStartTick = 0;  
   m_dwParamOrdinal = 0;  
}  
  
CISSHelper::~CISSHelper() {  
   Clear();  
}  
  
void CISSHelper::Clear() {  
   CoTaskMemFree( m_pBuffer );  
   m_cRef= 0;  
   m_pBuffer= NULL;  
   m_ulLength= 0;  
   m_ulStatus  = DBSTATUS_S_OK;  
   m_iReadPos= 0;  
   m_iWritePos= 0;  
}  
  
ULONG CISSHelper::AddRef() {  
   return ++m_cRef;  
}  
  
ULONG CISSHelper::Release() {  
   return --m_cRef;  
}  
  
HRESULT CISSHelper::QueryInterface( REFIID riid, void** ppv ) {  
   *ppv = NULL;  
   if ( riid == IID_IUnknown ) *ppv = this;  
   if ( riid == IID_ISequentialStream ) *ppv = this;  
   if ( *ppv )  
   {  
      ( (IUnknown*) *ppv )->AddRef();  
      return S_OK;  
   }  
   return E_NOINTERFACE;  
}  
  
HRESULT CISSHelper::Read( void *pv,ULONG cb, ULONG* pcbRead ) {  
   // Check parameters.  
   if ( pcbRead ) *pcbRead = 0;  
   if ( !pv ) return STG_E_INVALIDPOINTER;  
   if ( 0 == cb ) return S_OK;   
  
   // Cut out now if threshold is hit.  
   __int64 left = m_i64ThreshHold-m_i64BytesUploaded;  
   if (left < cb)  
      cb = (ULONG)left;  
  
   if (0 == m_dwStartTick)  
      m_dwStartTick = GetTickCount();  
  
   m_i64BytesUploaded += cb;  
  
   if (( m_i64BytesUploaded - m_i64LastPrintUploaded ) >= (1024*1024*4)) {  
      __int64 i64Elapsed = (__int64) GetTickCount()-m_dwStartTick;  
      // __int64 i64BytesPerSecond = m_i64BytesUploaded * 1000 / (i64Elapsed > 0 ) ? i64Elapsed : 1;  
      __int64 i64BytesPerSecond = m_i64BytesUploaded * 1000 / i64Elapsed > 0  ? i64Elapsed : 1;  
      printf("Param=%lu TotalBytes=%010I64u ElapsedMS=%010I64u BytesPerSec=%010I64u\n",   
         m_dwParamOrdinal, m_i64BytesUploaded, i64Elapsed, i64BytesPerSecond );  
      m_i64LastPrintUploaded = m_i64BytesUploaded;  
   }  
  
   if ( cb == left ) {  
      __int64 i64Elapsed = (__int64) GetTickCount()-m_dwStartTick;  
      // __int64 i64BytesPerSecond = m_i64BytesUploaded * 1000 / (i64Elapsed > 0 ) ? i64Elapsed : 1;  
      __int64 i64BytesPerSecond = m_i64BytesUploaded * 1000 / i64Elapsed > 0 ? i64Elapsed : 1;  
      printf("Last read:\nParam=%lu TotalBytes=%010I64u ElapsedMS=%010I64u BytesPerSec=%010I64u\n",   
         m_dwParamOrdinal, m_i64BytesUploaded, i64Elapsed, i64BytesPerSecond );  
      m_i64LastPrintUploaded = m_i64BytesUploaded;  
   }  
  
   *pcbRead = cb;  
   return S_OK;  
}  
  
HRESULT CISSHelper::Write( const void *pv, ULONG cb, ULONG* pcbWritten ) {  
   // Check parameters.  
   if ( !pv ) return STG_E_INVALIDPOINTER;  
   if ( pcbWritten ) *pcbWritten = 0;  
   if ( 0 == cb ) return S_OK;  
  
   // Enlarge the current buffer.  
   m_ulLength += cb;  
  
   // Grow internal buffer to new size.  
   m_pBuffer = CoTaskMemRealloc( m_pBuffer, m_ulLength );  
  
   // Check for out of memory situation.  
   if ( NULL == m_pBuffer ) {  
      Clear();  
      return E_OUTOFMEMORY;  
   }  
  
   // Copy callers memory to internal bufffer and update write position.  
   memcpy( (void*)((BYTE*)m_pBuffer + m_iWritePos), pv, cb );  
   m_iWritePos += cb;  
  
   // Return bytes written to caller.  
   if ( pcbWritten ) *pcbWritten = cb;  
  
   return S_OK;  
}  
```  
  
```  
//  ICommandDownload.cpp  
#pragma once  
  
#define WIN32_LEAN_AND_MEAN// Exclude rarely-used stuff from Windows headers  
#include <windows.h>  
#include <stdio.h>// printf(...)  
#include <stddef.h>// offsetof(...)  
#include <conio.h>// _getch()  
#include <oledb.h>// OLEDB  
#include <oledberr.h>// OLEDB  
#include <objbase.h>// CoInitializeEx()  
#include <msdasc.h>// IDataInitialize   
#include <msdadc.h>// OLEDB conversion library header.  
#include <msdaguid.h>// OLEDB conversion library guids.  
#include <tchar.h>  
  
// TODO: reference additional headers your program requires here  
#define _SQLNCLI_OLEDB_ 1  
#include <sqlncli.h>  
  
#include "ISSHelper.h"  
  
// Simple data record used by DumpRowset function.  
#define MAX_DATA_LENGTH 8096  
struct DATA_REC {  
   DWORD status;  
   DWORD length;  
   BYTE Data[MAX_DATA_LENGTH];  
};  
  
// Helper macros for COM/OLEDB.  
#define SAFE_RELEASE(x)    { if (x) { x->Release(); x = NULL; } }  
#define SAFE_SYSFREE(bstr) { if (bstr) { ::SysFreeString(bstr); bstr = NULL; } }  
#define CHECK_HR(hr,jump)  { if ( FAILED(hr) ) goto jump; }  
#define CHECK_HR_ERR(hr,jump)  { if ( FAILED(hr) ) { DumpErrorRecords(); goto jump; } }  
  
#define CLEAR_PROPS(x) { for ( int i = 0 ; i < x ; i++ ) VariantClear( &InitProperties[i].vValue ); }  
  
#define SET_BSTR_PROP(index,propid,value) { \  
   InitProperties[index].dwOptions = DBPROPOPTIONS_REQUIRED; \  
   InitProperties[index].colid = DB_NULLID; \  
   InitProperties[index].dwStatus = DBPROPSTATUS_OK; \  
   InitProperties[index].dwPropertyID = propid; \  
   InitProperties[index].vValue.vt = VT_BSTR; \  
   InitProperties[index].vValue.bstrVal = SysAllocString(value); }  
  
// Helper function for DumpErrorRecords() to convert a clisd to string representation of clsid.  
LPWSTR CLSIDToString( CLSID* pclsid ) {  
   static WCHAR wszCLSID[1024];  
   if ( NULL == pclsid )  
      wcscpy_s(wszCLSID, 1024, L"(Null)");  
   else  
      ::StringFromGUID2( *pclsid, (OLECHAR*)wszCLSID, sizeof(wszCLSID) );  
   return wszCLSID;  
}  
  
// Helper function to dump out OLE DB errors to console window.  
HRESULT DumpErrorRecords() {  
   HRESULT hr = S_OK;  
   IErrorRecords* pIErrorRecords = NULL;  
   IErrorInfo* pIErrorInfo  = NULL;  
   IErrorInfo* pIErrorInfoRecord = NULL;  
   BSTR bstrDescription  = NULL;  
   BSTR bstrSource  = NULL;  
   ULONG i;  
   ULONG cRecords = 0;  
   LCID lcid = 0;  
   ERRORINFO ErrorInfo;  
  
   // Get IErrorInfo pointer from OLE.  
   hr = ::GetErrorInfo( 0, &pIErrorInfo );  
   if ( FAILED(hr) ) {  
      printf( "No error records found.\n" );  
      goto DumpErrorRecordsExit;  
   }  
  
   // QI for IID_IErrorRecords.  
   hr = pIErrorInfo->QueryInterface( IID_IErrorRecords, (void**)&pIErrorRecords );  
   if ( FAILED(hr) ) {  
      printf( "pIErrorInfo->QueryInterface(IID_IErrorRecords) failed with hr=0x%08x.\n", hr );  
      goto DumpErrorRecordsExit;  
   }  
  
   // Get error record count.  
   hr = pIErrorRecords->GetRecordCount( &cRecords );  
   if ( FAILED(hr) ) {  
      printf( "pIErrorRecords->GetRecordCount() failed with hr=0x%08x.\n", hr );  
      goto DumpErrorRecordsExit;  
   }  
  
   // Get system LCID  
   lcid = GetSystemDefaultLCID();   
  
   //Loop through the error records.  
   for ( i = 0 ; i < cRecords ; i++ ) {  
      // Get pIErrorInfo from pIErrorRecords.  
      hr = pIErrorRecords->GetErrorInfo( i, lcid, &pIErrorInfoRecord );  
  
      if ( SUCCEEDED(hr) ) {  
         // Get error description and source.  
         hr = pIErrorInfoRecord->GetDescription( &bstrDescription );  
         hr = pIErrorInfoRecord->GetSource( &bstrSource );  
  
         // Retrieve the ErrorInfo structures.  
         hr = pIErrorRecords->GetBasicErrorInfo( i, &ErrorInfo );  
  
         // Dump out error.  
         printf( "\n" );  
         printf( "Dumping error record %lu of %lu:\n", i+1, cRecords );  
         printf( "--------------------------------------------------------------------------\n" );  
         printf( "   Description = %S\n", bstrDescription );  
         printf( "   Source      = %S\n", bstrSource );  
         printf( "   ERRORINFO.hrError = 0x%08x\n", ErrorInfo.hrError );  
         printf( "   ERRORINFO.dwMinor = %lu\n",ErrorInfo.dwMinor );   
         printf( "   ERRORINFO.clsid   = %S\n",CLSIDToString( &ErrorInfo.clsid ) );  
  
         SAFE_RELEASE(pIErrorInfoRecord);  
         SAFE_SYSFREE(bstrDescription);  
         SAFE_SYSFREE(bstrSource);  
      }  
   }  
  
DumpErrorRecordsExit:  
   SAFE_RELEASE(pIErrorInfoRecord);  
   SAFE_RELEASE(pIErrorRecords);  
   SAFE_RELEASE(pIErrorInfo);  
   SAFE_RELEASE(pIErrorInfo);  
   SAFE_SYSFREE(bstrDescription);  
   SAFE_SYSFREE(bstrSource);  
   return hr;  
}  
  
// Helper function to execute a sql command.  
HRESULT ExecuteSQL( IDBCreateCommand* pIDBCreateCommand, LPCOLESTR pwszCommand  ) {  
   HRESULT hr;  
   ICommandText* pICommandText = NULL;  
   hr = pIDBCreateCommand->CreateCommand( NULL, IID_ICommandText, (IUnknown**)&pICommandText );  
   CHECK_HR(hr,ExecuteSQLCleanup);  
   hr = pICommandText->SetCommandText( DBGUID_DBSQL, pwszCommand );  
   CHECK_HR(hr,ExecuteSQLCleanup);  
   hr = pICommandText->Execute( NULL, IID_NULL, NULL, NULL, NULL );  
  
ExecuteSQLCleanup:  
   SAFE_RELEASE(pICommandText);  
   return hr;  
}  
  
HRESULT OpenSessionSSHelper( IDBProperties*pIDBProperties, IDBCreateCommand** ppIDBCreateCommand, wchar_t* szServer ) {  
#define TOTAL_SS_PROPS 3  
   HRESULThr;  
   IDBInitialize*pIDBInitialize = NULL;    
   IDBCreateSession*pIDBCreateSession = NULL;  
   DBPROPInitProperties[TOTAL_SS_PROPS];  
   DBPROPSETInitPropSet;  
  
   if ( NULL == pIDBProperties )       
      return E_INVALIDARG;  
  
   if ( NULL == ppIDBCreateCommand )   
      return E_INVALIDARG;  
  
   // NULL our out param in case of failure.  
   *ppIDBCreateCommand = NULL;  
  
   // Hard coded this to point to local sql server, change as needed.  
   SET_BSTR_PROP( 0, DBPROP_INIT_DATASOURCE, szServer );  
   SET_BSTR_PROP( 1, DBPROP_INIT_CATALOG, L"master" );  
   SET_BSTR_PROP( 2, DBPROP_AUTH_INTEGRATED, L"SSPI" );  
  
   InitPropSet.guidPropertySet = DBPROPSET_DBINIT;   
   InitPropSet.cProperties = TOTAL_SS_PROPS;                 
   InitPropSet.rgProperties = InitProperties;        
  
   // Set initialization properties.   
   hr = pIDBProperties->SetProperties( 1, &InitPropSet );  
   CHECK_HR(hr,OpenSessionSSHelperCleanup);  
  
   // Clean up allocated variants.  
   CLEAR_PROPS( TOTAL_SS_PROPS );  
  
   // Initialize the DataSource.  
   hr = pIDBProperties->QueryInterface( IID_IDBInitialize, (void**) &pIDBInitialize );  
   CHECK_HR(hr,OpenSessionSSHelperCleanup);  
  
   hr = pIDBInitialize->Initialize();  
   CHECK_HR(hr,OpenSessionSSHelperCleanup);  
  
   // QI for IID_IDBCreateSession and then create session.  
   hr = pIDBInitialize->QueryInterface( IID_IDBCreateSession, (void**) &pIDBCreateSession );  
   CHECK_HR(hr,OpenSessionSSHelperCleanup);  
  
   hr = pIDBCreateSession->CreateSession( NULL, IID_IDBCreateCommand, (IUnknown**)ppIDBCreateCommand );  
  
OpenSessionSSHelperCleanup:  
  
   SAFE_RELEASE( pIDBInitialize );  
   SAFE_RELEASE( pIDBCreateSession );  
   return hr;      
}  
  
// Helper function to open a SS session.  
HRESULT OpenSessionSS( IDBCreateCommand** ppIDBCreateCommand, wchar_t* szServer ) {  
   HRESULT hr;  
   IDBProperties * pIDBProperties = NULL;  
  
   // Null our out param in case we fail.  
   if ( NULL == ppIDBCreateCommand )   
      return E_INVALIDARG;  
  
   // Co-create SQLOLEDB.  
   hr = CoCreateInstance( SQLNCLI_CLSID, NULL, CLSCTX_INPROC_SERVER, IID_IDBProperties,   
      (void **) &pIDBProperties );      
   CHECK_HR(hr,OpenSessionSSCleanup);  
  
   // Call helper to open session.  
   hr = OpenSessionSSHelper( pIDBProperties, ppIDBCreateCommand, szServer );  
  
OpenSessionSSCleanup:  
   SAFE_RELEASE( pIDBProperties );  
   return hr;  
}  
  
void wmain(int argc, wchar_t* argv[]) {  
#define PARAM_COUNT 2  
   HRESULT hr;  
   IDBCreateCommand * pIDBCreateCommand = NULL;  
   ICommandText * pICommandText = NULL;  
   ICommandPrepare * pICommandPrepare = NULL;  
   IAccessor * pIAccessor = NULL;  
   IRowset * pIRowset = NULL;  
   ICommandProperties * pICommandProperties = NULL;  
  
   HACCESSOR hAccessor;  
   DBBINDING dbBind[PARAM_COUNT];  
   DBROWCOUNT rows;  
   DBPROPSET rgCmdPropSet[1];  
   DBPROP rgCmdProperties[1];  
  
   if ( argc < 2 ) {  
      wprintf( L"Usage: %s: <server>\n", argv[0] );  
      return;  
   }  
  
   CoInitialize(NULL);  
  
   // Open connection to SS database.  
   hr = OpenSessionSS( &pIDBCreateCommand, argv[1] );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   // Get ICommandText.  
   hr = pIDBCreateCommand->CreateCommand( NULL, IID_ICommandText, (IUnknown**)&pICommandText );  
   printf( "pIDBCreateCommand->CreateCommand(...) returned 0x%08x.\n", hr );  
   CHECK_HR(hr,MainCleanup);  
  
   // Prepare the parameterized execute statement.  
   hr = pICommandText->SetCommandText( DBGUID_DBSQL, L"SELECT uid, val FROM [DBFsa]..[FSTRTable]" );  
  
   // Set up parameter value binding.  
   ZeroMemory( &dbBind, sizeof(dbBind) );  
  
   struct _DATA_BLOCK {  
      long f1;  
      long f1_status;  
      long f1_length;  
      unsigned long imageData_length;  
      long imageData_status;  
      IUnknown* imageData;  
   } DATA_BLOCK;  
  
   dbBind[0].iOrdinal= 1;  
   dbBind[0].dwPart= DBPART_VALUE | DBPART_STATUS | DBPART_LENGTH;  
   dbBind[0].eParamIO= DBPARAMIO_NOTPARAM;  
   dbBind[0].obValue= offsetof( _DATA_BLOCK, f1 );  
   dbBind[0].obStatus= offsetof( _DATA_BLOCK, f1_status );  
   dbBind[0].obLength= offsetof( _DATA_BLOCK, f1_length );  
   dbBind[0].wType= DBTYPE_I4;  
   dbBind[0].cbMaxLen= sizeof(int);  
  
   dbBind[1].iOrdinal= 2;  
   dbBind[1].dwPart= DBPART_VALUE | DBPART_STATUS;  
   dbBind[1].eParamIO= DBPARAMIO_NOTPARAM;  
   dbBind[1].obValue= offsetof( _DATA_BLOCK, imageData );  
   dbBind[1].obStatus= offsetof( _DATA_BLOCK, imageData_status );  
   dbBind[1].obLength  = offsetof( _DATA_BLOCK, imageData_length );  
   dbBind[1].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
   dbBind[1].wType= DBTYPE_IUNKNOWN;  
   dbBind[1].pObject= new DBOBJECT;  
   dbBind[1].pObject->dwFlags = STGM_READ;  
   dbBind[1].pObject->iid = IID_ISequentialStream;  
   dbBind[1].cbMaxLen= 0;  
  
   hr = pICommandText->QueryInterface(IID_ICommandProperties, (void**) &pICommandProperties);  
   CHECK_HR_ERR( hr, MainCleanup );  
  
   rgCmdProperties[0].dwPropertyID = DBPROP_ACCESSORDER;  
   rgCmdProperties[0].vValue.vt = VT_I4;  
   rgCmdProperties[0].vValue.lVal = DBPROPVAL_AO_SEQUENTIAL;  
   rgCmdProperties[0].dwOptions = DBPROPOPTIONS_REQUIRED;  
   rgCmdProperties[0].colid = DB_NULLID;  
  
   rgCmdPropSet[0].guidPropertySet = DBPROPSET_ROWSET;  
   rgCmdPropSet[0].cProperties = 1;  
   rgCmdPropSet[0].rgProperties = rgCmdProperties;  
  
   hr = pICommandProperties->SetProperties(1, rgCmdPropSet);   
   CHECK_HR_ERR( hr, MainCleanup );  
  
   // Get an accessor for our binding.  
   hr = pICommandText->Execute( NULL, IID_IRowset, NULL, &rows, (IUnknown**) &pIRowset );  
   CHECK_HR_ERR(hr, MainCleanup);  
   printf( "pICommandText->Execute(...) returned 0x%08x.\n", hr );  
  
   hr = pIRowset->QueryInterface( IID_IAccessor, (void**)&pIAccessor );  
   printf( "pIRowset->QueryInterface( IID_IAccessor) returned 0x%08x.\n", hr );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   hr = pIAccessor->CreateAccessor( DBACCESSOR_ROWDATA,  
      PARAM_COUNT,   
      &dbBind[0],   
      sizeof(DATA_BLOCK),   
      &hAccessor,   
      NULL );  
   printf( "pIAccessor->CreateAccessor(...) returned 0x%08x.\n", hr );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   // Set the parameter values and status/length.  
   DATA_BLOCK.f1 = 1;  
   DATA_BLOCK.f1_status = DBSTATUS_S_OK;  
   DATA_BLOCK.f1_length = sizeof(int);  
   DATA_BLOCK.imageData = NULL;  
   DATA_BLOCK.imageData_status = DBSTATUS_S_OK;  
   DATA_BLOCK.imageData_length = 0;  
  
   HROW hRow[1];  
   HROW* pRow = hRow;  
   DBCOUNTITEM count;  
   hr = pIRowset->GetNextRows( DB_NULL_HCHAPTER, 0, 1, &count, &pRow );  
   CHECK_HR_ERR(hr, MainCleanup);  
  
   hr = pIRowset->GetData( hRow[0], hAccessor, &DATA_BLOCK );  
   CHECK_HR_ERR(hr, MainCleanup);  
  
   // Read and process 5000 bytes at a time.  
   if( DATA_BLOCK.imageData_status == DBSTATUS_S_ISNULL ) {  
      // Process NULL data.  
   }  
   else if( DATA_BLOCK.imageData_status == DBSTATUS_S_OK ) {  
  
      ISequentialStream * pISequentialStream;  
      BYTE * rgBuffer = new BYTE[1024*1024*4];  
      ULONG   cb;  
      ULONGLONG total = 0;  
  
      pISequentialStream = (ISequentialStream *) DATA_BLOCK.imageData;  
      do {  
         cb = sizeof( rgBuffer );  
         pISequentialStream->Read(rgBuffer,   // Buffer to write the data  
            1024*1024*4,   // Number of bytes to get  
            &cb);   // Number of bytes written to buffer  
         if (cb > 0) {   // Check to see if we got data  
            total += cb;  
            printf( "Received %d bytes of data, total = %08I64x\n", cb, total );  
         }  
      } while (cb >= sizeof(rgBuffer));  
  
      delete rgBuffer;  
   }  
   else {  
      printf( "%08X Problem retrieving varbinary(max)\n", DATA_BLOCK.imageData_status );  
   }  
  
MainCleanup:  
   SAFE_RELEASE( pICommandProperties );  
   SAFE_RELEASE( pIRowset );  
   SAFE_RELEASE( pIDBCreateCommand );  
   SAFE_RELEASE( pICommandText );  
   SAFE_RELEASE( pICommandPrepare );  
   SAFE_RELEASE( pIAccessor );  
  
   printf_s( "Test complete.\n" );  
}  
```  
  
  
