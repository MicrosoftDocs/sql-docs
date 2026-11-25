---
title: Install the Microsoft ODBC Driver for SQL Server (Linux)
description: Learn how to install the Microsoft ODBC Driver for SQL Server on Linux clients to enable database connectivity.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl, vanto
ms.date: 11/25/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom:
  - intro-installation
  - linux-related-content
helpviewer_keywords:
  - "driver, installing"
---

# Install the Microsoft ODBC driver for SQL Server (Linux)

This article explains how to install the Microsoft ODBC Driver for SQL Server on Linux. It also includes instructions for the optional command-line tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC development headers.

This article provides commands for installing the ODBC driver from the bash shell. If you want to download the packages directly, see [Download ODBC Driver for SQL Server](../download-odbc-driver-for-sql-server.md).

## Choose a driver version

Use Driver 18 for new applications. Driver 17 remains supported for applications that require version 17 behavior. Before you upgrade an application from Driver 17 to Driver 18, review the [major version differences](../major-version-differences.md), including the change to encryption defaults. For support dates, see the [support lifecycle](../support-lifecycle.md).

The following table shows the driver and command-line tools packages:

| Version | Driver package | Optional tools package | Tools path |
| --- | --- | --- | --- |
| 18 | `msodbcsql18` | `mssql-tools18` | `/opt/mssql-tools18/bin` |
| 17 | `msodbcsql17` | `mssql-tools` | `/opt/mssql-tools/bin` |

<a id="18"></a>

## Microsoft ODBC 18

The following sections explain how to install the Microsoft ODBC driver 18 from the bash shell for different Linux distributions. Supported distributions are Alpine Linux, Debian, Red Hat Enterprise Linux (RHEL), Oracle Linux, SUSE Linux Enterprise Server (SLES), Ubuntu, and Azure Linux. Starting with version 18.4, to accept the End-User License Agreement (EULA) automatically when installing the non-Alpine Linux (`.deb` or `.rpm`) driver, you can create the file `/opt/microsoft/msodbcsql18/ACCEPT_EULA`.

### [Alpine](#tab/alpine18-install)

```bash
set -euo pipefail

case $(uname -m) in
    x86_64) architecture="amd64" ;;
    aarch64|arm64) architecture="arm64" ;;
    *) architecture="unsupported" ;;
esac
if [[ "unsupported" == "$architecture" ]];
then
    echo "Alpine architecture $(uname -m) is not currently supported."
    exit 1
fi

if ! command -v gpg >/dev/null 2>&1; then
    echo "Install gnupg before running this block."
    exit 1
fi

driver_package="msodbcsql18_18.7.2.1-1_${architecture}.apk"
tools_package="mssql-tools18_18.7.1.1-1_${architecture}.apk"
driver_signature="${driver_package%.apk}.sig"
tools_signature="${tools_package%.apk}.sig"

# Download the packages and signatures.
curl -fSLO "https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/$driver_package"
curl -fSLO "https://download.microsoft.com/download/a5dcc5e7-6124-49d3-8df4-48d738a0e784/$tools_package"
curl -fSLO "https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/$driver_signature"
curl -fSLO "https://download.microsoft.com/download/a5dcc5e7-6124-49d3-8df4-48d738a0e784/$tools_signature"
curl -fSL https://packages.microsoft.com/keys/microsoft.asc -o microsoft.asc

# Verify the package signatures before installation.
gpg --import microsoft.asc
gpg --verify "$driver_signature" "$driver_package"
gpg --verify "$tools_signature" "$tools_package"

# Install the packages.
sudo apk add --allow-untrusted "$driver_package"
sudo apk add --allow-untrusted "$tools_package"
```

> [!NOTE]  
> Driver 17.5 and later versions support Alpine.

### [Debian](#tab/debian18-install)

```bash
debian_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$debian_version" in
    9|10|11|12|13) ;;
    *) echo "Debian $debian_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/debian/$debian_version/packages-microsoft-prod.deb" || exit 1
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
# required: the driver links libgssapi_krb5.so.2 but the package doesn't declare it
sudo apt-get install -y libgssapi-krb5-2
```

