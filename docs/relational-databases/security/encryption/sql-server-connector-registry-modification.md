---
description: This article describes the modifying registry entries for SQL Server Connector.
title: SQL Connector error and information logging
ms.date: 07/17/2020
ms.localizationpriority: medium
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
author: rupp29
ms.author: arupp
---

# SQL Connector error and information logging

This article describes modifying registry entires to enable SQL Server Connector error and information logging.

## SQL Server Connector for Microsoft Azure Key Vault

The [SQL Server Connector for Microsoft Azure Key Vault](https://www.microsoft.com/download/details.aspx?id=45344) enables SQL Server encryption to use the Microsoft Azure Key Vault as an extensible key management (EKM) provider to protect its encryption keys.

The [download](https://www.microsoft.com/download/details.aspx?id=45344) consists of the SQL Server Connector as well as sample scripts to enable a SQL Server Administrator to learn how to configure the SQL Server Connector and enable SQL Server encryption scenarios. For more information, review the topic [Extensible key management using Key Vault (SQL Server)](https://go.microsoft.com/fwlink/p/?LinkId=521690).

Use the [Azure Key Vault forum](https://social.msdn.microsoft.com/Forums/en-US/AzureKeyVault) to ask questions, share insights and discuss the SQL Server Connector.

> [!NOTE]
> - SQL Server Connector versions 1.0.0.440 and older have been replaced and are no longer supported in production environments and using the instructions on the [SQL Server Connector Maintenance & Troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md) page under [Upgrade of SQL Server Connector](sql-server-connector-maintenance-troubleshooting.md#upgrade-of--connector).
> - Starting with version 1.0.3.0, the SQL Server Connector reports relevant error messages to the Windows event logs for troubleshooting.
> - Starting with version 1.0.4.0, there is support for private Azure clouds, including Azure China, Azure Germany, and Azure Government.
> - There is a breaking change in version 1.0.5.0 in terms of the thumbprint algorithm. You may experience database restore failures after upgrading to 1.0.5.0. For more information, refer to [KB article 447099](https://support.microsoft.com/help/4470999/db-backup-problems-to-sql-server-connector-for-azure-1-0-5-0).
> - **Starting with version 1.0.7.0, the SQL Server Connector supports filtering messages and network request retry logic.**

**System Requirements** - Supported SQL Server versions:

- SQL Server 2019 RTM Enterprise 64-bit
- SQL Server 2017 RTM Enterprise 64-bit
- SQL Server 2016 RTM Enterprise 64-bit
- SQL Server 2014 RTM Enterprise 64-bit
- SQL Server 2012 SP2 Enterprise 64-bit
- SQL Server 2012 SP1 CU6 Enterprise 64-bit
- SQL Server 2008 R2 SP2 CU8 Enterprise 64-bit

On SQL Server 2008 and 2012 versions lower than the ones listed above, the patch specified in the following KB article needs to be installed:  [https://support2.microsoft.com/kb/2859713](https://support2.microsoft.com/kb/2859713)

The SQL Server Connector for Microsoft Azure Key Vault also requires .NET Version 4.5.1 on the Microsoft SQL Server Virtual Machine on Azure. This library should be installed before you install the SQL Connector.

Have the appropriate version of the Visual Studio C++ redistributable installed based on the version of SQL Server that you're running:

- For SQL Server versions 2008, 2008 R2, 2012, and 2014, install the 2013 Visual C++ Redistributable

- For SQL Server 2016, install the 2015 Visual C++ Redistributable.

## Non-Administrator Installation

Installing SQL Server Connector needs **Local Administrator** privileges. Otherwise, manual configuration is required in the Windows Registry. Modifying the registry is only required if the SQL Server Connector (version **1.0.7.0**) was installed using a non-administrator login.

## Modify Windows registry steps

Modify registry entries to enable SQL Server Connector logging error and information events in the Windows Application Event Log.

> [!CAUTION]
> Follow the steps in this section carefully. Serious problems might occur if you modify the registry incorrectly. Before you modify it, [back up the registry for restoration](https://support.microsoft.com/help/322756) in case problems occur.

1. There are two ways to open Registry Editor in Windows 10:
    - In the search box on the taskbar, type regedit. Then, select the top result for Registry Editor (Desktop app).

    ![ekm-regedit-open.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-open.png "ekm-regedit-open.png")
    - Press and hold or right-click the Start  button, then select Run. Enter regedit in the Open: box and select OK.

   ![ekm-regedit-start](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-start.png "ekm-regedit-start.png")

1. Navigate to this registry key:

    **HKLM\SOFTWARE\Microsoft\SQL Server Cryptographic Provider\Azure Key Vault\\**

    ![ekm-regedit-akv.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-akv.png "ekm-regedit-akv.png")  

1. Add a new Key under **Azure Key Vault** named `Log`:

    **HKLM\SOFTWARE\Microsoft\SQL Server Cryptographic Provider\Azure Key Vault\\Log\\**

    ![ekm-regedit-akv-log.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-akv-log.png "ekm-regedit-akv-log.png")  

1. Below the **Log** key, add a DWORD (32-bit) Value named `Level`:

    **HKLM\SOFTWARE\Microsoft\SQL Server Cryptographic Provider\Azure Key Vault\\Log\\**

    ![ekm-regedit-akv-log-dword.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-akv-log-dword.png "ekm-regedit-akv-log-dword.png")  

1. Set the value of the DWORD as an appropriate Log Level (0,1,2):
   1. 0 (Info) - **Default**
   1. 1 (Error)
   1. 2 (No Log)

   ![ekm-regedit-akv-log-level.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-akv-log-level.png "ekm-regedit-akv-log-level.png")  

The registry entries described in this article are found under this key:

```console
\Computer
    \HKEY_LOCAL_MACHINE
       \SOFTWARE
          \Microsoft
             \SQL Server Cryptographic Provider
                \Azure Key Vault
                   \Log\
                      <Level>
```

## Related articles

- For additional sample scripts, see the blog at [SQL Server Transparent Data Encryption and Extensible Key Management with Azure Key Vault](https://techcommunity.microsoft.com/t5/sql-server/intro-sql-server-transparent-data-encryption-and-extensible-key/ba-p/1427549)
- [Extensible Key Management (EKM)](extensible-key-management-ekm.md)  
- [Extensible Key Management using Azure Key Vault](extensible-key-management-using-azure-key-vault-sql-server.md)
