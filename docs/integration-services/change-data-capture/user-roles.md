---
title: "User Roles | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: be0ec384-e03b-4483-96ca-02b289804d6a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# User Roles
  This section describes the user roles for the Change Data Capture Service for Oracle by Attunity. The roles described are [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database roles, Windows roles, or Oracle database roles.  
  
## Windows User Roles  
 The following describes the Windows user roles used by the Oracle CDC Service.  
  
### Computer Administrator: Oracle CDC Service  
 The computer administrator is a Windows user responsible for creating and maintaining the CDC Service on the computer. This user must belong to the group of local machine administrators.  
  
 The tasks performed by the Oracle CDC Service Computer Administrator include:  
  
-   Installing the CDC Service for Oracle software  
  
-   Creating an Oracle CDC Windows service  
  
-   Setting up the CDC Service connection to the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (connection string and credentials)  
  
-   Ensuring that the CDC Service Master Password with which Oracle log mining credentials are protected  
  
-   Deleting a CDC Service Windows service  
  
-   Uninstalling the CDC Service for Oracle software  
  
-   Maintaining the CDC Service for Oracle software (for example, installing updates)  
  
-   Starting and stopping a CDC Service Windows service  
  
 When working with high-availability configurations, such as Microsoft failover clusters, the computer administrator must have additional responsibilities and permissions such as:  
  
-   Installation and maintenance of the CDC Service for Oracle software on all cluster nodes.  
  
-   Defining generic cluster service resources for the CDC Service' Windows service on the various cluster nodes.  
  
-   Acting as the computer administrator authorized as an administrator on the computer where the CDC Service for Oracle is installed. This person installs the CDC Service for Oracle and uses the CDC Service Configuration Console to configure a CDC Service for Oracle on a local computer.  
  
### Service Account: Oracle CDC Service  
 This is Oracle CDC Service Windows Service Account is a Windows account used for running the Oracle CDC Service (the Service Account).  
  
 The only required privilege necessary for the service account is to be able to use the Oracle client and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native client ODBC provider. This account does not need to access files unless required by specific providers (for example, if the Oracle client connection string references Oracle database instances in a **tnsnames.ora** file, then that file must be read-accessible to the service account).  
  
 When creating an Oracle CDC Service on Windows Vista or Windows Server 2008, the default service account is the NETWORK SERVICE account.  
  
 On Windows 7, Windows Server 2008 R2 and later, the default service account is NT Service\\<service-name>.  
  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs on another machine or is a clustered [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and there the service needs to connect to the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Windows authentication, then the service account should be a domain account.  
  
## SQL Server User Roles  
 The following describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user roles used by the Oracle CDC Service.  
  
### Oracle CDC Service Administrator  
 The CDC Service Administrator is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user with full control over the Oracle CDC Service artifacts in the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. The CDC Service Administrator uses the Oracle CDC Designer Console to design Oracle CDC Instances.  
  
 The CDC Service Administrator should be granted the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fixed server roles **public** and **dbcreator**.  
  
 The tasks performed by the CDC Service Administrator include:  
  
-   Preparing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to host Oracle CDC Instances (which are [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases). In this task, a special database called MSXDBCDC is created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   Creating an Oracle CDC Instance [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Task includes enabling the newly created [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database for CDC, which requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator (**sysadmin**).  
  
-   Designing an Oracle CDC Instance. This task includes providing information about the source Oracle database and captured tables, which requires an Oracle database administrator.  
  
-   Maintaining the Oracle CDC Instance over time, which includes adding/removing capture instances and updating configuration.  
  
-   Enabling or disabling an Oracle CDC Instance.  
  
-   Monitoring the state of an Oracle CDC Instance.  
  
-   Troubleshooting issues that affect the Oracle CDC Instance.  
  
 The CDC Service Administrator is, at least initially, in the **db_owner** fixed database role for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC database associated with the Oracle CDC Instance. This gives the CDC Service Administrator access to the change data stored in the CDC database. Once created, the **db_owner** role of the CDC database can be assigned to a different user who can carry out all of the tasks listed above except for preparing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and creating another Oracle CDC Instance).  
  
 The CDC Service Administrator does not need to know the master password specified with the creation of the Oracle CDC Windows service.  
  
### System Administrator  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user and should be granted the fixed server **sysadmin** role on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance associated with the Oracle CDC Service(s).  
  
 There is only one Oracle CDC specific task that carried out with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] System Administrator and that is to enable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database for an Oracle CDC Instance for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC. This task is performed using the Oracle CDC Designer console when creating a new Oracle CDC Instance.  
  
### Oracle CDC Service User  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Oracle CDC Service user is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login which is used by the Oracle CDC Service to perform its work against the MSXDBCDC and all of the Oracle CDC Instances (CDC databases) handled by this service.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Oracle CDC Service user should be granted the following:  
  
-   Member of the fixed database roles **db_dlladmin**, **db_datareader**, and **db_datawriter** for all CDC databases handled by the server.  
  
-   Member of the fixed database roles **db_datareader** and **db_datawriter** for the MSXDBCDC database.  
  
 Because the Oracle CDC Service uses a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to work with all CDC databases and the MSXDBCDC database, this login should be mapped in all of these databases.  
  
### Oracle CDC Change Consumer  
 The Oracle CDC Change Consumer is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user that consumes changes stored in the CDC tables in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Oracle CDC Instance database.  
  
 This user determines the user role that is required for accessing each of the CDC tables through the CDC functions generated by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC infrastructure. If no user role is specified when a capture instance is specified, access to the changes is limited to the member of the **db_owner** fixed database role of the CDC database.  
  
## Oracle User Roles  
 The following describes the Oracle user roles used by the Oracle CDC Service.  
  
### Database Administrator (DBA)  
 The Oracle database administrator (DBA) is an Oracle database user. The tasks performed by the Oracle DBA include:  
  
-   Setting the source Oracle database to work in ARCHIVELOG mode.  
  
-   Setting up a log mining user with the required permissions.  
  
-   Setting supplemental logging for captured tables.  
  
-   Helping to restore archived transaction log files no longer available so they can be processed.  
  
 The Oracle database administrator can get Oracle SQL scripts that need to run so they can be evaluated before running them. The Oracle database administrator can also directly run Oracle SQL scripts from the Oracle CDC Designer console.  
  
 If the Oracle database administrator chooses to use the Oracle CDC Designer console, administrator's credentials are not kept except for the context (dialog) in which they were used.  
  
 The Oracle database administrator works in coordination with the Oracle CDC Service administrator on the configuration of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Oracle CDC Instances.  
  
### Log Mining User  
 The Oracle Log Miner user is a special Oracle database user that is granted the required privileges for accessing and processing the Oracle transaction logs.  
  
 The credentials for this user are stored in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Oracle CDC Instance database using asymmetric key encryption. They are accessible only to the Oracle CDC Service but not to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Oracle CDC Instance database owner.  
  
 The following list describes the required privileges of the log mining user should be granted:  
  
-   SELECT on \<any-captured-table>  
  
-   SELECT ANY TRANSACTION  
  
-   EXECUTE on DBMS_LOGMNR  
  
-   SELECT on V$LOGMNR_CONTENTS  
  
-   SELECT on V$ARCHIVED_LOG  
  
-   SELECT on V$LOG  
  
-   SELECT on V$LOGFILE  
  
-   SELECT on V$DATABASE  
  
-   SELECT on V$THREAD  
  
-   SELECT on ALL_INDEXES  
  
-   SELECT on ALL_OBJECTS  
  
-   SELECT on DBA_OBJECTS  
  
-   SELECT on ALL_TABLES  
  
 If any of these privileges cannot be granted to a V$xxx, then they should be granted to the V $xxx.  
  
### Schema User  
 The Oracle Schema User is an Oracle user with read access to the schema of the Oracle tables to be captured. This user is necessary when working with the Oracle CDC Designer console to retrieve the list of Oracle schema, tables to be captured and their columns, indexes and keys.  
  
 The credentials for this user are never stored. They are requested by the CDC Designer console each time they are needed and they are kept for the rest of the UI sessions.  
  
  
