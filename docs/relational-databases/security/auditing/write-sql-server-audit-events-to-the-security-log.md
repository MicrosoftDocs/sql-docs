---
title: "Write SQL Server Audit events to the Security log"
description: Learn how to write SQL Server audit events to the Windows Security log. Find out about the limitations and restrictions to using that log.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 03/20/2023
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "logs [SQL Server], Security Log"
  - "server audit [SQL Server]"
  - "audits [SQL Server], writing to Security Log"
  - "security logs [SQL Server]"
---
# Write SQL Server Audit events to the Security log

[!INCLUDE [sql-windows-only](../../../includes/applies-to-version/sql-windows-only.md)]

In a high security environment, the Windows Security log is the appropriate location to write events that record object access. Other audit locations are supported but are more subject to tampering.

There are three key requirements for writing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] server audits to the Windows Security log:

- The audit object access setting must be configured to capture the events. The audit policy tool (`auditpol.exe`) exposes various subpolicies settings in the **audit object access** category. To allow [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to audit object access, configure the **application generated** setting.
- The account that the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is running under must have the **generate security audits** permission to write to the Windows Security log. By default, the LOCAL SERVICE and the NETWORK SERVICE accounts have this permission. This step isn't required if [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running under one of those accounts.
- Provide full permission for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account to the registry hive `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog\Security`.

  > [!IMPORTANT]  
  > [!INCLUDE[ssnoteregistry-md](../../../includes/ssnoteregistry-md.md)]

## Limitations and restrictions

- **Local settings for the Security log can be overwritten by a domain policy.** In this case, the domain policy might overwrite the subcategory setting (`auditpol /get /subcategory:"application generated"`). [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] doesn't have any way to detect that the events it is trying to audit aren't going to be recorded.

- **Events can be lost if the audit policy is incorrectly configured.** The Windows audit policy can affect [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] auditing if it is configured to write to the Windows Security log. Typically, the Windows Security log is set to overwrite the older events. This preserves the most recent events. However, if the Windows Security log isn't set to overwrite older events, then if the Security log is full, the system issues Windows event 1104 (Log is full). At that point:

  - No further security events are recorded

  - [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can't detect that the system isn't able to record the events in the Security log, resulting in the potential loss of audit events

  - After the box administrator fixes the Security log, the logging behavior will return to normal.

- **[!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] audit records contain significantly more data than regular Windows Event log entries.** In addition, depending on the configuration of the audit specification, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] may generate many thousands of audit records in a short period of time (thousands per second). Under periods of high load, this may result in adverse conditions if the audit records are written to either the Application log or the Security log.

  These adverse conditions may include:

  - Rapid cycling of the event log (events are overwritten very quickly as the log file reaches its size limit)

  - Other applications or services that read from the Windows event log may be negatively affected

  - The targeted event log may be unusable by administrators due to events being overwritten so quickly

  Steps that administrators may take to mitigate these adverse conditions:

  1. Increase the size of the target log (4 GB isn't unreasonable when the audit specification is very detailed).

  1. Reduce the number of events being audited.

  1. Output the audit records to a file instead of the Event Logs. 

## Permissions

You must be a Windows administrator to configure these settings.

## <a id="auditpolAccess"></a> Configure the audit object access setting in Windows using `auditpol`

1. Open a command prompt with administrative permissions.

   1. From the **Start** menu, navigate to **Command Prompt**, and then select **Run as administrator**.

   1. If the **User Account Control** dialog box opens, select **Continue**.

1. Execute the following statement to enable auditing from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

   ```cmd
   auditpol /set /subcategory:"application generated" /success:enable /failure:enable
   ```

1. Close the command prompt window.

## <a id="secpolAccess"></a> Grant the generate security audits permission to an account using `secpol`

1. For any Windows operating system, on the **Start** menu, select **Run**.

1. Type `secpol.msc` and then select **OK**. If the **User Access Control** dialog box appears, select **Continue**.

1. In the Local Security Policy tool, navigate to **Security Settings > Local Policies > User Rights Assignment**.

1. In the results pane, open **Generate security audits**.

1. On the **Local Security Setting** tab, select **Add User or Group**.

1. In the **Select Users, Computers, or Groups** dialog box, either type the name of the user account, such as **domain1\user1** and then select **OK**, or select **Advanced** and search for the account.

1. Select **OK**.

1. Close the Security Policy tool.

1. Restart [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to enable this setting.

## <a id="secpolPermission"></a> Configure the audit object access setting in Windows using `secpol`

1. If the operating system is earlier than [!INCLUDE[winvista](../../../includes/winvista-md.md)] or Windows Server 2008, on the **Start** menu, select **Run**.

1. Type `secpol.msc` and then select **OK**. If the **User Access Control** dialog box appears, select **Continue**.

1. In the Local Security Policy tool, navigate to **Security Settings > Local Policies > Audit Policy**.

1. In the results pane, open **Audit object access**.

1. On the **Local Security Setting** tab, in the **Audit these attempts** area, select both **Success** and **Failure**.

1. Select **OK**.

1. Close the Security Policy tool.

## See also

- [SQL Server Audit (Database Engine)](../../../relational-databases/security/auditing/sql-server-audit-database-engine.md)
