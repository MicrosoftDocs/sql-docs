---
description: "GetSvcInstanceIDFromName"
title: "GetSvcInstanceIDFromName | Microsoft Docs"
ms.custom: ""
ms.date: "06/30/2021"
ms.prod: sql
ms.reviewer: ""
apiname: 
  - "GetSvcInstanceIDFromName"
apilocation: 
  - "instapi.dll"
apitype: "DllExport"
ms.topic: reference
author: MikeRayMSFT
ms.author: mikeray
---

# GetSvcInstanceIDFromName()

Retrieves the instance ID of an instance when given the friendly name, conditioned to search only the instance maps for the given service.

```c
GetSvcInstanceIDFromName(
    LPCWSTR     sInstanceName,
    SQL_SVCS    Service,
    PINST_ID    pInstanceID
    );
```

## Parameters

*sInstanceName*[in]

A pointer to the instance ID of the relevant instance.

*Service*[in]

The service type of the server whose instance ID is being looked for.

*pInstanceID*[out]

A pointer to a buffer to receive the instance ID.

## Returns

Boolean
   `true` if the call succeeded; otherwise, `false`.