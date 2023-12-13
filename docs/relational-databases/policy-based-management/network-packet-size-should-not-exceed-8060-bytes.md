---
title: "Network Packet Size Should Not Exceed 8060 Bytes"
description: "Network Packet Size Should Not Exceed 8060 Bytes"
author: VanMSFT
ms.author: vanto
ms.date: 12/13/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Network packet size should not exceed 8060 bytes

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

If the value specified for `sp_configure` 'network packet size' or if the network packet size of any logged-in user is more than 8060 bytes, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] performs different memory allocation operations. This can cause an increase in the process virtual address space that is not reserved for the buffer pool.

## Best practices recommendations

The network packet size should not exceed 8060 bytes.

## For more information

[Microsoft Knowledge Base article 903002](https://www.betaarchive.com/wiki/index.php?title=Microsoft_KB_Archive/903002)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
