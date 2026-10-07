---
title: Use Custom Password Policy for SQL Logins on Linux
description: Learn how to use a custom password policy for SQL logins with SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: matripathy, amitkh, atsingh
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - build-2025
  - linux-related-content
# customer intent: As a database professional, I want enforce password policy for SQL logins on SQL Server on Linux so that the configuration more closely aligns with secure practices.
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---

# Set custom password policy for SQL logins in SQL Server on Linux

[!INCLUDE [sqlserver2022-and-later-linux](../../../includes/applies-to-version/sqlserver2022-and-later-linux.md)]

This article describes how to set up and manage password policies for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] logins on Linux. Custom password policies are available starting in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] Cumulative Update (CU) 23, and [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)].

Password policies enforce rules for complexity, expiration, and changes, which help to keep [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] logins secure.

> [!NOTE]  
> Password policies are also available on Windows. For more information, see [Password policy](../../../relational-databases/security/password-policy.md).

This enforcement ensures that logins that use SQL Server authentication are secure.

## Custom policy settings

Set the following configuration parameters in the [mssql.conf file](../../configure/mssql-conf.md#mssql-conf-format) to enforce a custom password policy:

| Configuration option | Description |
| --- | --- |
| `passwordpolicy.passwordminimumlength` | Sets the minimum number of characters required for a password. Passwords can be up to 128 characters long. |
| `passwordpolicy.passwordhistorylength` | Sets the number of previous passwords that the system remembers. |
| `passwordpolicy.passwordminimumage` | Sets the minimum duration a user must wait before changing their password again. |
| `passwordpolicy.passwordmaximumage` | Sets the maximum duration a password can be used before it must be changed. |

> [!NOTE]  
> You can set the `passwordminimumlength` to fewer than eight characters. [!INCLUDE [password-complexity](../../includes/password-complexity.md)]

Configure the policy in one of two ways:

- [Set the policy](#adutil) with [adutil](adutil-introduction.md), which fetches values from Active Directory.
- [Set the policy manually](#manual) with the [mssql-conf tool](../../configure/mssql-conf.md).

<a id="adutil"></a>

## Set custom password policy with adutil

In environments where policy management is centralized in an Active Directory (AD) server, domain administrators set and modify the password policy values in the AD server. The Linux machine running SQL Server must also be part of the Windows domain.

Use [adutil](adutil-introduction.md) to fetch the password policy from the AD server and write it to the `mssql.conf` file. This method offers the benefit of centralized management, and ensures consistent application of policies across the SQL Server environment.

### Requirements for adutil

1. Establish a Kerberos authenticated session:

   - Run **`kinit`** with `sudo` to get or renew the Kerberos ticket-granting ticket (TGT).

   - Use a privileged account for the **`kinit`** command. The account needs permission to connect to the domain.

   In the following example, replace `<user>` with an account that has elevated privileges in the domain.

   ```bash
   sudo kinit <user>@CONTOSO.COM
   ```

1. Verify that the ticket is granted:

   ```bash
   sudo klist
   ```

1. To update the password policy, query the domain with **`adutil`**:

   ```bash
   sudo adutil updatepasswordpolicy
   ```

   If the command is successful, the output looks similar to the following example:

   ```output
   Successfully updated password policy in mssqlconf.
   Restart SQL Server to apply the changes.
   ```

   Optionally, you can add the `--path` option to the previous command. You might use this option if you have the **`mssql-conf`** tool in a different location from the default path. The default path is `/opt/mssql/bin/mssql-conf`.

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```

<a id="manual"></a>

## Manually set a custom password policy with mssql-conf

Update the policy parameters in `mssql.conf` directly by using **`mssql-conf`**. Use this method when the Linux host isn't joined to a domain, or when no domain controller is available to source the policy from.

Run the following **`mssql-conf`** commands to set each policy property.

1. Set the minimum password length to 14 characters, adhering to the complexity requirements outlined in the [Password policy](../../../relational-databases/security/password-policy.md).

   ```bash
   sudo /opt/mssql/bin/mssql-conf set passwordpolicy.passwordminimumlength 14
   ```

1. Set the minimum password age to one day. Users can change their password after one day.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set passwordpolicy.passwordminimumage 1
   ```

1. Set the password history length to 8. Users must use eight unique passwords before reusing an old one.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set passwordpolicy.passwordhistorylength 8
   ```

1. Set the maximum password age to 45 days. A user can use a password for up to 45 days before the user must change it.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set passwordpolicy.passwordmaximumage 45
   ```

1. Apply the changes.

   - In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] CU 23 and later versions, and in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] CU 3 and later versions, reload `mssql.conf` without restarting the service. Connect to the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance and run:

     ```sql
     EXECUTE sp_reload_mssqlconf;
     ```

   - Or, on earlier versions, restart the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] service instead:

     ```bash
     sudo systemctl restart mssql-server
     ```

## Limitations

Custom password policies are supported as follows:

- In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] CU 23, the `passwordminimumlength` parameter supports values up to 14 characters.  

- In [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] CU 3 and later versions, the `passwordminimumlength` parameter supports values with more than 14 characters.

Changes to the group password policy in Active Directory don't automatically propagate. Run `adutil updatepasswordpolicy` to refresh `mssql.conf` after each change, or set the values manually by using **`mssql-conf`** if the Linux host isn't joined to the domain.

In Active Directory, you can define or undefine each group-level password policy using a checkbox:

:::image type="content" source="media/custom-password-policy/password-length-properties.png" alt-text="Screenshot of minimum password length security policy setting.":::

Clearing the checkbox doesn't disable the policy in [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux. To stop enforcing a value, update it directly in **`mssql-conf`**.

## Related content

- [Password policy](../../../relational-databases/security/password-policy.md)
- [Strong passwords](../../../relational-databases/security/strong-passwords.md)