> [!NOTE]  
> Set the `debconf` value `msodbcsql/ACCEPT_EULA` instead of the `ACCEPT_EULA` environment variable:
>
> ```bash
> echo msodbcsql18 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

### [RHEL and Oracle Linux](#tab/redhat18-install)

```bash
rhel_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$rhel_version" in
    7|8|9|10) ;;
    *) echo "RHEL or Oracle Linux $rhel_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/rhel/$rhel_version/packages-microsoft-prod.rpm" || exit 1
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
sles_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$sles_version" in
    12|15|16) ;;
    *) echo "SLES $sles_version is not currently supported." >&2; exit 1 ;;
esac

# Import the GNU Privacy Guard (GPG) key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/sles/$sles_version/packages-microsoft-prod.rpm" || exit 1
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
ubuntu_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)
case "$ubuntu_version" in
    18.04|20.04|22.04|24.04|26.04) ;;
    *) echo "Ubuntu $ubuntu_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/ubuntu/$ubuntu_version/packages-microsoft-prod.deb" || exit 1
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
> Set the `debconf` value `msodbcsql/ACCEPT_EULA` instead of the `ACCEPT_EULA` environment variable:
>
> ```bash
> echo msodbcsql18 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

### [Azure Linux](#tab/azure18-install)

> [!NOTE]
> Azure Linux 3.0 includes the Microsoft packages repository by default. You don't need to manually configure the repository before you install the driver. If your Azure Linux image uses `tdnf` as the package manager, the following commands work directly.

```bash
azure_linux_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)
if [[ "$azure_linux_version" != "3.0" ]]; then
    echo "Azure Linux $azure_linux_version is not currently supported." >&2
    exit 1
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

<a id="17"></a>

## Microsoft ODBC 17

The following sections explain how to install the Microsoft ODBC driver 17 from the bash shell for different Linux distributions.

> [!IMPORTANT]  
> If you installed the v17 `msodbcsql` package that was briefly available, remove it before installing the `msodbcsql17` package to avoid conflicts. You can install the `msodbcsql17` package side by side with the `msodbcsql` v13 package.

### [Alpine](#tab/alpine17-install)

```bash
set -euo pipefail

if [[ "$(uname -m)" != "x86_64" ]]; then
    echo "Driver 17 Alpine packages are available only for x86_64." >&2
    exit 1
fi

if ! command -v gpg >/dev/null 2>&1; then
    echo "Install gnupg before running this block."
    exit 1
fi

driver_package="msodbcsql17_17.12.1.1-1_amd64.apk"
tools_package="mssql-tools_17.12.1.1-1_amd64.apk"
driver_signature="${driver_package%.apk}.sig"
tools_signature="${tools_package%.apk}.sig"

# Download the packages and signatures.
curl -fSLO "https://download.microsoft.com/download/01432586-9668-4062-a363-adc7810ae83e/$driver_package"
curl -fSLO "https://download.microsoft.com/download/2409b65e-ebd8-429d-bc9c-8821f7526d02/$tools_package"
curl -fSLO "https://download.microsoft.com/download/01432586-9668-4062-a363-adc7810ae83e/$driver_signature"
curl -fSLO "https://download.microsoft.com/download/2409b65e-ebd8-429d-bc9c-8821f7526d02/$tools_signature"
curl -fSL https://packages.microsoft.com/keys/microsoft.asc -o microsoft.asc

# Verify the package signatures before installation.
gpg --import microsoft.asc
gpg --verify "$driver_signature" "$driver_package"
gpg --verify "$tools_signature" "$tools_package"

# Install the packages.
sudo apk add --allow-untrusted "$driver_package"
sudo apk add --allow-untrusted "$tools_package"
```

> [!NOTE]  
> Driver 17.5 and later versions support Alpine.

### [Debian](#tab/debian17-install)

```bash
debian_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$debian_version" in
    8|9|10|11|12|13) ;;
    *) echo "Debian $debian_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/debian/$debian_version/packages-microsoft-prod.deb" || exit 1
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
# required: the driver links libgssapi_krb5.so.2 but the package doesn't declare it
sudo apt-get install -y libgssapi-krb5-2
```

