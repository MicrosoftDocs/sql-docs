---
title: Install the Microsoft ODBC driver for SQL Server (Linux)
description: Learn how to install the Microsoft ODBC Driver for SQL Server on Linux clients to enable database connectivity.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ms.custom:
  - intro-installation
  - linux-related-content
helpviewer_keywords:
  - "driver, installing"
---

# Install the Microsoft ODBC driver for SQL Server (Linux)

This article explains how to install the Microsoft ODBC Driver for SQL Server on Linux. It also includes instructions for the optional command-line tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC development headers.

This article provides commands for installing the ODBC driver from the bash shell. If you want to download the packages directly, see [Download ODBC Driver for SQL Server](../download-odbc-driver-for-sql-server.md).

<a id="18"></a>

## Microsoft ODBC 18

The following sections explain how to install the Microsoft ODBC driver 18 from the bash shell for different Linux distributions. Supported distributions are Alpine Linux, Debian, Red Hat Enterprise Linux (RHEL), Oracle Linux, SUSE Linux Enterprise Server (SLES), Ubuntu, and Azure Linux. Starting with version 18.4, to accept the End User License Agreement (EULA) automatically when installing the non-Alpine Linux (`.deb` or `.rpm`) driver, you can create the file `/opt/microsoft/msodbcsql18/ACCEPT_EULA`.

### [Alpine](#tab/alpine18-install)

```bash
case $(uname -m) in
    x86_64) architecture="amd64" ;;
    arm64) architecture="arm64" ;;
    *) architecture="unsupported" ;;
esac
if [[ "unsupported" == "$architecture" ]];
then
    echo "Alpine architecture $(uname -m) is not currently supported.";
    exit;
fi

#Download the desired package(s)
curl -O https://download.microsoft.com/download/0b3d5518-b4a7-4a2b-afc7-7ee9e967f93c/msodbcsql18_18.6.2.1-1_$architecture.apk
curl -O https://download.microsoft.com/download/cad0d30f-b9b1-4765-a011-81d8a66c8b8d/mssql-tools18_18.6.2.1-1_$architecture.apk

#(Optional) Verify signature, if 'gpg' is missing install it using 'apk add gnupg':
curl -O https://download.microsoft.com/download/0b3d5518-b4a7-4a2b-afc7-7ee9e967f93c/msodbcsql18_18.6.2.1-1_$architecture.sig
curl -O https://download.microsoft.com/download/cad0d30f-b9b1-4765-a011-81d8a66c8b8d/mssql-tools18_18.6.2.1-1_$architecture.sig

curl https://packages.microsoft.com/keys/microsoft.asc | gpg --import -
gpg --verify msodbcsql18_18.6.2.1-1_$architecture.sig msodbcsql18_18.6.2.1-1_$architecture.apk
gpg --verify mssql-tools18_18.6.2.1-1_$architecture.sig mssql-tools18_18.6.2.1-1_$architecture.apk

#Install the package(s)
sudo apk add --allow-untrusted msodbcsql18_18.6.2.1-1_$architecture.apk
sudo apk add --allow-untrusted mssql-tools18_18.6.2.1-1_$architecture.apk
```

> [!NOTE]  
> Driver version 17.5 or higher is required for Alpine support.

### [Debian](#tab/debian18-install)

```bash
if ! [[ "9 10 11 12 13" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "Debian $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/debian/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.deb
# Install the package
sudo dpkg -i packages-microsoft-prod.deb
# Delete the file
rm packages-microsoft-prod.deb

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
> Instead of setting the environment variable `ACCEPT_EULA`, you can set the `debconf` variable `msodbcsql/ACCEPT_EULA`:
>
> ```bash
> echo msodbcsql18 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

### [RHEL and Oracle Linux](#tab/redhat18-install)

```bash
if ! [[ "7 8 9 10" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "RHEL $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/rhel/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo yum install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

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
if ! [[ "12 15" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "SLES $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Import the GPG key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/sles/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo zypper install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

sudo zypper update
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
if ! [[ "18.04 20.04 22.04 24.04 25.10" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)"* ]];
then
    echo "Ubuntu $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/ubuntu/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)/packages-microsoft-prod.deb
# Install the package
sudo dpkg -i packages-microsoft-prod.deb
# Delete the file
rm packages-microsoft-prod.deb

# Install the driver
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
> Instead of setting the environment variable `ACCEPT_EULA`, you can set the `debconf` variable `msodbcsql/ACCEPT_EULA`:
>
> ```bash
> echo msodbcsql18 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

