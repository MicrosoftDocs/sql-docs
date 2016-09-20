---
title: "Understanding the Security Model of the HDInsight Region (Analytics Platform System)"
ms.custom: na
ms.date: 08/09/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: edd516a3-4e0e-4a5f-8c93-2a421b4e33c9
caps.latest.revision: 15
author: BarbKess
---
# Understanding the Security Model of the HDInsight Region (Analytics Platform System)
The security structure of the HDInsight region of MicrosoftAnalytics Platform System is separate from the SQL Server PDW region of the Analytics Platform System. External access to the HDInsight region is controlled by the Secure Node Gateway which uses the internal Active Directory for credential and role membership checks. The Secure Gateway requires HTTP Basic Authentication. Credentials are included in the every request as part of HTTP authorization header. The appliance Active Directory is used to store user credentials, control user membership, and provide granular control of the features that are available for each security group (role).  
  
The top level user of the HDInsight region is the **Administrator** user of the Analytics Platform Systemappliance domain (***appliance_domain*\Administrator**). The **Administrator** user is created during the initial appliance deployment.  
  
Within the HDInsight appliance, multiple cluster administrator accounts can be created as HDInsight users and multiple Information Worker accounts can be created as HDInsight users.  
  
## HDInsight Security Model Overview  
The HDInsight region supports the authentication of multiple user accounts, but once authenticated, all Hadoop services and data access is made using the security context of system accounts provided by HDInsight. As a result, access to data and processing done by Map Reduce jobs, are not isolated from separate users. That is, jobs initiated by different users run under the same service account and have same privileges over the data.  
  