> [!NOTE]  
> Set the `debconf` value `msodbcsql/ACCEPT_EULA` instead of the `ACCEPT_EULA` environment variable:
>
> ```bash
> echo msodbcsql17 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

### [RHEL and Oracle Linux](#tab/redhat17-install)

```bash
rhel_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$rhel_version" in
    6|7|8|9|10) ;;
    *) echo "RHEL or Oracle Linux $rhel_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/rhel/$rhel_version/packages-microsoft-prod.rpm" || exit 1
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
sles_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$sles_version" in
    11|12|15|16) ;;
    *) echo "SLES $sles_version is not currently supported." >&2; exit 1 ;;
esac

# Import the GNU Privacy Guard (GPG) key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/sles/$sles_version/packages-microsoft-prod.rpm" || exit 1
# Install the package
sudo zypper install packages-microsoft-prod.rpm
# Delete the file
rm packages-microsoft-prod.rpm

# For Driver 17.3 and earlier versions on SLES 15, also run:
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
ubuntu_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)
case "$ubuntu_version" in
    14.04|16.04|18.04|20.04|22.04|24.04|26.04) ;;
    *) echo "Ubuntu $ubuntu_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/ubuntu/$ubuntu_version/packages-microsoft-prod.deb" || exit 1
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
> Set the `debconf` value `msodbcsql/ACCEPT_EULA` instead of the `ACCEPT_EULA` environment variable:
>
> ```bash
> echo msodbcsql17 msodbcsql/ACCEPT_EULA boolean true | sudo debconf-set-selections
> ```

---

## Update or verify a driver installation

To update a driver you installed from a Linux package repository, refresh the package metadata and update the package for the same major version. For distributions with Driver 17 installation instructions in this article, replace `msodbcsql18` with `msodbcsql17` to update Driver 17. The Azure Linux commands apply to Driver 18. To move between major versions, install the other package and configure your application to request its driver name.

| Distribution | Update command |
| --- | --- |
| Debian or Ubuntu | `sudo apt-get update && sudo ACCEPT_EULA=Y apt-get install --only-upgrade -y msodbcsql18` |
| Red Hat Enterprise Linux or Oracle Linux | `sudo ACCEPT_EULA=Y yum update -y msodbcsql18` |
| SUSE Linux Enterprise Server | `sudo zypper refresh && sudo ACCEPT_EULA=Y zypper update -y msodbcsql18` |
| Azure Linux | `sudo ACCEPT_EULA=Y tdnf upgrade -y msodbcsql18` |

For Alpine, download and install the updated APK for the installed major version and system architecture. Driver 18 supports the `amd64` and `arm64` packages shown in the installation instructions. Driver 17 uses the `amd64` package:

The update commands require `curl` and `gpg`. If either is missing, install them with `sudo apk add curl gnupg`.

```bash
set -e
case "$(uname -m)" in
    x86_64) architecture="amd64" ;;
    aarch64|arm64) architecture="arm64" ;;
    *) echo "Unsupported Alpine architecture: $(uname -m)" >&2; exit 1 ;;
esac

driver_apk="msodbcsql18_18.7.2.1-1_${architecture}.apk"
curl -fSL -O "https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/$driver_apk"
curl -fSL -o "$driver_apk.sig" "https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/${driver_apk%.apk}.sig"
curl -fSL https://packages.microsoft.com/keys/microsoft.asc -o microsoft.asc
gpg --import microsoft.asc
gpg --verify "$driver_apk.sig" "$driver_apk"
sudo apk add --allow-untrusted "$driver_apk"
rm "$driver_apk" "$driver_apk.sig" microsoft.asc
```

For Driver 17, download and install the `amd64` package:

