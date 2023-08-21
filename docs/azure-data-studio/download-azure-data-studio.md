---
title: Download and install Azure Data Studio
description: Download and install Azure Data Studio for Windows, macOS, or Linux. This article provides release dates, version numbers, system requirements, and download links.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 08/10/2023
ms.service: azure-data-studio
ms.topic: overview
ms.custom: intro-overview
---
# Download and install Azure Data Studio

Azure Data Studio is a lightweight, cross-platform data management and development tool with connectivity to popular cloud and on-premises databases. Azure Data Studio supports Windows, macOS, and Linux, with immediate capability to connect to Azure SQL and SQL Server.  Browse the extension library for more database support options including MySQL, PostgreSQL, and CosmosDB.

Azure Data Studio's familiar interface offers a modern editor experience with IntelliSense, code snippets, source control integration, and an integrated terminal.  Engineered with the data platform user in mind, its extensibility allows users to customize their experience by installing the extensions relevant to their workflow, including database migrations, charting, GitHub Copilot, and more!

Use Azure Data Studio to query, design, and manage your databases and data warehouses wherever they are, on your local computer or in the cloud.

For more information about Azure Data Studio, visit [What is Azure Data Studio?](what-is-azure-data-studio.md).

## Download Azure Data Studio

Azure Data Studio 1.45.1 is the latest general availability (GA) version.

- Release number: 1.45.1
- Release date: August 07, 2023

