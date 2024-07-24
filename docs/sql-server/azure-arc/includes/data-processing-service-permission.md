---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 07/23/2024
ms.topic: include
---

Arc-enabled [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] requires outbound connection to Azure Arc Data Processing Service.

Each virtual or physical server needs to communicate with Azure. Specifically, they require connectivity to:

- URL: `*.<region>.arcdataservices.com`
- Port: 443
- Direction: Outbound
- Authentication provider: Entra ID

To get the region segment of a regional endpoint, remove all spaces from the Azure region name. For example, *East US 2* region, the region name is `eastus2`.

For example: `*.<region>.arcdataservices.com` should be `*.eastus2.arcdataservices.com` in the East US 2 region.

For a list of supported regions, review [Supported Azure regions](../overview.md#supported-azure-regions).

For a list of all regions, run this command:

```azcli
az account list-locations -o table
```
