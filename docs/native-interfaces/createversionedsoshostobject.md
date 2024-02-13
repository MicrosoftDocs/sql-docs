---
title: "CreateVersionedSOSHostObject()"
description: "CreateVersionedSOSHostObject()"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "02/06/2024"
ms.service: sql
ms.topic: reference
topictype: apiref
apilocation: "sqlos.dll"
apiname: "CreateVersionedSOSHostObject"
apitype: "DllExport"
---

# CreateVersionedSOSHostObject()

Creates a versioned object for the root hosting interface.

This article describes a native code API that is used by SQL Server and may also be called by other Microsoft products.

```c
CreateVersionedSOSHostObject(
    REFIID                 interfacIid, 
    const SOSHOST_CLIENTID clientId,    
    const PCWSTR           szClientName
    );
```

## Parameters

*interfacIid*[in]

Versioned interface identifier.

*clientId*[in]

Unique client identifier.

*scClientName*[in]

Hosting client name.

## Returns

*ppHost*

Host object returned.
