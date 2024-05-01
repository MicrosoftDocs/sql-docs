---
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 03/08/2024
ms.topic: include
---

> [!IMPORTANT]  
> Azure Arc automatically installs the Azure extension for SQL Server when a server connected to Azure Arc has SQL Server installed. All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Server instances.
>
> To automatically connect your SQL Server instances, see [Automatically Connect your SQL Server to Azure Arc](../automatically-connect.md).
>
> Use the method in this article, if your server is already connected to Azure, but Azure extension for SQL Server is not deployed automatically.
>
> An `ArcSQLServerExtensionDeployment = Disabled` tag is created on the Arc machine resource if the extension is deployed using this method.