---
title: Download and install Azure Data Studio
description: Download and Install Azure Data Studio for Windows, macOS, or Linux. This article provides release dates, version numbers, system requirements, and download links.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: overview
author: yualan
ms.author: alayu
ms.reviewer: maghan
ms.custom: seodec18
ms.date: 04/16/2021
---

# Download and install Azure Data Studio

Azure Data Studio is a cross-platform database tool for data professionals using on-premises and cloud data platforms on Windows, macOS, and Linux.

Azure Data Studio offers a modern editor experience with IntelliSense, code snippets, source control integration, and an integrated terminal. It's engineered with the data platform user in mind, with built-in charting of query result sets and customizable dashboards. For more information about Azure Data Studio, visit [What is Azure Data Studio](what-is-azure-data-studio.md).

## Download the latest release

| Platform | Download | Release date | Version |
|----------|----------|--------------|---------|
| Windows | [User Installer (recommended)](https://go.microsoft.com/fwlink/?linkid=2160781)<br>[System Installer](https://go.microsoft.com/fwlink/?linkid=2160780)<br>[.zip](https://go.microsoft.com/fwlink/?linkid=2160923) | April 15, 2021 | 1.28.0 |
| macOS | [.zip](https://go.microsoft.com/fwlink/?linkid=2160874) | April 15, 2021 | 1.28.0 |
| Linux | [.deb](https://go.microsoft.com/fwlink/?linkid=2160876)<br>[.rpm](https://go.microsoft.com/fwlink/?linkid=2160875)<br>[.tar.gz](https://go.microsoft.com/fwlink/?linkid=2160782) | April 15, 2021 | 1.28.0 |

> [!Note]
> Azure Data Studio currently does not support ARM64 architecture.

**For details about the latest release, see the [release notes](./release-notes-azure-data-studio.md).**

> [!Note]
> Azure Data Studio currently does not support the ARM architecture.

## Get Azure Data Studio for Windows

[!INCLUDE [ssms-ads-install](../includes/ssms-azure-data-studio-install.md)]

This release of Azure Data Studio includes a standard Windows Installer experience and a .zip file.

We recommend the *user installer* because it doesn't require administrator privileges, simplifying both installs and upgrades. The user installer doesn't require Administrator privileges as the location is under your user Local AppData (LOCALAPPDATA) folder. The user installer also provides a smoother background update experience. For more information, see [User setup for Windows](https://code.visualstudio.com/updates/v1_26#_user-setup-for-windows).

**User Installer** (recommended)

1. Download and run the [Azure Data Studio *user* installer for Windows](https://go.microsoft.com/fwlink/?linkid=2160781).
2. Start the Azure Data Studio app.

**System Installer**

1. Download and run the [Azure Data Studio *system* installer for Windows](https://go.microsoft.com/fwlink/?linkid=2160780).
2. Start the Azure Data Studio app.

**.zip file**

1. Download [Azure Data Studio .zip for Windows](https://go.microsoft.com/fwlink/?linkid=2160923).
2. Browse to the downloaded file and extract it.
3. Run `\azuredatastudio-windows\azuredatastudio.exe`

## Get Azure Data Studio for macOS

1. Download [Azure Data Studio for macOS](https://go.microsoft.com/fwlink/?linkid=2160874).
2. To expand the contents of the zip, double-click it.
3. To make Azure Data Studio available in the *Launchpad*, drag *Azure Data Studio.app* to the *Applications* folder.

## Get Azure Data Studio for Linux

1. Download Azure Data Studio for Linux by using one of the installers or the tar.gz archive:
    - [.deb](https://go.microsoft.com/fwlink/?linkid=2160876)
    - [.rpm](https://go.microsoft.com/fwlink/?linkid=2160875)
    - [.tar.gz](https://go.microsoft.com/fwlink/?linkid=2160782)
1. To extract the file and launch Azure Data Studio, open a new Terminal window and type the following commands:

   **Debian Installation:**

   ```bash
   cd ~
   sudo dpkg -i ./Downloads/azuredatastudio-linux-<version string>.deb

   azuredatastudio
   ```

   **rpm Installation:**

   ```bash
   cd ~
   yum install ./Downloads/azuredatastudio-linux-<version string>.rpm

   azuredatastudio
   ```

   **tar.gz Installation:**

   ```bash
   cd ~
   cp ~/Downloads/azuredatastudio-linux-<version string>.tar.gz ~ 
   tar -xvf ~/azuredatastudio-linux-<version string>.tar.gz 
   echo 'export PATH="$PATH:~/azuredatastudio-linux-x64"' >> ~/.bashrc
   source ~/.bashrc
   azuredatastudio
   ```

   > [!NOTE]
   > On Debian, Redhat, and Ubuntu, you may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:

   **Debian:**

   ```bash
   sudo apt-get install libunwind8
   ```

   **Redhat:**

   ```bash
   yum install libXScrnSaver
   ```

   **Ubuntu:**

   ```bash
   sudo apt-get install libxss1

   sudo apt-get install libgconf-2-4

   sudo apt-get install libunwind8
   ```

## Download Insiders build of Azure Data Studio

In general, users should download the stable release of Azure Data Studio above. However, if you want to try out the beta features and send feedback, you can download the [Insiders build of Azure Data Studio](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-main).

## Supported Operating Systems

Azure Data Studio runs on Windows, macOS, and Linux and is supported on the following platforms:

### Windows

- Windows 10 (64-bit)
- Windows 8.1 (64-bit)
- Windows 8 (64-bit)
- Windows 7 (SP1)
- Windows Server 2019
- Windows Server 2016
- Windows Server 2012 R2 (64-bit)
- Windows Server 2012 (64-bit)
- Windows Server 2008 R2 (64-bit)

### macOS

- macOS 10.15 Catalina
- macOS 10.14 Mojave
- macOS 10.13 High Sierra
- macOS 10.12 Sierra
- macOS 11.1  Big Sur

### Linux

- Red Hat Enterprise Linux 7.4
- Red Hat Enterprise Linux 7.3
- SUSE Linux Enterprise Server v12 SP2
- Ubuntu 16.04

## Recommended System Requirements

| Recommended/Minimum | CPU Cores | Memory/RAM |
|---------------------|-----------|------------|
|     Recommended     |     4     |   8 GB     |
|     Minimum         |     2     |   4 GB     |

## Check for updates

To check for the latest updates, select the gear icon on the bottom left of the window and select **Check for Updates**.

Offline environment updates can be applied by [installing the latest version](#download-and-install-azure-data-studio) directly over a previously installed version. Uninstalling prior versions of Azure Data Studio isn't necessary. The installer updates a currently installed application if present.

## Supported SQL offerings

- This version of Azure Data Studio works with all [supported versions of [!INCLUDE [sssql14-md](../includes/sssql14-md.md)] - [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)]](https://support.microsoft.com/lifecycle?C2=1044) and provides support for working with the latest cloud features in Azure SQL Database and Azure Synapse Analytics. Azure Data Studio also provides preview support for Azure SQL Managed Instance.

## Move user settings

If you're updating SQL Operations Studio to Azure Data Studio and want to keep your settings, keyboard shortcuts, or code snippets, follow the steps below.

*If you already have Azure Data Studio, or you've never installed or customized SQL Operations Studio, then you can ignore this section.*

1. Open Settings by selecting the gear on the bottom left and selecting **Settings.**

   ![edit settings in Azure Data Studio](./media/download/open-settings.png)

2. Right-click the **User Settings** tab on top and select **Reveal in Explorer**

   ![launch explorer, which will take you to your local file system](./media/download/reveal-in-explorer.png)

3. Copy all files in this folder and save in an easy to find the location on your local drive, like your Documents folder.

   ![use the files and copy over to your location](./media/download/copy-settings.png)

4. In your new version of Azure Data Studio, follow steps 1-2, then for step 3, paste the contents you saved into the folder. You can also manually copy over the settings, key bindings, or snippets in their respective locations.

5. If overriding an existing installation, delete the old install directory before installation to avoid errors connecting to your Azure account for the resource explorer.

## Unattended install for Windows

You can also install Azure Data Studio using a command prompt script.

If you want to install Azure Data Studio in the background with no GUI prompts, and you're on the Windows platform, then follow the steps below.

1. Launch the command prompt with elevated permissions.

2. Type the command below in the command prompt.

    ```console
    <path where the azuredatastudio-windows-user-setup-x.xx.x.exe file is located> /VERYSILENT /MERGETASKS=!runcode>
    ```

    Example:

    ```console
    %systemdrive%\azuredatastudio-windows-user-setup-1.24.0.exe /VERYSILENT /MERGETASKS=!runcode
    ```

    > [!Note]
    > The example also works with the system installer file.
    > 
    > ```console
    > <path where the azuredatastudio-windows-setup-x.xx.x.exe file is located> /VERYSILENT /MERGETASKS=!runcode>
    > ```

    You can also pass */SILENT* instead of */VERYSILENT* to see the setup UI.

3. If all goes well, you can see Azure Data Studio installed.

## Uninstall Azure Data Studio

If you installed Azure Data Studio using the Windows Installer, then uninstall the same way you remove any Windows application.

If you installed Azure Data Studio with a .zip or other archive, then delete the files.

## Next Steps

See one of the following quickstarts to get started:

- [What is Azure Data Studio](what-is-azure-data-studio.md)
- [Azure Data Studio release notes](release-notes-azure-data-studio.md)
- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Synapse Analytics](quickstart-sql-dw.md)

[!INCLUDE[get-help-sql-tools](../includes/paragraph-content/get-help-sql-tools.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839) and [usage data collection](usage-data-collection.md).
