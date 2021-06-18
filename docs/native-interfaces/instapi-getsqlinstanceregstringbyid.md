---
description: "InstAPI.GetSQLInstanceRegStringByID(INST_ID, String, String, StringBuilder, UInt32) "
title: "InstAPI.GetSQLInstanceRegStringByID(INST_ID, String, String, StringBuilder, UInt32)  | Microsoft Docs"
ms.custom: ""
ms.date: "06/30/2021"
ms.prod: sql
ms.reviewer: ""
apiname: 
  - "define"
apilocation: 
  - "define"
apitype: "define"
ms.topic: reference
author: MikeRayMSFT
ms.author: mikeray
---

# InstAPI.GetSQLInstanceRegStringByID() method

Retrieves a registry string from an instance specific registry tree by the given instance ID, the subtree, and the name of the value.

```csharp
__success(return == TRUE)
BOOL
WINAPI
GetSQLInstanceRegStringByID(
    __in IN                     PINST_ID  pInstanceID,
    IN                          LPCWSTR   sRegPath,
    IN                          LPCWSTR   sValueName,
    __out_ecount(*pdwSize) OUT  LPWSTR    sString,
    __inout IN OUT              PDWORD    pdwSize
    );
```

## Parameters
`pInstanceID` INST_ID

A pointer to the instance ID of the relevant instance.

`sRegPath` String

A pointer to registry path of the string to retrieve

`sValueName` String

A pointer to a string that contains the value name to retrieve.

`sString` String

A pointer to a buffer to receive the string that will be retrieved.

`pdwSize` PDWORD

A pointer to a `DWORD` value to supply the length of the buffer. Returns the required length if the supplied buffer is too small.

## Returns

Boolean
   `true` if the call succeeded; otherwise, `false`.