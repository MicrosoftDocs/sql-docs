---
title: "Fetching BLOB Data Using IRow::Open and ISequentialStream | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "fetching BLOB data"
  - "Open method"
  - "ISequentialStream interface"
  - "BLOBs, fetching"
ms.assetid: 439b3976-84e7-4d11-8dba-f668adbc9159
author: MightyPen
ms.author: genemi
manager: craigg
---
# Fetching BLOB Data Using IRow::Open and ISequentialStream
  **IRow::Open** supports only DBGUID_STREAM and DBGUID_NULL type of objects to be opened.  
  
 The following function uses **IRow::Open** and **ISequentialStream** to fetch large data.  
  
```  
void InitializeAndExecuteCommand()  
{  
    ULONG iidx;  
    WCHAR* wCmdString=OLESTR("SELECT * FROM MyTable");  
    // Do the initialization, create the session, and set command text.  
    hr = pICommandText->Execute(NULL, IID_IRow, NULL,   
                         &cNumRows,(IUnknown **)&pIRow)))  
    //Get 1 column at a time.  
    for(ULONG i=1; i <= NoOfColumns; i++)  
      GetSequentialColumn(pIRow, iidx);  
    // Do the clean up.  
}  
HRESULT GetSequentialColumn(IRow* pUnkRow, ULONG iCol)  
{  
    HRESULT hr = NOERROR;  
    ULONG cbRead = 0;  
    ULONG cbTotal = 0;  
    ULONG cColumns = 0;  
    ULONG cReads = 0;  
    ISequentialStream* pIStream = NULL;  
    WCHAR* pBuffer[kMaxBuff]; //50 chars read by ISequentialStream::Read()  
    DBCOLUMNINFO* prgInfo;  
    OLECHAR* pColNames;  
    IColumnsInfo* pIColumnsInfo;  
    DBID columnid;  
    DBCOLUMNACCESS column;  
  
    hr = pUnkRow->QueryInterface(IID_IColumnsInfo,   
                            (void**) &pIColumnsInfo);  
    hr = pIColumnsInfo->GetColumnInfo(&cColumns, &prgInfo, &pColNames);  
    // Get Column ID.  
    columnid = (prgInfo + (iCol - 1))->columnid;  
    // Get sequential stream object by calling IRow::Open.  
    hr = pUnkRow->Open(NULL, &columnid, DBGUID_STREAM, 0,   
                    IID_ISequentialStream,(LPUNKNOWN *)&pIStream);  
    ZeroMemory(pBuffer, kMaxBuff * sizeof(WCHAR));  
    // Read 50 chars at a time until no more data.  
    do  
    {  
        hr = pIStream->Read(pBuffer, kMaxBuff, &cbRead);  
        cbTotal = cbTotal + cbRead;  
        // Process the data.  
    } while(cbRead > 0);  
// Do the clean up.  
    return hr;  
}  
```  
  
 Large data can be bound or retrieved by using the **ISequentialStream** interface. For bound columns, the status flag indicates if the data is truncated by setting DBSTATUS_S_TRUNCATED.  
  
## See Also  
 [Fetching BLOB Data Using IRow](../../database-engine/dev-guide/fetching-blob-data-using-irow.md)  
  
  
