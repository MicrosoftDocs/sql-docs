---
description: This article describes the modifying registry entries for SQL Connector.
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

This article describes the modifying registry entires to enable SQL Connector error and information logging.

## Modify registry entires to enable SQL Connector logging  error and information events in the Windows Application Event Log

> [!IMPORTANT]
> Follow the steps in this section carefully. Serious problems might occur if you modify the registry incorrectly. Before you modify it, [back up the registry for restoration](https://support.microsoft.com/help/322756) in case problems occur.

> [!NOTE]
> **Enhanced registry setting for logging error and information messages in teh Appliaction Event Log  are only valid for SQL Connector version: 1.0.6.0 and greater**

> [!NOTE] 
  > Versions 1.0.0.440 and older have been replaced and are no longer supported in production environments. Upgrade to version 1.0.1.0 or later by visiting the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=45344) and using the instructions on the [SQL Server Connector Maintenance & Troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md) page under "Upgrade of SQL Server Connector."

> [!NOTE]
> There is a breaking change in 1.0.5.0 version, in terms of the thumbprint algorithm. You may experience database restore failure after upgrading to 1.0.5.0 version. Please refer KB article [447099](https://support.microsoft.com/help/4470999/db-backup-problems-to-sql-server-connector-for-azure-1-0-5-0).
  
## Modify Windows registry steps

1. There are two ways to open Registry Editor in Windows 10:
    1. In the search box on the taskbar, type regedit. Then, select the top result for Registry Editor (Desktop app).
    1. Press and hold or right-click the Start  button, then select Run. Enter regedit in the Open: box and select OK.

       ![ekm-regedit-open.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-open.png "ekm-regedit-open.png")  

1. Navigate to this registry key:

    **HKLM\SOFTWARE\Microsoft\SQL Server Cryptographic Provider\Azure Key Vault\\**
    
    ![ekm-regedit-akv.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-akv.png "ekm-regedit-akv.png")  

1. Add a new Key under "Azure Key Vault" named: Log:

    **HKLM\SOFTWARE\Microsoft\SQL Server Cryptographic Provider\Azure Key Vault\\Log\\**

    ![ekm-regedit-akv-log.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-akv-log.png "ekm-regedit-akv-log.png")  

1. Below the "Log" key add a DWORD (32-bit) Value named "Level":

    **HKLM\SOFTWARE\Microsoft\SQL Server Cryptographic Provider\Azure Key Vault\\Log\\**

    ![ekm-regedit-akv-log-dword.png](../../../relational-databases/security/encryption/media/ekm-registry/ekm-regedit-akv-log-dword.png "ekm-regedit-akv-log-dword.png")  

1. Set the value of the DWORD as appropriate LogLevel (0,1,2):
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

[Extensible Key Management (EKM)](extensible-key-management-ekm.md)  
[Extensible Key Management using Azure Key Vault](extensible-key-management-using-azure-key-vault-sql-server.md) 