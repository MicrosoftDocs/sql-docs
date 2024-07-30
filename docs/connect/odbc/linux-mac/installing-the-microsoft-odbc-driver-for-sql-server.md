---
title: Install the Microsoft ODBC driver for SQL Server (Linux)
description: Learn how to install the Microsoft ODBC Driver for SQL Server on Linux clients to enable database connectivity.
author: David-Engel
ms.author: davidengel
ms.date: 07/31/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
ms.custom:
  - intro-installation
  - linux-related-content
helpviewer_keywords:
  - "driver, installing"
---

# Install the Microsoft ODBC driver for SQL Server (Linux)

This article explains how to install the Microsoft ODBC Driver for SQL Server on Linux. It also includes instructions for the optional command-line tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC development headers.

This article provides commands for installing the ODBC driver from the bash shell. If you want to download the packages directly, see [Download ODBC Driver for SQL Server](../download-odbc-driver-for-sql-server.md).

## <a id="18"></a> Microsoft ODBC 18

The following sections explain how to install the Microsoft ODBC driver 18 from the bash shell for different Linux distributions. Supported distributions are Alpine Linux, Debian, Red Hat Enterprise Linux (RHEL), Oracle Linux, SUSE Linux Enterprise Server (SLES), and Ubuntu. Starting with version 18.4, to accept the EULA automatically when installing the non-Alpine Linux (.deb or .rpm) driver, you can create the file '/opt/microsoft/msodbcsql18/ACCEPT_EULA'.

### [Alpine](#tab/alpine18-install)

```bash
case $(uname -m) in
    x86_64)   architecture="amd64" ;;
    arm64)   architecture="arm64" ;;
    *) architecture="unsupported" ;;
esac
if [[ "unsupported" == "$architecture" ]];
then
    echo "Alpine architecture $(uname -m) is not currently supported.";
    exit;
fi

#Download the desired package(s)
curl -O https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_$architecture.apk
curl -O https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/mssql-tools18_18.4.1.1-1_$architecture.apk

#(Optional) Verify signature, if 'gpg' is missing install it using 'apk add gnupg':
curl -O https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_$architecture.sig
curl -O https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/mssql-tools18_18.4.1.1-1_$architecture.sig

curl https://packages.microsoft.com/keys/microsoft.asc  | gpg --import -
gpg --verify msodbcsql18_18.4.1.1-1_$architecture.sig msodbcsql18_18.4.1.1-1_$architecture.apk
gpg --verify mssql-tools18_18.4.1.1-1_$architecture.sig mssql-tools18_18.4.1.1-1_$architecture.apk

#Install the package(s)
sudo apk add --allow-untrusted msodbcsql18_18.4.1.1-1_$architecture.apk
sudo apk add --allow-untrusted mssql-tools18_18.4.1.1-1_$architecture.apk
```

> [!NOTE]  
> Driver version 17.5 or higher is required for Alpine support.

### [Debian](#tab/debian18-install)

```bash
#Debian 9-11
curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc

# Debian 12
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#Debian 9
curl https://packages.microsoft.com/config/debian/9/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

#Debian 10
curl https://packages.microsoft.com/config/debian/10/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

#Debian 11
curl https://packages.microsoft.com/config/debian/11/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

#Debian 12
curl https://packages.microsoft.com/config/debian/12/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install -y unixodbc-dev
# optional: kerberos library for debian-slim distributions
sudo apt-get install -y libgssapi-krb5-2
```

> [!NOTE]  
> You can substitute setting the environment variable `ACCEPT_EULA` with setting the debconf variable `msodbcsql/ACCEPT_EULA` instead: `echo msodbcsql18 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections`

#### Error: The following signatures couldn't be verified because the public key is not available

**Symptom**

You get an error while running `apt-get update` similar to the following text:

```output
W: GPG error: https://packages.microsoft.com/debian/12/prod bookworm InRelease: The following signatures couldn't be verified because the public key is not available: NO_PUBKEY EB3E94ADBE1229CF
E: The repository 'https://packages.microsoft.com/debian/12/prod bookworm InRelease' is not signed.
N: Updating from such a repository can't be done securely, and is therefore disabled by default.
N: See apt-secure(8) manpage for repository creation and user configuration details.
```

