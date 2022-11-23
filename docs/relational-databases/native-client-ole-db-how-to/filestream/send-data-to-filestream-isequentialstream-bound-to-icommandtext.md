---
description: "Send Data to FILESTREAM in SQL Server Native Client - ISequentialStream Bound to ICommandText"
title: "Data FILESTREAM, ISequentialStream ICommandText"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 2225f6ab-a6cf-4c95-8291-2d2a13be7952
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Send Data to FILESTREAM in SQL Server Native Client - ISequentialStream Bound to ICommandText
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  This sample uses an ISequentialStream interface bound to an ICommandText parameter to send between 4MB and 4GB of data to a filestream column.  
  
 For more information on the filestream feature, see [FILESTREAM Support &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/filestream-support-ole-db.md).  
  
## Example  
 Before you compile and run this sample, enable FILESTREAM support ([Enable and Configure FILESTREAM](../../../relational-databases/blob/enable-and-configure-filestream.md)).  
  
 Make sure your INCLUDE environment variable includes the directory that contains sqlncli.h.  
  
 The server must have a directory called C:\DBFsa; this is where the sample will create the database. Your instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] must have write access to this location (for example, log on as a local system account).  
  
 Copy the first code listing and paste it into a file called ISSHelper.h.  
  
 Copy the second code listing and paste it into a file called ISSHelper.cpp.  
  
 Copy the third code listing and paste it into a file called ICommandUpload.cpp.  
  
 Compile ICommandUpload.cpp, ISSHelper.cpp, ole32.lib, and oleaut32.lib.  
  
 When you run this sample, you must pass the name of a server, or a server\instance_name, as well as a value between 4 MB (0x400001) and 4 GB (0xFFFFFFFF) indicating the amount of data to write.  
  
 The fourth ( [!INCLUDE[tsql](../../../includes/tsql-md.md)]) code listing deletes the database created by this sample.  
  
```cpp
// ISSHelper.h: interface for the CISSHelper class.  
  
#if !defined(AFX_ISSHELPER_H__7B88E5F3_263F_11D2_9D1F_00C04F96B8B2__INCLUDED_)  
#define AFX_ISSHELPER_H__7B88E5F3_263F_11D2_9D1F_00C04F96B8B2__INCLUDED_  
  
#if _MSC_VER > 1000  
#pragma once  
#endif // _MSC_VER > 1000  
  
class CISSHelper : public ISequentialStream {  
public:  
   CISSHelper(__int64 i64LobBytes);  
   virtual ~CISSHelper();  
  
   // Helper function to clean up memory.  
   virtual void Clear();  
  
   // ISequentialStream interface implementation.  
   STDMETHODIMP_(ULONG) AddRef(void);  
   STDMETHODIMP_(ULONG) Release(void);  
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
   ULONG m_cRef;   // Reference count (not used).  
   ULONG m_iReadPos;   // Current index position for reading from the buffer.  
   ULONG m_iWritePos;   // Current index position for writing to the buffer.  
  
};  
  
#endif   // !defined(AFX_ISSHELPER_H__7B88E5F3_263F_11D2_9D1F_00C04F96B8B2__INCLUDED_)  
```  
  