```bash
set -e
if [[ "$(uname -m)" != "x86_64" ]]; then
    echo "Driver 17 Alpine packages are available only for x86_64." >&2
    exit 1
fi

driver_apk="msodbcsql17_17.12.1.1-1_amd64.apk"
curl -fSL -O "https://download.microsoft.com/download/01432586-9668-4062-a363-adc7810ae83e/$driver_apk"
curl -fSL -o "$driver_apk.sig" "https://download.microsoft.com/download/01432586-9668-4062-a363-adc7810ae83e/${driver_apk%.apk}.sig"
curl -fSL https://packages.microsoft.com/keys/microsoft.asc -o microsoft.asc
gpg --import microsoft.asc
gpg --verify "$driver_apk.sig" "$driver_apk"
sudo apk add --allow-untrusted "$driver_apk"
rm "$driver_apk" "$driver_apk.sig" microsoft.asc
```

After you install or update a driver, set `driver_version` to `17` or `18`, and then check its unixODBC registration and system architecture:

| Driver version | `driver_package` | `driver_name` |
| --- | --- | --- |
| 18 | `msodbcsql18` | `ODBC Driver 18 for SQL Server` |
| 17 | `msodbcsql17` | `ODBC Driver 17 for SQL Server` |

```bash
set -e
driver_version=18
driver_package="msodbcsql${driver_version}"
driver_name="ODBC Driver ${driver_version} for SQL Server"

odbcinst -q -d -n "$driver_name"
uname -m
```

The registration query prints the driver name in brackets. `uname -m` reports the system architecture. Use the package query for your distribution to check the installed package version and architecture:

| Distribution | Package query |
| --- | --- |
| Debian or Ubuntu | `dpkg-query -W -f='${Package} ${Version} ${Architecture}\n' "$driver_package"` |
| Red Hat Enterprise Linux, Oracle Linux, SUSE Linux Enterprise Server, or Azure Linux | `rpm -q --qf '%{NAME} %{VERSION}-%{RELEASE} %{ARCH}\n' "$driver_package"` |
| Alpine | `awk -v package="$driver_package" 'index($0, "P:") == 1 { name = substr($0, 3) } name == package && index($0, "V:") == 1 { version = substr($0, 3) } name == package && index($0, "A:") == 1 { printf "%s %s %s\n", name, version, substr($0, 3); found = 1 } END { if (!found) exit 1 }' /lib/apk/db/installed` |

The package query returns the package name, installed version, and package architecture. These commands don't connect to a database.

## Remove or reinstall Driver 17 or Driver 18

Close applications that use the driver before removing it. For distributions with Driver 17 installation instructions in this article, replace `msodbcsql18` with `msodbcsql17` to remove Driver 17. The Azure Linux command applies to Driver 18.

| Distribution | Removal command |
| --- | --- |
| Debian or Ubuntu | `sudo apt-get remove msodbcsql18` |
| Red Hat Enterprise Linux or Oracle Linux | `sudo yum remove msodbcsql18` |
| SUSE Linux Enterprise Server | `sudo zypper remove msodbcsql18` |
| Azure Linux | `sudo tdnf remove msodbcsql18` |
| Alpine | `sudo apk add unixodbc && sudo apk del msodbcsql18` |

Dependent applications or tools might also be removed. If your package manager asks for confirmation, review the proposed removals before accepting. On Red Hat Enterprise Linux and Oracle Linux releases where `yum` uses DNF, add `--setopt=clean_requirements_on_remove=0` to the removal command to retain unixODBC for the registration check. For Driver 17, replace `msodbcsql18` with `msodbcsql17`.

On Alpine, install unixODBC explicitly before removing the driver so `odbcinst` remains available for the registration check. `apk del` retains the driver if installed tools still depend on it. If you installed the optional tools, replace the Alpine command in the preceding table with:

```bash
sudo apk add unixodbc
sudo apk del mssql-tools18 msodbcsql18
```

For Driver 17, use `sudo apk add unixodbc` followed by `sudo apk del mssql-tools msodbcsql17`. Review the dependent applications before removing these packages, then repeat the registration query.

After removal, run `odbcinst -q -d`. If it lists driver names, confirm that the removed driver name isn't listed. If it reports `SQLGetPrivateProfileString failed`, run `odbcinst -j` and check the `DRIVERS` path it reports. If the driver configuration file exists, is readable, and contains no driver registrations, no drivers remain registered. If the file is missing, unreadable, or still contains the removed driver's entry, resolve that configuration issue before treating removal as complete. To reinstall, repeat the installation commands for the same distribution and major version, then repeat verification.

