---
description: "Fetching BLOB Data Using IRow::GetColumns and ISequentialStream"
title: "BLOB, IRow::GetColumns, ISequentialStream"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "fetching BLOB data"
  - "ISequentialStream interface"
  - "GetColumns method"
  - "BLOBs, fetching"
ms.assetid: b57decda-b0c1-4ef6-8c81-491956de2890
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Fetching BLOB Data Using IRow::GetColumns and ISequentialStream
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The following function uses **IRow::GetColumns** and **ISequentialStream** to fetch large data:  
  
```cpp
void InitializeAndExecuteCommand()  
{  
    ULONG iidx;  
    WCHAR* wCmdString=OLESTR("SELECT * FROM MyTable");  
    // Do the initialization, create the session, and set command text.  
    hr = pICommandText->Execute(NULL, IID_IRow, NULL,   
                         &cNumRows,(IUnknown **)&pIRow)))  
    //Get 1 column at a time.  
    for(ULONG i=0; i < NoOfColumns; i++)  
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
    if(FAILED(hr))  
        goto CLEANUP;  
    hr = pIColumnsInfo->GetColumnInfo(&cColumns, &prgInfo, &pColNames);  
    // Get Column ID.  
    columnid = (prgInfo + (iCol))->columnid;  
    IUnknown* pUnkStream = NULL;  
    ZeroMemory(&column, sizeof(column));  
    column.columnid = prgInfo[iCol].columnid;  
    // Ask for Iunknown interface pointer.  
    column.wType = DBTYPE_IUNKNOWN;  
    column.pData = (LPVOID*) &pUnkStream;  
  
    hr = pUnkRow->GetColumns(1, &column);  
    // Get ISequentialStream from Iunknown pointer retrieved from  
    // GetColumns().  
    hr = pUnkStream->QueryInterface(IID_ISequentialStream,   
                                   (LPVOID*) &pIStream);  
    ZeroMemory(pBuffer, kMaxBuff * sizeof(WCHAR));  
    // Read 50 chars at a time until no more data.  
    do  
    {  
        hr = pIStream->Read(pBuffer, kMaxBuff, &cbRead);  
        cbTotal = cbTotal + cbRead;  
        // Process the data.  
    } while(cbRead > 0);  
  // Do the cleanup.  
    return hr;  
}  
```  
  
## See Also  
 [Fetching BLOB Data Using IRow](./fetching-a-single-row-with-irow.md)  
  
