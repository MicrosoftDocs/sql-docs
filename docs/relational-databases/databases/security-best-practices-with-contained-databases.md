---
title: "Security Best Practices with Contained Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "contained database, threats"
ms.assetid: 026ca5fc-95da-46b6-b882-fa20f765b51d
ms.author: vanto
manager: craigg
manager: craigg
---
# Security Best Practices with Contained Databases
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Contained databases have some unique threats that should be understood and mitigated by [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] administrators. Most of the threats are related to the **USER WITH PASSWORD** authentication process, which moves the authentication boundary from the [!INCLUDE[ssDE](../../includes/ssde-md.md)] level to the database level.  
  
## Threats Related to Users  
 Users in a contained database that have the **ALTER ANY USER** permission, such as members of the **db_owner** and **db_securityadmin** fixed database roles, can grant access to the database without the knowledge or permission or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator. Granting users access to a contained database increases the potential attack surface area against the whole [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Administrators should understand this delegation of access control, and be very careful about granting users in the contained database the **ALTER ANY USER** permission. All database owners have the **ALTER ANY USER** permission. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators should periodically audit the users in a contained database.  
  
### Accessing Other Databases Using the guest Account  
 Database owners and database users with the **ALTER ANY USER** permission can create contained database users. After connecting to a contained database on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a contained database user can access other databases on the [!INCLUDE[ssDE](../../includes/ssde-md.md)], if the other databases have enabled the **guest** account.  
  
### Creating a Duplicate User in Another Database  
 Some applications might require that a user to have access to more than one database. This can be done by creating identical contained database users in each database. Use the SID option when creating the second user with password. The following example creates two identical users in two databases.  
  
```  
USE DB1;  
GO  
CREATE USER Carlo WITH PASSWORD = '<strong password>';   
-- Return the SID of the user  
SELECT SID FROM sys.database_principals WHERE name = 'Carlo';  
  
-- Change to the second database  
USE DB2;  
GO  
CREATE USER Carlo WITH PASSWORD = '<same password>', SID = <SID from DB1>;  
GO  
```  
  
 To execute a cross-database query, you must set the **TRUSTWORTHY** option on the calling database. For example if the user (Carlo) defined above is in DB1, to execute `SELECT * FROM db2.dbo.Table1;` then the **TRUSTWORTHY** setting must be on for database DB1. Execute the following code to set the **TRUSTWORTHY** setting on.  
  
```  
ALTER DATABASE DB1 SET TRUSTWORTHY ON;  
```  
  
### Creating a User that Duplicates a Login  
 If a contained database user with password is created, using the same name as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, and if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login connects specifying the contained database as the initial catalog, then the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login will be unable to connect. The connection will be evaluated as the contained database user with password principal on the contained database instead of as a user based on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. This could cause an intentional or accidental denial of service for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
-   As a best practice, members of the **sysadmin** fixed server role should consider always connecting without using the initial catalog option. This connects the login to the master database and avoids any attempts by a database owner to misuse the login attempt. Then the administrator can change to the contained database by using the **USE**_\<database>_ statement. You can also set the default database of the login to the contained database, which completes the login to **master**, and then transfers the login to the contained database.  
  
-   As a best practice, do not create contained database users with passwords who have the same name as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
-   If the duplicate login exists, connect to the **master** database without specifying an initial catalog, and then execute the **USE** command to change to the contained database.  
  
-   When contained databases are present, users of databases that are not contained databases should connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] without using an initial catalog or by specifying the database name of a non-contained database as the initial catalog. This avoids connecting to the contained database which is under less direct control by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] administrators.  
  
### Increasing Access by Changing the Containment Status of a Database  
 Logins that have the **ALTER ANY DATABASE** permission, such as members of the **dbcreator** fixed server role, and users in a non-contained database that have the **CONTROL DATABASE** permission, such as members of the **db_owner** fixed database role, can change the containment setting of a database. If the containment setting of a database is changed from **NONE** to either **PARTIAL** or **FULL**, then user access can be granted by creating contained database users with passwords. This could provide access without the knowledge or consent of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators. To prevent any databases from being contained, set the [!INCLUDE[ssDE](../../includes/ssde-md.md)]**contained database authentication** option to 0. To prevent connections by contained database users with passwords on selected contained databases, use login triggers to cancel login attempts by contained database users with passwords.  
  
### Attaching a Contained Database  
 By attaching a contained database, an administrator could give unwanted users access to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. An administrator concerned about this risk can bring the database online in **RESTRICTED_USER** mode, which prevents authentication for contained database users with passwords. Only principals authorized through logins will be able to access the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 Users are created using the password requirements in effect at the time that they are created and passwords are not rechecked when a database is attached. By attaching a contained database which allowed weak passwords to a system with a stricter password policy, an administrator could permit passwords that do not meet the current password policy on the attaching [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Administrators can avoid retaining the weak passwords by requiring that all passwords be reset for the attached database.  
  
### Password Policies  
 Passwords in a database can be required to be strong passwords, but cannot be protected by robust password policies. Use Windows Authentication whenever possible to take advantage of the more extensive password policies available from Windows.  
  
### Kerberos Authentication  
 Contained database users with passwords cannot use Kerberos Authentication. When possible, use Windows Authentication to take advantage of Windows features such as Kerberos.  
  
### Offline Dictionary Attack  
 The password hashes for contained database users with passwords are stored in the contained database. Anyone with access to the database files could perform a dictionary attack against the contained database users with passwords on an unaudited system. To mitigate this threat, restrict access to the database files, or only permit connections to contained databases by using Windows Authentication.  
  
## Escaping a Contained Database  
 If a database is partially contained, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators should periodically audit the capabilities of the users and modules in contained databases.  
  
## Denial of Service Through AUTO_CLOSE  
 Do not configure contained databases to auto close. If closed, opening the database to authenticate a user consumes additional resources and could contribute to a denial of service attack.  
  
## See Also  
 [Contained Databases](../../relational-databases/databases/contained-databases.md)   
 [Migrate to a Partially Contained Database](../../relational-databases/databases/migrate-to-a-partially-contained-database.md)  
  
  