HDP services run under Managed Service Accounts (MSA's) which are the optimal choice from a security point of view, in a distributed environment such as HDInsight cluster. MSA's enable automatic password management (passwords cannot be accessed or managed by human users), offer protection from interactive login, and simplified SPN management. For more information about MSA's, see [Group Managed Service Accounts Overview](http://technet.microsoft.com/en-us/library/hh831782.aspx).  
  
Information Workers cannot remotely connect to the cluster (using Remote Desktop). Information Workers can access the system by using the Developer Dashboard, or by using the REST APIs that are exposed for various purposes. Those APIs can be invoked either directly (through common HTTP client tools such as wget, PostMan, HTTPie) or from applications that users develop on top of them.  
  
There are three levels of privileges in HDI region which enables having different user profiles/roles optimized for subset of scenarios.  
  
HDInsight region admin  
A Windows login from the appliance **Domain Administrators** group. This is the single ***appliance_domain*\Administrator** account.  
  
HDInsight cluster admin  
A Power User, member of the HDInsight Cluster Admins group.  
  
HDInsight regular user  
A user account created by using the HDInsight Topology, **User Management** page of the **DW Configuration Manager**. These accounts are used by developers and information workers. Example: **Melisa**.  
  
For information about creating users, see [HDInsight User Management &#40;Analytics Platform System&#41;](../../mpp/management/hdinsight-user-management-analytics-platform-system.md).  
  
## Duties and Functions of the HDInsight Administrator Account  
The top level user of the HDInsight region is the **Administrator** user of the appliance domain (***appliance_domain*\Administrator**). The **Administrator** account is a member of  the **Domain Admins** group of the appliance domain Active Directory.  The **Administrator** user is created during the initial appliance deployment. The **Administrator** password can be changed using the **DWConfig** program, but the account cannot be renamed. Creating additional accounts which are member of Domain Admins group is not recommended.  
  
The **Administrator** account has administrator level privileges on the HDInsight region organizational unit and all virtual machines.  
  
In addition to having all the permissions of the HDInsight Cluster Admin, the **Administrator** account is used to impersonate access from **Admin Console** to HDI region. The **Administrator** account can be used also to:  
  
-   Connect remotely to the physical host computers.  
  
-   Connect remotely to the PDW region VM's (CTL01, etc.)  
  
## Duties and Functions of the HDInsight Cluster Administrator Accounts  
The Cluster admin account uses a remote desktop connection to access cluster nodes to manage, configure, and troubleshoot Hadoop components (name node, resource manager, data node, node manager and others).  
  
Connecting by using remote desktop is a two-step operation: [Connecting to HDInsight Using Remote Desktop &#40;Analytics Platform System&#41;](../../mpp/hdinsight/connecting-to-hdinsight-using-remote-desktop-analytics-platform-system.md)  
  
The **DW Configuration Manager** can change the password for the domain admin account. For all other users, the password is changed on the **Change password** page of **Developer Dashboard** which is available during login.  
  
In addition to having all the permissions of the HDInsight users, the HDInsight cluster administrators are members of the administrators group of the virtual machines, and can:  
  
-   Connect by using remote desktop to the dedicated cluster nodes (virtual machines)  
  
-   Collect and analyze HDP logs from cluster nodes to troubleshoot system issues  
  
-   Use HDP tools to perform advanced administrative tasks which are not exposed directly by appliance tools. Examples include: Hadoop commands (fs, fsck, balancer, dfsadmin, jar, job), sqoop, and pig consoles.  
  
-   Change whitelisted settings in Hadoop configuration files to adjust system to typical customer workloads. For more information, see [Changing Hadoop Configuration Settings &#40;Analytic Platform System&#41;](../../mpp/hdinsight/changing-hadoop-configuration-settings-analytic-platform-system.md).  
  
-   Install and uninstall additional Hadoop modules (such as encryption and serialization libraries)  
  
-   Create **RunAs** accounts to enable authentication of HDInsight or SCOM MP  
  
## Duties and Functions of the HDInsight User Accounts  
Use the HDInsight User Management page of the **DW Configuration Manager** to create low privileged user accounts for information workers and developers.  
  
User accounts can:  
  
-   Access HDInsight services through the Gateway.  
  
-   Use the **Developer Dashboard** to access Hive and to upload and download files.  
  
-   Use the **Change password** page of the **Developer Dashboard** to change their own passwords. To access the **Change password**, on the **Developer Dashboard** log in page, click **Change password**.  
  
For more information about creating users, see [HDInsight User Management &#40;Analytics Platform System&#41;](../../mpp/management/hdinsight-user-management-analytics-platform-system.md).  
  
## Access to HDInsight Data  
Hortonworks for Windows does not support user isolation therefore it’s not possible to run a job under security context of the user. In APS all HDInsight data access is performed by the Hadoop service account which has permission to read and alter any data in the HDInsight cluster. Therefore it is not possible to restrict an APS HDInsight user to only a limited subset of the data. One workaround for this problem is to access HDInsight data by using PolyBase queries that query HDInsight through a remote external table. You can use standard SQL Server permissions to control access to the remote external table.  
  
## Virtual Machines  
The HDInsight Cluster Admins have administrator level privileges on HDInsight region virtual machines.  
  
Remote desktop access by information workers to the new clusters is disabled by default. Enabling remote desktop access to non-admin users is not supported, necessary, or recommended.  
  
## Secure Perimeter  
Access to HDInsight services is routed and secured through additional (non-HDP) components: Secure Node (HTTP gateway) and **Developer Dashboard**.  
  
Secure node performs authentication and serves as a proxy for exposed set of Hadoop services:  
  
-   WebHDFS for file manipulation (smaller files are targeted this way)  
  
-   WebHCat (Templeton) for remote job submission and management  
  
-   Oozie (for scheduling and executing complex workflows)  
  
-   Hive2 server (for accessing HDInsight through the Hive ODBC driver)  
  
The **Developer Dashboard** serves as an endpoint to Hive, job submission, and as a file upload/download utility through the WebHDFS. Access to **Developer Dashboard** requires the same credentials as access through Secure Node Gateway.  
  
Secure Node Gateway communication only supports HTTPS.  
  
## Additional Information  
For information about the system architecture, see the **Microsoft Analytics Platform System** section of [Product Documentation &#40;Analytics Platform System&#41;](../../mpp/product-documentation-analytics-platform-system.md).  
  
For information about SQL Server PDW authentication, see [Security - Windows Authentication and SQL Server Authentication &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/security-windows-authentication-and-sql-server-authentication-sql-server-pdw.md).  
  
For information about SQL Server PDW Active Directory trusts, see [Security - Configure Domain Trusts &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/security-configure-domain-trusts-sql-server-pdw.md).  
  
