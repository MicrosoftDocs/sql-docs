---
title: "Data Collector Security | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "data collection [SQL Server]"
  - "security [data collector]"
  - "data collector [SQL Server], security"
ms.assetid: e75d6975-641e-440a-a642-cb39a583359a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Data Collector Security
  The data collector uses the role-based security model implemented by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. This model lets the database administrator run the various data collector tasks in a security context that has only the permissions required to perform that task. This approach is also used for operations involving internal tables, which can only be accessed by using a stored procedure or view. No permissions are granted to internal tables. Instead, permissions are checked on the user of the stored procedure or view that is used to access a table.  
  
> [!IMPORTANT]  
>  Another key aspect of this security model is concentric permissions. Under concentric permissions, more privileged roles inherit the permissions of less privileged roles on objects (including alerts, operators, jobs, schedules, and proxies). For more information, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 The following sections describe data collection security in general, as well as the roles you must grant to users so they can configure and use the data collector, and carry out tasks associated with the management data warehouse.  
  
## General Security  
 The data collector is installed according to the documented standards specified for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
### Network Security  
 Sensitive information can be passed between target instances, the relational instance associated with the configuration server, the collection sets that are running, and the server that hosts the management data warehouse.  
  
 To protect any data that is transferred over a network, the standard security mechanisms are implemented, such as protocol encryption for [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## Permissions for Configuring and Using the Data Collector  
 Depending on the task, users must be members of one or more of the fixed database roles provided for the data collector. In order of most-privileged to least-privileged access, the roles are as follows:  
  
-   `dc_admin`  
  
-   **dc_operator**  
  
-   **dc_proxy**  
  
 These roles are stored in the msdb database. By default, no user is a member of these database roles. User membership in these roles must be granted explicitly.  
  
 Users who are members of the `sysadmin` fixed server role have full access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent objects and data collector views. However, they need to be explicitly added to data collector roles.  
  
> [!IMPORTANT]  
>  Members of the db_ssisadmin role and the dc_admin role may be able to elevate their privileges to sysadmin. This elevation of privilege can occur because these roles can modify [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages can be executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the sysadmin security context of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. To guard against this elevation of privilege when running maintenance plans, data collection sets, and other [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs that run packages to use a proxy account with limited privileges or only add sysadmin members to the db_ssisadmin and dc_admin roles.  
  
### dc_admin Role  
 Users assigned to the `dc_admin` role have full administrator access (Create, Read, Update, and Delete) to the data collector configuration on a server instance. Members of this role can perform the following operations:  
  
-   Set collector-level properties.  
  
-   Add new collection sets.  
  
-   Install new collection types.  
  
-   Perform all the operations permitted to the **dc_operator** role.  
  
 The `dc_admin` role is a member of the following roles:  
  
-   **SQLAgentUserRole**. This role is required to create schedules and run jobs.  
  
    > [!NOTE]  
    >  Proxies created for the data collector must grant access to `dc_admin` to create them and use them in any job steps that require a proxy.  
  
-   **dc_operator**. Members of `dc_admin` inherit the permissions given to **dc_operator**.  
  
### dc_operator Role  
 Members of the **dc_operator** role have Read and Update access. This role supports operations tasks related to running and configuring collection sets. Members of this role can perform the following operations:  
  
-   Start or stop a collection set.  
  
-   Enumerate existing collection sets.  
  
-   View the detailed information (for example, collection items, and collection frequency) associated with a collection set.  
  
-   Change the upload frequency for existing collection sets.  
  
-   Change the collection frequency for collection items that are part of an existing collection set.  
  
 The **dc_operator** role is a member of the following roles that are required for enumerating and viewing data collector packages:  
  
-   **db_ssisltduser**  
  
-   **db_ssisoperator**  
  
 For more information, see [Integration Services Roles &#40;SSIS Service&#41;](../../integration-services/security/integration-services-roles-ssis-service.md).  
  
### dc_proxy Role  
 Members of the **dc_proxy** role have Read access to data collector collection sets and collector-level properties. Members of this role can also execute jobs that they own and create job steps that run as an existing proxy account.  
  
 Members of this role can perform the following operations:  
  
-   View collection set configuration information (for example, input parameters for collection items, and the collection frequency for these items).  
  
-   Obtain internal encrypted information that can only be accessed by a signed stored procedure (for example, data warehouse connection information used for data uploads).  
  
-   Log collection-set run-time events.  
  
 The **dc_proxy** role is a member of the following roles that are required for enumerating and viewing data collector packages:  
  
-   **db_ssisltduser**.  
  
-   **db_ssisoperator**  
  
 For more information, see [Integration Services Roles &#40;SSIS Service&#41;](../../integration-services/security/integration-services-roles-ssis-service.md).  
  
## Permissions for Configuring and Using the Management Data Warehouse  
 Depending on the task, users must be members of one or more of the fixed database roles provided for accessing the management data warehouse. In order of most-privileged to least-privileged access, the roles are as follows:  
  
-   **mdw_admin**  
  
-   **mdw_writer**  
  
-   **mdw_reader**  
  
 These roles are stored in the msdb database. By default, no user is a member of these database roles. User membership in these roles must be granted explicitly.  
  
 Users who are members of the `sysadmin` fixed server role have full access to data collector views. However, they need to be explicitly added to database roles to perform other operations.  
  
### mdw_admin Role  
 Members of the **mdw_admin** role have Read, Write, Update, and Delete access to the management data warehouse.  
  
 Members of this role can perform the following operations:  
  
-   Change the management data warehouse schema when required (for example, adding a new table when a new collection type is installed).  
  
    > [!NOTE]  
    >  Where there is a schema change, the user must also be a member of the `dc_admin` role to install a new collector type, since this action requires permission to update the data collector configuration in msdb.  
  
-   Run maintenance jobs on the management data warehouse, such as archive or cleanup.  
  
### mdw_writer Role  
 Members of the **mdw_writer** role can upload and write data to the management data warehouse. Any data collector that stores data in the management data warehouse has to be a member of this role.  
  
### mdw_reader Role  
 Members of the **mdw_reader** role have Read access to the management data warehouse. Because the purpose of this role is to support troubleshooting by providing access to historical data, members of this role cannot view other elements of the management data warehouse schema.  
  
## See Also  
 [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md)  
  
  