```cpp
// ISSHelper.cpp: implementation of the CISSHelper class.  
  
#pragma once  
  
#include <windows.h>  
#include <stdio.h>      // printf(...)  
#include <stddef.h>     // offsetof(...)  
#include <conio.h>      // _getch()  
#include <oledb.h>      // OLEDB  
#include <oledberr.h>   // OLEDB  
#include <objbase.h>    // CoInitializeEx()  
#include <msdasc.h>     // IDataInitialize  
#include <msdadc.h>     // OLEDB conversion library header.  
#include <msdaguid.h>   // OLEDB conversion library guids.  
  
#include "ISSHelper.h"  
  
#ifdef _DEBUG  
#undef THIS_FILE  
static char THIS_FILE[]=__FILE__;  
#define new DEBUm_NEW  
#endif  
  
// Class CISSHelper  
// Implementation of ISequentialStream interface  
  
CISSHelper::CISSHelper(__int64 i64LobBytes) {  
   m_cRef = 0;  
   m_pBuffer = NULL;  
   m_ulLength = 0;  
   m_ulStatus = DBSTATUS_S_OK;  
   m_iReadPos = 0;  
   m_iWritePos = 0;  
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
   m_cRef = 0;  
   m_pBuffer = NULL;  
   m_ulLength = 0;  
   m_ulStatus = DBSTATUS_S_OK;  
   m_iReadPos = 0;  
   m_iWritePos = 0;  
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
  
   if ( *ppv ) {  
      ( (IUnknown*) *ppv )->AddRef();  
      return S_OK;  
   }  
   return E_NOINTERFACE;  
}  
  
HRESULT CISSHelper::Read( void *pv, ULONG cb, ULONG* pcbRead ) {  
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
  
   if (( m_i64BytesUploaded - m_i64LastPrintUploaded ) >= (1024*1024*4))  {  
      __int64 i64Elapsed = (__int64) GetTickCount()-m_dwStartTick;  
      __int64 i64BytesPerSecond = m_i64BytesUploaded*1000/i64Elapsed;  
      printf("Param=%lu TotalBytes=%010I64u ElapsedMS=%010I64u BytesPerSec=%010I64u\n",   
         m_dwParamOrdinal, m_i64BytesUploaded, i64Elapsed, i64BytesPerSecond );  
      m_i64LastPrintUploaded = m_i64BytesUploaded;  
   }  
  
   if ( cb == left ) {  
      __int64 i64Elapsed = (__int64) GetTickCount()-m_dwStartTick;  
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
  
```cpp
// ICommandUpload.cpp  
#pragma once  
  
#include <windows.h>  
#include <stdio.h>      // printf(...)  
#include <stddef.h>     // offsetof(...)  
#include <conio.h>      // _getch()  
#include <oledb.h>      // OLEDB  
#include <oledberr.h>   // OLEDB  
#include <objbase.h>    // CoInitializeEx()  
#include <msdasc.h>     // IDataInitialize  
#include <msdadc.h>     // OLEDB conversion library header.  
#include <msdaguid.h>   // OLEDB conversion library guids.  
  
#define DBINITCONSTANTS  
#include <sqloledb.h>   // SS Provider.  
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
  
#define CLEAR_PROPS(x) { for ( int i=0; i<x; i++ ) VariantClear( &InitProperties[i].vValue ); }  
  
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
   IErrorRecords * pIErrorRecords = NULL;  
   IErrorInfo * pIErrorInfo = NULL;  
   IErrorInfo * pIErrorInfoRecord = NULL;  
   BSTR bstrDescription = NULL;  
   BSTR bstrSource = NULL;  
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
  
