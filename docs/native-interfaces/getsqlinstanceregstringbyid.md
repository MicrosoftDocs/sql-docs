---
description: "GetSQLInstanceRegStringByID() "
title: "GetSQLInstanceRegStringByID()  | Microsoft Docs"
ms.custom: ""
ms.date: "06/18/2021"
ms.service: sql
ms.reviewer: ""
apiname: 
  - "GetSQLInstanceRegStringByID"
apilocation: 
  - "instapi.dll"
apitype: "DllExport"
ms.topic: reference
author: MikeRayMSFT
ms.author: mikeray
---

# GetSQLInstanceRegStringByID()

Retrieves a registry string from an instance specific registry tree by the given instance ID, the subtree, and the name of the value.

This article describes a native code API that is used by SQL Server and may also be called by other Microsoft products. For a managed code method, see [InstAPI.GetSvcInstanceRegStringByName Method](/dotnet/api/microsoft.sqlserver.instapi.getsvcinstanceregstringbyname).

```c
GetSQLInstanceRegStringByID(
    PINST_ID  pInstanceID,
    LPCWSTR   sRegPath,
    LPCWSTR   sValueName,
    LPWSTR    sString,
    PDWORD    pdwSize
    );
```

## Arguments

*pInstanceID*[in]

A pointer to the instance ID of the relevant instance.

*sRegPath*[in]

A pointer to registry path of the string to retrieve.

*sValueName*[in]

A pointer to a string that contains the value name to retrieve.

*sString*[out]

A pointer to a buffer to receive the string that will be retrieved.

*pdwSize*[out]

A pointer to a `DWORD` value to supply the length of the buffer. Returns the required length if the supplied buffer is too small.

## Returns

Boolean
   `true` if the call succeeded; otherwise, `false`.
