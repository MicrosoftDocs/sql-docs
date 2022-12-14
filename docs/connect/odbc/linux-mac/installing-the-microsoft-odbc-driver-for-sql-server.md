---
title: Install the Microsoft ODBC driver for SQL Server (Linux)
description: Learn how to install the Microsoft ODBC Driver for SQL Server on Linux clients to enable database connectivity.
author: David-Engel
ms.author: v-davidengel
ms.date: 11/09/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
ms.custom: intro-installation
helpviewer_keywords:
  - "driver, installing"
---

# Install the Microsoft ODBC driver for SQL Server (Linux)

This article explains how to install the Microsoft ODBC Driver for SQL Server on Linux. It also includes instructions for the optional command-line tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC development headers.

This article provides commands for installing the ODBC driver from the bash shell. If you want to download the packages directly, see [Download ODBC Driver for SQL Server](../download-odbc-driver-for-sql-server.md).

## <a id="18"></a> Microsoft ODBC 18

The following sections explain how to install the Microsoft ODBC driver 18 from the bash shell for different Linux distributions.

- [Alpine Linux](#alpine18)
- [Debian](#debian18)
- [Red Hat Enterprise Linux and Oracle](#redhat18)
- [SUSE Linux Enterprise Server](#suse18)
- [Ubuntu](#ubuntu18)

### <a id="alpine18"></a> Alpine Linux

```bash
#Download the desired package(s)
curl -O https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.2.1-1_amd64.apk
curl -O https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/mssql-tools18_18.1.1.1-1_amd64.apk


#(Optional) Verify signature, if 'gpg' is missing install it using 'apk add gnupg':
curl -O https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.2.1-1_amd64.sig
curl -O https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/mssql-tools18_18.1.1.1-1_amd64.sig

curl https://packages.microsoft.com/keys/microsoft.asc  | gpg --import -
gpg --verify msodbcsql18_18.1.2.1-1_amd64.sig msodbcsql18_18.1.2.1-1_amd64.apk
gpg --verify mssql-tools18_18.1.1.1-1_amd64.sig mssql-tools18_18.1.1.1-1_amd64.apk


#Install the package(s)
sudo apk add --allow-untrusted msodbcsql18_18.1.2.1-1_amd64.apk
sudo apk add --allow-untrusted mssql-tools18_18.1.1.1-1_amd64.apk
```

> [!NOTE]
> Driver version 17.5 or higher is required for Alpine support.

### <a id="debian18"></a> Debian

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#Debian 9
curl https://packages.microsoft.com/config/debian/9/prod.list > /etc/apt/sources.list.d/mssql-release.list

#Debian 10
curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list

#Debian 11
curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list

exit
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
> You can substitute setting the environment variable 'ACCEPT_EULA' with setting the debconf variable 'msodbcsql/ACCEPT_EULA' instead: `echo msodbcsql18 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections`

### <a id="redhat18"></a> Red Hat Enterprise Server and Oracle Linux

```bash
sudo su

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#Red Hat Enterprise Server 7 and Oracle Linux 7
curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/mssql-release.repo

#Red Hat Enterprise Server 8 and Oracle Linux 8
curl https://packages.microsoft.com/config/rhel/8/prod.repo > /etc/yum.repos.d/mssql-release.repo

exit
sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install -y msodbcsql18
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install -y mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install -y unixODBC-devel
```

### <a id="suse18"></a> SUSE Linux Enterprise Server

```bash
sudo su
curl -O https://packages.microsoft.com/keys/microsoft.asc
rpm --import microsoft.asc

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#SUSE Linux Enterprise Server 11 SP4
#Ensure SUSE Linux Enterprise 11 Security Module has been installed
zypper ar https://packages.microsoft.com/config/sles/11/prod.repo

#SUSE Linux Enterprise Server 12
zypper ar https://packages.microsoft.com/config/sles/12/prod.repo

#SUSE Linux Enterprise Server 15
zypper ar https://packages.microsoft.com/config/sles/15/prod.repo
#(Only for driver 17.3 and below)
SUSEConnect -p sle-module-legacy/15/x86_64

exit
sudo ACCEPT_EULA=Y zypper install -y msodbcsql18
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install -y mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install -y unixODBC-devel
```

### <a id="ubuntu18"></a> Ubuntu

```bash
if ! [[ "18.04 20.04 22.04" == *"$(lsb_release -rs)"* ]];
then
    echo "Ubuntu $(lsb_release -rs) is not currently supported.";
    exit;
fi

sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -

curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list > /etc/apt/sources.list.d/mssql-release.list

exit
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

## Previous versions

The following sections provide instructions for installing previous versions of the Microsoft ODBC driver on Linux. The following driver versions are covered:

- [Microsoft ODBC driver 17 for SQL Server](#17)
- [Microsoft ODBC driver 13.1 for SQL Server](#13.1)
- [Microsoft ODBC driver 13 for SQL Server](#13)
- [Microsoft ODBC driver 11 for SQL Server](#11)

## <a id="17"></a> Microsoft ODBC 17

The following sections explain how to install the Microsoft ODBC driver 17 from the bash shell for different Linux distributions.

- [Alpine Linux](#alpine17)
- [Debian](#debian17)
- [Red Hat Enterprise Linux and Oracle](#redhat17)
- [SUSE Linux Enterprise Server](#suse17)
- [Ubuntu](#ubuntu17)

> [!IMPORTANT]
> If you installed the v17 `msodbcsql` package that was briefly available, you should remove it before installing the `msodbcsql17` package. This will avoid conflicts. The `msodbcsql17` package can be installed side by side with the `msodbcsql` v13 package.

### <a id="alpine17"></a> Alpine Linux

```bash
#Download the desired package(s)
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.10.2.1-1_amd64.apk
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/mssql-tools_17.10.1.1-1_amd64.apk



#(Optional) Verify signature, if 'gpg' is missing install it using 'apk add gnupg':
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.10.2.1-1_amd64.sig
curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/mssql-tools_17.10.1.1-1_amd64.sig


curl https://packages.microsoft.com/keys/microsoft.asc  | gpg --import -
gpg --verify msodbcsql17_17.10.1.1-1_amd64.sig msodbcsql17_17.10.2.1-1_amd64.apk
gpg --verify mssql-tools_17.10.1.1-1_amd64.sig mssql-tools_17.10.1.1-1_amd64.apk




#Install the package(s)
sudo apk add --allow-untrusted msodbcsql17_17.10.2.1-1_amd64.apk
sudo apk add --allow-untrusted mssql-tools_17.10.1.1-1_amd64.apk

```

> [!NOTE]
> Driver version 17.5 or higher is required for Alpine support.

### <a id="debian17"></a> Debian

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#Debian 8 (only supported up to driver version 17.6)
curl https://packages.microsoft.com/config/debian/8/prod.list > /etc/apt/sources.list.d/mssql-release.list

#Debian 9
curl https://packages.microsoft.com/config/debian/9/prod.list > /etc/apt/sources.list.d/mssql-release.list

#Debian 10
curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list

#Debian 11
curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list

exit
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

### <a id="redhat17"></a> Red Hat Enterprise Server and Oracle Linux

```bash
sudo su

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#Red Hat Enterprise Server 6 (only supported up to driver version 17.7)
curl https://packages.microsoft.com/config/rhel/6/prod.repo > /etc/yum.repos.d/mssql-release.repo

#Red Hat Enterprise Server 7 and Oracle Linux 7
curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/mssql-release.repo

#Red Hat Enterprise Server 8 and Oracle Linux 8
curl https://packages.microsoft.com/config/rhel/8/prod.repo > /etc/yum.repos.d/mssql-release.repo

exit
sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install -y msodbcsql17
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install -y mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install -y unixODBC-devel
```

### <a id="suse17"></a> SUSE Linux Enterprise Server

```bash
sudo su
curl -O https://packages.microsoft.com/keys/microsoft.asc
rpm --import microsoft.asc

#Download appropriate package for the OS version
#Choose only ONE of the following, corresponding to your OS version

#SUSE Linux Enterprise Server 11 SP4
#Ensure SUSE Linux Enterprise 11 Security Module has been installed
zypper ar https://packages.microsoft.com/config/sles/11/prod.repo

#SUSE Linux Enterprise Server 12
zypper ar https://packages.microsoft.com/config/sles/12/prod.repo

#SUSE Linux Enterprise Server 15
zypper ar https://packages.microsoft.com/config/sles/15/prod.repo
#(Only for driver 17.3 and below)
SUSEConnect -p sle-module-legacy/15/x86_64

exit
sudo ACCEPT_EULA=Y zypper install -y msodbcsql17
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install -y mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install -y unixODBC-devel
```

### <a id="ubuntu17"></a> Ubuntu

```bash
if ! [[ "16.04 18.04 20.04 22.04" == *"$(lsb_release -rs)"* ]];
then
    echo "Ubuntu $(lsb_release -rs) is not currently supported.";
    exit;
fi

sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -

curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list > /etc/apt/sources.list.d/mssql-release.list

exit
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

## <a id="13.1"></a> ODBC 13.1

The following sections explain how to install the Microsoft ODBC driver 13.1 from the bash shell for different Linux distributions.

### Debian 8

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/debian/8/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### Red Hat Enterprise Server 6

```bash
sudo su
curl https://packages.microsoft.com/config/rhel/6/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install unixODBC-devel
```

### Red Hat Enterprise Server 7

```bash
sudo su
curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install unixODBC-devel
```

### SUSE Linux Enterprise Server 11

```bash
sudo su
zypper ar https://packages.microsoft.com/config/sles/11/prod.repo
exit
sudo ACCEPT_EULA=Y zypper install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install unixODBC-devel
```

### SUSE Linux Enterprise Server 12

```bash
sudo su
zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
exit
sudo ACCEPT_EULA=Y zypper install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install unixODBC-devel
```

### Ubuntu 15.10

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/15.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### Ubuntu 16.04

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### Ubuntu 16.10

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

## <a id="13"></a> ODBC 13

The following sections explain how to install the Microsoft ODBC driver 13 from the bash shell for different Linux distributions.

### Red Hat Enterprise Server 6 (ODBC 13)

```bash
sudo su
curl https://packages.microsoft.com/config/rhel/6/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum update
sudo yum remove unixODBC #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo yum install unixODBC-utf16-devel #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### Red Hat Enterprise Server 7 (ODBC 13)

```bash
sudo su
curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum update
sudo yum remove unixODBC #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo yum install unixODBC-utf16-devel #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### Ubuntu 15.10 (ODBC 13)

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/15.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql=13.0.1.0-1 mssql-tools=14.0.2.0-1
sudo apt-get install unixodbc-dev-utf16 #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### Ubuntu 16.04 (ODBC 13)

```bash
sudo su
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql=13.0.1.0-1 mssql-tools=14.0.2.0-1
sudo apt-get install unixodbc-dev-utf16 #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### SUSE Linux Enterprise Server 12 (ODBC 13)

```bash
sudo su
zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
zypper update
sudo ACCEPT_EULA=Y zypper install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
zypper install unixODBC-utf16-devel
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### Offline installation

If you prefer/require the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 to be installed on a computer with no internet connection, you'll need to resolve package dependencies manually. The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 has the following direct dependencies:

- Ubuntu: libc6 (>= 2.21), libstdc++6 (>= 4.9), libkrb5-3, libcurl3, openssl, debconf (>= 0.5), unixodbc (>= 2.3.1-1)
- Red Hat: ```glibc, e2fsprogs, krb5-libs, openssl, unixODBC```
- SUSE: ```glibc, libuuid1, krb5, openssl, unixODBC```

Each of these packages in turn has their own dependencies, which may or may not be present on the system. For a general solution to this issue, refer to your distribution's package manager documentation: [Red Hat](https://wiki.centos.org/HowTos/CreateLocalRepos), [Ubuntu](https://unix.stackexchange.com/questions/87130/how-to-quickly-create-a-local-apt-repository-for-random-packages-using-a-debian), and [SUSE](https://en.opensuse.org/Portal:Zypper)

It's also common to manually download all the dependent packages and place them together on the installation computer, then manually install each package in turn, finishing with the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 package.

#### Red Hat Linux Enterprise Server 7

- Download the latest `msodbcsql` `.rpm` from [https://packages.microsoft.com/rhel/7/prod/](https://packages.microsoft.com/rhel/7/prod/).
- Install dependencies and the driver.

```bash
yum install glibc e2fsprogs krb5-libs openssl unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

#### Ubuntu 16.04 (ODBC 13 offline)

- Download the latest `msodbcsql` `.deb` from [https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/).
- Install dependencies and the driver.

```bash
sudo apt-get install libc6 libstdc++6 libkrb5-3 libcurl3 openssl debconf unixodbc unixodbc-dev #install dependencies
sudo dpkg -i msodbcsql_13.1.X.X-X_amd64.deb #install the Driver
```

#### SUSE Linux Enterprise Server 12 (ODBC 13 offline)

- Download the latest `msodbcsql` `.rpm` from [https://packages.microsoft.com/sles/12/prod/](https://packages.microsoft.com/sles/12/prod/).
- Install the dependencies and the driver.

```bash
zypper install glibc, libuuid1, krb5, openssl, unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

After you've completed the package installation, you can verify that the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 can find all its dependencies by running ldd and inspecting its output for missing libraries:

```bash
ldd /opt/microsoft/msodbcsql/lib64/libmsodbcsql-*
```

## <a id="11"></a> ODBC 11

The following sections explain how to install the Microsoft ODBC driver 11 on Linux. Before you can use the driver, install the unixODBC driver manager. For more information, see [Installing the Driver Manager](installing-the-driver-manager.md).

### Installation Steps

> [!IMPORTANT]
> These instructions refer to `msodbcsql-11.0.2270.0.tar.gz`, which is installation file for Red Hat Linux. If you are installing the Preview for SUSE Linux, the file name is `msodbcsql-11.0.2260.0.tar.gz`.

To install the driver:

1. Make sure that you have root permission.

2. Change to the directory where the download placed the file `msodbcsql-11.0.2270.0.tar.gz`. Make sure that you have the \*.tar.gz file that matches your version of Linux. To extract the files, execute the following command, `tar xvzf msodbcsql-11.0.2270.0.tar.gz`.

3. Change to the `msodbcsql-11.0.2270.0` directory and there you should see a file called **install.sh**.

4. To see a list of the available installation options, execute the following command: **./install.sh**.

5. Make a backup of **odbcinst.ini**. The driver installation updates **odbcinst.ini**. odbcinst.ini contains the list of drivers that are registered with the unixODBC Driver Manager. To discover the location of odbcinst.ini on your computer, execute the following command: ```odbc_config --odbcinstini```.

6. Before you install the driver, execute the following command: `./install.sh verify`. The output of `./install.sh verify` reports if your computer has the required software to support the ODBC driver on Linux.

7. When you're ready to install the ODBC driver on Linux, execute the command: `./install.sh install`. If you need to specify an install command (`bin-dir` or `lib-dir`), specify the command after the **install** option.

8. After reviewing the license agreement, type **YES** to continue with the installation.

Installation puts the driver in `/opt/microsoft/msodbcsql/11.0.2270.0`. The driver and its support files must be in `/opt/microsoft/msodbcsql/11.0.2270.0`.

To verify that the Microsoft ODBC driver on Linux was registered successfully, execute the following command: ```odbcinst -q -d -n "ODBC Driver 11 for SQL Server"```.

### Uninstall

You can uninstall the ODBC driver 11 on Linux by executing the following commands:

1. `rm -f /usr/bin/sqlcmd`

2. `rm -f /usr/bin/bcp`

3. `rm -rf /opt/microsoft/msodbcsql`

4. `odbcinst -u -d -n "ODBC Driver 11 for SQL Server"`

## Driver files

The ODBC driver on Linux consists of the following components:

|Component|Description|
|---------------|-----------------|
|libmsodbcsql-17.X.so.X.X or libmsodbcsql-13.X.so.X.X|The shared object (`so`) dynamic library file that contains all of the driver's functionality. This file is installed in `/opt/microsoft/msodbcsql17/lib64/` for the Driver 17 and in `/opt/microsoft/msodbcsql/lib64/` for Driver 13.|
|`msodbcsqlr17.rll` or `msodbcsqlr13.rll`|The accompanying resource file for the driver library. This file is installed in `[driver .so directory]../share/resources/en_US/`|
|msodbcsql.h|The header file that contains all of the new definitions needed to use the driver.<br /><br /> **Note:**  You can't reference msodbcsql.h and odbcss.h in the same program.<br /><br /> msodbcsql.h is installed in `/opt/microsoft/msodbcsql17/include/` for Driver 17 and in `/opt/microsoft/msodbcsql/include/` for Driver 13. |
|LICENSE.txt|The text file that contains the terms of the End-User License Agreement. This file is placed in `/usr/share/doc/msodbcsql17/` for Driver 17 and in `/usr/share/doc/msodbcsql/` for Driver 13.|
|RELEASE_NOTES|The text file that contains release notes. This file is placed in `/usr/share/doc/msodbcsql17/` for Driver 17 and in `/usr/share/doc/msodbcsql/` for Driver 13.|

## Resource file loading

The driver needs to load the resource file to function. This file is called `msodbcsqlr17.rll` or `msodbcsqlr13.rll` depending on the driver version. The location of the `.rll` file is relative to the location of the driver itself (`so` or `dylib`), as noted in the table above. As of version 17.1 the driver will also attempt to load the `.rll` from the default directory if loading from the relative path fails. The default resource file path on Linux is `/opt/microsoft/msodbcsql17/share/resources/en_US/`.

## Troubleshooting

If you're unable to make a connection to SQL Server using the ODBC driver, see the known issues article on [troubleshooting connection problems](known-issues-in-this-version-of-the-driver.md#connectivity).

## Next steps

After installing the driver, you can try the [C++ ODBC example application](../../odbc/cpp-code-example-app-connect-access-sql-db.md). For more information about developing ODBC applications, see [Developing Applications](../../../odbc/reference/develop-app/developing-applications.md).

For more information, see the ODBC driver [release notes](release-notes-odbc-sql-server-linux-mac.md) and [system requirements](system-requirements.md).
