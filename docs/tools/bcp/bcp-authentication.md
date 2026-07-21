---
title: Authenticate with Microsoft Entra ID in bcp
description: Use Microsoft Entra ID to authenticate the bulk copy program (bcp) utility against Azure SQL, Microsoft Fabric SQL database, or SQL Server 2022 and later versions.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: davidengel
ms.date: 06/25/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - linux-related-content
  - peer-review-program
helpviewer_keywords:
  - "bcp utility [SQL Server], authentication"
  - "bcp utility [SQL Server], Microsoft Entra ID"
  - "Microsoft Entra ID, bcp"
  - "managed identity, bcp"
monikerRange: ">=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb || =fabric"
---
# Authenticate with Microsoft Entra ID in bcp

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

The [bulk copy program utility (bcp)](bcp-utility.md) supports several Microsoft Entra ID authentication models when you connect to Azure SQL Database, Azure SQL Managed Instance, [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], Azure Synapse Analytics, or [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

To check whether your installed **bcp** supports Microsoft Entra authentication, run `bcp --help` and verify that `-G` appears in the list of available arguments.

## Platform restrictions

Not all authentication modes are available on every platform:

- Microsoft Entra interactive authentication is supported on Windows only.

- Microsoft Entra integrated authentication on Linux and macOS requires [Microsoft ODBC Driver 18 for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md) (driver 17.6.1 or later if you can't move to driver 18) and a [properly configured Kerberos environment](../../connect/odbc/linux-mac/using-integrated-authentication.md#configure-kerberos).

- Authentication with an access token file (`-P <token_file>`) is supported on Linux and macOS only.

- **bcp** is currently in preview in [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].
- **bcp** can't import data in [!INCLUDE [fabric-se](../../includes/fabric-se.md)].

## Microsoft Entra username and password

Provide `-G` together with `-U` (username) and `-P` (password).

The following example exports table `bcptest` from database `testdb` on `contoso.database.windows.net` to file `c:\last\data1.dat`. Replace `<password>` with a valid password.

```console
bcp bcptest out "c:\last\data1.dat" -c -S contoso.database.windows.net -d testdb -G -U alice@contoso.onmicrosoft.com -P <password>
```

The following example imports the same data:

```console
bcp bcptest in "c:\last\data1.dat" -c -S contoso.database.windows.net -d testdb -G -U alice@contoso.onmicrosoft.com -P <password>
```

## Microsoft Entra integrated

Provide `-G` without `-U` or `-P`. The current Windows account (or Kerberos identity on Linux/macOS) must be federated with Microsoft Entra ID. In the following examples, replace `<server>` with your server name.

Export:

```console
bcp bcptest out "c:\last\data2.dat" -S <server>.database.windows.net -d testdb -G -c
```

Import:

```console
bcp bcptest in "c:\last\data2.dat" -S <server>.database.windows.net -d testdb -G -c
```

## Microsoft Entra Managed Service Identity

Authenticate as either a system-assigned or user-assigned managed identity through a configured DSN. The same approach works for both `bcp in` and `bcp out`.

> [!IMPORTANT]  
> **bcp** is tightly coupled to its driver. The major version of **bcp** must match the major version of the driver the DSN is created with. To determine your **bcp** version, run `bcp -v`.

### [Windows](#tab/windows)

Configure a DSN through the **ODBC Data Source Administrator**:

1. Press the Windows key on your keyboard.
1. Type `ODBC` and select the appropriate version of the **ODBC Data Source Administrator**.
1. Select either the **User DSN** or **System DSN** tab.
1. Select **Add** and follow the prompts.
1. When asked for an authentication type, select **Azure Managed Service Identity authentication**.
1. For a User Assigned Managed Identity, paste the `Object (principal) ID` of the identity into the **Login ID** box on the authentication tab.
1. Continue following the prompts to finish configuring the DSN.

For a full walkthrough including screenshots, see [Creating and editing DSNs in the UI](../../connect/odbc/using-azure-active-directory.md#creating-and-editing-dsns-in-the-ui).

Use the `-D` flag to indicate that the value passed to `-S` is a DSN. The `-D` and `-S` switches can appear in any order on the command line.

```console
bcp bcptest out "c:\last\data1.dat" -c -D -S myDSN -d testdb
```

### [Linux and macOS](#tab/linux)

Edit the user `.odbc.ini` file (typically at `~/.odbc.ini`). Run `odbcinst -j` to confirm the DSN file location.

```bash
vi /home/<user>/.odbc.ini
```

Add an entry like the following. Omit the `LastUser` line for a system-assigned managed identity. Replace `<server>` and `<object_id_of_user_assigned_managed_identity>` with values for your environment.

```ini
[myDSN]
Driver = ODBC Driver 18 for SQL Server
Server = <server>.database.windows.net
Database = testdb
Encrypt = yes
LastUser = <object_id_of_user_assigned_managed_identity>
Authentication = ActiveDirectoryMSI
```

Use the `-D` flag to indicate that the value passed to `-S` is a DSN. The `-D` and `-S` switches can appear in any order on the command line.

```bash
bcp bcptest out data1.dat -c -D -S myDSN -d testdb
```

---

## Microsoft Entra ID access token

**Applies to**: Linux and macOS only. Windows isn't supported.

**bcp** 17.8 and later versions on Linux and macOS can authenticate with an access token. The following examples use the [Azure CLI](/cli/azure/install-azure-cli-linux) to retrieve the token and write it to a secure temporary file.

> [!IMPORTANT]
> The token file must be UTF-16LE without a BOM. Restrict file permissions and delete the file when it's no longer needed, as shown in the following examples.

### System-assigned managed identity

Replace `<server>` with your server name.

1. Sign in with your managed identity:

   ```bash
   az login --identity
   ```

1. Retrieve the token, write it to a secure temporary file, and run **bcp**:

   ```bash
   # Create a secure temporary file for the token
   tokenFile=$(mktemp)
   chmod 600 "$tokenFile"

   # Retrieve the access token and write it as UTF-16LE without BOM
   az account get-access-token --resource https://database.windows.net --output tsv | cut -f 1 | tr -d '\n' | iconv -f ascii -t UTF-16LE > "$tokenFile"

   # Run bcp with the token file
   bcp bcptest out data2.dat -S <server>.database.windows.net -d testdb -G -P "$tokenFile" -c

   # Clean up token file
   rm -f "$tokenFile"
   ```

### User-assigned managed identity

1. Sign in with your user-assigned managed identity. Replace `<client_id>` with a valid value for your environment.

   ```bash
   az login --identity --username <client_id>
   ```

1. Retrieve the token, write it to a secure temporary file, and run **bcp**. Replace `<server>` with a valid value for your environment.

   ```bash
   # Create a secure temporary file for the token
   tokenFile=$(mktemp)
   chmod 600 "$tokenFile"

   # Retrieve the access token and write it as UTF-16LE without BOM
   az account get-access-token --resource https://database.windows.net --output tsv | cut -f 1 | tr -d '\n' | iconv -f ascii -t UTF-16LE > "$tokenFile"

   # Run bcp with the token file
   bcp bcptest out data2.dat -S <server>.database.windows.net -d testdb -G -P "$tokenFile" -c

   # Clean up token file
   rm -f "$tokenFile"
   ```

## Microsoft Entra interactive

**Applies to**: Windows only. Linux and macOS aren't supported.

Microsoft Entra interactive authentication uses a dialog to authenticate, and supports multifactor authentication (MFA). Interactive authentication requires **bcp** [version 15.0.1000.34](bcp-download-install.md#download-the-latest-version) or later, and [ODBC Driver 18 for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md) (or driver 17.2 or later).

Provide `-G` with `-U` (username) only. Don't include `-P`. **bcp** prompts for the password (or for accounts with MFA enabled, completes the configured MFA flow).

```console
bcp bcptest out "c:\last\data1.dat" -c -S contoso.database.windows.net -d testdb -G -U alice@contoso.onmicrosoft.com
```

For a Microsoft Entra user that's a Windows account from a federated domain, include the domain in the username (for example, `joe@contoso.com`):

```console
bcp bcptest out "c:\last\data1.dat" -c -S contoso.database.windows.net -d testdb -G -U joe@contoso.com
```

If guest users in a Microsoft Entra tenant are part of a group that has database permissions in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], use the guest user alias (for example, `keith0@adventure-works.com`).

## Related content

- [bcp utility](bcp-utility.md)
- [Download and install the bcp utility](bcp-download-install.md)
- [How to use the bcp utility](bcp-use-utility.md)
- [Microsoft Entra authentication for Azure SQL](/azure/azure-sql/database/authentication-aad-overview)
- [Authentication in SQL database in Microsoft Fabric](/fabric/database/sql/authentication)

[!INCLUDE [get-help-options](../../includes/paragraph-content/get-help-options.md)]