**Resolution**

Once you download the `/etc/apt/sources.list.d/mssql-release.list` file, edit it to remove the `signed-by` value.

- **Old:** `deb [arch=amd64,arm64,armhf signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/debian/12/prod bookworm main`

- **New:** `deb [arch=amd64,arm64,armhf] https://packages.microsoft.com/debian/12/prod bookworm main`

Run `sudo apt-get update` and continue the installation from there.

### [RHEL and Oracle Linux](#tab/redhat18-install)

```bash
#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#RHEL 7 and Oracle Linux 7
curl https://packages.microsoft.com/config/rhel/7/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo

#RHEL 8 and Oracle Linux 8
curl https://packages.microsoft.com/config/rhel/8/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo

#RHEL 9
curl https://packages.microsoft.com/config/rhel/9/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo

sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install -y msodbcsql18
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install -y mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install -y unixODBC-devel
```

### [SLES](#tab/suse18-install)

```bash
curl -O https://packages.microsoft.com/keys/microsoft.asc
sudo rpm --import microsoft.asc

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#SLES 12
sudo zypper ar https://packages.microsoft.com/config/sles/12/prod.repo

#SLES 15
sudo zypper ar https://packages.microsoft.com/config/sles/15/prod.repo
#(Only for driver 17.3 and below)
sudo SUSEConnect -p sle-module-legacy/15/x86_64

sudo ACCEPT_EULA=Y zypper install -y msodbcsql18
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install -y mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install -y unixODBC-devel
```

### [Ubuntu](#tab/ubuntu18-install)

```bash
if ! [[ "18.04 20.04 22.04 23.04 24.04" == *"$(lsb_release -rs)"* ]];
then
    echo "Ubuntu $(lsb_release -rs) is not currently supported.";
    exit;
fi

curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc

curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install -y unixodbc-dev
```

> [!NOTE]  
> You can substitute setting the environment variable 'ACCEPT_EULA' with setting the debconf variable 'msodbcsql/ACCEPT_EULA' instead: `echo msodbcsql18 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections`

---

## Previous versions

The following sections provide instructions for installing previous versions of the Microsoft ODBC driver on Linux. The following driver versions are covered:

