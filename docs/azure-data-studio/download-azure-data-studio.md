---
title: Download and install Azure Data Studio
description: Download and Install Azure Data Studio for Windows, macOS, or Linux. This article provides release dates, version numbers, system requirements, and download links.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: overview
author: yualan
ms.author: alayu
ms.reviewer: maghan
ms.custom: seodec18, contperf-fy21q4
ms.date: 05/03/2021
---

# Download and install Azure Data Studio

Azure Data Studio is a cross-platform database tool for data professionals using on-premises and cloud data platforms on Windows, macOS, and Linux.

Azure Data Studio offers a modern editor experience with IntelliSense, code snippets, source control integration, and an integrated terminal. It's engineered with the data platform user in mind, with the built-in charting of query result sets and customizable dashboards.

Use Azure Data Studio to query, design, and manage your databases and data warehouses, wherever they are - on your local computer or in the cloud.

For more information about Azure Data Studio, visit [What is Azure Data Studio](what-is-azure-data-studio.md).

## Download Azure Data Studio

Azure Data Studio 1.28.0 is the latest general availability (GA) version. If you have a previous GA version of Azure Data Studio installed, installing Azure Data Studio 1.28.0 upgrades it to the latest version.

- Release number: 1.28.0
- Release date: April 15, 2021

