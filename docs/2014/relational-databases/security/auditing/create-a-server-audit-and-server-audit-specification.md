---
title: "Create a Server Audit and Server Audit Specification | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "SQL12.SWB.SQLAUDIT.FILTER.F1"
  - "sql12.swb.sqlaudit.srvaudit.general.f1"
  - "sql12.swb.sqlaudit.general.f1"
helpviewer_keywords: 
  - "server audit [SQL Server]"
  - "audits [SQL Server], specification"
ms.assetid: 6624b1ab-7ec8-44ce-8292-397edf644394
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Create a Server Audit and Server Audit Specification
  This topic describes how to create a server audit and server audit specification in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. *Auditing* an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database involves tracking and logging events that occur on the system. The *SQL Server Audit* object collects a single instance of server- or database-level actions and groups of actions to monitor. The audit is at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance level. You can have multiple audits per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The *Server Audit Specification* object belongs to an audit. You can create one server audit specification per audit, because both are created at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance scope. For more information, see [SQL Server Audit &#40;Database Engine&#41;](sql-server-audit-database-engine.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a server audit and server audit specification, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   An audit must exist before creating a server audit specification for it. When a server audit specification is created, it is in a disabled state.  
  
-   The CREATE SERVER AUDIT statement is in a transaction's scope. If the transaction is rolled back, the statement is also rolled back.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   To create, alter, or drop a server audit, principals require the ALTER ANY SERVER AUDIT or the CONTROL SERVER permission.  
  
-   Users with the ALTER ANY SERVER AUDIT permission can create server audit specifications and bind them to any audit.  
  
-   After a server audit specification is created, it can be viewed by principals with the CONTROL SERVER or ALTER ANY SERVER AUDIT permissions, the sysadmin account, or principals having explicit access to the audit.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a server audit  
  
1.  In Object Explorer, expand the **Security** folder.  
  
2.  Right-click the **Audits** folder and select **New Audit...**.  
  
     The following options are available on the **General** page of the **Create Audit** dialog box.  
  
     **Audit name**  
     The name of the audit. This is generated automatically when you create a new audit but is editable.  
  
     **Queue delay (in milliseconds)**  
     Specifies the amount of time in milliseconds that can elapse before audit actions are forced to be processed.  A value of 0 indicates synchronous delivery. The default minimum value is **1000** (1 second). The maximum is 2,147,483,647 (2,147,483.647 seconds or 24 days, 20 hours, 31 minutes, 23.647 seconds).  
  
     **On Audit Log Failure:**  
     **Continue**  
     [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] operations continue. Audit records are not retained. The audit continues to attempt to log events and will resume if the failure condition is resolved. Selecting the **Continue** option can allow unaudited activity which could violate your security policies. Select this option when continuing operation of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] is more important than maintaining a complete audit. This is the default selection.  
  
     **Shut down server**  
     Forces a server shut down when the server instance writing to the target cannot write data to the audit target. The login issuing this must have the `SHUTDOWN` permission. If the logon does not have this permission, this function will fail and an error message will be raised. No audited events occur. Select this option when an audit failure could compromise the security or integrity of the system.  
  
     **Fail operation**  
     In cases where the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit cannot write to the audit log this option causes database actions to fail if they would otherwise cause audited events. No audited events occur. Actions which do not cause audited events can continue. The audit continues to attempt to log events and will resume if the failure condition is resolved. Select this option when maintaining a complete audit is more important than full access to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
    > [!IMPORTANT]  
    >  When the audit is in a failed state, the Dedicated Administrator Connection can continue to perform audited events.  
  
     **Audit destination** list  
     Specifies the target for auditing data. The available options are a binary file, the Windows Application log, or the Windows Security log. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cannot write to the Windows Security log without configuring additional settings in Windows. For more information, see [Write SQL Server Audit Events to the Security Log](write-sql-server-audit-events-to-the-security-log.md).  
  
     **File path**  
     Specifies the location of the folder where audit data is written when the **Audit destination** is a file.  
  
     **Ellipsis (...)**  
     Opens the **Locate Folder -**_server_name_ dialog box to specify a file path or create a folder where the audit file is written.  
  
     **Audit File Maximum Limit:**  
     **Maximum rollover files**  
     Specifies that, when the maximum number of audit files is reached, the oldest audit files are overwritten by new file content.  
  
     **Maximum files**  
     Specifies that, when the maximum number of audit files is reached, any action that causes additional audit events to be generated will fail with an error.  
  
     **Unlimited** check box  
     When the **Unlimited** check box under **Maximum rollover files** is selected, there is no limit imposed on the number of audit files that will be created. The **Unlimited** check box is selected by default and applies to both the **Maximum rollover files** and **Maximum files** selections.  
  
     **Number of files** box  
     Specifies the number of audit files to be created, up to 2,147,483,647. This option is only available if **Unlimited** is unchecked.  
  
     **Maximum file size**  
     Specifies the maximum size for an audit file in either megabytes (MB), gigabytes (GB), or terabytes (TB). You can specify between 1024 MB and 2,147,483,647 TB. Selecting the **Unlimited** check box does not place a limit on the size of the file. Specifying a value lower than 1024 MB will fail, returning an error. The **Unlimited** check box is selected by default.  
  
     **Reserve disk space** check box  
     Specifies that space is pre-allocated on the disk equal to the specified maximum file size. This setting can only be used if the **Unlimited** check box under **Maximum file size** is not selected. This check box is not selected by default.  
  
