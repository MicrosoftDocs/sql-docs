---
title: Configure File System Permissions for Database Engine Access
description: Learn about per-service SIDs. See how to grant them access permission to the database file location so that the Database Engine can access the database files.
author: rwestMSFT
ms.author: randolphwest
ms.date: "01/31/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Configure File System Permissions for Database Engine Access
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This article describes how to grant the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] file system access to the location where database files are stored. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] service must have permission of the Windows file system to access the file folder where database files are stored. Permission to the default location is configured during setup. If you place your database files in a different location, you might must follow these steps to grant the [!INCLUDE[ssDE](../../includes/ssde-md.md)] the full control permission to that location.  
  
 Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], permissions are assigned to the per-service SID for each of its services. This system helps provide service isolation and defense in depth. The per-service SID is derived from the service name and is unique to each service. The article [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md) describes the per-service SID and provides the names in the section **Windows Privileges and Rights**. It is the per-service SID that must be assigned the access permission on the file location.  
  
## Grant file system permission to the per-service SID  
  
1.  Using Windows Explorer, navigate to the file system location where the database files are stored. Right-click the file system folder, and then select **Properties**.  
  
2.  On the **Security** tab, select **Edit**, and then **Add**.  
  
3.  In the **Select Users, Computer, Service Account, or Groups** dialog box, select **Locations**, at the top of the location list, select your computer name, and then select **OK**.  
  
4.  In the **Enter the object names to select** box, type the name of the per-service SID name. To locate it, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). (For the [!INCLUDE[ssDE](../../includes/ssde-md.md)] per service SID name, use **NT SERVICE\MSSQLSERVER** for a default instance, or **NT SERVICE\MSSQL$InstanceName** for a named instance.)  
  
5.  Select **Check Names** to validate the entry. (If the validation fails, it might advise you that the name was not found. When you select **OK**, a **Multiple Names Found** dialog box appears. Now select the per-service SID name, either **NT SERVICE\MSSQLSERVER** or **NT SERVICE\MSSQL$InstanceName**, and then select **OK**. Select **OK** again to return to the **Permissions** dialog box.)   

6.  In the **Group or user** names box, select the per-service SID name, and then in the **Permissions for** \<name> box, select the **Allow** check box for **Full control**.  
  
7. Select **Apply**, and then select **OK** twice to exit.  
  
## See also

- [Move System Databases](../../relational-databases/databases/move-system-databases.md)   
- [Move User Databases](../../relational-databases/databases/move-user-databases.md)  

## Next steps

- [Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md)   
