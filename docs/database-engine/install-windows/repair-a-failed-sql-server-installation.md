---
title: Repair a failed SQL Server installation
description: Follow these steps to repair an installation of SQL Server Standard, Developer, and Enterprise edition.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/03/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# Repair a failed SQL Server installation

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The repair operation can be used in the following scenarios:

- Repair an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that is corrupted after it was successfully installed.

- Repair an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] if the upgrade operation is canceled or fails after the instance name is mapped to the newly upgraded instance.

  - If you see the following message in the summary log, you can repair the failed upgrade instance:

    > [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] upgrade failed. To continue, investigate the reason for the failure, correct the problem, and then repair your installation.

  - If you see the following message in the summary log, you need to uninstall and reinstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can't repair the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

    > [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] upgrade failed. To continue, investigate the reason for the failure, correct the problem.

> [!NOTE]  
> These instructions don't apply to [!INCLUDE [ssexpress-md](../../includes/ssexpress-md.md)] edition.

When you repair an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

- All missing or corrupt files are replaced.
- All missing or corrupt registry keys are replaced.
- All missing or invalid configuration values are set to default values.

Before you continue, for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover clusters, review the following important information:

- Repair must be run on individual cluster nodes.

- To repair a failover cluster node after a failed Prepare operation, use **Remove node** and then perform the Prepare step again. For more information, see [Add or remove nodes in a failover cluster instance (Setup)](../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).

## Repair a failed installation of SQL Server from the Installation Center

1. Launch the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup program (`setup.exe`) from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media.

1. After prerequisites and system verification, the Setup program will display the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center page.

1. Select **Maintenance** in the left-hand navigation area, and then select **Repair** to start the repair operation.

   > [!TIP]  
   > If the Installation Center was launched using the start menu, you need to provide the location of the installation media at this time.

1. Setup support rule and file routines run to ensure that your system has prerequisites installed and that the computer passes Setup validation rules. Select **OK** or **Install** to continue.

1. On the Select Instance page, select the instance to repair, and then select **Next** to continue.

1. The repair rules run to validate the operation. To continue, select **Next**.

1. The Ready to Repair page indicates that the operation is ready to proceed. To continue, select **Repair**.

1. The Repair Progress page shows the status of the repair operation. The Complete page indicates that the operation is finished.

## Repair a failed installation of SQL Server using the command prompt

Run the following command at a command prompt. Replace `<instancename>` with the name of the instance you want to repair.

```cmd
setup.exe /q /ACTION=Repair /INSTANCENAME=<instancename>
```

## Related content

- [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md)
- [Installation How-to articles](/previous-versions/sql/)
