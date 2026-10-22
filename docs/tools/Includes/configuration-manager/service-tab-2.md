---
author: cwebb-msft
ms.author: chweb
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: include
ms.collection:
  - data-tools
---
#### Name

Indicates the display name of the service.

#### Process ID

Displays the number used by Windows to keep track of this program's processes.

#### SQL Service Type

Displays the type of service provided to calling processes. [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] installs several services.

#### Start Mode

Set this service to the following choices:

- **Manual**: This service doesn't automatically start when the computer starts. You must start the service using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Configuration Manager, or some other tool.

- **Automatic**: This service attempts to start when this computer starts.

- **Disabled**: This service can't be started.

#### State

Indicates whether this service is running, stopped, or disabled.

## Related content

- [SQL Server Services](../../configuration-manager/sql-server-services.md)
