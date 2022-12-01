---
title: Rotate SQL Server on Linux keytabs
description: Recommendations on rotating keytabs for SQL Server on Linux using adutil when configured for Active Directory authentication
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 09/27/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2017 || = sqlallproducts-allversions"
---

# Rotate keytabs for SQL Server on Linux

Based on your organization's security best practices, you may be required to rotate the password regularly for the Windows Active Directory account provided as **network.privilegedadaccount** in **mssql.conf**, or any other account that owns the service principal names (SPN) for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service. The supported method for changing the password for the account is documented in this article. The password change takes effect without the need to restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service on Linux.

The **adutil** tool is used to update the keytab. The **adutil** command must be run from a domain-joined machine. For more information about **adutil** and how to download the tool, see [Introduction to adutil - Active Directory Utility](sql-server-linux-ad-auth-adutil-introduction.md).

It's critical to update the new password in the keytab with the next **kvno** number before updating it in Active Directory. Using the next **kvno** number prevents the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service from the need to be restarted after the password change. If you update the password in Active Directory first, and then change the keytab, you'll need to restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service to ensure that Active Directory authentication works properly.

## Scenario for rotating the keytab

Let's consider an example. Active Directory authentication is already enabled for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. In the **mssql.conf** file, you have set the **network.privilegedadaccount** to `sqluser`. The account `sqluser@CONTOSO.COM` is already created in Active Directory and the keytab has also been created at the default location `/var/opt/mssql/secrets/mssql.keytab`. Now you want to change the password for the `sqluser@CONTOSO.COM`. Here are the steps that you'll need to follow:

1. [Install adutil](sql-server-linux-ad-auth-adutil-introduction.md#install-adutil) on the domain joined machine.

1. Obtain or renew the Kerberos TGT (ticket-granting ticket) using the `kinit` command. Use a privileged account for the `kinit` command. The account needs to have permission to connect to the domain and should be able to create accounts and SPNs in the domain. In this case, we're using the account **privilegeduser@CONTOSO.COM** that has permissions to create accounts and SPNs in our domain called **CONTOSO.COM**.

   ```bash
   kinit privilegeduser@CONTOSO.COM
   ```

1. Once you've run `kinit` to obtain/renew the TGT, query the current **kvno** number of the **network.privilegedadaccount**. In this case, it's `sqluser@CONTOSO.COM`.

   ```bash
   kvno sqluser@CONTOSO.COM
   ```

1. For example, if the **kvno** number from the previous command is `2`, use the **kvno** number `3` when updating the keytab. Updating the keytab using adutil adds an entry into the current keytab. Below are the adutil commands that you need to run.

   - Change the port number (-p), hostname (-H), path to keytab(-k), and kvno number to match your environment.

     ```bash
     adutil keytab createauto -k /var/opt/mssql/secrets/mssql.keytab -p 1433 -H mssql.contoso.com --password '<newpassword>' -s MSSQLSvc --kvno 3
     ```

     ```bash
     adutil keytab create -k /var/opt/mssql/secrets/mssql.keytab -p sqluser --password '<newpassword>' --kvno 3
     ```

     **-k:** is the path to the current keytab that is being used by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and set using the option **network.kerberoskeytabfile** in the **mssql.conf** file.

     **-H:** is the fully qualified domain name of the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] host.

     **-p:** is the port that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service is configured to listen on in the first command. In the second command, **-p** represents the **network.privilegedadaccount** that you are going to update the password for.

     **kvno:** value needs to be the current kvno + 1. The current **kvno** value is obtained from step 3.

Once you run the above commands, you'll have to provide your choice of encryption type for the keytab entries. Ensure you choose the right one for your environment.

## Check the keytab entries

After updating the keytab, you should now see the entries in the keytab for the `kvno 3` (new), and also `kvno 2` (old) for the same account `sqluser@CONTOSO.COM` and SPNs. You can run the `klist` command shown below to check the entries in the keytab:

```bash
klist -kte /var/opt/mssql/secrets/mssql.keytab
```

## Change the account password in Active Directory

The last step is to update the password of the **network.privilegedadaccount** or the account that owns the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] SPNs in Windows Active Directory. In the above scenario, we'll have to update the password for the sqluser@CONTOSO.COM in the Active Directory. Change the password to the `<newpassword>` that you provided in the step 3 in the section above. Active Directory authentication should continue to work, and without the need for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service to restart.

## Next steps

- [Use adutil to configure Active Directory authentication with SQL Server on Linux](sql-server-linux-ad-auth-adutil-tutorial.md)
- [Configure Active Directory authentication with SQL Server on Linux containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
- [Understanding Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-understanding.md)
- [Troubleshooting Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-troubleshooting.md)
