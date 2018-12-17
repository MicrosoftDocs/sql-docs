---
title: "Execute a User-Defined Function and Process Return Code (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "user-defined functions [OLE DB]"
ms.assetid: d96c33fd-ed17-4713-8921-bf2dc3347f78
author: MightyPen
ms.author: genemi
manager: craigg
---
# Execute a User-Defined Function and Process Return Code (OLE DB)
  In this example, a user-defined function is executed, and the return code is printed. This sample is not supported on IA64.  
  
 This sample requires the AdventureWorks sample database, which you can download from the [Microsoft SQL Server Samples and Community Projects](https://go.microsoft.com/fwlink/?LinkID=85384) home page.  
  
> [!IMPORTANT]  
>  When possible, use Windows Authentication. If Windows Authentication is not available, prompt users to enter their credentials at run time. Avoid storing credentials in a file. If you must persist credentials, you should encrypt them with the [Win32 crypto API](https://go.microsoft.com/fwlink/?LinkId=64532).  
  
## Example  
 Execute the first ([!INCLUDE[tsql](../../../includes/tsql-md.md)]) code listing to create the stored procedure used by the application.  
  
 Compile with ole32.lib oleaut32.lib and execute the second (C++) code listing. This application connects to your computer's default [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. On some Windows operating systems, you will need to change (localhost) or (local) to the name of your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. To connect to a named instance, change the connection string from L"(local)" to L"(local)\\\name" , where name is the named instance. By default, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Express installs to a named instance. Make sure your INCLUDE environment variable includes the directory that contains sqlncli.h.  
  
 Execute the third ([!INCLUDE[tsql](../../../includes/tsql-md.md)]) code listing to delete the stored procedure used by the application.  
  
```  
USE AdventureWorks  
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
  
```  
// compile with: ole32.lib oleaut32.lib  
void InitializeAndEstablishConnection();  
#define UNICODE  
#define DBINITCONSTANTS  
#define INITGUID  
#define OLEDBVER 0x0250   // to include correct interfaces  
  
#include <windows.h>  
#include <stdio.h>  
#include <stddef.h>  
#include <iostream>  
#include <oledb.h>  
#include <oledberr.h>  
#include <sqlncli.h>  
using namespace std;  
  
IDBInitialize* pIDBInitialize = NULL;      
IDBCreateSession* pIDBCreateSession = NULL;  
IDBCreateCommand* pIDBCreateCommand = NULL;  
ICommandText* pICommandText = NULL;  
IRowset* pIRowset = NULL;  
ICommandWithParameters* pICommandWithParams = NULL;  
IAccessor* pIAccessor = NULL;  
IDBProperties* pIDBProperties = NULL;  
WCHAR* pStringsBuffer;  
DBBINDING* pBindings;  
const ULONG nInitProps = 4;  
DBPROP InitProperties[nInitProps];      
const ULONG nPropSet = 1;  
DBPROPSET rgInitPropSet[nPropSet];    
HRESULT hr;  
HACCESSOR hAccessor;  
const ULONG nParams = 3;   // No. of parameters in the command  
DBPARAMBINDINFO ParamBindInfo[nParams];  
ULONG i;  
ULONG cbColOffset = 0;  
  
DB_UPARAMS ParamOrdinals[nParams];  
DBROWCOUNT cNumRows = 0;  
DBPARAMS Params;  
  
// Declare array of DBBINDING structures, one for each parameter in the command  
DBBINDING acDBBinding[nParams];  
DBBINDSTATUS acDBBindStatus[nParams];  
  
// The following buffer is used to store parameter values.  
typedef struct tagSPROCPARAMS {  
   long lReturnValue;  
   long inParam1;  
   long inParam2;  
} SPROCPARAMS;  
  
int main() {  
   // The command to execute.  
   WCHAR* wCmdString = L"{? = CALL fn_RectangleArea(?, ?) }";  
   // WCHAR* wCmdString = L"EXEC ? = fn_RectangleVolume(?, ?)";  
  
   SPROCPARAMS sprocparams = {0,5,10};  
  
   // All the initialization stuff in a separate function.  
   InitializeAndEstablishConnection();  
  
   // Let us create a new session from the data source object.  
   if (FAILED(pIDBInitialize->QueryInterface( IID_IDBCreateSession,  
                                              (void**) &pIDBCreateSession))) {  
      cout << "Failed to access IDBCreateSession interface\n";  
      goto EXIT;  
   }  
  
   if ( FAILED(pIDBCreateSession->CreateSession( NULL,   
                                                 IID_IDBCreateCommand,   
                                                 (IUnknown**) &pIDBCreateCommand))) {  
      cout << "pIDBCreateSession->CreateSession failed\n";  
      goto EXIT;  
   }  
  
   // Create a Command  
   if (FAILED(pIDBCreateCommand->CreateCommand( NULL,   
                                                IID_ICommandText,   
                                                (IUnknown**) &pICommandText))) {  
      cout << "Failed to access ICommand interface\n";  
      goto EXIT;  
   }  
  
   // Set the command text.  
   if (FAILED(pICommandText->SetCommandText(DBGUID_DBSQL, wCmdString))) {  
      cout << "failed to set command text\n";  
      goto EXIT;  
   }  
  
   // Describe the command parameters (parameter name, provider specific name   
   // of the parameter's data type etc.) in an array of DBPARAMBINDINFO   
   // structures.  This information is then used by SetParameterInfo().  
   ParamBindInfo[0].pwszDataSourceType = L"DBTYPE_I4";  
   ParamBindInfo[0].pwszName = NULL;  
   ParamBindInfo[0].ulParamSize = sizeof(long);  
   ParamBindInfo[0].dwFlags = DBPARAMFLAGS_ISOUTPUT;  
   ParamBindInfo[0].bPrecision = 11;  
   ParamBindInfo[0].bScale = 0;  
   ParamOrdinals[0] = 1;  
  
   ParamBindInfo[1].pwszDataSourceType = L"DBTYPE_I4";  
   ParamBindInfo[1].pwszName = NULL;   // L"@inparam1";                 
   ParamBindInfo[1].ulParamSize = sizeof(long);  
   ParamBindInfo[1].dwFlags = DBPARAMFLAGS_ISINPUT;  
   ParamBindInfo[1].bPrecision = 11;  
   ParamBindInfo[1].bScale = 0;  
   ParamOrdinals[1] = 2;  
  
   ParamBindInfo[2].pwszDataSourceType = L"DBTYPE_I4";  
   ParamBindInfo[2].pwszName = NULL;   // L"@inparam2";   
   ParamBindInfo[2].ulParamSize = sizeof(long);  
   ParamBindInfo[2].dwFlags = DBPARAMFLAGS_ISINPUT;  
   ParamBindInfo[2].bPrecision = 11;  
   ParamBindInfo[2].bScale = 0;  
   ParamOrdinals[2] = 3;  
  
   // Set the parameters information.  
   if (FAILED(pICommandText->QueryInterface( IID_ICommandWithParameters,  
                                             (void**)&pICommandWithParams))) {  
      cout << "failed to obtain ICommandWithParameters\n";  
      goto EXIT;  
   }  
   if (FAILED(pICommandWithParams->SetParameterInfo( nParams,   
                                                     ParamOrdinals,   
                                                     ParamBindInfo))) {  
      cout << "failed in setting parameter info.(SetParameterInfo)\n";  
      goto EXIT;  
   }  
  
   // Describe the consumer buffer; initialize the array of DBBINDING structures.    
   // Each binding associates a single parameter to the consumer's buffer.  
   for ( i = 0 ; i < nParams ; i++ ) {  
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
   hr = pICommandWithParams->QueryInterface( IID_IAccessor, (void**)&pIAccessor);  
   if (FAILED(hr))  
      cout << "Failed to get IAccessor interface\n";  
  
   hr = pIAccessor->CreateAccessor( DBACCESSOR_PARAMETERDATA,  
                                    nParams,   
                                    acDBBinding,   
                                    sizeof(SPROCPARAMS),   
                                    &hAccessor,  
                                    acDBBindStatus);  
   if (FAILED(hr))  
      cout << "failed to create accessor for the defined parameters\n";  
  
   // Initialize DBPARAMS structure for command execution. DBPARAMS specifies the  
   // parameter values in the command.  DBPARAMS is then passed to Execute.  
   Params.pData = &sprocparams;  
   Params.cParamSets = 1;  
   Params.hAccessor = hAccessor;  
  
   // Execute the command.  
   if (FAILED(hr = pICommandText->Execute( NULL,   
                                           IID_IRowset,   
                                           &Params,   
                                           &cNumRows,   
                                           (IUnknown **) &pIRowset))) {  
      cout << "failed to execute command\n";  
      goto EXIT;  
   }  
  
   printf("  Return value = %d\n", sprocparams.lReturnValue);  
  
   // Release result set without processing.  
   if (pIRowset != NULL)  
      pIRowset->Release();  
  
   // Release memory.  
   pIAccessor->ReleaseAccessor(hAccessor, NULL);  
   pIAccessor->Release();  
   pICommandWithParams->Release();  
   pICommandText->Release();  
   pIDBCreateCommand->Release();  
   pIDBCreateSession->Release();      
  
   if (FAILED(pIDBInitialize->Uninitialize()))  
      // Uninitialize is not required, but it fails if an interface  
      // has not been released.  This can be used for debugging.  
      cout << "Problem uninitializing\n";  
  
   pIDBInitialize->Release();  
  
   CoUninitialize();  
   return 0;  
  
EXIT:  
   if (pIAccessor != NULL)  
      pIAccessor->Release();  
   if (pICommandWithParams != NULL)  
      pICommandWithParams->Release();  
   if (pICommandText != NULL)  
      pICommandText->Release();  
   if (pIDBCreateCommand != NULL)  
      pIDBCreateCommand->Release();  
   if (pIDBCreateSession != NULL)  
      pIDBCreateSession->Release();  
   if (pIDBInitialize != NULL)  
      if (FAILED(pIDBInitialize->Uninitialize()))  
         // Uninitialize is not required, but it fails if an interface has   
         // not been released.  This can be used for debugging.  
         cout << "problem in uninitializing\n";  
      pIDBInitialize->Release();  
  
   CoUninitialize();  
}  
  
void InitializeAndEstablishConnection() {      
   // Initialize the COM library.  
   CoInitialize(NULL);  
  
   // Obtain access to the SQL Server native Client OLE DB provider.      
   hr = CoCreateInstance( CLSID_SQLNCLI11,   
                          NULL,   
                          CLSCTX_INPROC_SERVER,  
                          IID_IDBInitialize,   
                          (void **) &pIDBInitialize);  
  
   if (FAILED(hr))  
      cout << "Failed in CoCreateInstance()\n";  
  
   // Initialize the property values needed to establish the connection.  
   for ( i = 0 ; i < nInitProps ; i++ )  
      VariantInit(&InitProperties[i].vValue);  
  
   // Specify server name.  
   InitProperties[0].dwPropertyID = DBPROP_INIT_DATASOURCE;  
   InitProperties[0].vValue.vt = VT_BSTR;  
  
   // Replace "MySqlServer" with proper value.  
   InitProperties[0].vValue.bstrVal = SysAllocString(L"(local)");  
   InitProperties[0].dwOptions = DBPROPOPTIONS_REQUIRED;  
   InitProperties[0].colid = DB_NULLID;  
  
   // Specify database name.  
   InitProperties[1].dwPropertyID = DBPROP_INIT_CATALOG;  
   InitProperties[1].vValue.vt = VT_BSTR;  
   InitProperties[1].vValue.bstrVal = SysAllocString(L"AdventureWorks");  
   InitProperties[1].dwOptions = DBPROPOPTIONS_REQUIRED;  
   InitProperties[1].colid = DB_NULLID;  
  
   InitProperties[2].dwPropertyID = DBPROP_AUTH_INTEGRATED;  
   InitProperties[2].vValue.vt = VT_BSTR;  
   InitProperties[2].vValue.bstrVal = SysAllocString(L"SSPI");  
   InitProperties[2].dwOptions = DBPROPOPTIONS_REQUIRED;  
   InitProperties[2].colid = DB_NULLID;  
  
   // Now that properties are set, construct the DBPROPSET structure  
   // (rgInitPropSet).  The DBPROPSET structure is used to pass an array  
   // of DBPROP structures (InitProperties) to SetProperties method.  
   rgInitPropSet[0].guidPropertySet = DBPROPSET_DBINIT;  
   rgInitPropSet[0].cProperties = 4;  
   rgInitPropSet[0].rgProperties = InitProperties;  
  
   // Set initialization properties.  
   hr = pIDBInitialize->QueryInterface( IID_IDBProperties, (void **)&pIDBProperties);  
   if (FAILED(hr))  
      cout << "Failed to obtain IDBProperties interface.\n";  
  
   hr = pIDBProperties->SetProperties( nPropSet, rgInitPropSet);  
   if (FAILED(hr))  
      cout << "Failed to set initialization properties\n";  
  
   pIDBProperties->Release();  
  
   // Now we establish connection to the data source.  
   if (FAILED(pIDBInitialize->Initialize()))  
      cout << "Problem in initializing\n";  
}  
```  
  
```  
USE AdventureWorks  
drop function fn_RectangleArea  
go  
```  
  
## See Also  
 [Processing Results How-to Topics &#40;OLE DB&#41;](processing-results-how-to-topics-ole-db.md)  
  
  
