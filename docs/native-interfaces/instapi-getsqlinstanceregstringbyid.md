---
description: "InstAPI.GetSQLInstanceRegStringByID(INST_ID, String, String, StringBuilder, UInt32) "
title: "InstAPI.GetSQLInstanceRegStringByID(INST_ID, String, String, StringBuilder, UInt32)  | Microsoft Docs"
ms.custom: ""
ms.date: "06/30/2021"
ms.prod: sql
ms.reviewer: ""
ms.topic: reference
author: MikeRayMSFT
ms.author: mikeray
---

# InstAPI.GetSQLInstanceRegStringByID(INST_ID, String, String, StringBuilder, UInt32) method

Retrieves a registry string from an instance specific registry tree by the given instance ID, the subtree, and the name of the value.

```csharp
public static bool GetSQLInstanceRegStringByID (Microsoft.SqlServer.INST_ID instanceID, string registryPath, string valueName, System.Text.StringBuilder data, ref uint bufferSize);
```

## Parameters
`instanceID` INST_ID

A pointer to the instance ID of the relevant instance.

`registryPath` String

A pointer to a buffer to receive the registry path.

`valueName` String

A String value that contains the value name to retrieve.

`data` StringBuilder

A pointer to a buffer to receive the string that will be retrieved.

`bufferSize` UInt32

A pointer to a `DWORD` value to supply the length of the buffer. Returns the required length if the supplied buffer is too small.

## Returns

Boolean
   `true` if the call succeeded; otherwise, `false`.