---
title: "Use Large CLR UDTs (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 30f59c11-3bca-41be-8bd7-0d1efaf1f0be
author: MightyPen
ms.author: genemi
manager: craigg
---
# Use Large CLR UDTs (OLE DB)
  This sample shows how to fetch rows with large user-defined types from a result set. For more information, see [Large CLR User-Defined Types &#40;OLE DB&#41;](../native-client/ole-db/large-clr-user-defined-types-ole-db.md). This sample works with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later.  
  
## Example  
 This sample contains two projects. One project creates an assembly (DLL) from C# source code. This assembly contains the CLR type. A table will be added to the database. A column in the table will be of a type defined in the assembly, By default, this sample will use the master database. The second project is a native C application that reads data from the table.  
  
 Compile the first (C#) code listing to a DLL.  Then, copy the DLL to the root directory of the C drive.  
  
 Execute the second ([!INCLUDE[tsql](../../includes/tsql-md.md)]) code listing to add the assembly to the master database.  
  
 Compile with ole32.lib oleaut32.lib and execute the third (C++) code listing. This application connects to your computer's default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. On some Windows operating systems, you will need to change (localhost) or (local) to the name of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. To connect to a named instance, change the connection string from L"(local)" to L"(local)\\\name" , where name is the named instance. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express installs to a named instance. Make sure your INCLUDE environment variable includes the directory that contains sqlncli.h.  
  
 Execute the fourth ([!INCLUDE[tsql](../../includes/tsql-md.md)]) code listing to delete the assembly to the master database.  
  
```  
// compile with: /target: library  
using System;  
using System.Data;  
using System.Data.SqlTypes;  
using Microsoft.SqlServer.Server;  
using System.Text;  
  
[assembly: System.CLSCompliantAttribute(true)]  
[Serializable]  
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, IsFixedLength = false, MaxByteSize = -1, IsByteOrdered = true)]  
public class LargeStringUDT : INullable, IBinarySerialize {  
    private bool _isNull;  
    private string _largeString;  
  
    public bool IsNull {  
        get {  
            return (_isNull);  
        }  
    }  
  
    public static LargeStringUDT Null {  
        get {  
            LargeStringUDT lsUDT = new LargeStringUDT();  
            lsUDT._isNull = true;  
            return lsUDT;  
        }  
    }  
  
    public override string ToString() {  
        if (IsNull)  
            return "NULL";  
        else  
            return _largeString;  
    }  
  
    [SqlMethod(OnNullCall = false)]  
    public static LargeStringUDT Parse(SqlString s) {  
        if (s.IsNull)  
            return Null;  
  
        LargeStringUDT lsUDT = new LargeStringUDT();  
        lsUDT._largeString = s.Value;  
        return lsUDT;  
    }  
  
    public String LargeString {  
        get {  
            return _largeString;  
        }  
  
        set {  
            _largeString = value;  
        }  
    }  
  
    public void Read(System.IO.BinaryReader r) {  
        _isNull = r.ReadBoolean();  
        if (!_isNull)  
            _largeString = new String(r.ReadChars(r.ReadInt32()));  
    }  
  
    public void Write(System.IO.BinaryWriter w) {  
        w.Write(_isNull);  
        if (!_isNull) {  
            w.Write(_largeString.Length);  
            for (int i = 0; i < _largeString.Length; ++i)  
                w.Write(_largeString[i]);  
        }  
    }  
}  
```  
  
```  
USE [MASTER]  
GO  
  
CREATE ASSEMBLY LargeStringUDT  
FROM 'C:\LargeStringUDT.dll'  
WITh PERMISSION_SET=SAFE;  
GO  
  
CREATE TYPE dbo.LargeStringUDT   
EXTERNAL NAME LargeStringUDT.[LargeStringUDT];  
GO  
  
CREATE TABLE dbo.LargeStringUDTs  
(ID int IDENTITY(1,1) PRIMARY KEY, LargeString LargeStringUDT)  
GO  
  
INSERT INTO dbo.LargeStringUDTs (LargeString) VALUES (CONVERT(LargeStringUDT, 'This is the first string'));  
INSERT INTO dbo.LargeStringUDTs (LargeString) VALUES (CONVERT(LargeStringUDT, 'This is the second string'));  
INSERT INTO dbo.LargeStringUDTs (LargeString) VALUES (Convert(LargeStringUDT, 'This is the third string'));  
GO  
```  
  
```  
// compile with: ole32.lib oleaut32.lib  
// Gives length of an array  
#define ARRAY_SIZE(rgArray) (sizeof(rgArray)/sizeof(*rgArray))  
#define NUMELEM(rgArray) ARRAY_SIZE(rgArray)  
  
#define DBINITCONSTANTS  
#define INITGUID  
  
#define OLEDBVER 0x0250   // to include correct interfaces  
  
#define ROUND_UP_MINIMUM 8  
  
#define ROUND_UP(valueToRound) \  
   (((valueToRound) + (ROUND_UP_MINIMUM - 1)) & ~(ROUND_UP_MINIMUM - 1))  
  
#include <stdio.h>  
#include <tchar.h>  
#include <stddef.h>  
#include <windows.h>  
#include <iostream>  
#include <oledb.h>  
#include <SQLNCLI.h>  
  
using namespace std;  
  
// Arrangement of column data when standard rowbuffer layout is used.  
struct COLUMNDATA {  
   DBLENGTH dwLength;   // length of data (not space allocated)  
   DBSTATUS dwStatus;   // status of column  
#ifdef _WIN64  
   // rgbData needs to be COLUMN_ALIGNVAL byte aligned. This fixes it for 64 bit build.  
   DWORD dwAlign;  
#endif  
   BYTE rgbData[1];   // data here and beyond  
};  
  
int InitializeAndEstablishConnection();  
int ProcessResultSet();  
  
IDBInitialize* pIDBInitialize = NULL;  
IDBProperties* pIDBProperties = NULL;  
IDBCreateSession* pIDBCreateSession = NULL;  
IDBCreateCommand* pIDBCreateCommand = NULL;  
ICommandText* pICommandText = NULL;  
IRowset* pIRowset = NULL;  
IColumnsInfo* pIColumnsInfo = NULL;  
ISequentialStream* pISequentialStream;  
  
DBCOLUMNINFO* pDBColumnInfo = NULL;  
IAccessor* pIAccessor =  NULL;  
DBPROP InitProperties[4];  
DBPROPSET rgInitPropSet[1];  
  
ULONG i, j;  
HRESULT hr;  
DBROWCOUNT cNumRows = 0;  
DBORDINAL lNumCols;  
WCHAR* pStringsBuffer;  
DBBINDING* pBindings;  
HACCESSOR hAccessor;  
DBCOUNTITEM lNumRowsRetrieved;  
HROW hRows[10];  
HROW* pRows = &hRows[0];  
  
int main() {  
   // The command to execute.  
   WCHAR* wCmdString = OLESTR("SELECT ID, LargeString FROM dbo.LargeStringUDTs");  
  
   // Call a function to initialize and establish connection.   
   if (InitializeAndEstablishConnection() == -1) {  
      cout << "Failed to initialize and connect to the server.\n";  
      return -1;  
   }  
  
   // Create a session   
   if (FAILED(pIDBInitialize->QueryInterface( IID_IDBCreateSession, (void**) &pIDBCreateSession))) {  
         cout << "Failed to obtain IDBCreateSession interface.\n";  
         return -1;  
   }  
  
   if (FAILED(pIDBCreateSession->CreateSession( NULL, IID_IDBCreateCommand, (IUnknown**) &pIDBCreateCommand))) {  
         cout << "pIDBCreateSession->CreateSession failed.\n";  
         return -1;  
   }  
  
   // Access the ICommandText interface.  
   if (FAILED(pIDBCreateCommand->CreateCommand( NULL, IID_ICommandText, (IUnknown**) &pICommandText))) {  
         cout << "Failed to access ICommand interface.\n";  
         return -1;  
   }  
  
   // Specify the command text.  
   if (FAILED(pICommandText->SetCommandText(DBGUID_DBSQL, wCmdString))) {  
      cout << "Failed to set command text.\n";  
      return -1;  
   }  
  
   // Execute the command.  
   if (FAILED(hr = pICommandText->Execute( NULL, IID_IRowset, NULL, &cNumRows, (IUnknown **) &pIRowset))) {  
         cout << "Failed to execute command.\n";  
         return -1;  
   }  
  
   // Process the result set.  
   ProcessResultSet();   
  
   pIRowset->Release();  
  
   // release memory.  
   pICommandText->Release();  
   pIDBCreateCommand->Release();  
   pIDBCreateSession->Release();  
  
   if (FAILED(pIDBInitialize->Uninitialize())) {  
      // Uninitialize is not required, but it fails if an interface has not been released.  This can be used for debugging.  
      cout << "Problem uninitializing.\n";  
   }  
  
   pIDBInitialize->Release();  
   CoUninitialize();  
};  
  
int InitializeAndEstablishConnection() {      
   CoInitialize(NULL);  
  
   // Obtain access to the SQLNCLI provider.  
   hr = CoCreateInstance( CLSID_SQLNCLI11, NULL, CLSCTX_INPROC_SERVER, IID_IDBInitialize, (void **) &pIDBInitialize);  
  
   if (FAILED(hr)) {  
      printf("Failed to get IDBInitialize interface.\n");  
      return -1;  
   }  
  
   // Initialize the property values needed to establish the connection.  
   for ( i = 0 ; i < 4 ; i++ )  
      VariantInit(&InitProperties[i].vValue);  
  
   // Server name.  
   InitProperties[0].dwPropertyID = DBPROP_INIT_DATASOURCE;  
   InitProperties[0].vValue.vt = VT_BSTR;  
   //InitProperties[0].vValue.bstrVal= SysAllocString(L"(local)\\SQLExpress");  
   InitProperties[0].vValue.bstrVal= SysAllocString(L"(local)");  
   InitProperties[0].dwOptions = DBPROPOPTIONS_REQUIRED;  
   InitProperties[0].colid = DB_NULLID;  
  
   // Database.  
   InitProperties[1].dwPropertyID = DBPROP_INIT_CATALOG;  
   InitProperties[1].vValue.vt = VT_BSTR;  
   InitProperties[1].vValue.bstrVal = SysAllocString(L"master");  
   InitProperties[1].dwOptions = DBPROPOPTIONS_REQUIRED;  
   InitProperties[1].colid = DB_NULLID;  
  
   InitProperties[2].dwPropertyID = DBPROP_AUTH_INTEGRATED;  
   InitProperties[2].vValue.vt = VT_BSTR;  
   InitProperties[2].vValue.bstrVal = SysAllocString(L"SSPI");  
   InitProperties[2].dwOptions = DBPROPOPTIONS_REQUIRED;  
   InitProperties[2].colid = DB_NULLID;  
  
   // Properties are set, now construct the DBPROPSET structure (rgInitPropSet) used to pass   
   // an array of DBPROP structures (InitProperties) to the SetProperties method.  
   rgInitPropSet[0].guidPropertySet = DBPROPSET_DBINIT;  
   rgInitPropSet[0].cProperties = 4;  
   rgInitPropSet[0].rgProperties = InitProperties;  
  
   // Set initialization properties.  
   hr = pIDBInitialize->QueryInterface(IID_IDBProperties, (void **)&pIDBProperties);  
   if (FAILED(hr)) {  
      cout << "Failed to get IDBProperties interface.\n";  
      return -1;  
   }  
  
   hr = pIDBProperties->SetProperties(1, rgInitPropSet);   
   if (FAILED(hr)) {  
      cout << "Failed to set initialization properties.\n";  
      return -1;  
   }  
  
   pIDBProperties->Release();  
  
   // Now establish the connection to the data source.  
   if (FAILED(pIDBInitialize->Initialize())) {  
      cout << "Problem in establishing connection to the data"  
         "source.\n";  
      return -1;  
   }  
   return 0;  
}  
  
// Retrieve and display data resulting from a query.  
int ProcessResultSet() {  
   // Obtain access to the IColumnInfo interface  
   hr = pIRowset->QueryInterface(IID_IColumnsInfo, (void **)&pIColumnsInfo);  
   if (FAILED(hr)) {  
      cout << "Failed to get IColumnsInfo interface.\n";  
      return -1;  
   }   
  
   // Retrieve the column information.  
   pIColumnsInfo->GetColumnInfo(&lNumCols, &pDBColumnInfo, &pStringsBuffer);  
  
   // Free the columninfo interface.  
   pIColumnsInfo->Release();  
  
   // Create a DBBINDING array.  
   DBBINDING * p = (pBindings = new DBBINDING[lNumCols]);  
   if (!(p /* pBindings = new DBBINDING[lNumCols] */ ))  
      return -1;  
  
   // There are two columns in the table.  
   pBindings[0].iOrdinal = 1;   
   pBindings[0].obValue = 0;  
   pBindings[0].obLength = 0;  
   pBindings[0].obStatus = 0;  
   pBindings[0].pTypeInfo = NULL;  
   pBindings[0].pObject = NULL;  
   pBindings[0].pBindExt = NULL;  
   pBindings[0].dwPart = DBPART_VALUE | DBPART_LENGTH | DBPART_STATUS;  
   pBindings[0].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
   pBindings[0].eParamIO = DBPARAMIO_NOTPARAM;   // Count 10  
   pBindings[0].cbMaxLen = sizeof(long);  
   pBindings[0].dwFlags = 0;  
   pBindings[0].wType = DBTYPE_I4;  
   pBindings[0].bPrecision = 0;  
   pBindings[0].bScale = 0; //Count 15  
  
   pBindings[1].iOrdinal = 2;   
   pBindings[1].obValue = 0;  
   pBindings[1].obLength = 0;  
   pBindings[1].obStatus = 0;  
   pBindings[1].pTypeInfo = NULL;  
   pBindings[1].pObject = NULL;  
   pBindings[1].pBindExt = NULL;  
   pBindings[1].dwPart = DBPART_VALUE | DBPART_STATUS;  
   pBindings[1].dwMemOwner = DBMEMOWNER_CLIENTOWNED;  
   pBindings[1].eParamIO = DBPARAMIO_NOTPARAM; //Count 10  
   pBindings[1].cbMaxLen = sizeof(IUnknown*);  
   pBindings[1].dwFlags = 0;  
   pBindings[1].wType = DBTYPE_IUNKNOWN;  
   pBindings[1].bPrecision = 0;  
   pBindings[1].bScale = 0; //Count 15  
  
   DBBYTEOFFSET rowSize = 0;  
  
   for (size_t i = 0; i < lNumCols; i++) {  
      pBindings[i].obLength = rowSize + offsetof(COLUMNDATA, dwLength);  
      pBindings[i].obStatus = rowSize + offsetof(COLUMNDATA, dwStatus);  
      pBindings[i].obValue  = rowSize + offsetof(COLUMNDATA, rgbData);  
  
      rowSize += offsetof(COLUMNDATA, rgbData) + pBindings[i].cbMaxLen;  
      rowSize  = ROUND_UP(rowSize);  
   }  
  
   hr = pIRowset->QueryInterface(IID_IAccessor, (void **) &pIAccessor);  
   if (FAILED(hr)) {  
      cout << "Failed to obtain IAccessor interface.\n";  
      return -1;  
   }  
  
   // Create an accessor from the set of bindings (pBindings).  
   pIAccessor->CreateAccessor(DBACCESSOR_ROWDATA, lNumCols, pBindings, 0, &hAccessor, NULL);  
  
   // Print column names.  
   for ( j = 0 ; j < lNumCols ; j++ )  
      printf("%-30S", pDBColumnInfo[j].pwszName);  
  
   printf("\n");   // new line after the column names  
  
   // Get a set of 10 row at a time.  
   pIRowset->GetNextRows( NULL, 0, 10, &lNumRowsRetrieved, &pRows);  
  
   // Allocate space for the row buffer.  
   BYTE * pBuffer = new BYTE[rowSize];  
   if (!(pBuffer /* = new BYTE[rowSize]; */ )) {  
      // Free up all allocated memory.  
      pIAccessor->ReleaseAccessor(hAccessor, NULL);  
      pIAccessor->Release();  
      delete [] pBindings;  
      return 0;  
   }  
  
   // Display the rows.  
   while ( lNumRowsRetrieved > 0 ) {  
      // For each row, print the column data.  
      for ( j = 0 ; j < lNumRowsRetrieved ; j++ ) {  
         // Clear the buffer.  
         memset(pBuffer, 0, rowSize);  
  
         // Get the row data values.  
         pIRowset->GetData(hRows[j], hAccessor, pBuffer);  
  
         // Print the first column  
         printf("%-25d", *((long*)(*(&pBuffer) + pBindings[0].obValue)));  
         ULONG dwStatus = *((ULONG*) (pBuffer + pBindings[1].obStatus));  
  
         if (dwStatus == DBSTATUS_S_ISNULL) {  
            // Process NULL data  
         }  
  
         else if (dwStatus == DBSTATUS_S_OK) {  
            HRESULT hrStreamRead = S_OK;  
            ULONG cbRead = 0;  
            BYTE DataBuff[1024];  
  
            memset(DataBuff, 0, 1024);  
  
            pISequentialStream = *((ISequentialStream**)(pBuffer + pBindings[1].obValue));  
  
            do {  
               hrStreamRead = pISequentialStream->Read(DataBuff, sizeof(DataBuff), &cbRead);  
               if (SUCCEEDED(hrStreamRead)) {  
                  // First byte indicate the value for IsNull property and the next four bytes   
                  // indicate the length of the string. So we start from the fifth byte.  
                  for (ULONG i = 5; i < cbRead; i++)  
                     putchar((char)DataBuff[i]);  
  
                  printf("\n");  
               }  
            }  
            while (hrStreamRead != S_FALSE && cbRead == sizeof(DataBuff));  
  
            pISequentialStream->Release();  
         }  
         else  
            // Process error from GetData.  
            cout << "Failed to GetData.\n";  
  
      } // for  
  
      // Release the rows retrieved.  
      pIRowset->ReleaseRows(lNumRowsRetrieved, hRows, NULL, NULL, NULL);  
  
      // Get the next 10 rows.  
      pIRowset->GetNextRows(NULL, 0, 10, &lNumRowsRetrieved, &pRows);  
   } // while  
  
   // Free up all allocated memory.  
   delete [] pBuffer;  
   pIAccessor->ReleaseAccessor(hAccessor, NULL);  
   pIAccessor->Release();  
   delete [] pBindings;  
  
   return 0;  
}  
```  
  
```  
USE [MASTER]  
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'LargeStringUDTs')  
   DROP TABLE LargeStringUDTs  
GO  
  
IF EXISTS (SELECT * FROM sys.types WHERE name = 'LargeStringUDT')  
   DROP TYPE dbo.LargeStringUDT  
GO  
  
IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'LargeStringUDT')  
   DROP ASSEMBLY LargeStringUDT  
GO  
```  
  
  
