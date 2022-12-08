---
description: "Retrieve Rows Using Bookmarks (Native Client OLE DB provider)"
title: Retrieve rows using bookmarks (Native Client OLE DB provider)
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "bookmarks [OLE DB]"
  - "rows [OLE DB]"
ms.assetid: 5e14d5c8-e7c6-498f-8041-7e006a1c2d81
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Retrieve Rows Using Bookmarks (Native Client OLE DB provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The consumer sets the **dwFlag** field value of the binding structure to DBCOLUMNSINFO_ISBOOKMARK to indicate that the column is used as bookmark. The consumer also sets the rowset property DBPROP_BOOKMARKS to VARIANT_TRUE. This allows column 0 to be present in the rowset. **IRowsetLocate::GetRowsAt** is then used to fetch rows starting with the row specified an offset from a bookmark.  
  
> [!IMPORTANT]  
>  When possible, use Windows Authentication. If Windows Authentication is not available, prompt users to enter their credentials at run time. Avoid storing credentials in a file. If you must persist credentials, you should encrypt them with the [Win32 crypto API](/windows/win32/seccrypto/cryptography-reference).  
  
### To retrieve rows using bookmarks  
  
1.  Establish a connection to the data source.  
  
2.  Set the rowset DBPROP_IRowsetLocate property to VARIANT_TRUE.  
  
3.  Execute the command.  
  
4.  Set the **dwFlag** field of the binding structure to DBCOLUMNSINFO_ISBOOKMARK flag for the column that will be used as a bookmark.  
  
5.  Use **IRowsetLocate::GetRowsAt** to fetch rows, starting with the row specified by an offset from the bookmark.  

## Example  
 This sample shows how to fetch rows using a bookmark. This sample is not supported on IA64.  
  
 In this sample, the fifth row is retrieved from the result set produced from the execution of a SELECT statement.  
  
 This sample requires the AdventureWorks sample database, which you can download from the [Microsoft SQL Server Samples and Community Projects](https://go.microsoft.com/fwlink/?LinkID=85384) home page.  
  
 Compile with ole32.lib oleaut32.lib and execute the following C++ code listing. This application connects to your computer's default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. On some Windows operating systems, you will need to change (localhost) or (local) to the name of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. To connect to a named instance, change the connection string from L"(local)" to L"(local)\\\name", where name is the named instance. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express installs to a named instance. Make sure your INCLUDE environment variable includes the directory that contains sqlncli.h.  
  
```  
// compile with: ole32.lib oleaut32.lib  
int InitializeAndEstablishConnection();  
int ProcessResultSet();  
  
#define UNICODE  
#define _UNICODE  
#define DBINITCONSTANTS  
#define INITGUID  
#define OLEDBVER 0x0250   // to include correct interfaces  
  
#include <stdio.h>  
#include <tchar.h>  
#include <stddef.h>  
#include <windows.h>  
#include <iostream>  
#include <oledb.h>  
#include <sqlncli.h>  
  
using namespace std;  
  
IDBInitialize*       pIDBInitialize      = NULL;  
IDBProperties*       pIDBProperties      = NULL;  
IDBCreateSession*    pIDBCreateSession   = NULL;  
IDBCreateCommand*    pIDBCreateCommand   = NULL;  
ICommandProperties*  pICommandProperties = NULL;  
ICommandText*        pICommandText       = NULL;  
IRowset*             pIRowset            = NULL;  
IColumnsInfo*        pIColumnsInfo       = NULL;  
DBCOLUMNINFO*        pDBColumnInfo       = NULL;  
IAccessor*           pIAccessor          = NULL;  
IRowsetLocate*       pIRowsetLocate      = NULL;  
  
DBPROP        InitProperties[4];  
DBPROPSET     rgInitPropSet[1];   
DBPROPSET     rgPropSets[1];  
DBPROP        rgProperties[1];  
ULONG         i, j;                
HRESULT       hresult;  
DBROWCOUNT    cNumRows = 0;  
DBORDINAL     lNumCols;  
WCHAR*        pStringsBuffer;  
DBBINDING*    pBindings;  
DBLENGTH      ConsumerBufferColOffset = 0;  
HACCESSOR     hAccessor;  
DBCOUNTITEM   lNumRowsRetrieved;  
HROW          hRows[5];           
HROW*         pRows = &hRows[0];  
char*         pBuffer;  
  
int main() {  
   // The command to execute.  
   // WCHAR* wCmdString = OLESTR(" SELECT title_id, title FROM titles ");  
   WCHAR* wCmdString = OLESTR(" SELECT Name FROM Production.Product");  
  
   // Initialize and establish a connection to the data source.  
   if (InitializeAndEstablishConnection() == -1) {  
      // Handle error.  
      cout << "Failed to initialize and connect to the server.\n";  
      return -1;  
   }  
  
   // Create a session object.  
   if (FAILED(pIDBInitialize->QueryInterface(IID_IDBCreateSession, (void**) &pIDBCreateSession))) {  
      cout << "Failed to obtain IDBCreateSession interface.\n";  
      // Handle error.  
      return -1;  
   }  
  
   if (FAILED(pIDBCreateSession->CreateSession(NULL, IID_IDBCreateCommand, (IUnknown**) &pIDBCreateCommand))) {  
      cout << "pIDBCreateSession->CreateSession failed.\n";  
      // Handle error.  
      return -1;  
   }  
  
   // Access the ICommandText interface.  
   if (FAILED(pIDBCreateCommand->CreateCommand( NULL, IID_ICommandText, (IUnknown**) &pICommandText))) {  
      cout << "Failed to access ICommand interface.\n";  
      // Handle error.  
      return -1;  
   }  
  
   // Set DBPROP_IRowsetLocate  
   if (FAILED(pICommandText->QueryInterface( IID_ICommandProperties, (void **) &pICommandProperties ))) {  
      cout << "Failed to obtain ICommandProperties interface.\n";  
      // Handle error.  
      return -1;  
   }  
  
   // Set DBPROP_IRowsetLocate to VARIANT_TRUE to get the IRowsetLocate interface.  
   VariantInit(&rgProperties[0].vValue);  
  
   rgPropSets[0].guidPropertySet = DBPROPSET_ROWSET;  
   rgPropSets[0].cProperties = 1;  
   rgPropSets[0].rgProperties = rgProperties;  
  
   // Set properties in the property group (DBPROPSET_ROWSET)   
   rgPropSets[0].rgProperties[0].dwPropertyID  = DBPROP_IRowsetLocate;  
   rgPropSets[0].rgProperties[0].dwOptions     = DBPROPOPTIONS_REQUIRED;  
   rgPropSets[0].rgProperties[0].colid         = DB_NULLID;  
   rgPropSets[0].rgProperties[0].vValue.vt     = VT_BOOL;  
   rgPropSets[0].rgProperties[0].vValue.boolVal= VARIANT_TRUE;  
  
   // Set the rowset properties.  
   hresult = pICommandText->QueryInterface( IID_ICommandProperties,(void **)&pICommandProperties);  
   if (FAILED(hresult)) {  
      printf("Failed to get ICommandProperties to set rowset properties.\n");  
      // Release any references and return.  
      return -1;  
   }  
  
   hresult = pICommandProperties->SetProperties(1, rgPropSets);  
   if (FAILED(hresult)) {  
      printf("Execute failed to set rowset properties.\n");  
      // Release any references and return.  
      return -1;  
   }   
  
   pICommandProperties->Release();  
  
   // Specify the command text.  
   if (FAILED(pICommandText->SetCommandText(DBGUID_DBSQL, wCmdString))) {  
      cout << "Failed to set command text.\n";  
      // Handle error.  
      return -1;  
   }  
  
   // Execute the command.  
   if (FAILED(hresult =   
      pICommandText->Execute( NULL, IID_IRowset, NULL, &cNumRows, (IUnknown **) &pIRowset))) {  
      cout << "Failed to execute command.\n";  
      // Handle error.  
      return -1;  
   }  
  
   ProcessResultSet();   
  
   pIRowset->Release();  
  
   // Free up memory.  
   pICommandText->Release();  
   pIDBCreateCommand->Release();  
   pIDBCreateSession->Release();  
  
   pIDBInitialize->Uninitialize();  
   pIDBInitialize->Release();  
  
   // Release COM library.  
   CoUninitialize();  
  
   return -1;  
}  
  
int InitializeAndEstablishConnection() {      
   // Initialize the COM library.  
   CoInitialize(NULL);  
  
   // Obtain access to the SQL Server Native Client OLe DB provider.  
   CoCreateInstance( CLSID_SQLNCLI11, NULL, CLSCTX_INPROC_SERVER, IID_IDBInitialize, (void **) &pIDBInitialize);  
  
   // Initialize the property values that are the same for each property.  
   for ( i = 0 ; i < 4 ; i++ ) {  
      VariantInit(&InitProperties[i].vValue);  
      InitProperties[i].dwOptions = DBPROPOPTIONS_REQUIRED;  
      InitProperties[i].colid = DB_NULLID;  
   }  
  
   // Server name.  
   InitProperties[0].dwPropertyID = DBPROP_INIT_DATASOURCE;  
   InitProperties[0].vValue.vt = VT_BSTR;  
   InitProperties[0].vValue.bstrVal = SysAllocString(L"(local)");  
  
   // Database.  
   InitProperties[1].dwPropertyID = DBPROP_INIT_CATALOG;  
   InitProperties[1].vValue.vt = VT_BSTR;  
   InitProperties[1].vValue.bstrVal = SysAllocString(L"AdventureWorks");  
  
   InitProperties[2].dwPropertyID = DBPROP_AUTH_INTEGRATED;   
   InitProperties[2].vValue.vt = VT_BSTR;  
   InitProperties[2].vValue.bstrVal = SysAllocString(L"SSPI");  
  
   // Construct the PropertySet array.  
   rgInitPropSet[0].guidPropertySet = DBPROPSET_DBINIT;  
   rgInitPropSet[0].cProperties = 4;  
   rgInitPropSet[0].rgProperties = InitProperties;  
  
   // Set initialization properties.  
   pIDBInitialize->QueryInterface(IID_IDBProperties, (void **)&pIDBProperties);  
  
   hresult = pIDBProperties->SetProperties(1, rgInitPropSet);   
   if (FAILED(hresult)) {  
      cout << "Failed to set initialization properties.\n";  
      // Handle error.  
      return -1;  
   }  
  
   pIDBProperties->Release();  
  
   // Call the initialization method to establish the connection.  
   if (FAILED(pIDBInitialize->Initialize())) {  
      cout << "Problem initializing and connecting to the data source.\n";  
      // Handle error.  
      return -1;  
   }  
  
   return 0;  
}  
  
#ifdef _WIN64  
#define BUFFER_ALIGNMENT 8  
#else  
#define BUFFER_ALIGNMENT 4  
#endif  
  
#define ROUND_UP(value) (value + (BUFFER_ALIGNMENT - 1) & ~(BUFFER_ALIGNMENT - 1))  
  
int ProcessResultSet() {  
   HRESULT hr;  
  
   // Retrieve 5th row from the rowset (for example).  
   DBBKMARK iBookmark = 5;  
  
   pIRowset->QueryInterface(IID_IColumnsInfo, (void **)&pIColumnsInfo);  
  
   pIColumnsInfo->GetColumnInfo( &lNumCols, &pDBColumnInfo, &pStringsBuffer );  
  
   // Create a DBBINDING array.  
   pBindings = new DBBINDING[lNumCols];  
   if (!(pBindings /* = new DBBINDING[lNumCols] */ )) {  
      // Handle error.  
      return -1;  
   }  
  
   // Using the ColumnInfo strucuture, fill out the pBindings array.  
   for ( j = 0 ; j < lNumCols ; j++ ) {  
      pBindings[j].iOrdinal  = j;  
      pBindings[j].obValue   = ConsumerBufferColOffset;  
      pBindings[j].pTypeInfo = NULL;  
      pBindings[j].pObject   = NULL;  
      pBindings[j].pBindExt  = NULL;  
      pBindings[j].dwPart    = DBPART_VALUE;  
      pBindings[j].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
      pBindings[j].eParamIO  = DBPARAMIO_NOTPARAM;  
      pBindings[j].cbMaxLen  = pDBColumnInfo[j].ulColumnSize + 1;   // + 1 for null terminator  
      pBindings[j].dwFlags   = 0;  
      pBindings[j].wType      = pDBColumnInfo[j].wType;  
      pBindings[j].bPrecision = pDBColumnInfo[j].bPrecision;  
      pBindings[j].bScale     = pDBColumnInfo[j].bScale;  
  
      // Recalculate the next buffer offset.  
      ConsumerBufferColOffset = ConsumerBufferColOffset + pDBColumnInfo[j].ulColumnSize;  
  ConsumerBufferColOffset = ROUND_UP(ConsumerBufferColOffset);  
  
   };  
   // Indicate that the first field is used as a bookmark by setting  
   // dwFlags to DBCOLUMNFLAGS_ISBOOKMARK.  
   pBindings[0].dwFlags = DBCOLUMNFLAGS_ISBOOKMARK;  
  
   // Get IAccessor interface.  
   hr = pIRowset->QueryInterface( IID_IAccessor, (void **)&pIAccessor);  
   if (FAILED(hr)) {  
      printf("Failed to get IAccessor interface.\n");  
      // Handle error.  
      return -1;  
   }  
  
   // Create accessor.  
   hr = pIAccessor->CreateAccessor( DBACCESSOR_ROWDATA,  
                                    lNumCols,  
                                    pBindings,  
                                    0,  
                                    &hAccessor,  
                                    NULL);  
  
   if (FAILED(hr)) {  
      printf("Failed to create an accessor.\n");  
      // Handle error.  
      return -1;  
   }  
  
   hr = pIRowset->QueryInterface( IID_IRowsetLocate, (void **) &pIRowsetLocate);  
   if (FAILED(hr)) {  
      printf("Failed to get IRowsetLocate interface.\n");  
      // Handle error.  
      return -1;  
   }  
  
   hr = pIRowsetLocate->GetRowsAt( 0,  
                                   NULL,  
                                   sizeof(DBBKMARK),  
                                   (BYTE *) &iBookmark,  
                                   0,  
                                   1,  
                                   &lNumRowsRetrieved,  
                                   &pRows);  
  
   if (FAILED(hr)) {  
      printf("Calling the GetRowsAt method failed.\n");  
      // Handle error.  
      return -1;  
   }  
  
   // Create buffer and retrieve data.  
   pBuffer = new char[ConsumerBufferColOffset];  
   if (!(pBuffer /* = new char[ConsumerBufferColOffset] */ )) {  
      // Handle error.  
      return -1;  
   }  
  
   memset(pBuffer, 0, ConsumerBufferColOffset);  
  
   hr = pIRowset->GetData(hRows[0], hAccessor, pBuffer);  
   if (FAILED(hr)) {  
      printf("Failed GetDataCall.\n");  
      // Handle error.  
      return -1;  
   }  
  
   char szTitle[7] = {0};  
   strncpy_s(szTitle, &pBuffer[pBindings[1].obValue], 6);  
  
   printf("%S\n", &pBuffer[pBindings[1].obValue]);  
  
   pIRowset->ReleaseRows(lNumRowsRetrieved, hRows, NULL, NULL, NULL);  
  
   // Release allocated memory.  
   delete [] pBuffer;  
   pIAccessor->ReleaseAccessor(hAccessor, NULL);  
   pIAccessor->Release();  
   delete [] pBindings;  
  
   return 0;  
}  
```  
  
