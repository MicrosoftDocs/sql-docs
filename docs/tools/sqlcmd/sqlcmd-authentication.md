---
title: Azure Active Directory authentication in sqlcmd
description: Authenticating with Azure Active Directory in the new standalone sqlcmd utility.
author: dlevy-msft
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 08/15/2023
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017"
---
# Azure Active Directory authentication in sqlcmd

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

**sqlcmd** supports a variety of Azure Active Directory (Azure AD) authentication models, depending on which version you have installed.

For more information on the difference between **sqlcmd** versions, see [sqlcmd utility](sqlcmd-utility.md#find-out-which-version-you-have-installed).

## [sqlcmd (ODBC)](#tab/odbc)

The `-G` option is used by **sqlcmd** (ODBC) when connecting to Azure SQL Database or Azure Synapse Analytics, to specify that the user be authenticated using Azure AD authentication. This option sets the **sqlcmd** scripting variable `SQLCMDUSEAAD=true`.

- The `-G` option requires at least **sqlcmd** version [13.1](https://go.microsoft.com/fwlink/?LinkID=825643). Azure AD interactive authentication requires **sqlcmd** (ODBC) [version 15.0.1000.34](sqlcmd-utility.md#download-and-install-sqlcmd) and later versions, as well as [ODBC version 17.2 and later versions](../../connect/odbc/download-odbc-driver-for-sql-server.md).

- Azure AD integrated authentication requires [Microsoft ODBC Driver 17 for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md) version 17.6.1 and later versions, and a properly [configured Kerberos environment](../../connect/odbc/linux-mac/using-integrated-authentication.md).

- The `-G` option only applies to Azure SQL Database and Azure Synapse Analytics.

- Azure AD interactive authentication isn't currently supported on Linux or macOS.

To determine your version, execute `sqlcmd "-?"`. For more information, see [Connecting to SQL Database or Azure Synapse Analytics By Using Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview). The `-A` option isn't supported with the `-G` option.

### Azure Active Directory username and password

When you want to use an Azure AD user name and password, you can provide the `-G` option with the user name and password, by using the `-U` and `-P` options.

```console
sqlcmd -S testsrv.database.windows.net -d Target_DB_or_DW -U bob@contoso.com -P MyAzureADPassword -G
```

The `-G` parameter generates the following connection string in the backend:

```output
SERVER = Target_DB_or_DW.testsrv.database.windows.net;UID=bob@contoso.com;PWD=MyAzureADPassword;AUTHENTICATION=ActiveDirectoryPassword;
```

### Azure Active Directory integrated authentication

For Azure AD integrated authentication, provide the `-G` option without a user name or password.

```console
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net -G
```

This command generates the following connection string in the backend:

```output
SERVER = Target_DB_or_DW.testsrv.database.windows.net;Authentication=ActiveDirectoryIntegrated;Trusted_Connection=NO;
```

> [!NOTE]  
> The `-E` option (`Trusted_Connection`) cannot be used along with the `-G` option.

### Azure Active Directory interactive authentication

The Azure AD interactive authentication for Azure SQL Database and Azure Synapse Analytics, allows you to use an interactive method supporting multi-factor authentication. For more information, see [Active Directory Interactive Authentication](../../ssdt/azure-active-directory.md#active-directory-interactive-authentication).

To enable interactive authentication, provide the `-G` option with user name (`-U`) only, without a password.

The following example exports data using Azure AD interactive mode, indicating a username where the user represents an Azure AD account. This is the same example used in the previous section, **Azure Active Directory username and password**.

Interactive mode requires manually entering a password, or for accounts with multi-factor authentication enabled, complete your configured MFA authentication method.

```console
sqlcmd -S testsrv.database.windows.net -d Target_DB_or_DW -G -U alice@aadtest.onmicrosoft.com
```

The previous command generates the following connection string in the backend:

```output
SERVER = Target_DB_or_DW.testsrv.database.windows.net;UID=alice@aadtest.onmicrosoft.com;AUTHENTICATION=ActiveDirectoryInteractive
```

In case an Azure AD user is a domain federated user using a Windows account, the user name required in the command-line contains its domain account (for example `joe@contoso.com`):

```console
sqlcmd -S testsrv.database.windows.net -d Target_DB_or_DW -G -U joe@contoso.com
```

If guest users exist in a specific Azure AD tenant, and are part of a group that exists in Azure SQL Database that has database permissions to execute the **sqlcmd** command, their guest user alias is used (for example, `keith0@adventureworks.com`).

> [!IMPORTANT]  
> There is a known issue when using the `-G` and `-U` option with **sqlcmd** (ODBC), where putting the `-U` option before the `-G` option may cause authentication to fail. Always start with the `-G` option followed by the `-U` option.

## [sqlcmd (Go)](#tab/go)

**sqlcmd** (Go) supports more Azure AD authentication models, based on the [azidentity package](https://pkg.go.dev/github.com/Azure/azure-sdk-for-go/sdk/azidentity). The implementation relies on an Azure AD connector in the [go-sqlcmd driver](https://github.com/microsoft/go-mssqldb).

### Command line arguments

To use Azure AD authentication, you can use one of two command line switches.

`-G` is (mostly) compatible with its usage in **sqlcmd** (ODBC). If a username and password are provided, it authenticates using Azure AD Password authentication. If a user name is provided, it uses Azure AD Interactive authentication, which may display a web browser. If no username or password is provided, it uses a `DefaultAzureCredential`, which attempts to authenticate through various mechanisms.

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

Use this method when running **sqlcmd** (Go) on an Azure VM that has either a system-assigned or user-assigned managed identity. If using a user-assigned managed identity, set the user name to the ID of the managed identity. If using a system-assigned identity, leave user name empty.

#### ActiveDirectoryServicePrincipal

This method authenticates the provided user name as a service principal ID and the password as the client secret for the service principal. Provide a user name in the form `<service principal id>@<tenant id>`. Set `SQLCMDPASSWORD` variable to the client secret. If using a certificate instead of a client secret, set `AZURE_CLIENT_CERTIFICATE_PATH` environment variable to the path of the certificate file.

### Environment variables for Azure AD authentication

Some Azure AD authentication settings don't have command line inputs, and some environment variables are consumed directly by the `azidentity` package used by **sqlcmd** (Go).

These environment variables can be set to configure some aspects of Azure AD authentication and to bypass default behaviors. In addition to the variables listed previously, the following are specific to **sqlcmd** (Go), and apply to multiple methods.

#### SQLCMDCLIENTID

Set this environment variable to the identifier of an application registered in your Azure AD, which is authorized to authenticate to Azure SQL Database. Applies to `ActiveDirectoryInteractive` and `ActiveDirectoryPassword` methods.

---

## Next steps

- [sqlcmd utility](sqlcmd-utility.md)
- [sqlcmd - Start the utility](sqlcmd-start-utility.md)
- [sqlcmd - Use the utility](sqlcmd-use-utility.md)
