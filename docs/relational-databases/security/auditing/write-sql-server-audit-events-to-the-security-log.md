---
title: "Write SQL Server Audit Events to the Security Log | Microsoft Docs"
ms.custom: ""
ms.date: "09/21/2017"
ms.prod: sql
ms.prod_service: security
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], Security Log"
  - "server audit [SQL Server]"
  - "audits [SQL Server], writing to Security Log"
  - "security logs [SQL Server]"
ms.assetid: 6fabeea3-7a42-4769-a0f3-7e04daada314
author: VanMSFT
ms.author: vanto
manager: craigg
---  
# Write SQL Server Audit Events to the Security Log  
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In a high security environment, the Windows Security log is the appropriate location to write events that record object access. Other audit locations are supported but are more subject to tampering.  
  
 There are two key requirements for writing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] server audits to the Windows Security log:  
  
-   The audit object access setting must be configured to capture the events. The audit policy tool (`auditpol.exe`) exposes a variety of sub-policies settings in the **audit object access** category. To allow [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to audit object access, configure the **application generated** setting.  
-   The account that the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is running under must have the **generate security audits** permission to write to the Windows Security log. By default, the LOCAL SERVICE and the NETWORK SERVICE accounts have this permission. This step is not required if [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running under one of those accounts.  
-   Provide full permission for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account to the registry hive `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog\Security`.  

  > [!IMPORTANT]  
  > [!INCLUDE[ssnoteregistry-md](../../../includes/ssnoteregistry-md.md)]   
  
The Windows audit policy can affect [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] auditing if it is configured to write to the Windows Security log, with the potential of losing events if the audit policy is incorrectly configured. Typically, the Windows Security log is set to overwrite the older events. This preserves the most recent events. However, if the Windows Security log is not set to overwrite older events, then if the Security log is full, the system will issue Windows event 1104 (Log is full). At that point:  
-   No further security events will be recorded  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will not be able to detect that the system is not able to record the events in the Security log, resulting in the potential loss of audit events  
-   After the box administrator fixes the Security log, the logging behavior will return to normal.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Administrators of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] computer should understand that local settings for the Security log can be overwritten by a domain policy. In this case, the domain policy might overwrite the subcategory setting (**auditpol /get /subcategory:"application generated"**). This can affect [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] ability to log events without having any way to detect that the events that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is trying to audit are not going to be recorded.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must be a Windows administrator to configure these settings.  
  
##  <a name="auditpolAccess"></a> To configure the audit object access setting in Windows using auditpol  
  
1.  Open a command prompt with administrative permissions.  
  
    1.  On the **Start** menu, point to **All Programs**, point to **Accessories**, right-click **Command Prompt**, and then click **Run as administrator**.  
  
    2.  If the **User Account Control** dialog box opens, click **Continue**.  
  
2.  Execute the following statement to enable auditing from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
    ```  
    auditpol /set /subcategory:"application generated" /success:enable /failure:enable  
    ```  
  
3.  Close the command prompt window.  
  
##  <a name="secpolAccess"></a> To grant the generate security audits permission to an account using secpol  
  
1.  For any Windows operating system, on the **Start** menu, click **Run**.  
  
2.  Type **secpol.msc** and then click **OK**. If the **User Access Control** dialog box appears, click **Continue**.  
  
3.  In the Local Security Policy tool, expand **Security Settings**, expand **Local Policies**, and then click **User Rights Assignment**.  
  
4.  In the results pane, double-click **Generate security audits**.  
  
5.  On the **Local Security Setting** tab, click **Add User or Group**.  
  
6.  In the **Select Users, Computers, or Groups** dialog box, either type the name of the user account, such as **domain1\user1** and then click **OK**, or click **Advanced** and search for the account.  
  
7.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
8.  Close the Security Policy tool.  
  
9. Restart [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to enable this setting.  
  
##  <a name="secpolPermission"></a> To configure the audit object access setting in Windows using secpol  
  
1.  If the operating system is earlier than [!INCLUDE[wiprlhext](../../../includes/wiprlhext-md.md)] or Windows Server 2008, on the **Start** menu, click **Run**.  
  
2.  Type **secpol.msc** and then click **OK**. If the **User Access Control** dialog box appears, click **Continue**.  
  
3.  In the Local Security Policy tool, expand **Security Settings**, expand **Local Policies**, and then click **Audit Policy**.  
  
4.  In the results pane, double-click **Audit object access**.  
  
5.  On the **Local Security Setting** tab, in the **Audit these attempts** area, select both **Success** and **Failure**.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
7.  Close the Security Policy tool.  
  
## See Also  
 [SQL Server Audit &#40;Database Engine&#41;](../../../relational-databases/security/auditing/sql-server-audit-database-engine.md)  
  
  