3.  Optionally, on the **Filter** page, enter a predicate, or `WHERE` clause, to the server audit to specify additional options not available from the **General** page. Enclose the predicate in parentheses; for example: `(object_name = 'EmployeesTable')`.  
  
4.  When you are finished selecting options, click **OK**.  
  
#### To create a server audit specification  
  
1.  In Object Explorer, click the plus sign to expand the **Security** folder.  
  
2.  Right-click the **Server Audit Specifications** folder and select **New Server Audit Specification...**.  
  
     The following options are available on the **Create Server Audit Specification** dialog box.  
  
     **Name**  
     The name of the server audit specification. This is generated automatically when you create a new server audit specification but is editable.  
  
     **Audit**  
     The name of an existing server audit. Either type in the name of the audit or select it from the list.  
  
     **Audit Action Type**  
     Specifies the server-level audit action groups and audit actions to capture. For the list of server-level audit action groups and audit actions and a description of the events they contain, see [SQL Server Audit Action Groups and Actions](sql-server-audit-action-groups-and-actions.md).  
  
     **Object Schema**  
     Displays the schema for the specified **Object Name**.  
  
     **Object Name**  
     The name of the object to audit. This is only available for audit actions; it does not apply to audit groups.  
  
     **Ellipsis (...)**  
     Opens the **Select Objects** dialog to browse for and select an available object, based on the specified **Audit Action Type**.  
  
     **Principal Name**  
     The account to filter the audit by for the object being audited.  
  
     **Ellipsis (...)**  
     Opens the **Select Objects** dialog to browse for and select an available object, based on the specified **Object Name**.  
  
3.  When you are finished, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a server audit  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- Creates a server audit called "HIPAA_Audit" with a binary file as the target and no options.  
    CREATE SERVER AUDIT HIPAA_Audit  
        TO FILE ( FILEPATH ='\\SQLPROD_1\Audit\' );  
    ```  
  
#### To create a server audit specification  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    /*Creates a server audit specification called "HIPAA_Audit_Specification" that audits failed logins for the SQL Server audit "HIPAA_Audit" created above.  
    */  
  
    CREATE SERVER AUDIT SPECIFICATION HIPAA_Audit_Specification  
    FOR SERVER AUDIT HIPAA_Audit  
        ADD (FAILED_LOGIN_GROUP);  
    GO  
    -- Enables the audit.   
  
    ALTER SERVER AUDIT HIPAA_Audit  
    WITH (STATE = ON);  
    GO  
    ```  
  
 For more information, see [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-server-audit-transact-sql) and [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-server-audit-specification-transact-sql).  
  
  