HRESULT OpenSessionSSHelper( IDBProperties*pIDBProperties, IDBCreateCommand** ppIDBCreateCommand, const wchar_t* szServer ) {  
#define TOTAL_SS_PROPS 3  
   HRESULT hr;  
   IDBInitialize * pIDBInitialize = NULL;    
   IDBCreateSession * pIDBCreateSession = NULL;  
   DBPROP InitProperties[TOTAL_SS_PROPS];  
   DBPROPSET InitPropSet;  
  
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
   CHECK_HR_ERR(hr,OpenSessionSSHelperCleanup);  
  
   // Clean up allocated variants.  
   CLEAR_PROPS( TOTAL_SS_PROPS );  
  
   // Initialize the DataSource.  
   hr = pIDBProperties->QueryInterface( IID_IDBInitialize, (void**) &pIDBInitialize );  
   CHECK_HR_ERR(hr,OpenSessionSSHelperCleanup);  
  
   hr = pIDBInitialize->Initialize();  
   CHECK_HR_ERR(hr,OpenSessionSSHelperCleanup);  
  
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
HRESULT OpenSessionSS( IDBCreateCommand** ppIDBCreateCommand, const wchar_t* szServer ) {  
   HRESULT hr;  
   IDBProperties * pIDBProperties = NULL;  
  
   // Null our out param in case we fail.  
   if ( NULL == ppIDBCreateCommand )   
      return E_INVALIDARG;  
  
   // Co-create SQLOLEDB.  
   hr = CoCreateInstance( SQLNCLI_CLSID, NULL, CLSCTX_INPROC_SERVER, IID_IDBProperties,   
      (void **) &pIDBProperties );      
   CHECK_HR_ERR(hr,OpenSessionSSCleanup);  
  
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
  
   HACCESSOR hAccessor;  
   DBBINDING dbBind[PARAM_COUNT];  
   DBPARAMS dbParams;  
  
   if ( argc < 3 ) {  
      wprintf( L"Usage: ICommandUpload <server> <size>\n");  
      return;  
   }  
  
   unsigned __int64 size = _wcstoui64( argv[2], NULL, 16 ) ;  
   if ( size <= (1024*1024*4) ) {  
      wprintf( L"size must be at least 4MB (either it's too small or it's not a well formed number)\n" );  
      return;  
   }  
  
   CISSHelper issHelper1( size );  
   CoInitialize(NULL);  
  
   // Open connection to SS database.  
   hr = OpenSessionSS( &pIDBCreateCommand, argv[1] );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   // Enable filestreaming, drop and recreate the database and table  
   hr = ExecuteSQL( pIDBCreateCommand, L"exec sp_configure 'filestream access level', 2" );  
  
   CHECK_HR_ERR( hr, MainCleanup );  
   hr = ExecuteSQL( pIDBCreateCommand, L"reconfigure" );  
   CHECK_HR_ERR( hr, MainCleanup );  
   hr = ExecuteSQL( pIDBCreateCommand, L"IF EXISTS (SELECT name FROM master..sysdatabases WHERE name = 'DBFsa') DROP DATABASE [DBFsa]");  
   CHECK_HR_ERR(hr, MainCleanup);  
   hr = ExecuteSQL( pIDBCreateCommand, L"IF NOT EXISTS (SELECT name FROM sysdatabases WHERE name = 'DBFsa')"  
      L"CREATE DATABASE [DBFsa] ON PRIMARY (NAME=[dbf], FILENAME='c:\\DBFsa\\dbf.mdf'),"   
      L"FILEGROUP [FsGrp] CONTAINS FILESTREAM (NAME=[FsCnt], FILENAME='c:\\DBFsa\\FsCnt')"  
      L"LOG ON (NAME = [dbl], FILENAME = 'c:\\DBFsa\\dbl.ldf', SIZE = 5MB, MAXSIZE = 3000MB, FILEGROWTH = 5MB)");  
   CHECK_HR_ERR(hr, MainCleanup);  
   hr = ExecuteSQL( pIDBCreateCommand, L"IF NOT EXISTS(SELECT name FROM [DBFsa]..[sysobjects] WHERE name='FSTRTable' AND type='U') "  
      L"CREATE TABLE [DBFsa]..[FSTRTable] (uid int NOT NULL UNIQUE NONCLUSTERED, val VARBINARY(MAX) FILESTREAM, "  
      L"rowguid uniqueidentifier ROWGUIDCOL NOT NULL UNIQUE DEFAULT NEWID())");  
   CHECK_HR_ERR(hr, MainCleanup);  
  
   printf( "Creation of table+proc via ExecuteSQL(...) returned 0x%08x.\n", hr );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   // Get ICommandText.  
   hr = pIDBCreateCommand->CreateCommand( NULL, IID_ICommandText, (IUnknown**)&pICommandText );  
   printf( "pIDBCreateCommand->CreateCommand(...) returned 0x%08x.\n", hr );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   // Prepare the parameterized execute statement.  
   hr = pICommandText->SetCommandText( DBGUID_DBSQL, L"INSERT INTO [DBFsa]..[FSTRTable] VALUES (?,?,DEFAULT)" );  
  
   // Get ICommandPrepare interface and prepare statement.  
   hr = pICommandText->QueryInterface( IID_ICommandPrepare, (void**)&pICommandPrepare );  
   printf( "pICommandText->QueryInterface(IID_ICommandPrepare) returned 0x%08x.\n", hr );  
   CHECK_HR_ERR(hr,MainCleanup);  
   hr = pICommandPrepare->Prepare(0);  
  
   // Set up parameter value binding.  
   ZeroMemory( &dbBind, sizeof(dbBind) );  
  
   struct _PARAM_BLOCK {  
      long f1;  
      long f1_status;  
      long f1_length;  
      unsigned long imageData_length;  
      long imageData_status;  
      IUnknown* imageData;  
   } PARAM_BLOCK;  
  
   dbBind[0].iOrdinal= 1;  
   dbBind[0].dwPart= DBPART_VALUE | DBPART_STATUS | DBPART_LENGTH;  
   dbBind[0].eParamIO= DBPARAMIO_INPUT;  
   dbBind[0].obValue= offsetof( _PARAM_BLOCK, f1 );  
   dbBind[0].obStatus= offsetof( _PARAM_BLOCK, f1_status );  
   dbBind[0].obLength= offsetof( _PARAM_BLOCK, f1_length );  
   dbBind[0].wType= DBTYPE_I4;  
   dbBind[0].cbMaxLen= sizeof(int);  
  
   dbBind[1].iOrdinal= 2;  
   dbBind[1].dwPart= DBPART_VALUE | DBPART_STATUS;  
   dbBind[1].eParamIO= DBPARAMIO_INPUT;  
   dbBind[1].obValue= offsetof( _PARAM_BLOCK, imageData );  
   dbBind[1].obStatus= offsetof( _PARAM_BLOCK, imageData_status );  
   dbBind[1].obLength  = offsetof( _PARAM_BLOCK, imageData_length );  
   dbBind[1].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
   dbBind[1].wType= DBTYPE_IUNKNOWN;  
   dbBind[1].pObject= new DBOBJECT;  
   dbBind[1].pObject->dwFlags = STGM_READ;  
   dbBind[1].pObject->iid = IID_ISequentialStream;  
   dbBind[1].cbMaxLen= 0;  
  
   // Get an accessor for our binding.  
   hr = pICommandText->QueryInterface( IID_IAccessor, (void**)&pIAccessor );  
   printf( "pICommandText->QueryInterface( IID_IAccessor) returned 0x%08x.\n", hr );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   hr = pIAccessor->CreateAccessor( DBACCESSOR_PARAMETERDATA,  
      PARAM_COUNT,   
      &dbBind[0],   
      sizeof(PARAM_BLOCK),   
      &hAccessor,   
      NULL );  
   printf( "pIAccessor->CreateAccessor(...) returned 0x%08x.\n", hr );  
   CHECK_HR_ERR(hr,MainCleanup);  
  
   // Set the parameter values and status/length.  
   PARAM_BLOCK.f1 = 1;  
   PARAM_BLOCK.f1_status = DBSTATUS_S_OK;  
   PARAM_BLOCK.f1_length = sizeof(int);  
   PARAM_BLOCK.imageData = (IUnknown*)&issHelper1;  
   PARAM_BLOCK.imageData_status = DBSTATUS_S_OK;  
   PARAM_BLOCK.imageData_length = (long)issHelper1.m_i64ThreshHold;  
  
   issHelper1.m_dwParamOrdinal=1;  
  
   dbParams.cParamSets= 1;  
   dbParams.hAccessor= hAccessor;  
   dbParams.pData= &PARAM_BLOCK;  
  
   __int64 i64TotalBytesUploaded = issHelper1.m_i64ThreshHold;  
  
   DWORD dwStartTick = GetTickCount();  
   hr = pICommandText->Execute( NULL, IID_NULL, &dbParams, NULL, NULL );  
   CHECK_HR_ERR(hr, MainCleanup);  
   printf( "pICommandText->Execute(...) returned 0x%08x.\n", hr );  
   DWORD dwElapsed = GetTickCount()-dwStartTick;  
  
   __int64 i64Elapsed = (__int64) dwElapsed;  
   __int64 i64BytesPerSecond = i64TotalBytesUploaded*1000/i64Elapsed;  
   printf("TotalBytes=%010I64u ElapsedMS=%I64u BytesPerSecond=%010I64u\n",   
      i64TotalBytesUploaded, i64Elapsed, i64BytesPerSecond );  
  
MainCleanup:  
   SAFE_RELEASE( pIDBCreateCommand );  
   SAFE_RELEASE( pICommandText );  
   SAFE_RELEASE( pICommandPrepare );  
   SAFE_RELEASE( pIAccessor );  
}  
```  
  
```sql
sp_detach_db 'DBFsa'  
IF EXISTS (SELECT name FROM master..sysdatabases WHERE name = 'DBFsa') DROP DATABASE [DBFsa]  
```  
  
  
