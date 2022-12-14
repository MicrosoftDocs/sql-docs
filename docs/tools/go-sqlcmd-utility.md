---
title: go-sqlcmd utility (preview)
description: The go-sqlcmd utility lets you enter Transact-SQL statements, system procedures, and script files using different modes.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.custom: seo-lt-2019
ms.date: 10/12/2022
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017"
---

# go-sqlcmd utility (preview)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

 The **go-sqlcmd** utility (preview) lets you enter Transact-SQL statements, system procedures, and script files at the command prompt and uses the [go-mssqldb](https://github.com/microsoft/go-mssqldb) driver for go language.  go-sqlcmd aims to be a complete port of [sqlcmd](sqlcmd-utility.md) to the go language and compiles to executable binaries for Windows, macOS, and Linux on both x64 and arm64 architectures.  [Download and install](#download-and-install-go-sqlcmd) the go-sqlcmd binaries to get started without additional dependencies.  Using go-sqlcmd in place of sqlcmd removes the ODBC driver dependency, increases options for Azure Active Directory authentication types, and adds [additional enhancements](#enhancements).

 **go-sqlcmd** is open source under the MIT license and available on [GitHub](https://github.com/microsoft/go-sqlcmd). As a CLI, go-sqlcmd is ideal for pipelines and edge applications as it has no additional dependencies and supports a wide variety of environment configurations. The capabilities of go-sqlcmd expand beyond the ODBC-based [sqlcmd](sqlcmd-utility.md) to incorporate a [vertical output format](#enhancements) and extensive [Azure Active Directory authentication](#azure-active-directory-authentication) options.

## Download and install go-sqlcmd

### Linux

#### apt (Debian/Ubuntu)

1. Import the public repository GPG keys.

    ```bash
    curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
    ```

2. Add the Microsoft repository, where the `ubuntu/20.04` segment may be `debian/11`, `ubuntu/20.04`, or `ubuntu/22.04`.

    ```bash
    add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/prod.list)"
    ```

3. Install go-sqlcmd with apt.

    ```bash
    apt-get update
    apt-get install sqlcmd
    ```

#### yum (Fedora/CentOS)


1. Import the Microsoft repository key.

    ```bash
    rpm --import https://packages.microsoft.com/keys/microsoft.asc
    ```

2. Download the repository configuration file, where the `centos/8` segment may be `centos/8`, `fedora/32`, `opensuse/42.3`, `rhel/8`, or `sles/15`.  If the version of your OS may not directly correspond to one of those options, you can likely successfully use a repository configuration file from a version. For example, `centos/8` can be used in an environment running CentOS 7.

    ```bash
    curl -o /etc/yum.repos.d/packages-microsoft-com-prod.repo https://packages.microsoft.com/config/centos/8/prod.repo
    ```

3. Install go-sqlcmd with yum.

    ```bash
    yum install sqlcmd
    ```

#### Direct download

1. Download the corresponding `-linux-x64.tar.bz2` or `-linux-arm.tar.bz2` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of go-sqlcmd from the GitHub code repository. 

2. Extract the `sqlcmd` file from the downloaded zip folder.


### Windows

#### Winget (Windows Package Manager Client)

1. Install the [Windows Package Manager Client](/windows/package-manager/winget) if you don't already have it.
2. Run the following command to install go-sqlcmd.

    ```bash
    winget install sqlcmd
    ```

#### Direct download

1. Download the corresponding `-windows-x64.zip` or `-windows-arm.zip` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of go-sqlcmd from the GitHub code repository. 

2. Extract the `sqlcmd.exe` file from the downloaded zip folder.


### macOS

#### Homebrew

1. Install Homebrew if you don't already have it.

    ```bash
    /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
    ```
2. Install sqlcmd with Homebrew.

    ```bash
    brew install sqlcmd
    ```

#### Direct download

1. Download the `-darwin-x64.zip` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of go-sqlcmd from the GitHub code repository. 

2. Extract the `sqlcmd` file from the downloaded zip folder.

## Syntax

For more in-depth information on sqlcmd syntax and use, see: 

- [sqlcmd syntax](./sqlcmd-utility.md#syntax)
- [Start the sqlcmd Utility](../ssms/scripting/sqlcmd-start-the-utility.md)
- [Use the sqlcmd Utility](../ssms/scripting/sqlcmd-use-the-utility.md)

### Breaking changes from sqlcmd

Several switches and behaviors are altered from [sqlcmd](sqlcmd-utility.md) in go-sqlcmd.

- `-P` switch is removed. Passwords for SQL authentication can only be provided through these mechanisms:
    - The `SQLCMDPASSWORD` environment variable
    - The `:CONNECT` command
    - When prompted, the user can type the password to complete a connection
- `-r` requires a `0` or `1` argument
- `-R` switch is removed.
- `-I` switch is removed. To disable quoted identifier behavior, add `SET QUOTED IDENTIFIER OFF` in your scripts.
- `-N` now takes a string value that can be one of `true`, `false`, or `disable` to specify the encryption choice. (`default` is the same as omitting the parameter)
  - If `-N` and `-C` aren't provided, sqlcmd will negotiate authentication with the server without validating the server certificate.
  - If `-N` is provided but `-C` isn't, sqlcmd will require validation of the server certificate. A `false` value for encryption could still lead to encryption of the login packet.
  - If both `-N` and `-C` are provided, sqlcmd will use their values for encryption negotiation.
  - More information about client/server encryption negotiation can be found at [MS-TDS PRELOGIN](/openspecs/windows_protocols/ms-tds/60f56408-0188-4cd5-8b90-25c6f2423868).
- `-u` The generated Unicode output file will have the UTF16 Little-Endian Byte-order mark (BOM) written to it.
- Some behaviors that were kept to maintain compatibility with `OSQL` may be changed, such as alignment of column headers for some data types.
- All commands must fit on one line, even `EXIT`. Interactive mode won't check for open parentheses or quotes for commands and prompt for successive lines. The ODBC sqlcmd allows the query run by `EXIT(query)` to span multiple lines.

Connections from go-sqlcmd are limited to TCP connections. Named pipes are not supported at this time in the go-mssqldb driver.

## Enhancements

- `:Connect` now has an optional `-G` parameter to select one of the authentication methods for Azure SQL Database  - `SqlAuthentication`, `ActiveDirectoryDefault`, `ActiveDirectoryIntegrated`, `ActiveDirectoryServicePrincipal`, `ActiveDirectoryManagedIdentity`, `ActiveDirectoryPassword`. More information on [Azure Active Directory authentication](#azure-active-directory-authentication) support below. If `-G` isn't provided, either Integrated security or SQL Authentication will be used, dependent on the presence of a `-U` user name parameter.
- The new `--driver-logging-level` command line parameter allows you to see traces from the `go-mssqldb` client driver. Use `64` to see all traces.
- Sqlcmd can now print results using a vertical format. Use the new `-F vertical` command line option to set it. It's also controlled by the `SQLCMDFORMAT` scripting variable.

## Azure Active Directory authentication

This version of sqlcmd supports a broader range of Azure Active Directory authentication models, based on the [azidentity package](https://pkg.go.dev/github.com/Azure/azure-sdk-for-go/sdk/azidentity). The implementation relies on an Azure Active Directory Connector in the [driver](https://github.com/microsoft/go-mssqldb).

### Command line

To use Azure Active Directory auth, you can use one of two command line switches.

`-G` is (mostly) compatible with its usage in the prior version of sqlcmd. If a user name and password are provided, it will authenticate using Azure Active Directory Password authentication. If a user name is provided it will use Azure Active Directory Interactive authentication, which may display a web browser. If no user name or password is provided, it will use a DefaultAzureCredential, which attempts to authenticate through various mechanisms.

`--authentication-method=` can be used to specify one of the following authentication types.

`ActiveDirectoryDefault`

- For an overview of the types of authentication this mode will use, see [Default Azure Credential](https://github.com/Azure/azure-sdk-for-go/tree/main/sdk/azidentity#defaultazurecredential).
- Choose this method if your database automation scripts are intended to run in both local development environments and in a production deployment in Azure. In your development environment you'll be able to use a client secret or an Azure CLI login. Without changing the script from the development environment, you'll be able to use a managed identity or client secret on your production deployment.
- Setting environment variables AZURE_TENANT_ID, and AZURE_CLIENT_ID are necessary for DefaultAzureCredential to begin checking the environment configuration and look for one of the following additional environment variables in order to authenticate:
    - Setting environment variable AZURE_CLIENT_SECRET configures the DefaultAzureCredential to choose ClientSecretCredential.
    - Setting environment variable AZURE_CLIENT_CERTIFICATE_PATH configures the DefaultAzureCredential to choose ClientCertificateCredential if AZURE_CLIENT_SECRET isn't set.
    - Setting environment variable AZURE_USERNAME configures the DefaultAzureCredential to choose UsernamePasswordCredential if AZURE_CLIENT_SECRET and AZURE_CLIENT_CERTIFICATE_PATH aren't set.

`ActiveDirectoryIntegrated`

This method is currently not implemented and will fall back to `ActiveDirectoryDefault`

`ActiveDirectoryPassword`

This method will authenticate using a user name and password. It will not work if MFA is required.
You provide the user name and password using the usual command line switches or SQLCMD environment variables.
Set `AZURE_TENANT_ID` environment variable to the tenant ID of the server if not using the default tenant of the user.

`ActiveDirectoryInteractive`

This method will launch a web browser to authenticate the user.

`ActiveDirectoryManagedIdentity`

Use this method when running sqlcmd on an Azure VM that has either a system-assigned or user-assigned managed identity. If using a user-assigned managed identity, set the user name to the ID of the managed identity. If using a system-assigned identity, leave user name empty.

`ActiveDirectoryServicePrincipal`

This method authenticates the provided user name as a service principal ID and the password as the client secret for the service principal. Provide a user name in the form `<service principal id>@<tenant id>`. Set `SQLCMDPASSWORD` variable to the client secret. If using a certificate instead of a client secret, set `AZURE_CLIENT_CERTIFICATE_PATH` environment variable to the path of the certificate file.

### Environment variables for Azure Active Directory authentication

Some settings for Azure Active Directory authentication do not have command line inputs, and some environment variables are consumed directly by the `azidentity` package used by `sqlcmd`.
These environment variables can be set to configure some aspects of Azure Active Directory authentication and to bypass default behaviors. In addition to the variables listed above, the following are sqlcmd-specific and apply to multiple methods.

`SQLCMDCLIENTID` - set this environment variable to the identifier of an application registered in your Azure Active Directory, which is authorized to authenticate to Azure SQL Database. Applies to `ActiveDirectoryInteractive` and `ActiveDirectoryPassword` methods.

## Next steps
- [Learn more about go-sqlcmd on GitHub](https://github.com/microsoft/go-sqlcmd)
- [Run on Docker](../linux/quickstart-install-connect-docker.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
