---
title: Distributed Replay Security
description: This article describes security configuration steps for SQL Server Distributed Replay and important considerations for data protection and removal steps.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: distributed-replay
ms.topic: concept-article
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---

# Distributed Replay security

[!INCLUDE [sqlserver2016-2019-only](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

[!INCLUDE [distributed-replay-sql-server-2022](../../includes/distributed-replay-sql-server-2022.md)]

Before you install and use the Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay feature, review the important security information in this article. This article describes the post-installation security configuration steps that are required before you can use Distributed Replay. This article also describes important considerations regarding data protection and important removal steps.

## User and service accounts

The following table describes the accounts that are used for Distributed Replay. After the Distributed Replay installation, you must assign the security principals that the controller and client service accounts will run as. Therefore, we recommend that you configure the corresponding domain user accounts before you install the Distributed Replay features.

| User account | Requirements |
| --- | --- |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay controller service account <sup>1</sup> | Can be a domain user account or local user account. If you use a local user account, the administration tool, controller, and client must all be running on the same computer. |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay client service account <sup>1</sup> | Can be a domain user account or local user account. If you use a local user account, the controller, client, and target SQL Server must all be running on the same computer. |
| Interactive user account that is used to run the Distributed Replay administration tool | Can be either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer. |

<sup>1</sup> Don't add this account to the local Administrators group in Windows.

> [!IMPORTANT]  
> When you configure Distributed Replay controller, you can specify one or more user accounts to run the Distributed Replay client services, from the following list of supported accounts:

- Domain user account
- User created local user account
- Administrator
- Virtual account and MSA (Managed Service Account)
- Network Services, Local Services, and System

Group accounts (local or domain) and other built-in accounts (like Everyone) aren't accepted.

To set the service accounts or their passwords after you install Distributed Replay, you can use the Windows Services tool. To change the service accounts associated with the Distributed Replay controller or client services, follow these steps:

1. Do either of the following, depending on the operating system:

   - Select **Start**, type **services.msc** in the **Search** box, and then press **Enter**.
   - Select **Start**, select **Run**, type **services.msc**, and then press **Enter**.

1. In the **Services** dialog box, right-click the service that you want to configure, and then select **Properties**.

1. On the **Log On** tab, select **This account**.

1. Configure the user account that you want to use.

## File and folder permissions

After the service accounts are specified, you must grant the necessary file and folder permissions to those service accounts. Configure file and folder permissions according to the following table:

| Account | Folder permissions |
| --- | --- |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay controller service account | `<Controller_Installation_Path>\DReplayController` (Read, Write, Delete)<br /><br />`DReplayServer.xml` file (Read, Write) |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay client service account | `<Client_Installation_Path>\DReplayClient` (Read, Write, Delete)<br /><br />`DReplayClient.xml` file (Read, Write)<br /><br />The working and result directories, as specified in the client configuration file by the `WorkingDirectory` and `ResultDirectory` elements, respectively. (Read, Write) |

## DCOM permissions

DCOM is used for remote procedure call (RPC) communication between the controller and the administration tool, and between the controller and all clients. You must configure computer-wide and application-specific DCOM permissions on the controller after the Distributed Replay features are installed.

To configure the controller DCOM permissions, follow these steps:

1. **Open `dcomcnfg.exe`, the Component Services snap-in**: Use this tool to configure DCOM permissions.

   1. On the controller computer, select **Start**.
   1. Type `dcomcnfg.exe` in the **Search** box.
   1. Press **Enter**.

1. **Configure computer-wide DCOM permissions**: Grant the corresponding computer-wide DCOM permissions for each account listed in the following table. For more information about how to set computer-wide permissions, see [Checklist: Manage DCOM Applications](/windows/win32/com/setting-machine-wide-security-using-dcomcnfg).

1. **Configure application-specific DCOM permissions**: Grant the corresponding application-specific DCOM permissions for each account listed in the following table. The DCOM application name for the controller service is `DReplayController`. For more information about how to set application-specific permissions, see [Checklist: Manage DCOM Applications](/windows/win32/com/setting-processwide-security-using-dcomcnfg).

The following table describes which DCOM permissions are required for the administration tool interactive user account and the client service accounts:

| Feature | Account | Required DCOM permissions on controller |
| --- | --- | --- |
| Distributed Replay administration tool | The interactive user account | Local Access<br /><br />Remote Access<br /><br />Local Launch<br /><br />Remote Launch<br /><br />Local Activation<br /><br />Remote Activation |
| Distributed Replay client | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay client service account | Local Access<br /><br />Remote Access<br /><br />Local Launch<br /><br />Remote Launch<br /><br />Local Activation<br /><br />Remote Activation |

> [!IMPORTANT]  
> To help protect against malicious queries or denial of service attacks, make sure that you only use a trusted user account for the client service account. This account can connect and replay workloads against the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## SQL Server permissions

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay client service accounts are used to connect to the workload's target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Only Windows Authentication mode is supported for these connections.

After you install the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay client service on a set of computers, the security principal used for those service accounts must be granted the sysadmin server role on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that you intend to replay the trace workload against. This step isn't performed automatically during Distributed Replay Setup.

## Data protection

In the Distributed Replay environment, the following user accounts are granted full access to the target server instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the input trace data, and result trace files:

- The interactive user account that is used to run the administration tool.

- The controller service account.

- The client service account.

- Members of the local Administrators group on the controller.

- Members of the local Administrators group on the clients.

  > [!IMPORTANT]  
  > These accounts have full access to any personal data or sensitive information that is contained in the trace, intermediate, dispatch, or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data files that were used by Distributed Replay.

Take the following security precautions:

- Store the input trace data, output trace results, and database files in a location that uses the NTFS file system (NTFS), and apply the appropriate access control lists (ACLs). If necessary, encrypt the data that is stored on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] computer. ACLs aren't applied to the trace files and there's no data masking or obfuscation. You should delete these files quickly after use.

- Apply the appropriate ACLs and retention policy to all intermediate and dispatch files that Distributed Replay generates.

- Use Transport Layer Security (TLS) to help secure the network transport.

## Important removal steps

Only use Distributed Replay in a test environment. After testing is complete, and before you use those computers for a different task, make sure that you do the following steps:

- Uninstall the Distributed Replay features and remove the related configuration files from the controller and all clients.

- Delete any trace, intermediate, dispatch, and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database files that were used for testing. The intermediate and dispatch files are stored in the working directory on the controller and client, respectively.

## Related content

- [SQL Server Distributed Replay overview](sql-server-distributed-replay.md)
- [Install Distributed Replay](install-distributed-replay.md)
