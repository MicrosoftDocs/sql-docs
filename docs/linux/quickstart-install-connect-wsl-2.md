---
title: "WSL 2: Install SQL Server on Windows Subsystem for Linux"
description: This quickstart shows how to install SQL Server on Windows Subsystem for Linux (WSL 2) and then create and query a database with sqlcmd.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/02/2025
ms.service: sql
ms.subservice: linux
ms.topic: quickstart
ms.custom:
  - intro-installation
  - linux-related-content
---
# Quickstart: Install SQL Server and create a database on Windows Subsystem for Linux (WSL 2)

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Windows Subsystem for Linux (WSL) is a feature in Windows that allows you to run a Linux environment directly on your Windows machine, without the need for a virtual machine or dual booting. WSL provides a seamless and productive experience for developers who want to use both Windows and Linux simultaneously. For more information, see [What is the Windows Subsystem for Linux?](/windows/wsl/about)

## SQL Server on WSL is for development use only

SQL Server on WSL 2 is intended for development purposes only, and is **not** supported for production workloads. We recommend running SQL Server in WSL environments on one of the [Supported platforms](sql-server-linux-release-notes-2022.md#supported-platforms) as documented, for the version of SQL Server you intend to run.

For any support related issues, you can [obtain support from Microsoft](/troubleshoot/sql/database-engine/install/windows/support-policy-sql-server#obtain-support-from-microsoft).

## Get started with SQL Server on WSL 2

There are two ways to get started with SQL Server on WSL 2:

- Install SQL Server as a `systemd` service, which can be managed using `systemctl` commands. Make sure that you enable `systemd` on WSL. For more information, see [How to enable systemd](/windows/wsl/systemd#how-to-enable-systemd).

- Deploy SQL Server containers in WSL. For this option, you need to install a Linux container engine in WSL, such as Docker or Podman, and then deploy SQL Server containers.

## Prerequisites

Install WSL 2. Ensure you're running Windows 10 version 2004 or a later version (Build 19041 and higher), or Windows 11. To install WSL, open a PowerShell or Windows command prompt in administrator mode, and run the following command:

```console
wsl --install
```

For detailed instructions, see [How to install Linux on Windows with WSL](/windows/wsl/install). For information on setting up the WSL environment for development, see [Set up a WSL development environment](/windows/wsl/setup/environment).

## Install SQL Server in WSL

This section describes the steps to set up a Linux distribution in WSL, and how to install SQL Server in that Linux distribution.

### Choose Linux distribution

You can list all the valid distributions that can be installed in WSL using the following command:

```console
wsl -l -o
```

The output looks similar to the following example.

```output
The following is a list of valid distributions that can be installed.
Install using 'wsl.exe --install <Distro>'.

NAME                            FRIENDLY NAME
Ubuntu                          Ubuntu
Debian                          Debian GNU/Linux
kali-linux                      Kali Linux Rolling
Ubuntu-18.04                    Ubuntu 18.04 LTS
Ubuntu-20.04                    Ubuntu 20.04 LTS
Ubuntu-22.04                    Ubuntu 22.04 LTS
Ubuntu-24.04                    Ubuntu 24.04 LTS
OracleLinux_7_9                 Oracle Linux 7.9
OracleLinux_8_7                 Oracle Linux 8.7
OracleLinux_9_1                 Oracle Linux 9.1
openSUSE-Leap-15.6              openSUSE Leap 15.6
SUSE-Linux-Enterprise-15-SP5    SUSE Linux Enterprise 15 SP5
SUSE-Linux-Enterprise-15-SP6    SUSE Linux Enterprise 15 SP6
openSUSE-Tumbleweed             openSUSE Tumbleweed
```

For this quickstart, install Ubuntu 22.04, and then install SQL Server 2022 into that distribution.

To install Ubuntu 22.04, run the following command. Make a note of the UNIX user account and password. In this example, use `wsluser` as the username.

```console
wsl --install -d Ubuntu-22.04
```

You should see output similar to the following example. At the end, it should show that you're logged into the Ubuntu 22.04 bash shell.

```output
Installing: Ubuntu 22.04 LTS
Ubuntu 22.04 LTS has been installed.
Launching Ubuntu 22.04 LTS...
Installing, this may take a few minutes...
Please create a default UNIX user account. The username does not need to match your Windows username.
For more information visit: https://aka.ms/wslusers
Enter new UNIX username: wsluser
New password:
Retype new password:
passwd: password updated successfully
Installation successful!
To run a command as administrator (user "root"), use "sudo <command>".
See "man sudo_root" for details.

Welcome to Ubuntu 22.04.5 LTS (GNU/Linux 5.15.167.4-microsoft-standard-WSL2 x86_64)

 * Documentation:  https://help.ubuntu.com
 * Management:     https://landscape.canonical.com
 * Support:        https://ubuntu.com/pro

 System information as of Tue Dec  3 00:32:14 IST 2024

  System load:  0.33                Processes:             32
  Usage of /:   0.1% of 1006.85GB   Users logged in:       0
  Memory usage: 2%                  IPv4 address for eth0: 10.18.123.249
  Swap usage:   0%

This message is shown once a day. To disable it please create the
/home/wsluser/.hushlogin file.
```

### Install SQL Server

Once you're logged into the Ubuntu 22.04 bash shell, you can follow the steps outlined in [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md?view=sql-server-ver16&preserve-view=true&tabs=ubuntu2204#install-sql-server) to install SQL Server 2022.

You should [install the SQL Server command-line tools](quickstart-install-connect-ubuntu.md?view=sql-server-ver16&preserve-view=true&tabs=ubuntu2204#install-the-sql-server-command-line-tools) as well.

### Get IP address

To identify the IP address to connect to using SQL Server Management Studio (SSMS), run the `ifconfig` command as follows:

```bash
ifconfig
```

You should see output similar to the following example.

```output
eth0: flags=4163<UP,BROADCAST,RUNNING,MULTICAST>  mtu 1500
        inet 10.19.50.241  netmask 255.255.240.0  broadcast 10.19.63.255
        inet6 fe80::215:5dff:fe76:c05d  prefixlen 64  scopeid 0x20<link>
        ether 00:15:5d:76:c0:5d  txqueuelen 1000  (Ethernet)
        RX packets 2146  bytes 1452448 (1.4 MB)
        RX errors 0  dropped 0  overruns 0  frame 0
        TX packets 1905  bytes 345288 (345.2 KB)
        TX errors 0  dropped 0 overruns 0  carrier 0  collisions 0

lo: flags=73<UP,LOOPBACK,RUNNING>  mtu 65536
        inet 127.0.0.1  netmask 255.0.0.0
        inet6 ::1  prefixlen 128  scopeid 0x10<host>
        loop  txqueuelen 1000  (Local Loopback)
        RX packets 2039  bytes 4144340 (4.1 MB)
        RX errors 0  dropped 0  overruns 0  frame 0
        TX packets 2039  bytes 4144340 (4.1 MB)
        TX errors 0  dropped 0 overruns 0  carrier 0  collisions 0
```

## Deploy SQL Server containers in WSL

To deploy containers in WSL, you first need to install a Linux container engine, such as Docker. For more information, see [Get started with Docker remote containers on WSL](/windows/wsl/tutorials/wsl-containers). Once you have the Docker engine installed, deploy the SQL Server container image as follows.

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" \
-e "MSSQL_PID=Developer" -e "MSSQL_AGENT_ENABLED=true" \
-p 1433:1433 --name sqlcontainerwsl --hostname sqlcontainerwsl \
-d mcr.microsoft.com/mssql/server:2022-latest
```

> [!NOTE]  
> [!INCLUDE [password-complexity](includes/password-complexity.md)]

### Add persistent storage with WSL for SQL Server containers

You can create data volumes as described in [Mount a host directory as data volume](sql-server-linux-docker-container-configure.md?pivots=cs1-bash#mount-a-host-directory-as-data-volume).

For example, run the following command to set up a volume called `sql_volume` located at `/var/opt/mssql/`.

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" \
-e "MSSQL_PID=Developer" -e "MSSQL_AGENT_ENABLED=true" \
-p 1433:1433 --name sqlcontainerwsl --hostname sqlcontainerwsl \
-v sql_volume:/var/opt/mssql/ \
-d mcr.microsoft.com/mssql/server:2022-latest
```

Even if you run the `wsl --terminate` command, the data isn't lost. When you start up WSL again and run the `docker run` command to deploy using the `sql_volume` volume, it still has all the data intact.

If you want to delete the persisted volume, make sure that the container using the volume is stopped and removed, and run the following command.

```bash
docker volume rm sql_volume
```

## Remarks

You should be able to configure most of the features supported for SQL Server on Linux for development purposes, except the business continuity features that are dependent on clustering stacks. These features, such as Pacemaker or HPE Serviceguard, aren't supported on WSL.

For a full list of unsupported features for SQL Server on Linux, see [Editions and supported features of SQL Server 2022 on Linux](sql-server-linux-editions-and-components-2022.md).

[!INCLUDE [Connect, create, and query data](includes/quickstart-connect-query.md)]
