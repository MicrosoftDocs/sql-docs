---
title: "Slipstream Installation for SQL Server"
description: This article describes how to use the slipstream installation process to install SQL Server and all available updates.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest, jopilov
ms.date: 08/21/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
monikerRange: ">=sql-server-2016"
ms.custom:
  - intro-installation
---
# Slipstream installation for SQL Server

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Slipstream installation is the process of integrating cumulative updates (CUs) into the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] setup process. This method ensures that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is installed with the latest fixes and improvements in a single operation, without requiring a post-installation patch.

Slipstreaming is useful for:

- Avoiding known setup issues in base media

- Reducing deployment time and manual effort by performing installation and patching in one operation and helping avoid extra operating system reboots.

## How slipstreaming works

During setup, you run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] installation with the `/UpdateEnabled=True` parameter to enable update integration, and specify the folder containing the desired CU using `/UpdateSource`.

For example:

```console
setup.exe /Action=Install /UpdateEnabled=True /UpdateSource="C:\SQLUpdates"
```

This example command line operation tells Setup to use the updated binaries from the specified folder instead of the original base media, applying them during the installation process.

## Guidance for using slipstream

- You should integrate the latest cumulative updates into the installation media to ensure the instance starts with the most stable and secure configuration.

- Test slipstream installations in a staging environment before deploying to production.

- Use consistent update packages across all servers in an environment to ensure compatibility.

- If adding features later, use matching installation media or apply the same cumulative update to the existing instance.

## When to use slipstream

- You set up new instances of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and want them patched during installation to avoid unnecessary restarts.

- You encounter setup failures with base installation media and need patched setup binaries.

- You deploy [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] across multiple machines and want a consistent, reliable experience.

## Slipstream installation steps

### Step 1: Create a folder to store the SQL updates

Create a local folder to store the update files. For example:

```powershell
New-Item -ItemType "Directory" -Path "C:\SQLUpdates" -Force 
```

### Step 2: Download the latest update

Download the [latest cumulative update](/troubleshoot/sql/releases/download-and-install-latest-updates) for your version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] into the newly created folder.

### Step 3: Run the installer with slipstream parameters

Run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Setup using the following command:

```powershell
C:\SqlSetupMedia\setup.exe /Action=Install /UpdateEnabled=True /UpdateSource="C:\SQLUpdates"
```

| Parameter | Description |
| --- | --- |
| `/Action=Install` | Tells [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Setup to start the installation process. If you leave this parameter out, Setup only opens the Installation Center, and ignores other parameters like update paths. |
| `/UpdateEnabled=True` | Enables update detection. It tells Setup to look for and include any available updates (like a cumulative update) during the installation. |
| `/UpdateSource=<location>` | Points to the folder where your update files (CU) are stored. |

For more information about setup parameters, see [Install and configure SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md)

### Step 4: Complete setup

Proceed through the setup wizard as usual.

During installation, you see confirmation that the update was integrated.

:::image type="content" source="media/install-sql-server-using-slipstream/installation-screen.png" alt-text="Screenshot showing the results.":::

### Step 5: Verify installation success

After installation, the applied CU will be visible in:

- **Programs and Features** > **Installed Updates** (from the Windows Start menu)

- [SQL Server features discovery report](validate-a-sql-server-installation.md#run-sql-server-features-discovery-report)

- Setup log (`Summary.txt`) under `Patch Level`. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

  ```output
  Overall summary:
    Final result:                  Passed
    Exit code (Decimal):           0
    Start time:                    2025-08-17 11:55:45
    End time:                      2025-08-17 12:14:24
    Requested action:              Install

  Machine Properties:
    Machine name:                  <machine-name>
    Machine processor count:       8
    OS version:                    Microsoft Windows Server 2019 Standard (10.0.17763)
    OS service pack:
    OS region:                     United States
    OS language:                   English (United States)
    OS architecture:               X64
    Process architecture:          64 Bit
    OS clustered:                  No

  Product features discovered:
    Product        Instance      Instance ID      Feature      Language

  Package properties:
    Description:                   Microsoft SQL Server 2019
    ProductName:                   SQL Server 2019
    Type:                          RTM
    Version:                       15
    Installation location:         C:\SQL Server\SQL 2019\SQLFull_ENU\x64\setup\
    Installation edition:          Enterprise Edition: Core-based Licensing

    Slipstream:                    True
    SP Level                         0
    Patch Level:                   15.0.4430.1

  Product Update Status:
    Success:                       KB 5054833

  Product Updates Selected for Installation:
    Title:                         Hotfix Pack
    Knowledge Base Article:        KB 5054833
    Version:                       15.0.4430.0
    Architecture:                  x64
    Language:                      All
    Update Source:                 C:\SQL Server\SQL 2019\SQLFull_ENU\CU
  ```

## How to identify a slipstream installation

To confirm that slipstreaming was used to run Setup:

- The setup wizard displays rules such as:

  ```output
  Update Setup Media Language Rule
  ```

- The install log includes lines such as:

  ```output
  Slipstream:                    True
  SP Level                       0
  Patch Level:                   15.0.4430.1
  ```

- Run the [SQL Server features discovery report](validate-a-sql-server-installation.md#run-sql-server-features-discovery-report) to verify version levels.

## Remarks

If Setup fails, review the failure details in the logs file located at `C:\Program Files\Microsoft SQL Server\<nn>\Setup Bootstrap\Log\Summary.txt`, where `<nn>` is the version you're installing. For more information, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

Here's an example of a failed slipstream installation.

```output
Overall summary:
  Final result:                  Failed: see results below
  Exit code (Decimal):           -2068643839
  Start time:                    2025-08-17 09:55:12 
  End time:                      2025-08-17 10:03:01
  Requested action:              Install

Setup completed with required actions for features.
Troubleshooting information for those features:
  Next step for SQLEngine:       Use the following information to resolve the error, uninstall this feature, and then run the setup process again.
```

Slipstreaming doesn't eliminate the need to monitor future updates post-installation.

## Related content

- [Install and configure SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md)
- [Install SQL Server from the Installation Wizard (Setup)](install-sql-server-from-the-installation-wizard-setup.md)
- [Install SQL Server using a configuration file](install-sql-server-using-a-configuration-file.md)
