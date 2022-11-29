---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 06/22/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: include
---
Beginning with SQL Server 2019 CU5, when you deploy a new cluster with basic authentication all endpoints including gateway use `AZDATA_USERNAME` and `AZDATA_PASSWORD`. Endpoints on clusters that are upgraded to CU5 continue to use `root` as username to connect to gateway endpoint. This change does not apply to deployments using Active Directory authentication. See [Credentials for accessing services through gateway endpoint](../big-data-cluster/release-notes-big-data-cluster.md#credentials-for-accessing-services-through-gateway-endpoint) in the release notes.