| Platform | Type | Download |
| --- | --- | --- |
| Windows | User Installer | [64-bit](https://go.microsoft.com/fwlink/?linkid=2243234)&emsp;[ARM64](https://go.microsoft.com/fwlink/?linkid=2243236) |
| | System Installer | [64-bit](https://go.microsoft.com/fwlink/?linkid=2242848)&emsp;[ARM64](https://go.microsoft.com/fwlink/?linkid=2242941) |
| | .zip | [64 bit](https://go.microsoft.com/fwlink/?linkid=2243235)&emsp;[ARM64](https://go.microsoft.com/fwlink/?linkid=2242849) |
| Linux | .tar.gz | [64-bit](https://go.microsoft.com/fwlink/?linkid=2242942) |
| | .deb | [64-bit](https://go.microsoft.com/fwlink/?linkid=2242943) |
| | .rpm<sup>1</sup> | [64-bit](https://go.microsoft.com/fwlink/?linkid=2242640) |
| macOS | .zip | [Universal](https://go.microsoft.com/fwlink/?linkid=2242850)&emsp;[Intel Chip](https://go.microsoft.com/fwlink/?linkid=2242553)&emsp;[Apple Silicon](https://go.microsoft.com/fwlink/?linkid=2242554) |

<sup>1</sup> There is a known issue with install on RHEL. For more information see the [release notes](release-notes-azure-data-studio.md#known-issues-in-1451).

If you have comments or suggestions or want to report a problem with downloading Azure Data Studio, submit an issue to our team on the [Azure Data Studio feedback page](https://github.com/microsoft/azuredatastudio/issues/).

## Install Azure Data Studio

### Windows installation

[!INCLUDE [ssms-ads-install](../includes/ssms-azure-data-studio-install.md)]

This release of Azure Data Studio includes a standard Windows installer experience and a .zip file.

We recommend the *user installer*, which simplifies installations and updates and doesn't require Administrator privileges. (It doesn't require Administrator privileges because the location is under your user Local AppData (LOCALAPPDATA) folder.) The user installer also provides a smoother background update experience. For more information, see [User setup for Windows](https://code.visualstudio.com/updates/v1_26#_user-setup-for-windows).

**User installer** (recommended)

1. Download and run the [Azure Data Studio user installer for Windows](https://azuredatastudio-update.azurewebsites.net/latest/win32-x64-user/stable).

1. Start the Azure Data Studio app.

**System installer**

1. Download and run the [Azure Data Studio system installer for Windows](https://azuredatastudio-update.azurewebsites.net/latest/win32-x64/stable).

1. Start the Azure Data Studio app.

**.zip file**

1. Download the [Azure Data Studio .zip file for Windows](https://azuredatastudio-update.azurewebsites.net/latest/win32-x64-archive/stable).

1. Go to the downloaded file and extract it.

1. Run `\azuredatastudio-windows\azuredatastudio.exe`.

#### Unattended installation for Windows

You can also install Azure Data Studio by using a command prompt script.

For Windows, install Azure Data Studio in the background without prompts using the following steps:

1. Open the command prompt window with elevated permissions.

1. Run the following command:

   ```console
   <path where the azuredatastudio-windows-user-setup-x.xx.x.exe file is located> /VERYSILENT /MERGETASKS=!runcode>
   ```

   Example:

   ```console
   %systemdrive%\azuredatastudio-windows-user-setup-1.24.0.exe /VERYSILENT /MERGETASKS=!runcode
   ```

   > [!NOTE]  
   > The following example also works with the system installer file.

   > ```console
   > <path where the azuredatastudio-windows-setup-x.xx.x.exe file is located> /VERYSILENT /MERGETASKS=!runcode>
   > ```
   >
   > In the preceding code, you can also pass */SILENT* instead of */VERYSILENT* to see the setup user interface.

1. If you've run the commands successfully, you can see Azure Data Studio installed.

### macOS installation

1. Download [Azure Data Studio for macOS](https://azuredatastudio-update.azurewebsites.net/latest/darwin-universal/stable).

1. To expand the contents of the .zip file, double-click it.

1. To make Azure Data Studio available in Launchpad, drag the *Azure Data Studio.app* file to the *Applications* folder.

> [!NOTE]  
> For Apple Silicon users, please make sure you have Rosetta 2 installed. Some backend services are yet to be converted to native ARM64 binaries. You can run the following command in a Terminal window to install Rosetta 2.
>  
> ```bash
> /usr/sbin/softwareupdate --install-rosetta --agree-to-license
> ```

### Linux installation

You can install Azure Data Studio on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), Ubuntu, Debian, and Windows Subsystem for Linux (WSL).

#### [RHEL](#tab/redhat-install)

#### Install using .rpm file

1. Download Azure Data Studio for Red Hat Enterprise Linux by using the [.rpm](https://azuredatastudio-update.azurewebsites.net/latest/linux-rpm-x64/stable) file.

1. To extract the file, open a new terminal window, and then run the following commands:

   ```bash
   cd ~
   sudo yum install ./Downloads/azuredatastudio-linux-<version string>.rpm
   ```

1. To start Azure Data Studio, run this command:

   ```bash
   azuredatastudio
   ```

If you have missing dependencies, install them with following command:

```bash
yum install libXScrnSaver
```

#### Install using .tar.gz file

1. Download Azure Data Studio for Red Hat Enterprise Linux by using the [.tar.gz](https://azuredatastudio-update.azurewebsites.net/latest/linux-x64/stable) file.

1. To extract the file, open a new terminal window, and then run the following commands:

   ```bash
   cd ~
   cp ~/Downloads/azuredatastudio-linux-<version string>.tar.gz ~
   tar -xvf ~/azuredatastudio-linux-<version string>.tar.gz
   echo 'export PATH="$PATH:~/azuredatastudio-linux-x64"' >> ~/.bashrc
   source ~/.bashrc
   ```

1. To start Azure Data Studio, run this command:

   ```bash
   azuredatastudio
   ```

If you have missing dependencies, install them with the following command:

```bash
yum install libxss1 libgconf-2-4 libunwind8
```

#### [SLES](#tab/suse-install)

#### Install using .rpm file

1. Download Azure Data Studio for SUSE Linux Enterprise Server by using the [.rpm](https://azuredatastudio-update.azurewebsites.net/latest/linux-rpm-x64/stable) file.

1. To extract the file, open a new terminal window, and then run the following commands:

   ```bash
   cd ~
   sudo zypper install ./Downloads/azuredatastudio-linux-<version string>.rpm
   ```

1. To start Azure Data Studio, run this command:

   ```bash
   azuredatastudio
   ```

If you have missing dependencies, install them with the following command:

```bash
sudo zypper install libXScrnSaver
```

#### Install using .tar.gz file

1. Download Azure Data Studio for SUSE Linux Enterprise Server by using the [.tar.gz](https://azuredatastudio-update.azurewebsites.net/latest/linux-x64/stable) file.

1. To extract the file, open a new terminal window, and then run the following commands:

   ```bash
   cd ~
   cp ~/Downloads/azuredatastudio-linux-<version string>.tar.gz ~
   tar -xvf ~/azuredatastudio-linux-<version string>.tar.gz
   echo 'export PATH="$PATH:~/azuredatastudio-linux-x64"' >> ~/.bashrc
   source ~/.bashrc
   ```

1. To start Azure Data Studio, run this command:

   ```bash
   azuredatastudio
   ```

If you have missing dependencies, install them with the following command:

```bash
sudo zypper install libxss1 libgconf-2-4 libunwind8
```

#### [Ubuntu and Debian](#tab/ubuntu-install)

#### Install using .deb file

1. Download Azure Data Studio for Ubuntu or Debian by using the [.deb](https://azuredatastudio-update.azurewebsites.net/latest/linux-deb-x64/stable) file.

1. To extract the .deb file, open a new terminal window, and then run the following commands:

   ```bash
   cd ~
   sudo dpkg -i ./Downloads/azuredatastudio-linux-<version string>.deb
   ```

1. To start Azure Data Studio, run this command:

   ```bash
   azuredatastudio
   ```

If you have missing dependencies, install them with the following command:

```bash
sudo apt-get install libunwind8
```

#### Install using .tar.gz file

1. Download Azure Data Studio for Ubuntu or Debian by using the [.tar.gz](https://azuredatastudio-update.azurewebsites.net/latest/linux-x64/stable) file.

1. To extract the file, open a new terminal window, and then run the following commands:

    ```bash
    cd ~
    cp ~/Downloads/azuredatastudio-linux-<version string>.tar.gz ~
    tar -xvf ~/azuredatastudio-linux-<version string>.tar.gz
    echo 'export PATH="$PATH:~/azuredatastudio-linux-x64"' >> ~/.bashrc
    source ~/.bashrc
    ```

1. To start Azure Data Studio, run this command:

    ```bash
    azuredatastudio
    ```

If you have missing dependencies, install them with the following command:

```bash
sudo apt-get install libxss1 libgconf-2-4 libunwind8
```

#### [Windows Subsystem for Linux](#tab/windows-install)

1. Install Azure Data Studio for Windows. Then, use the `azuredatastudio` command in a Windows Subsystem for Linux (WSL) terminal just as you would in a standard command prompt. By default, the application is stored in your *AppData* folder.

1. Start Azure Data Studio from the WSL command prompt. When you're using the default Windows installation, start the application by running the following command. Replace `<your user name>` with your user name:

   ```bash
   /mnt/c/Users/<your user name>/AppData/Local/Programs/Azure Data Studio/azuredatastudio.exe
   ```

---

## What's new with Azure Data Studio

For details about the latest release of Azure Data Studio, see [Release notes for Azure Data Studio](./release-notes-azure-data-studio.md).

## Download the GA release of Azure Data Studio

We recommend that you [download the general availability (GA) release of Azure Data Studio](#download-azure-data-studio).

## Download the insiders build of Azure Data Studio

As an alternative, if you want to try out the beta features and send feedback, you can [download the insiders build of Azure Data Studio](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-main-branch).

## Supported operating systems

Azure Data Studio runs on Windows, macOS, and Linux. The following versions are supported:

- Windows: 10 and later versions
- Windows Server: 2016, 2019, 2022
- macOS: 10.12+ (Sierra) and later versions
- Linux: Debian 9+, RHEL 7+, SLES 15, Ubuntu 18.04+

> [!NOTE]  
> Incremental versions within a major operating system release may no longer be in support by the operating system. Consult the documentation for your operating system to ensure you have received applicable updates.

## Supported SQL Server versions

Azure Data Studio supports connecting to the following versions of SQL Server:

- Azure SQL Database
- Azure SQL Managed Instance
- [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]
- [!INCLUDE [sssql19-md](../includes/sssql19-md.md)]
- [!INCLUDE [sssql17-md](../includes/sssql17-md.md)]
- [!INCLUDE [sssql16-md](../includes/sssql16-md.md)]

## System requirements

| Requirement level | CPU cores | RAM memory |
| --- | :---: | :---: |
| Recommended | 4 | 8 GB |
| Minimum | 2 | 4 GB |

## Check for updates

To check for the latest updates, on the left pane, select **Manage** (gear icon), and then select **Check for Updates**.

To apply environment updates offline, [install the latest version](#download-and-install-azure-data-studio) directly over your previously installed version. You don't need to uninstall earlier versions of Azure Data Studio. If an earlier version is present, the installer automatically updates to the latest version.

## Move user settings

If you're updating SQL Operations Studio to Azure Data Studio and want to keep your settings, keyboard shortcuts, or code snippets, complete the following steps:

> [!NOTE]  
> If you've already installed Azure Data Studio or you've never installed or customized SQL Operations Studio, you can ignore this section.

1. On the left pane, select **Manage** (gear icon) and then select **Settings.**

   :::image type="content" source="./media/download/open-settings.png" alt-text="Screenshot of the Azure Data Studio Manage icon and Settings command.":::

1. At the top, right-click the **User Settings** tab, and then select **Reveal in Explorer**.

   :::image type="content" source="./media/download/reveal-in-explorer.png" alt-text="Screenshot of the Reveal in Explorer command in the User Settings tab.":::

1. Copy all files in this folder and save them in an easy-to-find location on your local drive, such as your *Documents* folder.

   :::image type="content" source="./media/download/copy-settings.png" alt-text="Screenshot of the settings.json file in the Windows Explorer folder structure.":::

1. In your updated version of Azure Data Studio, follow steps 1 and 2 and then, for step 3, paste the contents you saved into the folder. You can also manually copy over the settings, key bindings, or snippets in their respective locations.

1. If you're overriding your current installation, before you do so, delete the old installation directory to avoid errors connecting to your Azure account for the resource explorer.

## Uninstall Azure Data Studio from Windows

If you installed Azure Data Studio by using the Windows installer, uninstall it just as you would any Windows application.

If you installed Azure Data Studio with a .zip file or other archive, delete that file.

## Uninstall Azure Data Studio from macOS

You can [uninstall apps](https://support.apple.com/guide/mac-help/install-and-uninstall-other-apps-mh35835/mac) from the Internet or drive on Mac by completing the following steps:

1. Select the **Finder icon** in the Dock, and then select **Applications** in the **Finder** sidebar.

1. Do one of the following steps:

   - If an app is in a folder, open the app's folder to check for an uninstaller. Double-click **Uninstall [App]** or **[App] Uninstaller**, and then follow the onscreen instructions.

   - If an app isn't in a folder or doesn't have an uninstaller, drag the app from the *Applications* folder to the Trash (at the end of the Dock).

To uninstall apps you've downloaded from the App Store, use Launchpad.

## Uninstall Azure Data Studio from Linux

### [RHEL](#tab/redhat-uninstall)

Use the `rpm`, `yum`, or `dnf` command to delete Azure Data Studio under Red Hat Enterprise Linux.

To list installed software, run one of the following commands:

```bash
sudo rpm -qa | less
sudo yum list | less
sudo dnf list | less
```

To get information about the `azuredatastudio` package, run one of the following commands:

```bash
sudo rpm -qa azuredatastudio
sudo yum list azuredatastudio
sudo dnf list azuredatastudio
```

To delete a package called `azuredatastudio`, run one of the following commands:

```bash
sudo rpm -e azuredatastudio
sudo yum remove azuredatastudio
sudo dnf remove azuredatastudio
```

### [SLES](#tab/suse-uninstall)

Use the `rpm` or `zypper` command to delete Azure Data Studio under SUSE Linux Enterprise Server.

To list installed software, run one of the following commands:

```bash
sudo rpm -qa | less
sudo zypper list | less
```

To get information about the `azuredatastudio` package, run one of the following commands:

```bash
sudo rpm -qa azuredatastudio
sudo zypper list azuredatastudio
```

To delete a package called `azuredatastudio`, run one of the following commands:

```bash
sudo rpm -e azuredatastudio
sudo zypper remove azuredatastudio
```

### [Ubuntu and Debian](#tab/ubuntu-uninstall)

You can uninstall Azure Data Studio under Ubuntu or Debian.

To list installed software, run the following command:

```bash
sudo dpkg --list | less
```

To list installed software that matches `azuredatastudio`, run the following command:

```bash
sudo dpkg --list | grep azuredatastudio
```

To delete the software, run the following commands:

```bash
sudo apt-get remove azuredatastudio
```

---

## Next steps

To learn more about Azure Data Studio, see the following resources:

- [Azure Data Studio release notes](release-notes-azure-data-studio.md)
- [What is Azure Data Studio?](what-is-azure-data-studio.md)
- [SQL tools](../tools/overview-sql-tools.md)
- [Azure Data Architecture Guide](/azure/architecture/data-guide/)
- [SQL Server Blog](https://cloudblogs.microsoft.com/sqlserver/)

[!INCLUDE[get-help-sql-tools](../includes/paragraph-content/get-help-sql-tools.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839) and [Enable or disable usage data collection for Azure Data Studio](usage-data-collection.md).
