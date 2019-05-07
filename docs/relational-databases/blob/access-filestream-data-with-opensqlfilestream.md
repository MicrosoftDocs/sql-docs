---
title: "Access FILESTREAM Data with OpenSqlFilestream | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
apiname: 
  - "OpenSqlFilestream"
apilocation: 
  - "sqlncli11.dll"
apitype: "DLLExport"
helpviewer_keywords: 
  - "OpenSqlFilestream"
ms.assetid: d8205653-93dd-4599-8cdf-f9199074025f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Access FILESTREAM Data with OpenSqlFilestream
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The OpenSqlFilestream API obtains a Win32 compatible file handle for a FILESTREAM binary large object (BLOB) stored in the file system. The handle can be passed to any of the following Win32 APIs: [ReadFile](https://go.microsoft.com/fwlink/?LinkId=86422), [WriteFile](https://go.microsoft.com/fwlink/?LinkId=86423), [TransmitFile](https://go.microsoft.com/fwlink/?LinkId=86424), [SetFilePointer](https://go.microsoft.com/fwlink/?LinkId=86425), [SetEndOfFile](https://go.microsoft.com/fwlink/?LinkId=86426), or [FlushFileBuffers](https://go.microsoft.com/fwlink/?LinkId=86427). If you pass this handle to any other Win32 API, the error ERROR_ACCESS_DENIED is returned. The handle must be closed by passing it to the Win32 [CloseHandle](https://go.microsoft.com/fwlink/?LinkId=86428) API before the transaction is committed or rolled back. Failing to close the handle will cause server-side resource leaks.  
  
 You must perform All FILESTREAM data container access in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction. [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can also be executed in the same transaction. This maintains consistency between the SQL data and FILESTREAM BLOB data.  
  
 To access the FILESTREAM BLOB by using Win32, [Windows Authorization](../../relational-databases/security/choose-an-authentication-mode.md) must be enabled.  
  
> [!IMPORTANT]  
>  When the file is opened for write access, the transaction is owned by the FILESTREAM agent. Only Win32 file I/O is allowed until the transaction is released. To release the transaction, the write handle must be closed.  
  
## Syntax  
  
```  
  
HANDLE OpenSqlFilestream (  
    LPCWSTR FilestreamPath,  
    SQL_FILESTREAM_DESIRED_ACCESS DesiredAccess,  
    ULONG OpenOptions,  
    LPBYTE FilestreamTransactionContext,  
    SIZE_T FilestreamTransactionContextLength,  
    PLARGE_INTEGER AllocationSize);  
```  
  
#### Parameters  
 *FilestreamPath*  
 [in] Is the **nvarchar(max)** path that is returned by the [PathName](../../relational-databases/system-functions/pathname-transact-sql.md) function. PathName must be called from the context of an account that has [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SELECT or UPDATE permissions on the FILESTREAM table and column.  
  
 *DesiredAccess*  
 [in] Sets the mode used to access FILESTREAM BLOB data. This value is passed to the [DeviceIoControl Function](https://go.microsoft.com/fwlink/?LinkId=105527).  
  
|Name|Value|Meaning|  
|----------|-----------|-------------|  
|SQL_FILESTREAM_READ|0|Data can be read from the file.|  
|SQL_FILESTREAM_WRITE|1|Data can be written to the file.|  
|SQL_FILESTREAM_READWRITE|2|Data can be read and written from the file.|  
  
> [!NOTE]  
>  These values are defined in the SQL_FILESTREAM_DESIRED_ACCESS enumeration in sqlncli.h.  
  
 *OpenOptions*  
 [in] The file attributes and flags. This parameter can also include any combination of the following flags.  
  
|Flag|Value|Meaning|  
|----------|-----------|-------------|  
|SQL_FILESTREAM_OPEN_NONE|0x00000000:|The file is being opened or created with no special options.|  
|SQL_FILESTREAM_OPEN_FLAG_ASYNC|0x00000001L|The file is being opened or created for asynchronous I/O.|  
|SQL_FILESTREAM_OPEN_FLAG_NO_BUFFERING|0x00000002L|The system opens the file by using no system caching.|  
|SQL_FILESTREAM_OPEN_FLAG_NO_WRITE_THROUGH|0x00000004L|The system does not write through an intermediate cache. Writes go directly to disk.|  
|SQL_FILESTREAM_OPEN_FLAG_SEQUENTIAL_SCAN|0x00000008L|A file is accessed sequentially from beginning to end. The system can use this as a hint to optimize file caching. If an application moves the file pointer for random access, optimal caching may not occur.|  
|SQL_FILESTREAM_OPEN_FLAG_RANDOM_ACCESS|0x00000010L|A file is accessed randomly. The system can use this as a hint to optimize file caching.|  
  
 *FilestreamTransactionContext*  
 [in] The value that is returned by the [GET_FILESTREAM_TRANSACTION_CONTEXT](../../t-sql/functions/get-filestream-transaction-context-transact-sql.md) function.  
  
 *FilestreamTransactionContextLength*  
 [in] Number of bytes in the **varbinary(max)** data that is returned by the GET_FILESTREAM_TRANSACTION_CONTEXT function. The function returns an array of N bytes. N is determined by the function and is a property of the byte array that is returned.  
  
 *AllocationSize*  
 [in] Specifies the initial allocation size of the data file in bytes. It is ignored in read mode. This parameter can be NULL, in which case the default file system behavior is used.  
  
## Return Value  
 If the function succeeds, the return value is an open handle to a specified file. If the function fails, the return value is INVALID_HANDLE_VALUE. For extended error information, call GetLastError().  
  
## Examples  
 The following examples show you how to use the `OpenSqlFilestream` API to obtain a Win32 handle.  
  
 [!code-cs[FILESTREAM#FS_CS_ReadAndWriteBLOB](../../relational-databases/blob/codesnippet/csharp/access-filestream-data-w_0_1.cs)]  
  
 [!code-vb[FILESTREAM#FS_VB_ReadAndWriteBLOB](../../relational-databases/blob/codesnippet/visualbasic/access-filestream-data-w_0_2.vb)]  
  
 [!code-cpp[FILESTREAM#FS_CPP_WriteBLOB](../../relational-databases/blob/codesnippet/cpp/access-filestream-data-w_0_3.cpp)]  
  
## Remarks  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client must be installed to use this API. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is installed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client tools. For more information, see [Installing SQL Server Native Client](../../relational-databases/native-client/applications/installing-sql-server-native-client.md).  
  
## See Also  
 [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md)   
 [Make Partial Updates to FILESTREAM Data](../../relational-databases/blob/make-partial-updates-to-filestream-data.md)   
 [Avoid Conflicts with Database Operations in FILESTREAM Applications](../../relational-databases/blob/avoid-conflicts-with-database-operations-in-filestream-applications.md)  
  
  