Removing a driver can prevent applications that request its driver name from connecting. Don't delete driver files or the driver-manager configuration file. If a stale registration blocks installation, see [Previous driver version conflict](#previous-driver-version-conflict) for the targeted recovery steps.

## <a id="previous-versions"></a> Earlier versions

The following sections provide installation instructions for earlier versions of the Microsoft ODBC driver:

- [Microsoft ODBC driver 13.1 for SQL Server](#13.1)
- [Microsoft ODBC driver 13 for SQL Server](#13)
- [Microsoft ODBC driver 11 for SQL Server](#11)

<a id="13.1"></a>

### ODBC 13.1

The following sections explain how to install the Microsoft ODBC driver 13.1 from the bash shell for different Linux distributions.

### [Debian 8](#tab/debian8-install)

```bash
debian_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$debian_version" in
    8) ;;
    *) echo "Debian $debian_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/debian/$debian_version/packages-microsoft-prod.deb" || exit 1
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
rhel_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$rhel_version" in
    6|7) ;;
    *) echo "RHEL $rhel_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/rhel/$rhel_version/packages-microsoft-prod.rpm" || exit 1
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
sles_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$sles_version" in
    11|12) ;;
    *) echo "SLES $sles_version is not currently supported." >&2; exit 1 ;;
esac

# Import the GNU Privacy Guard (GPG) key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/sles/$sles_version/packages-microsoft-prod.rpm" || exit 1
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
ubuntu_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)
case "$ubuntu_version" in
    14.04|16.04) ;;
    *) echo "Ubuntu $ubuntu_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/ubuntu/$ubuntu_version/packages-microsoft-prod.deb" || exit 1
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
rhel_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$rhel_version" in
    6|7) ;;
    *) echo "RHEL $rhel_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/rhel/$rhel_version/packages-microsoft-prod.rpm" || exit 1
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
ubuntu_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)
case "$ubuntu_version" in
    14.04|16.04) ;;
    *) echo "Ubuntu $ubuntu_version is not currently supported." >&2; exit 1 ;;
esac

# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/ubuntu/$ubuntu_version/packages-microsoft-prod.deb" || exit 1
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
sles_version=$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)
case "$sles_version" in
    11|12) ;;
    *) echo "SLES $sles_version is not currently supported." >&2; exit 1 ;;
esac

# Import the GNU Privacy Guard (GPG) key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
# Download the package to configure the Microsoft repo
curl -fsSLO "https://packages.microsoft.com/config/sles/$sles_version/packages-microsoft-prod.rpm" || exit 1
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

To install the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13 without an Internet connection, manually resolve these direct package dependencies:

- Ubuntu: `libc6` (>= 2.21), `libstdc++6` (>= 4.9), `libkrb5-3`, `libcurl3`, `openssl`, `debconf` (>= 0.5), `unixODBC` (>= 2.3.1-1)
- Red Hat: `glibc`, `e2fsprogs`, `krb5-libs`, `openssl`, `unixODBC`
- SUSE: `glibc`, `libuuid1`, `krb5`, `openssl`, `unixODBC`

Each package has its own dependencies. For package manager guidance, see the documentation for [Red Hat](https://access.redhat.com/solutions/7019225), [Ubuntu](https://unix.stackexchange.com/questions/87130/how-to-quickly-create-a-local-apt-repository-for-random-packages-using-a-debian), and [SUSE](https://en.opensuse.org/Portal:Zypper).

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
sudo zypper install glibc libuuid1 krb5 openssl unixODBC unixODBC-devel # install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

After installation, run `ldd` on the driver library and check the output for missing dependencies:

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

<a id="installation-steps"></a>

#### Install Driver 11

> [!IMPORTANT]  
> Use the archive that matches your distribution: Red Hat Linux uses `msodbcsql-11.0.2270.0.tar.gz`, and SUSE Linux uses `msodbcsql-11.0.2260.0.tar.gz`.

To install the driver:

1. Confirm that you have root permission.

1. Download the driver.

   | Distribution | Driver | SHA256 hash |
   | --- | --- | --- |
   | Red Hat 5 | [msodbcsql-11.0.2270.0.tar.gz](https://go.microsoft.com/fwlink/?linkid=2299904) | [178280daf01a49b8322cd902b6440979adacd594c01cd2a1f081dda23dbfb343](https://go.microsoft.com/fwlink/?linkid=2299695) |
   | Red Hat 6 | [msodbcsql-11.0.2270.0.tar.gz](https://go.microsoft.com/fwlink/?linkid=2300003) | [e9b6bd33d174c7753b3a3f2d541713fbc156b46254484a169caa3f459dd828f7](https://go.microsoft.com/fwlink/?linkid=2299903) |
   | SUSE Linux | [msodbcsql-11.0.2260.0.tar.gz](https://go.microsoft.com/fwlink/?linkid=2300002) | 86d1c5842be4f0095234a9455e18a04fdf4cc7960ec0255b37258112e2391ef5 |

1. Change to the directory where you downloaded the archive. To extract the Red Hat package, run:

   ```bash
   tar xvzf msodbcsql-11.0.2270.0.tar.gz
   ```

   To extract the SUSE package, run:

   ```bash
   tar xvzf msodbcsql-11.0.2260.0.tar.gz
   ```

1. Change to the extracted directory that matches your archive version. You should see a file called `install.sh`.

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

   To set nondefault binary or library directories, pass `--bin-dir=<directory>` or `--lib-dir=<directory>` after the `install` option.

1. After reviewing the license agreement, type `YES` to continue with the installation.

The driver library is installed in `/opt/microsoft/msodbcsql/lib64`. Red Hat installs version `11.0.2270.0` and SUSE installs version `11.0.2260.0`. Each version's support files are installed in its corresponding directory under `/opt/microsoft/msodbcsql`. The package also installs versioned `bcp` and `sqlcmd` utilities under `/opt/microsoft/msodbcsql/bin` and creates links in `/usr/bin` by default. If you used the `--lib-dir` or `--bin-dir` option, substitute the paths you specified.

To verify that the Microsoft ODBC driver on Linux was registered successfully, run the following command:

```bash
odbcinst -q -d -n "ODBC Driver 11 for SQL Server"
```

#### Uninstall

Uninstall the ODBC driver 11 on Linux by running the following commands:

> [!CAUTION]
> These commands remove the Driver 11 libraries, utilities, and support files for the two listed package versions. Check the paths before you run them. If you used the `--lib-dir` or `--bin-dir` option, substitute the paths you specified.

```bash
set -e
odbcinst -u -d -n "ODBC Driver 11 for SQL Server"
for tool in bcp sqlcmd; do
    link="/usr/bin/$tool"
    if [ -L "$link" ]; then
        target=$(readlink "$link")
        case "$target" in
            "/opt/microsoft/msodbcsql/bin/$tool-11.0.2270.0"|"/opt/microsoft/msodbcsql/bin/$tool-11.0.2260.0")
                rm "$link"
                ;;
        esac
    fi
done
rm -f /opt/microsoft/msodbcsql/lib64/libmsodbcsql-11.0.so.2270.0 \
    /opt/microsoft/msodbcsql/lib64/libmsodbcsql-11.0.so.2260.0 \
    /opt/microsoft/msodbcsql/bin/bcp-11.0.2270.0 \
    /opt/microsoft/msodbcsql/bin/bcp-11.0.2260.0 \
    /opt/microsoft/msodbcsql/bin/sqlcmd-11.0.2270.0 \
    /opt/microsoft/msodbcsql/bin/sqlcmd-11.0.2260.0
rm -rf /opt/microsoft/msodbcsql/11.0.2270.0 \
    /opt/microsoft/msodbcsql/11.0.2260.0
```

## Driver files

Key ODBC driver files on Linux include:

| Component | Description |
| --- | --- |
| `libmsodbcsql-18.X.so.X.X`, `libmsodbcsql-17.X.so.X.X`, or `libmsodbcsql-13.X.so.X.X` | The main shared object (`so`) library for the driver. The installation path is `/opt/microsoft/msodbcsql18/lib64/` for Driver 18, `/opt/microsoft/msodbcsql17/lib64/` for Driver 17, and `/opt/microsoft/msodbcsql/lib64/` for Driver 13. |
| `msodbcsqlr18.rll`, `msodbcsqlr17.rll`, or `msodbcsqlr13.rll` | The resource file that accompanies the driver library. The installation path is `[driver .so directory]../share/resources/en_US/`. |
| `msodbcsql.h` | The header file that contains all of the new definitions needed to use the driver.<br /><br />**Note:** You can't reference `msodbcsql.h` and `odbcss.h` in the same program.<br /><br />The installation path is `/opt/microsoft/msodbcsql18/include/` for Driver 18, `/opt/microsoft/msodbcsql17/include/` for Driver 17, and `/opt/microsoft/msodbcsql/include/` for Driver 13. |
| `LICENSE.txt` | The text file that contains the terms of the End-User License Agreement. The installation path is `/usr/share/doc/msodbcsql18/` for Driver 18, `/usr/share/doc/msodbcsql17/` for Driver 17, and `/usr/share/doc/msodbcsql/` for Driver 13. |
| `RELEASE_NOTES` | The text file that contains release notes. The installation path is `/usr/share/doc/msodbcsql18/` for Driver 18, `/usr/share/doc/msodbcsql17/` for Driver 17, and `/usr/share/doc/msodbcsql/` for Driver 13. |

<a id="resource-file-loading"></a>

## Load the resource file

The driver needs to load the resource file to function. This file is called `msodbcsqlr18.rll`, `msodbcsqlr17.rll`, or `msodbcsqlr13.rll` depending on the driver version. The location of the `.rll` file is relative to the location of the driver itself (`so` or `dylib`), as described in the previous table. Starting with version 17.1, if the driver can't load the `.rll` file from the relative path, it also tries to load the `.rll` file from the default directory. The default resource file path on Linux is `/opt/microsoft/msodbcsql18/share/resources/en_US/` for Driver 18 and `/opt/microsoft/msodbcsql17/share/resources/en_US/` for Driver 17.

## Troubleshoot

### Previous driver version conflict

If you previously installed and registered a driver version with unixODBC, installation might fail with an error such as:

```output
Installation failed, ODBC Driver $1 for SQL Server detected!
```

To resolve the problem, unregister that driver version with `odbcinst`. Replace `<version>` with the number in the error message:

```bash
odbcinst -u -d -n "ODBC Driver <version> for SQL Server"
```

If `odbcinst` can't unregister the driver, back up `odbcinst.ini` and remove only the section for the conflicting driver. Don't delete the file or other driver sections. Run `odbcinst -j` to find the file location.

### Package download errors on Debian and Ubuntu

On Debian-based distributions, you might see the following error when you install the `packages-microsoft-prod.deb` package:

```output
dpkg-deb: error: 'packages-microsoft-prod.deb' is not a Debian format archive
```

A network interruption or proxy issue might leave the downloaded file corrupted or incomplete, causing this error. To resolve the problem:

1. Delete the corrupted file and download it again. For Ubuntu:

   ```bash
   rm packages-microsoft-prod.deb
   curl -fsSLO "https://packages.microsoft.com/config/ubuntu/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2)/packages-microsoft-prod.deb" || exit 1
   ```

   For Debian, replace `ubuntu` and use the major version number only:

   ```bash
   rm packages-microsoft-prod.deb
   curl -fsSLO "https://packages.microsoft.com/config/debian/$(grep VERSION_ID /etc/os-release | cut -d '"' -f 2 | cut -d '.' -f 1)/packages-microsoft-prod.deb" || exit 1
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

For more information about connection problems, see the known issues article on [troubleshooting connection problems](known-issues-in-this-version-of-the-driver.md#connectivity).

## Related content

- [Connect and query with C++ and ODBC](../cpp-code-example-app-connect-access-sql-db.md)
- [Developing Applications](../../../odbc/reference/develop-app/developing-applications.md)
- [Release notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS](release-notes-odbc-sql-server-linux-mac.md)
- [System Requirements (Linux and macOS)](system-requirements.md)
