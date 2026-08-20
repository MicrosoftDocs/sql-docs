---
author: MashaMSFT
ms.service: azure-arc
ms.topic: include
ms.date: 08/20/2026
ms.author: mathoma
---

Starting with Azure Extension for SQL Server version `1.1.3453.436`, least privileged configuration is applied by default.

Existing servers with extension version `1.1.2859.223` or greater but earlier than `1.1.3453.436` will eventually have the least privileged configuration applied. To prevent the automatic application of least privilege, block extension upgrades after `1.1.2859.223`.
