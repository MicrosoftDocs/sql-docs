---
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 06/29/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: include
---
Install one-time operating system specific prerequisites. Windows users can skip this step. For full platform details, see [Install mssql-python](../installation.md#platform-specific-notes).

### [Alpine](#tab/alpine-linux)

```console
apk add libtool krb5-libs krb5-dev
```

### [Debian/Ubuntu](#tab/debianUbuntu-linux)

```console
apt-get install -y libltdl7 libkrb5-3 libgssapi-krb5-2
```

### [RHEL](#tab/RHEL-linux)

```console
dnf install -y libtool-ltdl krb5-libs
```

### [SUSE](#tab/SUSE-linux)

```console
zypper install -y libltdl7 libkrb5-3 libgssapi-krb5-2
```

### [openSUSE](#tab/openSUSE-linux)

```console
zypper install -y libltdl7
```

### [macOS](#tab/mac)

```console
brew install openssl
```

---