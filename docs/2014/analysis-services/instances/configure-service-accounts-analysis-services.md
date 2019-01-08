---
title: "Configure Service Accounts (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "security [Analysis Services], logon accounts"
  - "logon accounts [Analysis Services]"
  - "accounts [Analysis Services]"
  - "logon accounts [Analysis Services], about logon accounts"
ms.assetid: b481bd51-e077-42f6-8598-ce08c1a38716
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Service Accounts (Analysis Services)
  Product-wide account provisioning is documented in [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md), a topic that provides comprehensive service account information for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, including [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Refer to it to learn about valid account types, Windows privileges assigned by setup, file system permissions, registry permissions, and more.  
  
 This topic provides supplemental information for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], including additional permissions necessary for tabular and clustered installations. It also covers permissions needed to support server operations. For example, you can configure processing and query operations to execute under the service account ─ in which case you will need to grant additional permissions to get this to work.  
  
-   [Windows privileges assigned to Analysis Services](#bkmk_winpriv)  
  
-   [File System Permissions assigned to Analysis Services](#bkmk_FilePermissions)  
  
-   [Granting additional permissions for specific server operations](#bkmk_tasks)  
  
 An additional configuration step, not documented here, is to register a Service Principal Name (SPN) for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and service account. This step enables pass-through authentication from client applications to backend data sources in double-hop scenarios. This step only applies for services configured for Kerberos constrained delegation. See [Configure Analysis Services for Kerberos constrained delegation](configure-analysis-services-for-kerberos-constrained-delegation.md) for further instructions.  
  
## Logon account recommendations  
 In a failover cluster, all instances of Analysis Services should be configured to use a Windows domain user account. Assign the same account to all instances. See [How to Cluster Analysis Services](https://msdn.microsoft.com/library/dn736073.aspx) for details.  
  
 Standalone instances should use the default virtual account, **NT Service\MSSQLServerOLAPService** for the default instance, or **NT Service\MSOLAP$**_instance-name_ for a named instance. This recommendation applies to Analysis Services instances in all server modes, assuming Windows Server 2008 R2 and later for the operating system, and SQL Server 2012 and later for Analysis Services.  
  
## Granting permissions to Analysis Services  
 This section explains the permissions that Analysis Services requires for local, internal operations, such as starting the executable, reading the configuration file, and loading databases from the data directory. If instead you're looking for guidance on setting permissions for external data access and interoperability with other services and applications, see [Granting additional permissions for specific server operations](#bkmk_tasks) further on in this topic.  
  
 For internal operations, the permission holder in Analysis Services is not the logon account, but a local Windows security group created by Setup that contains the per-service SID. Assigning permissions to the security group is consistent with previous versions of Analysis Services. Also, logon accounts can change over time, but the per-service SID and local security group are constant for the lifetime of the server installation. For Analysis Services, this makes the security group, rather than the logon account, a better choice for holding permissions. Whenever you manually grant rights to the service instance, whether file system permissions or Windows privileges, be sure to grant permissions to the local security group created for the server instance.  
  
 The name of the security group follows a pattern. The prefix is always `SQLServerMSASUser$`, followed by the computer name, ending with the instance name. The default instance is `MSSQLSERVER`. A named instance is the name given during set up.  
  
 You can see this security group in the local security settings:  
  
-   Run compmgmt.msc | **Local Users and Groups** | **Groups** | `SQLServerMSASUser$`\<server-name>`$MSSQLSERVER` (for a default instance).  
  
-   Double-click the security group to view its members.  
  
 The sole member of the group is the per-service SID. Right next to it is the logon account. The logon account name is cosmetic, there to provide context to the per-service SID. If you subsequently change the logon account and then return to this page, you'll notice that the security group and per-service SID do not change, but the logon account label is different.  
  
##  <a name="bkmk_winpriv"></a> Windows privileges assigned to the Analysis Services service account  
 Analysis Services needs permissions from the operating system for service startup and to request system resources. Requirements vary by server mode and whether the instance is clustered. If you are unfamiliar with Windows privileges, see [Privileges](https://msdn.microsoft.com/library/windows/desktop/aa379306\(v=vs.85\).aspx) and [Privilege Constants (Windows)](/windows/desktop/SecAuthZ/privilege-constants) for details.  
  
 All instances of Analysis Services require the **Log on as a service** (SeServiceLogonRight) privilege. SQL Server Setup assigns the privilege for you on the service account specified during installation. For servers running in Multidimensional and Data Mining mode, this is the only Windows privilege required by the Analysis Services service account for standalone server installations, and it is the only privilege that Setup configures for Analysis Services. For clustered and tabular instances, additional Windows privileges must be added manually.  
  
 Failover cluster instances, in either Tabular or Multidimensional mode, must have **Increase scheduling priority** (SeIncreaseBasePriorityPrivilege).  
  
 Tabular instances use the following three additional privileges, which must be granted manually after the instance is installed.  
  
|||  
|-|-|  
|**Increase a process working set** (SeIncreaseWorkingSetPrivilege)|This privilege is available to all users by default through the **Users** security group. If you lock down a server by removing privileges for this group, Analysis Services might fail to start, logging this error: "A required privilege is not held by the client." When this error occurs, restore the privilege to Analysis Services by granting it to the appropriate Analysis Services security group.|  
|**Adjust memory quotas for a process** (SeIncreaseQuotaSizePrivilege)|This privilege is used to request more memory if a process has insufficient resources to complete its execution, subject to the memory thresholds established for the instance.|  
|**Lock pages in memory** (SeLockMemoryPrivilege)|This privilege is needed only when paging is turned off entirely. By default, a tabular server instance uses the Windows paging file, but you can prevent it from using Windows paging by setting `VertiPaqPagingPolicy` to 0.<br /><br /> `VertiPaqPagingPolicy` to 1 (default), instructs the tabular server instance to use the Windows paging file. Allocations are not locked, allowing Windows to page out as needed. Because paging is being used, there is no need to lock pages in memory. Thus, for the default configuration (where `VertiPaqPagingPolicy` = 1), you do not need to grant the **Lock pages in memory** privilege to a tabular instance.<br /><br /> `VertiPaqPagingPolicy` to 0. If you turn off paging for Analysis Services, allocations are locked, assuming the **Lock pages in memory** privilege is granted to the tabular instance. Given this setting and the **Lock pages in memory** privilege, Windows cannot page out memory allocations made to Analysis Services when the system is under memory pressure. Analysis Services relies on the **Lock pages in memory** permission as the enforcement behind `VertiPaqPagingPolicy` = 0. Note that turning off Windows paging is not recommended. It will increase the rate of out-of-memory errors for operations that might otherwise succeed if paging were allowed. See [Memory Properties](../server-properties/memory-properties.md) for more information about `VertiPaqPagingPolicy`.|  
  
#### To view or add Windows privileges on the service account  
  
1.  Run GPEDIT.msc | Local Computer Policy | Computer Configuration | Windows Settings | Security Settings | Local Policies | User Rights Assignments.  
  
2.  Review existing policies that include `SQLServerMSASUser$`. This is a local security group found on computers having an Analysis Services installation. Both Windows privileges and file folder permissions are granted to this security group. Double-click **Log on as a service** policy to see how the security group is specified on your system. The full name of the security group will vary depending on whether you installed Analysis Services as a named instance. Use this security group, rather than the actual service account, when adding account privileges.  
  
3.  To add account privileges in GPEDIT, right-click **Increase a process working set** and select **Properties**.  
  
4.  Click **Add User or Group**.  
  
5.  Enter the user group for the Analysis Services instance. Remember that the service account is a member of a local security group, requiring that you prepend the local computer name as the domain of the account.  
  
     The following list shows two examples for a default instance and named instance called "Tabular" on a machine called "SQL01-WIN12", where the machine name is the local domain.  
  
    -   SQL01-WIN12\SQL01-WIN12$SQLServerMSASUser$MSSQLSERVER  
  
    -   SQL01-WIN12\SQL01-WIN12$SQLServerMSASUser$TABULAR  
  
6.  Repeat for **Adjust memory quotas for a process**, and optionally, for **Lock pages in memory** or **Increase scheduling priority**.  
  
> [!NOTE]  
>  Previous versions of Setup inadvertently added the Analysis Services service account to the **Performance Log Users** group. Although this defect has been fixed, existing installations might have this unnecessary group membership. Because the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service account does not require membership in the **Performance Log Users** group, you can remove it from the group.  
  
##  <a name="bkmk_FilePermissions"></a> File System Permissions assigned to the Analysis Services service account  
  
> [!NOTE]  
>  See [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md) for a list of permissions associated with each program folder.  
>   
>  See [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](configure-http-access-to-analysis-services-on-iis-8-0.md) for file permission information related to IIS configuration and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 All file system permissions required for server operations─ including permissions needed for loading and unloading databases from a designated data folder─ are assigned by SQL Server Setup during installation.  
  
 The permission holder on data files, program file executables, configuration files, log files, and temporary files is a local security group created by SQL Server Setup.  
  
 There is one security group created for each instance that you install. The security group is named after the instance ─ either **SQLServerMSASUser$MSSQLSERVER** for the default instance, or `SQLServerMSASUser$`\<servername>$\<instancename> for a named instance. Setup provisions this security group with the file permissions required to perform server operations. If you check the security permissions on the \MSAS12.MSSQLSERVER\OLAP\BIN directory, you will see that the security group (not the service account or its per-service SID) is the permission holder on that directory.  
  
 The security group contains one member only: the per-service Security Identifier (SID) of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance startup account. Setup adds the per-service SID to the local security group. The use of a local security group, with its SID membership, is a small but noticeable difference in how SQL Server Setup provisions Analysis Services, as compared to the Database Engine.  
  
 If you believe that file permissions are corrupted, follow these steps to verify the service is still correctly provisioned:  
  
1.  Use the Service Control command line tool (sc.exe) to obtain the SID of a default service instance.  
  
     `SC showsid MSSqlServerOlapService`  
  
     For a named instance (where the instance name is Tabular), use this syntax:  
  
     `SC showsid MSOlap$Tabular`  
  
2.  Use **Computer Manager** | **Local Users and Groups** | **Groups** to inspect the membership of the SQLServerMSASUser$\<servername>$\<instancename> security group.  
  
     The member SID should match the per-service SID from step 1.  
  
3.  Use **Windows Explorer** | **Program Files** | **Microsoft SQL Server** | MSASxx.MSSQLServer | **OLAP** | **bin** to verify folder Security properties are granted to the security group in step 2.  
  
> [!NOTE]  
>  Never remove or modify a SID. To restore a per-service SID that was inadvertently deleted, see [https://support.microsoft.com/kb/2620201](https://support.microsoft.com/kb/2620201).  
  
 **More about per-service SIDs**  
  
 Every Windows account has an associated [SID](http://en.wikipedia.org/wiki/Security_Identifier), but services can also have SIDs, hence referred to as per-service SIDs. A per-service SID is created when the service instance is installed, as a unique, permanent fixture of the service. The per-service SID is a local, machine-level SID generated from the service name. On a default instance, its user friendly name is NT SERVICE\MSSQLServerOLAPService.  
  
 The benefit of a per-service SID is that it allows the more widely-visible logon account to be changed arbitrarily, without affecting file permissions. For example, suppose you installed two instances of Analysis Services, a default instance and named instance, both running under the same Windows user account. While the logon account is shared, each service instance will have a unique per-service SID. This SID is distinct from the SID of the logon account. The per-service SID is used for file permissions and Windows privileges. In contrast, the logon account SID is used for authentication and authorization scenarios ─ different SIDS, used for different purposes.  
  
 Because the SID is immutable, file system ACLs created during service installation can be used indefinitely, regardless of how often you change the service account. As an added security measure, ACLs that specify permissions via a SID ensure that program executables and data folders are accessed only by a single instance of a service, even if other services run under the same account.  
  
##  <a name="bkmk_tasks"></a> Granting additional Analysis Services permissions for specific server operations  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] executes some tasks in the security context of the service account (or logon account) that is used to start [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and executes other tasks in the security context of the user who is requesting the task.  
  
 The following table describes additional permissions required to support tasks executing as the service account.  
  
|Server Operation|Work Item|Justification|  
|----------------------|---------------|-------------------|  
|Remote access to external relational data sources|Create a database login for the service account|Processing refers to data retrieval from an external data source (usually a relational database), which is subsequently loaded into an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. One of the credential options for retrieving external data is to use the service account. This credential option works only if you create a database login for the service account and grant read permissions on the source database. See [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](../multidimensional-models/set-impersonation-options-ssas-multidimensional.md) for more information about how the service account option is used for this task. Similarly, if ROLAP is used as the storage mode, the same impersonation options are available. In this case, the account must also have write access to the source data to process the ROLAP partitions (that is, to store aggregations).|  
|DirectQuery|Create a database login for the service account|DirectQuery is a tabular feature used to query external datasets that are either too large to fit inside the tabular model or have other characteristics that make DirectQuery a better fit than the default in-memory storage option. One of the connection options available in DirectQuery mode is to use the service account. Once again, this option works only when the service account has a database login and read permissions on the target data source. See [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](../multidimensional-models/set-impersonation-options-ssas-multidimensional.md) for more information about how the service account option is used for this task. Alternatively, the credentials of the current user can be used to retrieve data. In most cases this option entails a double-hop connection, so be sure to configure the service account for Kerberos constrained delegation so that the service account can delegate identities to a downstream server. For more information, see [Configure Analysis Services for Kerberos constrained delegation](configure-analysis-services-for-kerberos-constrained-delegation.md).|  
|Remote access to other SSAS instances|Add the service account to Analysis Services database roles defined on the remote server|Remote partitions and referencing linked objects on other remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances are both system capabilities requiring permissions on a remote computer or device. When a person creates and populates remote partitions, or sets up a linked object, that operation runs in the security context of the current user. If you subsequently automate these operations, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will access remote instances in the security context of its service account. In order to access linked objects on a remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the logon account must have permission to read the appropriate objects on the remote instance, such as Read access to certain dimensions. Similarly, using remote partitions requires that the service account have administrative rights on the remote instance. Such permissions are granted on the remote Analysis Services instance, using roles that associate permitted operations with a specific object. See [Grant database permissions &#40;Analysis Services&#41;](../multidimensional-models/grant-database-permissions-analysis-services.md) for instructions on how to grant Full Control permissions that allow processing and query operations. See [Create and Manage a Remote Partition &#40;Analysis Services&#41;](../multidimensional-models/create-and-manage-a-remote-partition-analysis-services.md) for more information about remote partitions.|  
|Writeback|Add the service account to Analysis Services database roles defined on the remote server|When enabled in client applications, writeback is a feature of multidimensional models that allows the creation of new data values during data analysis. If writeback is enabled within any dimension or cube, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service account must have write permissions to the writeback table in the source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database. If this table does not already exist and needs to be created, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service account must also have create table permissions within the designated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|Write to a query log table in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database|Create a database login for the service account and assign write permissions on the query log table|You can enable query logging to collect usage data in a database table for subsequent analysis. The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service account must have write permissions to the query log table in the designated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. If this table does not already exist and needs to be created, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] logon account must also have create table permissions within the designated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. For more information, see [Improve SQL Server Analysis Services Performance with the Usage Based Optimization Wizard (Blog)](http://www.mssqltips.com/sqlservertip/2876/improve-sql-server-analysis-services-performance-with-the-usage-based-optimization-wizard/) and [Query Logging in Analysis Services (Blog)](http://weblogs.asp.net/miked/archive/2013/07/31/query-logging-in-analysis-services.aspx).|  
  
## See Also  
 [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)   
 [SQL Server Service Account and Per-Service SID (Blog)](http://www.travisgan.com/2013/06/sql-server-service-account-and-per.html)   
 [SQL Server uses a service SID to provide service isolation (KB Article)](https://support.microsoft.com/kb/2620201)   
 [Access Token (MSDN)](/windows/desktop/SecAuthZ/access-tokens)   
 [Security Identifiers (MSDN)](/windows/desktop/SecAuthZ/security-identifiers)   
 [Access Token (Wikipedia)](http://en.wikipedia.org/wiki/Access_token)   
 [Access Control Lists (Wikipedia)](http://en.wikipedia.org/wiki/Access_control_list)  
  
  