| Platform | Download |
|----------|----------|
| Windows | [User Installer (recommended)](https://go.microsoft.com/fwlink/?linkid=2160781)<br>[System Installer](https://go.microsoft.com/fwlink/?linkid=2160780)<br>[.zip](https://go.microsoft.com/fwlink/?linkid=2160923) |
| macOS | [.zip](https://go.microsoft.com/fwlink/?linkid=2160874) |
| Linux | [.deb](https://go.microsoft.com/fwlink/?linkid=2160876)<br>[.rpm](https://go.microsoft.com/fwlink/?linkid=2160875)<br>[.tar.gz](https://go.microsoft.com/fwlink/?linkid=2160782) |

> [!Note]
> Azure Data Studio currently does not support the ARM architecture.

If you have comments or suggestions or want to report issues, the best way to contact the Azure Data Studio team is to file an issue on the [Azure Data Studio feedback page](https://github.com/microsoft/azuredatastudio/issues/).

## Install Azure Data Studio

### Windows install

[!INCLUDE [ssms-ads-install](../includes/ssms-azure-data-studio-install.md)]

This release of Azure Data Studio includes a standard Windows Installer experience and a .zip file.

We recommend the *user installer* because it doesn't require administrator privileges, simplifying installs and upgrades. The user installer doesn't require Administrator privileges as the location is under your user Local AppData (LOCALAPPDATA) folder. The user installer also provides a smoother background update experience. For more information, see [User setup for Windows](https://code.visualstudio.com/updates/v1_26#_user-setup-for-windows).

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

#### Unattended install for Windows

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

### macOS install

1. Download [Azure Data Studio for macOS](https://go.microsoft.com/fwlink/?linkid=2160874).

2. To expand the contents of the zip, double-select it.

3. To make Azure Data Studio available in the *Launchpad*, drag *Azure Data Studio.app* to the *Applications* folder.

### Linux install

#### .deb Installation

1. Download Azure Data Studio for Linux by using the [deb](https://go.microsoft.com/fwlink/?linkid=2160876) file.

2. To extract the file, open a new Terminal window and follow the below commands.

   ```bash
   cd ~
   sudo dpkg -i ./Downloads/azuredatastudio-linux-<version string>.deb
   ```

3. To launch Azure Data Studio

   ```bash
   azuredatastudio
   ```

You may have missing dependencies. Use the following commands to install these dependencies.

   ```bash
   sudo apt-get install libunwind8
   ```

#### .rpm Installation

1. Download Azure Data Studio for Linux by using the [rpm](https://go.microsoft.com/fwlink/?linkid=2160875) file.

2. To extract the file, open a new Terminal window and follow the below commands.

   ```bash
   cd ~
   yum install ./Downloads/azuredatastudio-linux-<version string>.rpm
   ```

3. To launch Azure Data Studio

   ```bash
   azuredatastudio
   ```

You may have missing dependencies. Use the following commands to install these dependencies.

   ```bash
   yum install libXScrnSaver
   ```

#### tar.gz Installation

1. Download Azure Data Studio for Linux by using the [.tar.gz](https://go.microsoft.com/fwlink/?linkid=2160782) file.

2. To extract the file, open a new Terminal window and follow the below commands.

   ```bash
   cd ~
   cp ~/Downloads/azuredatastudio-linux-<version string>.tar.gz ~ 
   tar -xvf ~/azuredatastudio-linux-<version string>.tar.gz 
   echo 'export PATH="$PATH:~/azuredatastudio-linux-x64"' >> ~/.bashrc
   source ~/.bashrc
   ```

3. To launch Azure Data Studio

  ```bash
   azuredatastudio
  ```

You may have missing dependencies. Use the following commands to install these dependencies.

   ```bash
   sudo apt-get install libxss1 libgconf-2-4 libunwind8
   ```

#### Windows Subsystem for Linux (WSL)

1. Install **Azure Data Studio** for Windows. Then use the `azuredatastudio` command in a WSL terminal just as you would in a standard command prompt. By default, the application is stored in your AppData folder.

2. Start **Azure Data Studio** from the WSL command prompt. When using the default Windows installation, the application can be started using:

```bash
'/mnt/c/Users/<your user name>/AppData/Local/Programs/Azure Data Studio/azuredatastudio.exe'
```

## What's new

For details about the latest release of Azure Data Studio, see **[Release notes for Azure Data Studio](./release-notes-azure-data-studio.md).**

## Download Insiders build of Azure Data Studio

It's recommended to [download the GA release of Azure Data Studio](#download-azure-data-studio). However, if you want to try out the beta features and send feedback, you can download the [Insiders build of Azure Data Studio](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-main).

## Supported operating systems

Azure Data Studio runs on Windows, macOS, and Linux and is supported on the following platforms:

### Windows operating systems

- Windows 10 (64-bit)
- Windows 8.1 (64-bit)
- Windows 8 (64-bit)
- Windows 7 (SP1)
- Windows Server 2019
- Windows Server 2016
- Windows Server 2012 R2 (64-bit)
- Windows Server 2012 (64-bit)
- Windows Server 2008 R2 (64-bit)

### macOS operating systems

- macOS 10.15 Catalina
- macOS 10.14 Mojave
- macOS 10.13 High Sierra
- macOS 10.12 Sierra
- macOS 11.1  Big Sur

### Linux operating systems

- Red Hat Enterprise Linux (RHEL) 8.3
- Red Hat Enterprise Linux (RHEL) 8.2
- Red Hat Enterprise Linux (RHEL) 8.1
- Red Hat Enterprise Linux (RHEL) 8.0
- Red Hat Enterprise Linux (RHEL) 7.4
- Red Hat Enterprise Linux (RHEL) 7.3
- SUSE Linux Enterprise Server v12 SP2
- Ubuntu 20.04
- Ubuntu 18.04
- Ubuntu 16.04

> [!Note]
>
> The versions of RHEL (7.3 and 7.4) are no longer supported by Red Hat. RHEL 7.3 support ended November 30, 2018, and RHEL 7.4 ended August 31, 2019.
>
> For details of the RHEL 7 Application Compatibility Guide, see: https://access.redhat.com/articles/rhel-abi-compatibility for more information.
>
> For details of the RHEL 8 Application Compatibility Guide, see: https://access.redhat.com/articles/rhel8-abi-compatibility.

## Recommended System Requirements

| Recommended/Minimum | CPU Cores | Memory/RAM |
|---------------------|-----------|------------|
|     Recommended     |     4     |   8 GB     |
|     Minimum         |     2     |   4 GB     |

## Check for updates

To check for the latest updates, select the gear icon on the bottom left of the window and select **Check for Updates**.

Offline environment updates can be applied by [installing the latest version](#download-and-install-azure-data-studio) directly over a previously installed version. Uninstalling prior versions of Azure Data Studio isn't necessary. Instead, the installer updates a currently installed application if present.

## Move user settings

If you're updating SQL Operations Studio to Azure Data Studio and want to keep your settings, keyboard shortcuts, or code snippets, follow the steps below.

*If you already have Azure Data Studio, or you've never installed or customized SQL Operations Studio, then you can ignore this section.*

1. Open Settings by selecting the gear on the bottom left and selecting **Settings.**

   ![edit settings in Azure Data Studio](./media/download/open-settings.png)

2. Right-click the **User Settings** tab on top, and select **Reveal in Explorer**

   ![launch explorer, which will take you to your local file system](./media/download/reveal-in-explorer.png)

3. Copy all files in this folder and save them in an easy-to-find location on your local drive, like your Documents folder.

   ![use the files and copy over to your location](./media/download/copy-settings.png)

4. In your new version of Azure Data Studio, follow steps 1-2, then for step 3, paste the contents you saved into the folder. You can also manually copy over the settings, key bindings, or snippets in their respective locations.

5. If overriding an existing installation, delete the old install directory before installation to avoid errors connecting to your Azure account for the resource explorer.

## Uninstall Azure Data Studio from Windows

If you installed Azure Data Studio using the Windows Installer, then uninstall the same way you remove any Windows application.

If you installed Azure Data Studio with a .zip or other archive, then delete the files.

## Uninstall Azure Data Studio from macOS

You can [uninstall apps](https://support.apple.com/guide/mac-help/install-and-uninstall-other-apps-mh35835/mac) from the internet or disc on Mac by the below steps.

1. Select the **Finder icon** in the Dock, then select Applications in the Finder sidebar.

2. Then choose from one of te steps below:

    - If an app is in a folder, open the app's folder to check for an Uninstaller. If you see Uninstall [App] or [App] Uninstaller, double-select it, then follow the onscreen instructions.

    - If an app isn't in a folder or doesn't have an Uninstaller, drag the app from the Applications folder to the Trash (at the end of the Dock).

To uninstall apps, you downloaded from the App Store, use Launchpad.

## Uninstall Azure Data Studio from Linux

### Debian

You can uninstall Azure Data Studio under Debian or Ubuntu Linux.

To list installed software type:

```bash
dpkg --list
dpkg --list | less
dpkg --list | grep apache
```

To delete the software, enter:

```bash
sudo apt-get remove azuredatastudio
sudo apt-get remove apache
```

### RedHat

Use rpm or yum command to delete Azure Data Studio.

To list the installed software type:

```bash
rpm -qa | less
rpm -qa azuredatastudio
yum list | less
yum list azuredatastudio
```

To get information about the azuredatastudio package, enter:

```bash
rpm -qa azuredatastudio
yum list azuredatastudio
```

To delete a package called azuredatastudio, enter:

```bash
rpm -e azuredatastudio
yum remove azuredatastudio
```

## Next Steps

- [What is Azure Data Studio](what-is-azure-data-studio.md)
- [Azure Data Studio release notes](release-notes-azure-data-studio.md)
- [SQL tools](../tools/overview-sql-tools.md)
- [Latest updates](../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md)
- [Azure Data Architecture Guide](/azure/architecture/data-guide/)
- [SQL Server Blog](https://cloudblogs.microsoft.com/sqlserver/)

[!INCLUDE[get-help-sql-tools](../includes/paragraph-content/get-help-sql-tools.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839) and [usage data collection](usage-data-collection.md).
