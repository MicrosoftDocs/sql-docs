---
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.topic: include
---
> [!IMPORTANT]  
> Possible elevation of credentials: Users in the **PolicyAdministratorRole** role can create server triggers and schedule policy executions that can affect the operation of the instance of the [!INCLUDE [ssde-md](../../../includes/ssde-md.md)]. For example, users in the **PolicyAdministratorRole** role can create a policy that can prevent most objects from being created in the [!INCLUDE [ssde-md](../../../includes/ssde-md.md)]. Because of this possible elevation of credentials, the **PolicyAdministratorRole** role should be granted only to users who are trusted with controlling the configuration of the [!INCLUDE [ssde-md](../../../includes/ssde-md.md)].
