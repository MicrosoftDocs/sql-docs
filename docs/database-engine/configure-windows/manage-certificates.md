---
title: Certificate management (SQL Server Configuration Manager)
description: Learn how to install certificates in various SQL Server configurations. Examples include single instances, failover clusters, and Always On availability groups.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/20/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "connections [SQL Server], encrypted"
  - "SSL [SQL Server]"
  - "Secure Sockets Layer (SSL)"
  - "encryption [SQL Server], connections"
  - "cryptography [SQL Server], connections"
  - "certificates [SQL Server], installing"
  - "requesting encrypted connections"
  - "installing certificates"
  - "security [SQL Server], encryption"
---
# Certificate management (SQL Server Configuration Manager)

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

This article describes how to deploy and manage certificates across your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Always On failover cluster instance (FCI) or availability group (AG) topology.

SSL/TLS certificates are widely used to secure access to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. With earlier versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], organizations with large [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] estates had to spend considerable effort to maintain their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] certificate infrastructure, often through developing scripts and running manual commands.

With [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, certificate management is integrated into the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, which simplifies the following common tasks:

- View and validate certificates installed in a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.
- Identify which certificates might be close to expiring.
- Deploy certificates across AG machines from the node holding the primary replica.
- Deploy certificates across FCI machines from the active node.

You can use certificate management in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager with earlier versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], starting with [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)].

::: moniker range=">=sql-server-ver15"

> [!NOTE]  
> These instructions apply to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. For [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and earlier versions, see [Certificate management (SQL Server 2017 Configuration Manager)](manage-certificates.md?view=sql-server-2017&preserve-view=true).

::: moniker-end

::: moniker range="<= sql-server-2017"

> [!NOTE]  
> These instructions apply to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and earlier versions. For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, see [Certificate management (SQL Server 2019 Configuration Manager)](manage-certificates.md?view=sql-server-ver15&preserve-view=true).

::: moniker-end

## <a id="provision-single-server-cert"></a> Install a certificate for a single SQL Server instance

::: moniker range=">=sql-server-ver15"

1. In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.

1. Right-click **Protocols for** *&lt;instance Name&gt;*, and then select **Properties**.

1. Choose the **Certificate** tab, and then select **Import**.

1. Select **Browse** and then select the certificate file.

1. Select **Next** to validate the certificate. If there are no errors, select **Next** to import the certificate to the local instance.
::: moniker-end

::: moniker range="<= sql-server-2017"

1. In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.

1. Right-click **Protocols for** *&lt;instance Name&gt;*, and then select **Properties**.

1. Select a certificate from the **Certificate** dropdown list, and then select **Apply**.

1. Select **OK**.
::: moniker-end

## <a id="provision-failover-cluster-cert"></a> Install a certificate in a failover cluster instance configuration

1. In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.

1. Right-click **Protocols for** *&lt;instance Name&gt;*, and then choose **Properties**.

1. Choose the **Certificate** tab, and then select **Import**.

1. Select the certificate type, and whether to import for the current node only, or for each individual cluster node.

1. If installing for a single node, choose **Browse** and select certificate file. Then skip to step 8.

1. If installing a certificate for each node, select **Next** to list possible owner nodes. Possible owners for the current FCI are preselected.

1. Choose **Next** to select the certificate to be imported.

1. Enter the password when prompted. Look for any warnings or errors after validation.

1. Select **Next** to import the selected certificates.

> [!NOTE]  
> Complete these steps in the active node of the FCI. User must have administrator permissions on all the cluster nodes.

## <a id="provision-availability-group-cert"></a> Install a certificate in an availability group configuration

1. In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.

1. Right-click **Protocols for** *&lt;instance Name&gt;*, and then select **Properties**.

1. Choose the **Certificate** tab, and then select **Import**.

1. Choose the certificate type and select **Next** to select from the list of known availability groups.

1. Select **Next** to choose certificates for each replica node. Certificates should have a file name that matches the netbios name of the nodes.

1. Select **Next** to import the certificate on each node.

> [!NOTE]  
> Complete these steps from the node holding the AG primary replica. User must have administrator permissions on all the cluster nodes.

## Related content

- [Certificate requirements for SQL Server](certificate-requirements.md)
- [GRANT Certificate Permissions (Transact-SQL)](../../t-sql/statements/grant-certificate-permissions-transact-sql.md)
- [REVOKE Certificate Permissions (Transact-SQL)](../../t-sql/statements/revoke-certificate-permissions-transact-sql.md)