### [Azure Linux](#tab/azure18-install)

> [!NOTE]
> Azure Linux 3.0 includes the Microsoft packages repository by default. You don't need to manually configure the repository before you install the driver. If your Azure Linux image uses `tdnf` as the package manager, the following commands work directly.

```bash
if ! [[ "3.0" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)"* ]];
then
    echo "Azure Linux $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2) is not currently supported.";
    exit;
fi

sudo ACCEPT_EULA=Y tdnf install -y msodbcsql18
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y tdnf install -y mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo tdnf install -y unixODBC-devel
```

> [!NOTE]  
> If you don't set the `ACCEPT_EULA` environment variable, the installation still continues. However, you need to manually run `/opt/microsoft/msodbcsql18/accept-eula.sh` and `/opt/mssql-tools18/bin/accept-eula.sh` to agree to the license terms. To automatically accept the EULA during installation, you can create the files `/opt/microsoft/msodbcsql18/ACCEPT_EULA` and `/opt/mssql-tools18/ACCEPT_EULA`.

---

## Previous versions

The following sections provide instructions for installing previous versions of the Microsoft ODBC driver on Linux. The following driver versions are covered:

- [Microsoft ODBC driver 17 for SQL Server](#17)
- [Microsoft ODBC driver 13.1 for SQL Server](#13.1)
- [Microsoft ODBC driver 13 for SQL Server](#13)
- [Microsoft ODBC driver 11 for SQL Server](#11)

<a id="17"></a>

### Microsoft ODBC 17

The following sections explain how to install the Microsoft ODBC driver 17 from the bash shell for different Linux distributions.

> [!IMPORTANT]  
> If you installed the v17 `msodbcsql` package that was briefly available, remove it before installing the `msodbcsql17` package to avoid conflicts. You can install the `msodbcsql17` package side by side with the `msodbcsql` v13 package.

### [Alpine](#tab/alpine17-install)

```bash
#Download the desired package(s)
curl -O https://download.microsoft.com/download/607ebe2c-e17c-4c34-b367-10a75b83bef9/msodbcsql17_17.11.1.1-1_amd64.apk
curl -O https://download.microsoft.com/download/aca0282f-6a67-49c0-ae08-887d59d16d1a/mssql-tools_17.11.1.1-1_amd64.apk

#(Optional) Verify signature, if 'gpg' is missing install it using 'apk add gnupg':
curl -O https://download.microsoft.com/download/607ebe2c-e17c-4c34-b367-10a75b83bef9/msodbcsql17_17.11.1.1-1_amd64.sig
curl -O https://download.microsoft.com/download/aca0282f-6a67-49c0-ae08-887d59d16d1a/mssql-tools_17.11.1.1-1_amd64.sig

curl https://packages.microsoft.com/keys/microsoft.asc | gpg --import -
gpg --verify msodbcsql17_17.11.1.1-1_amd64.sig msodbcsql17_17.11.1.1-1_amd64.apk
gpg --verify mssql-tools_17.11.1.1-1_amd64.sig mssql-tools_17.11.1.1-1_amd64.apk

#Install the package(s)
sudo apk add --allow-untrusted msodbcsql17_17.11.1.1-1_amd64.apk
sudo apk add --allow-untrusted mssql-tools_17.11.1.1-1_amd64.apk
```

> [!NOTE]  
> Driver version 17.5 or higher is required for Alpine support.

### [Debian](#tab/debian17-install)

```bash
if ! [[ "8 9 10 11 12 13" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "Debian $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/debian/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.deb
# Install the package
sudo dpkg -i packages-microsoft-prod.deb
# Delete the file
rm packages-microsoft-prod.deb

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
> Instead of setting the environment variable `ACCEPT_EULA`, you can set the `debconf` variable `msodbcsql/ACCEPT_EULA`:
>
> ```bash
> echo msodbcsql17 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

### [RHEL and Oracle Linux](#tab/redhat17-install)

```bash
if ! [[ "6 7 8 9 10" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "RHEL $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/rhel/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo yum install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

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
if ! [[ "11 12 15 16" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "SLES $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Import the GPG key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/sles/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo zypper install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

# If you need driver 17.3 or below on SLES 15, you also need to run:
# sudo SUSEConnect -p sle-module-legacy/15/x86_64

sudo zypper update
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
if ! [[ "14.04 16.04 18.04 20.04 22.04 24.04 25.10" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)"* ]];
then
    echo "Ubuntu $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/ubuntu/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)/packages-microsoft-prod.deb
# Install the package
sudo dpkg -i packages-microsoft-prod.deb
# Delete the file
rm packages-microsoft-prod.deb

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
> Instead of setting the environment variable `ACCEPT_EULA`, you can set the `debconf` variable `msodbcsql/ACCEPT_EULA`:
>
> ```bash
> echo msodbcsql17 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

---

<a id="13.1"></a>

### ODBC 13.1

The following sections explain how to install the Microsoft ODBC driver 13.1 from the bash shell for different Linux distributions.

### [Debian 8](#tab/debian8-install)

```bash
if ! [[ "8" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "Debian $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/debian/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.deb
# Install the package
sudo dpkg -i packages-microsoft-prod.deb
# Delete the file
rm packages-microsoft-prod.deb

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
if ! [[ "6 7" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "RHEL $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/rhel/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo yum install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

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
if ! [[ "11 12" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "SLES $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Import the GPG key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/sles/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo zypper install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

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
if ! [[ "14.04 16.04" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)"* ]];
then
    echo "Ubuntu $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/ubuntu/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)/packages-microsoft-prod.deb
# Install the package
sudo dpkg -i packages-microsoft-prod.deb
# Delete the file
rm packages-microsoft-prod.deb

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

<a id="13"></a>

### ODBC 13

The following sections explain how to install the Microsoft ODBC driver 13 from the bash shell for different Linux distributions.

### [RHEL 7 (ODBC 13)](#tab/redhat7-13-install)

```bash
if ! [[ "6 7" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "RHEL $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/rhel/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo yum install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

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
if ! [[ "14.04 16.04" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)"* ]];
then
    echo "Ubuntu $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2) is not currently supported.";
    exit;
fi

# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/ubuntu/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)/packages-microsoft-prod.deb
# Install the package
sudo dpkg -i packages-microsoft-prod.deb
# Delete the file
rm packages-microsoft-prod.deb

sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql=13.0.1.0-1 mssql-tools=14.0.2.0-1
sudo apt-get install unixodbc-dev-utf16 #this step is optional but recommended*
#Create symlinks for tools
sudo ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
sudo ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### [SLES 12 (ODBC 13)](#tab/suse12-13-install)

```bash
if ! [[ "11 12" == *"$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)"* ]];
then
    echo "SLES $(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1) is not currently supported.";
    exit;
fi

# Import the GPG key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -sSL -O https://packages.microsoft.com/config/sles/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.rpm
# Install the package
sudo zypper install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

sudo zypper update
sudo ACCEPT_EULA=Y zypper install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo zypper install unixODBC-utf16-devel
#Create symlinks for tools
sudo ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
sudo ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

---

<a id="offline-installation"></a>

#### Offline installation for ODBC 13

If you need to install the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 on a computer with no Internet connection, you must resolve package dependencies manually. The [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 has the following direct dependencies:

- Ubuntu: `libc6` (>= 2.21), `libstdc++6` (>= 4.9), `libkrb5-3`, `libcurl3`, `openssl`, `debconf` (>= 0.5), `unixODBC` (>= 2.3.1-1)
- Red Hat: `glibc`, `e2fsprogs`, `krb5-libs`, `openssl`, `unixODBC`
- SUSE: `glibc`, `libuuid1`, `krb5`, `openssl`, `unixODBC`

Each of these packages has its own dependencies, which might or might not be present on the system. For a general solution to this issue, refer to your distribution's package manager documentation: [Red Hat](https://access.redhat.com/solutions/7019225), [Ubuntu](https://unix.stackexchange.com/questions/87130/how-to-quickly-create-a-local-apt-repository-for-random-packages-using-a-debian), and [SUSE](https://en.opensuse.org/Portal:Zypper).

You can manually download all the dependent packages and place them together on the installation computer. Then, manually install each package in turn, finishing with the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 package.

#### [RHEL 7 (ODBC 13 offline)](#tab/rhel7-offline)

- Download the latest `msodbcsql` package from <https://packages.microsoft.com/rhel/7/prod/>.
- Install dependencies and the driver.

```bash
sudo yum install glibc e2fsprogs krb5-libs openssl unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

#### [SLES 12 (ODBC 13 offline)](#tab/sles-offline)

- Download the latest `msodbcsql` package from <https://packages.microsoft.com/sles/12/prod/>.
- Install the dependencies and the driver.

```bash
sudo zypper install glibc, libuuid1, krb5, openssl, unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

After you complete the package installation, you can verify that the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 can find all its dependencies by running `ldd` and inspecting its output for missing libraries:

```bash
ldd /opt/microsoft/msodbcsql/lib64/libmsodbcsql-*
```

#### [Ubuntu 16.04 (ODBC 13 offline)](#tab/ubuntu-offline)

- Download the latest `msodbcsql` package from <https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/>.
- Install dependencies and the driver.

```bash
sudo apt-get install libc6 libstdc++6 libkrb5-3 libcurl3 openssl debconf unixodbc unixodbc-dev #install dependencies
sudo dpkg -i msodbcsql_13.1.X.X-X_amd64.deb #install the Driver
```

---

<a id="11"></a>

### ODBC 11

The following sections explain how to install the Microsoft ODBC driver 11 on Linux. Before you can use the driver, install the unixODBC driver manager. For more information, see [Installing the Driver Manager](installing-the-driver-manager.md).

#### Installation steps

> [!IMPORTANT]  
> These instructions refer to `msodbcsql-11.0.2270.0.tar.gz`, which is installation file for Red Hat Linux. If you install for SUSE Linux, the file name is `msodbcsql-11.0.2260.0.tar.gz`.

To install the driver:

1. Make sure that you have root permission.

1. Download the driver.

   | Distribution | Driver | SHA256 hash |
   | --- | --- | --- |
   | Red Hat 5 | [msodbcsql-11.0.2270.0.tar.gz](https://go.microsoft.com/fwlink/?linkid=2299904) | [178280daf01a49b8322cd902b6440979adacd594c01cd2a1f081dda23dbfb343](https://go.microsoft.com/fwlink/?linkid=2299695) |
   | Red Hat 6 | [msodbcsql-11.0.2270.0.tar.gz](https://go.microsoft.com/fwlink/?linkid=2300003) | [e9b6bd33d174c7753b3a3f2d541713fbc156b46254484a169caa3f459dd828f7](https://go.microsoft.com/fwlink/?linkid=2299903) |
   | SUSE Linux | [msodbcsql-11.0.2260.0.tar.gz](https://go.microsoft.com/fwlink/?linkid=2300002) | 86d1c5842be4f0095234a9455e18a04fdf4cc7960ec0255b37258112e2391ef5 |

1. Change to the directory where the download placed the file `msodbcsql-11.0.2270.0.tar.gz`. Make sure that you have the `*.tar.gz` file that matches your version of Linux. To extract the files, run the following command:

   ```bash
   tar xvzf msodbcsql-11.0.2270.0.tar.gz
   ```

1. Change to the `msodbcsql-11.0.2270.0` directory. You should see a file called `install.sh`.

1. To see a list of the available installation options, run the following command:

   ```bash
   ./install.sh
   ```

1. Make a backup of `odbcinst.ini`. The driver installation updates `odbcinst.ini`. odbcinst.ini contains the list of drivers that are registered with the unixODBC Driver Manager. To discover the location of odbcinst.ini on your computer, run the following command:

   ```bash
   odbc_config --odbcinstini
   ```

1. Before you install the driver, run the following command:

   ```bash
   ./install.sh verify
   ```

   The output of `./install.sh verify` reports if your computer has the required software to support the ODBC driver on Linux.

1. When you're ready to install the ODBC driver on Linux, run the command:

   ```bash
   ./install.sh install
   ```

   If you need to specify an install command (`bin-dir` or `lib-dir`), specify the command after the `install` option.

1. After reviewing the license agreement, type `YES` to continue with the installation.

Installation puts the driver in `/opt/microsoft/msodbcsql/11.0.2270.0`. The driver and its support files must be in `/opt/microsoft/msodbcsql/11.0.2270.0`.

To verify that the Microsoft ODBC driver on Linux was registered successfully, run the following command:

```bash
odbcinst -q -d -n "ODBC Driver 11 for SQL Server"
```

#### Uninstall

Uninstall the ODBC driver 11 on Linux by running the following commands:

```bash
rm -f /usr/bin/sqlcmd
rm -f /usr/bin/bcp
rm -rf /opt/microsoft/msodbcsql
odbcinst -u -d -n "ODBC Driver 11 for SQL Server"
```

## Driver files

The ODBC driver on Linux includes the following components:

| Component | Description |
| --- | --- |
| `libmsodbcsql-17.X.so.X.X` or `libmsodbcsql-13.X.so.X.X` | The shared object (`so`) dynamic library file that contains all of the driver's functionality. The installation path is `/opt/microsoft/msodbcsql17/lib64/` for Driver 17 and `/opt/microsoft/msodbcsql/lib64/` for Driver 13. |
| `msodbcsqlr17.rll` or `msodbcsqlr13.rll` | The resource file that accompanies the driver library. The installation path is `[driver .so directory]../share/resources/en_US/` |
| `msodbcsql.h` | The header file that contains all of the new definitions needed to use the driver.<br /><br />**Note:** You can't reference `msodbcsql.h` and `odbcss.h` in the same program.<br /><br />The installation path is `/opt/microsoft/msodbcsql17/include/` for Driver 17 and `/opt/microsoft/msodbcsql/include/` for Driver 13. |
| `LICENSE.txt` | The text file that contains the terms of the End-User License Agreement. The installation path is `/usr/share/doc/msodbcsql17/` for Driver 17 and `/usr/share/doc/msodbcsql/` for Driver 13. |
| `RELEASE_NOTES` | The text file that contains release notes. The installation path is `/usr/share/doc/msodbcsql17/` for Driver 17 and `/usr/share/doc/msodbcsql/` for Driver 13. |

## Resource file loading

The driver needs to load the resource file to function. This file is called `msodbcsqlr17.rll` or `msodbcsqlr13.rll` depending on the driver version. The location of the `.rll` file is relative to the location of the driver itself (`so` or `dylib`), as described in the previous table. Starting with version 17.1, if the driver can't load the `.rll` file from the relative path, it also tries to load the `.rll` file from the default directory. The default resource file path on Linux is `/opt/microsoft/msodbcsql17/share/resources/en_US/`.

## Troubleshoot

### Previous driver version conflict

If you previously installed and registered a version of the driver with unixODBC, installation might fail with an error similar to:

```output
Installation failed, ODBC Driver $1 for SQL Server detected!
```

To resolve the problem, unregister that version of the driver by using the `odbcinst` command. Replace `$1` with the version of the driver reported in the installation error:

```bash
odbcinst -u -d -n "ODBC Driver $1 for SQL Server"
```

If uninstalling by using the `odbcinst` command fails, you can manually remove driver sections from the `odbcinst.ini` file. You can find the location of the `odbcinst.ini` file by using the command `odbcinst -j`.

### Package download errors on Debian and Ubuntu

On Debian-based distributions, you might see the following error when you install the `packages-microsoft-prod.deb` package:

```output
dpkg-deb: error: 'packages-microsoft-prod.deb' is not a Debian format archive
```

This error typically means the downloaded file is corrupted or incomplete, often caused by a network interruption or proxy issue. To resolve the problem:

1. Delete the corrupted file and download it again. For Ubuntu:

   ```bash
   rm packages-microsoft-prod.deb
   curl -sSL -O https://packages.microsoft.com/config/ubuntu/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)/packages-microsoft-prod.deb
   ```

   For Debian, replace `ubuntu` and use the major version number only:

   ```bash
   rm packages-microsoft-prod.deb
   curl -sSL -O https://packages.microsoft.com/config/debian/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.deb
   ```

1. Verify the download is a valid Debian package (the output should include `Debian binary package`):

   ```bash
   file packages-microsoft-prod.deb
   ```

1. If the download continues to fail, check your network connection and proxy settings, or use `wget` instead of `curl`:

   ```bash
   wget https://packages.microsoft.com/config/ubuntu/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)/packages-microsoft-prod.deb
   ```

### Connection problems

If you can't make a connection to SQL Server by using the ODBC driver, see the known issues article on [troubleshooting connection problems](known-issues-in-this-version-of-the-driver.md#connectivity).

## Related content

- [C / C++ ODBC example application accesses a SQL database](../cpp-code-example-app-connect-access-sql-db.md)
- [Developing Applications](../../../odbc/reference/develop-app/developing-applications.md)
- [Release notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS](release-notes-odbc-sql-server-linux-mac.md)
- [System Requirements (Linux and macOS)](system-requirements.md)
