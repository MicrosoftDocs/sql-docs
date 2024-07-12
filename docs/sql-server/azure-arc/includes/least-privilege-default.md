---
author: MikeRayMSFT
ms.service: azure-arc
ms.topic: include
ms.date: 07/11/2024
ms.author: mikeray
---

Beginning with the August 2024 release, any extensions deployed via auto-onboarding will automatically have the least privilege configuration.

Existing servers with extension `1.1.2717.190` (June 2024) or later will have least privileged configuration automatically applied. This application will happen gradually.

To prevent automatic application of least privilege, block extension upgrades to version `1.1.2717.190` or later.
