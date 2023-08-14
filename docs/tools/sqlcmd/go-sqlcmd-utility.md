---
title: go-sqlcmd utility
description: The go-sqlcmd utility lets you enter Transact-SQL statements, system procedures, and script files using different modes.
author: dlevy-msft
ms.author: dlevy
ms.reviewer: maghan, randolphwest
ms.date: 07/11/2023
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017"
---

# go-sqlcmd utility

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The **go-sqlcmd** utility lets you enter Transact-SQL statements, system procedures, and script files at the command prompt and uses the [go-mssqldb](https://github.com/microsoft/go-mssqldb) driver for go language. **go-sqlcmd** aims to be a complete port of [sqlcmd](sqlcmd-utility.md) to the go language and compiles to executable binaries for Windows, macOS, and Linux on both x64 and arm64 architectures. [Download and install](#download-and-install-go-sqlcmd) the **go-sqlcmd** binaries to get started without additional dependencies. Using **go-sqlcmd** in place of **sqlcmd** removes the ODBC driver dependency, increases options for Azure Active Directory (Azure AD) authentication types, and adds [additional enhancements](#enhancements).

**go-sqlcmd** is open source under the MIT license and available on [GitHub](https://github.com/microsoft/go-sqlcmd). As a CLI, **go-sqlcmd** is ideal for pipelines and edge applications as it has no additional dependencies and supports various environment configurations. The capabilities of **go-sqlcmd** expand beyond the ODBC-based [sqlcmd](sqlcmd-utility.md) to incorporate a [vertical output format](#enhancements) and extensive [Azure Active Directory authentication](#azure-active-directory-authentication) options.

> [!NOTE]  
> Installing **go-sqlcmd** via a package manager will replace **[sqlcmd](sqlcmd-utility.md)** with **go-sqlcmd** in your environment path. Any current command line sessions will need to be closed and reopened for this take to effect. **sqlcmd** won't be removed and can still be used by specifying the full path to the executable. You can also update your `PATH` variable to indicate which will take precedence. To do so in Windows 11, open **System settings** and go to **About > Advanced system settings**. When **System Properties** opens, select the **Environment Variables** button. In the lower half, under **System variables**, select **Path** and then select **Edit**. If the location **go-sqlcmd** is saved to (`C:\Program Files\sqlcmd` is default) is listed before `C:\Program Files\Microsoft SQL Server\<version>\Tools\Binn`, then **go-sqlcmd** is used. You can reverse the order to make **sqlcmd** the default again.

Review the [breaking changes from sqlcmd](#breaking-changes-from-sqlcmd) section in this article before installing via a package manager, to make sure that existing scripts aren't impacted. If you're unsure, consider the direct download option, which allows you to download the **go-sqlcmd** executable to a directory of your choice. You can then run **go-sqlcmd** from that directory, or by specifying the full path to the executable. This option doesn't update any system settings.

## Download and install go-sqlcmd

### [Windows](#tab/windows)

#### winget (Windows Package Manager CLI)

1. Install the [Windows Package Manager Client](/windows/package-manager/winget) if you don't already have it.
1. Run the following command to install **go-sqlcmd**.

   ```bash
   winget install sqlcmd
   ```

#### Chocolatey

1. Install [Chocolatey](https://community.chocolatey.org/) if you don't already have it.
1. Run the following command to install **go-sqlcmd**.

   ```bash
   choco install sqlcmd
   ```

#### Direct download

1. Download the corresponding `-windows-x64.zip` or `-windows-arm.zip` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of **go-sqlcmd** from the GitHub code repository.

1. Extract the `sqlcmd.exe` file from the downloaded zip folder.

### [macOS](#tab/mac)

#### Homebrew

1. Install Homebrew if you need to.

    ```bash
    /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
    ```

1. Install **sqlcmd** with Homebrew.

    ```bash
    brew install sqlcmd
    ```

#### Direct download

1. Download the `-darwin-x64.zip` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of **go-sqlcmd** from the GitHub code repository.

1. Extract the `sqlcmd` file from the downloaded zip folder.

### [Linux](#tab/linux)

#### apt (Debian/Ubuntu)

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Add the Microsoft repository, where the `ubuntu/20.04` segment may be `debian/11`, `ubuntu/20.04`, or `ubuntu/22.04`.

   ```bash
   add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/prod.list)"
   ```

1. Install **go-sqlcmd** with **apt**.

   ```bash
   apt-get update
   apt-get install sqlcmd
   ```

#### yum (Fedora/CentOS)

1. Import the Microsoft repository key.

   ```bash
   rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Download the repository configuration file, where the `centos/8` segment may be `centos/8`, `fedora/32`, `opensuse/42.3`, `rhel/8`, or `sles/15`. If the version of your OS doesn't directly correspond to one of those options, you can likely successfully use a repository configuration file from a version. For example, `centos/8` can be used in an environment running CentOS 7.

   ```bash
   curl -o /etc/yum.repos.d/packages-microsoft-com-prod.repo https://packages.microsoft.com/config/centos/8/prod.repo
   ```

1. Install **go-sqlcmd** with **yum**.

   ```bash
   yum install sqlcmd
   ```

#### Direct download

1. Download the corresponding `-linux-x64.tar.bz2` or `-linux-arm.tar.bz2` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of **go-sqlcmd** from the GitHub code repository.

1. Extract the `sqlcmd` file from the downloaded zip folder.

---

## Syntax

For more in-depth information on **sqlcmd** syntax and use, see:

- [sqlcmd syntax](./sqlcmd-utility.md#syntax)
- [Start the sqlcmd Utility](sqlcmd-start-utility.md)
- [Use the sqlcmd Utility](sqlcmd-use-utility.md)

### Breaking changes from sqlcmd

Several switches and behaviors are altered from [sqlcmd](sqlcmd-utility.md) in **go-sqlcmd**. For the most up-to-date list of missing flags for backwards compatibility, visit the [Prioritize implementation of back-compat flags](https://github.com/microsoft/go-sqlcmd/discussions/292) GitHub discussion.

- `-P` switch is removed. Passwords for SQL Server Authentication can only be provided through these mechanisms:
  - The `SQLCMDPASSWORD` environment variable
  - The `:CONNECT` command
  - When prompted, the user can type the password to complete a connection

- `-r` requires a `0` or `1` argument

- `-R` switch is removed.

- `-I` switch is removed. To disable quoted identifier behavior, add `SET QUOTED IDENTIFIER OFF` in your scripts.

- `-N` now takes a string value that can be one of `true`, `false`, or `disable` to specify the encryption choice. (`default` is the same as omitting the parameter)
  - If `-N` and `-C` aren't provided, **go-sqlcmd** negotiates authentication with the server without validating the server certificate.
  - If `-N` is provided but `-C` isn't, **go-sqlcmd** requires validation of the server certificate. A `false` value for encryption could still lead to the encryption of the login packet.
  - If both `-N` and `-C` are provided, **go-sqlcmd** uses their values for encryption negotiation.
  - More information about client/server encryption negotiation can be found at [MS-TDS PRELOGIN](/openspecs/windows_protocols/ms-tds/60f56408-0188-4cd5-8b90-25c6f2423868).

- `-u` The generated Unicode output file has the UTF-16 Little-Endian Byte-order mark (BOM) written to it.

- Some behaviors that were kept to maintain compatibility with `OSQL` may be changed, such as alignment of column headers for some data types.

- All commands must fit on one line, even `EXIT`. Interactive mode doesn't check for open parentheses or quotes for commands, and doesn't prompt for successive lines. The ODBC **sqlcmd** allows the query run by `EXIT(query)` to span multiple lines.

Connections from **go-sqlcmd** are limited to TCP connections. Named pipes aren't supported at this time in the `go-mssqldb` driver.

## Enhancements

- `:Connect` now has an optional `-G` parameter to select one of the authentication methods for Azure SQL Database  - `SqlAuthentication`, `ActiveDirectoryDefault`, `ActiveDirectoryIntegrated`, `ActiveDirectoryServicePrincipal`, `ActiveDirectoryManagedIdentity`, `ActiveDirectoryPassword`. More information on [Azure Active Directory authentication](#azure-active-directory-authentication) support follows. If `-G` isn't provided, Integrated security or SQL Server Authentication is used, depending on the presence of a `-U` user name parameter.

- The new `--driver-logging-level` command line parameter allows you to see traces from the `go-mssqldb` driver. Use `64` to see all traces.

- **sqlcmd** can now print results using a vertical format. Use the new `-F vertical` command line switch to set it. The `SQLCMDFORMAT` scripting variable also controls it.

## Azure Active Directory authentication

This version of **sqlcmd** supports a broader range of Azure AD authentication models based on the [azidentity package](https://pkg.go.dev/github.com/Azure/azure-sdk-for-go/sdk/azidentity). The implementation relies on an Azure AD connector in the [driver](https://github.com/microsoft/go-mssqldb).

### Command line arguments

To use Azure AD authentication, you can use one of two command line switches.

`-G` is (mostly) compatible with its usage in the prior version of **sqlcmd**. If a username and password are provided, it authenticates using Azure AD Password authentication. If a user name is provided, it uses Azure AD Interactive authentication, which may display a web browser. If no username or password is provided, it uses a `DefaultAzureCredential`, which attempts to authenticate through various mechanisms.

`--authentication-method=` can be used to specify one of the following authentication types.

#### ActiveDirectoryDefault

- For an overview of the types of authentication this mode uses, see [Default Azure Credential](https://github.com/Azure/azure-sdk-for-go/tree/main/sdk/azidentity#defaultazurecredential).
- Choose this method if your database automation scripts are intended to run in both local development environments and in a production deployment in Azure. In your development environment, you can use a client secret or an Azure CLI login. Without changing the script from the development environment, you can use a managed identity or client secret on your production deployment.
- Setting environment variables `AZURE_TENANT_ID` and `AZURE_CLIENT_ID` are necessary for `DefaultAzureCredential` to begin checking the environment configuration and look for one of the following additional environment variables in order to authenticate:
  - Setting environment variable `AZURE_CLIENT_SECRET` configures the `DefaultAzureCredential` to choose `ClientSecretCredential`.
  - Setting environment variable `AZURE_CLIENT_CERTIFICATE_PATH` configures the `DefaultAzureCredential` to choose `ClientCertificateCredential` if `AZURE_CLIENT_SECRET` isn't set.
  - Setting environment variable AZURE_USERNAME configures the `DefaultAzureCredential` to choose `UsernamePasswordCredential` if `AZURE_CLIENT_SECRET` and `AZURE_CLIENT_CERTIFICATE_PATH` aren't set.

#### ActiveDirectoryIntegrated

This method is currently not implemented, and falls back to `ActiveDirectoryDefault`.

#### ActiveDirectoryPassword

- This method authenticates using a username and password. It doesn't work if MFA is required.

- You provide the user name and password using the usual command line switches or `SQLCMD` environment variables.

- Set `AZURE_TENANT_ID` environment variable to the tenant ID of the server if not using the default tenant of the user.

#### ActiveDirectoryInteractive

This method launches a web browser to authenticate the user.

#### ActiveDirectoryManagedIdentity

Use this method when running **sqlcmd** on an Azure VM that has either a system-assigned or user-assigned managed identity. If using a user-assigned managed identity, set the user name to the ID of the managed identity. If using a system-assigned identity, leave user name empty.

#### ActiveDirectoryServicePrincipal

This method authenticates the provided user name as a service principal ID and the password as the client secret for the service principal. Provide a user name in the form `<service principal id>@<tenant id>`. Set `SQLCMDPASSWORD` variable to the client secret. If using a certificate instead of a client secret, set `AZURE_CLIENT_CERTIFICATE_PATH` environment variable to the path of the certificate file.

### Environment variables for Azure AD authentication

Some Azure AD authentication settings don't have command line inputs, and some environment variables are consumed directly by the `azidentity` package used by **sqlcmd**.

These environment variables can be set to configure some aspects of Azure AD authentication and to bypass default behaviors. In addition to the variables listed previously, the following are **sqlcmd**-specific and apply to multiple methods.

#### SQLCMDCLIENTID

Set this environment variable to the identifier of an application registered in your Azure AD, which is authorized to authenticate to Azure SQL Database. Applies to `ActiveDirectoryInteractive` and `ActiveDirectoryPassword` methods.

## Next steps

- [Learn more about go-sqlcmd on GitHub](https://github.com/microsoft/go-sqlcmd)
- [Run on Docker](../../linux/quickstart-install-connect-docker.md)