- [Microsoft ODBC driver 17 for SQL Server](#17)
- [Microsoft ODBC driver 13.1 for SQL Server](#13.1)
- [Microsoft ODBC driver 13 for SQL Server](#13)
- [Microsoft ODBC driver 11 for SQL Server](#11)

## <a id="17"></a> Microsoft ODBC 17

The following sections explain how to install the Microsoft ODBC driver 17 from the bash shell for different Linux distributions.

> [!IMPORTANT]  
> If you installed the v17 `msodbcsql` package that was briefly available, you should remove it before installing the `msodbcsql17` package. This will avoid conflicts. The `msodbcsql17` package can be installed side by side with the `msodbcsql` v13 package.

### [Alpine](#tab/alpine17-install)

```bash
#Download the desired package(s)
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.10.5.1-1_amd64.apk
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/mssql-tools_17.10.1.1-1_amd64.apk

#(Optional) Verify signature, if 'gpg' is missing install it using 'apk add gnupg':
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.10.5.1-1_amd64.sig
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/mssql-tools_17.10.1.1-1_amd64.sig

curl https://packages.microsoft.com/keys/microsoft.asc  | gpg --import -
gpg --verify msodbcsql17_17.10.5.1-1_amd64.sig msodbcsql17_17.10.5.1-1_amd64.apk
gpg --verify mssql-tools_17.10.1.1-1_amd64.sig mssql-tools_17.10.1.1-1_amd64.apk

#Install the package(s)
sudo apk add --allow-untrusted msodbcsql17_17.10.5.1-1_amd64.apk
sudo apk add --allow-untrusted mssql-tools_17.10.1.1-1_amd64.apk
```

> [!NOTE]  
> Driver version 17.5 or higher is required for Alpine support.

### [Debian](#tab/debian17-install)

```bash
curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#Debian 9
curl https://packages.microsoft.com/config/debian/9/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

#Debian 10
curl https://packages.microsoft.com/config/debian/10/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

#Debian 11
curl https://packages.microsoft.com/config/debian/11/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

#Debian 12
curl https://packages.microsoft.com/config/debian/12/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y msodbcsql17
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install -y unixodbc-dev
# optional: kerberos library for debian-slim distributions
sudo apt-get install -y libgssapi-krb5-2
```

> [!NOTE]  
> You can substitute setting the environment variable 'ACCEPT_EULA' with setting the debconf variable 'msodbcsql/ACCEPT_EULA' instead: `echo msodbcsql17 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections`

### [RHEL and Oracle Linux](#tab/redhat17-install)

```bash
#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#RHEL 7 and Oracle Linux 7
curl https://packages.microsoft.com/config/rhel/7/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo

#RHEL 8 and Oracle Linux 8
curl https://packages.microsoft.com/config/rhel/8/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo

#RHEL 9
curl https://packages.microsoft.com/config/rhel/9/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo

sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install -y msodbcsql17
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install -y mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install -y unixODBC-devel
```

### [SLES](#tab/suse17-install)

```bash
curl -O https://packages.microsoft.com/keys/microsoft.asc
sudo rpm --import microsoft.asc

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#SLES 12
sudo zypper ar https://packages.microsoft.com/config/sles/12/prod.repo

#SLES 15
sudo zypper ar https://packages.microsoft.com/config/sles/15/prod.repo
#(Only for driver 17.3 and below)
sudo SUSEConnect -p sle-module-legacy/15/x86_64

sudo ACCEPT_EULA=Y zypper install -y msodbcsql17
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install -y mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install -y unixODBC-devel
```

### [Ubuntu](#tab/ubuntu17-install)

```bash
if ! [[ "16.04 18.04 20.04 22.04" == *"$(lsb_release -rs)"* ]];
then
    echo "Ubuntu $(lsb_release -rs) is not currently supported.";
    exit;
fi

curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc

curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list

sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y msodbcsql17
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install -y unixodbc-dev
```

> [!NOTE]  
> You can substitute setting the environment variable 'ACCEPT_EULA' with setting the debconf variable 'msodbcsql/ACCEPT_EULA' instead: `echo msodbcsql17 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections`

---

## <a id="13.1"></a> ODBC 13.1

The following sections explain how to install the Microsoft ODBC driver 13.1 from the bash shell for different Linux distributions.

### [Debian 8](#tab/debian8-install)

```bash
curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
curl https://packages.microsoft.com/config/debian/8/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### [RHEL 7](#tab/redhat7-install)

```bash
curl https://packages.microsoft.com/config/rhel/7/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo
sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install unixODBC-devel
```

### [SLES 12](#tab/suse12-install)

```bash
sudo zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
sudo ACCEPT_EULA=Y zypper install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install unixODBC-devel
```

### [Ubuntu 16.04](#tab/ubuntu16-install)

```bash
curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

---

## <a id="13"></a> ODBC 13

The following sections explain how to install the Microsoft ODBC driver 13 from the bash shell for different Linux distributions.

### [RHEL 7 (ODBC 13)](#tab/redhat7-13-install)

```bash
curl https://packages.microsoft.com/config/rhel/7/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo
sudo yum update
sudo yum remove unixODBC #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo yum install unixODBC-utf16-devel #this step is optional but recommended*
#Create symlinks for tools
sudo ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
sudo ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### [Ubuntu 16.04 (ODBC 13)](#tab/ubuntu16-13-install)

```bash
curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql=13.0.1.0-1 mssql-tools=14.0.2.0-1
sudo apt-get install unixodbc-dev-utf16 #this step is optional but recommended*
#Create symlinks for tools
sudo ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
sudo ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### [SLES 12 (ODBC 13)](#tab/suse12-13-install)

```bash
sudo zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
sudo zypper update
sudo ACCEPT_EULA=Y zypper install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo zypper install unixODBC-utf16-devel
#Create symlinks for tools
sudo ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
sudo ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

---

### Offline installation

If you need the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 to be installed on a computer with no Internet connection, you must resolve package dependencies manually. The [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 has the following direct dependencies:

- Ubuntu: libc6 (>= 2.21), libstdc++6 (>= 4.9), libkrb5-3, libcurl3, openssl, debconf (>= 0.5), unixodbc (>= 2.3.1-1)
- Red Hat: ```glibc, e2fsprogs, krb5-libs, openssl, unixODBC```
- SUSE: ```glibc, libuuid1, krb5, openssl, unixODBC```

Each of these packages in turn has their own dependencies, which might or might not be present on the system. For a general solution to this issue, refer to your distribution's package manager documentation: [Red Hat](https://access.redhat.com/solutions/7019225), [Ubuntu](https://unix.stackexchange.com/questions/87130/how-to-quickly-create-a-local-apt-repository-for-random-packages-using-a-debian), and [SUSE](https://en.opensuse.org/Portal:Zypper)

It's also common to manually download all the dependent packages and place them together on the installation computer, then manually install each package in turn, finishing with the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 package.

#### [RHEL 7 (ODBC 13 offline)](#tab/rhel7-offline)

- Download the latest `msodbcsql` `.rpm` from [https://packages.microsoft.com/rhel/7/prod/](https://packages.microsoft.com/rhel/7/prod/).
- Install dependencies and the driver.

```bash
sudo yum install glibc e2fsprogs krb5-libs openssl unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

#### [SLES 12 (ODBC 13 offline)](#tab/sles-offline)

- Download the latest `msodbcsql` `.rpm` from [https://packages.microsoft.com/sles/12/prod/](https://packages.microsoft.com/sles/12/prod/).
- Install the dependencies and the driver.

```bash
sudo zypper install glibc, libuuid1, krb5, openssl, unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

After you complete the package installation, you can verify that the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 can find all its dependencies by running ldd and inspecting its output for missing libraries:

```bash
ldd /opt/microsoft/msodbcsql/lib64/libmsodbcsql-*
```

#### [Ubuntu 16.04 (ODBC 13 offline)](#tab/ubuntu-offline)

- Download the latest `msodbcsql` `.deb` from [https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/).
- Install dependencies and the driver.

```bash
sudo apt-get install libc6 libstdc++6 libkrb5-3 libcurl3 openssl debconf unixodbc unixodbc-dev #install dependencies
sudo dpkg -i msodbcsql_13.1.X.X-X_amd64.deb #install the Driver
```

---

## <a id="11"></a> ODBC 11

The following sections explain how to install the Microsoft ODBC driver 11 on Linux. Before you can use the driver, install the unixODBC driver manager. For more information, see [Installing the Driver Manager](installing-the-driver-manager.md).

### Installation steps

> [!IMPORTANT]  
> These instructions refer to `msodbcsql-11.0.2270.0.tar.gz`, which is installation file for Red Hat Linux. If you install the Preview for SUSE Linux, the file name is `msodbcsql-11.0.2260.0.tar.gz`.

To install the driver:

1. Make sure that you have root permission.

1. Change to the directory where the download placed the file `msodbcsql-11.0.2270.0.tar.gz`. Make sure that you have the \*.tar.gz file that matches your version of Linux. To extract the files, execute the following command, `tar xvzf msodbcsql-11.0.2270.0.tar.gz`.

1. Change to the `msodbcsql-11.0.2270.0` directory and there you should see a file called **install.sh**.

1. To see a list of the available installation options, execute the following command: **./install.sh**.

1. Make a backup of **odbcinst.ini**. The driver installation updates **odbcinst.ini**. odbcinst.ini contains the list of drivers that are registered with the unixODBC Driver Manager. To discover the location of odbcinst.ini on your computer, execute the following command: ```odbc_config --odbcinstini```.

1. Before you install the driver, execute the following command: `./install.sh verify`. The output of `./install.sh verify` reports if your computer has the required software to support the ODBC driver on Linux.

1. When you're ready to install the ODBC driver on Linux, execute the command: `./install.sh install`. If you need to specify an install command (`bin-dir` or `lib-dir`), specify the command after the **install** option.

1. After reviewing the license agreement, type **YES** to continue with the installation.

Installation puts the driver in `/opt/microsoft/msodbcsql/11.0.2270.0`. The driver and its support files must be in `/opt/microsoft/msodbcsql/11.0.2270.0`.

To verify that the Microsoft ODBC driver on Linux was registered successfully, execute the following command: ```odbcinst -q -d -n "ODBC Driver 11 for SQL Server"```.

### Uninstall

You can uninstall the ODBC driver 11 on Linux by executing the following commands:

1. `rm -f /usr/bin/sqlcmd`

1. `rm -f /usr/bin/bcp`

1. `rm -rf /opt/microsoft/msodbcsql`

1. `odbcinst -u -d -n "ODBC Driver 11 for SQL Server"`

## Driver files

The ODBC driver on Linux consists of the following components:

| Component | Description |
| --- | --- |
| libmsodbcsql-17.X.so.X.X or libmsodbcsql-13.X.so.X.X | The shared object (`so`) dynamic library file that contains all of the driver's functionality. This file is installed in `/opt/microsoft/msodbcsql17/lib64/` for the Driver 17 and in `/opt/microsoft/msodbcsql/lib64/` for Driver 13. |
| `msodbcsqlr17.rll` or `msodbcsqlr13.rll` | The accompanying resource file for the driver library. This file is installed in `[driver .so directory]../share/resources/en_US/` |
| msodbcsql.h | The header file that contains all of the new definitions needed to use the driver.<br /><br />**Note:** You can't reference msodbcsql.h and odbcss.h in the same program.<br /><br />msodbcsql.h is installed in `/opt/microsoft/msodbcsql17/include/` for Driver 17 and in `/opt/microsoft/msodbcsql/include/` for Driver 13. |
| LICENSE.txt | The text file that contains the terms of the End-User License Agreement. This file is placed in `/usr/share/doc/msodbcsql17/` for Driver 17 and in `/usr/share/doc/msodbcsql/` for Driver 13. |
| RELEASE_NOTES | The text file that contains release notes. This file is placed in `/usr/share/doc/msodbcsql17/` for Driver 17 and in `/usr/share/doc/msodbcsql/` for Driver 13. |

## Resource file loading

The driver needs to load the resource file to function. This file is called `msodbcsqlr17.rll` or `msodbcsqlr13.rll` depending on the driver version. The location of the `.rll` file is relative to the location of the driver itself (`so` or `dylib`), as noted in the previous table. As of version 17.1 the driver also attempts to load the `.rll` from the default directory if loading from the relative path fails. The default resource file path on Linux is `/opt/microsoft/msodbcsql17/share/resources/en_US/`.

## Troubleshoot

If a version of the driver was previously installed and registered with unixODBC, installation might fail with an error like `Installation failed, ODBC Driver $1 for SQL Server detected!`. To resolve the problem, unregister that version of the driver. You can unregister drivers via the `odbcinst` command: `odbcinst -u -d -n "ODBC Driver $1 for SQL Server`. (Replace `$1` with the version of the driver reported in the installation error.) If uninstall via the `odbcinst` command fails, you can manually remove driver sections from the `odbcinst.ini` file. You can find the location of the `odbcinst.ini` file via the command `odbcinst -j`.

If you're unable to make a connection to SQL Server using the ODBC driver, see the known issues article on [troubleshooting connection problems](known-issues-in-this-version-of-the-driver.md#connectivity).

## Related content

- [C++ ODBC example application](../cpp-code-example-app-connect-access-sql-db.md)
- [Developing Applications](../../../odbc/reference/develop-app/developing-applications.md)
- [release notes](release-notes-odbc-sql-server-linux-mac.md)
- [system requirements](system-requirements.md)
