---
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/11/2026
ms.service: sql
ms.topic: include
---
> [!WARNING]  
> SQL Server Agent runs Transact-SQL job steps in the job owner's security context. If you change *@owner_login_name*, those steps run under the new owner's permissions.