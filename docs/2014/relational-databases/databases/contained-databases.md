---
title: "Contained Databases | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "contained database"
  - "database_uncontained_usage event"
  - "partially contained database"
  - "contained database, understanding"
ms.assetid: 36af59d7-ce96-4a02-8598-ffdd78cdc948
author: stevestein
ms.author: sstein
manager: craigg
---
# Contained Databases
  A*contained database* is a database that is isolated from other databases and from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the database.  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] helps user to isolate their database from the instance in 4 ways.  
  
-   Much of the metadata that describes a database is maintained in the database. (In addition to, or instead of, maintaining metadata in the master database.)  
  
-   All metadata are defined using the same collation.  
  
-   User authentication can be performed by the database, reducing the databases dependency on the logins of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment (DMV's, XEvents, etc.) reports and can act upon containment information.  
  
 Some features of partially contained databases, such as storing metadata in the database, apply to all [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] databases. Some benefits of partially contained databases, such as database level authentication and catalog collation, must be enabled before they are available. Partial containment is enabled using the `CREATE DATABASE` and `ALTER DATABASE` statements or by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information about how to enable partial database containment, see [Migrate to a Partially Contained Database](migrate-to-a-partially-contained-database.md).  
  
 This topic contains the following sections.  
  
-   [Partially Contained Database Concepts](#Concepts)  
  
-   [Containment](#containment)  
  
-   [Benefits of using Partially Contained Databases](#benefits)  
  
-   [Limitations](#Limitations)  
  
-   [Identifying Database Containment](#Identifying)  
  
##  <a name="Concepts"></a> Partially Contained Database Concepts  
 A fully contained database includes all the settings and metadata required to define the database and has no configuration dependencies on the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] where the database is installed. In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], separating a database from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] could be time consuming and required detailed knowledge of the relationship between the database and the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Partially contained databases make it easier to separate a database from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and other databases.  
  
 The contained database considers features with regard to containment. Any user-defined entity that relies only on functions that reside in the database is considered fully contained. Any user-defined entity that relies on functions that reside outside the database is considered uncontained. (For more information, see the [Containment](#containment) section later in this topic.)  
  
 The following terms apply to the contained database model.  
  
 Database boundary  
 The boundary between a database and the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The boundary between a database and other databases.  
  
 Contained  
 An element that exists entirely in the database boundary.  
  
 Uncontained  
 An element that crosses the database boundary.  
  
 Non-contained database  
 A database that has containment set to **NONE**. All databases in versions earlier than [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] are non-contained. By default, all [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later databases have a containment set to **NONE**.  
  
 Partially contained database  
 A partially contained database is a contained database that can allow some features that cross the database boundary. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes the ability to determine when the containment boundary is crossed.  
  
 Contained user  
 There are two types of users for contained databases.  
  
-   **Contained database user with password**  
  
     Contained database users with passwords are authenticated by the database.  
  
-   **Windows principals**  
  
     Authorized Windows users and members of authorized Windows groups can connect directly to the database and do not need logins in the **master** database. The database trusts the authentication by Windows.  
  
 Users based on logins in the **master** database can be granted access to a contained database, but that would create a dependency on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Therefore, creating users based on logins see comment for partially contained databases.  
  
> [!IMPORTANT]  
>  Enabling partially contained databases delegates control over access to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the owners of the database. For more information, see [Security Best Practices with Contained Databases](security-best-practices-with-contained-databases.md).  
  
 Database Boundary  
 Because partially contained databases separate the database functionality from those of the instance, there is a clearly defined line between these two elements called the *database boundary*.  
  
 Inside of the database boundary is the *database model*, where the databases are developed and managed. Examples of entities located inside of the database include, system tables like **sys.tables**, contained database users with passwords, and user tables in the current database referenced by a two-part name.  
  
 Outside of the database boundary is the *management model*, which pertains to instance-level functions and management. Examples of entities located outside of the database boundary include, system tables like **sys.endpoints**, users mapped to logins, and user tables in another database referenced by a three-part-name.  
  
##  <a name="containment"></a> Containment  
 User entities that reside entirely within the database are considered *contained*. Any entities that reside outside of the database, or rely on interaction with functions outside of the database, are considered *uncontained*.  
  
 In general, user entities fall into the following categories of containment:  
  
-   Fully contained user entities (those that never cross the database boundary), for example sys.indexes. Any code that uses these features or any object that references only these entities is also fully contained.  
  
-   Uncontained user entities (those that cross the database boundary), for example sys.server_principals or a server principal (login) itself. Any code that uses these entities or any functions that references these entities are uncontained.  
  
###  <a name="partial"></a> Partially Contained Database  
 The contained database feature is currently available only in a partially contained state. A partially contained database is a contained database that allows the use of uncontained features.  
  
 Use the [sys.dm_db_uncontained_entities](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-uncontained-entities-transact-sql) and [sys.sql_modules &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sql-modules-transact-sql) view to return information about uncontained objects or features. By determining the containment status of the elements of your database, you can discover what objects or features must be replaced or altered to promote containment.  
  
> [!IMPORTANT]  
>  Because certain objects have a default containment setting of **NONE**, this view can return false positives.  
  
 The behavior of partially contained databases differs most distinctly from that of non-contained databases with regard to collation. For more information about collation issues, see [Contained Database Collations](contained-database-collations.md).  
  
##  <a name="benefits"></a> Benefits of using Partially Contained Databases  
 There are issues and complications associated with the non-contained databases that can be resolved by using a partially contained database.  
  
### Database Movement  
 One of the problems that occurs when moving databases, is that some important information can be unavailable when a database is moved from one instance to another. For example, login information is stored within the instance instead of in the database. When you move a non-contained database from one instance to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this information is left behind. You must identify the missing information and move it with your database to the new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This process can be difficult and time-consuming.  
  
 The partially contained database can store important information in the database so the database still has the information after it is moved.  
  
> [!NOTE]  
>  A partially contained database can provide documentation describing those features that are used by a database that cannot be separated from the instance. This includes a list of other interrelated databases, system settings that the database requires but cannot be contained, and so on.  
  
### Benefit of Contained Database Users with AlwaysOn  
 By reducing the ties to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], partially contained databases can be useful during failover when you use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].  
  
 Creating contained users enables the user to connect directly to the contained database. This is a very significant feature in high availability and disaster recovery scenarios such as in an AlwaysOn solution. If the users are contained users, in case of failover, people would be able to connect to the secondary without creating logins on the instance hosting the secondary. This provides an immediate benefit. For more information, see [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) and [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
### Initial Database Development  
 Because a developer may not know where a new database will be deployed, limiting the deployed environmental impacts on the database lessens the work and concern for the developer. In the non-contained model, the developer must consider possible environmental impacts on the new database and program accordingly. However, by using partially contained databases, developers can detect instance-level impacts on the database and instance-level concerns for the developer.  
  
### Database Administration  
 Maintaining database settings in the database, instead of in the master database, lets each database owner have more control over their database, without giving the database owner **sysadmin** permission.  
  
##  <a name="Limitations"></a> Limitations  
 Partially contained databases do not allow the following features.  
  
-   Partially contained databases cannot use replication, change data capture, or change tracking.  
  
-   Numbered procedures  
  
-   Schema-bound objects that depend on built-in functions with collation changes  
  
-   Binding change resulting from collation changes, including references to objects, columns, symbols, or types.  
  
-   Replication, change data capture, and change tracking.  
  
> [!WARNING]  
>  Temporary stored procedures are currently permitted. Because temporary stored procedures breach containment, they are not expected to be supported in future versions of contained database.  
  
##  <a name="Identifying"></a> Identifying Database Containment  
 There are two tools to help identify the containment status of the database. The [sys.dm_db_uncontained_entities &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-uncontained-entities-transact-sql) is a view that shows all the potentially uncontained entities in the database. The database_uncontained_usage event occurs when any actual uncontained entity is identified at run time.  
  
### sys.dm_db_uncontained_entities  
 This view shows any entities in the database that have the potential to be uncontained, such as those that cross-the database boundary. This includes those user entities that may use objects outside the database model. However, because the containment of some entities (for example, those using dynamic SQL) cannot be determined until run time, the view may show some entities that are not actually uncontained. For more information, see [sys.dm_db_uncontained_entities &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-uncontained-entities-transact-sql).  
  
### database_uncontained_usage event  
 This XEvent occurs whenever an uncontained entity is identified at run time. This includes entities originated in client code. This XEvent will occur only for actual uncontained entities. However, the event only occurs at run time. Therefore, any uncontained user entities you have not run will not be identified by this XEvent  
  
## Related Content  
 [Modified Features &#40;Contained Database&#41;](modified-features-contained-database.md)  
  
 [Contained Database Collations](contained-database-collations.md)  
  
 [Security Best Practices with Contained Databases](security-best-practices-with-contained-databases.md)  
  
 [Migrate to a Partially Contained Database](migrate-to-a-partially-contained-database.md)  
  
  
