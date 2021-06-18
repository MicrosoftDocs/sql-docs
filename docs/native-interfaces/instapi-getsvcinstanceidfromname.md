---
description: "InstAPI.GetSvcInstanceIDFromName"
title: "InstAPI.GetSvcInstanceIDFromName | Microsoft Docs"
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

# InstAPI.GetSvcInstanceIDFromName() method

Retrieves the instance ID of an instance when given the friendly name, conditioned to search only the instance maps for the given service.

```csharp
BOOL
WINAPI
    GetSvcInstanceIDFromName(
    IN      LPCWSTR     sInstanceName,
    IN      SQL_SVCS    Service,
    OUT     PINST_ID    pInstanceID
    );
```

## Parameters

`sInstanceName` LPCWSTR

A pointer to the instance ID of the relevant instance.

`Service` SQL_SVCS

The service type of the server whose instance ID is being looked for.

`pInstanceID` PINST_ID

A pointer to a buffer to receive the instance ID.

## Returns

Boolean
   `true` if the call succeeded; otherwise, `false`.